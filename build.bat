@echo off

:: Change to the directory that this batch file is in
:: NB: it must be invoked with a full path!
for /f %%i in ("%0") do set curpath=%%~dpi
cd /d %curpath%

:: Fetch input parameters
set target=%1
set config=%2

:: Set default target and config if needed
if "%target%"=="" set target=default
if "%config%"=="" set config=release

:: Execute the boo script with input params - accessible with env("x")
resources\phantom\phantom.exe -f:build.boo %target% -a:config=%config%
