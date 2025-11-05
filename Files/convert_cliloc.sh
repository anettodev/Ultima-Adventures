#!/bin/bash
# ====================================================================
# UO Cliloc Converter - Unified Script
# Converts Cliloc files to JSON and back
# ====================================================================

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
CYAN='\033[0;36m'
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

print_header() {
    echo -e "${CYAN}$1${NC}"
}

# Check if Python 3 is available
check_python() {
    if command -v python3 &> /dev/null; then
        PYTHON_CMD="python3"
        return 0
    elif command -v python &> /dev/null; then
        # Check if it's Python 3
        if python --version 2>&1 | grep -q "Python 3"; then
            PYTHON_CMD="python"
            return 0
        fi
    fi
    print_error "Python 3 is required but not found!"
    echo "Please install Python 3:"
    echo "  Ubuntu/Debian: sudo apt-get install python3"
    echo "  Fedora: sudo dnf install python3"
    echo "  macOS: brew install python3"
    exit 1
}

# Find Cliloc files in directory
find_cliloc_files() {
    local dir="$1"
    local files=()
    
    for file in "$dir"/Cliloc.* "$dir"/cliloc.*; do
        if [ -f "$file" ]; then
            local ext="${file##*.}"
            ext=$(echo "$ext" | tr '[:lower:]' '[:upper:]')
            # Skip JSON files
            if [ "$ext" != "JSON" ]; then
                files+=("$file")
            fi
        fi
    done
    
    printf '%s\n' "${files[@]}"
}

