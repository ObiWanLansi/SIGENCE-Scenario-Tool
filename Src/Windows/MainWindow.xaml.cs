using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

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

            sfdSaveScreenshot.Title = "Save Screenshot";
            sfdSaveScreenshot.Filter = "Portable Network Graphics (*.png)|*.png";
            sfdSaveScreenshot.AddExtension = true;
            sfdSaveScreenshot.CheckPathExists = true;

            //-----------------------------------------------------------------

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;

            this.Width = screen.WorkingArea.Width * 0.6666;
            this.Height = screen.WorkingArea.Height * 0.6666;

            SetTitle();
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
            this.Title = string.Format("{0} ({1}){2}", Tool.ProductTitle, Tool.Version, CurrentFile != null ? string.Format(" [{0}]", CurrentFile) : "");
        }


        /// <summary>
        /// Creates the screenshot.
        /// </summary>
        private void CreateScreenshot()
        {
            try
            {
                if (CurrentFile != null)
                {
                    sfdSaveScreenshot.FileName = new FileInfo(CurrentFile).Name;
                }

                if (sfdSaveScreenshot.ShowDialog() == true)
                {
                    var screenshot = Tools.Windows.GetWPFScreenshot(mcMapControl);

                    PngBitmapEncoder encoder = new PngBitmapEncoder();

                    encoder.Frames.Add(BitmapFrame.Create(screenshot));

                    using (BufferedStream bs = new BufferedStream(new FileStream(sfdSaveScreenshot.FileName, FileMode.Create)))
                    {
                        encoder.Save(bs);
                    }

                    Tools.Windows.OpenWithDefaultApplication(sfdSaveScreenshot.FileName);
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }
        }

    } // end public partial class MainWindow
}
