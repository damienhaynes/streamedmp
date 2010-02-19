@echo off
IF EXIST StreamedMPEditor_UNMERGED.exe del StreamedMPEditor_UNMERGED.exe
ren StreamedMPEditor.exe StreamedMPEditor_UNMERGED.exe
ilmerge /out:StreamedMPEditor.exe StreamedMPEditor_UNMERGED.exe ICSharpCode.SharpZipLib.dll
