using System.Windows;

using SIGENCEScenarioTool.Dialogs;



namespace SIGENCEScenarioTool.Windows
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
            ChartingWindow cw = new ChartingWindow
            {
                RFDevicesCollection = this.RFDevicesCollection
            };
            cw.InitChart();
            cw.ShowDialog();
            cw = null;
        }

    } // end public partial class MainWindow
}
