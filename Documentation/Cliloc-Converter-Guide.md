# Cliloc Converter Guide

## Overview

This guide explains how to use the unified Cliloc converter script to convert Ultima Online Cliloc files to JSON format and back. This tool is based on the [uo-cliloc-to-json-converter](https://github.com/felladrin/uo-cliloc-to-json-converter) project.

## What are Cliloc Files?

Cliloc files contain the localized text strings used by the Ultima Online client. These binary files store game text in different languages (e.g., `Cliloc.enu` for English, `Cliloc.deu` for German). Converting them to JSON makes it easy to edit, translate, and manage game text.

## Requirements

- **Python 3** - Required for binary file handling
  - Ubuntu/Debian: `sudo apt-get install python3`
  - Fedora: `sudo dnf install python3`
  - macOS: `brew install python3`
  - Windows: Download from [python.org](https://www.python.org/downloads/)
- **Bash shell** (Linux/macOS) or Git Bash/WSL (Windows)

## Quick Start

### 1. Prepare Your Files

Place your Cliloc files in the same directory where you'll run the converter. For example:
- `Cliloc.enu` (English)
- `Cliloc.deu` (German)
- `Cliloc.fra` (French)
- etc.

**Location on Ultima Adventures server:**
- Default location: `Files/Cliloc.enu`
- You can copy the file to any directory for conversion

### 2. Run the Converter

```bash
./convert_cliloc.sh
```

Or specify a directory:

```bash
./convert_cliloc.sh /path/to/cliloc/files
```

### 3. Choose Conversion Type

The script will display a menu:

```
===============================================================
  UO Cliloc Converter
===============================================================

1. Convert Cliloc to JSON
2. Convert JSON to Cliloc
3. Exit

Choose option (1-3):
```

## Usage Examples

### Example 1: Convert Cliloc to JSON

**Purpose:** Convert binary Cliloc files to editable JSON format

**Steps:**
1. Place your Cliloc files (e.g., `Cliloc.enu`, `Cliloc.deu`) in a directory
2. Run the converter: `./convert_cliloc.sh`
3. Choose option **1** (Convert Cliloc to JSON)
4. The script will:
   - Auto-detect all Cliloc files in the directory
   - Read all entries from each file
   - Create a unified `Cliloc.json` file

**Output:**
- `Cliloc.json` - Contains all entries from all detected Cliloc files, organized by entry number and language

**Example JSON structure:**
```json
{
  "1000000": {
    "ENU": "You see: ",
    "DEU": "Du siehst: "
  },
  "1000001": {
    "ENU": "You are dead.",
    "DEU": "Du bist tot."
  }
}
```

### Example 2: Convert JSON to Cliloc

**Purpose:** Generate binary Cliloc files from edited JSON

**Steps:**
1. Ensure `Cliloc.json` exists in your directory
2. Run the converter: `./convert_cliloc.sh`
3. Choose option **2** (Convert JSON to Cliloc)
4. The script will:
   - Read `Cliloc.json`
   - Generate binary Cliloc files for each language found in the JSON
   - Output files like `Cliloc.enu`, `Cliloc.deu`, etc.

**Output:**
- `Cliloc.enu` - English Cliloc file
- `Cliloc.deu` - German Cliloc file
- etc. (one file per language in the JSON)

**Note:** If a translation is missing for a language, the script will use the English (ENU) version as a fallback.

## Workflow: Editing Cliloc Files

### Step-by-Step Process

1. **Extract Cliloc from UO folder**
   ```bash
   # Copy Cliloc.enu from your UO client folder
   cp /path/to/UO/Cliloc.enu ./Cliloc.enu
   ```

2. **Convert to JSON**
   ```bash
   ./convert_cliloc.sh
   # Choose option 1
   ```

3. **Edit the JSON file**
   ```bash
   # Open Cliloc.json in your favorite text editor
   nano Cliloc.json
   # or
   code Cliloc.json  # VS Code
   ```

4. **Make your changes**
   - Edit text entries directly in the JSON
   - Add new entries with new numbers
   - Modify existing translations

5. **Convert back to Cliloc**
   ```bash
   ./convert_cliloc.sh
   # Choose option 2
   ```

6. **Copy back to UO folder**
   ```bash
   # Copy the generated Cliloc.enu back to your UO folder
   cp ./Cliloc.enu /path/to/UO/Cliloc.enu
   ```

## File Structure

### Cliloc File Format (Binary)

The Cliloc binary format consists of:
- **Header:** 6 bytes `[2, 0, 0, 0, 1, 0]`
- **Entries:** Each entry contains:
  - 4 bytes: Entry number (unsigned integer, little-endian)
  - 1 byte: Flag (always 0)
  - 2 bytes: Text length (unsigned short, little-endian)
  - N bytes: Text content (UTF-8 encoded)

### JSON Format

The JSON format is a simple object where:
- **Keys:** Entry numbers (as strings)
- **Values:** Objects with language codes as keys and text as values

```json
{
  "entry_number": {
    "LANGUAGE_CODE": "translated text"
  }
}
```

## Common Use Cases

### Creating a Custom Translation

1. Start with `Cliloc.enu` (English baseline)
2. Convert to JSON
3. Create a new language entry in JSON (e.g., `"PTB": "Portuguese text"`)
4. Convert back to Cliloc
5. Use the generated `Cliloc.ptb` file

### Modifying Existing Text

1. Convert Cliloc to JSON
2. Find the entry you want to modify (search by number or text)
3. Edit the text in JSON
4. Convert back to Cliloc
5. Replace the original Cliloc file

### Comparing Multiple Languages

1. Place multiple Cliloc files (e.g., `Cliloc.enu`, `Cliloc.deu`, `Cliloc.fra`) in the same directory
2. Convert all to JSON
3. The resulting `Cliloc.json` will contain all languages side-by-side, making comparison easy

## Troubleshooting

### "Python 3 is required but not found!"

**Solution:** Install Python 3 on your system.

- **Linux:** Use your package manager
- **macOS:** `brew install python3`
- **Windows:** Download from python.org or use WSL

### "No Cliloc files found"

**Causes:**
- Cliloc files not in the current directory
- Files named incorrectly (must contain "cliloc" in filename)
- File extensions not recognized (should be like `.enu`, `.deu`, etc.)

**Solution:**
- Check file names: should be `Cliloc.enu`, `Cliloc.deu`, etc.
- Run script from the directory containing the files
- Or specify directory: `./convert_cliloc.sh /path/to/files`

### "Cliloc.json not found"

**Causes:**
- JSON file not created yet (run Cliloc to JSON conversion first)
- JSON file in different directory

**Solution:**
- Run option 1 (Convert Cliloc to JSON) first
- Ensure `Cliloc.json` exists in the working directory

### Conversion Errors

**If conversion fails:**
- Check file permissions (must be readable/writable)
- Ensure sufficient disk space
- Verify Python 3 is working: `python3 --version`
- Check that Cliloc files are not corrupted

## Advanced Usage

### Batch Processing

You can process multiple directories by creating a wrapper script:

```bash
#!/bin/bash
for dir in /path/to/cliloc1 /path/to/cliloc2 /path/to/cliloc3; do
    echo "Processing $dir..."
    ./convert_cliloc.sh "$dir" <<EOF
1
3
EOF
done
```

### Integration with Git

Version control your Cliloc changes:

```bash
# Convert to JSON for easy diffing
./convert_cliloc.sh
git add Cliloc.json
git commit -m "Update Cliloc translations"
```

## File Locations in Ultima Adventures

### Server Files
- **Location:** `Files/Cliloc.enu`
- **Size:** ~2.8 MB (typical)
- **Purpose:** Server-side localization (if needed)

### Client Files
- **Location:** Client installation folder
- **Typical path:** `C:\Program Files\Electronic Arts\Ultima Online Classic\Cliloc.enu`
- **Purpose:** Client-side text display

## Related Resources

- **Original Converter:** [github.com/felladrin/uo-cliloc-to-json-converter](https://github.com/felladrin/uo-cliloc-to-json-converter)
- **Ultima Online Wiki:** [uo.com/wiki](https://uo.com/wiki)
- **Cliloc Format Documentation:** Check UO emulator documentation

## Tips and Best Practices

1. **Always backup** your original Cliloc files before conversion
2. **Test changes** in a development environment first
3. **Keep JSON files** as source of truth for easy editing
4. **Use version control** for JSON files (much easier to diff than binary)
5. **Validate JSON** before converting back (use a JSON validator)
6. **Keep original files** - don't overwrite until you've tested the new version

## Support

If you encounter issues:
1. Check the troubleshooting section above
2. Verify Python 3 is installed correctly
3. Ensure file permissions are correct
4. Check that Cliloc files are not corrupted

---

**Last Updated:** 2025-01-XX
**Script Version:** 1.0

