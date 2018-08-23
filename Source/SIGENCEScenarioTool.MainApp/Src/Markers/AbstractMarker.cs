using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET;
using GMap.NET.WindowsPresentation;



namespace SIGENCEScenarioTool.Markers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    public abstract class AbstractMarker : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// The popup
        /// </summary>
        private readonly Popup popup = null;

        /// <summary>
        /// The label
        /// </summary>
        private readonly Label label = null;

        /// <summary>
        /// The marker
        /// </summary>
        private readonly GMapMarker mmMarker = null;

        /// <summary>
        /// The mc map control
        /// </summary>
        private readonly GMapControl mcMapControl = null;

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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pll">The PLL.</param>
        public delegate void PositionChangedHandler(object sender, PointLatLng pll);


        /// <summary>
        /// Occurs when [on position changed].
        /// </summary>
        public event PositionChangedHandler OnPositionChanged;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        public delegate void SelectionChangedHandler(object sender, bool bIsSelected);

        /// <summary>
        /// Occurs when [on selection changed].
        /// </summary>
        public event SelectionChangedHandler OnSelectionChanged;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the marker tool tip.
        /// </summary>
        /// <value>
        /// The marker tool tip.
        /// </value>
        public string MarkerToolTip
        {
            get { return label.Content as string; }
            set { label.Content = value; }
        }


        /// <summary>
        /// The b is selected
        /// </summary>
        private bool bIsSelected = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get { return bIsSelected; }
            set
            {
                bIsSelected = value;

                Stroke = bIsSelected ? Brushes.Blue : Brushes.Black;
                StrokeThickness = bIsSelected ? 4 : 1;

                //OnSelectionChanged?.Invoke(this, bIsSelected);

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b stroke
        /// </summary>
        private Brush bStroke = Brushes.Black;

        /// <summary>
        /// Gets or sets the stroke.
        /// </summary>
        /// <value>
        /// The stroke.
        /// </value>
        public Brush Stroke
        {
            get { return bStroke; }
            set
            {
                bStroke = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The d stroke thickness
        /// </summary>
        private double dStrokeThickness = 1;

        /// <summary>
        /// Gets or sets the stroke thickness.
        /// </summary>
        /// <value>
        /// The stroke thickness.
        /// </value>
        public double StrokeThickness
        {
            get { return dStrokeThickness; }
            set
            {
                dStrokeThickness = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMarker" /> class.
        /// </summary>
        public AbstractMarker()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMarker" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The mm marker.</param>
        /// <param name="strToolTip">The string tool tip.</param>
        /// <param name="fDirectionAngle">The f direction angle.</param>
        public AbstractMarker(GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip)
        {
            this.DataContext = this;
            this.mcMapControl = mcMapControl;
            this.mmMarker = mmMarker;

            label = new Label
            {
                Background = Brushes.Yellow,
                Foreground = Brushes.Black,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Padding = new Thickness(3),
                FontSize = 14,
                FontFamily = new FontFamily("Courier New"),
                Content = strToolTip
            };

            popup = new Popup
            {
                Placement = PlacementMode.Mouse,
                Child = label
            };

            Cursor = Cursors.Hand;
            this.MouseMove += new MouseEventHandler(MarkerControl_MouseMove);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MarkerControl_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(MarkerControl_MouseLeftButtonDown);

            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
        }


        /// <summary>
        /// Handles the MouseMove event of the CustomMarkerDemo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
                Point p = e.GetPosition(mcMapControl);

                PointLatLng pll = mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                mmMarker.Position = pll;

                OnPositionChanged?.Invoke(this, pll);

                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the MouseLeftButtonDown event of the CustomMarkerDemo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mcMapControl.DragButton == MouseButton.Left)
            {
                IsSelected = !IsSelected;

                OnSelectionChanged?.Invoke(this, bIsSelected);

                e.Handled = true;
                return;
            }

            if (!IsMouseCaptured)
            {
                Mouse.Capture(this);
                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the MouseLeftButtonUp event of the CustomMarkerDemo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);

                e.Handled = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeave event of the MarkerControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            mmMarker.ZIndex -= 10000;
            popup.IsOpen = false;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the MouseEnter event of the MarkerControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            mmMarker.ZIndex += 10000;
            popup.IsOpen = true;

            e.Handled = true;
        }

    } // end public abstract class AbstractMarker
}
