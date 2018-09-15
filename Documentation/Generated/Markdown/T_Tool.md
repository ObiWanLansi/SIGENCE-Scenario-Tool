# Tool
## Base Class
- Object
## Fields
Flags|Type|Name
-|-|-
*static*|String|FRANZ
*static*|String|FOX
*static*|String|WILFRIED
*static*|String|XYLOPHONMUSIK
*static*|String|ALLCHARS
*static*|List&lt;T&gt;|ALLPANGRAMS
## Properties
Flags|Type|Name
-|-|-
*r* *w*|String|ProductName
*r* *w*|String|ProductTitle
*r* *w*|String|StartupPath
*r* *w*|String|Version
## Methods
Flags|Result|Name|Parameters
-|-|-|-
*static*|String|GetHumanSize|( Int64 lSizeInBytes )
*static*|String|GetHumanDistance|( Int64 lLengthInMeter )
*static*|String|ReadResourceAsString|( String strResourceName )
*static*|Double|GetGrad|( Double grad , Double minutes , Double seconds )
*static*|String|GetGradMinutesSeconds|( Double grad )
