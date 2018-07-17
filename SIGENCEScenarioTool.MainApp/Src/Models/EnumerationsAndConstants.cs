
namespace SIGENCEScenarioTool.Models
{

    /// <summary>
    /// 
    /// </summary>
    public enum RxTxType : int
    {
        // For all receivers (i.e. ID’s < 0) this parameter defines the radio being used:
        HackRF = -1,
        TwinRx = -2,
        B200Mini = -3,
        IdealSDR = -4,

        // For transmitters (i.e. ID’s >= 0) this parameter defines transmitter signal type:
        QPSK = 1,
        SIN = 2,
        FMRadio = 3,

        // Should not happen, but you never know ...
        Unknown = 4242

    } // end public enum RxTxType


    /// <summary>
    /// 
    /// </summary>
    public enum AntennaType : byte
    {
        OmniDirectional = 0,

        OmniLOG30800 = 1,
        HyperLOG60200 = 2,
        SimradArgusRadar = 3,


        Unknown = 255

    } // end public enum AntennaType


    /// <summary>
    /// 
    /// </summary>
    public enum DeviceType : byte
    {
        Unknown,

        Receiver,

        Transmitter,

        Reference

    } // end public enum DeviceType

}
