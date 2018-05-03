using System.Windows;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.WindowsPresentation;

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
            MB.NotYetImplemented();
        }

    } // end public partial class MainWindow
}
