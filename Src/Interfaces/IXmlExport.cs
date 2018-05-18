using System.Xml.Linq;



namespace TransmitterTool.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IXmlExport
    {
        XElement ToXml();

    } // end public interface IXmlExport
}
