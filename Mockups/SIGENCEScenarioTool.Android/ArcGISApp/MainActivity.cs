using System.ComponentModel;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

using Esri.ArcGISRuntime.Geometry;
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
        /// <summary>
        /// The map view model
        /// </summary>
        private readonly MapViewModel _mapViewModel = new MapViewModel();

        /// <summary>
        /// The map view
        /// </summary>
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

            Button bGoHome = FindViewById<Button>(Resource.Id.GoHome);
            //bGoHome.SetOnClickListener(new IOnC            { });
            bGoHome.Click += GoHome_Click;
        }


        /// <summary>
        /// Handles the Click event of the GoHome control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void GoHome_Click(object sender, System.EventArgs e)
        {
            this._mapView.SetViewpointScaleAsync(100000);
            this._mapView.SetViewpointCenterAsync(new MapPoint(9.387443, 47.668532, SpatialReferences.Wgs84));
            //Toast.MakeText(this, "Go Home", ToastLength.Short).Show();
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

    } // end public class MainActivity
}