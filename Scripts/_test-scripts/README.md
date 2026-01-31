# test-scripts - Experimental Content (NOT COMPILED)

⚠️ **IMPORTANT:** This folder is **EXCLUDED from compilation**

---

## Purpose

This folder contains **experimental and optional content** that is:
- ❌ **NOT compiled** into Scripts.dll
- ❌ **NOT available** in-game
- ❌ **NOT deployed** to production
- ✅ **ONLY for evaluation** and testing

---

## How Exclusion Works

### In Scripts.csproj:
```xml
<ItemGroup>
  <!-- This line EXCLUDES test-scripts from compilation -->
  <Compile Remove="test-scripts/**/*.cs" />
</ItemGroup>
```

### Verification:
After building Scripts.dll, test-scripts content will NOT be included:
- No classes from test-scripts will load
- No systems from test-scripts will run
- No items/mobiles from test-scripts will spawn

**Production is SAFE from experimental content!** ✅

---

## Folder Structure

```
test-scripts/
├── README.md                  # This file
│
├── Experimental/              # Legacy experimental content
│   └── [NOT COMPILED]         # 1,539 files (Phase 2)
│
├── OptionalSystems/           # Optional game systems
│   ├── Casino/                # [NOT COMPILED]
│   ├── JediSystem/            # [NOT COMPILED]
│   └── SquireSystem/          # [NOT COMPILED]
│
├── ExtraContent/              # Excess variants
│   ├── Creatures/             # [NOT COMPILED]
│   ├── Items/                 # [NOT COMPILED]
│   └── Spells/                # [NOT COMPILED]
│
└── Evaluation/                # Testing documentation
    ├── README.md              # Evaluation process
    ├── DECISIONS.md           # Decision log
    ├── TESTING-LOG.md         # Test results
    └── templates/             # Test templates
```

---

## Status of Content

### ❌ NOT Compiled (Default)
All content in test-scripts is **excluded by default**.
- Scripts.dll does NOT include this code
- Production server does NOT load this code
- Players do NOT see this content

### ✅ How to Test Content

**ONLY on development server:**

1. **Edit Scripts.csproj** (temporarily):
   ```xml
   <!-- Uncomment this line to enable test-scripts -->
   <Compile Include="test-scripts/**/*.cs" />
   ```

2. **Build and test:**
   ```bash
   dotnet build Scripts/Scripts.csproj
   ./LinuxServer.exe  # Test on dev server
   ```

3. **After testing:**
   - Comment out the include line
   - Rebuild for production
   - test-scripts excluded again

**WARNING:** Never deploy with test-scripts enabled!

---

## Content Lifecycle

### 1. New Experimental Content
```
Developer creates → test-scripts/Experimental/ → NOT compiled
```

### 2. Evaluation Phase
```
test-scripts/ → Test on dev server → Document in Evaluation/
```

### 3. Decision
```
✅ Keep → Move to production folders → Compiles normally
⚠️ Maybe → Keep in test-scripts → Re-evaluate later
❌ Remove → Delete from test-scripts → Gone forever
```

---

## Why This Approach?

### ✅ Benefits:
1. **Safety** - Production never includes experimental code
2. **Clean** - Scripts.dll only contains tested code
3. **Gradual** - Evaluate content over time, not all at once
4. **Reversible** - Can always bring content back if needed
5. **Documentation** - Clear history of what was evaluated

### ❌ Alternative (Bad):
Delete everything immediately:
- May delete useful content
- Hard to reverse
- No evaluation process
- Risky

---

## Verification

### Check Scripts.dll Does NOT Include test-scripts:

```bash
# 1. Build project
dotnet build Scripts/Scripts.csproj

# 2. Check assembly contents (if you have tools)
# Scripts.dll should NOT contain classes from test-scripts

# 3. Run server
./LinuxServer.exe

# 4. In-game, test-scripts content should NOT exist
# - Systems don't load
# - Items don't spawn
# - Mobiles don't appear
```

---

## Summary

**test-scripts is a holding area for content evaluation:**

- ❌ NOT compiled by default
- ❌ NOT in production
- ✅ Available for evaluation
- ✅ Can be tested individually
- ✅ Moved to production if proven useful
- ✅ Deleted if confirmed unnecessary

**This keeps your production server lean, tested, and safe!** 🚀

---

## Questions?

See:
- `../doc/TEST-SCRIPTS-MIGRATION.md` - Complete migration strategy
- `Evaluation/README.md` - How to evaluate content
- `../MIGRATION-PLAN.md` - Overall refactoring plan
