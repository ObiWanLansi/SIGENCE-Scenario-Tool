using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.RxTxTypes;
using SIGENCEScenarioTool.Models.Templates;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.Ui;



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

            if (this.settings.IsUpgraded == false)
            {
                this.settings.Upgrade();
                this.settings.IsUpgraded = true;
                this.settings.Save();
            }

            //if(string.IsNullOrEmpty( this.settings.UDPHost ))
            //{
            //    MB.Warning( "The value in the configuration file for the setting UDPHost is invalid!\nPlease correct the value and restart the application." );
            //    this.settings.UDPHost = "127.0.0.1";
            //}

            //if(this.settings.UDPPortSending < 1025 || this.settings.UDPPortSending > 65535)
            //{
            //    MB.Warning( "The value in the configuration file for the setting UDPPort is invalid!\nPlease correct the value and restart the application." );
            //    this.settings.UDPPortSending = 4242;
            //}

            //if(this.settings.UDPDelay < 0 || this.settings.UDPDelay > 10000)
            //{
            //    MB.Warning( "The value in the configuration file for the setting UDPDelay is invalid!\nPlease correct the value and restart the application." );
            //    this.settings.UDPDelay = 500;
            //}

            if (this.settings.MapZoomLevel < 1 || this.settings.MapZoomLevel > 20)
            {
                MB.Warning("The value in the configuration file for the setting MapZoomLevel is invalid!\nPlease correct the value and restart the application.");
                this.settings.MapZoomLevel = 18;
            }

            //-----------------------------------------------------------------

            this.DataContext = this;

            //-----------------------------------------------------------------

            if (Properties.Settings.Default.LastOpenFiles == null)
            {
                Properties.Settings.Default.LastOpenFiles = new StringCollection();
            }

            //-----------------------------------------------------------------

            InitMapControl();
            InitMapProvider();
            InitCommands();
            InitFileOpenSaveDialogs();
            InitTextEditorControls();

            //-----------------------------------------------------------------

            SetTitle();
            //UpdateScenarioDescription();
            UpdateFileHistory();

            //-----------------------------------------------------------------

            this.lcvRFDevices = CollectionViewSource.GetDefaultView(this.RFDeviceViewModelCollection) as ListCollectionView;

            if (this.lcvRFDevices != null)
            {
                this.lcvRFDevices.IsLiveFiltering = true;
                this.lcvRFDevices.Filter = IsWantedRFDevice;
            }

            //-----------------------------------------------------------------

            this.dgcbcRxTxType.ItemsSource = RxTxTypes.Values;

            List<RxTxType> lRxTxTypes = new List<RxTxType> { RxTxType.Empty };
            lRxTxTypes.AddRange(RxTxTypes.Values);
            this.cbRxTxType.ItemsSource = lRxTxTypes;

            this.cbAntennaType.ItemsSource = DisplayableEnumeration.GetCollection<AntennaType>();

            //-----------------------------------------------------------------

            this.RFDeviceTemplateCollection.Add(EMPTY_TEMPLATE);

            this.MetaInformation.PropertyChanged += MetaInformation_PropertyChanged;

            //-----------------------------------------------------------------
