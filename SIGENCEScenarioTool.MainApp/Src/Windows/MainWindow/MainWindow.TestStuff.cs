using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {

        /// <summary>
        /// Handles the Click event of the MenuItem_ChartingTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_ChartingTest_Click(object sender, RoutedEventArgs e)
        {
            ChartingWindow cw = new ChartingWindow(new RFDeviceList(from device in RFDevicesCollection select device.RFDevice));
            cw.ShowDialog();
            cw = null;
        }


        /// <summary>
        /// UDPs the receive data.
        /// </summary>
        private void UDPReceiveData()
        {
            using (UdpClient client = new UdpClient(7474))
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(settings.UDPHost), settings.UDPPortReceiving);

                // A neverending story ...
                while (true)
                {
                    try
                    {
                        byte[] baReceived = client.Receive(ref ep);

                        string strReceived = Encoding.Default.GetString(baReceived);

                        ReceivedData += strReceived+"\n\n";

                        //Debug.WriteLine(strReceived);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The randomizer.
        /// </summary>
        static private readonly Random r = new Random();


        /// <summary>
        /// Creates the randomized RFDevices.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        private void CreateRandomizedRFDevices(int iMaxCount)
        {
            Cursor = Cursors.Wait;

            for (int iCounter = 1; iCounter < iMaxCount + 1; iCounter++)
            {
                AddRFDevice(new RFDevice
                {
                    Id = r.Next(-1000, 1000),
                    Name = string.Format("RFDevice #{0}", iCounter),
                    Latitude = (r.NextDouble() * 0.05) + 49.7454,
                    Longitude = (r.NextDouble() * 0.05) + 6.6149,
                    Altitude = 0,
                    RxTxType = r.NextEnum<RxTxType>(),
                    AntennaType = r.NextEnum<AntennaType>(),
                    CenterFrequency_Hz = (uint)r.Next(85, 105) * 100000,
                    Bandwith_Hz = (uint)r.Next(10, 20) * 1000,
                    Gain_dB = 0,
                    SignalToNoiseRatio_dB = 0,
                    Roll = 0,
                    Pitch = 0,
                    Yaw = 0,
                    XPos = 0,
                    YPos = 0,
                    ZPos = 0,
                    Remark = r.NextObject(Tool.ALLPANGRAMS)
                });
            }

            Cursor = Cursors.Arrow;
        }

    } // end public partial class MainWindow
}
