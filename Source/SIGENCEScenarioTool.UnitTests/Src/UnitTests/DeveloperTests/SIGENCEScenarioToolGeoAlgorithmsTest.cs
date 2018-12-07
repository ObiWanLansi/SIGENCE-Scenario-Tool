using log4net;

using Microsoft.SqlServer.Types;

using NetTopologySuite.Geometries;

using NUnit.Framework;

using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.UnitTest.Attributes;

namespace SIGENCEScenarioTool.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    [SIGENCEScenarioToolTestAction]
    internal sealed class SIGENCEScenarioToolGeoAlgorithmsTest
    {
        /// <summary>
        /// Logger zum Ausgeben der Protokollierung.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger( typeof( SIGENCEScenarioToolGeoAlgorithmsTest ) );

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// The wg S84
        /// </summary>
        private const int WGS84 = 4326;

        /// <summary>
        /// Gets the geometry.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        private static SqlGeometry GetSqlGeometry( Point p )
        {
            return SqlGeometry.Point( p.X, p.Y, WGS84 );
        }


        /// <summary>
        /// Test000s the distance.
        /// </summary>
        [Test, Category( "GeoAlgorithms" ), SIGENCEScenarioToolTestCase( "f66d4f65-bafc-401d-bb2b-7c75fe6efc17" ), Description( "Tests some geo algorithms." )]
        public void Test000_Distance()
        {
            SIGENCEScenarioToolTestCaseHelper.ShowTestCaseInformation();

            //-----------------------------------------------------------------

            var p1 = GetSqlGeometry( GeoHelper.PORTANIGRA );
            var p2 = GetSqlGeometry( GeoHelper.AMPHITHEATER );

            var dist = p1.STDistance( p2 );
            Log.InfoFormat( "Port Nigra -> Amphitheater :{0}", dist * 100 );

            double result = GeoHelper.PORTANIGRA.Distance( GeoHelper.AMPHITHEATER );
            Log.InfoFormat( "Port Nigra -> Amphitheater :{0}", result * 100 );

            var x = NetTopologySuite.Operation.Distance.DistanceOp.Distance( GeoHelper.PORTANIGRA, GeoHelper.AMPHITHEATER );
            Log.InfoFormat( "Port Nigra -> Amphitheater :{0}", x * 100 );
        }

    } // end sealed class SIGENCEScenarioToolGeoAlgorithmsTest
}
