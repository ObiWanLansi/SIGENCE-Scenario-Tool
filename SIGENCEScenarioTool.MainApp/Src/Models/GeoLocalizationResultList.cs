using System.Collections.Generic;



namespace SIGENCEScenarioTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class GeoLocalizationResultList : List<GeoLocalizationResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocalizationResultList"/> class.
        /// </summary>
        public GeoLocalizationResultList()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocalizationResultList"/> class.
        /// </summary>
        /// <param name="iInitialSize">Initial size of the i.</param>
        public GeoLocalizationResultList(int iInitialSize) : base(iInitialSize)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocalizationResultList"/> class.
        /// </summary>
        /// <param name="collection">Die Auflistung, deren Elemente in die neue Liste kopiert werden.</param>
        public GeoLocalizationResultList(IEnumerable<GeoLocalizationResult> collection)
            : base(collection)
        {
        }

    } // end sealed public class GeoLocalizationResultList
}
