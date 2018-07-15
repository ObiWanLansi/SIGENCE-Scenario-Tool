﻿using System;
using System.IO;
using System.Windows;

using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.Windows.MainWindow;



namespace SIGENCEScenarioTool
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs" /> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 1)
            {
                string strFilename = e.Args[0];

                if (File.Exists(strFilename))
                {
                    var window = new MainWindow();
                    window.OpenFile(strFilename);
                    window.Show();
                }
            }
            else
            {
                new MainWindow().Show();
            }
        }


        /// <summary>
        /// Handles the Exit event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.ExitEventArgs" /> instance containing the event data.</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Blink.Off();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                MB.Error(e.ExceptionObject as Exception);
            }
        }

    } // end public partial class App
}
