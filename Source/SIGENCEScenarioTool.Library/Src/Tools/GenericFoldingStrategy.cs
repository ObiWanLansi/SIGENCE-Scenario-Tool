using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor.Document;

using SIGENCEScenarioTool.Extensions;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IFoldingStrategy" />
    public sealed class GenericFoldingStrategy : IFoldingStrategy
    {
        /// <summary>
        /// The region start
        /// </summary>
        public static readonly string REGION_START = "@region";

        /// <summary>
        /// The region end
        /// </summary>
        public static readonly string REGION_END = "@endregion";

        /// <summary>
        /// The sb
        /// </summary>
        private readonly StringBuilder sb = new StringBuilder(256);

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Generates the fold markers.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="strFileName">Name of the file.</param>
        /// <param name="oParseInformation">The parse information.</param>
        /// <returns></returns>
        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string strFileName, object oParseInformation)
        {
            List<FoldMarker> lFolderMarker = new List<FoldMarker>();

            int iStartLine = -1;
            string strFoldingDescription = "";

            // Create foldmarkers for the whole document, enumerate through every line.
            for (int iCounter = 0; iCounter < document.TotalNumberOfLines; iCounter++)
            {
                LineSegment ls = document.GetLineSegment(iCounter);

                if (ls.Words != null && ls.Words.Count > 0)
                {
                    TextWord tw = ls.Words[0];

                    if (tw != null)
                    {
                        if (tw.Word.EqualsIgnoreCase(REGION_START))
                        {
                            iStartLine = iCounter;

                            //Alle Wörter dahiner autmatisch als FoldingDescription

                            this.sb.Clear();

                            for (int iWordCounter = 1; iWordCounter < ls.Words.Count; iWordCounter++)
                            {
                                string strWord = ls.Words[iWordCounter].Word;

                                if (strWord != "--" && strWord.IsEmpty() == false)
                                {
                                    this.sb.AppendFormat("{0} ", strWord);
                                }
                            }
                            strFoldingDescription = this.sb.ToString();

                            continue;
                        }

                        if (tw.Word.EqualsIgnoreCase(REGION_END) && iStartLine > -1)
                        {
                            lFolderMarker.Add(new FoldMarker(document, iStartLine, 0, iCounter, ls.TotalLength, FoldType.Region, strFoldingDescription));

                            iStartLine = -1;

                            continue;
                        }
                    }
                }
            }

            return lFolderMarker;
        }

    } // end public sealed class GenericFoldingStrategy 
}
