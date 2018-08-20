using System;
using System.Collections.ObjectModel;



namespace SIGENCEScenarioTool.Models.Validation
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class ValidationResult
    {

        public Guid Id { get; private set; }

        public DateTime Timestamp { get; private set; }

        public Servity Servity { get; private set; }

        public String Message { get; private set; }

        public Object Source { get; private set; }

        public ValidationResult( Servity sServity , String strMessage , Object oSource )
        {
            this.Id = Guid.NewGuid();
            this.Timestamp = DateTime.Now;
            this.Servity = sServity;
            this.Message = strMessage;
            this.Source = oSource;
        }

    } // end sealed public class ValidationResult



    ///// <summary>
    ///// 
    ///// </summary>
    //sealed public class ValidationResultList : ObservableCollection<ValidationResult>
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    static public ValidationResultList Empty
    //    {
    //        get
    //        {
    //            return new ValidationResultList();
    //        }
    //    }

    //} // end sealed public class ValidationResultList
}
