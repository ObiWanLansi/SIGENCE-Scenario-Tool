using System;



namespace TransmitterTool.UnitTest.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TransmitterToolTestCaseAttribute : Attribute
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TransmitterToolTestCaseAttribute"/> class.
        /// </summary>
        /// <param name="g">The g.</param>
        public TransmitterToolTestCaseAttribute(Guid g)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TransmitterToolTestCaseAttributeAttribute"/> class.
        /// </summary>
        /// <param name="strGUID">The string unique identifier.</param>
        public TransmitterToolTestCaseAttribute(string strGUID)
        {
            Id = new Guid(strGUID);
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Id.ToString();
        }

    } // end public sealed class TransmitterToolTestCaseAttribute 
}