#if DEBUG
            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(new RFDevice { Name = "GPS Jammer", Id = 1 }));
            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(new RFDevice { Name = "FMBroadcast", Id = 2 }));
            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(new RFDevice { Name = "NFMRadio", Id = 3 }));
            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(new RFDevice { Name = "AIS Sender", Id = 4 }));

            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(new RFDevice { Name = "B200 Mini", Id = -2 }));
            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(new RFDevice { Name = "HackRF", Id = -3 }));
            this.RFDeviceTemplateCollection.Add(new RFDeviceTemplate(new RFDevice { Name = "TwinRx", Id = -4 }));

            //LoadTemplates( @"D:\EigeneDateien\Entwicklung.GitHub\SIGENCE-Scenario-Tool\Examples\Templates.stt" );

            //-----------------------------------------------------------------

            try
            {
                string strFilename = $"{Tool.StartupPath}\\tuebingen-regbez-latest.osm.sqlite";
                this.GeoNodeCollection = GeoNodeCollection.GetCollection(strFilename);
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            this.lcvGeoNodes = CollectionViewSource.GetDefaultView(this.GeoNodeCollection) as ListCollectionView;

            if (this.lcvGeoNodes != null)
            {
                this.lcvGeoNodes.IsLiveFiltering = true;
                this.lcvGeoNodes.Filter = IsWantedGeoNode;
            }

            //-----------------------------------------------------------------

            //CreateRandomizedRFDevices(10, true);

            //AddRFDevice( new RFDevice { PrimaryKey = Guid.Empty, Id = -1, Latitude = 1974, Longitude = 1974, StartTime = -1974 } );

            //AddRFDevice( new RFDevice
            //{
            //    Id = 42,
            //    DeviceSource = DeviceSource.User,
            //    Latitude = 47.666557,
            //    Longitude = 9.386941,
            //    AntennaType = AntennaType.HyperLOG60200,
            //    RxTxType = RxTxTypes.FMBroadcast,
            //    CenterFrequency_Hz = 90_000_000,
            //    Bandwidth_Hz = 30_000
            //} );

            //AddRFDevice( new RFDevice
            //{
            //    Id = -42,
            //    DeviceSource = DeviceSource.User,
            //    Latitude = 47.666100,
            //    Longitude = 9.172648,
            //    AntennaType = AntennaType.OmniDirectional,
            //    RxTxType = RxTxTypes.IdealSDR,
            //    CenterFrequency_Hz = 90_000_000,
            //    Bandwidth_Hz = 30_000
            //} );

            //CreateHeatmap();
            //CreateExampleRFDevices();
            CreateRandomizedRFDevices(42);

            //-----------------------------------------------------------------

            //SaveFile( @"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf" );
            //LoadFile( @"D:\BigData\GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.stf" );

            //SaveFile( @"C:\Transfer\TestScenario.stf" );
            //LoadFile( @"C:\Transfer\TestScenario.stf" );

            //LoadFile(@"D:\EigeneDateien\Entwicklung.GitHub\SIGENCE-Scenario-Tool\Examples\LongLineForSimulationPlayer.stf");

            //-----------------------------------------------------------------

            //RFDeviceList devicelist = GetDeviceList();

            //ExportRFDevices( devicelist, new FileInfo( @"D:\EigeneDateien\Entwicklung.GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.csv" ) );
            //ExportRFDevices( devicelist, new FileInfo( @"D:\EigeneDateien\Entwicklung.GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.xml" ) );
            //ExportRFDevices( devicelist, new FileInfo( @"D:\EigeneDateien\Entwicklung.GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.json" ) );
            //ExportRFDevices( devicelist, new FileInfo( @"D:\EigeneDateien\Entwicklung.GitHub\SIGENCE-Scenario-Tool\Examples\TestScenario.xlsx" ) );

            //-----------------------------------------------------------------

            this.QuickCommands.Add("new");
            this.QuickCommands.Add("rand 20");
            this.QuickCommands.Add("export csv");
            this.QuickCommands.Add("set rxtxtype unknown");
            this.QuickCommands.Add("set name nasenbär");
            this.QuickCommands.Add("remove");
            this.QuickCommands.Add("save");
            this.QuickCommands.Add("exit");

            //-----------------------------------------------------------------

            //OpenScenarioSimulationPlayer();

            //-----------------------------------------------------------------

            ////string strMarkdown = $"{Tool.StartupPath}\\ExampleScenarioDescription.md";

            //this.MetaInformation.Version = "1.0";
            //this.MetaInformation.ApplicationContext = "Scenario Meta Information Test";
            //this.MetaInformation.ContactPerson = "Jörg Lanser";

            ////this.MetaInformation.Description = File.ReadAllText($"{Tool.StartupPath}\\ExampleScenarioDescription.md");
            ////this.MetaInformation.Stylesheet = "h1 { border: 1px solid red; }";
            //this.MetaInformation.SetDescriptionAndStylesheet(File.ReadAllText($"{Tool.StartupPath}\\ExampleScenarioDescription.md"), "h1 { border: 1px solid red; }");

            ////this.MetaInformation.Attachements.Add(new Attachement(new FileInfo($"{Tool.StartupPath}\\ExampleScenarioDescription.md"), AttachementType.Embedded));
            ////this.MetaInformation.Attachements.Add(new Attachement(new FileInfo($"{Tool.StartupPath}\\HelloWorld.py"), AttachementType.Embedded));
            ////this.MetaInformation.Attachements.Add(new Attachement(new FileInfo($"{Tool.StartupPath}\\CheatSheet.pdf"), AttachementType.Link));
            ////this.MetaInformation.Attachements.Add(new Attachement(new FileInfo($"{Tool.StartupPath}\\CheatSheet.xps"), AttachementType.Link));
            ////this.MetaInformation.Attachements.Add(new Attachement(new FileInfo($"{Tool.StartupPath}\\streets_bw.sqlite"), AttachementType.Link));

            //this.tecDescription.Text = this.MetaInformation.Description;
            //this.tecStyleSheet.Text = this.MetaInformation.Stylesheet;

            //this.tiMetaInformation.IsSelected = true;

            //-----------------------------------------------------------------

            //RFDevice device = new RFDevice().WithId(0).WithName("Hello").WithStartTime(5);
            //MB.Information(device.ToXml().ToString());
#else
            this.mMainMenu.Items.Remove(this.miTest);
            this.tcTabControl.Items.Remove(this.tiGeoNodes);
            //this.tcTabControl.Items.Remove(this.tiMetaInformation);
#endif
        }

    } // end public partial class MainWindow
}
