# Class RandomExtension
## Base Class
- Object
## Methods
Flags|Result|Name|Parameters
-|-|-|-
*static*|Int32|NextInt|( Random r )
*static*|UInt32|NextUInt|( Random r )
*static*|Boolean|NextBool|( Random r )
*static*|Int32|NextEnum|( Random r , Type tEnum )
*static*|T|NextEnum|( Random r )
*static*|Int64|NextLong|( Random r )
*static*|UInt64|NextULong|( Random r )
*static*|DateTime|NextDateTime|( Random r , DateTime dtMin , DateTime dtMax , DateTimeKind dtk )
*static*|DateTime|NextDateTime|( Random r , DateTimeKind dtk )
*static*|T|NextObject|( Random r , IList&lt;T&gt; lValues )
*static*|T|NextObject|( Random r , ICollection&lt;T&gt; cValues )
*static*|String|NextString|( Random r , Int32 iMinLength , Int32 iMaxLength )
*static*|String|NextSalt|( Random r , Int32 iSaltLength )
*static*|Color|NextColor|( Random r )
*static*|String|NextAutoKennzeichen|( Random r )

<br /><hr />
SIGENCEScenarioTool.Library, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
