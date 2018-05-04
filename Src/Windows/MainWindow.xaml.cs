using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

using TransmitterTool.Tools;
using TransmitterTool.ViewModels;



namespace TransmitterTool.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //-----------------------------------------------------------------

            this.TransmitterCollection = new ObservableCollection<TransmitterViewModel>();
            this.DataContext = this;

            //-----------------------------------------------------------------

            InitMapControl();
            InitMapProvider();
            InitCommands();

            //-----------------------------------------------------------------

            sfdSaveTransmitter.Title = "Save SIGINT Transmitter File";
            sfdSaveTransmitter.Filter = "SIGINT Transmitter File (*.stf)|*.stf";
            sfdSaveTransmitter.AddExtension = true;
            sfdSaveTransmitter.CheckPathExists = true;

            ofdLoadTransmitter.Title = "Load SIGINT Transmitter File";
            ofdLoadTransmitter.Filter = "SIGINT Transmitter File (*.stf)|*.stf";
            ofdLoadTransmitter.AddExtension = true;
            ofdLoadTransmitter.CheckPathExists = true;
            ofdLoadTransmitter.CheckFileExists = true;
            ofdLoadTransmitter.Multiselect = false;

            //-----------------------------------------------------------------

            sfdExportTransmitter.Title = "Export SIGINT Transmitter File";
            sfdExportTransmitter.Filter = "Comma Separated Values (*.csv)|*.csv|JavaScript Object Notation (*.json)|*.json";
            sfdExportTransmitter.AddExtension = true;
            sfdExportTransmitter.CheckPathExists = true;

            //-----------------------------------------------------------------

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;

            this.Width = screen.WorkingArea.Width * 0.6666;
            this.Height = screen.WorkingArea.Height * 0.6666;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            CurrentFile = null;
            TransmitterCollection.Clear();
        }


        /// <summary>
        /// Sets the title.
        /// </summary>
        private void SetTitle()
        {
            this.Title = string.Format("{0}{1}", Tool.ProductTitle, CurrentFile != null ? string.Format(" [{0}]", CurrentFile) : "");
        }

    } // end public partial class MainWindow
}
