@echo off
IF EXIST SMPCheckSum_Test_UNMERGED.exe del SMPCheckSum_Test_UNMERGED.exe
ren SMPCheckSum_Test.exe SMPCheckSum_Test_UNMERGED.exe
ilmerge /out:SMPCheckSum_Test.exe SMPCheckSum_Test_UNMERGED.exe SMPCheckSum.dll
