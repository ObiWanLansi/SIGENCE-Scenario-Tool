# GeoLocalizationResult
## Base Class
- [AbstractModelBase](./T_AbstractModelBase.md)
## Fields
Flags|Type|Name
-|-|-
*static*|Guid|DEFAULT_PRIMARYKEY
*static*|Int32|DEFAULT_ID
*static*|Double|DEFAULT_LATITUDE
*static*|Double|DEFAULT_LONGITUDE
*static*|UInt32|DEFAULT_ALTITUDE
*static*|Double|DEFAULT_LOCALIZATIONTIME
*static*|String|PRIMARYKEY
*static*|String|ID
*static*|String|LATITUDE
*static*|String|LONGITUDE
*static*|String|ALTITUDE
*static*|String|LOCALIZATIONTIME
## Properties
Flags|Type|Name
-|-|-
*r* *w*|Guid|PrimaryKey
*r* *w*|Int32|Id
*r* *w*|Double|Latitude
*r* *w*|Double|Longitude
*r* *w*|UInt32|Altitude
*r* *w*|Double|LocalizationTime
## Constructors
Flags|Name|Parameters
-|-|-
&nbsp;|GeoLocalizationResult|( )
## Methods
Flags|Result|Name|Parameters
-|-|-|-
&nbsp;|XElement|ToXml|( )
*static*|GeoLocalizationResult|FromXml|( XElement eRoot )
&nbsp;|Boolean|Equals|( GeoLocalizationResult other )
&nbsp;|GeoLocalizationResult|Clone|( )
