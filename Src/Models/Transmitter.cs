
/**
 * SourceFile     : D:\BigData\GitHub\TransmitterTool\Src\Models\Transmitter.xml
 * Timestamp      : 01.05.2018, 20:35
 * User           : Jörg Lanser Lokal
 * Host           : MARUSHA
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

using GMap.NET;

using TransmitterTool.Extensions;



namespace TransmitterTool.Models
{
    ///<summary>
    /// Generated Model Class from Transmitter.xml.
    ///</summary>
    sealed public class Transmitter : IEquatable<Transmitter>, INotifyPropertyChanged, ICloneable
    {

        #region Instance Properties

        #region Id

        ///<summary>
        /// The PropertyName As ReadOnly String For Id.
        ///</summary>
        public const String ID = "Id";

        ///<summary>
        /// The DefaultValue For Id.
        ///</summary>
        static public readonly Guid DEFAULT_ID = Guid.NewGuid();
        
        ///<summary>
        /// The Internal Field For Id.
        ///</summary>
        private Guid _Id = Guid.NewGuid();

        ///<summary>
        /// Id As Guid.
        ///</summary>
        public Guid Id 
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

        #region Name

        ///<summary>
        /// The PropertyName As ReadOnly String For Name.
        ///</summary>
        public const String NAME = "Name";

        ///<summary>
        /// The DefaultValue For Name.
        ///</summary>
        static public readonly string DEFAULT_NAME = "Transmitter";
        
        ///<summary>
        /// The Internal Field For Name.
        ///</summary>
        private string _Name = "Transmitter";

        ///<summary>
        /// Name As string.
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
        static public readonly double DEFAULT_LATITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Latitude.
        ///</summary>
        private double _Latitude = double.NaN;

        ///<summary>
        /// Latitude As double.
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
        static public readonly double DEFAULT_LONGITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Longitude.
        ///</summary>
        private double _Longitude = double.NaN;

        ///<summary>
        /// Longitude As double.
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
        /// Remark As string.
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

        #endregion        

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public XElement ToXml()
        {
            return new XElement("Transmitter",

                XElementExtension.GetXElement("Id", Id),
                XElementExtension.GetXElement("Name", Name),
                XElementExtension.GetXElement("Latitude", Latitude),
                XElementExtension.GetXElement("Longitude", Longitude),
                XElementExtension.GetXElement("Remark", Remark)  
            );
        }


        static public Transmitter FromXml(XElement eRoot)
        {
            XElement eChild = null;

            if (eRoot.Name.LocalName.Equals("Transmitter"))
            {
                eChild = eRoot;
            }
            else
            {
                eChild = eRoot.Element("Transmitter");
            }

            return new Transmitter
            {
                Id = eChild.GetProperty<Guid>("Id",Guid.NewGuid()),
                Name = eChild.GetProperty<string>("Name","Transmitter"),
                Latitude = eChild.GetProperty<double>("Latitude",double.NaN),
                Longitude = eChild.GetProperty<double>("Longitude",double.NaN),
                Remark = eChild.GetProperty<string>("Remark","")            
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public bool Equals(Transmitter other)
        {
            if (other == null)
            {
                return false;
            }

            if (Id != other.Id )
            {
                return false;
            }

            if (Name != other.Name )
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

            if (Remark != other.Remark )
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
        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "Unknown" : Name;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Transmitter Clone()
        {
            return (Transmitter)this.MemberwiseClone();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPropertyName"></param>
        private void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            // Wir cachen das Event lokal da es während der Abfrage in der if Anweisung und
            // dem eigentlichen Ausführen zurückgesetzt werden könnte und somit eine Exception
            // hervorgerufen werden könnte obwohl wir es ja überprüft haben.
            var temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }

    } // end sealed public class Transmitter
}