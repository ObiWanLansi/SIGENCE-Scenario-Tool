using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET;

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
            ChartingDialog cw = new ChartingDialog(new RFDeviceList(from device in RFDevicesCollection select device.RFDevice));
            cw.ShowDialog();
            cw = null;

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sets the blink1.
        /// </summary>
        private void SetBlink1()
        {
            try
            {
                if (ReceivedData == true)
                {
                    Blink.SetColor(Colors.Green);
                }
                else
                {
                    Blink.Off();
                }
            }
            catch (Exception)
            {
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// The t UDP server
        /// </summary>
        private Thread tUDPServer = null;

        /// <summary>
        /// Initializes the UDP server.
        /// </summary>
        private void StartUDPServer()
        {
            if (tUDPServer == null)
            {
                tUDPServer = new Thread(UDPReceiveData)
                {
                    IsBackground = true,
                    Name = "UDPServerThread"
                };
                tUDPServer.Start();
            }
        }


        /// <summary>
        /// Stops the UDP server.
        /// </summary>
        private void StopUDPServer()
        {
            if (tUDPServer != null)
            {
                tUDPServer.Abort();

                tUDPServer = null;
            }
        }


        /// <summary>
        /// UDPs the receive data.
        /// </summary>
        private void UDPReceiveData()
        {
            UdpClient client = null;

            try
            {
                client = new UdpClient(settings.UDPPortReceiving);
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(settings.UDPHost), settings.UDPPortReceiving);

                    // A neverending story ...
                    while (true)
                    {
                        // Obwohl der Thread Aborted wird beendet er das Receiver nicht und somit auch nicht Thread :-(
                        // Erst wenn er was empfangen hat merkt er das er Aborted ist und die Expcetion tritt auf ...
                        byte[] baReceived = client.Receive(ref ep);

                        string strReceived = Encoding.Default.GetString(baReceived);

                        DebugOutput += strReceived + "\n\n";
                        ReceivedData = true;
                    }
                }
            }
            catch (ThreadAbortException)
            {
                // Do nothing ...
                //Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                MB.Warning(ex.Message);
            }
            finally
            {
                if (client != null)
                {
                    try
                    {
                        client.Close();
                        client.Dispose();
                        client = null;
                    }
                    catch (Exception)
                    {

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

            foreach (var device in CreateRandomizedRFDeviceList(iMaxCount, mcMapControl.Position))
            {
                AddRFDevice(device);
            }

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Creates the randomized rf device list.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        /// <param name="pllCenter">The PLL center.</param>
        /// <returns></returns>
        static public RFDeviceList CreateRandomizedRFDeviceList(int iMaxCount, PointLatLng pllCenter)
        {
            RFDeviceList list = new RFDeviceList(iMaxCount);

            for (int i = 0; i < iMaxCount; i++)
            {
                list.Add(new RFDevice
                {
                    Id = r.Next(-1000, 1000),
                    DeviceSource = DeviceSource.Automatic,
                    Name = string.Format("RFDevice #{0}", i),
                    Latitude = (r.NextDouble() * 0.05) + pllCenter.Lat,
                    Longitude = (r.NextDouble() * 0.05) + pllCenter.Lng,
                    Altitude = (uint)r.Next(12345),
                    RxTxType = r.NextEnum<RxTxType>(),
                    AntennaType = r.NextEnum<AntennaType>(),
                    CenterFrequency_Hz = (uint)r.Next(85, 105) * 100000,
                    Bandwith_Hz = (uint)r.Next(10, 20) * 1000,
                    Gain_dB = r.Next(140),
                    SignalToNoiseRatio_dB = (uint)r.Next(140),
                    Roll = r.Next(-90, 90),
                    Pitch = r.Next(-90, 90),
                    Yaw = r.Next(-90, 90),
                    XPos = r.Next(-74, 74),
                    YPos = r.Next(-74, 74),
                    ZPos = r.Next(-74, 74),
                    Remark = r.NextObject(Tool.ALLPANGRAMS)
                });
            }

            if (list.Exists(d => d.Id == 0) == false)
            {
                list.First().Id = 0;
            }

            return list;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the scenario report.
        /// </summary>
        private void CreateScenarioReport()
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                MB.Information("The scenario has not been saved yet.\nSave it first and then try again.");
                return;
            }

            Cursor = Cursors.Wait;

            FileInfo fiCurrentFile = new FileInfo(CurrentFile);

            string strOutputFilename = string.Format("{0}{1}.html", Path.GetTempPath(), fiCurrentFile.GetFilenameWithoutExtension());

            StringBuilder sb = new StringBuilder(8192);

            sb.Append("<!DOCTYPE html><html><head><title>Scenario Documentation</title></head><body>");

            //-----------------------------------------------------------------

            sb.AppendFormat("<center style=\"width: 100%; border: 1px solid black; background-color: lightblue;\"><h1>{0}</h1></center>", fiCurrentFile.GetFilenameWithoutExtension());

            sb.Append("<hr />");

            //-----------------------------------------------------------------

            if (string.IsNullOrEmpty(ScenarioDescription) == false)
            {
                sb.Append(ScenarioDescription);
            }

            //-----------------------------------------------------------------

            //Guid gScreenshot = Guid.NewGuid();
            //string strOutputFilenameScreenshot = string.Format("{0}{1}.png", Path.GetTempPath(), gScreenshot);
            //var screenshot = Tools.Windows.GetWPFScreenshot(mcMapControl);
            //Tools.Windows.SaveWPFScreenshot(screenshot, strOutputFilenameScreenshot);
            //sb.AppendFormat("<center><img src=\"{0}.png\" style=\"border: 1px solid black;\"/></center>", gScreenshot);

            //-----------------------------------------------------------------



            //-----------------------------------------------------------------

            sb.Append("</body></html> ");

            File.WriteAllText(strOutputFilename, sb.ToString(), Encoding.Default);

            Tools.Windows.OpenWithDefaultApplication(strOutputFilename);

            Cursor = Cursors.Arrow;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Inserts the HTML snippet.
        ///// </summary>
        ///// <param name="strSnippetId">The string snippet identifier.</param>
        //private void InsertHtmlSnippet(string strSnippetId)
        //{
        //    string strSnippet = null;

        //    Func<string, string> GetDefaultTag = ((tag) => { return string.Format("<{0}></{0}>", tag); });

        //    strSnippetId = strSnippetId.ToLower();

        //    switch (strSnippetId)
        //    {

        //        case "table":
        //            strSnippet = "<table border=\"1\">\n</table>";
        //            break;

        //        case "br":
        //            strSnippet = "<br />";
        //            break;

        //        case "hr":
        //            strSnippet = "<hr />";
        //            break;

        //        case "image":
        //            strSnippet = "<image src=\"url\" />";
        //            break;

        //        case "link":
        //            strSnippet = "<a href=\"url\">Link Text</a>";
        //            break;

        //        default:
        //            strSnippet = GetDefaultTag(strSnippetId);
        //            break;
        //    }

        //    int iOldCaretIndex = tbScenarioDescription.CaretIndex;
        //    ScenarioDescription = ScenarioDescription.Insert(iOldCaretIndex, strSnippet);
        //    tbScenarioDescription.CaretIndex = iOldCaretIndex;
        //}

    } // end public partial class MainWindow
}
