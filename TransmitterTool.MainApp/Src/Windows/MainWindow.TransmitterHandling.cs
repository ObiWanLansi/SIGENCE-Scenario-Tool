using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

using GMap.NET;

using TransmitterTool.Extensions;
using TransmitterTool.Models;
using TransmitterTool.Tools;
using TransmitterTool.ViewModels;



namespace TransmitterTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Sets the map to creating transmitter mode.
        /// </summary>
        private void SetMapToCreatingTransmitterMode()
        {
            mcMapControl.DragButton = bCreatingTransmitter ? MouseButton.Right : MouseButton.Left;
        }


        /// <summary>
        /// Begins the create transmitter.
        /// </summary>
        private void BeginCreateTransmitter()
        {
            CreatingTransmitter = true;
            mcMapControl.Cursor = Cursors.Cross;
        }


        /// <summary>
        /// Ends the create transmitter.
        /// </summary>
        private void EndCreateTransmitter()
        {
            mcMapControl.Cursor = Cursors.Arrow;
            CreatingTransmitter = false;
        }


        /// <summary>
        /// Creates the transmitter.
        /// </summary>
        /// <param name="pll">The PLL.</param>
        private void AddTransmitter( PointLatLng pll )
        {
            AddTransmitter( new Transmitter
            {
                Latitude = pll.Lat ,
                Longitude = pll.Lng
            } );
        }


        /// <summary>
        /// Adds the transmitter.
        /// </summary>
        /// <param name="t">The t.</param>
        private void AddTransmitter( Transmitter t )
        {
            TransmitterViewModel tvm = new TransmitterViewModel( t );

            TransmitterCollection.Add( tvm );
            mcMapControl.Markers.Add( tvm.Marker );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Deletes the transmitter.
        /// </summary>
        /// <param name="tvm">The TVM.</param>
        private void DeleteTransmitter( TransmitterViewModel tvm )
        {
            TransmitterCollection.Remove( tvm );
            mcMapControl.Markers.Remove( tvm.Marker );
        }


        /// <summary>
        /// Deletes the transmitter.
        /// </summary>
        private void DeleteTransmitter()
        {
            if( dgTransmitter.SelectedItem != null )
            {
                DeleteTransmitter( dgTransmitter.SelectedItem as TransmitterViewModel );
            }
            else
            {
                MB.Information( "No Transmitter Is Selected In The DataGrid!" );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Exports the transmitter.
        /// </summary>
        private void ExportTransmitter()
        {
            if( TransmitterCollection.Count == 0 )
            {
                MB.Warning( "No transmitter avaible for export!" );
                return;
            }

            if( CurrentFile != null )
            {
                sfdExportTransmitter.FileName = new FileInfo( CurrentFile ).Name;
            }
            else
            {
                sfdExportTransmitter.FileName = DateTime.Now.Fmt_YYYYMMDDHHMMSS();
            }

            if( sfdExportTransmitter.ShowDialog() == true )
            {
                FileInfo fiExportFile = new FileInfo( sfdExportTransmitter.FileName );

                List<Transmitter> tl = TransmitterCollection.Select( t => t.Transmitter ).ToList();

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
        /// Imports the transmitter.
        /// </summary>
        private void ImportTransmitter()
        {
            MB.NotYetImplemented();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The randomizer.
        /// </summary>
        static private readonly Random r = new Random();


        /// <summary>
        /// Creates the randomized transmitter.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        private void CreateRandomizedTransmitter( int iMaxCount )
        {
            Cursor = Cursors.Wait;
            DoEvents();

            for( int iCounter = 1 ; iCounter < iMaxCount + 1 ; iCounter++ )
            {
                Transmitter t = new Transmitter
                {
                    Id = r.Next( -1000 , 1000 ) ,
                    Name = string.Format( "Transmitter #{0}" , iCounter ) ,
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

                AddTransmitter( t );
            }

            Cursor = Cursors.Arrow;
        }

    } // end public partial class MainWindow
}
