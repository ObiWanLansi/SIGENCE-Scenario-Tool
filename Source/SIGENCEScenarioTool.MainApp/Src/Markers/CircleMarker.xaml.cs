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
    public partial class CircleMarker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircleMarker"/> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The mm marker.</param>
        /// <param name="strToolTip">The string tool tip.</param>
        public CircleMarker(GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip) :
            base(mcMapControl, mmMarker, strToolTip)
        {
            this.InitializeComponent();
        }

    } // end public partial class CircleMarker
}