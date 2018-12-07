using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GeoAPI.Geometries;

using GMap.NET;

using NetTopologySuite.Geometries;
using NetTopologySuite.IO;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public static class GeoHelper
    {
        /// <summary>
        /// The wkbreader
        /// </summary>
        private static readonly WKBReader wkbreader = new WKBReader();

        /// <summary>
        /// The wkbwriter
        /// </summary>
        private static readonly WKBWriter wkbwriter = new WKBWriter();

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The germany centerpoint
        /// </summary>
        public static readonly Point GERMANY_CENTERPOINT = new Point( 51.133333333333, 10.416666666667 );

        /// <summary>
        /// The portanigra
        /// </summary>
        public static readonly Point PORTANIGRA = new Point( 49.759652, 6.643998 );

        /// <summary>
        /// The kaiserthermen
        /// </summary>
        public static readonly Point KAISERTHERMEN = new Point( 49.749856, 6.641915 );

        /// <summary>
        /// The barbarathermen
        /// </summary>
        public static readonly Point BARBARATHERMEN = new Point( 49.750309, 6.630350 );

        /// <summary>
        /// The amphitheater
        /// </summary>
        public static readonly Point AMPHITHEATER = new Point( 49.748011, 6.649092 );

        /// <summary>
        /// The römerbrücke
        /// </summary>
        public static readonly Point RÖMERBRÜCKE = new Point( 49.751893, 6.626421 );

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Strings to geometry.
        /// </summary>
        /// <param name="strWKBAsString">The string WKB as string.</param>
        /// <returns></returns>
        public static IGeometry StringToGeometry( string strWKBAsString )
        {
            if(strWKBAsString == null || strWKBAsString.Trim().Length == 0)
            {
                return null;
            }

            int iCount = strWKBAsString.Length >> 1;

            byte[] baData = new byte[iCount];

            using(StringReader sr = new StringReader( strWKBAsString ))
            {
                for(int i = 0 ; i < iCount ; i++)
                {
                    baData[i] = Convert.ToByte( new string( new[] { (char)sr.Read(), (char)sr.Read() } ), 16 );
                }
            }

            return wkbreader.Read( baData );
        }


        /// <summary>
        /// Geometries to string.
        /// </summary>
        /// <param name="geo">The geo.</param>
        /// <returns></returns>
        public static string GeometryToString( IGeometry geo )
        {
            // Kann ja passieren, ist ja auch nicht weiter schlimm, liefern wir halt eben auch null zurück.
            if(geo == null)
            {
                return null;
            }

            byte[] baData = wkbwriter.Write( geo );

            StringBuilder sb = new StringBuilder( baData.Length << 1 );

            foreach(byte b in baData)
            {
                sb.AppendFormat( "{0:X2}", b );
            }

            return sb.ToString();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the polygon.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Es müssen mindestens drei Punkte übergeben werden!</exception>
        public static Polygon CreatePolygon( params Point[] points )
        {
            if(points == null || points.Length < 3)
            {
                throw new ArgumentException( "Es müssen mindestens drei Punkte übergeben werden!" );
            }

            List<Coordinate> lPoints = new List<Coordinate>( points.Length + 1 );

            // Die Koordinaten der Punkte können in die Liste kopieren.
            lPoints.AddRange( points.Select( p => p.Coordinate ) );

            // Damit es aber auch wirklich ein Polygon und nicht nur eine Line ist müssen wir den ersten Punkten noch hinten drann hängen damit es auch geschlossen wird.
            lPoints.Add( points[0].Coordinate );

            // Aus dem LinearRing können wir das Polygon erzeugen.
            return new Polygon( new LinearRing( lPoints.ToArray() ) );
        }


        /// <summary>
        /// Coordinates to point lat LNG.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static PointLatLng CoordinateToPointLatLng( Coordinate c )
        {
            return new PointLatLng( c.Y, c.X );
        }

    } // end public static class GeoHelper
}
