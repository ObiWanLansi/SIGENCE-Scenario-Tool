
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
    public partial class RFDevice : AbstractModelBase, IEquatable<RFDevice>, ICloneable, IXmlExport
    {

        #region Instance Properties

        #region PrimaryKey

        ///<summary>
        /// The PropertyName As ReadOnly String For PrimaryKey.
        ///</summary>
        public const string PRIMARYKEY = "PrimaryKey";

        ///<summary>
        /// The DefaultValue For PrimaryKey.
        ///</summary>
        public static readonly Guid DEFAULT_PRIMARYKEY = Guid.NewGuid();
        
        ///<summary>
        /// The Internal Field For PrimaryKey.
        ///</summary>
        //private Guid _PrimaryKey = Guid.NewGuid();
        private Guid _PrimaryKey = DEFAULT_PRIMARYKEY;

        ///<summary>
        /// The Unique PrimarKey For This RF Device.
        ///</summary>
        public Guid PrimaryKey
        {
            get => this._PrimaryKey;
            set
            {
                if( this._PrimaryKey != value )
                {
                    this._PrimaryKey = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Id

        ///<summary>
        /// The PropertyName As ReadOnly String For Id.
        ///</summary>
        public const string ID = "Id";

        ///<summary>
        /// The DefaultValue For Id.
        ///</summary>
        public static readonly int DEFAULT_ID = 0;
        
        ///<summary>
        /// The Internal Field For Id.
        ///</summary>
        //private int _Id = 0;
        private int _Id = DEFAULT_ID;

        ///<summary>
        /// Every Scenario Element (I.E. Transmitter, Receiver) Must Be Assigned An Unique Id. Negative Id’S Are Reserved For Receivers While All Other Id’S Are Transmitters By Default. Some Applications (I.E. Tdoa Emitter Localization) Require A Reference Transmitter. For These Applications Id=0 Is The Reference Transmitter. Receivers Must Be Assigned First In The Table, Followed Be Transmitters (With Id=0 Being The First). After The Static Scenario, Update Of Id’S Requires No Specific Order. Note That Definition Of New Transmitters/Receivers After The Static Scenario Is Prohibited.
        ///</summary>
        public int Id
        {
            get => this._Id;
            set
            {
                if( this._Id != value )
                {
                    this._Id = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region DeviceSource

        ///<summary>
        /// The PropertyName As ReadOnly String For DeviceSource.
        ///</summary>
        public const string DEVICESOURCE = "DeviceSource";

        ///<summary>
        /// The DefaultValue For DeviceSource.
        ///</summary>
        public static readonly DeviceSource DEFAULT_DEVICESOURCE = DeviceSource.Unknown;
        
        ///<summary>
        /// The Internal Field For DeviceSource.
        ///</summary>
        //private DeviceSource _DeviceSource = DeviceSource.Unknown;
        private DeviceSource _DeviceSource = DEFAULT_DEVICESOURCE;

        ///<summary>
        /// The Source Of This RF Device.
        ///</summary>
        public DeviceSource DeviceSource
        {
            get => this._DeviceSource;
            set
            {
                if( this._DeviceSource != value )
                {
                    this._DeviceSource = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region StartTime

        ///<summary>
        /// The PropertyName As ReadOnly String For StartTime.
        ///</summary>
        public const string STARTTIME = "StartTime";

        ///<summary>
        /// The DefaultValue For StartTime.
        ///</summary>
        public static readonly double DEFAULT_STARTTIME = 0;
        
        ///<summary>
        /// The Internal Field For StartTime.
        ///</summary>
        //private double _StartTime = 0;
        private double _StartTime = DEFAULT_STARTTIME;

        ///<summary>
        /// This Is The Simulation Time At Which The Parameters (Following The Time Parameter In The Same Line) Are Set. All Transmitters And Receivers Used In The Simulation Must Be Set At Start Of The Simulation, I.E. At Time=0. For Static Scenarios, Where Positions Or Characteristics Settings Never Change Throughout The Simulation, The Time Column Only Contains Zero’s.
        ///</summary>
        public double StartTime
        {
            get => this._StartTime;
            set
            {
                if( this._StartTime != value )
                {
                    this._StartTime = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Name

        ///<summary>
        /// The PropertyName As ReadOnly String For Name.
        ///</summary>
        public const string NAME = "Name";

        ///<summary>
        /// The DefaultValue For Name.
        ///</summary>
        public static readonly string DEFAULT_NAME = "RFDevice";
        
        ///<summary>
        /// The Internal Field For Name.
        ///</summary>
        //private string _Name = "RFDevice";
        private string _Name = DEFAULT_NAME;

        ///<summary>
        /// A Short Describing Display Name For The RF Device.
        ///</summary>
        public string Name
        {
            get => this._Name;
            set
            {
                if( this._Name != value )
                {
                    this._Name = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Latitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Latitude.
        ///</summary>
        public const string LATITUDE = "Latitude";

        ///<summary>
        /// The DefaultValue For Latitude.
        ///</summary>
        public static readonly Latitude DEFAULT_LATITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Latitude.
        ///</summary>
        //private Latitude _Latitude = double.NaN;
        private Latitude _Latitude = DEFAULT_LATITUDE;

        ///<summary>
        /// The Latitude Of The RF Device (WGS84).
        ///</summary>
        public Latitude Latitude
        {
            get => this._Latitude;
            set
            {
                if( this._Latitude != value )
                {
                    this._Latitude = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Longitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Longitude.
        ///</summary>
        public const string LONGITUDE = "Longitude";

        ///<summary>
        /// The DefaultValue For Longitude.
        ///</summary>
        public static readonly Longitude DEFAULT_LONGITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Longitude.
        ///</summary>
        //private Longitude _Longitude = double.NaN;
        private Longitude _Longitude = DEFAULT_LONGITUDE;

        ///<summary>
        /// The Longitude Of The RF Device (WGS84).
        ///</summary>
        public Longitude Longitude
        {
            get => this._Longitude;
            set
            {
                if( this._Longitude != value )
                {
                    this._Longitude = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Altitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Altitude.
        ///</summary>
        public const string ALTITUDE = "Altitude";

        ///<summary>
        /// The DefaultValue For Altitude.
        ///</summary>
        public static readonly Altitude DEFAULT_ALTITUDE = 0;
        
        ///<summary>
        /// The Internal Field For Altitude.
        ///</summary>
        //private Altitude _Altitude = 0;
        private Altitude _Altitude = DEFAULT_ALTITUDE;

        ///<summary>
        /// The Elevation Of The RF Device Above The Sea Level (Meter).
        ///</summary>
        public Altitude Altitude
        {
            get => this._Altitude;
            set
            {
                if( this._Altitude != value )
                {
                    this._Altitude = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Roll

        ///<summary>
        /// The PropertyName As ReadOnly String For Roll.
        ///</summary>
        public const string ROLL = "Roll";

        ///<summary>
        /// The DefaultValue For Roll.
        ///</summary>
        public static readonly double DEFAULT_ROLL = 0;
        
        ///<summary>
        /// The Internal Field For Roll.
        ///</summary>
        //private double _Roll = 0;
        private double _Roll = DEFAULT_ROLL;

        ///<summary>
        /// These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
        ///</summary>
        public double Roll
        {
            get => this._Roll;
            set
            {
                if( this._Roll != value )
                {
                    this._Roll = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Pitch

        ///<summary>
        /// The PropertyName As ReadOnly String For Pitch.
        ///</summary>
        public const string PITCH = "Pitch";

        ///<summary>
        /// The DefaultValue For Pitch.
        ///</summary>
        public static readonly double DEFAULT_PITCH = 0;
        
        ///<summary>
        /// The Internal Field For Pitch.
        ///</summary>
        //private double _Pitch = 0;
        private double _Pitch = DEFAULT_PITCH;

        ///<summary>
        /// These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
        ///</summary>
        public double Pitch
        {
            get => this._Pitch;
            set
            {
                if( this._Pitch != value )
                {
                    this._Pitch = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Yaw

        ///<summary>
        /// The PropertyName As ReadOnly String For Yaw.
        ///</summary>
        public const string YAW = "Yaw";

        ///<summary>
        /// The DefaultValue For Yaw.
        ///</summary>
        public static readonly double DEFAULT_YAW = 0;
        
        ///<summary>
        /// The Internal Field For Yaw.
        ///</summary>
        //private double _Yaw = 0;
        private double _Yaw = DEFAULT_YAW;

        ///<summary>
        /// These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
        ///</summary>
        public double Yaw
        {
            get => this._Yaw;
            set
            {
                if( this._Yaw != value )
                {
                    this._Yaw = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region RxTxType

        ///<summary>
        /// The PropertyName As ReadOnly String For RxTxType.
        ///</summary>
        public const string RXTXTYPE = "RxTxType";

        ///<summary>
        /// The DefaultValue For RxTxType.
        ///</summary>
        public static readonly RxTxType DEFAULT_RXTXTYPE = RxTxTypes.RxTxTypes.Unknown;
        
        ///<summary>
        /// The Internal Field For RxTxType.
        ///</summary>
        //private RxTxType _RxTxType = RxTxTypes.RxTxTypes.Unknown;
        private RxTxType _RxTxType = DEFAULT_RXTXTYPE;

        ///<summary>
        /// For All Receivers (i.e. ID’s &lt; 0) This Parameter Defines The Radio Being Used.
        ///</summary>
        public RxTxType RxTxType
        {
            get => this._RxTxType;
            set
            {
                if( this._RxTxType != value )
                {
                    this._RxTxType = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region AntennaType

        ///<summary>
        /// The PropertyName As ReadOnly String For AntennaType.
        ///</summary>
        public const string ANTENNATYPE = "AntennaType";

        ///<summary>
        /// The DefaultValue For AntennaType.
        ///</summary>
        public static readonly AntennaType DEFAULT_ANTENNATYPE = AntennaType.Unknown;
        
        ///<summary>
        /// The Internal Field For AntennaType.
        ///</summary>
        //private AntennaType _AntennaType = AntennaType.Unknown;
        private AntennaType _AntennaType = DEFAULT_ANTENNATYPE;

        ///<summary>
        /// AntennaType Defines The Antenna Type Used For Transmitter And Receiver Respectively. Note: Currently, Only Omnidirectional Antenna Type Is Available / Supported.
        ///</summary>
        public AntennaType AntennaType
        {
            get => this._AntennaType;
            set
            {
                if( this._AntennaType != value )
                {
                    this._AntennaType = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region CenterFrequency_Hz

        ///<summary>
        /// The PropertyName As ReadOnly String For CenterFrequency_Hz.
        ///</summary>
        public const string CENTERFREQUENCY_HZ = "CenterFrequency_Hz";

        ///<summary>
        /// The DefaultValue For CenterFrequency_Hz.
        ///</summary>
        public static readonly Frequency DEFAULT_CENTERFREQUENCY_HZ = 0;
        
        ///<summary>
        /// The Internal Field For CenterFrequency_Hz.
        ///</summary>
        //private Frequency _CenterFrequency_Hz = 0;
        private Frequency _CenterFrequency_Hz = DEFAULT_CENTERFREQUENCY_HZ;

        ///<summary>
        /// For Transmitters (I.E. Id’s &gt;= 0) This Parameter Defines Transmitter Signal Center Frequency [Hz]. For Receivers (I.E. Id’s &lt; 0) This Parameter Is Currently Unused.
        ///</summary>
        public Frequency CenterFrequency_Hz
        {
            get => this._CenterFrequency_Hz;
            set
            {
                if( this._CenterFrequency_Hz != value )
                {
                    this._CenterFrequency_Hz = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Bandwidth_Hz

        ///<summary>
        /// The PropertyName As ReadOnly String For Bandwidth_Hz.
        ///</summary>
        public const string BANDWIDTH_HZ = "Bandwidth_Hz";

        ///<summary>
        /// The DefaultValue For Bandwidth_Hz.
        ///</summary>
        public static readonly Bandwidth DEFAULT_BANDWIDTH_HZ = 0;
        
        ///<summary>
        /// The Internal Field For Bandwidth_Hz.
        ///</summary>
        //private Bandwidth _Bandwidth_Hz = 0;
        private Bandwidth _Bandwidth_Hz = DEFAULT_BANDWIDTH_HZ;

        ///<summary>
        /// The Bandwith Of The Transmitter.
        ///</summary>
        public Bandwidth Bandwidth_Hz
        {
            get => this._Bandwidth_Hz;
            set
            {
                if( this._Bandwidth_Hz != value )
                {
                    this._Bandwidth_Hz = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Gain_dB

        ///<summary>
        /// The PropertyName As ReadOnly String For Gain_dB.
        ///</summary>
        public const string GAIN_DB = "Gain_dB";

        ///<summary>
        /// The DefaultValue For Gain_dB.
        ///</summary>
        public static readonly Gain DEFAULT_GAIN_DB = 0;
        
        ///<summary>
        /// The Internal Field For Gain_dB.
        ///</summary>
        //private Gain _Gain_dB = 0;
        private Gain _Gain_dB = DEFAULT_GAIN_DB;

        ///<summary>
        /// For Transmitters (I.E. Id’s &gt;= 0) This Parameter Defines Transmitter Signal Power [Dbm]. For Receivers (I.E. Id’s &lt; 0) This Parameter Is Currently Unused.
        ///</summary>
        public Gain Gain_dB
        {
            get => this._Gain_dB;
            set
            {
                if( this._Gain_dB != value )
                {
                    this._Gain_dB = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region SignalToNoiseRatio_dB

        ///<summary>
        /// The PropertyName As ReadOnly String For SignalToNoiseRatio_dB.
        ///</summary>
        public const string SIGNALTONOISERATIO_DB = "SignalToNoiseRatio_dB";

        ///<summary>
        /// The DefaultValue For SignalToNoiseRatio_dB.
        ///</summary>
        public static readonly SignalToNoiseRatio DEFAULT_SIGNALTONOISERATIO_DB = 0;
        
        ///<summary>
        /// The Internal Field For SignalToNoiseRatio_dB.
        ///</summary>
        //private SignalToNoiseRatio _SignalToNoiseRatio_dB = 0;
        private SignalToNoiseRatio _SignalToNoiseRatio_dB = DEFAULT_SIGNALTONOISERATIO_DB;

        ///<summary>
        /// For Receivers (I.E. Id’s &lt; 0) This Parameter Is Imposes Gaussian White Noise To The Respective Receiver Signal. For Transmitters (I.E. Id’s &gt;= 0) This Parameter Is Unused.
        ///</summary>
        public SignalToNoiseRatio SignalToNoiseRatio_dB
        {
            get => this._SignalToNoiseRatio_dB;
            set
            {
                if( this._SignalToNoiseRatio_dB != value )
                {
                    this._SignalToNoiseRatio_dB = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region XPos

        ///<summary>
        /// The PropertyName As ReadOnly String For XPos.
        ///</summary>
        public const string XPOS = "XPos";

        ///<summary>
        /// The DefaultValue For XPos.
        ///</summary>
        public static readonly int DEFAULT_XPOS = 0;
        
        ///<summary>
        /// The Internal Field For XPos.
        ///</summary>
        //private int _XPos = 0;
        private int _XPos = DEFAULT_XPOS;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int XPos
        {
            get => this._XPos;
            set
            {
                if( this._XPos != value )
                {
                    this._XPos = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region YPos

        ///<summary>
        /// The PropertyName As ReadOnly String For YPos.
        ///</summary>
        public const string YPOS = "YPos";

        ///<summary>
        /// The DefaultValue For YPos.
        ///</summary>
        public static readonly int DEFAULT_YPOS = 0;
        
        ///<summary>
        /// The Internal Field For YPos.
        ///</summary>
        //private int _YPos = 0;
        private int _YPos = DEFAULT_YPOS;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int YPos
        {
            get => this._YPos;
            set
            {
                if( this._YPos != value )
                {
                    this._YPos = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region ZPos

        ///<summary>
        /// The PropertyName As ReadOnly String For ZPos.
        ///</summary>
        public const string ZPOS = "ZPos";

        ///<summary>
        /// The DefaultValue For ZPos.
        ///</summary>
        public static readonly int DEFAULT_ZPOS = 0;
        
        ///<summary>
        /// The Internal Field For ZPos.
        ///</summary>
        //private int _ZPos = 0;
        private int _ZPos = DEFAULT_ZPOS;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int ZPos
        {
            get => this._ZPos;
            set
            {
                if( this._ZPos != value )
                {
                    this._ZPos = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region TechnicalParameters

        ///<summary>
        /// The PropertyName As ReadOnly String For TechnicalParameters.
        ///</summary>
        public const string TECHNICALPARAMETERS = "TechnicalParameters";

        ///<summary>
        /// The DefaultValue For TechnicalParameters.
        ///</summary>
        public static readonly string DEFAULT_TECHNICALPARAMETERS = "";
        
        ///<summary>
        /// The Internal Field For TechnicalParameters.
        ///</summary>
        //private string _TechnicalParameters = "";
        private string _TechnicalParameters = DEFAULT_TECHNICALPARAMETERS;

        ///<summary>
        /// Additional (Optional) Technical Parameters For The Simulation.
        ///</summary>
        public string TechnicalParameters
        {
            get => this._TechnicalParameters;
            set
            {
                if( this._TechnicalParameters != value )
                {
                    this._TechnicalParameters = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Remark

        ///<summary>
        /// The PropertyName As ReadOnly String For Remark.
        ///</summary>
        public const string REMARK = "Remark";

        ///<summary>
        /// The DefaultValue For Remark.
        ///</summary>
        public static readonly string DEFAULT_REMARK = "";
        
        ///<summary>
        /// The Internal Field For Remark.
        ///</summary>
        //private string _Remark = "";
        private string _Remark = DEFAULT_REMARK;

        ///<summary>
        /// A Comment Or Remark For The RF Device.
        ///</summary>
        public string Remark
        {
            get => this._Remark;
            set
            {
                if( this._Remark != value )
                {
                    this._Remark = value;

                    FirePropertyChanged();
                }
            }
        }

        #endregion        

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the XML.
        /// </summary>
        /// <returns></returns>
        public XElement ToXml()
        {
            return new XElement("RFDevice",

                XElementExtension.GetXElement("PrimaryKey", this.PrimaryKey),
                XElementExtension.GetXElement("Id", this.Id),
                XElementExtension.GetXElement("DeviceSource", this.DeviceSource),
                XElementExtension.GetXElement("StartTime", this.StartTime),
                XElementExtension.GetXElement("Name", this.Name),
                XElementExtension.GetXElement("Latitude", this.Latitude),
                XElementExtension.GetXElement("Longitude", this.Longitude),
                XElementExtension.GetXElement("Altitude", this.Altitude),
                XElementExtension.GetXElement("Roll", this.Roll),
                XElementExtension.GetXElement("Pitch", this.Pitch),
                XElementExtension.GetXElement("Yaw", this.Yaw),
                XElementExtension.GetXElement("RxTxType", this.RxTxType),
                XElementExtension.GetXElement("AntennaType", this.AntennaType),
                XElementExtension.GetXElement("CenterFrequency_Hz", this.CenterFrequency_Hz),
                XElementExtension.GetXElement("Bandwidth_Hz", this.Bandwidth_Hz),
                XElementExtension.GetXElement("Gain_dB", this.Gain_dB),
                XElementExtension.GetXElement("SignalToNoiseRatio_dB", this.SignalToNoiseRatio_dB),
                XElementExtension.GetXElement("XPos", this.XPos),
                XElementExtension.GetXElement("YPos", this.YPos),
                XElementExtension.GetXElement("ZPos", this.ZPos),
                XElementExtension.GetXElement("TechnicalParameters", this.TechnicalParameters),
                XElementExtension.GetXElement("Remark", this.Remark)  
            );
        }


        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="eRoot">The e root.</param>
        /// <returns></returns>
        static public RFDevice FromXml(XElement eRoot)
        {
            XElement eChild = null;

            if (eRoot.Name.LocalName.Equals("RFDevice"))
            {
                eChild = eRoot;
            }
            else
            {
                eChild = eRoot.Element("RFDevice");
            }

            return new RFDevice
            {
                PrimaryKey = eChild.GetProperty<Guid>("PrimaryKey",Guid.NewGuid()),
                Id = eChild.GetProperty<int>("Id",0),
                DeviceSource = eChild.GetProperty<DeviceSource>("DeviceSource",DeviceSource.Unknown),
                StartTime = eChild.GetProperty<double>("StartTime",0),
                Name = eChild.GetProperty<string>("Name","RFDevice"),
                Latitude = eChild.GetProperty<Latitude>("Latitude",double.NaN),
                Longitude = eChild.GetProperty<Longitude>("Longitude",double.NaN),
                Altitude = eChild.GetProperty<Altitude>("Altitude",0),
                Roll = eChild.GetProperty<double>("Roll",0),
                Pitch = eChild.GetProperty<double>("Pitch",0),
                Yaw = eChild.GetProperty<double>("Yaw",0),
                RxTxType = eChild.GetProperty<RxTxType>("RxTxType",RxTxTypes.RxTxTypes.Unknown),
                AntennaType = eChild.GetProperty<AntennaType>("AntennaType",AntennaType.Unknown),
                CenterFrequency_Hz = eChild.GetProperty<Frequency>("CenterFrequency_Hz",0),
                Bandwidth_Hz = eChild.GetProperty<Bandwidth>("Bandwidth_Hz",0),
                Gain_dB = eChild.GetProperty<Gain>("Gain_dB",0),
                SignalToNoiseRatio_dB = eChild.GetProperty<SignalToNoiseRatio>("SignalToNoiseRatio_dB",0),
                XPos = eChild.GetProperty<int>("XPos",0),
                YPos = eChild.GetProperty<int>("YPos",0),
                ZPos = eChild.GetProperty<int>("ZPos",0),
                TechnicalParameters = eChild.GetProperty<string>("TechnicalParameters",""),
                Remark = eChild.GetProperty<string>("Remark","")            
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
        public bool Equals(RFDevice other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.PrimaryKey == null && this.PrimaryKey != null)
            {
                return false;
            }

            if (other.PrimaryKey != null && this.PrimaryKey == null)
            {
                return false;
            }

            if (other.PrimaryKey != null && this.PrimaryKey != null && other.PrimaryKey.Equals(this.PrimaryKey) == false)
            {
                return false;
            }

            if ( this.Id != other.Id )
            {
                return false;
            }

            if ( this.DeviceSource != other.DeviceSource )
            {
                return false;
            }

            if ( this.StartTime != other.StartTime )
            {
                return false;
            }

            if (other.Name == null && this.Name != null)
            {
                return false;
            }

            if (other.Name != null && this.Name == null)
            {
                return false;
            }

            if (other.Name != null && this.Name != null && other.Name.Equals(this.Name) == false)
            {
                return false;
            }

            if (other.Latitude == null && this.Latitude != null)
            {
                return false;
            }

            if (other.Latitude != null && this.Latitude == null)
            {
                return false;
            }

            if (other.Latitude != null && this.Latitude != null && other.Latitude.Equals(this.Latitude) == false)
            {
                return false;
            }

            if (other.Longitude == null && this.Longitude != null)
            {
                return false;
            }

            if (other.Longitude != null && this.Longitude == null)
            {
                return false;
            }

            if (other.Longitude != null && this.Longitude != null && other.Longitude.Equals(this.Longitude) == false)
            {
                return false;
            }

            if (other.Altitude == null && this.Altitude != null)
            {
                return false;
            }

            if (other.Altitude != null && this.Altitude == null)
            {
                return false;
            }

            if (other.Altitude != null && this.Altitude != null && other.Altitude.Equals(this.Altitude) == false)
            {
                return false;
            }

            if ( this.Roll != other.Roll )
            {
                return false;
            }

            if ( this.Pitch != other.Pitch )
            {
                return false;
            }

            if ( this.Yaw != other.Yaw )
            {
                return false;
            }

            if (other.RxTxType == null && this.RxTxType != null)
            {
                return false;
            }

            if (other.RxTxType != null && this.RxTxType == null)
            {
                return false;
            }

            if (other.RxTxType != null && this.RxTxType != null && other.RxTxType.Equals(this.RxTxType) == false)
            {
                return false;
            }

            if ( this.AntennaType != other.AntennaType )
            {
                return false;
            }

            if (other.CenterFrequency_Hz == null && this.CenterFrequency_Hz != null)
            {
                return false;
            }

            if (other.CenterFrequency_Hz != null && this.CenterFrequency_Hz == null)
            {
                return false;
            }

            if (other.CenterFrequency_Hz != null && this.CenterFrequency_Hz != null && other.CenterFrequency_Hz.Equals(this.CenterFrequency_Hz) == false)
            {
                return false;
            }

            if (other.Bandwidth_Hz == null && this.Bandwidth_Hz != null)
            {
                return false;
            }

            if (other.Bandwidth_Hz != null && this.Bandwidth_Hz == null)
            {
                return false;
            }

            if (other.Bandwidth_Hz != null && this.Bandwidth_Hz != null && other.Bandwidth_Hz.Equals(this.Bandwidth_Hz) == false)
            {
                return false;
            }

            if (other.Gain_dB == null && this.Gain_dB != null)
            {
                return false;
            }

            if (other.Gain_dB != null && this.Gain_dB == null)
            {
                return false;
            }

            if (other.Gain_dB != null && this.Gain_dB != null && other.Gain_dB.Equals(this.Gain_dB) == false)
            {
                return false;
            }

            if (other.SignalToNoiseRatio_dB == null && this.SignalToNoiseRatio_dB != null)
            {
                return false;
            }

            if (other.SignalToNoiseRatio_dB != null && this.SignalToNoiseRatio_dB == null)
            {
                return false;
            }

            if (other.SignalToNoiseRatio_dB != null && this.SignalToNoiseRatio_dB != null && other.SignalToNoiseRatio_dB.Equals(this.SignalToNoiseRatio_dB) == false)
            {
                return false;
            }

            if ( this.XPos != other.XPos )
            {
                return false;
            }

            if ( this.YPos != other.YPos )
            {
                return false;
            }

            if ( this.ZPos != other.ZPos )
            {
                return false;
            }

            if (other.TechnicalParameters == null && this.TechnicalParameters != null)
            {
                return false;
            }

            if (other.TechnicalParameters != null && this.TechnicalParameters == null)
            {
                return false;
            }

            if (other.TechnicalParameters != null && this.TechnicalParameters != null && other.TechnicalParameters.Equals(this.TechnicalParameters) == false)
            {
                return false;
            }

            if (other.Remark == null && this.Remark != null)
            {
                return false;
            }

            if (other.Remark != null && this.Remark == null)
            {
                return false;
            }

            if (other.Remark != null && this.Remark != null && other.Remark.Equals(this.Remark) == false)
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
            return (RFDevice)this.MemberwiseClone();
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
    public sealed class RFDeviceTooltips
    {

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
        /// The tooltip for the TechnicalParameters.
        /// </summary>
        public string TOOLTIP_TECHNICALPARAMETERS { get { return "Additional (Optional) Technical Parameters\nFor The Simulation."; } }

        /// <summary>
        /// The tooltip for the Remark.
        /// </summary>
        public string TOOLTIP_REMARK { get { return "A Comment Or Remark For The RF Device."; } }

    } // end public class RFDeviceTooltips
}