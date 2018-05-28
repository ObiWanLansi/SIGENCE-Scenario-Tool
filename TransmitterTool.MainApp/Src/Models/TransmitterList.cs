using System.Collections.Generic;



namespace TransmitterTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class TransmitterList : List<Transmitter>
    {
        public TransmitterList()
        {

        }


        public TransmitterList( int iInitialSize ) : base( iInitialSize )
        {

        }

    } // end sealed public class TransmitterList
}
