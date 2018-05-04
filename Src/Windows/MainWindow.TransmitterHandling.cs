using System;
using System.Collections.Generic;
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
            GMapMarker currentMarker = new GMapMarker(pll)
            {
                Shape = new Cross(),
                Offset = new Point(-15, -15),
                ZIndex = int.MaxValue
            };

            mcMapControl.Markers.Add(currentMarker);

            Transmitter t = new Transmitter
            {
                Latitude = pll.Lat,
                Longitude = pll.Lng
            };

            TransmitterCollection.Add(new TransmitterViewModel(t));
        }


        /// <summary>
        /// Adds the transmitter.
        /// </summary>
        /// <param name="t">The t.</param>
        private void AddTransmitter(Transmitter t)
        {
            GMapMarker currentMarker = new GMapMarker(new PointLatLng(t.Latitude, t.Longitude))
            {
                Shape = new Cross(),
                Offset = new Point(-15, -15),
                ZIndex = int.MaxValue
            };

            mcMapControl.Markers.Add(currentMarker);

            TransmitterCollection.Add(new TransmitterViewModel(t));
        }


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
