namespace SIGENCEScenarioTool.Datatypes.Physically
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Datatypes.Physically.DataTypeBase{System.UInt32}" />
    sealed public class SignalToNoiseRatio : DataTypeBase<uint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignalToNoiseRatio" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public SignalToNoiseRatio( uint value ) : base( value )
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

    } // end sealed public class SignalToNoiseRatio 
}
