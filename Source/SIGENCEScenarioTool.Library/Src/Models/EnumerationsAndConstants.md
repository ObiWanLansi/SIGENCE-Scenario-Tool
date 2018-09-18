# Overview Of Enumeration Types

Here are all known Enumerations used by SIGENCE Scenario Tool.
At present, the overview is still created by hand. 
But it is planned to have this created by a script that is always up-to-date.

<hr/>

<span style="color: #FF0000;">

<center><h1>OBSOLETE</h1></center>

## RxTxType

*For all receivers (i.e. ID’s < 0) this parameter defines the radio being used:*

Name|Value|
-|-|
HackRF|-1
TwinRx|-2
B200Mini|-3
IdealSDR|-4

*For transmitters (i.e. ID’s >= 0) this parameter defines transmitter signal type:*

Name|Value|
-|-|
QPSK|1
SIN|2
FMRadio|3

*Should not happen, but you never know if the Vogons will come:*

Name|Value|
-|-|
Unknown|4242

</span>

<hr/>

## AntennaType

Name|Value|
-|-|
OmniDirectional|0
OmniLOG30800|1
HyperLOG60200|2
SimradArgusRadar|3
Unknown|255
