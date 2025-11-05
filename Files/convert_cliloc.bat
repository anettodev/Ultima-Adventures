@echo off
REM ====================================================================
REM UO Cliloc Converter - Unified Script (Windows Batch)
REM Converts Cliloc files to JSON and back
REM ====================================================================

setlocal enabledelayedexpansion

REM ====================================================================
REM Configuration
REM ====================================================================

REM Get working directory (default to script directory)
if "%~1"=="" (
    set "WORK_DIR=%~dp0"
) else (
    set "WORK_DIR=%~1"
)

REM Remove trailing backslash if present
if "%WORK_DIR:~-1%"=="\" set "WORK_DIR=%WORK_DIR:~0,-1%"

REM Check if directory exists
if not exist "%WORK_DIR%" (
    echo [ERROR] Directory does not exist: %WORK_DIR%
    pause
    exit /b 1
)

REM Create temp directory for Python scripts
set "TEMP_DIR=%TEMP%\cliloc_converter_%RANDOM%"
mkdir "%TEMP_DIR%" >nul 2>&1

REM ====================================================================
REM Check Python Availability
REM ====================================================================

:CHECK_PYTHON
set "PYTHON_CMD="

REM Try python3 first
where python3 >nul 2>&1
if %errorlevel% equ 0 (
    set "PYTHON_CMD=python3"
    goto :PYTHON_FOUND
)

REM Try python (check if version 3)
python --version 2>nul | findstr /i "Python 3" >nul 2>&1
if %errorlevel% equ 0 (
    set "PYTHON_CMD=python"
    goto :PYTHON_FOUND
)

REM Try py launcher (Windows Python Launcher)
where py >nul 2>&1
if %errorlevel% equ 0 (
    REM Check if py launcher can run Python 3
    py -3 --version >nul 2>&1
    if %errorlevel% equ 0 (
        set "PYTHON_CMD=py -3"
        goto :PYTHON_FOUND
    )
)

echo [ERROR] Python 3 is required but not found!
echo.
echo Please install Python 3:
echo   Download from: https://www.python.org/downloads/
echo   Or use: winget install Python.Python.3
echo.
pause
exit /b 1

:PYTHON_FOUND

REM ====================================================================
REM Main Menu Loop
REM ====================================================================

:MENU_LOOP
cls
echo.
echo ================================================================
echo   UO Cliloc Converter
echo ================================================================
echo.
echo Working directory: %WORK_DIR%
echo.
echo 1. Convert Cliloc to JSON
echo 2. Convert JSON to Cliloc
echo 3. Exit
echo.
set /p "CHOICE=Choose option (1-3): "

if "%CHOICE%"=="1" goto :CONVERT_CLILOC_TO_JSON
if "%CHOICE%"=="2" goto :CONVERT_JSON_TO_CLILOC
if "%CHOICE%"=="3" goto :EXIT_SCRIPT
if "%CHOICE%"=="" goto :MENU_LOOP

echo [ERROR] Invalid option. Please choose 1, 2, or 3.
timeout /t 1 >nul
goto :MENU_LOOP

REM ====================================================================
REM Convert Cliloc to JSON
REM ====================================================================

:CONVERT_CLILOC_TO_JSON
cls
echo.
echo ================================================================
echo   Converting Cliloc to JSON
echo ================================================================
echo.

REM Find Cliloc files
set "CLILOC_COUNT=0"
set "CLILOC_FILES="

for %%f in ("%WORK_DIR%\Cliloc.*" "%WORK_DIR%\cliloc.*") do (
    if exist "%%f" (
        set "EXT=%%~xf"
        set "EXT=!EXT:~1!"
        REM Skip JSON files
        if /i not "!EXT!"=="json" (
            set /a CLILOC_COUNT+=1
            if "!CLILOC_FILES!"=="" (
                set "CLILOC_FILES=%%f"
            ) else (
                set "CLILOC_FILES=!CLILOC_FILES! %%f"
            )
        )
    )
)

