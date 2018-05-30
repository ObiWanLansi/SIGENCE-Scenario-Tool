using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ChartingWindow.xaml
    /// </summary>
    public partial class ChartingWindow : Window
    {
        /// <summary>
        /// Gets or sets the rf devices collection.
        /// </summary>
        /// <value>
        /// The rf devices collection.
        /// </value>
        public ObservableCollection<RFDeviceViewModel> RFDevicesCollection { get; set; }


        /// <summary>
        /// Gets or sets the rx tx type distribution.
        /// </summary>
        /// <value>
        /// The rx tx type distribution.
        /// </value>
        public ObservableCollection<KeyValuePair<string, int>> RxTxTypeDistribution { get; set; }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartingWindow" /> class.
        /// </summary>
        public ChartingWindow()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the chart.
        /// </summary>
        public void InitChart()
        {
            if (RFDevicesCollection != null && RFDevicesCollection.Count > 0)
            {
                RxTxTypeDistribution = new ObservableCollection<KeyValuePair<string, int>>();
                SortedDictionary<RxTxType, int> sd = new SortedDictionary<RxTxType, int>();

                foreach (RFDevice device in from device in RFDevicesCollection select device.RFDevice)
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
                foreach (RxTxType rtt in sd.Keys)
                {
                    RxTxTypeDistribution.Add(new KeyValuePair<string, int>(rtt.ToString(), sd[rtt]));
                }

                chart.DataContext = RxTxTypeDistribution;
            }
        }

    } // end public partial class ChartingWindow
}
