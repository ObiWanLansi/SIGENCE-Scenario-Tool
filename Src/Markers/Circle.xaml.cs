using System.Windows.Controls;

using GMap.NET;



namespace TransmitterTool.Markers
{
    /// <summary>
    /// Interaction logic for Circle.xaml
    /// </summary>
    public partial class Circle : UserControl
    {
        public PointLatLng Center;

        public PointLatLng Bound;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle()
        {
            InitializeComponent();
        }

    } // end public partial class Circle
}
