using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using SIGENCEScenarioTool.Tools;

namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// A Extension Class For The String Class With Markdown Functions.
    /// </summary>
    public static class MarkdownExtension
    {
        /// <summary>
        /// The temporary StringBuilder for private use.
        /// </summary>
        private static readonly StringBuilder sbTemp = new StringBuilder(512);

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// A Parting Line.
        /// </summary>
        /// <returns></returns>
        public static string PartingLine() => "***";


        /// <summary>
        /// Converts the string into a bold string.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <returns></returns>
        public static string Bold(this string strContent) => $"**{strContent}**";


        /// <summary>
        /// Converts the string into a italic string.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <returns></returns>
        public static string Italic(this string strContent) => $"*{strContent}*";


        /// <summary>
        /// Converts the string into a strikethrough string.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <returns></returns>
        public static string Strikethrough(this string strContent) => $"~~{strContent}~~";


        /// <summary>
        /// Converts the string into a header string.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <param name="iLevel">The level.</param>
        /// <returns></returns>
        public static string Header(this string strContent, uint iLevel)
        {
            if (iLevel == 0)
            {
                throw new ArgumentException("The level must be above 0!");
            }

            if (iLevel > 6)
            {
                throw new ArgumentException("The level must be below 7!");
            }

            return $"{new string('#', (int)iLevel)} {strContent}";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Create a Hyperlink from the link with caption.
        /// </summary>
        /// <param name="strLink">The link.</param>
        /// <param name="strCaption">The caption.</param>
        /// <returns></returns>
        public static string Hyperlink(this string strLink, string strCaption = null) => string.IsNullOrEmpty(strCaption) ? strLink : $"[{strCaption}]({strLink})";


        /// <summary>
        /// Create a Hyperlink from the link with caption and tooltip.
        /// </summary>
        /// <param name="strLink">The link.</param>
        /// <param name="strCaption">The caption.</param>
        /// <param name="strTooltip">The tooltip.</param>
        /// <returns></returns>
        public static string Hyperlink(this string strLink, string strCaption, string strTooltip) => $"[{strCaption}]({strLink} \"{strTooltip}\")";


        /// <summary>
        /// Create a Image from the source with a alttext.
        /// </summary>
        /// <param name="strImageSource">The image source.</param>
        /// <param name="strAltText">The alt text.</param>
        /// <returns></returns>
        public static string Image(this string strImageSource, string strAltText = null) => string.IsNullOrEmpty(strAltText) ? $"![]({strImageSource})" : $"![{strAltText}]({strImageSource} \"{strAltText}\")";

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Generics the list.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="strPrefix">The string prefix.</param>
        /// <returns></returns>
        private static string GenericList<T>(this IEnumerable<T> items, string strPrefix)
        {
            sbTemp.Clear();

            foreach (T item in items)
            {
                sbTemp.AppendLine($"{strPrefix}{item}");
            }

            return sbTemp.ToString();
        }


        /// <summary>
        /// Create a unordered list.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string UnorderedList<T>(this IEnumerable<T> items) => GenericList(items, "* ");


        /// <summary>
        /// Create a ordered list.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string OrderedList<T>(this IEnumerable<T> items) => GenericList(items, "1. ");


        /// <summary>
        /// Create a taskslist.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string TaskList(this IEnumerable<object> items) => GenericList(items, "- [ ] ");


        /// <summary>
        /// Blockquotes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string BlockQuote(this IEnumerable<string> items) => GenericList(items, "> ");

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sources the code.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        /// <param name="strType">Type of the string.</param>
        /// <returns></returns>
        public static string SourceCode(this string strContent, string strType = null)
        {
            sbTemp.Clear();

            sbTemp.AppendLine($"```{strType ?? ""}");
            sbTemp.AppendLine(strContent);
            sbTemp.AppendLine("```");

            return sbTemp.ToString();
        }


        /// <summary>
        /// Sources the code.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <param name="strType">Type of the string.</param>
        /// <returns></returns>
        public static string SourceCode(this IEnumerable<string> lines, string strType = null)
        {
            sbTemp.Clear();

            sbTemp.AppendLine($"```{strType ?? ""}");

            foreach (string strLine in lines)
            {
                sbTemp.AppendLine(strLine);
            }

            sbTemp.AppendLine("```");

            return sbTemp.ToString();
        }


        /// <summary>
        /// Sources the code.
        /// </summary>
        /// <param name="fiSourceFile">The fi source file.</param>
        /// <param name="strType">Type of the string.</param>
        /// <returns></returns>
        public static string SourceCode(this FileInfo fiSourceFile, string strType = null)
        {
            sbTemp.Clear();

            if (string.IsNullOrEmpty(strType))
            {
                switch (fiSourceFile.Extension.ToLower())
                {
                    case ".cs":
                        strType = "C#";
                        break;

                    case ".py":
                        strType = "Python";
                        break;

                    case ".rb":
                        strType = "Ruby";
                        break;

                    case ".r":
                        strType = "R";
                        break;
                }
            }

            sbTemp.AppendLine($"```{strType ?? ""}");

            sbTemp.AppendLine(File.ReadAllText(fiSourceFile.FullName));

            sbTemp.AppendLine("```");

            return sbTemp.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Includes the specified fi markdown file.
        /// </summary>
        /// <param name="fiMarkdownFile">The fi markdown file.</param>
        /// <returns></returns>
        public static string Include(this FileInfo fiMarkdownFile)
        {
            return File.ReadAllText(fiMarkdownFile.FullName);
        }


        /// <summary>
        /// Includes the specified string include filename.
        /// </summary>
        /// <param name="strIncludeFilename">The string include filename.</param>
        /// <returns></returns>
        public static string Include(this string strIncludeFilename)
        {
            return File.ReadAllText(strIncludeFilename);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Directories the tree.
        /// </summary>
        /// <param name="strRootDirectory">The string root directory.</param>
        /// <param name="bFiles">if set to <c>true</c> [b files].</param>
        /// <param name="iMaxLevel">The i maximum level.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string DirectoryTree(this string strRootDirectory, bool bFiles = false, int iMaxLevel = -1)
        {
            return DirectoryTree(new DirectoryInfo(strRootDirectory), bFiles, iMaxLevel);
        }


        /// <summary>
        /// Directories the tree.
        /// </summary>
        /// <param name="diRootDirectory">The di root directory.</param>
        /// <param name="bFiles">if set to <c>true</c> [b files].</param>
        /// <param name="iMaxLevel">The i maximum level.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string DirectoryTree(this DirectoryInfo diRootDirectory, bool bFiles = false, int iMaxLevel = -1)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Directories the table.
        /// </summary>
        /// <param name="strRootDirectory">The string root directory.</param>
        /// <param name="bDisplaySize">if set to <c>true</c> [b display size].</param>
        /// <param name="bDisplayTimestamp">if set to <c>true</c> [b display timestamp].</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string DirectoryTable(this string strRootDirectory, bool bDisplaySize = true, bool bDisplayTimestamp = true)
        {
            return DirectoryTable(new DirectoryInfo(strRootDirectory), bDisplaySize, bDisplayTimestamp);
        }


        /// <summary>
        /// Directories the table.
        /// </summary>
        /// <param name="diRootDirectory">The di root directory.</param>
        /// <param name="bDisplaySize">if set to <c>true</c> [b display size].</param>
        /// <param name="bDisplayTimestamp">if set to <c>true</c> [b display timestamp].</param>
        /// <returns></returns>
        public static string DirectoryTable(this DirectoryInfo diRootDirectory, bool bDisplaySize = true, bool bDisplayTimestamp = true)
        {
            StringBuilder sb = new StringBuilder(8192);

            //sb.AppendLine( "```" );

            sb.AppendLine("**Directory {0}**", diRootDirectory.FullName);

            sb.AppendLine();

            sb.AppendLine("|Name|Type|Date|Size|");
            sb.AppendLine("|----|---:|:--:|---:|");

            foreach (DirectoryInfo diChild in from dir in diRootDirectory.GetDirectories() orderby dir.Name select dir)
            {
                sb.AppendLine("|{0}|{1}|{2}|{3}|", diChild.Name, "&lt;DIR&gt;", diChild.LastWriteTime.Fmt_DD_MM_YYYY_HH_MM_SS(), "");
            }

            foreach (FileInfo fiChild in from file in diRootDirectory.GetFiles() orderby file.Name select file)
            {
                sb.AppendLine("|{0}|{1}|{2}|{3}|", fiChild.GetFilenameWithoutExtension(), fiChild.Extension, fiChild.LastWriteTime.Fmt_DD_MM_YYYY_HH_MM_SS(), fiChild.GetFileSize());
            }


            //sb.AppendLine( "```" );

            return sb.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region Tables

        /// <summary>
        /// Converts the datatable to a markdown table.
        /// </summary>
        /// <param name="dtDataTable">The data table.</param>
        /// <returns></returns>
        public static string Table(this DataTable dtDataTable)
        {
            if (dtDataTable == null)
            {
                throw new ArgumentNullException(nameof(dtDataTable));
            }

            if (dtDataTable.Columns.Count == 0)
            {
                throw new ArgumentException("The DataTable Contains No Columns!");
            }

            if (dtDataTable.Rows.Count == 0)
            {
                throw new ArgumentException("The DataTable Contains No Rows!");
            }

            //-------------------------

            sbTemp.Clear();

            //-------------------------

            int iCounter = 0;

            foreach (DataColumn dc in dtDataTable.Columns)
            {
                if (iCounter > 0)
                {
                    sbTemp.Append('|');
                }

                sbTemp.Append(dc.ColumnName);

                iCounter++;
            }

            sbTemp.AppendLine();

            //-------------------------

            iCounter = 0;

            foreach (DataColumn dc in dtDataTable.Columns)
            {
                if (iCounter > 0)
                {
                    sbTemp.Append('|');
                }

                if (Tool.TYPETEXTALIGN.ContainsKey(dc.DataType))
                {
                    switch (Tool.TYPETEXTALIGN[dc.DataType])
                    {
                        case TextAlign.Left:
                            sbTemp.Append(":---");
                            break;

                        case TextAlign.Center:
                            sbTemp.Append(":---:");
                            break;

                        case TextAlign.Right:
                            sbTemp.Append("---:");
                            break;
                    }
                }
                else
                {
                    sbTemp.Append("---");
                }

                iCounter++;
            }

            sbTemp.AppendLine();

            //-------------------------

            foreach (DataRow dr in dtDataTable.Rows)
            {
                iCounter = 0;

                foreach (DataColumn dc in dtDataTable.Columns)
                {
                    if (iCounter > 0)
                    {
                        sbTemp.Append('|');
                    }

                    sbTemp.Append(dr[dc]);

                    iCounter++;
                }

                sbTemp.AppendLine();
            }

            //-------------------------

            return sbTemp.ToString();
        }


        /// <summary>
        /// Converts the list to a markdown table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">list</exception>
        /// <exception cref="ArgumentException">The List Contains No Data!</exception>
        public static string Table<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count == 0)
            {
                throw new ArgumentException("The List Contains No Data!");
            }

            sbTemp.Clear();

            //-------------------------

            Type t = typeof(T);

            List<PropertyInfo> lColumns = new List<PropertyInfo>(5);

            int iCounter = 0;

            foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance))
            {
                if (pi.GetCustomAttributes(typeof(NoDisplayAttribute), false).Length > 0)
                {
                    continue;
                }

                if (Tool.IGNORETYPES.Contains(pi.PropertyType.Name) == true)
                {
                    continue;
                }

                //if (iCounter > 0)
                {
                    sbTemp.Append('|');
                }

                sbTemp.Append(pi.Name);

                lColumns.Add(pi);

                iCounter++;
            }

            sbTemp.Append('|');

            sbTemp.AppendLine();

            //-------------------------

            iCounter = 0;

            foreach ( PropertyInfo pi in lColumns)
            {
                //if (iCounter > 0)
                {
                    sbTemp.Append('|');
                }

                TextAlign ta = TextAlign.Left;

                if (Tool.TYPETEXTALIGN.ContainsKey(pi.PropertyType))
                {
                    ta = Tool.TYPETEXTALIGN[pi.PropertyType];
                }

                switch (ta)
                {
                    case TextAlign.Left:
                        sbTemp.Append(":---");
                        break;

                    case TextAlign.Center:
                        sbTemp.Append(":---:");
                        break;

                    case TextAlign.Right:
                        sbTemp.Append("---:");
                        break;
                }

                iCounter++;
            }

            sbTemp.Append('|');

            sbTemp.AppendLine();

            //-------------------------

            foreach (object o in list)
            {
                if (o != null)
                {
                    iCounter = 0;

                    foreach (PropertyInfo pi in lColumns)
                    {
                        //if (iCounter > 0)
                        {
                            sbTemp.Append('|');
                        }

                        object oValue = pi.GetValue(o, null);

                        if (o != null && o != DBNull.Value)
                        {
                            sbTemp.Append(oValue);
                        }

                        iCounter++;
                    }

                    sbTemp.Append('|');

                    sbTemp.AppendLine();
                }
            }

            //-------------------------

            return sbTemp.ToString();
        }

        #endregion

    } // end static public class MarkdownExtension
}
