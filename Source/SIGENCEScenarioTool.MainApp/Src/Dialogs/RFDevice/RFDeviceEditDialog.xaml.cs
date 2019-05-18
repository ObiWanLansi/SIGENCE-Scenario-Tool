using System.Windows;

using SIGENCEScenarioTool.ViewModels;

namespace SIGENCEScenarioTool.Dialogs.RFDevice
{
    /// <summary>
    /// Interaktionslogik für RFDeviceEditDialog.xaml
    /// </summary>
    public partial class RFDeviceEditDialog : Window
    {

        public RFDeviceViewModel Device { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceEditDialog"/> class.
        /// </summary>
        public RFDeviceEditDialog(Models.RFDevice device)
        {
            InitializeComponent();
        }

    } // end public partial class RFDeviceEditDialog
}
