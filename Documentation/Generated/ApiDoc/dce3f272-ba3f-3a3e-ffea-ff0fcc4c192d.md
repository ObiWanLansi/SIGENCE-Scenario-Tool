# RandomExtension.NextEnum Method (Random, Type)<a href="https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool">SIGENCE Scenario Tool Library Home</a> _**\[This is preliminary documentation and is subject to change.\]**_

Nexts the enum.

**Namespace:**&nbsp;<a href="f2af11f5-ae9d-3dcc-a4a9-ba07a037925f.md">SIGENCEScenarioTool.Extensions</a><br />**Assembly:**&nbsp;SIGENCEScenarioTool.Library (in SIGENCEScenarioTool.Library.dll) Version: 1.5.0.0 (1.5)

## Syntax

**C#**<br />
``` C#
public static int NextEnum(
	this Random r,
	Type tEnum
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function NextEnum ( 
	r As Random,
	tEnum As Type
) As Integer
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static int NextEnum(
	Random^ r, 
	Type^ tEnum
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member NextEnum : 
        r : Random * 
        tEnum : Type -> int 

```


#### Parameters
&nbsp;<dl><dt>r</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/ts6se2ek" target="_blank">System.Random</a><br />The r.</dd><dt>tEnum</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">System.Type</a><br />The t enum.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">Int32</a><br />

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="http://msdn2.microsoft.com/en-us/library/ts6se2ek" target="_blank">Random</a>. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="ec79cd66-cabe-b34d-c958-1063ff30e004.md">RandomExtension Class</a><br /><a href="b880c301-4e06-70c6-708c-7ed021071cfb.md">NextEnum Overload</a><br /><a href="f2af11f5-ae9d-3dcc-a4a9-ba07a037925f.md">SIGENCEScenarioTool.Extensions Namespace</a><br />