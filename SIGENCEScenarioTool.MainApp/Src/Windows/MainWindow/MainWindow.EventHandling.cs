using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using GMap.NET;

using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Windows.MainWindow
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


        /// <summary>
        /// Maps the control on tile load start.
        /// </summary>
        private void MapControl_OnTileLoadStart()
        {
            IsTileLoading = true;
        }


        /// <summary>
        /// Maps the control on tile load complete.
        /// </summary>
        /// <param name="ElapsedMilliseconds">The elapsed milliseconds.</param>
        private void MapControl_OnTileLoadComplete( long ElapsedMilliseconds )
        {
            IsTileLoading = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the KeyDown event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        private void DataGrid_KeyDown( object sender , KeyEventArgs e )
        {
            if( bDataGridInEditMode == true )
            {
                return;
            }

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
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_BeginningEdit( object sender , DataGridBeginningEditEventArgs e )
        {
            bDataGridInEditMode = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_CellEditEnding( object sender , DataGridCellEditEndingEventArgs e )
        {
            bDataGridInEditMode = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the MenuItem_OpenInGoogleMaps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_OpenInGoogleMaps_Click( object sender , RoutedEventArgs e )
        {
            OpenRFDeviceInGoogleMaps();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_CreateSomeRandomizedRFDevices_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_CreateSomeRandomizedRFDevices_Click( object sender , RoutedEventArgs e )
        {
            CreateRandomizedRFDevices( int.Parse( ( sender as MenuItem ).Tag as string ) );

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_ClearDebugOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_ClearDebugOutput_Click( object sender , RoutedEventArgs e )
        {
            DebugOutput = "";

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the Button_RefreshScenarioDescription control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void Button_RefreshScenarioDescription_Click(object sender, RoutedEventArgs e)
        //{
        //    UpdateScenarioDescription();

        //    e.Handled = true;
        //}


        /// <summary>
        /// Handles the Click event of the ToogleButton_EditScenarioDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ToogleButton_EditScenarioDescription_Click( object sender , RoutedEventArgs e )
        {
            ScenarioDescriptionEditMode = ( sender as ToggleButton ).IsChecked ?? false;

            UpdateScenarioDescription();

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the MenuItem_InsertHtmlSnippet control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_InsertHtmlSnippet_Click(object sender, RoutedEventArgs e)
        //{
        //    InsertHtmlSnippet((sender as Control).Tag as string);

        //    e.Handled = true;
        //}


        /// <summary>
        /// Handles the Click event of the Button_HtmlHelp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_HtmlHelp_Click( object sender , RoutedEventArgs e )
        {
            Tools.Windows.OpenWebAdress( "https://www.w3schools.com/html/default.asp" );

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_ScenarioReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_ScenarioReport_Click( object sender , RoutedEventArgs e )
        {
            CreateScenarioReport();

            e.Handled = true;
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Copy event of the DataGrid_CommandBinding_CanExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CommandBinding_CanExecute_Copy( object sender , CanExecuteRoutedEventArgs e )
        {
            // For the first step we'll return every time true ...
            e.CanExecute = true;
        }


        /// <summary>
        /// Handles the Paste event of the DataGrid_CommandBinding_CanExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CommandBinding_CanExecute_Paste( object sender , CanExecuteRoutedEventArgs e )
        {
            // For the first step we'll return every time true ...
            e.CanExecute = true;
        }


        /// <summary>
        /// Handles the Copy event of the DataGrid_Execute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_Execute_Copy( object sender , ExecutedRoutedEventArgs e )
        {
            CopyRFDevice();
        }


        /// <summary>
        /// Handles the Paste event of the DataGrid_Execute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_Execute_Paste( object sender , ExecutedRoutedEventArgs e )
        {
            PasteRFDevice();
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
