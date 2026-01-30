# Project Updates Summary

**Date:** 2026-01-30
**Project:** NMS UO Server (formerly Ultima Adventures)

---

## Major Changes

### 1. Project Renamed ✅

**Old Name:** Ultima Adventures
**New Name:** NMS UO Server

**Updated In:**
- All GitHub workflows
- All documentation
- Deployment scripts
- Systemd service file
- Artifact names

### 2. Paths Updated ✅

**Old Path:** `/opt/ultima-adventures`
**New Path:** `/opt/nms-uoserver`

**Old Service:** `ultima-server`
**New Service:** `nms-uoserver`

**Old User:** `ultima:ultima`
**New User:** `nms:nms`

### 3. Production Build Retention Changed ✅

**Old:** 90 days
**New:** 30 days

### 4. Documentation Reorganized ✅

**Old:** Documentation files in root
**New:** All documentation in `/doc` folder

**Moved Files:**
- `REFACTORING-PROPOSAL.md` → `doc/REFACTORING-PROPOSAL.md`
- `CI-CD-SETUP.md` → `doc/CI-CD-SETUP.md`
- `CI-CD-QUICK-REFERENCE.md` → `doc/CI-CD-QUICK-REFERENCE.md`
- `CI-CD-FLOW-DIAGRAM.md` → `doc/CI-CD-FLOW-DIAGRAM.md`
- `BUILD-PLATFORMS.md` → `doc/BUILD-PLATFORMS.md`
- `DUAL-PLATFORM-STRATEGY.md` → `doc/DUAL-PLATFORM-STRATEGY.md`
- `RETENTION-AND-SCHEDULE.md` → `doc/RETENTION-AND-SCHEDULE.md`

**New Files Created:**
- `doc/README.md` - Documentation index
- `doc/TEST-SCRIPTS-MIGRATION.md` - Experimental content strategy
- `doc/DEPLOYMENT-GUIDE.md` - Production deployment guide

### 5. Deployment Scripts Updated ✅

**File:** `deploy/deploy.sh`
- Updated to use `/opt/nms-uoserver`
- Updated service name to `nms-uoserver`
- Updated GitHub repository references

**File:** `deploy/nms-uoserver.service` (renamed from `ultima-server.service`)
- Updated paths to `/opt/nms-uoserver`
- Updated service name
- Updated user to `nms:nms`

---

## CI/CD Configuration

### Artifact Retention

| Artifact Type | Old Retention | New Retention |
|--------------|---------------|---------------|
| **Dev builds** | 14 days | **5 days** ✅ |
| **Production builds** | 90 days | **30 days** ✅ |
| **Nightly builds** | 7 days | **5 days** ✅ |
| **GitHub Releases** | Permanent | Permanent |

### Nightly Build Schedule

**Old:** Daily at 2 AM UTC (7× per week)
**New:** Monday & Thursday at 3 AM UTC (2× per week) ✅

**Branch:** `dev` only (uses Linux/Mono)

### Build Platform Strategy

| Branch | Platform | Purpose | Build Time |
|--------|----------|---------|------------|
| **`dev`** | 🐧 Linux/Mono | Fast development | ~2-3 min |
| **`main`** | 🪟 Windows/MSBuild | Production releases | ~3-5 min |

---

## New test-scripts Strategy

### Purpose
Instead of immediately deleting experimental content, gradually evaluate it:

1. **Isolate** experimental files in `Scripts/test-scripts/`
2. **Test** individual systems before decisions
3. **Document** evaluation results
4. **Migrate** tested content to production or delete

### Benefits
- ✅ **Safe** - No immediate deletion
- ✅ **Gradual** - 3-4 month migration vs 1 week
- ✅ **Reversible** - Can bring back content if needed
- ✅ **Knowledge** - Learn what each system does
- ✅ **Quality** - Only tested code in production

### Proposed Structure

```
Scripts/
├── Configuration/          # Core config
├── Core/                   # Essential systems
├── Mobiles/                # Creatures + AI
├── Items/                  # Essential items
├── Systems/                # Game systems
├── Custom/                 # Verified custom content
│
└── test-scripts/           # Experimental & optional
    ├── Experimental/       # 1,539 legacy files
    ├── OptionalSystems/    # Casino, JediSystem, etc.
    ├── ExtraContent/       # Excess variants
    └── Evaluation/         # Testing logs
```

See `doc/TEST-SCRIPTS-MIGRATION.md` for complete strategy.

