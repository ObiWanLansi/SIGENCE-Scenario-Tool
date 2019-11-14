
/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */

using System;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Datatypes.Physically;
using SIGENCEScenarioTool.Models.RxTxTypes;



namespace SIGENCEScenarioTool.Models
{
    ///<summary>
    /// Represent A Device Based On A Radio Frequency.
    ///</summary>
    public static class RFDeviceExtensions
    {
        
        public static RFDevice WithPrimaryKey(this RFDevice instance,Guid value)
        {
            instance.PrimaryKey = value;
            return instance;
        }


        public static RFDevice WithId(this RFDevice instance,int value)
        {
            instance.Id = value;
            return instance;
        }


        public static RFDevice WithDeviceSource(this RFDevice instance,DeviceSource value)
        {
            instance.DeviceSource = value;
            return instance;
        }


        public static RFDevice WithStartTime(this RFDevice instance,double value)
        {
            instance.StartTime = value;
            return instance;
        }


        public static RFDevice WithName(this RFDevice instance,string value)
        {
            instance.Name = value;
            return instance;
        }


        public static RFDevice WithLatitude(this RFDevice instance,Latitude value)
        {
            instance.Latitude = value;
            return instance;
        }


        public static RFDevice WithLongitude(this RFDevice instance,Longitude value)
        {
            instance.Longitude = value;
            return instance;
        }


        public static RFDevice WithAltitude(this RFDevice instance,Altitude value)
        {
            instance.Altitude = value;
            return instance;
        }


        public static RFDevice WithRoll(this RFDevice instance,double value)
        {
            instance.Roll = value;
            return instance;
        }


        public static RFDevice WithPitch(this RFDevice instance,double value)
        {
            instance.Pitch = value;
            return instance;
        }


        public static RFDevice WithYaw(this RFDevice instance,double value)
        {
            instance.Yaw = value;
            return instance;
        }


        public static RFDevice WithRxTxType(this RFDevice instance,RxTxType value)
        {
            instance.RxTxType = value;
            return instance;
        }


        public static RFDevice WithAntennaType(this RFDevice instance,AntennaType value)
        {
            instance.AntennaType = value;
            return instance;
        }


        public static RFDevice WithCenterFrequency_Hz(this RFDevice instance,Frequency value)
        {
            instance.CenterFrequency_Hz = value;
            return instance;
        }


        public static RFDevice WithBandwidth_Hz(this RFDevice instance,Bandwidth value)
        {
            instance.Bandwidth_Hz = value;
            return instance;
        }


        public static RFDevice WithGain_dB(this RFDevice instance,Gain value)
        {
            instance.Gain_dB = value;
            return instance;
        }


        public static RFDevice WithSignalToNoiseRatio_dB(this RFDevice instance,SignalToNoiseRatio value)
        {
            instance.SignalToNoiseRatio_dB = value;
            return instance;
        }


        public static RFDevice WithXPos(this RFDevice instance,int value)
        {
            instance.XPos = value;
            return instance;
        }


        public static RFDevice WithYPos(this RFDevice instance,int value)
        {
            instance.YPos = value;
            return instance;
        }


        public static RFDevice WithZPos(this RFDevice instance,int value)
        {
            instance.ZPos = value;
            return instance;
        }


        public static RFDevice WithTechnicalParameters(this RFDevice instance,string value)
        {
            instance.TechnicalParameters = value;
            return instance;
        }


        public static RFDevice WithRemark(this RFDevice instance,string value)
        {
            instance.Remark = value;
            return instance;
        }

    } // end public static class  RFDeviceExtensions
}