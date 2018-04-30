using System;



namespace TransmitterMan.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    static public class DateTimeExtension
    {
        /// <summary>
        /// yyyyMMdd
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        static public string Fmt_YYYYMMDD(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }


        /// <summary>
        /// yyyyMMdd_HHmmss
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        static public string Fmt_YYYYMMDD_HHMMSS(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd_HHmmss");
        }


        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        static public string Fmt_YYYYMMDDHHMMSS(this DateTime dt)
        {
            return dt.ToString("yyyyMMddHHmmss");
        }


        /// <summary>
        /// yyyyMMdd_HHmmssfff
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string Fmt_YYYYMMDD_HHMMSSFFF(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd_HHmmssfff");
        }


        /// <summary>
        /// dd.MM.yyyy, HH:mm:ss
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        static public string Fmt_DD_MM_YYYY_HH_MM_SS(this DateTime dt)
        {
            return dt.ToString("dd.MM.yyyy, HH:mm:ss");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string Fmt_DD_MM_YYYY_HH_MM(this DateTime dt)
        {
            return dt.ToString("dd.MM.yyyy, HH:mm");
        }


        /// <summary>
        /// dd.MM.yyyy
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        static public string Fmt_DD_MM_YYYY(this DateTime dt)
        {
            return dt.ToString("dd.MM.yyyy");
        }


        /// <summary>
        /// HH:mm:ss
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string Fmt_HH_MM_SS(this DateTime dt)
        {
            return dt.ToString("HH:mm:ss");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public int DaysInMonth(this DateTime dt)
        {
            return DateTime.DaysInMonth(dt.Year, dt.Month);
        }

    } // end static public class DateTimeExtension
}
