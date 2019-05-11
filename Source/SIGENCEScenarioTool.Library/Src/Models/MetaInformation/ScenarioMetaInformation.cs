
/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */

using System;
using System.Xml.Linq;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Interfaces;




namespace SIGENCEScenarioTool.Models.MetaInformation
{
    ///<summary>
    /// Represent MetaInformationData For An Scenario.
    ///</summary>
    public partial class ScenarioMetaInformation : AbstractModelBase, IEquatable<ScenarioMetaInformation>, ICloneable, IXmlExport
    {

        #region Instance Properties

        #region Version

        ///<summary>
        /// The PropertyName As ReadOnly String For Version.
        ///</summary>
        public const string VERSION = "Version";

        ///<summary>
        /// The DefaultValue For Version.
        ///</summary>
        public static readonly string DEFAULT_VERSION = "";
        
        ///<summary>
        /// The Internal Field For Version.
        ///</summary>
        //private string _Version = "";
        private string _Version = DEFAULT_VERSION;

        ///<summary>
        /// The Version Of This Scenario.
        ///</summary>
        public string Version 
        {
            get { return _Version; }
            set
            {
                _Version = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_VERSION = "The Version Of This Scenario.";

        #endregion        

        //---------------------------------------------------------------------

        #region ApplicationContext

        ///<summary>
        /// The PropertyName As ReadOnly String For ApplicationContext.
        ///</summary>
        public const string APPLICATIONCONTEXT = "ApplicationContext";

        ///<summary>
        /// The DefaultValue For ApplicationContext.
        ///</summary>
        public static readonly string DEFAULT_APPLICATIONCONTEXT = "";
        
        ///<summary>
        /// The Internal Field For ApplicationContext.
        ///</summary>
        //private string _ApplicationContext = "";
        private string _ApplicationContext = DEFAULT_APPLICATIONCONTEXT;

        ///<summary>
        /// For Which Application Or Tool Is The Scenario.
        ///</summary>
        public string ApplicationContext 
        {
            get { return _ApplicationContext; }
            set
            {
                _ApplicationContext = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_APPLICATIONCONTEXT = "For Which Application Or Tool Is The Scenario.";

        #endregion        

        //---------------------------------------------------------------------

        #region ContactPerson

        ///<summary>
        /// The PropertyName As ReadOnly String For ContactPerson.
        ///</summary>
        public const string CONTACTPERSON = "ContactPerson";

        ///<summary>
        /// The DefaultValue For ContactPerson.
        ///</summary>
        public static readonly string DEFAULT_CONTACTPERSON = "";
        
        ///<summary>
        /// The Internal Field For ContactPerson.
        ///</summary>
        //private string _ContactPerson = "";
        private string _ContactPerson = DEFAULT_CONTACTPERSON;

        ///<summary>
        /// An Contact Person If You Have Questions About The Scenario.
        ///</summary>
        public string ContactPerson 
        {
            get { return _ContactPerson; }
            set
            {
                _ContactPerson = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_CONTACTPERSON = "An Contact Person If You Have Questions About The Scenario.";

        #endregion        

        //---------------------------------------------------------------------

        #region Description

        ///<summary>
        /// The PropertyName As ReadOnly String For Description.
        ///</summary>
        public const string DESCRIPTION = "Description";

        ///<summary>
        /// The DefaultValue For Description.
        ///</summary>
        public static readonly string DEFAULT_DESCRIPTION = "";
        
        ///<summary>
        /// The Internal Field For Description.
        ///</summary>
        //private string _Description = "";
        private string _Description = DEFAULT_DESCRIPTION;

        ///<summary>
        /// An Markdown Text Content Where You Can Describe The Scenario And The Excepted Result.
        ///</summary>
        public string Description 
        {
            get { return _Description; }
            set
            {
                _Description = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_DESCRIPTION = "An Markdown Text Content Where You Can Describe The Scenario And The Excepted Result.";

        #endregion        

        //---------------------------------------------------------------------

        #region Stylesheet

        ///<summary>
        /// The PropertyName As ReadOnly String For Stylesheet.
        ///</summary>
        public const string STYLESHEET = "Stylesheet";

        ///<summary>
        /// The DefaultValue For Stylesheet.
        ///</summary>
        public static readonly string DEFAULT_STYLESHEET = "";
        
        ///<summary>
        /// The Internal Field For Stylesheet.
        ///</summary>
        //private string _Stylesheet = "";
        private string _Stylesheet = DEFAULT_STYLESHEET;

        ///<summary>
        /// An Optional Stylesheet To Use For Rendering The Generated HTML.
        ///</summary>
        public string Stylesheet 
        {
            get { return _Stylesheet; }
            set
            {
                _Stylesheet = value;
                FirePropertyChanged();
            }
        }

        //static public readonly string TOOLTIP_STYLESHEET = "An Optional Stylesheet To Use For Rendering The Generated HTML.";

        #endregion        

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the XML.
        /// </summary>
        /// <returns></returns>
        public XElement ToXml()
        {
            return new XElement("ScenarioMetaInformation",

                XElementExtension.GetXElement("Version", Version, false),
                XElementExtension.GetXElement("ApplicationContext", ApplicationContext, false),
                XElementExtension.GetXElement("ContactPerson", ContactPerson, false),
                XElementExtension.GetXElement("Description", Description, true),
                XElementExtension.GetXElement("Stylesheet", Stylesheet, true)  
            );
        }


        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="eRoot">The e root.</param>
        /// <returns></returns>
        static public ScenarioMetaInformation FromXml(XElement eRoot)
        {
            XElement eChild = null;

            if (eRoot.Name.LocalName.Equals("ScenarioMetaInformation"))
            {
                eChild = eRoot;
            }
            else
            {
                eChild = eRoot.Element("ScenarioMetaInformation");
            }

            return new ScenarioMetaInformation
            {
                Version = eChild.GetProperty<string>("Version",""),
                ApplicationContext = eChild.GetProperty<string>("ApplicationContext",""),
                ContactPerson = eChild.GetProperty<string>("ContactPerson",""),
                Description = eChild.GetProperty<string>("Description",""),
                Stylesheet = eChild.GetProperty<string>("Stylesheet","")            
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
        public bool Equals(ScenarioMetaInformation other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.Version == null && Version != null)
            {
                return false;
            }

            if (other.Version != null && Version == null)
            {
                return false;
            }

            if (other.Version != null && Version != null && other.Version.Equals(Version) == false)
            {
                return false;
            }

            if (other.ApplicationContext == null && ApplicationContext != null)
            {
                return false;
            }

            if (other.ApplicationContext != null && ApplicationContext == null)
            {
                return false;
            }

            if (other.ApplicationContext != null && ApplicationContext != null && other.ApplicationContext.Equals(ApplicationContext) == false)
            {
                return false;
            }

            if (other.ContactPerson == null && ContactPerson != null)
            {
                return false;
            }

            if (other.ContactPerson != null && ContactPerson == null)
            {
                return false;
            }

            if (other.ContactPerson != null && ContactPerson != null && other.ContactPerson.Equals(ContactPerson) == false)
            {
                return false;
            }

            if (other.Description == null && Description != null)
            {
                return false;
            }

            if (other.Description != null && Description == null)
            {
                return false;
            }

            if (other.Description != null && Description != null && other.Description.Equals(Description) == false)
            {
                return false;
            }

            if (other.Stylesheet == null && Stylesheet != null)
            {
                return false;
            }

            if (other.Stylesheet != null && Stylesheet == null)
            {
                return false;
            }

            if (other.Stylesheet != null && Stylesheet != null && other.Stylesheet.Equals(Stylesheet) == false)
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
        public ScenarioMetaInformation Clone()
        {
            return (ScenarioMetaInformation)this.MemberwiseClone();
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

    } // end sealed public class ScenarioMetaInformation



    /// <summary>
    /// The tooltips for our properties to display in the HMI.
    /// </summary>
    public sealed class ScenarioMetaInformationTooltips
    {

        /// <summary>
        /// The tooltip for the Version.
        /// </summary>
        public string TOOLTIP_VERSION { get { return "The Version Of This Scenario."; } }

        /// <summary>
        /// The tooltip for the ApplicationContext.
        /// </summary>
        public string TOOLTIP_APPLICATIONCONTEXT { get { return "For Which Application Or Tool Is The Scenario."; } }

        /// <summary>
        /// The tooltip for the ContactPerson.
        /// </summary>
        public string TOOLTIP_CONTACTPERSON { get { return "An Contact Person If You Have Questions About\nThe Scenario."; } }

        /// <summary>
        /// The tooltip for the Description.
        /// </summary>
        public string TOOLTIP_DESCRIPTION { get { return "An Markdown Text Content Where You Can Describe\nThe Scenario And The Excepted Result."; } }

        /// <summary>
        /// The tooltip for the Stylesheet.
        /// </summary>
        public string TOOLTIP_STYLESHEET { get { return "An Optional Stylesheet To Use For Rendering\nThe Generated HTML."; } }

    } // end public class ScenarioMetaInformationTooltips
}