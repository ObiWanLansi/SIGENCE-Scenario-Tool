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
        /// Gets the export transmitter.
        /// </summary>
        /// <value>
        /// The export transmitter.
        /// </value>
        static public RoutedUICommand ExportTransmitter { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the <see cref="RegisteredCommands"/> class.
        /// </summary>
        static RegisteredCommands()
        {
            CreateTransmitter = new RoutedUICommand("CreateTransmitter", "CreateTransmitter", typeof(RegisteredCommands));
            CreateTransmitter.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));

            ExportTransmitter = new RoutedUICommand("ExportTransmitter", "ExportTransmitter", typeof(RegisteredCommands));
            ExportTransmitter.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
        }

    } // end static public class RegisteredCommands
}
