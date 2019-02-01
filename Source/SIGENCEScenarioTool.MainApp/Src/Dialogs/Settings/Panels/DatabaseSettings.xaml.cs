using System.Windows.Controls;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels
{
    /// <summary>
    /// Interaktionslogik für NetworkSettings.xaml
    /// </summary>
    public partial class DatabaseSettings : ISettingsControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSettings"/> class.
        /// </summary>
        public DatabaseSettings()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets the Image.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            return (Image)this.Resources["DATABASE"];
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "Database Settings";
        }

    } // end public partial class DatabaseSettings
}
