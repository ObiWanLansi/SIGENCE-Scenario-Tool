using System;
using System.Collections.Generic;
using System.Text;



namespace TransmitterMan.Extensions
{
    /// <summary>
    /// Eine Erweiterungsklasse für Dictionary &lt;TKey , TValue&gt; und SortedDictionary&lt;TKey , TValue&gt; .
    /// </summary>
    static public class DictionaryExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="action">The action.</param>
        static public void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            IEnumerator<TKey> e = dict.Keys.GetEnumerator();

            while (e.MoveNext())
            {
                action(e.Current, dict[e.Current]);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="action">The action.</param>
        static public void ForEach<TKey, TValue>(this SortedDictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            IEnumerator<TKey> e = dict.Keys.GetEnumerator();

            while (e.MoveNext())
            {
                action(e.Current, dict[e.Current]);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="cDivider"></param>
        /// <returns></returns>
        static public string ToString<TKey, TValue>(this SortedDictionary<TKey, TValue> dict, char cDivider)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerator<TKey> e = dict.Keys.GetEnumerator();

            while (e.MoveNext())
            {
                sb.AppendFormat("{0}={1}{2}", e.Current, dict[e.Current], cDivider);
            }

            return sb.ToString();
        }

    } // end static public class DictionaryExtension
}
