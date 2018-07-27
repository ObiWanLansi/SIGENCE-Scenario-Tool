using SIGENCEScenarioTool.Models.Database.GeoDb;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Jumps to geo node.
        /// </summary>
        /// <param name="gn">The gn.</param>
        private void JumpToGeoNode(GeoNode gn)
        {
            mcMapControl.Position = gn.Position;
            mcMapControl.Zoom = settings.MapZoomLevel;

            tiMap.IsSelected = true;
        }

    } // end public partial class MainWindow
}
