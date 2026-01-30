# Test-Scripts Migration Strategy

**Purpose:** Gradually evaluate experimental and non-vital content before moving to production

---

## Overview

Instead of immediately deleting 7,000+ files, use a **test-scripts** subfolder to:
1. **Isolate experimental content** for testing
2. **Gradually evaluate** what's worth keeping
3. **Safely remove** what's not needed
4. **Keep production lean** with only tested, essential code

---

## Strategy Summary

```
Current Structure          Test-Scripts Isolation       Production-Ready
┌────────────────┐        ┌────────────────┐          ┌───────────────┐
│ Scripts/       │        │ Scripts/       │          │ Scripts/      │
│ ├─ 8,269 files│   →    │ ├─ 800 core    │    →     │ ├─ 800 files  │
│ └─ Mixed       │        │ └─ test-       │          │ └─ Tested ✅  │
│    quality     │        │    scripts/    │          │               │
│                │        │    ├─ 6,469    │          └───────────────┘
│                │        │    │   files    │
│                │        │    └─ Testing  │
└────────────────┘        └────────────────┘
```

---

## Folder Structure

### Proposed Structure

```
Scripts/
├── Configuration/          # [Core] Server config (7 files)
├── Core/                   # [Core] Essential systems (~100 files)
├── Mobiles/                # [Core] Creatures + AI (~50 files)
│   ├── _Base/              # Base classes
│   ├── AI/                 # AI systems
│   ├── NPCs/               # Essential NPCs
│   └── Creatures/          # Basic creatures
├── Items/                  # [Core] Essential items (~100 files)
│   ├── _Base/              # Base classes
│   ├── Equipment/          # Weapons, armor
│   ├── Consumables/        # Potions, food
│   └── Resources/          # Crafting materials
├── Systems/                # [Core] Game systems (~250 files)
│   ├── Magic/              # Spell systems
│   ├── Skills/             # Skill systems
│   ├── Combat/             # Combat mechanics
│   └── Crafting/           # Crafting systems
├── Custom/                 # [Production] Verified custom content
│   ├── Soulbound/          # Phylactery system (tested)
│   ├── RedBlueAI/          # Faction AI (tested)
│   └── Midlands/           # Custom race (tested)
│
└── test-scripts/           # [Testing] Experimental & unverified
    ├── Experimental/       # 1,539 files - Legacy content
    ├── OptionalSystems/    # Optional game systems
    │   ├── Casino/         # Casino games
    │   ├── Holiday/        # Holiday events
    │   ├── JediSystem/     # Force abilities
    │   └── SquireSystem/   # Squire followers
    ├── ExtraContent/       # Additional content
    │   ├── ExtraCreatures/ # 1,000+ creature variants
    │   ├── ExtraItems/     # 2,000+ item variants
    │   └── ExtraSpells/    # Custom magic schools
    └── Evaluation/         # Content being evaluated
        └── README.md       # Evaluation criteria
```

---

## Migration Phases

### Phase 1: Initial Separation (Week 1)

**Goal:** Move non-essential content to test-scripts

**Actions:**
1. Create `Scripts/test-scripts/` folder structure
2. Move clearly experimental content:
   - `Experimental/` → `test-scripts/Experimental/`
   - Large optional systems → `test-scripts/OptionalSystems/`
3. Keep server running with core systems only
4. Verify server boots and works

**Files moved:** ~2,000-3,000
**Server downtime:** None (do in dev environment)

### Phase 2: System Evaluation (Weeks 2-4)

**Goal:** Test individual systems from test-scripts

**Process for each system:**

1. **Identify system** in test-scripts
2. **Research dependencies**
   ```bash
   # Find what references this system
   grep -r "NameOfSystem" Scripts/Core/
   grep -r "NameOfSystem" Scripts/Mobiles/
   ```

3. **Create test plan**
   - Does it compile when enabled?
   - Does server start?
   - Does it work as expected?
   - Is it actually used?

4. **Make decision**
   - ✅ **Keep** → Move to production folder
   - ⚠️ **Maybe** → Keep in test-scripts for now
   - ❌ **Remove** → Delete from test-scripts

5. **Document decision** in `test-scripts/Evaluation/DECISIONS.md`

