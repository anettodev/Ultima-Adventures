# CI/CD Quick Reference

Fast reference guide for common CI/CD operations.

---

## Quick Start

### 1. Create a Release

```bash
# Make sure you're on main and up to date
git checkout main
git pull origin main

# Create and push tag
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0

# Done! GitHub Actions will:
# → Compile Scripts.dll
# → Run tests
# → Create GitHub release
# → Upload binaries
```

### 2. Deploy to Production

**Automated (via GitHub Actions):**
```bash
# Just push to main
git push origin main

# GitHub Actions will:
# → Build and test
# → Deploy to production server (if configured)
```

**Manual (on production server):**
```bash
# Download and run deployment script
sudo ./deploy.sh latest --auto-restart

# Or specific version
sudo ./deploy.sh 1.0.0 --auto-restart
```

### 3. Test a PR

```bash
# Create PR on GitHub
# GitHub Actions automatically:
# → Builds the code
# → Comments on PR with results
# → Shows ✅ or ❌ status
```

---

## Common Commands

### Git Operations

```bash
# Push to develop (triggers build)
git push origin develop

# Create release tag
git tag v1.2.3
git push origin v1.2.3

# Delete tag (if mistake)
git tag -d v1.2.3
git push origin :refs/tags/v1.2.3

# View tags
git tag -l
```

### GitHub CLI

```bash
# Install: https://cli.github.com/

# View releases
gh release list

# Download latest release
gh release download

# Download specific version
gh release download v1.0.0

# View workflow runs
gh run list

# Download artifacts
gh run download <run-id>

# Trigger workflow manually
gh workflow run build-and-release.yml
```

### Server Commands (Production)

```bash
# Check server status
sudo systemctl status ultima-server

# View logs (live)
sudo journalctl -u ultima-server -f

# View last 100 lines
sudo journalctl -u ultima-server -n 100

# Restart server
sudo systemctl restart ultima-server

# Stop server
sudo systemctl stop ultima-server

# Start server
sudo systemctl start ultima-server
```

### Deployment Commands

```bash
# Deploy latest version
sudo ./deploy.sh latest --auto-restart

# Deploy specific version
sudo ./deploy.sh 1.0.0 --auto-restart

# Interactive deployment (asks before restart)
sudo ./deploy.sh 1.0.0

# Rollback to previous version
sudo ./deploy.sh rollback

# Check deployment status
ls -lh /opt/ultima-adventures/bin/Scripts.dll
ls -lh /opt/ultima-adventures/backups/
```

---

## Workflow Triggers

| Workflow | Trigger | Purpose |
|----------|---------|---------|
| **Build and Release** | Push to main/develop | Build and test |
| | Tag `v*.*.*` | Create release |
| | Manual dispatch | On-demand build |
| **Build PR** | Pull request | Validate PR builds |
| **Nightly Build** | Daily at 2 AM UTC | Catch build breaks |
| | Manual dispatch | On-demand nightly |

---

## Version Numbering

### Semantic Versioning

| Type | Version | Example | When to Use |
|------|---------|---------|-------------|
| **Major** | v**X**.0.0 | v2.0.0 | Breaking changes |
| **Minor** | vX.**Y**.0 | v1.1.0 | New features |
| **Patch** | vX.Y.**Z** | v1.0.1 | Bug fixes |
| **Pre-release** | vX.Y.Z-**alpha** | v1.0.0-beta | Testing |

### Examples

```bash
# Bug fix release
git tag v1.0.1 -m "Fix AI targeting bug"

# New feature
git tag v1.1.0 -m "Add new soulbound essence type"

# Breaking change
git tag v2.0.0 -m "Refactor magic system"

# Beta release
git tag v1.0.0-beta.1 -m "Beta release for testing"
```

---

## Artifact Locations

### GitHub Actions Artifacts

1. Go to repository → **Actions**
2. Click on workflow run
3. Scroll to **Artifacts** section
4. Download `ultima-adventures-{version}.zip`

### GitHub Releases

1. Go to repository → **Releases**
2. Find your version
3. Download from **Assets** section

### On Production Server

```bash
# Current version
/opt/ultima-adventures/bin/Scripts.dll

# Backups
/opt/ultima-adventures/backups/Scripts.dll.20260130-143022

# Logs
/opt/ultima-adventures/Logs/
```

---

## Troubleshooting Quick Fixes

### Build Failed

```bash
# Check workflow logs
# Go to Actions → Click failed run → View logs

# Common fixes:
1. Fix compilation errors in code
2. Ensure Scripts.csproj is valid
3. Check namespace issues
```

### Deployment Failed

```bash
# On production server, check:
sudo journalctl -u ultima-server -n 50

# Common fixes:
1. Check file permissions: ls -lh bin/Scripts.dll
2. Verify user ownership: sudo chown -R ultima:ultima /opt/ultima-adventures
3. Check disk space: df -h
4. Verify service is enabled: sudo systemctl is-enabled ultima-server
```

### Server Won't Start After Deployment

```bash
# Rollback immediately
sudo ./deploy.sh rollback

# Or manually
sudo cp backups/Scripts.dll.20260130-143022 bin/Scripts.dll
sudo systemctl restart ultima-server

# Check logs for errors
sudo journalctl -u ultima-server -n 100
```

### Can't Connect After Deployment

```bash
# Check server is running
sudo systemctl status ultima-server

# Check ports
sudo netstat -tuln | grep 2593  # Default UO port

# Check firewall
sudo ufw status
sudo firewall-cmd --list-all

# Check world saved properly
ls -lh Saves/
```

---

## File Structure Quick Reference

### Source Repository

