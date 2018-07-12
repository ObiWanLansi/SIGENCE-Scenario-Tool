using GMap.NET.WindowsPresentation;



/// <summary>
/// 
/// </summary>
namespace SIGENCEScenarioTool.Markers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class RectangleMarker
    {
        ///// <summary>
        ///// The popup
        ///// </summary>
        //private readonly Popup popup = null;

        ///// <summary>
        ///// The label
        ///// </summary>
        //private readonly Label label = null;

        ///// <summary>
        ///// The marker
        ///// </summary>
        //private readonly GMapMarker mmMarker = null;

        ///// <summary>
        ///// The mc map control
        ///// </summary>
        //private readonly GMapControl mcMapControl = null;

        ////-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="pll">The PLL.</param>
        //public delegate void PositionChangedHandler(object sender, PointLatLng pll);


        ///// <summary>
        ///// Occurs when [on position changed].
        ///// </summary>
        //public event PositionChangedHandler OnPositionChanged;

        ////-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Gets or sets the marker tool tip.
        ///// </summary>
        ///// <value>
        ///// The marker tool tip.
        ///// </value>
        //public string MarkerToolTip
        //{
        //    get { return label.Content as string; }
        //    set { label.Content = value; }
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleMarker" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The mm marker.</param>
        /// <param name="strToolTip">The tooltip.</param>
        public RectangleMarker( GMapControl mcMapControl , GMapMarker mmMarker , string strToolTip ) :
            base( mcMapControl , mmMarker , strToolTip )
        {
            this.InitializeComponent();

            //this.mcMapControl = mcMapControl;
            //this.mmMarker = mmMarker;

            //label = new Label
            //{
            //    Background = Brushes.Yellow,
            //    Foreground = Brushes.Black,
            //    BorderBrush = Brushes.Black,
            //    BorderThickness = new Thickness(2),
            //    Padding = new Thickness(3),
            //    FontSize = 14,
            //    FontFamily = new FontFamily("Courier New"),
            //    Content = strToolTip
            //};

            //popup = new Popup
            //{
            //    Placement = PlacementMode.Mouse,
            //    Child = label
            //};

            //this.MouseMove += new MouseEventHandler(MarkerControl_MouseMove);
            //this.MouseLeftButtonUp += new MouseButtonEventHandler(MarkerControl_MouseLeftButtonUp);
            //this.MouseLeftButtonDown += new MouseButtonEventHandler(MarkerControl_MouseLeftButtonDown);

            //this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            //this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Handles the MouseMove event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        //private void MarkerControl_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
        //    {
        //        Point p = e.GetPosition(mcMapControl);

        //        PointLatLng pll = mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y); 

        //        mmMarker.Position = pll;

        //        OnPositionChanged?.Invoke(this, pll);

        //        e.Handled = true;
        //    }
        //}


        ///// <summary>
        ///// Handles the MouseLeftButtonDown event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        //private void MarkerControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (mcMapControl.DragButton == MouseButton.Left)
        //    {
        //        return;
        //    }

        //    if (!IsMouseCaptured)
        //    {
        //        Mouse.Capture(this);

        //        e.Handled = true;
        //    }
        //}


        ///// <summary>
        ///// Handles the MouseLeftButtonUp event of the CustomMarkerDemo control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        //private void MarkerControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (IsMouseCaptured)
        //    {
        //        Mouse.Capture(null);

        //        e.Handled = true;
        //    }
        //}

        ////-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Handles the MouseLeave event of the MarkerControl control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        //private void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    mmMarker.ZIndex -= 10000;
        //    popup.IsOpen = false;

        //    e.Handled = true;
        //}

        ///// <summary>
        ///// Handles the MouseEnter event of the MarkerControl control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        //private void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    mmMarker.ZIndex += 10000;
        //    popup.IsOpen = true;

        //    e.Handled = true;
        //}

    } // end public partial class RectangleMarker
}