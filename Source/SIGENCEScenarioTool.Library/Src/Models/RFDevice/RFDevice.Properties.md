
# RFDevice (Version 21)

*Represent A Device Based On A Radio Frequency.*

Name|DataType|DefaultValue|Comment
----|--------|------------|-------
PrimaryKey|Guid|Guid.NewGuid()|The Unique PrimarKey For This RF Device.
Id|int|0|Every Scenario Element (I.E. Transmitter, Receiver) Must Be Assigned An Unique Id. Negative Id’S Are Reserved For Receivers While All Other Id’S Are Transmitters By Default. Some Applications (I.E. Tdoa Emitter Localization) Require A Reference Transmitter. For These Applications Id=0 Is The Reference Transmitter. Receivers Must Be Assigned First In The Table, Followed Be Transmitters (With Id=0 Being The First). After The Static Scenario, Update Of Id’S Requires No Specific Order. Note That Definition Of New Transmitters/Receivers After The Static Scenario Is Prohibited.
DeviceSource|DeviceSource|DeviceSource.Unknown|The Source Of This RF Device.
StartTime|double|0|This Is The Simulation Time At Which The Parameters (Following The Time Parameter In The Same Line) Are Set. All Transmitters And Receivers Used In The Simulation Must Be Set At Start Of The Simulation, I.E. At Time=0. For Static Scenarios, Where Positions Or Characteristics Settings Never Change Throughout The Simulation, The Time Column Only Contains Zero’s.
Name|string|"RFDevice"|A Short Describing Display Name For The RF Device.
Latitude|Latitude|double.NaN|The Latitude Of The RF Device (WGS84).
Longitude|Longitude|double.NaN|The Longitude Of The RF Device (WGS84).
Altitude|Altitude|0|The Elevation Of The RF Device Above The Sea Level (Meter).
Roll|double|0|These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
Pitch|double|0|These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
Yaw|double|0|These Parameters Set The Orientation Of Transmitter / Receiver Antennas. The Respective Antenna Type Is Defined By Antennatype. The Rf Simulation Uses The Antenna Orientation To Compute The Resulting Signal Power At The Receivers.
RxTxType|RxTxType|RxTxTypes.RxTxTypes.Unknown|For All Receivers (i.e. ID’s < 0) This Parameter Defines The Radio Being Used.
AntennaType|AntennaType|AntennaType.Unknown|AntennaType Defines The Antenna Type Used For Transmitter And Receiver Respectively. Note: Currently, Only Omnidirectional Antenna Type Is Available / Supported.
CenterFrequency_Hz|Frequency|0|For Transmitters (I.E. Id’s >= 0) This Parameter Defines Transmitter Signal Center Frequency [Hz]. For Receivers (I.E. Id’s < 0) This Parameter Is Currently Unused.
Bandwidth_Hz|Bandwidth|0|The Bandwith Of The Transmitter.
Gain_dB|Gain|0|For Transmitters (I.E. Id’s >= 0) This Parameter Defines Transmitter Signal Power [Dbm]. For Receivers (I.E. Id’s < 0) This Parameter Is Currently Unused.
SignalToNoiseRatio_dB|SignalToNoiseRatio|0|For Receivers (I.E. Id’s < 0) This Parameter Is Imposes Gaussian White Noise To The Respective Receiver Signal. For Transmitters (I.E. Id’s >= 0) This Parameter Is Unused.
XPos|int|0|XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
YPos|int|0|XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
ZPos|int|0|XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
Remark|string|""|A Comment Or Remark For The RF Device.
TechnicalParameters|string|""|Additional (Optional) Technical Parameters For The Simulation.
