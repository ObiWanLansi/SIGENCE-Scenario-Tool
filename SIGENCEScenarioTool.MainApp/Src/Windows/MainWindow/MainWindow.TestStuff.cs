using System;
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
            try
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

                            DebugOutput += strReceived + "\n\n";
                        }
                        catch (Exception ex)
                        {
                            MB.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MB.Warning(ex.Message);
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

            foreach (var device in CreateRandomizedRFDeviceList(iMaxCount))
            {
                AddRFDevice(device);
            }

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Creates the randomized rf device list.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        /// <returns></returns>
        static public RFDeviceList CreateRandomizedRFDeviceList(int iMaxCount)
        {
            RFDeviceList list = new RFDeviceList(iMaxCount);

            for (int i = 0; i < iMaxCount; i++)
            {
                list.Add(new RFDevice
                {
                    Id = r.Next(-1000, 1000),
                    Name = string.Format("RFDevice #{0}", i),
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

            return list;
        }

    } // end public partial class MainWindow
}
