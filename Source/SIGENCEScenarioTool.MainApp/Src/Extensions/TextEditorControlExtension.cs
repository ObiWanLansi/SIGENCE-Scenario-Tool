using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Document;

using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    static public class TextEditorControlExtension
    {

        /// <summary>
        /// Toogles the special character.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ToogleSpecialCharacter(this TextEditorControl tec)
        {
            tec.ShowSpaces = !tec.ShowSpaces;
            tec.ShowTabs = !tec.ShowTabs;
            tec.ShowEOLMarkers = !tec.ShowEOLMarkers;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the upper case.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ToUpperCase(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    tec.Document.Replace(selection.Offset, selection.Length, selection.SelectedText.ToUpper());
                }
            }
        }


        /// <summary>
        /// To the lower case.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ToLowerCase(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    tec.Document.Replace(selection.Offset, selection.Length, selection.SelectedText.ToLower());
                }
            }
        }


        /// <summary>
        /// To the capitalize.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ToCapitalize(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    tec.Document.Replace(selection.Offset, selection.Length, selection.SelectedText.Capitalize());
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the bold.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ToBold(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    string strText = $"**{selection.SelectedText}**";
                    tec.Document.Replace(selection.Offset, selection.Length, strText);
                }
            }
            else
            {
                tec.ActiveTextAreaControl.TextArea.InsertString("**Bold Text**");
            }
        }


        /// <summary>
        /// To the italic.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ToItalic(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    string strText = $"*{selection.SelectedText}*";
                    tec.Document.Replace(selection.Offset, selection.Length, strText);
                }
            }
            else
            {
                tec.ActiveTextAreaControl.TextArea.InsertString("*Italic Text*");
            }
        }


        /// <summary>
        /// To the strikethrough.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ToStrikethrough(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    string strText = $"~~{selection.SelectedText}~~";
                    tec.Document.Replace(selection.Offset, selection.Length, strText);
                }
            }
            else
            {
                tec.ActiveTextAreaControl.TextArea.InsertString("*Italic Text*");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the header.
        /// </summary>
        /// <param name="tec">The tec.</param>
        /// <param name="iLevel">The i level.</param>
        public static void ToHeader(this TextEditorControl tec, int iLevel)
        {
            //if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            //{
            //    tec.ActiveTextAreaControl.SelectionManager.ClearSelection();
            //}

            //tec.ActiveTextAreaControl.TextArea.Caret.Column = 0;

            //tec.ActiveTextAreaControl.TextArea.InsertString(new string('#', iLevel) + " ");

            InsertLinePrefix(tec, new string('#', iLevel));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Inserts the lorem ipsum.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertLoremIpsum(this TextEditorControl tec)
        {
            tec.ActiveTextAreaControl.TextArea.InsertString(LoremIpsum.GetLoremIpsum());
        }


        /// <summary>
        /// Inserts the date time.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertDateTime(this TextEditorControl tec)
        {
            tec.ActiveTextAreaControl.TextArea.InsertString(DateTime.Now.Fmt_DD_MM_YYYY_HH_MM_SS());
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Inserts the table.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertTable(this TextEditorControl tec)
        {
            string strTableTemplate = "|Header 1|Header 2|Header 3|\r\n" +
                                      "|:-------|:------:|-------:|\r\n" +
                                      "|Cell 1/1|Cell 2/1|Cell 3/1|\r\n" +
                                      "|Cell 1/2|Cell 2/2|Cell 3/2|\r\n" +
                                      "|Cell 1/3|Cell 2/3|Cell 3/3|\r\n" +
                                      "|Cell 1/4|Cell 2/4|Cell 3/4|\r\n" +
                                      "\r\n";

            tec.ActiveTextAreaControl.TextArea.InsertString(strTableTemplate);
        }


        /// <summary>
        /// Inserts the image.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertImage(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    tec.Document.Replace(selection.Offset, selection.Length, $"![{selection.SelectedText}]({selection.SelectedText})");
                }
            }
            else
            {
                tec.ActiveTextAreaControl.TextArea.InsertString("![Alternative Text](Link To The Image)");
            }
        }


        /// <summary>
        /// Inserts the link.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertLink(this TextEditorControl tec)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    tec.Document.Replace(selection.Offset, selection.Length, $"[{selection.SelectedText}]({selection.SelectedText})");
                }
            }
            else
            {
                tec.ActiveTextAreaControl.TextArea.InsertString("[Caption](https://www.google.de)");
            }
        }


        /// <summary>
        /// Inserts the line prefix.
        /// </summary>
        /// <param name="tec">The tec.</param>
        /// <param name="strPrefix">The string prefix.</param>
        private static void InsertLinePrefix(this TextEditorControl tec, string strPrefix)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    StringBuilder sb = new StringBuilder();

                    foreach (string line in selection.SelectedText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        sb.AppendLine($"{strPrefix} {line}");
                    }

                    tec.Document.Replace(selection.Offset, selection.Length, sb.ToString());
                }
            }
            else
            {
                tec.ActiveTextAreaControl.TextArea.InsertString($"{strPrefix} Item 1\n{strPrefix} Item 2\n{strPrefix} Item 3\n{strPrefix} Item 4");
            }
        }


        /// <summary>
        /// Inserts the ordered list.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertOrderedList(this TextEditorControl tec)
        {
            InsertLinePrefix(tec, "1.");
        }


        /// <summary>
        /// Inserts the unordered list.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertUnorderedList(this TextEditorControl tec)
        {
            InsertLinePrefix(tec, "-");
        }


        /// <summary>
        /// Inserts the blockquote.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertBlockquote(this TextEditorControl tec)
        {
            InsertLinePrefix(tec, ">");
        }


        /// <summary>
        /// Inserts the code.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void InsertCode(this TextEditorControl tec, string strLanguage = "console")
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if (lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    tec.Document.Replace(selection.Offset, selection.Length, $"```{strLanguage}\r\n{selection.SelectedText.Trim()}\r\n```");
                }
            }
            else
            {
                tec.ActiveTextAreaControl.TextArea.InsertString($"```{strLanguage}\r\nEnter Your Text Here ...\r\n```");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Removes the trailing white spaces.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void RemoveTrailingWhiteSpaces(this TextEditorControl tec)
        {
            new RemoveTrailingWS().Execute(tec.ActiveTextAreaControl.TextArea);
        }


        /// <summary>
        /// Converts the tabs to spaces.
        /// </summary>
        /// <param name="tec">The tec.</param>
        public static void ConvertTabsToSpaces(this TextEditorControl tec)
        {
            new ConvertTabsToSpaces().Execute(tec.ActiveTextAreaControl.TextArea);
        }

    } // end static public class TextEditorControlExtension
}
