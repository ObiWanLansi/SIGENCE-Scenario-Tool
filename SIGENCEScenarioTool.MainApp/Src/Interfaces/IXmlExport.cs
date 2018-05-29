using System.Xml.Linq;



namespace SIGENCEScenarioTool.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IXmlExport
    {
        /// <summary>
        /// To the XML.
        /// </summary>
        /// <returns></returns>
        XElement ToXml();

    } // end public interface IXmlExport
}
