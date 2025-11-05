#!/bin/bash
# ====================================================================
# Convert MUL to UOP for Ultima Online Map Files (v2.0)
# This script converts edited .mul files to .uop format for the game client
# ====================================================================

# ====================================================================
# CONFIGURATION - Edit these paths as needed
# ====================================================================
FILES_DIR="$(cd "$(dirname "$0")" && pwd)"
CONVERTER_EXE="LegacyMULCL-N.exe"
MAP_NUMBER=""

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# ====================================================================
# Functions
# ====================================================================

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

print_info() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

# Function to prompt and validate map number
get_map_number() {
    while true; do
        echo ""
        print_info "Available maps: 0 (Felucca), 1 (Trammel), 2 (Ilshenar), 3 (Ilshenar), 4 (Malas), 5 (Tokuno)"
        echo ""
        read -p "Enter map number (0, 1, 2, 3, 4, or 5): " MAP_NUMBER
        
        # Validate map number
        if [ -z "$MAP_NUMBER" ]; then
            print_error "Map number cannot be empty!"
            continue
        fi
        
        # Check if map number is valid
        if [[ "$MAP_NUMBER" =~ ^[012345]$ ]]; then
            print_info "Selected map: map${MAP_NUMBER}"
            break
        else
            print_error "Invalid map number! Please enter 0, 1, 2, 3, 4, or 5."
            continue
        fi
    done
}

# ====================================================================
# Script Start
# ====================================================================

echo ""
echo "================================================================"
echo "  Ultima Online - MUL to UOP Converter v2.0"
echo "================================================================"
echo ""
print_info "This script will create the .uop file in the Files directory."
print_info "ClassicUO will load it automatically via files_override.txt"
echo ""

# Prompt for map number
get_map_number

# Check if converter exists
if [ ! -f "$FILES_DIR/$CONVERTER_EXE" ]; then
    print_error "Converter not found: $FILES_DIR/$CONVERTER_EXE"
    echo ""
    echo "Please download LegacyMULConverter-N from:"
    echo "https://github.com/cbnolok/LegacyMULConverter-N/releases"
    echo ""
    echo "Extract the files to: $FILES_DIR"
    echo ""
    exit 1
fi

# Check if map files exist
if [ ! -f "$FILES_DIR/map${MAP_NUMBER}.mul" ]; then
    print_error "Map file not found: map${MAP_NUMBER}.mul"
    echo ""
    exit 1
fi

if [ ! -f "$FILES_DIR/statics${MAP_NUMBER}.mul" ]; then
    print_error "Statics file not found: statics${MAP_NUMBER}.mul"
    echo ""
    exit 1
fi

if [ ! -f "$FILES_DIR/staidx${MAP_NUMBER}.mul" ]; then
    print_error "Statics index file not found: staidx${MAP_NUMBER}.mul"
    echo ""
    exit 1
fi

print_info "Found all required map files"
print_info "Map: map${MAP_NUMBER}.mul"
print_info "Statics: statics${MAP_NUMBER}.mul"
print_info "Index: staidx${MAP_NUMBER}.mul"
echo ""

# Remove existing UOP file in Files directory if it exists (converter needs clean directory)
if [ -f "$FILES_DIR/map${MAP_NUMBER}LegacyMUL.uop" ]; then
    print_info "Removing existing UOP file in Files directory..."
    rm -f "$FILES_DIR/map${MAP_NUMBER}LegacyMUL.uop"
fi

# Temporarily move multi files out of the way (converter tries to convert them but we don't need them for maps)
MULTI_MOVED=0
if [ -f "$FILES_DIR/multi.mul" ]; then
    print_info "Temporarily moving multi.mul out of the way (not needed for map conversion)..."
    if mv "$FILES_DIR/multi.mul" "$FILES_DIR/multi.mul.tmp" 2>/dev/null; then
        MULTI_MOVED=1
    fi
fi

MULTI_IDX_MOVED=0
if [ -f "$FILES_DIR/multi.idx" ]; then
    print_info "Temporarily moving multi.idx out of the way..."
    if mv "$FILES_DIR/multi.idx" "$FILES_DIR/multi.idx.tmp" 2>/dev/null; then
        MULTI_IDX_MOVED=1
    fi
fi

# Check if Wine/Mono is needed (for Windows executables)
if command -v wine &> /dev/null; then
    WINE_CMD="wine"
    print_info "Using Wine to run Windows converter"
elif command -v mono &> /dev/null; then
    print_warning "Wine not found, trying Mono..."
    WINE_CMD="mono"
else
    print_error "Neither Wine nor Mono found!"
    echo "Please install Wine to run Windows executables:"
    echo "  Ubuntu/Debian: sudo apt-get install wine"
    echo "  Fedora: sudo dnf install wine"
    echo "  macOS: brew install wine-stable"
    exit 1
fi

# Change to Files directory and run converter
print_info "Converting MUL files to UOP format..."
print_info "Working directory: $FILES_DIR"
print_info "Note: Converter will process all files, but we only need the map file."
print_info "Errors for multi.mul, art.mul, etc. are normal and can be ignored."
echo ""

cd "$FILES_DIR"

# Run the converter
if $WINE_CMD "$FILES_DIR/$CONVERTER_EXE" "$FILES_DIR"; then
    CONVERT_SUCCESS=1
else
    CONVERT_SUCCESS=0
fi

# Restore multi files if we moved them
if [ $MULTI_MOVED -eq 1 ]; then
    if [ -f "$FILES_DIR/multi.mul.tmp" ]; then
        print_info "Restoring multi.mul..."
        mv "$FILES_DIR/multi.mul.tmp" "$FILES_DIR/multi.mul" 2>/dev/null
    fi
fi

if [ $MULTI_IDX_MOVED -eq 1 ]; then
    if [ -f "$FILES_DIR/multi.idx.tmp" ]; then
        print_info "Restoring multi.idx..."
        mv "$FILES_DIR/multi.idx.tmp" "$FILES_DIR/multi.idx" 2>/dev/null
    fi
fi

# Check conversion result - note: converter may show errors for other files (multi, art, etc.)
# but we only care about the map file conversion
if [ $CONVERT_SUCCESS -eq 0 ]; then
    echo ""
    print_warning "Converter reported errors (this may be normal for non-map files)"
    echo ""
fi

# Check if UOP file was created
if [ ! -f "$FILES_DIR/map${MAP_NUMBER}LegacyMUL.uop" ]; then
    echo ""
    print_error "Conversion completed but UOP file not found!"
    echo "Expected: map${MAP_NUMBER}LegacyMUL.uop"
    echo ""
    exit 1
fi

echo ""
print_success "Conversion completed!"
print_info "Created: $FILES_DIR/map${MAP_NUMBER}LegacyMUL.uop"
echo ""

echo "================================================================"
echo "  Conversion Complete!"
echo "================================================================"
echo ""
print_info "The .uop file has been created in the Files directory."
print_info "File location: $FILES_DIR/map${MAP_NUMBER}LegacyMUL.uop"
echo ""
print_info "ClassicUO will automatically load this file via files_override.txt"
print_info "No manual copying needed - just launch ClassicUO!"
echo ""
echo "Next steps:"
echo "1. Launch ClassicUO client"
echo "2. Test your map changes"
echo ""
