#!/bin/bash
# NMS UO Server - Production Deployment Script
# Usage: ./deploy.sh [version] [--auto-restart]

set -e  # Exit on error

# Configuration
DEPLOY_DIR="/opt/nms-uoserver"
BACKUP_DIR="$DEPLOY_DIR/backups"
SERVICE_NAME="nms-uoserver"

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Parse arguments
VERSION="${1:-latest}"
AUTO_RESTART=false

if [[ "$2" == "--auto-restart" ]]; then
    AUTO_RESTART=true
fi

echo -e "${GREEN}NMS UO Server - Deployment Script${NC}"
echo "========================================="
echo "Version: $VERSION"
echo "Deploy Directory: $DEPLOY_DIR"
echo "Auto Restart: $AUTO_RESTART"
echo ""

# Check if running as root or with sudo
if [[ $EUID -ne 0 ]]; then
   echo -e "${RED}Error: This script must be run as root or with sudo${NC}"
   exit 1
fi

# Function: Create backup
create_backup() {
    echo -e "${YELLOW}Creating backup...${NC}"

    # Create backup directory if it doesn't exist
    mkdir -p "$BACKUP_DIR"

    # Backup with timestamp
    TIMESTAMP=$(date +%Y%m%d-%H%M%S)
    BACKUP_FILE="$BACKUP_DIR/Scripts.dll.$TIMESTAMP"

    if [ -f "$DEPLOY_DIR/bin/Scripts.dll" ]; then
        cp "$DEPLOY_DIR/bin/Scripts.dll" "$BACKUP_FILE"
        echo -e "${GREEN}✓ Backup created: $BACKUP_FILE${NC}"

        # Keep only last 10 backups
        ls -t "$BACKUP_DIR"/Scripts.dll.* | tail -n +11 | xargs -r rm
        echo -e "${GREEN}✓ Old backups cleaned (keeping last 10)${NC}"
    else
        echo -e "${YELLOW}⚠ No existing Scripts.dll found, skipping backup${NC}"
    fi
}

# Function: Stop server
stop_server() {
    echo -e "${YELLOW}Stopping server...${NC}"

    if systemctl is-active --quiet "$SERVICE_NAME"; then
        systemctl stop "$SERVICE_NAME"
        echo -e "${GREEN}✓ Server stopped${NC}"

        # Wait for graceful shutdown
        sleep 2
    else
        echo -e "${YELLOW}⚠ Server not running${NC}"
    fi
}

