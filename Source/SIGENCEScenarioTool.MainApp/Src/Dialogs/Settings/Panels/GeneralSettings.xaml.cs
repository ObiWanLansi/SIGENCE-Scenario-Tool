using System.Windows.Controls;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels
{
    //internal class GeneralSettingsViewModel
    //{


    //    public string MapProvider
    //    {
    //        get; set;
    //    }

    //    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    //    /// <summary>
    //    /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
    //    /// </summary>
    //    public event PropertyChangedEventHandler PropertyChanged;


    //    /// <summary>
    //    /// Fires the property changed.
    //    /// </summary>
    //    /// <param name="strPropertyName">Name of the string property.</param>
    //    private void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
    //    {
    //        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( strPropertyName ) );
    //    }
    //}

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
            return (Image)this.Resources["NUT_AND_BOLT"];
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
