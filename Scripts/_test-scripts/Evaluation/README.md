# test-scripts Evaluation Process

How to evaluate experimental content before deciding to keep or delete.

---

## Evaluation Workflow

```
1. Select System/Content
   ↓
2. Research Dependencies
   ↓
3. Create Test Plan
   ↓
4. Test on Dev Server
   ↓
5. Document Results
   ↓
6. Make Decision
   ↓
7. Keep, Remove, or Defer
```

---

## Step-by-Step Process

### 1. Select Content to Evaluate

```bash
# List what's in test-scripts
ls -la Scripts/test-scripts/OptionalSystems/

# Pick one system to evaluate
SYSTEM="Casino"
```

### 2. Research Dependencies

```bash
# Find what references this system
cd Scripts
grep -r "Casino" Core/ Mobiles/ Items/ --include="*.cs" | head -20

# Check for dependencies
grep -r "using.*Casino" **/*.cs
```

### 3. Create Test Plan

Use template: `templates/system-test.md`

Document:
- What the system does
- Dependencies found
- How to test it
- Expected behavior

### 4. Test on Dev Server

```bash
# 1. Temporarily enable in Scripts.csproj
# Uncomment: <Compile Include="test-scripts/**/*.cs" />

# 2. Build
dotnet build Scripts/Scripts.csproj

# 3. Run on dev server
./LinuxServer.exe

# 4. Test functionality
# - Does it compile?
# - Does server start?
# - Does it work as expected?
# - Any errors in logs?

# 5. Disable again
# Comment out: <Compile Include="test-scripts/**/*.cs" />
```

### 5. Document Results

Update `TESTING-LOG.md` with:
- What was tested
- Compilation results
- Functionality results
- Decision rationale

### 6. Make Decision

**Keep (Move to Production):**
- ✅ Compiles successfully
- ✅ Works as expected
- ✅ Players use it
- ✅ Part of server identity

**Remove (Delete):**
- ❌ Doesn't compile
- ❌ Broken functionality
- ❌ Never used
- ❌ Obsolete/superseded

**Defer (Keep in test-scripts):**
- ⚠️ Works but rarely used
- ⚠️ Needs more testing
- ⚠️ Uncertain value

### 7. Execute Decision

**If Keep:**
```bash
# Move to production location
mkdir -p Scripts/Systems/Casino
mv test-scripts/OptionalSystems/Casino/* Scripts/Systems/Casino/

# Document in DECISIONS.md
echo "2026-01-30 | Casino | Keep → Systems/ | Used by players" >> DECISIONS.md
```

**If Remove:**
```bash
# Delete from test-scripts
rm -rf test-scripts/OptionalSystems/Casino

# Document in DECISIONS.md
echo "2026-01-30 | Casino | Remove | Broken, never used" >> DECISIONS.md
```

**If Defer:**
```bash
# Leave in test-scripts
# Document why
echo "2026-01-30 | Casino | Defer | Needs player feedback" >> DECISIONS.md
```

---

## Evaluation Criteria

### For Systems (Engines/)

| Criteria | Keep | Remove |
|----------|------|--------|
| **Used by players** | ✅ Yes | ❌ No |
| **Works correctly** | ✅ Yes | ❌ No |
| **Has dependencies** | ✅ Core systems depend on it | ❌ Nothing uses it |
| **Part of server identity** | ✅ Custom feature | ❌ Generic system |
| **Maintenance** | ✅ Easy to maintain | ❌ Broken/complex |

### For Content (Mobiles/Items)

| Criteria | Keep | Remove |
|----------|------|--------|
| **Spawns in world** | ✅ Yes | ❌ No |
| **Quest critical** | ✅ Yes | ❌ No |
| **Unique/Special** | ✅ Yes | ❌ Duplicate |
| **Player favorites** | ✅ Yes | ❌ Never seen |

---

## Documentation Files

### DECISIONS.md
Log of all decisions made:
- Date
- System/Content name
- Decision (Keep/Remove/Defer)
- Rationale
- Tested by

### TESTING-LOG.md
Detailed test results:
- System name
- Test date
- Compilation results
- Functionality tests
- Dependencies found
- Decision

---

## Templates

### templates/system-test.md
Template for testing systems/engines

### templates/content-test.md
Template for testing content (creatures/items)

---

## Tips

### ✅ Do This:
- Test one system at a time
- Document everything
- Take your time
- Ask for player feedback
- Check spawn references before deleting

### ❌ Don't Do This:
- Test multiple systems at once
- Delete without testing
- Rush decisions
- Forget to document
- Delete without checking dependencies

---

## Example: Casino System

See full example in `doc/TEST-SCRIPTS-MIGRATION.md`

**Summary:**
1. Found Casino in test-scripts/OptionalSystems/
2. Researched: Found dependencies on Gambling system
3. Tested: Slot machine crashes server
4. Decision: Remove (broken, not worth fixing)
5. Documented in DECISIONS.md
6. Deleted from test-scripts

---

## Progress Tracking

Track your evaluation progress:

```bash
# Total systems to evaluate
find test-scripts -type d -maxdepth 2 | wc -l

# Systems evaluated
wc -l < Evaluation/DECISIONS.md

# Decisions breakdown
grep "Keep" Evaluation/DECISIONS.md | wc -l    # Kept
grep "Remove" Evaluation/DECISIONS.md | wc -l  # Removed
grep "Defer" Evaluation/DECISIONS.md | wc -l   # Deferred
```

---

## Questions?

See:
- `../doc/TEST-SCRIPTS-MIGRATION.md` - Complete strategy
- `DECISIONS.md` - Decision log
- `TESTING-LOG.md` - Test results
- `templates/` - Test plan templates
