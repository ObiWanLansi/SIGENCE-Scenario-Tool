for i in range(10):
    print (i,". Hello World !")

###################################################################################################
    
if 5 > 4 :
    print ("Fünf ist größer als vier !")
else:
    print ("Fünf ist nicht größer als vier !")

###################################################################################################    

i=5
while i>0:
    print (i)
    i=i-1

###################################################################################################

for device in devices:
    print ("{0}: {1} / {2}".format(device.Name,device.Latitude,device.Longitude))

for device in devices:
    if device.Id > 0:
        device.StartTime = device.StartTime + 10
        device.Latitude = device.Latitude + 0.125


iStartTime = 0
for device in devices:
    if device.Id == 1974:
        device.StartTime = iStartTime 
        iStartTime += 20

###################################################################################################

import clr 

clr.AddReference("SIGENCEScenarioTool")

from SIGENCEScenarioTool.Models import *

for device in devices:
    device.RxTxType = RxTxType.HackRF
    