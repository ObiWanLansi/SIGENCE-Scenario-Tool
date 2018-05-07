using System.Windows.Input;



namespace TransmitterTool.Commands
{
    /// <summary>
    /// 
    /// </summary>
    static public class RegisteredCommands
    {

        /// <summary>
        /// Gets the create transmitter.
        /// </summary>
        /// <value>
        /// The create transmitter.
        /// </value>
        static public RoutedUICommand CreateTransmitter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        static public RoutedUICommand DeleteTransmitter { get; private set; }

        /// <summary>
        /// Gets the export transmitter.
        /// </summary>
        /// <value>
        /// The export transmitter.
        /// </value>
        static public RoutedUICommand ExportTransmitter { get; private set; }


        /// <summary>
        /// Gets the import transmitter.
        /// </summary>
        /// <value>
        /// The import transmitter.
        /// </value>
        static public RoutedUICommand ImportTransmitter { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        static public RoutedUICommand CreateScreenshot { get; private set; }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the <see cref="RegisteredCommands"/> class.
        /// </summary>
        static RegisteredCommands()
        {
            CreateTransmitter = new RoutedUICommand( "CreateTransmitter" , "CreateTransmitter" , typeof( RegisteredCommands ) );
            CreateTransmitter.InputGestures.Add( new KeyGesture( Key.C , ModifierKeys.Alt ) );

            DeleteTransmitter = new RoutedUICommand( "DeleteTransmitter" , "DeleteTransmitter" , typeof( RegisteredCommands ) );
            DeleteTransmitter.InputGestures.Add( new KeyGesture( Key.D , ModifierKeys.Alt ) );

            ExportTransmitter = new RoutedUICommand( "ExportTransmitter" , "ExportTransmitter" , typeof( RegisteredCommands ) );
            ExportTransmitter.InputGestures.Add( new KeyGesture( Key.E , ModifierKeys.Control ) );

            ImportTransmitter = new RoutedUICommand( "ImportTransmitter" , "ImportTransmitter" , typeof( RegisteredCommands ) );
            ImportTransmitter.InputGestures.Add( new KeyGesture( Key.I , ModifierKeys.Control ) );

            CreateScreenshot = new RoutedUICommand( "CreateScreenshot" , "CreateScreenshot" , typeof( RegisteredCommands ) );
            //CreateScreenshot.InputGestures.Add( new KeyGesture( Key.I , ModifierKeys.Control ) );
        }

    } // end static public class RegisteredCommands
}
