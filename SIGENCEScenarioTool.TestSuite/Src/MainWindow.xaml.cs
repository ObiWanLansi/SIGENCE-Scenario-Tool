using System.Windows;



namespace SIGENCEScenarioTool.TestSuite
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_NewScenario_Click( object sender , RoutedEventArgs e )
        {
            new Windows.MainWindow.MainWindow().ShowDialog();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_LoadScenario_Click( object sender , RoutedEventArgs e )
        {
            var dlg = new Windows.MainWindow.MainWindow();

            dlg.OpenFile( @"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.stf" );
            dlg.ShowDialog();
        }

    } // end public partial class MainWindow 
}
