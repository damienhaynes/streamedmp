@echo off
IF EXIST SMPpatch_UNMERGED.exe del SMPpatch_UNMERGED.exe
ren SMPpatch.exe SMPpatch_UNMERGED.exe
rem ilmerge /out:SMPpatch.exe SMPpatch_UNMERGED.exe ICSharpCode.SharpZipLib.dll SMPCheckSum.dll
ilmerge /out:SMPpatch.exe SMPpatch_UNMERGED.exe ICSharpCode.SharpZipLib.dll SMPCheckSum.dll /target:exe /targetplatform:v4,C:\Windows\Microsoft.NET\Framework\v4.0.30319 /wildcards