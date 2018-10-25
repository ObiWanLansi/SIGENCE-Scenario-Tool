using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using ICSharpCode.TextEditor.Document;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;
// ReSharper disable ExplicitCallerInfoArgument



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Handles the Loaded event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainWindow_Loaded( object sender, RoutedEventArgs e )
        {
            this.Left = this.settings.LastLeft;
            this.Top = this.settings.LastTop;
            this.Width = this.settings.LastWidth >= this.MinWidth ? this.settings.LastWidth : this.MinWidth;
            this.Height = this.settings.LastHeight >= this.MinHeight ? this.settings.LastHeight : this.MinHeight;

            try
            {
                this.WindowState = this.settings.LastWindowState.IsNotEmpty() ? (WindowState)Enum.Parse( typeof( WindowState ), this.settings.LastWindowState, true ) : this.WindowState;
            }
            catch(Exception)
            {
            }
        }


        /// <summary>
        /// Handles the Closed event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainWindow_Closed( object sender, EventArgs e )
        {
            this.settings.LastLeft = this.Left;
            this.settings.LastTop = this.Top;
            this.settings.LastWidth = this.Width;
            this.settings.LastHeight = this.Height;
            this.settings.LastWindowState = this.WindowState.ToString();

            this.settings.Save();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the SelectionChanged event of the TabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void TabControl_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if(e.OriginalSource == this.tcTabControl)
            {
                if(this.tiValidation.IsSelected == true)
                {
                    ExecuteValidateScenario();
                }

                e.Handled = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeftButtonDown event of the McMapControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MapControl_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            if(this.CreatingRFDevice == true)
            {
                Point p = e.GetPosition( this.mcMapControl );

                PointLatLng pll = this.mcMapControl.FromLocalToLatLng( (int)p.X, (int)p.Y );

                AddRFDevice( pll, DeviceSource.User, true );

                EndCreateRFDevice();

                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the MouseRightButtonDown event of the MapControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MapControl_MouseRightButtonDown( object sender, MouseButtonEventArgs e )
        {
            if(this.StartedDALF == true)
            {
                Point p = e.GetPosition( this.mcMapControl );

                PointLatLng pll = this.mcMapControl.FromLocalToLatLng( (int)p.X, (int)p.Y );

                if(this.mrDALF == null)
                {
                    var list = new List<PointLatLng>( 2 );

                    if(this.dvmLastSelectedDevice != null)
                    {
                        list.Add( new PointLatLng( this.dvmLastSelectedDevice.Latitude, this.dvmLastSelectedDevice.Longitude ) );
                    }
                    else
                    {
                        MB.Warning( "Strange ..." );
                    }

                    list.Add( pll );

                    this.mrDALF = new GMapRoute( list );
                }
                else
                {
                    this.mrDALF.Points.Add( pll );
                }

                this.mcMapControl.Markers.Remove( this.mrDALF );
                this.mcMapControl.Markers.Add( this.mrDALF );

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
            this.IsTileLoading = true;
        }


        /// <summary>
        /// Maps the control on tile load complete.
        /// </summary>
        /// <param name="ElapsedMilliseconds">The elapsed milliseconds.</param>
        private void MapControl_OnTileLoadComplete( long ElapsedMilliseconds )
        {
            this.IsTileLoading = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        /// <summary>
        /// Handles the PreviewKeyDown event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        private void DataGrid_PreviewKeyDown( object sender, KeyEventArgs e )
        {
            if(this.bDataGridInEditMode == true)
            {
                return;
            }

            if(e.Key == Key.Insert)
            {
                AddRFDevice( this.mcMapControl.Position, DeviceSource.User, true );

                e.Handled = true;
                return;

            }

            if(e.Key == Key.Delete)
            {
                DeleteRFDevices();

                e.Handled = true;
                return;
            }
        }


        /// <summary>
        /// Handles the KeyDown event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        private void DataGrid_KeyDown( object sender, KeyEventArgs e )
        {
            if(this.bDataGridInEditMode == true)
            {
                return;
            }

            if(e.Key == Key.Space)
            {
                foreach(RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                {
                    x.IsMarked = !x.IsMarked;
                }

                e.Handled = true;
                return;
            }

            if(e.Key == Key.Add)
            {
                foreach(RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                {
                    x.IsMarked = true;
                }

                e.Handled = true;
                return;
            }

            if(e.Key == Key.Subtract)
            {
                foreach(RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                {
                    x.IsMarked = false;
                }

                e.Handled = true;
                return;
            }
        }


        /// <summary>
        /// Handles the BeginningEdit event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridBeginningEditEventArgs"/> instance containing the event data.</param>
        private void DataGrid_BeginningEdit( object sender, DataGridBeginningEditEventArgs e )
        {
            this.bDataGridInEditMode = true;
        }


        /// <summary>
        /// Handles the CellEditEnding event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridCellEditEndingEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CellEditEnding( object sender, DataGridCellEditEndingEventArgs e )
        {
            this.bDataGridInEditMode = false;
        }


        /// <summary>
        /// Handles the SelectionChanged event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void DataGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            e.Handled = true;

            if(this.bNoFlashBack == true)
            {
                return;
            }

            foreach(var item in e.AddedItems)
            {
                if(item is RFDeviceViewModel)
                {
                    (item as RFDeviceViewModel).IsSelected = true;

                    if(this.SyncMapAndGrid == true)
                    {
                        ZoomToRFDevice( (item as RFDeviceViewModel).RFDevice, false );
                    }
                }
            }

            foreach(var item in e.RemovedItems)
            {
                if(item is RFDeviceViewModel)
                {
                    (item as RFDeviceViewModel).IsSelected = false;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Copy event of the DataGrid_CommandBinding_CanExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CommandBinding_CanExecute_Copy( object sender, CanExecuteRoutedEventArgs e )
        {
            // For the first step we'll return every time true ...
            e.CanExecute = true;
        }


        /// <summary>
        /// Handles the Paste event of the DataGrid_CommandBinding_CanExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CommandBinding_CanExecute_Paste( object sender, CanExecuteRoutedEventArgs e )
        {
            // For the first step we'll return every time true ...
            e.CanExecute = true;
        }


        /// <summary>
        /// Handles the Copy event of the DataGrid_Execute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_Execute_Copy( object sender, ExecutedRoutedEventArgs e )
        {
            CopyRFDevice();
        }


        /// <summary>
        /// Handles the Paste event of the DataGrid_Execute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_Execute_Paste( object sender, ExecutedRoutedEventArgs e )
        {
            PasteRFDevice();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseDoubleClick event of the DataGridGeoData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DataGridGeoData_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            GeoNode gn = (sender as DataGrid).SelectedItem as GeoNode;

            JumpToGeoNode( gn );

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the MenuItem_RestoreInitialMapValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_RestoreInitialMapValues_Click( object sender, RoutedEventArgs e )
        {
            RestoreInitialMapValues();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_SaveCurrentCenter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_SaveInitialMapValues_Click( object sender, RoutedEventArgs e )
        {
            SaveInitialMapValues();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_CreateSomeRandomizedRFDevices_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_CreateSomeRandomizedRFDevices_Click( object sender, RoutedEventArgs e )
        {
            CreateRandomizedRFDevices( int.Parse( (sender as MenuItem).Tag as string ) );

            e.Handled = true;
        }



        /// <summary>
        /// Handles the Click event of the MenuItem_About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_About_Click( object sender, RoutedEventArgs e )
        {
            Tools.Windows.OpenWebAdress( "https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/" );

            e.Handled = true;
        }

        /// <summary>
        /// Handles the Click event of the MenuItem_OpenIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_OpenIssues_Click( object sender, RoutedEventArgs e )
        {
            Tools.Windows.OpenWebAdress( "https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/issues/" );

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_KanbanBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_KanbanBoard_Click( object sender, RoutedEventArgs e )
        {
            Tools.Windows.OpenWebAdress( "https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/projects/1?fullscreen=true" );

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_OpenWiki control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_OpenWiki_Click( object sender, RoutedEventArgs e )
        {
            Tools.Windows.OpenWebAdress( "https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/wiki" );

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the Button_ClearDebugOutput control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        //private void Button_ClearDebugOutput_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DebugOutput = "";

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the Click event of the Button_Acknowledge control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        //private void Button_Acknowledge_Click(object sender, RoutedEventArgs e)
        //{
        //    this.ReceivedData = false;

        //    e.Handled = true;
        //}


        /// <summary>
        /// Handles the Click event of the ToogleButton_EditScenarioDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ToogleButton_EditScenarioDescription_Click( object sender, RoutedEventArgs e )
        {
            bool bSwitchToEdit = (sender as ToggleButton).IsChecked ?? false;

            if(bSwitchToEdit == true)
            {
                this.tecScenarioDescription.Text = this.ScenarioDescription;
            }
            else
            {
                this.ScenarioDescription = this.tecScenarioDescription.Text;
            }

            this.ScenarioDescriptionEditMode = bSwitchToEdit;

            //UpdateScenarioDescription();

            e.Handled = true;
        }



        /// <summary>
        /// Handles the Click event of the Button_HtmlHelp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_DocumentationHelp_Click( object sender, RoutedEventArgs e )
        {
            Tools.Windows.OpenWebAdress( "https://www.w3schools.com/html/default.asp" );
            //Tools.Windows.OpenWebAdress("https://guides.github.com/features/mastering-markdown/");

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_InsertScenarioDescriptionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_InsertScenarioDescriptionTemplate_Click( object sender, RoutedEventArgs e )
        {
            InsertScenarioDescriptionTemplate();

            this.tecScenarioDescription.Focus();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_HtmlConvertGermanUmlauts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_HtmlConvertGermanUmlauts_Click( object sender, RoutedEventArgs e )
        {
            if(string.IsNullOrEmpty( this.tecScenarioDescription.Text ) == false)
            {
                this.tecScenarioDescription.Text = this.tecScenarioDescription.Text.ReplaceHtml( true );
            }

            this.tecScenarioDescription.Focus();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_HtmlConvertToCapitalize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_HtmlConvertToCapitalize_Click( object sender, RoutedEventArgs e )
        {
            if(this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if(lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    this.tecScenarioDescription.Document.Replace( selection.Offset, selection.Length, selection.SelectedText.Capitalize() );

                }
            }

            this.tecScenarioDescription.Focus();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_HtmlConvertToLowerText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_HtmlConvertToLowerText_Click( object sender, RoutedEventArgs e )
        {
            if(this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if(lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    this.tecScenarioDescription.Document.Replace( selection.Offset, selection.Length, selection.SelectedText.ToLower() );

                }
            }

            this.tecScenarioDescription.Focus();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_HtmlConvertToUpperText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_HtmlConvertToUpperText_Click( object sender, RoutedEventArgs e )
        {
            if(this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                List<ISelection> lSelection = this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.SelectionCollection;

                if(lSelection.Count == 1)
                {
                    ISelection selection = lSelection[0];
                    this.tecScenarioDescription.Document.Replace( selection.Offset, selection.Length, selection.SelectedText.ToUpper() );

                }
            }

            this.tecScenarioDescription.Focus();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_ScenarioReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_ScenarioReport_Click( object sender, RoutedEventArgs e )
        {
            CreateScenarioReport();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_InsertHtmlSnippet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_InsertHtmlSnippet_Click( object sender, RoutedEventArgs e )
        {
            InsertHtmlSnippet( (sender as Control).Tag as string );

            this.tecScenarioDescription.Focus();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_ExecuteValidateScenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_ExecuteValidateScenario_Click( object sender, RoutedEventArgs e )
        {
            ExecuteValidateScenario();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_ClearScenarioValidation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_ClearScenarioValidation_Click( object sender, RoutedEventArgs e )
        {
            ClearScenarioValidation();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_resetAllFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_ResetAllDeviceFilter_Click( object sender, RoutedEventArgs e )
        {
            ResetAllDeviceFilter();

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Devices the view model on selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        private void DeviceViewModel_OnSelectionChanged( object sender, bool bIsSelected )
        {
            // Mark Or Unmark The Device in the DataGrid And Scroll To It If it is neccary ...

            if(bIsSelected == true)
            {
                this.bNoFlashBack = true;

                this.dgRFDevices.SelectedItems.Clear();
                this.dgRFDevices.SelectedItems.Add( sender );

                // Bei allen anderen die Selection aufheben ...
                foreach(RFDeviceViewModel model in this.RFDeviceViewModelCollection)
                {
                    if(model != sender)
                    {
                        model.IsSelected = false;
                    }
                }

                if(this.SyncMapAndGrid == true)
                {
                    this.dgRFDevices.ScrollIntoView( sender );
                    this.dgRFDevices.Focus();
                }

                this.bNoFlashBack = false;
            }
            else
            {
                foreach(var item in this.dgRFDevices.SelectedItems)
                {
                    if(item == sender)
                    {
                        this.dgRFDevices.SelectedItems.Remove( item );
                        break;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the KeyDown event of the ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        private void ComboBox_QuickCommand_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.Key != Key.Enter)
            {
                return;
            }

            QuickCommandAction();
            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseDoubleClick event of the DataGridScenarioValidation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DataGridScenarioValidation_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            object item = (sender as DataGrid).SelectedItem;

            if(item is ValidationResultViewModel)
            {
                if((item as ValidationResultViewModel).Result.Source is RFDevice source)
                {
                    this.tiMap.IsSelected = true;

                    foreach(RFDeviceViewModel model in this.RFDeviceViewModelCollection)
                    {
                        if(model.RFDevice == source)
                        {
                            this.dgRFDevices.ScrollIntoView( model );
                            this.dgRFDevices.SelectedItem = model;
                            this.dgRFDevices.Focus();
                        }
                    }
                }
            }

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
        private void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( strPropertyName ) );
        }

    } // end public partial class MainWindow
}
