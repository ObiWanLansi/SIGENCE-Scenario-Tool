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

            sfd.Title = "Save SIGINT Transmitter File";
            sfd.Filter = "SIGINT Transmitter File (*.stf)|*.stf";
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;

            ofd.Title = "Load SIGINT Transmitter File";
            ofd.Filter = "SIGINT Transmitter File (*.stf)|*.stf";
            ofd.AddExtension = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;

            //-----------------------------------------------------------------

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;

            this.Width = screen.WorkingArea.Width * 0.6666;
            this.Height= screen.WorkingArea.Height * 0.6666;
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
        /// 
        /// </summary>
        private void SetTitle()
        {
            this.Title = string.Format( "{0}{1}" , Tool.ProductTitle , CurrentFile != null ? string.Format( " [{0}]" , CurrentFile ) : "" );
        }

    } // end public partial class MainWindow
}
