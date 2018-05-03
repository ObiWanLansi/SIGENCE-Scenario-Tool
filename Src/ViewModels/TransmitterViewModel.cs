using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        private void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( strPropertyName ) );
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
            get { return string.Format( "{0:F8}" , Transmitter.Latitude ); }
        }


        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public string Longitude
        {
            get { return string.Format( "{0:F8}" , Transmitter.Longitude ); }
        }


        public uint Altitude
        {
            get { return Transmitter.Altitude; }
            set
            {
                Transmitter.Altitude = value;
                FirePropertyChanged();
            }
        }


        public double Roll
        {
            get { return Transmitter.Roll; }
            set
            {
                Transmitter.Roll = value;
                FirePropertyChanged();
            }
        }


        public double Pitch
        {
            get { return Transmitter.Pitch; }
            set
            {
                Transmitter.Pitch = value;
                FirePropertyChanged();
            }
        }


        public double Yaw
        {
            get { return Transmitter.Yaw; }
            set
            {
                Transmitter.Yaw = value;
                FirePropertyChanged();
            }
        }

        public RxTxType RxTxType
        {
            get { return Transmitter.RxTxType; }
            set
            {
                Transmitter.RxTxType = value;
                FirePropertyChanged();
            }
        }

        public AntennaType AntennaType
        {
            get { return Transmitter.AntennaType; }
            set
            {
                Transmitter.AntennaType = value;
                FirePropertyChanged();
            }
        }

        public uint Gain
        {
            get { return Transmitter.Gain; }
            set
            {
                Transmitter.Gain = value;
                FirePropertyChanged();
            }
        }

        public uint CenterFrequency
        {
            get { return Transmitter.CenterFrequency; }
            set
            {
                Transmitter.CenterFrequency = value;
                FirePropertyChanged();
            }
        }

        public uint Bandwith
        {
            get { return Transmitter.Bandwith; }
            set
            {
                Transmitter.Bandwith = value;
                FirePropertyChanged();
            }
        }

        public uint SignalToNoiseRatio
        {
            get { return Transmitter.SignalToNoiseRatio; }
            set
            {
                Transmitter.SignalToNoiseRatio = value;
                FirePropertyChanged();
            }
        }

        public int XPos
        {
            get { return Transmitter.XPos; }
            set
            {
                Transmitter.XPos = value;
                FirePropertyChanged();
            }
        }
        
        public int YPos
        {
            get { return Transmitter.YPos; }
            set
            {
                Transmitter.YPos = value;
                FirePropertyChanged();
            }
        }

        public int ZPos
        {
            get { return Transmitter.ZPos; }
            set
            {
                Transmitter.ZPos = value;
                FirePropertyChanged();
            }
        }

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
        /// Initializes a new instance of the <see cref="TransmitterViewModel"/> class.
        /// </summary>
        /// <param name="t">The t.</param>
        public TransmitterViewModel( Transmitter t )
        {
            if( t == null )
            {
                throw new ArgumentNullException( "t" );
            }

            this.Transmitter = t;
        }

    } // end sealed public class TransmitterViewModel
}
