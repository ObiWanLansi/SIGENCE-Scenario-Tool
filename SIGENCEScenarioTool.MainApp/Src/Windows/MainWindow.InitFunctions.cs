using System.Net;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using SIGENCEScenarioTool.Commands;



namespace SIGENCEScenarioTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Initializes the commands.
        /// </summary>
        private void InitCommands()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    NewFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    OpenFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SaveFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    SaveAsFile();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    Close();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    BeginCreateRFDevice();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.DeleteRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    DeleteRFDevice();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.ExportRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    ExportRFDevices();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.ImportRFDevice,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    ImportRFDevices();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add(new CommandBinding(RegisteredCommands.CreateScreenshot,
                (object sender, ExecutedRoutedEventArgs e) =>
                {
                    CreateScreenshot();
                    e.Handled = true;
                },
                (object sender, CanExecuteRoutedEventArgs e) =>
                {
                    e.CanExecute = true;
                }
            ));

            CommandBindings.Add( new CommandBinding( RegisteredCommands.SendDataUDP,
                ( object sender , ExecutedRoutedEventArgs e ) =>
                {
                    SendDataUDP();
                    e.Handled = true;
                } ,
                ( object sender , CanExecuteRoutedEventArgs e ) =>
                {
                    e.CanExecute = true;
                }
            ) );

            CommandBindings.Add( new CommandBinding( RegisteredCommands.ZoomToRFDevice ,
                ( object sender , ExecutedRoutedEventArgs e ) =>
                {
                    ZoomToRFDevice();
                    e.Handled = true;
                } ,
                ( object sender , CanExecuteRoutedEventArgs e ) =>
                {
                    e.CanExecute = true;
                }
            ) );
        }


        /// <summary>
        /// Initializes the map control.
        /// </summary>
        private void InitMapControl()
        {
            GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            mcMapControl.DragButton = MouseButton.Left;
            mcMapControl.MapProvider = GMapProviders.GoogleMap;
            mcMapControl.Manager.Mode = AccessMode.ServerAndCache;

            mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            mcMapControl.ShowCenter = false;
            mcMapControl.MinZoom = 2;
            mcMapControl.MaxZoom = 22;

            mcMapControl.Position = new PointLatLng(49.761471, 6.650053);
            mcMapControl.Zoom = 14;
        }


        /// <summary>
        /// Initializes the map provider.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitMapProvider()
        {
            // Wir fügen nur die für unsere Region sinnvollen hinzu ...
            cbMapProvider.Items.Add(GMapProviders.GoogleMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleSatelliteMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleTerrainMap);
            cbMapProvider.Items.Add(GMapProviders.GoogleHybridMap);

            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_Imagery_World_2D_Map);
            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_ShadedRelief_World_2D_Map);
            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_StreetMap_World_2D_Map);
            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_World_Physical_Map);
            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_World_Shaded_Relief_Map);
            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_World_Street_Map);
            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_World_Terrain_Base_Map);
            //cbMapProvider.Items.Add(GMapProviders.ArcGIS_World_Topo_Map);

            cbMapProvider.Items.Add(GMapProviders.OpenStreetMap);

            cbMapProvider.Items.Add(GMapProviders.BingHybridMap);
            cbMapProvider.Items.Add(GMapProviders.BingMap);
            cbMapProvider.Items.Add(GMapProviders.BingSatelliteMap);
        }

    } // end public partial class MainWindow 
}
