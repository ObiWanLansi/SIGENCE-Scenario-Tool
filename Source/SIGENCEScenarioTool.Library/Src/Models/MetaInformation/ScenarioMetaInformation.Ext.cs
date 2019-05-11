using System.Xml.Linq;

using SIGENCEScenarioTool.Extensions;



namespace SIGENCEScenarioTool.Models.MetaInformation
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ScenarioMetaInformation
    {

        ///// <summary>
        ///// Sets the description and stylesheet.
        ///// </summary>
        ///// <param name="strDescription">The string description.</param>
        ///// <param name="strStylesheet">The string stylesheet.</param>
        //public void SetDescriptionAndStylesheet(string strDescription, string strStylesheet)
        //{
        //    this._Description = strDescription;
        //    this._Stylesheet = strStylesheet;

        //    //FirePropertyChanged("DescriptionAndStylesheet");
        //}


        /// <summary>
        /// Sets the description without event.
        /// </summary>
        /// <param name="strDescription">The string description.</param>
        public void SetDescriptionWithoutEvent(string strDescription)
        {
            this._Description = strDescription;
        }


        /// <summary>
        /// Sets the style sheet without event.
        /// </summary>
        /// <param name="strStyleSheet">The string style sheet.</param>
        public void SetStyleSheetWithoutEvent(string strStyleSheet)
        {
            this._Stylesheet = strStyleSheet;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.Version = DEFAULT_VERSION;
            this.ApplicationContext = DEFAULT_APPLICATIONCONTEXT;
            this.ContactPerson = DEFAULT_CONTACTPERSON;

            //SetDescriptionAndStylesheet(DEFAULT_DESCRIPTION, DEFAULT_STYLESHEET);

            this.Description = DEFAULT_DESCRIPTION;
            this.Stylesheet = DEFAULT_STYLESHEET;

            //if (this.Attachements == null)
            //{
            //    this.Attachements = new ObservableCollection<Attachement>();
            //}
            //else
            //{
            //    this.Attachements.Clear();
            //}
        }


        /// <summary>
        /// Loads from XML.
        /// </summary>
        /// <param name="eRoot">The e root.</param>
        public void LoadFromXml(XElement eRoot)
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

            this.Version = eChild.GetProperty<string>(VERSION, "");
            this.ApplicationContext = eChild.GetProperty<string>(APPLICATIONCONTEXT, "");
            this.ContactPerson = eChild.GetProperty<string>(CONTACTPERSON, "");
            this.Description = eChild.GetProperty<string>(DESCRIPTION, "");
            this.Stylesheet = eChild.GetProperty<string>(STYLESHEET, "");
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        #region OldStuff
        ///// <summary>
        ///// Gets or sets the version.
        ///// </summary>
        ///// <value>
        ///// The version.
        ///// </value>
        //private string Version { get; set; }

        ///// <summary>
        ///// Gets or sets the application context.
        ///// </summary>
        ///// <value>
        ///// The application context.
        ///// </value>
        //private string ApplicationContext { get; set; }

        ///// <summary>
        ///// Gets or sets the contact person.
        ///// </summary>
        ///// <value>
        ///// The contact person.
        ///// </value>
        //private string ContactPerson { get; set; }

        ///// <summary>
        ///// Gets or sets the description.
        ///// </summary>
        ///// <value>
        ///// The description.
        ///// </value>
        //public string Description { get; set; }

        ///// <summary>
        ///// Gets or sets the stylesheet.
        ///// </summary>
        ///// <value>
        ///// The stylesheet.
        ///// </value>
        //public string Stylesheet { get; set; }

        ///// <summary>
        ///// Gets or sets the attachements.
        ///// </summary>
        ///// <value>
        ///// The attachements.
        ///// </value>
        //public AttachementList Attachements { get; set; } = new AttachementList();

        ////-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// To the XML.
        ///// </summary>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //public XElement ToXml()
        //{
        //    throw new NotImplementedException();
        //}


        ///// <summary>
        ///// Froms the XML.
        ///// </summary>
        ///// <param name="eRoot">The e root.</param>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //static public ScenarioMetaInformation FromXml(XElement eRoot)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

    } // end public partial class ScenarioMetaInformation
}
