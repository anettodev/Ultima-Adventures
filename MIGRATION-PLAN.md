# NMS UO Server - Migration Plan

**Current Status:** Assessment Complete
**Start Date:** 2026-01-30

---

## Current State Assessment

### Repository Structure
```
Ultima-Adventures/ (8,269 files in Scripts/)
├── .github/workflows/        # ✅ NEW - CI/CD workflows created
├── doc/                       # ✅ NEW - Documentation organized
├── deploy/                    # ✅ NEW - Deployment scripts
├── Scripts/                   # 📋 NEEDS REFACTOR
│   ├── Core/                  # Keep (essential)
│   ├── Engines/               # Review & reorganize
│   ├── Experimental/          # Move to test-scripts
│   ├── Items/                 # Keep but trim
│   ├── Mobiles/               # Keep but reorganize
│   ├── MyServerSettings.cs    # Rename & move
│   └── UltimaLive/            # Keep
├── Server/                    # Keep (core engine)
├── Data/                      # Keep
└── [Root files]               # Keep
```

### File Counts (from analysis)
- **Scripts/Core/**: 501 files
- **Scripts/Engines/**: 1,875 files (largest)
- **Scripts/Items/**: 2,845 files
- **Scripts/Mobiles/**: 1,495 files
- **Scripts/Experimental/**: 1,539 files
- **Scripts/UltimaLive/**: 13 files

**Total:** 8,269 files → Target: ~800 essential files

---

## Migration Phases

### Phase 1: Safe Initialization ✅ (Ready to Start)
**Goal:** Set up structure without breaking anything
**Risk:** Low
**Time:** 30 minutes

**Actions:**
1. ✅ Commit current CI/CD and documentation changes
2. Create `Scripts/test-scripts/` directory structure
3. Create evaluation templates
4. Create backup tag in git
5. No file moves yet - just setup

**Deliverables:**
- Git commit with CI/CD and docs
- test-scripts structure ready
- Evaluation templates
- Git tag: `before-refactor`

---

### Phase 2: Move Experimental Content (Week 1)
**Goal:** Move obviously experimental content
**Risk:** Low (experimental files rarely used)
**Time:** 1-2 hours

**Actions:**
1. Move `Scripts/Experimental/` → `Scripts/test-scripts/Experimental/`
2. Update `.csproj` to exclude test-scripts from compilation
3. Test server compiles and runs
4. Commit changes

**Files Moved:** ~1,539 files
**Impact:** Experimental content isolated, not compiled

---

### Phase 3: Move Optional Systems (Week 2-3)
**Goal:** Move large optional systems
**Risk:** Medium (may have dependencies)
**Time:** 2-4 hours

**Systems to Evaluate & Move:**
- Casino/ (1.7MB) - Move to test-scripts
- Holiday/ (360KB) - Move to test-scripts
- JediSystem/ - Move to test-scripts
- SquireSystem/ (748KB) - Move to test-scripts
- MyRunUO/ - Move to test-scripts
- RemoteAdmin/ - Check if needed

**Process:**
1. Research dependencies for each
2. Move to test-scripts/OptionalSystems/
3. Test compilation after each move
4. Document decisions

**Files Moved:** ~500-800 files
**Impact:** Optional systems isolated

---

### Phase 4: Reorganize Core Structure (Week 4)
**Goal:** Create optimal folder structure
**Risk:** Medium (namespace changes)
**Time:** 4-6 hours

**Actions:**
1. Create new structure:
   - `Scripts/Configuration/`
   - `Scripts/Mobiles/_Base/`
   - `Scripts/Mobiles/AI/` (move from Engines/AI/)
   - `Scripts/Items/_Base/`
   - `Scripts/Systems/` (rename from Engines/)

2. Move files to new locations
3. Update namespaces
4. Update using statements
5. Test compilation

**Files Reorganized:** ~500 files
**Impact:** Better organization, namespace changes

---

### Phase 5: Trim Content Variants (Week 5-8)
**Goal:** Reduce redundant content
**Risk:** Medium (may break spawns/quests)
**Time:** Ongoing evaluation

**Targets:**
- Creatures: 1,495 → ~200 files (keep 10-20 per category)
- Items: 2,845 → ~500 files (keep essentials)
- Custom spells: Remove unused schools

**Process:**
1. Identify variants
2. Check spawn references
3. Check quest references
4. Move excess to test-scripts
5. Test thoroughly

**Files Moved:** ~3,000-4,000 files
**Impact:** Leaner production codebase

---

### Phase 6: Final Cleanup (Week 9-10)
**Goal:** Production-ready codebase
**Risk:** Low (already tested)
**Time:** 2-3 hours

**Actions:**
1. Review test-scripts decisions
2. Delete confirmed unnecessary content
3. Update documentation
4. Final testing
5. Create release

**Result:** ~800 essential files in production

---

## Detailed Phase 1 Plan (Today)

### Step 1.1: Commit Current Changes
```bash
git add .github/workflows/
git add doc/
git add deploy/
git add CHANGES-SUMMARY.md
git commit -m "feat: Add CI/CD, reorganize docs, add deployment scripts"
```

### Step 1.2: Create Backup Tag
```bash
git tag -a before-refactor -m "Backup before starting refactor"
git push origin before-refactor
```

### Step 1.3: Create test-scripts Structure
```bash
mkdir -p Scripts/test-scripts/{Experimental,OptionalSystems,ExtraContent,Evaluation}
mkdir -p Scripts/test-scripts/Evaluation/templates
```

### Step 1.4: Create Evaluation Templates
- test-scripts/README.md
- test-scripts/Evaluation/README.md
- test-scripts/Evaluation/DECISIONS.md
- test-scripts/Evaluation/TESTING-LOG.md
- test-scripts/Evaluation/templates/system-test.md
- test-scripts/Evaluation/templates/content-test.md

### Step 1.5: Update .csproj
Add exclusion for test-scripts:
```xml
<ItemGroup>
  <!-- Exclude test-scripts from compilation -->
  <Compile Remove="test-scripts/**/*.cs" />
