using GMap.NET.WindowsPresentation;



namespace SIGENCEScenarioTool.Markers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Markers.AbstractMarker" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class TriangleMarker
    {
        /// <summary>
        /// The d yaw
        /// </summary>
        private double dYaw = 0;

        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>
        /// The yaw.
        /// </value>
        public double Yaw
        {
            get { return this.dYaw; }
            set
            {
                this.dYaw = value;

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleMarker" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The marker.</param>
        /// <param name="strToolTip">The string tool tip.</param>
        public TriangleMarker( GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip ) :
            base( mcMapControl, mmMarker, strToolTip )
        {
            InitializeComponent();

            this.DataContext = this;
        }

    } // end public partial class TriangleMarker
}