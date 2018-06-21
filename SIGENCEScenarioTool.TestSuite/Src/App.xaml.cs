using System.Windows;



namespace SIGENCEScenarioTool.TestSuite
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
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
