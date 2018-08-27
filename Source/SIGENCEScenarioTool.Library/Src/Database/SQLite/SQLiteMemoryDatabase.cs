using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

using SIGENCEScenarioTool.Extensions;



namespace SIGENCEScenarioTool.Database.SQLite
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    sealed public class SQLiteMemoryDatabase : IDisposable
    {

        /// <summary>
        /// The b is disposed
        /// </summary>
        private bool bIsDisposed = false;


        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public SQLiteConnection Connection { get; private set; } = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteMemoryDatabase"/> class.
        /// </summary>
        public SQLiteMemoryDatabase()
        {
            Connection = new SQLiteConnection(new SQLiteConnectionStringBuilder { DataSource = ":memory:" }.ConnectionString);
            Connection.Open();
        }


        /// <summary>
        /// Finalizes an instance of the <see cref="SQLiteMemoryDatabase"/> class.
        /// </summary>
        ~SQLiteMemoryDatabase()
        {
            Dispose(false);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Performs an implicit conversion from <see cref="SQLiteMemoryDatabase"/> to <see cref="SQLiteConnection"/>.
        /// </summary>
        /// <param name="memdb">The memdb.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator SQLiteConnection(SQLiteMemoryDatabase memdb) => memdb.Connection;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Loads the specified fi.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <returns></returns>
        public bool Load(FileInfo fi)
        {
            if (fi.Exists == false)
            {
                return false;
            }

            using (SQLiteConnection dbFile = new SQLiteConnection(new SQLiteConnectionStringBuilder { DataSource = fi.FullName }.ConnectionString))
            {
                dbFile.Open();

                dbFile.BackupDatabase(Connection, "main", "main", -1, null, -1);

                dbFile.Close();
            }

            return true;
        }


        /// <summary>
        /// Loads the specified string filename.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        /// <returns></returns>
        public bool Load(string strFilename) => Load(new FileInfo(strFilename));

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Saves the specified fi.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <param name="bOverWrite">if set to <c>true</c> [b over write].</param>
        /// <param name="bCleanWrite">if set to <c>true</c> [b clean write].</param>
        /// <returns></returns>
        public bool Save(FileInfo fi, bool bOverWrite = true, bool bCleanWrite = true)
        {
            if (fi.Exists == true)
            {
                fi.Delete();
            }

            // Immer schön sauber wegschreiben ...
            if (bCleanWrite == true)
            {
                Connection.Analyze();
                Connection.Reindex();
                Connection.Vacuum();
            }

            using (SQLiteConnection dbFile = new SQLiteConnection(new SQLiteConnectionStringBuilder { DataSource = fi.FullName }.ConnectionString))
            {
                dbFile.Open();

                Connection.BackupDatabase(dbFile, "main", "main", -1, null, -1);

                dbFile.Close();
            }

            return false;
        }


        /// <summary>
        /// Saves the specified string filename.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        /// <param name="bOverWrite">if set to <c>true</c> [b over write].</param>
        /// <param name="bCleanWrite">if set to <c>true</c> [b clean write].</param>
        /// <returns></returns>
        public bool Save(string strFilename, bool bOverWrite = true, bool bCleanWrite = true) => Save(new FileInfo(strFilename), bOverWrite, bCleanWrite);

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="bShouldDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool bShouldDisposing)
        {
            if (bIsDisposed == false)
            {
                if (bShouldDisposing == true)
                {
                    if (Connection != null)
                    {
                        if (Connection.State != ConnectionState.Closed)
                        {
                            Connection.Close();
                        }

                        Connection.Dispose();
                        Connection = null;
                    }
                }

                bIsDisposed = true;
            }
        }

    } // end sealed public class SQLiteMemoryDatabase 
}
