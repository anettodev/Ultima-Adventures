# NMS UO Server - Deployment Guide

Complete guide for deploying NMS UO Server to production and development environments.

---

## Server Information

**Project Name:** NMS UO Server
**Service Name:** `nms-uoserver`
**Install Path:** `/opt/nms-uoserver`
**System User:** `nms:nms`
**Port:** 2593 (default UO port)

---

## Prerequisites

### System Requirements

**Minimum:**
- CPU: 2 cores
- RAM: 2GB
- Disk: 10GB
- OS: Ubuntu 20.04+ / Debian 11+ / CentOS 8+

**Recommended:**
- CPU: 4+ cores
- RAM: 4GB+
- Disk: 20GB+ (SSD preferred)
- OS: Ubuntu 22.04 LTS

### Software Requirements

**For Linux (Production):**
```bash
# Mono runtime (for LinuxServer.exe)
sudo apt-get install -y mono-complete

# Or via Mono repository for latest version:
sudo apt install -y gnupg ca-certificates
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb https://download.mono-project.com/repo/ubuntu stable-focal main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
sudo apt update
sudo apt install -y mono-complete

# Verify installation
mono --version  # Should be 6.0+
```

**For Windows (Development/Testing):**
- .NET Framework 4.8 Runtime
- Windows Server 2016+

---

## Installation

### 1. Create System User

```bash
# Create dedicated user for security
sudo useradd -r -m -d /opt/nms-uoserver -s /bin/bash nms

# Set password (optional, for direct login)
sudo passwd nms
```

### 2. Create Directory Structure

```bash
# Create server directories
sudo mkdir -p /opt/nms-uoserver/{bin,Data,Files,Saves,Logs,backups,deploy}

# Set ownership
sudo chown -R nms:nms /opt/nms-uoserver

# Set permissions
sudo chmod 755 /opt/nms-uoserver
sudo chmod 750 /opt/nms-uoserver/Saves
sudo chmod 750 /opt/nms-uoserver/Logs
```

### 3. Download Release

**Option A: From GitHub Release**
```bash
# Download latest release
cd /tmp
wget https://github.com/YOUR_ORG/nms-uoserver/releases/latest/download/nms-uoserver-v1.0.0.zip

# Extract to server directory
sudo -u nms unzip nms-uoserver-v1.0.0.zip -d /opt/nms-uoserver/

# Verify
ls -lh /opt/nms-uoserver/bin/Scripts.dll
```

**Option B: Using Deployment Script**
```bash
# Copy deployment script
sudo cp deploy.sh /opt/nms-uoserver/deploy/
sudo chmod +x /opt/nms-uoserver/deploy/deploy.sh

# Run deployment
sudo /opt/nms-uoserver/deploy/deploy.sh latest
```

### 4. Install UO Client Files

```bash
# Copy UO client files to Files/ directory
# These are NOT included in releases due to size

# Option 1: Copy from existing installation
sudo cp -r /path/to/uo/client/files/* /opt/nms-uoserver/Files/

# Option 2: Download UO client and extract
# (Download Ultima Online client from official source)

# Verify critical files exist
ls /opt/nms-uoserver/Files/map*.mul
ls /opt/nms-uoserver/Files/tiledata.mul
ls /opt/nms-uoserver/Files/Cliloc.enu

# Set ownership
sudo chown -R nms:nms /opt/nms-uoserver/Files/
```

### 5. Configure Server

```bash
# Edit configuration (if needed)
# Most settings are in Data/Regions.xml and Scripts/Configuration/

# Example: Edit server name, ports, etc.
# This depends on your specific configuration needs
```

### 6. Install Systemd Service

```bash
# Copy service file
sudo cp /opt/nms-uoserver/deploy/nms-uoserver.service /etc/systemd/system/

# Reload systemd
sudo systemctl daemon-reload

# Enable service (start on boot)
sudo systemctl enable nms-uoserver

# Start service
sudo systemctl start nms-uoserver

# Check status
sudo systemctl status nms-uoserver
```

### 7. Verify Installation

