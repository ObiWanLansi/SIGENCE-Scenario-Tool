using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IDbConnectionExtension
    {
        /// <summary>
        /// Selects the specified db connection.
        /// </summary>
        /// <param name="dbConnection">The db connection.</param>
        /// <param name="strFormat">The STR format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static IEnumerable<IDataReader> Select(this IDbConnection dbConnection, string strFormat, params object[] args)
        {
            string strSQLStatement = args != null && args.Length > 0 ? string.Format(strFormat, args) : strFormat;

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSQLStatement;
                dbCommand.CommandTimeout = 0;

                using (IDataReader dbResult = dbCommand.ExecuteReader())
                {
                    while (dbResult.Read())
                    {
                        yield return dbResult;
                    }

                    dbResult.Close();
                }
            }
        }


        /// <summary>
        /// Selects the specified db connection.
        /// </summary>
        /// <param name="dbConnection">Die aktuelle Datenbankverbindung.</param>
        /// <param name="strSelectStatement">The STR select statement.</param>
        /// <returns></returns>
        public static IEnumerable<IDataReader> Select(this IDbConnection dbConnection, string strSelectStatement)
        {
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSelectStatement;
                dbCommand.CommandTimeout = 0;

                using (IDataReader dbResult = dbCommand.ExecuteReader())
                {
                    while (dbResult.Read())
                    {
                        yield return dbResult;
                    }

                    dbResult.Close();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Exceutes the non query.
        /// </summary>
        /// <param name="dbConnection">The db connection.</param>
        /// <param name="strFormat">The STR format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this IDbConnection dbConnection, string strFormat, params object[] args)
        {
            string strSQLStatement = args != null && args.Length > 0 ? string.Format(strFormat, args) : strFormat;

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSQLStatement;

                return dbCommand.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="iTimeout">The i timeout.</param>
        /// <param name="bTransaction">if set to <c>true</c> [b transaction].</param>
        /// <param name="strFormat">The string format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this IDbConnection dbConnection, int iTimeout, bool bTransaction, string strFormat, params object[] args)
        {
            string strSQLStatement = args != null && args.Length > 0 ? string.Format(strFormat, args) : strFormat;

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                IDbTransaction dbTransaction = null;

                if (bTransaction == true)
                {
                    dbTransaction = dbConnection.BeginTransaction();
                    dbCommand.Transaction = dbTransaction;
                }

                dbCommand.CommandText = strSQLStatement;
                dbCommand.CommandTimeout = iTimeout;

                int iResult = -1;

                try
                {
                    iResult = dbCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // TODO: Hmmmm, das können wir so natürlich nicht stehen lassen ...
                    MB.Error(ex);
                }

                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }

                return iResult;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="strFormat">The string format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static object ExecuteScalar(this IDbConnection dbConnection, string strFormat, params object[] args) => ExecuteScalar(dbConnection, 0, strFormat, args);


        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="dbConnection">The db connection.</param>
        /// <param name="iTimeOut">The i time out.</param>
        /// <param name="strFormat">The STR format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static object ExecuteScalar(this IDbConnection dbConnection, int iTimeOut, string strFormat, params object[] args)
        {
            string strSQLStatement = args != null && args.Length > 0 ? string.Format(strFormat, args) : strFormat;

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSQLStatement;
                dbCommand.CommandTimeout = iTimeOut;

                return dbCommand.ExecuteScalar();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Liefert das Ergebnis eines Statements als SortedDictionary zurück.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="strSelectStatement">The string select statement.</param>
        /// <returns></returns>
        public static SortedDictionary<T1, T2> GetSortedDictionary<T1, T2>(this IDbConnection dbConnection, string strSelectStatement)
        {
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSelectStatement;

                using (IDataReader dbResult = dbCommand.ExecuteReader())
                {
                    SortedDictionary<T1, T2> sdResult = new SortedDictionary<T1, T2>();

                    while (dbResult.Read())
                    {
                        sdResult.Add((T1)dbResult.GetValue(0), (T2)dbResult.GetValue(1));
                    }

                    dbResult.Close();

                    return sdResult;
                }
            }
        }


        /// <summary>
        /// Gets the dictionary.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="strSelectStatement">The string select statement.</param>
        /// <returns></returns>
        public static Dictionary<T1, T2> GetDictionary<T1, T2>(this IDbConnection dbConnection, string strSelectStatement)
        {
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSelectStatement;

                using (IDataReader dbResult = dbCommand.ExecuteReader())
                {
                    Dictionary<T1, T2> sdResult = new Dictionary<T1, T2>();

                    while (dbResult.Read())
                    {
                        sdResult.Add((T1)dbResult.GetValue(0), (T2)dbResult.GetValue(1));
                    }

                    dbResult.Close();

                    return sdResult;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Closes if open.
        /// </summary>
        /// <param name="dbConnection">The db connection.</param>
        /// <param name="bIgnoreCloseException">if set to <c>true</c> [b ignore close exception].</param>
        /// <returns></returns>
        public static bool CloseIfOpen(this IDbConnection dbConnection, bool bIgnoreCloseException = true)
        {
            bool bResult = false;

            if (dbConnection.State == ConnectionState.Open)
            {
                try
                {
                    dbConnection.Close();
                }
                catch (Exception ex)
                {
                    if (bIgnoreCloseException == false)
                    {
                        throw ex;
                    }
                }

                bResult = true;
            }

            return bResult;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Exports the CSV.
        /// </summary>
        /// <param name="dbConnection">The db connection.</param>
        /// <param name="strSelectStatement">The STR select statement.</param>
        /// <param name="fiExportFile">The fi export file.</param>
        /// <param name="cDivider">The c divider.</param>
        public static void SaveAsCSV(this IDbConnection dbConnection, string strSelectStatement, FileInfo fiExportFile, char cDivider)
        {
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSelectStatement;

                using (IDataReader dbResult = dbCommand.ExecuteReader())
                {
                    using (StreamWriter fs = new StreamWriter(new BufferedStream(new FileStream(fiExportFile.FullName, FileMode.Create, FileAccess.Write))))
                    {
                        for (int iFieldCounter = 0; iFieldCounter < dbResult.FieldCount; iFieldCounter++)
                        {
                            if (iFieldCounter > 0)
                            {
                                fs.Write(cDivider);
                            }

                            fs.Write("{0}", dbResult.GetName(iFieldCounter));
                        }

                        while (dbResult.Read())
                        {
                            for (int iFieldCounter = 0; iFieldCounter < dbResult.FieldCount; iFieldCounter++)
                            {
                                if (iFieldCounter > 0)
                                {
                                    fs.Write(cDivider);
                                }

                                fs.Write("{0}", dbResult[iFieldCounter]);
                            }

                            fs.WriteLine();
                        }

                        fs.Close();
                    }

                    dbResult.Close();
                }
            }
        }


        /// <summary>
        /// Selects as data table.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="strResultTableName">Name of the string result table.</param>
        /// <param name="strFormat">The string format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static DataTable SelectAsDataTable(this IDbConnection dbConnection, string strResultTableName, string strFormat, params object[] args)
        {
            string strSQLStatement = args != null && args.Length > 0 ? string.Format(strFormat, args) : strFormat;

            DataTable dtResult = null;

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = strSQLStatement;
                dbCommand.CommandTimeout = 0;

                using (IDataReader dbResult = dbCommand.ExecuteReader())
                {
                    dtResult = new DataTable(strResultTableName);

                    for (int iCounter = 0; iCounter < dbResult.FieldCount; iCounter++)
                    {
                        dtResult.Columns.Add(dbResult.GetName(iCounter), dbResult.GetFieldType(iCounter));
                    }

                    List<object> lData = new List<object>();

                    while (dbResult.Read())
                    {
                        lData.Clear();

                        for (int iCounter = 0; iCounter < dbResult.FieldCount; iCounter++)
                        {
                            lData.Add(dbResult[iCounter]);
                        }

                        dtResult.Rows.Add(lData.ToArray());
                    }

                    dbResult.Close();
                }
            }

            return dtResult;
        }

    } // end static public class IDBConnectionExtension
}
