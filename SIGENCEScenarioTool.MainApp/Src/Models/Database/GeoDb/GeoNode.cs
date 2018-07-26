using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;

using SIGENCEScenarioTool.Extensions;



namespace SIGENCEScenarioTool.Models.Database.GeoDb
{
    /// <summary>
    /// 
    /// </summary>
    public enum GeoTag : byte
    {
        Aeroway,
        Amenity,
        Craft,
        Emergency,
        Leisure,
        Man_Made,
        Military,
        Place,
        Power,
        Shop,
        Vending    } // end public enum GeoTag



    /// <summary>
    /// 
    /// </summary>
    sealed public class GeoNode
    {

        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        /// <value>
        /// The node identifier.
        /// </value>
        public long NodeId { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public GeoTag Tag { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

    } // end sealed public class GeoNode



    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SIGENCEScenarioTool.Models.Database.GeoDb.GeoNode}" />
    sealed public class GeoNodeCollection : ObservableCollection<GeoNode>
    {

        ///// <summary>
        ///// The instance
        ///// </summary>
        //static private GeoNodeCollection instance = null;

        ///// <summary>
        ///// Prevents a default instance of the <see cref="GeoNodeCollection" /> class from being created.
        ///// </summary>
        //private GeoNodeCollection()
        //{

        //}

        ///// <summary>
        ///// Gets the instance.
        ///// </summary>
        ///// <value>
        ///// The instance.
        ///// </value>
        //static public GeoNodeCollection Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new GeoNodeCollection();
        //        }

        //        return instance;
        //    }
        //}


        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <param name="strDatabaseFilename">The string database filename.</param>
        /// <param name="geotag">The geotag.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">The parameter should not be empty! - strDatabaseFilename</exception>
        /// <exception cref="FileNotFoundException">The database can't not be found!</exception>
        static public GeoNodeCollection GetCollection(string strDatabaseFilename, GeoTag? geotag = null)
        {
            if (string.IsNullOrEmpty(strDatabaseFilename))
            {
                throw new ArgumentException("The parameter should not be empty!", "strDatabaseFilename");
            }

            if (File.Exists(strDatabaseFilename) == false)
            {
                throw new FileNotFoundException("The database can't not be found!", strDatabaseFilename);
            }

            GeoNodeCollection collection = new GeoNodeCollection();

            SQLiteConnectionStringBuilder csbDatabase = new SQLiteConnectionStringBuilder
            {
                DataSource = strDatabaseFilename
            };

            using (SQLiteConnection dbConnection = new SQLiteConnection(csbDatabase.ConnectionString))
            {
                dbConnection.Open();

                try
                {
                    //                                     0   1   2   3    4    5
                    string strSelectStatement = "select osmid,lat,lon,name,tag,value from osmnode";

                    if (geotag != null)
                    {
                        strSelectStatement += string.Format(" where tag='{0}'", geotag.ToString().ToLower());
                    }

                    using (SQLiteCommand dbSelectCommand = new SQLiteCommand(strSelectStatement, dbConnection))
                    {
                        using (SQLiteDataReader dbResult = dbSelectCommand.ExecuteReader())
                        {
                            while (dbResult.Read())
                            {
                                GeoNode gn = new GeoNode
                                {
                                    NodeId = dbResult.GetInt64(0),
                                    Latitude = dbResult.GetDouble(1),
                                    Longitude = dbResult.GetDouble(2),
                                    Name = dbResult.IsDBNull(3) == false ? dbResult.GetString(3) : "",
                                    Tag = (GeoTag)Enum.Parse(typeof(GeoTag), dbResult.GetString(4), true),
                                    Value = dbResult.GetString(5).CapitalizeOnlyFirstLetter()
                                };

                                collection.Add(gn);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }

            return collection;
        }

    } // sealed public class GeoNodeCollection 

}
