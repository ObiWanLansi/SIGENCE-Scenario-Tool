# RFDeviceExtensions.WithYaw Method <a href="https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool">SIGENCE Scenario Tool Library Home</a> _**\[This is preliminary documentation and is subject to change.\]**_

**Namespace:**&nbsp;<a href="f93b21e6-e11a-5c2f-6a3f-e615945fd019.md">SIGENCEScenarioTool.Models</a><br />**Assembly:**&nbsp;SIGENCEScenarioTool.Library (in SIGENCEScenarioTool.Library.dll) Version: 1.5.0.0 (1.5)

## Syntax

**C#**<br />
``` C#
public static RFDevice WithYaw(
	this RFDevice instance,
	double value
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function WithYaw ( 
	instance As RFDevice,
	value As Double
) As RFDevice
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static RFDevice^ WithYaw(
	RFDevice^ instance, 
	double value
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member WithYaw : 
        instance : RFDevice * 
        value : float -> RFDevice 

```


#### Parameters
&nbsp;<dl><dt>instance</dt><dd>Type: <a href="a824a6f0-dedb-4d3f-8139-8c48872258ae.md">SIGENCEScenarioTool.Models.RFDevice</a><br /></dd><dt>value</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/643eft0t" target="_blank">System.Double</a><br /></dd></dl>

#### Return Value
Type: <a href="a824a6f0-dedb-4d3f-8139-8c48872258ae.md">RFDevice</a>

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="a824a6f0-dedb-4d3f-8139-8c48872258ae.md">RFDevice</a>. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="196b2488-5270-e65a-b6a9-547c84a5341b.md">RFDeviceExtensions Class</a><br /><a href="f93b21e6-e11a-5c2f-6a3f-e615945fd019.md">SIGENCEScenarioTool.Models Namespace</a><br />