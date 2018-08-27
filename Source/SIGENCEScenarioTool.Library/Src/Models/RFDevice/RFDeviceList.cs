using System;
using System.Collections.Generic;
using System.Linq;

using GMap.NET;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class RFDeviceList : List<RFDevice>
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
        public RFDeviceList(int iInitialSize) : base(iInitialSize)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceList"/> class.
        /// </summary>
        /// <param name="collection">Die Auflistung, deren Elemente in die neue Liste kopiert werden.</param>
        public RFDeviceList(IEnumerable<RFDevice> collection)
            : base(collection)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The randomizer.
        /// </summary>
        static private readonly Random r = new Random();


        /// <summary>
        /// Creates the randomized rf device list.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        /// <param name="pllCenter">The PLL center.</param>
        /// <param name="bEnsureRefDevice">if set to <c>true</c> [b ensure reference device].</param>
        /// <returns></returns>
        static public RFDeviceList CreateRandomizedRFDeviceList(int iMaxCount, PointLatLng pllCenter, bool bEnsureRefDevice = false)
        {
            RFDeviceList list = new RFDeviceList(iMaxCount);

            for (int i = 0; i < iMaxCount; i++)
            {
                list.Add(new RFDevice
                {
                    Id = r.Next(-1000, 1000),
                    DeviceSource = DeviceSource.Automatic,
                    Name = string.Format("RFDevice #{0}", i),
                    Latitude = pllCenter.Lat + (r.NextBool() ? (r.NextDouble() * 0.05) : (r.NextDouble() * 0.05) * -1),
                    Longitude = pllCenter.Lng + (r.NextBool() ? (r.NextDouble() * 0.05) : (r.NextDouble() * 0.05) * -1),
                    //Latitude = (r.NextDouble() * 0.05) + pllCenter.Lat,
                    //Longitude = (r.NextDouble() * 0.05) + pllCenter.Lng,
                    Altitude = (uint)r.Next(12345),
                    RxTxType = r.NextEnum<RxTxType>(),
                    AntennaType = r.NextEnum<AntennaType>(),
                    CenterFrequency_Hz = (uint)r.Next(85, 105) * 100000,
                    Bandwith_Hz = (uint)r.Next(10, 20) * 1000,
                    Gain_dB = r.Next(140),
                    SignalToNoiseRatio_dB = (uint)r.Next(140),
                    Roll = r.Next(-90, 90),
                    Pitch = r.Next(-90, 90),
                    Yaw = r.Next(-90, 90),
                    XPos = r.Next(-74, 74),
                    YPos = r.Next(-74, 74),
                    ZPos = r.Next(-74, 74),
                    Remark = r.NextObject(Tool.ALLPANGRAMS)
                });
            }

            if (bEnsureRefDevice == true)
            {
                RFDevice refdev = list.FirstOrDefault(d => d.Id == 0);

                if (refdev == null)
                {
                    refdev = list.First();
                }

                refdev.Id = 0;
                refdev.Latitude = pllCenter.Lat;
                refdev.Longitude = pllCenter.Lng;
            }

            return list;
        }

    } // end sealed public class RFDeviceList
}
