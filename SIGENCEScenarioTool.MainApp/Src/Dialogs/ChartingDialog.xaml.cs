using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

using SIGENCEScenarioTool.Models;



namespace SIGENCEScenarioTool.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ChartingDialog.xaml
    /// </summary>
    public partial class ChartingDialog : Window
    {
        /// <summary>
        /// Gets or sets the rf devices collection.
        /// </summary>
        /// <value>
        /// The rf devices collection.
        /// </value>
        private readonly RFDeviceList lRFDevices = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="ChartingDialog" /> class.
        /// </summary>
        public ChartingDialog(RFDeviceList lRFDevices)
        {
            this.lRFDevices = lRFDevices;

            InitializeComponent();

            InitChart();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the chart.
        /// </summary>
        public void InitChart()
        {
            if (lRFDevices != null && lRFDevices.Count > 0)
            {
                SortedDictionary<RxTxType, int> sd = new SortedDictionary<RxTxType, int>();

                foreach (RFDevice device in lRFDevices)
                {
                    if (sd.ContainsKey(device.RxTxType))
                    {
                        sd[device.RxTxType]++;
                    }
                    else
                    {
                        sd.Add(device.RxTxType, 1);
                    }
                }

                //-----------------------------------------------------------------

                {
                    ChartArea ca = new ChartArea();

                    ca.AxisX.Title = "RxTxType";
                    ca.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;

                    ca.AxisY.Title = "Count";
                    ca.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

                    cRxTxTypeDistribution.ChartAreas.Add(ca);

                    //-------------------------

                    Series series = new Series
                    {
                        ChartType = SeriesChartType.Column,
                        IsValueShownAsLabel = true
                    };

                    foreach (RxTxType rtt in sd.Keys)
                    {
                        series.Points.AddXY(rtt.ToString(), sd[rtt]);
                    }

                    cRxTxTypeDistribution.Series.Add(series);
                    cRxTxTypeDistribution.Titles.Add("RxTxType Distribution");
                }

                //-----------------------------------------------------------------

                {
                    ChartArea ca = new ChartArea();
                    //ca.AxisX.Title = "RxTxType";
                    //ca.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;

                    //ca.AxisY.Title = "Count";
                    //ca.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

                    cReceiverTransmitterDistribution.ChartAreas.Add(ca);

                    //-------------------------

                    Series series = new Series
                    {
                        ChartType = SeriesChartType.Pie,
                        IsValueShownAsLabel = true,
                        IsVisibleInLegend = true
                    };

                    int iReceiver = lRFDevices.Count((d) => d.Id > 0);
                    DataPoint dpReceiver = new DataPoint { ToolTip = "Receiver" };
                    dpReceiver.SetValueXY("Receiver", iReceiver);
                    series.Points.Add(dpReceiver);

                    int iTransmitter = lRFDevices.Count((d) => d.Id < 0);
                    DataPoint dpTransmitter = new DataPoint { ToolTip = "Transmitter" };
                    dpTransmitter.SetValueXY("Transmitter", iTransmitter);
                    series.Points.Add(dpTransmitter);

                    cReceiverTransmitterDistribution.Legends.Add(new Legend());
                    cReceiverTransmitterDistribution.Series.Add(series);
                    cReceiverTransmitterDistribution.Titles.Add("Transmitter / Receiver Distribution");
                }
            }
        }

    } // end public partial class ChartingDialog
}
