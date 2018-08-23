using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Tools;

using NTS = NetTopologySuite.Geometries;



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
        private void MenuItem_ChartingTest_Click( object sender , RoutedEventArgs e )
        {
            ChartingDialog cw = new ChartingDialog( new RFDeviceList( from device in RFDevicesCollection select device.RFDevice ) );
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
                if( ReceivedData == true )
                {
                    Blink.SetColor( Colors.Green );
                }
                else
                {
                    Blink.Off();
                }
            }
            catch( Exception )
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
            if( tUDPServer == null )
            {
                tUDPServer = new Thread( UDPReceiveData )
                {
                    IsBackground = true ,
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
            if( tUDPServer != null )
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
                client = new UdpClient( settings.UDPPortReceiving );
                {
                    IPEndPoint ep = new IPEndPoint( IPAddress.Parse( settings.UDPHost ) , settings.UDPPortReceiving );

                    // A neverending story ...
                    while( true )
                    {
                        // Obwohl der Thread Aborted wird beendet er das Receiver nicht und somit auch nicht Thread :-(
                        // Erst wenn er was empfangen hat merkt er das er Aborted ist und die Expcetion tritt auf ...
                        byte [] baReceived = client.Receive( ref ep );

                        string strReceived = Encoding.Default.GetString( baReceived );

                        DebugOutput += strReceived + "\n\n";
                        ReceivedData = true;
                    }
                }
            }
            catch( ThreadAbortException )
            {
                // Do nothing ...
                //Debug.WriteLine(ex.Message);
            }
            catch( Exception ex )
            {
                MB.Warning( ex.Message );
            }
            finally
            {
                if( client != null )
                {
                    try
                    {
                        client.Close();
                        client.Dispose();
                        client = null;
                    }
                    catch( Exception )
                    {

                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the scenario report.
        /// </summary>
        private void CreateScenarioReport()
        {
            if( string.IsNullOrEmpty( CurrentFile ) )
            {
                MB.Information( "The scenario has not been saved yet.\nSave it first and then try again." );
                return;
            }

            Cursor = Cursors.Wait;

            FileInfo fiCurrentFile = new FileInfo( CurrentFile );

            string strOutputFilename = string.Format( "{0}{1}.html" , Path.GetTempPath() , fiCurrentFile.GetFilenameWithoutExtension() );

            StringBuilder sb = new StringBuilder( 8192 );

            sb.Append( "<!DOCTYPE html><html><head><title>Scenario Documentation</title></head><body>" );

            //-----------------------------------------------------------------

            sb.AppendFormat( "<center style=\"width: 100%; border: 1px solid black; background-color: lightblue;\"><h1>{0}</h1></center>" , fiCurrentFile.GetFilenameWithoutExtension() );

            sb.Append( "<hr />" );

            //-----------------------------------------------------------------

            if( string.IsNullOrEmpty( ScenarioDescription ) == false )
            {
                sb.Append( ScenarioDescription );
            }

            //-----------------------------------------------------------------

            //Guid gScreenshot = Guid.NewGuid();
            //string strOutputFilenameScreenshot = string.Format("{0}{1}.png", Path.GetTempPath(), gScreenshot);
            //var screenshot = Tools.Windows.GetWPFScreenshot(mcMapControl);
            //Tools.Windows.SaveWPFScreenshot(screenshot, strOutputFilenameScreenshot);
            //sb.AppendFormat("<center><img src=\"{0}.png\" style=\"border: 1px solid black;\"/></center>", gScreenshot);

            //-----------------------------------------------------------------



            //-----------------------------------------------------------------

            sb.Append( "</body></html> " );

            File.WriteAllText( strOutputFilename , sb.ToString() , Encoding.Default );

            Tools.Windows.OpenWithDefaultApplication( strOutputFilename );

            Cursor = Cursors.Arrow;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Handles the Click event of the MenuItem_InsertHtmlSnippet control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void MenuItem_InsertHtmlSnippet_Click(object sender, RoutedEventArgs e)
        //{
        //    InsertHtmlSnippet((sender as Control).Tag as string);

        //    e.Handled = true;
        //}


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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        private void RemoveStreets()
        {
            List<GMapMarker> lDelete = new List<GMapMarker>();

            foreach( GMapMarker mm in mcMapControl.Markers )
            {
                if( mm.Tag != null && mm.Tag is Highway )
                {
                    lDelete.Add( mm );
                }
            }

            Dispatcher.Invoke( () =>
            {
                lDelete.ForEach( mm => mcMapControl.Markers.Remove( mm ) );
            } );
        }


        /// <summary>
        /// 
        /// </summary>
        [Conditional( "DEBUG" )]
        private void LoadStreets()
        {
            RemoveStreets();

            const string STREETS_DB = @"c:\Lanser\BigData\SQLiteGeoData\streets_bw.sqlite";

            RectLatLng bb = mcMapControl.ViewArea;

            //TODO: Check if the area is not to big ... what is big ?

            SQLiteConnectionStringBuilder csbDatabase = new SQLiteConnectionStringBuilder
            {
                DataSource = STREETS_DB
            };

            using( SQLiteConnection dbConnection = new SQLiteConnection( csbDatabase.ConnectionString ) )
            {
                dbConnection.Open();

                try
                {
                    //                                     0     1   2   3 
                    string strSelectStatement = "select highway,ref,name,way from streets_bw where highway in ('motorway','trunk','primary','secondary')";

                    uint iCounter = 0;

                    DateTime dtStart = DateTime.Now;

                    using( SQLiteCommand dbSelectCommand = new SQLiteCommand( strSelectStatement , dbConnection ) )
                    {
                        using( SQLiteDataReader dbResult = dbSelectCommand.ExecuteReader() )
                        {
                            while( dbResult.Read() )
                            {
                                Highway type = ( Highway ) Enum.Parse( typeof( Highway ) , dbResult.GetString( 0 ) , true );
                                string strRef = dbResult.GetStringOrNull( 1 );
                                string strName = dbResult.GetStringOrNull( 2 );
                                NTS.LineString way = ( NTS.LineString ) dbResult.GetGeometryFromWKB( 3 );

                                if( bb.Contains( GeoHelper.CoordinateToPointLatLng( way.Coordinate ) ) )
                                {
                                    List<PointLatLng> list = new List<PointLatLng>( way.Count );

                                    foreach( var pos in way.Coordinates )
                                    {
                                        list.Add( GeoHelper.CoordinateToPointLatLng( pos ) );
                                    }

                                    GMapRoute mrWay = new GMapRoute( list )
                                    {
                                        Tag = type
                                    };

                                    Dispatcher.Invoke( () =>
                                    {
                                        mcMapControl.Markers.Add( mrWay );

                                        if( mrWay.Shape is System.Windows.Shapes.Path )
                                        {
                                            System.Windows.Shapes.Path path = mrWay.Shape as System.Windows.Shapes.Path;

                                            //path.ToolTip = string.Format( "{0}{1}" , strName , strRef.IsNotEmpty() ? string.Format( " ({0})" , strRef ) : "" );
                                            //path.Cursor = Cursors.Cross;
                                            //path.ForceCursor = true;

                                            switch( type )
                                            {
                                                case Highway.Motorway:
                                                case Highway.Motorway_Link:
                                                    path.Stroke = new SolidColorBrush( Colors.Red );
                                                    path.StrokeThickness = 5;
                                                    break;

                                                case Highway.Trunk:
                                                case Highway.Trunk_Link:
                                                    path.Stroke = new SolidColorBrush( Colors.Orange );
                                                    path.StrokeThickness = 4;
                                                    break;

                                                case Highway.Primary:
                                                case Highway.Primary_Link:
                                                    path.Stroke = new SolidColorBrush( Colors.Yellow );
                                                    path.StrokeThickness = 3;
                                                    break;

                                                case Highway.Secondary:
                                                case Highway.Secondary_Link:
                                                    path.Stroke = new SolidColorBrush( Colors.Black );
                                                    path.StrokeThickness = 2;
                                                    break;

                                            }
                                        }
                                    } );

                                    iCounter++;
                                }
                            }
                        }
                    }

                    DateTime dtStop = DateTime.Now;

                    MB.Information( "Load {0} Streets In {1}." , iCounter , ( dtStop - dtStart ).ToHHMMSSString() );
                }
                catch( Exception ex )
                {
                    MB.Error( ex );
                }
                finally
                {
                    if( dbConnection.State == ConnectionState.Open )
                    {
                        dbConnection.Close();
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_LoadStreets_Click( object sender , RoutedEventArgs e )
        {
            Task t = Task.Run( () => { LoadStreets(); } );
        }

    } // end public partial class MainWindow
}