if %CLILOC_COUNT% equ 0 (
    echo [ERROR] No Cliloc files found in: %WORK_DIR%
    echo Expected files like: Cliloc.enu, Cliloc.deu, etc.
    echo.
    pause
    goto :MENU_LOOP
)

echo [INFO] Found %CLILOC_COUNT% Cliloc file^(s^):
for %%f in (%CLILOC_FILES%) do (
    echo   - %%~nxf
)
echo.

REM Create Python script file
set "PYTHON_SCRIPT=%TEMP_DIR%\convert_to_json.py"
(
echo import os
echo import sys
echo import json
echo import struct
echo.
echo def read_cliloc_file(filepath^):
echo     """Read a Cliloc file and return entries as dict {number: text}"""
echo     entries = {}
echo     with open(filepath, 'rb'^) as f:
echo         # Skip header ^(6 bytes^)
echo         f.seek^(6^)
echo         while True:
echo             # Read entry number ^(4 bytes, unsigned int, little-endian^)
echo             number_bytes = f.read^(4^)
echo             if len^(number_bytes^) ^< 4:
echo                 break
echo             # Read flag ^(1 byte^)
echo             f.read^(1^)
echo             # Read text length ^(2 bytes, unsigned short, little-endian^)
echo             length_bytes = f.read^(2^)
echo             if len^(length_bytes^) ^< 2:
echo                 break
echo             number = struct.unpack^('<I', number_bytes^)[0]
echo             length = struct.unpack^('<H', length_bytes^)[0]
echo             # Read text
echo             if length ^> 0:
echo                 text = f.read^(length^).decode^('utf-8', errors='ignore'^)
echo             else:
echo                 text = ''
echo             entries[number] = text
echo     return entries
echo.
echo def main^(^):
echo     cliloc_files = sys.argv[1:-1]
echo     output_file = sys.argv[-1]
echo     # Collect all languages
echo     all_languages = []
echo     for cliloc_file in cliloc_files:
echo         ext = os.path.splitext^(cliloc_file^)[1][1:].upper^(^)
echo         if ext not in all_languages:
echo             all_languages.append^(ext^)
echo     # Read all Cliloc files
echo     output = {}
echo     for cliloc_file in cliloc_files:
echo         print^(f'Reading {os.path.basename^(cliloc_file^)}... ', end='', flush=True^)
echo         ext = os.path.splitext^(cliloc_file^)[1][1:].upper^(^)
echo         entries = read_cliloc_file^(cliloc_file^)
echo         # Initialize all languages for each entry
echo         for number, text in entries.items^(^):
echo             if number not in output:
echo                 output[number] = {}
echo             for lang in all_languages:
echo                 if lang not in output[number]:
echo                     output[number][lang] = ''
echo             output[number][ext] = text
echo         print^('OK!'^)
echo     # Write JSON file
echo     with open^(output_file, 'w', encoding='utf-8'^) as f:
echo         json.dump^(output, f, ensure_ascii=False, indent=2^)
echo     print^(f'Done! Cliloc.json has been saved in: {os.path.dirname^(output_file^)}'^)
echo     return 0
echo.
echo if __name__ == '__main__':
echo     sys.exit^(main^(^)^)
) > "%PYTHON_SCRIPT%"

REM Build Python command arguments
set "PYTHON_ARGS="
for %%f in (%CLILOC_FILES%) do (
    set "PYTHON_ARGS=!PYTHON_ARGS! "%%f""
)
set "PYTHON_ARGS=!PYTHON_ARGS! "%WORK_DIR%\Cliloc.json""

REM Execute Python script
%PYTHON_CMD% "%PYTHON_SCRIPT%" %PYTHON_ARGS%

if %errorlevel% equ 0 (
    echo.
    echo [SUCCESS] Conversion completed!
) else (
    echo.
    echo [ERROR] Conversion failed!
)

echo.
pause
goto :MENU_LOOP

REM ====================================================================
REM Convert JSON to Cliloc
REM ====================================================================

:CONVERT_JSON_TO_CLILOC
cls
echo.
echo ================================================================
echo   Converting JSON to Cliloc
echo ================================================================
echo.

