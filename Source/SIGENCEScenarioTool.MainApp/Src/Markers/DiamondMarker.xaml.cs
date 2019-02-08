using GMap.NET.WindowsPresentation;



namespace SIGENCEScenarioTool.Markers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Markers.AbstractMarker" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class DiamondMarker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleMarker" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The mm marker.</param>
        /// <param name="strToolTip">The tooltip.</param>
        public DiamondMarker( GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip ) :
            base( mcMapControl, mmMarker, strToolTip )
        {
            InitializeComponent();

            this.DirectionAngle.Angle = 0;
        }

    } // end public partial class DiamondMarker
}