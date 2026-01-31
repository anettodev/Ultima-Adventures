# [Content Name] - Content Test Plan

**Date:** YYYY-MM-DD
**Tester:** [Your Name]
**Type:** [ ] Creature / [ ] Item / [ ] Spell
**Location:** `test-scripts/ExtraContent/[Type]/[Name]/`

---

## Content Overview

**Name:** [Content name]

**Type:** [Creature/Item/Spell/etc.]

**Quantity:** [How many variants]

**Original Location:** `Scripts/[Mobiles/Items]/[Category]/`

---

## Pre-Test Research

### Spawn References

```bash
# Check if this spawns anywhere
grep -r "NameOfContent" Data/Spawns/ Data/Decoration/ --include="*.xml"

# Results:
# [paste results or "Not found in spawns"]
```

### Quest References

```bash
# Check if used in quests
grep -r "NameOfContent" Scripts/Engines/Quests/ --include="*.cs"

# Results:
# [paste results or "Not found in quests"]
```

### Script References

```bash
# Check if referenced in code
grep -r "NameOfContent" Scripts/ --include="*.cs" | grep -v "test-scripts"

# Results:
# [paste results or "Not referenced"]
```

---

## Evaluation Criteria

### For Creatures:

- [ ] **Spawns in world** - Found in spawn data
- [ ] **Quest critical** - Required for quests
- [ ] **Unique abilities** - Has special features
- [ ] **Player favorites** - Players like it
- [ ] **Part of story** - Lore/story element

**Result:** [X/5 criteria met]

### For Items:

- [ ] **Craftable** - Can be crafted
- [ ] **Drops from mobs** - Part of loot tables
- [ ] **Quest reward** - Quest item
- [ ] **Unique properties** - Special features
- [ ] **Player favorites** - Popular with players

**Result:** [X/5 criteria met]

### For Spells:

- [ ] **Learnable** - Can be learned in-game
- [ ] **Balanced** - Not overpowered/useless
- [ ] **Used by NPCs** - NPCs cast it
- [ ] **Player accessible** - Players can use it
- [ ] **Unique effect** - Not duplicate of existing

**Result:** [X/5 criteria met]

---

## Variant Analysis

**If this is one of many variants:**

**Total Variants:** [number]

**Differences:**
- Variant 1: [what makes it unique]
- Variant 2: [what makes it unique]
- This one: [what makes THIS one unique]

**Duplicate Check:**
- [ ] Exact duplicate of [name]
- [ ] Similar to [name] with minor differences
- [ ] Unique variant worth keeping

---

## Test Plan

### 1. Compilation Test

**Enable in Scripts.csproj:**
```xml
<Compile Include="test-scripts/**/*.cs" />
```

**Build and verify:**
```bash
dotnet build Scripts/Scripts.csproj
```

**Result:**
- [ ] Compiles successfully
- [ ] Errors: [list]

---

### 2. In-Game Spawn Test

**Spawn the content:**
```
[admin command to spawn]
```

**Verify:**
- [ ] Spawns correctly
- [ ] Appears as expected
- [ ] No errors in console

---

### 3. Functionality Test

**For Creatures:**
- [ ] AI works correctly
- [ ] Attacks/abilities function
- [ ] Loot drops as expected
- [ ] No console errors

**For Items:**
- [ ] Can be equipped/used
- [ ] Properties work correctly
- [ ] Graphics display properly
- [ ] No console errors

**For Spells:**
- [ ] Can be cast
- [ ] Effect works correctly
- [ ] Mana cost appropriate
- [ ] No console errors

---

### 4. Uniqueness Check

**Is this content unique and needed?**

**Compared to similar content:**
- Similar content: [list similar items/creatures]
- Unique features: [what makes this different]
- Worth keeping: ✅ Yes / ❌ No - [explain]

---

## Test Results

### Compilation: ✅/❌
[Details]

### Spawn Test: ✅/❌
[Details]

### Functionality: ✅/❌
[Details]

### Uniqueness: ✅/❌
[Details]

---

## Decision

**Decision:** [ ] Keep / [ ] Remove / [ ] Defer

**Rationale:**
[Explain decision]

**If Keep:**
- Destination: `Scripts/[Mobiles/Items]/[Category]/`
- Why: [explain value]

**If Remove:**
- Reason: [why removing]
- Alternative exists: [name of similar content to use instead]

**If Defer:**
- Reason: [why waiting]
- Need more info about: [what]

---

## Action Items

- [ ] Update DECISIONS.md
- [ ] Update TESTING-LOG.md
- [ ] Move files if keeping
- [ ] Delete files if removing
- [ ] Update spawn data if needed
- [ ] Disable test-scripts in .csproj
- [ ] Commit changes

---

## Notes

[Any additional observations]
