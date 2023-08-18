!include "MUI2.nsh"
!include "LogicLib.nsh"
!include "x64.nsh"

Name "GUI-MEU (Gooey Mew)"
ShowInstDetails show
OutFile "GUI-MEU Installer.exe"
Unicode True
InstallDir "$PROGRAMFILES64\Neo Yuki Aylor\GUI-MEU"
RequestExecutionLevel admin

!define APPNAME "GUI-MEU (Gooey Mew)"
!define COMPANYNAME "Neo Yuki Aylor"
!define DESCRIPTION "A simple application to make uninstalling Microsoft Edge a simple and painless task."
!define VERSIONMAJOR 1
!define VERSIONMINOR 1
!define VERSIONBUILD 0
!define INSTALLSIZE 292

!define MUI_ABORTWARNING
!define MUI_ICON "Assets\GUI-MEU.ico"

!define MUI_WELCOMEPAGE_TITLE "Welcome to the GUI-MEU installer"
!define MUI_WELCOMEPAGE_TEXT "GUI-MEU, pronounced gooey mew, is a simple tool which allows for uninstalling Microsoft Edge."
!define MUI_WELCOMEFINISHPAGE_BITMAP "Assets\Installer Banner.bmp"
!define MUI_FINISHPAGE_NOAUTOCLOSE

!define MUI_UNWELCOMEPAGE_TITLE "Welcome to the GUI-MEU uninstaller"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "Assets\Uninstaller Banner.bmp"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "Assets\License.txt"
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "English"

Section "Install"

    SetOutPath "$INSTDIR"
    file "Assets\GUI-MEU.exe"  
    file "Assets\GUI-MEU.ico"

    Exec 'SCHTASKS.exe /Create /SC ONLOGON /TN Neo\GUI-MEU /TR "$\'C:\Program Files\Neo Yuki Aylor\GUI-MEU\GUI-MEU.exe$\'" /RL HIGHEST /IT /F'

    SetRegView 32
    
    ${If} ${RunningX64}
        SetRegView 64
    ${EndIf}
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "DisplayName" "${APPNAME}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "UninstallString" "$\"$INSTDIR\Uninstall.exe$\""
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "QuietUninstallString" "$\"$INSTDIR\Uninstall.exe$\" /S"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "InstallLocation" "$\"$INSTDIR$\""
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "DisplayIcon" "$\"$INSTDIR\GUI-MEU.ico$\""
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "Publisher" "${COMPANYNAME}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "DisplayVersion" "${VERSIONMAJOR}.${VERSIONMINOR}.${VERSIONBUILD}"
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "VersionMajor" ${VERSIONMAJOR}
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "VersionMinor" ${VERSIONMINOR}
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "NoModify" 1
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "NoRepair" 1
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU" "EstimatedSize" ${INSTALLSIZE}

    WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd

Section "Uninstall"

    Exec 'SCHTASKS.exe /Delete /TN Neo\GUI-MEU /F'
    Exec 'taskkill /T /F /IM GUI-MEU.exe'

    Delete "$INSTDIR\GUI-MEU.exe"
    Delete "$INSTDIR\GUI-MEU.ico"
    Delete "$INSTDIR\Uninstall.exe"
    RMDir "$INSTDIR"

    SetRegView 32
    
    ${If} ${RunningX64}
        SetRegView 64
    ${EndIf}
    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GUI-MEU"

SectionEnd