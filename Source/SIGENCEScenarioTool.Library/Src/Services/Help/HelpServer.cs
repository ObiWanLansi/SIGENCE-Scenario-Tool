using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using Ceen;
using Ceen.Httpd;

using Markdig;
using Markdig.SyntaxHighlighting;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Services.Help
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Ceen.IHttpModule" />
    public sealed class HelpServer : IHttpModule
    {

        /// <summary>
        /// The TCS
        /// </summary>
        private CancellationTokenSource tcs = null;

        /// <summary>
        /// The task
        /// </summary>
        private Task task = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        static public uint Port { get; set; } = 1510;

        /// <summary>
        /// Gets or sets the help path.
        /// </summary>
        /// <value>
        /// The help path.
        /// </value>
        static public string Path { get; set; } = ".\\help";

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public IPEndPoint EndPoint { get; private set; } = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Prevents a default instance of the <see cref="HelpServer"/> class from being created.
        /// </summary>
        private HelpServer()
        {
        }


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        static public HelpServer Instance { get; } = new HelpServer();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {

            // Dann sind wir schon gestartet ...
            if( this.tcs != null )
            {
                return;
            }

            //-----------------------------------------------------------------

            // Wir erzeugen die Config jedesmal neu damit OnTheFly Änderungen eines Tages besser getracked / eingebaut werden können.
            ServerConfig config = new ServerConfig
            {
                MaxActiveRequests = 3
            };

            //-----------------------------------------------------------------

            CreateHelp();

            //-----------------------------------------------------------------

            // Der muss immer zuletzt hinzugefügt werden
            config.AddRoute("", this);

            //-----------------------------------------------------------------

            this.tcs = new CancellationTokenSource();

            // Erstmal nur auf dem Loopback lauschen ...
            this.EndPoint = new IPEndPoint(IPAddress.Loopback, (int) Port);

            this.task = HttpServer.ListenAsync(this.EndPoint, false, config, this.tcs.Token);
        }


        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if( this.tcs != null )
            {

                this.tcs.Cancel();
                this.tcs = null;
            }

            if( this.task != null )
            {
                this.task.Wait();
                this.task = null;
            }

            if( this.EndPoint != null )
            {
                this.EndPoint = null;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The mapi
        /// </summary>
        private static readonly MarkdownPipeline MAPI = new MarkdownPipelineBuilder().
                                                            UseAdvancedExtensions().
                                                            UseSyntaxHighlighting().
                                                            UseEmojiAndSmiley().
                                                            UseBootstrap().
                                                            UseAutoLinks().
                                                            //UseAutoIdentifiers().
                                                            Build();

        /// <summary>
        /// The sd help pages
        /// </summary>
        private readonly SortedDictionary<string, HelpPage> sdHelpPages = new SortedDictionary<string, HelpPage>();


        /// <summary>
        /// Creates the HTML from markdown.
        /// </summary>
        /// <param name="hp">The hp.</param>
        /// <returns></returns>
        private string CreateHTMLFromMarkdown( HelpPage hp )
        {
            string strMarkdownFilename = $"{Path}\\{hp.Document}";

            string strMarkdownContent = File.Exists(strMarkdownFilename) ? File.ReadAllText(strMarkdownFilename, Encoding.Default) : LoremIpsum.GetLoremIpsum();

            return Markdown.ToHtml(strMarkdownContent, MAPI);
        }


        /// <summary>
        /// Creates the HTML from markdown.
        /// </summary>
        /// <param name="strMarkdownContent">Content of the string markdown.</param>
        /// <returns></returns>
        private string CreateHTMLFromMarkdown( string strMarkdownContent )
        {
            return Markdown.ToHtml(strMarkdownContent, MAPI);
        }


        /// <summary>
        /// Adds the main page.
        /// </summary>
        /// <param name="hc">The hc.</param>
        private void AddMainPage( HelpConfig hc )
        {
            //string strMarkdownFilename = $"{Path}\\{hc.MainPage.Document}";

            //string strMarkdownContent = File.Exists(strMarkdownFilename) ? File.ReadAllText(strMarkdownFilename, Encoding.Default) : LoremIpsum.GetLoremIpsum();

            string strTemplateContent = File.ReadAllText(@"O:\SIGENCE-Scenario-Tool\Documentation\Help\main.html", Encoding.Default);

            //---------------------------------------------------------------------------------------------------------

            StringBuilder sbBTN = new StringBuilder(8192);
            StringBuilder sbTAB = new StringBuilder(8192);

            sbBTN.Append($"<button class=\"tablink\" onclick=\"openPage('{hc.MainPage.Caption}', this, 'gray')\" id=\"defaultOpen\">{hc.MainPage.Caption}</button>\n");
            sbTAB.Append($"<div id=\"Home\" class=\"tabcontent\">{CreateHTMLFromMarkdown(hc.MainPage)}</div>\n");

            foreach( HelpPage hp in hc.Pages )
            {
                // Wenn beide leer sind können wir nix weiter machen
                if( hp.Validate() == false )
                {
                    continue;
                }

                sbBTN.Append($"<button class=\"tablink\" onclick=\"openPage('{hp.Caption}', this, 'gray')\">{hp.Caption}</button>\n");
                //sbTAB.Append($"<div id=\"{hp.Caption}\" class=\"tabcontent\"><iframe src=\"./{hp.Document}\" /></div>\n");
                sbTAB.Append($"<div id=\"{hp.Caption}\" class=\"tabcontent\">{CreateHTMLFromMarkdown(hp)}</div>\n");
            }

            //---------------------------------------------------------------------------------------------------------

            try
            {
                StringBuilder sbDependencies = new StringBuilder(8192);

                sbDependencies.AppendLine("# Dependencies");
                sbDependencies.AppendLine();
                sbDependencies.AppendLine("|Package|Version|Link|");
                sbDependencies.AppendLine("|:------|:------|:---|");
                string strDependencyFile = @"O:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.Library\packages.config";

                XDocument xdoc = XDocument.Load(strDependencyFile);

                foreach( XElement package in xdoc.Root.Elements("package") )
                {
                    string strPackageName = package.Attribute("id").Value;
                    string strPackageVersion = package.Attribute("version").Value;
                    sbDependencies.AppendLine($"|{strPackageName}|{strPackageVersion}|https://www.nuget.org/packages/{strPackageName}/|");
                }

                sbBTN.Append($"<button class=\"tablink\" onclick=\"openPage('dependencies', this, 'gray')\" id=\"defaultOpen\">Dependencies</button>\n");
                sbTAB.Append($"<div id=\"dependencies\" class=\"tabcontent\">{CreateHTMLFromMarkdown(sbDependencies.ToString())}</div>\n");
            }
            catch( Exception )
            {
            }

            //---------------------------------------------------------------------------------------------------------

            try
            {
                string strReleaseNotes = ".\\ReleaseNotes\\ReleaseNotes.Markdown.md";
                string strMarkdownContent = File.ReadAllText(strReleaseNotes);
                sbBTN.Append($"<button class=\"tablink\" onclick=\"openPage('ReleaseNotes', this, 'gray')\" id=\"defaultOpen\">Release Notes</button>\n");
                sbTAB.Append($"<div id=\"ReleaseNotes\" class=\"tabcontent\">{CreateHTMLFromMarkdown(strMarkdownContent)}</div>\n");
            }
            catch( Exception )
            {
            }

            //---------------------------------------------------------------------------------------------------------

            hc.MainPage.HtmlContent = strTemplateContent.Replace("$BUTTONS$", sbBTN.ToString()).Replace("$TABS$", sbTAB.ToString());

            this.sdHelpPages.Add("/", hc.MainPage);
        }


        /// <summary>
        /// Creates the help.
        /// </summary>
        [Conditional("DEBUG")]
        private void CreateHelp()
        {
            this.sdHelpPages.Clear();

            if( Path.IsEmpty() )
            {
                //TODO: Warning or Error ?
                return;
            }

            if( Path.StartsWith(".\\") )
            {
                Path = string.Format("{0}\\{1}", Tools.Tool.StartupPath, Path);
            }

            if( Directory.Exists(Path) == false )
            {
                //TODO: Warning or Error ?
                return;
            }


            HelpConfig hc = HelpConfigFactory.LoadConfig(Path);

            if( hc == null )
            {
                //TODO: Warning or Error ?
                return;
            }

            if( hc.MainPage.Validate() == false )
            {
                //TODO: Warning or Error ?
                return;
            }


            AddMainPage(hc);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The allowed file types
        /// </summary>
        private static readonly HashSet<string> ALLOWED_FILE_TYPES = new HashSet<string> { ".jpg", ".png", ".gif" };


        /// <summary>
        /// Handles the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> HandleAsync( IHttpContext context )
        {
            IHttpRequest request = context.Request;

            if( this.sdHelpPages.ContainsKey(request.Path) )
            {
                await context.Response.WriteAllAsync(sdHelpPages [request.Path].HtmlContent);
            }
            else
            {
                bool bHandled = false;
                //TODO Jetzt erstmal schauen ob es vielleicht eine Resource (.jpg, ...) ist die sogar im Pfad liegt...

                string strPath = request.Path.ToLower();

                foreach( string strExtension in ALLOWED_FILE_TYPES )
                {
                    if( strPath.EndsWith(strExtension) )
                    {
                        // TODO: Wir müssen natürlich noch checken ob der Pfad gegen Manieren (zB, ..\..\ oder C:\ - so könnte der Request ausbrechen)
                        string strFullFilename = $"{Path}\\{strPath}";

                        await context.Response.WriteAllAsync(File.ReadAllBytes(strFullFilename));
                        bHandled = true;

                        break;
                    }
                }

                if( !bHandled )
                {
                    //TODO 404 ( mal als embedded resource machen ...)
                    await context.Response.WriteAllAsync("<h1>vierhundertvier (404)</h1>");
                }
            }

            return true;
        }

    } // end public sealed class HelpServer
}
