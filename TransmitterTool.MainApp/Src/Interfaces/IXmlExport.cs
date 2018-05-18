using System.Xml.Linq;



namespace TransmitterTool.Interfaces
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