### Phase 3: Content Trimming (Weeks 5-8)

**Goal:** Reduce redundant content (creatures, items, etc.)

**Strategy:**
- Keep 10-20 creatures per category (not 100+)
- Keep basic item types (not every variant)
- Keep core magic schools (Magery, Necromancy, Chivalry)
- Remove duplicates and legacy versions

**Example: Creatures/Dragons/**
```bash
# Current: 50+ dragon variants
# Keep: 10 essential dragons
# Move to test-scripts: 40 variants

# Test each before deleting:
# 1. Is it referenced in spawns?
# 2. Is it quest-critical?
# 3. Is it unique/special?
# 4. If no to all → Remove
```

### Phase 4: Final Cleanup (Week 9-10)

**Goal:** Delete confirmed unnecessary content

**Actions:**
1. Review `test-scripts/Evaluation/DECISIONS.md`
2. Delete all "Remove" marked content
3. Move all "Keep" content to production
4. Archive test-scripts folder for reference

**Result:**
- Production: ~800 files (tested, essential)
- Archive: `test-scripts-archive.tar.gz` (for historical reference)
- Deleted: ~6,500 files (confirmed unnecessary)

---

## Evaluation Criteria

### For Systems (Engines/)

**Keep if:**
- ✅ Referenced by core functionality
- ✅ Players actively use it
- ✅ Part of server identity (custom systems)
- ✅ Required for world spawns/quests

**Test-scripts if:**
- ⚠️ Rarely used but functional
- ⚠️ Experimental feature
- ⚠️ Optional enhancement

**Remove if:**
- ❌ Never used by players
- ❌ Broken/non-functional
- ❌ Superseded by better implementation
- ❌ Test/debug code

### For Content (Mobiles/Items)

**Keep if:**
- ✅ Essential gameplay (basic creatures, weapons, armor)
- ✅ Quest-critical
- ✅ Spawn-critical
- ✅ Player favorites

**Test-scripts if:**
- ⚠️ Variant of existing content
- ⚠️ Seasonal/event content
- ⚠️ Experimental design

**Remove if:**
- ❌ Duplicate of existing
- ❌ Never spawns
- ❌ Legacy/obsolete
- ❌ Broken implementation

---

## test-scripts/ Implementation

### Structure

```
Scripts/test-scripts/
├── README.md                   # Overview and instructions
├── Evaluation/
│   ├── README.md               # Evaluation process
│   ├── DECISIONS.md            # Decision log
│   ├── TESTING-LOG.md          # Test results
│   └── templates/              # Test plan templates
│       ├── system-test.md      # System testing template
│       └── content-test.md     # Content testing template
│
├── OptionalSystems/            # Complete optional systems
│   ├── Casino/
│   │   ├── README.md           # What it does, why optional
│   │   └── [casino files]
│   ├── JediSystem/
│   │   ├── README.md
│   │   └── [jedi files]
│   └── [other systems]
│
├── ExtraContent/               # Excess content variants
│   ├── Creatures/
│   │   ├── Dragons/            # 40+ dragon variants
│   │   ├── Demons/             # 30+ demon variants
│   │   └── [other categories]
│   ├── Items/
│   │   ├── Weapons/            # 100+ weapon variants
│   │   └── Armor/              # 100+ armor variants
│   └── Spells/
│       ├── DeathKnight/
│       ├── HolyMan/
│       └── Syth/
│
└── Experimental/               # Legacy experimental folder
    └── [1,539 experimental files]
```

### test-scripts/README.md

```markdown
# Test Scripts

This folder contains **experimental and optional content** that is NOT loaded
by the production server.

## Purpose

- **Isolation:** Keep production server lean and tested
- **Evaluation:** Test systems before production deployment
- **Archive:** Preserve content for future consideration

## Status

Content here is:
- ⚠️ **NOT compiled** by default
- ⚠️ **NOT tested** for production
- ⚠️ **MAY be broken** or incomplete

## How to Test

1. Copy system to production location
2. Test compilation
3. Test functionality
4. Document results in `Evaluation/TESTING-LOG.md`
5. Make decision (Keep, Remove, or Back to test-scripts)

## Enabling test-scripts (Development Only)

To compile test-scripts for testing:

1. Edit `Scripts.csproj`
2. Uncomment test-scripts include:
   ```xml
   <!-- <Compile Include="test-scripts/**/*.cs" /> -->
   ```
3. Build and test
4. Comment out before production deployment

**WARNING:** Never deploy with test-scripts enabled in production!
```

