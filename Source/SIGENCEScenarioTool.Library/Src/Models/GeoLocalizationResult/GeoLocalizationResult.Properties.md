
# GeoLocalizationResult (Version 17)

*Represent The Geo Localization Result Of A RFDevice.*

Name|DataType|DefaultValue|Comment
----|--------|------------|-------
PrimaryKey|Guid|Guid.NewGuid()|The Unique PrimarKey For This Result.
Id|int|0|The Id Of The Localized RFDevice.
Latitude|double|double.NaN|The Latitude Of The Localized RF Device (WGS84).
Longitude|double|double.NaN|The Longitude Of The Localized RF Device (WGS84).
Altitude|uint|0|The Elevation Of The Localized RF Device Above The Sea Level (Meter).
LocalizationTime|double|0|The Localization Time.
