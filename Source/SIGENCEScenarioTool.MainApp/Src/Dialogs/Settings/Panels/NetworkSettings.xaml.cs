using System.Windows.Controls;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels
{
    /// <summary>
    /// Interaktionslogik für NetworkSettings.xaml
    /// </summary>
    public partial class NetworkSettings : ISettingsControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkSettings"/> class.
        /// </summary>
        public NetworkSettings()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets the Image.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            return ( Image ) Resources ["NETWORK"];
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "Network Settings";
        }

    } // end public partial class NetworkSettings
}
