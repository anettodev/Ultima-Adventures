# CI/CD Flow Diagram

Visual representation of the automated build and deployment pipeline.

---

## Complete CI/CD Pipeline

```
┌─────────────────────────────────────────────────────────────────────┐
│                         DEVELOPER WORKFLOW                          │
└─────────────────────────────────────────────────────────────────────┘

    Developer                GitHub                Actions              Release            Production
       │                       │                      │                    │                   │
       │                       │                      │                    │                   │
       │  1. Code Changes      │                      │                    │                   │
       ├──────────────────────>│                      │                    │                   │
       │   git push            │                      │                    │                   │
       │                       │                      │                    │                   │
       │                       │  2. Trigger Workflow │                    │                   │
       │                       ├─────────────────────>│                    │                   │
       │                       │                      │                    │                   │
       │                       │                      │  3. Build          │                   │
       │                       │                      ├───────────┐        │                   │
       │                       │                      │           │        │                   │
       │                       │                      │  ┌────────▼──────┐ │                   │
       │                       │                      │  │  Compile      │ │                   │
       │                       │                      │  │  Scripts.dll  │ │                   │
       │                       │                      │  └────────┬──────┘ │                   │
       │                       │                      │           │        │                   │
       │                       │                      │  ┌────────▼──────┐ │                   │
       │                       │                      │  │  Run Tests    │ │                   │
       │                       │                      │  └────────┬──────┘ │                   │
       │                       │                      │           │        │                   │
       │                       │                      │  ┌────────▼──────┐ │                   │
       │                       │                      │  │  Package      │ │                   │
       │                       │                      │  │  Binaries     │ │                   │
       │                       │                      │  └────────┬──────┘ │                   │
       │                       │                      │<──────────┘        │                   │
       │                       │                      │                    │                   │
       │                       │  4. Upload Artifacts │                    │                   │
       │                       │<─────────────────────┤                    │                   │
       │                       │                      │                    │                   │
       │                       │                      │  5. Create Release │                   │
       │                       │                      │   (if tagged)      │                   │
       │                       │                      ├───────────────────>│                   │
       │                       │                      │                    │                   │
       │                       │                      │                    │  6. Download      │
       │                       │                      │                    │     Binaries      │
       │                       │                      │                    ├──────────────────>│
       │                       │                      │                    │                   │
       │                       │                      │                    │  7. Backup        │
       │                       │                      │                    │                   ├────┐
       │                       │                      │                    │                   │    │
       │                       │                      │                    │                   │<───┘
       │                       │                      │                    │                   │
       │                       │                      │                    │  8. Deploy        │
       │                       │                      │                    │                   ├────┐
       │                       │                      │                    │                   │    │
       │                       │                      │                    │                   │<───┘
       │                       │                      │                    │                   │
       │                       │                      │                    │  9. Restart       │
       │                       │                      │                    │                   ├────┐
       │                       │                      │                    │                   │    │
       │  10. Notify           │                      │                    │                   │<───┘
       │<──────────────────────┤                      │                    │                   │
       │   "Deploy Success"    │                      │                    │                   │
       │                       │                      │                    │                   │
```

---

## Branch Strategy

```
┌─────────────────────────────────────────────────────────────────────┐
│                          GIT WORKFLOW                               │
└─────────────────────────────────────────────────────────────────────┘

    feature/new-system          develop              main                releases
           │                       │                   │                     │
           │                       │                   │                     │
     ┌─────▼──────┐               │                   │                     │
     │  Feature   │               │                   │                     │
     │  Branch    │               │                   │                     │
     └─────┬──────┘               │                   │                     │
           │                       │                   │                     │
           │   PR #123             │                   │                     │
           ├──────────────────────>│                   │                     │
           │   (Build & Test)      │                   │                     │
           │                       │                   │                     │
           │                  ┌────▼────┐             │                     │
           │                  │ Develop │             │                     │
           │                  │ (Tested)│             │                     │
           │                  └────┬────┘             │                     │
           │                       │                   │                     │
           │                       │   PR #124         │                     │
           │                       ├──────────────────>│                     │
           │                       │   (Build & Test)  │                     │
           │                       │                   │                     │
           │                       │              ┌────▼────┐               │
           │                       │              │  Main   │               │
           │                       │              │(Stable) │               │
           │                       │              └────┬────┘               │
           │                       │                   │                     │
           │                       │                   │  git tag v1.0.0     │
           │                       │                   ├────────────────────>│
           │                       │                   │                     │
           │                       │                   │                ┌────▼────┐
           │                       │                   │                │ Release │
           │                       │                   │                │ v1.0.0  │
           │                       │                   │                └─────────┘
           │                       │                   │                     │
           │                       │                   │                     │  Auto-deploy
           │                       │                   │                     │  to Production
           │                       │                   │                     ▼
```

