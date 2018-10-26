using System.Collections.ObjectModel;



namespace SIGENCEScenarioTool.Models.Templates
{
    /// <summary>
    /// A Instance With Template Data From An RFDevice.
    /// </summary>
    /// <seealso cref="RFDevice" />
    public sealed class RFDeviceTemplate
    {

        /// <summary>
        /// The device
        /// </summary>
        private readonly RFDevice device = null;


        /// <summary>
        /// Performs an implicit conversion from <see cref="RFDeviceTemplate"/> to <see cref="RFDevice"/>.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator RFDevice( RFDeviceTemplate template )
        {
            return template.device.Clone();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceTemplate"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <remarks>Copy only the important properties for an template to this new instance.</remarks>
        public RFDeviceTemplate( RFDevice device )
        {
            this.device = device.Clone();
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.device.Name;

    } // end sealed public class RFDeviceTemplate


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{RFDeviceTemplate}" />
    public sealed class RFDeviceTemplateCollection : ObservableCollection<RFDeviceTemplate>
    {
    } // end sealed public class RFDeviceTemplateCollection
}
