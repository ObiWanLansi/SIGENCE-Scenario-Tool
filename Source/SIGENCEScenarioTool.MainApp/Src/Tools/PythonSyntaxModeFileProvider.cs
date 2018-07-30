using System.Collections.Generic;
using System.Reflection;
using System.Xml;

using ICSharpCode.TextEditor.Document;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    sealed class PythonSyntaxModeFileProvider : ISyntaxModeFileProvider
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
            get { return syntaxModes; }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="PythonSyntaxModeFileProvider"/> class.
        /// </summary>
        public PythonSyntaxModeFileProvider()
        {
            syntaxModes = new List<SyntaxMode> { new SyntaxMode("Python.xshd", "Python", ".py") };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the syntax mode file.
        /// </summary>
        /// <param name="syntaxMode">The syntax mode.</param>
        /// <returns></returns>
        public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
        {
            return new XmlTextReader(Assembly.GetEntryAssembly().GetManifestResourceStream("SIGENCEScenarioTool.Python.xshd"));
        }


        /// <summary>
        /// Updates the syntax mode list.
        /// </summary>
        public void UpdateSyntaxModeList()
        {
        }

    } // end sealed class PythonSyntaxModeFileProvider
}