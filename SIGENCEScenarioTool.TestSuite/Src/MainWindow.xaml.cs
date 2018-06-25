using System.Windows;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.TestSuite
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the Click event of the Button_NewScenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_NewScenario_Click(object sender, RoutedEventArgs e)
        {
            new Windows.MainWindow.MainWindow().ShowDialog();
        }


        /// <summary>
        /// Handles the Click event of the Button_LoadScenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_LoadScenario_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Windows.MainWindow.MainWindow();

            dlg.OpenFile(@"C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Examples\TestScenario.stf");
            dlg.ShowDialog();
        }


        /// <summary>
        /// Handles the Click event of the Button_FindScenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_FindScenario_Click(object sender, RoutedEventArgs e)
        {
            //Dirty Hack ..

            MB.Information("There Is Only One Scenario,\nso we load it!");

            foreach (var w in App.Current.Windows)
            {
                if (w is ScenarioWindow)
                {
                    (w as ScenarioWindow).DisplayScenario();
                }
            }
        }

    } // end public partial class MainWindow 
}
