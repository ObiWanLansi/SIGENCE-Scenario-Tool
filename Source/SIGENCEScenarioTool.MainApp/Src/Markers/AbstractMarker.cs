﻿using System.ComponentModel;
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
        /// The d yaw
        /// </summary>
        private double dYaw = 0;

        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>
        /// The yaw.
        /// </value>
        public double Yaw
        {
            get => this.dYaw;
            set
            {
                this.dYaw = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The d pitch
        /// </summary>
        private double dPitch = 0;

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        public double Pitch
        {
            get => this.dPitch;
            set
            {
                this.dPitch = value;

                FirePropertyChanged();
            }
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
            get => this.label.Content as string;
            set => this.label.Content = value;
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
            get => this.bIsSelected;
            set
            {
                this.bIsSelected = value;

                this.Stroke = this.bIsSelected ? Brushes.Blue : Brushes.Black;
                this.StrokeThickness = this.bIsSelected ? 4 : 1;

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
            get => this.bStroke;
            set
            {
                this.bStroke = value;
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
            get => this.dStrokeThickness;
            set
            {
                this.dStrokeThickness = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


#if DEBUG
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMarker"/> class.
        /// </summary>
        public AbstractMarker()
        {
        }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMarker" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The mm marker.</param>
        /// <param name="strToolTip">The string tool tip.</param>
        protected AbstractMarker(GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip)
        {
            this.DataContext = this;
            this.mcMapControl = mcMapControl;
            this.mmMarker = mmMarker;

            this.label = new Label
            {
                //Background = Brushes.Yellow,
                //Foreground = Brushes.Black,
                Background = SystemColors.InfoBrush,
                Foreground = SystemColors.InfoTextBrush,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Padding = new Thickness(3),
                FontSize = 12,
                FontFamily = new FontFamily("Courier New"),
                Content = strToolTip
            };

            this.popup = new Popup
            {
                Placement = PlacementMode.Mouse,
                Child = label
            };

            this.Cursor = Cursors.Hand;

            MouseMove += MarkerControl_MouseMove;
            MouseLeftButtonUp += MarkerControl_MouseLeftButtonUp;
            MouseLeftButtonDown += MarkerControl_MouseLeftButtonDown;

            MouseEnter += MarkerControl_MouseEnter;
            MouseLeave += MarkerControl_MouseLeave;
        }


        /// <summary>
        /// Handles the MouseMove event of the CustomMarkerDemo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.IsMouseCaptured)
            {
                Point p = e.GetPosition(this.mcMapControl);

                PointLatLng pll = this.mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                this.mmMarker.Position = pll;

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
            if (this.mcMapControl.DragButton == MouseButton.Left)
            {
                this.IsSelected = !this.IsSelected;

                OnSelectionChanged?.Invoke(this, this.bIsSelected);

                e.Handled = true;
                return;
            }

            if (!this.IsMouseCaptured)
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
            if (this.IsMouseCaptured)
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
            this.mmMarker.ZIndex -= 10000;
            this.popup.IsOpen = false;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the MouseEnter event of the MarkerControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.mmMarker.ZIndex += 10000;
            this.popup.IsOpen = true;

            e.Handled = true;
        }

    } // end public abstract class AbstractMarker
}
