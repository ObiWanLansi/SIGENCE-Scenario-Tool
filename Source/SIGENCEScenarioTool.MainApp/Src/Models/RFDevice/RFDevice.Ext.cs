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

            if (PrimaryKey == Guid.Empty)
            {
                vrl.Add(Servity.Fatal, "The PrimaryKey Is Empty!", this, "PrimaryKey", PrimaryKey);
            }

            if (Name.IsEmpty() == true)
            {
                vrl.Add(Servity.Warning, "The Name Is Empty!", this, "Name", Name);
            }

            if (DeviceSource == DeviceSource.Unknown)
            {
                vrl.Add(Servity.Warning, "The DeviceSource Is Unknown!", this, "DeviceSource", DeviceSource);
            }

            if (StartTime < 0)
            {
                vrl.Add(Servity.Error, "The StartTime Is Less Than Zero!", this, "StartTime", StartTime);
            }

            //-----------------------------------------------------------------

            if (Latitude < -90 || Latitude > 90)
            {
                vrl.Add(Servity.Error, "The Latitude Is Not In The Normal Range (-90/90)!", this, "Latitude", Latitude);
            }

            if (Longitude < -180 || Longitude > 180)
            {
                vrl.Add(Servity.Error, "The Longitude Is Not In The Normal Range (-180/180)!", this, "Longitude", Longitude);
            }

            if (Altitude > 20000)
            {
                vrl.Add(Servity.Warning, "The Altitude Is Over 20000m!", this, "Altitude", Altitude);
            }

            //-----------------------------------------------------------------

            //TODO: Question the right values here
            if (Roll < -360 || Roll > 360)
            {
                vrl.Add(Servity.Warning, "The Roll Is Not In The Normal Range (-360/360)!", this, "Roll", Roll);
            }

            if (Pitch < -360 || Pitch > 360)
            {
                vrl.Add(Servity.Warning, "The Pitch Is Not In The Normal Range (-360/360)!", this, "Pitch", Pitch);
            }

            if (Yaw < -360 || Yaw > 360)
            {
                vrl.Add(Servity.Warning, "The Yaw Is Not In The Normal Range (-360/360)!", this, "Yaw", Yaw);
            }

            //-----------------------------------------------------------------

            if (RxTxType == RxTxType.Unknown)
            {
                vrl.Add(Servity.Warning, "The RxTxType Is Unknown!", this, "RxTxType", RxTxType);
            }

            if (AntennaType == AntennaType.Unknown)
            {
                vrl.Add(Servity.Warning, "The AntennaType Is Unknown!", this, "AntennaType", AntennaType);
            }

            //-----------------------------------------------------------------

            //  TODO:
            //  CenterFrequency_Hz
            //  Bandwith_Hz
            //  Gain_dB
            //  SignalToNoiseRatio_dB

            //-----------------------------------------------------------------

            if (Remark.IsEmpty() == true)
            {
                vrl.Add(Servity.Information, "The Remark Is Empty!", this, "Remark", Remark);
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
            return string.Format("{0} ({1})", string.IsNullOrEmpty(Name) ? "Unknown" : Name, Id);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    } // end sealed public partial class RFDevice
}
