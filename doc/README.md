# NMS UO Server Documentation

Complete documentation for NMS UO Server project management, CI/CD, and refactoring.

---

## 📚 Documentation Index

### Project Overview
- **[../CLAUDE.md](../CLAUDE.md)** - Project instructions for AI assistance
- **[../README.md](../README.md)** - Main project README (if exists)

### Refactoring & Migration
- **[REFACTORING-PROPOSAL.md](./REFACTORING-PROPOSAL.md)** - Complete refactoring strategy (8,269 → 800 files)
- **[TEST-SCRIPTS-MIGRATION.md](./TEST-SCRIPTS-MIGRATION.md)** - Experimental content migration strategy
- **[FOLDER-STRUCTURE.md](./FOLDER-STRUCTURE.md)** - Proposed optimal folder organization

### CI/CD Documentation
- **[CI-CD-SETUP.md](./CI-CD-SETUP.md)** - Complete CI/CD setup guide
- **[CI-CD-QUICK-REFERENCE.md](./CI-CD-QUICK-REFERENCE.md)** - Quick command reference
- **[CI-CD-FLOW-DIAGRAM.md](./CI-CD-FLOW-DIAGRAM.md)** - Visual workflow diagrams
- **[DUAL-PLATFORM-STRATEGY.md](./DUAL-PLATFORM-STRATEGY.md)** - Dev/Production platform strategy
- **[BUILD-PLATFORMS.md](./BUILD-PLATFORMS.md)** - Windows vs Linux build comparison
- **[RETENTION-AND-SCHEDULE.md](./RETENTION-AND-SCHEDULE.md)** - Artifact retention & build schedules

### Deployment
- **[DEPLOYMENT-GUIDE.md](./DEPLOYMENT-GUIDE.md)** - Production deployment procedures
- **[../deploy/deploy.sh](../deploy/deploy.sh)** - Automated deployment script
- **[../deploy/nms-uoserver.service](../deploy/nms-uoserver.service)** - Systemd service file

---

## Quick Links

### Getting Started
1. Read [CI-CD-SETUP.md](./CI-CD-SETUP.md) to set up automation
2. Review [DUAL-PLATFORM-STRATEGY.md](./DUAL-PLATFORM-STRATEGY.md) for build strategy
3. Check [CI-CD-QUICK-REFERENCE.md](./CI-CD-QUICK-REFERENCE.md) for common commands

### Planning Refactor
1. Review [REFACTORING-PROPOSAL.md](./REFACTORING-PROPOSAL.md) for overall strategy
2. Read [TEST-SCRIPTS-MIGRATION.md](./TEST-SCRIPTS-MIGRATION.md) for experimental content
3. Check [FOLDER-STRUCTURE.md](./FOLDER-STRUCTURE.md) for target structure

### Deploying
1. Read [DEPLOYMENT-GUIDE.md](./DEPLOYMENT-GUIDE.md)
2. Configure secrets (see CI-CD-SETUP.md)
3. Use `../deploy/deploy.sh` for deployment

---

## Project Information

**Project Name:** NMS UO Server
**Forked From:** Ultima Odyssey (ServUO/RunUO base)
**Target Framework:** .NET Framework 4.8
**Repository:** nms-uoserver

### Deployment Paths
- **Production:** `/opt/nms-uoserver`
- **Development:** `/opt/nms-uoserver` (can be same or different server)
- **Service Name:** `nms-uoserver`
- **System User:** `nms:nms`

### Branches
- **`main`** - Production releases (Windows builds)
- **`dev`** - Active development (Linux builds)

### Build Strategy
- **Dev Branch:** Linux/Mono (fast iteration)
- **Main Branch:** Windows/MSBuild (production reliability)
- **Nightly Builds:** Monday & Thursday 3 AM UTC on dev

### Artifact Retention
- **Dev builds:** 5 days
- **Production builds:** 30 days
- **Nightly builds:** 5 days
- **GitHub Releases:** Permanent

---

## File Organization

