# CentrED Hue Matching Guide for Wood/Logs and Boards

This document provides the hue values used in the game for wood logs and boards, and how to match them in CentrED.

## Important Notes

1. **Logs and Boards use the same hues** - Both `Log.cs` and `Board.cs` use `CraftResources.GetHue(resource)` to get their hue values, ensuring they match perfectly.

2. **Hue Format**: 
   - In code: Hexadecimal format (0x000 to 0xFFFF)
   - In CentrED: Decimal format (0 to 65535)
   - Conversion: Hex `0x41C` = Decimal `1052`

3. **Wood Hue System**: All wood types use the default style (empty string) in `Materials.cs`, so these are the actual hues your server uses.

## Wood/Log Hue Reference Table

| Wood Type | Hex Value | Decimal Value | Notes |
|-----------|-----------|---------------|-------|
| **Regular Wood** | `0x000` | `0` | No hue (default brown) |
| **Ash** | `0x41C` | `1052` | Gray/ash colored |
| **Ebony** | `0x96C` | `2412` | Dark brown/black |
| **Elven** | `0x1C9` | `457` | Light/white wood |
| **Golden Oak** | `0x501` | `1281` | Golden/yellow-brown |
| **Cherry** | `0x8D` | `141` | Red/pink wood |
| **Rosewood** | `0x66D` | `1645` | Red-brown wood |
| **Hickory** | `0xB8B` | `2955` | Silver/gray wood |

## How to Use in CentrED

1. **When placing logs/boards in CentrED**, you can set the hue by:
   - Right-clicking the item
   - Selecting "Properties" or "Hue"
   - Entering the **decimal value** from the table above

2. **Example**: To set a Cherry Log/Board:
   - Use hue value: `141` (decimal for 0x8D)

3. **Verification**: 
   - The hue values in `Log.cs` and `Board.cs` are identical
   - Both use `CraftResources.GetHue(resource)` which pulls from `ResourceInfo.cs`
   - The actual hue values come from `Materials.cs` → `GetMaterialColor()` function

## Code References

- **Log Hues**: `Scripts/Items and addons/Skill Items/Lumberjack/Log.cs` (line 51)
- **Board Hues**: Uses same `CraftResources.GetHue(resource)` system
- **Hue Source**: `Scripts/Server Functions/Misc/ResourceInfo.cs` (lines 950-959)
- **Material Colors**: `Scripts/Server Functions/Functions/Materials.cs` (lines 1446-1452)

## Quick Reference (Decimal Values Only)

For quick copy-paste into CentrED:

```
Regular Wood: 0
Ash: 1052
Ebony: 2412
Elven: 457
Golden Oak: 1281
Cherry: 141
Rosewood: 1645
Hickory: 2955
```

## Wood Type Details

- **Regular Wood**: Standard brown wood, no hue applied
- **Ash**: Grayish wood color (Carvalho Cinza)
- **Ebony**: Dark brown/black wood (Ébano)
- **Elven**: Light/white wood (Élfica)
- **Golden Oak**: Golden/yellow-brown wood (Ipê-Amarelo)
- **Cherry**: Red/pink wood (Cerejeira)
- **Rosewood**: Red-brown wood (Pau-Brasil)
- **Hickory**: Silver/gray wood (Nogueira Branca)

## Notes

- All wood types use empty string style (`""`) in `MaterialInfo.GetMaterialColor()`, so these are the definitive hue values
- These hues are consistent across all wood-based items (logs, boards, crafted items)
- The hue values are defined in `Materials.cs` and are not affected by "classic" vs "monster" style settings

