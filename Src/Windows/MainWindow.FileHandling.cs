using System;
using System.Linq;
using System.Xml.Linq;

using GMap.NET.MapProviders;

using TransmitterTool.Extensions;
using TransmitterTool.Models;
using TransmitterTool.Tools;



namespace TransmitterTool.Windows
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
            if (ofdLoadTransmitter.ShowDialog() == true)
            {
                Reset();
                CurrentFile = ofdLoadTransmitter.FileName;

                try
                {
                    XDocument xdoc = XDocument.Load(CurrentFile);

                    //---------------------------------------------------------

                    XElement eGeneralSettings = xdoc.Root.Element("GeneralSettings");
                    Zoom = eGeneralSettings.GetDoubleFromNode("Zoom") ?? Zoom;
                    ShowCenter = eGeneralSettings.GetBoolFromNode("ShowCenter") ?? ShowCenter;

                    string strMapProvider = eGeneralSettings.GetStringFromNode("MapProvider") ?? MapProvider.Name;
                    foreach (var mp in GMapProviders.List)
                    {
                        if (mp.Name == strMapProvider)
                        {
                            MapProvider = mp;
                            break;
                        }
                    }

                    XElement eCenterPosition = eGeneralSettings.Element("CenterPosition");
                    Latitude = eCenterPosition.GetDoubleFromNodePoint("Latitude") ?? Latitude;
                    Longitude = eCenterPosition.GetDoubleFromNodePoint("Longitude") ?? Longitude;

                    //---------------------------------------------------------

                    XElement eTransmitterList = xdoc.Root.Element("TransmitterCollection");

                    foreach (XElement e in eTransmitterList.Elements())
                    {
                        AddTransmitter(Transmitter.FromXml(e));
                    }
                }
                catch (Exception ex)
                {
                    MB.Error(ex);
                }
            }
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        private void SaveFile()
        {
            if (CurrentFile == null)
            {
                if (sfdSaveTransmitter.ShowDialog() == true)
                {
                    CurrentFile = sfdSaveTransmitter.FileName;
                }
                else
                {
                    return;
                }
            }

            try
            {
                XElement eTransmitterTool = new XElement("TransmitterTool", new XAttribute("Version", Tool.Version));

                //-------------------------------------------------------------

                XElement eGeneralSettings = new XElement("GeneralSettings");

                eGeneralSettings.Add(new XElement("Zoom", mcMapControl.Zoom));
                eGeneralSettings.Add(new XElement("ShowCenter", mcMapControl.ShowCenter));
                eGeneralSettings.Add(new XElement("CenterPosition",
                    new XElement("Latitude", mcMapControl.Position.Lat),
                    new XElement("Longitude", mcMapControl.Position.Lng))
                );
                eGeneralSettings.Add(new XElement("MapProvider", mcMapControl.MapProvider));

                eTransmitterTool.Add(eGeneralSettings);

                //-------------------------------------------------------------

                XElement eTransmitter = new XElement("TransmitterCollection");

                foreach (Transmitter t in from transmitter in TransmitterCollection select transmitter.Transmitter)
                {
                    eTransmitter.Add(t.ToXml());
                }

                eTransmitterTool.Add(eTransmitter);

                //-------------------------------------------------------------

                eTransmitterTool.SaveDefault(CurrentFile);
            }
            catch (Exception ex)
            {
                MB.Error(ex);
            }
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
