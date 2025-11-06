# Ultima Adventures - Administrator Documentation Index

**Version:** 2.21+  
**Last Updated:** November 2025

---

## Welcome, Administrator!

This documentation suite provides complete information for managing, configuring, and administering your Ultima Adventures server. Whether you're setting up a server for the first time, managing day-to-day operations, or customizing advanced systems, you'll find everything you need here.

---

## Documentation Structure

### 📚 Core Documentation

#### **[Administrator_Guide.md](Administrator_Guide.md)** - START HERE
*Your complete administrative reference*

**Contents**:
- **Getting Started**: Staff access levels, initial setup, GM Body command
- **Core Commands**: Object creation, player management, world manipulation
- **World Building**: CreateWorld system, decoration, teleporters, signs
- **MyServerSettings**: Overview of configuration system
- **Task Manager**: Centralized timing system for world events
- **Advanced Administration**: Regional spawning, champion spawns, custom regions
- **Troubleshooting**: Best practices, common issues, emergency recovery

**When to Use**:
- First time server setup
- Learning administrative commands
- Understanding server systems
- Troubleshooting problems
- Reference for advanced features

---

#### **[Premium_Spawner_Guide.md](Premium_Spawner_Guide.md)** - SPAWNER BIBLE
*Complete guide to the Premium Spawner system*

**Contents**:
- **System Overview**: Architecture, data flow, file locations
- **Map File Format**: Complete syntax reference with examples
- **Advanced Features**: Override commands, weighted spawns, multiple creatures
- **Commands**: Loading, saving, removing spawns
- **Spawner Properties**: All editable properties explained
- **Creating Spawns**: Manual placement, map files, export methods
- **Spawns' Overseer**: Intelligent spawn management system
- **Best Practices**: Organization schemes, workflows, avoiding mistakes
- **Troubleshooting**: Common problems and solutions
- **Advanced Techniques**: Progressive difficulty, boss spawns, ecosystems

**When to Use**:
- Creating custom spawns
- Managing existing spawns
- Understanding spawn files
- Optimizing spawn performance
- Troubleshooting spawn issues

---

#### **[Quick_Reference_Commands.md](Quick_Reference_Commands.md)** - QUICK LOOKUP
*Alphabetical command listing for fast reference*

**Contents**:
- **Complete Command List**: All commands A-Z with syntax
- **Access Levels**: Who can use which commands
- **Common Patterns**: Grouped by function
- **Command Modifiers**: Global, region, area, etc.
- **Emergency Commands**: Quick fixes for common problems

**When to Use**:
- Looking up specific command syntax
- Finding right command for task
- Checking access level requirements
- Quick reference during administration

---

#### **[MyServerSettings_Reference.md](MyServerSettings_Reference.md)** - CONFIGURATION
*Complete reference for all server settings*

**Contents**:
- **Server Identity**: Name, file paths
- **Save Configuration**: Auto-save, logout save, player save
- **Skill & Stat Systems**: Caps, character types
- **Combat & Difficulty**: HP scaling, trap chances, healing/swing speeds
- **Economy & Gold**: Gold drop rates, quest rewards
- **Item Systems**: Identification, decay, powerscrolls
- **Vendor Systems**: Buy/sell chances, rare items
- **Player Vendors**: Simulated sales, advertisements, charges
- **Housing & Decay**: Houses per account, decay times
- **Region Settings**: Mounts, detection, macroing
- **Follower Mechanics**: Speed matching, positioning, guarding
- **Sound & Ambience**: Dungeon/ambient sounds
- **Attribute Caps**: Regen, combat, magic, utility
- **Spell Systems**: Damage, casting
- **Advanced Settings**: Startup routines, difficulty modifiers

**When to Use**:
- Configuring server behavior
- Balancing gameplay
- Understanding current settings
- Customizing for playstyle
- Testing configuration changes

---

## Quick Start Guides

### First Time Server Setup

1. **Read**: [Administrator_Guide.md](Administrator_Guide.md) - Introduction & Initial Server Setup sections
2. **Create**: Owner account (server prompts automatically)
3. **Execute**: `[gmbody` command to equip your admin character
4. **Generate**: World with `[createworld` command
5. **Configure**: Edit `Scripts/MyServerSettings.cs` (see [MyServerSettings_Reference.md](MyServerSettings_Reference.md))
6. **Test**: Navigate world, verify systems working

### Adding Custom Spawns

1. **Read**: [Premium_Spawner_Guide.md](Premium_Spawner_Guide.md) - Basic Map File Structure
2. **Scout**: Locations with `[where` command
3. **Create**: Map file in `Data/Monsters/`
4. **Load**: With `[spawngen yourfile.map`
5. **Test**: In-game
6. **Refine**: Edit file, `[spawngen unload <ID>`, reload

