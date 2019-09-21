using System;
using System.Collections.Generic;

using GMap.NET;



namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    sealed public class TerrainModel
    {
        /// <summary>
        /// 
        /// </summary>
        private List<PointLatLng> lTerrainModel = new List<PointLatLng>();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double XMin { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double YMin { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double XMax { get; private set; }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        private void Reset()
        {
            Source = null;

            lTerrainModel.Clear();

            XMin = double.MaxValue;
            YMin = double.MaxValue;
            XMax = double.MinValue;
            YMax = double.MinValue;

            //Envelope = null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilename"></param>
        public void LoadXYZFile(string strFilename)
        {
            Reset();

            Source = strFilename;

            //foreach (string strLine in File.ReadLines(strFilename))
            //{
            //    string[] fields = strLine.Split(' ');

            //    if (fields.Length != 3)
            //    {
            //        continue;
            //    }

            //    double x = double.Parse(fields[0].Replace('.', ','));
            //    double y = double.Parse(fields[1].Replace('.', ','));
            //    int z = int.Parse(fields[2]);

            //    if (x < XMin)
            //    {
            //        XMin = x;
            //    }

            //    if (x > XMax)
            //    {
            //        XMax = x;
            //    }

            //    if (y < YMin)
            //    {
            //        YMin = y;
            //    }

            //    if (y > YMax)
            //    {
            //        YMax = y;
            //    }

            //    lTerrainModel.Add(new PointLatLng(x, y, z, SpatialReferences.Wgs84));
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
