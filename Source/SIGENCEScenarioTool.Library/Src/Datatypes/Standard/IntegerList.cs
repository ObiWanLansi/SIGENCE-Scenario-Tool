using System.Collections.Generic;



namespace SIGENCEScenarioTool.Datatypes.Standard
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{System.Int32}" />
    public sealed class IntegerList : List<int>
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
        /// Initializes a new instance of the <see cref="IntegerList"/> class.
        /// </summary>
        /// <param name="collection">Die Auflistung, deren Elemente in die neue Liste kopiert werden.</param>
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
        public static IntegerList operator *(IntegerList ilSource, int iMultiplier)
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
