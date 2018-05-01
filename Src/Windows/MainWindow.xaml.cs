using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

using Microsoft.Win32;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

using TransmitterTool.Commands;
using TransmitterTool.Extensions;
using TransmitterTool.Markers;
using TransmitterTool.Models;
using TransmitterTool.Tools;
using TransmitterTool.ViewModels;
using System.Windows.Controls;

namespace TransmitterTool.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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

#if DEBUG
            //WindowState = WindowState.Maximized;
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the commands.
        /// </summary>
        private void InitCommands()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    NewFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    OpenFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SaveFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SaveAsFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    Close();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateTransmitter,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    BeginCreateTransmitter();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            //CommandBindings.Add(new CommandBinding(RegisteredCommands.ExportTransmitter,
            //    (object sender, ExecutedRoutedEventArgs e) =>
            //    {
            //        ExportTransmitter();
            //        e.Handled = true;
            //    },
            //    (object sender, CanExecuteRoutedEventArgs e) =>
            //    {
            //        e.CanExecute = true;
            //    }
            //));
        }


        /// <summary>
        /// Initializes the map control.
        /// </summary>
        private void InitMapControl()
        {
            mcMapControl.DragButton = MouseButton.Left;
            mcMapControl.MapProvider = GMapProviders.OpenStreetMap;
            mcMapControl.Manager.Mode = AccessMode.ServerAndCache;

            mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            mcMapControl.ShowCenter = false;
            mcMapControl.MinZoom = 2;
            mcMapControl.MaxZoom = 24;

            mcMapControl.Position = new PointLatLng(49.761471, 6.650053);
            mcMapControl.Zoom = 14;

            mcMapControl.MouseLeftButtonDown += McMapControl_MouseLeftButtonDown;
        }


        /// <summary>
        /// Initializes the map provider.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitMapProvider()
        {
            // Wir fügen nur die für unsere Region sinnvollen hinzu ...
            cbMapProvider.Items.Add(GMapProviders.OpenStreetMap);

            cbMapProvider.Items.Add(GMapProviders.GoogleHybridMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleSatelliteMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleTerrainMap);

            cbMapProvider.Items.Add(GMapProviders.BingHybridMap);
            cbMapProvider.Items.Add(GMapProviders.BingMap);
            cbMapProvider.Items.Add(GMapProviders.BingSatelliteMap);
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
            this.Title = string.Format("{0}{1}", Tool.ProductTitle, CurrentFile != null ? string.Format(" [{0}]", CurrentFile) : "");
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// News the file.
        /// </summary>
        private void NewFile()
        {
            Reset();
        }


        /// <summary>
        /// Opens the file.
        /// </summary>
        private void OpenFile()
        {
            if (ofd.ShowDialog() == true)
            {
                Reset();
                CurrentFile = ofd.FileName;

                try
                {
                    XDocument xdoc = XDocument.Load(CurrentFile);

                    foreach (XElement e in xdoc.Root.Elements())
                    {
                        AddTransmitter(Transmitter.FromXml(e));
                    }
                }
                catch (Exception ex)
                {
                    MB.Error(ex);
                }
            }
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        private void SaveFile()
        {
            if (CurrentFile == null)
            {
                if (sfd.ShowDialog() == true)
                {
                    CurrentFile = sfd.FileName;
                }
                else
                {
                    return;
                }
            }

            try
            {
                XElement eTransmitter = new XElement("TransmitterCollection");

                foreach (Transmitter t in from transmitter in TransmitterCollection select transmitter.Transmitter)
                {
                    eTransmitter.Add(t.ToXml());
                }

                eTransmitter.SaveDefault(CurrentFile);
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }
        }


        /// <summary>
        /// Saves with different filename.
        /// </summary>
        private void SaveAsFile()
        {
            CurrentFile = null;

            SaveFile();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sets the map to creating transmitter mode.
        /// </summary>
        private void SetMapToCreatingTransmitterMode()
        {
            mcMapControl.DragButton = bCreatingTransmitter ? MouseButton.Right : MouseButton.Left;
        }


        /// <summary>
        /// Begins the create transmitter.
        /// </summary>
        private void BeginCreateTransmitter()
        {
            CreatingTransmitter = true;
            mcMapControl.Cursor = Cursors.Cross;
        }


        /// <summary>
        /// Ends the create transmitter.
        /// </summary>
        private void EndCreateTransmitter()
        {
            mcMapControl.Cursor = Cursors.Arrow;
            CreatingTransmitter = false;
        }


        /// <summary>
        /// Creates the transmitter.
        /// </summary>
        /// <param name="pll">The PLL.</param>
        private void AddTransmitter(PointLatLng pll)
        {
            GMapMarker currentMarker = new GMapMarker(pll)
            {
                Shape = new Cross(),
                Offset = new Point(-15, -15),
                ZIndex = int.MaxValue
            };

            mcMapControl.Markers.Add(currentMarker);

            Transmitter t = new Transmitter
            {
                Latitude = pll.Lat,
                Longitude = pll.Lng
            };

            TransmitterCollection.Add(new TransmitterViewModel(t));
        }


        /// <summary>
        /// Adds the transmitter.
        /// </summary>
        /// <param name="t">The t.</param>
        private void AddTransmitter(Transmitter t)
        {
            GMapMarker currentMarker = new GMapMarker(new PointLatLng(t.Latitude, t.Longitude))
            {
                Shape = new Cross(),
                Offset = new Point(-15, -15),
                ZIndex = int.MaxValue
            };

            mcMapControl.Markers.Add(currentMarker);

            TransmitterCollection.Add(new TransmitterViewModel(t));
        }


        /// <summary>
        /// Exports the transmitter.
        /// </summary>
        private void ExportTransmitter()
        {
            MB.NotYetImplemented();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeftButtonDown event of the McMapControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void McMapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CreatingTransmitter == true)
            {
                Point p = e.GetPosition(mcMapControl);

                PointLatLng pll = mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                AddTransmitter(pll);

                EndCreateTransmitter();

                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TransmitterViewModel item = (sender as DataGrid).SelectedItem as TransmitterViewModel;

            mcMapControl.Position = new PointLatLng(item.Transmitter.Latitude, item.Transmitter.Longitude);
            mcMapControl.Zoom = 20;

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        protected void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

    } // end public partial class MainWindow
}
