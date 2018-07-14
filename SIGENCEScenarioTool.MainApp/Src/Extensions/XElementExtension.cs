using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;



namespace SIGENCEScenarioTool.Extensions
{
    static public class XElementExtension
    {

        /// <summary>
        /// 
        /// </summary>
        static private readonly string [] formats = new [] {
            "dd.MM.yyyy, HH:mm:ss" , "dd.MM.yyyy HH:mm:ss" ,
            "ddMMyyyy_HHmmss" , "yyyyMMdd_HHmmss" ,
            "yyyy-MM-dd" , "dd.MM.yyyy" , "dd-MM-yyyy" ,
            "yyyy-MM-dd HH:mm:ss"
        };

        /// <summary>
        /// Damit double Zahlen die mit . wegeschrieben sind geparst werden können.
        /// </summary>
        static private readonly CultureInfo ci = new CultureInfo( "en-US" );

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region SubNode's Abfragen


        ///// <summary>
        ///// Gets the date time from node.
        ///// </summary>
        ///// <param name="xCurrentElement">The be current element.</param>
        ///// <param name="strElementName">Name of the STR element.</param>
        ///// <returns></returns>
        //static public DateTime? GetDateTimeFromNode(this XElement xCurrentElement, string strElementName, bool bIsUTC = false)
        //{
        //    XElement x = xCurrentElement.Element(strElementName);

        //    if (x != null && x.Value.IsNotEmpty())
        //    {
        //        string strDatetime = bIsUTC == false ? x.Value : x.Value.Replace('T', ' ');

        //        return DateTime.ParseExact(strDatetime, formats, CultureInfo.CurrentCulture, bIsUTC == false ? DateTimeStyles.None : DateTimeStyles.AdjustToUniversal);
        //    }

        //    return null;
        //}


        /// <summary>
        /// Gets the date time from node UTC.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public DateTime? GetDateTimeFromNodeUTC( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                //string strDatetime = bIsUTC == false ? x.Value : x.Value.Replace('T', ' ');

                DateTime dt = DateTime.ParseExact( x.Value , formats , CultureInfo.CurrentCulture , DateTimeStyles.AssumeUniversal );
                return dt;
            }