```bash
# Check if server is running
sudo systemctl status nms-uoserver

# Check logs
sudo journalctl -u nms-uoserver -n 50

# Check if port is listening
sudo netstat -tuln | grep 2593

# Or with ss:
sudo ss -tuln | grep 2593
```

---

## Configuration

### Firewall Setup

```bash
# UFW (Ubuntu/Debian)
sudo ufw allow 2593/tcp
sudo ufw reload

# firewalld (CentOS/RHEL)
sudo firewall-cmd --permanent --add-port=2593/tcp
sudo firewall-cmd --reload

# Verify
sudo ufw status  # Ubuntu
sudo firewall-cmd --list-all  # CentOS
```

### Server Settings

**Main configuration file:** `Scripts/Configuration/ServerSettings.cs`

Key settings:
- Server name
- Port (default 2593)
- Max players
- World save interval

**After changing configuration:**
```bash
# Recompile Scripts.dll (or download new release)
# Then restart server
sudo systemctl restart nms-uoserver
```

---

## Daily Operations

### Starting/Stopping Server

```bash
# Start server
sudo systemctl start nms-uoserver

# Stop server
sudo systemctl stop nms-uoserver

# Restart server
sudo systemctl restart nms-uoserver

# Check status
sudo systemctl status nms-uoserver
```

### Viewing Logs

```bash
# Real-time logs (follow)
sudo journalctl -u nms-uoserver -f

# Last 100 lines
sudo journalctl -u nms-uoserver -n 100

# Since specific time
sudo journalctl -u nms-uoserver --since "1 hour ago"
sudo journalctl -u nms-uoserver --since "2026-01-30 14:00"

# Server log files
tail -f /opt/nms-uoserver/Logs/Console.log
```

### World Saves

**Manual save:**
```bash
# Connect as admin and use [save command
# Or trigger via script/API if configured
```

**Automatic saves:**
- Server saves automatically at configured intervals
- Saves are in `/opt/nms-uoserver/Saves/`

**Backup saves:**
```bash
# Create backup of current save
sudo -u nms tar -czf /opt/nms-uoserver/backups/save-$(date +%Y%m%d-%H%M%S).tar.gz \
  -C /opt/nms-uoserver Saves/

# Keep only last 10 backups
cd /opt/nms-uoserver/backups
ls -t save-*.tar.gz | tail -n +11 | xargs -r rm
```

---

## Updating

### Using Deployment Script (Recommended)

```bash
# Download and deploy latest version
sudo /opt/nms-uoserver/deploy/deploy.sh latest --auto-restart

# Or specific version
sudo /opt/nms-uoserver/deploy/deploy.sh 1.2.0 --auto-restart
```

### Manual Update

```bash
# 1. Stop server
sudo systemctl stop nms-uoserver

# 2. Backup current version
sudo -u nms cp /opt/nms-uoserver/bin/Scripts.dll \
  /opt/nms-uoserver/backups/Scripts.dll.$(date +%Y%m%d-%H%M%S)

# 3. Download new version
cd /tmp
wget https://github.com/YOUR_ORG/nms-uoserver/releases/download/v1.2.0/Scripts.dll

# 4. Deploy new version
sudo -u nms cp /tmp/Scripts.dll /opt/nms-uoserver/bin/Scripts.dll

# 5. Start server
sudo systemctl start nms-uoserver

# 6. Verify
sudo systemctl status nms-uoserver
sudo journalctl -u nms-uoserver -n 50
```

### Rollback

```bash
# If update fails, rollback to previous version
sudo /opt/nms-uoserver/deploy/deploy.sh rollback

# Or manually:
sudo systemctl stop nms-uoserver
sudo -u nms cp /opt/nms-uoserver/backups/Scripts.dll.20260130-143022 \
  /opt/nms-uoserver/bin/Scripts.dll
sudo systemctl start nms-uoserver
```

---

## Monitoring

### System Resources

```bash
# CPU and memory usage
top -u nms

# Or with htop (if installed)
htop -u nms

# Disk usage
df -h /opt/nms-uoserver
du -sh /opt/nms-uoserver/*
```

### Server Status

```bash
# Check if server is running
systemctl is-active nms-uoserver

# Check player count (if status API enabled)
curl http://localhost:8080/status

# Check uptime
systemctl status nms-uoserver | grep "Active:"
```

