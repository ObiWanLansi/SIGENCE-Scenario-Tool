using System.Linq;
using System.Windows;

using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Models;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Handles the Click event of the MenuItem_ChartingTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_ChartingTest_Click(object sender, RoutedEventArgs e)
        {
            ChartingWindow cw = new ChartingWindow(new RFDeviceList(from device in RFDevicesCollection select device.RFDevice));
            cw.ShowDialog();
            cw = null;
        }

    } // end public partial class MainWindow
}
