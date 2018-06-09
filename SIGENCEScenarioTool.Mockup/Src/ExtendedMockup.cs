using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;



namespace SIGENCEScenarioTool.Mockup
{
    /// <summary>
    /// 
    /// </summary>
    static class ExtendedMockup
    {
        /// <summary>
        /// The divider
        /// </summary>
        static private readonly string DIVIDER = new string('-', 80);


        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static public void Main(string[] args)
        {
            Properties.Settings settings = Properties.Settings.Default;

            IPEndPoint epReceive = new IPEndPoint(IPAddress.Parse(settings.UDPServerHost), settings.UDPPortReceiving);
            UdpClient ucReceive = new UdpClient(settings.UDPPortReceiving);

            Socket sSenderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint epSenderEndPoint = new IPEndPoint(IPAddress.Parse(settings.UDPServerHost), settings.UDPPortSending);

            Console.Out.WriteLine("Listening To {0}:{1} And Waiting For Incoming Data ...", settings.UDPServerHost, settings.UDPPortReceiving);

            // A neverending story ...
            while (true)
            {
                byte[] baReceived = ucReceive.Receive(ref epReceive);

                string strReceived = Encoding.Default.GetString(baReceived);
                string strDateTime = DateTime.Now.Fmt_YYYYMMDD_HHMMSSFFF();

                Console.Out.WriteLine(DIVIDER);
                Console.Out.WriteLine("{0}: Received {1} Bytes.", strDateTime, strReceived.Length);

                try
                {
                    XDocument xDevice = XDocument.Parse(strReceived);
                    RFDevice device = RFDevice.FromXml(xDevice.Root);

                    Console.Out.WriteLine("Id        : {0}", device.Id);
                    Console.Out.WriteLine("Name      : {0}", device.Name);
                    Console.Out.WriteLine("Latitude  : {0}", device.Latitude);
                    Console.Out.WriteLine("Longitude : {0}", device.Longitude);
                    Console.Out.WriteLine("StartTime : {0}", device.StartTime);

                    // ----------------------------------------------------

                    GeoLocalizationResult gcr = new GeoLocalizationResult
                    {
                        Id = device.Id,
                        Latitude = device.Latitude,
                        Longitude = device.Longitude,
                        Altitude = 0,
                        LocalizationTime = DateTime.Now.Ticks
                    };

                    XElement eGeoLocalizationResult = gcr.ToXml();

                    byte[] baMessage = Encoding.Default.GetBytes(eGeoLocalizationResult.ToDefaultString());

                    sSenderSocket.SendTo(baMessage, epSenderEndPoint);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
        }

    } // end static class ExtendedMockup
}
