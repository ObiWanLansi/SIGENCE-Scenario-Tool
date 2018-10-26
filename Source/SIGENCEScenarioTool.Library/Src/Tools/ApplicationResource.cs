using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using SIGENCEScenarioTool.Extensions;



namespace SIGENCEScenarioTool.Tools
{
    /// <summary>
    /// Eine Klasse zum auslesen von statischen Ressourcen direkt aus einem Assembly.
    /// </summary>
    static public class ApplicationResource
    {
        /// <summary>
        /// a
        /// </summary>
        static private Assembly a = Assembly.GetEntryAssembly();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the assembly.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        /// <exception cref="ArgumentNullException"></exception>
        static public Assembly Assembly
        {
            get { return a; }
            set
            {
                // Before 4.7 ...
                if(a == null)
                {
                    throw new ArgumentNullException();
                }

                a = value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the embedded resourcen names.
        /// </summary>
        /// <returns></returns>
        static public string[] GetEmbeddedResourcenNames() => a.GetManifestResourceNames();


        /// <summary>
        /// Determines whether [contains] [the specified string resource name].
        /// </summary>
        /// <param name="strResourceName">Name of the string resource.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified string resource name]; otherwise, <c>false</c>.
        /// </returns>
        static public bool Contains( string strResourceName ) => a.GetManifestResourceNames().Any( strName => strName.EqualsIgnoreCase( strResourceName ) == true );


        /// <summary>
        /// Reads the ressource as byte array.
        /// </summary>
        /// <param name="strResourceName">Name of the string resource.</param>
        /// <returns></returns>
        static public byte[] ReadResourceAsByteArray( string strResourceName )
        {
            using(Stream s = a.GetManifestResourceStream( strResourceName ))
            {
                if(s == null)
                {
                    return null;
                }

                byte[] bBuffer = new byte[s.Length];

                return s.Read( bBuffer, 0, bBuffer.Length ) != bBuffer.Length ? null : bBuffer;
            }
        }


        /// <summary>
        /// Reads the resource as x document.
        /// </summary>
        /// <param name="strResourceName">Name of the string resource.</param>
        /// <returns></returns>
        static public XDocument ReadResourceAsXDocument( string strResourceName )
        {
            using(Stream s = a.GetManifestResourceStream( strResourceName ))
            {
                return s == null ? null : XDocument.Load( new XmlTextReader( s ) );
            }
        }


        /// <summary>
        /// Reads the resource as string.
        /// </summary>
        /// <param name="strResourceName">Name of the string resource.</param>
        /// <returns></returns>
        static public string ReadResourceAsString( string strResourceName )
        {
            using(Stream s = a.GetManifestResourceStream( strResourceName ))
            {

                if(s == null)
                {
                    return null;
                }

                byte[] bBuffer = new byte[s.Length];

                if(s.Read( bBuffer, 0, bBuffer.Length ) != bBuffer.Length)
                {
                    return null;
                }

                StringBuilder sb = new StringBuilder( bBuffer.Length );

                foreach(byte t in bBuffer)
                {
                    sb.Append( (char)t );
                }

                return sb.ToString();
            }
        }

    } // end static public class ApplicationResource
}
