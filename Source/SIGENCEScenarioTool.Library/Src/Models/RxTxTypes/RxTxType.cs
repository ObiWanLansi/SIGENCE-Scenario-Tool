


namespace SIGENCEScenarioTool.Models.RxTxTypes
{
    /// <summary>
    /// A class to encapsule an RxTxType.
    /// </summary>
    sealed public class RxTxType
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="RxTxType"/> class.
        /// </summary>
        /// <param name="iValue">The i value.</param>
        /// <param name="strName">Name of the string.</param>
        /// <param name="strRemark">The string remark.</param>
        internal RxTxType( int iValue , string strName , string strRemark )
        {
            Value = iValue;
            Name = strName;
            Remark = strRemark;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

    } // end sealed public class RxTxType
}
