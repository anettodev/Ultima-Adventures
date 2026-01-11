#!/bin/bash
#
# Ultima Adventures Linux Compile Script
# Compiles Scripts.dll and LinuxServer.exe
#

set -e  # Exit on error

echo "==========================================="
echo "Ultima Adventures Linux Build Script"
echo "==========================================="
echo ""

# Step 1: Build Scripts.dll
echo "[1/2] Building Scripts.dll..."
cd Scripts
dotnet build Scripts.csproj -c Release
if [ $? -ne 0 ]; then
    echo ""
    echo "ERROR: Failed to build Scripts.dll!"
    exit 1
fi
echo "✓ Scripts.dll built successfully"
echo ""

# Go back to root
cd ..

# Step 2: Build LinuxServer.exe
echo "[2/2] Building LinuxServer.exe..."

# Backup old executable if it exists
if [ -f "LinuxServer.exe" ]; then
    echo "Backing up old LinuxServer.exe to LinuxServer.exe.bak"
    mv LinuxServer.exe LinuxServer.exe.bak
fi

cd Server
dotnet build Server.csproj -c Release
if [ $? -ne 0 ]; then
    echo ""
    echo "ERROR: Failed to build LinuxServer.exe!"
    exit 1
fi
cd ..

echo "✓ LinuxServer.exe built successfully"
echo ""
echo "==========================================="
echo "Build completed successfully!"
echo "==========================================="
echo ""
echo "Your server executable is ready: LinuxServer.exe"
echo "Run with: ./LinuxServer.exe"
echo ""
