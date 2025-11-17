# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Ultima Adventures is an Ultima Online server emulator forked from Ultima Odyssey, built on ServUO/RunUO. It features extensive custom systems including a soulbound system, custom AI (red/blue AI, magery AI, champ spawn AI), and heavily modified gameplay mechanics. The server targets .NET Framework 4.8.

## Architecture

### Two-Layer Structure

The codebase is split into two main components:

1. **Server/** - Core engine providing the base infrastructure:
   - Network layer, packet handling
   - Core types: `Mobile`, `Item`, `Map`, `Region`
   - Persistence/serialization
   - Command system, gumps, targeting
   - Event system (`EventSink`)
   - Root namespace: `Server`

2. **Scripts/** - Game content and logic (compiled as library referencing Server):
   - All game mechanics, items, mobiles, spells
   - Custom systems and engines
   - Root namespace: `Server`
   - Compiles to `Scripts.dll` loaded by the server at runtime

### Scripts Directory Structure

- **Custom/** - Custom content and testing scripts
- **Engines and systems/** - 40+ major game systems (see Key Systems below)
- **Items and addons/** - All item definitions organized by category
- **Mobiles/** - NPC and creature definitions, AI logic
- **Server Functions/** - Core server utilities (commands, regions, spawners, task managers)
- **Ultima Live/** - Dynamic map modification system

## Building and Running

### Configuration Required

Before running, edit `Scripts/MyServerSettings.cs`:
- Set `FilesPath()` to point to your UO client files directory
  - Windows: `@"C:\UOShard\Ultima-Adventures\Files"`
  - Linux: `@"./Files"`
- Configure `ServerName()` if desired

### Build Process

The project uses .NET Framework 4.8 SDK compilation:
- Main project: `Scripts/Scripts.csproj` (compiles to library)
- References: `Server.csproj`, `OrbServerSDK.dll`, `UOArchitectInterface.dll`
- Defines: `NEWTIMERS`, `ServUO`

### Running the Server

Execute the appropriate server executable:
- Windows: `WindowsServer.exe`
- Linux: `LinuxServer.exe` (via Mono)

The server loads `Scripts.dll` at startup and initializes all systems.

## Key Systems

### AI and Combat

- **BaseAI** (`Scripts/Mobiles/AI/BaseAI.cs`) - Foundation for all creature AI
  - AI types: `AI_Melee`, `AI_Mage`, `AI_Archer`, `AI_Healer`, `AI_Berserk`, etc.
  - Action types: `Wander`, `Combat`, `Guard`, `Flee`, `Backoff`, `Interact`
- **IntelligentAction** (`Scripts/Mobiles/IntelligentAction.cs`) - Enemy detection and targeting logic
  - Handles faction systems (red/blue AI)
  - Midland race system integration
  - Disguise/incognito detection
- **Red/Blue AI System** - Friendly (blue) and hostile (red) NPC factions
  - Blues: `Scripts/Mobiles/NPCs/Blues/` - Helpers, guards, friendly NPCs
  - Reds: `Scripts/Mobiles/NPCs/Reds/` - Hostile spawns, enemy NPCs
- **BaseCreature** (`Scripts/Mobiles/BaseCreature.cs`) - Base class for all mobiles
  - Fight modes: `None`, `Aggressor`, `Strongest`, `Weakest`, `Closest`, `Evil`, `Good`
  - Pet orders: `Come`, `Follow`, `Guard`, `Attack`, `Stay`, `Stop`, `Transfer`
  - Food types, pack instincts, scaling

### Soulbound System

Located in `Scripts/Engines and systems/Soulbound/`
- **Phylactery** - Core soulbound item that stores character essences
- Players can bind items/stats to their phylactery
- Essence types: `Power`, `Regular`, `Channeling`, `Luck`
- Various essence categories: Fire, Cold, Physical, Energy, Poison, etc.

### Champion Spawns

Located in `Scripts/Engines and systems/Champ Spawns/`
- Custom champ spawn AI with unique behaviors per spawn type
- Spawn types: Abyss, Arachnid, Cold Blood, Corrupt, Forest Lord, Glade, etc.
- Champions: Barracoon, Harrower, Mephitis, Neira, Lord Oaks, etc.
- Power scrolls and special loot integration

### Magic Systems

Multiple magic systems in `Scripts/Engines and systems/Magic/`:
- **Magery** - Standard UO spellcasting (1st-8th circles)
- **Necromancy** - Death magic
- **Chivalry** - Paladin abilities
- **Mystic** - Mystic spellcasting
- **Bard** - Song-based buffs/debuffs
- **Syth** - Custom magic system
- **Death Knight** - Custom dark magic
- **Holy Man** - Custom divine magic
- **Jedi System** - Force-based abilities

### Other Major Systems

- **Quests** - Multiple quest types (Standard, Assassin, Fishing, Museum, Search, Summon, Thief)
- **Houses** - Player housing with security and customization
- **Crafting** - Extensive crafting systems with bulk order deeds
- **Boats** - Ships with cargo, pirate bounties
- **Animal Broker** - Pet trading, monster contracts, pet customization
- **Skills** - Custom skill implementations (Stealing, Taming, Arms Lore, etc.)
- **Task Manager** - Automated server tasks on timers (Daily, 150min, 200min, 250min)
- **Spawner** - PremiumSpawner for world population
- **Harvest** - Resource gathering (mining, lumberjacking, etc.)

## Important Base Classes

When creating new content, inherit from these base classes:

- **BaseCreature** - All mobiles/NPCs/monsters
- **BaseAI** - Custom AI behaviors
- **Item** - Base item class (from Server/)
- **BaseWeapon** - Weapons with combat properties
- **BaseArmor** - Armor pieces
- **BasePotion** - Consumable potions
- **Spell** - Magic spells
- **BaseQuest** - Quest definitions

## Common File Locations

- Server configuration: `Scripts/MyServerSettings.cs`
- Player mobile: `Scripts/Mobiles/PlayerMobile.cs`
- Vendor SB (sell/buy) info: Throughout scripts in `SB*.cs` files
- Loot generation: `Scripts/Server Functions/Misc/Loot.cs`
- Region definitions: `Scripts/Server Functions/Regions/`
- Command handlers: `Scripts/Server Functions/Commands/Handlers.cs`

## Development Notes

- The codebase targets a hardcore, consequence-heavy gameplay style
- Items are not insured - death has significant consequences
- Extensive custom content not found in standard ServUO
- Uses ServUO defines and timers (`NEWTIMERS`, `ServUO`)
- Many `.bak` files exist throughout - these are backup copies of modified files
- Unsafe code blocks are enabled for performance-critical operations