---

## Build Process Detail

```
┌─────────────────────────────────────────────────────────────────────┐
│                        BUILD PIPELINE                               │
└─────────────────────────────────────────────────────────────────────┘

 GitHub Actions (Windows Runner)
 ┌────────────────────────────────────────────────────────────────┐
 │                                                                 │
 │  1. Checkout Code                                              │
 │     ├─ Clone repository                                        │
 │     └─ Fetch full history                                      │
 │                                                                 │
 │  2. Setup Build Environment                                    │
 │     ├─ Setup MSBuild                                           │
 │     ├─ Setup NuGet                                             │
 │     └─ Restore packages                                        │
 │                                                                 │
 │  3. Compile                                                     │
 │     ├─ Build Scripts.csproj                                    │
 │     ├─ Target: Release                                         │
 │     ├─ Platform: AnyCPU                                        │
 │     └─ Output: bin/Release/Scripts.dll                         │
 │                                                                 │
 │  4. Verify Build                                               │
 │     ├─ Check Scripts.dll exists                                │
 │     ├─ Check file size                                         │
 │     └─ Validate assembly                                       │
 │                                                                 │
 │  5. Generate Version Info                                      │
 │     ├─ Extract version from tag                                │
 │     ├─ Get commit hash                                         │
 │     ├─ Get build date                                          │
 │     └─ Create VERSION.txt                                      │
 │                                                                 │
 │  6. Package Binaries                                           │
 │     ├─ Create directory structure                              │
 │     ├─ Copy Scripts.dll                                        │
 │     ├─ Copy dependencies (*.dll)                               │
 │     ├─ Copy executables                                        │
 │     ├─ Copy Data/Regions.xml                                   │
 │     ├─ Generate README.md                                      │
 │     └─ Create ZIP archive                                      │
 │                                                                 │
 │  7. Upload Artifacts                                           │
 │     ├─ Full package (.zip)                                     │
 │     └─ Scripts.dll (standalone)                                │
 │                                                                 │
 └────────────────────────────────────────────────────────────────┘
```

---

## Deployment Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                      DEPLOYMENT PIPELINE                            │
└─────────────────────────────────────────────────────────────────────┘

 Production Server
 ┌────────────────────────────────────────────────────────────────┐
 │                                                                 │
 │  1. Pre-Deployment Checks                                      │
 │     ├─ Verify SSH connection                                   │
 │     ├─ Check disk space                                        │
 │     └─ Verify user permissions                                 │
 │                                                                 │
 │  2. Create Backup                                              │
 │     ├─ Timestamp: YYYYMMDD-HHMMSS                             │
 │     ├─ Copy: bin/Scripts.dll                                   │
 │     ├─ To: backups/Scripts.dll.{timestamp}                     │
 │     └─ Keep last 10 backups                                    │
 │                                                                 │
 │  3. Stop Server                                                │
 │     ├─ systemctl stop ultima-server                           │
 │     ├─ Wait for graceful shutdown                              │
 │     └─ Verify stopped                                          │
 │                                                                 │
 │  4. Download Artifacts                                         │
 │     ├─ From: GitHub Release                                    │
 │     ├─ To: /tmp/Scripts.dll                                    │
 │     └─ Verify checksum                                         │
 │                                                                 │
 │  5. Deploy Files                                               │
 │     ├─ Copy: /tmp/Scripts.dll                                  │
 │     ├─ To: bin/Scripts.dll                                     │
 │     ├─ Set ownership: ultima:ultima                            │
 │     └─ Set permissions: 644                                    │
 │                                                                 │
 │  6. Start Server                                               │
 │     ├─ systemctl start ultima-server                          │
 │     ├─ Wait 3 seconds                                          │
 │     └─ Verify running                                          │
 │                                                                 │
 │  7. Verify Deployment                                          │
 │     ├─ Check Scripts.dll exists                                │
 │     ├─ Check server status                                     │
 │     ├─ Check logs for errors                                   │
 │     └─ Test basic connectivity                                 │
 │                                                                 │
 │  8. Cleanup                                                    │
 │     ├─ Remove temp files                                       │
 │     └─ Update deployment log                                   │
 │                                                                 │
 └────────────────────────────────────────────────────────────────┘
