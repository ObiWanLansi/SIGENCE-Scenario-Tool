using System;
using System.Threading;
using System.Windows.Media;

using ThingM.Blink1;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    static public partial class Blink
    {

        /// <summary>
        /// Ons the LED.
        /// </summary>
        static public void On() => SetColor(Colors.White);


        /// <summary>
        /// Offs the LED.
        /// </summary>
        static public void Off()
        {
            using (Blink1 blink = new Blink1())
            {
                blink.Open();

                blink.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Shows the specified number of time.
        /// </summary>
        /// <param name="numberOfTime">The number of time.</param>
        /// <param name="numberOfMillisecondOn">The number of millisecond on.</param>
        /// <param name="numberOfMillisecondOff">The number of millisecond off.</param>
        /// <param name="c">The c.</param>
        static public void Show(ushort numberOfTime, ushort numberOfMillisecondOn, ushort numberOfMillisecondOff, Color c)
        {
            using (Blink1 blink = new Blink1())
            {
                blink.Open();

                blink.Blink(numberOfTime, numberOfMillisecondOn, numberOfMillisecondOff, c.R, c.G, c.B);

                blink.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tests this instance.
        /// </summary>
        static public void Test()
        {
            using (Blink1 blink = new Blink1())
            {
                blink.Open();

                Action<Color> SetColor = c =>
                {
                    blink.SetColor(c.R, c.G, c.B);
                    Thread.Sleep(1000);
                };

                SetColor(Colors.Red);
                SetColor(Colors.Green);
                SetColor(Colors.Blue);

                blink.Close();
            }
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Fades the white to black.
        /// </summary>
        static public void FadeWhiteToBlack()
        {
            using (Blink1 blink = new Blink1())
            {
                blink.Open();

                Action<int, int, int> SetColor = (r, g, b) =>
                {
                    blink.SetColor((ushort)r, (ushort)g, (ushort)b);
                    Thread.Sleep(20);
                };

                for (int iCounter = 255; iCounter > 0; iCounter--)
                {
                    SetColor(iCounter, iCounter, iCounter);
                }

                blink.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="iR">The i r.</param>
        /// <param name="iG">The i g.</param>
        /// <param name="iB">The i b.</param>
        static public void SetColor(int iR, int iG, int iB)
        {
            using (Blink1 blink = new Blink1())
            {
                blink.Open();
                blink.SetColor((ushort)iR, (ushort)iG, (ushort)iB);
            }
        }


        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="c">The c.</param>
        static public void SetColor(Color c)
        {
            SetColor(c.R, c.G, c.B);
        }

    } // end static public class Blink
}
