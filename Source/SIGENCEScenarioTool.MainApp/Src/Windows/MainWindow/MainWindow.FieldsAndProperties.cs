using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

using Microsoft.Win32;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Datatypes.Observable;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.RxTxTypes;
using SIGENCEScenarioTool.Models.Templates;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ExplicitCallerInfoArgument



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The b data grid in edit mode
        /// </summary>
        private bool bDataGridInEditMode = false;

        /// <summary>
        /// The b no flash back
        /// </summary>
        private bool bNoFlashBack = false;

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

        /// <summary>
        /// The mr dalf
        /// </summary>
        private GMapRoute mrDALF = null;

        /// <summary>
        /// The DVM last selected device
        /// </summary>
        private RFDevice dvmLastSelectedDevice = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the RFDevice collection.
        /// </summary>
        /// <value>
        /// The RFDevice collection.
        /// </value>
        public RFDeviceViewModelCollection RFDeviceViewModelCollection { get; set; } = new RFDeviceViewModelCollection();

        /// <summary>
        /// Gets or sets the rf device template collection.
        /// </summary>
        /// <value>
        /// The rf device template collection.
        /// </value>
        public RFDeviceTemplateCollection RFDeviceTemplateCollection { get; set; } = new RFDeviceTemplateCollection();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The string scenario description
        /// </summary>
        private string strScenarioDescription = "";

        /// <summary>
        /// Gets or sets the scenario description.
        /// </summary>
        /// <value>
        /// The scenario description.
        /// </value>
        public string ScenarioDescription
        {
            get { return this.strScenarioDescription; }
            set
            {
                this.strScenarioDescription = value;

                UpdateScenarioDescription();

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
            get { return this.bCreatingRFDevice; }
            set
            {
                this.bCreatingRFDevice = value;

                SetMapToCreatingRFDeviceMode();

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// The b received data
        /// </summary>
        private bool bSimulationMode = true;

        /// <summary>
        /// Gets or sets a value indicating whether [received data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [received data]; otherwise, <c>false</c>.
        /// </value>
        public bool SimulationMode
        {
            get { return this.bSimulationMode; }
            set
            {
                this.bSimulationMode = value;

                FirePropertyChanged();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        ///// <summary>
        ///// The b received data
        ///// </summary>
        //private bool bReceivedData = false;

        ///// <summary>
        ///// Gets or sets a value indicating whether [received data].
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if [received data]; otherwise, <c>false</c>.
        ///// </value>
        //public bool ReceivedData
        //{
        //    get { return this.bReceivedData; }
        //    set
        //    {
        //        this.bReceivedData = value;

        //        SetBlink1();

        //        FirePropertyChanged();
        //    }
        //}


        ///// <summary>
        ///// The b is receive data UDP
        ///// </summary>
        //private bool bIsReceiveDataUDP = false;

        ///// <summary>
        ///// Gets or sets a value indicating whether this instance is receive data UDP.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if this instance is receive data UDP; otherwise, <c>false</c>.
        ///// </value>
        //public bool IsReceiveDataUDP
        //{
        //    get { return this.bIsReceiveDataUDP; }
        //    set
        //    {
        //        this.bIsReceiveDataUDP = value;

        //        if (this.bIsReceiveDataUDP == true)
        //        {
        //            StartUDPServer();
        //        }
        //        else
        //        {
        //            StopUDPServer();
        //        }

        //        FirePropertyChanged();
        //    }
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
            get { return this.bScenarioDescriptionEditMode; }
            set
            {
                this.bScenarioDescriptionEditMode = value;

                SwitchScenarioEditMode();

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
            get { return this.bIsTileLoading; }
            set
            {
                this.bIsTileLoading = value;

                FirePropertyChanged();
            }
        }


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
            get { return this.bIsDeviceMovingMode; }
            set
            {
                this.bIsDeviceMovingMode = value;

                this.mcMapControl.DragButton = this.bIsDeviceMovingMode ? MouseButton.Right : MouseButton.Left;

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the map provider.
        /// </summary>
        /// <value>
        /// The map provider.
        /// </value>
        public GMapProvider MapProvider
        {
            get { return this.mcMapControl.MapProvider; }
            set
            {
                this.mcMapControl.MapProvider = value;
                // ReSharper disable PossibleUnintendedReferenceComparison
                this.mcMapControl.Manager.Mode = value != GMapProviders.EmptyProvider ? AccessMode.ServerAndCache : AccessMode.CacheOnly;
                // ReSharper restore PossibleUnintendedReferenceComparison

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
            get { return this.mcMapControl.Zoom; }
            set
            {
                this.mcMapControl.Zoom = value;

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
            get { return this.mcMapControl.Position.Lat; }
            set
            {
                this.mcMapControl.Position = new PointLatLng( value, this.mcMapControl.Position.Lng );

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
            get { return this.mcMapControl.Position.Lng; }
            set
            {
                this.mcMapControl.Position = new PointLatLng( this.mcMapControl.Position.Lat, value );

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
            get { return this.mcMapControl.ShowCenter; }
            set
            {
                this.mcMapControl.ShowCenter = value;
                this.mcMapControl.ReloadMap();

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
            get { return this.strCurrentFile; }
            set
            {
                this.strCurrentFile = value;
                SetTitle();

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The b synchronize map and grid
        /// </summary>
        private bool bSyncMapAndGrid = true;

        /// <summary>
        /// Gets or sets a value indicating whether [synchronize map and grid].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [synchronize map and grid]; otherwise, <c>false</c>.
        /// </value>
        public bool SyncMapAndGrid
        {
            get { return this.bSyncMapAndGrid; }
            set
            {
                this.bSyncMapAndGrid = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b use browser internal
        /// </summary>
        private bool bUseBrowserInternal = false;

        /// <summary>
        /// Gets or sets a value indicating whether [use browser internal].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use browser internal]; otherwise, <c>false</c>.
        /// </value>
        public bool UseBrowserInternal
        {
            get { return this.bUseBrowserInternal; }
            set
            {
                this.bUseBrowserInternal = value;

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Gets the UDP host.
        ///// </summary>
        ///// <value>
        ///// The UDP host.
        ///// </value>
        //public string UDPHost
        //{
        //    get { return this.settings.UDPHost; }
        //    set
        //    {
        //        this.settings.UDPHost = value;

        //        FirePropertyChanged();
        //    }
        //}


        ///// <summary>
        ///// Gets the UDP port.
        ///// </summary>
        ///// <value>
        ///// The UDP port.
        ///// </value>
        //public int UDPPort
        //{
        //    get { return this.settings.UDPPortSending; }
        //    set
        //    {
        //        this.settings.UDPPortSending = value;

        //        FirePropertyChanged();
        //    }
        //}


        ///// <summary>
        ///// Gets the UDP delay.
        ///// </summary>
        ///// <value>
        ///// The UDP delay.
        ///// </value>
        //public int UDPDelay
        //{
        //    get { return this.settings.UDPDelay; }
        //    set
        //    {
        //        this.settings.UDPDelay = value;

        //        FirePropertyChanged();
        //    }
        //}


        ///// <summary>
        ///// The string debug output
        ///// </summary>
        //private string strDebugOutput = null;

        ///// <summary>
        ///// Gets or sets the debug output.
        ///// </summary>
        ///// <value>
        ///// The debug output.
        ///// </value>
        //public string DebugOutput
        //{
        //    get { return this.strDebugOutput; }
        //    set
        //    {
        //        this.strDebugOutput = value;

        //        FirePropertyChanged();
        //    }
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the geo node collection.
        /// </summary>
        /// <value>
        /// The geo node collection.
        /// </value>
        public GeoNodeCollection GeoNodeCollection { get; set; }

        /// <summary>
        /// The LCV
        /// </summary>
        private readonly ListCollectionView lcvGeoNodes = null;

        /// <summary>
        /// Gets the current nodes.
        /// </summary>
        /// <value>
        /// The current nodes.
        /// </value>
        public int CurrentNodes
        {
            get { return this.lcvGeoNodes?.Count ?? 0; }
        }

        //---------------------------------------------------------------------

        #region GeoNodeFilter

        /// <summary>
        /// The gt filter
        /// </summary>
        private GeoTag gtGeoTagFilter = GeoTag.Place;

        /// <summary>
        /// Gets or sets the geo tag filter.
        /// </summary>
        /// <value>
        /// The geo tag filter.
        /// </value>
        public GeoTag GeoTagFilter
        {
            get { return this.gtGeoTagFilter; }
            set
            {
                this.gtGeoTagFilter = value;

                this.lcvGeoNodes.Refresh();

                FirePropertyChanged();
                FirePropertyChanged( "CurrentNodes" );
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
            get { return this.bUseGeoTagFilter; }
            set
            {
                this.bUseGeoTagFilter = value;

                this.lcvGeoNodes.Refresh();

                FirePropertyChanged();
                FirePropertyChanged( "CurrentNodes" );
            }
        }


        /// <summary>
        /// The string name filter
        /// </summary>
        private string strNameFilter = "";

        /// <summary>
        /// Gets or sets the name filter.
        /// </summary>
        /// <value>
        /// The name filter.
        /// </value>
        public string NameFilter
        {
            get { return this.strNameFilter; }
            set
            {
                this.strNameFilter = value;

                this.lcvGeoNodes.Refresh();

                FirePropertyChanged();
                FirePropertyChanged( "CurrentNodes" );
            }
        }

        /// <summary>
        /// The b use name filter
        /// </summary>
        private bool bUseNameFilter = false;

        /// <summary>
        /// Gets or sets a value indicating whether [use name filter].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use name filter]; otherwise, <c>false</c>.
        /// </value>
        public bool UseNameFilter
        {
            get { return this.bUseNameFilter; }
            set
            {
                this.bUseNameFilter = value;

                this.lcvGeoNodes.Refresh();

                FirePropertyChanged();
                FirePropertyChanged( "CurrentNodes" );
            }
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The b started dalf
        /// </summary>
        private bool bStartedDALF = false;

        /// <summary>
        /// Gets or sets a value indicating whether [started dalf].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [started dalf]; otherwise, <c>false</c>.
        /// </value>
        public bool StartedDALF
        {
            get { return this.bStartedDALF; }
            set
            {
                if(value == true)
                {
                    // Nur wenn es erfolgreich gestartet werden konnte machen wir weiter ...
                    if(StartDALF() == false)
                    {
                        return;
                    }
                }
                else
                {
                    StopDALF();
                }

                this.bStartedDALF = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region RFDevice Filter

        /// <summary>
        /// The LCV
        /// </summary>
        private readonly ListCollectionView lcvRFDevices = null;

        /// <summary>
        /// The i identifier filter
        /// </summary>
        private int? iIdFilter = null;

        /// <summary>
        /// Gets or sets the identifier filter.
        /// </summary>
        /// <value>
        /// The identifier filter.
        /// </value>
        /// <remarks>We use a string so that we can provide a empty a string as integer NULL ...</remarks>
        public string IdFilter
        {
            get { return this.iIdFilter != null ? this.iIdFilter.ToString() : ""; }
            set
            {
                if(string.IsNullOrEmpty( value ))
                {
                    this.iIdFilter = null;
                }
                else
                {
                    try
                    {

                        this.iIdFilter = int.Parse( value );
                    }
                    catch(Exception)
                    {
                        this.iIdFilter = null;
                    }
                }

                this.lcvRFDevices.Refresh();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b show receiver
        /// </summary>
        private bool bShowReceiver = true;

        /// <summary>
        /// Gets or sets a value indicating whether [show receiver].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show receiver]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowReceiver
        {
            get { return this.bShowReceiver; }
            set
            {
                this.bShowReceiver = value;

                this.lcvRFDevices.Refresh();

                FirePropertyChanged();
            }
        }

        /// <summary>
        /// The b show transmitter
        /// </summary>
        private bool bShowTransmitter = true;

        /// <summary>
        /// Gets or sets a value indicating whether [show transmitter].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show transmitter]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTransmitter
        {
            get { return this.bShowTransmitter; }
            set
            {
                this.bShowTransmitter = value;

                this.lcvRFDevices.Refresh();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The RTT rx tx type filter
        /// </summary>
        private RxTxType rttRxTxTypeFilter = RxTxType.Empty;

        /// <summary>
        /// Gets or sets the rx tx type filter.
        /// </summary>
        /// <value>
        /// The rx tx type filter.
        /// </value>
        public RxTxType RxTxTypeFilter
        {
            get { return this.rttRxTxTypeFilter; }
            set
            {
                this.rttRxTxTypeFilter = value;

                this.lcvRFDevices.Refresh();

                FirePropertyChanged();
            }
        }
        #endregion


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// The quick commands
        /// </summary>
        // ReSharper disable CollectionNeverQueried.Global
        public ObservableStringCollection QuickCommands { get; set; } = new ObservableStringCollection();
        // ReSharper restore CollectionNeverQueried.Global


        /// <summary>
        /// Gets or sets the validation result.
        /// </summary>
        /// <value>
        /// The validation result.
        /// </value>
        public ValidationResultViewModelList ValidationResult { get; set; } = new ValidationResultViewModelList();

    } // end public partial class MainWindow
}
