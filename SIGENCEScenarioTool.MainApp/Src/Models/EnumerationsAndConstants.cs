
namespace SIGENCEScenarioTool.Models
{

    /// <summary>
    /// 
    /// </summary>
    public enum RxTxType : byte
    {
        IdealSDR = 0,
        HackRF = 1,
        TwinRx = 2,

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
    static public class EnumerationsAndConstants
    {
    } // end static public class EnumerationsAndConstants
}
