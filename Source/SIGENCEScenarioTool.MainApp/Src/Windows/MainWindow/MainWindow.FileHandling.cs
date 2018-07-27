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
        public void NewFile()
        {
            Reset();
        }


        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="strInputFilename">The string input filename.</param>
        public void OpenFile(string strInputFilename)
        {
            Cursor = Cursors.Wait;

            CurrentFile = strInputFilename;

            try
            {
                XDocument xdoc = XDocument.Load(strInputFilename);

                //---------------------------------------------------------

                XElement eGeneralSettings = xdoc.Root.Element("GeneralSettings");
                Zoom = eGeneralSettings.GetDoubleFromNode("Zoom") ?? Zoom;
                //ShowCenter = eGeneralSettings.GetBoolFromNode("ShowCenter") ?? ShowCenter;
                ScenarioDescription = eGeneralSettings.GetStringFromCData("ScenarioDescription");

                string strMapProvider = eGeneralSettings.GetStringFromNode("MapProvider") ?? MapProvider.Name;
                //foreach (var mp in GMapProviders.List)
                //{
                //    if (mp.Name == strMapProvider)
                //    {
                //        MapProvider = mp;
                //        break;
                //    }
                //}
                MapProvider = GetProviderFromString(strMapProvider);

                XElement eCenterPosition = eGeneralSettings.Element("CenterPosition");
                Latitude = eCenterPosition.GetDoubleFromNodePoint("Latitude") ?? Latitude;
                Longitude = eCenterPosition.GetDoubleFromNodePoint("Longitude") ?? Longitude;

                //---------------------------------------------------------

                XElement eRFDeviceCollection = xdoc.Root.Element("RFDeviceCollection");

                foreach (XElement e in eRFDeviceCollection.Elements())
                {
                    AddRFDevice(RFDevice.FromXml(e));
                }
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Opens the file.
        /// </summary>
        public void OpenFile()
        {
            if (ofdLoadSIGENCEScenario.ShowDialog() == true)
            {
                Reset();

                OpenFile(ofdLoadSIGENCEScenario.FileName);
            }
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="strOutputFilename">The string output filename.</param>
        public void SaveFile(string strOutputFilename)
        {
            Cursor = Cursors.Wait;

            try
            {
                XElement eSIGENCEScenarioTool = new XElement("SIGENCEScenarioTool", new XAttribute("Version", Tool.Version));

                //-------------------------------------------------------------

                XElement eGeneralSettings = new XElement("GeneralSettings");

                eGeneralSettings.Add(new XElement("ScenarioDescription", new XCData(ScenarioDescription ?? "")));
                eGeneralSettings.Add(new XElement("Zoom", Zoom));
                //eGeneralSettings.Add(new XElement("ShowCenter", mcMapControl.ShowCenter));
                eGeneralSettings.Add(new XElement("CenterPosition",
                    new XElement("Latitude", Latitude),
                    new XElement("Longitude", Longitude))
                );
                eGeneralSettings.Add(new XElement("MapProvider", MapProvider));

                eSIGENCEScenarioTool.Add(eGeneralSettings);

                //-------------------------------------------------------------

                XElement eRFDeviceCollection = new XElement("RFDeviceCollection");

                foreach (RFDevice t in from rfdevice in RFDevicesCollection select rfdevice.RFDevice)
                {
                    eRFDeviceCollection.Add(t.ToXml());
                }

                eSIGENCEScenarioTool.Add(eRFDeviceCollection);

                //-------------------------------------------------------------

                eSIGENCEScenarioTool.SaveDefault(strOutputFilename);
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }

            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        public void SaveFile()
        {
            if (CurrentFile == null)
            {
                if (sfdSaveSIGENCEScenario.ShowDialog() == true)
                {
                    CurrentFile = sfdSaveSIGENCEScenario.FileName;
                }
                else
                {
                    return;
                }
            }

            SaveFile(CurrentFile);
        }


        /// <summary>
        /// Saves with different filename.
        /// </summary>
        public void SaveAsFile()
        {
            CurrentFile = null;

            SaveFile();
        }

    } // end public partial class MainWindow
}