### Log Monitoring

```bash
# Watch for errors
sudo journalctl -u nms-uoserver -f | grep -i error

# Watch for specific events
sudo journalctl -u nms-uoserver -f | grep -i "player\|login\|disconnect"
```

---

## Backup & Restore

### Backup Strategy

**What to backup:**
- `/opt/nms-uoserver/Saves/` - World save (CRITICAL)
- `/opt/nms-uoserver/Data/` - Configuration
- `/opt/nms-uoserver/bin/Scripts.dll` - Current version
- `/opt/nms-uoserver/Logs/` - Logs (optional)

**Backup script:**
```bash
#!/bin/bash
# /opt/nms-uoserver/deploy/backup.sh

BACKUP_DIR="/opt/nms-uoserver/backups"
DATE=$(date +%Y%m%d-%H%M%S)
BACKUP_FILE="$BACKUP_DIR/full-backup-$DATE.tar.gz"

# Create backup
tar -czf "$BACKUP_FILE" \
  -C /opt/nms-uoserver \
  Saves/ Data/ bin/Scripts.dll

# Keep only last 30 days
find "$BACKUP_DIR" -name "full-backup-*.tar.gz" -mtime +30 -delete

echo "Backup created: $BACKUP_FILE"
```

**Schedule with cron:**
```bash
# Edit crontab
sudo crontab -e

# Add daily backup at 4 AM
0 4 * * * /opt/nms-uoserver/deploy/backup.sh >> /opt/nms-uoserver/Logs/backup.log 2>&1
```

### Restore

```bash
# 1. Stop server
sudo systemctl stop nms-uoserver

# 2. Restore from backup
BACKUP_FILE="/opt/nms-uoserver/backups/full-backup-20260130-040000.tar.gz"
sudo -u nms tar -xzf "$BACKUP_FILE" -C /opt/nms-uoserver

# 3. Start server
sudo systemctl start nms-uoserver

# 4. Verify
sudo journalctl -u nms-uoserver -n 50
```

---

## Troubleshooting

### Server Won't Start

**1. Check logs:**
```bash
sudo journalctl -u nms-uoserver -n 100
```

**2. Common issues:**

**Missing dependencies:**
```bash
# Verify Mono is installed
mono --version

# Reinstall if needed
sudo apt-get install --reinstall mono-complete
```

**Permission issues:**
```bash
# Fix ownership
sudo chown -R nms:nms /opt/nms-uoserver

# Fix permissions
sudo chmod 755 /opt/nms-uoserver/LinuxServer.exe
sudo chmod 644 /opt/nms-uoserver/bin/Scripts.dll
```

**Port already in use:**
```bash
# Check what's using port 2593
sudo netstat -tuln | grep 2593
sudo ss -tulnp | grep 2593

# Kill conflicting process or change server port
```

**Missing Files/ directory:**
```bash
# Verify UO client files exist
ls -lh /opt/nms-uoserver/Files/map*.mul

# Copy if missing (see Installation section)
```

### Server Crashes

**1. Check crash logs:**
```bash
sudo journalctl -u nms-uoserver --since "10 minutes ago"
tail -n 200 /opt/nms-uoserver/Logs/Crash.log
```

**2. Common causes:**

**Out of memory:**
```bash
# Check memory usage
free -h

# Increase memory limit in service file
sudo nano /etc/systemd/system/nms-uoserver.service
# Add: Environment="MONO_GC_PARAMS=max-heap-size=4g"

sudo systemctl daemon-reload
sudo systemctl restart nms-uoserver
```

**Corrupted save:**
```bash
# Restore from backup
sudo systemctl stop nms-uoserver
sudo -u nms cp -r /opt/nms-uoserver/backups/save-20260130/* \
  /opt/nms-uoserver/Saves/
sudo systemctl start nms-uoserver
```

### Players Can't Connect

**1. Check firewall:**
```bash
# Verify port 2593 is open
sudo ufw status | grep 2593  # Ubuntu
sudo firewall-cmd --list-all | grep 2593  # CentOS
```

