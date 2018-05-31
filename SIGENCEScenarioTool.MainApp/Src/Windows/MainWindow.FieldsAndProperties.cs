using System;
using System.Collections.ObjectModel;

using GMap.NET;
using GMap.NET.MapProviders;

using Microsoft.Win32;

using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The missing
        /// </summary>
        private readonly object Missing = Type.Missing;

        /// <summary>
        /// The SFD save sigence scenario
        /// </summary>
        private readonly SaveFileDialog sfdSaveSIGENCEScenario = new SaveFileDialog();

        /// <summary>
        /// The ofd load sigence scenario
        /// </summary>
        private readonly OpenFileDialog ofdLoadSIGENCEScenario = new OpenFileDialog();

        /// <summary>
        /// The SFD export rf devices
        /// </summary>
        private readonly SaveFileDialog sfdExportRFDevices = new SaveFileDialog();

        /// <summary>
        /// The SFD save screenshot
        /// </summary>
        private readonly SaveFileDialog sfdSaveScreenshot = new SaveFileDialog();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the RFDevice collection.
        /// </summary>
        /// <value>
        /// The RFDevice collection.
        /// </value>
        public ObservableCollection<RFDeviceViewModel> RFDevicesCollection { get; set; }


        /// <summary>
        /// The b creating RFDevice
        /// </summary>
        private bool bCreatingRFDevice = false;


        /// <summary>
        /// Gets or sets a value indicating whether [creating rf device].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [creating rf device]; otherwise, <c>false</c>.
        /// </value>
        public bool CreatingRFDevice
        {
            get { return bCreatingRFDevice; }
            set
            {
                this.bCreatingRFDevice = value;

                SetMapToCreatingRFDeviceMode();
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
