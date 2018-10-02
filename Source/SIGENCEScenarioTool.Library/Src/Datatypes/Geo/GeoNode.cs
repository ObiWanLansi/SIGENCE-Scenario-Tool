using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;

using GMap.NET;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GeoNode
    {

        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        /// <value>
        /// The node identifier.
        /// </value>
        public long NodeId { get; internal set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public Latitude Latitude { get; internal set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public Longitude Longitude { get; internal set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public GeoTag Tag { get; internal set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; internal set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The PLL position
        /// </summary>
        private PointLatLng? pllPosition = null;

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public PointLatLng Position
        {
            get
            {
                if (this.pllPosition == null)
                {
                    this.pllPosition = new PointLatLng(this.Latitude, this.Longitude);
                }

                return this.pllPosition.Value;
            }
        }

    } // end sealed public class GeoNode



    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{GeoNode}" />
    public sealed class GeoNodeCollection : ObservableCollection<GeoNode>
    {
        #region If We Want To Have The Instance As Singelton Object

        ///// <summary>
        ///// The instance
        ///// </summary>
        //static private GeoNodeCollection instance = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="GeoNodeCollection" /> class from being created.
        /// </summary>
        private GeoNodeCollection()
        {

        }

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
        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <param name="strDatabaseFilename">The string database filename.</param>
        /// <param name="geotag">The geotag.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">The parameter should not be empty! - strDatabaseFilename</exception>
        /// <exception cref="FileNotFoundException">The database can't not be found!</exception>
        public static GeoNodeCollection GetCollection(string strDatabaseFilename, GeoTag? geotag = null)
        {
            if (string.IsNullOrEmpty(strDatabaseFilename))
            {
                throw new ArgumentException("The parameter should not be empty!", nameof(strDatabaseFilename));
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
                        strSelectStatement += $" where tag='{geotag.ToString().ToLower()}'";
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
