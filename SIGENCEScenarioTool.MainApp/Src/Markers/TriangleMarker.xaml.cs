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
        /// The d direction angle
        /// </summary>
        private double dDirectionAngle = 0;

        /// <summary>
        /// Gets or sets the direction angle.
        /// </summary>
        /// <value>
        /// The direction angle.
        /// </value>
        public double DirectionAngle
        {
            get { return dDirectionAngle; }
            set
            {
                this.dDirectionAngle = value;

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
        public TriangleMarker(GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip) :
            base(mcMapControl, mmMarker, strToolTip)
        {
            this.InitializeComponent();

            this.DataContext = this;

            //this.DirectionArrow.Visibility = System.Windows.Visibility.Hidden;
            //this.DirectionAngle.Angle = 0;
        }

    } // end public partial class TriangleMarker
}