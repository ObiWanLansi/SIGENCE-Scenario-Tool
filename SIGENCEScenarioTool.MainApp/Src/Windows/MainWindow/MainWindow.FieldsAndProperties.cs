using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using Microsoft.Win32;
using SIGENCEScenarioTool.Models.Database.GeoDb;
using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
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
        /// The ofd import rf devices
        /// </summary>
        private readonly OpenFileDialog ofdImportRFDevices = new OpenFileDialog();

        /// <summary>
        /// The SFD save screenshot
        /// </summary>
        private readonly SaveFileDialog sfdSaveScreenshot = new SaveFileDialog();

        //---------------------------------------------------------------------


        /// <summary>
        /// The missing
        /// </summary>
        private readonly object Missing = Type.Missing;

        /// <summary>
        /// The settings
        /// </summary>
        private readonly Properties.Settings settings = Properties.Settings.Default;

        /// <summary>
        /// A list with temporery devices to copy and paste.
        /// </summary>
        private readonly List<RFDeviceViewModel> lCopiedRFDevices = new List<RFDeviceViewModel>();

        //---------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the RFDevice collection.
        /// </summary>
        /// <value>
        /// The RFDevice collection.
        /// </value>
        public ObservableCollection<RFDeviceViewModel> RFDevicesCollection { get; set; }

        //---------------------------------------------------------------------


        /// <summary>
        /// The string scenario description
        /// </summary>
        private String strScenarioDescription = "";

        /// <summary>
        /// Gets or sets the scenario description.
        /// </summary>
        /// <value>
        /// The scenario description.
        /// </value>
        public String ScenarioDescription
        {
            get { return strScenarioDescription; }
            set
            {
                strScenarioDescription = value;

                UpdateScenarioDescription();

                FirePropertyChanged();
            }
        }

        //---------------------------------------------------------------------


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


        /// <summary>
        /// The b received data
        /// </summary>
        private bool bReceivedData = false;

        /// <summary>
        /// Gets or sets a value indicating whether [received data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [received data]; otherwise, <c>false</c>.
        /// </value>
        public bool ReceivedData
        {
            get { return bReceivedData; }
            set
            {
                this.bReceivedData = value;

                SetBlink1();

                FirePropertyChanged();
            }
        }

        //---------------------------------------------------------------------


        /// <summary>
        /// The b scenario description edit mode
        /// </summary>
        private bool bScenarioDescriptionEditMode = false;

        /// <summary>
        /// Gets or sets a value indicating whether [scenario description edit mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [scenario description edit mode]; otherwise, <c>false</c>.
        /// </value>
        public bool ScenarioDescriptionEditMode
        {
            get { return bScenarioDescriptionEditMode; }
            set
            {
                this.bScenarioDescriptionEditMode = value;

                SwitchScenarioEditMode();

                FirePropertyChanged();
            }
        }

        //---------------------------------------------------------------------


        /// <summary>
        /// The b data grid in edit mode
        /// </summary>
        private bool bDataGridInEditMode = false;


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
                mcMapControl.Manager.Mode = value != GMapProviders.EmptyProvider ? AccessMode.ServerAndCache : AccessMode.CacheOnly;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b is tile loading
        /// </summary>
        private bool bIsTileLoading = false;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is tile loading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is tile loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsTileLoading
        {
            get { return bIsTileLoading; }
            set
            {
                bIsTileLoading = value;

                FirePropertyChanged();
            }
        }

        ///// <summary>
        ///// Gets or sets the drag button.
        ///// </summary>
        ///// <value>
        ///// The drag button.
        ///// </value>
        //public MouseButton DragButton
        //{
        //    get { return mcMapControl.DragButton; }
        //    set
        //    {
        //        mcMapControl.DragButton = value;

        //        FirePropertyChanged();
        //    }
        //}


        /// <summary>
        /// The b is device moving mode
        /// </summary>
        private bool bIsDeviceMovingMode = false;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is device moving mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is device moving mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeviceMovingMode
        {
            get { return bIsDeviceMovingMode; }
            set
            {
                bIsDeviceMovingMode = value;

                mcMapControl.DragButton = bIsDeviceMovingMode ? MouseButton.Right : MouseButton.Left;

                FirePropertyChanged();
            }
        }

        ///// <summary>
        ///// Gets or sets a value indicating whether [show center].
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if [show center]; otherwise, <c>false</c>.
        ///// </value>
        //public bool ShowCenter
        //{
        //    get { return mcMapControl.ShowCenter; }
        //    set
        //    {
        //        mcMapControl.ShowCenter = value;
        //        mcMapControl.ReloadMap();

        //        FirePropertyChanged();
        //    }
        //}


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

        //---------------------------------------------------------------------


        /// <summary>
        /// Gets the UDP host.
        /// </summary>
        /// <value>
        /// The UDP host.
        /// </value>
        public string UDPHost
        {
            get { return settings.UDPHost; }
            set
            {
                settings.UDPHost = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the UDP port.
        /// </summary>
        /// <value>
        /// The UDP port.
        /// </value>
        public int UDPPort
        {
            get { return settings.UDPPortSending; }
            set
            {
                settings.UDPPortSending = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the UDP delay.
        /// </summary>
        /// <value>
        /// The UDP delay.
        /// </value>
        public int UDPDelay
        {
            get { return settings.UDPDelay; }
            set
            {
                settings.UDPDelay = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The string debug output
        /// </summary>
        private String strDebugOutput = null;

        /// <summary>
        /// Gets or sets the debug output.
        /// </summary>
        /// <value>
        /// The debug output.
        /// </value>
        public String DebugOutput
        {
            get { return strDebugOutput; }
            set
            {
                strDebugOutput = value;
                FirePropertyChanged();
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the geo node collection.
        /// </summary>
        /// <value>
        /// The geo node collection.
        /// </value>
        public GeoNodeCollection GeoNodeCollection { get; set; }


        /// <summary>
        /// The gt filter
        /// </summary>
        private GeoTag gtFilter = GeoTag.Place;

        /// <summary>
        /// Gets or sets the geo tag filter.
        /// </summary>
        /// <value>
        /// The geo tag filter.
        /// </value>
        public GeoTag GeoTagFilter
        {
            get { return gtFilter; }
            set
            {
                this.gtFilter = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b use geo tag filter
        /// </summary>
        private bool bUseGeoTagFilter = false;

        /// <summary>
        /// Gets or sets a value indicating whether [use geo tag filter].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use geo tag filter]; otherwise, <c>false</c>.
        /// </value>
        public bool UseGeoTagFilter
        {
            get { return bUseGeoTagFilter; }
            set
            {
                this.bUseGeoTagFilter = value;
                FirePropertyChanged();
            }
        }

    } // end public partial class MainWindow
}
