@echo off
IF EXIST SMPpatch_UNMERGED.exe del SMPpatch_UNMERGED.exe
ren SMPpatch.exe SMPpatch_UNMERGED.exe
ilmerge /out:SMPpatch.exe SMPpatch_UNMERGED.exe ICSharpCode.SharpZipLib.dll SMPCheckSum.dll