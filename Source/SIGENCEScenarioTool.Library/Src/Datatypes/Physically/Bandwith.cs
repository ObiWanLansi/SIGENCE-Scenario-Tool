namespace SIGENCEScenarioTool.Datatypes.Physically
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Datatypes.Physically.DataTypeBase{uint}" />
    sealed public class Bandwith : DataTypeBase<uint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bandwith"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Bandwith(uint value) : base(value)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Froms the string.
        ///// </summary>
        ///// <param name="strValue">The string value.</param>
        ///// <returns></returns>
        //public override uint FromString(string strValue)
        //{
        //    return uint.Parse(strValue);
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

    } // end sealed public class Bandwith
}
