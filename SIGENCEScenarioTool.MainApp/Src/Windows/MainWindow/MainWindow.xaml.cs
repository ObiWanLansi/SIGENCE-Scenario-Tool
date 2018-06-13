using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;

using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows.MainWindow
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
                settings.UDPHost = "127.0.0.1";
            }

            if( settings.UDPPortSending < 1025 || settings.UDPPortSending > 65535 )
            {
                MB.Warning( "The value in the configuration file for the setting UDPPort is invalid!\nPlease correct the value and restart the application." );
                settings.UDPPortSending = 4242;
            }

            if( settings.UDPDelay < 0 || settings.UDPDelay > 10000 )
            {
                MB.Warning( "The value in the configuration file for the setting UDPDelay is invalid!\nPlease correct the value and restart the application." );
                settings.UDPDelay = 500;
            }

            if( settings.MapZoomLevel < 1 || settings.MapZoomLevel > 20 )
            {
                MB.Warning( "The value in the configuration file for the setting MapZoomLevel is invalid!\nPlease correct the value and restart the application." );
                settings.MapZoomLevel = 18;
            }

            //-----------------------------------------------------------------

            this.RFDevicesCollection = new ObservableCollection<RFDeviceViewModel>();
            this.DataContext = this;
            this.PropertyChanged += MainWindow_PropertyChanged;

            //-----------------------------------------------------------------

            InitMapControl();
            InitMapProvider();
            InitCommands();
            InitUDPServer();
            InitFileOpenSaveDialogs();

            //-----------------------------------------------------------------

            SetTitle();

            //-----------------------------------------------------------------

#if DEBUG
            CreateRandomizedRFDevices( 10 );
            //ScenarioDescription = "<h3>Enter a short description of the scenario here ...</h3>";
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            CurrentFile = null;

            RFDevicesCollection.Clear();
            mcMapControl.Markers.Clear();
            ScenarioDescription = "";

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
        /// 
        /// </summary>
        private void OpenCheatSheet()
        {
            new HelpWindow.HelpWindow().Show();
        }


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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEditMode"></param>
        private void SwitchScenarioEditMode( bool bEditMode )
        {
            wbScenarioDescription.Visibility = bEditMode ? Visibility.Hidden : Visibility.Visible;
            tbScenarioDescription.Visibility = bEditMode ? Visibility.Visible : Visibility.Hidden;
            btnEditScenarioDescription.IsEnabled = !bEditMode;
            btnViewScenarioDescription.IsEnabled = bEditMode;
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpdateScenarioDescription()
        {
            if( string.IsNullOrEmpty( ScenarioDescription ) == false )
            {
                wbScenarioDescription.NavigateToString( ScenarioDescription );
            }
            else
            {
                wbScenarioDescription.NavigateToString( "<html/>" );
            }
        }

    } // end public partial class MainWindow
}
