/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */
using SIGENCEScenarioTool.Models;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Models.AbstractModelBase" />
    internal sealed class GeneralSettingsViewModel : AbstractModelBase
    {
        /// <summary>
        /// The properties
        /// </summary>
        private readonly Properties.Settings properties = Properties.Settings.Default;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Int32 UDPPortSending
        {
            get => this.properties.UDPPortSending;
            set
            {
                if( this.properties.UDPPortSending != value )
                {
                    this.properties.UDPPortSending = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Int32 UDPDelay
        {
            get => this.properties.UDPDelay;
            set
            {
                if( this.properties.UDPDelay != value )
                {
                    this.properties.UDPDelay = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Int32 MapZoomLevel
        {
            get => this.properties.MapZoomLevel;
            set
            {
                if( this.properties.MapZoomLevel != value )
                {
                    this.properties.MapZoomLevel = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.String UDPHost
        {
            get => this.properties.UDPHost;
            set
            {
                if( this.properties.UDPHost != value )
                {
                    this.properties.UDPHost = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Int32 UDPPortReceiving
        {
            get => this.properties.UDPPortReceiving;
            set
            {
                if( this.properties.UDPPortReceiving != value )
                {
                    this.properties.UDPPortReceiving = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Double DeviceCopyTimeAddValue
        {
            get => this.properties.DeviceCopyTimeAddValue;
            set
            {
                if( this.properties.DeviceCopyTimeAddValue != value )
                {
                    this.properties.DeviceCopyTimeAddValue = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Double InitialLatitude
        {
            get => this.properties.InitialLatitude;
            set
            {
                if( this.properties.InitialLatitude != value )
                {
                    this.properties.InitialLatitude = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Double InitialLongitude
        {
            get => this.properties.InitialLongitude;
            set
            {
                if( this.properties.InitialLongitude != value )
                {
                    this.properties.InitialLongitude = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.UInt32 InitialZoom
        {
            get => this.properties.InitialZoom;
            set
            {
                if( this.properties.InitialZoom != value )
                {
                    this.properties.InitialZoom = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.String InitialMap
        {
            get => this.properties.InitialMap;
            set
            {
                if( this.properties.InitialMap != value )
                {
                    this.properties.InitialMap = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Double LastWidth
        {
            get => this.properties.LastWidth;
            set
            {
                if( this.properties.LastWidth != value )
                {
                    this.properties.LastWidth = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Double LastHeight
        {
            get => this.properties.LastHeight;
            set
            {
                if( this.properties.LastHeight != value )
                {
                    this.properties.LastHeight = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Double LastLeft
        {
            get => this.properties.LastLeft;
            set
            {
                if( this.properties.LastLeft != value )
                {
                    this.properties.LastLeft = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Double LastTop
        {
            get => this.properties.LastTop;
            set
            {
                if( this.properties.LastTop != value )
                {
                    this.properties.LastTop = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.String LastWindowState
        {
            get => this.properties.LastWindowState;
            set
            {
                if( this.properties.LastWindowState != value )
                {
                    this.properties.LastWindowState = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Boolean IsUpgraded
        {
            get => this.properties.IsUpgraded;
            set
            {
                if( this.properties.IsUpgraded != value )
                {
                    this.properties.IsUpgraded = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Single DensifyInMeters
        {
            get => this.properties.DensifyInMeters;
            set
            {
                if( this.properties.DensifyInMeters != value )
                {
                    this.properties.DensifyInMeters = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Collections.Specialized.StringCollection LastOpenFiles
        {
            get => this.properties.LastOpenFiles;
            set
            {
                if( this.properties.LastOpenFiles != value )
                {
                    this.properties.LastOpenFiles = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Int32 MaxLastItems
        {
            get => this.properties.MaxLastItems;
            set
            {
                if( this.properties.MaxLastItems != value )
                {
                    this.properties.MaxLastItems = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Boolean EnableMQTT
        {
            get => this.properties.EnableMQTT;
            set
            {
                if( this.properties.EnableMQTT != value )
                {
                    this.properties.EnableMQTT = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.String MQTTHost
        {
            get => this.properties.MQTTHost;
            set
            {
                if( this.properties.MQTTHost != value )
                {
                    this.properties.MQTTHost = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Int32 MQTTPort
        {
            get => this.properties.MQTTPort;
            set
            {
                if( this.properties.MQTTPort != value )
                {
                    this.properties.MQTTPort = value;

                    FirePropertyChanged();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <value>
        ///
        /// </value>
        public System.Boolean EnableUDP
        {
            get => this.properties.EnableUDP;
            set
            {
                if( this.properties.EnableUDP != value )
                {
                    this.properties.EnableUDP = value;

                    FirePropertyChanged();
                }
            }
        }


    } // end internal sealed class GeneralSettingsViewModel
}
