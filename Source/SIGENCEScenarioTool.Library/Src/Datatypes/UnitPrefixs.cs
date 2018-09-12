namespace SIGENCEScenarioTool.Datatypes
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class UnitPrefixs
    {

        /// <summary>
        /// The exa
        /// </summary>
        static public readonly UnitPrefix Exa = new UnitPrefix( "Exa" , "E" , 1_000_000_000_000_000_000 );

        /// <summary>
        /// The peta
        /// </summary>
        static public readonly UnitPrefix Peta = new UnitPrefix( "Peta" , "P" , 1_000_000_000_000_000 );

        /// <summary>
        /// The tera
        /// </summary>
        static public readonly UnitPrefix Tera = new UnitPrefix( "Tera" , "T" , 1_000_000_000_000 );

        /// <summary>
        /// The giga
        /// </summary>
        static public readonly UnitPrefix Giga = new UnitPrefix( "Giga" , "G" , 1_000_000_000 );

        /// <summary>
        /// The mega
        /// </summary>
        static public readonly UnitPrefix Mega = new UnitPrefix( "Mega" , "M" , 1_000_000 );

        /// <summary>
        /// The kilo
        /// </summary>
        static public readonly UnitPrefix Kilo = new UnitPrefix( "Kilo" , "k" , 1_000 );

        /// <summary>
        /// The default
        /// </summary>
        static public readonly UnitPrefix Default = new UnitPrefix( "Def" , "" , 1 );

        /// <summary>
        /// The milli
        /// </summary>
        static public readonly UnitPrefix Milli = new UnitPrefix( "Milli" , "m" , 0.001 );

        /// <summary>
        /// The mikro
        /// </summary>
        static public readonly UnitPrefix Mikro = new UnitPrefix( "Mikro" , "µ" , 0.000_001 );

        /// <summary>
        /// The nano
        /// </summary>
        static public readonly UnitPrefix Nano = new UnitPrefix( "Nano" , "n" , 0.000_000_001 );

        /// <summary>
        /// The piko
        /// </summary>
        static public readonly UnitPrefix Piko = new UnitPrefix( "Piko" , "p" , 0.000_000_000_001 );

        /// <summary>
        /// The femto
        /// </summary>
        static public readonly UnitPrefix Femto = new UnitPrefix( "Femto" , "f" , 0.000_000_000_000_001 );

        /// <summary>
        /// The atto
        /// </summary>
        static public readonly UnitPrefix Atto = new UnitPrefix( "Atto" , "a" , 0.000_000_000_000_000_001 );

    } // end sealed public class UnitPrefixs
}