using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.Database.GeoDb;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            Cursor = Cursors.Wait;

            CurrentFile = null;

            RFDevicesCollection.Clear();
            mcMapControl.Markers.Clear();
            ScenarioDescription = "";

            GC.WaitForPendingFinalizers();
            GC.Collect();

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Sets the title.
        /// </summary>
        private void SetTitle()
        {
            this.Title = string.Format("{0} ({1}){2}", Tool.ProductTitle, Tool.Version, CurrentFile != null ? string.Format(" [{0}]", new FileInfo(CurrentFile).Name) : "");
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Opens the cheat sheet.
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
                if (CurrentFile != null)
                {
                    sfdSaveScreenshot.FileName = new FileInfo(CurrentFile).Name;
                }

                if (sfdSaveScreenshot.ShowDialog() == true)
                {
                    var screenshot = Tools.Windows.GetWPFScreenshot(mcMapControl);

                    Tools.Windows.SaveWPFScreenshot(screenshot, sfdSaveScreenshot.FileName);

                    Tools.Windows.OpenWithDefaultApplication(sfdSaveScreenshot.FileName);
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Switches the scenario edit mode.
        /// </summary>
        private void SwitchScenarioEditMode()
        {
            wbScenarioDescription.Visibility = ScenarioDescriptionEditMode ? Visibility.Hidden : Visibility.Visible;
            tbScenarioDescription.Visibility = ScenarioDescriptionEditMode ? Visibility.Visible : Visibility.Hidden;
        }


        /// <summary>
        /// Updates the scenario description.
        /// </summary>
        private void UpdateScenarioDescription()
        {
            if (string.IsNullOrEmpty(ScenarioDescription) == false)
            {
                wbScenarioDescription.NavigateToString(ScenarioDescription);
            }
            else
            {
                //wbScenarioDescription.NavigateToString( "<html/>" );
                wbScenarioDescription.NavigateToString("<i>No scenario description avaible.</i>");
            }
        }


        /// <summary>
        /// HTMLs the convert german umlauts.
        /// </summary>
        private void HtmlConvertGermanUmlauts()
        {
            if (string.IsNullOrEmpty(tbScenarioDescription.Text) == false)
            {
                tbScenarioDescription.Text = tbScenarioDescription.Text.ReplaceHtml();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Opens the settings.
        /// </summary>
        private void OpenSettings()
        {
            SettingsDialog dlg = new SettingsDialog();

            if (dlg.ShowDialog() == true)
            {
                //MB.Information("Saving The Settings ...");
            }
            else
            {
                //MB.Warning("Not Saving The Settings ...");
            }

            dlg = null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Restores the initial map values.
        /// </summary>
        private void RestoreInitialMapValues()
        {
            mcMapControl.Position = new PointLatLng(settings.InitialLatitude, settings.InitialLongitude);
            mcMapControl.Zoom = settings.InitialZoom;
            mcMapControl.MapProvider = GetProviderFromString(settings.InitialMap);
        }


        /// <summary>
        /// Saves the initial map values.
        /// </summary>
        private void SaveInitialMapValues()
        {
            PointLatLng pll = mcMapControl.Position;
            settings.InitialLatitude = pll.Lat;
            settings.InitialLongitude = pll.Lng;

            settings.InitialZoom = (uint)mcMapControl.Zoom;
            settings.InitialMap = mcMapControl.MapProvider.ToString();

            settings.Save();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the provider from string.
        /// </summary>
        /// <param name="strMapProvider">The string map provider.</param>
        /// <returns></returns>
        static private GMapProvider GetProviderFromString(string strMapProvider)
        {
            foreach (var mp in GMapProviders.List)
            {
                if (mp.Name == strMapProvider)
                {
                    return mp;
                }
            }

            return GMapProviders.GoogleMap;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Removes the old dalf.
        /// </summary>
        private bool StartDALF()
        {
            if (mcMapControl.Markers.Contains(mrDALF))
            {
                if (MessageBox.Show("Should The Existing Line Be Continued Or A New One Started?", Tool.ProductTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    mcMapControl.Markers.Remove(mrDALF);
                    mrDALF = null;
                    dvmLastSelectedDevice = null;
                }
            }

            if (dvmLastSelectedDevice == null)
            {
                if (dgRFDevices.SelectedItems.Count != 1)
                {
                    MB.Information("There Are No One Or More Than One RFDevice Selected In The DataGrid!");
                    return false;
                }

                RFDevice selectedDevice = (dgRFDevices.SelectedItems[0] as RFDeviceViewModel).RFDevice;

                if (selectedDevice.Id == 0)
                {
                    MB.Information("There Reference Transmitter Is Not Good For DALF!");
                    return false;
                }

                if (selectedDevice.DeviceSource == DeviceSource.Automatic)
                {
                    MB.Information("There DeviceSource Is Automatic. That's Not Good For Copying The Device!");
                    return false;
                }

                dvmLastSelectedDevice = selectedDevice;
            }

            return true;
        }


        /// <summary>
        /// Checks for RFD evices.
        /// </summary>
        private void StopDALF()
        {
            if (mrDALF != null && mrDALF.Points.Count > 0)
            {
                // Erst die alten löschen ...
                DeleteRFDevices(device => device.Id == dvmLastSelectedDevice.Id && device.DeviceSource == DeviceSource.Automatic);

                int iCounter = 1;

                foreach (PointLatLng pos in mrDALF.Points)
                {
                    RFDevice device = dvmLastSelectedDevice.Clone();

                    device.DeviceSource = DeviceSource.Automatic;
                    //device.Name = string.Format("{0} #{1}", 42 < 0 ? "Receiver" : "Transmitter", iCounter);
                    device.Latitude = pos.Lat;
                    device.Longitude = pos.Lng;
                    device.StartTime = settings.DeviceCopyTimeAddValue * iCounter;

                    AddRFDevice(device);

                    iCounter++;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Determines whether [is wanted geo node] [the specified object].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if [is wanted geo node] [the specified object]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsWantedGeoNode(object obj)
        {
            if (obj == null || obj is GeoNode == false)
            {
                return false;
            }

            GeoNode gn = obj as GeoNode;

            if (UseGeoTagFilter == true)
            {
                if (gn.Tag != GeoTagFilter)
                {
                    return false;
                }
            }

            if (UseNameFilter == true && NameFilter.IsNotEmpty())
            {
                if (gn.Name.ToLower().Contains(NameFilter.ToLower()) == false)
                {
                    return false;
                }
            }

            return true;
        }

    } // end public partial class MainWindow
}
