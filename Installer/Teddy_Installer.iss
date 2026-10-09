#define MyAppName "Teddy Cosmos' Pro Wheeler 2"
#define MyAppVersion "0.0.9"
#define MyAppPublisher "Unity"
#define MyAppExeName "TeddyProWheeler2.exe"
#define MyBuildFolder "C:\Users\iank1\source\repos\RebuildOfTeddygelion\Builds\TeddyProWheeler2"

[Setup]
AppId={{A7258D52-17B0-44C3-AB00-78E510D73E77}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}

DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}

OutputDir=C:\Users\iank1\source\repos\RebuildOfTeddygelion\Installer
OutputBaseFilename=Teddy_Cosmos_Pro_Wheeler_2_{#MyAppVersion}

Compression=lzma2
SolidCompression=yes

ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

PrivilegesRequired=admin

Uninstallable=yes
UninstallDisplayName={#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}

WizardStyle=modern

; Optional custom installer icon:
SetupIconFile=C:\Users\iank1\source\repos\RebuildOfTeddygelion\Installer\teddo.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; \
    GroupDescription: "Additional shortcuts:"; Flags: unchecked

[Files]
Source: "{#MyBuildFolder}\*"; \
    DestDir: "{app}"; \
    Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; \
    Filename: "{app}\{#MyAppExeName}"; \
    WorkingDir: "{app}"

Name: "{autodesktop}\{#MyAppName}"; \
    Filename: "{app}\{#MyAppExeName}"; \
    WorkingDir: "{app}"; \
    Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; \
    Description: "Launch {#MyAppName}"; \
    WorkingDir: "{app}"; \
    Flags: nowait postinstall skipifsilent