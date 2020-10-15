using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace SIGENCEScenarioTool.Services.Help
{

    /// <summary>
    /// 
    /// </summary>
    public sealed class Page
    {
        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption { get; set; } = "";

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        public string Document { get; set; } = "";

    } // end public sealed class Page



    /// <summary>
    /// 
    /// </summary>
    public sealed class HelpConfig
    {

        /// <summary>
        /// Gets or sets the index page.
        /// </summary>
        /// <value>
        /// The index page.
        /// </value>
        public string IndexPage { get; set; } = "Index.md";

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>
        /// The pages.
        /// </value>
        public List<Page> Pages { get; set; } = new List<Page>();

    } // end public sealed class HelpConfig



    /// <summary>
    /// 
    /// </summary>
    public static class HelpConfigFactory
    {
        const string CONFGIG_FILE = "config.json";

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="strHelpDirectory">The string help directory.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static HelpConfig LoadConfig( string strHelpDirectory )
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="strHelpDirectory">The string help directory.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void SaveConfig( HelpConfig config, string strHelpDirectory )
        {
            string strJsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);
            string strOutputFilename = string.Format("{0}\\{1}", strHelpDirectory, CONFGIG_FILE);

            File.WriteAllText(strOutputFilename, strJsonContent, Encoding.GetEncoding("ISO-8859-1"));
        }


        /// <summary>
        /// Creates the template.
        /// </summary>
        /// <param name="strHelpDirectory">The string help directory.</param>
        public static void CreateTemplate( string strHelpDirectory )
        {
            HelpConfig config = new HelpConfig();

            config.Pages.Add(new Page { Caption = "Download", Document = "Download.md" });
            config.Pages.Add(new Page { Caption = "Install", Document = "Install.md" });
            config.Pages.Add(new Page { Caption = "Screenshots", Document = "Screenshots.md" });
            config.Pages.Add(new Page { Caption = "Third Party Libraries", Document = "ThirdPartyLibraries.md" });

            SaveConfig(config, strHelpDirectory);
        }

    } // end public static class HelpConfigFactory

}
