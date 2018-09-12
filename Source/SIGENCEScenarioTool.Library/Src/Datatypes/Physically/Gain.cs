namespace SIGENCEScenarioTool.Datatypes.Physically
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Datatypes.Physically.DataTypeBase{System.UInt32}" />
    sealed public class Gain : DataTypeBase<uint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gain"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Gain( uint value ) : base( value )
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

    } // end sealed public class Gain
}
