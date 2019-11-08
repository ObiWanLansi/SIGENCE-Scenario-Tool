using System;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models.Validation;



namespace SIGENCEScenarioTool.Models
{
    /// <summary>
    /// Addition Functions For An RFDevice.
    /// </summary>
    /// <seealso cref="System.IEquatable{RFDevice}" />
    /// <seealso cref="AbstractModelBase" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="ICloneable" />
    /// <seealso cref="Interfaces.IXmlExport" />
    public partial class RFDevice
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RFDevice"/> class.
        /// </summary>
        public RFDevice()
        {
            this.PrimaryKey = Guid.NewGuid();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public ValidationResultList Validate()
        {
            ValidationResultList vrl = ValidationResultList.Empty;

            //-----------------------------------------------------------------

            if (this.PrimaryKey == Guid.Empty)
            {
                vrl.Add(Servity.Fatal, "The PrimaryKey Is Empty!", this, "PrimaryKey", this.PrimaryKey);
            }

            if (this.Name.IsEmpty() == true)
            {
                vrl.Add(Servity.Warning, "The Name Is Empty!", this, "Name", this.Name);
            }

            if (this.DeviceSource == DeviceSource.Unknown)
            {
                vrl.Add(Servity.Warning, "The DeviceSource Is Unknown!", this, "DeviceSource", this.DeviceSource);
            }

            if (this.StartTime < 0)
            {
                vrl.Add(Servity.Error, "The StartTime Is Less Than Zero!", this, "StartTime", this.StartTime);
            }

            //-----------------------------------------------------------------

            if (this.Latitude < -90 || this.Latitude > 90)
            {
                vrl.Add(Servity.Error, "The Latitude Is Not In The Normal Range (-90/90)!", this, "Latitude", this.Latitude);
            }

            if (this.Longitude < -180 || this.Longitude > 180)
            {
                vrl.Add(Servity.Error, "The Longitude Is Not In The Normal Range (-180/180)!", this, "Longitude", this.Longitude);
            }

            if (this.Altitude > 20000)
            {
                vrl.Add(Servity.Warning, "The Altitude Is Over 20000m!", this, "Altitude", this.Altitude);
            }

            //-----------------------------------------------------------------

            //TODO: Question the right values here
            if (this.Roll < -360 || this.Roll > 360)
            {
                vrl.Add(Servity.Warning, "The Roll Is Not In The Normal Range (-360/360)!", this, "Roll", this.Roll);
            }

            if (this.Pitch < -90 || this.Pitch > 90)
            {
                vrl.Add(Servity.Warning, "The Pitch Is Not In The Normal Range (-90/90)!", this, "Pitch", this.Pitch);
            }

            if (this.Yaw < -180 || this.Yaw > 180)
            {
                vrl.Add(Servity.Warning, "The Yaw Is Not In The Normal Range (-180/180)!", this, "Yaw", this.Yaw);
            }

            //-----------------------------------------------------------------

            if (this.RxTxType == null)
            {
                vrl.Add(Servity.Error, "The RxTxType Is Unknown!", this, "RxTxType", this.RxTxType);
            }

            if (this.RxTxType == RxTxTypes.RxTxTypes.Unknown)
            {
                vrl.Add(Servity.Warning, "The RxTxType Is Unknown!", this, "RxTxType", this.RxTxType);
            }

            // Check The RxTxType At Validation (https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/issues/154)
            if (RxTxTypes.RxTxTypes.IsValidForId(this.Id, this.RxTxType) == false)
            {
                vrl.Add(Servity.Warning, "The RxTxType Is Not Valid For This Device!", this, "RxTxType", this.RxTxType);
            }

            if (this.AntennaType == AntennaType.Unknown)
            {
                vrl.Add(Servity.Warning, "The AntennaType Is Unknown!", this, "AntennaType", this.AntennaType);
            }

            //-----------------------------------------------------------------

            //  TODO: Validate the following properties:
            //  CenterFrequency_Hz
            //  Bandwith_Hz
            //  Gain_dB
            //  SignalToNoiseRatio_dB

            //-----------------------------------------------------------------

            if (this.Remark.IsEmpty() == true)
            {
                vrl.Add(Servity.Information, "The Remark Is Empty!", this, "Remark", this.Remark);
            }

            //-----------------------------------------------------------------

            // vrl.Add( Servity., "" , this );

            return vrl;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{(string.IsNullOrEmpty(this.Name) ? "Unknown" : this.Name)} ({this.Id})";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the dummy.
        /// </summary>
        /// <returns></returns>
        static public RFDevice CreateDummy()
        {
            return new RFDevice
            {
                Id = 42,
                Name = "Dummy Device",
                DeviceSource = DeviceSource.User,
                StartTime = 3.14,
                Latitude = 47.666557,
                Longitude = 9.386941,
                Altitude = 1234,
                AntennaType = AntennaType.HyperLOG60200,
                RxTxType = RxTxTypes.RxTxTypes.FMBroadcast,
                CenterFrequency_Hz = 90_000_000,
                Bandwidth_Hz = 30_000,
                Gain_dB = 42,
                SignalToNoiseRatio_dB = 42,
                Roll = 123,
                Pitch = 123,
                Yaw = 123,
                XPos = 15,
                YPos = 10,
                ZPos = 74,
                Remark = Tools.Tool.FRANZ,
                TechnicalParameters = "SPEED:60;DIRECTION:-42;"
            };
        }


        ///// <summary>
        ///// The dummy
        ///// </summary>
        //static public readonly RFDevice DUMMY = new RFDevice
        //{
        //    Id = 42,
        //    Name = "Dummy Device",
        //    DeviceSource = DeviceSource.User,
        //    StartTime = 3.14,
        //    Latitude = 47.666557,
        //    Longitude = 9.386941,
        //    Altitude = 1234,
        //    AntennaType = AntennaType.HyperLOG60200,
        //    RxTxType = RxTxTypes.RxTxTypes.FMBroadcast,
        //    CenterFrequency_Hz = 90_000_000,
        //    Bandwidth_Hz = 30_000,
        //    Gain_dB = 42,
        //    SignalToNoiseRatio_dB = 42,
        //    Roll = 123,
        //    Pitch = 123,
        //    Yaw = 123,
        //    XPos = 15,
        //    YPos = 10,
        //    ZPos = 74,
        //    Remark = Tools.Tool.FOX,
        //    TechnicalParameters = "SPEED:60;DIRECTION:-42;"
        //};

    } // end public partial class RFDevice
}
