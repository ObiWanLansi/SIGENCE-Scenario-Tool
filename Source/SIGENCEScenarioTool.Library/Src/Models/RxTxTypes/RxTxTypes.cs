/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */

using System;
using System.Collections.Generic;
using System.Reflection;



namespace SIGENCEScenarioTool.Models.RxTxTypes
{
    /// <summary>
    /// A class with all known RxTxTypes as static Property.
    /// </summary>
    public static class RxTxTypes
    {

        /// <summary>
        /// Unknown RxTxType.
        /// </summary>
        public static RxTxType Unknown { get; } = new RxTxType(4242,"Unknown","Unknown RxTxType");

        /// <summary>
        /// Ideal Sdr Receiver (Passes Signal Through).
        /// </summary>
        public static RxTxType IdealSDR { get; } = new RxTxType(1,"IdealSDR","Ideal Sdr Receiver (Passes Signal Through)");

        /// <summary>
        /// HackRF One.
        /// </summary>
        public static RxTxType HackRF { get; } = new RxTxType(2,"HackRF","HackRF One");

        /// <summary>
        /// Ettus B200mini.
        /// </summary>
        public static RxTxType B200mini { get; } = new RxTxType(3,"B200mini","Ettus B200mini");

        /// <summary>
        /// Ettus X310 / TwinRx.
        /// </summary>
        public static RxTxType TwinRx { get; } = new RxTxType(4,"TwinRx","Ettus X310 / TwinRx");

        /// <summary>
        /// QPSK Signal With 2kHz Bandwidth.
        /// </summary>
        public static RxTxType QPSK { get; } = new RxTxType(1,"QPSK","QPSK Signal With 2kHz Bandwidth");

        /// <summary>
        /// This Is A Sine Generator A 500Hz Frequency.
        /// </summary>
        public static RxTxType SIN { get; } = new RxTxType(2,"SIN","This Is A Sine Generator A 500Hz Frequency");

        /// <summary>
        /// This Is A Fm Broadcast Radio Transmitter (Awgn Noise Signal) With Input 20Khz Signal And 50Khz Bandwidth.
        /// </summary>
        public static RxTxType FMBroadcast { get; } = new RxTxType(3,"FMBroadcast","This Is A Fm Broadcast Radio Transmitter (Awgn Noise Signal) With Input 20Khz Signal And 50Khz Bandwidth");

        /// <summary>
        /// 10MHz L1 GPS Jammer.
        /// </summary>
        public static RxTxType GPSJammer { get; } = new RxTxType(4,"GPSJammer","10MHz L1 GPS Jammer");

        /// <summary>
        /// Iridium Satcom Transmitter.
        /// </summary>
        public static RxTxType Iridium { get; } = new RxTxType(5,"Iridium","Iridium Satcom Transmitter");

        /// <summary>
        /// LTE Signal.
        /// </summary>
        public static RxTxType LTE { get; } = new RxTxType(6,"LTE","LTE Signal");

        /// <summary>
        /// AIS Signal.
        /// </summary>
        public static RxTxType AIS { get; } = new RxTxType(7,"AIS","AIS Signal");

        /// <summary>
        /// Narrow Fm Band (Voice With 5Khz Bandwidth).
        /// </summary>
        public static RxTxType NFMRadio { get; } = new RxTxType(8,"NFMRadio","Narrow Fm Band (Voice With 5Khz Bandwidth)");

        /// <summary>
        /// 200Khz GSM Signal With Random Data.
        /// </summary>
        public static RxTxType GSM { get; } = new RxTxType(9,"GSM","200Khz GSM Signal With Random Data");

        /// <summary>
        /// SIMRAD's Argus S-Band Radar.
        /// </summary>
        public static RxTxType SBandRadar { get; } = new RxTxType(10,"SBandRadar","SIMRAD's Argus S-Band Radar");

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The internal list with all RxTxType's.
        /// </summary>
        private static readonly List<RxTxType> lRxTxTypes = null;


        /// <summary>
        /// Initializes the <see cref="RxTxTypes" /> class.
        /// </summary>
        static RxTxTypes()
        {
            Type tRxTxType = typeof( RxTxType );

            lRxTxTypes = new List<RxTxType>();

            foreach( PropertyInfo pi in typeof( RxTxTypes ).GetProperties( BindingFlags.Public | BindingFlags.Static ) )
            {
                if( pi.PropertyType == tRxTxType )
                {
                    lRxTxTypes.Add( pi.GetValue( null ) as RxTxType );
                }
            }
        }


        /// <summary>
        /// Gets the list with all RxTxType's.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public static IReadOnlyCollection<RxTxType> Values
        {
            get { return lRxTxTypes.AsReadOnly(); }
        }
        
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        /// <summary>
        /// Determines whether [is valid for identifier] [the specified i rf device identifier].
        /// </summary>
        /// <param name="iRFDeviceId">The i rf device identifier.</param>
        /// <param name="rttValue">The RTT value.</param>
        /// <returns>
        ///   <c>true</c> if [is valid for identifier] [the specified i rf device identifier]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidForId( int iRFDeviceId , RxTxType rttValue )
        {
            // Transmitters ( Id >= 0 )
            if( iRFDeviceId >= 0 )
            {
                switch( rttValue.Name )
                {
                    case "QPSK":
                    case "SIN":
                    case "FMBroadcast":
                    case "GPSJammer":
                    case "Iridium":
                    case "LTE":
                    case "AIS":
                    case "NFMRadio":
                    case "GSM":
                    case "SBandRadar":
                        return true;
                }

                return false;
            }

            // Receivers ( Id < 0 )
            switch( rttValue.Name )
            {
                case "IdealSDR":
                case "HackRF":
                case "B200mini":
                case "TwinRx":
                    return true;
            }

            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        public static RxTxType FromString( string strName )
        {
            if( string.IsNullOrEmpty( strName ) )
            {
                return Unknown;
            }

            foreach( RxTxType rtt in lRxTxTypes )
            {
                if( rtt.Name.Equals( strName , StringComparison.CurrentCultureIgnoreCase ) )
                {
                    return rtt;
                }
            }

            return Unknown;
        }

        
        /// <summary>
        /// Returns the RxTxType from a int value.
        /// </summary>
        /// <remarks>
        /// Because the RxTxType as integer is not unique, it is important to have the rfdeviceid to choose the right RxTxType.
        /// </remarks>
        /// <param name="iRFDeviceId">The rf device identifier.</param>
        /// <param name="iValue">The value.</param>
        /// <returns></returns>
        public static RxTxType FromInt( int iRFDeviceId , int iValue )
        {
            if( iValue < 0 )
            {
                iValue *= -1;
            }

            if( iRFDeviceId >= 0 )
            {
                switch( iValue )
                {
                    case 1:
                        return QPSK;

                    case 2:
                        return SIN;

                    case 3:
                        return FMBroadcast;

                    case 4:
                        return GPSJammer;

                    case 5:
                        return Iridium;

                    case 6:
                        return LTE;

                    case 7:
                        return AIS;

                    case 8:
                        return NFMRadio;

                    case 9:
                        return GSM;

                    case 10:
                        return SBandRadar;

                    default:
                        return Unknown;

                }
            }

            if( iRFDeviceId < 0 )
            {
                switch( iValue )
                {
                    case 1:
                        return IdealSDR;

                    case 2:
                        return HackRF;

                    case 3:
                        return B200mini;

                    case 4:
                        return TwinRx;

                    default:
                        return Unknown;
                }
            }

            return Unknown;
        }

    } // end public static class RxTxTypes
}
