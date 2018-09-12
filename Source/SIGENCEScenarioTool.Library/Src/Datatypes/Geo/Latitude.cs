namespace SIGENCEScenarioTool.Datatypes.Geo
{
    sealed public class Latitude : DataTypeBase<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Latitude" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Latitude( double value ) : base( value )
        {
        }


        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="Latitude"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator Latitude( double value )
        {
            return new Latitude( value );
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

    } // end sealed public class Latitude 
}
