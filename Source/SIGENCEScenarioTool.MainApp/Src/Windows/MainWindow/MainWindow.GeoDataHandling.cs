using System.Windows;

using GMap.NET.WindowsPresentation;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Markers;



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
        private void JumpToGeoNode( GeoNode gn )
        {
            mcMapControl.Position = gn.Position;
            mcMapControl.Zoom = settings.MapZoomLevel;

            CreateGeoNodeMarker( gn );

            tiMap.IsSelected = true;
        }


        /// <summary>
        /// Creates the geo node marker.
        /// </summary>
        /// <param name="gn">The gn.</param>
        private void CreateGeoNodeMarker( GeoNode gn )
        {
            GMapMarker Marker = new GMapMarker( gn.Position )
            {
                Offset = new Point( -10 , -10 ) ,
                ZIndex = int.MaxValue ,
                Shape = new GeoNodeMarker( gn ) ,
                Tag = gn
            };

            mcMapControl.Markers.Add( Marker );
        }

    } // end public partial class MainWindow
}
