using System.Net;
using System.Windows;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using SIGENCEScenarioTool.ViewModels;
using SIGENCEScenarioTool.Windows.MainWindow;



namespace SIGENCEScenarioTool.Dialogs.Simulation
{
    /// <summary>
    /// Interaktionslogik für SimulationDialog.xaml
    /// </summary>
    public partial class SimulationDialog : Window
    {
        /// <summary>
        /// Gets or sets the RFDevice collection.
        /// </summary>
        /// <value>
        /// The RFDevice collection.
        /// </value>
        public RFDeviceViewModelCollection RFDeviceViewModelCollection { get; set; } = new RFDeviceViewModelCollection();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationDialog"/> class.
        /// </summary>
        public SimulationDialog( RFDeviceViewModelCollection _RFDeviceViewModelCollection )
        {
            this.DataContext = this;

            this.RFDeviceViewModelCollection = _RFDeviceViewModelCollection;

            InitializeComponent();

            InitMapControl();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the map control.
        /// </summary>
        private void InitMapControl()
        {
            Properties.Settings settings = Properties.Settings.Default;

            GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            this.mcMapControl.DragButton = MouseButton.Left;
            this.mcMapControl.Manager.Mode = AccessMode.ServerAndCache;
            this.mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            this.mcMapControl.ShowCenter = false;
            this.mcMapControl.MinZoom = 2;
            this.mcMapControl.MaxZoom = 22;
            this.mcMapControl.Position = new PointLatLng( settings.InitialLatitude, settings.InitialLongitude );
            this.mcMapControl.Zoom = settings.InitialZoom;
            this.mcMapControl.MapProvider = MainWindow.GetProviderFromString( settings.InitialMap );
        }

    } // end public partial class SimulationDialog
}
