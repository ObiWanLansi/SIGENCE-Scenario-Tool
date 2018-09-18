
/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */

using System;
using System.Xml.Linq;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Datatypes.Physically;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Interfaces;
using SIGENCEScenarioTool.Models.RxTxTypes;



namespace SIGENCEScenarioTool.Models
{
    ///<summary>
    /// Represent A Device Based On A Radio Frequency.
    ///</summary>
    sealed public partial class RFDevice : AbstractModelBase, IEquatable<RFDevice>, ICloneable, IXmlExport
    {

        #region Instance Properties

        #region PrimaryKey

        ///<summary>
        /// The PropertyName As ReadOnly String For PrimaryKey.
        ///</summary>
        public const String PRIMARYKEY = "PrimaryKey";

        ///<summary>
        /// The DefaultValue For PrimaryKey.
        ///</summary>
        static public readonly Guid DEFAULT_PRIMARYKEY = Guid.NewGuid();

        ///<summary>
        /// The Internal Field For PrimaryKey.
        ///</summary>
        private Guid _PrimaryKey = Guid.NewGuid();

        ///<summary>
        /// The Unique PrimarKey For This RF Device.
        ///</summary>
        public Guid PrimaryKey
        {
            get { return _PrimaryKey; }
            set
            {
                _PrimaryKey = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_PRIMARYKEY = "The Unique PrimarKey For This RF Device.";

        #endregion        

        //---------------------------------------------------------------------

        #region Id

        ///<summary>
        /// The PropertyName As ReadOnly String For Id.
        ///</summary>
        public const String ID = "Id";

        ///<summary>
        /// The DefaultValue For Id.
        ///</summary>
        static public readonly int DEFAULT_ID = 0;

        ///<summary>
        /// The Internal Field For Id.
        ///</summary>
        private int _Id = 0;

        ///<summary>
        /// Every Scenario Element (I.E. Transmitter, Receiver) Must Be Assigned An Unique Id. Negative Id’S Are Reserved For Receivers While All Other Id’S Are Transmitters By Default. Some Applications (I.E. Tdoa Emitter Localization) Require A Reference Transmitter. For These Applications Id=0 Is The Reference Transmitter. Receivers Must Be Assigned First In The Table, Followed Be Transmitters (With Id=0 Being The First). After The Static Scenario, Update Of Id’S Requires No Specific Order. Note That Definition Of New Transmitters/Receivers After The Static Scenario Is Prohibited.
        ///</summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_ID = "Every Scenario Element (I.E. Transmitter, Receiver) Must Be Assigned An Unique Id. Negative Id’S Are Reserved For Receivers While All Other Id’S Are Transmitters By Default. Some Applications (I.E. Tdoa Emitter Localization) Require A Reference Transmitter. For These Applications Id=0 Is The Reference Transmitter. Receivers Must Be Assigned First In The Table, Followed Be Transmitters (With Id=0 Being The First). After The Static Scenario, Update Of Id’S Requires No Specific Order. Note That Definition Of New Transmitters/Receivers After The Static Scenario Is Prohibited.";

        #endregion        

        //---------------------------------------------------------------------

        #region DeviceSource

        ///<summary>
        /// The PropertyName As ReadOnly String For DeviceSource.
        ///</summary>
        public const String DEVICESOURCE = "DeviceSource";

        ///<summary>
        /// The DefaultValue For DeviceSource.
        ///</summary>
        static public readonly DeviceSource DEFAULT_DEVICESOURCE = DeviceSource.Unknown;

        ///<summary>
        /// The Internal Field For DeviceSource.
        ///</summary>
        private DeviceSource _DeviceSource = DeviceSource.Unknown;

        ///<summary>
        /// The Source Of This RF Device.
        ///</summary>
        public DeviceSource DeviceSource
        {
            get { return _DeviceSource; }
            set
            {
                _DeviceSource = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_DEVICESOURCE = "The Source Of This RF Device.";

        #endregion        

        //---------------------------------------------------------------------

        #region StartTime

        ///<summary>
        /// The PropertyName As ReadOnly String For StartTime.
        ///</summary>
        public const String STARTTIME = "StartTime";

        ///<summary>
        /// The DefaultValue For StartTime.
        ///</summary>
        static public readonly double DEFAULT_STARTTIME = 0;

        ///<summary>
        /// The Internal Field For StartTime.
        ///</summary>
        private double _StartTime = 0;

        ///<summary>
        /// This Is The Simulation Time At Which The Parameters (Following The Time Parameter In The Same Line) Are Set. All Transmitters And Receivers Used In The Simulation Must Be Set At Start Of The Simulation, I.E. At Time=0. For Static Scenarios, Where Positions Or Characteristics Settings Never Change Throughout The Simulation, The Time Column Only Contains Zero’s.
        ///</summary>
        public double StartTime
        {
            get { return _StartTime; }
            set
            {
                _StartTime = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_STARTTIME = "This Is The Simulation Time At Which The Parameters (Following The Time Parameter In The Same Line) Are Set. All Transmitters And Receivers Used In The Simulation Must Be Set At Start Of The Simulation, I.E. At Time=0. For Static Scenarios, Where Positions Or Characteristics Settings Never Change Throughout The Simulation, The Time Column Only Contains Zero’s.";

        #endregion        

        //---------------------------------------------------------------------

        #region Name

        ///<summary>
        /// The PropertyName As ReadOnly String For Name.
        ///</summary>
        public const String NAME = "Name";

        ///<summary>
        /// The DefaultValue For Name.
        ///</summary>
        static public readonly string DEFAULT_NAME = "RFDevice";

        ///<summary>
        /// The Internal Field For Name.
        ///</summary>
        private string _Name = "RFDevice";

        ///<summary>
        /// A Short Describing Display Name For The RF Device.
        ///</summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_NAME = "A Short Describing Display Name For The RF Device.";

        #endregion        

        //---------------------------------------------------------------------

        #region Latitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Latitude.
        ///</summary>
        public const String LATITUDE = "Latitude";

        ///<summary>
        /// The DefaultValue For Latitude.
        ///</summary>
        static public readonly Latitude DEFAULT_LATITUDE = double.NaN;

        ///<summary>
        /// The Internal Field For Latitude.
        ///</summary>
        private Latitude _Latitude = double.NaN;

        ///<summary>
        /// The Latitude Of The RF Device (WGS84).
        ///</summary>
        public Latitude Latitude
        {
            get { return _Latitude; }
            set
            {
                _Latitude = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_LATITUDE = "The Latitude Of The RF Device (WGS84).";

        #endregion        

        //---------------------------------------------------------------------

        #region Longitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Longitude.
        ///</summary>
        public const String LONGITUDE = "Longitude";

        ///<summary>
        /// The DefaultValue For Longitude.
        ///</summary>
        static public readonly Longitude DEFAULT_LONGITUDE = double.NaN;

        ///<summary>
        /// The Internal Field For Longitude.
        ///</summary>
        private Longitude _Longitude = double.NaN;

        ///<summary>
        /// The Longitude Of The RF Device (WGS84).
        ///</summary>
        public Longitude Longitude
        {
            get { return _Longitude; }
            set
            {
                _Longitude = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_LONGITUDE = "The Longitude Of The RF Device (WGS84).";

        #endregion        

        //---------------------------------------------------------------------

        #region Altitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Altitude.
        ///</summary>
        public const String ALTITUDE = "Altitude";

        ///<summary>
        /// The DefaultValue For Altitude.
        ///</summary>
        static public readonly Altitude DEFAULT_ALTITUDE = 0;

        ///<summary>
        /// The Internal Field For Altitude.
        ///</summary>
        private Altitude _Altitude = 0;

        ///<summary>
        /// The Elevation Of The RF Device Above The Sea Level (Meter).
        ///</summary>
        public Altitude Altitude
        {
            get { return _Altitude; }
            set
            {
                _Altitude = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_ALTITUDE = "The Elevation Of The RF Device Above The Sea Level (Meter).";

        #endregion        

        //---------------------------------------------------------------------

        #region Roll

        ///<summary>
        /// The PropertyName As ReadOnly String For Roll.
        ///</summary>
        public const String ROLL = "Roll";

        ///<summary>
        /// The DefaultValue For Roll.
        ///</summary>
        static public readonly double DEFAULT_ROLL = 0;

        ///<summary>
        /// The Internal Field For Roll.
        ///</summary>
        private double _Roll = 0;

        ///<summary>
        /// These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
        ///</summary>
        public double Roll
        {
            get { return _Roll; }
            set
            {
                _Roll = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_ROLL = "These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.";

        #endregion        

        //---------------------------------------------------------------------

        #region Pitch

        ///<summary>
        /// The PropertyName As ReadOnly String For Pitch.
        ///</summary>
        public const String PITCH = "Pitch";

        ///<summary>
        /// The DefaultValue For Pitch.
        ///</summary>
        static public readonly double DEFAULT_PITCH = 0;

        ///<summary>
        /// The Internal Field For Pitch.
        ///</summary>
        private double _Pitch = 0;

        ///<summary>
        /// These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
        ///</summary>
        public double Pitch
        {
            get { return _Pitch; }
            set
            {
                _Pitch = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_PITCH = "These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.";

        #endregion        

        //---------------------------------------------------------------------

        #region Yaw

        ///<summary>
        /// The PropertyName As ReadOnly String For Yaw.
        ///</summary>
        public const String YAW = "Yaw";

        ///<summary>
        /// The DefaultValue For Yaw.
        ///</summary>
        static public readonly double DEFAULT_YAW = 0;

        ///<summary>
        /// The Internal Field For Yaw.
        ///</summary>
        private double _Yaw = 0;

        ///<summary>
        /// These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
        ///</summary>
        public double Yaw
        {
            get { return _Yaw; }
            set
            {
                _Yaw = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_YAW = "These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.";

        #endregion        

        //---------------------------------------------------------------------

        #region RxTxType

        ///<summary>
        /// The PropertyName As ReadOnly String For RxTxType.
        ///</summary>
        public const String RXTXTYPE = "RxTxType";

        ///<summary>
        /// The DefaultValue For RxTxType.
        ///</summary>
        static public readonly RxTxType DEFAULT_RXTXTYPE = RxTxTypes.RxTxTypes.Unknown;

        ///<summary>
        /// The Internal Field For RxTxType.
        ///</summary>
        private RxTxType _RxTxType = RxTxTypes.RxTxTypes.Unknown;

        ///<summary>
        /// For All Receivers (i.e. ID’s &lt; 0) This Parameter Defines The Radio Being Used.
        ///</summary>
        public RxTxType RxTxType
        {
            get { return _RxTxType; }
            set
            {
                if( value == null )
                {
                    throw new ArgumentException();
                }
                _RxTxType = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_RXTXTYPE = "For All Receivers (i.e. ID’s &lt; 0) This Parameter Defines The Radio Being Used.";

        #endregion        

        //---------------------------------------------------------------------

        #region AntennaType

        ///<summary>
        /// The PropertyName As ReadOnly String For AntennaType.
        ///</summary>
        public const String ANTENNATYPE = "AntennaType";

        ///<summary>
        /// The DefaultValue For AntennaType.
        ///</summary>
        static public readonly AntennaType DEFAULT_ANTENNATYPE = AntennaType.Unknown;

        ///<summary>
        /// The Internal Field For AntennaType.
        ///</summary>
        private AntennaType _AntennaType = AntennaType.Unknown;

        ///<summary>
        /// AntennaType Defines The Antenna Type Used For Transmitter And Receiver Respectively. Note: Currently, Only Omnidirectional Antenna Type Is Available / Supported.
        ///</summary>
        public AntennaType AntennaType
        {
            get { return _AntennaType; }
            set
            {
                _AntennaType = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_ANTENNATYPE = "AntennaType Defines The Antenna Type Used For Transmitter And Receiver Respectively. Note: Currently, Only Omnidirectional Antenna Type Is Available / Supported.";

        #endregion        

        //---------------------------------------------------------------------

        #region CenterFrequency_Hz

        ///<summary>
        /// The PropertyName As ReadOnly String For CenterFrequency_Hz.
        ///</summary>
        public const String CENTERFREQUENCY_HZ = "CenterFrequency_Hz";

        ///<summary>
        /// The DefaultValue For CenterFrequency_Hz.
        ///</summary>
        static public readonly Frequency DEFAULT_CENTERFREQUENCY_HZ = 0;

        ///<summary>
        /// The Internal Field For CenterFrequency_Hz.
        ///</summary>
        private Frequency _CenterFrequency_Hz = 0;

        ///<summary>
        /// For Transmitters (I.E. Id’s &gt;= 0) This Parameter Defines Transmitter Signal Center Frequency [Hz]. For Receivers (I.E. Id’s &lt; 0) This Parameter Is Currently Unused.
        ///</summary>
        public Frequency CenterFrequency_Hz
        {
            get { return _CenterFrequency_Hz; }
            set
            {
                _CenterFrequency_Hz = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_CENTERFREQUENCY_HZ = "For Transmitters (I.E. Id’s &gt;= 0) This Parameter Defines Transmitter Signal Center Frequency [Hz]. For Receivers (I.E. Id’s &lt; 0) This Parameter Is Currently Unused.";

        #endregion        

        //---------------------------------------------------------------------

        #region Bandwidth_Hz

        ///<summary>
        /// The PropertyName As ReadOnly String For Bandwidth_Hz.
        ///</summary>
        public const String BANDWIDTH_HZ = "Bandwidth_Hz";

        ///<summary>
        /// The DefaultValue For Bandwidth_Hz.
        ///</summary>
        static public readonly Bandwidth DEFAULT_BANDWIDTH_HZ = 0;

        ///<summary>
        /// The Internal Field For Bandwidth_Hz.
        ///</summary>
        private Bandwidth _Bandwidth_Hz = 0;

        ///<summary>
        /// The Bandwith Of The Transmitter.
        ///</summary>
        public Bandwidth Bandwidth_Hz
        {
            get { return _Bandwidth_Hz; }
            set
            {
                _Bandwidth_Hz = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_BANDWIDTH_HZ = "The Bandwith Of The Transmitter.";

        #endregion        

        //---------------------------------------------------------------------

        #region Gain_dB

        ///<summary>
        /// The PropertyName As ReadOnly String For Gain_dB.
        ///</summary>
        public const String GAIN_DB = "Gain_dB";

        ///<summary>
        /// The DefaultValue For Gain_dB.
        ///</summary>
        static public readonly Gain DEFAULT_GAIN_DB = 0;

        ///<summary>
        /// The Internal Field For Gain_dB.
        ///</summary>
        private Gain _Gain_dB = 0;

        ///<summary>
        /// For Transmitters (I.E. Id’s &gt;= 0) This Parameter Defines Transmitter Signal Power [Dbm]. For Receivers (I.E. Id’s &lt; 0) This Parameter Is Currently Unused.
        ///</summary>
        public Gain Gain_dB
        {
            get { return _Gain_dB; }
            set
            {
                _Gain_dB = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_GAIN_DB = "For Transmitters (I.E. Id’s &gt;= 0) This Parameter Defines Transmitter Signal Power [Dbm]. For Receivers (I.E. Id’s &lt; 0) This Parameter Is Currently Unused.";

        #endregion        

        //---------------------------------------------------------------------

        #region SignalToNoiseRatio_dB

        ///<summary>
        /// The PropertyName As ReadOnly String For SignalToNoiseRatio_dB.
        ///</summary>
        public const String SIGNALTONOISERATIO_DB = "SignalToNoiseRatio_dB";

        ///<summary>
        /// The DefaultValue For SignalToNoiseRatio_dB.
        ///</summary>
        static public readonly SignalToNoiseRatio DEFAULT_SIGNALTONOISERATIO_DB = 0;

        ///<summary>
        /// The Internal Field For SignalToNoiseRatio_dB.
        ///</summary>
        private SignalToNoiseRatio _SignalToNoiseRatio_dB = 0;

        ///<summary>
        /// For Receivers (I.E. Id’s &lt; 0) This Parameter Is Imposes Gaussian White Noise To The Respective Receiver Signal. For Transmitters (I.E. Id’s &gt;= 0) This Parameter Is Unused.
        ///</summary>
        public SignalToNoiseRatio SignalToNoiseRatio_dB
        {
            get { return _SignalToNoiseRatio_dB; }
            set
            {
                _SignalToNoiseRatio_dB = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_SIGNALTONOISERATIO_DB = "For Receivers (I.E. Id’s &lt; 0) This Parameter Is Imposes Gaussian White Noise To The Respective Receiver Signal. For Transmitters (I.E. Id’s &gt;= 0) This Parameter Is Unused.";

        #endregion        

        //---------------------------------------------------------------------

        #region XPos

        ///<summary>
        /// The PropertyName As ReadOnly String For XPos.
        ///</summary>
        public const String XPOS = "XPos";

        ///<summary>
        /// The DefaultValue For XPos.
        ///</summary>
        static public readonly int DEFAULT_XPOS = 0;

        ///<summary>
        /// The Internal Field For XPos.
        ///</summary>
        private int _XPos = 0;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int XPos
        {
            get { return _XPos; }
            set
            {
                _XPos = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_XPOS = "XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.";

        #endregion        

        //---------------------------------------------------------------------

        #region YPos

        ///<summary>
        /// The PropertyName As ReadOnly String For YPos.
        ///</summary>
        public const String YPOS = "YPos";

        ///<summary>
        /// The DefaultValue For YPos.
        ///</summary>
        static public readonly int DEFAULT_YPOS = 0;

        ///<summary>
        /// The Internal Field For YPos.
        ///</summary>
        private int _YPos = 0;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int YPos
        {
            get { return _YPos; }
            set
            {
                _YPos = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_YPOS = "XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.";

        #endregion        

        //---------------------------------------------------------------------

        #region ZPos

        ///<summary>
        /// The PropertyName As ReadOnly String For ZPos.
        ///</summary>
        public const String ZPOS = "ZPos";

        ///<summary>
        /// The DefaultValue For ZPos.
        ///</summary>
        static public readonly int DEFAULT_ZPOS = 0;

        ///<summary>
        /// The Internal Field For ZPos.
        ///</summary>
        private int _ZPos = 0;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int ZPos
        {
            get { return _ZPos; }
            set
            {
                _ZPos = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_ZPOS = "XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.";

        #endregion        

        //---------------------------------------------------------------------

        #region Remark

        ///<summary>
        /// The PropertyName As ReadOnly String For Remark.
        ///</summary>
        public const String REMARK = "Remark";

        ///<summary>
        /// The DefaultValue For Remark.
        ///</summary>
        static public readonly string DEFAULT_REMARK = "";

        ///<summary>
        /// The Internal Field For Remark.
        ///</summary>
        private string _Remark = "";

        ///<summary>
        /// A Comment Or Remark For The RF Device.
        ///</summary>
        public string Remark
        {
            get { return _Remark; }
            set
            {
                _Remark = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_REMARK = "A Comment Or Remark For The RF Device.";

        #endregion        

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the XML.
        /// </summary>
        /// <returns></returns>
        public XElement ToXml()
        {
            return new XElement( "RFDevice" ,

                XElementExtension.GetXElement( "PrimaryKey" , PrimaryKey ) ,
                XElementExtension.GetXElement( "Id" , Id ) ,
                XElementExtension.GetXElement( "DeviceSource" , DeviceSource ) ,
                XElementExtension.GetXElement( "StartTime" , StartTime ) ,
                XElementExtension.GetXElement( "Name" , Name ) ,
                XElementExtension.GetXElement( "Latitude" , Latitude ) ,
                XElementExtension.GetXElement( "Longitude" , Longitude ) ,
                XElementExtension.GetXElement( "Altitude" , Altitude ) ,
                XElementExtension.GetXElement( "Roll" , Roll ) ,
                XElementExtension.GetXElement( "Pitch" , Pitch ) ,
                XElementExtension.GetXElement( "Yaw" , Yaw ) ,
                XElementExtension.GetXElement( "RxTxType" , RxTxType ) ,
                XElementExtension.GetXElement( "AntennaType" , AntennaType ) ,
                XElementExtension.GetXElement( "CenterFrequency_Hz" , CenterFrequency_Hz ) ,
                XElementExtension.GetXElement( "Bandwidth_Hz" , Bandwidth_Hz ) ,
                XElementExtension.GetXElement( "Gain_dB" , Gain_dB ) ,
                XElementExtension.GetXElement( "SignalToNoiseRatio_dB" , SignalToNoiseRatio_dB ) ,
                XElementExtension.GetXElement( "XPos" , XPos ) ,
                XElementExtension.GetXElement( "YPos" , YPos ) ,
                XElementExtension.GetXElement( "ZPos" , ZPos ) ,
                XElementExtension.GetXElement( "Remark" , Remark )
            );
        }


        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="eRoot">The e root.</param>
        /// <returns></returns>
        static public RFDevice FromXml( XElement eRoot )
        {
            XElement eChild = null;

            if( eRoot.Name.LocalName.Equals( "RFDevice" ) )
            {
                eChild = eRoot;
            }
            else
            {
                eChild = eRoot.Element( "RFDevice" );
            }

            return new RFDevice
            {
                PrimaryKey = eChild.GetProperty<Guid>( "PrimaryKey" , Guid.NewGuid() ) ,
                Id = eChild.GetProperty<int>( "Id" , 0 ) ,
                DeviceSource = eChild.GetProperty<DeviceSource>( "DeviceSource" , DeviceSource.Unknown ) ,
                StartTime = eChild.GetProperty<double>( "StartTime" , 0 ) ,
                Name = eChild.GetProperty<string>( "Name" , "RFDevice" ) ,
                Latitude = eChild.GetProperty<Latitude>( "Latitude" , double.NaN ) ,
                Longitude = eChild.GetProperty<Longitude>( "Longitude" , double.NaN ) ,
                Altitude = eChild.GetProperty<Altitude>( "Altitude" , 0 ) ,
                Roll = eChild.GetProperty<double>( "Roll" , 0 ) ,
                Pitch = eChild.GetProperty<double>( "Pitch" , 0 ) ,
                Yaw = eChild.GetProperty<double>( "Yaw" , 0 ) ,
                RxTxType = eChild.GetProperty<RxTxType>( "RxTxType" , RxTxTypes.RxTxTypes.Unknown ) ,
                AntennaType = eChild.GetProperty<AntennaType>( "AntennaType" , AntennaType.Unknown ) ,
                CenterFrequency_Hz = eChild.GetProperty<Frequency>( "CenterFrequency_Hz" , 0 ) ,
                Bandwidth_Hz = eChild.GetProperty<Bandwidth>( "Bandwidth_Hz" , 0 ) ,
                Gain_dB = eChild.GetProperty<Gain>( "Gain_dB" , 0 ) ,
                SignalToNoiseRatio_dB = eChild.GetProperty<SignalToNoiseRatio>( "SignalToNoiseRatio_dB" , 0 ) ,
                XPos = eChild.GetProperty<int>( "XPos" , 0 ) ,
                YPos = eChild.GetProperty<int>( "YPos" , 0 ) ,
                ZPos = eChild.GetProperty<int>( "ZPos" , 0 ) ,
                Remark = eChild.GetProperty<string>( "Remark" , "" )
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gibt an, ob das aktuelle Objekt gleich einem anderen Objekt des gleichen Typs ist.
        /// </summary>
        /// <param name="other">Ein Objekt, das mit diesem Objekt verglichen werden soll.</param>
        /// <returns>
        /// true, wenn das aktuelle Objekt gleich dem <paramref name="other" />-Parameter ist, andernfalls false.
        /// </returns>
        public bool Equals( RFDevice other )
        {
            if( other == null )
            {
                return false;
            }

            if( PrimaryKey != other.PrimaryKey )
            {
                return false;
            }

            if( Id != other.Id )
            {
                return false;
            }

            if( DeviceSource != other.DeviceSource )
            {
                return false;
            }

            if( StartTime != other.StartTime )
            {
                return false;
            }

            if( Name != other.Name )
            {
                return false;
            }

            if( Latitude != other.Latitude )
            {
                return false;
            }

            if( Longitude != other.Longitude )
            {
                return false;
            }

            if( Altitude != other.Altitude )
            {
                return false;
            }

            if( Roll != other.Roll )
            {
                return false;
            }

            if( Pitch != other.Pitch )
            {
                return false;
            }

            if( Yaw != other.Yaw )
            {
                return false;
            }

            if( RxTxType != other.RxTxType )
            {
                return false;
            }

            if( AntennaType != other.AntennaType )
            {
                return false;
            }

            if( CenterFrequency_Hz != other.CenterFrequency_Hz )
            {
                return false;
            }

            if( Bandwidth_Hz != other.Bandwidth_Hz )
            {
                return false;
            }

            if( Gain_dB != other.Gain_dB )
            {
                return false;
            }

            if( SignalToNoiseRatio_dB != other.SignalToNoiseRatio_dB )
            {
                return false;
            }

            if( XPos != other.XPos )
            {
                return false;
            }

            if( YPos != other.YPos )
            {
                return false;
            }

            if( ZPos != other.ZPos )
            {
                return false;
            }

            if( Remark != other.Remark )
            {
                return false;
            }

            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public RFDevice Clone()
        {
            return ( RFDevice ) this.MemberwiseClone();
        }


        /// <summary>
        /// Erstellt ein neues Objekt, das eine Kopie der aktuellen Instanz darstellt.
        /// </summary>
        /// <returns>
        /// Ein neues Objekt, das eine Kopie dieser Instanz ist.
        /// </returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

    } // end sealed public class RFDevice



    /// <summary>
    /// The tooltips for our properties to display in the HMI.
    /// </summary>
    sealed public class RFDeviceTooltips
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="RFDeviceTooltips"/> class from being created.
        /// </summary>
        //private RFDeviceTooltips()
        //{
        //}

        /// <summary>
        /// The instance
        /// </summary>
        //static private RFDeviceTooltips _instance = new RFDeviceTooltips();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        //static public RFDeviceTooltips GetInstance()
        //{
        //    return _instance;
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The tooltip for the PrimaryKey.
        /// </summary>
        public string TOOLTIP_PRIMARYKEY { get { return "The Unique PrimarKey For This RF Device."; } }

        /// <summary>
        /// The tooltip for the Id.
        /// </summary>
        public string TOOLTIP_ID { get { return "Every Scenario Element (I.E. Transmitter,\nReceiver) Must Be Assigned An Unique Id.\nNegative Id’S Are Reserved For Receivers\nWhile All Other Id’S Are Transmitters By\nDefault. Some Applications (I.E. Tdoa Emitter\nLocalization) Require A Reference Transmitter.\nFor These Applications Id=0 Is The Reference\nTransmitter. Receivers Must Be Assigned First\nIn The Table, Followed Be Transmitters (With\nId=0 Being The First). After The Static Scenario,\nUpdate Of Id’S Requires No Specific Order.\nNote That Definition Of New Transmitters/Receivers\nAfter The Static Scenario Is Prohibited."; } }

        /// <summary>
        /// The tooltip for the DeviceSource.
        /// </summary>
        public string TOOLTIP_DEVICESOURCE { get { return "The Source Of This RF Device."; } }

        /// <summary>
        /// The tooltip for the StartTime.
        /// </summary>
        public string TOOLTIP_STARTTIME { get { return "This Is The Simulation Time At Which The Parameters\n(Following The Time Parameter In The Same\nLine) Are Set. All Transmitters And Receivers\nUsed In The Simulation Must Be Set At Start\nOf The Simulation, I.E. At Time=0. For Static\nScenarios, Where Positions Or Characteristics\nSettings Never Change Throughout The Simulation,\nThe Time Column Only Contains Zero’s."; } }

        /// <summary>
        /// The tooltip for the Name.
        /// </summary>
        public string TOOLTIP_NAME { get { return "A Short Describing Display Name For The RF\nDevice."; } }

        /// <summary>
        /// The tooltip for the Latitude.
        /// </summary>
        public string TOOLTIP_LATITUDE { get { return "The Latitude Of The RF Device (WGS84)."; } }

        /// <summary>
        /// The tooltip for the Longitude.
        /// </summary>
        public string TOOLTIP_LONGITUDE { get { return "The Longitude Of The RF Device (WGS84)."; } }

        /// <summary>
        /// The tooltip for the Altitude.
        /// </summary>
        public string TOOLTIP_ALTITUDE { get { return "The Elevation Of The RF Device Above The Sea\nLevel (Meter)."; } }

        /// <summary>
        /// The tooltip for the Roll.
        /// </summary>
        public string TOOLTIP_ROLL { get { return "These Parameters Set The Orientation Of Transmitter\n/ Receiver Antennas. The Respective Antenna\nType Is Defined By Antennatype. The Rf Simulation\nUses The Antenna Orientation To Compute The\nResulting Signal Power At The Receivers."; } }

        /// <summary>
        /// The tooltip for the Pitch.
        /// </summary>
        public string TOOLTIP_PITCH { get { return "These Parameters Set The Orientation Of Transmitter\n/ Receiver Antennas. The Respective Antenna\nType Is Defined By Antennatype. The Rf Simulation\nUses The Antenna Orientation To Compute The\nResulting Signal Power At The Receivers."; } }

        /// <summary>
        /// The tooltip for the Yaw.
        /// </summary>
        public string TOOLTIP_YAW { get { return "These Parameters Set The Orientation Of Transmitter\n/ Receiver Antennas. The Respective Antenna\nType Is Defined By Antennatype. The Rf Simulation\nUses The Antenna Orientation To Compute The\nResulting Signal Power At The Receivers."; } }

        /// <summary>
        /// The tooltip for the RxTxType.
        /// </summary>
        public string TOOLTIP_RXTXTYPE { get { return "For All Receivers (i.e. ID’s < 0) This Parameter\nDefines The Radio Being Used."; } }

        /// <summary>
        /// The tooltip for the AntennaType.
        /// </summary>
        public string TOOLTIP_ANTENNATYPE { get { return "AntennaType Defines The Antenna Type Used\nFor Transmitter And Receiver Respectively.\nNote: Currently, Only Omnidirectional Antenna\nType Is Available / Supported."; } }

        /// <summary>
        /// The tooltip for the CenterFrequency_Hz.
        /// </summary>
        public string TOOLTIP_CENTERFREQUENCY_HZ { get { return "For Transmitters (I.E. Id’s >= 0) This Parameter\nDefines Transmitter Signal Center Frequency\n[Hz]. For Receivers (I.E. Id’s < 0) This\nParameter Is Currently Unused."; } }

        /// <summary>
        /// The tooltip for the Bandwidth_Hz.
        /// </summary>
        public string TOOLTIP_BANDWIDTH_HZ { get { return "The Bandwith Of The Transmitter."; } }

        /// <summary>
        /// The tooltip for the Gain_dB.
        /// </summary>
        public string TOOLTIP_GAIN_DB { get { return "For Transmitters (I.E. Id’s >= 0) This Parameter\nDefines Transmitter Signal Power [Dbm]. For\nReceivers (I.E. Id’s < 0) This Parameter\nIs Currently Unused."; } }

        /// <summary>
        /// The tooltip for the SignalToNoiseRatio_dB.
        /// </summary>
        public string TOOLTIP_SIGNALTONOISERATIO_DB { get { return "For Receivers (I.E. Id’s < 0) This Parameter\nIs Imposes Gaussian White Noise To The Respective\nReceiver Signal. For Transmitters (I.E. Id’s\n>= 0) This Parameter Is Unused."; } }

        /// <summary>
        /// The tooltip for the XPos.
        /// </summary>
        public string TOOLTIP_XPOS { get { return "XPos,YPos,ZPos Define The Transmitter / Receiver\nPositions In A Local Coordinate System With\nThe Transmitter (ID=0) Being The Center Position."; } }

        /// <summary>
        /// The tooltip for the YPos.
        /// </summary>
        public string TOOLTIP_YPOS { get { return "XPos,YPos,ZPos Define The Transmitter / Receiver\nPositions In A Local Coordinate System With\nThe Transmitter (ID=0) Being The Center Position."; } }

        /// <summary>
        /// The tooltip for the ZPos.
        /// </summary>
        public string TOOLTIP_ZPOS { get { return "XPos,YPos,ZPos Define The Transmitter / Receiver\nPositions In A Local Coordinate System With\nThe Transmitter (ID=0) Being The Center Position."; } }

        /// <summary>
        /// The tooltip for the Remark.
        /// </summary>
        public string TOOLTIP_REMARK { get { return "A Comment Or Remark For The RF Device."; } }

    } // end sealed public class RFDeviceTooltips
}