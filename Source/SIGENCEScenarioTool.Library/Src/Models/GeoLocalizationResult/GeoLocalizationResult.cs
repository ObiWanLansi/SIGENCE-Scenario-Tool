
/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */

using System;
using System.Xml.Linq;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Interfaces;



namespace SIGENCEScenarioTool.Models
{
    ///<summary>
    /// Represent The Geo Localization Result Of A RFDevice.
    ///</summary>
    public sealed class GeoLocalizationResult : AbstractModelBase, IEquatable<GeoLocalizationResult>, ICloneable, IXmlExport
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
        public static readonly Guid DEFAULT_PRIMARYKEY = Guid.NewGuid();
        
        ///<summary>
        /// The Internal Field For PrimaryKey.
        ///</summary>
        private Guid _PrimaryKey = Guid.NewGuid();

        ///<summary>
        /// The Unique PrimarKey For This Result.
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
        public static readonly int DEFAULT_ID = 0;
        
        ///<summary>
        /// The Internal Field For Id.
        ///</summary>
        private int _Id = 0;

        ///<summary>
        /// The Id Of The Localized RFDevice.
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
        public static readonly double DEFAULT_LATITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Latitude.
        ///</summary>
        private double _Latitude = double.NaN;

        ///<summary>
        /// The Latitude Of The Localized RF Device (WGS84).
        ///</summary>
        public double Latitude 
        {
            get { return _Latitude; }
            set
            {
                _Latitude = value;
                FirePropertyChanged();
            }
        }

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
        public static readonly double DEFAULT_LONGITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Longitude.
        ///</summary>
        private double _Longitude = double.NaN;

        ///<summary>
        /// The Longitude Of The Localized RF Device (WGS84).
        ///</summary>
        public double Longitude 
        {
            get { return _Longitude; }
            set
            {
                _Longitude = value;
                FirePropertyChanged();
            }
        }

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
        public static readonly uint DEFAULT_ALTITUDE = 0;
        
        ///<summary>
        /// The Internal Field For Altitude.
        ///</summary>
        private uint _Altitude = 0;

        ///<summary>
        /// The Elevation Of The Localized RF Device Above The Sea Level (Meter).
        ///</summary>
        public uint Altitude 
        {
            get { return _Altitude; }
            set
            {
                _Altitude = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region LocalizationTime

        ///<summary>
        /// The PropertyName As ReadOnly String For LocalizationTime.
        ///</summary>
        public const String LOCALIZATIONTIME = "LocalizationTime";

        ///<summary>
        /// The DefaultValue For LocalizationTime.
        ///</summary>
        public static readonly double DEFAULT_LOCALIZATIONTIME = 0;
        
        ///<summary>
        /// The Internal Field For LocalizationTime.
        ///</summary>
        private double _LocalizationTime = 0;

        ///<summary>
        /// The Localization Time.
        ///</summary>
        public double LocalizationTime 
        {
            get { return _LocalizationTime; }
            set
            {
                _LocalizationTime = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Return An XML Element From This Instance.
        /// </summary>
        /// <returns></returns>
        public XElement ToXml()
        {
            return new XElement("GeoLocalizationResult",

                XElementExtension.GetXElement("PrimaryKey", PrimaryKey),
                XElementExtension.GetXElement("Id", Id),
                XElementExtension.GetXElement("Latitude", Latitude),
                XElementExtension.GetXElement("Longitude", Longitude),
                XElementExtension.GetXElement("Altitude", Altitude),
                XElementExtension.GetXElement("LocalizationTime", LocalizationTime)  
            );
        }


        /// <summary>
        /// Create An Instance From An XML Element.
        /// </summary>
        /// <param name="eRoot">The e root.</param>
        /// <returns></returns>
        public static GeoLocalizationResult FromXml(XElement eRoot)
        {
            XElement eChild = null;

            if (eRoot.Name.LocalName.Equals("GeoLocalizationResult"))
            {
                eChild = eRoot;
            }
            else
            {
                eChild = eRoot.Element("GeoLocalizationResult");
            }

            return new GeoLocalizationResult
            {
                PrimaryKey = eChild.GetProperty<Guid>("PrimaryKey",Guid.NewGuid()),
                Id = eChild.GetProperty<int>("Id",0),
                Latitude = eChild.GetProperty<double>("Latitude",double.NaN),
                Longitude = eChild.GetProperty<double>("Longitude",double.NaN),
                Altitude = eChild.GetProperty<uint>("Altitude",0),
                LocalizationTime = eChild.GetProperty<double>("LocalizationTime",0)            
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public bool Equals(GeoLocalizationResult other)
        {
            if (other == null)
            {
                return false;
            }

            if (PrimaryKey != other.PrimaryKey )
            {
                return false;
            }

            if (Id != other.Id )
            {
                return false;
            }

            if (Latitude != other.Latitude )
            {
                return false;
            }

            if (Longitude != other.Longitude )
            {
                return false;
            }

            if (Altitude != other.Altitude )
            {
                return false;
            }

            if (LocalizationTime != other.LocalizationTime )
            {
                return false;
            }

            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GeoLocalizationResult Clone()
        {
            return (GeoLocalizationResult)this.MemberwiseClone();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

    } // end sealed public class GeoLocalizationResult
}