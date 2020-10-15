using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Ceen;
using Ceen.Httpd;

using SIGENCEScenarioTool.Extensions;



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

            this.task = HttpServer.ListenAsync(EndPoint, false, config, this.tcs.Token);
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
        /// Creates the help.
        /// </summary>
        private void CreateHelp()
        {
            if( Path.IsEmpty() )
            {
                return;
            }

            if( Path.StartsWith(".\\") )
            {
                Path = string.Format("{0}\\{1}", Tools.Tool.StartupPath, Path);
            }

            if( Directory.Exists(Path) == false )
            {
                return;
            }

            string strConfigFile = string.Format("{0}\\config.yaml", Path);

            if( File.Exists(strConfigFile) == false )
            {
                return;
            }


            string strHome = string.Format("{0}\\Home.md", Path);
            string strIndex = string.Format("{0}\\Index.md", Path);

            if( File.Exists(strHome) == false && File.Exists(strIndex) == false )
            {

            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> HandleAsync( IHttpContext context )
        {
            await context.Response.WriteAllAsync(DateTime.Now.Fmt_DD_MM_YYYY_HH_MM_SS());

            return true;
        }

    } // end public sealed class HelpServer
}
