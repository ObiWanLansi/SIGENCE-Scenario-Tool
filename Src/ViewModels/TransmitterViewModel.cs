using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using TransmitterTool.Markers;
using TransmitterTool.Models;



namespace TransmitterTool.ViewModels
{
    /// <summary>
    /// A ViewModel for a Transmitter.
    /// </summary>
    sealed public class TransmitterViewModel : INotifyPropertyChanged
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPropertyName"></param>
        private void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the transmitter.
        /// </summary>
        /// <value>
        /// The transmitter.
        /// </value>
        public Transmitter Transmitter { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return Transmitter.Name; }
            set
            {
                Transmitter.Name = value;
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
        public string Latitude
        {
            get { return string.Format("{0:F8}", Transmitter.Latitude); }
        }


        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public string Longitude
        {
            get { return string.Format("{0:F8}", Transmitter.Longitude); }
        }


        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>
        /// The altitude.
        /// </value>
        public uint Altitude
        {
            get { return Transmitter.Altitude; }
            set
            {
                Transmitter.Altitude = value;
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
            get { return Transmitter.Roll; }
            set
            {
                Transmitter.Roll = value;
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
            get { return Transmitter.Pitch; }
            set
            {
                Transmitter.Pitch = value;
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
            get { return Transmitter.Yaw; }
            set
            {
                Transmitter.Yaw = value;
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
            get { return Transmitter.RxTxType; }
            set
            {
                Transmitter.RxTxType = value;
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
            get { return Transmitter.AntennaType; }
            set
            {
                Transmitter.AntennaType = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the gain.
        /// </summary>
        /// <value>
        /// The gain.
        /// </value>
        public uint Gain
        {
            get { return Transmitter.Gain; }
            set
            {
                Transmitter.Gain = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the center frequency.
        /// </summary>
        /// <value>
        /// The center frequency.
        /// </value>
        public uint CenterFrequency
        {
            get { return Transmitter.CenterFrequency; }
            set
            {
                Transmitter.CenterFrequency = value;
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
            get { return Transmitter.Bandwith; }
            set
            {
                Transmitter.Bandwith = value;
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
            get { return Transmitter.SignalToNoiseRatio; }
            set
            {
                Transmitter.SignalToNoiseRatio = value;
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
            get { return Transmitter.XPos; }
            set
            {
                Transmitter.XPos = value;
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
            get { return Transmitter.YPos; }
            set
            {
                Transmitter.YPos = value;
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
            get { return Transmitter.ZPos; }
            set
            {
                Transmitter.ZPos = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public uint StartTime
        {
            get { return Transmitter.StartTime; }
            set
            {
                Transmitter.StartTime = value;
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
            get { return Transmitter.Remark; }
            set
            {
                Transmitter.Remark = value;
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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="TransmitterViewModel"/> class.
        /// </summary>
        /// <param name="t">The t.</param>
        public TransmitterViewModel(Transmitter t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            //-----------------------------------------------------------------

            this.Transmitter = t;

            this.Marker = new GMapMarker(new PointLatLng(t.Latitude, t.Longitude))
            {
                Offset = new Point(-15, -15),
                ZIndex = int.MaxValue
            };

            this.Marker.Shape = new CustomMarker(this.Marker, string.Format("{0}\n{1,1:00.########}\n{2,1:00.########}", t.Name, t.Latitude, t.Longitude));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Updates the marker tooltip.
        /// </summary>
        private void UpdateMarkerTooltip()
        {
            (this.Marker.Shape as CustomMarker).Title = Name;
        }

    } // end sealed public class TransmitterViewModel
}
