using System;



namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DataTypeBase{double}" />
    public sealed class Longitude : DataTypeBase<double>
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
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="Longitude"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Longitude(double value)
        {
            return new Longitude(value);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CULTUREINFO, "{0}", this.Value);
        }


        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool? IsValid()
        {
            throw new NotImplementedException("public override bool? IsValid()");
        }

    } // end sealed public class Longitude 
}
