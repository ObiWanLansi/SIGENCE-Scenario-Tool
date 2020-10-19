using System;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

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
        /// Loads the file.
        /// </summary>
        /// <param name="strInputFilename">The string input filename.</param>
        internal void LoadFile( string strInputFilename )
        {
            this.Cursor = Cursors.Wait;

            this.CurrentFile = strInputFilename;

            try
            {
                XDocument xdoc = XDocument.Load(strInputFilename);

                //---------------------------------------------------------

                XElement eGeneralSettings = xdoc.Root.Element("GeneralSettings");
                this.Zoom = eGeneralSettings.GetDoubleFromNode("Zoom") ?? this.Zoom;
                //ShowCenter = eGeneralSettings.GetBoolFromNode("ShowCenter") ?? ShowCenter;
                //this.ScenarioDescription = eGeneralSettings.GetStringFromCData( "ScenarioDescription" );

                string strMapProvider = eGeneralSettings.GetStringFromNode("MapProvider") ?? this.MapProvider.Name;
                this.MapProvider = GetProviderFromString(strMapProvider);

                XElement eCenterPosition = eGeneralSettings.Element("CenterPosition");
                this.Latitude = eCenterPosition.GetDoubleFromNodePoint("Latitude") ?? this.Latitude;
                this.Longitude = eCenterPosition.GetDoubleFromNodePoint("Longitude") ?? this.Longitude;

                //---------------------------------------------------------

                // Create not an new instance, only load the data into the existing one ...
                this.MetaInformation.LoadFromXml(xdoc.Root);

                // Ist ja noch nix passiert ...
                this.DescriptionMarkdownChanged = false;
                this.DescriptionStylesheetChanged = false;

                //this.tecDescription.Text = this.MetaInformation.Description;
                //this.tecStyleSheet.Text = this.MetaInformation.Stylesheet;

                //---------------------------------------------------------

                XElement eRFDeviceCollection = xdoc.Root.Element("RFDeviceCollection");

                foreach( XElement e in eRFDeviceCollection.Elements() )
                {
                    AddRFDevice(RFDevice.FromXml(e));
                }

                AddFileHistory(strInputFilename);
            }
            catch( Exception ex )
            {
                MB.Error(ex);
            }

            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Loads the file.
        /// </summary>
        private void LoadFile()
        {
            if( this.ofdLoadSIGENCEScenario.ShowDialog() == true )
            {
                Reset();

                LoadFile(this.ofdLoadSIGENCEScenario.FileName);
            }
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="strOutputFilename">The string output filename.</param>
        internal void SaveFile( string strOutputFilename )
        {
            this.Cursor = Cursors.Wait;

            try
            {
                XElement eSIGENCEScenarioTool = new XElement("SIGENCEScenarioTool", new XAttribute("Version", Tool.Version));

                //-------------------------------------------------------------

                XElement eGeneralSettings = new XElement("GeneralSettings");

                //eGeneralSettings.Add(new XElement("ScenarioDescription", new XCData(this.ScenarioDescription ?? "")));
                eGeneralSettings.Add(new XElement("Zoom", this.Zoom));
                //eGeneralSettings.Add(new XElement("ShowCenter", mcMapControl.ShowCenter));
                eGeneralSettings.Add(new XElement("CenterPosition",
                    new XElement("Latitude", this.Latitude),
                    new XElement("Longitude", this.Longitude))
                );
                eGeneralSettings.Add(new XElement("MapProvider", this.MapProvider));

                eSIGENCEScenarioTool.Add(eGeneralSettings);

                //-------------------------------------------------------------

                eSIGENCEScenarioTool.Add(this.MetaInformation.ToXml());

                //-------------------------------------------------------------

                XElement eRFDeviceCollection = new XElement("RFDeviceCollection");

                foreach( RFDevice d in from rfdevice in this.RFDeviceViewModelCollection select rfdevice.RFDevice )
                {
                    eRFDeviceCollection.Add(d.ToXml());
                }

                eSIGENCEScenarioTool.Add(eRFDeviceCollection);

                //-------------------------------------------------------------

                eSIGENCEScenarioTool.SaveDefault(strOutputFilename);
            }
            catch( Exception ex )
            {
                MB.Error(ex);
            }

            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        private void SaveFile()
        {
            if( this.CurrentFile == null )
            {
                if( this.sfdSaveSIGENCEScenario.ShowDialog() == true )
                {
                    this.CurrentFile = this.sfdSaveSIGENCEScenario.FileName;
                }
                else
                {
                    return;
                }
            }

            SaveFile(this.CurrentFile);
        }


        /// <summary>
        /// Saves with different filename.
        /// </summary>
        private void SaveAsFile()
        {
            this.CurrentFile = null;

            SaveFile();
        }

    } // end public partial class MainWindow
}
