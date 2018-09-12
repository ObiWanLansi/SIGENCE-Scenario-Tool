using System;
using System.Globalization;



namespace SIGENCEScenarioTool.Datatypes
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //abstract public class DataTypeBase<T> : IFromStringConverter<T> where T : IComparable<T>, IEquatable<T>
    abstract public class DataTypeBase<T> where T : IComparable<T>, IEquatable<T>
    {
        ///// <summary>
        ///// The unit
        ///// </summary>
        //public PhysicalUnit Unit { get; set; } = PhysicalUnits.Default;

        //public UnitPrefix Prefix { get; set; } = UnitPrefixs.Default;


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


        ///// <summary>
        ///// Performs an implicit conversion from <see cref="T"/> to <see cref="DataTypeBase{T}"/>.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns>
        ///// The result of the conversion.
        ///// </returns>
        //static public implicit operator DataTypeBase<T>( T value )
        //{
        //    return new DataTypeBase<T>( value );
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The ci
        /// </summary>
        static private readonly CultureInfo ci = new CultureInfo("en-US");


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(ci, "{0}", this.Value);
        }


        /// <summary>
        /// Returns true if the value is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        abstract public bool? IsValid();


        ///// <summary>
        ///// Froms the string.
        ///// </summary>
        ///// <param name="strValue">The string value.</param>
        ///// <returns></returns>
        //abstract public T FromString(string strValue);

    } // end abstract public class DataTypeBase<T>
}
