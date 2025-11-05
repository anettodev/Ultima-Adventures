@echo off
REM ====================================================================
REM Convert MUL to UOP for Ultima Online Map Files (v2.0)
REM This script converts edited .mul files to .uop format for the game client
REM ====================================================================

REM Keep window open on error
setlocal enabledelayedexpansion

REM ====================================================================
REM CONFIGURATION - Edit these paths as needed
REM ====================================================================
set "FILES_DIR=%~dp0"
set "CONVERTER_EXE=LegacyMULCL-N.exe"
set "MAP_NUMBER="

REM ====================================================================
REM Script Start
REM ====================================================================

echo.
echo ================================================================
echo   Ultima Online - MUL to UOP Converter v2.0
echo ================================================================
echo.
echo [INFO] This script will create the .uop file in the Files directory.
echo [INFO] ClassicUO will load it automatically via files_override.txt
echo.

REM ====================================================================
REM Prompt for Map Number
REM ====================================================================
:GET_MAP_NUMBER
echo [INFO] Available maps: 0 (Felucca), 1 (Trammel), 2 (Ilshenar), 3 (Ilshenar), 4 (Malas), 5 (Tokuno)
echo.
set /p "MAP_NUMBER=Enter map number (0, 1, 2, 3, 4, or 5): "

REM Validate map number
if "!MAP_NUMBER!"=="" (
    echo [ERROR] Map number cannot be empty!
    echo.
    goto GET_MAP_NUMBER
)

REM Check if map number is valid
if not "!MAP_NUMBER!"=="0" (
    if not "!MAP_NUMBER!"=="1" (
        if not "!MAP_NUMBER!"=="2" (
            if not "!MAP_NUMBER!"=="3" (
                if not "!MAP_NUMBER!"=="4" (
                    if not "!MAP_NUMBER!"=="5" (
                        echo [ERROR] Invalid map number! Please enter 0, 1, 2, 3, 4, or 5.
                        echo.
                        goto GET_MAP_NUMBER
                    )
                )
            )
        )
    )
)

echo [INFO] Selected map: map!MAP_NUMBER!
echo.

REM Check if converter exists
if not exist "%FILES_DIR%%CONVERTER_EXE%" (
    echo [ERROR] Converter not found: %FILES_DIR%%CONVERTER_EXE%
    echo.
    echo Please download LegacyMULConverter-N from:
    echo https://github.com/cbnolok/LegacyMULConverter-N/releases
    echo.
    echo Extract the files to: %FILES_DIR%
    echo.
    pause
    exit /b 1
)

REM Check if map files exist
if not exist "%FILES_DIR%map%MAP_NUMBER%.mul" (
    echo [ERROR] Map file not found: map%MAP_NUMBER%.mul
    echo.
    pause
    exit /b 1
)

if not exist "%FILES_DIR%statics%MAP_NUMBER%.mul" (
    echo [ERROR] Statics file not found: statics%MAP_NUMBER%.mul
    echo.
    pause
    exit /b 1
)

if not exist "%FILES_DIR%staidx%MAP_NUMBER%.mul" (
    echo [ERROR] Statics index file not found: staidx%MAP_NUMBER%.mul
    echo.
    pause
    exit /b 1
)

echo [INFO] Found all required map files
echo [INFO] Map: map%MAP_NUMBER%.mul
echo [INFO] Statics: statics%MAP_NUMBER%.mul
echo [INFO] Index: staidx%MAP_NUMBER%.mul
echo.

REM Remove existing UOP file in Files directory if it exists (converter needs clean directory)
if exist "%FILES_DIR%map%MAP_NUMBER%LegacyMUL.uop" (
    echo [INFO] Removing existing UOP file in Files directory...
    del "%FILES_DIR%map%MAP_NUMBER%LegacyMUL.uop" >nul 2>&1
)

REM Temporarily move multi files out of the way (converter tries to convert them but we don't need them for maps)
set "MULTI_MOVED=0"
if exist "%FILES_DIR%multi.mul" (
    echo [INFO] Temporarily moving multi.mul out of the way (not needed for map conversion)...
    move "%FILES_DIR%multi.mul" "%FILES_DIR%multi.mul.tmp" >nul 2>&1
    if !errorlevel! equ 0 (
        set "MULTI_MOVED=1"
    )
)
set "MULTI_IDX_MOVED=0"
if exist "%FILES_DIR%multi.idx" (
    echo [INFO] Temporarily moving multi.idx out of the way...
    move "%FILES_DIR%multi.idx" "%FILES_DIR%multi.idx.tmp" >nul 2>&1
    if !errorlevel! equ 0 (
        set "MULTI_IDX_MOVED=1"
    )
)

REM Change to Files directory and run converter
echo [INFO] Converting MUL files to UOP format...
echo [INFO] Working directory: %FILES_DIR%
echo [INFO] Note: Converter will process all files, but we only need the map file.
echo [INFO] Errors for multi.mul, art.mul, etc. are normal and can be ignored.
echo.

REM Change to Files directory and run converter
cd /d "!FILES_DIR!"

REM Run converter - it expects the directory path without trailing backslash
REM Use pushd to get clean path, then pass current directory
pushd "!FILES_DIR!" >nul 2>&1
"%CD%\!CONVERTER_EXE%" "%CD%"
set "CONVERT_ERROR=!errorlevel!"
popd >nul 2>&1

REM Restore multi files if we moved them
if !MULTI_MOVED! equ 1 (
    if exist "%FILES_DIR%multi.mul.tmp" (
        echo [INFO] Restoring multi.mul...
        move "%FILES_DIR%multi.mul.tmp" "%FILES_DIR%multi.mul" >nul 2>&1
    )
)
if !MULTI_IDX_MOVED! equ 1 (
    if exist "%FILES_DIR%multi.idx.tmp" (
        echo [INFO] Restoring multi.idx...
        move "%FILES_DIR%multi.idx.tmp" "%FILES_DIR%multi.idx" >nul 2>&1
    )
)

REM Check conversion result - note: converter may show errors for other files (multi, art, etc.)
REM but we only care about the map file conversion
if !CONVERT_ERROR! neq 0 (
    echo.
    echo [WARNING] Converter reported errors (this may be normal for non-map files)
    echo.
)

REM Check if UOP file was created
if not exist "%FILES_DIR%map%MAP_NUMBER%LegacyMUL.uop" (
    echo.
    echo [ERROR] Conversion completed but UOP file not found!
    echo Expected: map%MAP_NUMBER%LegacyMUL.uop
    echo.
    pause
    exit /b 1
)

echo.
echo [SUCCESS] Conversion completed!
echo [INFO] Created: %FILES_DIR%map%MAP_NUMBER%LegacyMUL.uop
echo.

echo ================================================================
echo   Conversion Complete!
echo ================================================================
echo.
echo [INFO] The .uop file has been created in the Files directory.
echo [INFO] File location: %FILES_DIR%map%MAP_NUMBER%LegacyMUL.uop
echo.
echo [INFO] ClassicUO will automatically load this file via files_override.txt
echo [INFO] No manual copying needed - just launch ClassicUO!
echo.
echo Next steps:
echo 1. Launch ClassicUO client
echo 2. Test your map changes
echo.
echo ================================================================
echo   Press any key to exit...
echo ================================================================
pause >nul
