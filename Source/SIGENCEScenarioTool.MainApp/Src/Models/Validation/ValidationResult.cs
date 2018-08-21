using System;
using System.Collections.Generic;



namespace SIGENCEScenarioTool.Models.Validation
{
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



    sealed public class ValidationResultList : List<ValidationResult>
    {
        static public ValidationResultList Empty
        {
            get
            {
                return new ValidationResultList();
            }
        }


        public void Add( Servity sServity , String strMessage , Object oSource )
        {
            Add( new ValidationResult( sServity , strMessage , oSource ) );
        }

    } // end sealed public class ValidationResultList
}
