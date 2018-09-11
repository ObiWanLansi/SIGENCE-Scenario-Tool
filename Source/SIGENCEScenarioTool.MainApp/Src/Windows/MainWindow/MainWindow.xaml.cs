using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

using SIGENCEScenarioTool.Datatypes.Observable;
using SIGENCEScenarioTool.Models;
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

            if( settings.IsUpgraded == false )
            {
                settings.Upgrade();
                settings.IsUpgraded = true;
                settings.Save();
            }

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

            RFDevicesCollection = new RFDeviceViewModelList();
            QuickCommands = new ObservableStringCollection();
            ValidationResult = new ValidationResultViewModelList();

            DataContext = this;

            //-----------------------------------------------------------------

            InitMapControl();
            InitMapProvider();
            InitCommands();
            InitFileOpenSaveDialogs();

            //-----------------------------------------------------------------

            SetTitle();
            UpdateScenarioDescription();

            //-----------------------------------------------------------------

            try
            {
                string strFilename = string.Format( "{0}\\tuebingen-regbez-latest.osm.sqlite" , Tool.StartupPath );
                GeoNodeCollection = GeoNodeCollection.GetCollection( strFilename );
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }

            lcv = CollectionViewSource.GetDefaultView( GeoNodeCollection ) as ListCollectionView;

            lcv.IsLiveFiltering = true;
            lcv.Filter = IsWantedGeoNode;

            //-----------------------------------------------------------------

#if DEBUG
            //CreateRandomizedRFDevices( 10 , false );
            
            //CreateHeatmap();

            //SaveFile(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf");

            //RFDeviceList devicelist = GetDeviceList();

            //ExportRFDevices(devicelist, new FileInfo(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.sqlite"));
            //ExportRFDevices(devicelist, new FileInfo(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.csv"));
            //ExportRFDevices(devicelist, new FileInfo(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.json"));
            //ExportRFDevices(devicelist, new FileInfo(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.xml"));
            //ExportRFDevices(devicelist, new FileInfo(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.xlsx"));

            AddRFDevice( new RFDevice { PrimaryKey = Guid.Empty , Id = -1 , Latitude = 1974 , Longitude = 1974 , StartTime = -1974 } );

            QuickCommands.Add( "new" );
            QuickCommands.Add( "rand 20" );
            QuickCommands.Add( "export csv" );
            QuickCommands.Add( "set rxtxtype unknown" );
            QuickCommands.Add( "set name nasenbär" );
            QuickCommands.Add( "remove" );
            QuickCommands.Add( "save" );
            QuickCommands.Add( "exit" );

            //OpenFile(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf");

            //RFDevice device = new RFDevice().WithId(0).WithName("Hello").WithStartTime(5);
            //MB.Information(device.ToXml().ToString());
#endif
        }

    } // end public partial class MainWindow
}
