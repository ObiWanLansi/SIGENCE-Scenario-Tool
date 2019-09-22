using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using NetTopologySuite.Geometries;



namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class TerrainModel
    {
        /// <summary>
        /// The l terrain model
        /// </summary>
        private readonly List<LatLonAlt> lTerrainModel = new List<LatLonAlt>();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public FileInfo TerrainFile { get; private set; }

        /// <summary>
        /// Gets the x minimum.
        /// </summary>
        /// <value>
        /// The x minimum.
        /// </value>
        public double XMin { get; private set; }

        /// <summary>
        /// Gets the y minimum.
        /// </summary>
        /// <value>
        /// The y minimum.
        /// </value>
        public double YMin { get; private set; }

        /// <summary>
        /// Gets the x maximum.
        /// </summary>
        /// <value>
        /// The x maximum.
        /// </value>
        public double XMax { get; private set; }

        /// <summary>
        /// Gets the y maximum.
        /// </summary>
        /// <value>
        /// The y maximum.
        /// </value>
        public double YMax { get; private set; }

        ///// <summary>
        ///// Gets the envelope.
        ///// </summary>
        ///// <value>
        ///// The envelope.
        ///// </value>
        //public Envelope Envelope { get; private set; }

        /// <summary>
        /// Gets the point count.
        /// </summary>
        /// <value>
        /// The point count.
        /// </value>
        public int PointCount
        {
            get { return lTerrainModel.Count; }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the highest points.
        /// </summary>
        /// <param name="envelope">The envelope.</param>
        /// <param name="iPointCount">The i point count.</param>
        /// <param name="iPointDeltaInMeters">The i point delta in meters.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">envelope</exception>
        public List<LatLonAlt> GetHighestPoints(Envelope envelope, int iPointCount, int iPointDeltaInMeters)
        {
            if (envelope == null)
            {
                throw new ArgumentNullException("envelope");
            }

            //Polygon envelope = pPolygon.GetWGS84();

            List<LatLonAlt> lMapPointsInEvelope = new List<LatLonAlt>(1024);

            lMapPointsInEvelope.AddRange(lTerrainModel.Where(mp => envelope.Contains(mp.Lon, mp.Lat)));

            // Absteigent sortieren
            lMapPointsInEvelope.Sort((x, y) => { return x.Alt.CompareTo(y.Alt) * -1; });

            List<LatLonAlt> lHighestPoints = new List<LatLonAlt>(iPointCount);

            int iCounter = 0;

            foreach (LatLonAlt mpCurrent in lMapPointsInEvelope)
            {
                bool bAbstandGroßGenug = true;

                foreach (LatLonAlt mp in lHighestPoints)
                {
                    //double dAbstand = GeometryEngine.GeodesicDistance(mpCurrent, mp, LinearUnits.Meters);

                    //if (dAbstand < iPointDeltaInMeters)
                    //{
                    //    bAbstandGroßGenug = false;
                    //    break;
                    //}
                }

                if (bAbstandGroßGenug == true)
                {
                    lHighestPoints.Add(mpCurrent);

                    if (++iCounter == iPointCount)
                    {
                        break;
                    }
                }
            }

            return lHighestPoints;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            TerrainFile = null;

            lTerrainModel.Clear();

            XMin = double.MaxValue;
            YMin = double.MaxValue;
            XMax = double.MinValue;
            YMax = double.MinValue;

            //Envelope = null;
        }


        /// <summary>
        /// Parses the line.
        /// </summary>
        /// <param name="strLine">The string line.</param>
        private void ParseLine(string strLine)
        {
            string[] fields = strLine.Split(' ');

            if (fields.Length != 3)
            {
                return;
            }

            double x = double.Parse(fields[0].Replace('.', ','));
            double y = double.Parse(fields[1].Replace('.', ','));
            int z = int.Parse(fields[2]);

            lock (lTerrainModel)
            {
                if (x < XMin)
                {
                    XMin = x;
                }

                if (x > XMax)
                {
                    XMax = x;
                }

                if (y < YMin)
                {
                    YMin = y;
                }

                if (y > YMax)
                {
                    YMax = y;
                }

                lTerrainModel.Add(new LatLonAlt(y, x, z));
            }
        }


        /// <summary>
        /// Loads the xyz file.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        public void LoadXYZFile(string strFilename)
        {
            Reset();

            TerrainFile = new FileInfo(strFilename);

            string[] strLines = File.ReadAllLines(strFilename);

            Parallel.ForEach(strLines, (strLine) =>
            {
                ParseLine(strLine);
            });

            //foreach (string strLine in strLines)
            //{
            //    ParseLine(strLine);
            //}


            //Envelope = new Envelope(XMin, XMax, YMin, YMax);
        }

    } // end sealed public class TerrainModel
}
