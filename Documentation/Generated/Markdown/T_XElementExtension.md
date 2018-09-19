# Class XElementExtension
## Base Class
- Object
## Methods
Flags|Result|Name|Parameters
-|-|-|-
*static*|DateTime?|GetDateTimeFromNodeUTC|( XElement xCurrentElement , String strElementName )
*static*|Int64?|GetLongFromNode|( XElement xCurrentElement , String strElementName )
*static*|Int32?|GetInt32FromNode|( XElement xCurrentElement , String strElementName )
*static*|UInt32?|GetUInt32FromNode|( XElement xCurrentElement , String strElementName )
*static*|Single?|GetSingleFromNode|( XElement xCurrentElement , String strElementName )
*static*|Single?|GetSingleFromNodeComma|( XElement xCurrentElement , String strElementName )
*static*|Single?|GetSingleFromNodePoint|( XElement xCurrentElement , String strElementName )
*static*|Double?|GetDoubleFromNode|( XElement xCurrentElement , String strElementName )
*static*|Double?|GetDoubleFromNodeComma|( XElement xCurrentElement , String strElementName )
*static*|Double?|GetDoubleFromNodePoint|( XElement xCurrentElement , String strElementName )
*static*|String|GetStringFromNode|( XElement xCurrentElement , String strElementName )
*static*|String|GetStringFromNode|( XElement xCurrentElement , String strNamespace , String strElementName )
*static*|String|GetStringFromCData|( XElement xCurrentElement , String strElementName )
*static*|Boolean?|GetBoolFromNode|( XElement xCurrentElement , String strElementName )
*static*|Guid?|GetGuidFromNode|( XElement xCurrentElement , String strElementName )
*static*|BitmapSource|GetBitmapSourceFromNode|( XElement xCurrentElement , String strElementName )
*static*|T|GetEnumFromNode|( XElement xCurrentElement , String strElementName , T tDefault )
*static*|Color|GetColorFromNode|( XElement xCurrentElement , String strElementName , Color cDefault )
*static*|FileInfo|GetFileInfoFromNode|( XElement xCurrentElement , String strElementName , FileInfo fiDefault )
*static*|DirectoryInfo|GetDirectoryInfoFromNode|( XElement xCurrentElement , String strElementName , DirectoryInfo diDefault )
*static*|DateTime?|GetDateTimeAttribute|( XElement eParent , String strName , Boolean bIsUTC )
*static*|String|GetStringAttribute|( XElement eParent , String strName )
*static*|Boolean?|GetBoolAttribute|( XElement eParent , String strName )
*static*|UInt32?|GetUInt32Attribute|( XElement eParent , String strName )
*static*|Int32?|GetInt32Attribute|( XElement eParent , String strName )
*static*|Int64?|GetInt64Attribute|( XElement eParent , String strName )
*static*|Double?|GetDoubleAttribute|( XElement eParent , String strName )
*static*|Single?|GetSingleAttribute|( XElement eParent , String strName )
*static*|XElement|GetXElement|( String strPropertyName , Object o )
*static*|T|GetProperty|( XElement eParent , String strElementName , T tDefault )
*static*|Void|SaveDefault|( XElement element , String strOutputFilename )
*static*|String|ToDefaultString|( XElement element )

<br /><hr />
SIGENCEScenarioTool.Library, Version=15.0.0.0, Culture=neutral, PublicKeyToken=null
