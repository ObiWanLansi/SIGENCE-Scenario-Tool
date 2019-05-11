using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NoDisplayAttribute : Attribute
    {
    } // end public sealed class NoDisplayAttribute



    /// <summary>
    /// 
    /// </summary>
    public enum TextAlign : byte
    {
        /// <summary>
        /// The left
        /// </summary>
        Left,

        /// <summary>
        /// The center
        /// </summary>
        Center,

        /// <summary>
        /// The right
        /// </summary>
        Right

    } // end public enum TextAlign



    /// <summary>
    /// Klasse mit statischen Standalonefunktionen.
    /// </summary>
    public static class Tool
    {
        /// <summary>
        /// Franz jagt im komplett verwahrlosten Taxi quer durch Bayern.
        /// </summary>
        public static readonly string FRANZ = "Franz jagt im komplett verwahrlosten Taxi quer durch Bayern.";

        /// <summary>
        /// The quick brown fox jumps over a lazy dog.
        /// </summary>
        public static readonly string FOX = "The quick brown fox jumps over a lazy dog.";

        /// <summary>
        /// Vom Ödipuskomplex maßlos gequält, übt Wilfried zyklisches Jodeln.
        /// </summary>
        public static readonly string WILFRIED = "Vom Ödipuskomplex maßlos gequält, übt Wilfried zyklisches Jodeln.";

        /// <summary>
        /// Falsches Üben von Xylophonmusik quält jeden größeren Zwerg.
        /// </summary>
        public static readonly string XYLOPHONMUSIK = "Falsches Üben von Xylophonmusik quält jeden größeren Zwerg.";

        /// <summary>
        /// The allchars
        /// </summary>
        public static readonly string ALLCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZÖÄÜabcdefghijklmnopqrstuvwxyzöäü0123456789/*-+!\"§$%&()=?,.;:@€µ#'´`<>|^°\\";

        /// <summary>
        /// The allpangrams
        /// </summary>
        public static readonly List<string> ALLPANGRAMS = new List<string> { FRANZ, FOX, WILFRIED, XYLOPHONMUSIK };

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public static string ProductName { get; private set; }

        /// <summary>
        /// Gets the product title.
        /// </summary>
        /// <value>
        /// The product title.
        /// </value>
        public static string ProductTitle { get; private set; }

        /// <summary>
        /// Gets the startup path.
        /// </summary>
        /// <value>
        /// The startup path.
        /// </value>
        public static string StartupPath { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public static string Version { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The hs ignore types
        /// </summary>
        public static readonly HashSet<string> IGNORETYPES = new HashSet<string> { "List`1", "HashSet`1", "SortedDictionary`2", "IntPtr", "StreamWriter", "StreamReader" };


        /// <summary>
        /// The typetextalign
        /// </summary>
        public static readonly Dictionary<Type, TextAlign> TYPETEXTALIGN = new Dictionary<Type, TextAlign>
        {
            // 20120504: Damit ich an diversen anderen Stellen nicht nochmal extra auf String überprüfen muss ...
            { typeof( string ) , TextAlign.Left },

            { typeof( byte ) , TextAlign.Right },
            { typeof( byte? ) , TextAlign.Right },
            { typeof( short ) , TextAlign.Right },
            { typeof( short? ) , TextAlign.Right },
            { typeof( int ) , TextAlign.Right },
            { typeof( int? ) , TextAlign.Right },
            { typeof( long ) , TextAlign.Right },
            { typeof( long? ) , TextAlign.Right },

            { typeof( ushort ) , TextAlign.Right },
            { typeof( ushort? ) , TextAlign.Right },
            { typeof( uint ) , TextAlign.Right },
            { typeof( uint? ) , TextAlign.Right },
            { typeof( ulong ) , TextAlign.Right },
            { typeof( ulong? ) , TextAlign.Right },

            { typeof( float ) , TextAlign.Right },
            { typeof( float? ) , TextAlign.Right },
            { typeof( double ) , TextAlign.Right },
            { typeof( double? ) , TextAlign.Right },

            { typeof( bool ) , TextAlign.Center },
            { typeof( bool? ) , TextAlign.Center },

            { typeof( DateTime ) , TextAlign.Center },
            { typeof( DateTime? ) , TextAlign.Center },
            { typeof( TimeSpan) , TextAlign.Center },
            { typeof( TimeSpan? ) , TextAlign.Center }
        };


        /// <summary>
        /// The numbertypes
        /// </summary>
        public static readonly List<Type> NUMBERTYPES = new List<Type>
        {
            typeof( byte ) , typeof( byte? ) ,
            typeof( short ) , typeof( short? ) ,
            typeof( int ) , typeof( int? ) ,
            typeof( long ) , typeof( long? ) ,
            typeof( ushort ) , typeof( ushort? ) ,
            typeof( uint ) , typeof( uint? ) ,
            typeof( ulong ) , typeof( ulong? ) ,
            typeof( float ) , typeof( float? ) ,
            typeof( double ) , typeof( double? )
        };


        /// <summary>
        /// The floatingtypes
        /// </summary>
        public static readonly List<Type> FLOATINGTYPES = new List<Type>
        {
            typeof( float ) , typeof( float? ) ,
            typeof( double ) , typeof( double? )
        };


        /// <summary>
        /// The valuetypes
        /// </summary>
        public static readonly List<Type> VALUETYPES = new List<Type>(32);


        /// <summary>
        /// The datetimeformats
        /// </summary>
        public static readonly string[] DATETIMEFORMATS =
        {
            "dd.MM.yyyy, HH:mm:ss" ,
            "dd.MM.yyyy HH:mm:ss" ,
            "dd.MM.yyyy, HH:mm" ,
            "dd.MM.yyyy HH:mm" ,
            "dd.MM.yyyy" ,
            "ddMMyyyy_HHmmss" ,
            "yyyyMMdd_HHmmss" ,
            "yyyy-MM-dd" ,
            "dd-MM-yyyy"
        };

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the name of the module file.
        /// </summary>
        /// <param name="hModule">The h module.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetModuleFileName(HandleRef hModule, StringBuilder buffer, int length);

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes the <see cref="Tools" /> class.
        /// </summary>
        static Tool()
        {
            StringBuilder buffer = new StringBuilder(256);
            GetModuleFileName(new HandleRef(null, IntPtr.Zero), buffer, buffer.Capacity);
            StartupPath = Path.GetDirectoryName(buffer.ToString());

            //-----------------------------------------------------------------

            try
            {
                Assembly aEntryAssembly = Assembly.GetEntryAssembly();

                if (aEntryAssembly != null)
                {
                    {
                        object[] oAttributes = aEntryAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                        if (oAttributes.Length == 1)
                        {
                            ProductName = (oAttributes[0] as AssemblyProductAttribute).Product;
                        }
                    }
                    {
                        object[] oAttributes = aEntryAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                        if (oAttributes.Length == 1)
                        {
                            ProductTitle = (oAttributes[0] as AssemblyTitleAttribute).Title;
                        }
                    }
                    {
                        object[] oAttributes = aEntryAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);

                        if (oAttributes.Length == 1)
                        {
                            Version = (oAttributes[0] as AssemblyFileVersionAttribute).Version;
                        }
                    }
                }
                else
                {
                    ProductName = "Unknown";
                    ProductTitle = "Unknown";
                    Version = "0.0";
                }
            }
            catch (Exception ex)
            {
                ProductName = ex.Message;
            }

            //-----------------------------------------------------------------

            VALUETYPES.AddRange(NUMBERTYPES);
            VALUETYPES.AddRange(FLOATINGTYPES);
            VALUETYPES.Add(typeof(string));
            VALUETYPES.Add(typeof(bool));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the date time from unix timestamp.
        /// </summary>
        /// <param name="lUnixTimeStamp">The l unix time stamp.</param>
        /// <param name="dtk">The DTK.</param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromUnixTimestamp(long lUnixTimeStamp, DateTimeKind dtk)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, dtk).AddMilliseconds(lUnixTimeStamp);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the human distance.
        /// </summary>
        /// <param name="iMeter">The i meter.</param>
        /// <returns></returns>
        public static string GetHumanDistance(int iMeter)
        {
            return (iMeter < 1000) ? $"{iMeter} m" : $"{((float)iMeter) / 1000} km";
        }


        /// <summary>
        /// Gets the human size for physics.
        /// </summary>
        /// <param name="dValue">The d value.</param>
        /// <param name="strSuffix">The string suffix.</param>
        /// <returns></returns>
        public static string GetHumanSizeForPhysics(double dValue, string strSuffix)
        {
            if (dValue >= 1_000_000_000_000)
            {
                return $"{dValue / 1_000_000_000_000:F} T{strSuffix}";
            }

            if (dValue >= 1_000_000_000)
            {
                return $"{dValue / 1_000_000_000:F} G{strSuffix}";
            }

            if (dValue >= 1_000_000)
            {
                return $"{dValue / 1_000_000:F} M{strSuffix}";
            }

            if (dValue >= 1_000)
            {
                return $"{dValue / 1_000:F} K{strSuffix}";
            }

            return $"{dValue:F} {strSuffix}";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the human size for informatics.
        /// </summary>
        /// <param name="lSizeInBytes">The l size in bytes.</param>
        /// <returns></returns>
        public static string GetHumanSizeForInformatics(long lSizeInBytes)
        {
            if (lSizeInBytes >= 1099511627776)
            {
                return $"{(float)lSizeInBytes / 1099511627776:F} Tb";
            }

            if (lSizeInBytes >= 1073741824)
            {
                return $"{(float)lSizeInBytes / 1073741824:F} Gb";
            }

            if (lSizeInBytes >= 1048576)
            {
                return $"{(float)lSizeInBytes / 1048576:F} Mb";
            }

            if (lSizeInBytes >= 1024)
            {
                return $"{(float)lSizeInBytes / 1024:F} Kb";
            }

            return $"{lSizeInBytes} Bytes";
        }


        /// <summary>
        /// Gets the human distance.
        /// </summary>
        /// <param name="lLengthInMeter">The l length in meter.</param>
        /// <returns></returns>
        public static string GetHumanDistance(long lLengthInMeter)
        {
            if (lLengthInMeter < 1000)
            {
                return $"{lLengthInMeter} m";
            }

            return $"{(float)lLengthInMeter / 1000:F} km";
        }


        /// <summary>
        /// Reads the resource as string.
        /// </summary>
        /// <param name="strResourceName">Name of the string resource.</param>
        /// <returns></returns>
        public static string ReadResourceAsString(string strResourceName)
        {
            using (Stream s = Assembly.GetEntryAssembly().GetManifestResourceStream(strResourceName))
            {

                if (s == null)
                {
                    return null;
                }

                byte[] bBuffer = new byte[s.Length];

                if (s.Read(bBuffer, 0, bBuffer.Length) != bBuffer.Length)
                {
                    return null;
                }

                StringBuilder sb = new StringBuilder(bBuffer.Length);

                foreach (byte t in bBuffer)
                {
                    sb.Append((char)t);
                }

                return sb.ToString();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the grad.
        /// </summary>
        /// <param name="grad">The grad.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static double GetGrad(double grad, double minutes, double seconds)
        {
            return grad + (minutes / 60) + (seconds / 3600);
        }


        /// <summary>
        /// Gets the grad minutes seconds.
        /// </summary>
        /// <param name="grad">The grad.</param>
        /// <returns></returns>
        public static string GetGradMinutesSeconds(double grad)
        {
            double dGrad = Math.Floor(grad);
            double dMin = (grad - dGrad) * 60;
            double dMinGrad = Math.Floor(dMin);
            double dSec = (dMin - dMinGrad) * 60;

            return $"{dGrad}°{dMinGrad}'{dSec:F}''";
        }

    } // end static public class Tools



    /// <summary>
    /// Helper For A MessageBox.
    /// </summary>
    public static class MB
    {

        /// <summary>
        /// Nots the yet implemented.
        /// </summary>
        /// <param name="strCallerName">Name of the string caller.</param>
        public static void NotYetImplemented([CallerMemberName]string strCallerName = null)
        {
            MessageBox.Show($"{strCallerName} reported :\nNotYetImplemented ...", Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="strErrorText"></param>
        //static public void Error(string strErrorText)
        //{
        //    MessageBox.Show(strErrorText, Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        //}


        /// <summary>
        /// Errors the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="strCallerName">Name of the string caller.</param>
        public static void Error(Exception ex, [CallerMemberName]string strCallerName = null)
        {
            string strMessage = $"{strCallerName} reported:\n{(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}";
            MessageBox.Show(strMessage, Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }


        /// <summary>
        /// Warnings the specified string information text.
        /// </summary>
        /// <param name="strInformationText">The string information text.</param>
        public static void Warning(string strInformationText)
        {
            MessageBox.Show(strInformationText, Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        /// <summary>
        /// Warnings the specified string format.
        /// </summary>
        /// <param name="strFormat">The string format.</param>
        /// <param name="param">The parameter.</param>
        public static void Warning(string strFormat, params object[] param)
        {
            MessageBox.Show(string.Format(strFormat, param), Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        /// <summary>
        /// Informations the specified string information text.
        /// </summary>
        /// <param name="strInformationText">The string information text.</param>
        public static void Information(string strInformationText)
        {
            MessageBox.Show(strInformationText, Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Informations the specified string format.
        /// </summary>
        /// <param name="strFormat">The string format.</param>
        /// <param name="param">The parameter.</param>
        public static void Information(string strFormat, params object[] param)
        {
            MessageBox.Show(string.Format(strFormat, param), Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Heres the i am.
        /// </summary>
        /// <param name="strCallerName">Name of the string caller.</param>
        [Conditional("DEBUG")]
        public static void HereIAm([CallerMemberName]string strCallerName = null)
        {
            MessageBox.Show($"Here I'am:\n{strCallerName}", Tool.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

    } // end static public class MB
}
