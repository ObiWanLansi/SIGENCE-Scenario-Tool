using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

using GMap.NET;

using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models.Database.GeoDb;
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

            if (string.IsNullOrEmpty(settings.UDPHost))
            {
                MB.Warning("The value in the configuration file for the setting UDPHost is invalid!\nPlease correct the value and restart the application.");
                settings.UDPHost = "127.0.0.1";
            }

            if (settings.UDPPortSending < 1025 || settings.UDPPortSending > 65535)
            {
                MB.Warning("The value in the configuration file for the setting UDPPort is invalid!\nPlease correct the value and restart the application.");
                settings.UDPPortSending = 4242;
            }

            if (settings.UDPDelay < 0 || settings.UDPDelay > 10000)
            {
                MB.Warning("The value in the configuration file for the setting UDPDelay is invalid!\nPlease correct the value and restart the application.");
                settings.UDPDelay = 500;
            }

            if (settings.MapZoomLevel < 1 || settings.MapZoomLevel > 20)
            {
                MB.Warning("The value in the configuration file for the setting MapZoomLevel is invalid!\nPlease correct the value and restart the application.");
                settings.MapZoomLevel = 18;
            }

            //-----------------------------------------------------------------

            this.RFDevicesCollection = new ObservableCollection<RFDeviceViewModel>();
            this.DataContext = this;

            //-----------------------------------------------------------------

            InitMapControl();
            InitMapProvider();
            InitCommands();
            InitUDPServer();
            InitFileOpenSaveDialogs();

            //-----------------------------------------------------------------

            SetTitle();
            UpdateScenarioDescription();

            //-----------------------------------------------------------------

#if DEBUG
            CreateRandomizedRFDevices(10);

            //OpenFile(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf");

            try
            {
                //this.GeoNodeCollection = GeoNodeCollection.GetCollection( @"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Databases\GeoDb\freiburg-regbez-latest.osm.sqlite" );
                this.GeoNodeCollection = GeoNodeCollection.GetCollection( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Databases\GeoDb\freiburg-regbez-latest.osm.sqlite" );

                //-----------------------------------------------------------------

                //ListCollectionView _listCollectionView = CollectionViewSource.GetDefaultView(ocServices) as ListCollectionView;

                //if (_listCollectionView != null)
                //{
                //    _listCollectionView.IsLiveSorting = true;
                //    _listCollectionView.CustomSort = new ServiceComparer();
                //}

                //-----------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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


        /// <summary>
        /// Opens the settings.
        /// </summary>
        private void OpenSettings()
        {
            SettingsDialog dlg = new SettingsDialog();

            if (dlg.ShowDialog() == true)
            {
                MB.Information("Saving The Settings ...");
            }
            else
            {
                MB.Warning("Not Saving The Settings ...");
            }

            dlg = null;
        }


        /// <summary>
        /// Saves the initial map values.
        /// </summary>
        private void SaveInitialMapValues()
        {
            PointLatLng pll = mcMapControl.Position;
            uint iZoom = (uint)mcMapControl.Zoom;

            settings.InitialLatitude = pll.Lat;
            settings.InitialLongitude = pll.Lng;
            settings.InitialZoom = iZoom;
            settings.InitialMap = mcMapControl.MapProvider.ToString();

            settings.Save();
        }

    } // end public partial class MainWindow
}