---

## File Changes Summary

### Workflows Updated

All in `.github/workflows/`:
- ✅ `build-dev.yml` - Dev builds (5-day retention, renamed artifacts)
- ✅ `build-and-release.yml` - Production (30-day retention, Windows only for main)
- ✅ `build-pr.yml` - PR validation (dynamic platform)
- ✅ `nightly-build.yml` - Nightly builds (Mon/Thu 3AM, dev branch, Linux)
- ✅ `.github/workflows/README.md` - Workflow documentation

### Deployment Files Updated

In `deploy/`:
- ✅ `deploy.sh` - Updated paths and service name
- ✅ `nms-uoserver.service` - Renamed and updated (was `ultima-server.service`)

### Documentation Created

In `doc/`:
- ✅ `README.md` - Documentation index
- ✅ `REFACTORING-PROPOSAL.md` - Moved from root
- ✅ `CI-CD-SETUP.md` - Moved from root
- ✅ `CI-CD-QUICK-REFERENCE.md` - Moved from root
- ✅ `CI-CD-FLOW-DIAGRAM.md` - Moved from root
- ✅ `BUILD-PLATFORMS.md` - Moved from root
- ✅ `DUAL-PLATFORM-STRATEGY.md` - Moved from root
- ✅ `RETENTION-AND-SCHEDULE.md` - Moved from root
- ✅ `TEST-SCRIPTS-MIGRATION.md` - **NEW** - Experimental content strategy
- ✅ `DEPLOYMENT-GUIDE.md` - **NEW** - Production deployment guide

---

## Configuration Updates Needed

### GitHub Secrets

If using automated deployment, update secret values:

| Secret | Old Value | New Value |
|--------|-----------|-----------|
| `PRODUCTION_PATH` | `/opt/ultima-adventures` | `/opt/nms-uoserver` |
| `PRODUCTION_USER` | `ultima` | `nms` |

### Server Configuration

When deploying to production:

1. **Create new user:**
   ```bash
   sudo useradd -r -m -d /opt/nms-uoserver -s /bin/bash nms
   ```

2. **Create directory structure:**
   ```bash
   sudo mkdir -p /opt/nms-uoserver/{bin,Data,Files,Saves,Logs,backups,deploy}
   sudo chown -R nms:nms /opt/nms-uoserver
   ```

3. **Install systemd service:**
   ```bash
   sudo cp deploy/nms-uoserver.service /etc/systemd/system/
   sudo systemctl daemon-reload
   sudo systemctl enable nms-uoserver
   ```

---

## Migration Checklist

### For Existing Deployments

If you have an existing `ultima-adventures` installation:

#### Option 1: Fresh Install (Recommended)
```bash
# 1. Backup old installation
sudo tar -czf ~/ultima-adventures-backup.tar.gz /opt/ultima-adventures/Saves

# 2. Install new version
# Follow doc/DEPLOYMENT-GUIDE.md

# 3. Restore saves
sudo tar -xzf ~/ultima-adventures-backup.tar.gz -C /opt/nms-uoserver/
```

#### Option 2: In-Place Migration
```bash
# 1. Stop old service
sudo systemctl stop ultima-server

# 2. Rename directory
sudo mv /opt/ultima-adventures /opt/nms-uoserver

# 3. Update user
sudo usermod -d /opt/nms-uoserver -l nms ultima

# 4. Install new service
sudo cp /opt/nms-uoserver/deploy/nms-uoserver.service /etc/systemd/system/
sudo systemctl daemon-reload
sudo systemctl disable ultima-server
sudo systemctl enable nms-uoserver

# 5. Start new service
sudo systemctl start nms-uoserver
```

---

## Next Steps

### Immediate Actions

1. **Push changes to repository:**
   ```bash
   git add .
   git commit -m "refactor: Rename to nms-uoserver, reorganize docs, add test-scripts strategy"
   git push origin dev
   ```

2. **Update GitHub Secrets** (if using auto-deployment):
   - Go to Settings → Secrets → Actions
   - Update `PRODUCTION_PATH` to `/opt/nms-uoserver`
   - Update `PRODUCTION_USER` to `nms`

3. **Test CI/CD:**
   ```bash
   # Trigger dev build (Linux)
   git push origin dev

   # Verify build succeeds with new names
   # Check Actions tab
   ```

4. **Update Production Server:**
   - Follow `doc/DEPLOYMENT-GUIDE.md`
   - Or use migration checklist above

### Long-Term Planning

