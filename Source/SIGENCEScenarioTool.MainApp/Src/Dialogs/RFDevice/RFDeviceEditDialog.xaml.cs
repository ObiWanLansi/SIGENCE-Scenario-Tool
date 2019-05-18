using System.Windows;

using SIGENCEScenarioTool.Models.RxTxTypes;
using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Dialogs.RFDevice
{
    /// <summary>
    /// Interaktionslogik für RFDeviceEditDialog.xaml
    /// </summary>
    public partial class RFDeviceEditDialog : Window
    {

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public RFDeviceViewModel Device { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceEditDialog"/> class.
        /// </summary>
        public RFDeviceEditDialog(Models.RFDevice device)
        {
            InitializeComponent();

            // Use An Copy If The User Discard The Changes ...
            this.Device = new RFDeviceViewModel(null, device.Clone());
            
            this.dgcbcRxTxType.ItemsSource = RxTxTypes.Values;

            this.DataContext = this;
        }

    } // end public partial class RFDeviceEditDialog
}
