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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_ChartingTest_Click( object sender , RoutedEventArgs e )
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
