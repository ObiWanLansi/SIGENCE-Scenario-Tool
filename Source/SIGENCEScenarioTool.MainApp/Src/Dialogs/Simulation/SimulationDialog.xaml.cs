using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;
using SIGENCEScenarioTool.Windows.MainWindow;



/*
 * Bei der Simluation müssen nachher nur die Sender berücksichtigt werden, die Receiver empfangen einfach nur ... ?
*/

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
        /// 
        /// </summary>
        /// <seealso cref="System.Collections.IComparer" />
        private class Comparer : IComparer
        {
            /// <summary>
            /// Vergleicht zwei Objekte und gibt einen Wert zurück, der angibt, ob ein Wert niedriger, gleich oder größer als der andere Wert ist.
            /// </summary>
            /// <param name="x">Das erste zu vergleichende Objekt.</param>
            /// <param name="y">Das zweite zu vergleichende Objekt.</param>
            /// <returns>
            /// Eine ganze Zahl mit Vorzeichen, die die relativen Werte von <paramref name="x" /> und <paramref name="y" /> angibt, wie in der folgenden Tabelle veranschaulicht.Wert Bedeutung Kleiner als 0 <paramref name="x" /> ist kleiner als <paramref name="y" />. Zero <paramref name="x" /> ist gleich <paramref name="y" />. Größer als 0 (null) <paramref name="x" /> ist größer als <paramref name="y" />.
            /// </returns>
            public int Compare( object x, object y )
            {
                return (x as RFDeviceViewModel).StartTime.CompareTo( (y as RFDeviceViewModel).StartTime );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationDialog"/> class.
        /// </summary>
        public SimulationDialog( RFDeviceViewModelCollection _RFDeviceViewModelCollection )
        {
            this.DataContext = this;

            this.RFDeviceViewModelCollection = new RFDeviceViewModelCollection( _RFDeviceViewModelCollection );

            //-----------------------------------------------------------------

            if(CollectionViewSource.GetDefaultView( this.RFDeviceViewModelCollection ) is ListCollectionView lcvRFDevices)
            {
                lcvRFDevices.IsLiveSorting = false;
                lcvRFDevices.CustomSort = new Comparer();
            }

            //-----------------------------------------------------------------

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
            this.IsRunning = !this.IsRunning;

            this.btnPause.IsEnabled = this.bIsRunning;
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
            MB.NotYetImplemented();
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
