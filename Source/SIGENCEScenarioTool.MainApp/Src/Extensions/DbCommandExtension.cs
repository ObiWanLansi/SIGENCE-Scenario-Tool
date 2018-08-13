using System;
using System.Data.Common;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    static public class DbCommandExtension
    {

        /// <summary>
        /// Adds the nullable paramter.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="strParameterName">Name of the string parameter.</param>
        /// <param name="o">The o.</param>
        static public void SetNullableParamter(this DbCommand dbCommand, string strParameterName, object o) => dbCommand.Parameters[strParameterName].Value = o ?? DBNull.Value;


        /// <summary>
        /// Sets the nullable paramter.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="iParameterIndex">Index of the i parameter.</param>
        /// <param name="o">The o.</param>
        static public void SetNullableParamter(this DbCommand dbCommand, int iParameterIndex, object o) => dbCommand.Parameters[iParameterIndex].Value = o ?? DBNull.Value;


        /// <summary>
        /// Set alle Parameters to NULL.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        static public void ResetParameters(this DbCommand dbCommand)
        {
            for (int iParameter = 0; iParameter < dbCommand.Parameters.Count; iParameter++)
            {
                dbCommand.Parameters[iParameter].Value = DBNull.Value;
            }
        }

    } // end static public class DbCommandExtension
}
