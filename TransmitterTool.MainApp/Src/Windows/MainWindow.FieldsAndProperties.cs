using System.Collections.ObjectModel;

using GMap.NET;
using GMap.NET.MapProviders;

using Microsoft.Win32;

using TransmitterTool.ViewModels;



namespace TransmitterTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// The SFD save transmitter
        /// </summary>
        private readonly SaveFileDialog sfdSaveTransmitter = new SaveFileDialog();

        /// <summary>
        /// The ofd load transmitter
        /// </summary>
        private readonly OpenFileDialog ofdLoadTransmitter = new OpenFileDialog();

        /// <summary>
        /// The SFD export transmitter
        /// </summary>
        private readonly SaveFileDialog sfdExportTransmitter = new SaveFileDialog();

        /// <summary>
        /// The SFD save screenshot
        /// </summary>
        private readonly SaveFileDialog sfdSaveScreenshot = new SaveFileDialog();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the transmitter.
        /// </summary>
        /// <value>
        /// The transmitter.
        /// </value>
        public ObservableCollection<TransmitterViewModel> TransmitterCollection { get; set; }


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

        //---------------------------------------------------------------------


        //public GMapControl MapControl
        //{
        //    get { return mcMapControl; }
        //}

        /// <summary>
        /// Gets or sets the map provider.
        /// </summary>
        /// <value>
        /// The map provider.
        /// </value>
        public GMapProvider MapProvider
        {
            get { return mcMapControl.MapProvider; }
            set
            {
                mcMapControl.MapProvider = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [show center].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show center]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowCenter
        {
            get { return mcMapControl.ShowCenter; }
            set
            {
                mcMapControl.ShowCenter = value;
                mcMapControl.ReloadMap();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>
        /// The zoom.
        /// </value>
        public double Zoom
        {
            get { return mcMapControl.Zoom; }
            set
            {
                mcMapControl.Zoom = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude
        {
            get { return mcMapControl.Position.Lat; }
            set
            {
                mcMapControl.Position = new PointLatLng(value, mcMapControl.Position.Lng);

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude
        {
            get { return mcMapControl.Position.Lng; }
            set
            {
                mcMapControl.Position = new PointLatLng(mcMapControl.Position.Lat, value);

                FirePropertyChanged();
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The string current file
        /// </summary>
        private string strCurrentFile = null;

        /// <summary>
        /// Gets or sets the current file.
        /// </summary>
        /// <value>
        /// The current file.
        /// </value>
        public string CurrentFile
        {
            get { return strCurrentFile; }
            set
            {
                this.strCurrentFile = value;
                SetTitle();
                FirePropertyChanged();
            }
        }

    } // end public partial class MainWindow
}