1. **Review test-scripts strategy:**
   - Read `doc/TEST-SCRIPTS-MIGRATION.md`
   - Plan Phase 1: Initial separation
   - Schedule evaluation over 3-4 months

2. **Implement refactoring:**
   - Start with `Scripts/Experimental/` → `Scripts/test-scripts/Experimental/`
   - Gradually move optional systems
   - Test and document decisions

3. **Optimize codebase:**
   - Target: 8,269 → 800 essential files
   - Use test-scripts for safe evaluation
   - Keep production lean and tested

---

## Storage Impact

### Artifact Storage Savings

**Before:**
- Dev builds: 14 days × 50MB × 4/week = ~2.8GB
- Production builds: 90 days × 50MB × 3/week = ~13.5GB
- Nightly builds: 7 days × 50MB × 7/week = ~2.5GB
- **Total:** ~18.8GB

**After:**
- Dev builds: 5 days × 50MB × 4/week = ~1GB
- Production builds: 30 days × 50MB × 3/week = ~4.5GB
- Nightly builds: 5 days × 50MB × 2/week = ~0.5GB
- **Total:** ~6GB

**Savings:** ~12.8GB (68% reduction) 🎉

### Build Frequency Savings

**Before:**
- Nightly builds: 7× per week × 5 min = 35 min/week

**After:**
- Nightly builds: 2× per week × 3 min = 6 min/week

**Savings:** 29 min/week (83% reduction) 🎉

---

## Documentation Structure

```
/doc/
├── README.md                       # Documentation index
│
├── Refactoring & Migration
│   ├── REFACTORING-PROPOSAL.md     # Overall refactoring strategy
│   ├── TEST-SCRIPTS-MIGRATION.md   # Experimental content strategy
│   └── FOLDER-STRUCTURE.md         # [Future] Proposed structure
│
├── CI/CD
│   ├── CI-CD-SETUP.md              # Complete setup guide
│   ├── CI-CD-QUICK-REFERENCE.md    # Command reference
│   ├── CI-CD-FLOW-DIAGRAM.md       # Visual workflows
│   ├── DUAL-PLATFORM-STRATEGY.md   # Dev/Prod platform strategy
│   ├── BUILD-PLATFORMS.md          # Windows vs Linux
│   └── RETENTION-AND-SCHEDULE.md   # Artifact & schedule config
│
└── Deployment
    └── DEPLOYMENT-GUIDE.md         # Production deployment
```

---

## Quick Reference

### New Project Info
- **Name:** NMS UO Server
- **Repo:** nms-uoserver
- **Path:** `/opt/nms-uoserver`
- **Service:** `nms-uoserver`
- **User:** `nms:nms`

### Build Retention
- Dev: 5 days
- Production: 30 days
- Nightly: 5 days

### Build Schedule
- Dev: On push/PR
- Production: On push/tag to main
- Nightly: Mon/Thu 3 AM UTC (dev branch)

### Platforms
- Dev: Linux/Mono
- Production: Windows/MSBuild
- Nightly: Linux/Mono

### Documentation
- All in `/doc` folder
- Index: `doc/README.md`
- test-scripts strategy: `doc/TEST-SCRIPTS-MIGRATION.md`

---

## Testing Commands

```bash
# Test dev build (Linux)
git push origin dev
# → Check Actions tab for build

# Test production build (Windows)
git push origin main
# → Check Actions tab for build

# Create test release
git tag v0.1.0-test
git push origin v0.1.0-test
# → Check Releases for artifacts

# Test deployment script locally
sudo /opt/nms-uoserver/deploy/deploy.sh latest --auto-restart
```

---

## Summary

✅ **Completed:**
1. Project renamed to NMS UO Server
2. All paths updated to `/opt/nms-uoserver`
3. Production retention reduced to 30 days
4. Documentation moved to `/doc` folder
5. test-scripts migration strategy created
6. Deployment guide created
7. All workflows updated with new names

📋 **Next Steps:**
1. Push changes to GitHub
2. Update GitHub Secrets (if auto-deploying)
3. Test CI/CD pipelines
4. Deploy to production using new paths
5. Begin test-scripts migration (Phase 1)

🎯 **Goals:**
- Efficient artifact storage (68% reduction)
- Clear documentation organization
- Safe path to refactor (test-scripts)
- Production-ready deployment process

---

**All changes are backward compatible with Git** - you can always revert if needed!

For questions, see documentation in `/doc` folder.
