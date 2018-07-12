using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using SIGENCEScenarioTool.Markers;
using SIGENCEScenarioTool.Models;



namespace SIGENCEScenarioTool.ViewModels
{
    /// <summary>
    /// A ViewModel for a RFDevice.
    /// </summary>
    sealed public class RFDeviceViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// The mc map control
        /// </summary>
        private readonly GMapControl mcMapControl = null;

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
            PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( strPropertyName ) );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the RFDevice.
        /// </summary>
        /// <value>
        /// The RFDevice.
        /// </value>
        public RFDevice RFDevice { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get { return RFDevice.Id; }
            set
            {
                RFDevice.Id = value;

                UpdateMarkerShape();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public double StartTime
        {
            get { return RFDevice.StartTime; }
            set
            {
                RFDevice.StartTime = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return RFDevice.Name; }
            set
            {
                RFDevice.Name = value;

                UpdateMarkerTooltip();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude
        {
            get { return RFDevice.Latitude; }
            set
            {
                RFDevice.Latitude = value;

                UpdateMarkerPosition();
                UpdateMarkerTooltip();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude
        {
            get { return RFDevice.Longitude; }
            set
            {
                RFDevice.Longitude = value;

                UpdateMarkerPosition();
                UpdateMarkerTooltip();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>
        /// The altitude.
        /// </value>
        public uint Altitude
        {
            get { return RFDevice.Altitude; }
            set
            {
                RFDevice.Altitude = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the roll.
        /// </summary>
        /// <value>
        /// The roll.
        /// </value>
        public double Roll
        {
            get { return RFDevice.Roll; }
            set
            {
                RFDevice.Roll = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        public double Pitch
        {
            get { return RFDevice.Pitch; }
            set
            {
                RFDevice.Pitch = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>
        /// The yaw.
        /// </value>
        public double Yaw
        {
            get { return RFDevice.Yaw; }
            set
            {
                RFDevice.Yaw = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the type of the rx tx.
        /// </summary>
        /// <value>
        /// The type of the rx tx.
        /// </value>
        public RxTxType RxTxType
        {
            get { return RFDevice.RxTxType; }
            set
            {
                RFDevice.RxTxType = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the type of the antenna.
        /// </summary>
        /// <value>
        /// The type of the antenna.
        /// </value>
        public AntennaType AntennaType
        {
            get { return RFDevice.AntennaType; }
            set
            {
                RFDevice.AntennaType = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the gain.
        /// </summary>
        /// <value>
        /// The gain.
        /// </value>
        public int Gain
        {
            get { return RFDevice.Gain_dB; }
            set
            {
                RFDevice.Gain_dB = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the center frequency.
        /// </summary>
        /// <value>
        /// The center frequency.
        /// </value>
        public ulong CenterFrequency
        {
            get { return RFDevice.CenterFrequency_Hz; }
            set
            {
                RFDevice.CenterFrequency_Hz = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the bandwith.
        /// </summary>
        /// <value>
        /// The bandwith.
        /// </value>
        public uint Bandwith
        {
            get { return RFDevice.Bandwith_Hz; }
            set
            {
                RFDevice.Bandwith_Hz = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the signal to noise ratio.
        /// </summary>
        /// <value>
        /// The signal to noise ratio.
        /// </value>
        public uint SignalToNoiseRatio
        {
            get { return RFDevice.SignalToNoiseRatio_dB; }
            set
            {
                RFDevice.SignalToNoiseRatio_dB = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>
        /// The x position.
        /// </value>
        public int XPos
        {
            get { return RFDevice.XPos; }
            set
            {
                RFDevice.XPos = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>
        /// The y position.
        /// </value>
        public int YPos
        {
            get { return RFDevice.YPos; }
            set
            {
                RFDevice.YPos = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the z position.
        /// </summary>
        /// <value>
        /// The z position.
        /// </value>
        public int ZPos
        {
            get { return RFDevice.ZPos; }
            set
            {
                RFDevice.ZPos = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark
        {
            get { return RFDevice.Remark; }
            set
            {
                RFDevice.Remark = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The marker
        /// </summary>
        private GMapMarker _Marker = null;

        /// <summary>
        /// Gets or sets the marker.
        /// </summary>
        /// <value>
        /// The marker.
        /// </value>
        public GMapMarker Marker
        {
            get { return _Marker; }
            set
            {
                _Marker = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b is selected
        /// </summary>
        private bool bIsSelected = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get { return bIsSelected; }
            set
            {
                bIsSelected = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public DeviceType DeviceType
        {
            get
            {
                if( RFDevice.Id == 0 )
                {
                    return DeviceType.Reference;
                }

                if( RFDevice.Id > 0 )
                {
                    return DeviceType.Transmitter;
                }

                if( RFDevice.Id < 0 )
                {
                    return DeviceType.Receiver;
                }

                return DeviceType.Unknown;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceViewModel" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="device">The device.</param>
        /// <exception cref="ArgumentNullException">device</exception>
        public RFDeviceViewModel( GMapControl mcMapControl , RFDevice device )
        {
            if( device == null )
            {
                throw new ArgumentNullException( "device" );
            }

            if( mcMapControl == null )
            {
                throw new ArgumentNullException( "mcMapControl" );
            }

            //-----------------------------------------------------------------

            this.mcMapControl = mcMapControl;

            this.RFDevice = device;

            this.Marker = new GMapMarker( new PointLatLng( device.Latitude , device.Longitude ) )
            {
                Offset = new Point( -15 , -15 ) ,
                ZIndex = int.MaxValue
            };

            UpdateMarkerShape();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the tool tip.
        /// </summary>
        /// <returns></returns>
        private string GetToolTip()
        {
            return string.Format( "- {0} -\n{1} ({2})\n{3,1:00.########}\n{4,1:00.########}" , DeviceType , RFDevice.Name , RFDevice.Id , RFDevice.Latitude , RFDevice.Longitude );
        }


        /// <summary>
        /// Updates the marker tooltip.
        /// </summary>
        private void UpdateMarkerTooltip()
        {
            if( this.Marker.Shape is CircleMarker )
            {
                ( this.Marker.Shape as CircleMarker ).MarkerToolTip = GetToolTip();
                return;
            }

            if( this.Marker.Shape is RectangleMarker )
            {
                ( this.Marker.Shape as RectangleMarker ).MarkerToolTip = GetToolTip();
                return;
            }

            if( this.Marker.Shape is TriangleMarker )
            {
                ( this.Marker.Shape as TriangleMarker ).MarkerToolTip = GetToolTip();
                return;
            }
        }


        /// <summary>
        /// Updates the marker shape.
        /// </summary>
        private void UpdateMarkerShape()
        {
            if( this.Marker.Shape != null )
            {
                //TODO: Remove Old Event OnPositionChanged ...
                this.Marker.Shape = null;
            }


//#if DEBUG
//            if( RFDevice.Id == 42 )
//            {
//                var shape = new DiamondMarker( this.mcMapControl , this.Marker , GetToolTip() );
//                shape.OnPositionChanged += Shape_OnPositionChanged;
//                this.Marker.Shape = shape;
//                return;
//            }

//#endif

            // Reference Transmitter
            if( RFDevice.Id == 0 )
            {
                var shape = new CircleMarker( this.mcMapControl , this.Marker , GetToolTip() );
                shape.OnPositionChanged += Shape_OnPositionChanged;
                this.Marker.Shape = shape;
                return;
            }

            // Receiver
            if( RFDevice.Id < 0 )
            {
                var shape = new RectangleMarker( this.mcMapControl , this.Marker , GetToolTip() );
                shape.OnPositionChanged += Shape_OnPositionChanged;
                this.Marker.Shape = shape;
                return;
            }

            // Last but not least all other are transmitters ... 
            {
                var shape = new TriangleMarker( this.mcMapControl , this.Marker , GetToolTip() );
                shape.OnPositionChanged += Shape_OnPositionChanged;
                this.Marker.Shape = shape;
            }
        }


        /// <summary>
        /// Updates the marker position.
        /// </summary>
        private void UpdateMarkerPosition()
        {
            this.Marker.Position = new PointLatLng( RFDevice.Latitude , RFDevice.Longitude );
        }


        /// <summary>
        /// Shapes the on position changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pll">The PLL.</param>
        private void Shape_OnPositionChanged( object sender , PointLatLng pll )
        {
            RFDevice.Latitude = pll.Lat;
            RFDevice.Longitude = pll.Lng;

            //UpdateMarkerPosition();
            UpdateMarkerTooltip();

            FirePropertyChanged( "Latitude" );
            FirePropertyChanged( "Longitude" );
        }

    } // end sealed public class RFDeviceViewModel
}
