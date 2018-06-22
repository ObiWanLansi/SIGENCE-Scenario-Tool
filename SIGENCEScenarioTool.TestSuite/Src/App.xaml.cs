using System.Windows;



namespace SIGENCEScenarioTool.TestSuite
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup( object sender , StartupEventArgs e )
        {
            MainWindow mw = new MainWindow();
            mw.Show();

            ScenarioWindow sw = new ScenarioWindow();
            sw.Top = mw.Top;
            sw.Left = mw.Left + mw.Width + 10;

            sw.Show();
        }
    } // end public partial class App 
}
