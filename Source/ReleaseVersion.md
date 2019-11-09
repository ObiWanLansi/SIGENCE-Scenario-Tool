# Merkzettel Create Release Version

## Technische Voraussetzungen

- Microsoft Visual Studio 2019 Community<br/>
    *( andere Version z.B. 2017 geht natürlich auch, das muss dann nur im BuildSkript angepasst werden)*
- .NET Framework 4.6.1 muss vorhanden sein
- Microsoft Word 2016<br/>
- [NAnt](http://nant.sourceforge.net/)<br/>
    *(NAnt is a free .NET build tool)*
- [Nullsoft Scriptable Install System](https://nsis.sourceforge.io/Main_Page)<br/>
    *(NSIS (Nullsoft Scriptable Install System) is a professional open source system to create Windows installers)*
- [7-Zip](https://www.7-zip.org/)<br/>
    *(Zum erstellen des selbstextrahierenden Archives)*


## Issues

Sicherstellen das alle behobenen Issues in dem [Kanban Board](https://github.com/ObiWanLansi/SIGENCE-Scenario-Tool/projects/1?fullscreen=true) auf **Done** stehen und in 
der [ReleaseNotes.xml](./SIGENCEScenarioTool.MainApp/ReleaseNotes/ReleaseNotes.xml) eingetragen sind.

*Anmerkung zur ReleaseNotes.xml:*<br/>
Dort muss auch manuell die richtige Versionsnummer und der Title des Release eintragen sein.

```xml
    <Version Version="XX" Title="$(MONTH) $(YEAR) Release">
        <Features>
```


## Dokumentation

Überdenken ob es neue HotKeys, QuickCommands oder Erweiterungen des RFDevice gibt welche würdig sind
in das [CheatSheet](../Documentation/CheatSheet.docx) aufgenommen zu werden.


## Versionsnummern

Update der Versionsnummern, dabei ist darauf zu achten das auch wirklich nur die Versionsnummer in der Datei steht,
keine zusätzlichen Leerzeichen oder Zeilenumbrüche.

- `./VERSION` Diese Versionsnummer wird von den T4 Skripten zum generieren herangezogen, kann auch schon weiter sein als die eigentliche letzte offizielle Version
- `./LATESTVERSION` Diese wird dann benötigt um eine Online Überprüfung der Versionunmmer durchzuführen

*Anmerkung:* Während der Entwicklungsphase kann die VERSION schonmal eine Nummer der LATESTVERSION voraus sein,
spätestens beim erzeugen eines neuen offiziellen Releases sollten dann aber beide gleich sein.


## Generierung T4

In VisualStudio alle T4 Skripte neu ausführen / generieren lassen, dazu muss natürlich die Projektmappe (`SIGENCEScenarioTool.sln`) geladen sein.


## Installer

In der Datei `./SIGENCEScenarioTool.nsi` die Versionsnummer manuell anpassen:

```console
; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "SIGENCE Scenario Tool"
!define PRODUCT_VERSION "20"
```


## Deployment

Aufruf des BuildSkriptes mit `nant all`, hier exemplarische Ausgabe beim erstellen der Version 20:

```console
z:\SIGENCE-Scenario-Tool\Source>nant all
NAnt 0.92 (Build 0.92.4543.0; release; 09.06.2012)
Copyright (C) 2001-2012 Gerry Shaw
http://nant.sourceforge.net

Buildfile: file:///z:/SIGENCE-Scenario-Tool/Source/SIGENCEScenarioTool.build
Target framework: Microsoft .NET Framework 4.0
Target(s) specified: all

doc:

clean:

compile:

deploy:

    [mkdir] Creating directory 'z:\SIGENCE-Scenario-Tool\Executable'.
     [copy] Copying 192 files to 'z:\SIGENCE-Scenario-Tool\Executable'.
   [delete] Deleting 8 files.

sfx:

    [mkdir] Creating directory 'z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.SelfExtractingZip'.
     [exec]
     [exec] 7-Zip [64] 16.04 : Copyright (c) 1999-2016 Igor Pavlov : 2016-10-04
     [exec]
     [exec] Scanning the drive:
     [exec] 6 folders, 184 files, 94964131 bytes (91 MiB)
     [exec]
     [exec] Creating archive: .\SIGENCEScenarioTool.SelfExtractingZip\SIGENCEScenarioTool_SelfExtracting.exe
     [exec]
     [exec] Items to compress: 190
     [exec]
     [exec] Write SFX: C:\Program Files\7-Zip\7zCon.sfx : 172032 bytes (168 KiB)
     [exec]
     [exec] Files read from disk: 184
     [exec] Archive size: 26137143 bytes (25 MiB)
     [exec] Everything is Ok
     [exec] z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.SelfExtractingZip\SIGENCEScenarioTool_SelfExtracting.exe (24,93 Mb) ...

installer:

    [mkdir] Creating directory 'z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.Installer'.
     [exec] Processing config: C:\Program Files (x86)\NSIS\nsisconf.nsh
     [exec] Processing script file: "SIGENCEScenarioTool.nsi" (ACP)
     [exec]
     [exec] Processed 1 file, writing output (x86-ansi):
     [exec]
     [exec] Output: "z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.Installer\SIGENCEScenarioTool_Setup-20.exe"
     [exec] Install: 5 pages (320 bytes), 3 sections (2 required) (6216 bytes), 531 instructions (14868 bytes), 536 strings (15646 bytes), 4 language tables (1256 bytes).
     [exec] Uninstall: 1 page (128 bytes), 1 section (2072 bytes), 46 instructions (1288 bytes), 183 strings (4044 bytes), 4 language tables (904 bytes).
     [exec]
     [exec] Using zlib compression.
     [exec]
     [exec] EXE header size:               50688 / 37888 bytes
     [exec] Install code:                   9724 / 35674 bytes
     [exec] Install data:               34797643 / 95014124 bytes
     [exec] Uninstall code+data:           12333 / 16653 bytes
     [exec] CRC (0x464CC41A):                  4 / 4 bytes
     [exec]
     [exec] Total size:                 34870392 / 95104343 bytes (36.6%)
     [exec] z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.Installer\SIGENCEScenarioTool_Setup-20.exe (33,26 Mb) ...

all:

BUILD SUCCEEDED

Total time: 87.4 seconds.

z:\SIGENCE-Scenario-Tool\Source>
```


## SmokeTest

Wenn erfolgreich gebaut wurde, sollte ein "SmokeTest" durchgeführt werden.
Dazu kann die `../Executable/SIGENCEScenarioTool.exe` gestartet werden.

Minimale Tests und Überprüfungen:
- [ ] Wird in der Title Leiste die richtige Versionsnummer angezeigt ?
- [ ] Läßt sich die Hilfe über F1 öffnen ?
- [ ] Wird in der Hilfe die richtige Versionsnummer mit Datum angezeigt ?
- [ ] Können jweils ein Receiver, Transmitter und Referenzsender angelegt werden ?
- [ ] Können diese in einer Datei gespeichert werden ?
- [ ] Kann diese Datei wieder geladen werden ?


## Commit & Push

Die letzten Anpassungen und Änderungen durch die Vergabe der Versionsnummer sollten natürlich commited und gepusht werden.
Dazu gibt es extra den Issue "Create New Binaries Or Release #51".
Nicht dabei sein sollten jedoch die Buildartifakte (Installer, Zip, Bin, ...).


## Create Release On GitHub

***TODO ENTER TEXT HERE ...***


## Local Clean Up

Mit `nant clean` kann das lokale Repository (Buildartifakte) bereinigt werden.
Dabei sollten folgende Verzeichnisse gelöscht werden:

- `../Executable`
- `./SIGENCEScenarioTool.SelfExtractingZip`
- `./SIGENCEScenarioTool.Installer`
- `./bin`

```console
z:\SIGENCE-Scenario-Tool\Source>nant clean
NAnt 0.92 (Build 0.92.4543.0; release; 09.06.2012)
Copyright (C) 2001-2012 Gerry Shaw
http://nant.sourceforge.net

Buildfile: file:///z:/SIGENCE-Scenario-Tool/Source/SIGENCEScenarioTool.build
Target framework: Microsoft .NET Framework 4.0
Target(s) specified: clean

clean:

   [delete] Deleting directory 'z:\SIGENCE-Scenario-Tool\Source\bin'.
   [delete] Deleting directory 'z:\SIGENCE-Scenario-Tool\Executable'.
   [delete] Deleting directory 'z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.MainApp\obj'.
   [delete] Deleting directory 'z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.Library\obj'.
   [delete] Deleting directory 'z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.UnitTests\obj'.
   [delete] Deleting directory 'z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.Installer'.
   [delete] Deleting directory 'z:\SIGENCE-Scenario-Tool\Source\SIGENCEScenarioTool.SelfExtractingZip'.

BUILD SUCCEEDED

Total time: 0.3 seconds.

z:\SIGENCE-Scenario-Tool\Source>
```
