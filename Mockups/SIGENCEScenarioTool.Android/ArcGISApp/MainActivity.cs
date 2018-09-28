using System.ComponentModel;

using Android.App;
using Android.Content.PM;
using Android.OS;

using Esri.ArcGISRuntime.UI.Controls;

/// <summary>
/// 
/// </summary>
namespace ArcGISApp
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Android.App.Activity" />
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        private readonly MapViewModel _mapViewModel = new MapViewModel();

        private MapView _mapView;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set the view from the "Main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get MapView from the view and assign map from view-model
            this._mapView = FindViewById<MapView>(Resource.Id.MyMapView);
            this._mapView.Map = this._mapViewModel.Map;

            // Listen for changes on the view model
            this._mapViewModel.PropertyChanged += MapViewModel_PropertyChanged;

            //this._mapView.SetViewpointCenterAsync(new MapPoint(47.668532, 9.387443, SpatialReferences.Wgs84));
            //this._mapView.SetViewpoint(new Viewpoint(new MapPoint(47.668532, 9.387443, SpatialReferences.Wgs84)));
        }

        /// <summary>
        /// Handles the PropertyChanged event of the MapViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void MapViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Update the map view with the view model's new map
            if (e.PropertyName == "Map" && this._mapView != null)
            {
                this._mapView.Map = this._mapViewModel.Map;
            }
        }
    }
}