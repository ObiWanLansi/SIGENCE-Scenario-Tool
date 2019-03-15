using System;
using System.Drawing;
using System.Runtime.InteropServices;



namespace SIGENCEScenarioTool.Tools
{

    /// <summary>
    /// 
    /// </summary>
    public static class ExtractIcon
    {
        //-------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Extracts the icon ex.
        /// </summary>
        /// <param name="lpszFile">The LPSZ file.</param>
        /// <param name="nIconIndex">Index of the n icon.</param>
        /// <param name="phIconLarge">The ph icon large.</param>
        /// <param name="phIconSmall">The ph icon small.</param>
        /// <param name="nIcons">The n icons.</param>
        /// <returns></returns>
        [DllImport( "Shell32", CharSet = CharSet.Auto )]
        public static extern int ExtractIconEx( [MarshalAs( UnmanagedType.LPTStr )]string lpszFile, int nIconIndex, IntPtr[] phIconLarge, IntPtr[] phIconSmall, int nIcons );

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="hIcon"></param>
        ///// <returns></returns>
        //[DllImport( "user32" )]
        //private static extern int DestroyIcon( IntPtr hIcon );

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="strFilenameAndIconIndex">Index of the string filename and icon.</param>
        /// <returns></returns>
        public static Icon GetIcon( string strFilenameAndIconIndex ) => GetIcon( strFilenameAndIconIndex, false );


        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        /// <param name="iIconIndex">Index of the i icon.</param>
        /// <returns></returns>
        public static Icon GetIcon( string strFilename, int iIconIndex ) => GetIcon( strFilename, iIconIndex, false );


        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="strFilenameAndIconIndex">Index of the string filename and icon.</param>
        /// <param name="bLargeIcon">if set to <c>true</c> [b large icon].</param>
        /// <returns></returns>
        public static Icon GetIcon( string strFilenameAndIconIndex, bool bLargeIcon )
        {
            int iPosition = strFilenameAndIconIndex.IndexOf( ',' );

            string strIconFilename;
            int iIconIndex = 0;

            if(iPosition > 0)
            {
                strIconFilename = strFilenameAndIconIndex.Substring( 0, iPosition );
                iIconIndex = int.Parse( strFilenameAndIconIndex.Substring( iPosition + 1 ) );
            }
            else
            {
                strIconFilename = strFilenameAndIconIndex;
            }

            return GetIcon( strIconFilename, iIconIndex, bLargeIcon );
        }


        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        /// <param name="iIconIndex">Index of the i icon.</param>
        /// <param name="bLargeIcon">if set to <c>true</c> [b large icon].</param>
        /// <returns></returns>
        public static Icon GetIcon( string strFilename, int iIconIndex, bool bLargeIcon )
        {
            IntPtr[] hBigIcon = { IntPtr.Zero };
            IntPtr[] hSmallIcon = { IntPtr.Zero };

            ExtractIconEx( strFilename, iIconIndex, hBigIcon, hSmallIcon, 1 );

            if(bLargeIcon == true && hBigIcon[0] != IntPtr.Zero)
            {
                return Icon.FromHandle( hBigIcon[0] );
            }

            if(bLargeIcon == false && hBigIcon[0] != IntPtr.Zero)
            {
                return Icon.FromHandle( hSmallIcon[0] );
            }

            return null;
        }

    } // end static public class ExtractIcon
}
