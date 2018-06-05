using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using GMap.NET;

using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the McMapControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MapControl_MouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            if( CreatingRFDevice == true )
            {
                Point p = e.GetPosition( mcMapControl );

                PointLatLng pll = mcMapControl.FromLocalToLatLng( ( int ) p.X , ( int ) p.Y );

                AddRFDevice( pll );

                EndCreateRFDevice();

                e.Handled = true;
            }
        }


        /// <summary>
        /// Maps the control on position changed.
        /// </summary>
        /// <param name="point">The point.</param>
        private void MapControl_OnPositionChanged( PointLatLng point )
        {
            FirePropertyChanged( "Latitude" );
            FirePropertyChanged( "Longitude" );
        }


        /// <summary>
        /// Maps the control on map zoom changed.
        /// </summary>
        private void MapControl_OnMapZoomChanged()
        {
            FirePropertyChanged( "Zoom" );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the KeyDown event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        private void DataGrid_KeyDown( object sender , KeyEventArgs e )
        {
            if( e.Key == Key.Space )
            {
                foreach( RFDeviceViewModel x in ( sender as DataGrid ).SelectedItems )
                {
                    x.IsSelected = !x.IsSelected;
                }

                e.Handled = true;
                return;
            }

            if( e.Key == Key.Add )
            {
                foreach( RFDeviceViewModel x in ( sender as DataGrid ).SelectedItems )
                {
                    x.IsSelected = true;
                }

                e.Handled = true;
                return;
            }

            if( e.Key == Key.Subtract )
            {
                foreach( RFDeviceViewModel x in ( sender as DataGrid ).SelectedItems )
                {
                    x.IsSelected = false;
                }

                e.Handled = true;
                return;
            }

            //if( e.Key == Key.Delete )
            //{
            //    DeleteRFDevices();

            //    e.Handled = true;
            //    return;
            //}
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void DataGrid_PreviewKeyDown( object sender , KeyEventArgs e )
        //{
        //    Debug.Write( "private void DataGrid_PreviewKeyDown( object sender , KeyEventArgs e )" );
        //}


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the MenuItem_CreateSomeRandomizedRFDevices_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_CreateSomeRandomizedRFDevices_Click( object sender , RoutedEventArgs e )
        {
            CreateRandomizedRFDevices( int.Parse( ( sender as MenuItem ).Tag as string ) );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        delegate void DoForegoroundEvents();

        /// <summary>
        /// Does the events.
        /// </summary>
        public static void DoEvents()
        {
            DoForegoroundEvents add = () => { }; //looks strange but it works
            Application.Current.Dispatcher.Invoke( DispatcherPriority.Background , add );
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
        protected void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( strPropertyName ) );
        }

    } // end public partial class MainWindow
}