            return null;
        }


        /// <summary>
        /// Gets the long from node.
        /// </summary>
        /// <param name="xCurrentElement">The be current element.</param>
        /// <param name="strElementName">Name of the STR element.</param>
        /// <returns></returns>
        static public long? GetLongFromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return long.Parse( x.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the int32 from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public int? GetInt32FromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return int.Parse( x.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the u int32 from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public uint? GetUInt32FromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return uint.Parse( x.Value );
            }

            return null;
        }



        /// <summary>
        /// Gets the single from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public float? GetSingleFromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return float.Parse( x.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the single from node comma.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public float? GetSingleFromNodeComma( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return float.Parse( x.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the single from node point.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public float? GetSingleFromNodePoint( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return float.Parse( x.Value , ci );
            }

            return null;
        }


        /// <summary>
        /// Gets the double from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public double? GetDoubleFromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return double.Parse( x.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the double from node comma.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public double? GetDoubleFromNodeComma( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return double.Parse( x.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the double from node point.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public double? GetDoubleFromNodePoint( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return double.Parse( x.Value , ci );
            }

            return null;
        }


        /// <summary>
        /// Gets the string from node.
        /// </summary>
        /// <param name="xCurrentElement">The be current element.</param>
        /// <param name="strElementName">Name of the STR element.</param>
        /// <returns></returns>
        static public string GetStringFromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            // Es wird NULL zurückgeliefert wenn das Element nicht da ist oder es ist da aber der Content ist leer ...
            return x != null ? string.IsNullOrEmpty( x.Value ) ? null : x.Value : null;
        }


        /// <summary>
        /// Gets the string from node.
        /// </summary>
        /// <param name="xCurrentElement">The be current element.</param>
        /// <param name="strNamespace">The STR namespace.</param>
        /// <param name="strElementName">Name of the STR element.</param>
        /// <returns></returns>
        static public string GetStringFromNode( this XElement xCurrentElement , string strNamespace , string strElementName )
        {
            string strName = string.Format( "{{{0}}}{1}" , strNamespace , strElementName );

            XElement x = xCurrentElement.Element( strName );

            return x != null ? x.Value : null;
        }


        /// <summary>
        /// Liefert den ersten CData Eintrag des Knotens wenn einer vorhanden ist.
        /// </summary>
        /// <param name="xCurrentElement"></param>
        /// <param name="strElementName"></param>
        /// <returns></returns>
        static public string GetStringFromCData( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            return x != null ? x.Value : null;
        }


        /// <summary>
        /// Gets the bool from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public bool? GetBoolFromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return bool.Parse( x.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the unique identifier from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public Guid? GetGuidFromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return Guid.Parse( x.Value );
            }

            return null;
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="xCurrentElement"></param>
        ///// <param name="strElementName"></param>
        ///// <returns></returns>
        //static public MapPoint GetMapPointFromNode(this XElement xCurrentElement, string strElementName)
        //{
        //    XElement x = xCurrentElement.Element(strElementName);

        //    if (x != null && x.Value.IsNotEmpty())
        //    {
        //        return MapPoint.FromJson(x.Value) as MapPoint;
        //    }

        //    return null;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="xCurrentElement"></param>
        ///// <param name="strElementName"></param>
        ///// <returns></returns>
        //static public Viewpoint GetViewpointFromNode(this XElement xCurrentElement, string strElementName)
        //{
        //    XElement x = xCurrentElement.Element(strElementName);

        //    if (x != null && x.Value.IsNotEmpty())
        //    {
        //        // Wir habe zum serialisieren ein ViewPoint hinzugefügt, deswegen müsssen wir hier auch eins Tiefer gehen ...
        //        x = x.Element("ViewPoint");

        //        double dRotation = x.GetDoubleFromNodePoint("Rotation") ?? 0;
        //        double dScale = x.GetDoubleFromNodePoint("TargetScale") ?? 0;
        //        Esri.ArcGISRuntime.Geometry.Geometry gTargetGeometry = Esri.ArcGISRuntime.Geometry.Geometry.FromJson(x.GetStringFromNode("TargetGeometry"));

        //        Camera camera = null;
        //        XElement xCamera = x.Element("Camera");

        //        if (xCamera != null && string.IsNullOrEmpty(xCamera.Value) == false)
        //        {
        //            double dHeading = xCamera.GetDoubleFromNodePoint("Heading") ?? 0;
        //            double dPitch = xCamera.GetDoubleFromNodePoint("Pitch") ?? 0;
        //            MapPoint mpLocation = (MapPoint)Esri.ArcGISRuntime.Geometry.Geometry.FromJson(xCamera.GetStringFromNode("Location"));

        //            camera = new Camera(mpLocation, dHeading, dPitch);
        //        }

        //        return camera != null ? new Viewpoint(camera, gTargetGeometry, dRotation) : new Viewpoint(gTargetGeometry);
        //    }

        //    return null;
        //}


        /// <summary>
        /// Gets the bitmap source from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <returns></returns>
        static public BitmapSource GetBitmapSourceFromNode( this XElement xCurrentElement , string strElementName )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                PngBitmapDecoder decoder = new PngBitmapDecoder( new MemoryStream( Convert.FromBase64String( x.Value ) ) , BitmapCreateOptions.None , BitmapCacheOption.Default );

                if( decoder != null && decoder.Frames.Count > 0 )
                {
                    return decoder.Frames [0];
                }
            }

            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the enum from node.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <param name="tDefault">The t default.</param>
        /// <returns></returns>
        static public T GetEnumFromNode<T>( this XElement xCurrentElement , string strElementName , T tDefault )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() == true )
            {
                return ( T ) Enum.Parse( typeof( T ) , x.Value );
            }

            return tDefault;
        }


        /// <summary>
        /// Gets the color from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <param name="cDefault">The c default.</param>
        /// <returns></returns>
        static public Color GetColorFromNode( this XElement xCurrentElement , string strElementName , Color cDefault )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return x.Value.ToColor( cDefault );
            }

            return cDefault;
        }


        /// <summary>
        /// Gets the file information from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <param name="fiDefault">The fi default.</param>
        /// <returns></returns>
        static public FileInfo GetFileInfoFromNode( this XElement xCurrentElement , string strElementName , FileInfo fiDefault )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return new FileInfo( x.Value );
            }

            return fiDefault;
        }


        /// <summary>
        /// Gets the directory information from node.
        /// </summary>
        /// <param name="xCurrentElement">The x current element.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <param name="diDefault">The di default.</param>
        /// <returns></returns>
        static public DirectoryInfo GetDirectoryInfoFromNode( this XElement xCurrentElement , string strElementName , DirectoryInfo diDefault )
        {
            XElement x = xCurrentElement.Element( strElementName );

            if( x != null && x.Value.IsNotEmpty() )
            {
                return new DirectoryInfo( x.Value );
            }

            return diDefault;
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region Attribut Abfragen

        /// <summary>
        /// Gets the date time attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <param name="bIsUTC">if set to <c>true</c> [b is UTC].</param>
        /// <returns></returns>
        static public DateTime? GetDateTimeAttribute( this XElement eParent , string strName , bool bIsUTC = false )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                string strDatetime = bIsUTC == false ? a.Value : a.Value.Replace( 'T' , ' ' );

                return DateTime.ParseExact( strDatetime , formats , CultureInfo.CurrentCulture , bIsUTC == false ? DateTimeStyles.None : DateTimeStyles.AssumeUniversal );
            }

            return null;
        }


        /// <summary>
        /// Gets the string attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public string GetStringAttribute( this XElement eParent , string strName )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                return a.Value.Trim();
            }

            return null;
        }


        /// <summary>
        /// Gets the bool attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public bool? GetBoolAttribute( this XElement eParent , string strName )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                return bool.Parse( a.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the u int32 attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public uint? GetUInt32Attribute( this XElement eParent , string strName )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                return uint.Parse( a.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the int32 attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public int? GetInt32Attribute( this XElement eParent , string strName )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                return int.Parse( a.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the int64 attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public long? GetInt64Attribute( this XElement eParent , string strName )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                return long.Parse( a.Value );
            }

            return null;
        }


        /// <summary>
        /// Gets the double attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public double? GetDoubleAttribute( this XElement eParent , string strName )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                if( a.Value.Equals( "NaN" ) )
                {
                    return double.NaN;
                }

                return double.Parse( a.Value.Replace( '.' , ',' ) );
            }

            return null;
        }


        /// <summary>
        /// Gets the single attribute.
        /// </summary>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        static public float? GetSingleAttribute( this XElement eParent , string strName )
        {
            XAttribute a = eParent.Attribute( strName );

            if( a != null )
            {
                if( a.Value.Equals( "NaN" ) )
                {
                    return float.NaN;
                }

                return float.Parse( a.Value.Replace( '.' , ',' ) );
            }

            return null;
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the x element.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        static public XElement GetXElement( string strPropertyName , object o )
        {
            //if( o is MapPoint )
            //{
            //    return new XElement(strPropertyName, (o as MapPoint).ToJson());
            //}

            //if( o is Viewpoint )
            //{
            //    Viewpoint vp = o as Viewpoint;

            //    //-------------------------------------------------------------

            //    XElement eViewPoint = new XElement("ViewPoint",
            //        new XElement("Rotation", vp.Rotation),
            //        new XElement("TargetScale", vp.TargetScale),
            //        new XElement("TargetGeometry", vp.TargetGeometry.ToJson()),
            //            vp.Camera != null ?
            //            new XElement("Camera",
            //                new XElement("Heading", vp.Camera.Heading),
            //                new XElement("Pitch", vp.Camera.Pitch),
            //                new XElement("Location", vp.Camera.Location.ToJson())
            //        ) : new XElement("Camera")
            //    );

            //    return new XElement(strPropertyName, eViewPoint);
            //}

            if( o is BitmapSource )
            {
                BitmapSource bs = o as BitmapSource;

                using( MemoryStream ms = new MemoryStream( 8192 ) )
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add( BitmapFrame.Create( bs ) );

                    encoder.Save( ms );

                    return new XElement( strPropertyName , new XCData( Convert.ToBase64String( ms.GetBuffer() ) ) );
                }
            }

            // Bei DateTime immer UTC rausschreiben ...
            if( o is DateTime )
            {
                DateTime dt = ( DateTime ) o;
                dt = dt.ToUniversalTime();
                return new XElement( strPropertyName , dt.Fmt_DD_MM_YYYY_HH_MM_SS() );
            }

            return new XElement( strPropertyName , o );
        }


        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eParent">The e parent.</param>
        /// <param name="strElementName">Name of the string element.</param>
        /// <param name="tDefault">The t default.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        static public T GetProperty<T>( this XElement eParent , string strElementName , T tDefault )
        {
            Type t = typeof( T );

            if( t.IsEnum )
            {
                return GetEnumFromNode<T>( eParent , strElementName , tDefault );
            }

            //-----------------------------------------------------------------

            // Standard DataTypes

            if( t == typeof( string ) )
            {
                string xDefault = ( string ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetStringFromNode( eParent , strElementName ) ?? xDefault );
            }

            if( t == typeof( bool ) )
            {
                bool xDefault = ( bool ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetBoolFromNode( eParent , strElementName ) ?? xDefault );
            }

            if( t == typeof( double ) )
            {
                double xDefault = ( double ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetDoubleFromNodePoint( eParent , strElementName ) ?? xDefault );
            }

            if( t == typeof( uint ) )
            {
                uint xDefault = ( uint ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetUInt32FromNode( eParent , strElementName ) ?? xDefault );
            }

            if( t == typeof( int ) )
            {
                int xDefault = ( int ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetInt32FromNode( eParent , strElementName ) ?? xDefault );
            }

            if( t == typeof( byte ) )
            {
                byte xDefault = ( byte ) ( ( object ) tDefault );
                return ( T ) ( ( object ) ( ( byte ) ( GetUInt32FromNode( eParent , strElementName ) ?? xDefault ) ) );
            }

            if( t == typeof( ulong ) )
            {
                ulong xDefault = ( ulong ) ( ( object ) tDefault );
                return ( T ) ( ( object ) ( ( ulong ) ( GetUInt32FromNode( eParent , strElementName ) ?? xDefault ) ) );
            }

            //-----------------------------------------------------------------

            // Standard Classes && Structs ...

            if( t == typeof( Guid ) )
            {
                Guid xDefault = ( Guid ) ( ( object ) tDefault );
                return ( T ) ( ( object ) ( ( Guid ) ( GetGuidFromNode( eParent , strElementName ) ?? xDefault ) ) );
            }

            if( t == typeof( DateTime ) )
            {
                DateTime xDefault = ( DateTime ) ( ( object ) tDefault );
                return ( T ) ( ( object ) ( ( DateTime ) ( GetDateTimeFromNodeUTC( eParent , strElementName ) ?? xDefault ) ) );
                //return tDefault; ;
            }

            if( t == typeof( Color ) )
            {
                Color xDefault = ( Color ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetColorFromNode( eParent , strElementName , xDefault ) );
            }

            if( t == typeof( FileInfo ) )
            {
                FileInfo xDefault = ( FileInfo ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetFileInfoFromNode( eParent , strElementName , xDefault ) );
            }

            if( t == typeof( DirectoryInfo ) )
            {
                DirectoryInfo xDefault = ( DirectoryInfo ) ( ( object ) tDefault );
                return ( T ) ( ( object ) GetDirectoryInfoFromNode( eParent , strElementName , xDefault ) );
            }

            //-----------------------------------------------------------------

            //// ESRI DataTypes ...
            //if (t == typeof(MapPoint))
            //{
            //    MapPoint xDefault = (MapPoint)((object)tDefault);
            //    return (T)((object)((MapPoint)(GetMapPointFromNode(eParent, strElementName) ?? xDefault)));
            //}

            //if (t == typeof(Viewpoint))
            //{
            //    Viewpoint xDefault = (Viewpoint)((object)tDefault);
            //    return (T)((object)((Viewpoint)(GetViewpointFromNode(eParent, strElementName) ?? xDefault)));
            //}

            if( t == typeof( BitmapSource ) )
            {
                BitmapSource xDefault = ( BitmapSource ) ( ( object ) tDefault );
                return ( T ) ( ( object ) ( ( BitmapSource ) ( GetBitmapSourceFromNode( eParent , strElementName ) ?? xDefault ) ) );
            }

            //-----------------------------------------------------------------

            //TODO: Check if the obkect have a FromXml Function, maybe es ist hilfreich to create a interface IXElementConverter

            throw new NotSupportedException();
            //return tDefault;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The XML writer settings
        /// </summary>
        static private readonly XmlWriterSettings XML_WRITER_SETTINGS = new XmlWriterSettings
        {
            Indent = true ,
            IndentChars = "    " ,
            Encoding = Encoding.Unicode
        };


        /// <summary>
        /// The XML declaration
        /// </summary>
        static private readonly XDeclaration XML_DECLARATION = new XDeclaration( "1.0" , "ISO-8859-1" , "yes" );


        /// <summary>
        /// Speichert einen XML Baum mit den Standardoptionen.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="strOutputFilename"></param>
        static public void SaveDefault( this XElement element , string strOutputFilename )
        {
            using( BufferedStream bs = new BufferedStream( new FileStream( strOutputFilename , FileMode.Create , FileAccess.Write ) ) )
            {
                using( XmlWriter xtw = XmlWriter.Create( bs , XML_WRITER_SETTINGS ) )
                {
                    new XDocument( XML_DECLARATION , element ).WriteTo( xtw );
                }
            }
        }


        /// <summary>
        /// To the default string.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        static public string ToDefaultString( this XElement element )
        {
            StringBuilder sb = new StringBuilder( 8192 );

            using( XmlWriter xtw = XmlWriter.Create( sb , XML_WRITER_SETTINGS ) )
            {
                new XDocument( XML_DECLARATION , element ).WriteTo( xtw );
            }

            return sb.ToString();
        }

    } // end static public class XElementExtension
}
