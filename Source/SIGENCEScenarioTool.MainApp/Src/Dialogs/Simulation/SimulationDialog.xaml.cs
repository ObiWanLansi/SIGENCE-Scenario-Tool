using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.ViewModels;
using SIGENCEScenarioTool.Windows.MainWindow;



namespace SIGENCEScenarioTool.Dialogs.Simulation
{
    /// <summary>
    /// Interaktionslogik für SimulationDialog.xaml
    /// </summary>
    public partial class SimulationDialog : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the RFDevice collection.
        /// </summary>
        /// <value>
        /// The RFDevice collection.
        /// </value>
        public RFDeviceViewModelCollection RFDeviceViewModelCollection { get; set; } = new RFDeviceViewModelCollection();


        /// <summary>
        /// The i current time
        /// </summary>
        private double iCurrentTime;

        /// <summary>
        /// Gets or sets the current time.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        public double CurrentTime
        {
            get { return this.iCurrentTime; }
            set
            {
                this.iCurrentTime = value;
                this.CurrentTimeAsString = TimeSpan.FromSeconds( value ).ToShortString();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// </summary>
        private double iMinTime = 0;

        /// <summary>
        /// </summary>
        /// <value>
        /// </value>
        public double MinTime
        {
            get { return this.iMinTime; }
            set
            {
                this.iMinTime = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The i maximum time
        /// </summary>
        private double iMaxTime = 0;

        /// <summary>
        /// Gets or sets the maximum time.
        /// </summary>
        /// <value>
        /// The maximum time.
        /// </value>
        public double MaxTime
        {
            get { return this.iMaxTime; }
            set
            {
                this.iMaxTime = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The string current time
        /// </summary>
        private string strCurrentTime = "88:88:88.888";

        /// <summary>
        /// Gets or sets the current time as string.
        /// </summary>
        /// <value>
        /// The current time as string.
        /// </value>
        public string CurrentTimeAsString
        {
            get { return this.strCurrentTime; }
            set
            {
                this.strCurrentTime = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b is running
        /// </summary>
        private bool bIsRunning = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is runnig.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is runnig; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get { return this.bIsRunning; }
            set
            {
                this.bIsRunning = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationDialog"/> class.
        /// </summary>
        public SimulationDialog( RFDeviceViewModelCollection _RFDeviceViewModelCollection )
        {
            this.DataContext = this;

            //TODO: Sort List bei StartTime und vorher ggf. noch als Clone (wir brauchen auch ein eigenes ViewModell und als Parameter nur die RFDeviceList) ...
            this.RFDeviceViewModelCollection = _RFDeviceViewModelCollection;

            InitializeComponent();

            InitMapControl();
            InitSlider();
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


        /// <summary>
        /// Initializes the slider.
        /// </summary>
        private void InitSlider()
        {
            this.MinTime = 0;
            this.CurrentTime = 0;

            //TODO: Das muss natürlich über die letzte Startzeit noch eine gewisse Zeit X hinausgehen ...
            this.MaxTime = this.RFDeviceViewModelCollection.Max( d => d.StartTime ) + 10;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the Button_PlayStop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_PlayStop_Click( object sender, RoutedEventArgs e )
        {
            ////TODO: Switch Start/Stop Image, Disable / Enable COntrols ...
            //if(this.IsRunning == true)
            //{
            //    this.IsRunning = false;
            //    //this.btnPause.IsEnabled = false;
            //}
            //else
            //{
            //    this.IsRunning = true;
            //    //this.btnPause.IsEnabled = true;
            //}

            this.IsRunning = !this.IsRunning;

            this.btnPause.IsEnabled = !this.bIsRunning;
            this.sCurrentTime.IsEnabled = !this.bIsRunning;
            this.btnPlayStop.Content = this.Resources[this.bIsRunning ? "STOP" : "PLAY"];
        }

        /// <summary>
        /// Handles the Click event of the Button_Pause control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Pause_Click( object sender, RoutedEventArgs e )
        {

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        private void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( strPropertyName ) );
        }

    } // end public partial class SimulationDialog
}
