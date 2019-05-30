using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Document;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.MetaInformation;
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
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = this.settings.LastLeft;
            this.Top = this.settings.LastTop;
            this.Width = this.settings.LastWidth >= this.MinWidth ? this.settings.LastWidth : this.MinWidth;
            this.Height = this.settings.LastHeight >= this.MinHeight ? this.settings.LastHeight : this.MinHeight;

            try
            {
                this.WindowState = this.settings.LastWindowState.IsNotEmpty() ? (WindowState)Enum.Parse(typeof(WindowState), this.settings.LastWindowState, true) : this.WindowState;
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
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource == this.tcTabControl)
            {
                if (this.tiValidation.IsSelected == true)
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
        private void MapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.CreatingRFDevice == true)
            {
                Point p = e.GetPosition(this.mcMapControl);

                PointLatLng pll = this.mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                //AddRFDevice( pll, DeviceSource.User, true );
                AddRFDevice(pll);

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
            if (this.StartedDALF == true)
            {
                Point p = e.GetPosition(this.mcMapControl);

                PointLatLng pll = this.mcMapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

                if (this.mrDALF == null)
                {
                    var list = new List<PointLatLng>(2);

                    if (this.dvmLastSelectedDevice != null)
                    {
                        list.Add(new PointLatLng(this.dvmLastSelectedDevice.Latitude, this.dvmLastSelectedDevice.Longitude));
                    }
                    else
                    {
                        MB.Warning("Strange ...");
                    }

                    list.Add(pll);

                    this.mrDALF = new GMapRoute(list);
                }
                else
                {
                    this.mrDALF.Points.Add(pll);
                }

                this.mcMapControl.Markers.Remove(this.mrDALF);
                this.mcMapControl.Markers.Add(this.mrDALF);

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
            this.IsTileLoading = true;
        }


        /// <summary>
        /// Maps the control on tile load complete.
        /// </summary>
        /// <param name="ElapsedMilliseconds">The elapsed milliseconds.</param>
        private void MapControl_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            this.IsTileLoading = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the PreviewKeyDown event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (this.bDataGridInEditMode == true)
            {
                if (e.Key == Key.Return)
                {
                    this.dgRFDevices.CommitEdit();
                    e.Handled = true;
                }

                return;
            }

            switch (e.Key)
            {
                case Key.Insert:
                    AddRFDevice(this.mcMapControl.Position, DeviceSource.User, true);

                    e.Handled = true;
                    return;

                case Key.Delete:
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
        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.bDataGridInEditMode == true)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Space:
                    {
                        foreach (RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                        {
                            x.IsMarked = !x.IsMarked;
                        }

                        e.Handled = true;
                        return;
                    }

                case Key.Add:
                    {
                        foreach (RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                        {
                            x.IsMarked = true;
                        }

                        e.Handled = true;
                        return;
                    }

                case Key.Subtract:
                    {
                        foreach (RFDeviceViewModel x in (sender as DataGrid).SelectedItems)
                        {
                            x.IsMarked = false;
                        }

                        e.Handled = true;
                        return;
                    }
            }
        }


        /// <summary>
        /// Handles the BeginningEdit event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridBeginningEditEventArgs"/> instance containing the event data.</param>
        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            this.bDataGridInEditMode = true;
        }


        /// <summary>
        /// Handles the CellEditEnding event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridCellEditEndingEventArgs"/> instance containing the event data.</param>
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            this.bDataGridInEditMode = false;
        }


        /// <summary>
        /// Handles the SelectionChanged event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;

            if (this.bNoFlashBack == true)
            {
                return;
            }

            foreach (var item in e.AddedItems)
            {
                if (item is RFDeviceViewModel dvm)
                {
                    dvm.IsSelected = true;

                    if (this.SyncMapAndGrid == true)
                    {
                        ZoomToRFDevice(dvm.RFDevice, false);
                    }

                    this.CurrentSelectedDevice = dvm;
                }
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is RFDeviceViewModel model)
                {
                    model.IsSelected = false;
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
        /// Handles the Click event of the MenuItem_SwitchInfoWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_SwitchInfoWindow_Click(object sender, RoutedEventArgs e)
        {
            ToggleInfoWindow();

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the MenuItem_LoadTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_LoadTemplates_Click(object sender, RoutedEventArgs e)
        {
            LoadTemplates();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_SaveTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_SaveTemplates_Click(object sender, RoutedEventArgs e)
        {
            SaveTemplates();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_AddToTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_AddToTemplates_Click(object sender, RoutedEventArgs e)
        {
            AddToTemplates();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_DelFromTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_DelFromTemplates_Click(object sender, RoutedEventArgs e)
        {
            DeleteFromTemplates();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_EditTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_EditTemplate_Click(object sender, RoutedEventArgs e)
        {
            EditTemplate();

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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the Click event of the MenuItem_ScenarioSimulationPlayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_ScenarioSimulationPlayer_Click(object sender, RoutedEventArgs e)
        {
            OpenScenarioSimulationPlayer();

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the MenuItem_DisplayScenarioDescription control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_DisplayScenarioDescription_Click(object sender, RoutedEventArgs e)
        //{
        //    DisplayScenarioDescription();

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the IsVisibleChanged event of the WebBrowser_Markdown control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        //private void WebBrowser_Markdown_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (this.wbWebBrowser.IsVisible)
        //    {
        //        // Jedesmal wenn der Browser sichtbar wir updaten ...
        //        //UpdateScenarioDescriptionMarkdown();
        //        //this.MetaInformation.Description = this.tecDescription.Text;
        //        //this.MetaInformation.Stylesheet = this.tecStyleSheet.Text;

        //        //this.MetaInformation.SetDescriptionAndStylesheet(this.tecDescription.Text, this.tecStyleSheet.Text);
        //        UpdateScenarioDescriptionMarkdown();
        //    }
        //}


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
        /// Handles the Click event of the MenuItem_About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            Tools.Windows.OpenWebAdress("https://obiwanlansi.github.io/SIGENCE-Scenario-Tool/");

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the MenuItem_OpenIssues control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_OpenIssues_Click(object sender, RoutedEventArgs e)
        //{
        //    Tools.Windows.OpenWebAdress("https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/issues/");

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the Click event of the MenuItem_KanbanBoard control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_KanbanBoard_Click(object sender, RoutedEventArgs e)
        //{
        //    Tools.Windows.OpenWebAdress("https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/projects/1?fullscreen=true");

        //    e.Handled = true;
        //}


        /// <summary>
        /// Handles the Click event of the MenuItem_ReleaseNotes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_ReleaseNotes_Click(object sender, RoutedEventArgs e)
        {
            OpenWebbrowserInternal($"{Tool.StartupPath}\\ReleaseNotes\\ReleaseNotes.Hypertext.html", "Release Notes");

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the MenuItem_OpenWiki control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_OpenWiki_Click(object sender, RoutedEventArgs e)
        //{
        //    Tools.Windows.OpenWebAdress("https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/wiki");

        //    e.Handled = true;
        //}


        /// <summary>
        /// Handles the IniMetaInformation event of the MenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_InitMetaInformation(object sender, RoutedEventArgs e)
        {
            InitMetaInformation();

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the MenuItem_CheckVersion control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        //private void MenuItem_CheckVersion_Click( object sender, RoutedEventArgs e )
        //{
        //    CheckVersion();

        //    e.Handled = true;
        //}


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


        ///// <summary>
        ///// Handles the Click event of the ToogleButton_EditScenarioDescription control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void ToogleButton_EditScenarioDescription_Click(object sender, RoutedEventArgs e)
        //{
        //    bool bSwitchToEdit = (sender as ToggleButton).IsChecked ?? false;

        //    if (bSwitchToEdit == true)
        //    {
        //        this.tecScenarioDescription.Text = this.ScenarioDescription;
        //    }
        //    else
        //    {
        //        this.ScenarioDescription = this.tecScenarioDescription.Text;
        //    }

        //    this.ScenarioDescriptionEditMode = bSwitchToEdit;

        //    //UpdateScenarioDescription();

        //    e.Handled = true;
        //}


        /// <summary>
        /// Handles the Click event of the Button_HtmlHelp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_DocumentationHelp_Click(object sender, RoutedEventArgs e)
        {
            Tools.Windows.OpenWebAdress("https://www.w3schools.com/html/default.asp");
            //Tools.Windows.OpenWebAdress("https://guides.github.com/features/mastering-markdown/");

            e.Handled = true;
        }


        ///// <summary>
        ///// Handles the Click event of the Button_InsertScenarioDescriptionTemplate control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        //private void Button_InsertScenarioDescriptionTemplate_Click(object sender, RoutedEventArgs e)
        //{
        //    InsertScenarioDescriptionTemplate();

        //    this.tecScenarioDescription.Focus();

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the Click event of the Button_HtmlConvertGermanUmlauts control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        //private void MenuItem_HtmlConvertGermanUmlauts_Click(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.tecScenarioDescription.Text) == false)
        //    {
        //        this.tecScenarioDescription.Text = this.tecScenarioDescription.Text.ReplaceHtml(true);
        //    }

        //    this.tecScenarioDescription.Focus();

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the Click event of the MenuItem_HtmlConvertToCapitalize control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        //private void MenuItem_HtmlConvertToCapitalize_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
        //    {
        //        List<ISelection> lSelection = this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.SelectionCollection;

        //        if (lSelection.Count == 1)
        //        {
        //            ISelection selection = lSelection[0];
        //            this.tecScenarioDescription.Document.Replace(selection.Offset, selection.Length, selection.SelectedText.Capitalize());

        //        }
        //    }

        //    this.tecScenarioDescription.Focus();

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the Click event of the MenuItem_HtmlConvertToLowerText control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        //private void MenuItem_HtmlConvertToLowerText_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
        //    {
        //        List<ISelection> lSelection = this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.SelectionCollection;

        //        if (lSelection.Count == 1)
        //        {
        //            ISelection selection = lSelection[0];
        //            this.tecScenarioDescription.Document.Replace(selection.Offset, selection.Length, selection.SelectedText.ToLower());

        //        }
        //    }

        //    this.tecScenarioDescription.Focus();

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the Click event of the MenuItem_HtmlConvertToUpperText control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_HtmlConvertToUpperText_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
        //    {
        //        List<ISelection> lSelection = this.tecScenarioDescription.ActiveTextAreaControl.SelectionManager.SelectionCollection;

        //        if (lSelection.Count == 1)
        //        {
        //            ISelection selection = lSelection[0];
        //            this.tecScenarioDescription.Document.Replace(selection.Offset, selection.Length, selection.SelectedText.ToUpper());

        //        }
        //    }

        //    this.tecScenarioDescription.Focus();

        //    e.Handled = true;
        //}


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


        ///// <summary>
        ///// Handles the Click event of the MenuItem_InsertHtmlSnippet control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_InsertHtmlSnippet_Click(object sender, RoutedEventArgs e)
        //{
        //    InsertHtmlSnippet((sender as Control).Tag as string);

        //    this.tecScenarioDescription.Focus();

        //    e.Handled = true;
        //}


        /// <summary>
        /// Handles the Click event of the MenuItem_FileHistory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_FileHistory_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as MenuItem).Tag != null)
            {
                string strFilename = (string)((sender as MenuItem).Tag);

                LoadFile(strFilename);
            }

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_ExecuteValidateScenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_ExecuteValidateScenario_Click(object sender, RoutedEventArgs e)
        {
            ExecuteValidateScenario();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_ClearScenarioValidation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_ClearScenarioValidation_Click(object sender, RoutedEventArgs e)
        {
            ClearScenarioValidation();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_resetAllFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_ResetAllDeviceFilter_Click(object sender, RoutedEventArgs e)
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
        private void DeviceViewModel_OnSelectionChanged(object sender, bool bIsSelected)
        {
            // Mark Or Unmark The Device in the DataGrid And Scroll To It If it is neccary ...

            if (bIsSelected == true)
            {
                this.bNoFlashBack = true;

                this.dgRFDevices.SelectedItems.Clear();
                this.dgRFDevices.SelectedItems.Add(sender);

                // Bei allen anderen die Selection aufheben ...
                foreach (RFDeviceViewModel model in this.RFDeviceViewModelCollection)
                {
                    if (model != sender)
                    {
                        model.IsSelected = false;
                    }
                }

                this.CurrentSelectedDevice = sender as RFDeviceViewModel;

                if (this.SyncMapAndGrid == true)
                {
                    this.dgRFDevices.ScrollIntoView(sender);
                    this.dgRFDevices.Focus();
                }

                this.bNoFlashBack = false;
            }
            else
            {
                foreach (var item in this.dgRFDevices.SelectedItems)
                {
                    if (item == sender)
                    {
                        this.dgRFDevices.SelectedItems.Remove(item);
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
        private void ComboBox_QuickCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
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
        private void DataGridScenarioValidation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object item = (sender as DataGrid).SelectedItem;

            if (item is ValidationResultViewModel)
            {
                if ((item as ValidationResultViewModel).Result.Source is RFDevice source)
                {
                    this.tiMap.IsSelected = true;

                    foreach (RFDeviceViewModel model in this.RFDeviceViewModelCollection)
                    {
                        if (model.RFDevice == source)
                        {
                            this.dgRFDevices.ScrollIntoView(model);
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
        /// Handles the PropertyChanged event of the MetaInformation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void MetaInformation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "DescriptionAndStylesheet")
            //{
            //    // Nur wenn der Browser gerade sichbar ist noch updaten ...
            //    //if (this.wbWebBrowser.IsVisible)
            //    {
            //        UpdateScenarioDescriptionMarkdown();
            //    }

            //    return;
            //}

            if (e.PropertyName == ScenarioMetaInformation.DESCRIPTIONMARKDOWN)
            {
                this.tecDescriptionMarkdown.Text = this.MetaInformation.DescriptionMarkdown;
                this.tecDescriptionMarkdown.Refresh();

                // Nur wenn der Browser gerade sichbar ist noch updaten ...
                //if (this.wbWebBrowser.IsVisible)
                {
                    UpdateScenarioDescriptionMarkdown();
                }

                return;
            }

            if (e.PropertyName == ScenarioMetaInformation.DESCRIPTIONSTYLESHEET)
            {
                this.tecDescriptionStyleheet.Text = this.MetaInformation.DescriptionStylesheet;
                this.tecDescriptionStyleheet.Refresh();

                // Nur wenn der Browser gerade sichbar ist noch updaten ...
                //if (this.wbWebBrowser.IsVisible)
                {
                    UpdateScenarioDescriptionMarkdown();
                }

                return;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the DocumentChanged event of the Document_DescriptionMarkdown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DocumentEventArgs"/> instance containing the event data.</param>
        private void Document_DescriptionMarkdown_DocumentChanged(object sender, DocumentEventArgs e)
        {
            this.DescriptionMarkdownChanged = true;
        }

        /// <summary>
        /// Handles the DocumentChanged event of the Document_DescriptionStylesheet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DocumentEventArgs"/> instance containing the event data.</param>
        private void Document_DescriptionStylesheet_DocumentChanged(object sender, DocumentEventArgs e)
        {
            this.DescriptionStylesheetChanged = true;
        }

        /// <summary>
        /// Handles the KeyUp event of the TextArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        private void TextArea_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e.KeyCode == System.Windows.Forms.Keys.F1 && (e.Alt || e.Control || e.Shift))
            //{
            //    // Display a pop-up Help topic to assist the user.
            //    System.Windows.Forms.Help.ShowPopup(this.tecDescription, "Enter your name.", new System.Drawing.Point(this.tecDescription.Bottom, this.tecDescription.Right));
            //}

            // ToDo: Highliter for Markdown / CSS
            // ToDo: CSS for <code />

            switch (e.KeyData)
            {
                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.A:
                    this.MetaInformation.SetDescriptionWithoutEvent(this.tecDescriptionMarkdown.Text);
                    UpdateScenarioDescriptionMarkdown();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.D:
                    this.tecDescriptionMarkdown.Text = this.MetaInformation.DescriptionMarkdown;
                    UpdateScenarioDescriptionMarkdown();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y:
                    new DeleteLine().Execute(this.tecDescriptionMarkdown.ActiveTextAreaControl.TextArea);
                    break;

                //-------------------------------------------------------------

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.U:
                    this.tecDescriptionMarkdown.ToUpperCase();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.L:
                    this.tecDescriptionMarkdown.ToLowerCase();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.C:
                    this.tecDescriptionMarkdown.ToCapitalize();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.B:
                    this.tecDescriptionMarkdown.ToBold();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.I:
                    this.tecDescriptionMarkdown.ToItalic();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S:
                    this.tecDescriptionMarkdown.ToStrikethrough();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.D1:
                    this.tecDescriptionMarkdown.ToHeader(1);
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.D2:
                    this.tecDescriptionMarkdown.ToHeader(2);
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.D3:
                    this.tecDescriptionMarkdown.ToHeader(3);
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.D4:
                    this.tecDescriptionMarkdown.ToHeader(4);
                    break;

                //-------------------------------------------------------------

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.X:
                    this.tecDescriptionMarkdown.ToogleSpecialCharacter();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.T:
                    this.tecDescriptionMarkdown.ConvertTabsToSpaces();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.R:
                    this.tecDescriptionMarkdown.RemoveTrailingWhiteSpaces();
                    break;

                //-------------------------------------------------------------

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L:
                    this.tecDescriptionMarkdown.InsertLoremIpsum();
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D:
                    this.tecDescriptionMarkdown.InsertDateTime();
                    break;

                //-------------------------------------------------------------

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B:
                    new ToggleBookmark().Execute(this.tecDescriptionMarkdown.ActiveTextAreaControl.TextArea);
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up:
                    new GotoPrevBookmark(b => true).Execute(this.tecDescriptionMarkdown.ActiveTextAreaControl.TextArea);
                    break;

                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down:
                    new GotoNextBookmark(b => true).Execute(this.tecDescriptionMarkdown.ActiveTextAreaControl.TextArea);
                    break;
            }

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_AcceptMarkdown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_AcceptMarkdown_Click(object sender, RoutedEventArgs e)
        {
            this.MetaInformation.SetDescriptionWithoutEvent(this.tecDescriptionMarkdown.Text);

            UpdateScenarioDescriptionMarkdown();

            this.DescriptionMarkdownChanged = false;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_DiscardMarkdown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_DiscardMarkdown_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.Text = this.MetaInformation.DescriptionMarkdown;

            //this.tecDescription.Refresh();
            UpdateScenarioDescriptionMarkdown();

            this.DescriptionMarkdownChanged = false;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_AcceptStylesheet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_AcceptStylesheet_Click(object sender, RoutedEventArgs e)
        {
            this.MetaInformation.SetStyleSheetWithoutEvent(this.tecDescriptionStyleheet.Text);

            UpdateScenarioDescriptionMarkdown();

            this.DescriptionStylesheetChanged = false;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_DiscardStylesheet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_DiscardStylesheet_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionStyleheet.Text = this.MetaInformation.DescriptionStylesheet;

            //this.tecStyleSheet.Refresh();
            UpdateScenarioDescriptionMarkdown();

            this.DescriptionStylesheetChanged = false;

            e.Handled = true;
        }



        /// <summary>
        /// Handles the Click event of the Button_InsertTable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_InsertTable_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.InsertTable();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_InsertImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_InsertImage_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.InsertImage();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_InsertLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_InsertLink_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.InsertLink();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_OrderedList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_InsertOrderedList_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.InsertOrderedList();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_UnorderedList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_InsertUnorderedList_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.InsertUnorderedList();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_InsertBlockquote control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_InsertBlockquote_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.InsertBlockquote();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_InsertCode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_InsertCode_Click(object sender, RoutedEventArgs e)
        {
            this.tecDescriptionMarkdown.InsertCode();

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region Drag & Drop Test


        ///// <summary>
        ///// Handles the DragOver event of the StackPanel_Attachements control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        //private void StackPanel_Attachements_DragOver(object sender, DragEventArgs e)
        //{
        //    e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) == false
        //        ? DragDropEffects.None
        //        : (e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey ? DragDropEffects.Copy : DragDropEffects.Link;

        //    e.Handled = true;
        //}


        ///// <summary>
        ///// Handles the Drop event of the StackPanel control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        //private void StackPanel_Drop(object sender, DragEventArgs e)
        //{
        //    //throw new NotImplementedException();

        //    bool bIsControlKeyPressed = (e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey;

        //    foreach (FileInfo fi in from strFilename in (string[])e.Data.GetData(DataFormats.FileDrop) select new FileInfo(strFilename))
        //    {
        //        //this.Attachements.Add(new Attachement(fi, bIsControlKeyPressed ? AttachementType.Embedded : AttachementType.Link));
        //        //this.ScenarioMetaInformation.Attachements.Add(new Attachement(fi, bIsControlKeyPressed? AttachementType.Embedded : AttachementType.Link));
        //        this.MetaInformation.Attachements.Add(new Attachement(fi, bIsControlKeyPressed ? AttachementType.Embedded : AttachementType.Link));
        //    }

        //    e.Handled = true;
        //}

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        private void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

    } // end public partial class MainWindow
}
