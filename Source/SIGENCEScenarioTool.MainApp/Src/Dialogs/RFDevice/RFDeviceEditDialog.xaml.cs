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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the Button_Accept control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            //Close();
        }

    } // end public partial class RFDeviceEditDialog
}
