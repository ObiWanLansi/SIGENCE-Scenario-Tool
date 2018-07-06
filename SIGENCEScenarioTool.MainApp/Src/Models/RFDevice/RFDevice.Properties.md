
# RFDevice

*Represent A Device Based On A Radio Frequency.*

Name|DataType|DefaultValue|Comment
----|--------|------------|-------
PrimaryKey|Guid|Guid.NewGuid()|The Unique PrimarKey For This RF Device.
Id|int|0|Every scenario element (i.e. transmitter, receiver) must be assigned an unique ID. Negative ID’s are reserved for receivers while all other ID’s are transmitters by default. Some applications (i.e. TDoA Emitter Localization) require a reference transmitter. For these applications ID=0 is the reference transmitter. Receivers must be assigned first in the table, followed be transmitters (with ID=0 being the first). After the Static Scenario, update of ID’s requires no specific order. Note that definition of new transmitters/receivers after the Static Scenario is prohibited.
Name|string|"RFDevice"|A Short Describing Display Name For The RF Device.
Latitude|double|double.NaN|The Latitude Of The RF Device (WGS84).
Longitude|double|double.NaN|The Longitude Of The RF Device (WGS84).
Altitude|uint|0|The Elevation Of The RF Device Above The Sea Level (Meter).
Roll|double|0|These parameters set the orientation of transmitter / receiver antennas. The respective antenna type is defined by AntennaType. The RF simulation uses the antenna orientation to compute the resulting signal power at the receivers.
Pitch|double|0|These parameters set the orientation of transmitter / receiver antennas. The respective antenna type is defined by AntennaType. The RF simulation uses the antenna orientation to compute the resulting signal power at the receivers.
Yaw|double|0|These parameters set the orientation of transmitter / receiver antennas. The respective antenna type is defined by AntennaType. The RF simulation uses the antenna orientation to compute the resulting signal power at the receivers.
RxTxType|RxTxType|RxTxType.Unknown|For All Receivers (i.e. ID’s < 0) This Parameter Defines The Radio Being Used.
AntennaType|AntennaType|AntennaType.Unknown|AntennaType Defines The Antenna Type Used For Transmitter And Receiver Respectively. Note: Currently, Only Omnidirectional Antenna Type Is Available / Supported.
CenterFrequency_Hz|ulong|0|For transmitters (i.e. ID’s >= 0) this parameter defines transmitter signal center frequency [Hz]. For receivers (i.e. ID’s < 0) this parameter is currently unused.
Bandwith_Hz|uint|0|
Gain_dB|uint|0|For transmitters (i.e. ID’s >= 0) this parameter defines transmitter signal power [dBm]. For receivers (i.e. ID’s < 0) this parameter is currently unused.
SignalToNoiseRatio_dB|uint|0|For receivers (i.e. ID’s < 0) this parameter is imposes Gaussian White Noise to the respective receiver signal. For transmitters (i.e. ID’s >= 0) this parameter is unused.
XPos|int|0|XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
YPos|int|0|XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
ZPos|int|0|XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
StartTime|double|0|This Is The Simulation Time At Which The Parameters (Following The Time Parameter In The Same Line) Are Set. All Transmitters And Receivers Used In The Simulation Must Be Set At Start Of The Simulation, I.E. At Time=0. For Static Scenarios, Where Positions Or Characteristics Settings Never Change Throughout The Simulation, The Time Column Only Contains Zero’s.
Remark|string|""|A Comment Or Remark For The RF Device.
