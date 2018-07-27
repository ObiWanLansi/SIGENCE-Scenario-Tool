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
    public partial class RectangleMarker
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
        /// Initializes a new instance of the <see cref="RectangleMarker" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="mmMarker">The mm marker.</param>
        /// <param name="strToolTip">The tooltip.</param>
        public RectangleMarker(GMapControl mcMapControl, GMapMarker mmMarker, string strToolTip) :
            base(mcMapControl, mmMarker, strToolTip)
        {
            this.InitializeComponent();

            this.DataContext = this;

            //this.DirectionArrow.Visibility = System.Windows.Visibility.Hidden;
            //this.DirectionAngle.Angle = 45;
        }

    } // end public partial class RectangleMarker
}