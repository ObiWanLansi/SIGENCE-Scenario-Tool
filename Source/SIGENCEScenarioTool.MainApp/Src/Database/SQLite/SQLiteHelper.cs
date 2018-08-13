using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

using SIGENCEScenarioTool.Extensions;



namespace SIGENCEScenarioTool.Database.SQLite
{
    /// <summary>
    /// 
    /// </summary>
    static public class SQLiteHelper
    {
        /// <summary>
        /// The type mapping
        /// </summary>
        static public readonly Dictionary<Type, Tuple<string, DbType, bool>> TypeMapping = new Dictionary<Type, Tuple<string, DbType, bool>>
        {
            {typeof(long),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,
            {typeof(UInt64),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,

            {typeof(int),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,
            {typeof(UInt32),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,

            {typeof(Int16),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,
            {typeof(UInt16),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,

            {typeof(Byte),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,
            {typeof(SByte),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,false)} ,

            {typeof(Double),new Tuple<string,DbType,bool>("REAL",DbType.Double,false)} ,
            {typeof(Single),new Tuple<string,DbType,bool>("REAL",DbType.Single,false)} ,

            {typeof(Guid),new Tuple<string,DbType,bool>("TEXT",DbType.String,false)} ,
            {typeof(Boolean),new Tuple<string,DbType,bool>("BOOLEAN",DbType.Boolean,false)} ,
            {typeof(DateTime),new Tuple<string,DbType,bool>("DATETIME",DbType.DateTime,false)} ,

            //-----------------------------------------------------------------

            {typeof(Byte?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,
            {typeof(SByte?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,

            {typeof(Int16?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,
            {typeof(UInt16?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,

            {typeof(int?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,
            {typeof(UInt32?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,

            {typeof(long?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,
            {typeof(UInt64?),new Tuple<string,DbType,bool>("INTEGER",DbType.Int64,true)} ,

            {typeof(Single?),new Tuple<string,DbType,bool>("REAL",DbType.Single,true)} ,
            {typeof(Double?),new Tuple<string,DbType,bool>("REAL",DbType.Double,true)} ,

            {typeof(Guid?),new Tuple<string,DbType,bool>("TEXT",DbType.String,true)} ,
            {typeof(Boolean?),new Tuple<string,DbType,bool>("BOOLEAN",DbType.Boolean,true)} ,
            {typeof(DateTime?),new Tuple<string,DbType,bool>("DATETIME",DbType.DateTime,true)} ,

            //-----------------------------------------------------------------

            {typeof(Byte []),new Tuple<string,DbType,bool>("BLOB",DbType.Object,true)}

            //{typeof(String),new Trio<String,DbType,bool>("TEXT",DbType.String,false)} 
        };

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the sq lite parameter.
        /// </summary>
        /// <param name="pi">The pi.</param>
        /// <returns></returns>
        static public SQLiteParameter GetSQLiteParameter(PropertyInfo pi)
        {
            Type t = pi.PropertyType;

            if (TypeMapping.ContainsKey(t) == true)
            {
                return new SQLiteParameter("@" + pi.Name, TypeMapping[t].Item2) { IsNullable = TypeMapping[t].Item3 };
            }

            //-------------------------------------------------------------------------------------

            if (t == typeof(string))
            {
                // Der ist mal statisch immer auf NOT NULL ...
                return new SQLiteParameter("@" + pi.Name, DbType.String) { IsNullable = false };
            }

            //-------------------------------------------------------------------------------------

            if (t.IsEnum == true)
            {
                return new SQLiteParameter("@" + pi.Name, DbType.String) { IsNullable = false };
            }

            if (t.IsGenericType)
            {
                if (t.GenericTypeArguments.Length == 1)
                {
                    if (t.GenericTypeArguments[0].IsEnum == true)
                    {
                        return new SQLiteParameter("@" + pi.Name, DbType.String) { IsNullable = true };
                    }
                }
            }

            //-------------------------------------------------------------------------------------

            #region Sonderbehandlungen für KnownStructs

            //if (t == typeof(Color) || t == typeof(Rectangle) || t == typeof(Size) || t == typeof(Point))
            //{
            //    return new SQLiteParameter("@" + pi.Name,DbType.String) { IsNullable = false };
            //}

            //if (t == typeof(Color?) || t == typeof(Rectangle?) || t == typeof(Size?) || t == typeof(Point?))
            //{
            //    return new SQLiteParameter("@" + pi.Name,DbType.String) { IsNullable = true };
            //}

            //if (t == typeof(TimeSpan))
            //{
            //    return new SQLiteParameter("@" + pi.Name,DbType.Double) { IsNullable = false };
            //}

            //if (t == typeof(TimeSpan?))
            //{
            //    return new SQLiteParameter("@" + pi.Name,DbType.Double) { IsNullable = true };
            //}

            #endregion

            //-------------------------------------------------------------------------------------

            // Dann nehmen wir an das es Binärdaten sind ...
            return new SQLiteParameter("@" + pi.Name, DbType.Binary) { IsNullable = true };
        }


        /// <summary>
        /// Gets the sq lite column.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        static public string GetSQLiteColumn(Type t)
        {
            if (TypeMapping.ContainsKey(t))
            {
                return string.Format("{0}{1}", TypeMapping[t].Item1, TypeMapping[t].Item3 ? "" : " NOT NULL");
            }

            //-------------------------------------------------------------------------------------

            if (t == typeof(string))
            {
                // Der ist mal statisch immer auf NOT NULL ...
                return string.Format("TEXT{0}", false ? "" : " NOT NULL");
            }

            //-------------------------------------------------------------------------------------

            if (t.IsEnum == true)
            {
                return "TEXT NOT NULL";
            }

            if (t.IsGenericType)
            {
                if (t.GenericTypeArguments.Length == 1)
                {
                    if (t.GenericTypeArguments[0].IsEnum == true)
                    {
                        return "TEXT";
                    }
                }
            }

            //-------------------------------------------------------------------------------------

            #region Sonderbehandlungen für KnownStructs

            //if (t == typeof(Color) || t == typeof(Rectangle) || t == typeof(Size) || t == typeof(Point))
            //{
            //    return "TEXT NOT NULL";
            //}

            //if (t == typeof(Color?) || t == typeof(Rectangle) || t == typeof(Size?) || t == typeof(Point?))
            //{
            //    return "TEXT";
            //}

            //if (t == typeof(TimeSpan))
            //{
            //    return "REAL NOT NULL";
            //}

            //if (t == typeof(TimeSpan?))
            //{
            //    return "REAL";
            //}

            #endregion

            //-------------------------------------------------------------------------------------

            // Dann nehmen wir an das es Binärdaten sind ...
            return "BLOB";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the type of the database.
        /// </summary>
        /// <param name="strSqlType">Type of the string SQL.</param>
        /// <returns></returns>
        static public DbType GetDbType(string strSqlType)
        {
            if (strSqlType == "INTEGER")
            {
                return DbType.Int64;
            }

            if (strSqlType.StartsWith("VARCHAR") || strSqlType == "TEXT" || strSqlType == "CLOB")
            {
                return DbType.String;
            }

            if (strSqlType == "REAL" || strSqlType == "DOUBLE" || strSqlType == "FLOAT")
            {
                return DbType.Decimal;
            }

            if (strSqlType == "NUMERIC")
            {
                return DbType.Single;
            }

            if (strSqlType == "DATE" || strSqlType == "DATETIME")
            {
                return DbType.DateTime;
            }

            if (strSqlType == "BOOLEAN" || strSqlType == "BOOL")
            {
                return DbType.Boolean;
            }

            return DbType.Object;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the type of the native.
        /// </summary>
        /// <param name="strSqlType">Type of the string SQL.</param>
        /// <returns></returns>
        static public Type GetNativeType(string strSqlType)
        {
            if (strSqlType.EqualsIgnoreCase("TEXT"))
            {
                return typeof(string);
            }

            foreach (Type t in TypeMapping.Keys)
            {
                Tuple<string, DbType, bool> tMapping = TypeMapping[t];

                if (tMapping.Item1.EqualsIgnoreCase(strSqlType))
                {
                    return t;
                }
            }

            return null;
        }

    } // end static public class SQLiteHelper
}
