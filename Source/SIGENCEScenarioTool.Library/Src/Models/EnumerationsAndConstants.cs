


namespace SIGENCEScenarioTool.Models
{

    /// <summary>
    /// All Known Antenna Types.
    /// </summary>
    public enum AntennaType : byte
    {
        OmniDirectional = 0,
        Dipole = 1,
        HyperLog60100 = 2,
        OmniLog30800 = 3,
        IsoLog3DMobile = 4,
        SimradArgusRadar = 5,


        // The Old values Before 20191122
        //OmniLOG30800 = 1,
        //HyperLOG60200 = 2,
        //SimradArgusRadar = 3,

        Unknown = 255

    } // end public enum AntennaType



    /// <summary>
    /// The Type Of The Device.
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
    /// The Source Of The Device.
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
    /// An Servity For Different Use Cases.
    /// </summary>
    public enum Servity : byte
    {
        /// <summary>
        /// The information
        /// </summary>
        Information,

        /// <summary>
        /// The warning
        /// </summary>
        Warning,

        /// <summary>
        /// The error
        /// </summary>
        Error,

        /// <summary>
        /// The fatal
        /// </summary>
        Fatal

    } // end public enum Servity
}
