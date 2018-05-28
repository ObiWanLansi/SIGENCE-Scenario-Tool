using System.Collections.Generic;



namespace TransmitterTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class TransmitterList : List<Transmitter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransmitterList"/> class.
        /// </summary>
        public TransmitterList()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TransmitterList"/> class.
        /// </summary>
        /// <param name="iInitialSize">Initial size of the i.</param>
        public TransmitterList(int iInitialSize) : base(iInitialSize)
        {
        }

    } // end sealed public class TransmitterList
}
