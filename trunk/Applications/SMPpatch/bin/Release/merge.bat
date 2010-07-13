@echo off
IF EXIST SMPpatch_UNMERGED.exe del SMPpatch_UNMERGED.exe
ren SMPpatch.exe SMPatch_UNMERGED.exe
ilmerge /out:SMPpatch.exe SMPatch_UNMERGED.exe ICSharpCode.SharpZipLib.dll
