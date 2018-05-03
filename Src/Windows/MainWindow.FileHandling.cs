using System;
using System.Linq;
using System.Xml.Linq;

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
            if (ofd.ShowDialog() == true)
            {
                Reset();
                CurrentFile = ofd.FileName;

                try
                {
                    XDocument xdoc = XDocument.Load(CurrentFile);

                    foreach (XElement e in xdoc.Root.Elements())
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
                if (sfd.ShowDialog() == true)
                {
                    CurrentFile = sfd.FileName;
                }
                else
                {
                    return;
                }
            }

            try
            {
                XElement eTransmitter = new XElement("TransmitterCollection");

                foreach (Transmitter t in from transmitter in TransmitterCollection select transmitter.Transmitter)
                {
                    eTransmitter.Add(t.ToXml());
                }

                eTransmitter.SaveDefault(CurrentFile);
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
