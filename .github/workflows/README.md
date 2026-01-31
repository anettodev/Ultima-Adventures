# GitHub Actions Workflows

Overview of all CI/CD workflows for Ultima Adventures.

---

## Workflow Summary

| Workflow | File | Platform | Trigger | Purpose |
|----------|------|----------|---------|---------|
| **Build Dev** | `build-dev.yml` | 🐧 Linux/Mono | Push/PR to `dev` | Fast dev builds |
| **Build Production** | `build-and-release.yml` | 🪟 Windows/MSBuild | Push/PR/Tag to `main` | Production releases |
| **Build PR** | `build-pr.yml` | 🔄 Dynamic | Any PR | Smart PR validation |
| **Nightly Build** | `nightly-build.yml` | 🪟 Windows/MSBuild | Daily 2 AM UTC | Catch regressions |

---

## Platform Strategy

### Development (`dev` branch)
- **Platform:** Linux + Mono
- **Why:** Fast iteration, lower cost, quick feedback
- **Build time:** ~2-3 minutes
- **Use for:** Development, testing, experimentation

### Production (`main` branch)
- **Platform:** Windows + MSBuild
- **Why:** Maximum compatibility, production-ready
- **Build time:** ~3-5 minutes
- **Use for:** Releases, production deployments

---

## Quick Reference

### Trigger a Dev Build
```bash
git push origin dev
```
→ Builds on Linux (fast)

### Trigger a Production Build
```bash
git push origin main
```
→ Builds on Windows (reliable)

### Create a Release
```bash
git tag v1.0.0
git push origin v1.0.0
```
→ Builds on Windows + creates GitHub Release

### Test a PR
```bash
gh pr create --base dev  # → Builds on Linux
gh pr create --base main # → Builds on Windows
```

---

## Workflow Details

### 1. build-dev.yml (Linux/Mono)

**Runs on:** `ubuntu-latest`

**Steps:**
1. Install Mono + MSBuild
2. Restore NuGet packages
3. Build Scripts.dll
4. Upload dev artifacts (14-day retention)

**Artifacts:**
- `scripts-dll-dev-{version}`
- `dev-build-{version}`

**Status Badge:**
```markdown
[![Dev Build](../../actions/workflows/build-dev.yml/badge.svg)](../../actions/workflows/build-dev.yml)
```

---

### 2. build-and-release.yml (Windows/MSBuild)

**Runs on:** `windows-latest`

**Steps:**
1. Setup MSBuild + NuGet
2. Restore packages
3. Build Scripts.dll
4. Run tests
5. Package full release
6. Create GitHub Release (if tagged)
7. Deploy to production (if configured)

**Artifacts:**
- `ultima-adventures-{version}.zip` (full package)
- `Scripts.dll` (standalone)

**Outputs:**
- GitHub Release (for tags)
- Production deployment (for main branch)

**Status Badge:**
```markdown
[![Production Build](../../actions/workflows/build-and-release.yml/badge.svg)](../../actions/workflows/build-and-release.yml)
```

---

### 3. build-pr.yml (Dynamic Platform)

**Runs on:** Depends on target branch
- PR → `dev`: Linux/Mono
- PR → `main`: Windows/MSBuild

**Steps:**
1. Detect target branch
2. Build on appropriate platform
3. Comment on PR with results

**Smart platform selection** ensures PRs are tested with the same platform they'll use when merged!

---

### 4. nightly-build.yml (Windows/MSBuild)

**Runs on:** `windows-latest`

**Schedule:** Daily at 2 AM UTC (`0 2 * * *`)

**Steps:**
1. Build latest commit
2. Upload nightly artifacts (7-day retention)
3. Create GitHub issue on failure

**Purpose:** Catch build breaks early

---

## Branch Protection

Recommended settings for each branch:

### `main` branch
- ✅ Require pull request reviews (1+ reviewer)
- ✅ Require status checks to pass
  - `Build Production (Windows/MSBuild)`
- ✅ Require conversation resolution
- ✅ Require linear history
- ✅ Do not allow force pushes

