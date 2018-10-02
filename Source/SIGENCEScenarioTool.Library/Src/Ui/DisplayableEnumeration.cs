using System;
using System.Collections.ObjectModel;



namespace SIGENCEScenarioTool.Ui
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DisplayableEnumeration
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; internal set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }


        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bAllSelector">if set to <c>true</c> [b all selector].</param>
        /// <returns></returns>
        public static ObservableCollection<DisplayableEnumeration> GetCollection<T>( bool bAllSelector = false )
        {
            Type tEnum = typeof( T );

            if( tEnum.IsEnum == false )
            {
                throw new ArgumentException( $"Bei dem Typ {tEnum.FullName} handelt es sich nicht um ein Aufzählungstyp!" );
            }

            if( tEnum.GetCustomAttributes( typeof( FlagsAttribute ) , false ).Length > 0 )
            {
                throw new ArgumentException( $"Bei dem Typ {tEnum.FullName} handelt es sich um ein Aufzählungstyp mit Mehrfachauswahl!" );
            }

            ObservableCollection<DisplayableEnumeration> oc = new ObservableCollection<DisplayableEnumeration>
            {
                new DisplayableEnumeration {Name = ""}
            };

            foreach( T value in Enum.GetValues( ( tEnum ) ) )
            {
                oc.Add( new DisplayableEnumeration { Value = Convert.ToInt32( value ) , Name = $"{value}" } );
            }

            return oc;
        }

    } // end public sealed class DisplayableEnumeration


}
