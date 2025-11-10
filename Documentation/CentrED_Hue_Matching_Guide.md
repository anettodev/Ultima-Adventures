# CentrED Hue Matching Guide for Ores and Ingots

This document provides the hue values used in the game for ores and ingots, and how to match them in CentrED.

## Important Notes

1. **Ores and Ingots use the same hues** - Both `Ore.cs` and `Ingots.cs` use `CraftResources.GetHue(resource)` to get their hue values, ensuring they match perfectly.

2. **Hue Format**: 
   - In code: Hexadecimal format (0x000 to 0xFFFF)
   - In CentrED: Decimal format (0 to 65535)
   - Conversion: Hex `0xAB5` = Decimal `2741`

3. **Two Hue Systems**: 
   - **Classic UO Hues**: Standard Ultima Online hues (what CentrED typically displays)
   - **Custom Server Hues**: Your server's custom hue values (what the code actually uses)

4. **CentrED Hue System**: CentrED uses standard Ultima Online hue numbers (0-3000+ range). The image shown in CentrED typically displays the "classic" UO hues.

## ⚠️ CRITICAL: Which Hues Does Your Server Use?

Your server code uses **custom hues** (empty string style), NOT the classic UO hues. However, CentrED may display the classic hues in its hue picker. Use the table that matches what you see in-game.

## Classic UO Hues (Standard - What CentrED Shows)

These are the standard Ultima Online hues that CentrED typically displays:

| Resource Type | Hex Value | Decimal Value | Notes |
|--------------|-----------|---------------|-------|
| **Iron** | `0x000` | `0` | No hue (default gray) |
| **Dull Copper** | `0x973` | `2419` | Brownish copper |
| **Shadow Iron** | `0x966` | `2406` | Dark gray/black |
| **Copper** | `0x96D` | `2413` | Bright copper |
| **Bronze** | `0x972` | `2418` | Bronze/golden brown |
| **Gold** | `0x8A5` | `2213` | Gold/yellow |
| **Agapite** | `0x979` | `2425` | Silver/blue-gray |
| **Verite** | `0x89F` | `2207` | Greenish |
| **Valorite** | `0x8AB` | `2219` | Blue-gray |
| **Titanium** | `0x565` | `1381` | Light gray/silver |
| **Rosenium** | `0xEC` | `236` | Pink/rose |
| **Platinum** | `0x911` | `2321` | Silver/white |
| **Nepturite** | `0x58E` | `1422` | Blue-green |
| **Obsidian** | `0x8B8` | `2232` | Dark black |
| **Steel** | `0x515` | `1301` | Gray |
| **Brass** | `0xA5D` | `2653` | Yellow-gold |
| **Mithril** | `0x9C2` | `2498` | Bright silver/white |
| **Xormite** | `0xB96` | `2966` | Purple/violet |
| **Dwarven** | `0xA1D` | `2589` | Dark gray/brown |

## Custom Server Hues (What Your Code Actually Uses)

These are the hues your server code actually uses (from `Materials.cs` with empty string style):

| Resource Type | Hex Value | Decimal Value | Notes |
|--------------|-----------|---------------|-------|
| **Iron** | `0x000` | `0` | No hue (default gray) |
| **Dull Copper** | `0xAB5` | `2741` | Brownish copper |
| **Shadow Iron** | `0xAB3` | `2739` | Dark gray/black |
| **Copper** | `0xB18` | `2840` | Bright copper |
| **Bronze** | `0x8BC` | `2236` | Bronze/golden brown |
| **Gold** | `0xB1B` | `2843` | Gold/yellow |
| **Agapite** | `0xAEA` | `2794` | Silver/blue-gray |
| **Verite** | `0x85D` | `2141` | Greenish |
| **Valorite** | `0x95D` | `2397` | Blue-gray |
| **Titanium** | `0x565` | `1381` | Light gray/silver |
| **Rosenium** | `0xEC` | `236` | Pink/rose |
| **Platinum** | `0x911` | `2321` | Silver/white |
| **Nepturite** | `0x948` | `2376` | Blue-green |
| **Obsidian** | `0x8B8` | `2232` | Dark black |
| **Steel** | `0x99F` | `2463` | Gray |
| **Brass** | `0x993` | `2451` | Yellow-gold |
| **Mithril** | `0xB78` | `2936` | Bright silver/white |
| **Xormite** | `0x7C7` | `1991` | Purple/violet |
| **Dwarven** | `0x6FC` | `1788` | Dark gray/brown |

## How to Use in CentrED

1. **When placing ores/ingots in CentrED**, you can set the hue by:
   - Right-clicking the item
   - Selecting "Properties" or "Hue"
   - Entering the **decimal value** from the table above

2. **Example**: To set a Gold Ore/Ingot:
   - Use hue value: `2843` (decimal for 0xB1B)

3. **Verification**: 
   - The hue values in `Ore.cs` and `Ingots.cs` are identical
   - Both use `CraftResources.GetHue(resource)` which pulls from `ResourceInfo.cs`
   - The actual hue values come from `Materials.cs` → `GetMaterialColor()` function

## Code References

- **Ore Hues**: `Scripts/Items and addons/Resources/Blacksmithing/Ore.cs` (line 94)
- **Ingot Hues**: `Scripts/Items and addons/Resources/Blacksmithing/Ingots.cs` (line 88)
- **Hue Source**: `Scripts/Server Functions/Misc/ResourceInfo.cs` (lines 900-920)
- **Material Colors**: `Scripts/Server Functions/Functions/Materials.cs` (lines 1378-1415)

## Special Cases

Some ores override the base hue:
- **TitaniumOre**: Uses `MaterialInfo.GetMaterialColor("titanium", "classic", 0)` (same as base)
- **RoseniumOre**: Uses `MaterialInfo.GetMaterialColor("rosenium", "classic", 0)` (same as base)
- **PlatinumOre**: Uses `MaterialInfo.GetMaterialColor("platinum", "classic", 0)` (same as base)

These still match their corresponding ingots because they use the same MaterialInfo function.

## Quick Reference - Classic UO Hues (Decimal)

For quick copy-paste into CentrED (Standard UO hues):

```
Iron: 0
Dull Copper: 2419
Shadow Iron: 2406
Copper: 2413
Bronze: 2418
Gold: 2213
Agapite: 2425
Verite: 2207
Valorite: 2219
Titanium: 1381
Rosenium: 236
Platinum: 2321
Nepturite: 1422
Obsidian: 2232
Steel: 1301
Brass: 2653
Mithril: 2498
Xormite: 2966
Dwarven: 2589
```

## Quick Reference - Custom Server Hues (Decimal)

For quick copy-paste into CentrED (Your server's custom hues):

```
Iron: 0
Dull Copper: 2741
Shadow Iron: 2739
Copper: 2840
Bronze: 2236
Gold: 2843
Agapite: 2794
Verite: 2141
Valorite: 2397
Titanium: 1381
Rosenium: 236
Platinum: 2321
Nepturite: 2376
Obsidian: 2232
Steel: 2463
Brass: 2451
Mithril: 2936
Xormite: 1991
Dwarven: 1788
```

## How to Determine Which Hues to Use

1. **Check in-game**: Place an ore/ingot in-game and note its color
2. **Check CentrED**: Look at the hue picker in CentrED - it typically shows classic UO hues
3. **Match the values**: Use the table that matches what you see in-game

**Most likely**: Your server uses the **Custom Server Hues** (the second table), but CentrED's hue picker shows the **Classic UO Hues** (the first table). You may need to manually enter the custom hue values in CentrED.

