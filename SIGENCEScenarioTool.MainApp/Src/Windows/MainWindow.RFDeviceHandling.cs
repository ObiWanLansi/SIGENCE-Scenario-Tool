using System;
using System.IO;
using System.Linq;
using System.Windows.Input;

using GMap.NET;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Sets the map to creating RFDevice mode.
        /// </summary>
        private void SetMapToCreatingRFDeviceMode()
        {
            mcMapControl.DragButton = bCreatingRFDevice ? MouseButton.Right : MouseButton.Left;
        }


        /// <summary>
        /// Begins the create RFDevice.
        /// </summary>
        private void BeginCreateRFDevice()
        {
            CreatingRFDevice = true;
            mcMapControl.Cursor = Cursors.Cross;
        }


        /// <summary>
        /// Ends the create RFDevice.
        /// </summary>
        private void EndCreateRFDevice()
        {
            mcMapControl.Cursor = Cursors.Arrow;
            CreatingRFDevice = false;
        }


        /// <summary>
        /// Creates the RFDevice.
        /// </summary>
        /// <param name="pll">The PLL.</param>
        private void AddRFDevice( PointLatLng pll )
        {
            AddRFDevice( new RFDevice
            {
                Latitude = pll.Lat ,
                Longitude = pll.Lng
            } );
        }


        /// <summary>
        /// Adds the RFDevice.
        /// </summary>
        /// <param name="t">The t.</param>
        private void AddRFDevice( RFDevice t )
        {
            RFDeviceViewModel tvm = new RFDeviceViewModel( t );

            RFDevicesCollection.Add( tvm );
            mcMapControl.Markers.Add( tvm.Marker );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Deletes the RFDevice.
        /// </summary>
        /// <param name="tvm">The TVM.</param>
        private void DeleteRFDevice( RFDeviceViewModel tvm )
        {
            RFDevicesCollection.Remove( tvm );
            mcMapControl.Markers.Remove( tvm.Marker );
        }


        /// <summary>
        /// Deletes the RFDevice.
        /// </summary>
        private void DeleteRFDevice()
        {
            if( dgRFDevices.SelectedItem != null )
            {
                DeleteRFDevice( dgRFDevices.SelectedItem as RFDeviceViewModel );
            }
            else
            {
                MB.Information( "No RFDevice Is Selected In The DataGrid!" );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Exports the RFDevices.
        /// </summary>
        private void ExportRFDevices()
        {
            if( RFDevicesCollection.Count == 0 )
            {
                MB.Warning( "No RFDevice Avaible For Export!" );
                return;
            }

            if( CurrentFile != null )
            {
                sfdExportRFDevices.FileName = new FileInfo( CurrentFile ).Name;
            }
            else
            {
                sfdExportRFDevices.FileName = DateTime.Now.Fmt_YYYYMMDDHHMMSS();
            }

            if( sfdExportRFDevices.ShowDialog() == true )
            {
                FileInfo fiExportFile = new FileInfo( sfdExportRFDevices.FileName );

                //List<RFDevice> tl = TransmitterCollection.Select( t => t.Transmitter ).ToList();
                RFDeviceList tl = new RFDeviceList( RFDevicesCollection.Select( t => t.RFDevice ) );

                Cursor = Cursors.Wait;
                DoEvents();

                try
                {
                    switch( fiExportFile.Extension.ToLower() )
                    {
                        case ".csv":
                            tl.SaveAsCsv( fiExportFile.FullName );
                            break;

                        case ".json":
                            tl.SaveAsJson( fiExportFile.FullName );
                            break;

                        case ".xml":
                            tl.SaveAsXml( fiExportFile.FullName );
                            break;

#if EXCEL_SUPPORT
                        case ".xlsx":
                            tl.SaveAsExcel( fiExportFile.FullName );
                            break;
#endif
                    }

                    //MB.Information( "File {0} successful created." , fiExportFile.Name );
                }
                catch( Exception ex )
                {
                    MB.Error( ex );
                }

                Cursor = Cursors.Arrow;
            }
        }


        /// <summary>
        /// Imports the RFDevices.
        /// </summary>
        private void ImportRFDevices()
        {
            MB.NotYetImplemented();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The randomizer.
        /// </summary>
        static private readonly Random r = new Random();


        /// <summary>
        /// Creates the randomized RFDevices.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        private void CreateRandomizedRFDevices( int iMaxCount )
        {
            Cursor = Cursors.Wait;
            DoEvents();

            for( int iCounter = 1 ; iCounter < iMaxCount + 1 ; iCounter++ )
            {
                RFDevice t = new RFDevice
                {
                    Id = r.Next( -1000 , 1000 ) ,
                    Name = string.Format( "RFDevice #{0}" , iCounter ) ,
                    Latitude = ( r.NextDouble() * 0.05 ) + 49.7454 ,
                    Longitude = ( r.NextDouble() * 0.05 ) + 6.6149 ,
                    Altitude = 0 ,
                    RxTxType = r.NextEnum<RxTxType>() ,
                    AntennaType = r.NextEnum<AntennaType>() ,
                    CenterFrequency_Hz = ( uint ) r.Next( 85 , 105 ) ,
                    Bandwith_Hz = ( uint ) r.Next( 10 , 80 ) ,
                    Gain_dB = 0 ,
                    SignalToNoiseRatio_dB = 0 ,
                    Roll = 0 ,
                    Pitch = 0 ,
                    Yaw = 0 ,
                    XPos = 0 ,
                    YPos = 0 ,
                    ZPos = 0 ,
                    Remark = r.NextObject( Tool.ALLPANGRAMS )
                };

                AddRFDevice( t );
            }

            Cursor = Cursors.Arrow;
        }

    } // end public partial class MainWindow
}
