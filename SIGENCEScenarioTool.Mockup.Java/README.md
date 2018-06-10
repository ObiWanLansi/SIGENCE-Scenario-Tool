This is a small example to show how to create a Java access class for a GeoLocaizationResult object using an XSD file and then send a generated instance using UDP.

- Generate the Java Class. This is possible with the GenerateClasses.bat in the root directory of the java mockup. Maybe you have to edit the directory of the xjc.exe command to your installation of the Java JDK.

- Create a small java project. I used Eclipse for this job an create one class with a main function.

- Type the required code
And test it soon. For testig i used the hardcoded settings 127.0.0.1 as host and 7474 as port. The SIGNENCE Scenarion Tool must nur running, but when, you should see some output in the tabitem "Data Receiver".
