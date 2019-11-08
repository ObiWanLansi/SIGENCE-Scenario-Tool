using System;
using System.Windows.Input;



namespace SIGENCEScenarioTool.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegisteredCommands
    {
        /// <summary>
        /// Gets the open cheat sheet.
        /// </summary>
        /// <value>
        /// The open cheat sheet.
        /// </value>
        public static RoutedUICommand OpenCheatSheet { get; private set; }

        //---------------------------------------------------------------------


        /// <summary>
        /// Gets the create RFDevice.
        /// </summary>
        /// <value>
        /// The create RFDevice.
        /// </value>
        public static RoutedUICommand CreateRFDevice { get; private set; }

        /// <summary>
        /// Gets the delete RFDevice.
        /// </summary>
        /// <value>
        /// The delete RFDevice.
        /// </value>
        public static RoutedUICommand DeleteRFDevice { get; private set; }

        /// <summary>
        /// Gets the move rf device.
        /// </summary>
        /// <value>
        /// The move rf device.
        /// </value>
        public static RoutedUICommand MoveRFDevice { get; private set; }

        /// <summary>
        /// Gets the copy rf device.
        /// </summary>
        /// <value>
        /// The copy rf device.
        /// </value>
        public static RoutedUICommand CopyRFDevice { get; private set; }

        /// <summary>
        /// Gets the paste rf device.
        /// </summary>
        /// <value>
        /// The paste rf device.
        /// </value>
        public static RoutedUICommand PasteRFDevice { get; private set; }

        /// <summary>
        /// Gets the export RFDevice.
        /// </summary>
        /// <value>
        /// The export RFDevice.
        /// </value>
        public static RoutedUICommand ExportRFDevice { get; private set; }


        /// <summary>
        /// Gets the import RFDevice.
        /// </summary>
        /// <value>
        /// The import RFDevice.
        /// </value>
        public static RoutedUICommand ImportRFDevice { get; private set; }

        /// <summary>
        /// Gets the edit rf device.
        /// </summary>
        /// <value>
        /// The edit rf device.
        /// </value>
        public static RoutedUICommand EditRFDevice { get; private set; }

        //---------------------------------------------------------------------


        /// <summary>
        /// Gets the create screenshot.
        /// </summary>
        /// <value>
        /// The create screenshot.
        /// </value>
        public static RoutedUICommand CreateScreenshot { get; private set; }


        ///// <summary>
        ///// Gets the send data UDP.
        ///// </summary>
        ///// <value>
        ///// The send data UDP.
        ///// </value>
        //public static RoutedUICommand SendDataUDP { get; private set; }

        ///// <summary>
        ///// Gets the receive data UDP.
        ///// </summary>
        ///// <value>
        ///// The receive data UDP.
        ///// </value>
        //public static RoutedUICommand ReceiveDataUDP { get; private set; }

        /// <summary>
        /// Gets the zoom to rf device.
        /// </summary>
        /// <value>
        /// The zoom to rf device.
        /// </value>
        public static RoutedUICommand ZoomToRFDevice { get; private set; }

        /// <summary>
        /// Gets the rf device qr code.
        /// </summary>
        /// <value>
        /// The rf device qr code.
        /// </value>
        public static RoutedUICommand RFDeviceQRCode { get; private set; }

        /// <summary>
        /// Gets the open settings.
        /// </summary>
        /// <value>
        /// The open settings.
        /// </value>
        public static RoutedUICommand EditSettings { get; private set; }


        /// <summary>
        /// Gets the synchronize map and grid.
        /// </summary>
        /// <value>
        /// The synchronize map and grid.
        /// </value>
        public static RoutedUICommand SyncMapAndGrid { get; private set; }

        /// <summary>
        /// Gets the toggle dalf.
        /// </summary>
        /// <value>
        /// The toggle dalf.
        /// </value>
        public static RoutedUICommand ToggleDALF { get; private set; }

        /// <summary>
        /// Gets the toggle information window.
        /// </summary>
        /// <value>
        /// The toggle information window.
        /// </value>
        public static RoutedUICommand ToggleInfoWindow { get; private set; }

        ///// <summary>
        ///// Gets the toggle data grid.
        ///// </summary>
        ///// <value>
        ///// The toggle data grid.
        ///// </value>
        //public static RoutedUICommand ToggleDataGrid { get; private set; }

        /// <summary>
        /// Gets the open script editor.
        /// </summary>
        /// <value>
        /// The open script editor.
        /// </value>
        public static RoutedUICommand OpenScriptEditor { get; private set; }

        /// <summary>
        /// Gets the open in google maps.
        /// </summary>
        /// <value>
        /// The open in google maps.
        /// </value>
        public static RoutedUICommand OpenInGoogleMaps { get; private set; }

        /// <summary>
        /// Gets the view device map.
        /// </summary>
        /// <value>
        /// The view device map.
        /// </value>
        public static RoutedUICommand ViewDeviceMap { get; private set; }

        /// <summary>
        /// Gets the view description hypertext.
        /// </summary>
        /// <value>
        /// The view description hypertext.
        /// </value>
        public static RoutedUICommand ViewDescriptionHypertext { get; private set; }

        /// <summary>
        /// Gets the edit description markdown.
        /// </summary>
        /// <value>
        /// The edit description markdown.
        /// </value>
        public static RoutedUICommand EditDescriptionMarkdown { get; private set; }

        /// <summary>
        /// Gets the edit description stylesheet.
        /// </summary>
        /// <value>
        /// The edit description stylesheet.
        /// </value>
        public static RoutedUICommand EditDescriptionStylesheet { get; private set; }

        /// <summary>
        /// Gets the view validation.
        /// </summary>
        /// <value>
        /// The view validation.
        /// </value>
        public static RoutedUICommand ViewValidationResults { get; private set; }

        /// <summary>
        /// Gets the mark devices with the same value.
        /// </summary>
        /// <value>
        /// The mark devices with the same value.
        /// </value>
        public static RoutedUICommand MarkDevicesWithTheSameValue { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the <see cref="RegisteredCommands"/> class.
        /// </summary>
        static RegisteredCommands()
        {
            Func<RoutedUICommand, RoutedUICommand> CreateCommand = (name) => new RoutedUICommand(nameof(name), nameof(name), typeof(RegisteredCommands));

            OpenCheatSheet = CreateCommand(OpenCheatSheet);
            OpenCheatSheet.InputGestures.Add(new KeyGesture(Key.F1));

            //-----------------------------------------------------------------

            CopyRFDevice = CreateCommand(CopyRFDevice);
            CopyRFDevice.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));

            PasteRFDevice = CreateCommand(PasteRFDevice);
            PasteRFDevice.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));

            CreateRFDevice = CreateCommand(CreateRFDevice);
            CreateRFDevice.InputGestures.Add(new KeyGesture(Key.F5));
            CreateRFDevice.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

            DeleteRFDevice = CreateCommand(DeleteRFDevice);
            DeleteRFDevice.InputGestures.Add(new KeyGesture(Key.F6));
            DeleteRFDevice.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));

            MoveRFDevice = CreateCommand(MoveRFDevice);
            MoveRFDevice.InputGestures.Add(new KeyGesture(Key.F7));
            MoveRFDevice.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Alt));

            EditRFDevice = CreateCommand(EditRFDevice);
            EditRFDevice.InputGestures.Add(new KeyGesture(Key.F8));
            EditRFDevice.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));

            ExportRFDevice = CreateCommand(ExportRFDevice);
            ExportRFDevice.InputGestures.Add(new KeyGesture(Key.F9));

            ImportRFDevice = CreateCommand(ImportRFDevice);
            ImportRFDevice.InputGestures.Add(new KeyGesture(Key.F10));

            ZoomToRFDevice = CreateCommand(ZoomToRFDevice);
            ZoomToRFDevice.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));

            RFDeviceQRCode = CreateCommand(RFDeviceQRCode);
            RFDeviceQRCode.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));

            //-----------------------------------------------------------------

            //SendDataUDP = CreateCommand("SendDataUDP", "SendDataUDP", typeof(RegisteredCommands));
            //SendDataUDP.InputGestures.Add(new KeyGesture(Key.F9));

            //ReceiveDataUDP = CreateCommand("ReceiveDataUDP", "ReceiveDataUDP", typeof(RegisteredCommands));
            //ReceiveDataUDP.InputGestures.Add(new KeyGesture(Key.F10));

            // F11 is reserved for fullscreen ...

            ToggleInfoWindow = CreateCommand(ToggleInfoWindow);
            ToggleInfoWindow.InputGestures.Add(new KeyGesture(Key.F12));

            //-----------------------------------------------------------------

            CreateScreenshot = CreateCommand(CreateScreenshot);
            CreateScreenshot.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));

            EditSettings = CreateCommand(EditSettings);
            EditSettings.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));

            SyncMapAndGrid = CreateCommand(SyncMapAndGrid);
            SyncMapAndGrid.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));

            ToggleDALF = CreateCommand(ToggleDALF);
            ToggleDALF.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));

            OpenScriptEditor = CreateCommand(OpenScriptEditor);
            OpenScriptEditor.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));

            OpenInGoogleMaps = CreateCommand(OpenInGoogleMaps);
            OpenInGoogleMaps.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));

            //-----------------------------------------------------------------

            ViewDeviceMap = CreateCommand(ViewDeviceMap);
            ViewDeviceMap.InputGestures.Add(new KeyGesture(Key.F5, ModifierKeys.Shift));

            ViewDescriptionHypertext = CreateCommand(ViewDescriptionHypertext);
            ViewDescriptionHypertext.InputGestures.Add(new KeyGesture(Key.F6, ModifierKeys.Shift));

            EditDescriptionMarkdown = CreateCommand(EditDescriptionMarkdown);
            EditDescriptionMarkdown.InputGestures.Add(new KeyGesture(Key.F7, ModifierKeys.Shift));

            EditDescriptionStylesheet = CreateCommand(EditDescriptionStylesheet);
            EditDescriptionStylesheet.InputGestures.Add(new KeyGesture(Key.F8, ModifierKeys.Shift));

            ViewValidationResults = CreateCommand(ViewValidationResults);
            ViewValidationResults.InputGestures.Add(new KeyGesture(Key.F9, ModifierKeys.Shift));

            MarkDevicesWithTheSameValue = CreateCommand(MarkDevicesWithTheSameValue);
            MarkDevicesWithTheSameValue.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Shift | ModifierKeys.Control));
        }

    } // end static public class RegisteredCommands
}
