using System.Windows.Media;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ColorExtension
    {
        /// <summary>
        /// Returns The Color With Changed Alpha Value.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="bAlpha"></param>
        /// <returns></returns>
        public static Color WithAlpha(this Color color, byte bAlpha)
        {
            return new Color { R = color.R, G = color.G, B = color.B, A = bAlpha };
        }

    } // end static public class ColorExtension
}
