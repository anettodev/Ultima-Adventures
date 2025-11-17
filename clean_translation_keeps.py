#!/usr/bin/env python3
"""
Clean up [KEEP: ...] annotations from PTB translations in Cliloc_translated.json
These annotations were used as context during translation but should not be in the final output.
"""
import json
import re
import shutil
from typing import Dict

# Configuration
INPUT_FILE = 'Files/Cliloc_translated.json'
OUTPUT_FILE = 'Files/Cliloc_translated.json'
BACKUP_FILE = 'Files/Cliloc_translated.backup.json'

# Pattern to match [KEEP: ...] at the end of translations
KEEP_PATTERN = r'\s*\[KEEP:[^\]]*\]\s*$'

def clean_translations(data: Dict) -> tuple[Dict, int]:
    """
    Remove [KEEP: ...] annotations from PTB fields
    Returns: (cleaned_data, count_cleaned)
    """
    cleaned_count = 0

    for key, value in data.items():
        if 'PTB' in value:
            original = value['PTB']
            # Remove [KEEP: ...] pattern
            cleaned = re.sub(KEEP_PATTERN, '', original)

            # If something was removed, update and count
            if cleaned != original:
                value['PTB'] = cleaned
                cleaned_count += 1

    return data, cleaned_count

def main():
    print("=" * 60)
    print("CLEANING [KEEP: ...] ANNOTATIONS FROM TRANSLATIONS")
    print("=" * 60)

    # Load the translated file
    print(f"\nLoading {INPUT_FILE}...")
    with open(INPUT_FILE, 'r', encoding='utf-8') as f:
        data = json.load(f)

    total_entries = len(data)
    ptb_entries = sum(1 for v in data.values() if 'PTB' in v)
    print(f"  Total entries: {total_entries:,}")
    print(f"  PTB translations: {ptb_entries:,}")

    # Create backup
    print(f"\nCreating backup: {BACKUP_FILE}")
    shutil.copy2(INPUT_FILE, BACKUP_FILE)

    # Clean the data
    print("\nScanning for [KEEP: ...] annotations...")
    cleaned_data, cleaned_count = clean_translations(data)

    if cleaned_count == 0:
        print("  ✓ No [KEEP: ...] annotations found - file is already clean!")
    else:
        print(f"  ✓ Found and cleaned {cleaned_count:,} entries")

        # Show some examples
        print("\nExamples of cleaned entries (first 5):")
        example_count = 0
        for key, value in data.items():
            if 'PTB' in value:
                # Check if this entry was in the original with KEEP
                with open(BACKUP_FILE, 'r', encoding='utf-8') as f:
                    backup_data = json.load(f)
                    if 'PTB' in backup_data[key]:
                        original = backup_data[key]['PTB']
                        if re.search(KEEP_PATTERN, original):
                            cleaned = value['PTB']
                            print(f"\n  {key}:")
                            print(f"    Before: {original}")
                            print(f"    After:  {cleaned}")
                            example_count += 1
                            if example_count >= 5:
                                break

        # Save cleaned file
        print(f"\nSaving cleaned file to {OUTPUT_FILE}...")
        with open(OUTPUT_FILE, 'w', encoding='utf-8') as f:
            json.dump(cleaned_data, f, indent=2, ensure_ascii=False)

        print("\n✓ File cleaned successfully!")

    # Final summary
    print("\n" + "=" * 60)
    print("SUMMARY")
    print("=" * 60)
    print(f"Total entries processed: {total_entries:,}")
    print(f"Entries cleaned:         {cleaned_count:,}")
    print(f"Backup saved to:         {BACKUP_FILE}")
    print("=" * 60)

if __name__ == '__main__':
    main()
