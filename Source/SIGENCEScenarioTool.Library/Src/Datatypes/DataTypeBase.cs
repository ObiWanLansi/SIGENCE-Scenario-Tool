using System;
using System.Globalization;



namespace SIGENCEScenarioTool.Datatypes
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataTypeBase<T> : IComparable<T>, IEquatable<T> where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// The ci
        /// </summary>
        static protected readonly CultureInfo CULTUREINFO = new CultureInfo("en-US");

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value in it's default SI Einheit.
        /// </value>
        public T Value { get; set; } = default(T);

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="DataTypeBase{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public DataTypeBase(T value)
        {
            this.Value = value;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Liefert den Wert als den generischen Typ zurück.
        /// </summary>
        /// <param name="apb">The apb.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator T(DataTypeBase<T> apb)
        {
            return apb.Value;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            //return string.Format( ci , "{0}" , this.Value );
            return this.Value.ToString();
        }


        /// <summary>
        /// Returns true if the value is valid, false when he is invalid and null when it is not neccery to check it or not implemented.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>This Funktion Is For The Future And Get Currently Not Evaluated Anywhere, So Devired Class Should Throw A NotImplementedException</remarks>
        abstract public bool? IsValid();


        /// <summary>
        /// Vergleicht die aktuelle Instanz mit einem anderen Objekt vom selben Typ und gibt eine ganze Zahl zurück, die angibt, ob die aktuelle Instanz in der Sortierreihenfolge vor oder nach dem anderen Objekt oder an derselben Position auftritt.
        /// </summary>
        /// <param name="other">Ein Objekt, das mit dieser Instanz verglichen werden soll.</param>
        /// <returns>
        /// Ein Wert, der die relative Reihenfolge der verglichenen Objekte angibt.Der Rückgabewert hat folgende Bedeutung:Wert Bedeutung Kleiner als 0 (null) Diese Instanz befindet sich in der Sortierreihenfolge vor <paramref name="other" />.  Zero Diese Instanz tritt in der Sortierreihenfolge an der gleichen Position wie <paramref name="other" /> auf. Größer als 0 (null) Diese Instanz folgt in der Sortierreihenfolge auf <paramref name="other" />.
        /// </returns>
        public int CompareTo(T other)
        {
            return ((IComparable<T>)this.Value).CompareTo(other);
        }


        /// <summary>
        /// Gibt an, ob das aktuelle Objekt gleich einem anderen Objekt des gleichen Typs ist.
        /// </summary>
        /// <param name="other">Ein Objekt, das mit diesem Objekt verglichen werden soll.</param>
        /// <returns>
        /// true, wenn das aktuelle Objekt gleich dem <paramref name="other" />-Parameter ist, andernfalls false.
        /// </returns>
        public bool Equals(T other)
        {
            return ((IEquatable<T>)this.Value).Equals(other);
        }

    } // end public abstract class DataTypeBase<T>
}
