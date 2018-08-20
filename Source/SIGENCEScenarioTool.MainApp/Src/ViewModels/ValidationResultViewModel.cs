using System;
using System.Collections.ObjectModel;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.Validation;



namespace SIGENCEScenarioTool.ViewModels
{
    sealed public class ValidationResultViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidationResult Result { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public String Timestamp { get { return Result.Timestamp.Fmt_DD_MM_YYYY_HH_MM_SS(); } }

        public Servity Servity { get { return Result.Servity; } }

        public String Message { get { return Result.Message; } }

        //public Object Source { get { return Result.Source; } }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vr"></param>
        public ValidationResultViewModel( ValidationResult vr )
        {
            this.Result = vr;
        }

    } // end sealed public class ValidationResultViewModel



    /// <summary>
    /// 
    /// </summary>
    sealed public class ValidationResultViewModelList : ObservableCollection<ValidationResultViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        static public ValidationResultViewModelList Empty
        {
            get
            {
                return new ValidationResultViewModelList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vr"></param>
        public void Add( ValidationResult vr )
        {
            Add( new ValidationResultViewModel( vr ) );
        }

    } // end sealed public class ValidationResultViewModelList

}
