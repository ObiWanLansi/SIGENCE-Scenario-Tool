using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SIGENCEScenarioTool.Extensions;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SIGENCEScenarioTool.Services.Help
{

    /// <summary>
    /// 
    /// </summary>
    public sealed class HelpPage
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


        /// <summary>
        /// Gets or sets the content of the HTML.
        /// </summary>
        /// <value>
        /// The content of the HTML.
        /// </value>
        //[JsonIgnore]
        [YamlIgnore]
        public string HtmlContent { get; set; } = null;// = "";


        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            // Wenn beide leer sind können wir nix weiter machen
            if( this.Document.IsEmpty() && this.Caption.IsEmpty() )
            {
                return false;
            }

            // Wenn der Documentname leer ist nehmen wir die Caption + .md
            if( this.Document.IsEmpty() )
            {
                this.Document = this.Caption + ".md";
            }

            // Wenn die Caption leer ist nehmen wir das Document - .md
            if( this.Caption.IsEmpty() )
            {
                this.Caption = this.Document.Substring(0, this.Document.Length - 3);
            }

            return true;
        }


    } // end public sealed class HelpPage



    /// <summary>
    /// 
    /// </summary>
    public sealed class HelpConfig
    {

        /// <summary>
        /// Gets or sets the main page.
        /// </summary>
        /// <value>
        /// The main page.
        /// </value>
        public HelpPage MainPage { get; set; } = null;//"Index.md";

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>
        /// The pages.
        /// </value>
        public List<HelpPage> Pages { get; set; } = new List<HelpPage>();

    } // end public sealed class HelpConfig



    /// <summary>
    /// 
    /// </summary>
    public static class HelpConfigFactory
    {
        /// <summary>
        /// The confgig file
        /// </summary>
        //private const string CONFGIG_FILE_JSON = "config.json";
        private const string CONFGIG_FILE_YAML = "config.yaml";


        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="strHelpDirectory">The string help directory.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static HelpConfig LoadConfig( string strHelpDirectory )
        {
            //string strConfigFilename = string.Format("{0}\\{1}", strHelpDirectory, CONFGIG_FILE_JSON);
            //string strJsonContent = File.ReadAllText(strConfigFilename, Encoding.Default);
            //return JsonConvert.DeserializeObject<HelpConfig>(strJsonContent);

            string strConfigFilename = string.Format("{0}\\{1}", strHelpDirectory, CONFGIG_FILE_YAML);
            string strYamlContent = File.ReadAllText(strConfigFilename, Encoding.Default);
            return new DeserializerBuilder().Build().Deserialize<HelpConfig>(strYamlContent);
        }


        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="strHelpDirectory">The string help directory.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void SaveConfig( HelpConfig config, string strHelpDirectory )
        {
            //string strConfigFilenameJSON = string.Format("{0}\\{1}", strHelpDirectory, CONFGIG_FILE_JSON);
            //string strJsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);
            //File.WriteAllText(strConfigFilenameJSON, strJsonContent, Encoding.Default);

            string strConfigFilenameYAML = string.Format("{0}\\{1}", strHelpDirectory, CONFGIG_FILE_YAML);
            string strYamlContent = new SerializerBuilder().Build().Serialize(config);
            File.WriteAllText(strConfigFilenameYAML, strYamlContent, Encoding.Default);
        }


        /// <summary>
        /// Creates the template.
        /// </summary>
        /// <param name="strHelpDirectory">The string help directory.</param>
        public static void CreateTemplate( string strHelpDirectory )
        {
            HelpConfig config = new HelpConfig
            {
                MainPage = new HelpPage { Caption = "Home", Document = "Home.md" }
            };

            config.Pages.Add(new HelpPage { /*Caption = "Download", */Document = "Download.md" });
            config.Pages.Add(new HelpPage { Caption = "Install"/*, Document = "Install.md"*/ });
            config.Pages.Add(new HelpPage { Caption = "Screenshots"/*, Document = "Screenshots.md"*/ });
            config.Pages.Add(new HelpPage { Caption = "Third Party Libraries", Document = "ThirdPartyLibraries.md" });

            SaveConfig(config, strHelpDirectory);
        }

    } // end public static class HelpConfigFactory
}
