using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

using Newtonsoft.Json;

using TransmitterTool.Interfaces;

#if EXCEL_SUPPORT
using Excel = global::Microsoft.Office.Interop.Excel;
#endif



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


#if EXCEL_SUPPORT

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="iColumn"></param>
        /// <param name="iRow"></param>
        /// <param name="value"></param>
        /// <param name="strFormat"></param>
        /// <param name="cTextColor"></param>
        /// <param name="haTextAlign"></param>
        static private void AddCell( Excel.Worksheet sheet , int iColumn , int iRow , object value , string strFormat , Excel.XlRgbColor? cTextColor , Excel.XlHAlign? haTextAlign )
        {

            Excel.Range cell = sheet.Cells [iRow , iColumn] as Excel.Range;
            cell.Value2 = value;

            if( strFormat != null )
            {
                cell.NumberFormat = strFormat;
            }

            if( cTextColor != null )
            {
                cell.Font.Color = cTextColor.Value;
            }

            if( haTextAlign != null )
            {
                cell.HorizontalAlignment = haTextAlign;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lValues"></param>
        /// <param name="strOutputFilename"></param>
        static public void SaveAsExcel<T>( this List<T> lValues , string strOutputFilename )
        {
            if( lValues == null || lValues.Count == 0 )
            {
                throw new ArgumentException( "Die Liste darf nicht leer sein!" , "lValues" );
            }

            if( strOutputFilename.IsEmpty() )
            {
                throw new ArgumentException( "Der Ausgabedateiname darf nicht leer sein!" , "strOutputFilename" );
            }

            //-----------------------------------------------------------------

            object Missing = Type.Missing;
            Type tType = typeof( T );

            //-----------------------------------------------------------------

            Excel.Application excel = new Excel.Application();
            excel.SheetsInNewWorkbook = 1;

            Excel.Workbook wb = excel.Workbooks.Add( Missing );
            Excel.Worksheet sheet = wb.Sheets [1] as Excel.Worksheet;

            sheet.Name = tType.Name;

            //-----------------------------------------------------------------

            // Create Header Columns
            {
                int iColumnCounter = 1;

                foreach( PropertyInfo pi in tType.GetProperties() )
                {
                    if( hsIgnoreTypes.Contains( pi.PropertyType.Name ) == true )
                    {
                        continue;
                    }

                    Excel.Range cell = sheet.Cells [1 , iColumnCounter++] as Excel.Range;
                    cell.Font.Bold = true;
                    cell.Orientation = Excel.XlOrientation.xlUpward;
                    cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    cell.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                    cell.Value2 = " " + pi.Name;
                }
            }

            //-----------------------------------------------------------------

            // Create Data Columns And Rows
            {
                int iRowCounter = 2;

                foreach( T row in lValues )
                {
                    int iColumnCounter = 1;

                    foreach( PropertyInfo pi in tType.GetProperties() )
                    {
                        if( hsIgnoreTypes.Contains( pi.PropertyType.Name ) == true )
                        {
                            continue;
                        }

                        object value = pi.GetValue( row , null );

                        if( value != DBNull.Value && value != null )
                        {
                            if( value is Guid )
                            {
                                value = value.ToString();
                            }

                            AddCell( sheet , iColumnCounter , iRowCounter , value , null , null , value is string ? Excel.XlHAlign.xlHAlignLeft : Excel.XlHAlign.xlHAlignRight );
                        }

                        iColumnCounter++;
                    }

                    iRowCounter++;
                }
            }

            //-----------------------------------------------------------------

            sheet.Columns.AutoFit();

            excel.Visible = true;

            wb.SaveAs( strOutputFilename , Missing , Missing , Missing , Missing , Missing , Excel.XlSaveAsAccessMode.xlNoChange , Missing , Missing , Missing , Missing , Missing );

            // Achtung: Auch wenn diese Funktion beendet wird bleibt Excel geöffnet. Die Daten sind
            // aber noch nicht in einer Datei gespeichert. Das muß in Excel der User selbst machen.
        }
#endif

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Saves the list as XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lValues">The l values.</param>
        /// <param name="strOutputFilename">The string output filename.</param>
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
