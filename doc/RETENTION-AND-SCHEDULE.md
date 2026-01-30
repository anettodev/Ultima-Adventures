# Artifact Retention & Build Schedule

Updated retention policies and build schedules for CI/CD workflows.

---

## Artifact Retention Policy

| Workflow | Artifact Type | Retention | Rationale |
|----------|--------------|-----------|-----------|
| **Dev Builds** | `scripts-dll-dev-*` | **5 days** | Short-term testing only |
| **Dev Builds** | `dev-build-*` | **5 days** | Short-term testing only |
| **Production Builds** | `ultima-adventures-*` | **90 days** | Long-term access to builds |
| **Nightly Builds** | `nightly-*` | **5 days** | Weekly cadence, recent only |
| **GitHub Releases** | All assets | **Permanent** | Official releases |

### Storage Impact

**Before:**
- Dev builds: 14 days × ~50MB × ~4 builds/week = ~2.8GB storage
- Nightly builds: 7 days × ~50MB × 7 builds/week = ~2.5GB storage
- **Total:** ~5.3GB

**After:**
- Dev builds: 5 days × ~50MB × ~4 builds/week = ~1GB storage
- Nightly builds: 5 days × ~50MB × 2 builds/week = ~0.5GB storage
- **Total:** ~1.5GB storage

**Savings:** ~72% reduction in artifact storage! 🎉

---

## Build Schedule

### Dev Builds (build-dev.yml)

**Triggers:**
- ✅ Push to `dev` branch (on-demand)
- ✅ Pull requests to `dev` branch (on-demand)
- ✅ Manual dispatch (on-demand)

**Platform:** Linux/Mono 🐧

**Frequency:** As needed (developer-driven)

---

### Production Builds (build-and-release.yml)

**Triggers:**
- ✅ Push to `main` branch (on-demand)
- ✅ Tags `v*.*.*` (releases)
- ✅ Pull requests to `main` branch (on-demand)
- ✅ Manual dispatch (on-demand)

**Platform:** Windows/MSBuild 🪟

**Frequency:** As needed (release-driven)

---

### Nightly Builds (nightly-build.yml)

**Schedule:** **Twice weekly** 📅
- **Monday** at 3 AM UTC
- **Thursday** at 3 AM UTC

**Branch:** `dev` only

**Platform:** Linux/Mono 🐧

**Purpose:** Catch build regressions between development cycles

**Frequency:** 2× per week (reduced from 7×)

---

## Nightly Build Schedule Details

### Cron Schedule

```yaml
schedule:
  - cron: '0 3 * * 1'  # Monday at 3 AM UTC
  - cron: '0 3 * * 4'  # Thursday at 3 AM UTC
```

### Timezone Conversions

| Timezone | Monday | Thursday |
|----------|---------|----------|
| **UTC** | 3:00 AM | 3:00 AM |
| **PST (UTC-8)** | 7:00 PM (Sunday) | 7:00 PM (Wednesday) |
| **EST (UTC-5)** | 10:00 PM (Sunday) | 10:00 PM (Wednesday) |
| **CET (UTC+1)** | 4:00 AM | 4:00 AM |
| **JST (UTC+9)** | 12:00 PM (Noon) | 12:00 PM (Noon) |

### Why Monday & Thursday?

**Monday:**
- ✅ Catches any weekend changes
- ✅ Validates work week start
- ✅ Early week detection

**Thursday:**
- ✅ Mid-week validation
- ✅ Catches changes from Monday-Wednesday
- ✅ Gives Friday to fix issues before weekend

**Why not daily?**
- ❌ Wasteful - dev branch doesn't change that much
- ❌ Higher cost (artifact storage, compute minutes)
- ❌ Alert fatigue if builds fail repeatedly

**Why not just once weekly?**
- ❌ Too infrequent - issues could go unnoticed for a week
- ❌ Less coverage of development cycle

**Twice weekly = sweet spot!** ✅

---

## Manual Triggering

All workflows can be triggered manually via **workflow_dispatch**:

### Via GitHub UI

1. Go to **Actions** tab
2. Select workflow (e.g., "Nightly Build (Dev)")
3. Click **Run workflow**
4. Choose branch (if applicable)
5. Click **Run workflow**

### Via GitHub CLI

```bash
# Trigger nightly build manually
gh workflow run nightly-build.yml

# Trigger dev build
gh workflow run build-dev.yml

# Trigger production build
gh workflow run build-and-release.yml
```

### Via API

```bash
curl -X POST \
  -H "Accept: application/vnd.github+json" \
  -H "Authorization: Bearer $GITHUB_TOKEN" \
  https://api.github.com/repos/OWNER/REPO/actions/workflows/nightly-build.yml/dispatches \
  -d '{"ref":"dev"}'
```

---

## Artifact Cleanup

GitHub automatically deletes artifacts after their retention period expires.

### Manual Cleanup

If you need to free space immediately:

```bash
# List all artifacts
gh api repos/OWNER/REPO/actions/artifacts

# Delete specific artifact
gh api -X DELETE repos/OWNER/REPO/actions/artifacts/ARTIFACT_ID
```

### Cleanup Script (Optional)

```bash
#!/bin/bash
# cleanup-old-artifacts.sh
# Delete artifacts older than 3 days

gh api repos/OWNER/REPO/actions/artifacts --paginate \
  | jq -r '.artifacts[] | select(.created_at < (now - 259200 | strftime("%Y-%m-%dT%H:%M:%SZ"))) | .id' \
  | xargs -I {} gh api -X DELETE repos/OWNER/REPO/actions/artifacts/{}
```

---

## Build Frequency Analysis

### Expected Build Counts