```

---

## Rollback Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                       ROLLBACK PROCEDURE                            │
└─────────────────────────────────────────────────────────────────────┘

 If Deployment Fails or Issues Detected
 ┌────────────────────────────────────────────────────────────────┐
 │                                                                 │
 │  1. Detect Issue                                               │
 │     ├─ Server won't start                                      │
 │     ├─ Errors in logs                                          │
 │     ├─ Players can't connect                                   │
 │     └─ Functionality broken                                    │
 │                                                                 │
 │  2. Initiate Rollback                                          │
 │     └─ ./deploy.sh rollback                                    │
 │                                                                 │
 │  3. Find Latest Backup                                         │
 │     ├─ List: backups/Scripts.dll.*                            │
 │     ├─ Sort by timestamp                                       │
 │     └─ Select most recent                                      │
 │                                                                 │
 │  4. Stop Server                                                │
 │     └─ systemctl stop ultima-server                           │
 │                                                                 │
 │  5. Restore Backup                                             │
 │     ├─ Copy: backups/Scripts.dll.{timestamp}                   │
 │     └─ To: bin/Scripts.dll                                     │
 │                                                                 │
 │  6. Start Server                                               │
 │     └─ systemctl start ultima-server                          │
 │                                                                 │
 │  7. Verify Rollback                                            │
 │     ├─ Server running?                                         │
 │     ├─ Players can connect?                                    │
 │     └─ No errors in logs?                                      │
 │                                                                 │
 │  8. Document Incident                                          │
 │     ├─ What failed?                                            │
 │     ├─ Why did it fail?                                        │
 │     ├─ How was it resolved?                                    │
 │     └─ Create GitHub issue                                     │
 │                                                                 │
 └────────────────────────────────────────────────────────────────┘
```

---

## File Structure Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                    SOURCE → BUILD → DEPLOY                          │
└─────────────────────────────────────────────────────────────────────┘

 Development Machine                Build (GitHub)              Production Server
 ┌──────────────────┐              ┌──────────────┐            ┌───────────────────┐
 │                  │              │              │            │                   │
 │  Scripts/        │              │  Scripts.dll │            │  bin/Scripts.dll  │
 │  ├─ 8,269 files  │   Compile    │  (50 MB)     │   Deploy   │  (50 MB)          │
 │  ├─ Core/        │──────────────>│              │───────────>│                   │
 │  ├─ Mobiles/     │   MSBuild    │  Binary only │   Rsync    │  Binary only      │
 │  ├─ Items/       │              │              │            │                   │
 │  └─ Systems/     │              │  + VERSION   │            │  + Backups        │
 │                  │              │  + README    │            │  + Logs           │
 │  580 MB          │              │  130 MB      │            │  130 MB           │
 │  (source code)   │              │  (package)   │            │  (deployed)       │
 │                  │              │              │            │                   │
 └──────────────────┘              └──────────────┘            └───────────────────┘
        │                                 │                            │
        │                                 │                            │
        ▼                                 ▼                            ▼
   Developer edits              GitHub stores release         Server runs binary
   Source files                 Compiled binaries only        No source code
   Full control                 Ready to distribute           Secure & fast