**2. Check server is listening:**
```bash
sudo netstat -tuln | grep 2593
# Should show: 0.0.0.0:2593 or :::2593
```

**3. Check server configuration:**
```bash
# Verify server is public (not localhost only)
# Check Scripts/Configuration/ServerSettings.cs
```

### High CPU/Memory Usage

**Check resource usage:**
```bash
top -u nms

# Investigate with profiler (if available)
```

**Common causes:**
- Too many spawns
- Runaway timers
- Memory leak in custom script
- Database/world save operations

**Mitigation:**
```bash
# Restart server during low-traffic time
sudo systemctl restart nms-uoserver

# Review and optimize custom scripts
# Reduce spawn density if needed
```

---

## Security

### System Hardening

**1. Run as non-root user:**
✅ Already configured (user `nms`)

**2. Limit file permissions:**
```bash
# Server should not write to executables
sudo chmod 755 /opt/nms-uoserver/LinuxServer.exe
sudo chmod 644 /opt/nms-uoserver/bin/Scripts.dll

# Only nms user can write to Saves/Logs
sudo chown -R nms:nms /opt/nms-uoserver/Saves
sudo chmod 750 /opt/nms-uoserver/Saves
```

**3. Enable systemd hardening:**
Edit `/etc/systemd/system/nms-uoserver.service`:
```ini
[Service]
# Security hardening
PrivateTmp=true
NoNewPrivileges=true
ProtectSystem=strict
ProtectHome=true
ReadWritePaths=/opt/nms-uoserver/Saves /opt/nms-uoserver/Logs
```

**4. Firewall rules:**
```bash
# Only allow game port
sudo ufw default deny incoming
sudo ufw allow 2593/tcp
sudo ufw enable
```

### SSH Access

**For deployment access:**
```bash
# Generate SSH key for deployment
ssh-keygen -t rsa -b 4096 -C "github-actions-nms-uoserver" -f ~/.ssh/nms-deploy

# Add to server
ssh-copy-id -i ~/.ssh/nms-deploy.pub nms@your-server.com

# Or manually:
# Copy ~/.ssh/nms-deploy.pub to server's /opt/nms-uoserver/.ssh/authorized_keys
```

---

## Production Checklist

### Pre-Launch

- [ ] Mono 6.0+ installed
- [ ] System user `nms` created
- [ ] Directory structure created
- [ ] Server binaries deployed
- [ ] UO client Files/ copied
- [ ] Configuration reviewed
- [ ] Systemd service installed
- [ ] Firewall configured
- [ ] Backup script configured
- [ ] Test server starts successfully
- [ ] Test player can connect
- [ ] Test world save works

### Post-Launch

- [ ] Monitor logs for errors
- [ ] Verify backups are running
- [ ] Check resource usage
- [ ] Test restart after crash
- [ ] Document any custom configuration
- [ ] Set up monitoring/alerting

---

## Support

### Log Files

- **System logs:** `journalctl -u nms-uoserver`
- **Server logs:** `/opt/nms-uoserver/Logs/Console.log`
- **Crash logs:** `/opt/nms-uoserver/Logs/Crash.log`
- **Backup logs:** `/opt/nms-uoserver/Logs/backup.log`

### Common Commands

```bash
# Server management
sudo systemctl start nms-uoserver
sudo systemctl stop nms-uoserver
sudo systemctl restart nms-uoserver
sudo systemctl status nms-uoserver

# Logs
sudo journalctl -u nms-uoserver -f
sudo journalctl -u nms-uoserver -n 100
tail -f /opt/nms-uoserver/Logs/Console.log

# Deployment
sudo /opt/nms-uoserver/deploy/deploy.sh latest
sudo /opt/nms-uoserver/deploy/deploy.sh rollback

# Backup
sudo /opt/nms-uoserver/deploy/backup.sh
```

---

## Additional Resources

- [CI/CD Setup](./CI-CD-SETUP.md) - Automated deployment
- [Test-Scripts Migration](./TEST-SCRIPTS-MIGRATION.md) - Content management
- [Refactoring Proposal](./REFACTORING-PROPOSAL.md) - Code optimization

---

**Last Updated:** 2026-01-30
