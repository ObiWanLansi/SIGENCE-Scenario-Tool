using System.Collections.ObjectModel;

using GMap.NET.WindowsPresentation;

using Microsoft.Win32;

using TransmitterTool.ViewModels;



namespace TransmitterTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        ///
        /// </summary>
        private readonly SaveFileDialog sfd = new SaveFileDialog();

        /// <summary>
        ///
        /// </summary>
        private readonly OpenFileDialog ofd = new OpenFileDialog();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the transmitter.
        /// </summary>
        /// <value>
        /// The transmitter.
        /// </value>
        public ObservableCollection<TransmitterViewModel> TransmitterCollection { get; set; }


        /// <summary>
        /// The b creating transmitter
        /// </summary>
        private bool bCreatingTransmitter = false;

        /// <summary>
        /// Gets or sets a value indicating whether [creating transmitter].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [creating transmitter]; otherwise, <c>false</c>.
        /// </value>
        public bool CreatingTransmitter
        {
            get { return bCreatingTransmitter; }
            set
            {
                this.bCreatingTransmitter = value;

                SetMapToCreatingTransmitterMode();
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the map control.
        /// </summary>
        /// <value>
        /// The map control.
        /// </value>
        public GMapControl MapControl
        {
            get { return mcMapControl; }
        }


        /// <summary>
        /// The string current file
        /// </summary>
        private string strCurrentFile = null;

        /// <summary>
        /// Gets or sets the current file.
        /// </summary>
        /// <value>
        /// The current file.
        /// </value>
        public string CurrentFile
        {
            get { return strCurrentFile; }
            set
            {
                this.strCurrentFile = value;
                SetTitle();
                FirePropertyChanged();
            }
        }

    } // end public partial class MainWindow
}
