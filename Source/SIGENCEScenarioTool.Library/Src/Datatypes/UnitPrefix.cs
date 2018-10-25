namespace SIGENCEScenarioTool.Datatypes
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnitPrefix
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>
        /// The symbol.
        /// </value>
        public string Symbol { get; private set; }

        /// <summary>
        /// Gets or sets the factor.
        /// </summary>
        /// <value>
        /// The factor.
        /// </value>
        public double Factor { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnitPrefix" /> class.
        /// </summary>
        /// <param name="strName">Name of the string.</param>
        /// <param name="strSymbol">The string symblo.</param>
        /// <param name="dFactor">The d factor.</param>
        public UnitPrefix(string strName, string strSymbol, double dFactor)
        {
            this.Name = strName;
            this.Symbol = strSymbol;
            this.Factor = dFactor;
        }

    } // end sealed public class UnitPrefix
}
