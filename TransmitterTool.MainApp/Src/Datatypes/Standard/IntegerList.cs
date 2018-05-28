using System.Collections.Generic;



namespace TransmitterTool.Datatypes.Standard
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{System.Int32}" />
    sealed public class IntegerList : List<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerList"/> class.
        /// </summary>
        public IntegerList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerList"/> class.
        /// </summary>
        /// <param name="iSize">Size of the i.</param>
        public IntegerList(int iSize) :
            base(iSize)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public IntegerList(IEnumerable<int> collection) :
            base(collection)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="ilSource">The il source.</param>
        /// <param name="iMultiplier">The i multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        static public IntegerList operator *(IntegerList ilSource, int iMultiplier)
        {
            IntegerList ilDestination = new IntegerList(ilSource);

            for (int i = 0; i < ilDestination.Count; i++)
            {
                ilDestination[i] *= iMultiplier;
            }

            return ilDestination;
        }

    } // end sealed public class IntegerList
}
