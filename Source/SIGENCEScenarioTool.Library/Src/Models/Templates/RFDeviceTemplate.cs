using System.Collections.ObjectModel;



namespace SIGENCEScenarioTool.Models.Templates
{
    /// <summary>
    /// A Instance With Template Data From An RFDevice.
    /// </summary>
    /// <seealso cref="RFDevice" />
    public sealed class RFDeviceTemplate : RFDevice
    {
    } // end sealed public class RFDeviceTemplate


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{RFDeviceTemplate}" />
    public sealed class RFDeviceTemplateCollection : ObservableCollection<RFDeviceTemplate>
    {
    } // end sealed public class RFDeviceTemplateCollection
}
