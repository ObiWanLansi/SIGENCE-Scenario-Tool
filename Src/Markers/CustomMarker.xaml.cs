using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET.WindowsPresentation;



/// <summary>
/// 
/// </summary>
namespace TransmitterTool.Markers
{
    /// <summary>
    /// Interaction logic for CustomMarkerDemo.xaml
    /// </summary>
    public partial class CustomMarker
    {
        /// <summary>
        /// The popup
        /// </summary>
        private readonly Popup Popup = null;

        /// <summary>
        /// The label
        /// </summary>
        private readonly Label Label = null;

        /// <summary>
        /// The marker
        /// </summary>
        private readonly GMapMarker Marker = null;

        ///// <summary>
        ///// The main window
        ///// </summary>
        //private readonly MainWindow MainWindow = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get { return Label.Content as string; }
            set { Label.Content = value; }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMarker" /> class.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <param name="strTitle">The title.</param>
        public CustomMarker(GMapMarker marker, string strTitle)
        {
            this.InitializeComponent();

            this.Marker = marker;

            Label = new Label
            {
                Background = Brushes.Yellow,
                Foreground = Brushes.Black,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Padding = new Thickness(3),
                FontSize = 14,
                FontFamily = new FontFamily("Courier New"),
                Content = strTitle
            };

            Popup = new Popup
            {
                Placement = PlacementMode.Mouse,
                Child = Label
            };

            //this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
            //this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
            //this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
            //this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            //this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);


            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        ///// <summary>
        ///// Handles the Loaded event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //if (icon.Source.CanFreeze)
        //    //{
        //    //    icon.Source.Freeze();
        //    //}
        //}

        ///// <summary>
        ///// Handles the SizeChanged event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="SizeChangedEventArgs"/> instance containing the event data.</param>
        //private void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    Marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        //}

        ///// <summary>
        ///// Handles the MouseMove event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        //private void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
        //    {
        //        Point p = e.GetPosition(MainWindow.mcMapControl);
        //        Marker.Position = MainWindow.mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);
        //    }
        //}

        ///// <summary>
        ///// Handles the MouseLeftButtonDown event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        //private void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (!IsMouseCaptured)
        //    {
        //        Mouse.Capture(this);
        //    }
        //}

        ///// <summary>
        ///// Handles the MouseLeftButtonUp event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        //private void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (IsMouseCaptured)
        //    {
        //        Mouse.Capture(null);
        //    }
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeave event of the MarkerControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Marker.ZIndex -= 10000;
            Popup.IsOpen = false;
        }

        /// <summary>
        /// Handles the MouseEnter event of the MarkerControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Marker.ZIndex += 10000;
            Popup.IsOpen = true;
        }

    } // end public partial class CustomMarkerRed
}