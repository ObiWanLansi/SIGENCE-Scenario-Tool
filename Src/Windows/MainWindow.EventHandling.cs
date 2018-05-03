using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GMap.NET;

using TransmitterTool.ViewModels;



namespace TransmitterTool.Windows
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
        private void McMapControl_MouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            if( CreatingTransmitter == true )
            {
                Point p = e.GetPosition( mcMapControl );

                PointLatLng pll = mcMapControl.FromLocalToLatLng( ( int ) p.X , ( int ) p.Y );

                AddTransmitter( pll );

                EndCreateTransmitter();

                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the MouseDoubleClick event of the DataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DataGrid_MouseDoubleClick( object sender , MouseButtonEventArgs e )
        {
            TransmitterViewModel item = ( sender as DataGrid ).SelectedItem as TransmitterViewModel;

            mcMapControl.Position = new PointLatLng( item.Transmitter.Latitude , item.Transmitter.Longitude );
            mcMapControl.Zoom = 20;

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
        protected void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( strPropertyName ) );
        }

    } // end public partial class MainWindow
}
