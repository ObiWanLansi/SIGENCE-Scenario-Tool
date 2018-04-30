using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

using TransmitterMan.Commands;
using TransmitterMan.Markers;
using TransmitterMan.Models;
using TransmitterMan.ViewModels;



namespace TransmitterMan.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the transmitter.
        /// </summary>
        /// <value>
        /// The transmitter.
        /// </value>
        public ObservableCollection<TransmitterViewModel> Transmitter { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The b creating transmitter
        /// </summary>
        private bool bCreatingTransmitter = false;

        /// <summary>
        /// Gets or sets a value indicating whether [creating transmitter].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [creating transmitter]; otherwise, <c>false</c>.
        /// </value>
        public bool CreatingTransmitter
        {
            get { return bCreatingTransmitter; }
            set
            {
                this.bCreatingTransmitter = value;

                SetMapToCreatingTransmitterMode();
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the map control.
        /// </summary>
        /// <value>
        /// The map control.
        /// </value>
        public GMapControl MapControl
        {
            get { return mcMapControl; }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.Transmitter = new ObservableCollection<TransmitterViewModel>();
            this.DataContext = this;

            InitMapControl();
            InitMapProvider();
            InitCommands();

#if DEBUG
            WindowState = WindowState.Maximized;
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the commands.
        /// </summary>
        private void InitCommands()
        {
            CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateTransmitter,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    BeginCreateTransmitter();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    NewFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    Close();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));
        }


        /// <summary>
        /// Initializes the map control.
        /// </summary>
        private void InitMapControl()
        {
            mcMapControl.DragButton = MouseButton.Left;
            mcMapControl.MapProvider = GMapProviders.OpenStreetMap;
            mcMapControl.Manager.Mode = AccessMode.ServerAndCache;

            mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            mcMapControl.ShowCenter = false;
            mcMapControl.MinZoom = 2;
            mcMapControl.MaxZoom = 24;

            mcMapControl.Position = new PointLatLng(49.761471, 6.650053);
            mcMapControl.Zoom = 14;

            mcMapControl.MouseLeftButtonDown += McMapControl_MouseLeftButtonDown;
        }


        /// <summary>
        /// Initializes the map provider.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitMapProvider()
        {
            // Wir fügen nur die für unsere Region sinnvollen hinzu ...
            cbMapProvider.Items.Add(GMapProviders.OpenStreetMap);

            cbMapProvider.Items.Add(GMapProviders.GoogleHybridMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleSatelliteMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleTerrainMap);

            cbMapProvider.Items.Add(GMapProviders.BingHybridMap);
            cbMapProvider.Items.Add(GMapProviders.BingMap);
            cbMapProvider.Items.Add(GMapProviders.BingSatelliteMap);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// News the file.
        /// </summary>
        private void NewFile()
        {
            Transmitter.Clear();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sets the map to creating transmitter mode.
        /// </summary>
        private void SetMapToCreatingTransmitterMode()
        {
            mcMapControl.DragButton = bCreatingTransmitter ? MouseButton.Right : MouseButton.Left;
        }


        /// <summary>
        /// Begins the create transmitter.
        /// </summary>
        private void BeginCreateTransmitter()
        {
            CreatingTransmitter = true;
        }


        /// <summary>
        /// Ends the create transmitter.
        /// </summary>
        private void EndCreateTransmitter()
        {
            CreatingTransmitter = false;
        }


        /// <summary>
        /// Creates the transmitter.
        /// </summary>
        /// <param name="pll">The PLL.</param>
        private void CreateTransmitter(PointLatLng pll)
        {
            GMapMarker currentMarker = new GMapMarker(pll)
            {
                Shape = new Cross(),
                Offset = new Point(-15, -15),
                ZIndex = int.MaxValue
            };

            mcMapControl.Markers.Add(currentMarker);

            Transmitter t = new Transmitter
            {
                Location = pll
            };

            Transmitter.Add(new TransmitterViewModel(t));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeftButtonDown event of the McMapControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void McMapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CreatingTransmitter == true)
            {
                Point p = e.GetPosition(mcMapControl);

                PointLatLng pll = mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                CreateTransmitter(pll);

                EndCreateTransmitter();

                e.Handled = true;
            }
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
        protected void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

    } // end public partial class MainWindow
}