# Function: Download release
download_release() {
    echo -e "${YELLOW}Downloading release $VERSION...${NC}"

    cd "$DEPLOY_DIR"

    if [ "$VERSION" == "latest" ]; then
        # Download latest release
        DOWNLOAD_URL=$(curl -s https://api.github.com/repos/YOUR_ORG/nms-uoserver/releases/latest | grep "browser_download_url.*Scripts.dll" | cut -d '"' -f 4)
    else
        # Download specific version
        DOWNLOAD_URL="https://github.com/YOUR_ORG/nms-uoserver/releases/download/v$VERSION/Scripts.dll"
    fi

    if [ -z "$DOWNLOAD_URL" ]; then
        echo -e "${RED}✗ Failed to find download URL${NC}"
        exit 1
    fi

    # Download to temp location
    curl -L -o "/tmp/Scripts.dll" "$DOWNLOAD_URL"

    if [ ! -f "/tmp/Scripts.dll" ]; then
        echo -e "${RED}✗ Download failed${NC}"
        exit 1
    fi

    echo -e "${GREEN}✓ Downloaded Scripts.dll${NC}"
}

# Function: Deploy files
deploy_files() {
    echo -e "${YELLOW}Deploying files...${NC}"

    # Ensure bin directory exists
    mkdir -p "$DEPLOY_DIR/bin"

    # Copy new Scripts.dll
    cp /tmp/Scripts.dll "$DEPLOY_DIR/bin/Scripts.dll"
    chown ultima:ultima "$DEPLOY_DIR/bin/Scripts.dll"
    chmod 644 "$DEPLOY_DIR/bin/Scripts.dll"

    echo -e "${GREEN}✓ Files deployed${NC}"

    # Cleanup temp file
    rm /tmp/Scripts.dll
}

# Function: Start server
start_server() {
    echo -e "${YELLOW}Starting server...${NC}"

    systemctl start "$SERVICE_NAME"

    # Wait a bit and check status
    sleep 3

    if systemctl is-active --quiet "$SERVICE_NAME"; then
        echo -e "${GREEN}✓ Server started successfully${NC}"
    else
        echo -e "${RED}✗ Server failed to start${NC}"
        echo -e "${YELLOW}Check logs: sudo journalctl -u $SERVICE_NAME -n 50${NC}"
        exit 1
    fi
}

# Function: Verify deployment
verify_deployment() {
    echo -e "${YELLOW}Verifying deployment...${NC}"

    # Check if Scripts.dll exists
    if [ -f "$DEPLOY_DIR/bin/Scripts.dll" ]; then
        SIZE=$(du -h "$DEPLOY_DIR/bin/Scripts.dll" | cut -f1)
        echo -e "${GREEN}✓ Scripts.dll exists ($SIZE)${NC}"
    else
        echo -e "${RED}✗ Scripts.dll not found${NC}"
        exit 1
    fi

    # Check server status
    if systemctl is-active --quiet "$SERVICE_NAME"; then
        echo -e "${GREEN}✓ Server is running${NC}"
    else
        echo -e "${YELLOW}⚠ Server is not running${NC}"
    fi
}

# Function: Rollback
rollback() {
    echo -e "${RED}Rolling back to previous version...${NC}"

    # Find most recent backup
    LATEST_BACKUP=$(ls -t "$BACKUP_DIR"/Scripts.dll.* 2>/dev/null | head -n 1)

    if [ -z "$LATEST_BACKUP" ]; then
        echo -e "${RED}✗ No backup found for rollback${NC}"
        exit 1
    fi

    # Stop server
    stop_server

    # Restore backup
    cp "$LATEST_BACKUP" "$DEPLOY_DIR/bin/Scripts.dll"
    echo -e "${GREEN}✓ Restored from: $LATEST_BACKUP${NC}"

    # Start server
    start_server

    echo -e "${GREEN}✓ Rollback complete${NC}"
}

# Main deployment flow
main() {
    echo ""
    echo -e "${YELLOW}Starting deployment process...${NC}"
    echo ""

    # Create backup
    create_backup

    # Stop server
    if [ "$AUTO_RESTART" = true ]; then
        stop_server
    else
        read -p "Stop server now? (y/n) " -n 1 -r
        echo
        if [[ $REPLY =~ ^[Yy]$ ]]; then
            stop_server
        else
            echo -e "${YELLOW}⚠ Continuing without stopping server${NC}"
        fi
    fi

    # Download and deploy
    download_release
    deploy_files

    # Start server
    if [ "$AUTO_RESTART" = true ]; then
        start_server
    else
        read -p "Start server now? (y/n) " -n 1 -r
        echo
        if [[ $REPLY =~ ^[Yy]$ ]]; then
            start_server
        else
            echo -e "${YELLOW}⚠ Server not started${NC}"
            echo -e "${YELLOW}Start manually: sudo systemctl start $SERVICE_NAME${NC}"
        fi
    fi

    # Verify
    verify_deployment

    echo ""
    echo -e "${GREEN}=========================================${NC}"
    echo -e "${GREEN}Deployment Complete!${NC}"
    echo -e "${GREEN}=========================================${NC}"
    echo ""
    echo "Version: $VERSION"
    echo "Deployed: $(date)"
    echo ""
    echo "Useful commands:"
    echo "  sudo systemctl status $SERVICE_NAME     # Check status"
    echo "  sudo journalctl -u $SERVICE_NAME -f     # View logs"
    echo "  sudo systemctl restart $SERVICE_NAME    # Restart"
    echo ""
    echo "To rollback:"
    echo "  sudo $0 rollback"
    echo ""
}

# Handle rollback command
if [ "$VERSION" == "rollback" ]; then
    rollback
    exit 0
fi

# Run main deployment
main
