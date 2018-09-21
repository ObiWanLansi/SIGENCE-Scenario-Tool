using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public static class Windows
    {
        /// <summary>
        /// Opens the with default application.
        /// </summary>
        /// <param name="fiFile">The fi file.</param>
        /// <returns></returns>
        public static Process OpenWithDefaultApplication(FileInfo fiFile)
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
        public static Process OpenWithDefaultApplication(string strFile)
        {
            return OpenWithDefaultApplication(new FileInfo(strFile));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Opens the web adress.
        /// </summary>
        /// <param name="strURL">The STR URL.</param>
        /// <returns></returns>
        public static Process OpenWebAdress(string strURL)
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
        /// Gets the WPF screenshot.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="iWidth">Width of the i.</param>
        /// <param name="iHeight">Height of the i.</param>
        /// <returns></returns>
        public static BitmapSource GetWPFScreenshot(Control control, int? iWidth = null, int? iHeight = null)
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


        /// <summary>
        /// Saves the WPF screenshot.
        /// </summary>
        /// <param name="screenshot">The screenshot.</param>
        /// <param name="strOutputFilename">The string output filename.</param>
        public static void SaveWPFScreenshot(BitmapSource screenshot, string strOutputFilename)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(screenshot));

            using (BufferedStream bs = new BufferedStream(new FileStream(strOutputFilename, FileMode.Create)))
            {
                encoder.Save(bs);
            }
        }

    } // end static public class Windows
}
