# Administrator Guide
## Quick Start for Server Administrators

---

## Welcome

This guide provides essential information for Ultima Adventures server administrators. For complete documentation, see `Server_Administration_Guide.md`.

---

## First Time Setup

### Step 1: Configure Server

Edit `Scripts/MyServerSettings.cs`:

```csharp
public static string ServerName() { return "Your Server Name"; }
public static string FilesPath() { return @"C:\Path\To\Files"; }
public static double ServerSaveMinutes() { return 20.0; }
```

See `MyServerSettings_Reference.md` for all options.

### Step 2: Build the World

```
[buildworld
```

This command:
- Decorates the world
- Spawns creatures
- Generates items
- **Preserves all players** and their belongings

**First-Time Alternative:**

```
[createworld
```

Opens menu for step-by-step setup.

### Step 3: Verify

1. Walk through major cities
2. Check spawns are working
3. Test teleporters/moongates
4. Verify vendors have items

---

## Access Levels

### Level Hierarchy

```
0 - Player       - Normal players
1 - Counselor    - Help players, limited editing
2 - GameMaster   - Full building, world management
3 - Seer         - Same as GameMaster
4 - Administrator - Full server control
5 - Developer    - Development tools
6 - Owner        - Complete control
```

### Setting Access Level

```
[props on yourself
Set AccessLevel to desired level
```

Or via account system:
```
[admin
```

---

## Essential Commands

### Most Used

```
[props            - Edit object properties
[add              - Add items/mobiles
[delete           - Delete objects
[go               - Teleport to location
[tele             - Teleport to target
[where            - Show current location
[save             - Save world
```

### Navigation

```
[go britain       - Teleport to Britain
[go 1450 1650 0   - Teleport to coordinates
[tele             - Teleport to targeted location
[where            - Show your location/region
```

### Item Management

```
[add dagger       - Add specific item
[add              - Open add menu
[delete           - Delete targeted object
[dupe 5           - Duplicate item 5 times
[move             - Reposition object
[props            - Edit properties
```

### Player Management

```
[kill             - Kill target
[res              - Resurrect target
[kick             - Disconnect player
[ban              - Ban account
[invul            - Make invulnerable
[mortal           - Remove invulnerability
```

### World Building

```
[tile floor       - Tile floor in area
[add premiumspawner - Add spawner
[wipe             - Delete all in area
[editor           - Spawn editor
```

### Server Management

```
[save             - Save world
[recompile        - Recompile scripts
[admin            - Admin interface
[buildworld       - Rebuild world
```

---

## Daily Tasks

### Morning Checklist

- [ ] Check server is running
- [ ] Review any player pages/issues
- [ ] Check for stuck players
- [ ] Verify spawns working

### Evening Review

- [ ] Check backup status
- [ ] Review day's events
- [ ] Plan maintenance if needed

---

## World Management

### Normal Maintenance

```
[save                    - Manual save
[spawngen save           - Backup spawns
[respawnallregions       - Refresh spawns
```

### Reset World (Keeps Players)

```
[save                    - Save first!
[buildworld              - Rebuild world
```

### Clear Specific Area

```
[wipe                    - Delete all in targeted area
[wipeitems               - Delete only items
[wipenpcs                - Delete only NPCs
```

**⚠ WARNING**: These are destructive! Always backup first.

---

## Spawner Basics

### Quick Spawner Commands

```
[spawner                 - Spawner menu
[editor                  - Spawn editor
[spawngen spawns.map     - Load spawns
[spawngen save           - Save all spawns
[spawngen unload 20      - Remove spawns by ID
[add premiumspawner      - Add spawner manually
```

### Add Spawner

```
1. [add premiumspawner Orc
2. [props (on spawner)
3. Set:
   - Count0: 5
   - MinDelay: 00:05:00
   - MaxDelay: 00:10:00
   - HomeRange: 15
   - WalkingRange: 30
   - Running: true
4. [spawngen savebyhand
```

**See**: `Premium_Spawner_Guide.md` for complete spawner guide.

---

## Player Assistance

### Stuck Player

```
1. [go PlayerName
2. Assess situation
3. [stuck (on player)
   OR
   [tele (teleport them)
4. Verify they're safe
```

### Lost Items

```
1. Verify story
2. If legitimate:
   [add <item>
   [set properties to match
3. Document incident
```

### Dead Player

```
1. [go PlayerName
2. [res
3. Check health/status
```

---

## Emergency Commands

### For Players in Danger

```
[invul            - Make invulnerable
[res              - Resurrect
[tele             - Teleport to safety
```

### For Problem Players

```
[kick             - Disconnect
[ban              - Ban account
[jail             - Send to jail (if available)
```

### Server Issues

```
[save             - Emergency save
[recompile        - Fix script errors
Restart server    - Ultimate solution
```

---

## Common Issues & Fixes

### Spawns Not Working

**Fix**:
```
[stopallregionspawns
[startallregionspawns
```

Or:
```
[editor
Find spawner → Props → Running = true
```

### Server Won't Save

**Fix**:
```
[savebg    - Background save
```

Or restart server.

### Commands Not Working

**Fix**:
```
[recompile
```

Or check access level:
```
[props (on yourself)
Check AccessLevel
```

### Player Can't Move

**Fix**:
```
[props (on player)
Check: Frozen, Paralyzed
Set to false if needed
```

Or:
```
[stuck (on player)
```

---

## Server Configuration

### MyServerSettings.cs

Key settings to adjust:

