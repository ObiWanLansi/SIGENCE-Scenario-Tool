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
        public System.UInt32 UDPPortSending
        {
            get { return this.properties.UDPPortSending; }
            set
            {
                if(this.properties.UDPPortSending != value)
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
        public System.UInt32 UDPDelay
        {
            get { return this.properties.UDPDelay; }
            set
            {
                if(this.properties.UDPDelay != value)
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
        public System.UInt32 MapZoomLevel
        {
            get { return this.properties.MapZoomLevel; }
            set
            {
                if(this.properties.MapZoomLevel != value)
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
            get { return this.properties.UDPHost; }
            set
            {
                if(this.properties.UDPHost != value)
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
        public System.UInt32 UDPPortReceiving
        {
            get { return this.properties.UDPPortReceiving; }
            set
            {
                if(this.properties.UDPPortReceiving != value)
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
            get { return this.properties.DeviceCopyTimeAddValue; }
            set
            {
                if(this.properties.DeviceCopyTimeAddValue != value)
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
            get { return this.properties.InitialLatitude; }
            set
            {
                if(this.properties.InitialLatitude != value)
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
            get { return this.properties.InitialLongitude; }
            set
            {
                if(this.properties.InitialLongitude != value)
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
            get { return this.properties.InitialZoom; }
            set
            {
                if(this.properties.InitialZoom != value)
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
            get { return this.properties.InitialMap; }
            set
            {
                if(this.properties.InitialMap != value)
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
            get { return this.properties.LastWidth; }
            set
            {
                if(this.properties.LastWidth != value)
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
            get { return this.properties.LastHeight; }
            set
            {
                if(this.properties.LastHeight != value)
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
            get { return this.properties.LastLeft; }
            set
            {
                if(this.properties.LastLeft != value)
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
            get { return this.properties.LastTop; }
            set
            {
                if(this.properties.LastTop != value)
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
            get { return this.properties.LastWindowState; }
            set
            {
                if(this.properties.LastWindowState != value)
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
            get { return this.properties.IsUpgraded; }
            set
            {
                if(this.properties.IsUpgraded != value)
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
            get { return this.properties.DensifyInMeters; }
            set
            {
                if(this.properties.DensifyInMeters != value)
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
            get { return this.properties.LastOpenFiles; }
            set
            {
                if(this.properties.LastOpenFiles != value)
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
        public System.UInt32 MaxLastItems
        {
            get { return this.properties.MaxLastItems; }
            set
            {
                if(this.properties.MaxLastItems != value)
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
            get { return this.properties.EnableMQTT; }
            set
            {
                if(this.properties.EnableMQTT != value)
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
            get { return this.properties.MQTTHost; }
            set
            {
                if(this.properties.MQTTHost != value)
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
        public System.UInt32 MQTTPort
        {
            get { return this.properties.MQTTPort; }
            set
            {
                if(this.properties.MQTTPort != value)
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
            get { return this.properties.EnableUDP; }
            set
            {
                if(this.properties.EnableUDP != value)
                {
                    this.properties.EnableUDP = value;

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
        public System.String HelpPath
        {
            get { return this.properties.HelpPath; }
            set
            {
                if(this.properties.HelpPath != value)
                {
                    this.properties.HelpPath = value;

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
        public System.UInt32 HelpPort
        {
            get { return this.properties.HelpPort; }
            set
            {
                if(this.properties.HelpPort != value)
                {
                    this.properties.HelpPort = value;

                    FirePropertyChanged();
                }
            }
        }

    
    } // end internal sealed class GeneralSettingsViewModel
}