REM Check if JSON file exists
if not exist "%WORK_DIR%\Cliloc.json" (
    echo [ERROR] Cliloc.json not found in: %WORK_DIR%
    echo.
    pause
    goto :MENU_LOOP
)

echo [INFO] Found: Cliloc.json
echo.

REM Create Python script file
set "PYTHON_SCRIPT=%TEMP_DIR%\convert_to_cliloc.py"
(
echo import os
echo import sys
echo import json
echo import struct
echo.
echo def write_cliloc_file^(json_file, output_file, language^):
echo     """Write a Cliloc file from JSON data"""
echo     with open^(json_file, 'r', encoding='utf-8'^) as f:
echo         data = json.load^(f^)
echo     # Determine if we should use ENU as fallback
echo     use_enu_fallback = False
echo     if language != 'ENU':
echo         # Check if ENU exists in any entry
echo         for entry in data.values^(^):
echo             if isinstance^(entry, dict^) and 'ENU' in entry:
echo                 use_enu_fallback = True
echo                 break
echo     with open^(output_file, 'wb'^) as f:
echo         # Write header ^(6 bytes^)
echo         f.write^(struct.pack^('BBBBBB', 2, 0, 0, 0, 1, 0^)^)
echo         # Write entries
echo         for number in sorted^(data.keys^(^)^):
echo             entry = data[number]
echo             if not isinstance^(entry, dict^):
echo                 continue
echo             text = entry.get^(language, ''^)
echo             # Use ENU as fallback if available and text is empty
echo             if not text and use_enu_fallback:
echo                 text = entry.get^('ENU', ''^)
echo             # Write entry number ^(4 bytes, unsigned int, little-endian^)
echo             f.write^(struct.pack^('<I', int^(number^)^)^)
echo             # Write flag ^(1 byte^)
echo             f.write^(struct.pack^('B', 0^)^)
echo             # Write text length ^(2 bytes, unsigned short, little-endian^)
echo             text_bytes = text.encode^('utf-8'^)
echo             f.write^(struct.pack^('<H', len^(text_bytes^)^)^)
echo             # Write text
echo             if text_bytes:
echo                 f.write^(text_bytes^)
echo     return True
echo.
echo def main^(^):
echo     json_file = sys.argv[1]
echo     output_dir = sys.argv[2]
echo     # Read JSON to determine available languages
echo     with open^(json_file, 'r', encoding='utf-8'^) as f:
echo         data = json.load^(f^)
echo     # Find all languages in JSON
echo     languages = set^(^)
echo     for entry in data.values^(^):
echo         if isinstance^(entry, dict^):
echo             languages.update^(entry.keys^(^)^)
echo     if not languages:
echo         print^('ERROR: No languages found in JSON file!'^)
echo         return 1
echo     # Create output files for each language
echo     for lang in sorted^(languages^):
echo         output_file = os.path.join^(output_dir, f'Cliloc.{lang.lower^(^)}'^)
echo         print^(f'Writing {os.path.basename^(output_file^)}... ', end='', flush=True^)
echo         if write_cliloc_file^(json_file, output_file, lang^):
echo             print^('OK!'^)
echo         else:
echo             print^('FAILED!'^)
echo             return 1
echo     print^(f'Done! Cliloc files have been saved in: {output_dir}'^)
echo     return 0
echo.
echo if __name__ == '__main__':
echo     sys.exit^(main^(^)^)
) > "%PYTHON_SCRIPT%"

REM Execute Python script
%PYTHON_CMD% "%PYTHON_SCRIPT%" "%WORK_DIR%\Cliloc.json" "%WORK_DIR%"

if %errorlevel% equ 0 (
    echo.
    echo [SUCCESS] Conversion completed!
) else (
    echo.
    echo [ERROR] Conversion failed!
)

echo.
pause
goto :MENU_LOOP

REM ====================================================================
REM Exit
REM ====================================================================

:EXIT_SCRIPT
REM Cleanup temp directory
if exist "%TEMP_DIR%" rmdir /s /q "%TEMP_DIR%" >nul 2>&1
echo.
echo [INFO] Goodbye!
echo.
exit /b 0
