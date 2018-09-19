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
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
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

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    LoadFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
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

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs,
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

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenCheatSheet,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateRFDevice,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.DeleteRFDevice,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.MoveRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    this.IsDeviceMovingMode = !this.IsDeviceMovingMode;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.CopyRFDevice,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.PasteRFDevice,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ExportRFDevice,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ImportRFDevice,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ZoomToRFDevice,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.RFDeviceQRCode,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ToggleDALF,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    this.StartedDALF = !this.StartedDALF;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //---------------------------------------------------------------------

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateScreenshot,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.SendDataUDP,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ReceiveDataUDP,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    this.IsReceiveDataUDP = !this.IsReceiveDataUDP;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //---------------------------------------------------------------------

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenSettings,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.SyncMapAndGrid,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    this.SyncMapAndGrid = !this.SyncMapAndGrid;
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenScriptEditor,
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

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenInGoogleMaps,
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

            this.mcMapControl.DragButton = MouseButton.Left;
            this.mcMapControl.Manager.Mode = AccessMode.ServerAndCache;
            this.mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            this.mcMapControl.ShowCenter = false;
            this.mcMapControl.MinZoom = 2;
            this.mcMapControl.MaxZoom = 22;

            RestoreInitialMapValues();

            this.mcMapControl.OnTileLoadStart += MapControl_OnTileLoadStart;
            this.mcMapControl.OnTileLoadComplete += MapControl_OnTileLoadComplete;
        }


        /// <summary>
        /// Initializes the map provider.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitMapProvider()
        {
            // Wir fügen nur die für unsere Region sinnvollen hinzu ...
            this.cbMapProvider.Items.Add(GMapProviders.GoogleMap);
            this.cbMapProvider.Items.Add(GMapProviders.GoogleSatelliteMap);
            this.cbMapProvider.Items.Add(GMapProviders.GoogleTerrainMap);
            this.cbMapProvider.Items.Add(GMapProviders.GoogleHybridMap);

            this.cbMapProvider.Items.Add(GMapProviders.OpenStreetMap);

            this.cbMapProvider.Items.Add(GMapProviders.BingHybridMap);
            this.cbMapProvider.Items.Add(GMapProviders.BingMap);
            this.cbMapProvider.Items.Add(GMapProviders.BingSatelliteMap);

            this.cbMapProvider.Items.Add(GMapProviders.EmptyProvider);
        }


        /// <summary>
        /// Initializes the file open save dialogs.
        /// </summary>
        private void InitFileOpenSaveDialogs()
        {
            this.sfdSaveSIGENCEScenario.Title = "Save SIGENCE Scenario Tool File";
            this.sfdSaveSIGENCEScenario.Filter = "SIGINT SIGENCE Scenario Tool File (*.stf)|*.stf";
            this.sfdSaveSIGENCEScenario.AddExtension = true;
            this.sfdSaveSIGENCEScenario.CheckPathExists = true;

            //-----------------------------------------------------------------

            this.ofdLoadSIGENCEScenario.Title = "Load SIGENCE Scenario Tool File";
            this.ofdLoadSIGENCEScenario.Filter = "SIGINT SIGENCE Scenario ToolFile (*.stf)|*.stf";
            this.ofdLoadSIGENCEScenario.AddExtension = true;
            this.ofdLoadSIGENCEScenario.CheckPathExists = true;
            this.ofdLoadSIGENCEScenario.CheckFileExists = true;
            this.ofdLoadSIGENCEScenario.Multiselect = false;

            //-----------------------------------------------------------------

            this.sfdExportRFDevices.Title = "Export RF Devices";
            //sfdExportRFDevices.Filter = "Comma Separated Values (*.csv)|*.csv|Extensible Markup Language (*.xml)|*.xml|JavaScript Object Notation (*.json)|*.json|Office Open XML File Format (*.xlsx)|*.xlsx|SQLite Database (*.sqlite)|*.sqlite";
            this.sfdExportRFDevices.Filter = "Office Open XML File Format (*.xlsx)|*.xlsx|Comma Separated Values (*.csv)|*.csv|Extensible Markup Language (*.xml)|*.xml";
            this.sfdExportRFDevices.AddExtension = true;
            this.sfdExportRFDevices.CheckPathExists = true;

            //-----------------------------------------------------------------

            this.ofdImportRFDevices.Title = "Import RF Devices";
            this.ofdImportRFDevices.Filter = "Office Open XML File Format (*.xlsx)|*.xlsx";
            this.ofdImportRFDevices.AddExtension = true;
            this.ofdImportRFDevices.CheckPathExists = true;
            this.ofdImportRFDevices.CheckFileExists = true;
            this.ofdImportRFDevices.Multiselect = false;

            //-----------------------------------------------------------------

            this.sfdSaveScreenshot.Title = "Save Screenshot";
            this.sfdSaveScreenshot.Filter = "Portable Network Graphics (*.png)|*.png";
            this.sfdSaveScreenshot.AddExtension = true;
            this.sfdSaveScreenshot.CheckPathExists = true;
        }

    } // end public partial class MainWindow 
}
