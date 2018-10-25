using System;
using System.Collections.Generic;
using System.Windows.Media;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// Eine Erweiterungsklasse für unseren lieblichen String.
    /// </summary>
    public static class StringExtension
    {

        /// <summary>
        /// Removes the quotation.
        /// </summary>
        /// <param name="strContent">Content of the STR.</param>
        /// <returns></returns>
        public static string RemoveQuotation(this string strContent)
        {
            string strTemp = strContent.Trim();

            if (strTemp.StartsWith("\"") && strTemp.EndsWith("\""))
            {
                return strTemp.Substring(1, strContent.Length - 2);
            }

            return strContent;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Liefert zurück ob ein String null oder dessen Länge 0 ist.
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string strContent)
        {
            return string.IsNullOrEmpty(strContent);
        }


        /// <summary>
        /// Liefert zurück ob ein String nicht null oder dessen Länge > 0 ist.
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string strContent)
        {
            return !string.IsNullOrEmpty(strContent);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Capitalizes the only first letter.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <returns></returns>
        public static string CapitalizeOnlyFirstLetter(this string strContent)
        {
            char[] aData = strContent.ToLower().ToCharArray();

            if (char.IsLower(aData[0]))
            {
                aData[0] -= (char)0x20;
            }

            return new string(aData);
        }


        /// <summary>
        /// Capitalizes the specified string content.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <returns></returns>
        public static string Capitalize(this string strContent)
        {
            char[] aData = strContent.ToLower().ToCharArray();

            for (int iCounter = 0; iCounter < aData.Length; iCounter++)
            {
                if (iCounter == 0)
                {
                    if (char.IsLetter(aData[iCounter]))
                    {
                        aData[iCounter] -= (char)0x20;
                    }
                }
                else
                {
                    if (char.IsLetter(aData[iCounter - 1]) == false && aData[iCounter - 1] != '\'')
                    {
                        if (char.IsLetter(aData[iCounter]))
                        {
                            aData[iCounter] -= (char)0x20;
                        }
                    }
                }
            }

            return new string(aData);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Liefert aus einem String wie z.b. "#FFAACC" den Farbwert als Color Objekt zurück.
        /// </summary>
        /// <param name="strColor"></param>
        /// <param name="cDefault"></param>
        /// <returns></returns>
        /// <remarks>Es könnten auch die .NET symbolischen Farbnamen wie "SlateBlue" übergeben werden.</remarks>
        public static Color ToColor(this string strColor, Color cDefault)
        {
            if (!string.IsNullOrEmpty(strColor))
            {
                if (strColor[0] == '#')
                {
                    try
                    {
                        return (Color)ColorConverter.ConvertFromString(strColor);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return cDefault;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Vergleicht zwei String wobei nicht zwischen Groß- und Kleinschreibung unterschieden wird.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <param name="strOtherString">The string other string.</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string strContent, string strOtherString)
        {
            return strContent.Equals(strOtherString, StringComparison.CurrentCultureIgnoreCase);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The HTML entinities
        /// </summary>
        private static readonly SortedDictionary<string, string> sdHtmlEntinities = new SortedDictionary<string, string>
        {
            { "ä" , "&auml;" } , { "ö" , "&ouml;" } , { "ü" , "&uuml;" } ,
            { "Ä" , "&Auml;" } , { "Ö" , "&Ouml;" } , { "Ü" , "&Uuml;" } ,
            { "ß" , "&szlig;" }, { "<" , "&lt;" } , { ">" , "&gt;" } 
            //{ " " , "&nbsp;" }
        };


        /// <summary>
        /// The german umlauts
        /// </summary>
        private static readonly SortedDictionary<string, string> sdGermanUmlauts = new SortedDictionary<string, string>
        {
            { "ä" , "&auml;" } , { "ö" , "&ouml;" } , { "ü" , "&uuml;" } ,
            { "Ä" , "&Auml;" } , { "Ö" , "&Ouml;" } , { "Ü" , "&Uuml;" } ,
            { "ß" , "&szlig;" }
        };


        /// <summary>
        /// Replaces the HTML.
        /// </summary>
        /// <param name="strContent">Content of the STR.</param>
        /// <returns></returns>
        public static string ReplaceHtml(this string strContent, bool bOnlyGermanUmlauts = false)
        {
            if (strContent.IsNotEmpty() == true)
            {
                if (bOnlyGermanUmlauts)
                {
                    sdGermanUmlauts.ForEach((k, v) =>
                    {
                        strContent = strContent.Replace(k, v);
                    });
                }
                else
                {
                    sdHtmlEntinities.ForEach((k, v) =>
                    {
                        strContent = strContent.Replace(k, v);
                    });
                }
            }

            return strContent;
        }

    } // end static public class StringExtension
}
