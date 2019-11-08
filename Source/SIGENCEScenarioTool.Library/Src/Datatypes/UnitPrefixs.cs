namespace SIGENCEScenarioTool.Datatypes
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnitPrefixs
    {

        /// <summary>
        /// The exa
        /// </summary>
        public static readonly UnitPrefix Exa = new UnitPrefix( "Exa" , "E" , 1_000_000_000_000_000_000 );

        /// <summary>
        /// The peta
        /// </summary>
        public static readonly UnitPrefix Peta = new UnitPrefix( "Peta" , "P" , 1_000_000_000_000_000 );

        /// <summary>
        /// The tera
        /// </summary>
        public static readonly UnitPrefix Tera = new UnitPrefix( "Tera" , "T" , 1_000_000_000_000 );

        /// <summary>
        /// The giga
        /// </summary>
        public static readonly UnitPrefix Giga = new UnitPrefix( "Giga" , "G" , 1_000_000_000 );

        /// <summary>
        /// The mega
        /// </summary>
        public static readonly UnitPrefix Mega = new UnitPrefix( "Mega" , "M" , 1_000_000 );

        /// <summary>
        /// The kilo
        /// </summary>
        public static readonly UnitPrefix Kilo = new UnitPrefix( "Kilo" , "k" , 1_000 );

        /// <summary>
        /// The default
        /// </summary>
        public static readonly UnitPrefix Default = new UnitPrefix( "Def" , "" , 1 );

        /// <summary>
        /// The milli
        /// </summary>
        public static readonly UnitPrefix Milli = new UnitPrefix( "Milli" , "m" , 0.001 );

        /// <summary>
        /// The mikro
        /// </summary>
        public static readonly UnitPrefix Mikro = new UnitPrefix( "Mikro" , "µ" , 0.000_001 );

        /// <summary>
        /// The nano
        /// </summary>
        public static readonly UnitPrefix Nano = new UnitPrefix( "Nano" , "n" , 0.000_000_001 );

        /// <summary>
        /// The piko
        /// </summary>
        public static readonly UnitPrefix Piko = new UnitPrefix( "Piko" , "p" , 0.000_000_000_001 );

        /// <summary>
        /// The femto
        /// </summary>
        public static readonly UnitPrefix Femto = new UnitPrefix( "Femto" , "f" , 0.000_000_000_000_001 );

        /// <summary>
        /// The atto
        /// </summary>
        public static readonly UnitPrefix Atto = new UnitPrefix( "Atto" , "a" , 0.000_000_000_000_000_001 );

    } // end public sealed class UnitPrefixs
}