# Convert Cliloc to JSON
convert_cliloc_to_json() {
    local work_dir="$1"
    
    print_header "Converting Cliloc to JSON"
    echo ""
    
    # Find Cliloc files
    local cliloc_files=($(find_cliloc_files "$work_dir"))
    
    if [ ${#cliloc_files[@]} -eq 0 ]; then
        print_error "No Cliloc files found in: $work_dir"
        echo "Expected files like: Cliloc.enu, Cliloc.deu, etc."
        return 1
    fi
    
    print_info "Found ${#cliloc_files[@]} Cliloc file(s):"
    for file in "${cliloc_files[@]}"; do
        echo "  - $(basename "$file")"
    done
    echo ""
    
    local output_file="$work_dir/Cliloc.json"
    
    # Create Python script for conversion
    local python_script=$(cat << 'PYTHON_EOF'
import os
import sys
import json
import struct

def read_cliloc_file(filepath):
    """Read a Cliloc file and return entries as dict {number: text}"""
    entries = {}
    with open(filepath, 'rb') as f:
        # Skip header (6 bytes)
        f.seek(6)
        
        while True:
            # Read entry number (4 bytes, unsigned int, little-endian)
            number_bytes = f.read(4)
            if len(number_bytes) < 4:
                break
            
            # Read flag (1 byte)
            f.read(1)
            
            # Read text length (2 bytes, unsigned short, little-endian)
            length_bytes = f.read(2)
            if len(length_bytes) < 2:
                break
            
            number = struct.unpack('<I', number_bytes)[0]
            length = struct.unpack('<H', length_bytes)[0]
            
            # Read text
            if length > 0:
                text = f.read(length).decode('utf-8', errors='ignore')
            else:
                text = ''
            
            entries[number] = text
    
    return entries

def main():
    cliloc_files = sys.argv[1:-1]
    output_file = sys.argv[-1]
    
    # Collect all languages
    all_languages = []
    for cliloc_file in cliloc_files:
        ext = os.path.splitext(cliloc_file)[1][1:].upper()
        if ext not in all_languages:
            all_languages.append(ext)
    
    # Read all Cliloc files
    output = {}
    for cliloc_file in cliloc_files:
        print(f"Reading {os.path.basename(cliloc_file)}... ", end='', flush=True)
        ext = os.path.splitext(cliloc_file)[1][1:].upper()
        
        entries = read_cliloc_file(cliloc_file)
        
        # Initialize all languages for each entry
        for number, text in entries.items():
            if number not in output:
                output[number] = {}
            for lang in all_languages:
                if lang not in output[number]:
                    output[number][lang] = ''
            output[number][ext] = text
        
        print("OK!")
    
    # Write JSON file
    with open(output_file, 'w', encoding='utf-8') as f:
        json.dump(output, f, ensure_ascii=False, indent=2)
    
    print(f"Done! Cliloc.json has been saved in: {os.path.dirname(output_file)}")
    return 0

if __name__ == '__main__':
    sys.exit(main())
PYTHON_EOF
)
    
    # Build Python command arguments
    local python_args=()
    for file in "${cliloc_files[@]}"; do
        python_args+=("$file")
    done
    python_args+=("$output_file")
    
    # Execute Python script
    if $PYTHON_CMD -c "$python_script" "${python_args[@]}"; then
        print_success "Conversion completed!"
        return 0
    else
        print_error "Conversion failed!"
        return 1
    fi
}

# Convert JSON to Cliloc
convert_json_to_cliloc() {
    local work_dir="$1"
    
    print_header "Converting JSON to Cliloc"
    echo ""
    
    local json_file="$work_dir/Cliloc.json"
    
    if [ ! -f "$json_file" ]; then
        print_error "Cliloc.json not found in: $work_dir"
        return 1
    fi
    
    print_info "Found: Cliloc.json"
    echo ""
    
    # Create Python script for conversion
    local python_script=$(cat << 'PYTHON_EOF'
import os
import sys
import json
import struct

def write_cliloc_file(json_file, output_file, language):
    """Write a Cliloc file from JSON data"""
    with open(json_file, 'r', encoding='utf-8') as f:
        data = json.load(f)
    
    # Determine if we should use ENU as fallback
    use_enu_fallback = False
    if language != 'ENU':
        # Check if ENU exists in any entry
        for entry in data.values():
            if isinstance(entry, dict) and 'ENU' in entry:
                use_enu_fallback = True
                break
    
    with open(output_file, 'wb') as f:
        # Write header (6 bytes)
        f.write(struct.pack('BBBBBB', 2, 0, 0, 0, 1, 0))
        
        # Write entries
        for number in sorted(data.keys()):
            entry = data[number]
            if not isinstance(entry, dict):
                continue
            
            text = entry.get(language, '')
            
            # Use ENU as fallback if available and text is empty
            if not text and use_enu_fallback:
                text = entry.get('ENU', '')
            
            # Write entry number (4 bytes, unsigned int, little-endian)
            f.write(struct.pack('<I', int(number)))
            
            # Write flag (1 byte)
            f.write(struct.pack('B', 0))
            
            # Write text length (2 bytes, unsigned short, little-endian)
            text_bytes = text.encode('utf-8')
            f.write(struct.pack('<H', len(text_bytes)))
            
            # Write text
            if text_bytes:
                f.write(text_bytes)
    
    return True

def main():
    json_file = sys.argv[1]
    output_dir = sys.argv[2]
    
    # Read JSON to determine available languages
    with open(json_file, 'r', encoding='utf-8') as f:
        data = json.load(f)
    
    # Find all languages in JSON
    languages = set()
    for entry in data.values():
        if isinstance(entry, dict):
            languages.update(entry.keys())
    
    if not languages:
        print("ERROR: No languages found in JSON file!")
        return 1
    
    # Create output files for each language
    for lang in sorted(languages):
        output_file = os.path.join(output_dir, f"Cliloc.{lang.lower()}")
        print(f"Writing {os.path.basename(output_file)}... ", end='', flush=True)
        
        if write_cliloc_file(json_file, output_file, lang):
            print("OK!")
        else:
            print("FAILED!")
            return 1
    
    print(f"Done! Cliloc files have been saved in: {output_dir}")
    return 0

if __name__ == '__main__':
    sys.exit(main())
PYTHON_EOF
)
    
    # Execute Python script
    if $PYTHON_CMD -c "$python_script" "$json_file" "$work_dir"; then
        print_success "Conversion completed!"
        return 0
    else
        print_error "Conversion failed!"
        return 1
    fi
}

# Show menu
show_menu() {
    echo ""
    echo "================================================================"
    print_header "  UO Cliloc Converter"
    echo "================================================================"
    echo ""
    echo "1. Convert Cliloc to JSON"
    echo "2. Convert JSON to Cliloc"
    echo "3. Exit"
    echo ""
}

# Main function
main() {
    # Check Python availability
    check_python
    
    # Get working directory (default to current directory)
    if [ -n "$1" ]; then
        WORK_DIR="$1"
    else
        WORK_DIR="$(pwd)"
    fi
    
    # Check if directory exists
    if [ ! -d "$WORK_DIR" ]; then
        print_error "Directory does not exist: $WORK_DIR"
        exit 1
    fi
    
    WORK_DIR="$(cd "$WORK_DIR" && pwd)"
    
    while true; do
        show_menu
        read -p "Choose option (1-3): " choice
        
        case $choice in
            1)
                echo ""
                convert_cliloc_to_json "$WORK_DIR"
                echo ""
                read -p "Press Enter to continue..."
                ;;
            2)
                echo ""
                convert_json_to_cliloc "$WORK_DIR"
                echo ""
                read -p "Press Enter to continue..."
                ;;
            3)
                print_info "Goodbye!"
                exit 0
                ;;
            *)
                print_error "Invalid option. Please choose 1, 2, or 3."
                sleep 1
                ;;
        esac
    done
}

# Run main function
main "$@"

