@echo off

if "%programfiles(x86)%XXX"=="XXX" goto 32BIT
    :: 64-bit
    set PROGS=%programfiles(x86)%
    rem pause
    rem echo Current path is %PROGS%	
    goto CONT
:32BIT
    set PROGS=%ProgramFiles%
    rem echo Current path is %PROGS%
    rem pause
:CONT

IF EXIST SMPpatch_UNMERGED.exe del SMPpatch_UNMERGED.exe
ren SMPpatch.exe SMPpatch_UNMERGED.exe
ilmerge /out:SMPpatch.exe SMPpatch_UNMERGED.exe ICSharpCode.SharpZipLib.dll SMPCheckSum.dll /target:dll /targetplatform:"v4,%PROGS%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0" /wildcards