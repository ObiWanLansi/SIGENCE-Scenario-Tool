using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;



namespace TransmitterTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    static public class Windows
    {
        /// <summary>
        /// Opens the with default application.
        /// </summary>
        /// <param name="fiFile">The fi file.</param>
        /// <returns></returns>
        static public Process OpenWithDefaultApplication(FileInfo fiFile)
        {
            Process p = new Process
            {
                StartInfo = { FileName = fiFile.FullName, WorkingDirectory = fiFile.DirectoryName, Verb = "Open" }
            };

            p.Start();

            return p;
        }


        /// <summary>
        /// Opens the with default application.
        /// </summary>
        /// <param name="strFile">The STR file.</param>
        /// <returns></returns>
        static public Process OpenWithDefaultApplication(string strFile)
        {
            return OpenWithDefaultApplication(new FileInfo(strFile));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Opens the web adress.
        /// </summary>
        /// <param name="strURL">The STR URL.</param>
        /// <returns></returns>
        static public Process OpenWebAdress(string strURL)
        {
            Process p = new Process
            {
                StartInfo =
                {
                    FileName = strURL,
                    Verb = "Open"
                }
            };

            p.Start();

            return p;
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        static public BitmapSource GetWPFScreenshot(Control control, int? iWidth = null, int? iHeight = null)
        {
            if (control == null)
            {
                return null;
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)control.ActualWidth, (int)control.ActualHeight, 96, 96, PixelFormats.Default);

            rtb.Render(control);

            if (iWidth.HasValue && iHeight.HasValue)
            {
                return new TransformedBitmap(rtb, new ScaleTransform((double)iWidth / rtb.PixelWidth, (double)iHeight / rtb.PixelHeight));
            }

            return rtb;
        }

    } // end static public class Windows
}
