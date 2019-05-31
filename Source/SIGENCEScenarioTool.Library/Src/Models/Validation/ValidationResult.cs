using System;
using System.Collections.Generic;

using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Models.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ValidationResult
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [NoDisplay]
        public Guid Id { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the servity.
        /// </summary>
        /// <value>
        /// The servity.
        /// </value>
        public Servity Servity { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public object Source { get; }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <value>
        /// The property.
        /// </value>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult" /> class.
        /// </summary>
        /// <param name="sServity">The servity.</param>
        /// <param name="strMessage">The message.</param>
        /// <param name="oSource">The source.</param>
        /// <param name="strPropertyName">Name of the property.</param>
        /// <param name="oValue">The value.</param>
        public ValidationResult(Servity sServity, string strMessage, object oSource, string strPropertyName, object oValue)
        {
            this.Id = Guid.NewGuid();
            this.Timestamp = DateTime.Now;
            this.Servity = sServity;
            this.Message = strMessage;
            this.Source = oSource;
            this.PropertyName = strPropertyName;
            this.Value = oValue;
        }

    } // end public sealed class ValidationResult



    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{ValidationResult}" />
    public sealed class ValidationResultList : List<ValidationResult>
    {
        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>
        /// The empty.
        /// </value>
        public static ValidationResultList Empty
        {
            get { return new ValidationResultList(); }
        }


        /// <summary>
        /// Adds the specified validation.
        /// </summary>
        /// <param name="sServity">The s servity.</param>
        /// <param name="strMessage">The string message.</param>
        /// <param name="oSource">The o source.</param>
        /// <param name="strPropertyName">Name of the string property.</param>
        /// <param name="oValue">The o value.</param>
        public void Add(Servity sServity, string strMessage, object oSource, string strPropertyName, object oValue)
        {
            Add(new ValidationResult(sServity, strMessage, oSource, strPropertyName, oValue));
        }

    } // end public sealed class ValidationResultList
}
