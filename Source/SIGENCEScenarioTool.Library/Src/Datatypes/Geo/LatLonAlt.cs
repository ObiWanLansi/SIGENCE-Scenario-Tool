using GMap.NET;
using NetTopologySuite.Geometries;

namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    public struct LatLonAlt
    {
        /// <summary>
        /// The lat
        /// </summary>
        public readonly double Lat;

        /// <summary>
        /// The lon
        /// </summary>
        public readonly double Lon;

        /// <summary>
        /// The alt
        /// </summary>
        public readonly int Alt;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="LatLonAlt"/> struct.
        /// </summary>
        /// <param name="Lat">The lat.</param>
        /// <param name="Lon">The lon.</param>
        /// <param name="Alt">The alt.</param>
        public LatLonAlt(double Lat, double Lon, int Alt)
        {
            this.Lat = Lat;
            this.Lon = Lon;
            this.Alt = Alt;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Converts to pointlatlng.
        /// </summary>
        /// <returns></returns>
        public PointLatLng ToPointLatLng()
        {
            return new PointLatLng(Lat, Lon);
        }


        /// <summary>
        /// Distances the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public double Distance(LatLonAlt other)
        {
            Point p1 = new Point(Lon, Lat);
            Point p2 = new Point(other.Lon, other.Lat);

            return p1.Distance(p2);
        }

    } // end public struct LatLonAlt
}