### Troubleshooting Issues

1. **Read**: [Administrator_Guide.md](Administrator_Guide.md) - Troubleshooting section
2. **Check**: Console for error messages
3. **Reference**: [Quick_Reference_Commands.md](Quick_Reference_Commands.md) for emergency commands
4. **Verify**: Settings in [MyServerSettings_Reference.md](MyServerSettings_Reference.md)
5. **Restore**: From backup if necessary

---

## Documentation by Task

### World Management

| Task | Primary Document | Section |
|------|-----------------|---------|
| Generate world decorations | Administrator_Guide.md | World Building Systems |
| Create custom spawns | Premium_Spawner_Guide.md | Creating Custom Spawns |
| Manage spawn groups | Premium_Spawner_Guide.md | Managing Spawn Groups |
| Configure champion spawns | Administrator_Guide.md | Advanced Administration |
| Set up teleporters | Administrator_Guide.md | World Building Systems |
| Place signs | Administrator_Guide.md | Advanced Administration |

### Player Management

| Task | Primary Document | Section |
|------|-----------------|---------|
| Create/manage accounts | Administrator_Guide.md | Account Management |
| Set staff access levels | Administrator_Guide.md | Staff Access Levels |
| Kick/ban players | Quick_Reference_Commands.md | Player Management |
| Manage player skills | Quick_Reference_Commands.md | Skill Management |
| Resurrect/heal players | Quick_Reference_Commands.md | Player Status |

### Server Configuration

| Task | Primary Document | Section |
|------|-----------------|---------|
| Adjust skill caps | MyServerSettings_Reference.md | Skill & Stat Systems |
| Configure gold drops | MyServerSettings_Reference.md | Economy & Gold |
| Set vendor behavior | MyServerSettings_Reference.md | Vendor Systems |
| Configure housing | MyServerSettings_Reference.md | Housing & Decay |
| Adjust combat difficulty | MyServerSettings_Reference.md | Combat & Difficulty |
| Set attribute caps | MyServerSettings_Reference.md | Attribute Caps |

### System Management

| Task | Primary Document | Section |
|------|-----------------|---------|
| Configure auto-save | MyServerSettings_Reference.md | Save Configuration |
| Set up task manager | Administrator_Guide.md | Task Manager System |
| Configure overseers | Premium_Spawner_Guide.md | Spawns' Overseer System |
| Manage regions | Administrator_Guide.md | Advanced Administration |
| Set up factions | Administrator_Guide.md | World Building Systems |

---

## Command Quick Access

### Most Used Commands

| Command | Document | Purpose |
|---------|----------|---------|
| `[gmbody` | Administrator_Guide.md | Equip staff character |
| `[createworld` | Administrator_Guide.md | Generate world |
| `[spawner` | Premium_Spawner_Guide.md | Spawn management menu |
| `[spawngen <file>` | Premium_Spawner_Guide.md | Load spawns |
| `[props` | Quick_Reference_Commands.md | Edit object properties |
| `[add` | Quick_Reference_Commands.md | Create objects |
| `[delete` | Quick_Reference_Commands.md | Remove objects |
| `[go` | Quick_Reference_Commands.md | Teleport to location |
| `[save` | Quick_Reference_Commands.md | Save world |
| `[where` | Quick_Reference_Commands.md | Show location |

### Emergency Commands

| Command | Document | Purpose |
|---------|----------|---------|
| `[save` | Quick_Reference_Commands.md | Immediate save |
| `[res` | Quick_Reference_Commands.md | Resurrect self |
| `[immortal` | Quick_Reference_Commands.md | Make invulnerable |
| `[go` | Quick_Reference_Commands.md | Escape stuck location |
| `[recompile` | Quick_Reference_Commands.md | Recompile scripts |

---

## Configuration Quick Access

### Most Adjusted Settings

| Setting | Document | Purpose |
|---------|----------|---------|
| `skillcap()` | MyServerSettings_Reference.md | Total skill cap |
| `newstatcap()` | MyServerSettings_Reference.md | Total stat cap |
| `ServerSaveMinutes()` | MyServerSettings_Reference.md | Auto-save interval |
| `GetGoldCutRate()` | MyServerSettings_Reference.md | Gold drop modifier |
| `SpellDamage()` | MyServerSettings_Reference.md | Spell damage % |
| `HousesPerAccount()` | MyServerSettings_Reference.md | Houses per account |
| `HomeDecay()` | MyServerSettings_Reference.md | House decay time |

---

## Learning Path

### Beginner Administrator

