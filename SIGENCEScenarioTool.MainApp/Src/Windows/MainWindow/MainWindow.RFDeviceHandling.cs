using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

using GMap.NET;

using SIGENCEScenarioTool.Datatypes.Standard;
using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;

using Excel = Microsoft.Office.Interop.Excel;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Gets the device list.
        /// </summary>
        /// <returns>A RFDeviceList with all selected RFDevices from the DataGrid.</returns>
        private RFDeviceList GetDeviceList()
        {
            return new RFDeviceList( from devicemodel in RFDevicesCollection where devicemodel.IsSelected == true select devicemodel.RFDevice );
        }


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
        private void AddRFDevice( RFDevice d )
        {
            AddRFDevice( new RFDeviceViewModel( this.mcMapControl , d ) );
        }


        /// <summary>
        /// Adds the RFDeviceViewModel.
        /// </summary>
        /// <param name="dvm"></param>
        private void AddRFDevice( RFDeviceViewModel dvm )
        {
            RFDevicesCollection.Add( dvm );
            mcMapControl.Markers.Add( dvm.Marker );
        }


        /// <summary>
        /// Adds the rf devices.
        /// </summary>
        /// <param name="devices">The devices.</param>
        private void AddRFDevices( RFDeviceList devices )
        {
            devices.ForEach( d => AddRFDevice( d ) );
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
            if( dgRFDevices.SelectedItem == null )
            {
                MB.Information( "No RFDevice Is Selected In The DataGrid!" );
                return;
            }

            if( dgRFDevices.SelectedItems.Count > 1 )
            {
                MB.Information( "There Are More Than One RFDevice Selected In The DataGrid!" );
                return;
            }

            DeleteRFDevice( dgRFDevices.SelectedItem as RFDeviceViewModel );
        }


        /// <summary>
        /// Deletes the RFDevices.
        /// </summary>
        private void DeleteRFDevices()
        {
            foreach( RFDeviceViewModel device in ( from devicemodel in RFDevicesCollection where devicemodel.IsSelected == true select devicemodel ).ToList() )
            {
                DeleteRFDevice( device );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region Specialied Excel Import

        /// <summary>
        /// Loads from excel.
        /// </summary>
        /// <param name="strInputFilename">The string input filename.</param>
        private void LoadFromExcel( string strInputFilename )
        {
            if( strInputFilename.IsEmpty() )
            {
                throw new ArgumentException( "The input filename can not be empty!" , "strInputFilename" );
            }

            Excel.Application excel = new Excel.Application();

            try
            {
                Excel.Workbook wb = excel.Workbooks.Open( strInputFilename );

                // We guess, the RF Device Data is on sheet #1, starting @ row 1 ...
                Excel.Worksheet maindatasheet = wb.Sheets [1] as Excel.Worksheet;

                Excel.Range range = maindatasheet.UsedRange;

                int iColumnCount = range.Columns.Count;

                if( iColumnCount < 17 )
                {
                    throw new Exception( string.Format( "The Current Excel File Can Not Be Imported Because There Are Only {0} Columns!\nWe Need At Least 17 Columns For A Good Import." , iColumnCount ) );
                }

                int iRowCount = range.Rows.Count;

                if( iRowCount < 2 )
                {
                    throw new Exception( string.Format( "The Current Excel File Can Not Be Imported Because There Are Only {0} Rows!\nWe Need At Least 2 Rows For A Good Import." , iRowCount ) );
                }

                RFDeviceList dlImportedDevices = new RFDeviceList();

                for( int iRow = 2 ; iRow < iRowCount + 1 ; iRow++ )
                {
                    RFDevice device = new RFDevice();

                    for( int iColumn = 1 ; iColumn < 17 + 1 ; iColumn++ )
                    {
                        object value = ( range.Cells [iRow , iColumn] as Excel.Range ).Value2;

                        if( value == null )
                        {
                            continue;
                        }

                        // Hier ist zu überlegen wie wir das ganze generisch machen können falls sich das Model nochmal ändert ...
                        switch( iColumn )
                        {
                            case 1:
                                device.StartTime = Convert.ToDouble( value );
                                break;

                            case 2:
                                device.Id = Convert.ToInt32( value );
                                break;

                            case 3:
                                device.Latitude = Convert.ToDouble( value );
                                break;

                            case 4:
                                device.Longitude = Convert.ToDouble( value );
                                break;

                            case 5:
                                device.Altitude = Convert.ToUInt32( value );
                                break;

                            case 6:
                                device.Roll = Convert.ToDouble( value );
                                break;

                            case 7:
                                device.Pitch = Convert.ToDouble( value );
                                break;

                            case 8:
                                device.Yaw = Convert.ToDouble( value );
                                break;

                            case 9:
                                device.RxTxType = ( RxTxType ) Convert.ToInt32( value );
                                break;

                            case 10:
                                device.AntennaType = ( AntennaType ) Convert.ToInt32( value );
                                break;

                            case 11:
                                device.Gain_dB = Convert.ToInt32( value );
                                break;

                            case 12:
                                device.CenterFrequency_Hz = Convert.ToUInt32( value );
                                break;

                            case 13:
                                device.Bandwith_Hz = Convert.ToUInt32( value );
                                break;

                            case 14:
                                device.SignalToNoiseRatio_dB = Convert.ToUInt32( value );
                                break;

                            case 15:
                                device.XPos = Convert.ToInt32( value );
                                break;

                            case 16:
                                device.YPos = Convert.ToInt32( value );
                                break;

                            case 17:
                                device.ZPos = Convert.ToInt32( value );
                                break;
                        }
                    }

                    dlImportedDevices.Add( device );
                }

                AddRFDevices( dlImportedDevices );
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }

            excel.Quit();
            excel = null;

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion


        #region Specialied Excel Export

        /// <summary>
        /// Adds the cell.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="iColumn">The i column.</param>
        /// <param name="iRow">The i row.</param>
        /// <param name="value">The value.</param>
        private void AddCell( Excel.Worksheet sheet , int iColumn , int iRow , object value )
        {
            Excel.Range cell = sheet.Cells [iRow , iColumn] as Excel.Range;
            cell.Value2 = value;
            cell.HorizontalAlignment = value is string ? Excel.XlHAlign.xlHAlignLeft : Excel.XlHAlign.xlHAlignRight;
        }


        /// <summary>
        /// Saves as excel file.
        /// </summary>
        /// <param name="dl">The dl.</param>
        /// <param name="strOutputFilename">The string output filename.</param>
        /// <exception cref="ArgumentException">Der Ausgabedateiname darf nicht leer sein! - strOutputFilename</exception>
        private void SaveAsExcel( RFDeviceList dl , string strOutputFilename )
        {
            if( strOutputFilename.IsEmpty() )
            {
                throw new ArgumentException( "The output filename can not be empty!" , "strOutputFilename" );
            }

            //-----------------------------------------------------------------

            Excel.Application excel = new Excel.Application
            {
                SheetsInNewWorkbook = 1
            };

            Excel.Workbook wb = excel.Workbooks.Add( Missing );

            //-----------------------------------------------------------------

            Excel.Worksheet maindatasheet = wb.Sheets [1] as Excel.Worksheet;

            maindatasheet.Name = "RF Devices";

            StringList slColumnNames = new StringList
            {
                "Time","ID",
                "Lat","Long","Alt",
                "Roll","Pitch","Yaw",
                "RxTxType","AntType",
                "Gain","CenterFreq","BandWith","SNR",
                "x","y","z",
                "Remark"
            };

            // Create Header Columns
            {
                int iColumnCounter = 1;

                foreach( string strColumn in slColumnNames )
                {
                    Excel.Range cell = maindatasheet.Cells [1 , iColumnCounter++] as Excel.Range;
                    cell.Font.Bold = true;
                    cell.Orientation = Excel.XlOrientation.xlUpward;
                    cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    cell.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                    cell.Value2 = " " + strColumn;
                }
            }

            //-----------------------------------------------------------------

            // Create Data Columns And Rows
            {
                int iRowCounter = 2;

                foreach( RFDevice device in dl )
                {
                    AddCell( maindatasheet , 1 , iRowCounter , device.StartTime );
                    AddCell( maindatasheet , 2 , iRowCounter , device.Id );
                    AddCell( maindatasheet , 3 , iRowCounter , device.Latitude );
                    AddCell( maindatasheet , 4 , iRowCounter , device.Longitude );
                    AddCell( maindatasheet , 5 , iRowCounter , device.Altitude );
                    AddCell( maindatasheet , 6 , iRowCounter , device.Roll );
                    AddCell( maindatasheet , 7 , iRowCounter , device.Pitch );
                    AddCell( maindatasheet , 8 , iRowCounter , device.Yaw );
                    AddCell( maindatasheet , 9 , iRowCounter , device.RxTxType );
                    AddCell( maindatasheet , 10 , iRowCounter , device.AntennaType );
                    AddCell( maindatasheet , 11 , iRowCounter , device.Gain_dB );
                    AddCell( maindatasheet , 12 , iRowCounter , device.CenterFrequency_Hz );
                    AddCell( maindatasheet , 13 , iRowCounter , device.Bandwith_Hz );
                    AddCell( maindatasheet , 14 , iRowCounter , device.SignalToNoiseRatio_dB );
                    AddCell( maindatasheet , 15 , iRowCounter , device.XPos );
                    AddCell( maindatasheet , 16 , iRowCounter , device.YPos );
                    AddCell( maindatasheet , 17 , iRowCounter , device.ZPos );
                    AddCell( maindatasheet , 18 , iRowCounter , device.Remark );

                    iRowCounter++;
                }
            }

            maindatasheet.Columns.AutoFit();

            //-----------------------------------------------------------------

            if( string.IsNullOrEmpty( ScenarioDescription ) == false )
            {
                if( wb.Sheets.Count < 2 )
                {
                    wb.Sheets.Add( Missing , maindatasheet );
                }

                Excel.Worksheet descriptionsheet = wb.Sheets [2] as Excel.Worksheet;
                descriptionsheet.Name = "Scenario Description";
                Excel.Range cell = descriptionsheet.Cells [1 , 1] as Excel.Range;

                Clipboard.SetDataObject( ScenarioDescription );
                cell.PasteSpecial();
            }

            //-----------------------------------------------------------------

            excel.Visible = true;

            wb.SaveAs( strOutputFilename , Missing , Missing , Missing , Missing , Missing , Excel.XlSaveAsAccessMode.xlNoChange , Missing , Missing , Missing , Missing , Missing );

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Imports the rf devices.
        /// </summary>
        /// <param name="fiImportFile">The fi import file.</param>
        private void ImportRFDevices( FileInfo fiImportFile )
        {
            Cursor = Cursors.Wait;

            try
            {
                switch( fiImportFile.Extension.ToLower() )
                {
                    //case ".csv":
                    //    devicelist.SaveAsCsv(fiExportFile.FullName);
                    //    MB.Information("File {0} successful created.", fiExportFile.Name);
                    //    break;

                    //case ".json":
                    //    devicelist.SaveAsJson(fiExportFile.FullName);
                    //    MB.Information("File {0} successful created.", fiExportFile.Name);
                    //    break;

                    //case ".xml":
                    //    devicelist.SaveAsXml(fiExportFile.FullName);
                    //    MB.Information("File {0} successful created.", fiExportFile.Name);
                    //    break;

                    case ".xlsx":
                        LoadFromExcel( fiImportFile.FullName );
                        break;

                    default:
                        MB.Information( "Currently The Import Of {0} Is Not Implemented!" , fiImportFile.Extension );
                        break;
                }
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Exports the RFDevices.
        /// </summary>
        /// <param name="devicelist">The devicelist.</param>
        /// <param name="fiExportFile">The fi export file.</param>
        private void ExportRFDevices( RFDeviceList devicelist , FileInfo fiExportFile )
        {
            Cursor = Cursors.Wait;

            try
            {
                switch( fiExportFile.Extension.ToLower() )
                {
                    case ".csv":
                        devicelist.SaveAsCsv( fiExportFile.FullName );
                        MB.Information( "File {0} successful created." , fiExportFile.Name );
                        break;

                    case ".json":
                        devicelist.SaveAsJson( fiExportFile.FullName );
                        MB.Information( "File {0} successful created." , fiExportFile.Name );
                        break;

                    case ".xml":
                        devicelist.SaveAsXml( fiExportFile.FullName );
                        MB.Information( "File {0} successful created." , fiExportFile.Name );
                        break;

                    case ".xlsx":
                        SaveAsExcel( devicelist , fiExportFile.FullName );
                        break;
                }
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Exports the RFDevices.
        /// </summary>
        private void ExportRFDevices()
        {
            RFDeviceList devicelist = GetDeviceList();

            if( devicelist.Count == 0 )
            {
                MB.Warning( "No Selected RFDevice Avaible For Export!" );
                return;
            }

            sfdExportRFDevices.FileName = CurrentFile != null ? new FileInfo( CurrentFile ).GetFilenameWithoutExtension() : DateTime.Now.Fmt_YYYYMMDDHHMMSS();

            if( sfdExportRFDevices.ShowDialog() == true )
            {
                ExportRFDevices( devicelist , new FileInfo( sfdExportRFDevices.FileName ) );
            }
        }


        /// <summary>
        /// Imports the RFDevices.
        /// </summary>
        private void ImportRFDevices()
        {
            if( ofdImportRFDevices.ShowDialog() == true )
            {
                ImportRFDevices( new FileInfo( ofdImportRFDevices.FileName ) );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Zooms to RFDevice.
        /// </summary>
        /// <param name="device">The device.</param>
        private void ZoomToRFDevice( RFDevice device )
        {
            mcMapControl.Position = new PointLatLng( device.Latitude , device.Longitude );
            mcMapControl.Zoom = settings.MapZoomLevel;
        }


        /// <summary>
        /// Zooms to RFDevice.
        /// </summary>
        private void ZoomToRFDevice()
        {
            if( dgRFDevices.SelectedItem == null )
            {
                MB.Information( "No RFDevice Is Selected In The DataGrid!" );
                return;
            }

            if( dgRFDevices.SelectedItems.Count > 1 )
            {
                MB.Information( "There Are More Than One RFDevice Selected In The DataGrid!" );
                return;
            }

            ZoomToRFDevice( ( dgRFDevices.SelectedItem as RFDeviceViewModel ).RFDevice );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sends the RFDeviceList via UDP to any connect client.
        /// This function is not asynchron, so the main thread is blocked when sending data. 
        /// Maybe in oen of the next versions me make this function asynchron.
        /// </summary>
        /// <param name="devicelist">The devicelist.</param>
        private void SendDataUDP( RFDeviceList devicelist )
        {
            try
            {
                using( Socket sender = new Socket( AddressFamily.InterNetwork , SocketType.Dgram , ProtocolType.Udp ) )
                {
                    IPEndPoint endpoint = new IPEndPoint( IPAddress.Parse( settings.UDPHost ) , settings.UDPPortSending );

                    foreach( RFDevice device in devicelist )
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
        }


        /// <summary>
        /// Sends the data UDP.
        /// </summary>
        private void SendDataUDP()
        {
            RFDeviceList devicelist = GetDeviceList();

            if( devicelist.Count == 0 )
            {
                MB.Warning( "No Selected RFDevice Avaible For Sending!" );
                return;
            }

            SendDataUDP( devicelist );
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Copies the RFDevice.
        /// </summary>
        private void CopyRFDevice()
        {
            lCopiedRFDevices.Clear();

            if( dgRFDevices.SelectedItems.Count == 0 )
            {
                MB.Information( "You marked no RFDevice to copy.\nPlease select the RFDevices in the datagrid and push the button again." );
                return;
            }

            foreach( var device in dgRFDevices.SelectedItems )
            {
                lCopiedRFDevices.Add( device as RFDeviceViewModel );
            }
        }


        /// <summary>
        /// Pastes the RFDevice.
        /// </summary>
        private void PasteRFDevice()
        {
            if( lCopiedRFDevices.Count > 0 )
            {
                foreach( var device in lCopiedRFDevices )
                {
                    // Create a copy of the original device and change the primarykey    
                    RFDevice newdevice = device.RFDevice.Clone();

                    newdevice.PrimaryKey = Guid.NewGuid();
                    newdevice.StartTime += settings.DeviceCopyTimeAddValue;

                    AddRFDevice( newdevice );
                }
            }
            else
            {
                MB.Information( "There are no copied RFDevices in the list.\nPlease mark a RFDevice and copied it." );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Opens the rf device in google maps.
        /// </summary>
        /// <param name="rfdefvice">The rfdefvice.</param>
        private void OpenRFDeviceInGoogleMaps( RFDeviceViewModel rfdefvice )
        {
            if( rfdefvice == null )
            {
                throw new ArgumentNullException( "rfdefvice" );
            }

            //string strUrl = string.Format(new NumberFormatInfo { NumberDecimalSeparator = "." }, "https://www.google.de/maps/@{0},{1},500m/sensorset=!3m1!1e3", rfdefvice.Latitude, rfdefvice.Longitude);
            //string strUrl = string.Format( new NumberFormatInfo { NumberDecimalSeparator = "." } , "https://www.google.de/maps/@{0},{1},100m" , rfdefvice.Latitude , rfdefvice.Longitude );
            string strUrl = string.Format( new NumberFormatInfo { NumberDecimalSeparator = "." } , "https://www.google.de/maps/place/{0},{1}" , rfdefvice.Latitude , rfdefvice.Longitude );

            Tools.Windows.OpenWebAdress( strUrl );
        }


        /// <summary>
        /// Opens the rf device in google maps.
        /// </summary>
        private void OpenRFDeviceInGoogleMaps()
        {
            if( dgRFDevices.SelectedItem == null )
            {
                MB.Information( "No RFDevice Is Selected In The DataGrid!" );
                return;
            }

            if( dgRFDevices.SelectedItems.Count > 1 )
            {
                MB.Information( "There Are More Than One RFDevice Selected In The DataGrid!" );
                return;
            }

            OpenRFDeviceInGoogleMaps( dgRFDevices.SelectedItem as RFDeviceViewModel );
        }


        /// <summary>
        /// Rfs the device qr code.
        /// </summary>
        private void RFDeviceQRCode()
        {
            if( dgRFDevices.SelectedItem == null )
            {
                MB.Information( "No RFDevice Is Selected In The DataGrid!" );
                return;
            }

            if( dgRFDevices.SelectedItems.Count > 1 )
            {
                MB.Information( "There Are More Than One RFDevice Selected In The DataGrid!" );
                return;
            }

            QRCodeDialog dlg = new QRCodeDialog( dgRFDevices.SelectedItem as RFDeviceViewModel );
            dlg.ShowDialog();
            dlg = null;
        }

    } // end public partial class MainWindow
}
