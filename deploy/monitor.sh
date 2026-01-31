#!/bin/bash
# UO Server Monitor - with maintenance mode support
# This script monitors the Ultima Adventures server and automatically restarts it if it crashes
#
# Setup:
#   1. Copy to production server: /opt/Ultima-Adventures/deploy/monitor.sh
#   2. Make executable: chmod +x /opt/Ultima-Adventures/deploy/monitor.sh
#   3. Add to crontab: */5 * * * * /opt/Ultima-Adventures/deploy/monitor.sh
#
# Maintenance Mode:
#   Enable:  touch /tmp/uo-maintenance-mode
#   Disable: rm /tmp/uo-maintenance-mode

SERVER_DIR="/opt/Ultima-Adventures"
LOG_FILE="/tmp/uo-monitor.log"
MAINTENANCE_FLAG="/tmp/uo-maintenance-mode"

# Check if maintenance mode is active
if [ -f "$MAINTENANCE_FLAG" ]; then
    echo "$(date): Maintenance mode active - skipping check" >> "$LOG_FILE"
    exit 0
fi

# Normal monitoring
if ! pgrep -f "mono.*LinuxServer" > /dev/null; then
    echo "$(date): Server down, restarting..." >> "$LOG_FILE"
    cd "$SERVER_DIR"
    screen -dmS uoshard mono LinuxServer.exe
    echo "$(date): Server restarted" >> "$LOG_FILE"
fi
