# [System Name] - Test Plan

**Date:** YYYY-MM-DD
**Tester:** [Your Name]
**Location:** `test-scripts/OptionalSystems/[SystemName]/`

---

## System Overview

**Purpose:** [What does this system do?]

**Size:** [File count, total MB]

**Original Location:** `Scripts/Engines/[SystemName]/`

---

## Pre-Test Research

### Dependencies Found

**Requires:**
- [ ] [Dependency 1]
- [ ] [Dependency 2]

**Referenced By:**
- [ ] [System/File that uses this]

**References:**
- [ ] [What this system uses]

### Code Search Results

```bash
# Search for references
grep -r "SystemName" Scripts/Core/ Scripts/Mobiles/ Scripts/Items/ --include="*.cs"

# Results:
# [paste search results]
```

---

## Test Plan

### 1. Compilation Test

**Enable in Scripts.csproj:**
```xml
<Compile Include="test-scripts/**/*.cs" />
```

**Build:**
```bash
dotnet build Scripts/Scripts.csproj
```

**Expected:** Compiles without errors

**Result:**
- [ ] Success
- [ ] Warnings: [list]
- [ ] Errors: [list]

---

### 2. Server Start Test

**Run server:**
```bash
./LinuxServer.exe
```

**Expected:** Server starts, system loads

**Result:**
- [ ] Server starts
- [ ] System loads
- [ ] Errors in logs: [list]

---

### 3. Functionality Test

**Test Cases:**

#### Test Case 1: [Basic Functionality]
- **Action:** [What to do]
- **Expected:** [What should happen]
- **Result:** ✅/❌ [What actually happened]

#### Test Case 2: [Feature X]
- **Action:** [What to do]
- **Expected:** [What should happen]
- **Result:** ✅/❌ [What actually happened]

#### Test Case 3: [Edge Case]
- **Action:** [What to do]
- **Expected:** [What should happen]
- **Result:** ✅/❌ [What actually happened]

---

### 4. Performance Test

**Monitor:**
- CPU usage during operation
- Memory usage
- Server lag/responsiveness

**Result:**
- CPU: [%]
- Memory: [MB]
- Impact: ✅ Minimal / ⚠️ Moderate / ❌ High

---

### 5. Player Usage Test

**Check if players actually use this:**
- [ ] Check spawn references
- [ ] Check quest references
- [ ] Ask players about it
- [ ] Check server logs for usage

**Result:** [Usage frequency: Never / Rarely / Sometimes / Often]

---

## Test Results Summary

### Compilation: ✅/❌
[Details]

### Functionality: ✅/❌
[Details]

### Performance: ✅/⚠️/❌
[Details]

### Player Value: ✅/⚠️/❌
[Details]

---

## Decision

**Decision:** [ ] Keep / [ ] Remove / [ ] Defer

**Rationale:**
[Explain why this decision was made]

**If Keep, move to:**
- Destination: `Scripts/Systems/[SystemName]/`
- Update references: [list any needed updates]

**If Remove:**
- Reason: [why removing]
- Alternative: [if any]

**If Defer:**
- Reason: [why waiting]
- Re-evaluate: [when/under what conditions]

---

## Action Items

- [ ] Update DECISIONS.md with decision
- [ ] Update TESTING-LOG.md with results
- [ ] Move files if keeping
- [ ] Delete files if removing
- [ ] Disable test-scripts in .csproj
- [ ] Commit changes

---

## Notes

[Any additional notes or observations]
