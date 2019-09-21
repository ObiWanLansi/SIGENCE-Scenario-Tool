using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;



namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    sealed public class TerrainModel
    {
        /// <summary>
        /// The l terrain model
        /// </summary>
        private readonly List<LatLonAlt> lTerrainModel = new List<LatLonAlt>();


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
        ///// 
        ///// </summary>
        //public Envelope Envelope { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pPolygon"></param>
        ///// <param name="iPointCount"></param>
        ///// <param name="iPointDeltaInMeters"></param>
        ///// <returns></returns>
        //public List<PointLatLng> GetHighestPoints(Polygon pPolygon, int iPointCount, int iPointDeltaInMeters)
        //{
        //    if (pPolygon == null)
        //    {
        //        throw new ArgumentNullException("pPolygon");
        //    }

        //    Polygon envelope = pPolygon.GetWGS84();

        //    List<PointLatLng> lMapPointsInEvelope = new List<PointLatLng>(1024);

        //    lMapPointsInEvelope.AddRange(lTerrainModel.Where(mp => GeometryEngine.Contains(envelope, mp)));

        //    // Absteigent sortieren
        //    lMapPointsInEvelope.Sort((x, y) => { return x.Z.CompareTo(y.Z) * -1; });

        //    List<PointLatLng> lHighestPoints = new List<PointLatLng>(iPointCount);

        //    int iCounter = 0;

        //    foreach (MapPoint mpCurrent in lMapPointsInEvelope)
        //    {
        //        bool bAbstandGroßGenug = true;

        //        foreach (MapPoint mp in lHighestPoints)
        //        {
        //            double dAbstand = GeometryEngine.GeodesicDistance(mpCurrent, mp, LinearUnits.Meters);

        //            if (dAbstand < iPointDeltaInMeters)
        //            {
        //                bAbstandGroßGenug = false;
        //                break;
        //            }
        //        }

        //        if (bAbstandGroßGenug == true)
        //        {
        //            lHighestPoints.Add(mpCurrent);

        //            if (++iCounter == iPointCount)
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    return lHighestPoints;
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            //Source = null;

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


            //Envelope = new Envelope(XMin, YMin, XMax, YMax, SpatialReferences.Wgs84);

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="strInputFile"></param>
        ///// <param name="strOuputFile"></param>
        //static public void ConvertGeoTifToXyz(string strInputFile, string strOuputFile)
        //{
        //    // Exceptions werden weitergeschmissen ...
        //    GdalTranslateUtil.ConvertGeoTifToXYZ(strInputFile, strOuputFile);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="strInputFile"></param>
        //static public void ConvertGeoTifToXyz(string strInputFile)
        //{
        //    ConvertGeoTifToXyz(strInputFile, strInputFile + ".xyz");
        //}

    } // end sealed public class TerrainModel
}
