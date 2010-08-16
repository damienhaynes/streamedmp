@echo off
IF EXIST SMPEditor_UNMERGED.exe del SMPEditor_UNMERGED.exe
ren SMPEditor.exe SMPEditor_UNMERGED.exe
ilmerge /out:SMPEditor.exe SMPEditor_UNMERGED.exe StreamedMPEditor.dll SMPCheckSum.dll
