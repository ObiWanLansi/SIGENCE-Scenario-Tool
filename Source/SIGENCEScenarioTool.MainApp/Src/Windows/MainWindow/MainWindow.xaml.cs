using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

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

            settings.Upgrade();

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
            InitFileOpenSaveDialogs();

            //-----------------------------------------------------------------

            SetTitle();
            UpdateScenarioDescription();

            //-----------------------------------------------------------------

#if DEBUG
            CreateRandomizedRFDevices(10);

            //OpenFile(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf");
#endif
            //-----------------------------------------------------------------

            try
            {
                string strFilename = string.Format("{0}\\tuebingen-regbez-latest.osm.sqlite", Tool.StartupPath);
                this.GeoNodeCollection = GeoNodeCollection.GetCollection(strFilename);
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            lcv = CollectionViewSource.GetDefaultView(this.GeoNodeCollection) as ListCollectionView;

            lcv.IsLiveFiltering = true;
            lcv.Filter = IsWantedGeoNode;
        }

    } // end public partial class MainWindow
}
