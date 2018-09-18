using System;
using System.Collections.Generic;
using System.Reflection;



namespace SIGENCEScenarioTool.Models.RxTxTypes
{
    /// <summary>
    /// A class with all known RxTxTypes as static Property.
    /// </summary>
    static public class RxTxTypes
    {

        /// <summary>
        /// Unknown RxTxType.
        /// </summary>
        static public RxTxType Unknown { get; private set; } = new RxTxType(-1,"Unknown","Unknown RxTxType");

        /// <summary>
        /// Ideal Sdr Receiver (Passes Signal Through).
        /// </summary>
        static public RxTxType IdealSDR { get; private set; } = new RxTxType(0,"IdealSDR","Ideal Sdr Receiver (Passes Signal Through)");

        /// <summary>
        /// Reserved For RF Simulation Playback Functionality.
        /// </summary>
        static public RxTxType Reserved { get; private set; } = new RxTxType(1,"Reserved","Reserved For RF Simulation Playback Functionality");

        /// <summary>
        /// HackRF One.
        /// </summary>
        static public RxTxType HackRF { get; private set; } = new RxTxType(2,"HackRF","HackRF One");

        /// <summary>
        /// Ettus B200mini.
        /// </summary>
        static public RxTxType B200mini { get; private set; } = new RxTxType(3,"B200mini","Ettus B200mini");

        /// <summary>
        /// Ettus X310 / TwinRx.
        /// </summary>
        static public RxTxType TwinRx { get; private set; } = new RxTxType(4,"TwinRx","Ettus X310 / TwinRx");

        /// <summary>
        /// QPSK Signal With 2kHz Bandwidth.
        /// </summary>
        static public RxTxType QPSK { get; private set; } = new RxTxType(1,"QPSK","QPSK Signal With 2kHz Bandwidth");

        /// <summary>
        /// This Is A Sine Generator A 500Hz Frequency.
        /// </summary>
        static public RxTxType SIN { get; private set; } = new RxTxType(2,"SIN","This Is A Sine Generator A 500Hz Frequency");

        /// <summary>
        /// This Is A Fm Broadcast Radio Transmitter (Awgn Noise Signal) With Input 20Khz Signal And 50Khz Bandwidth.
        /// </summary>
        static public RxTxType FMBroadcast { get; private set; } = new RxTxType(3,"FMBroadcast","This Is A Fm Broadcast Radio Transmitter (Awgn Noise Signal) With Input 20Khz Signal And 50Khz Bandwidth");

        /// <summary>
        /// 10MHz L1 GPS Jammer.
        /// </summary>
        static public RxTxType GPSJammer { get; private set; } = new RxTxType(4,"GPSJammer","10MHz L1 GPS Jammer");

        /// <summary>
        /// Iridium Satcom Transmitter.
        /// </summary>
        static public RxTxType Iridium { get; private set; } = new RxTxType(5,"Iridium","Iridium Satcom Transmitter");

        /// <summary>
        /// LTE Signal.
        /// </summary>
        static public RxTxType LTE { get; private set; } = new RxTxType(6,"LTE","LTE Signal");

        /// <summary>
        /// AIS Signal.
        /// </summary>
        static public RxTxType AIS { get; private set; } = new RxTxType(7,"AIS","AIS Signal");

        /// <summary>
        /// Narrow Fm Band (Voice With 5Khz Bandwidth).
        /// </summary>
        static public RxTxType NFMRadio { get; private set; } = new RxTxType(8,"NFMRadio","Narrow Fm Band (Voice With 5Khz Bandwidth)");

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The internal list with all RxTxType's.
        /// </summary>
        static private readonly List<RxTxType> lRxTxTypes = null;


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
        static public IReadOnlyCollection<RxTxType> Values
        {
            get { return lRxTxTypes.AsReadOnly(); }
        }
        

        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public RxTxType FromString( string strName )
        {
            if( string.IsNullOrEmpty( strName ) == false )
            {
                foreach( RxTxType rtt in lRxTxTypes )
                {
                    if( rtt.Name.Equals( strName , StringComparison.CurrentCultureIgnoreCase ) )
                    {
                        return rtt;
                    }
                }
            }

            return RxTxTypes.Unknown;
        }

    } // end static public class RxTxTypes
}
