
using System;
using System.Xml.Linq;

using SIGENCEScenarioTool.Models.Attachements;



namespace SIGENCEScenarioTool.Models.Scenario
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SIGENCEScenario
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        private string Version { get; set; }

        /// <summary>
        /// Gets or sets the application context.
        /// </summary>
        /// <value>
        /// The application context.
        /// </value>
        private string ApplicationContext { get; set; }

        /// <summary>
        /// Gets or sets the contact person.
        /// </summary>
        /// <value>
        /// The contact person.
        /// </value>
        private string ContactPerson { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the stylesheet.
        /// </summary>
        /// <value>
        /// The stylesheet.
        /// </value>
        public string Stylesheet { get; set; }


        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        public RFDeviceList Devices { get; set; }

        /// <summary>
        /// Gets or sets the attachements.
        /// </summary>
        /// <value>
        /// The attachements.
        /// </value>
        public AttachementList Attachements { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the XML.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public XElement ToXml()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="eRoot">The e root.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static public SIGENCEScenario FromXml(XElement eRoot)
        {
            throw new NotImplementedException();
        }

    } // end public sealed class SIGENCEScenario
}
