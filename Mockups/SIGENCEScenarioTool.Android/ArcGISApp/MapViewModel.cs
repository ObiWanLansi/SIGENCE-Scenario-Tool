using System.ComponentModel;
using System.Runtime.CompilerServices;

using Esri.ArcGISRuntime.Mapping;

namespace ArcGISApp
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapViewModel"/> class.
        /// </summary>
        public MapViewModel()
        {
            //this._map.InitialViewpoint = new Viewpoint(new MapPoint(47.668532, 9.387443, SpatialReferences.Wgs84));
        }

        /// <summary>
        /// The map
        /// </summary>
        private Map _map = new Map(Basemap.CreateTopographic());

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        /// <value>
        /// The map.
        /// </value>
        public Map Map
        {
            get { return this._map; }
            set
            {
                this._map = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// To be added.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
