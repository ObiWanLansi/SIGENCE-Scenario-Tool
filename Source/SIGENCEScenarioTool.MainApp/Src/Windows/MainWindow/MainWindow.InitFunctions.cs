using System.Net;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using SIGENCEScenarioTool.Commands;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Initializes the commands.
        /// </summary>
        private void InitCommands()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    NewFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    OpenFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SaveFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SaveAsFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    Close();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //---------------------------------------------------------------------

            CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenCheatSheet,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    OpenCheatSheet();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //---------------------------------------------------------------------

            CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    BeginCreateRFDevice();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.DeleteRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    //DeleteRFDevice();
                    DeleteRFDevices();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.MoveRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    IsDeviceMovingMode = !IsDeviceMovingMode;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.CopyRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    CopyRFDevice();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.PasteRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    PasteRFDevice();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.ExportRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    ExportRFDevices();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.ImportRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    ImportRFDevices();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.ZoomToRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    ZoomToRFDevice();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.RFDeviceQRCode,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    RFDeviceQRCode();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.ToggleDALF,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    StartedDALF = !StartedDALF;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //---------------------------------------------------------------------

            CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateScreenshot,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    CreateScreenshot();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //---------------------------------------------------------------------

            CommandBindings.Add(new CommandBinding(RegisteredCommands.SendDataUDP,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SendDataUDP();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.ReceiveDataUDP,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    IsReceiveDataUDP = !IsReceiveDataUDP;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //---------------------------------------------------------------------

            CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenSettings,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    OpenSettings();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.SyncMapAndGrid,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SyncMapAndGrid = !SyncMapAndGrid;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenScriptEditor,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    OpenScriptEditor();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenInGoogleMaps,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    OpenInGoogleMaps();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));
        }


        /// <summary>
        /// Initializes the map control.
        /// </summary>
        private void InitMapControl()
        {
            GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            mcMapControl.DragButton = MouseButton.Left;
            mcMapControl.Manager.Mode = AccessMode.ServerAndCache;
            mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            mcMapControl.ShowCenter = false;
            mcMapControl.MinZoom = 2;
            mcMapControl.MaxZoom = 22;

            RestoreInitialMapValues();

            mcMapControl.OnTileLoadStart += MapControl_OnTileLoadStart;
            mcMapControl.OnTileLoadComplete += MapControl_OnTileLoadComplete;
        }


        /// <summary>
        /// Initializes the map provider.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitMapProvider()
        {
            // Wir fügen nur die für unsere Region sinnvollen hinzu ...
            cbMapProvider.Items.Add(GMapProviders.GoogleMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleSatelliteMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleTerrainMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleHybridMap);

            cbMapProvider.Items.Add(GMapProviders.OpenStreetMap);

            cbMapProvider.Items.Add(GMapProviders.BingHybridMap);
            cbMapProvider.Items.Add(GMapProviders.BingMap);
            cbMapProvider.Items.Add(GMapProviders.BingSatelliteMap);

            cbMapProvider.Items.Add(GMapProviders.EmptyProvider);
        }


        /// <summary>
        /// Initializes the file open save dialogs.
        /// </summary>
        private void InitFileOpenSaveDialogs()
        {
            sfdSaveSIGENCEScenario.Title = "Save SIGENCE Scenario Tool File";
            sfdSaveSIGENCEScenario.Filter = "SIGINT SIGENCE Scenario Tool File (*.stf)|*.stf";
            sfdSaveSIGENCEScenario.AddExtension = true;
            sfdSaveSIGENCEScenario.CheckPathExists = true;

            //-----------------------------------------------------------------

            ofdLoadSIGENCEScenario.Title = "Load SIGENCE Scenario Tool File";
            ofdLoadSIGENCEScenario.Filter = "SIGINT SIGENCE Scenario ToolFile (*.stf)|*.stf";
            ofdLoadSIGENCEScenario.AddExtension = true;
            ofdLoadSIGENCEScenario.CheckPathExists = true;
            ofdLoadSIGENCEScenario.CheckFileExists = true;
            ofdLoadSIGENCEScenario.Multiselect = false;

            //-----------------------------------------------------------------

            sfdExportRFDevices.Title = "Export RF Devices";
            sfdExportRFDevices.Filter = "Comma Separated Values (*.csv)|*.csv|Extensible Markup Language (*.xml)|*.xml|JavaScript Object Notation (*.json)|*.json|Office Open XML File Format (*.xlsx)|*.xlsx|SQLite Database (*.sqlite)|*.sqlite";
            sfdExportRFDevices.AddExtension = true;
            sfdExportRFDevices.CheckPathExists = true;

            //-----------------------------------------------------------------

            ofdImportRFDevices.Title = "Import RF Devices";
            ofdImportRFDevices.Filter = "Office Open XML File Format (*.xlsx)|*.xlsx";
            ofdImportRFDevices.AddExtension = true;
            ofdImportRFDevices.CheckPathExists = true;
            ofdImportRFDevices.CheckFileExists = true;
            ofdImportRFDevices.Multiselect = false;

            //-----------------------------------------------------------------

            sfdSaveScreenshot.Title = "Save Screenshot";
            sfdSaveScreenshot.Filter = "Portable Network Graphics (*.png)|*.png";
            sfdSaveScreenshot.AddExtension = true;
            sfdSaveScreenshot.CheckPathExists = true;
        }

    } // end public partial class MainWindow 
}
