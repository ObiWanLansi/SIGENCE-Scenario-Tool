using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Ceen;
using Ceen.Httpd;

using Markdig;

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

            //DefaultModule defaulthandler = new DefaultModule();

            //foreach( Type type in typeof(AppMonitoring).Assembly.GetTypes() )
            //{
            //    if( type.ImplementsInterface(this.tIMonitoringModule) )
            //    {
            //        IMonitoringModule module = (IMonitoringModule) Activator.CreateInstance(type);

            //        defaulthandler.AddModule(module);
            //        config.AddRoute($"/{module.GetRoute()}", module);
            //    }
            //}

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


        //        /// <summary>
        //        /// The HTML template
        //        /// </summary>
        //        private readonly string HTML_TEMPLATE =
        //@"<!DOCTYPE html>
        //<html>
        //    <head>
        //        <title>$APPLICATION_TITLE$ (Version $VERSION$) - [Help]</title>
        //        $BOOTSTRAP_LINK$
        //        <style>
        //            body { background-color: #F0FFFF; }
        //        </style>
        //        $BOOTSTRAP_SCRIPT$
        //    </head>
        //    <body>
        //        $CONTENT$
        //    </body>
        //</html>";


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

            return Markdown.ToHtml(strMarkdownContent);
        }


        /// <summary>
        /// Adds the main page.
        /// </summary>
        /// <param name="hc">The hc.</param>
        private void AddMainPage( HelpConfig hc )
        {
            string strMarkdownFilename = $"{Path}\\{hc.MainPage.Document}";

            string strMarkdownContent = File.Exists(strMarkdownFilename) ? File.ReadAllText(strMarkdownFilename, Encoding.Default) : LoremIpsum.GetLoremIpsum();

            string strTemplateContent = File.ReadAllText(@"O:\SIGENCE-Scenario-Tool\Documentation\Help\main.html", Encoding.Default);

            StringBuilder sbBTN = new StringBuilder(8192);
            StringBuilder sbTAB = new StringBuilder(8192);

            sbBTN.Append($"<button class=\"tablink\" onclick=\"openPage('{hc.MainPage.Caption}', this, 'gray')\" id=\"defaultOpen\">{hc.MainPage.Caption}</button>\n");
            //sbTAB.Append($"<div id=\"Home\" class=\"tabcontent\"><h3>Home</h3></div>\n");
            sbTAB.Append($"<div id=\"Home\" class=\"tabcontent\">{CreateHTMLFromMarkdown(hc.MainPage)}</div>\n");

            foreach( HelpPage hp in hc.Pages )
            {
                // Wenn beide leer sind können wir nix weiter machen
                if( hp.Validate() == false )
                {
                    continue;
                }

                sbBTN.Append($"<button class=\"tablink\" onclick=\"openPage('{hp.Caption}', this, 'gray')\">{hp.Caption}</button>\n");
                //sbTAB.Append($"<div id=\"{hp.Caption}\" class=\"tabcontent\"><h3>{hp.Caption}</h3></div>\n");
                //sbTAB.Append($"<div id=\"{hp.Caption}\" class=\"tabcontent\"><iframe src=\"./{hp.Document}\" /></div>\n");
                sbTAB.Append($"<div id=\"{hp.Caption}\" class=\"tabcontent\">{CreateHTMLFromMarkdown(hp)}</div>\n");
            }

            hc.MainPage.HtmlContent = strTemplateContent.Replace("$BUTTONS$", sbBTN.ToString()).Replace("$TABS$", sbTAB.ToString());

            this.sdHelpPages.Add("/", hc.MainPage);
        }

        /// <summary>
        /// Adds the page.
        /// </summary>
        /// <param name="strPath">The string path.</param>
        /// <param name="hp">The hp.</param>
        /// <returns></returns>
        private void AddHelpPage( string strPath, HelpPage hp )
        {
            //string strMarkdownFilename = $"{Path}\\{hp.Document}";

            //string strMarkdownContent = File.Exists(strMarkdownFilename) ? File.ReadAllText(strMarkdownFilename, Encoding.Default) : LoremIpsum.GetLoremIpsum();


            //string strTemplateContent = File.ReadAllText(@"O:\SIGENCE-Scenario-Tool\Documentation\Help\tabbar.html", Encoding.Default);

            #region OldStuff
            //string strHtmlContent = Markdown.ToHtml(strMarkdownContent);
            ////string strHtmlContent = this.HTML_TEMPLATE.Replace("$APPLICATION_TITLE$", Tool.ProductTitle).Replace("$VERSION$", Tool.Version);

            ////hp.HtmlContent = this.HTML_TEMPLATE.Replace("$APPLICATION_TITLE$", Tool.ProductTitle).Replace("$VERSION$", Tool.Version).Replace("$CONTENT$", $"<h1>{hp.Caption}</h1>");
            //hp.HtmlContent = this.HTML_TEMPLATE.Replace("$APPLICATION_TITLE$", Tool.ProductTitle).
            //                                    Replace("$VERSION$", Tool.Version).
            //                                    Replace("$CONTENT$", strHtmlContent).
            //                                    Replace("$BOOTSTRAP_LINK$", "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\">").
            //                                    Replace("$BOOTSTRAP_SCRIPT$", "<script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js\" integrity=\"sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI\" crossorigin=\"anonymous\"></script>");
            #endregion

            //this.sdHelpPages.Add(strPath, hp);
        }


        /// <summary>
        /// Creates the help.
        /// </summary>
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
            //AddHelpPage("/", hc.MainPage);

            //foreach( HelpPage hp in hc.Pages )
            //{
            //    // Wenn beide leer sind können wir nix weiter machen
            //    if( hp.Validate() == false )
            //    {
            //        continue;
            //    }

            //    #region OldStuff
            //    //// Wenn der Documentname leer ist nehmen wir die Caption + .md
            //    //if( hp.Document.IsEmpty() )
            //    //{
            //    //    hp.Document = hp.Caption + ".md";
            //    //}

            //    //// Wenn die Caption leer ist nehmen wir das Document - .md
            //    //if( hp.Caption.IsEmpty() )
            //    //{
            //    //    hp.Caption = hp.Document.Substring(0, hp.Document.Length - 3);
            //    //}

            //    //string strMarkdownFilename = $"{Path}\\{hp.Document}";

            //    //string strMarkdownContent = File.Exists(strMarkdownFilename) ? File.ReadAllText(strMarkdownFilename, Encoding.Default) : LoremIpsum.GetLoremIpsum();

            //    //string strHtmlContent = Markdown.ToHtml(strMarkdownContent);
            //    //string strHtmlContent = this.HTML_TEMPLATE.Replace("$APPLICATION_TITLE$", Tool.ProductTitle).Replace("$VERSION$", Tool.Version);

            //    //hp.HtmlContent = this.HTML_TEMPLATE.Replace("$APPLICATION_TITLE$", Tool.ProductTitle).Replace("$VERSION$", Tool.Version).Replace("$CONTENT$", $"<h1>{hp.Caption}</h1>");
            //    #endregion

            //    // Der Pfad ist der Dokumentname ohne .md
            //    //string strPath = hp.Document.Substring(0, hp.Document.Length - 3).ToLower();
            //    //AddHelpPage($"/{strPath}", hp);

            //    AddHelpPage($"/{hp.Document}", hp);
            //}
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
