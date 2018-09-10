namespace SIGENCEScenarioTool.Datatypes.Physically
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract public class AbstractPhysicalBase<T> //     : where T is float,double,int,uint,...
    {
        ///// <summary>
        ///// The unit
        ///// </summary>
        //public PhysicalUnit Unit { get; set; } = PhysicalUnits.Default;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value in it's default SI Einheit.
        /// </value>
        public T Value { get; set; } = default(T);

    } // end abstract public class AbstractPhysicalBase<T>
}
