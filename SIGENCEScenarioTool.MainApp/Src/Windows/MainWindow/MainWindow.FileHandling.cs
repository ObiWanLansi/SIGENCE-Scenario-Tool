using System;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

using GMap.NET.MapProviders;

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
        /// News the file.
        /// </summary>
        private void NewFile()
        {
            Reset();
        }


        /// <summary>
        /// Opens the file.
        /// </summary>
        private void OpenFile()
        {
            if( ofdLoadSIGENCEScenario.ShowDialog() == true )
            {
                Cursor = Cursors.Wait;

                Reset();
                CurrentFile = ofdLoadSIGENCEScenario.FileName;

                try
                {
                    XDocument xdoc = XDocument.Load( CurrentFile );

                    //---------------------------------------------------------

                    XElement eGeneralSettings = xdoc.Root.Element( "GeneralSettings" );
                    Zoom = eGeneralSettings.GetDoubleFromNode( "Zoom" ) ?? Zoom;
                    ShowCenter = eGeneralSettings.GetBoolFromNode( "ShowCenter" ) ?? ShowCenter;

                    string strMapProvider = eGeneralSettings.GetStringFromNode( "MapProvider" ) ?? MapProvider.Name;
                    foreach( var mp in GMapProviders.List )
                    {
                        if( mp.Name == strMapProvider )
                        {
                            MapProvider = mp;
                            break;
                        }
                    }

                    XElement eCenterPosition = eGeneralSettings.Element( "CenterPosition" );
                    Latitude = eCenterPosition.GetDoubleFromNodePoint( "Latitude" ) ?? Latitude;
                    Longitude = eCenterPosition.GetDoubleFromNodePoint( "Longitude" ) ?? Longitude;

                    ScenarioDescription = eGeneralSettings.GetStringFromCData( "ScenarioDescription" );

                    //---------------------------------------------------------

                    XElement eRFDeviceCollection = xdoc.Root.Element( "RFDeviceCollection" );

                    foreach( XElement e in eRFDeviceCollection.Elements() )
                    {
                        AddRFDevice( RFDevice.FromXml( e ) );
                    }
                }
                catch( Exception ex )
                {
                    MB.Error( ex );
                }

                Cursor = Cursors.Arrow;
            }
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        private void SaveFile()
        {
            if( CurrentFile == null )
            {
                if( sfdSaveSIGENCEScenario.ShowDialog() == true )
                {
                    CurrentFile = sfdSaveSIGENCEScenario.FileName;
                }
                else
                {
                    return;
                }
            }

            Cursor = Cursors.Wait;

            try
            {
                XElement eSIGENCEScenarioTool = new XElement( "SIGENCEScenarioTool" , new XAttribute( "Version" , Tool.Version ) );

                //-------------------------------------------------------------

                XElement eGeneralSettings = new XElement( "GeneralSettings" );

                eGeneralSettings.Add( new XElement( "ScenarioDescription" , new XCData( ScenarioDescription != null ? ScenarioDescription : "" ) ) );
                eGeneralSettings.Add( new XElement( "Zoom" , mcMapControl.Zoom ) );
                eGeneralSettings.Add( new XElement( "ShowCenter" , mcMapControl.ShowCenter ) );
                eGeneralSettings.Add( new XElement( "CenterPosition" ,
                    new XElement( "Latitude" , mcMapControl.Position.Lat ) ,
                    new XElement( "Longitude" , mcMapControl.Position.Lng ) )
                );
                eGeneralSettings.Add( new XElement( "MapProvider" , mcMapControl.MapProvider ) );

                eSIGENCEScenarioTool.Add( eGeneralSettings );

                //-------------------------------------------------------------

                XElement eRFDeviceCollection = new XElement( "RFDeviceCollection" );

                foreach( RFDevice t in from rfdevice in RFDevicesCollection select rfdevice.RFDevice )
                {
                    eRFDeviceCollection.Add( t.ToXml() );
                }

                eSIGENCEScenarioTool.Add( eRFDeviceCollection );

                //-------------------------------------------------------------

                eSIGENCEScenarioTool.SaveDefault( CurrentFile );
            }
            catch( Exception ex )
            {
                MB.Error( ex );
            }

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Saves with different filename.
        /// </summary>
        private void SaveAsFile()
        {
            CurrentFile = null;

            SaveFile();
        }

    } // end public partial class MainWindow
}