### Evaluation/DECISIONS.md Template

```markdown
# Test-Scripts Evaluation Decisions

Log of decisions made during content evaluation.

## Format

Date | System/Content | Decision | Rationale | Tested By
-----|----------------|----------|-----------|----------
2026-01-30 | Casino/ | Remove | Never used, 1.7MB | Dev Team
2026-01-30 | Soulbound/ | Keep → Custom/ | Core feature | Dev Team
2026-01-31 | Dragon variants | Trim to 10 | Too many variants | Dev Team

## Decisions

### Keep → Move to Production
- **Soulbound/** - Core custom feature, move to `Scripts/Custom/Soulbound/`
- **RedBlueAI/** - Faction system, move to `Scripts/Custom/RedBlueAI/`

### Remove - Confirmed Unnecessary
- **Casino/** - 1.7MB, never used by players
- **Holiday/** - Outdated, not maintained
- **JediSystem/** - Experimental, incomplete

### Test-Scripts - Keep for Now
- **SquireSystem/** - Functional but rarely used
- **Extra dragon variants** - May need some for events

### Pending Evaluation
- **DeathKnight spells** - Need to check player usage
- **Champ spawn variants** - Need to verify spawn references
```

---

## Compilation Control

### Exclude test-scripts from Build

**Method 1: .csproj Exclusion (Recommended)**

Edit `Scripts/Scripts.csproj`:

```xml
<ItemGroup>
  <!-- Exclude test-scripts from compilation -->
  <Compile Remove="test-scripts/**/*.cs" />

  <!-- Optional: Include for testing (commented by default) -->
  <!-- <Compile Include="test-scripts/**/*.cs" /> -->
</ItemGroup>
```

**Method 2: Conditional Compilation**

```xml
<ItemGroup>
  <!-- Only include test-scripts in Debug builds -->
  <Compile Remove="test-scripts/**/*.cs" />
  <Compile Include="test-scripts/**/*.cs" Condition="'$(Configuration)' == 'Debug'" />
</ItemGroup>
```

**Method 3: Separate Project (Advanced)**

Create `Scripts-Test.csproj`:
- References main `Scripts.csproj`
- Includes test-scripts
- Only used in development environment

---

## Testing Workflow

### 1. Select System to Test

```bash
# List systems in test-scripts
ls -1 Scripts/test-scripts/OptionalSystems/

# Example: Testing Casino system
SYSTEM="Casino"
```

### 2. Enable in Build

```bash
# Temporarily enable in Scripts.csproj
# Uncomment: <Compile Include="test-scripts/**/*.cs" />
```

### 3. Compile and Test

```bash
# Build
./LinuxServer.exe  # or WindowsServer.exe

# Check for errors
# If compilation fails, system needs fixes or dependencies

# If successful, test functionality:
# - Does it load?
# - Can you access casino games?
# - Do they work correctly?
# - Are there any errors in logs?
```

### 4. Document Results

Edit `test-scripts/Evaluation/TESTING-LOG.md`:

```markdown
## Casino System Test - 2026-01-30

**Tester:** Dev Team
**Version:** dev-2026.01.30-a1b2c3d

### Compilation
- ✅ Compiles successfully
- ⚠️ 2 warnings about obsolete methods

### Functionality
- ✅ Casino NPC spawns
- ✅ Can enter casino
- ❌ Slot machine crashes server
- ❌ Poker game has infinite loop bug

### Dependencies
- Requires: `Scripts/Core/Gambling/`
- References: `Scripts/Items/Currency/`

### Decision
❌ **Remove** - Broken functionality, not worth fixing
- Alternative: Use simpler gambling system
```

### 5. Make Decision

Based on testing:

**Keep → Production:**
```bash
mkdir -p Scripts/Systems/Casino
mv test-scripts/OptionalSystems/Casino/* Scripts/Systems/Casino/
```

**Remove:**
```bash
rm -rf test-scripts/OptionalSystems/Casino
```

