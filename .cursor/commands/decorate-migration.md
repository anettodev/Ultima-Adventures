---
description: Comprehensive decoration migration system for moving world features between maps - supports coordinate-based range selection, entry categorization, and systematic migration
globs: Data/Decoration/**/*.cfg
alwaysApply: false
---

# Decorate Migration Command

> **Note**: This command provides a comprehensive system for migrating decoration entries between map configuration files, supporting coordinate-based selection and systematic categorization.

## Usage

Type `/decorate-migration` followed by migration parameters to migrate decoration entries between maps.

**Basic Syntax:**
```
/decorate-migration [SOURCE_MAP] [TARGET_MAP] [X_RANGE] [Y_RANGE] [Z_RANGE] [CATEGORIES]
```

**Examples:**
```
# Basic coordinate range migration
/decorate-migration Sosaria NMS/Common X:6100-7100 Y:1000-2500 Z:ANY

# Specific categories migration
/decorate-migration Sosaria NMS/Common X:6642-6945 Y:353-85 WorkingSpots InteractiveElements

# Single category migration
/decorate-migration Sosaria NMS/Common X:6585-7071 Y:73-875 StaticDecorations
```

## Migration Parameters

### Map Specifications
- **SOURCE_MAP**: Source map file (e.g., `Sosaria`, `NMS/Common`, `Lodor`, `BottleOfWorld`)
- **TARGET_MAP**: Target map file (typically `NMS/Common` for global migration)

### Coordinate Ranges
- **X_RANGE**: X coordinate range (e.g., `X:6100-7100`, `X:6642-6945`)
- **Y_RANGE**: Y coordinate range (e.g., `Y:1000-2500`, `Y:353-85`)
- **Z_RANGE**: Z coordinate range (e.g., `Z:0`, `Z:ANY`, `Z:-2-20`)

### Category Filters
- **WorkingSpots**: Crafting stations, training areas, resource gathering spots
- **InteractiveElements**: Teleporters, hoard tiles, quest NPCs
- **StaticDecorations**: Decorative items, buildings, landmarks
- **EnvironmentalHazards**: Killer tiles, damaging areas
- **SpecialStructures**: Altars, shrines, unique structures
- **ContainersBarrels**: Storage containers and barrels
- **DoorsGates**: Doors and gate systems
- **AddonsTraining**: Training dummies, archery butts, etc.

## Migration Process

### 1. Analysis Phase
The command will:
- Scan source map for entries within coordinate range
- Categorize entries by type and functionality
- Count total entries per category
- Identify special parameters (hues, names, destinations)

### 2. Migration Phase
For each selected category:
- Extract entries from source map
- Add entries to target map with migration comments
- Comment out entries in source map with migration markers
- Preserve all original parameters and formatting

### 3. Verification Phase
- Confirm all entries migrated successfully
- Verify source entries are properly commented
- Report migration statistics

## Category Definitions

### WorkingSpots
**Crafting & Training:**
- `smith`, `anvil`, `forge`, `smelter` - Blacksmithing
- `alchemist`, `potion` - Alchemy
- `cook`, `pan` - Cooking
- `saw`, `lumber` - Sawmilling
- `warrior`, `knight`, `fighter` - Combat training
- `target`, `archer`, `dummy` - Ranged training
- `wizard`, `pentagram` - Magic training
- `tree`, `lumberjack` - Woodcutting
- `rock`, `miner` - Mining
- `fisherman`, `water` - Fishing
- `tanner`, `hide` - Leatherworking

### InteractiveElements
**Player Interactions:**
- `Teleporter` - Map teleportation points
- `HoardTile` - Treasure hunting spots
- `QuestTransporter` - Quest-related teleporters
- `SkillTeleporter` - Skill-based teleporters

### StaticDecorations
**World Features:**
- `Static` - Decorative items with custom names
- Building landmarks and thematic decorations
- Pirate ships, inns, shrines, etc.

### EnvironmentalHazards
**Danger Zones:**
- `KillerTile` - Damaging terrain areas
- Steam vents, lava pits, etc.

### SpecialStructures
**Unique Locations:**
- `AltarSea` - Underwater shrines and altars
- Sacred sites with special properties

### ContainersBarrels
**Storage Systems:**
- `FishBarrel`, `ScrapIronBarrel` - Specialized containers
- `BankChest`, `GuildBoard` - Interactive storage

### DoorsGates
**Access Control:**
- `DarkWoodDoor`, `LightWoodGate` - Door systems
- `PublicDoor` - Public access doors

