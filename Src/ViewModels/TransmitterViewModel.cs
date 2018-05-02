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
            set { Transmitter.Name = value; }
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


        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark
        {
            get { return Transmitter.Remark; }
            set { Transmitter.Remark = value; }
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