**Keep in test-scripts:**
```bash
# Leave as-is, document decision
```

---

## Benefits of test-scripts Approach

### ✅ Safety
- No immediate deletion of potentially useful content
- Can test before deciding
- Easy to revert decisions

### ✅ Gradual Migration
- Work in phases (2-3 months vs 1 week)
- Test thoroughly
- No rush to delete

### ✅ Knowledge Building
- Learn what each system does
- Understand dependencies
- Document server architecture

### ✅ Production Stays Clean
- Only tested code in production
- No experimental content
- Easier maintenance

### ✅ Reversible
- Deleted content preserved in test-scripts
- Can re-enable if needed
- Historical reference

---

## Migration Timeline

### Month 1: Setup & Initial Move
- **Week 1:** Create test-scripts structure
- **Week 2:** Move Experimental/ (1,539 files)
- **Week 3:** Move large optional systems (Casino, Holiday, etc.)
- **Week 4:** Verify server stability

**Files in production:** ~6,500 (down from 8,269)

### Month 2: System Evaluation
- **Week 5:** Evaluate magic schools (DeathKnight, HolyMan, etc.)
- **Week 6:** Evaluate optional systems (SquireSystem, JediSystem, etc.)
- **Week 7:** Evaluate custom AI systems
- **Week 8:** Evaluate quest systems

**Decision:** Keep ~10-20 systems, remove ~30-40 systems

### Month 3: Content Trimming
- **Week 9:** Trim creature variants (1,495 → ~200 files)
- **Week 10:** Trim item variants (2,845 → ~500 files)
- **Week 11:** Trim spell variants
- **Week 12:** Final cleanup and documentation

**Files in production:** ~800 (core + tested custom)

### Month 4: Archival
- **Week 13:** Final review of test-scripts
- **Week 14:** Archive deleted content
- **Week 15:** Update documentation
- **Week 16:** Production deployment

---

## Comparison: Immediate vs Gradual

| Aspect | Immediate Deletion | test-scripts Approach |
|--------|-------------------|----------------------|
| **Speed** | 1 week | 3-4 months |
| **Risk** | High (may delete needed code) | Low (test before delete) |
| **Reversibility** | Difficult (git only) | Easy (just move back) |
| **Knowledge** | Little learned | Deep understanding |
| **Production quality** | Uncertain | Thoroughly tested |
| **Stress** | High | Low |

**Recommendation:** Use test-scripts approach ✅

---

## Best Practices

### ✅ Do This
- Document every decision
- Test before deleting
- Move gradually (not all at once)
- Keep evaluation log updated
- Version control test-scripts too

### ❌ Don't Do This
- Don't delete without testing
- Don't rush the process
- Don't skip documentation
- Don't test in production
- Don't lose original code (use git tags)

---

## Example: Casino System Migration

**1. Initial State**
```
Scripts/Engines/Casino/  (1.7MB, 50+ files)
```

**2. Move to test-scripts**
```bash
mkdir -p Scripts/test-scripts/OptionalSystems/Casino
mv Scripts/Engines/Casino/* Scripts/test-scripts/OptionalSystems/Casino/

# Create README
cat > Scripts/test-scripts/OptionalSystems/Casino/README.md <<EOF
# Casino System

**Size:** 1.7MB (50+ files)
**Purpose:** Casino games (slots, poker, dice)
**Status:** Untested in test-scripts

## Testing Needed
- Compile check
- Functionality test
- Player usage check
- Performance impact

## Decision: TBD
EOF
```

**3. Test**
```bash
# Enable in build, compile, test
# Result: Broken, slot machine crashes
```

**4. Document Decision**
```bash
# Add to Evaluation/DECISIONS.md:
# 2026-01-30 | Casino/ | Remove | Broken functionality, not worth fixing
```

**5. Remove**
```bash
rm -rf Scripts/test-scripts/OptionalSystems/Casino
```

---

## Summary

The **test-scripts migration strategy** provides:
1. **Safe path** from 8,269 to 800 files
2. **Gradual evaluation** over 3-4 months
3. **Reversible decisions** at every step
4. **Production quality** through testing
5. **Knowledge building** about the codebase

**Start with Phase 1** and work through systematically. No rush, no risk! 🚀
