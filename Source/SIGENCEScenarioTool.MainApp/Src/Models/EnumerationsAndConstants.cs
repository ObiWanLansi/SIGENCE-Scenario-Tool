
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
        /// <summary>
        /// Unknown DeviceType
        /// </summary>
        Unknown,

        /// <summary>
        /// Receiver
        /// </summary>
        Receiver,

        /// <summary>
        /// Transmitter
        /// </summary>
        Transmitter,

        /// <summary>
        /// Reference Transmitter
        /// </summary>
        Reference

    } // end public enum DeviceType



    /// <summary>
    /// 
    /// </summary>
    public enum DeviceSource : byte
    {
        /// <summary>
        /// The source of the device is unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// The device was created by the user
        /// </summary>
        User,

        /// <summary>
        /// The device was automatically generated
        /// </summary>
        Automatic,

        /// <summary>
        /// The device comes from a data import
        /// </summary>
        DataImport,

        /// <summary>
        /// The device comes from a simulation result
        /// </summary>
        SimulationResult

    } // end public enum DeviceSource 



    /// <summary>
    /// 
    /// </summary>
    public enum Servity : byte
    {
        Low,
        
        Normal,
        
        High

    } // end public enum Servity
}
