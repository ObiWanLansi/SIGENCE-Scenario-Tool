namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    struct LatLonAlt
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

    } // end struct LatLonAlt
}
