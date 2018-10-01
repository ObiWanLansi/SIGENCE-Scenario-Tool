using System.Windows.Controls;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels
{
    /// <summary>
    /// Interaktionslogik für ImportSettings.xaml
    /// </summary>
    public partial class ImportSettings : ISettingsControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportSettings"/> class.
        /// </summary>
        public ImportSettings()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets the Image.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            return ( Image ) Resources ["IMPORT"];
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "Import Settings";
        }

    } // end public partial class ImportSettings
}
