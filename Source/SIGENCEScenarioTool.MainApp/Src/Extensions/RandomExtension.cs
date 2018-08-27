using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// Eine Erweiterungsklasse für System.Random .
    /// </summary>
    static public class RandomExtension
    {
        /// <summary>
        /// Der Vollständigkeit wegen.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        static public int NextInt( this Random r )
        {
            return r.Next();
        }


        /// <summary>
        /// Der Vollständigkeit wegen.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        static public uint NextUInt( this Random r )
        {
            return ( uint ) r.Next();
        }


        /// <summary>
        /// Liefert einen Zufalls Boolschen Wert zurück.
        /// </summary>
        /// <param name="r">The current random object</param>
        /// <returns></returns>
        static public bool NextBool( this Random r )
        {
            return ( r.Next() % 2 ) != 0;
        }


        /// <summary>
        /// Nexts the enum.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="tEnum">The t enum.</param>
        /// <returns></returns>
        static public int NextEnum( this Random r , Type tEnum )
        {
            if( tEnum.IsEnum == false )
            {
                return 0;
            }

            Array aValues = Enum.GetValues( tEnum );

            if( tEnum.GetCustomAttributes( typeof( FlagsAttribute ) , false ).Length > 0 )
            {
                int iValue = 0;

                for( int iCounter = 0 ; iCounter < r.Next( aValues.Length ) ; iCounter++ )
                {
                    iValue |= ( int ) aValues.GetValue( r.Next( aValues.Length ) );
                }

                return ( int ) Enum.ToObject( tEnum , iValue );
            }

            return ( int ) aValues.GetValue( r.Next( aValues.Length ) );
        }


        /// <summary>
        /// Liefert einen zufälligen Enumeration Wert zu einer Enumeration zurück.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The current random object</param>
        /// <returns></returns>
        static public T NextEnum<T>( this Random r )
        {
            Type tEnum = typeof( T );

            if( tEnum.IsEnum == false )
            {
                return default( T );
            }

            Array aValues = Enum.GetValues( tEnum );

            if( tEnum.GetCustomAttributes( typeof( FlagsAttribute ) , false ).Length > 0 )
            {
                int iValue = 0;

                for( int iCounter = 0 ; iCounter < r.Next( aValues.Length ) ; iCounter++ )
                {
                    iValue |= ( int ) aValues.GetValue( r.Next( aValues.Length ) );
                }

                return ( T ) Enum.ToObject( tEnum , iValue );
            }

            return ( T ) aValues.GetValue( r.Next( aValues.Length ) );
        }


        /// <summary>
        /// Nexts the long.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        static public long NextLong( this Random r )
        {
            return ( ( ( long ) r.NextInt() ) << 32 ) + r.NextInt();
        }


        /// <summary>
        /// Nexts the u long.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        static public ulong NextULong( this Random r )
        {
            return ( ( ulong ) r.NextInt() << 32 ) + ( ulong ) r.NextInt();
        }


        /// <summary>
        /// Nexts the date time.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="dtMin">The dt minimum.</param>
        /// <param name="dtMax">The dt maximum.</param>
        /// <param name="dtk">The DTK.</param>
        /// <returns></returns>
        static public DateTime NextDateTime( this Random r , DateTime dtMin , DateTime dtMax , DateTimeKind dtk = DateTimeKind.Local )
        {
            return new DateTime( ( r.NextLong() % ( dtMax.Ticks - dtMin.Ticks ) ) + dtMin.Ticks , dtk );
        }


        /// <summary>
        /// Nexts the date time.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="dtk">The DTK.</param>
        /// <returns></returns>
        static public DateTime NextDateTime( this Random r , DateTimeKind dtk = DateTimeKind.Local )
        {
            return NextDateTime( r , DateTime.MinValue , DateTime.MaxValue , dtk );
        }


        /// <summary>
        /// Nexts the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The r.</param>
        /// <param name="lValues">The l values.</param>
        /// <returns></returns>
        static public T NextObject<T>( this Random r , IList<T> lValues )
        {
            return lValues [r.Next( lValues.Count )];
        }


        /// <summary>
        /// Nexts the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The r.</param>
        /// <param name="cValues">The c values.</param>
        /// <returns></returns>
        static public T NextObject<T>( this Random r , ICollection<T> cValues )
        {
            int iSelectedValue = r.Next( cValues.Count );
            int iCounter = 0;

            IEnumerator<T> e = cValues.GetEnumerator();

            while( e.MoveNext() )
            {
                if( iCounter++ == iSelectedValue )
                {
                    return e.Current;
                }
            }

            return default( T );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="iMinLength">Length of the i min.</param>
        /// <param name="iMaxLength">Length of the i max.</param>
        /// <returns></returns>
        static public string NextString( this Random r , int iMinLength , int iMaxLength )
        {
            int iLength = r.Next( iMinLength , iMaxLength + 1 );

            StringBuilder sb = new StringBuilder( iLength );

            for( int iCounter = 0 ; iCounter < iLength ; iCounter++ )
            {
                //sb.Append( (char) r.Next( (int) 'A' , (int) 'Z' + 1 ) );
                sb.Append( ( char ) r.Next( 65 , 91 ) );
            }

            return sb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="iSaltLength"></param>
        /// <returns></returns>
        static public string NextSalt( this Random r , int iSaltLength = 5 )
        {
            StringBuilder sb = new StringBuilder( iSaltLength );

            for( int iCounter = 0 ; iCounter < iSaltLength ; iCounter++ )
            {
                sb.Append( ( char ) r.Next( 32 , 255 ) );
            }

            return sb.ToString();
        }


        /// <summary>
        /// Returns the next Color.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        static public Color NextColor( this Random r )
        {
            return Color.FromRgb( ( byte ) r.Next( 256 ) , ( byte ) r.Next( 256 ) , ( byte ) r.Next( 256 ) );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        static public string NextAutoKennzeichen( this Random r )
        {
            string strPart1 = r.NextString( 2 , 3 );
            string strPart2 = r.NextString( 2 , 2 );
            int iPart3 = r.Next( 100 , 9999 );

            return string.Format( "{0}-{1} {2}" , strPart1 , strPart2 , iPart3 );
        }

    } // end static class RandomExtension
}
