using System;
using System.Collections.Generic;
using System.Linq;

using GMap.NET;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models.RxTxTypes;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RFDeviceList : List<RFDevice>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceList"/> class.
        /// </summary>
        public RFDeviceList()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceList"/> class.
        /// </summary>
        /// <param name="iInitialSize">Initial size of the i.</param>
        public RFDeviceList( int iInitialSize ) : base( iInitialSize )
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceList"/> class.
        /// </summary>
        /// <param name="collection">Die Auflistung, deren Elemente in die neue Liste kopiert werden.</param>
        public RFDeviceList( IEnumerable<RFDevice> collection )
            : base( collection )
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The randomizer.
        /// </summary>
        private static readonly Random r = new Random();


        /// <summary>
        /// Creates the randomized rf device list.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        /// <param name="pllCenter">The PLL center.</param>
        /// <param name="bEnsureRefDevice">if set to <c>true</c> [b ensure reference device].</param>
        /// <returns></returns>
        public static RFDeviceList CreateRandomizedRFDeviceList( int iMaxCount, PointLatLng pllCenter, bool bEnsureRefDevice = false )
        {
            RFDeviceList list = new RFDeviceList( iMaxCount );

            List<RxTxType> RxTxTypes = new List<RxTxType>( Models.RxTxTypes.RxTxTypes.Values );

            for(int i = 1 ; i < iMaxCount + 1 ; i++)
            {
                list.Add( new RFDevice
                {
                    Id = r.Next( -1000, 1000 ),
                    DeviceSource = DeviceSource.Automatic,
                    Name = $"RFDevice #{i}",

                    StartTime = Math.Round( i + ((double)1 / i), 2 ),

                    Latitude = pllCenter.Lat + (r.NextBool() ? (r.NextDouble() * 0.05) : (r.NextDouble() * 0.05) * -1),
                    Longitude = pllCenter.Lng + (r.NextBool() ? (r.NextDouble() * 0.05) : (r.NextDouble() * 0.05) * -1),
                    Altitude = r.Next( 12345 ),

                    //RxTxType = r.NextEnum<RxTxType>(),
                    RxTxType = r.NextObject( RxTxTypes ),
                    AntennaType = r.NextEnum<AntennaType>(),

                    CenterFrequency_Hz = r.Next( 85, 105 ) * 100000 + r.NextDouble(),
                    Bandwidth_Hz = r.Next( 10, 20 ) * 1000 + r.NextDouble(),
                    Gain_dB = r.Next( 140 ) + r.NextDouble(),
                    SignalToNoiseRatio_dB = r.Next( 140 ) + r.NextDouble(),

                    Roll = r.Next( -90, 90 ),
                    Pitch = r.Next( -90, 90 ),
                    Yaw = r.Next( -180, 180 ),

                    XPos = r.Next( -74, 74 ),
                    YPos = r.Next( -74, 74 ),
                    ZPos = r.Next( -74, 74 ),

                    Remark = r.NextObject( Tool.ALLPANGRAMS )
                } );
            }

            if(bEnsureRefDevice == true)
            {
                RFDevice refdev = list.FirstOrDefault( d => d.Id == 0 ) ?? list.First();

                refdev.Id = 0;
                refdev.Latitude = pllCenter.Lat;
                refdev.Longitude = pllCenter.Lng;
            }

            return list;
        }

    } // end public sealed class RFDeviceList
}
