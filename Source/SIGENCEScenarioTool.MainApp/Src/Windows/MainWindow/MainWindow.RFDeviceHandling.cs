using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Xml.Linq;

using GMap.NET;

using SIGENCEScenarioTool.Datatypes.Standard;
using SIGENCEScenarioTool.Dialogs.QRCode;
using SIGENCEScenarioTool.Dialogs.RFDevice;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.RxTxTypes;
using SIGENCEScenarioTool.Models.Templates;
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
            return new RFDeviceList(from devicemodel in this.RFDeviceViewModelCollection where devicemodel.IsMarked == true select devicemodel.RFDevice);
        }


        /// <summary>
        /// Sets the map to creating RFDevice mode.
        /// </summary>
        private void SetMapToCreatingRFDeviceMode()
        {
            this.mcMapControl.DragButton = this.bCreatingRFDevice ? MouseButton.Right : MouseButton.Left;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Begins the create RFDevice.
        /// </summary>
        private void BeginCreateRFDevice()
        {
            this.CreatingRFDevice = true;
            this.mcMapControl.Cursor = Cursors.Cross;
        }


        /// <summary>
        /// Ends the create RFDevice.
        /// </summary>
        private void EndCreateRFDevice()
        {
            this.mcMapControl.Cursor = Cursors.Arrow;
            this.CreatingRFDevice = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the RFDevice.
        /// </summary>
        /// <param name="pll">The PLL.</param>
        /// <param name="ds">The ds.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        private void AddRFDevice(PointLatLng pll, DeviceSource ds = DeviceSource.User, bool bIsSelected = false)
        {
            RFDevice newdevice = this.CurrentSelectedTemplate;

            // Die müssen wir ja neu vergeben ...
            newdevice.PrimaryKey = Guid.NewGuid();

            // Und die müssen wir zuweisen, der Rest wird aus dem Template übernommen ...
            newdevice.DeviceSource = ds;
            newdevice.Latitude = pll.Lat;
            newdevice.Longitude = pll.Lng;

            AddRFDevice(newdevice, bIsSelected);

            //AddRFDevice( new RFDevice
            //{
            //    DeviceSource = ds,
            //    Latitude = pll.Lat,
            //    Longitude = pll.Lng
            //}, bIsSelected );
        }


        /// <summary>
        /// Adds the RFDevice.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        private void AddRFDevice(RFDevice d, bool bIsSelected = false)
        {
            // Adding Everything, The User Can Still Correct This Later...

            //if (d.IsValid() == false)
            //{
            //    MB.Warning("Device Is Not Valid And Will Not Be Added To The Scenario!");
            //    return;
            //}

            AddRFDevice(new RFDeviceViewModel(this.mcMapControl, d), bIsSelected);
        }


        /// <summary>
        /// Adds the RFDeviceViewModel.
        /// </summary>
        /// <param name="dvm">The DVM.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        private void AddRFDevice(RFDeviceViewModel dvm, bool bIsSelected = false)
        {
            dvm.OnSelectionChanged += DeviceViewModel_OnSelectionChanged;

            this.RFDeviceViewModelCollection.Add(dvm);
            this.mcMapControl.Markers.Add(dvm.Marker);

            if (bIsSelected == true)
            {
                this.bNoFlashBack = true;

                this.dgRFDevices.SelectedItems.Clear();
                this.dgRFDevices.SelectedItems.Add(dvm);
                this.dgRFDevices.ScrollIntoView(dvm);

                dvm.IsSelected = true;

                foreach (RFDeviceViewModel model in this.RFDeviceViewModelCollection)
                {
                    if (model != dvm)
                    {
                        model.IsSelected = false;
                    }
                }

                this.bNoFlashBack = false;
            }
        }


        /// <summary>
        /// Adds the rf devices.
        /// </summary>
        /// <param name="devices">The devices.</param>
        private void AddRFDevices(RFDeviceList devices)
        {
            devices.ForEach(d => AddRFDevice(d));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Deletes the RFDevice.
        /// </summary>
        /// <param name="dvm">The DVM.</param>
        private void DeleteRFDevice(RFDeviceViewModel dvm)
        {
            dvm.OnSelectionChanged -= DeviceViewModel_OnSelectionChanged;
            this.RFDeviceViewModelCollection.Remove(dvm);
            this.mcMapControl.Markers.Remove(dvm.Marker);
        }


        /// <summary>
        /// Deletes the RFDevice.
        /// </summary>
        private void DeleteRFDevice()
        {
            if (this.dgRFDevices.SelectedItem == null)
            {
                MB.Information("No RFDevice Is Selected In The DataGrid!");
                return;
            }

            if (this.dgRFDevices.SelectedItems.Count > 1)
            {
                MB.Information("There Are More Than One RFDevice Selected In The DataGrid!");
                return;
            }

            DeleteRFDevice(this.dgRFDevices.SelectedItem as RFDeviceViewModel);
        }


        /// <summary>
        /// Deletes the RFDevices.
        /// </summary>
        private void DeleteRFDevices()
        {
            if (this.dgRFDevices.SelectedItems.Count == 0)
            {
                MB.Information("No RFDevice Is Selected In The DataGrid!");
                return;
            }

            List<RFDeviceViewModel> list = new List<RFDeviceViewModel>(this.dgRFDevices.SelectedItems.Count);

            foreach (var item in this.dgRFDevices.SelectedItems)
            {
                list.Add(item as RFDeviceViewModel);
            }

            list.ForEach(DeleteRFDevice);

            //var lDelete =new List<RFDeviceViewModel>( dgRFDevices.SelectedItems);
            //            foreach( RFDeviceViewModel device in .ToList() )
            //            {
            //                DeleteRFDevice( device );
            //            }

            //foreach (RFDeviceViewModel device in (from devicemodel in this.RFDevicesCollection where devicemodel.IsMarked == true select devicemodel).ToList())
            //{
            //    DeleteRFDevice(device);
            //}
        }


        /// <summary>
        /// Deletes the rf devices.
        /// </summary>
        /// <param name="predicator">The predicator.</param>
        private void DeleteRFDevices(Predicate<RFDeviceViewModel> predicator)
        {
            foreach (RFDeviceViewModel device in (from devicemodel in this.RFDeviceViewModelCollection where devicemodel.IsMarked == true select devicemodel).ToList())
            {
                if (predicator(device) == true)
                {
                    DeleteRFDevice(device);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region Specialied Excel Import

        /// <summary>
        /// Loads from excel.
        /// </summary>
        /// <param name="strInputFilename">The string input filename.</param>
        private void LoadFromExcel(string strInputFilename)
        {
            if (strInputFilename.IsEmpty())
            {
                throw new ArgumentException("The input filename can not be empty!", nameof(strInputFilename));
            }

            Excel.Application excel = new Excel.Application();

            try
            {
                Excel.Workbook wb = excel.Workbooks.Open(strInputFilename);

                // We guess, the RF Device Data is on sheet #1, starting @ row 1 ...
                Excel.Worksheet maindatasheet = wb.Sheets[1] as Excel.Worksheet;

                Excel.Range range = maindatasheet.UsedRange;

                int iColumnCount = range.Columns.Count;

                if (iColumnCount < 17)
                {
                    throw new Exception(
                        $"The Current Excel File Can Not Be Imported Because There Are Only {iColumnCount} Columns!\nWe Need At Least 17 Columns For A Good Import.");
                }

                int iRowCount = range.Rows.Count;

                if (iRowCount < 2)
                {
                    throw new Exception(
                        $"The Current Excel File Can Not Be Imported Because There Are Only {iRowCount} Rows!\nWe Need At Least 2 Rows For A Good Import.");
                }

                RFDeviceList dlImportedDevices = new RFDeviceList();

                for (int iRow = 2; iRow < iRowCount + 1; iRow++)
                {
                    RFDevice device = new RFDevice
                    {
                        DeviceSource = DeviceSource.DataImport,
                    };

                    for (int iColumn = 1; iColumn < 19 + 1; iColumn++)
                    {
                        object value = (range.Cells[iRow, iColumn] as Excel.Range).Value2;

                        if (value == null)
                        {
                            continue;
                        }

                        // Hier ist zu überlegen wie wir das ganze generisch machen können falls sich das Model nochmal ändert ...
                        switch (iColumn)
                        {
                            case 1:
                                device.StartTime = Convert.ToDouble(value);
                                break;

                            case 2:
                                device.Id = Convert.ToInt32(value);
                                break;

                            case 3:
                                device.Latitude = Convert.ToDouble(value);
                                break;

                            case 4:
                                device.Longitude = Convert.ToDouble(value);
                                break;

                            case 5:
                                device.Altitude = Convert.ToInt32(value);
                                break;

                            case 6:
                                device.Roll = Convert.ToDouble(value);
                                break;

                            case 7:
                                device.Pitch = Convert.ToDouble(value);
                                break;

                            case 8:
                                device.Yaw = Convert.ToDouble(value);
                                break;

                            case 9:
                                device.RxTxType = RxTxTypes.FromInt(device.Id, Convert.ToInt32(value));
                                break;

                            case 10:
                                device.AntennaType = (AntennaType)Convert.ToInt32(value);
                                break;

                            case 11:
                                device.Gain_dB = Convert.ToDouble(value);
                                break;

                            case 12:
                                device.CenterFrequency_Hz = Convert.ToDouble(value);
                                break;

                            case 13:
                                device.Bandwidth_Hz = Convert.ToDouble(value);
                                break;

                            case 14:
                                device.SignalToNoiseRatio_dB = Convert.ToDouble(value);
                                break;

                            case 15:
                                device.XPos = Convert.ToInt32(value);
                                break;

                            case 16:
                                device.YPos = Convert.ToInt32(value);
                                break;

                            case 17:
                                device.ZPos = Convert.ToInt32(value);
                                break;

                            case 18:
                                device.Remark = Convert.ToString(value);
                                break;

                            case 19:
                                device.TechnicalParameters = Convert.ToString(value);
                                break;
                        }
                    }

                    dlImportedDevices.Add(device);
                }

                AddRFDevices(dlImportedDevices);
            }
            catch (Exception ex)
            {
                MB.Error(ex);
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
        /// <param name="c">The c.</param>
        private void AddCell(Excel.Worksheet sheet, int iColumn, int iRow, object value, Color? c)
        {
            Excel.Range cell = sheet.Cells[iRow, iColumn] as Excel.Range;
            cell.Value2 = value;
            cell.HorizontalAlignment = value is string ? Excel.XlHAlign.xlHAlignLeft : Excel.XlHAlign.xlHAlignRight;

            if (c != null)
            {
                cell.Interior.Color = ColorTranslator.ToWin32(c.Value);
            }
        }


        /// <summary>
        /// Saves as excel file.
        /// </summary>
        /// <param name="dl">The dl.</param>
        /// <param name="strOutputFilename">The string output filename.</param>
        /// <exception cref="ArgumentException">Der Ausgabedateiname darf nicht leer sein! - strOutputFilename</exception>
        private void SaveAsExcel(RFDeviceList dl, string strOutputFilename)
        {
            if (strOutputFilename.IsEmpty())
            {
                throw new ArgumentException("The output filename can not be empty!", nameof(strOutputFilename));
            }

            //-----------------------------------------------------------------

            Excel.Application excel = new Excel.Application
            {
                SheetsInNewWorkbook = 1
            };

            Excel.Workbook wb = excel.Workbooks.Add(this.Missing);

            //-----------------------------------------------------------------

            Excel.Worksheet maindatasheet = wb.Sheets[1] as Excel.Worksheet;

            maindatasheet.Name = "RF Devices";

            StringList slColumnNames = new StringList
            {
                "Time","ID",
                "Lat","Long","Alt",
                "Roll","Pitch","Yaw",
                "RxTxType","AntType",
                "Gain","CenterFreq","BandWidth","SNR",
                "x","y","z",
                "Remark","TechnicalParameters"
            };

            // Create Header Columns
            {
                int iColumnCounter = 1;

                foreach (string strColumn in slColumnNames)
                {
                    Excel.Range cell = maindatasheet.Cells[1, iColumnCounter++] as Excel.Range;
                    cell.Font.Bold = true;
                    cell.Orientation = Excel.XlOrientation.xlUpward;
                    cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    cell.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                    cell.Value2 = " " + strColumn;
                    cell.Interior.Color = ColorTranslator.ToWin32(Color.CornflowerBlue);
                    cell.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                    cell.Borders.Weight = Excel.XlBorderWeight.xlThick;
                }
            }

            //-----------------------------------------------------------------

            // Create Data Columns And Rows
            {
                int iRowCounter = 2;

                int iLastId = 0;
                bool bIdToggle = false;

                foreach (RFDevice device in from dev in dl orderby dev.StartTime, dev.Id select dev)
                {
                    if (device.Id != iLastId)
                    {
                        bIdToggle = !bIdToggle;
                        iLastId = device.Id;
                    }

                    Color c = bIdToggle ? Color.AliceBlue : Color.LightGoldenrodYellow;

                    AddCell(maindatasheet, 1, iRowCounter, device.StartTime, c);
                    AddCell(maindatasheet, 2, iRowCounter, device.Id, c);
                    AddCell(maindatasheet, 3, iRowCounter, (double)device.Latitude, c);
                    AddCell(maindatasheet, 4, iRowCounter, (double)device.Longitude, c);
                    AddCell(maindatasheet, 5, iRowCounter, (int)device.Altitude, c);
                    AddCell(maindatasheet, 6, iRowCounter, device.Roll, c);
                    AddCell(maindatasheet, 7, iRowCounter, device.Pitch, c);
                    AddCell(maindatasheet, 8, iRowCounter, device.Yaw, c);
                    AddCell(maindatasheet, 9, iRowCounter, device.RxTxType * (this.SimulationMode ? 1 : -1), c);
                    AddCell(maindatasheet, 10, iRowCounter, device.AntennaType, c);
                    AddCell(maindatasheet, 11, iRowCounter, (double)device.Gain_dB, c);
                    AddCell(maindatasheet, 12, iRowCounter, (double)device.CenterFrequency_Hz, c);
                    AddCell(maindatasheet, 13, iRowCounter, (double)device.Bandwidth_Hz, c);
                    AddCell(maindatasheet, 14, iRowCounter, (double)device.SignalToNoiseRatio_dB, c);
                    AddCell(maindatasheet, 15, iRowCounter, device.XPos, c);
                    AddCell(maindatasheet, 16, iRowCounter, device.YPos, c);
                    AddCell(maindatasheet, 17, iRowCounter, device.ZPos, c);
                    AddCell(maindatasheet, 18, iRowCounter, device.Remark, c);
                    AddCell(maindatasheet, 19, iRowCounter, device.TechnicalParameters, c);

                    iRowCounter++;
                }
            }

            maindatasheet.Columns.AutoFit();

            //-----------------------------------------------------------------

            //if(string.IsNullOrEmpty( this.ScenarioDescription ) == false)
            //{
            //    if(wb.Sheets.Count < 2)
            //    {
            //        wb.Sheets.Add( this.Missing, maindatasheet );
            //    }

            //    Excel.Worksheet descriptionsheet = wb.Sheets[2] as Excel.Worksheet;
            //    descriptionsheet.Name = "Scenario Description";
            //    Excel.Range cell = descriptionsheet.Cells[1, 1] as Excel.Range;

            //    Clipboard.SetDataObject( this.ScenarioDescription );
            //    cell.PasteSpecial();
            //}

            //-----------------------------------------------------------------

            excel.Visible = true;

            wb.SaveAs(strOutputFilename, this.Missing, this.Missing, this.Missing, this.Missing, this.Missing, Excel.XlSaveAsAccessMode.xlNoChange, this.Missing, this.Missing, this.Missing, this.Missing, this.Missing);

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Imports the rf devices.
        /// </summary>
        /// <param name="fiImportFile">The fi import file.</param>
        private void ImportRFDevices(FileInfo fiImportFile)
        {
            this.Cursor = Cursors.Wait;

            try
            {
                switch (fiImportFile.Extension.ToLower())
                {
                    //case ".csv":
                    //    MB.Information("File {0} successful imported.", fiImportFile.Name);
                    //    break;

                    //case ".json":
                    //    MB.Information("File {0} successful imported.", fiImportFile.Name);
                    //    break;

                    //case ".xml":
                    //    MB.Information("File {0} successful imported.", fiImportFile.Name);
                    //    break;

                    case ".xlsx":
                        LoadFromExcel(fiImportFile.FullName);
                        break;

                    default:
                        MB.Information("Currently The Import Of {0} Is Not Implemented!", fiImportFile.Extension);
                        break;
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Exports the RFDevices.
        /// </summary>
        /// <param name="devicelist">The devicelist.</param>
        /// <param name="fiExportFile">The fi export file.</param>
        private void ExportRFDevices(RFDeviceList devicelist, FileInfo fiExportFile)
        {
            this.Cursor = Cursors.Wait;

            try
            {
                switch (fiExportFile.Extension.ToLower())
                {
                    case ".csv":
                        devicelist.SaveAsCsv(fiExportFile.FullName);
                        MB.Information("File {0} successful created.", fiExportFile.Name);
                        break;

                    case ".json":
                        devicelist.SaveAsJson(fiExportFile.FullName);
                        MB.Information("File {0} successful created.", fiExportFile.Name);
                        break;

                    case ".xml":
                        devicelist.SaveAsXml(fiExportFile.FullName);
                        MB.Information("File {0} successful created.", fiExportFile.Name);
                        break;

                    //case ".sqlite":
                    //    devicelist.SaveAsSQLite( fiExportFile.FullName );
                    //    MB.Information( "File {0} successful created." , fiExportFile.Name );
                    //    break;

                    case ".xlsx":
                        SaveAsExcel(devicelist, fiExportFile.FullName);
                        break;

                    default:
                        MB.Warning("The FileType '{0}' Is Currently Not Supported For Export!", fiExportFile.Extension.ToLower());
                        break;
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Exports the RFDevices.
        /// </summary>
        private void ExportRFDevices()
        {
            RFDeviceList devicelist = GetDeviceList();

            if (devicelist.Count == 0)
            {
                MB.Warning("No Selected RFDevice Avaible For Export!");
                return;
            }

            this.sfdExportSIGENCEScenario.FileName = this.CurrentFile != null ? new FileInfo(this.CurrentFile).GetFilenameWithoutExtension() : DateTime.Now.Fmt_YYYYMMDDHHMMSS();

            if (this.sfdExportSIGENCEScenario.ShowDialog() == true)
            {
                ExportRFDevices(devicelist, new FileInfo(this.sfdExportSIGENCEScenario.FileName));
            }
        }


        /// <summary>
        /// Imports the RFDevices.
        /// </summary>
        private void ImportRFDevices()
        {
            if (this.ofdImportSIGENCEScenario.ShowDialog() == true)
            {
                ImportRFDevices(new FileInfo(this.ofdImportSIGENCEScenario.FileName));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Zooms to RFDevice.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="bSetMapZoomLevel">if set to <c>true</c> [b set map zoom level].</param>
        private void ZoomToRFDevice(RFDevice device, bool bSetMapZoomLevel = true)
        {
            this.mcMapControl.Position = new PointLatLng(device.Latitude, device.Longitude);

            if (bSetMapZoomLevel == true)
            {
                this.mcMapControl.Zoom = this.settings.MapZoomLevel;
            }
        }


        /// <summary>
        /// Zooms to RFDevice.
        /// </summary>
        private void ZoomToRFDevice()
        {
            if (this.dgRFDevices.SelectedItem == null)
            {
                MB.Information("No RFDevice Is Selected In The DataGrid!");
                return;
            }

            if (this.dgRFDevices.SelectedItems.Count > 1)
            {
                MB.Information("There Are More Than One RFDevice Selected In The DataGrid!");
                return;
            }

            ZoomToRFDevice((this.dgRFDevices.SelectedItem as RFDeviceViewModel).RFDevice);
        }


        /// <summary>
        /// Edits the rf device.
        /// </summary>
        private void EditRFDevice()
        {
            if (this.dgRFDevices.SelectedItem == null)
            {
                MB.Information("No RFDevice Is Selected In The DataGrid!");
                return;
            }

            if (this.dgRFDevices.SelectedItems.Count > 1)
            {
                MB.Information("There Are More Than One RFDevice Selected In The DataGrid!");
                return;
            }

            //OpenDeviceEditDialog((this.dgRFDevices.SelectedItem as RFDeviceViewModel).RFDevice);
            OpenDeviceEditDialog(this.dgRFDevices.SelectedItem as RFDeviceViewModel);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Clears the templates.
        /// </summary>
        private void ClearTemplates()
        {
            this.RFDeviceTemplateCollection.Clear();
            this.RFDeviceTemplateCollection.Add(EMPTY_TEMPLATE);
        }


        /// <summary>
        /// Loads the templates.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        private void LoadTemplates(string strFilename)
        {
            this.Cursor = Cursors.Wait;

            ClearTemplates();

            try
            {
                XDocument xdoc = XDocument.Load(strFilename);

                foreach (XElement eTemplate in xdoc.Root.Elements("RFDevice"))
                {
                    RFDevice device = RFDevice.FromXml(eTemplate);

                    if (device != null)
                    {
                        this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(device));
                    }
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Loads the templates.
        /// </summary>
        private void LoadTemplates()
        {
            if (this.ofdLoadTemplates.ShowDialog() == true)
            {
                LoadTemplates(this.ofdLoadTemplates.FileName);
            }
        }


        /// <summary>
        /// Saves the templates.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        private void SaveTemplates(string strFilename)
        {
            this.Cursor = Cursors.Wait;

            try
            {
                XElement eSIGENCEScenarioTemplates = new XElement("SIGENCEScenarioTemplates", new XAttribute("Version", Tool.Version));

                //-------------------------------------------------------------

                foreach (RFDevice d in from template in this.RFDeviceTemplateCollection select template)
                {
                    if (d.PrimaryKey != Guid.Empty)
                    {
                        eSIGENCEScenarioTemplates.Add(d.ToXml());
                    }
                }

                //-------------------------------------------------------------

                eSIGENCEScenarioTemplates.SaveDefault(strFilename);

                MB.Information("{0}\nsuccessfully saved.", strFilename);
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Saves the templates.
        /// </summary>
        private void SaveTemplates()
        {
            if (this.sfdSaveTemplates.ShowDialog() == true)
            {
                SaveTemplates(this.sfdSaveTemplates.FileName);
            }
        }


        /// <summary>
        /// Adds to templates.
        /// </summary>
        /// <param name="device">The device.</param>
        private void AddToTemplates(RFDevice device)
        {
            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(device));
        }


        /// <summary>
        /// Adds to templates.
        /// </summary>
        /// <param name="dvm">The DVM.</param>
        private void AddToTemplates(RFDeviceViewModel dvm)
        {
            AddToTemplates(dvm.RFDevice);
        }


        /// <summary>
        /// Adds to templates.
        /// </summary>
        private void AddToTemplates()
        {
            if (this.dgRFDevices.SelectedItem == null)
            {
                MB.Information("No RFDevice Is Selected In The DataGrid!");
                return;
            }

            if (this.dgRFDevices.SelectedItems.Count > 1)
            {
                MB.Information("There Are More Than One RFDevice Selected In The DataGrid!");
                return;
            }

            AddToTemplates(this.dgRFDevices.SelectedItem as RFDeviceViewModel);
        }


        /// <summary>
        /// Deletes from templates.
        /// </summary>
        /// <param name="dvm">The DVM.</param>
        private void DeleteFromTemplates(RFDeviceViewModel dvm = null)
        {
            if (this.CurrentSelectedTemplate == EMPTY_TEMPLATE)
            {
                MB.Warning("You Can't Not Delete The Default Template!");
                return;
            }

            this.RFDeviceTemplateCollection.Remove(this.CurrentSelectedTemplate);

            this.CurrentSelectedTemplate = EMPTY_TEMPLATE;
        }


        /// <summary>
        /// Edits the template.
        /// </summary>
        private void EditTemplate()
        {
            if (this.CurrentSelectedTemplate == EMPTY_TEMPLATE)
            {
                MB.Warning("You Can't Not Edit The Default Template!");
                return;
            }

            RFDeviceEditDialog ded = new RFDeviceEditDialog(this.CurrentSelectedTemplate);

            if (ded.ShowDialog() ?? false == true)
            {
                this.RFDeviceTemplateCollection.Remove(this.CurrentSelectedTemplate);

                RFDeviceTemplate newtemplate = new RFDeviceTemplate(ded.Device);

                this.RFDeviceTemplateCollection.Add(newtemplate);
                this.CurrentSelectedTemplate = newtemplate;
            }

            ded = null;

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sends the RFDeviceList via UDP to any connect client.
        /// This function is not asynchron, so the main thread is blocked when sending data. 
        /// Maybe in oen of the next versions me make this function asynchron.
        /// </summary>
        /// <param name="devicelist">The devicelist.</param>
        private void SendDataUDP(RFDeviceList devicelist)
        {
            try
            {
                using (Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(this.settings.UDPHost), this.settings.UDPPortSending);

                    foreach (RFDevice device in devicelist)
                    {
                        XElement eDevice = device.ToXml();

                        byte[] baMessage = Encoding.Default.GetBytes(eDevice.ToDefaultString());

                        sender.SendTo(baMessage, endpoint);

                        // Give the poor client some time to process the data when he need or bleed ...
                        if (this.settings.UDPDelay > 0)
                        {
                            Thread.Sleep(this.settings.UDPDelay);
                        }
                    }

                    sender.Close();
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }
        }


        /// <summary>
        /// Sends the data UDP.
        /// </summary>
        private void SendDataUDP()
        {
            RFDeviceList devicelist = GetDeviceList();

            if (devicelist.Count == 0)
            {
                MB.Warning("No Selected RFDevice Avaible For Sending!");
                return;
            }

            SendDataUDP(devicelist);
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Copies the RFDevice.
        /// </summary>
        private void CopyRFDevice()
        {
            this.lCopiedRFDevices.Clear();

            if (this.dgRFDevices.SelectedItems.Count == 0)
            {
                MB.Information("You marked no RFDevice to copy.\nPlease select the RFDevices in the datagrid and push the button again.");
                return;
            }

            foreach (var device in this.dgRFDevices.SelectedItems)
            {
                this.lCopiedRFDevices.Add(device as RFDeviceViewModel);
            }
        }


        /// <summary>
        /// Pastes the RFDevice.
        /// </summary>
        private void PasteRFDevice()
        {
            if (this.lCopiedRFDevices.Count > 0)
            {
                foreach (var device in this.lCopiedRFDevices)
                {
                    // Create a copy of the original device and change the primarykey    
                    RFDevice newdevice = device.RFDevice.Clone();

                    newdevice.PrimaryKey = Guid.NewGuid();
                    newdevice.StartTime += this.settings.DeviceCopyTimeAddValue;

                    AddRFDevice(newdevice);
                }
            }
            else
            {
                MB.Information("There are no copied RFDevices in the list.\nPlease mark a RFDevice and copied it.");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Opens the rf device in google maps.
        /// </summary>
        /// <param name="rfdefvice">The rfdefvice.</param>
        private void OpenInGoogleMaps(RFDeviceViewModel rfdefvice)
        {
            if (rfdefvice == null)
            {
                throw new ArgumentNullException(nameof(rfdefvice));
            }

            //string strUrl = string.Format(new NumberFormatInfo { NumberDecimalSeparator = "." }, "https://www.google.de/maps/@{0},{1},500m/sensorset=!3m1!1e3", rfdefvice.Latitude, rfdefvice.Longitude);
            //string strUrl = string.Format( new NumberFormatInfo { NumberDecimalSeparator = "." } , "https://www.google.de/maps/@{0},{1},100m" , rfdefvice.Latitude , rfdefvice.Longitude );
            string strUrl = string.Format(new NumberFormatInfo { NumberDecimalSeparator = "." }, "https://www.google.de/maps/place/{0},{1}", rfdefvice.Latitude, rfdefvice.Longitude);

            Tools.Windows.OpenWebAdress(strUrl);
        }


        /// <summary>
        /// Opens the rf device in google maps.
        /// </summary>
        private void OpenInGoogleMaps()
        {
            if (this.dgRFDevices.SelectedItem == null)
            {
                MB.Information("No RFDevice Is Selected In The DataGrid!");
                return;
            }

            if (this.dgRFDevices.SelectedItems.Count > 1)
            {
                MB.Information("There Are More Than One RFDevice Selected In The DataGrid!");
                return;
            }

            OpenInGoogleMaps(this.dgRFDevices.SelectedItem as RFDeviceViewModel);
        }


        /// <summary>
        /// Rfs the device qr code.
        /// </summary>
        private void RFDeviceQRCode()
        {
            if (this.dgRFDevices.SelectedItem == null)
            {
                MB.Information("No RFDevice Is Selected In The DataGrid!");
                return;
            }

            if (this.dgRFDevices.SelectedItems.Count > 1)
            {
                MB.Information("There Are More Than One RFDevice Selected In The DataGrid!");
                return;
            }

            QRCodeDialog dlg = new QRCodeDialog(this.dgRFDevices.SelectedItem as RFDeviceViewModel);
            dlg.ShowDialog();
            dlg = null;
        }

    } // end public partial class MainWindow
}
