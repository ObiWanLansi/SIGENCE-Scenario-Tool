# DictionaryExtension.ForEach(*TKey*, *TValue*) Method (Dictionary(*TKey*, *TValue*), Action(*TKey*, *TValue*))<a href="https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool">SIGENCE Scenario Tool Library Home</a> _**\[This is preliminary documentation and is subject to change.\]**_

Fors the each.

**Namespace:**&nbsp;<a href="f2af11f5-ae9d-3dcc-a4a9-ba07a037925f.md">SIGENCEScenarioTool.Extensions</a><br />**Assembly:**&nbsp;SIGENCEScenarioTool.Library (in SIGENCEScenarioTool.Library.dll) Version: 1.5.0.0 (1.5)

## Syntax

**C#**<br />
``` C#
public static void ForEach<TKey, TValue>(
	this Dictionary<TKey, TValue> dict,
	Action<TKey, TValue> action
)

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub ForEach(Of TKey, TValue) ( 
	dict As Dictionary(Of TKey, TValue),
	action As Action(Of TKey, TValue)
)
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
generic<typename TKey, typename TValue>
static void ForEach(
	Dictionary<TKey, TValue>^ dict, 
	Action<TKey, TValue>^ action
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member ForEach : 
        dict : Dictionary<'TKey, 'TValue> * 
        action : Action<'TKey, 'TValue> -> unit 

```


#### Parameters
&nbsp;<dl><dt>dict</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/xfhwa508" target="_blank">System.Collections.Generic.Dictionary</a>(*TKey*, *TValue*)<br />The dict.</dd><dt>action</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/bb549311" target="_blank">System.Action</a>(*TKey*, *TValue*)<br />The action.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TKey</dt><dd>The type of the key.</dd><dt>TValue</dt><dd>The type of the value.</dd></dl>

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="http://msdn2.microsoft.com/en-us/library/xfhwa508" target="_blank">Dictionary</a>(*TKey*, *TValue*). When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="fad9f38d-4408-43b2-763a-5dd0dacf6b42.md">DictionaryExtension Class</a><br /><a href="8ea755ad-e5e3-ca3a-a35a-2e491f29c72e.md">ForEach Overload</a><br /><a href="f2af11f5-ae9d-3dcc-a4a9-ba07a037925f.md">SIGENCEScenarioTool.Extensions Namespace</a><br />