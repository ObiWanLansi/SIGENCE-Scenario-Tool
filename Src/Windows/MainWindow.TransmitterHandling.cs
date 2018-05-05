using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using TransmitterTool.Extensions;
using TransmitterTool.Markers;
using TransmitterTool.Models;
using TransmitterTool.Tools;
using TransmitterTool.ViewModels;



namespace TransmitterTool.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Sets the map to creating transmitter mode.
        /// </summary>
        private void SetMapToCreatingTransmitterMode()
        {
            mcMapControl.DragButton = bCreatingTransmitter ? MouseButton.Right : MouseButton.Left;
        }


        /// <summary>
        /// Begins the create transmitter.
        /// </summary>
        private void BeginCreateTransmitter()
        {
            CreatingTransmitter = true;
            mcMapControl.Cursor = Cursors.Cross;
        }


        /// <summary>
        /// Ends the create transmitter.
        /// </summary>
        private void EndCreateTransmitter()
        {
            mcMapControl.Cursor = Cursors.Arrow;
            CreatingTransmitter = false;
        }


        /// <summary>
        /// Creates the transmitter.
        /// </summary>
        /// <param name="pll">The PLL.</param>
        private void AddTransmitter(PointLatLng pll)
        {
            AddTransmitter(new Transmitter
            {
                Latitude = pll.Lat,
                Longitude = pll.Lng
            });
        }


        /// <summary>
        /// Adds the transmitter.
        /// </summary>
        /// <param name="t">The t.</param>
        private void AddTransmitter(Transmitter t)
        {
            TransmitterViewModel tvm = new TransmitterViewModel(t);
            TransmitterCollection.Add(tvm);
            
            //GMapMarker marker = new GMapMarker(new PointLatLng(t.Latitude, t.Longitude))
            //{
            //    Offset = new Point(-15, -15),
            //    ZIndex = int.MaxValue
            //};
            //marker.Shape = new CustomMarker(this, marker, t.Name);
            mcMapControl.Markers.Add(tvm.Marker);


            // Falls durch die Datagrid nachträglich Datengeändert werden 
            // müssen wir das natürlich mitbekommen um z.B. den Tooltip wieder anzupassen.
            //tvm.PropertyChanged += Transmitter_PropertyChanged;
        }


        ///// <summary>
        ///// Handles the PropertyChanged event of the Transmitter control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        //private void Transmitter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
            
        //}


        /// <summary>
        /// Exports the transmitter.
        /// </summary>
        private void ExportTransmitter()
        {
            if (TransmitterCollection.Count == 0)
            {
                MB.Warning("No transmitter avaible for export!");
                return;
            }

            if (CurrentFile != null)
            {
                sfdExportTransmitter.FileName = new FileInfo(CurrentFile).Name;
            }

            if (sfdExportTransmitter.ShowDialog() == true)
            {
                FileInfo fiExportFile = new FileInfo(sfdExportTransmitter.FileName);

                List<Transmitter> tl = TransmitterCollection.Select(t => t.Transmitter).ToList();

                try
                {
                    switch (fiExportFile.Extension.ToLower())
                    {
                        case ".csv":
                            tl.SaveAsCsv(fiExportFile.FullName);
                            break;

                        case ".json":
                            tl.SaveAsJson(fiExportFile.FullName);
                            break;
                    }

                    MB.Information("File {0} successful created.", fiExportFile.Name);
                }
                catch (Exception ex)
                {
                    MB.Error(ex);
                }
            }
        }


        /// <summary>
        /// Imports the transmitter.
        /// </summary>
        private void ImportTransmitter()
        {
            MB.NotYetImplemented();
        }

    } // end public partial class MainWindow
}
