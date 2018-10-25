using System;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TimeSpanExtension
    {
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public static string ToShortString(this TimeSpan ts) => $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}.{ts.Milliseconds:D3}";


        /// <summary>
        /// To the HHMMSS string.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns></returns>
        public static string ToHHMMSSString(this TimeSpan ts) => $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";

    } // end static public class TimeSpanExtension
}
