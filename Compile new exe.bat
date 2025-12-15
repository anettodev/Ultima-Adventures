@echo off
REM
REM -- RunUO compile script for .NET 4.0 --
REM
REM The full .NET framework needs to be installed for this script.
REM i.e. not the "Client Profile", as it is missing several required DLLs.
REM
set targetfile=WindowsServer.exe
set scriptsdll=Scripts\bin\Release\Scripts.dll

REM Step 1: Build Scripts.dll first
echo Building Scripts.dll...
dotnet build Scripts\Scripts.csproj -c Release
if errorlevel 1 (
	echo.
	echo Failed to build Scripts.dll!
	echo.
	goto end
)
echo Scripts.dll built successfully.
echo.

REM Check if Scripts.dll exists
if not exist "%scriptsdll%" (
	echo Error: Scripts.dll not found at %scriptsdll%
	echo.
	goto end
)

REM Step 2: Delete existing WindowsServer.exe if it exists
if exist "%targetfile%" (
	echo Deleting existing %targetfile%...
	del "%targetfile%" 1>NUL 2>NUL
	
	if exist "%targetfile%" (
		echo Failed!
		echo.
		echo Is "%targetfile%" in use?
		echo.
		goto end
	) else (
		echo Success.
		echo.
	)
)

REM Step 3: Compile Server.exe using MSBuild/dotnet build
echo Compiling %targetfile%...
dotnet build Server\ServerExe.csproj -c Release /p:OutputPath=..\

if errorlevel 1 (
	echo.
	echo Compilation failed!
	echo.
	goto end
)

echo.
echo Build completed successfully!
echo.

:end
pause
