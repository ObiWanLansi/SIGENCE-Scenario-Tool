using System;
using System.Collections.Generic;



namespace TransmitterTool.Extensions
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
        static public int NextInt(this Random r)
        {
            return r.Next();
        }


        /// <summary>
        /// Der Vollständigkeit wegen.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        static public uint NextUInt(this Random r)
        {
            return (uint)r.Next();
        }


        /// <summary>
        /// Liefert einen Zufalls Boolschen Wert zurück.
        /// </summary>
        /// <param name="r">The current random object</param>
        /// <returns></returns>
        static public bool NextBool(this Random r)
        {
            return (r.Next() % 2) != 0;
        }


        /// <summary>
        /// Nexts the enum.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="tEnum">The t enum.</param>
        /// <returns></returns>
        static public int NextEnum(this Random r, Type tEnum)
        {
            if (tEnum.IsEnum == false)
            {
                return 0;
            }

            Array aValues = Enum.GetValues(tEnum);

            if (tEnum.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0)
            {
                int iValue = 0;

                for (int iCounter = 0; iCounter < r.Next(aValues.Length); iCounter++)
                {
                    iValue |= (int)aValues.GetValue(r.Next(aValues.Length));
                }

                return (int)Enum.ToObject(tEnum, iValue);
            }

            return (int)aValues.GetValue(r.Next(aValues.Length));
        }


        /// <summary>
        /// Liefert einen zufälligen Enumeration Wert zu einer Enumeration zurück.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The current random object</param>
        /// <returns></returns>
        static public T NextEnum<T>(this Random r)
        {
            Type tEnum = typeof(T);

            if (tEnum.IsEnum == false)
            {
                return default(T);
            }

            Array aValues = Enum.GetValues(tEnum);

            if (tEnum.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0)
            {
                int iValue = 0;

                for (int iCounter = 0; iCounter < r.Next(aValues.Length); iCounter++)
                {
                    iValue |= (int)aValues.GetValue(r.Next(aValues.Length));
                }

                return (T)Enum.ToObject(tEnum, iValue);
            }

            return (T)aValues.GetValue(r.Next(aValues.Length));
        }


        /// <summary>
        /// Nexts the long.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        static public long NextLong(this Random r)
        {
            return (((long)r.NextInt()) << 32) + r.NextInt();
        }


        /// <summary>
        /// Nexts the u long.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        static public ulong NextULong(this Random r)
        {
            return ((ulong)r.NextInt() << 32) + (ulong)r.NextInt();
        }


        /// <summary>
        /// Nexts the date time.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="dtMin">The dt minimum.</param>
        /// <param name="dtMax">The dt maximum.</param>
        /// <param name="dtk">The DTK.</param>
        /// <returns></returns>
        static public DateTime NextDateTime(this Random r, DateTime dtMin, DateTime dtMax, DateTimeKind dtk = DateTimeKind.Local)
        {
            return new DateTime((r.NextLong() % (dtMax.Ticks - dtMin.Ticks)) + dtMin.Ticks, dtk);
        }


        /// <summary>
        /// Nexts the date time.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="dtk">The DTK.</param>
        /// <returns></returns>
        static public DateTime NextDateTime(this Random r, DateTimeKind dtk = DateTimeKind.Local)
        {
            return NextDateTime(r, DateTime.MinValue, DateTime.MaxValue, dtk);
        }


        /// <summary>
        /// Nexts the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The r.</param>
        /// <param name="lValues">The l values.</param>
        /// <returns></returns>
        static public T NextObject<T>(this Random r, IList<T> lValues)
        {
            return lValues[r.Next(lValues.Count)];
        }


        /// <summary>
        /// Nexts the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The r.</param>
        /// <param name="cValues">The c values.</param>
        /// <returns></returns>
        static public T NextObject<T>(this Random r, ICollection<T> cValues)
        {
            int iSelectedValue = r.Next(cValues.Count);
            int iCounter = 0;

            IEnumerator<T> e = cValues.GetEnumerator();

            while (e.MoveNext())
            {
                if (iCounter++ == iSelectedValue)
                {
                    return e.Current;
                }
            }

            return default(T);
        }

    } // end static class RandomExtension
}