### `dev` branch
- ✅ Require status checks to pass
  - `Build Dev (Linux/Mono)`
- ⚠️ Optional: Require pull request reviews
- ✅ Allow force pushes (for rebasing)

---

## GitHub Secrets (Optional)

For automated production deployment, configure these secrets:

| Secret | Description | Required |
|--------|-------------|----------|
| `SSH_PRIVATE_KEY` | SSH key for production server | For auto-deploy |
| `PRODUCTION_HOST` | Server hostname/IP | For auto-deploy |
| `PRODUCTION_USER` | SSH username | For auto-deploy |
| `PRODUCTION_PATH` | Server install path | For auto-deploy |

**Setup:** Settings → Secrets and variables → Actions → New repository secret

---

## Artifact Retention

| Workflow | Artifact | Retention |
|----------|----------|-----------|
| Dev builds | `scripts-dll-dev-*` | 14 days |
| Dev builds | `dev-build-*` | 14 days |
| Production builds | `ultima-adventures-*` | 90 days |
| Nightly builds | `nightly-*` | 7 days |
| GitHub Releases | All assets | Permanent |

---

## Build Time Comparison

| Branch | Platform | Startup | Compile | Total |
|--------|----------|---------|---------|-------|
| `dev` | Linux | ~15s | ~90s | **2-3 min** |
| `main` | Windows | ~45s | ~120s | **3-5 min** |

**Dev builds are ~40% faster!** ⚡

---

## Troubleshooting

### Build Failed on Linux but Works Locally

**Cause:** Mono compatibility issue

**Fix:**
1. Check build logs
2. Test with Mono locally
3. Fix compatibility issue or use Windows build

### Build Failed on Windows

**Cause:** Compilation error

**Fix:**
1. Check build logs for errors
2. Fix code issues
3. Test locally with MSBuild

### PR Not Building

**Cause:** Workflow permissions or branch protection

**Fix:**
1. Check Actions are enabled (Settings → Actions)
2. Check workflow permissions (Read and write)
3. Check branch protection rules

---

## Viewing Build Results

### Via GitHub UI
1. Go to **Actions** tab
2. Click on workflow run
3. View logs and artifacts

### Via GitHub CLI
```bash
# List recent runs
gh run list --workflow=build-dev.yml
gh run list --workflow=build-and-release.yml

# View specific run
gh run view <run-id>

# Download artifacts
gh run download <run-id>
```

### Via API
```bash
curl -H "Authorization: token $GITHUB_TOKEN" \
  https://api.github.com/repos/USER/REPO/actions/runs
```

---

## Documentation

- **[DUAL-PLATFORM-STRATEGY.md](../DUAL-PLATFORM-STRATEGY.md)** - Detailed platform strategy
- **[CI-CD-SETUP.md](../CI-CD-SETUP.md)** - Complete setup guide
- **[CI-CD-QUICK-REFERENCE.md](../CI-CD-QUICK-REFERENCE.md)** - Command reference
- **[BUILD-PLATFORMS.md](../BUILD-PLATFORMS.md)** - Platform comparison

---

## Best Practices

### ✅ Do
- Use `dev` for daily development (fast Linux builds)
- Use `main` for production releases (reliable Windows builds)
- Create PRs for all changes to `main`
- Tag releases from `main` only
- Test Windows builds before production deployment

### ❌ Don't
- Don't push directly to `main`
- Don't deploy Linux/Mono builds to production without testing
- Don't skip PR validation
- Don't tag releases from `dev`

---

## Getting Help

- Check workflow logs in Actions tab
- Review documentation files
- Open GitHub issue for problems
- Check GitHub Actions docs: https://docs.github.com/en/actions

---

**Status Badges** for README.md:

```markdown
![Dev Build](https://github.com/USER/REPO/actions/workflows/build-dev.yml/badge.svg?branch=dev)
![Production Build](https://github.com/USER/REPO/actions/workflows/build-and-release.yml/badge.svg?branch=main)
```
