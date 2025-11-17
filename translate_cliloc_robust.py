#!/usr/bin/env python3
"""
Robust batch translation of Cliloc.json using Claude Haiku API
Features: Incremental saves, resume capability, retry logic, progress tracking
"""
import json
import re
import os
import time
from typing import Dict, List, Tuple

# Configuration
BATCH_SIZE = 500  # Entries per API call
MODEL = "claude-3-5-haiku-20241022"
DRY_RUN = False
SAVE_INTERVAL = 10  # Save progress every N batches
MAX_RETRIES = 3  # Retry failed API calls
RETRY_DELAY = 5  # Seconds between retries
CHECKPOINT_FILE = 'Files/translation_checkpoint.json'
OUTPUT_FILE = 'Files/Cliloc_translated.json'
OUTPUT_TEMP = 'Files/Cliloc_translated.tmp.json'

# Import Anthropic only if needed
if not DRY_RUN:
    try:
        from anthropic import Anthropic
    except ImportError:
        print("ERROR: anthropic package not installed. Run: pip install anthropic")
        exit(1)

class ClilocTranslator:
    def __init__(self, skiplist_path='translation_skiplist.json', api_key=None):
        """Initialize translator with skiplist and API"""
        # Load skiplist
        with open(skiplist_path, 'r', encoding='utf-8') as f:
            skiplist_data = json.load(f)
            self.skip_terms = set(skiplist_data['terms'])
            self.skip_patterns = skiplist_data['regex_patterns']

        # API client
        self.api_key = api_key or os.environ.get('ANTHROPIC_API_KEY')
        if not DRY_RUN and not self.api_key:
            raise ValueError("ANTHROPIC_API_KEY not found in environment")

        if not DRY_RUN:
            self.client = Anthropic(api_key=self.api_key)

        # Statistics
        self.stats = {
            'total': 0,
            'skipped_empty': 0,
            'skipped_terms': 0,
            'translated': 0,
            'resumed': 0,
            'api_calls': 0,
            'retries': 0,
            'total_cost': 0.0
        }

        # Progress tracking
        self.last_batch_saved = 0

    def should_skip(self, text: str) -> Tuple[bool, str]:
        """Determine if entry should be skipped (not translated)"""
        if not text or len(text.strip()) == 0:
            return True, 'empty'

        for pattern in self.skip_patterns:
            if re.search(pattern, text):
                if re.fullmatch(pattern, text.strip()):
                    return True, 'pattern_only'

        if len(text.strip()) <= 2:
            return True, 'too_short'

        if re.match(r'^[\d\s\-\.%]+$', text):
            return True, 'numeric'

        text_lower = text.lower().strip()
        if text_lower in self.skip_terms:
            return True, 'skip_term'

        words = text.split()
        if len(words) == 1 and text[0].isupper():
            if text_lower in self.skip_terms:
                return True, 'proper_noun_skip'

        return False, ''

    def contains_skip_terms(self, text: str) -> List[str]:
        """Identify which skip terms are in the text"""
        found = []
        text_lower = text.lower()
        for term in self.skip_terms:
            if term in text_lower:
                found.append(term)
        return found

    def create_translation_prompt(self, entries: List[Dict]) -> str:
        """Create optimized prompt for batch translation"""
        entry_lines = []
        for entry in entries:
            skip_terms = self.contains_skip_terms(entry['text'])
            context = f" [KEEP: {', '.join(skip_terms[:3])}]" if skip_terms else ""
            entry_lines.append(f"{entry['id']}: {entry['text']}{context}")

        batch_text = "\n".join(entry_lines)

        prompt = f"""Translate the following Ultima Online game strings from English to Brazilian Portuguese (pt-BR).

CRITICAL RULES:
1. Preserve ALL placeholders EXACTLY: ~1_name~, {{0}}, <BR>, #1234, etc.
2. DO NOT translate game-specific terms like: skill names, spell names, city names, creature names
3. Keep these terms in English: magery, taming, britain, dragon, buff, pvp, mana, etc.
4. Translate UI messages, dialog, descriptions naturally to pt-BR
5. Match the tone: informal dialog stays informal, formal stays formal
6. For items: "a sword" → "uma espada", "the shield" → "o escudo"

EXAMPLES:
- "You cannot use skills while dead." → "Você não pode usar habilidades enquanto morto."
- "~1_name~ has been poisoned!" → "~1_name~ foi envenenado!"
- "Welcome to Britain" → "Bem-vindo a Britain" (city name stays)
- "Magery increased by 0.1" → "Magery aumentou em 0.1" (skill name stays)

Return ONLY the translations in this format:
ID: Translation

INPUT:
{batch_text}

OUTPUT (translations only, no explanations):"""

        return prompt

    def translate_batch(self, entries: List[Dict], retry_count: int = 0) -> Dict[str, str]:
        """Translate a batch with retry logic"""
        if DRY_RUN:
            return {e['id']: f"[SIMULATED] {e['text']}" for e in entries}

        prompt = self.create_translation_prompt(entries)

        try:
            response = self.client.messages.create(
                model=MODEL,
                max_tokens=8000,
                temperature=0.3,
                timeout=120.0,  # 2 minute timeout per request
                messages=[{
                    "role": "user",
                    "content": prompt
                }]
            )

            # Parse response
            translations = {}
            lines = response.content[0].text.strip().split('\n')
            for line in lines:
                if ':' in line:
                    id_part, trans = line.split(':', 1)
                    translations[id_part.strip()] = trans.strip()

            # Track costs
            input_tokens = response.usage.input_tokens
            output_tokens = response.usage.output_tokens
            cost = (input_tokens * 0.25 / 1_000_000) + (output_tokens * 1.25 / 1_000_000)

            self.stats['api_calls'] += 1
            self.stats['total_cost'] += cost

            return translations

        except Exception as e:
            if retry_count < MAX_RETRIES:
                self.stats['retries'] += 1
                wait_time = RETRY_DELAY * (2 ** retry_count)  # Exponential backoff
                print(f"\n  ⚠ Error: {e}")
                print(f"  ↻ Retrying in {wait_time}s (attempt {retry_count + 1}/{MAX_RETRIES})...", end='', flush=True)
                time.sleep(wait_time)
                return self.translate_batch(entries, retry_count + 1)
            else:
                print(f"\n  ✗ FAILED after {MAX_RETRIES} retries: {e}")
                return {}

    def save_progress(self, result: Dict, batch_num: int):
        """Save progress to temp file and checkpoint"""
        # Save to temp file first (atomic write)
        with open(OUTPUT_TEMP, 'w', encoding='utf-8') as f:
            json.dump(result, f, indent=2, ensure_ascii=False)

        # Rename temp to actual (atomic operation)
        os.replace(OUTPUT_TEMP, OUTPUT_FILE)

        # Save checkpoint
        checkpoint = {
            'last_batch': batch_num,
            'stats': self.stats,
            'timestamp': time.time()
        }
        with open(CHECKPOINT_FILE, 'w', encoding='utf-8') as f:
            json.dump(checkpoint, f, indent=2)

        self.last_batch_saved = batch_num
        print(f" [SAVED]", end='')

    def load_checkpoint(self) -> int:
        """Load checkpoint if exists, return last completed batch"""
        if os.path.exists(CHECKPOINT_FILE):
            try:
                with open(CHECKPOINT_FILE, 'r', encoding='utf-8') as f:
                    checkpoint = json.load(f)
                    return checkpoint.get('last_batch', 0)
            except:
                return 0
        return 0

    def load_existing_translations(self, data: Dict) -> Dict:
        """Load existing translations if resuming"""
        if os.path.exists(OUTPUT_FILE):
            try:
                with open(OUTPUT_FILE, 'r', encoding='utf-8') as f:
                    existing = json.load(f)
                    # Count how many have PTB
                    resumed = sum(1 for entry in existing.values() if 'PTB' in entry)
                    if resumed > 0:
                        print(f"\n✓ Resuming: Found {resumed:,} existing translations")
                        self.stats['resumed'] = resumed
                        return existing
            except:
                pass
        return data

    def translate_file(self, input_path: str, output_path: str):
        """Main translation pipeline with robustness features"""
        print(f"Loading {input_path}...")
        with open(input_path, 'r', encoding='utf-8') as f:
            data = json.load(f)

        self.stats['total'] = len(data)
        print(f"Total entries: {self.stats['total']:,}")

        # Check for resume
        last_batch = self.load_checkpoint()
        if last_batch > 0:
            print(f"✓ Resuming from batch {last_batch}")

        # Phase 1: Filter and categorize
        print("\nPhase 1: Filtering entries...")
        to_translate = []
        result = self.load_existing_translations(data)

        for key, value in data.items():
            # Skip if already translated
            if key in result and 'PTB' in result[key]:
                continue

            text = value.get('ENU', '')
            should_skip, reason = self.should_skip(text)

            if should_skip:
                result[key] = value
                if reason == 'empty':
                    self.stats['skipped_empty'] += 1
                else:
                    self.stats['skipped_terms'] += 1
            else:
                to_translate.append({'id': key, 'text': text})
                if key not in result:
                    result[key] = value

        print(f"  ✓ Skipped (empty): {self.stats['skipped_empty']:,}")
        print(f"  ✓ Skipped (terms/patterns): {self.stats['skipped_terms']:,}")
        print(f"  ✓ To translate: {len(to_translate):,}")

        if DRY_RUN:
            print("\nDRY RUN MODE")
            return

        # Phase 2: Batch translation
        print(f"\nPhase 2: Translating in batches of {BATCH_SIZE}...")
        print(f"  (Saving every {SAVE_INTERVAL} batches)")
        total_batches = (len(to_translate) + BATCH_SIZE - 1) // BATCH_SIZE

        for i in range(0, len(to_translate), BATCH_SIZE):
            batch = to_translate[i:i+BATCH_SIZE]
            batch_num = i // BATCH_SIZE + 1

            # Skip if already processed (resume)
            if batch_num <= last_batch:
                print(f"  Batch {batch_num}/{total_batches} [SKIPPED - already done]")
                continue

            print(f"  Batch {batch_num}/{total_batches} ({len(batch)} entries)...", end='', flush=True)

            translations = self.translate_batch(batch)

            # Add PTB to result
            for entry in batch:
                entry_id = entry['id']
                if entry_id in translations:
                    result[entry_id]['PTB'] = translations[entry_id]
                    self.stats['translated'] += 1

            print(f" ✓ Cost: ${self.stats['total_cost']:.4f}", end='')

            # Save progress periodically
            if batch_num % SAVE_INTERVAL == 0 or batch_num == total_batches:
                self.save_progress(result, batch_num)

            print()  # New line

        # Final save
        if self.last_batch_saved < total_batches:
            print(f"\nFinal save...")
            self.save_progress(result, total_batches)

        # Cleanup checkpoint on completion
        if os.path.exists(CHECKPOINT_FILE):
            os.remove(CHECKPOINT_FILE)

        # Final stats
        print("\n" + "="*60)
        print("TRANSLATION COMPLETE")
        print("="*60)
        print(f"Total entries:        {self.stats['total']:,}")
        print(f"Skipped (empty):      {self.stats['skipped_empty']:,}")
        print(f"Skipped (terms):      {self.stats['skipped_terms']:,}")
        print(f"Resumed:              {self.stats['resumed']:,}")
        print(f"Newly translated:     {self.stats['translated']:,}")
        print(f"API calls:            {self.stats['api_calls']:,}")
        print(f"Retries:              {self.stats['retries']:,}")
        print(f"Total cost:           ${self.stats['total_cost']:.2f}")
        print("="*60)

def main():
    translator = ClilocTranslator()
    translator.translate_file(
        input_path='Files/Cliloc.json',
        output_path=OUTPUT_FILE
    )

if __name__ == '__main__':
    main()
