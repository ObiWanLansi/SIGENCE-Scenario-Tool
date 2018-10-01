using System.Windows.Controls;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels
{
    /// <summary>
    /// Interaktionslogik für GeneralSettings.xaml
    /// </summary>
    public partial class GeneralSettings : ISettingsControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSettings"/> class.
        /// </summary>
        public GeneralSettings()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets the Image.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            return ( Image ) Resources ["NUT_AND_BOLT"];
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "General Settings";
        }

    } // end public partial class GeneralSettings
}