```
nms-uoserver/
├── doc/                      # THIS FOLDER - All documentation
│   ├── README.md             # This file
│   ├── CI-CD-*.md            # CI/CD documentation
│   ├── REFACTORING-*.md      # Refactoring guides
│   └── *.md                  # Other documentation
│
├── .github/workflows/        # GitHub Actions
│   ├── build-dev.yml         # Dev builds (Linux)
│   ├── build-and-release.yml # Production (Windows)
│   ├── build-pr.yml          # PR validation
│   └── nightly-build.yml     # Nightly builds
│
├── deploy/                   # Deployment scripts
│   ├── deploy.sh             # Deployment script
│   └── nms-uoserver.service  # Systemd service
│
├── Server/                   # Core engine (122 files)
├── Scripts/                  # Game logic (8,269 files)
│   ├── Configuration/        # [Proposed] Config files
│   ├── Core/                 # Core systems
│   ├── Mobiles/              # Creatures + AI
│   ├── Items/                # All items
│   ├── Systems/              # Game systems
│   ├── Custom/               # [Proposed] Server-specific
│   └── test-scripts/         # [Proposed] Experimental
│
├── Data/                     # World data, spawns
├── Files/                    # UO client files
└── CLAUDE.md                 # AI assistant instructions
```

---

## Current Status

### Completed ✅
- [x] Dual-platform CI/CD strategy (Linux for dev, Windows for prod)
- [x] Automated builds on push/PR/tags
- [x] Artifact retention optimized (5/30 days)
- [x] Nightly builds (Mon/Thu 3 AM UTC)
- [x] GitHub release automation
- [x] Project renamed to nms-uoserver
- [x] Paths updated to /opt/nms-uoserver
- [x] Documentation organized in /doc
- [x] Deployment scripts updated

### Planned 📋
- [ ] Implement test-scripts migration strategy
- [ ] Refactor to optimal folder structure
- [ ] Reduce from 8,269 to ~800 essential files
- [ ] Separate experimental content
- [ ] Set up automated production deployment

---

## Key Concepts

### Test-Scripts Strategy
**Purpose:** Gradually evaluate experimental content before production

**Approach:**
- Move experimental/non-vital content to `Scripts/test-scripts/`
- Test in isolated environment
- Migrate to production folders only after validation
- Keep production lean and tested

See [TEST-SCRIPTS-MIGRATION.md](./TEST-SCRIPTS-MIGRATION.md) for details.

### Dual-Platform Builds
**Dev (Linux/Mono):**
- Fast iteration (~2-3 min)
- Lower cost
- Development testing only

**Main (Windows/MSBuild):**
- Production reliability (~3-5 min)
- 100% .NET Framework 4.8 compatibility
- Official releases

See [DUAL-PLATFORM-STRATEGY.md](./DUAL-PLATFORM-STRATEGY.md) for details.

### Artifact Retention
**Short retention (5 days):**
- Dev builds - rapid iteration
- Nightly builds - recent only

**Medium retention (30 days):**
- Production builds - recent releases

**Permanent:**
- GitHub Releases - official versions

See [RETENTION-AND-SCHEDULE.md](./RETENTION-AND-SCHEDULE.md) for details.

---

## Contributing

### Development Workflow
```bash
# Work on dev branch
git checkout dev
# Make changes
git commit -m "feat: Add new feature"
git push origin dev
# → Triggers Linux build (~2-3 min)

# Create PR to main when ready
gh pr create --base main --head dev
# → Triggers Windows build (~3-5 min)

# After merge, tag release
git checkout main
git tag v1.0.0
git push origin v1.0.0
# → Creates GitHub Release
```

### CI/CD Commands
```bash
# View builds
gh run list

# Trigger manual build
gh workflow run build-dev.yml

# Download artifacts
gh run download <run-id>
```

### Deployment
```bash
# Deploy to production
sudo /opt/nms-uoserver/deploy/deploy.sh latest --auto-restart

# Check status
sudo systemctl status nms-uoserver

# View logs
sudo journalctl -u nms-uoserver -f
```

---

## Support & Resources

### Documentation Files
All documentation is in this `/doc` folder - see index above

### GitHub Resources
- **Actions Tab** - View build history
- **Releases** - Download production binaries
- **Issues** - Track build failures, bugs

### External Links
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [ServUO Documentation](https://www.servuo.com)
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

---

## Version History

### v0.1.0 (Current)
- Initial documentation structure
- CI/CD pipeline implemented
- Dual-platform strategy
- Project renamed to nms-uoserver

---

**Last Updated:** 2026-01-30
**Maintained By:** NMS Development Team
