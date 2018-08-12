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

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.Database.GeoDb;
using SIGENCEScenarioTool.Tools;
using SIGENCEScenarioTool.ViewModels;



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
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = settings.LastLeft;
            this.Top = settings.LastTop;
            this.Width = settings.LastWidth >= MinWidth ? settings.LastWidth : MinWidth;
            this.Height = settings.LastHeight >= MinHeight ? settings.LastHeight : MinHeight;

            try
            {
                this.WindowState = settings.LastWindowState.IsNotEmpty() ? (WindowState)Enum.Parse(typeof(WindowState), settings.LastWindowState, true) : WindowState;
            }
            catch (Exception)
            {
            }
        }


        /// <summary>
        /// Handles the Closed event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            settings.LastLeft = this.Left;
            settings.LastTop = this.Top;
            settings.LastWidth = this.Width;
            settings.LastHeight = this.Height;
            settings.LastWindowState = this.WindowState.ToString();

            settings.Save();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseLeftButtonDown event of the McMapControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CreatingRFDevice == true)
            {
                Point p = e.GetPosition(mcMapControl);

                PointLatLng pll = mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                AddRFDevice(pll, DeviceSource.User);

                EndCreateRFDevice();

                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the MouseRightButtonDown event of the MapControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void MapControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StartedDALF == true)
            {
                Point p = e.GetPosition(mcMapControl);

                PointLatLng pll = mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                if (mrDALF == null)
                {
                    var list = new List<PointLatLng>(2);

                    if (dvmLastSelectedDevice != null)
                    {
                        list.Add(new PointLatLng(dvmLastSelectedDevice.Latitude, dvmLastSelectedDevice.Longitude));
                    }
                    else
                    {
                        MB.Warning("Strange ...");
                    }

                    list.Add(pll);

                    mrDALF = new GMapRoute(list);
                }
                else
                {
                    mrDALF.Points.Add(pll);
                }

                mcMapControl.Markers.Remove(mrDALF);
                mcMapControl.Markers.Add(mrDALF);

                e.Handled = true;
            }
        }


        /// <summary>
        /// Maps the control on position changed.
        /// </summary>
        /// <param name="point">The point.</param>
        private void MapControl_OnPositionChanged(PointLatLng point)
        {
            FirePropertyChanged("Latitude");
            FirePropertyChanged("Longitude");
        }


        /// <summary>
        /// Maps the control on map zoom changed.
        /// </summary>
        private void MapControl_OnMapZoomChanged()
        {
            FirePropertyChanged("Zoom");
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
        private void MapControl_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            IsTileLoading = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the KeyDown event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (bDataGridInEditMode == true)
            {
                return;
            }

            if (e.Key == Key.Space)
            {
                foreach (RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                {
                    x.IsMarked = !x.IsMarked;
                }

                e.Handled = true;
                return;
            }

            if (e.Key == Key.Add)
            {
                foreach (RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                {
                    x.IsMarked = true;
                }

                e.Handled = true;
                return;
            }

            if (e.Key == Key.Subtract)
            {
                foreach (RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
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
        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            bDataGridInEditMode = true;
        }


        /// <summary>
        /// Handles the CellEditEnding event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridCellEditEndingEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            bDataGridInEditMode = false;
        }


        /// <summary>
        /// Handles the SelectionChanged event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;

            if (bNoFlashBack == true)
            {
                return;
            }

            foreach (var item in e.AddedItems)
            {
                if (item is RFDeviceViewModel)
                {
                    (item as RFDeviceViewModel).IsSelected = true;

                    if (SyncMapAndGrid == true)
                    {
                        ZoomToRFDevice((item as RFDeviceViewModel).RFDevice, false);
                    }
                }
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is RFDeviceViewModel)
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
        private void DataGrid_CommandBinding_CanExecute_Copy(object sender, CanExecuteRoutedEventArgs e)
        {
            // For the first step we'll return every time true ...
            e.CanExecute = true;
        }


        /// <summary>
        /// Handles the Paste event of the DataGrid_CommandBinding_CanExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CommandBinding_CanExecute_Paste(object sender, CanExecuteRoutedEventArgs e)
        {
            // For the first step we'll return every time true ...
            e.CanExecute = true;
        }


        /// <summary>
        /// Handles the Copy event of the DataGrid_Execute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_Execute_Copy(object sender, ExecutedRoutedEventArgs e)
        {
            CopyRFDevice();
        }


        /// <summary>
        /// Handles the Paste event of the DataGrid_Execute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DataGrid_Execute_Paste(object sender, ExecutedRoutedEventArgs e)
        {
            PasteRFDevice();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseDoubleClick event of the DataGridGeoData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DataGridGeoData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GeoNode gn = (sender as DataGrid).SelectedItem as GeoNode;

            JumpToGeoNode(gn);

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the MenuItem_RestoreInitialMapValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_RestoreInitialMapValues_Click(object sender, RoutedEventArgs e)
        {
            RestoreInitialMapValues();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_SaveCurrentCenter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_SaveInitialMapValues_Click(object sender, RoutedEventArgs e)
        {
            SaveInitialMapValues();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_CreateSomeRandomizedRFDevices_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_CreateSomeRandomizedRFDevices_Click(object sender, RoutedEventArgs e)
        {
            CreateRandomizedRFDevices(int.Parse((sender as MenuItem).Tag as string));

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_OpenWiki control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_OpenWiki_Click(object sender, RoutedEventArgs e)
        {
            Tools.Windows.OpenWebAdress("https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/wiki");

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_ClearDebugOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_ClearDebugOutput_Click(object sender, RoutedEventArgs e)
        {
            DebugOutput = "";

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_Acknowledge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_Acknowledge_Click(object sender, RoutedEventArgs e)
        {
            ReceivedData = false;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the ToogleButton_EditScenarioDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ToogleButton_EditScenarioDescription_Click(object sender, RoutedEventArgs e)
        {
            ScenarioDescriptionEditMode = (sender as ToggleButton).IsChecked ?? false;

            UpdateScenarioDescription();

            e.Handled = true;
        }



        /// <summary>
        /// Handles the Click event of the Button_HtmlHelp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_HtmlHelp_Click(object sender, RoutedEventArgs e)
        {
            Tools.Windows.OpenWebAdress("https://www.w3schools.com/html/default.asp");

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_HtmlConvertGermanUmlauts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_HtmlConvertGermanUmlauts_Click(object sender, RoutedEventArgs e)
        {
            HtmlConvertGermanUmlauts();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_ScenarioReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_ScenarioReport_Click(object sender, RoutedEventArgs e)
        {
            CreateScenarioReport();

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Devices the view model on selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        private void DeviceViewModel_OnSelectionChanged(object sender, bool bIsSelected)
        {
            // Mark Or Unmark The Device in the DataGrid And Scroll To It If it is neccary ...

            if (bIsSelected == true)
            {
                bNoFlashBack = true;

                dgRFDevices.SelectedItems.Clear();
                dgRFDevices.SelectedItems.Add(sender);

                // Bei allen anderen die Selection aufheben ...
                foreach (RFDeviceViewModel model in RFDevicesCollection)
                {
                    if (model != sender)
                    {
                        model.IsSelected = false;
                    }
                }

                if (SyncMapAndGrid == true)
                {
                    dgRFDevices.ScrollIntoView(sender);
                    dgRFDevices.Focus();
                }

                bNoFlashBack = false;
            }
            else
            {
                foreach (var item in dgRFDevices.SelectedItems)
                {
                    if (item == sender)
                    {
                        dgRFDevices.SelectedItems.Remove(item);
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
        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                QuickCommandAction();
                e.Handled = true;
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

    } // end public partial class MainWindow
}