```

---

## Security Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                      SECURITY BOUNDARIES                            │
└─────────────────────────────────────────────────────────────────────┘

 Developer Machine        GitHub (Public)          Production (Private)
 ┌──────────────┐        ┌──────────────┐         ┌──────────────────┐
 │              │        │              │         │                  │
 │  Source Code │ Push   │  Source Code │         │  NO Source Code  │
 │  (Private)   │───────>│  (Public?)   │         │                  │
 │              │        │              │         │                  │
 │  SSH Keys    │        │  Releases    │ Download│  Scripts.dll     │
 │  Credentials │        │  - Binaries  │────────>│  (Binary Only)   │
 │  Secrets     │        │  - No Source │         │                  │
 │              │        │              │         │  + Saves/        │
 │  .env        │        │  Artifacts   │         │  + Logs/         │
 │  .gitignore  │        │  - 90 days   │         │  + backups/      │
 │              │        │              │         │                  │
 └──────────────┘        └──────────────┘         └──────────────────┘
       │                        │                          │
       │                        │                          │
       ▼                        ▼                          ▼
  Full access            Compiled binaries           Binaries only
  Edit source            Public releases             Cannot edit
  Test locally           GitHub Actions              Run in production
```

---

## Monitoring Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                    MONITORING & FEEDBACK                            │
└─────────────────────────────────────────────────────────────────────┘

 Production Server               Monitoring               Alerts
 ┌──────────────┐               ┌──────────┐            ┌──────────┐
 │              │               │          │            │          │
 │  Server      │  Logs         │  systemd │  Failed?   │  Email   │
 │  Running     │──────────────>│  journal │───────────>│  Discord │
 │              │               │          │            │  Slack   │
 │              │               │          │            │          │
 │  Scripts.dll │  Metrics      │  Status  │  Down?     │  SMS     │
 │  Loaded      │──────────────>│  Check   │───────────>│  Phone   │
 │              │               │          │            │          │
 │  Players     │  Activity     │  Monitor │  Spike?    │  Alert   │
 │  Connected   │──────────────>│  Trends  │───────────>│  Team    │
 │              │               │          │            │          │
 └──────────────┘               └──────────┘            └──────────┘
       │                             │                       │
       │                             │                       │
       ▼                             ▼                       ▼
  Generates logs              Analyzes data            Notifies team
  Saves metrics               Detects issues           Enables response
  Reports status              Tracks trends            Quick fixes
```

---

## Summary: End-to-End Flow

```
1. Developer writes code
         ↓
2. Push to GitHub (develop branch)
         ↓
3. GitHub Actions builds & tests (automated)
         ↓
4. Create PR to main
         ↓
5. PR approved & merged to main
         ↓
6. Create release tag (v1.0.0)
         ↓
7. GitHub Actions:
   - Compiles Scripts.dll
   - Runs tests
   - Creates release
   - Uploads binaries
         ↓
8. Deployment (automated or manual):
   - Download binaries
   - Backup current version
   - Stop server
   - Deploy new Scripts.dll
   - Start server
         ↓
9. Monitor logs
         ↓
10. If issues: Rollback
    If success: Celebrate! 🎉
```

---

## Key Benefits

| Stage | Without CI/CD | With CI/CD |
|-------|---------------|------------|
| **Build** | Manual compile on dev machine | Automated build on every push |
| **Test** | Maybe test locally | Automated tests on every PR |
| **Package** | Manually copy files | Automated packaging with versioning |
| **Release** | Email zip file? | GitHub releases with changelog |
| **Deploy** | SSH, copy files, hope it works | Automated deployment with backups |
| **Rollback** | Manual file restore | One command rollback |
| **Time** | 30-60 minutes | 5-10 minutes |
| **Errors** | High risk | Low risk |
| **Consistency** | Varies | Always the same |

---

This visual guide shows how the entire CI/CD pipeline works from code to production!
