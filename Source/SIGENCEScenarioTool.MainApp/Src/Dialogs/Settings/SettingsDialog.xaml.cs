using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using SIGENCEScenarioTool.Dialogs.Settings.Panels;



namespace SIGENCEScenarioTool.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class SettingsPanelViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public Image Image { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPanelViewModel"/> class.
        /// </summary>
        /// <param name="sc">The sc.</param>
        public SettingsPanelViewModel(ISettingsControl sc)
        {
            Name = sc.GetName();
            Image = sc.GetImage();
        }

    } // end public sealed class SettingsPanel



    /// <summary>
    /// Interaktionslogik für SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window, INotifyPropertyChanged
    {

        /// <summary>
        /// The panels
        /// </summary>
        public ObservableCollection<SettingsPanelViewModel> Panels { get; set; }



        /// <summary>
        /// The uc selected panel
        /// </summary>
        private UserControl ucSelectedPanel = new GeneralSettings();
        /// <summary>
        /// Gets or sets the selected panel.
        /// </summary>
        /// <value>
        /// The selected panel.
        /// </value>
        public UserControl SelectedPanel
        {
            get
            {
                return ucSelectedPanel;
            }
            set
            {
                ucSelectedPanel = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsDialog"/> class.
        /// </summary>
        public SettingsDialog()
        {
            InitializeComponent();

            InitPanels();

            DataContext = this;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the panels.
        /// </summary>
        private void InitPanels()
        {
            Panels = new ObservableCollection<SettingsPanelViewModel>
            {
                new SettingsPanelViewModel(new GeneralSettings()),
                new SettingsPanelViewModel(new NetworkSettings()),
                new SettingsPanelViewModel(new ImportSettings()),
                new SettingsPanelViewModel(new ExportSettings())
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the Button_Accept control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Properties.Settings.Default.Save();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_Cancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Properties.Settings.Default.Reload();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the SelectionChanged event of the ListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Switch Panel
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        protected void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

    } // end public partial class SettingsDialog 
}
