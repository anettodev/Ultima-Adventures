# CI/CD Setup Guide - Ultima Adventures

Complete guide for automated builds, testing, and deployment using GitHub Actions.

---

## Table of Contents

1. [Overview](#overview)
2. [Workflows](#workflows)
3. [Setup Instructions](#setup-instructions)
4. [GitHub Secrets Configuration](#github-secrets-configuration)
5. [Release Process](#release-process)
6. [Deployment](#deployment)
7. [Troubleshooting](#troubleshooting)

---

## Overview

The CI/CD pipeline automates:
- ✅ **Building** - Compiles Scripts.dll from source
- ✅ **Testing** - Verifies build integrity
- ✅ **Packaging** - Creates production-ready binary packages
- ✅ **Releasing** - Publishes GitHub releases with binaries
- ✅ **Deploying** - Optionally deploys to production server

### Why This Matters

**Without CI/CD:**
```
Developer → Manual compile → Manual test → Manual copy to server → Manual restart
(Error-prone, slow, inconsistent)
```

**With CI/CD:**
```
Developer → Push to GitHub → Auto compile → Auto test → Auto package → Auto deploy
(Fast, consistent, reliable)
```

---

## Workflows

### 1. Build and Release (`.github/workflows/build-and-release.yml`)

**Triggers:**
- Push to `main` or `develop` branches
- New tags matching `v*.*.*` (e.g., `v1.0.0`)
- Manual workflow dispatch

**Jobs:**
1. **Build** - Compiles Scripts.dll on Windows runner
2. **Test** - Verifies package integrity
3. **Release** - Creates GitHub release (only for tags)
4. **Deploy** - Deploys to production (only for main branch)

**Outputs:**
- `Scripts.dll` - Compiled game logic
- `ultima-adventures-{version}.zip` - Full package with binaries
- GitHub Release with changelog

### 2. Build PR (`.github/workflows/build-pr.yml`)

**Triggers:**
- Pull requests to `main` or `develop`

**Purpose:**
- Validates PR builds successfully
- Comments on PR with build results
- Prevents merging broken code

**Outputs:**
- PR comment with build status
- Build artifacts (for testing)

### 3. Nightly Build (`.github/workflows/nightly-build.yml`)

**Triggers:**
- Daily at 2 AM UTC
- Manual workflow dispatch

**Purpose:**
- Catches build breaks early
- Provides bleeding-edge builds for testing
- Creates GitHub issues on failure

**Outputs:**
- Nightly build artifact (7-day retention)
- Issue created if build fails

---

## Setup Instructions

### Step 1: Enable GitHub Actions

1. Go to your repository on GitHub
2. Click **Settings** → **Actions** → **General**
3. Under "Actions permissions", select:
   - ✅ **Allow all actions and reusable workflows**
4. Under "Workflow permissions", select:
   - ✅ **Read and write permissions**
5. Click **Save**

### Step 2: Add Workflow Files

The following files should already exist:
```
.github/workflows/
├── build-and-release.yml    # Main build pipeline
├── build-pr.yml              # PR validation
└── nightly-build.yml         # Nightly builds
```

If not, create them from this guide.

### Step 3: Configure Repository Secrets

See [GitHub Secrets Configuration](#github-secrets-configuration) below.

### Step 4: Test the Pipeline

**Option A: Push to develop**
```bash
git checkout develop
git commit --allow-empty -m "Test CI/CD pipeline"
git push origin develop
```

**Option B: Create a test tag**
```bash
git tag v0.1.0-test
git push origin v0.1.0-test
```

**Option C: Manual trigger**
1. Go to **Actions** tab
2. Select **Build and Release** workflow
3. Click **Run workflow**
4. Choose branch and release type
5. Click **Run workflow**

### Step 5: Verify Success

1. Go to **Actions** tab
2. Click on the workflow run
3. Verify all jobs are green ✅
4. Check **Artifacts** section for builds
5. Check **Releases** if you created a tag

---

## GitHub Secrets Configuration

### Required Secrets

Go to **Settings** → **Secrets and variables** → **Actions** → **New repository secret**

#### For Production Deployment (Optional)

| Secret Name | Description | Example |
|------------|-------------|---------|
| `SSH_PRIVATE_KEY` | SSH private key for server access | `-----BEGIN RSA PRIVATE KEY-----...` |
| `PRODUCTION_HOST` | Production server hostname/IP | `ultima.example.com` or `192.168.1.100` |
| `PRODUCTION_USER` | SSH username | `ubuntu` or `ultima-server` |
| `PRODUCTION_PATH` | Server installation path | `/opt/ultima-adventures` |

#### Optional Secrets

| Secret Name | Description | Example |
|------------|-------------|---------|
| `DISCORD_WEBHOOK` | Discord webhook for notifications | `https://discord.com/api/webhooks/...` |
| `SLACK_WEBHOOK` | Slack webhook for notifications | `https://hooks.slack.com/services/...` |

### How to Generate SSH Key for Deployment

```bash
# On your local machine
ssh-keygen -t rsa -b 4096 -C "github-actions" -f github-actions-key

# Copy the PUBLIC key to your server
ssh-copy-id -i github-actions-key.pub user@server.com

# OR manually append to authorized_keys
cat github-actions-key.pub | ssh user@server.com "mkdir -p ~/.ssh && cat >> ~/.ssh/authorized_keys"

# Copy the PRIVATE key contents
cat github-actions-key
# Copy the entire output including -----BEGIN/END-----

# Add to GitHub Secrets as SSH_PRIVATE_KEY
```

### Environment Protection Rules (Optional)

For production deployments, add protection:

1. Go to **Settings** → **Environments** → **New environment**
2. Name: `production`
3. Configure protection rules:
   - ✅ **Required reviewers** (add team members)
   - ✅ **Wait timer** (e.g., 5 minutes)
4. Click **Save protection rules**

Now production deploys require manual approval.

---

## Release Process

### Semantic Versioning

Use semantic versioning: `vMAJOR.MINOR.PATCH`

- **MAJOR** - Breaking changes (v2.0.0)
- **MINOR** - New features (v1.1.0)
- **PATCH** - Bug fixes (v1.0.1)

### Creating a Release

#### Method 1: Git Tags (Recommended)

```bash
# Ensure you're on the main branch
git checkout main
git pull origin main

# Create and push a tag
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0
```

This automatically:
1. Triggers build workflow
2. Compiles Scripts.dll
3. Runs tests
4. Creates GitHub release
5. Uploads artifacts

#### Method 2: GitHub Releases UI

1. Go to **Releases** → **Draft a new release**
2. Click **Choose a tag** → Create new tag `v1.0.0`
3. **Target:** `main` branch
4. **Release title:** `Ultima Adventures v1.0.0`
5. **Description:** Write changelog
6. Click **Publish release**

This triggers the same workflow.

#### Method 3: Manual Workflow

1. Go to **Actions** → **Build and Release**
2. Click **Run workflow**
3. Choose branch: `main`
4. Choose release type: `production` or `beta`
5. Click **Run workflow**

### Release Artifacts

Each release includes:

```
ultima-adventures-v1.0.0.zip
├── bin/
│   ├── Scripts.dll           ← Compiled game logic
│   ├── OrbServerSDK.dll
│   ├── UOArchitectInterface.dll
│   └── *.dll
├── Data/
│   ├── Regions.xml
│   └── *.xml
├── Saves/                    ← Empty directory
├── Logs/                     ← Empty directory
├── WindowsServer.exe
├── LinuxServer.exe
├── VERSION.txt
├── README.md
└── deploy.sh
```

**Also available:**
- `Scripts.dll` (standalone, for quick updates)

---

## Deployment

### Automated Deployment (Production)

**Trigger:** Push to `main` branch

**Process:**
1. Build completes successfully
2. Tests pass
3. SSH into production server
4. Backup current `Scripts.dll`
5. Upload new binaries via rsync
6. Optionally restart server

**Safety:**
- Backups created: `backups/Scripts.dll.YYYYMMDD-HHMMSS`
- Existing saves NOT touched
- Can rollback easily

### Manual Deployment

#### Option 1: Download from GitHub Release

```bash
# On production server
cd /opt/ultima-adventures

# Backup current version
cp bin/Scripts.dll backups/Scripts.dll.$(date +%Y%m%d)

# Download and extract release
wget https://github.com/YOUR_ORG/Ultima-Adventures/releases/download/v1.0.0/ultima-adventures-v1.0.0.zip
unzip ultima-adventures-v1.0.0.zip -d temp/

# Copy binaries
cp temp/bin/* bin/

# Restart server
systemctl restart ultima-server
```

#### Option 2: Download from Artifacts

```bash
# Download artifact from Actions tab
# Or use GitHub CLI
gh run download 123456789 -n ultima-adventures-v1.0.0

# Extract and deploy as above
```

#### Option 3: Hot-swap Scripts.dll Only

```bash
# For quick updates (no dependency changes)
cd /opt/ultima-adventures

# Backup
cp bin/Scripts.dll backups/Scripts.dll.backup

# Download only Scripts.dll
wget https://github.com/YOUR_ORG/Ultima-Adventures/releases/download/v1.0.0/Scripts.dll
mv Scripts.dll bin/Scripts.dll

# Restart
systemctl restart ultima-server
```

### Rollback Procedure

```bash
# If new version has issues
cd /opt/ultima-adventures

# Restore previous version
cp backups/Scripts.dll.20260130 bin/Scripts.dll

# Restart
systemctl restart ultima-server
```

---

## Workflow Details

### Build Job

**Platform:** Windows (required for .NET Framework 4.8)

**Steps:**
1. Checkout code
2. Setup MSBuild + NuGet
3. Restore packages
4. Build `Scripts.csproj` → `Scripts.dll`
5. Verify output exists
6. Generate version info
7. Package binaries
8. Upload artifacts

**Build Command:**
```bash
msbuild Scripts/Scripts.csproj
  /p:Configuration=Release
  /p:Platform=AnyCPU
  /p:OutputPath=../bin/Release/
```

### Test Job

**Purpose:** Verify package integrity

**Checks:**
- ✅ Scripts.dll exists
- ✅ Dependencies included
- ✅ Version file present
- ✅ README included
- ✅ Package structure valid

### Release Job

**Trigger:** Only for tags (`refs/tags/v*`)

**Creates:**
- GitHub Release
- Changelog from git commits
- Uploads artifacts to release

**Changelog Generation:**
```bash
git log --pretty=format:"- %s (%h)" v1.0.0..v1.1.0
```

### Deploy Job

**Trigger:** Push to `main` branch

**Environment:** `production` (requires approval if configured)

**Process:**
1. Download build artifacts
2. Setup SSH connection
3. Backup current version on server
4. Rsync new binaries
5. Optionally restart server

**Safety Features:**
- Backups before deploy
- Dry-run option
- Manual approval required
- Rollback capability

---

## Version Numbering

### Tagged Releases
```
v1.2.3 → Version: 1.2.3
```

### Branch Builds
```
main branch → Version: 2026.01.30-a1b2c3d
develop branch → Version: 2026.01.30-a1b2c3d
```

### Nightly Builds
```
nightly-2026.01.30-a1b2c3d
```

---

## Advanced Usage

### Building Locally with Same Process

```bash
# Install MSBuild (Windows)
# Or install Visual Studio Build Tools

# Run build
msbuild Scripts/Scripts.csproj /p:Configuration=Release /p:Platform=AnyCPU /p:OutputPath=../bin/Release/

# Output: bin/Release/Scripts.dll
```

### Triggering Builds via API

```bash
# Using GitHub CLI
gh workflow run build-and-release.yml --ref main

# Using curl
curl -X POST \
  -H "Accept: application/vnd.github+json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  https://api.github.com/repos/YOUR_ORG/Ultima-Adventures/actions/workflows/build-and-release.yml/dispatches \
  -d '{"ref":"main","inputs":{"release_type":"beta"}}'
```

### Custom Deployment Script

Create `.github/workflows/deploy-custom.yml`:

```yaml
name: Deploy to Custom Server

on:
  workflow_dispatch:
    inputs:
      server:
        description: 'Target server'
        required: true
        type: choice
        options:
          - staging
          - production
          - test

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Deploy to ${{ github.event.inputs.server }}
        run: |
          echo "Deploying to ${{ github.event.inputs.server }}"
          # Your custom deployment logic here
```

---

## Monitoring and Notifications

### Viewing Build Status

**Badge in README:**
```markdown
![Build Status](https://github.com/YOUR_ORG/Ultima-Adventures/actions/workflows/build-and-release.yml/badge.svg)
```

**Actions Tab:**
- Go to repository → **Actions**
- See all workflow runs
- Filter by workflow, status, branch

### Email Notifications

GitHub automatically emails you for:
- Failed builds on your branches
- Failed builds on protected branches
- Workflow runs you triggered

**Configure:**
Settings → Notifications → Actions

### Discord Notifications (Optional)

Add to workflow:

```yaml
- name: Notify Discord
  if: always()
  uses: sarisia/actions-status-discord@v1
  with:
    webhook: ${{ secrets.DISCORD_WEBHOOK }}
    status: ${{ job.status }}
    title: "Build ${{ job.status }}"
    description: "Version ${{ steps.version.outputs.version }}"
```

---

## Troubleshooting

### Build Fails with "MSBuild not found"

**Cause:** Running on Linux runner instead of Windows

**Fix:** Ensure workflow uses `runs-on: windows-latest`

### "Scripts.dll not found" after build

**Cause:** Wrong output path

**Fix:** Check `/p:OutputPath=../bin/Release/` matches your project structure

### SSH Connection Failed in Deploy

**Possible causes:**
1. Wrong SSH key format
2. Missing newlines in secret
3. Host key not in known_hosts

**Fix:**
```yaml
- name: Debug SSH
  run: |
    ssh -vvv ${{ secrets.PRODUCTION_USER }}@${{ secrets.PRODUCTION_HOST }} echo "Connected"
```

### "Permission denied" on Deployment

**Cause:** User doesn't have write access to deployment directory

**Fix:**
```bash
# On server
sudo chown -R ubuntu:ubuntu /opt/ultima-adventures
sudo chmod -R 755 /opt/ultima-adventures
```

### Release Not Created

**Cause:** Tag not pushed or wrong tag format

**Fix:**
```bash
# Ensure tag matches v*.*.* pattern
git tag v1.0.0  # ✅ Correct
git tag 1.0.0   # ❌ Wrong
git tag release-1.0.0  # ❌ Wrong

# Push tag
git push origin v1.0.0
```

### Artifacts Not Uploading

**Cause:** File path doesn't exist

**Fix:** Add debug step:
```yaml
- name: Debug files
  run: |
    ls -R bin/
    ls -R ultima-adventures-*/
```

---

## Best Practices

### 1. Use Branches

```
main          → Production releases only
develop       → Active development
feature/*     → New features
hotfix/*      → Emergency fixes
```

### 2. Protect Main Branch

**Settings** → **Branches** → **Add rule**
- Branch name pattern: `main`
- ✅ Require pull request before merging
- ✅ Require status checks to pass
- ✅ Require conversation resolution before merging

### 3. Test Before Tagging

```bash
# Develop and test on develop branch
git checkout develop
# ... make changes ...
git push origin develop

# Merge to main when ready
git checkout main
git merge develop
git push origin main

# Create release tag
git tag v1.0.0
git push origin v1.0.0
```

### 4. Semantic Commit Messages

```bash
git commit -m "feat: Add new soulbound essence type"
git commit -m "fix: Resolve AI targeting bug"
git commit -m "refactor: Reorganize magic system"
git commit -m "docs: Update CI/CD documentation"
```

### 5. Keep Secrets Secure

- ❌ Never commit secrets to repository
- ✅ Use GitHub Secrets
- ✅ Rotate SSH keys periodically
- ✅ Use environment-specific secrets

---

## Summary

**Setup Checklist:**
- [ ] Enable GitHub Actions
- [ ] Add workflow files
- [ ] Configure secrets (if deploying)
- [ ] Test with manual trigger
- [ ] Create first release tag
- [ ] Verify artifacts generated
- [ ] Document your process

**Daily Workflow:**
```bash
# Develop
git checkout develop
# ... make changes ...
git commit -m "feat: Add new feature"
git push origin develop
# → Auto-builds and tests

# Release
git checkout main
git merge develop
git tag v1.0.0
git push origin v1.0.0
# → Auto-builds, tests, releases, deploys
```

**Result:**
- ✅ Automated builds on every push
- ✅ Binary-only releases (no source code)
- ✅ One-click deployments
- ✅ Rollback capability
- ✅ Professional CI/CD pipeline

---

**Questions?** Check the [GitHub Actions Documentation](https://docs.github.com/en/actions) or open an issue.
