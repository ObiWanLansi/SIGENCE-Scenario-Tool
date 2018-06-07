using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Mockup
{
    /// <summary>
    /// This is a small mockup application to receive the data which is send from the SIGENCE Scenario Tool.
    /// The main thread is blocking as long as data are received. The received data is
    /// displayed in the console window and stored in a file in the TEMP directory to check it.
    /// </summary>
    sealed class MockUp
    {

        /// <summary>
        /// The divider
        /// </summary>
        static private readonly string DIVIDER = new string('-', 80);

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Console.Title = Tool.ProductTitle;

            //-------------------------

            var settings = Properties.Settings.Default;

            string strTempPath = Path.GetTempPath();

            ulong iReceivedCounter = 0;

            //-------------------------

            using (UdpClient client = new UdpClient(settings.UDPServerPort))
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(settings.UDPServerHost), settings.UDPServerPort);

                Console.Out.WriteLine("Connected to {0}:{1} and waiting for incoming data ...", settings.UDPServerHost, settings.UDPServerPort);

                // A neverending story ...
                while (true)
                {
                    byte[] baReceived = client.Receive(ref ep);

                    string strReceived = Encoding.Default.GetString(baReceived);

                    string strDateTime = DateTime.Now.Fmt_YYYYMMDD_HHMMSSFFF();
                    Console.Out.WriteLine(DIVIDER);
                    Console.Out.WriteLine("[{0}] {1} ({2} Bytes)", ++iReceivedCounter, strDateTime, strReceived.Length);
                    Console.Out.WriteLine(strReceived);

                    string strFilename = string.Format("{0}sigence_data_{1}.xml", strTempPath, strDateTime);
                    File.WriteAllText(strFilename, strReceived);
                }
            }
        }

    } // end sealed class MockUp
}
