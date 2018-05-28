using System;
using System.IO;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;

using NUnit.Framework;



namespace TransmitterTool.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [SetUpFixture]
    sealed class TransmitterToolInit_And_Deinit
    {
        /// <summary>
        /// Logger zum Ausgeben der Protokollierung.
        /// </summary>
        static private readonly ILog Log = LogManager.GetLogger(typeof(TransmitterToolInit_And_Deinit));

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Adds the console appender.
        /// </summary>
        static private bool AddConsoleAppender()
        {
            ConsoleAppender ca = new ConsoleAppender
            {
                Layout = new PatternLayout("%date{dd.MM.yyyy HH:mm:ss,fff} [%thread] %-5level - %logger - %message %newline"),
#if DEBUG
                Threshold = Level.Debug
#else
                Threshold = Level.Info
#endif
            };

            ca.ActivateOptions();

            BasicConfigurator.Configure(ca);

            return true;
        }


        /// <summary>
        /// Adds the file appender.
        /// </summary>
        static private void AddFileAppender()
        {
            FileAppender rfa = new FileAppender
            {
                Layout = new XmlLayout(true),
                AppendToFile = false,
                File = string.Format("{0}{1}_{2}.log", Path.GetTempPath(), "TransmitterTool.NUnit", DateTime.Now.ToString("yyyyMMdd_HHmmssfff")),
                ImmediateFlush = true,
#if DEBUG
                Threshold = Level.Debug
#else
                        Threshold = Level.Info
#endif
            };

            rfa.ActivateOptions();

            BasicConfigurator.Configure(rfa);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------


        //private readonly List<string> CLEANUP_EXTENSIONS = new List<string>
        //{
        //    ".tif", ".xml", ".csv", ".meta"//, ".log"
        //};

        //private void CleanUpTemp()
        //{
        //    foreach (FileInfo fi in new DirectoryInfo(Path.GetTempPath()).EnumerateFiles())
        //    {
        //        if (CLEANUP_EXTENSIONS.Contains(fi.Extension.ToLower()))
        //        {
        //            fi.Delete();
        //        }
        //    }
        //}

        //-----------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        private DateTime dtStart;


        /// <summary>
        /// Initialisiert die gesamte TestSuite.
        /// </summary>
        [OneTimeSetUp]
        public void InitTests()
        {
            //CleanUpTemp();

            AddConsoleAppender();
            AddFileAppender();

            TransmitterToolTestCaseHelper.ValidateTestCaseInformation();

            dtStart = DateTime.Now;

            Log.InfoFormat("Tests Started @ {0}", dtStart.ToString("dd.MM.yyyy, HH:mm:ss"));
        }


        /// <summary>
        /// Deinitialisiert die gesamte TestSuite.
        /// </summary>
        [OneTimeTearDown]
        public void DeinitTests()
        {
            DateTime dtStop = DateTime.Now;

            Log.InfoFormat("Tests Stopped @ {0}", dtStop.ToString("dd.MM.yyyy, HH:mm:ss"));
            Log.InfoFormat("Total TestTime: {0} ", dtStop - dtStart);
        }

    } // end sealed class TransmitterToolInit_And_Deinit
}
