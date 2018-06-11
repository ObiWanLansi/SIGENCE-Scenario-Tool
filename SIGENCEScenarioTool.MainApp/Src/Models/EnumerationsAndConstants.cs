
namespace SIGENCEScenarioTool.Models
{

    /// <summary>
    /// 
    /// </summary>
    public enum RxTxType : byte
    {
        // For all receivers (i.e. ID’s < 0) this parameter defines the radio being used:
        IdealSDR = 0,
        HackRF = 1,
        TwinRx = 2,

        // For transmitters (i.e. ID’s >= 0) this parameter defines transmitter signal type:
        QPSK = 101,
        SIN = 102,
        FMRadio = 103,

        // Should not happen, but you never know ...
        Unknown = 255

    } // end public enum RxTxType


    /// <summary>
    /// 
    /// </summary>
    public enum AntennaType : byte
    {
        OmniDirectional = 0,

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
