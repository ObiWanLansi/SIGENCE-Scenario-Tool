using System.Text;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringBuilderExtension
    {

        /// <summary>
        /// Appends the line.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="strFormat">The string format.</param>
        /// <param name="param">The parameter.</param>
        public static void AppendLine(this StringBuilder sb, string strFormat, params object[] param)
        {
            sb.AppendFormat(strFormat, param);
            sb.AppendLine();
        }

    } // end static public class StringBuilderExtension
}
