# MineSpirit Bulk Placement Guide

## Overview

The `SpawnGen` system now supports bulk placement of `MineSpirit` entities directly from `.map` files. This allows you to place many mining spots at once without manually spawning each one in-game.

## File Format

Add MineSpirit entries to your `.map` files using the `!` prefix:

```
!|MineSpirit|OreType|ReqSkill|MinSkill|MaxSkill|Range|X|Y|Z|Map
```

### Field Descriptions

| Position | Field | Description | Example |
|----------|-------|-------------|---------|
| 0 | Prefix | Must be `!` | `!` |
| 1 | Mobile Type | Always `MineSpirit` | `MineSpirit` |
| 2 | Ore Type | Full class name of ore type | `TitaniumOre` |
| 3 | Req Skill | Required skill to mine (0-120) | `100` |
| 4 | Min Skill | Minimum skill for success (0-120) | `100` |
| 5 | Max Skill | Maximum skill for success (0-120) | `120` |
| 6 | Range | Mining range (0-3 tiles) | `3` |
| 7 | X | X coordinate | `6708` |
| 8 | Y | Y coordinate | `458` |
| 9 | Z | Z coordinate | `0` |
| 10 | Map | Map number (see below) | `1` |

### Map Numbers

- `0` - Both Trammel and Felucca (places on both maps)
- `1` - Felucca only
- `2` - Trammel only
- `3` - Ilshenar
- `4` - Malas
- `5` - Tokuno
- `6` - TerMur

### Available Ore Types

Use the full class name:
- `IronOre`
- `DullCopperOre`
- `ShadowIronOre`
- `CopperOre`
- `BronzeOre`
- `GoldOre`
- `AgapiteOre`
- `VeriteOre`
- `ValoriteOre`
- `TitaniumOre`
- `RoseniumOre`
- `PlatinumOre`
- `ObsidianOre`
- `MithrilOre`
- `DwarvenOre`
- `XormiteOre`
- `NepturiteOre`

## Usage Examples

### Single Titanium Vein

```
!|MineSpirit|TitaniumOre|100|100|120|3|6708|458|0|1
```

### Multiple Veins in a Region

```
## Titanium Mining Area - Northern Mountains
!|MineSpirit|TitaniumOre|100|100|120|3|6708|458|0|1
!|MineSpirit|TitaniumOre|100|100|120|3|6710|460|0|1
!|MineSpirit|TitaniumOre|100|100|120|3|6712|462|0|1
!|MineSpirit|TitaniumOre|100|100|120|3|6714|464|0|1
```

### Mixed Ore Types

```
## Mixed Mining Area
!|MineSpirit|GoldOre|80|80|100|2|5000|1000|0|1
!|MineSpirit|VeriteOre|90|90|110|3|5002|1002|0|1
!|MineSpirit|ValoriteOre|95|95|115|3|5004|1004|0|1
!|MineSpirit|TitaniumOre|100|100|120|3|5006|1006|0|1
```

### Both Facets (Trammel and Felucca)

```
## Place on both Trammel and Felucca
!|MineSpirit|TitaniumOre|100|100|120|3|6708|458|0|0
```

## Running the Spawn Generator

1. Create or edit a `.map` file in `Data/Monsters/` directory
2. Add your MineSpirit entries using the format above
3. In-game, use the command:
   ```
   [SpawnGen filename.map
   ```
   Example:
   ```
   [SpawnGen titanium_mines.map
   ```

## Notes

- MineSpirit entities are created as hidden, frozen, and blessed mobiles
- They override the default mining system within their range
- Invalid ore types will default to `IronOre`
- Skill values are clamped to 0-120 range
- Range values are clamped to 0-3 tiles
- The system counts MineSpirit placements in the spawn count message

## Troubleshooting

### Invalid Ore Type
If you see a console message like:
```
MineSpirit Parser: Invalid ore type 'Titanium', defaulting to IronOre.
```
Make sure you're using the full class name: `TitaniumOre` not `Titanium`

### Invalid Format
If entries are skipped, check:
- The prefix is exactly `!` (not `! ` with a space)
- All required fields are present (at least 10 pipe-separated fields)
- Numeric values are valid numbers

### Not Appearing
- Verify the map number is correct for your target facet
- Check coordinates are valid for that map
- Ensure you're on the correct facet when checking

