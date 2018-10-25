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
        public static string GetFileSize(this FileInfo fi)
        {
            if (fi.Length >= 1073741824)
            {
                return string.Format("{0:F}", ((float)fi.Length / 1073741824)) + " Gb";
            }

            if (fi.Length >= 1048576)
            {
                return string.Format("{0:F}", ((float)fi.Length / 1048576)) + " Mb";
            }

            if (fi.Length >= 1024)
            {
                return string.Format("{0:F}", ((float)fi.Length / 1024)) + " Kb";
            }

            return fi.Length + " Bytes";
        }


        /// <summary>
        /// Gets the filename without extension.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <returns></returns>
        public static string GetFilenameWithoutExtension(this FileInfo fi)
        {
            return fi.Extension.Length == 0 ? fi.Name : fi.Name.Remove(fi.Name.Length - fi.Extension.Length);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Moves to file to a other directory.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <param name="diDirectory">The di directory.</param>
        public static void MoveTo(this FileInfo fi, DirectoryInfo diDirectory)
        {
            fi.MoveTo(string.Format("{0}\\{1}", diDirectory.FullName, fi.Name));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Copies to file to a other directory.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <param name="di">The di.</param>
        /// <returns></returns>
        public static FileInfo CopyTo(this FileInfo fi, DirectoryInfo di)
        {
            return fi.CopyTo(string.Format("{0}\\{1}", di.FullName, fi.Name));
        }


        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="fi">The fi.</param>
        /// <param name="di">The di.</param>
        /// <param name="bOverwrite">if set to <c>true</c> [b overwrite].</param>
        /// <returns></returns>
        public static FileInfo CopyTo(this FileInfo fi, DirectoryInfo di, bool bOverwrite)
        {
            return fi.CopyTo(string.Format("{0}\\{1}", di.FullName, fi.Name), bOverwrite);
        }

    } // end static public class FileInfoExtension
}