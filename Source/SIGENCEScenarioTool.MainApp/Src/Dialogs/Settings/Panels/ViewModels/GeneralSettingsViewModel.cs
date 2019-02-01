/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */
using SIGENCEScenarioTool.Models;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels.ViewModels
{
    public sealed class GeneralSettingsViewModel : AbstractModelBase
    {
        private System.Int32 _UDPPortSending = 4242;

        public System.Int32 UDPPortSending
        {
            get { return _UDPPortSending; }
            set
            {
                this._UDPPortSending = value;
                FirePropertyChanged();
            }
        }

        private System.Int32 _UDPDelay = 100;

        public System.Int32 UDPDelay
        {
            get { return _UDPDelay; }
            set
            {
                this._UDPDelay = value;
                FirePropertyChanged();
            }
        }

        private System.Int32 _MapZoomLevel = 18;

        public System.Int32 MapZoomLevel
        {
            get { return _MapZoomLevel; }
            set
            {
                this._MapZoomLevel = value;
                FirePropertyChanged();
            }
        }

        private System.String _UDPHost = "127.0.0.1";

        public System.String UDPHost
        {
            get { return _UDPHost; }
            set
            {
                this._UDPHost = value;
                FirePropertyChanged();
            }
        }

        private System.Int32 _UDPPortReceiving = 7474;

        public System.Int32 UDPPortReceiving
        {
            get { return _UDPPortReceiving; }
            set
            {
                this._UDPPortReceiving = value;
                FirePropertyChanged();
            }
        }

        private System.Double _DeviceCopyTimeAddValue = 5;

        public System.Double DeviceCopyTimeAddValue
        {
            get { return _DeviceCopyTimeAddValue; }
            set
            {
                this._DeviceCopyTimeAddValue = value;
                FirePropertyChanged();
            }
        }

        private System.Double _InitialLatitude = 0;

        public System.Double InitialLatitude
        {
            get { return _InitialLatitude; }
            set
            {
                this._InitialLatitude = value;
                FirePropertyChanged();
            }
        }

        private System.Double _InitialLongitude = 0;

        public System.Double InitialLongitude
        {
            get { return _InitialLongitude; }
            set
            {
                this._InitialLongitude = value;
                FirePropertyChanged();
            }
        }

        private System.UInt32 _InitialZoom = 14;

        public System.UInt32 InitialZoom
        {
            get { return _InitialZoom; }
            set
            {
                this._InitialZoom = value;
                FirePropertyChanged();
            }
        }

        private System.String _InitialMap = "GoogleMap";

        public System.String InitialMap
        {
            get { return _InitialMap; }
            set
            {
                this._InitialMap = value;
                FirePropertyChanged();
            }
        }

        private System.Double _LastWidth = 0;

        public System.Double LastWidth
        {
            get { return _LastWidth; }
            set
            {
                this._LastWidth = value;
                FirePropertyChanged();
            }
        }

        private System.Double _LastHeight = 0;

        public System.Double LastHeight
        {
            get { return _LastHeight; }
            set
            {
                this._LastHeight = value;
                FirePropertyChanged();
            }
        }

        private System.Double _LastLeft = 0;

        public System.Double LastLeft
        {
            get { return _LastLeft; }
            set
            {
                this._LastLeft = value;
                FirePropertyChanged();
            }
        }

        private System.Double _LastTop = 0;

        public System.Double LastTop
        {
            get { return _LastTop; }
            set
            {
                this._LastTop = value;
                FirePropertyChanged();
            }
        }

        private System.String _LastWindowState = "";

        public System.String LastWindowState
        {
            get { return _LastWindowState; }
            set
            {
                this._LastWindowState = value;
                FirePropertyChanged();
            }
        }

        private System.Boolean _IsUpgraded = false;

        public System.Boolean IsUpgraded
        {
            get { return _IsUpgraded; }
            set
            {
                this._IsUpgraded = value;
                FirePropertyChanged();
            }
        }

        private System.Single _DensifyInMeters = 500;

        public System.Single DensifyInMeters
        {
            get { return _DensifyInMeters; }
            set
            {
                this._DensifyInMeters = value;
                FirePropertyChanged();
            }
        }

    
    } // end public sealed class GeneralSettingsViewModel
}
