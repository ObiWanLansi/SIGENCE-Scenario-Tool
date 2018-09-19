using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Datatypes.Observable;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.RxTxTypes;
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

            if( this.settings.IsUpgraded == false )
            {
                this.settings.Upgrade();
                this.settings.IsUpgraded = true;
                this.settings.Save();
            }

            if( string.IsNullOrEmpty( this.settings.UDPHost ) )
            {
                MB.Warning( "The value in the configuration file for the setting UDPHost is invalid!\nPlease correct the value and restart the application." );
                this.settings.UDPHost = "127.0.0.1";
            }

            if( this.settings.UDPPortSending < 1025 || this.settings.UDPPortSending > 65535 )
            {
                MB.Warning( "The value in the configuration file for the setting UDPPort is invalid!\nPlease correct the value and restart the application." );
                this.settings.UDPPortSending = 4242;
            }

            if( this.settings.UDPDelay < 0 || this.settings.UDPDelay > 10000 )
            {
                MB.Warning( "The value in the configuration file for the setting UDPDelay is invalid!\nPlease correct the value and restart the application." );
                this.settings.UDPDelay = 500;
            }

            if( this.settings.MapZoomLevel < 1 || this.settings.MapZoomLevel > 20 )
            {
                MB.Warning( "The value in the configuration file for the setting MapZoomLevel is invalid!\nPlease correct the value and restart the application." );
                this.settings.MapZoomLevel = 18;
            }

            //-----------------------------------------------------------------

            this.RFDevicesCollection = new RFDeviceViewModelList();
            this.QuickCommands = new ObservableStringCollection();
            this.ValidationResult = new ValidationResultViewModelList();

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

            try
            {
                string strFilename = string.Format( "{0}\\tuebingen-regbez-latest.osm.sqlite" , Tool.StartupPath );
                this.GeoNodes = GeoNodeCollection.GetCollection( strFilename );
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }

            this.lcv = CollectionViewSource.GetDefaultView( this.GeoNodes ) as ListCollectionView;

            this.lcv.IsLiveFiltering = true;
            this.lcv.Filter = IsWantedGeoNode;

            //-----------------------------------------------------------------

            dgcbcRxTxType.ItemsSource = RxTxTypes.Values;

            //-----------------------------------------------------------------

#if DEBUG
            CreateRandomizedRFDevices( 500 , true );

            //AddRFDevice(new RFDevice { PrimaryKey = Guid.Empty, Id = -1, Latitude = 1974, Longitude = 1974, StartTime = -1974 });

            //CreateHeatmap();

            //-----------------------------------------------------------------

            //SaveFile( @"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf" );
            //LoadFile( @"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf" );

            //SaveFile( @"C:\Transfer\TestScenario.stf" );
            //LoadFile( @"C:\Transfer\TestScenario.stf" );

            //-----------------------------------------------------------------

            //RFDeviceList devicelist = GetDeviceList();

            //ExportRFDevices( devicelist , new FileInfo( @"C:\Transfer\TestScenario.csv" ) );
            //ExportRFDevices( devicelist , new FileInfo( @"C:\Transfer\TestScenario.xml" ) );

            //ExportRFDevices( devicelist , new FileInfo( @"C:\Transfer\TestScenario.xlsx" ) );
            //ImportRFDevices( new FileInfo( @"C:\Transfer\TestScenario.xlsx" ) );

            //-----------------------------------------------------------------

            //SaveFile( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.stf" );
            ////LoadFile( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.stf" );

            //RFDeviceList devicelist = GetDeviceList();

            //ExportRFDevices( devicelist , new FileInfo( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.csv" ) );
            //ExportRFDevices( devicelist , new FileInfo( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.xml" ) );
            //ExportRFDevices( devicelist , new FileInfo( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.xlsx" ) );
            //ImportRFDevices( new FileInfo( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.xlsx" ) );

            //-----------------------------------------------------------------

            this.QuickCommands.Add( "new" );
            this.QuickCommands.Add( "rand 20" );
            this.QuickCommands.Add( "export csv" );
            this.QuickCommands.Add( "set rxtxtype unknown" );
            this.QuickCommands.Add( "set name nasenbär" );
            this.QuickCommands.Add( "remove" );
            this.QuickCommands.Add( "save" );
            this.QuickCommands.Add( "exit" );

            //OpenFile(@"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf");

            //RFDevice device = new RFDevice().WithId(0).WithName("Hello").WithStartTime(5);
            //MB.Information(device.ToXml().ToString());
#endif
        }

    } // end public partial class MainWindow
}
