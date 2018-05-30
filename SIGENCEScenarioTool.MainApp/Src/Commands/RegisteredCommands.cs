using System.Windows.Input;



namespace SIGENCEScenarioTool.Commands
{
    /// <summary>
    /// 
    /// </summary>
    static public class RegisteredCommands
    {

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


        /// <summary>
        /// Gets the create screenshot.
        /// </summary>
        /// <value>
        /// The create screenshot.
        /// </value>
        static public RoutedUICommand CreateScreenshot { get; private set; }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the <see cref="RegisteredCommands"/> class.
        /// </summary>
        static RegisteredCommands()
        {
            CreateRFDevice = new RoutedUICommand( "CreateRFDevice" , "CreateRFDevice" , typeof(RegisteredCommands));
            CreateRFDevice.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

            DeleteRFDevice = new RoutedUICommand( "DeleteRFDevice" , "DeleteRFDevice" , typeof(RegisteredCommands));
            DeleteRFDevice.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));

            ExportRFDevice = new RoutedUICommand( "ExportRFDevice" , "ExportRFDevice" , typeof(RegisteredCommands));
            ExportRFDevice.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));

            ImportRFDevice = new RoutedUICommand( "ImportRFDevice" , "ImportRFDevice" , typeof(RegisteredCommands));
            ImportRFDevice.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));

            CreateScreenshot = new RoutedUICommand("CreateScreenshot", "CreateScreenshot", typeof(RegisteredCommands));
            //CreateScreenshot.InputGestures.Add( new KeyGesture( Key.I , ModifierKeys.Control ) );
        }

    } // end static public class RegisteredCommands
}
