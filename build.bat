@echo off

:: Change to the directory that this batch file is in
:: NB: it must be invoked with a full path!
:: for /f %%i in ("%0") do set curpath=%%~dpi
:: cd /d %curpath%

:: Fetch input parameters
set target=%1
set build.version=%2
set build.config=release

:: Fallback to default target if none is provided
if "%target%"=="" set target=default

:: Execute the boo script with params - accessible with e.g. env("build.config")
resources\phantom\phantom.exe -f:build.boo %target% -a:build.config=%build.config% -a:build.version=%build.version%
