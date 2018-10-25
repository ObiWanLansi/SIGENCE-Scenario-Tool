


namespace SIGENCEScenarioTool.Models.RxTxTypes
{
    /// <summary>
    /// A class to encapsule an RxTxType.
    /// </summary>
    public sealed class RxTxType
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The empty identifier
        /// </summary>
        public const int EmptyId = -4242;

        ///// <summary>
        ///// Gets the empty.
        ///// </summary>
        ///// <value>
        ///// The empty.
        ///// </value>
        //public static RxTxType Empty
        //{
        //    get
        //    {
        //        // Return every time a new instance so that if sombody change the values the next one have a right empty isntance.
        //        return new RxTxType( EmptyId , null , null );
        //    }
        //}


        /// <summary>
        /// The empty
        /// </summary>
        public static readonly RxTxType Empty = new RxTxType( EmptyId , null , null );

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="RxTxType"/> class.
        /// </summary>
        /// <param name="iValue">The i value.</param>
        /// <param name="strName">Name of the string.</param>
        /// <param name="strRemark">The string remark.</param>
        internal RxTxType( int iValue , string strName , string strRemark )
        {
            this.Value = iValue;
            this.Name = strName;
            this.Remark = strRemark;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Performs an implicit conversion from <see cref="RxTxType" /> to <see cref="int" />.
        /// </summary>
        /// <param name="rtt">The RTT.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator int( RxTxType rtt )
        {
            return rtt.Value;
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
            return this.Name;
        }

    } // end public sealed class RxTxType
}
