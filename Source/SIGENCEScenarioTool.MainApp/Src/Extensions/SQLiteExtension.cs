using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using SIGENCEScenarioTool.Database.SQLite;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    static public class SQLiteExtension
    {
        /// <summary>
        /// Analyzes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        static public void Analyze(this SQLiteConnection dbConnection)
        {
            using (SQLiteCommand dbCommand = new SQLiteCommand("ANALYZE", dbConnection))
            {
                dbCommand.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Reindexes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        static public void Reindex(this SQLiteConnection dbConnection)
        {
            using (SQLiteCommand dbCommand = new SQLiteCommand("REINDEX", dbConnection))
            {
                dbCommand.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Vacuums the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        static public void Vacuum(this SQLiteConnection dbConnection)
        {
            using (SQLiteCommand dbCommand = new SQLiteCommand("VACUUM", dbConnection))
            {
                dbCommand.ExecuteNonQuery();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Drops the table.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="strTablename">The string tablename.</param>
        static public void DropTable(this SQLiteConnection dbConnection, string strTablename)
        {
            dbConnection.ExecuteNonQuery("DROP TABLE {0}", strTablename);
        }


        /// <summary>
        /// Tables the exists.
        /// </summary>
        /// <param name="dbConnection">The db connection.</param>
        /// <param name="strTablename">The STR tablename.</param>
        /// <returns></returns>
        static public bool TableExists(this SQLiteConnection dbConnection, string strTablename)
        {
            return 1 == (long)dbConnection.ExecuteScalar("SELECT COUNT(1) FROM SQLITE_MASTER WHERE TYPE = 'table' AND LOWER(NAME) = '{0}'", strTablename.ToLower());
        }


        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        static public List<string> GetTableNames(this SQLiteConnection dbConnection)
        {
            return dbConnection.Select("SELECT NAME FROM SQLITE_MASTER WHERE TYPE='table' AND NAME NOT LIKE 'sqlite_%'").Select(dbResult => dbResult.GetString(0)).ToList();
        }


        /// <summary>
        /// Gets the view names.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        static public List<string> GetViewNames(this SQLiteConnection dbConnection)
        {
            return dbConnection.Select("SELECT NAME FROM SQLITE_MASTER WHERE TYPE='view' AND NAME NOT LIKE 'sqlite_%'").Select(dbResult => dbResult.GetString(0)).ToList();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Truncates the specified string tablename.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="strTablename">The string tablename.</param>
        static public void Truncate(this SQLiteConnection dbConnection, string strTablename)
        {
            using (SQLiteTransaction transaction = dbConnection.BeginTransaction())
            {
                // In SQLite gibt es nicht wirklich ein Truncate, deswegen ein DELETE mit Transaction
                using (SQLiteCommand dbCommand = new SQLiteCommand(string.Format("DELETE FROM {0}", strTablename), dbConnection))
                {
                    dbCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the last primarykey.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        static public long GetLastPrimarykey(this SQLiteConnection dbConnection)
        {
            using (SQLiteCommand dbCommand = new SQLiteCommand("SELECT LAST_INSERT_ROWID()", dbConnection))
            {
                return (long)dbCommand.ExecuteScalar();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The sb insert statement
        /// </summary>
        static private readonly StringBuilder sbInsertStatement = new StringBuilder(512);


        /// <summary>
        /// Prepares the insert statement.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="strTablename">The string tablename.</param>
        /// <param name="bIgnorePrimaryKey">if set to <c>true</c> [b ignore primary key].</param>
        /// <returns></returns>
        static public SQLiteCommand PrepareInsertStatement(this SQLiteConnection dbConnection, string strTablename, bool bIgnorePrimaryKey = true)
        {
            sbInsertStatement.Clear();

            SQLiteCommand dbInsertCommand = new SQLiteCommand(dbConnection);

            sbInsertStatement.AppendFormat("INSERT INTO {0} (", strTablename);

            using (SQLiteCommand dbSelectColumns = new SQLiteCommand(string.Format("PRAGMA TABLE_INFO({0})", strTablename), dbConnection))
            {
                using (SQLiteDataReader dbResult = dbSelectColumns.ExecuteReader())
                {
                    int iCounter = 0;

                    while (dbResult.Read())
                    {
                        //TODO: Wir gehen hier davon aus das wenn es sich um den PK handelt auch ein AUTO_INCREMENT daruf ist, dem muss aber nicht sein,
                        // also nochmal gucken ob das nicht besser geht ...
                        if ((long)dbResult[5] == 0 || bIgnorePrimaryKey == false)
                        {
                            if (iCounter++ > 0)
                            {
                                sbInsertStatement.Append(',');
                            }

                            string strColumnName = (string)dbResult[1];
                            sbInsertStatement.Append(strColumnName);
                            dbInsertCommand.Parameters.Add(new SQLiteParameter(strColumnName, SQLiteHelper.GetDbType((string)dbResult[2])));
                        }
                    }

                    sbInsertStatement.Append(") VALUES (");

                    while (iCounter != 0)
                    {
                        sbInsertStatement.Append('?');

                        if (--iCounter > 0)
                        {
                            sbInsertStatement.Append(',');
                        }
                    }

                    sbInsertStatement.Append(")");

                    dbResult.Close();
                }
            }

            dbInsertCommand.CommandText = sbInsertStatement.ToString();

            dbInsertCommand.Prepare();

            return dbInsertCommand;
        }

    } // end static public class SQLiteExtension
}
