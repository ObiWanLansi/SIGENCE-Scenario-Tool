using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET;
using GMap.NET.MapProviders;

using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Dialogs.Scripting;
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
            this.Cursor = Cursors.Wait;

            this.CurrentFile = null;

            this.RFDevicesCollection.Clear();
            this.ValidationResult.Clear();

            this.mcMapControl.Markers.Clear();
            this.ScenarioDescription = "";

            GC.WaitForPendingFinalizers();
            GC.Collect();

            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Sets the title.
        /// </summary>
        private void SetTitle()
        {
            this.Title = string.Format("{0} ({1}){2}", Tool.ProductTitle, Tool.Version, this.CurrentFile != null ? string.Format(" [{0}]", new FileInfo(this.CurrentFile).Name) : "");
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
                if (this.CurrentFile != null)
                {
                    this.sfdSaveScreenshot.FileName = new FileInfo(this.CurrentFile).Name;
                }

                if (this.sfdSaveScreenshot.ShowDialog() == true)
                {
                    var screenshot = Tools.Windows.GetWPFScreenshot(this.mcMapControl);

                    Tools.Windows.SaveWPFScreenshot(screenshot, this.sfdSaveScreenshot.FileName);

                    Tools.Windows.OpenWithDefaultApplication(this.sfdSaveScreenshot.FileName);
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }
        }


        /// <summary>
        /// Opens the script editor.
        /// </summary>
        private void OpenScriptEditor()
        {
            ScriptingDialog sd = new ScriptingDialog(this);
            sd.ShowDialog();
            sd = null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Switches the scenario edit mode.
        /// </summary>
        private void SwitchScenarioEditMode()
        {
            this.wbScenarioDescription.Visibility = this.ScenarioDescriptionEditMode ? Visibility.Hidden : Visibility.Visible;
            this.tbScenarioDescription.Visibility = this.ScenarioDescriptionEditMode ? Visibility.Visible : Visibility.Hidden;
        }


        /// <summary>
        /// Updates the scenario description.
        /// </summary>
        private void UpdateScenarioDescription()
        {
            if (string.IsNullOrEmpty(this.ScenarioDescription) == false)
            {
                this.wbScenarioDescription.NavigateToString(this.ScenarioDescription);
            }
            else
            {
                //wbScenarioDescription.NavigateToString( "<html/>" );
                this.wbScenarioDescription.NavigateToString("<i>No scenario description avaible.</i>");
            }
        }


        /// <summary>
        /// HTMLs the convert german umlauts.
        /// </summary>
        private void HtmlConvertGermanUmlauts()
        {
            if (string.IsNullOrEmpty(this.tbScenarioDescription.Text) == false)
            {
                this.tbScenarioDescription.Text = this.tbScenarioDescription.Text.ReplaceHtml();
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
            this.mcMapControl.Position = new PointLatLng(this.settings.InitialLatitude, this.settings.InitialLongitude);
            this.mcMapControl.Zoom = this.settings.InitialZoom;

            var mapprovider = GetProviderFromString(this.settings.InitialMap);
            this.mcMapControl.MapProvider = mapprovider;
            this.cbMapProvider.SelectedItem = mapprovider;
        }


        /// <summary>
        /// Saves the initial map values.
        /// </summary>
        private void SaveInitialMapValues()
        {
            PointLatLng pll = this.mcMapControl.Position;
            this.settings.InitialLatitude = pll.Lat;
            this.settings.InitialLongitude = pll.Lng;

            this.settings.InitialZoom = (uint)this.mcMapControl.Zoom;
            this.settings.InitialMap = this.mcMapControl.MapProvider.ToString();

            this.settings.Save();
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
            if (this.mcMapControl.Markers.Contains(this.mrDALF))
            {
                if (MessageBox.Show("Should The Existing Line Be Continued Or A New One Started?", Tool.ProductTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    this.mcMapControl.Markers.Remove(this.mrDALF);
                    this.mrDALF = null;
                    this.dvmLastSelectedDevice = null;
                }
            }

            if (this.dvmLastSelectedDevice == null)
            {
                if (this.dgRFDevices.SelectedItems.Count != 1)
                {
                    MB.Information("There Are No One Or More Than One RFDevice Selected In The DataGrid!");
                    return false;
                }

                RFDevice selectedDevice = (this.dgRFDevices.SelectedItems[0] as RFDeviceViewModel).RFDevice;

                if (selectedDevice.Id == 0)
                {
                    MB.Information("The Reference Transmitter Is Not Good For DALF!");
                    return false;
                }

                if (selectedDevice.DeviceSource == DeviceSource.Automatic)
                {
                    MB.Information("The DeviceSource Of The Current Device Is Automatic. That's Not Good For Copying The Device!");
                    return false;
                }

                this.dvmLastSelectedDevice = selectedDevice;
            }

            return true;
        }


        /// <summary>
        /// Stops the dalf.
        /// </summary>
        private void StopDALF()
        {
            if (this.mrDALF != null && this.mrDALF.Points.Count > 0)
            {
                // Erst die alten löschen ...
                DeleteRFDevices(device => device.Id == this.dvmLastSelectedDevice.Id && device.DeviceSource == DeviceSource.Automatic);

                int iCounter = 1;

                foreach (PointLatLng pos in this.mrDALF.Points)
                {
                    RFDevice device = this.dvmLastSelectedDevice.Clone();

                    device.DeviceSource = DeviceSource.Automatic;
                    //device.Name = string.Format("{0} #{1}", 42 < 0 ? "Receiver" : "Transmitter", iCounter);
                    device.Latitude = pos.Lat;
                    device.Longitude = pos.Lng;
                    device.StartTime = this.settings.DeviceCopyTimeAddValue * iCounter;

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

            if (this.UseGeoTagFilter == true)
            {
                if (gn.Tag != this.GeoTagFilter)
                {
                    return false;
                }
            }

            if (this.UseNameFilter == true && this.NameFilter.IsNotEmpty())
            {
                if (gn.Name.ToLower().Contains(this.NameFilter.ToLower()) == false)
                {
                    return false;
                }
            }

            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Quicks the command action.
        /// </summary>
        /// <param name="strCommand">The string command.</param>
        private void QuickCommandAction(string strCommand)
        {
            if (strCommand.IsEmpty())
            {
                return;
            }

            string[] strSplitted = strCommand.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (strSplitted.Length > 0)
            {
                string strMainCommand = strSplitted[0].ToLower();

                switch (strMainCommand)
                {
                    case "new":
                        NewFile();
                        break;

                    case "rand":
                        if (strSplitted.Length > 1)
                        {
                            try
                            {
                                int iCount = int.Parse(strSplitted[1]);
                                CreateRandomizedRFDevices(iCount);
                            }
                            catch (Exception ex)
                            {
                                MB.Error(ex);
                            }
                        }
                        break;


                    case "git":
                        OpenWebbrowser("https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool");
                        break;
                    case "web":
                        OpenWebbrowser();
                        break;
                    case "wiki":
                        OpenWebbrowser("https://de.wikipedia.org/wiki/Wikipedia:Hauptseite");
                        break;
                    case "go":
                        OpenWebbrowser("https://www.google.de/");
                        break;


                    case "close":
                    case "exit":
                    case "quit":
                        Close();
                        break;

                    /*
                     * save [fullname] ansonsten aktuelle datei
                     * set "property" "value" --> --> Marked Rows
                     * export csv|xml|json  --> Marked Rows
                     * load [fullname] wenn no fullname then FileOpenDialog
                     * remove, delete --> Marked Rows
                     * sendudp --> Marked Rows
                     * startreceive
                     * stopreceive
                     * goto lat/lon 
                     * zoom level
                     */

                    default:
                        MB.Warning("Unknown Command \"{0}\".", strMainCommand);
                        break;
                }
            }
        }


        /// <summary>
        /// Quicks the command action.
        /// </summary>
        private void QuickCommandAction()
        {
            string strCommand = this.cbQuickCommand.Text;

            if (strCommand.IsEmpty())
            {
                return;
            }

            this.Cursor = Cursors.Wait;

            string[] strSplitted = strCommand.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (strSplitted.Length > 0)
            {
                foreach (string strSubCommand in strSplitted)
                {
                    QuickCommandAction(strSubCommand);
                }
            }

            this.QuickCommands.Add(strCommand);

            this.Cursor = Cursors.Arrow;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Executes the validate scenario.
        /// </summary>
        private void ExecuteValidateScenario()
        {
            this.ValidationResult.Clear();

            //-----------------------------------------------------------------

            if (this.RFDevicesCollection.Count == 0)
            {
                this.ValidationResult.Add(new Models.Validation.ValidationResult(Servity.Information, "No Devices Are Configured.", "Scenario", "RFDevicesCollection", null));
                this.ValidationResult.EstimateCounts();

                // When we have no devices, we have nothing to validate ...
                return;
            }

            // Validation over the entire scenario, then only the individual RFDevices
            if (this.RFDevicesCollection.FirstOrDefault(d => d.Id == 0) == null)
            {
                this.ValidationResult.Add(new Models.Validation.ValidationResult(Servity.Warning, "No Reference Device Is Avaible.", "Scenario", "RFDevicesCollection", null));
            }

            //TODO: Add Some Other Rules Here ...

            //-----------------------------------------------------------------

            foreach (RFDevice device in this.RFDevicesCollection)
            {
                this.ValidationResult.Add(device.Validate());
            }

            //-----------------------------------------------------------------

            this.ValidationResult.EstimateCounts();

            if (this.tiValidation.IsSelected == false)
            {
                this.tiValidation.IsSelected = true;
            }
        }


        /// <summary>
        /// Clears the scenario validation.
        /// </summary>
        private void ClearScenarioValidation()
        {
            this.ValidationResult.Clear();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Opens the webbrowser.
        /// </summary>
        /// <param name="strUrl">The string URL.</param>
        private void OpenWebbrowser(string strUrl = null)
        {
            StackPanel header = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            header.Children.Add(new Label
            {
                Content = FindResource("SERVER_EARTH")
            });

            header.Children.Add(new Label
            {
                Content = "Webbrowser"
            });

            Button close = new Button
            {
                Content = "Ó",
                FontFamily = new FontFamily("Wingdings 2"),
                Width = 18,
                Height = 18,
                Foreground = Brushes.White,
                Background = Brushes.Red,
                Margin = new Thickness(3),
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            close.Click += Webbrowser_Close_Click;

            header.Children.Add(close);

            WebBrowser webbrowser = new WebBrowser
            {
                Source = strUrl != null ? new Uri(strUrl) : null
            };

            webbrowser.LoadCompleted += Webbrowser_LoadCompleted;

            TabItem ti = new TabItem { Header = header, Content = webbrowser, IsSelected = true };

            this.tcTabControl.Items.Add(ti);
        }


        /// <summary>
        /// Handles the Click event of the Webbrowser_Close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Webbrowser_Close_Click(object sender, RoutedEventArgs e)
        {
            this.tcTabControl.Items.Remove(((sender as Button).Parent as StackPanel).Parent);
        }


        /// <summary>
        /// Handles the LoadCompleted event of the Webbrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
        private void Webbrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ((((sender as WebBrowser).Parent as TabItem).Header as StackPanel).Children[1] as Label).Content = e.Uri != null ? e.Uri.AbsoluteUri : "Webbrowser";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the randomized RFDevices.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        /// <param name="bEnsureRefDevice">if set to <c>true</c> [b ensure reference device].</param>
        private void CreateRandomizedRFDevices(int iMaxCount, bool bEnsureRefDevice = false)
        {
            this.Cursor = Cursors.Wait;

            foreach (var device in RFDeviceList.CreateRandomizedRFDeviceList(iMaxCount, this.mcMapControl.Position, bEnsureRefDevice))
            {
                AddRFDevice(device);
            }

            this.Cursor = Cursors.Arrow;
        }

    } // end public partial class MainWindow
}
