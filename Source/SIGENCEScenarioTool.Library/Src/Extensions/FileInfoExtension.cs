using System.Drawing;
using System.IO;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// Eine Erweiterungsklasse für System.IO.FileInfo .
    /// </summary>
    public static class FileInfoExtension
    {
        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <returns></returns>
        public static string GetFileSize( this FileInfo fi )
        {
            if(fi.Length >= 1073741824)
            {
                return $"{(float)fi.Length / 1073741824:F}" + " Gb";
            }

            if(fi.Length >= 1048576)
            {
                return $"{(float)fi.Length / 1048576:F}" + " Mb";
            }

            if(fi.Length >= 1024)
            {
                return $"{(float)fi.Length / 1024:F}" + " Kb";
            }

            return fi.Length + " Bytes";
        }


        /// <summary>
        /// Gets the filename without extension.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <returns></returns>
        public static string GetFilenameWithoutExtension( this FileInfo fi )
        {
            return fi.Extension.Length == 0 ? fi.Name : fi.Name.Remove( fi.Name.Length - fi.Extension.Length );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Moves to file to a other directory.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <param name="diDirectory">The di directory.</param>
        public static void MoveTo( this FileInfo fi, DirectoryInfo diDirectory )
        {
            fi.MoveTo( $"{diDirectory.FullName}\\{fi.Name}" );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Copies to file to a other directory.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <param name="di">The di.</param>
        /// <returns></returns>
        public static FileInfo CopyTo( this FileInfo fi, DirectoryInfo di )
        {
            return fi.CopyTo( $"{di.FullName}\\{fi.Name}" );
        }


        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <param name="di">The di.</param>
        /// <param name="bOverwrite">if set to <c>true</c> [b overwrite].</param>
        /// <returns></returns>
        public static FileInfo CopyTo( this FileInfo fi, DirectoryInfo di, bool bOverwrite )
        {
            return fi.CopyTo( $"{di.FullName}\\{fi.Name}", bOverwrite );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the associated icon.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <returns></returns>
        static public Icon GetAssociatedIcon( this FileInfo fi )
        {
            return Icon.ExtractAssociatedIcon( fi.FullName );
        }

    } // end static public class FileInfoExtension
}