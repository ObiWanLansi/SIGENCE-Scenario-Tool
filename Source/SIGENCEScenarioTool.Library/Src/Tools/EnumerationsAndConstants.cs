
namespace SIGENCEScenarioTool.Tools
{

    /// <summary>
    /// 
    /// </summary>
    public enum GeoTag : byte
    {

        /// <summary>
        /// 
        /// </summary>
        Aeroway,

        /// <summary>
        /// 
        /// </summary>
        Amenity,

        /// <summary>
        /// 
        /// </summary>
        Craft,

        /// <summary>
        /// 
        /// </summary>
        Emergency,

        /// <summary>
        /// 
        /// </summary>
        Leisure,

        /// <summary>
        /// 
        /// </summary>
        Man_Made,

        /// <summary>
        /// 
        /// </summary>
        Military,

        /// <summary>
        /// 
        /// </summary>
        Place,

        /// <summary>
        /// 
        /// </summary>
        Power,

        /// <summary>
        /// 
        /// </summary>
        Shop,

        /// <summary>
        /// 
        /// </summary>
        Vending

    } // end public enum GeoTag



    /// <summary>
    /// 
    /// </summary>
    public enum Highway : byte
    {
        /// <summary>
        /// Unbekannter Straßentyp
        /// </summary>
        Unknown,

        /// <summary>
        /// Autobahn
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dmotorway
        /// </summary>
        Motorway,

        /// <summary>
        /// Autobahnähnliche Straße
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dtrunk
        /// </summary>
        Trunk,

        /// <summary>
        /// Bundesstraße
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dprimary
        /// </summary>
        Primary,

        /// <summary>
        /// Landes-, (Staats-,) oder sehr gut ausgebaute Kreisstraße
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dsecondary
        /// </summary>
        Secondary,

        /// <summary>
        /// Kreisstraße, sehr gut ausgebaute Gemeindeverbindungsstraße
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dtertiary
        /// </summary>
        Tertiary,

        /// <summary>
        /// Öffentlich befahrbare Nebenstraßen mit einfachstem Ausbauzustand, typischerweise keine Mittellinie
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dunclassified
        /// </summary>
        Unclassified,

        /// <summary>
        /// Straße an und in Wohngebieten, die keiner anderen Straßenklasse angehört (unclassified, tertiary, secondary, primary)
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dresidential
        /// </summary>
        Residential,

        /// <summary>
        /// Erschließungsweg zu oder innerhalb von Einrichtungen wie Sportanlagen, Stränden, Autobahnraststätten oder allgemein zu Gebäuden. Wird auch für den Zugang zu Parkplätzen oder Recyclinghöfen benutzt.
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dservice
        /// </summary>
        Service,

        //---------------------------------------------------------------------


        /// <summary>
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dmotorway_link
        /// </summary>
        Motorway_Link,

        /// <summary>
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dtrunk_link
        /// </summary>
        Trunk_Link,

        /// <summary>
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dprimary_link
        /// </summary>
        Primary_Link,

        /// <summary>
        /// https://wiki.openstreetmap.org/wiki/DE:Tag:highway%3Dsecondary_link
        /// </summary>
        Secondary_Link,

        /// <summary>
        /// https://wiki.openstreetmap.org/wiki/Tag:highway%3Dtertiary_link 
        /// </summary>
        Tertiary_Link

    } // end public enum Highway

}
