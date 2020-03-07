using System;



namespace SIGENCEScenarioTool.UnitTest.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SIGENCEScenarioToolTestCaseAttribute : Attribute
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SIGENCEScenarioToolTestCaseAttribute"/> class.
        /// </summary>
        /// <param name="g">The g.</param>
        public SIGENCEScenarioToolTestCaseAttribute(Guid g)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SIGENCEScenarioToolTestCaseAttribute"/> class.
        /// </summary>
        /// <param name="strGUID">The string unique identifier.</param>
        public SIGENCEScenarioToolTestCaseAttribute(string strGUID)
        {
            this.Id = new Guid(strGUID);
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }

    } // end public sealed class SIGENCEScenarioToolTestCaseAttribute 
}