```
Ultima-Adventures/
├── .github/workflows/        # CI/CD workflows
├── Server/                   # Core engine (don't touch)
├── Scripts/                  # Game logic (8,269 files)
│   ├── Configuration/        # Server config
│   ├── Core/                 # Core systems
│   ├── Mobiles/              # Creatures + AI
│   ├── Items/                # All items
│   └── Systems/              # Game systems
├── Data/                     # XML configs
├── deploy/                   # Deployment scripts
└── CI-CD-SETUP.md           # Full documentation
```

### Production Server

```
/opt/ultima-adventures/
├── bin/
│   ├── Scripts.dll          # ← Deployed binary
│   └── *.dll                # Dependencies
├── Data/
│   └── Regions.xml
├── Files/                   # UO client data
├── Saves/                   # World save
├── Logs/                    # Server logs
├── backups/                 # Script backups
│   └── Scripts.dll.*
├── LinuxServer.exe          # Server executable
└── deploy.sh                # Deployment script
```

### GitHub Release Package

```
ultima-adventures-v1.0.0.zip
├── bin/Scripts.dll          # Compiled binary
├── Data/Regions.xml         # Configs
├── WindowsServer.exe        # Executables
├── LinuxServer.exe
├── VERSION.txt              # Build info
├── README.md                # Deploy instructions
└── deploy.sh                # Deployment script
```

---

## Security Checklist

### Repository Secrets

- [ ] `SSH_PRIVATE_KEY` - Server SSH key
- [ ] `PRODUCTION_HOST` - Server hostname
- [ ] `PRODUCTION_USER` - SSH username
- [ ] `PRODUCTION_PATH` - Install path

### Server Security

- [ ] SSH key authentication (no passwords)
- [ ] Firewall configured (UFW/firewalld)
- [ ] Limited user permissions (don't run as root)
- [ ] Regular backups enabled
- [ ] HTTPS for web admin (if applicable)

### GitHub Security

- [ ] Branch protection on `main`
- [ ] Require PR reviews
- [ ] Require status checks
- [ ] Signed commits (optional)
- [ ] Environment protection rules

---

## Useful Links

### Documentation

- [Full CI/CD Setup Guide](./CI-CD-SETUP.md)
- [Refactoring Proposal](./REFACTORING-PROPOSAL.md)
- [Project Instructions](./CLAUDE.md)

### GitHub

- [Actions Documentation](https://docs.github.com/en/actions)
- [Workflow Syntax](https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions)
- [GitHub CLI](https://cli.github.com/)

### Tools

- [Semantic Versioning](https://semver.org/)
- [GitHub Actions Marketplace](https://github.com/marketplace?type=actions)
- [MSBuild Reference](https://docs.microsoft.com/en-us/visualstudio/msbuild/)

---

## Daily Workflow

### Developer

```bash
# Morning
git checkout develop
git pull origin develop

# Work
# ... code changes ...
git add .
git commit -m "feat: Add new feature"
git push origin develop
# → Auto-builds and tests

# Create PR
gh pr create --base main --head develop

# After PR approved and merged
git checkout main
git pull origin main
git tag v1.1.0
git push origin v1.1.0
# → Auto-releases
```

### Server Administrator

```bash
# Morning - Check server status
sudo systemctl status ultima-server
sudo journalctl -u ultima-server --since "1 hour ago"

# Deploy new version when ready
sudo ./deploy.sh latest --auto-restart

# Monitor deployment
sudo journalctl -u ultima-server -f

# If issues, rollback
sudo ./deploy.sh rollback
```

---

## Emergency Procedures

### Server Crash

```bash
# 1. Check status
sudo systemctl status ultima-server

# 2. View crash logs
sudo journalctl -u ultima-server -n 200

# 3. Try restart
sudo systemctl restart ultima-server

# 4. If still failing, rollback
sudo ./deploy.sh rollback

# 5. Notify developers
# Create GitHub issue with logs
```

### Build Pipeline Broken

```bash
# 1. Check Actions tab for errors
# 2. Review failed workflow logs
# 3. Fix in hotfix branch
git checkout -b hotfix/build-fix
# ... fix ...
git commit -m "fix: Resolve build error"
git push origin hotfix/build-fix

# 4. Create PR and merge quickly
gh pr create --base main

# 5. Verify build passes
```

### Accidental Production Deploy

```bash
# 1. Immediately rollback
sudo ./deploy.sh rollback

# 2. Verify rollback successful
sudo systemctl status ultima-server

# 3. Document incident
# Create post-mortem issue

# 4. Fix deployment workflow if needed
# Add approval gates, better testing
```

---

## Metrics to Monitor

### Build Metrics

- ✅ Build success rate (target: >95%)
- ⏱️ Build duration (track trends)
- 📦 Artifact size (watch for bloat)
- 🔄 Build frequency

### Deployment Metrics

- ✅ Deployment success rate (target: >99%)
- ⏱️ Deployment duration
- 🔙 Rollback frequency (target: <5%)
- ⏰ Deployment downtime

### Server Metrics

- 🖥️ CPU usage
- 💾 Memory usage
- 💿 Disk space
- 👥 Active players
- ⚠️ Error rate in logs

---

## Best Practices Summary

✅ **DO:**
- Tag releases with semantic versions
- Test on develop before merging to main
- Create backups before deploying
- Monitor logs after deployment
- Use branch protection
- Document changes in commits

❌ **DON'T:**
- Push directly to main
- Skip testing
- Deploy on Fridays (unless necessary)
- Ignore failed builds
- Commit secrets
- Deploy without backups

---

**Need Help?** See [CI-CD-SETUP.md](./CI-CD-SETUP.md) for detailed documentation.