### AddonsTraining
**Training Equipment:**
- `ArcheryButteAddon` - Ranged training
- `TrainingDummySouthAddon` - Combat training
- `SawMillEastAddon` - Crafting training

## Migration Output Format

### Migration Report
```
## 🎯 DECORATION MIGRATION REPORT

📊 Migration Summary:
- Source: Sosaria
- Target: NMS/Common
- Range: X:6100-7100, Y:1000-2500, Z:ANY
- Categories: WorkingSpots, InteractiveElements, StaticDecorations

📈 Results:
✅ WorkingSpots: 58 entries migrated
✅ InteractiveElements: 6 entries migrated
✅ StaticDecorations: 5 entries migrated

🎉 TOTAL: 69 entries successfully migrated!

📍 Distribution:
- X coordinates: 6625-6995 (scattered locations)
- Y coordinates: 459-1917 (various areas)
- Z coordinates: -2 to 90 (ground to elevated)
```

### Entry Format
**Added to Target:**
```cfg
# Moved from Sosaria coordinate range (X:6100-7100, Y:1000-2500)
WorkingSpots 7035 (Name=smith)
6657 1914 2
```

**Commented in Source:**
```cfg
# Moved to NMS/Common - WorkingSpots 7035 (Name=smith)
#6657 1914 2
```

## Best Practices

### Coordinate Range Selection
- Use `Z:ANY` for most migrations (ignore Z-axis)
- Choose ranges that encompass complete areas/features
- Consider map boundaries and natural terrain divisions

### Category Selection
- Start with `WorkingSpots` for crafting/training systems
- Follow with `InteractiveElements` for player interactions
- Include `StaticDecorations` for thematic consistency
- Add `EnvironmentalHazards` for danger zones

### Migration Order
1. **WorkingSpots** - Core gameplay systems
2. **InteractiveElements** - Player interaction points
3. **StaticDecorations** - Visual/thematic elements
4. **EnvironmentalHazards** - Safety/combat elements
5. **SpecialStructures** - Unique locations

## Migration Examples

### Complete Area Migration
```
/decorate-migration Sosaria NMS/Common X:6100-7100 Y:1000-2500 Z:ANY WorkingSpots InteractiveElements StaticDecorations EnvironmentalHazards SpecialStructures
```

### Resource Gathering Migration
```
/decorate-migration Sosaria NMS/Common X:6100-7100 Y:1000-2500 WorkingSpots
```

### Interactive Elements Only
```
/decorate-migration Sosaria NMS/Common X:6642-6945 Y:85-353 InteractiveElements
```

## Verification Commands

After migration, verify results:

```bash
# Check source file entries
grep -c "^[^#]*WorkingSpots" Data/Decoration/Sosaria/decorate.cfg

# Check target file additions
grep -c "# Moved from Sosaria" Data/Decoration/NMS/Common/decorate.cfg

# Verify coordinate ranges
grep "# Moved from Sosaria" Data/Decoration/NMS/Common/decorate.cfg | grep -o "[0-9]\+ [0-9]\+ [0-9-]\+" | sort -u
```

## Troubleshooting

### Common Issues

**No entries found:**
- Verify coordinate ranges are correct
- Check file paths and map names
- Ensure category names match exactly

**Incomplete migration:**
- Some entries may have complex parameters
- Check for special characters in names/destinations
- Verify target file write permissions

**Coordinate verification:**
- Use PowerShell coordinate checking:
```powershell
Select-String -Pattern "^WorkingSpots" -Path decorate.cfg | Where-Object { $parts = $_.Line -split ' '; $x = [int]$parts[2]; $y = [int]$parts[3]; $x -ge 6100 -and $x -le 7100 -and $y -ge 1000 -and $y -le 2500 }
```

## Migration Statistics

### Recent Migrations Completed

| Range | Categories | Entries | Status |
|-------|------------|---------|---------|
| X:6100-7100 Y:1000-2500 | WorkingSpots, Interactive, Static, Hazards, Special | 78 | ✅ Complete |
| X:6919-7163 Y:528-782 | WorkingSpots, Interactive, Static, Hazards, Containers | 125 | ✅ Complete |
| X:6642-6945 Y:85-353 | WorkingSpots, Interactive, Static, Doors | 67 | ✅ Complete |
| X:6585-7071 Y:73-875 | WorkingSpots, Interactive, Static, Hazards | 45 | ✅ Complete |

## Related Commands

- `/refactor-plan` - Code refactoring analysis
- `/spell-circle-analysis` - Magic system analysis

---

**Remember**: Always verify migration results and test world loading after major migrations. Coordinate ranges should encompass complete functional areas to maintain gameplay integrity.
