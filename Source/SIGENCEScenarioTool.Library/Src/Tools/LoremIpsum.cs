using System;
using System.Text;

using SIGENCEScenarioTool.Extensions;

namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// Eine kleine Klasse um einen Lorem Ipsum Blindtext zu generiern.
    /// </summary>
    public sealed class LoremIpsum
    {
        /// <summary>
        /// The li start
        /// </summary>
        private const string LI_START = "Lorem ipsum dolor sit amet,";

        /// <summary>
        /// The rand
        /// </summary>
        private static readonly Random rand = new Random();

        ///// <summary>
        ///// Die Anzahl der Wörter pro Zeile die generiert werden sollen, default 10.
        ///// </summary>
        //public uint WordsPerLine { get; set; }

        ///// <summary>
        ///// Die Anzahl der Zeilen die generiert werden soll, default 5.
        ///// </summary>
        //public uint Lines { get; set; }

        ///// <summary>
        ///// Der String der als Zeilenende verwendet werden soll, default "\r\n".
        ///// </summary>
        //public string LineEnding { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region LoremIpsumWords
        /// <summary>
        /// The lorem ipsum words
        /// </summary>
        private static readonly string[] LOREM_IPSUM_WORDS = {
            "lorem","ipsum","dolor","sit","amet",
            "consectetuer","adipiscing","elit","donec","id",
            "eros","quisque","nisl","lacinia","cursus",
            "tincidunt","porttitor","non","leo","nunc",
            "metus","nibh","semper","ac","interdum",
            "nec","fringilla","ut","felis","tempor",
            "ligula","suspendisse","quis","est","commodo",
            "dignissim","mauris","neque","convallis","urna",
            "sed","orci","turpis","pretium","consequat",
            "venenatis","et","risus","cras","facilisis",
            "augue","in","praesent","pellentesque","diam",
            "vestibulum","justo","curabitur","dui","vivamus",
            "vitae","at","enim","laoreet","phasellus",
            "eget","ornare","pulvinar","a","aenean",
            "condimentum","nullam","ante","fermentum","scelerisque",
            "velit","morbi","nam","rhoncus","dapibus",
            "libero","suscipit","hendrerit","tortor","integer",
            "congue","sem","eu","mi","tellus",
            "nonummy","lacus","duis","wisi","aliquam",
            "nulla","cum","sociis","natoque","penatibus",
            "magnis","dis","parturient","montes","nascetur",
            "ridiculus","mus","sodales","proin","odio",
            "auctor","sollicitudin","maecenas","magna","tristique",
            "malesuada","euismod","vel","porta","sapien",
            "tempus","mattis","massa","purus","arcu",
            "eleifend","mollis","aliquet","pharetra","habitant",
            "senectus","netus","fames","egestas","sagittis",
            "gravida","molestie","facilisi","volutpat","feugiat",
            "accumsan","pede","dictum","lobortis","hac",
            "habitasse","platea","dictumst","rutrum","iaculis",
            "faucibus","fusce","ultrices","ullamcorper","quam",
            "lansi","analus","corinnus","felix","nussi",
            "willus","gabrielus","ancus","nasus","wingtsun",
            "i","e","sinus","illuminati","sanktus","spiritus",
            "lancer","nitus","sammy","shari"
        };
        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the lorem ipsum.
        /// </summary>
        /// <returns></returns>
        static public string GetLoremIpsum(uint uiLines = 20, uint uiWordsPerLine = 20, string strLineEnding = "\r\n")
        {
            if (uiWordsPerLine < 10)
            {
                uiWordsPerLine = 10;
            }

            if (uiLines == 0)
            {
                uiLines = 5;
            }

            if (strLineEnding.IsEmpty() == true)
            {
                strLineEnding = "\r\n";
            }

            //-------------------------------------------------------------------------------------

            int iPointRand = rand.Next(10) + 3;
            bool bNewLine = false;
            uint iLineCounter = 0;
            uint iLineWordCounter = 5;
            uint iSentenceWordCounter = 0;
            bool bSentenceStart = false;

            StringBuilder sb = new StringBuilder(256);

            sb.Append(LI_START);

            while (iLineCounter++ < uiLines)
            {
                while (iLineWordCounter++ < uiWordsPerLine)
                {
                    string strWord = LOREM_IPSUM_WORDS[rand.Next(LOREM_IPSUM_WORDS.Length)];

                    if (bSentenceStart == true)
                    {
                        strWord = strWord.Capitalize();
                        bSentenceStart = false;
                    }

                    if (bNewLine == true)
                    {
                        sb.Append(strWord);
                        bNewLine = false;
                    }
                    else
                    {
                        sb.Append(' ');
                        sb.Append(strWord);
                    }

                    iSentenceWordCounter++;

                    if (iSentenceWordCounter > iPointRand)
                    {
                        sb.Append('.');

                        // Es sollten schon midenstens drei Wörter pro Satz sein, maximal also 12
                        iPointRand = rand.Next(10) + 3;

                        iSentenceWordCounter = iSentenceWordCounter ^ iSentenceWordCounter;
                        bSentenceStart = true;
                    }
                }

                iLineWordCounter = iLineWordCounter ^ iLineWordCounter;

                if (iLineCounter < uiLines)
                {
                    sb.Append(strLineEnding);
                    bNewLine = true;
                }
                else
                {
                    sb.Append('.');
                }
            }

            //-------------------------------------------------------------------------------------

            return sb.ToString();
        }

    } // end sealed public class LoremIpsum
}
