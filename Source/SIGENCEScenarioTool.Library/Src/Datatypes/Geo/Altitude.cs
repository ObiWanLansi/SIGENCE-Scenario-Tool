using System;



namespace SIGENCEScenarioTool.Datatypes.Geo
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Datatypes.DataTypeBase{int}" />
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
        /// Performs an implicit conversion from <see cref="int"/> to <see cref="Altitude"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Altitude(int value)
        {
            return new Altitude(value);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Returns a <see cref="System.String" /> that represents this instance.
        ///// </summary>
        ///// <returns>
        ///// A <see cref="System.String" /> that represents this instance.
        ///// </returns>
        //public override string ToString()
        //{
        //    return string.Format( CULTUREINFO , "{0}" , this.Value );
        //}


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

    } // end sealed public class Altitude 
}
