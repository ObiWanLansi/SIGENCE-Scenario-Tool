namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Datatypes.DataTypeBase{double}" />
    sealed public class Longitude : DataTypeBase<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Longitude"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Longitude(double value) : base(value)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="Longitude"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator Longitude(double value)
        {
            return new Longitude(value);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Froms the string.
        ///// </summary>
        ///// <param name="strValue">The string value.</param>
        ///// <returns></returns>
        //public override double FromString(string strValue)
        //{
        //    return double.Parse(strValue);
        //}


        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool? IsValid()
        {
            return null;
        }

    } // end sealed public class Longitude 
}
