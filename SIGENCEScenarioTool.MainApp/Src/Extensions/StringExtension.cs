using System;
using System.Collections.Generic;
using System.Windows.Media;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// Eine Erweiterungsklasse für unseren lieblichen String.
    /// </summary>
    static public class StringExtension
    {

        /// <summary>
        /// Removes the quotation.
        /// </summary>
        /// <param name="strContent">Content of the STR.</param>
        /// <returns></returns>
        static public string RemoveQuotation(this string strContent)
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
        static public bool IsEmpty(this string strContent)
        {
            return string.IsNullOrEmpty(strContent);
        }


        /// <summary>
        /// Liefert zurück ob ein String nicht null oder dessen Länge > 0 ist.
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        static public bool IsNotEmpty(this string strContent)
        {
            return !string.IsNullOrEmpty(strContent);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Capitalizes the only first letter.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <returns></returns>
        static public string CapitalizeOnlyFirstLetter(this string strContent)
        {
            char[] aData = strContent.ToLower().ToCharArray();

            if (Char.IsLower(aData[0]))
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
        static public string Capitalize(this string strContent)
        {
            char[] aData = strContent.ToLower().ToCharArray();

            for (int iCounter = 0; iCounter < aData.Length; iCounter++)
            {
                if (iCounter == 0)
                {
                    if (Char.IsLetter(aData[iCounter]))
                    {
                        aData[iCounter] -= (char)0x20;
                    }
                }
                else
                {
                    if (Char.IsLetter(aData[iCounter - 1]) == false && aData[iCounter - 1] != '\'')
                    {
                        if (Char.IsLetter(aData[iCounter]))
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
        static public Color ToColor(this string strColor, Color cDefault)
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
        /// The sd HTML entinities
        /// </summary>
        private static readonly SortedDictionary<string, string> sdHtmlEntinities = new SortedDictionary<string, string>
        {
            { "ä" , "&auml;" } , { "ö" , "&ouml;" } , { "ü" , "&uuml;" } ,
            { "Ä" , "&Auml;" } , { "Ö" , "&Ouml;" } , { "Ü" , "&Uuml;" } ,
            { "ß" , "&szlig;" }, { "<" , "&lt;" } , { ">" , "&gt;" } 
            //{ " " , "&nbsp;" }
        };


        /// <summary>
        /// Replaces the HTML.
        /// </summary>
        /// <param name="strContent">Content of the STR.</param>
        /// <returns></returns>
        static public string ReplaceHtml(this string strContent)
        {
            if (strContent.IsNotEmpty() == true)
            {
                sdHtmlEntinities.ForEach((k, v) =>
                {
                    strContent = strContent.Replace(k, v);
                });
            }

            return strContent;
        }

    } // end static public class StringExtension
}
