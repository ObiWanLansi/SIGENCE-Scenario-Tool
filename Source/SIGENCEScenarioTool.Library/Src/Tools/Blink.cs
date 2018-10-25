using System.Threading;
using System.Windows.Media;

using ThingM.Blink1;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public static class Blink
    {

        /// <summary>
        /// Ons the LED.
        /// </summary>
        public static void On() => SetColor( Colors.White );


        /// <summary>
        /// Offs the LED.
        /// </summary>
        public static void Off()
        {
            using( Blink1 blink = new Blink1() )
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
        public static void Show( ushort numberOfTime , ushort numberOfMillisecondOn , ushort numberOfMillisecondOff , Color c )
        {
            using( Blink1 blink = new Blink1() )
            {
                blink.Open();

                blink.Blink( numberOfTime , numberOfMillisecondOn , numberOfMillisecondOff , c.R , c.G , c.B );

                blink.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tests this instance.
        /// </summary>
        public static void Test()
        {
            using( Blink1 blink = new Blink1() )
            {
                blink.Open();

                void SetColor( Color c )
                {
                    blink.SetColor( c.R , c.G , c.B );
                    Thread.Sleep( 1000 );
                }

                SetColor( Colors.Red );
                SetColor( Colors.Green );
                SetColor( Colors.Blue );

                blink.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Fades the white to black.
        /// </summary>
        public static void FadeWhiteToBlack()
        {
            using( Blink1 blink = new Blink1() )
            {
                blink.Open();

                void SetColor( int r , int g , int b )
                {
                    blink.SetColor( ( ushort ) r , ( ushort ) g , ( ushort ) b );
                    Thread.Sleep( 20 );
                }

                for( int iCounter = 255 ; iCounter > 0 ; iCounter-- )
                {
                    SetColor( iCounter , iCounter , iCounter );
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
        private static void SetColor( int iR , int iG , int iB )
        {
            using( Blink1 blink = new Blink1() )
            {
                blink.Open();
                blink.SetColor( ( ushort ) iR , ( ushort ) iG , ( ushort ) iB );
            }
        }


        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="c">The c.</param>
        public static void SetColor( Color c )
        {
            SetColor( c.R , c.G , c.B );
        }

    } // end static public class Blink
}
