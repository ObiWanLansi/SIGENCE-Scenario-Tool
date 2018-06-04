using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Mockup
{
    /// <summary>
    /// 
    /// </summary>
    sealed class MockUp
    {

        /// <summary>
        /// 
        /// </summary>
        static private readonly string DIVIDER = new string( '-' , 80 );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main( string [] args )
        {
            Console.Title = Tool.ProductTitle;

            var settings = Properties.Settings.Default;

            ulong iReceivedCounter = 0;

            using( UdpClient client = new UdpClient( settings.UDPServerPort ) )
            {
                IPEndPoint ep = new IPEndPoint( IPAddress.Parse( settings.UDPServerHost ) , settings.UDPServerPort );

                Console.Out.WriteLine( "Connected to {0}:{1} and waiting for incoming data ..." , settings.UDPServerHost , settings.UDPServerPort );

                // The neverending story ...
                while( true )
                {
                    byte [] baReceived = client.Receive( ref ep );

                    string strReceived = Encoding.Default.GetString( baReceived );

                    //if( strReceived.Equals( "end" ) )
                    //{
                    //    goto END;
                    //}

                    Console.Out.WriteLine( DIVIDER );
                    Console.Out.WriteLine( "[{0}] {1} ({2} Bytes)" , ++iReceivedCounter , DateTime.Now.Fmt_YYYYMMDD_HHMMSSFFF() , strReceived.Length );
                    Console.Out.WriteLine( strReceived );
                }

                //END:
                //client.Close();
            }
        }

    } // end sealed class MockUp
}
