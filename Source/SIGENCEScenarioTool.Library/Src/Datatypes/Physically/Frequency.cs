namespace SIGENCEScenarioTool.Datatypes.Physically
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Datatypes.Physically.DataTypeBase{System.UInt64}" />
    sealed public class Frequency : DataTypeBase<ulong>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Frequency"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Frequency( ulong value ) : base( value )
        {
        }


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

    } // end sealed public class Frequency
}
