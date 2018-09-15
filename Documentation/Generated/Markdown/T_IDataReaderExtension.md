# Class IDataReaderExtension
## Base Class
- Object
## Methods
Flags|Result|Name|Parameters
-|-|-|-
*static*|String|GetStringOrNull|( IDataReader dbResult , Int32 iColumnIndex )
*static*|Int32?|GetInt32OrNull|( IDataReader dbResult , Int32 iColumnIndex )
*static*|Int64?|GetInt64OrNull|( IDataReader dbResult , Int32 iColumnIndex )
*static*|DateTime?|GetDateTimeOrNull|( IDataReader dbResult , Int32 iColumnIndex )
*static*|IGeometry|GetGeometryFromWKB|( IDataReader dbResult , Int32 iColumnIndex )
*static*|MultiPolygon|GetMultiPolygonFromWKB|( IDataReader dbResult , Int32 iColumnIndex )
*static*|Polygon|GetPolygonFromWKB|( IDataReader dbResult , Int32 iColumnIndex )
*static*|LineString|GetLineStringFromWKB|( IDataReader dbResult , Int32 iColumnIndex )
*static*|Point|GetPointFromWKB|( IDataReader dbResult , Int32 iColumnIndex )

<br /><hr />
SIGENCEScenarioTool.Library, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
