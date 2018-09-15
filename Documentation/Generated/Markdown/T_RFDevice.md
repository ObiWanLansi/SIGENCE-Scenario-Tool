# Class RFDevice
## Base Class
- [AbstractModelBase](./T_AbstractModelBase.md)
## Fields
Flags|Type|Name
-|-|-
*static*|Guid|DEFAULT_PRIMARYKEY
*static*|Int32|DEFAULT_ID
*static*|DeviceSource|DEFAULT_DEVICESOURCE
*static*|Double|DEFAULT_STARTTIME
*static*|String|DEFAULT_NAME
*static*|Latitude|DEFAULT_LATITUDE
*static*|Longitude|DEFAULT_LONGITUDE
*static*|Altitude|DEFAULT_ALTITUDE
*static*|Double|DEFAULT_ROLL
*static*|Double|DEFAULT_PITCH
*static*|Double|DEFAULT_YAW
*static*|RxTxType|DEFAULT_RXTXTYPE
*static*|AntennaType|DEFAULT_ANTENNATYPE
*static*|Frequency|DEFAULT_CENTERFREQUENCY_HZ
*static*|Bandwidth|DEFAULT_BANDWIDTH_HZ
*static*|Gain|DEFAULT_GAIN_DB
*static*|SignalToNoiseRatio|DEFAULT_SIGNALTONOISERATIO_DB
*static*|Int32|DEFAULT_XPOS
*static*|Int32|DEFAULT_YPOS
*static*|Int32|DEFAULT_ZPOS
*static*|String|DEFAULT_REMARK
*static*|String|PRIMARYKEY
*static*|String|ID
*static*|String|DEVICESOURCE
*static*|String|STARTTIME
*static*|String|NAME
*static*|String|LATITUDE
*static*|String|LONGITUDE
*static*|String|ALTITUDE
*static*|String|ROLL
*static*|String|PITCH
*static*|String|YAW
*static*|String|RXTXTYPE
*static*|String|ANTENNATYPE
*static*|String|CENTERFREQUENCY_HZ
*static*|String|BANDWIDTH_HZ
*static*|String|GAIN_DB
*static*|String|SIGNALTONOISERATIO_DB
*static*|String|XPOS
*static*|String|YPOS
*static*|String|ZPOS
*static*|String|REMARK
## Properties
Flags|Type|Name
-|-|-
*r* *w*|Guid|PrimaryKey
*r* *w*|Int32|Id
*r* *w*|DeviceSource|DeviceSource
*r* *w*|Double|StartTime
*r* *w*|String|Name
*r* *w*|Latitude|Latitude
*r* *w*|Longitude|Longitude
*r* *w*|Altitude|Altitude
*r* *w*|Double|Roll
*r* *w*|Double|Pitch
*r* *w*|Double|Yaw
*r* *w*|RxTxType|RxTxType
*r* *w*|AntennaType|AntennaType
*r* *w*|Frequency|CenterFrequency_Hz
*r* *w*|Bandwidth|Bandwidth_Hz
*r* *w*|Gain|Gain_dB
*r* *w*|SignalToNoiseRatio|SignalToNoiseRatio_dB
*r* *w*|Int32|XPos
*r* *w*|Int32|YPos
*r* *w*|Int32|ZPos
*r* *w*|String|Remark
## Constructors
Flags|Name|Parameters
-|-|-
&nbsp;|RFDevice|( )
## Methods
Flags|Result|Name|Parameters
-|-|-|-
&nbsp;|XElement|ToXml|( )
*static*|RFDevice|FromXml|( XElement eRoot )
&nbsp;|Boolean|Equals|( RFDevice other )
&nbsp;|RFDevice|Clone|( )
&nbsp;|ValidationResultList|Validate|( )
&nbsp;|String|ToString|( )

<br /><hr />
SIGENCEScenarioTool.Library, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
