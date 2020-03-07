using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using SIGENCEScenarioTool.Dialogs.Settings.Panels;



namespace SIGENCEScenarioTool.Dialogs.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SettingsPanelViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public Image Image { get; private set; }

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
        public UserControl Control { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; } = true;


        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPanelViewModel" /> class.
        /// </summary>
        /// <param name="uc">The uc.</param>
        /// <param name="sc">The sc.</param>
        public SettingsPanelViewModel(UserControl uc, ISettingsControl sc)
        {
            this.Control = uc;
            this.Name = sc.GetName();
            this.Image = sc.GetImage();
        }

    } // end public sealed class SettingsPanelViewModel



    /// <summary>
    /// Interaktionslogik für SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : INotifyPropertyChanged
    {

        /// <summary>
        /// The panels
        /// </summary>
        public ObservableCollection<SettingsPanelViewModel> Panels { get; set; }



        /// <summary>
        /// The uc selected panel
        /// </summary>
        private UserControl ucSelectedPanel = null;// new GeneralSettings();
        /// <summary>
        /// Gets or sets the selected panel.
        /// </summary>
        /// <value>
        /// The selected panel.
        /// </value>
        public UserControl SelectedPanel
        {
            get => this.ucSelectedPanel;
            set
            {
                this.ucSelectedPanel = value;
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

            this.DataContext = this;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the panels.
        /// </summary>
        private void InitPanels()
        {
            var _gs = new GeneralSettings();
            var _ns = new NetworkSettings();
            var _ds = new DatabaseSettings();
            var _is = new ImportSettings();
            var _es = new ExportSettings();

            this.Panels = new ObservableCollection<SettingsPanelViewModel>
            {
                new SettingsPanelViewModel(_gs,_gs),
                new SettingsPanelViewModel(_ns,_ns) { IsEnabled = false },
                new SettingsPanelViewModel(_ds,_ds) { IsEnabled = false },
                new SettingsPanelViewModel(_is,_is) { IsEnabled = false },
                new SettingsPanelViewModel(_es,_es) { IsEnabled = false }
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
            this.DialogResult = true;
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
            this.DialogResult = false;
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
            this.SelectedPanel = ((sender as ListBox).SelectedItem as SettingsPanelViewModel).Control;
            e.Handled = true;
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
        private void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

    } // end public partial class SettingsDialog 
}
