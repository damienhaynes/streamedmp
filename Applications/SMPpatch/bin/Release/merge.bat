@echo off
IF EXIST SMPPatch_UNMERGED.exe del SMPPatch_UNMERGED.exe
ren SMPpatch.exe SMPPatch_UNMERGED.exe
ilmerge /out:SMPPatch.exe SMPPatch_UNMERGED.exe ICSharpCode.SharpZipLib.dll
