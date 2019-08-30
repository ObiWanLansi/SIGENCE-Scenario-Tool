using System;
using System.Net;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

using SIGENCEScenarioTool.Commands;
using SIGENCEScenarioTool.Tools;

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
                (sender, e) =>
                {
                    NewFile();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
                (sender, e) =>
                {
                    LoadFile();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
                (sender, e) =>
                {
                    SaveFile();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs,
                (sender, e) =>
                {
                    SaveAsFile();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                (sender, e) =>
                {
                    Close();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            //---------------------------------------------------------------------

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenCheatSheet,
                (sender, e) =>
                {
                    OpenCheatSheet();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            //---------------------------------------------------------------------

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateRFDevice,
                (sender, e) =>
                {
                    BeginCreateRFDevice();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.DeleteRFDevice,
                (sender, e) =>
                {
                    DeleteRFDevices();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.MoveRFDevice,
                (sender, e) =>
                {
                    this.IsDeviceMovingMode = !this.IsDeviceMovingMode;
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.EditRFDevice,
                (sender, e) =>
                {
                    EditRFDevice();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.CopyRFDevice,
                (sender, e) =>
                {
                    CopyRFDevice();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.PasteRFDevice,
                (sender, e) =>
                {
                    PasteRFDevice();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ExportRFDevice,
                (sender, e) =>
                {
                    ExportRFDevices();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ImportRFDevice,
                (sender, e) =>
                {
                    ImportRFDevices();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ZoomToRFDevice,
                (sender, e) =>
                {
                    ZoomToRFDevice();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.RFDeviceQRCode,
                (sender, e) =>
                {
                    RFDeviceQRCode();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ToggleDALF,
                (sender, e) =>
                {
                    this.StartedDALF = !this.StartedDALF;
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ToggleInfoWindow,
                (sender, e) =>
                {
                    ToggleInfoWindow();

                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            //this.CommandBindings.Add( new CommandBinding( RegisteredCommands.ToggleDataGrid,
            //    ( sender, e ) =>
            //    {
            //        ToggleDataGrid();

            //        e.Handled = true;
            //    },
            //    ( sender, e ) =>
            //    {
            //        e.CanExecute = true;
            //    }
            //) );

            //---------------------------------------------------------------------

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateScreenshot,
                (sender, e) =>
                {
                    CreateScreenshot();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            //---------------------------------------------------------------------

            //this.CommandBindings.Add(new CommandBinding(RegisteredCommands.SendDataUDP,
            //    (sender, e) =>
            //    {
            //        SendDataUDP();
            //        e.Handled = true;
            //    },
            //    (sender, e) =>
            //    {
            //        e.CanExecute = true;
            //    }
            //));

            //this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ReceiveDataUDP,
            //    (sender, e) =>
            //    {
            //        this.IsReceiveDataUDP = !this.IsReceiveDataUDP;
            //        e.Handled = true;
            //    },
            //    (sender, e) =>
            //    {
            //        e.CanExecute = true;
            //    }
            //));

            //---------------------------------------------------------------------

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.EditSettings,
                (sender, e) =>
                {
                    OpenSettings();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.SyncMapAndGrid,
                (sender, e) =>
                {
                    this.SyncMapAndGrid = !this.SyncMapAndGrid;
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenScriptEditor,
                (sender, e) =>
                {
                    OpenScriptEditor();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.OpenInGoogleMaps,
                (sender, e) =>
                {
                    OpenInGoogleMaps();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            //---------------------------------------------------------------------

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ViewDeviceMap,
                (sender, e) =>
                {
                    ViewDeviceMap();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ViewDescriptionHypertext,
                (sender, e) =>
                {
                    ViewDescriptionHypertext();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.EditDescriptionMarkdown,
                (sender, e) =>
                {
                    EditDescriptionMarkdown();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.EditDescriptionStylesheet,
                (sender, e) =>
                {
                    EditDescriptionStylesheet();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.ViewValidationResults,
                (sender, e) =>
                {
                    ViewValidationResults();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
            ));

            this.CommandBindings.Add(new CommandBinding(RegisteredCommands.MarkDevicesWithTheSameValue,
                (sender, e) =>
                {
                    MarkDevicesWithTheSameValue();
                    e.Handled = true;
                },
                (sender, e) => { e.CanExecute = true; }
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
            #region Scenario Load & Save

            this.sfdSaveSIGENCEScenario.Title = "Save SIGENCE Scenario File";
            this.sfdSaveSIGENCEScenario.Filter = "SIGENCE Scenario File (*.stf)|*.stf";
            this.sfdSaveSIGENCEScenario.AddExtension = true;
            this.sfdSaveSIGENCEScenario.CheckPathExists = true;

            //---------------------------------------------

            this.ofdLoadSIGENCEScenario.Title = "Load SIGENCE Scenario File";
            this.ofdLoadSIGENCEScenario.Filter = "SIGENCE Scenario File (*.stf)|*.stf";
            this.ofdLoadSIGENCEScenario.AddExtension = true;
            this.ofdLoadSIGENCEScenario.CheckPathExists = true;
            this.ofdLoadSIGENCEScenario.CheckFileExists = true;
            this.ofdLoadSIGENCEScenario.Multiselect = false;

            #endregion

            //-------------------------------------------------------------------------------------

            #region Scenario Import & Export

            this.sfdExportSIGENCEScenario.Title = "Export SIGENCE Scenario";
            //sfdExportRFDevices.Filter = "Comma Separated Values (*.csv)|*.csv|Extensible Markup Language (*.xml)|*.xml|JavaScript Object Notation (*.json)|*.json|Office Open XML File Format (*.xlsx)|*.xlsx|SQLite Database (*.sqlite)|*.sqlite";
            this.sfdExportSIGENCEScenario.Filter = "Office Open XML File Format (*.xlsx)|*.xlsx|Comma Separated Values (*.csv)|*.csv|Extensible Markup Language (*.xml)|*.xml|JavaScript Object Notation (*.json)|*.json";
            this.sfdExportSIGENCEScenario.AddExtension = true;
            this.sfdExportSIGENCEScenario.CheckPathExists = true;

            //---------------------------------------------

            this.ofdImportSIGENCEScenario.Title = "Import SIGENCE Scenario";
            this.ofdImportSIGENCEScenario.Filter = "Office Open XML File Format (*.xlsx)|*.xlsx";
            this.ofdImportSIGENCEScenario.AddExtension = true;
            this.ofdImportSIGENCEScenario.CheckPathExists = true;
            this.ofdImportSIGENCEScenario.CheckFileExists = true;
            this.ofdImportSIGENCEScenario.Multiselect = false;

            #endregion

            //-------------------------------------------------------------------------------------

            #region Templates

            this.sfdSaveTemplates.Title = "Save SIGENCE Scenario Templates";
            this.sfdSaveTemplates.Filter = "SIGENCE Scenario Templates (*.stt)|*.stt";
            this.sfdSaveTemplates.AddExtension = true;
            this.sfdSaveTemplates.CheckPathExists = true;

            //---------------------------------------------

            this.ofdLoadTemplates.Title = "Load SIGENCE Scenario Templates";
            this.ofdLoadTemplates.Filter = "SIGENCE Scenario Templates (*.stt)|*.stt";
            this.ofdLoadTemplates.AddExtension = true;
            this.ofdLoadTemplates.CheckPathExists = true;
            this.ofdLoadTemplates.CheckFileExists = true;
            this.ofdLoadTemplates.Multiselect = false;

            #endregion

            //-------------------------------------------------------------------------------------

            #region Settings

            this.sfdExportSettings.Title = "Export SIGENCE Scenario Settings";
            this.sfdExportSettings.Filter = "SIGENCE Scenario Settings (*.sts)|*.sts";
            this.sfdExportSettings.AddExtension = true;
            this.sfdExportSettings.CheckPathExists = true;

            //---------------------------------------------

            this.ofdImportSettings.Title = "Import SIGENCE Scenario Settings";
            this.ofdImportSettings.Filter = "SIGENCE Scenario Settings (*.sts)|*.sts";
            this.ofdImportSettings.AddExtension = true;
            this.ofdImportSettings.CheckPathExists = true;
            this.ofdImportSettings.CheckFileExists = true;
            this.ofdImportSettings.Multiselect = false;

            #endregion
            //-------------------------------------------------------------------------------------

            this.sfdSaveScreenshot.Title = "Save Screenshot";
            this.sfdSaveScreenshot.Filter = "Portable Network Graphics (*.png)|*.png";
            this.sfdSaveScreenshot.AddExtension = true;
            this.sfdSaveScreenshot.CheckPathExists = true;
        }


        /// <summary>
        /// Initializes the scenario description editor.
        /// </summary>
        private void InitTextEditorControls()
        {
            Action<TextEditorControl> Init = (tec) =>
            {
                tec.HideMouseCursor = true;
                tec.LineViewerStyle = LineViewerStyle.FullRow;
                tec.ConvertTabsToSpaces = true;

                tec.ShowSpaces = false;
                tec.ShowTabs = false;
                tec.ShowEOLMarkers = false;
                tec.ShowLineNumbers = true;

                tec.IsIconBarVisible = true;
                tec.AllowCaretBeyondEOL = true;
                tec.AllowDrop = false;
                tec.VRulerRow = 120;
            };

            Init(this.tecDescriptionMarkdown);
            Init(this.tecDescriptionStyleheet);

            //---------------------------------------------

            this.tecDescriptionMarkdown.ActiveTextAreaControl.TextArea.KeyUp += TextArea_KeyUp;
            this.tecDescriptionMarkdown.ActiveTextAreaControl.TextArea.Document.DocumentChanged += Document_DescriptionMarkdown_DocumentChanged;

            this.tecDescriptionMarkdown.Document.FoldingManager.FoldingStrategy = this.gfs;
            HighlightingManager.Manager.AddSyntaxModeFileProvider(new MarkdownSyntaxModeFileProvider());
            this.tecDescriptionMarkdown.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("Markdown");

            //---------------------------------------------

            //this.tecDescriptionStyleheet.ActiveTextAreaControl.TextArea.TextChanged += TextArea_TextChanged;
            this.tecDescriptionStyleheet.ActiveTextAreaControl.TextArea.Document.DocumentChanged += Document_DescriptionStylesheet_DocumentChanged;
        }

    } // end public partial class MainWindow 
}