**Dev Builds:**
- Pushes: ~4 per week (developer-driven)
- PRs: ~2 per week
- **Total:** ~6 builds/week

**Production Builds:**
- Pushes: ~1 per week
- Tags: ~1 per month
- PRs: ~1 per week
- **Total:** ~2-3 builds/week

**Nightly Builds:**
- Monday: 1 build
- Thursday: 1 build
- **Total:** 2 builds/week

**Grand Total:** ~10-11 builds/week

### Cost Impact (Private Repos)

| Workflow | Builds/Week | Platform | Minutes/Build | Total Minutes/Week |
|----------|-------------|----------|---------------|-------------------|
| Dev | 6 | Linux (1x) | 3 min | 18 min |
| Production | 3 | Windows (2x) | 5 min × 2 | 30 min |
| Nightly | 2 | Linux (1x) | 3 min | 6 min |
| **Total** | **11** | - | - | **54 min/week** |

**Monthly:** ~216 minutes (well within 2,000 free tier) ✅

**For public repos:** Unlimited free! 🎉

---

## Optimization Summary

### Changes Made

1. ✅ **Dev builds retention:** 14 days → **5 days** (-64%)
2. ✅ **Nightly build schedule:** Daily → **Twice weekly** (-71%)
3. ✅ **Nightly build branch:** All branches → **dev only**
4. ✅ **Nightly build platform:** Windows → **Linux** (faster, cheaper)
5. ✅ **Nightly build retention:** 7 days → **5 days** (-29%)

### Results

| Metric | Before | After | Improvement |
|--------|--------|-------|-------------|
| **Nightly builds/week** | 7 | 2 | -71% |
| **Dev artifact storage** | ~2.8GB | ~1GB | -64% |
| **Nightly artifact storage** | ~2.5GB | ~0.5GB | -80% |
| **Total artifact storage** | ~5.3GB | ~1.5GB | **-72%** |
| **Nightly build minutes/week** | ~35 min | ~6 min | -83% |

**Overall:** Much more efficient while maintaining quality! 🚀

---

## Monitoring

### Check Next Nightly Build

```bash
# View nightly build schedule
gh workflow view nightly-build.yml

# See last run
gh run list --workflow=nightly-build.yml --limit 1

# See next scheduled run (in GitHub UI)
# Actions → Nightly Build (Dev) → "Next scheduled run"
```

### Email Notifications

Configure in **Settings → Notifications → Actions**:
- ✅ Notify on workflow failures
- ⚠️ Optional: Notify on nightly build success

### Issue Tracking

Nightly build failures automatically create GitHub issues with:
- ✅ Failure date and day
- ✅ Branch (dev)
- ✅ Platform (Linux/Mono)
- ✅ Link to workflow logs
- ✅ Labels: `build-failure`, `automated`, `nightly-build`, `dev`

---

## Adjusting the Schedule

### Change Nightly Build Days

Edit `.github/workflows/nightly-build.yml`:

```yaml
# Example: Change to Tuesday and Friday
schedule:
  - cron: '0 3 * * 2'  # Tuesday at 3 AM UTC
  - cron: '0 3 * * 5'  # Friday at 3 AM UTC
```

**Cron day numbers:**
- 0 = Sunday
- 1 = Monday
- 2 = Tuesday
- 3 = Wednesday
- 4 = Thursday
- 5 = Friday
- 6 = Saturday

### Change Nightly Build Time

```yaml
# Example: Change to 6 AM UTC
schedule:
  - cron: '0 6 * * 1'  # Monday at 6 AM UTC
  - cron: '0 6 * * 4'  # Thursday at 6 AM UTC
```

### Change Retention Period

```yaml
# Example: Keep for 10 days
retention-days: 10
```

### Disable Nightly Builds

Comment out the schedule section:

```yaml
on:
  # schedule:
  #   - cron: '0 3 * * 1'
  #   - cron: '0 3 * * 4'
  workflow_dispatch:  # Keep manual trigger
```

---

## Best Practices

### ✅ Do This

- **Monitor nightly build failures** - Fix promptly
- **Download artifacts before expiration** - If needed for testing
- **Keep retention short for dev builds** - Reduces storage costs
- **Use production builds for releases** - Not dev or nightly builds
- **Let GitHub auto-delete old artifacts** - Don't accumulate

### ❌ Don't Do This

- Don't rely on nightly builds for production
- Don't set retention too high (wastes storage)
- Don't run nightly builds daily (unnecessary)
- Don't ignore nightly build failures
- Don't download nightly builds unless testing

---

## FAQ

**Q: Why not run nightly builds on main branch?**
- A: Main branch doesn't change as frequently, and production builds already validate it.

**Q: Can I change nightly builds back to daily?**
- A: Yes, but it's wasteful. Twice weekly is sufficient for catching regressions.

**Q: What if I need an older dev build?**
- A: With 5-day retention, you have ~5 recent builds. For older versions, use Git tags.

**Q: Why Monday and Thursday specifically?**
- A: Monday catches weekend changes, Thursday validates mid-week work. Adjust to your team's schedule.

**Q: Can I keep nightly builds longer?**
- A: Yes, increase `retention-days`, but it will use more storage.

**Q: What happens if nightly build fails?**
- A: GitHub issue is created automatically with details and workflow logs.

---

## Current Configuration Summary

✅ **Dev builds:** 5-day retention
✅ **Nightly builds:** Monday & Thursday at 3 AM UTC on dev branch
✅ **Nightly retention:** 5 days
✅ **Production builds:** 90-day retention
✅ **Platform strategy:** Linux for dev, Windows for production

**Result:** Efficient, cost-effective, and maintains quality! 🎯