**Week 1** - Server Setup & Basic Commands
1. Read: Administrator_Guide.md - Sections 1-5
2. Practice: Navigation, object creation, player management
3. Experiment: `[props`, `[add`, `[delete`, `[go` commands

**Week 2** - World Building
1. Read: Administrator_Guide.md - Section 6
2. Practice: `[createworld`, decoration generation
3. Experiment: Custom teleporters, signs, decorations

**Week 3** - Configuration
1. Read: MyServerSettings_Reference.md - All sections
2. Practice: Edit MyServerSettings.cs
3. Experiment: Different skill caps, gold rates, vendor settings

**Week 4** - Spawning Basics
1. Read: Premium_Spawner_Guide.md - Sections 1-5
2. Practice: Loading existing spawn files
3. Experiment: `[spawner` menu, `[spawngen` commands

### Intermediate Administrator

**Month 2** - Advanced Spawning
1. Read: Premium_Spawner_Guide.md - Sections 6-9
2. Practice: Creating custom map files
3. Experiment: Override commands, weighted spawns, SpawnID organization

**Month 3** - System Mastery
1. Read: Administrator_Guide.md - Sections 8-9
2. Read: Premium_Spawner_Guide.md - Sections 10-12
3. Practice: Task Manager configuration, Spawns' Overseer setup
4. Experiment: Advanced techniques, custom workflows

### Advanced Administrator

**Month 4+** - Customization & Optimization
1. Master all documentation
2. Create custom systems
3. Optimize performance
4. Develop unique content
5. Share knowledge with community

---

## Additional Resources

### In-Game Documentation

- **[help`** - Opens help menu (all players)
- **[helpinfo [command]`** - Command-specific help
- **[admin`** - Admin interface (administrators)

### External Resources

- **ServUO Forums**: https://www.servuo.com/
- **Ultima Adventures Thread**: https://www.servuo.com/threads/ultima-adventures.12503/
- **Original UO Odyssey**: https://www.servuo.com/archive/ultima-odyssey.1117/

### Other Documentation Files

- **[ReadMeFirst.txt](ReadMeFirst.txt)** - Quick start guide
- **[Admin Commands.txt](Admin%20Commands.txt)** - Simple command list
- **[Player_Guide.md](Player_Guide.md)** - Player-facing documentation
- **[Premium Spawner Tutorial (EN-rev).txt](Premium%20Spawner%20Tutorial%20(EN-rev).txt)** - Original spawner tutorial
- **[RunUO Documentation - Commands.html](RunUO%20Documentation%20-%20Commands.html)** - Base RunUO commands

---

## Documentation Updates

### Version History

- **2024-11-05**: Initial comprehensive documentation suite created
- Includes: Administrator_Guide, Premium_Spawner_Guide, Quick_Reference_Commands, MyServerSettings_Reference

### Contributing

If you find errors or have suggestions:
1. Document the issue clearly
2. Note document name and section
3. Provide suggested correction
4. Submit to development team

### Documentation Standards

All documentation follows these standards:
- Clear, concise language
- Step-by-step instructions
- Code examples for complex topics
- Cross-references between documents
- Troubleshooting for common problems
- Best practices and warnings

---

## Support & Community

### Getting Help

**Order of Operations**:
1. **Search Documentation**: Use Ctrl+F to search all guides
2. **Check Console**: Look for error messages
3. **Test in Isolation**: Create test environment
4. **Ask Community**: ServUO forums with detailed information

**Good Question Format**:
- What you're trying to do
- What you've tried
- Error messages (exact text)
- Relevant code/configuration
- Server version

### Sharing Your Solutions

When you solve a problem:
1. Document your solution
2. Share on ServUO forums
3. Help others with similar issues
4. Contribute to community knowledge

---

## Disclaimer

This documentation is for **Ultima Adventures version 2.21+**. While most information applies to other RunUO/ServUO servers, some features (Premium Spawner, Task Manager, MyServerSettings) may differ.

**Always backup before**:
- Major configuration changes
- Script modifications
- World generation operations
- Testing new systems

**Server administration requires**:
- Patience and experimentation
- Willingness to learn
- Attention to detail
- Regular backups

---

## Document Quick Links

1. **[Administrator_Guide.md](Administrator_Guide.md)** - Complete administrative reference
2. **[Premium_Spawner_Guide.md](Premium_Spawner_Guide.md)** - Spawner system bible
3. **[Quick_Reference_Commands.md](Quick_Reference_Commands.md)** - Command quick lookup
4. **[MyServerSettings_Reference.md](MyServerSettings_Reference.md)** - Configuration reference

---

**Welcome to Ultima Adventures Administration!**

*Take your time, read thoroughly, experiment safely, and enjoy building your world.*

---

**End of Documentation Index**

