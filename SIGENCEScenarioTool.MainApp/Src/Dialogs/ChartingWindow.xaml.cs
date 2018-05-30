using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using SIGENCEScenarioTool.ViewModels;
using SIGENCEScenarioTool.Models;

namespace SIGENCEScenarioTool.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ChartingWindow.xaml
    /// </summary>
    public partial class ChartingWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<RFDeviceViewModel> RFDevicesCollection { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<KeyValuePair<string , int>> RxTxTypeDistribution { get; set; }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// 
        /// </summary>
        public ChartingWindow()
        {
            InitializeComponent();

            //InitChart();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public void InitChart()
        {
            if( RFDevicesCollection != null && RFDevicesCollection.Count > 0 )
            {
                RxTxTypeDistribution = new ObservableCollection<KeyValuePair<string , int>>();
                SortedDictionary<RxTxType , int> sd = new SortedDictionary<RxTxType , int>();

                foreach( RFDevice device in from device in RFDevicesCollection select device.RFDevice )
                {
                    if( sd.ContainsKey( device.RxTxType ) )
                    {
                        sd [device.RxTxType]++;
                    }
                    else
                    {
                        sd.Add( device.RxTxType , 1 );
                    }
                }
                foreach( RxTxType rtt in sd.Keys )
                {
                    RxTxTypeDistribution.Add( new KeyValuePair<string , int>( rtt.ToString() , sd [rtt] ) );
                }

                chart.DataContext = RxTxTypeDistribution;
            }
        }

    } // end public partial class ChartingWindow
}
