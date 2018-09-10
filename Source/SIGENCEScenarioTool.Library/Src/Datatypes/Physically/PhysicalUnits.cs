namespace SIGENCEScenarioTool.Datatypes.Physically
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class PhysicalUnits
    {

        /// <summary>
        /// The exa
        /// </summary>
        static public readonly PhysicalUnit Exa = new PhysicalUnit("Exa", "E", 1_000_000_000_000_000_000);

        /// <summary>
        /// The peta
        /// </summary>
        static public readonly PhysicalUnit Peta = new PhysicalUnit("Peta", "P", 1_000_000_000_000_000);

        /// <summary>
        /// The tera
        /// </summary>
        static public readonly PhysicalUnit Tera = new PhysicalUnit("Tera", "T", 1_000_000_000_000);

        /// <summary>
        /// The giga
        /// </summary>
        static public readonly PhysicalUnit Giga = new PhysicalUnit("Giga", "G", 1_000_000_000);

        /// <summary>
        /// The mega
        /// </summary>
        static public readonly PhysicalUnit Mega = new PhysicalUnit("Mega", "M", 1_000_000);

        /// <summary>
        /// The kilo
        /// </summary>
        static public readonly PhysicalUnit Kilo = new PhysicalUnit("Kilo", "k", 1_000);

        /// <summary>
        /// The default
        /// </summary>
        static public readonly PhysicalUnit Default = new PhysicalUnit("Def", "", 1);

        /// <summary>
        /// The milli
        /// </summary>
        static public readonly PhysicalUnit Milli = new PhysicalUnit("Milli", "m", 0.001);

        /// <summary>
        /// The mikro
        /// </summary>
        static public readonly PhysicalUnit Mikro = new PhysicalUnit("Mikro", "µ", 0.000_001);

        /// <summary>
        /// The nano
        /// </summary>
        static public readonly PhysicalUnit Nano = new PhysicalUnit("Nano", "n", 0.000_000_001);

        /// <summary>
        /// The piko
        /// </summary>
        static public readonly PhysicalUnit Piko = new PhysicalUnit("Piko", "p", 0.000_000_000_001);

        /// <summary>
        /// The femto
        /// </summary>
        static public readonly PhysicalUnit Femto = new PhysicalUnit("Femto", "f", 0.000_000_000_000_001);

        /// <summary>
        /// The atto
        /// </summary>
        static public readonly PhysicalUnit Atto = new PhysicalUnit("Atto", "a", 0.000_000_000_000_000_001);

    } // end sealed public class PhysicalUnits
}