```csharp
// Server basics
ServerName()              - Server name
FilesPath()               - Client files path
ServerSaveMinutes()       - Auto-save interval

// Game difficulty
FloorTrapTrigger()        - Trap trigger chance (0-100)
GetUnidentifiedChance()   - Unidentified items (0-100)
CreaturesDetectHidden()   - Monsters detect hidden players

// Player settings
PlayerLevelMod()          - HP/Mana/Stam multiplier
NoMountsInCertainRegions() - Disable mounts in dungeons

// Housing
HomeDecay()               - Days before house decay
HousesPerAccount()        - Max houses per account
```

**Apply Changes**:
```
[recompile
```

Or restart server.

**Full Reference**: See `MyServerSettings_Reference.md`

---

## Backup Strategy

### What to Backup

```
/Saves/                  - World saves (CRITICAL)
/Data/Monsters/*.map     - Spawn files
/Scripts/MyServerSettings.cs - Configuration
/Data/Regions.xml        - Custom regions
```

### Backup Schedule

- **Before major changes**: Manual backup
- **Daily**: Automatic with world saves
- **Weekly**: Manual full backup
- **Monthly**: Archive to external storage

### Quick Backup

```
[save                    - Save world
[spawngen save           - Save spawns

Copy /Saves/ folder to backup location
```

---

## Best Practices

### DO

- ✅ Backup before major changes
- ✅ Test on test server first
- ✅ Announce maintenance to players
- ✅ Use `[save` before dangerous operations
- ✅ Document what you change
- ✅ Start with low access levels for staff
- ✅ Review logs regularly

### DON'T

- ❌ Use `[clearall` without backups
- ❌ Give admin access casually
- ❌ Recompile during peak hours
- ❌ Change critical systems without testing
- ❌ Run destructive commands without confirmation
- ❌ Skip documentation

---

## Security

### Account Management

```
[admin               - Account management interface
[ban                 - Ban player account
[firewall            - Block IP address
```

### Access Levels

- Use **minimum required** access level
- **Counselor** for helpers
- **GameMaster** for builders
- **Administrator** for trusted staff only
- **Owner** for you only

### Regular Audits

- Review who has what access
- Check for inactive staff accounts
- Monitor admin command usage
- Review ban/firewall lists

---

## Command Modifiers

Apply commands to multiple targets:

```
[global <command>        - All objects in world
[online <command>        - All logged-in players
[region <command>        - All in current region
[area <command>          - All in targeted area
[multi <command>         - Multiple targets
```

**Examples**:
```
[region res              - Resurrect all in region
[area delete             - Delete all in area
[online msg "Welcome"    - Message all players
```

---

## Quick Reference

### Top 10 Commands

```
1. [props           - Edit properties
2. [add             - Add items/mobiles
3. [delete          - Delete objects
4. [go              - Teleport
5. [where           - Show location
6. [save            - Save world
7. [editor          - Spawn editor
8. [buildworld      - Rebuild world
9. [tele            - Teleport to target
10. [res            - Resurrect
```

### Emergency Contacts

**For Emergencies**:
```
[save               - Save immediately
[b <message>        - Broadcast to all
[kick               - Remove problem player
```

**Critical Commands**:
```
[invul              - Protect self/player
[tele               - Emergency teleport
[ban                - Ban if needed
```

---

## Getting Help

### Documentation

- **This Guide**: Quick start
- **Server_Administration_Guide.md**: Complete command reference
- **Premium_Spawner_Guide.md**: Spawner quick start
- **MyServerSettings_Reference.md**: Configuration options
- **Quick_Reference_Commands.md**: Command lookup

### In-Game Help

```
[help               - Help menu
[helpinfo <command> - Command details
```

### Community Resources

- ServUO Forums: https://www.servuo.com/
- Ultima Adventures thread
- Discord communities

---

## Next Steps

### Learn More

1. **Complete Guides**:
   - `Server_Administration_Guide.md` - Full admin reference
   - `Premium_Spawner_System_Guide.md` - Complete spawner docs

2. **Quick References**:
   - `Quick_Reference_Commands.md` - Command cheat sheet
   - `Quick_Reference_Spawners.md` - Spawner cheat sheet

3. **Configuration**:
   - `MyServerSettings_Reference.md` - All config options

### Expand Your Skills

**Week 1**: Master basic commands
- Navigation ([go, [tele, [where])
- Item management ([add, [delete, [props])
- Player assistance ([res, [stuck])

**Week 2**: Learn world building
- Spawn management ([editor, [spawngen])
- Decoration ([tile, [add])
- Region management

**Week 3**: Server management
- Configuration (MyServerSettings.cs)
- Backup procedures
- Performance optimization

**Week 4**: Advanced features
- Custom scripts
- Event hosting
- Community management

---

## Cheat Sheet

### Navigation
```
[go <place>    [tele    [where
```

### Items
```
[add    [delete    [props    [dupe
```

### Players
```
[res    [kill    [kick    [ban    [invul
```

### World
```
[buildworld    [save    [editor    [spawner
```

### Emergency
```
[save    [invul    [tele    [kick
```

---

## Contact & Support

### Server Issues

- Check Documentation folder first
- Review console for errors
- Check ServUO forums
- Ask community on Discord

### Critical Problems

1. **Save immediately**: `[save`
2. **Document issue**: What happened?
3. **Check backups**: Can you restore?
4. **Seek help**: Forums/Discord with details

---

**Guide Version**: 1.0  
**Last Updated**: 2025  
**For**: Ultima Adventures Server  
**Quick Start Focus**: Get administrating fast!

---

*For complete documentation, see Server_Administration_Guide.md*

