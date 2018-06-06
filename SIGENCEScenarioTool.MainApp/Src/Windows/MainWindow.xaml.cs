﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //-----------------------------------------------------------------

            if( string.IsNullOrEmpty( settings.UDPHost ) )
            {
                MB.Warning( "The value in the configuration file for the setting UDPHost is invalid!\nPlease correct the value and restart the application." );
                //Application.Current.Shutdown( -1 );
                settings.UDPHost = "127.0.0.1";
            }


            if( settings.UDPPort < 1025 || settings.UDPPort > 65535 )
            {
                MB.Warning( "The value in the configuration file for the setting UDPPort is invalid!\nPlease correct the value and restart the application." );
                //Application.Current.Shutdown( -1 );
                settings.UDPPort = 4242;
            }

            if( settings.UDPDelay < 0 || settings.UDPDelay > 10000 )
            {
                MB.Warning( "The value in the configuration file for the setting UDPDelay is invalid!\nPlease correct the value and restart the application." );
                //Application.Current.Shutdown( -1 );
                settings.UDPDelay = 500;
            }

            if( settings.MapZoomLevel < 1 || settings.MapZoomLevel > 20 )
            {
                MB.Warning( "The value in the configuration file for the setting MapZoomLevel is invalid!\nPlease correct the value and restart the application." );
                //Application.Current.Shutdown( -1 );
                settings.MapZoomLevel = 18;
            }

            //-----------------------------------------------------------------

            this.RFDevicesCollection = new ObservableCollection<RFDeviceViewModel>();
            this.DataContext = this;

            //-----------------------------------------------------------------

            InitMapControl();
            InitMapProvider();
            InitCommands();

            //-----------------------------------------------------------------

            sfdSaveSIGENCEScenario.Title = "Save SIGENCE Scenario Tool File";
            sfdSaveSIGENCEScenario.Filter = "SIGINT SIGENCE Scenario Tool File (*.stf)|*.stf";
            sfdSaveSIGENCEScenario.AddExtension = true;
            sfdSaveSIGENCEScenario.CheckPathExists = true;

            ofdLoadSIGENCEScenario.Title = "Load SIGENCE Scenario Tool File";
            ofdLoadSIGENCEScenario.Filter = "SIGINT SIGENCE Scenario ToolFile (*.stf)|*.stf";
            ofdLoadSIGENCEScenario.AddExtension = true;
            ofdLoadSIGENCEScenario.CheckPathExists = true;
            ofdLoadSIGENCEScenario.CheckFileExists = true;
            ofdLoadSIGENCEScenario.Multiselect = false;

            //-----------------------------------------------------------------

            sfdExportRFDevices.Title = "Export SIGENCE Scenario Tool File";
            sfdExportRFDevices.Filter = "Comma Separated Values (*.csv)|*.csv|Extensible Markup Language (*.xml)|*.xml|JavaScript Object Notation (*.json)|*.json";
#if EXCEL_SUPPORT
            sfdExportRFDevices.Filter += "|Office Open XML File Format (*.xlsx)|*.xlsx";
#endif
            sfdExportRFDevices.AddExtension = true;
            sfdExportRFDevices.CheckPathExists = true;

            //-----------------------------------------------------------------

            sfdSaveScreenshot.Title = "Save Screenshot";
            sfdSaveScreenshot.Filter = "Portable Network Graphics (*.png)|*.png";
            sfdSaveScreenshot.AddExtension = true;
            sfdSaveScreenshot.CheckPathExists = true;

            //-----------------------------------------------------------------

            //System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;

            //this.Width = screen.WorkingArea.Width * 0.6666;
            //this.Height = screen.WorkingArea.Height * 0.6666;

            SetTitle();

            //-----------------------------------------------------------------

#if DEBUG
            CreateRandomizedRFDevices( 10 );
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sends the RFDeviceList via UDP to any connect client.
        /// This function is not asynchron, so the main thread is blocked when sending data. 
        /// Maybe in oen of the next versions me make this function asynchron.
        /// </summary>
        private void SendDataUDP()
        {
            //Task.Run( () =>
            //     Speech.Say( "Starting transfer of radio frequency devices" )
            //);

            try
            {
                using( Socket sender = new Socket( AddressFamily.InterNetwork , SocketType.Dgram , ProtocolType.Udp ) )
                {
                    IPEndPoint endpoint = new IPEndPoint( IPAddress.Parse( settings.UDPHost ) , settings.UDPPort );

                    foreach( RFDevice device in from devicemodel in RFDevicesCollection where devicemodel.IsSelected == true select devicemodel.RFDevice )
                    {
                        XElement eDevice = device.ToXml();

                        byte [] baMessage = Encoding.Default.GetBytes( eDevice.ToDefaultString() );

                        sender.SendTo( baMessage , endpoint );

                        // Give the poor client some time to process the data when he need or bleed ...
                        if( settings.UDPDelay > 0 )
                        {
                            Thread.Sleep( settings.UDPDelay );
                        }
                    }

                    sender.Close();
                }
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }

            //Task.Run( () =>
            //    Speech.Say( "Finished with sending the data" )
            //);
        }


        ///// <summary>
        ///// Sends the RFDeviceList via UDP to any connect client.
        ///// This function is not asynchron, so the main thread is blocked when sending data. 
        ///// Maybe in oen of the next versions me make this function asynchron.
        ///// </summary>
        //private void xSendDataUDP()
        //{
        //    XElement eDeviceList = new XElement( "DeviceList" );

        //    foreach( RFDevice device in from devicemodel in RFDevicesCollection select devicemodel.RFDevice )
        //    {
        //        eDeviceList.Add( device.ToXml() );
        //    }

        //    try
        //    {
        //        using( Socket sender = new Socket( AddressFamily.InterNetwork , SocketType.Dgram , ProtocolType.Udp ) )
        //        {
        //            IPEndPoint endpoint = new IPEndPoint( IPADDRESS , settings.UDPPort );

        //            byte [] baMessage = Encoding.Default.GetBytes( eDeviceList.ToDefaultString() );

        //            sender.SendTo( baMessage , endpoint );

        //            sender.Close();
        //        }
        //    }
        //    catch( Exception ex )
        //    {
        //        MB.Error( ex );
        //    }
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            CurrentFile = null;

            RFDevicesCollection.Clear();
            mcMapControl.Markers.Clear();

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        /// <summary>
        /// Sets the title.
        /// </summary>
        private void SetTitle()
        {
            this.Title = string.Format( "{0} ({1}){2}" , Tool.ProductTitle , Tool.Version , CurrentFile != null ? string.Format( " [{0}]" , CurrentFile ) : "" );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the screenshot.
        /// </summary>
        private void CreateScreenshot()
        {
            try
            {
                if( CurrentFile != null )
                {
                    sfdSaveScreenshot.FileName = new FileInfo( CurrentFile ).Name;
                }

                if( sfdSaveScreenshot.ShowDialog() == true )
                {
                    var screenshot = Tools.Windows.GetWPFScreenshot( mcMapControl );

                    PngBitmapEncoder encoder = new PngBitmapEncoder();

                    encoder.Frames.Add( BitmapFrame.Create( screenshot ) );

                    using( BufferedStream bs = new BufferedStream( new FileStream( sfdSaveScreenshot.FileName , FileMode.Create ) ) )
                    {
                        encoder.Save( bs );
                    }

                    Tools.Windows.OpenWithDefaultApplication( sfdSaveScreenshot.FileName );
                }
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }
        }

    } // end public partial class MainWindow
}
