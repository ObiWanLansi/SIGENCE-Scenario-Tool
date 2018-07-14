using GMap.NET.WindowsPresentation;



/// <summary>
/// 
/// </summary>
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
        /// Initializes a new instance of the <see cref="TriangleMarker" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The marker.</param>
        /// <param name="strToolTip">The string tool tip.</param>
        public TriangleMarker(GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip) :
            base(mcMapControl, mmMarker, strToolTip)
        {
            this.InitializeComponent();
        }

    } // end public partial class TriangleMarker
}