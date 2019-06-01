using System.Collections.Generic;
using System.Reflection;
using System.Xml;

using ICSharpCode.TextEditor.Document;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MarkdownSyntaxModeFileProvider : ISyntaxModeFileProvider
    {
        /// <summary>
        /// The syntax modes
        /// </summary>
        private readonly List<SyntaxMode> syntaxModes;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the syntax modes.
        /// </summary>
        public ICollection<SyntaxMode> SyntaxModes
        {
            get { return this.syntaxModes; }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownSyntaxModeFileProvider"/> class.
        /// </summary>
        public MarkdownSyntaxModeFileProvider()
        {
            this.syntaxModes = new List<SyntaxMode> { new SyntaxMode("Markdown.xshd", "Markdown", ".md") };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the syntax mode file.
        /// </summary>
        /// <param name="syntaxMode">The syntax mode.</param>
        /// <returns></returns>
        public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
        {
            //return new XmlTextReader(Assembly.GetEntryAssembly().GetManifestResourceStream("SIGENCEScenarioTool.Markdown.xshd"));
            return new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SIGENCEScenarioTool.Markdown.xshd"));
        }


        /// <summary>
        /// Updates the syntax mode list.
        /// </summary>
        public void UpdateSyntaxModeList()
        {
        }

    } // end sealed public class MarkdownSyntaxModeFileProvider 
}