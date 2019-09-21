using System;
using System.Data;

using NetTopologySuite.Geometries;

using SIGENCEScenarioTool.Tools;




namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IDataReaderExtension
    {
        /// <summary>
        /// Gets the string or null.
        /// </summary>
        /// <param name="dbResult">The database result.</param>
        /// <param name="iColumnIndex">Index of the i column.</param>
        /// <returns></returns>
        public static string GetStringOrNull(this IDataReader dbResult, int iColumnIndex)
        {
            if (dbResult.IsDBNull(iColumnIndex) == false)
            {
                return dbResult.GetString(iColumnIndex);
            }

            return null;
        }


        /// <summary>
        /// Gets the int32 or null.
        /// </summary>
        /// <param name="dbResult">The database result.</param>
        /// <param name="iColumnIndex">Index of the i column.</param>
        /// <returns></returns>
        public static int? GetInt32OrNull(this IDataReader dbResult, int iColumnIndex)
        {
            if (dbResult.IsDBNull(iColumnIndex) == false)
            {
                return dbResult.GetInt32(iColumnIndex);
            }

            return null;
        }


        /// <summary>
        /// Gets the int64 or null.
        /// </summary>
        /// <param name="dbResult">The database result.</param>
        /// <param name="iColumnIndex">Index of the i column.</param>
        /// <returns></returns>
        public static long? GetInt64OrNull(this IDataReader dbResult, int iColumnIndex)
        {
            if (dbResult.IsDBNull(iColumnIndex) == false)
            {
                return dbResult.GetInt64(iColumnIndex);
            }

            return null;
        }


        /// <summary>
        /// Gets the date time or null.
        /// </summary>
        /// <param name="dbResult">The database result.</param>
        /// <param name="iColumnIndex">Index of the i column.</param>
        /// <returns></returns>
        public static DateTime? GetDateTimeOrNull(this IDataReader dbResult, int iColumnIndex)
        {
            if (dbResult.IsDBNull(iColumnIndex) == false)
            {
                return dbResult.GetDateTime(iColumnIndex);
            }

            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbResult"></param>
        /// <param name="iColumnIndex"></param>
        /// <returns></returns>
        public static Geometry GetGeometryFromWKB(this IDataReader dbResult, int iColumnIndex)
        {
            if (dbResult.IsDBNull(iColumnIndex) == false)
            {
                string strWKB = dbResult.GetString(iColumnIndex);

                if (strWKB.IsNotEmpty())
                {
                    return strWKB.ToGeometry();
                }
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbResult"></param>
        /// <param name="iColumnIndex"></param>
        /// <returns></returns>
        public static MultiPolygon GetMultiPolygonFromWKB(this IDataReader dbResult, int iColumnIndex)
        {
            return (MultiPolygon)GetGeometryFromWKB(dbResult, iColumnIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbResult"></param>
        /// <param name="iColumnIndex"></param>
        /// <returns></returns>
        public static Polygon GetPolygonFromWKB(this IDataReader dbResult, int iColumnIndex)
        {
            return (Polygon)GetGeometryFromWKB(dbResult, iColumnIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbResult"></param>
        /// <param name="iColumnIndex"></param>
        /// <returns></returns>
        public static LineString GetLineStringFromWKB(this IDataReader dbResult, int iColumnIndex)
        {
            return (LineString)GetGeometryFromWKB(dbResult, iColumnIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbResult"></param>
        /// <param name="iColumnIndex"></param>
        /// <returns></returns>
        public static Point GetPointFromWKB(this IDataReader dbResult, int iColumnIndex)
        {
            return (Point)GetGeometryFromWKB(dbResult, iColumnIndex);
        }

    } // end static public class IDataReaderExtension
}
