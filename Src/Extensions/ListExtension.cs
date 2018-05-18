using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

using Newtonsoft.Json;

using TransmitterTool.Interfaces;



namespace TransmitterTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    static public class ListExtension
    {
        /// <summary>
        /// The hs ignore types
        /// </summary>
        static private readonly HashSet<string> hsIgnoreTypes = new HashSet<string> { "List`1" , "HashSet`1" , "SortedDictionary`2" , "IntPtr" , "StreamWriter" , "StreamReader" };

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Saves the list as XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lValues"></param>
        /// <param name="strOutputFilename"></param>
        static public void SaveAsXml<T>( this List<T> lValues , string strOutputFilename ) where T : IXmlExport
        {
            XElement eTransmitter = new XElement( typeof( T ).Name + "List" );

            foreach( T t in lValues )
            {
                eTransmitter.Add( t.ToXml() );
            }

            eTransmitter.SaveDefault( strOutputFilename );
        }


        /// <summary>
        /// Saves the list as JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lValues">The l values.</param>
        /// <param name="strOutputFilename">The string output filename.</param>
        /// <exception cref="ArgumentException">
        /// Die Liste darf nicht leer sein! - lValues
        /// or
        /// Der Ausgabedateiname darf nicht leer sein! - strOutputFilename
        /// </exception>
        static public void SaveAsJson<T>( this List<T> lValues , string strOutputFilename )
        {
            if( lValues == null || lValues.Count == 0 )
            {
                throw new ArgumentException( "Die Liste darf nicht leer sein!" , "lValues" );
            }

            if( strOutputFilename.IsEmpty() )
            {
                throw new ArgumentException( "Der Ausgabedateiname darf nicht leer sein!" , "strOutputFilename" );
            }

            //---------------------------------------------

            string strJson = JsonConvert.SerializeObject( lValues , Formatting.Indented );

            File.WriteAllText( strOutputFilename , strJson , Encoding.GetEncoding( "ISO-8859-1" ) );
        }


        /// <summary>
        /// Saves the list as CSV.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lValues">The l values.</param>
        /// <param name="strOutputFilename">The string output filename.</param>
        /// <param name="bUseQuotationMark">if set to <c>true</c> [b use quotation mark].</param>
        /// <exception cref="ArgumentException">Die Liste darf nicht leer sein! - lValues
        /// or
        /// Der Ausgabedateiname darf nicht leer sein! - strOutputFilename</exception>
        /// <exception cref="System.ArgumentException">Die Liste darf nicht leer sein! - lValues
        /// or
        /// Der Ausgabedateiname darf nicht leer sein! - strOutputFilename</exception>
        static public void SaveAsCsv<T>( this List<T> lValues , string strOutputFilename , bool bUseQuotationMark = false )
        {
            if( lValues == null || lValues.Count == 0 )
            {
                throw new ArgumentException( "Die Liste darf nicht leer sein!" , "lValues" );
            }

            if( strOutputFilename.IsEmpty() )
            {
                throw new ArgumentException( "Der Ausgabedateiname darf nicht leer sein!" , "strOutputFilename" );
            }

            //---------------------------------------------

            Type tType = typeof( T );

            StringBuilder sb = new StringBuilder( 8192 );

            //---------------------------------------------

            int iColumnCounter = 0;

            foreach( PropertyInfo pi in tType.GetProperties() )
            {
                if( hsIgnoreTypes.Contains( pi.PropertyType.Name ) == true )
                {
                    continue;
                }

                if( iColumnCounter > 0 )
                {
                    sb.Append( ';' );
                }

                sb.Append( pi.Name );

                iColumnCounter++;
            }

            sb.AppendLine();

            //---------------------------------------------

            foreach( object o in lValues )
            {
                iColumnCounter = 0;

                foreach( PropertyInfo pi in tType.GetProperties() )
                {
                    if( hsIgnoreTypes.Contains( pi.PropertyType.Name ) == true )
                    {
                        continue;
                    }

                    if( iColumnCounter > 0 )
                    {
                        sb.Append( ';' );
                    }

                    object value = pi.GetValue( o , null );

                    if( value == DBNull.Value || value == null )
                    {
                        sb.Append( "-" );
                    }
                    else
                    {
                        // Es kann vorkommen das wir einen String haben der nicht NULL aber "" ist ...
                        if( value is string )
                        {
                            if( ( ( string ) value ).Length == 0 )
                            {
                                value = "-";
                            }

                            if( bUseQuotationMark == true )
                            {
                                sb.AppendFormat( "\"{0}\"" , value );
                            }
                            else
                            {
                                sb.Append( value );
                            }
                        }
                        else
                        {
                            sb.Append( value );
                        }
                    }

                    iColumnCounter++;
                }

                sb.AppendLine();
            }

            //---------------------------------------------

            File.WriteAllText( strOutputFilename , sb.ToString() , Encoding.GetEncoding( "ISO-8859-1" ) );
        }

    } // end static public class ListExtension
}
