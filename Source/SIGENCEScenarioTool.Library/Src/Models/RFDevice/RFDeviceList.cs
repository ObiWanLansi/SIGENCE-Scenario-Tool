using System;
using System.Collections.Generic;
using System.Linq;

using GMap.NET;

using SIGENCEScenarioTool.Datatypes.Standard;
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
        private static readonly Random RAND = new Random();


        /// <summary>
        /// Some Dummy Technical Parameters For Generating Random Devices.
        /// </summary>
        private static readonly StringList TECHNICAL_PARAMETERS = new StringList
        {
            "PARAM1:VALUE1; PARAM2:VALUE2; PARAM3:VALUE3;",
            "PARAM3:VALUE3; PARAM2:VALUE2; PARAM1:VALUE1;",
            "PARAM2:VALUE2; PARAM3:VALUE3; PARAM1:VALUE1;",
        };

        /// <summary>
        /// Creates the randomized rf device list.
        /// </summary>
        /// <param name="iMaxCount">The i maximum count.</param>
        /// <param name="pllCenter">The PLL center.</param>
        /// <param name="bEnsureRefDevice">if set to <c>true</c> [b ensure reference device].</param>
        /// <returns></returns>
        public static RFDeviceList CreateRandomizedRFDeviceList(int iMaxCount, PointLatLng pllCenter, bool bEnsureRefDevice = false)
        {
            RFDeviceList list = new RFDeviceList(iMaxCount);

            List<RxTxType> RxTxTypes = new List<RxTxType>(Models.RxTxTypes.RxTxTypes.Values);

            for (int i = 1; i < iMaxCount + 1; i++)
            {
                list.Add(new RFDevice
                {
                    Id = RAND.Next(-1000, 1000),
                    DeviceSource = DeviceSource.Automatic,
                    Name = $"RFDevice #{i}",

                    StartTime = Math.Round(i + ((double)1 / i), 2),

                    Latitude = pllCenter.Lat + (RAND.NextBool() ? (RAND.NextDouble() * 0.05) : (RAND.NextDouble() * 0.05) * -1),
                    Longitude = pllCenter.Lng + (RAND.NextBool() ? (RAND.NextDouble() * 0.05) : (RAND.NextDouble() * 0.05) * -1),
                    Altitude = RAND.Next(12345),

                    //RxTxType = r.NextEnum<RxTxType>(),
                    RxTxType = RAND.NextObject(RxTxTypes),
                    AntennaType = RAND.NextEnum<AntennaType>(),

                    CenterFrequency_Hz = RAND.Next(85, 105) * 100000 + RAND.NextDouble(),
                    Bandwidth_Hz = RAND.Next(10, 20) * 1000 + RAND.NextDouble(),
                    Gain_dB = RAND.Next(140) + RAND.NextDouble(),
                    SignalToNoiseRatio_dB = RAND.Next(140) + RAND.NextDouble(),

                    Roll = RAND.Next(-90, 90),
                    Pitch = RAND.Next(-90, 90),
                    Yaw = RAND.Next(-180, 180),

                    XPos = RAND.Next(-74, 74),
                    YPos = RAND.Next(-74, 74),
                    ZPos = RAND.Next(-74, 74),

                    TechnicalParameters = RAND.NextObject(TECHNICAL_PARAMETERS),
                    Remark = RAND.NextObject(Tool.ALLPANGRAMS)
                });
            }

            if (bEnsureRefDevice == true)
            {
                RFDevice refdev = list.FirstOrDefault(d => d.Id == 0) ?? list.First();

                refdev.Id = 0;
                refdev.Latitude = pllCenter.Lat;
                refdev.Longitude = pllCenter.Lng;
            }

            return list;
        }

    } // end public sealed class RFDeviceList
}
