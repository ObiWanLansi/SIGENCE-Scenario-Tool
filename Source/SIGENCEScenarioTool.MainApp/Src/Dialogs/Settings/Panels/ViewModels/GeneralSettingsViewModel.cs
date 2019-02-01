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


        public System.Int32 UDPPortSending
        {
            get { return this.properties.UDPPortSending; }
            set
            {
                this.properties.UDPPortSending = value;
                FirePropertyChanged();
            }
        }

        public System.Int32 UDPDelay
        {
            get { return this.properties.UDPDelay; }
            set
            {
                this.properties.UDPDelay = value;
                FirePropertyChanged();
            }
        }

        public System.Int32 MapZoomLevel
        {
            get { return this.properties.MapZoomLevel; }
            set
            {
                this.properties.MapZoomLevel = value;
                FirePropertyChanged();
            }
        }

        public System.String UDPHost
        {
            get { return this.properties.UDPHost; }
            set
            {
                this.properties.UDPHost = value;
                FirePropertyChanged();
            }
        }

        public System.Int32 UDPPortReceiving
        {
            get { return this.properties.UDPPortReceiving; }
            set
            {
                this.properties.UDPPortReceiving = value;
                FirePropertyChanged();
            }
        }

        public System.Double DeviceCopyTimeAddValue
        {
            get { return this.properties.DeviceCopyTimeAddValue; }
            set
            {
                this.properties.DeviceCopyTimeAddValue = value;
                FirePropertyChanged();
            }
        }

        public System.Double InitialLatitude
        {
            get { return this.properties.InitialLatitude; }
            set
            {
                this.properties.InitialLatitude = value;
                FirePropertyChanged();
            }
        }

        public System.Double InitialLongitude
        {
            get { return this.properties.InitialLongitude; }
            set
            {
                this.properties.InitialLongitude = value;
                FirePropertyChanged();
            }
        }

        public System.UInt32 InitialZoom
        {
            get { return this.properties.InitialZoom; }
            set
            {
                this.properties.InitialZoom = value;
                FirePropertyChanged();
            }
        }

        public System.String InitialMap
        {
            get { return this.properties.InitialMap; }
            set
            {
                this.properties.InitialMap = value;
                FirePropertyChanged();
            }
        }

        public System.Double LastWidth
        {
            get { return this.properties.LastWidth; }
            set
            {
                this.properties.LastWidth = value;
                FirePropertyChanged();
            }
        }

        public System.Double LastHeight
        {
            get { return this.properties.LastHeight; }
            set
            {
                this.properties.LastHeight = value;
                FirePropertyChanged();
            }
        }

        public System.Double LastLeft
        {
            get { return this.properties.LastLeft; }
            set
            {
                this.properties.LastLeft = value;
                FirePropertyChanged();
            }
        }

        public System.Double LastTop
        {
            get { return this.properties.LastTop; }
            set
            {
                this.properties.LastTop = value;
                FirePropertyChanged();
            }
        }

        public System.String LastWindowState
        {
            get { return this.properties.LastWindowState; }
            set
            {
                this.properties.LastWindowState = value;
                FirePropertyChanged();
            }
        }

        public System.Boolean IsUpgraded
        {
            get { return this.properties.IsUpgraded; }
            set
            {
                this.properties.IsUpgraded = value;
                FirePropertyChanged();
            }
        }

        public System.Single DensifyInMeters
        {
            get { return this.properties.DensifyInMeters; }
            set
            {
                this.properties.DensifyInMeters = value;
                FirePropertyChanged();
            }
        }
    
    } // end internal sealed class GeneralSettingsViewModel
}
