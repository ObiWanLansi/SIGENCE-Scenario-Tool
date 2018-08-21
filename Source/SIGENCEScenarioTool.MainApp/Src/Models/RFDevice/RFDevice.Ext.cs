using System;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models.Validation;



namespace SIGENCEScenarioTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{SIGENCEScenarioTool.Models.RFDevice}" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.ICloneable" />
    /// <seealso cref="SIGENCEScenarioTool.Interfaces.IXmlExport" />
    sealed public partial class RFDevice
    {
        public ValidationResultList Validate()
        {
            ValidationResultList vrl = ValidationResultList.Empty;

            //-----------------------------------------------------------------

            if( PrimaryKey == Guid.Empty )
            {
                vrl.Add( Servity.Fatal , "The PrimaryKey Is Empty!" , this );
            }

            if( Name.IsEmpty() == true )
            {
                vrl.Add( Servity.Warning , "The Name Is Empty!" , this );
            }

            if( DeviceSource == DeviceSource.Unknown )
            {
                vrl.Add( Servity.Warning , "The DeviceSource Is Unknown!" , this );
            }

            if( StartTime < 0 )
            {
                vrl.Add( Servity.Error , "The StartTime Is Less Than Zero!" , this );
            }

            //-----------------------------------------------------------------

            if( Latitude < -90 || Latitude > 90 )
            {
                vrl.Add( Servity.Error , "The Latitude Is Not In The Normal Range (-90/90)!" , this );
            }

            if( Longitude < -180 || Longitude > 180 )
            {
                vrl.Add( Servity.Error , "The Longitude Is Not In The Normal Range (-180/180)!" , this );
            }

            if( Altitude > 20000 )
            {
                vrl.Add( Servity.Warning , "The Altitude Is Over 20000m!" , this );
            }

            //-----------------------------------------------------------------

            //TODO: Question the right values here
            if( Roll < -360 || Roll > 360 )
            {
                vrl.Add( Servity.Warning , "The Roll Is Not In The Normal Range (-360/360)!" , this );
            }

            if( Pitch < -360 || Pitch > 360 )
            {
                vrl.Add( Servity.Warning , "The Pitch Is Not In The Normal Range (-360/360)!" , this );
            }

            if( Yaw < -360 || Yaw > 360 )
            {
                vrl.Add( Servity.Warning , "The Yaw Is Not In The Normal Range (-360/360)!" , this );
            }

            //-----------------------------------------------------------------

            if( RxTxType == RxTxType.Unknown )
            {
                vrl.Add( Servity.Warning , "The RxTxType Is Unknown!" , this );
            }

            if( AntennaType == AntennaType.Unknown )
            {
                vrl.Add( Servity.Warning , "The AntennaType Is Unknown!" , this );
            }

            //-----------------------------------------------------------------

            //  TODO:
            //  CenterFrequency_Hz
            //  Bandwith_Hz
            //  Gain_dB
            //  SignalToNoiseRatio_dB

            //-----------------------------------------------------------------

            if( Remark.IsEmpty() == true )
            {
                vrl.Add( Servity.Information , "The Remark Is Empty!" , this );
            }

            //-----------------------------------------------------------------

            // vrl.Add( Servity., "" , this );

            return vrl;
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format( "{0} ({1})" , string.IsNullOrEmpty( Name ) ? "Unknown" : Name , Id );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    } // end sealed public partial class RFDevice
}
