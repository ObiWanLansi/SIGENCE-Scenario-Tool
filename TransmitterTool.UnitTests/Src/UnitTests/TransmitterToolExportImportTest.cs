using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

using log4net;

using NUnit.Framework;

using TransmitterTool.Extensions;
using TransmitterTool.Models;
using TransmitterTool.Tools;
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
        static private readonly ILog Log = LogManager.GetLogger( typeof( TransmitterExportImportTest ) );

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// The randomizer.
        /// </summary>
        static private readonly Random r = new Random();


        /// <summary>
        /// Creates the randomized transmitter.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        private TransmitterList CreateRandomizedTransmitter( int iMaxCount )
        {
            TransmitterList lTransmitter = new TransmitterList( iMaxCount );

            for( int iCounter = 1 ; iCounter < iMaxCount + 1 ; iCounter++ )
            {
                lTransmitter.Add( new Transmitter
                {
                    Id = r.Next( -1000 , 1000 ) ,
                    Name = string.Format( "Transmitter #{0}" , iCounter ) ,
                    Latitude = ( r.NextDouble() * 0.05 ) + 49.7454 ,
                    Longitude = ( r.NextDouble() * 0.05 ) + 6.6149 ,
                    Altitude = 0 ,
                    RxTxType = r.NextEnum<RxTxType>() ,
                    AntennaType = r.NextEnum<AntennaType>() ,
                    CenterFrequency_Hz = ( uint ) r.Next( 85 , 105 ) ,
                    Bandwith_Hz = ( uint ) r.Next( 10 , 80 ) ,
                    Gain_dB = 0 ,
                    SignalToNoiseRatio_dB = 0 ,
                    Roll = 0 ,
                    Pitch = 0 ,
                    Yaw = 0 ,
                    XPos = 0 ,
                    YPos = 0 ,
                    ZPos = 0 ,
                    Remark = r.NextObject( Tool.ALLPANGRAMS )
                } );
            }

            return lTransmitter;
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Test, Category( "ExportImport" ), TransmitterToolTestCase( "28321d28-352c-462a-9d87-49b1b3ad7224" ), Description( "Tests if the transmitters are properly exported and imported as XML." )]
        public void Test000_ExportAndImportXml()
        {
            TransmitterToolTestCaseHelper.ShowTestCaseInformation();

            //-----------------------------------------------------------------

            Transmitter tSource = new Transmitter
            {
                Id = -42 ,
                RxTxType = RxTxType.IdealSDR ,
                AntennaType = AntennaType.Unknown ,
                StartTime = 42 ,
                Latitude = 15 ,
                Longitude = 10 ,
                Altitude = 1974 ,
                CenterFrequency_Hz = 90000 ,
                Bandwith_Hz = 10000 ,
                Gain_dB = 5 ,
                SignalToNoiseRatio_dB = 263 ,
                Roll = -42 ,
                Pitch = -42 ,
                Yaw = -42 ,
                XPos = -1 ,
                YPos = -2 ,
                ZPos = -3 ,
                Name = "Han Solo" ,
                Remark = "A Star Wars Story." ,
            };

            XElement e = tSource.ToXml();

            Assert.NotNull( e );

            string strFilename = string.Format( "{0}nunit_transmitter.{1}.xml" , Path.GetTempPath() , DateTime.Now.ToString( "yyyyMMdd_HHmmssfff" ) );

            e.SaveDefault( strFilename );

            Assert.True( File.Exists( strFilename ) );

            //-----------------------------------------------------------------

            XDocument xdoc = XDocument.Load( strFilename );

            Assert.NotNull( xdoc );

            Transmitter tDestination = Transmitter.FromXml( xdoc.Root );

            Assert.NotNull( tDestination );

            Assert.True( tDestination.Equals( tSource ) );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public enum FileFormat : byte
        {
            Csv, Xml, Json

        } // end public enum FileFormat


        static private readonly Array aFormats = Enum.GetValues( typeof( FileFormat ) );


        [Test, Category( "ExportImport" ), TransmitterToolTestCase( "9ABC401E-9790-4FD4-9339-371914053AD8" ), Description( "Tests if the transmitters are properly exported in different formats." )]
        public void Test001_ExportTransmitter( [ValueSource( "aFormats" )] FileFormat format , [Values( 10 , 50 , 200 , 1000 )] int iCount )
        {
            TransmitterToolTestCaseHelper.ShowTestCaseInformation();

            //-----------------------------------------------------------------

            TransmitterList tl = CreateRandomizedTransmitter( iCount );

            string strFilename = string.Format( "{0}nunit_transmitter.{1}.{2}" , Path.GetTempPath() , DateTime.Now.ToString( "yyyyMMdd_HHmmssfff" ) , format );

            switch( format )
            {
                case FileFormat.Xml:
                    tl.SaveAsXml( strFilename );
                    break;

                case FileFormat.Csv:
                    tl.SaveAsCsv( strFilename );
                    break;

                case FileFormat.Json:
                    tl.SaveAsJson( strFilename );
                    break;
            }

            //-----------------------------------------------------------------

            Assert.True( File.Exists( strFilename ) );
        }

    } // end sealed class TransmitterExportImportTest
}
