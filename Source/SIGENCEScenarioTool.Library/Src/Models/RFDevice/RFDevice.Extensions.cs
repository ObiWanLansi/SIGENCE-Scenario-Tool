
/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */

using System;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Interfaces;
using SIGENCEScenarioTool.Datatypes;
using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Datatypes.Physically;




namespace SIGENCEScenarioTool.Models
{
    ///<summary>
    /// Represent A Device Based On A Radio Frequency.
    ///</summary>
    public static class RFDeviceExtensions
    {
        
        static public RFDevice WithPrimaryKey(this RFDevice instance,Guid value)
        {
            instance.PrimaryKey = value;
            return instance;
        }


        static public RFDevice WithId(this RFDevice instance,int value)
        {
            instance.Id = value;
            return instance;
        }


        static public RFDevice WithDeviceSource(this RFDevice instance,DeviceSource value)
        {
            instance.DeviceSource = value;
            return instance;
        }


        static public RFDevice WithStartTime(this RFDevice instance,double value)
        {
            instance.StartTime = value;
            return instance;
        }


        static public RFDevice WithName(this RFDevice instance,string value)
        {
            instance.Name = value;
            return instance;
        }


        static public RFDevice WithLatitude(this RFDevice instance,Latitude value)
        {
            instance.Latitude = value;
            return instance;
        }


        static public RFDevice WithLongitude(this RFDevice instance,Longitude value)
        {
            instance.Longitude = value;
            return instance;
        }


        static public RFDevice WithAltitude(this RFDevice instance,Altitude value)
        {
            instance.Altitude = value;
            return instance;
        }


        static public RFDevice WithRoll(this RFDevice instance,double value)
        {
            instance.Roll = value;
            return instance;
        }


        static public RFDevice WithPitch(this RFDevice instance,double value)
        {
            instance.Pitch = value;
            return instance;
        }


        static public RFDevice WithYaw(this RFDevice instance,double value)
        {
            instance.Yaw = value;
            return instance;
        }


        static public RFDevice WithRxTxType(this RFDevice instance,RxTxType value)
        {
            instance.RxTxType = value;
            return instance;
        }


        static public RFDevice WithAntennaType(this RFDevice instance,AntennaType value)
        {
            instance.AntennaType = value;
            return instance;
        }


        static public RFDevice WithCenterFrequency_Hz(this RFDevice instance,ulong value)
        {
            instance.CenterFrequency_Hz = value;
            return instance;
        }


        static public RFDevice WithBandwith_Hz(this RFDevice instance,uint value)
        {
            instance.Bandwith_Hz = value;
            return instance;
        }


        static public RFDevice WithGain_dB(this RFDevice instance,int value)
        {
            instance.Gain_dB = value;
            return instance;
        }


        static public RFDevice WithSignalToNoiseRatio_dB(this RFDevice instance,uint value)
        {
            instance.SignalToNoiseRatio_dB = value;
            return instance;
        }


        static public RFDevice WithXPos(this RFDevice instance,int value)
        {
            instance.XPos = value;
            return instance;
        }


        static public RFDevice WithYPos(this RFDevice instance,int value)
        {
            instance.YPos = value;
            return instance;
        }


        static public RFDevice WithZPos(this RFDevice instance,int value)
        {
            instance.ZPos = value;
            return instance;
        }


        static public RFDevice WithRemark(this RFDevice instance,string value)
        {
            instance.Remark = value;
            return instance;
        }

    } // end public static class  RFDeviceExtensions
}