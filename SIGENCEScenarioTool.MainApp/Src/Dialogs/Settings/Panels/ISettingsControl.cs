using System.Windows.Controls;



namespace SIGENCEScenarioTool.Dialogs.Settings.Panels
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingsControl
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Gets the bitmap.
        /// </summary>
        /// <returns></returns>
        Image GetImage();

    } // end public interface ISettingsControl
}
