using System.Windows.Input;



namespace SIGENCEScenarioTool.Commands
{
    /// <summary>
    /// 
    /// </summary>
    static public class RegisteredCommands
    {
        /// <summary>
        /// Gets the open cheat sheet.
        /// </summary>
        /// <value>
        /// The open cheat sheet.
        /// </value>
        static public RoutedUICommand OpenCheatSheet { get; private set; }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the create RFDevice.
        /// </summary>
        /// <value>
        /// The create RFDevice.
        /// </value>
        static public RoutedUICommand CreateRFDevice { get; private set; }

        /// <summary>
        /// Gets the delete RFDevice.
        /// </summary>
        /// <value>
        /// The delete RFDevice.
        /// </value>
        static public RoutedUICommand DeleteRFDevice { get; private set; }

        /// <summary>
        /// Gets the move rf device.
        /// </summary>
        /// <value>
        /// The move rf device.
        /// </value>
        static public RoutedUICommand MoveRFDevice { get; private set; }

        /// <summary>
        /// Gets the copy rf device.
        /// </summary>
        /// <value>
        /// The copy rf device.
        /// </value>
        static public RoutedUICommand CopyRFDevice { get; private set; }

        /// <summary>
        /// Gets the paste rf device.
        /// </summary>
        /// <value>
        /// The paste rf device.
        /// </value>
        static public RoutedUICommand PasteRFDevice { get; private set; }

        /// <summary>
        /// Gets the export RFDevice.
        /// </summary>
        /// <value>
        /// The export RFDevice.
        /// </value>
        static public RoutedUICommand ExportRFDevice { get; private set; }


        /// <summary>
        /// Gets the import RFDevice.
        /// </summary>
        /// <value>
        /// The import RFDevice.
        /// </value>
        static public RoutedUICommand ImportRFDevice { get; private set; }

        //---------------------------------------------------------------------


        /// <summary>
        /// Gets the create screenshot.
        /// </summary>
        /// <value>
        /// The create screenshot.
        /// </value>
        static public RoutedUICommand CreateScreenshot { get; private set; }


        /// <summary>
        /// Gets the send data UDP.
        /// </summary>
        /// <value>
        /// The send data UDP.
        /// </value>
        static public RoutedUICommand SendDataUDP { get; private set; }

        /// <summary>
        /// Gets the receive data UDP.
        /// </summary>
        /// <value>
        /// The receive data UDP.
        /// </value>
        static public RoutedUICommand ReceiveDataUDP { get; private set; }

        /// <summary>
        /// Gets the zoom to rf device.
        /// </summary>
        /// <value>
        /// The zoom to rf device.
        /// </value>
        static public RoutedUICommand ZoomToRFDevice { get; private set; }

        /// <summary>
        /// Gets the rf device qr code.
        /// </summary>
        /// <value>
        /// The rf device qr code.
        /// </value>
        static public RoutedUICommand RFDeviceQRCode { get; private set; }

        /// <summary>
        /// Gets the open settings.
        /// </summary>
        /// <value>
        /// The open settings.
        /// </value>
        static public RoutedUICommand OpenSettings { get; private set; }


        /// <summary>
        /// Gets the synchronize map and grid.
        /// </summary>
        /// <value>
        /// The synchronize map and grid.
        /// </value>
        static public RoutedUICommand SyncMapAndGrid { get; private set; }

        /// <summary>
        /// Gets the toggle dalf.
        /// </summary>
        /// <value>
        /// The toggle dalf.
        /// </value>
        static public RoutedUICommand ToggleDALF { get; private set; }

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
            MoveRFDevice.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));

            CopyRFDevice = new RoutedUICommand("CopyRFDevice", "CopyRFDevice", typeof(RegisteredCommands));

            PasteRFDevice = new RoutedUICommand("PasteRFDevice", "PasteRFDevice", typeof(RegisteredCommands));

            ExportRFDevice = new RoutedUICommand("ExportRFDevice", "ExportRFDevice", typeof(RegisteredCommands));
            ExportRFDevice.InputGestures.Add(new KeyGesture(Key.F7));
            ExportRFDevice.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));

            ImportRFDevice = new RoutedUICommand("ImportRFDevice", "ImportRFDevice", typeof(RegisteredCommands));
            ImportRFDevice.InputGestures.Add(new KeyGesture(Key.F8));
            ImportRFDevice.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));

            ZoomToRFDevice = new RoutedUICommand("ZoomToRFDevice", "ZoomToRFDevice", typeof(RegisteredCommands));
            ZoomToRFDevice.InputGestures.Add(new KeyGesture(Key.F9));
            ZoomToRFDevice.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));

            RFDeviceQRCode = new RoutedUICommand("RFDeviceQRCode", "RFDeviceQRCode", typeof(RegisteredCommands));
            RFDeviceQRCode.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));

            //-----------------------------------------------------------------

            CreateScreenshot = new RoutedUICommand("CreateScreenshot", "CreateScreenshot", typeof(RegisteredCommands));
            CreateScreenshot.InputGestures.Add(new KeyGesture(Key.F10));
            CreateScreenshot.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));

            // F11 is reserved for fullscreen ...

            SendDataUDP = new RoutedUICommand("SendDataUDP", "SendDataUDP", typeof(RegisteredCommands));
            SendDataUDP.InputGestures.Add(new KeyGesture(Key.F12));
            //SendDataUDP.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Control));

            ReceiveDataUDP = new RoutedUICommand("ReceiveDataUDP", "ReceiveDataUDP", typeof(RegisteredCommands));
            ReceiveDataUDP.InputGestures.Add(new KeyGesture(Key.F12, ModifierKeys.Control));
            //ReceiveDataUDP.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Control));

            //-----------------------------------------------------------------

            OpenSettings = new RoutedUICommand("OpenSettings", "OpenSettings", typeof(RegisteredCommands));
            OpenSettings.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));

            SyncMapAndGrid = new RoutedUICommand("SyncMapAndGrid", "SyncMapAndGrid", typeof(RegisteredCommands));
            SyncMapAndGrid.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));

            ToggleDALF = new RoutedUICommand("ToggleDALF", "ToggleDALF", typeof(RegisteredCommands));
            ToggleDALF.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));
        }

    } // end static public class RegisteredCommands
}
