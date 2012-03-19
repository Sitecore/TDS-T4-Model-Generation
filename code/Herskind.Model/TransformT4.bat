@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

:: set the working dir (default to current dir)
set wdir=%cd%
if not (%1)==() set wdir=%1

:: set the file extension (default to vb)
set extension=
if not (%2)==() set extension=%2

echo executing transform_all from %wdir%
:: create a list of all the T4 templates in the working dir
dir %wdir%\*.tt /b /s > t4list.txt

echo the following T4 templates will be transformed:
type t4list.txt

:: transform all the templates
for /f %%d in (t4list.txt) do (
set file_name=%%d
set file_name=!file_name:~0,-3!.%extension%
echo:  \--^> !file_name!   
"%ProgramFiles%\Common Files\Microsoft Shared\TextTemplating\10.0\texttransform.exe" -out !file_name! %%d
)

echo transformation complete
REM EXIT 0