</ItemGroup>
```

### Step 1.6: Test Compilation
```bash
# Verify server still compiles
dotnet build Scripts/Scripts.csproj
# Or use mono
```

### Step 1.7: Commit Phase 1
```bash
git add Scripts/test-scripts/
git add Scripts/Scripts.csproj
git commit -m "feat: Set up test-scripts structure for gradual refactoring"
```

---

## Decision Points (Will Ask Before Proceeding)

### Major Decision 1: Move Experimental/ (Phase 2)
**Question:** Move 1,539 experimental files to test-scripts?
**Impact:** Files won't compile, but safe to test later
**Reversible:** Yes (git revert)

### Major Decision 2: Move Optional Systems (Phase 3)
**Question:** Move Casino, Holiday, JediSystem, etc.?
**Impact:** Features won't be available unless re-enabled
**Reversible:** Yes (move back)

### Major Decision 3: Namespace Reorganization (Phase 4)
**Question:** Reorganize folder structure (AI to Mobiles, etc.)?
**Impact:** Namespace changes, using statement updates
**Reversible:** Yes but tedious

### Major Decision 4: Trim Content (Phase 5)
**Question:** Remove excess creature/item variants?
**Impact:** Less variety, but cleaner codebase
**Reversible:** Yes (from test-scripts)

---

## Risk Mitigation

### For Each Phase:
1. ✅ **Git commit before starting**
2. ✅ **Test compilation after each major change**
3. ✅ **Test server starts (if applicable)**
4. ✅ **Document what was changed**
5. ✅ **Create git tag after completion**

### Emergency Rollback:
```bash
# Rollback last commit
git reset --hard HEAD~1

# Rollback to specific tag
git reset --hard before-refactor

# Rollback specific file
git checkout HEAD -- path/to/file
```

---

## Testing Checklist (After Each Phase)

- [ ] Project compiles without errors
- [ ] Server executable runs
- [ ] No missing namespace errors
- [ ] Core systems load
- [ ] Git history preserved
- [ ] Documentation updated

---

## Success Metrics

### Phase 1 Success:
- ✅ test-scripts structure created
- ✅ Evaluation templates ready
- ✅ Git backup created
- ✅ Server still compiles

### Phase 2 Success:
- ✅ Experimental files moved
- ✅ Server compiles without them
- ✅ test-scripts excluded from build

### Final Success (Phase 6):
- ✅ ~800 production files
- ✅ ~6,500 files in test-scripts or deleted
- ✅ Server runs smoothly
- ✅ All tests pass
- ✅ Documentation complete

---

## Timeline Estimate

| Phase | Duration | Complexity | Risk |
|-------|----------|------------|------|
| Phase 1 | 30 min | Low | Low |
| Phase 2 | 1-2 hours | Low | Low |
| Phase 3 | 2-4 hours | Medium | Medium |
| Phase 4 | 4-6 hours | Medium | Medium |
| Phase 5 | Ongoing | High | Medium |
| Phase 6 | 2-3 hours | Low | Low |

**Total Estimated Time:** 3-4 months (working incrementally)

---

## Current Status

**Phase 0:** ✅ Complete - Assessment done
**Phase 1:** 🟡 Ready to start
**Phase 2:** ⏳ Pending
**Phase 3:** ⏳ Pending
**Phase 4:** ⏳ Pending
**Phase 5:** ⏳ Pending
**Phase 6:** ⏳ Pending

---

## Ready to Start?

**Next Step:** Execute Phase 1
**Estimated Time:** 30 minutes
**Risk:** Low (no file moves, just setup)

Shall we proceed with Phase 1?
