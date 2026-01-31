# Production Deployment Scripts

This directory contains operational scripts for managing the Ultima Adventures server in production.

## 📁 Files

### deploy.sh
Main production deployment script for updating Scripts.dll.

**Usage:**
```bash
sudo ./deploy.sh [version] [--auto-restart]
```

**Features:**
- Automatic backup creation (keeps last 10)
- Download from GitHub releases
- Graceful stop/start with systemd
- Deployment verification
- Rollback capability

**Examples:**
```bash
# Deploy latest version
sudo ./deploy.sh latest --auto-restart

# Deploy specific version
sudo ./deploy.sh 1.2.3

# Rollback to previous version
sudo ./deploy.sh rollback
```

---

### monitor.sh
Automated server monitoring and auto-restart script.

**Purpose:** Monitors the UO server process and automatically restarts it if it crashes.

**Setup on Production Server:**

1. **Copy to server:**
   ```bash
   scp deploy/monitor.sh user@server:/opt/Ultima-Adventures/deploy/
   ```

2. **Make executable:**
   ```bash
   chmod +x /opt/Ultima-Adventures/deploy/monitor.sh
   ```

3. **Add to crontab** (runs every 5 minutes):
   ```bash
   crontab -e
   ```

   Add this line:
   ```
   */5 * * * * /opt/Ultima-Adventures/deploy/monitor.sh
   ```

4. **View monitor logs:**
   ```bash
   tail -f /tmp/uo-monitor.log
   ```

**Features:**
- Detects crashed server processes
- Automatically restarts server in screen session
- Logs all restart events
- Maintenance mode support (see below)

---

### nms-maintenance
Maintenance mode manager that controls both the maintenance flag and server process.

**Purpose:** Manage maintenance mode and server state together.

**Usage:**
```bash
# Enable maintenance mode and stop server
./nms-maintenance start

# Disable maintenance mode and start server
./nms-maintenance stop

# Check maintenance mode status
./nms-maintenance status
```

**What it does:**
- **start**: Enables maintenance flag, stops server (monitor won't auto-restart)
- **stop**: Disables maintenance flag, starts server in screen session
- **status**: Shows current maintenance mode state

**When to use:**
- Before manual troubleshooting (prevents auto-restart while debugging)
- During planned maintenance or updates
- When performing database operations or world edits

---

### nms-uoserver.service
Systemd service file for managing the UO server as a system service.

**Installation:**
```bash
sudo cp nms-uoserver.service /etc/systemd/system/
sudo systemctl daemon-reload
sudo systemctl enable nms-uoserver
```

**Management:**
```bash
sudo systemctl start nms-uoserver     # Start server
sudo systemctl stop nms-uoserver      # Stop server
sudo systemctl restart nms-uoserver   # Restart server
sudo systemctl status nms-uoserver    # Check status
sudo journalctl -u nms-uoserver -f    # View logs
```

---

## 🔧 Production Setup Checklist

### Initial Server Setup

1. **Install dependencies:**
   ```bash
   sudo apt update
   sudo apt install mono-complete screen
   ```

2. **Create server directory:**
   ```bash
   sudo mkdir -p /opt/Ultima-Adventures
   sudo chown ultima:ultima /opt/Ultima-Adventures
   ```

3. **Deploy server files:**
   ```bash
   # Copy LinuxServer.exe, Files/, Data/, etc. to /opt/Ultima-Adventures
   ```

4. **Install systemd service:**
   ```bash
   sudo cp deploy/nms-uoserver.service /etc/systemd/system/
   sudo systemctl daemon-reload
   sudo systemctl enable nms-uoserver
   ```

5. **Setup monitoring:**
   ```bash
   chmod +x deploy/monitor.sh
   crontab -e  # Add: */5 * * * * /opt/Ultima-Adventures/deploy/monitor.sh
   ```

6. **Setup maintenance mode manager:**
   ```bash
   chmod +x deploy/nms-maintenance
   ```

### Deployment Workflow

1. **Enable maintenance mode and stop server:**
   ```bash
   ./nms-maintenance start
   ```

2. **Deploy new version:**
   ```bash
   sudo ./deploy.sh latest --auto-restart
   ```

3. **Verify deployment:**
   ```bash
   sudo systemctl status nms-uoserver
   sudo journalctl -u nms-uoserver -n 50
   ```

4. **Disable maintenance mode and start server:**
   ```bash
   ./nms-maintenance stop
   ```

### Troubleshooting

**Server won't start:**
```bash
# Check logs
sudo journalctl -u nms-uoserver -n 100 --no-pager

# Check process
ps aux | grep mono

# Check permissions
ls -la /opt/Ultima-Adventures/bin/Scripts.dll
```

**Monitor not working:**
```bash
# Check crontab
crontab -l

# Check monitor logs
tail -f /tmp/uo-monitor.log

# Test monitor manually
/opt/Ultima-Adventures/deploy/monitor.sh
```

**Need to rollback:**
```bash
sudo ./deploy.sh rollback
```

---

## 📊 Directory Structure (Production)

```
/opt/Ultima-Adventures/
├── LinuxServer.exe           # Server executable
├── Server.dll                # Core engine
├── bin/
│   └── Scripts.dll           # Game scripts (deployed)
├── backups/
│   └── Scripts.dll.*         # Automatic backups
├── deploy/
│   ├── deploy.sh             # Deployment script
│   ├── monitor.sh            # Monitoring script
│   └── nms-maintenance       # Maintenance mode manager
├── Files/                    # Client files
├── Data/                     # World data
├── Saves/                    # World saves
└── Logs/                     # Server logs
```

---

## 🔐 Security Notes

- Run monitor.sh as non-root user
- Use systemd service for proper process management
- Keep backups in secure location
- Review deployment logs regularly
- Restrict access to /opt/Ultima-Adventures

---

## 📝 Maintenance Schedule

**Daily:**
- Monitor auto-restart logs (`/tmp/uo-monitor.log`)
- Check systemd status

**Weekly:**
- Review backup count and size
- Check disk space
- Review server logs for errors

**Monthly:**
- Clean old backups (script auto-cleans, keeping last 10)
- Review cron jobs
- Update system packages

---

For more information, see the main project documentation.
