namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Datatypes.DataTypeBase{int}" />
    public sealed class Altitude : DataTypeBase<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Altitude"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Altitude(int value) : base(value)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="Altitude"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator Altitude(int value)
        {
            return new Altitude(value);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Froms the string.
        ///// </summary>
        ///// <param name="strValue">The string value.</param>
        ///// <returns></returns>
        //public override int FromString(string strValue)
        //{
        //    return int.Parse(strValue);
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

    } // end sealed public class Altitude 
}
