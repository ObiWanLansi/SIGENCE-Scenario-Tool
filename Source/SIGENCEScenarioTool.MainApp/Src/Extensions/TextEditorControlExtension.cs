using System.Collections.Generic;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;



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

    } // end static public class TextEditorControlExtension
}
