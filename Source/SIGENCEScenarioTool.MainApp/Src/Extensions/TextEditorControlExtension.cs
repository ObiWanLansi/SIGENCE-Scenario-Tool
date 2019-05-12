using System;
using System.Collections.Generic;

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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// To the header.
        /// </summary>
        /// <param name="tec">The tec.</param>
        /// <param name="iLevel">The i level.</param>
        public static void ToHeader(this TextEditorControl tec, int iLevel)
        {
            if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                tec.ActiveTextAreaControl.SelectionManager.ClearSelection();
            }

            tec.ActiveTextAreaControl.TextArea.Caret.Column = 0;

            tec.ActiveTextAreaControl.TextArea.InsertString(new string('#', iLevel) + " ");

            //if (tec.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            //{
            //    List<ISelection> lSelection = tec.ActiveTextAreaControl.SelectionManager.SelectionCollection;

            //    if (lSelection.Count == 1)
            //    {
            //        ISelection selection = lSelection[0];
            //        string strText = $"*{selection.SelectedText}*";
            //        tec.Document.Replace(selection.Offset, selection.Length, strText);
            //    }
            //}
            //else
            //{
            //    tec.ActiveTextAreaControl.TextArea.InsertString("*Italic Text*");
            //}
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
