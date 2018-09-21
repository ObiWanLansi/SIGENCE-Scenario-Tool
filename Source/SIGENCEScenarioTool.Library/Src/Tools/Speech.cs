using System;
using System.Speech.Synthesis;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// Klasse zum Ausgeben von Text in Sprache mittels Microsoft SAM.
    /// </summary>
    public sealed class Speech : IDisposable
    {
        /// <summary>
        /// The speech sythesizer
        /// </summary>
        private SpeechSynthesizer speech = new SpeechSynthesizer();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public SynthesizerState State
        {
            get { return speech.State; }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="Speech" /> class.
        /// </summary>
        public Speech()
        {
            // Wir geben immer 100%.
            speech.Volume = 100;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gibt den übergebenen Text aus.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        public void Speak(string strContent)
        {
            //// Falls er noch am quatschen ist warten wir.
            //while( speech.State == SynthesizerState.Speaking )
            //{
            //    Thread.Sleep( 10 );
            //}

            speech.Speak(strContent);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Says the specified string content.
        /// </summary>
        /// <param name="strContent">Content of the string.</param>
        public static void Say(string strContent)
        {
            using (Speech speech = new Speech())
            {
                speech.Speak(strContent);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        public void Dispose()
        {
            if (speech != null)
            {
                speech.Dispose();
                speech = null;
            }
        }

    } // end sealed public class Speech
}
