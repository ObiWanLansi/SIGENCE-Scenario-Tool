using System;
using System.Collections.Generic;



namespace SIGENCEScenarioTool.Models.Validation
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class ValidationResult
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets the servity.
        /// </summary>
        /// <value>
        /// The servity.
        /// </value>
        public Servity Servity { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public String Message { get; private set; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public Object Source { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="sServity">The s servity.</param>
        /// <param name="strMessage">The string message.</param>
        /// <param name="oSource">The o source.</param>
        public ValidationResult(Servity sServity, String strMessage, Object oSource)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Servity = sServity;
            Message = strMessage;
            Source = oSource;
        }

    } // end sealed public class ValidationResult



    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{SIGENCEScenarioTool.Models.Validation.ValidationResult}" />
    sealed public class ValidationResultList : List<ValidationResult>
    {
        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>
        /// The empty.
        /// </value>
        static public ValidationResultList Empty
        {
            get
            {
                return new ValidationResultList();
            }
        }


        /// <summary>
        /// Adds the specified validation.
        /// </summary>
        /// <param name="sServity">The s servity.</param>
        /// <param name="strMessage">The string message.</param>
        /// <param name="oSource">The o source.</param>
        public void Add(Servity sServity, String strMessage, Object oSource)
        {
            Add(new ValidationResult(sServity, strMessage, oSource));
        }

    } // end sealed public class ValidationResultList
}
