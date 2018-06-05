@rmdir /s /q .\bin

@powershell.exe -File .\Documentation\ConvertDocuments.ps1

@msbuild.exe /p:Configuration=Release ".\SIGENCE Scenario Tool.sln"

@REM del /s /q .\SIGENCEScenarioTool.Executable\*.*
@ERM copy .\bin\Release\*.* .\SIGENCEScenarioTool.Executable
