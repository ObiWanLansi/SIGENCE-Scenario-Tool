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

        //---------------------------------------------------------------------


        /// <summary>
        /// Gets the create screenshot.
        /// </summary>
        /// <value>
        /// The create screenshot.
        /// </value>
        public static RoutedUICommand CreateScreenshot { get; private set; }


        /// <summary>
        /// Gets the send data UDP.
        /// </summary>
        /// <value>
        /// The send data UDP.
        /// </value>
        public static RoutedUICommand SendDataUDP { get; private set; }

        /// <summary>
        /// Gets the receive data UDP.
        /// </summary>
        /// <value>
        /// The receive data UDP.
        /// </value>
        public static RoutedUICommand ReceiveDataUDP { get; private set; }

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
        public static RoutedUICommand OpenSettings { get; private set; }


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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the <see cref="RegisteredCommands"/> class.
        /// </summary>
        static RegisteredCommands()
        {
            OpenCheatSheet = new RoutedUICommand("OpenCheatSheet", "OpenCheatSheet", typeof(RegisteredCommands));
            OpenCheatSheet.InputGestures.Add(new KeyGesture(Key.F1));

            //-----------------------------------------------------------------

            CreateRFDevice = new RoutedUICommand("CreateRFDevice", "CreateRFDevice", typeof(RegisteredCommands));
            CreateRFDevice.InputGestures.Add(new KeyGesture(Key.F5));
            CreateRFDevice.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

            DeleteRFDevice = new RoutedUICommand("DeleteRFDevice", "DeleteRFDevice", typeof(RegisteredCommands));
            DeleteRFDevice.InputGestures.Add(new KeyGesture(Key.F6));
            DeleteRFDevice.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));

            MoveRFDevice = new RoutedUICommand("MoveRFDevice", "MoveRFDevice", typeof(RegisteredCommands));
            MoveRFDevice.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Alt));

            CopyRFDevice = new RoutedUICommand("CopyRFDevice", "CopyRFDevice", typeof(RegisteredCommands));

            PasteRFDevice = new RoutedUICommand("PasteRFDevice", "PasteRFDevice", typeof(RegisteredCommands));

            ExportRFDevice = new RoutedUICommand("ExportRFDevice", "ExportRFDevice", typeof(RegisteredCommands));
            ExportRFDevice.InputGestures.Add(new KeyGesture(Key.F7));
            ExportRFDevice.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));

            ImportRFDevice = new RoutedUICommand("ImportRFDevice", "ImportRFDevice", typeof(RegisteredCommands));
            ImportRFDevice.InputGestures.Add(new KeyGesture(Key.F8));
            ImportRFDevice.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));

            ZoomToRFDevice = new RoutedUICommand("ZoomToRFDevice", "ZoomToRFDevice", typeof(RegisteredCommands));
            //ZoomToRFDevice.InputGestures.Add(new KeyGesture(Key.F9));
            ZoomToRFDevice.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));

            RFDeviceQRCode = new RoutedUICommand("RFDeviceQRCode", "RFDeviceQRCode", typeof(RegisteredCommands));
            RFDeviceQRCode.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));

            //-----------------------------------------------------------------

            SendDataUDP = new RoutedUICommand("SendDataUDP", "SendDataUDP", typeof(RegisteredCommands));
            SendDataUDP.InputGestures.Add(new KeyGesture(Key.F9));
            //SendDataUDP.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Control));

            ReceiveDataUDP = new RoutedUICommand("ReceiveDataUDP", "ReceiveDataUDP", typeof(RegisteredCommands));
            ReceiveDataUDP.InputGestures.Add(new KeyGesture(Key.F10));
            //ReceiveDataUDP.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Control));

            // F11 is reserved for fullscreen ...

            //-----------------------------------------------------------------

            CreateScreenshot = new RoutedUICommand("CreateScreenshot", "CreateScreenshot", typeof(RegisteredCommands));
            CreateScreenshot.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));

            OpenSettings = new RoutedUICommand("OpenSettings", "OpenSettings", typeof(RegisteredCommands));
            OpenSettings.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));

            SyncMapAndGrid = new RoutedUICommand("SyncMapAndGrid", "SyncMapAndGrid", typeof(RegisteredCommands));
            SyncMapAndGrid.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));

            ToggleDALF = new RoutedUICommand("ToggleDALF", "ToggleDALF", typeof(RegisteredCommands));
            ToggleDALF.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));

            OpenScriptEditor = new RoutedUICommand("OpenScriptEditor", "OpenScriptEditor", typeof(RegisteredCommands));
            OpenScriptEditor.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));

            OpenInGoogleMaps = new RoutedUICommand("OpenInGoogleMaps", "OpenInGoogleMaps", typeof(RegisteredCommands));
            OpenInGoogleMaps.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
        }

    } // end static public class RegisteredCommands
}
