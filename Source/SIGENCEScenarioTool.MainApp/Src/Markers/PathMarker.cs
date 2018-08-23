using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Markers
{
    public class PathMarker : GMapRoute, INotifyPropertyChanged
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
        /// The mc map control
        /// </summary>
        private readonly GMapControl mcMapControl = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        protected void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            //PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( strPropertyName ) );
            OnPropertyChanged( new PropertyChangedEventArgs( strPropertyName ) );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        public delegate void SelectionChangedHandler( object sender , bool bIsSelected );

        /// <summary>
        /// Occurs when [on selection changed].
        /// </summary>
        public event SelectionChangedHandler OnSelectionChanged;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        private Highway hType;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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

                if( bIsSelected == true )
                {
                    System.Windows.Shapes.Path path = this.Shape as System.Windows.Shapes.Path;

                    path.Stroke = new SolidColorBrush( Colors.Blue );
                    path.StrokeThickness = 7;
                }
                else
                {
                    SyncHighwayType();
                }

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mcMapControl"></param>
        /// <param name="points"></param>
        /// <param name="strToolTip"></param>
        public PathMarker( GMapControl mcMapControl , IEnumerable<PointLatLng> points , Highway h , string strToolTip ) :
            base( points )
        {
            this.mcMapControl = mcMapControl;
            this.hType = h;

            label = new Label
            {
                Background = Brushes.Yellow ,
                Foreground = Brushes.Black ,
                BorderBrush = Brushes.Black ,
                BorderThickness = new Thickness( 2 ) ,
                Padding = new Thickness( 3 ) ,
                FontSize = 14 ,
                FontFamily = new FontFamily( "Courier New" ) ,
                Content = strToolTip
            };

            popup = new Popup
            {
                Placement = PlacementMode.Mouse ,
                Child = label
            };

            this.RegenerateShape( mcMapControl );

            System.Windows.Shapes.Path path = this.Shape as System.Windows.Shapes.Path;
            path.IsHitTestVisible = true;
            path.Cursor = Cursors.Cross;

            SyncHighwayType();

            this.Shape.MouseLeftButtonUp += new MouseButtonEventHandler( MarkerControl_MouseLeftButtonUp );
            this.Shape.MouseLeftButtonDown += new MouseButtonEventHandler( MarkerControl_MouseLeftButtonDown );

            this.Shape.MouseEnter += new MouseEventHandler( MarkerControl_MouseEnter );
            this.Shape.MouseLeave += new MouseEventHandler( MarkerControl_MouseLeave );
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        private void SyncHighwayType()
        {
            System.Windows.Shapes.Path path = this.Shape as System.Windows.Shapes.Path;

            switch( hType )
            {
                case Highway.Motorway:
                case Highway.Motorway_Link:
                    path.Stroke = new SolidColorBrush( Colors.Red );
                    path.StrokeThickness = 5;
                    break;

                case Highway.Trunk:
                case Highway.Trunk_Link:
                    path.Stroke = new SolidColorBrush( Colors.Orange );
                    path.StrokeThickness = 4;
                    break;

                case Highway.Primary:
                case Highway.Primary_Link:
                    path.Stroke = new SolidColorBrush( Colors.Yellow );
                    path.StrokeThickness = 3;
                    break;

                case Highway.Secondary:
                case Highway.Secondary_Link:
                    path.Stroke = new SolidColorBrush( Colors.Black );
                    path.StrokeThickness = 2;
                    break;

            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeftButtonDown event of the CustomMarkerDemo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            if( mcMapControl.DragButton == MouseButton.Left )
            {
                IsSelected = !IsSelected;

                OnSelectionChanged?.Invoke( this , bIsSelected );

                e.Handled = true;
                return;
            }

            if( !this.Shape.IsMouseCaptured )
            {
                Mouse.Capture( this.Shape );
                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the MouseLeftButtonUp event of the CustomMarkerDemo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseLeftButtonUp( object sender , MouseButtonEventArgs e )
        {
            if( this.Shape.IsMouseCaptured )
            {
                Mouse.Capture( null );

                e.Handled = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeave event of the MarkerControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseLeave( object sender , MouseEventArgs e )
        {
            ZIndex -= 10000;
            popup.IsOpen = false;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the MouseEnter event of the MarkerControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MarkerControl_MouseEnter( object sender , MouseEventArgs e )
        {
            ZIndex += 10000;
            popup.IsOpen = true;

            e.Handled = true;
        }

    } // end public class PathMarker
}
