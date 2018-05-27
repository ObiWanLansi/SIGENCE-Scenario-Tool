using System;
using System.IO;
using System.Xml.Linq;
using log4net;

using NUnit.Framework;
using TransmitterTool.Models;
using TransmitterTool.Extensions;

using TransmitterTool.UnitTest.Attributes;
using TransmitterTool.UnitTest.Framework;



namespace TransmitterTool.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    [TransmitterToolTestAction]
    sealed class TransmitterExportImportTest
    {
        /// <summary>
        /// Logger zum Ausgeben der Protokollierung.
        /// </summary>
        static private readonly ILog Log = LogManager.GetLogger(typeof(TransmitterExportImportTest));

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Test, Category("ExportImport"), TransmitterToolTestCase("28321d28-352c-462a-9d87-49b1b3ad7224"), Description("Tests if the transmitters are properly exported and imported as XML.")]
        public void Test000_ExportImportXML()
        {
            TransmitterToolTestCaseHelper.ShowTestCaseInformation();

            //-----------------------------------------------------------------

            Transmitter tSource = new Transmitter
            {
                Id = -42,
                RxTxType = RxTxType.IdealSDR,
                AntennaType = AntennaType.Unknown,
                StartTime = 42,
                Latitude = 15,
                Longitude = 10,
                Altitude = 1974,
                Bandwith = 1000,
                Gain = 5,
                Roll = -42,
                Pitch = -42,
                Yaw = -42,
                XPos = -1,
                YPos = -2,
                ZPos = -3,
                Name = "Han Solo",
                Remark = "A Star Wars Story.",
                SignalToNoiseRatio = 263
            };

            XElement e = tSource.ToXml();

            Assert.NotNull(e);

            string strFilename = string.Format("{0}nunit_transmitter.{1}.xml", Path.GetTempPath(), DateTime.Now.ToString("yyyyMMdd_HHmmssfff"));

            e.SaveDefault(strFilename);

            Assert.True(File.Exists(strFilename));

            //-----------------------------------------------------------------

            XDocument xdoc = XDocument.Load(strFilename);

            Transmitter tDestination = Transmitter.FromXml(xdoc.Root);

            Assert.NotNull(tDestination);

            Assert.True(tDestination.Equals(tSource));
        }

    } // end sealed class TransmitterExportImportTest
}
