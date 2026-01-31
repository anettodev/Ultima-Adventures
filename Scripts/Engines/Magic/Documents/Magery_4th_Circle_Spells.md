# Magery 4th Circle Spells - Complete Analysis

## Overview

The 4th Circle of Magery represents a significant power increase, introducing area-effect spells, advanced utility, and more complex mechanics. This document provides a comprehensive analysis of all eight 4th circle spells, their mechanics, formulas, and strategic applications.

---

## Spell List

1. **Recall** (`Kal Ort Por`) - Transportation
2. **Mana Drain** (`Ort Rel`) - Offensive/Resource Denial
3. **Greater Heal** (`In Vas Mani`) - Beneficial/Healing
4. **Lightning** (`Por Ort Grav`) - Direct Damage
5. **Fire Field** (`In Flam Grav`) - Area Control/Damage Over Time
6. **Curse** (`Des Sanct`) - Debuff/Stat Reduction
7. **Arch Cure** (`Vas An Nox`) - Area Beneficial/Poison Removal
8. **Arch Protection** (`Vas Uus Sanct`) - Area Beneficial/Defensive Buff

---

## 1. Recall (`Kal Ort Por`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 239
- **Reagents:** Black Pearl, Bloodmoss, Mandrake Root
- **Type:** Utility/Transportation

### Mechanics

#### Casting Requirements
- **Standard Casting:** Requires skill check via `GetCastSkills()`
- **Runebook Casting:** No skill check required (uses item charge)
- **Wraith Form:** No skill check required (transformation bypass)

#### Target Selection
The spell can target:
- **Recall Rune** - Teleports to rune's marked location
- **Runebook** - Teleports to default entry location
- **Boat Key** - Teleports to boat's marked location (if valid)
- **House Raffle Deed** - Teleports to plot location

#### Travel Restrictions

**From Location Checks:**
- Cannot recall if overloaded (weight check)
- Must pass `TravelCheckType.RecallFrom` validation
- Region must allow escape: `Worlds.AllowEscape()`
- Region must allow recall: `Worlds.RegionAllowedRecall()`

**To Location Checks:**
- Must pass `TravelCheckType.RecallTo` validation
- Destination region must allow teleport: `Worlds.RegionAllowedTeleport()`
- Must have discovered the destination world: `CharacterDatabase.GetDiscovered()`
- Cannot recall to blocked locations (multi-check via `SpellHelper.CheckMulti()`)

**Special Cases:**
- **Staff (AccessLevel > Player):** Bypasses all travel restrictions
- **Criminal Players:** Special handling in DarkMoor and Temple of Praetoria regions

#### Effects
- Teleports caster and pets to destination
- Consumes Runebook charges if used
- Visual/audio effects on departure and arrival
- Uses custom spell hue from `CharacterDatabase.GetMySpellHue()`

#### Error Messages (Portuguese)
- "Você está muito pesado para se teletransportar." - Too heavy
- "Esse feitiço parece não funcionar neste lugar." - Region blocked
- "O destino parece magicamente inacessível." - Destination blocked
- "Você não conhece esse local e não têm ideia de como mentaliza-lo!" - Undiscovered location
- "Esse local está bloqueado para o uso de teletransporte!" - Location blocked
- "Não há mais cargas nesse item!" - No charges remaining

---

## 2. Mana Drain (`Ort Rel`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 215
- **Reagents:** Black Pearl, Mandrake Root, Spider's Silk
- **Type:** Offensive/Resource Denial

### Mechanics

#### Mana Drain Calculation
```csharp
double magebonus = (Caster.Skills.Magery.Value * NMSUtils.getDamageEvalBenefit(Caster));
int toDrain = (int)((magebonus / 10) * 1.5);
```

**Limits:**
- **Players:** Maximum 30% of target's `ManaMax`
- **Creatures:** Maximum 50% of target's `ManaMax`
- Cannot exceed target's current mana
- If target already under Mana Drain effect: `toDrain = 0` (no additional drain)

#### Duration Calculation
```csharp
int seconds = (int)(5 * NMSUtils.getDamageEvalBenefit(Caster)) + Utility.RandomMinMax(1, 3);
```

#### Effect Flow
1. Drains mana immediately from target
2. Stores drained amount in timer
3. After duration expires, returns mana to target
4. Target cannot be drained again while effect is active

#### Special Mechanics
- **Spell Interruption:** If target is casting, `m.Spell.OnCasterHurt()` is called
- **Paralysis Removal:** Removes paralysis from target
- **Reflection:** Can be reflected via `SpellHelper.CheckReflect()`
- **Visual Effects:** Uses custom spell hue from `CharacterDatabase.GetMySpellHue()`

#### Example Calculations

**Example 1: Magery 100, DamageEvalBenefit 1.0**
```
magebonus = 100 * 1.0 = 100
toDrain = (100 / 10) * 1.5 = 15
Duration = (5 * 1.0) + 1-3 = 6-8 seconds
```

**Example 2: Magery 120, DamageEvalBenefit 1.2**
```
magebonus = 120 * 1.2 = 144
toDrain = (144 / 10) * 1.5 = 21.6 → 21
Duration = (5 * 1.2) + 1-3 = 7-9 seconds
```

**Example 3: Target with 100 Max Mana (Player)**
```
Max drain = 100 * 0.3 = 30 mana
If toDrain calculated > 30, capped at 30
```

---

## 3. Greater Heal (`In Vas Mani`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 204
- **Reagents:** Garlic, Ginseng, Mandrake Root, Spider's Silk
- **Type:** Beneficial/Healing

### Mechanics

#### Healing Calculation
```csharp
int toHeal = (int)(NMSUtils.getBeneficialMageryInscribePercentage(Caster) / 1.5);
if (Caster != m)
    toHeal = (int)(toHeal * 1.15); // 15% bonus when healing others
```

**Healing Formula:**
- Base heal = `BeneficialMageryInscribePercentage / 1.5`
- Self-heal: Base amount
- Healing others: Base amount × 1.15 (15% bonus)

#### Restrictions

**Cannot Heal:**
- Dead bonded pets (`IsDeadBondedPet`)
- Animated dead creatures (`IsAnimatedDead`)
- Golems
- Targets with Level 4+ poison
- Targets with Mortal Strike wound (`MortalStrike.IsWounded()`)
- Targets wearing One Ring (special item blocks healing)

#### Effects
- Uses `SpellHelper.Heal()` for proper healing mechanics
- Visual particles and sound effects
- Custom spell hue from `CharacterDatabase.GetMySpellHue()`

#### Example Calculations

**Example 1: Magery 100, BeneficialMageryInscribePercentage = 60**
```
Base heal = 60 / 1.5 = 40 HP
Self-heal = 40 HP
Healing others = 40 * 1.15 = 46 HP
```

**Example 2: Magery 120, BeneficialMageryInscribePercentage = 80**
```
Base heal = 80 / 1.5 = 53.3 → 53 HP
Self-heal = 53 HP
Healing others = 53 * 1.15 = 60.95 → 60 HP
```

---

## 4. Lightning (`Por Ort Grav`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 239
- **Reagents:** Mandrake Root, Sulfurous Ash
- **Type:** Direct Damage

### Mechanics

#### Damage Calculation
```csharp
damage = GetNMSDamage(4, 1, 6, m) + nBenefit;
```

**Parameters:**
- Circle: 4
- Min multiplier: 1
- Max multiplier: 6
- Target: m

**Damage Type:** 100% Energy damage
**Delay:** Instant (`DelayedDamage = false`)

#### Visual Effects
- **With Custom Spell Hue:** Blast effect at target location (`0x2A4E`)
- **Without Custom Spell Hue:** Standard bolt effect (`BoltEffect()`)

#### Special Mechanics
- Can be reflected via `SpellHelper.CheckReflect()`
- Turns caster to face target
- Uses custom spell hue from `CharacterDatabase.GetMySpellHue()`

#### Example Damage Range
Assuming `GetNMSDamage()` returns base damage of 20-40:
- Minimum: 20 damage
- Maximum: 40 damage
- Average: ~30 damage

---

## 5. Fire Field (`In Flam Grav`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 215
- **Reagents:** Black Pearl, Spider's Silk, Sulfurous Ash
- **Type:** Area Control/Damage Over Time

### Mechanics

#### Field Creation
Creates a 5-tile line of fire:
- **Orientation:** East-West or North-South (calculated from cast angle)
- **Calculation:**
  ```csharp
  int dx = Caster.Location.X - p.X;
  int dy = Caster.Location.Y - p.Y;
  int rx = (dx - dy) * 44;
  int ry = (dx + dy) * 44;
  ```
- **Direction:** Determined by sign of `rx` and `ry`

#### Damage Calculation
```csharp
int damage = GetNMSDamage(1, 2, 6, Caster) + nBenefit;
```

**Per Tick:**
- Base damage: `GetNMSDamage(1, 2, 6, Caster)`
- Damage chance: 50% per tick (`Utility.RandomMinMax(0, 1) > 0`)

#### Duration
```csharp
duration = SpellHelper.NMSGetDuration(Caster, Caster, false);
```

#### Friendly Fire Reduction
**50% Damage Reduction For:**
- Self
- Guildmates (`fromGuild == toGuild`)
- Allied guilds (`fromGuild.IsAlly(toGuild)`)
- Party members (`Party.Get(from).Contains(to)`)

**Pet Damage:**
- Own pets: 50-100% damage (variable: `Utility.RandomMinMax(damage/2, damage)`)
- Other creatures: Full damage minus 0-1 random reduction

#### Field Properties
- **Blocks Movement:** `BlocksFit = true`
- **Light Source:** `LightType.Circle300`
- **Visibility:** Hidden initially, becomes visible after LOS check
- **Damage Triggers:**
  - On move-over (`OnMoveOver()`)
  - Periodically while standing in field (timer tick)

#### Timer Mechanics
- Initial delay: `Math.Abs(val) * 0.2` seconds (based on tile position)
- Tick interval: 1.0 second
- Checks for mobiles in range each tick
- Field expires after duration

#### Example Scenarios

**Scenario 1: Standard Fire Field**
```
Damage per tick: 2-6 (if 50% chance succeeds)
Duration: Based on NMSGetDuration (typically 15-30 seconds)
Total potential damage: 30-180 (if standing in field entire duration)
```

**Scenario 2: Friendly Fire**
```
Normal damage: 4
Friendly damage: 1-2 (50% reduction)
```

---

## 6. Curse (`Des Sanct`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 227
- **Reagents:** Nightshade, Garlic, Sulfurous Ash
- **Type:** Debuff/Stat Reduction

### Mechanics

#### Stat Reduction
Applies curses to all three stats:
```csharp
SpellHelper.AddStatCurse(Caster, m, StatType.Str);
SpellHelper.AddStatCurse(Caster, m, StatType.Dex);
SpellHelper.AddStatCurse(Caster, m, StatType.Int);
```

**Effect:**
- Reduces Strength, Dexterity, and Intelligence
- Percentage based on caster skill vs target resistance
- Formula: `SpellHelper.GetOffsetScalar(Caster, m, true) * 100`

#### Duration
```csharp
TimeSpan duration = SpellHelper.NMSGetDuration(Caster, m, false);
```

**Timer Application:**
- Only applies timer for PvP curses (player-to-player, non-self)
- Self-curses and PvM curses don't use timer (permanent until removed)

#### Resistance Impact
```csharp
m.UpdateResistances();
```
- Reduces target's magic resistances
- Makes target more vulnerable to other spells

#### Tracking
- Adds target to static list: `CurseSpell.Cursed`
- Tracks in hashtable: `m_UnderEffect` (for timer management)
- Can check if mobile is cursed: `CurseSpell.UnderEffect(m)`

#### Special Mechanics
- **Reflection:** Can be reflected via `SpellHelper.CheckReflect()`
- **Spell Interruption:** If target is casting, `m.Spell.OnCasterHurt()` is called
- **Paralysis Removal:** Removes paralysis from target
- **Buff Icon:** Displays curse debuff icon to target

#### Example Stat Reduction

**Example 1: Caster 100 Magery vs Target 50 Resist**
```
Offset Scalar: ~0.25 (25% reduction)
Result: All stats reduced by 25%
```

**Example 2: Caster 120 Magery vs Target 30 Resist**
```
Offset Scalar: ~0.40 (40% reduction)
Result: All stats reduced by 40%
```

---

## 7. Arch Cure (`Vas An Nox`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 215
- **Reagents:** Garlic, Ginseng, Mandrake Root
- **Type:** Area Beneficial/Poison Removal

### Mechanics

#### Cast Time Bonus
```csharp
public override TimeSpan CastDelayBase { 
    get { return base.CastDelayBase - TimeSpan.FromSeconds(0.25); } 
}
```
- **0.25 seconds faster** than standard 4th circle cast time

#### Area of Effect
- **Radius:** 2 tiles from target point
- **Target Selection:** Only affects poisoned mobiles in range

#### Cure Calculation
```csharp
int chanceToCure = (int)NMSUtils.getBeneficialMageryInscribePercentage(Caster);
chanceToCure -= (poison.Level >= 4) ? poison.Level * 2 : poison.Level;
if (chanceToCure < 0) chanceToCure = 0;
```

**Success Condition:**
```csharp
if (chanceToCure >= Utility.RandomMinMax(poison.Level * 2, 100) && m.CurePoison(Caster))
```

**Formula Breakdown:**
- Base chance = `BeneficialMageryInscribePercentage`
- Penalty = `poison.Level` (or `poison.Level * 2` for Level 4+)
- Must roll >= `(poison.Level * 2)` to succeed
- Lethal poison (Level 4+) has double penalty

#### Rewards System
- **Karma Bonus:** 10 karma per cured target
- **Success Messages:**
  - All cured: Special message + sound effect + visual
  - Partial cure: Standard message
  - Failure: Failure message

#### Visual Effects
- **Success:** Green particles (`0x373A`), sound `0x1E0`
- **Failure:** Purple particles (`0x374A`), sound `342`

#### Example Calculations

**Example 1: Magery 100, Level 1 Poison**
```
Base chance = 60 (example)
Penalty = 1
Adjusted chance = 59
Must roll >= 2 (1 * 2)
Success if: 59 >= Random(2, 100) → ~59% chance
```

**Example 2: Magery 120, Level 4 Poison (Lethal)**
```
Base chance = 80 (example)
Penalty = 4 * 2 = 8
Adjusted chance = 72
Must roll >= 8 (4 * 2)
Success if: 72 >= Random(8, 100) → ~77% chance
```

**Example 3: Magery 80, Level 5 Poison (Lethal)**
```
Base chance = 50 (example)
Penalty = 5 * 2 = 10
Adjusted chance = 40
Must roll >= 10 (5 * 2)
Success if: 40 >= Random(10, 100) → ~44% chance
```

---

## 8. Arch Protection (`Vas Uus Sanct`)

### Spell Information
- **Circle:** Fourth
- **Mana Cost:** 239 (AOS) / 215 (Pre-AOS)
- **Reagents:** Garlic, Ginseng, Mandrake Root, Sulfurous Ash
- **Type:** Area Beneficial/Defensive Buff

### Mechanics

#### Two Implementation Modes

**AOS Mode (Core.AOS = true):**
- Applies `ProtectionSpell` (2nd circle spell) to targets
- **Targets:** Only caster and party members
- **Radius:** 2 tiles
- Uses existing Protection spell mechanics

**Pre-AOS Mode (Core.AOS = false):**
- Adds Virtual Armor Mod to targets
- **Targets:** All beneficial mobiles in range
- **Radius:** 3 tiles

#### Pre-AOS Protection Calculation
```csharp
int val = (int)(Caster.Skills[SkillName.Magery].Value / 10.0 + 1);
m.VirtualArmorMod += val;
```

**Duration:**
```csharp
double time = caster.Skills[SkillName.Magery].Value * 1.2;
if (time > 144)
    time = 144;
Delay = TimeSpan.FromSeconds(time);
```

**Formula:**
- Protection value = `(Magery / 10) + 1`
- Duration = `Magery * 1.2` seconds (max 144 seconds)

#### Target Selection
```csharp
if (Caster.CanBeBeneficial(m, false))
    targets.Add(m);
```

**AOS Mode:**
- Only caster and party members receive protection

**Pre-AOS Mode:**
- All mobiles that pass `CanBeBeneficial()` check

#### Example Calculations

**Example 1: Magery 100 (Pre-AOS)**
```
Protection value = (100 / 10) + 1 = 11 Virtual Armor
Duration = 100 * 1.2 = 120 seconds (2 minutes)
```

**Example 2: Magery 120 (Pre-AOS)**
```
Protection value = (120 / 10) + 1 = 13 Virtual Armor
Duration = 120 * 1.2 = 144 seconds (2.4 minutes, capped)
```

**Example 3: Magery 50 (Pre-AOS)**
```
Protection value = (50 / 10) + 1 = 6 Virtual Armor
Duration = 50 * 1.2 = 60 seconds (1 minute)
```

---

## Common Patterns and Mechanics

### Spell Hue System
All spells use custom spell coloring:
```csharp
Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0)
```
- Allows players to customize spell visual effects
- Returns hue value for particles and effects
- Default: 0 (standard colors)

### Damage Calculation System
Offensive spells use `GetNMSDamage()`:
```csharp
GetNMSDamage(circle, minMult, maxMult, target)
```
- Standardized damage calculation
- Scales with circle level
- Considers target properties

### Duration System
Area and timed effects use:
```csharp
SpellHelper.NMSGetDuration(Caster, target, beneficial)
```
- Standardized duration calculation
- Scales with caster skill
- Different for beneficial vs harmful effects

### Reflection System
Harmful spells check for reflection:
```csharp
SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);
```
- Can reflect spells back at caster
- Based on target's reflection chance
- Modifies target reference if reflected

### Spell Interruption
Many spells interrupt target's casting:
```csharp
if (m.Spell != null)
    m.Spell.OnCasterHurt();
```
- Disrupts ongoing spell casting
- Prevents spell completion

### Paralysis Removal
Several spells remove paralysis:
```csharp
m.Paralyzed = false;
```
- Side effect of certain spells
- Allows movement after spell impact

### Localization
Error messages use Portuguese:
- Custom messages via `Caster.SendMessage(55, "message")`
- Color code 55 for error messages
- Color code 33 for warnings
- Color code 2253 for success

---

## Strategic Applications

### PvP Strategies

**Mana Drain:**
- Deny enemy spellcasters their primary resource
- Force melee combat or retreat
- Combine with other offensive spells for kill potential

**Curse:**
- Reduce enemy combat effectiveness
- Lower resistances for follow-up spells
- Weaken before engaging

**Fire Field:**
- Area denial and control
- Force movement or take damage
- Block escape routes

**Lightning:**
- Fast, reliable damage
- Good for finishing wounded enemies
- Cannot be dodged (instant)

### PvM Strategies

**Greater Heal:**
- Primary healing spell for mid-level content
- More efficient than lower circle heals
- Can heal party members with bonus

**Arch Cure:**
- Essential for poison-heavy areas
- Group support spell
- Rewards karma for successful cures

**Arch Protection:**
- Pre-battle preparation
- Group defensive buff
- Reduces incoming damage

**Recall:**
- Fast travel between locations
- Escape mechanism
- Resource gathering efficiency

### Group Play

**Arch Cure + Arch Protection:**
- Standard group preparation combo
- Arch Protection first, then Arch Cure if needed
- Maximizes group survivability

**Fire Field + Mana Drain:**
- Control enemy movement
- Deny resources
- Area denial strategy

---

## Spell Synergies

### Offensive Combos
1. **Curse → Lightning:** Weaken then finish
2. **Mana Drain → Fire Field:** Deny resources and control area
3. **Fire Field → Lightning:** Area control then direct damage

### Defensive Combos
1. **Arch Protection → Greater Heal:** Buff then heal
2. **Arch Cure → Greater Heal:** Remove poison then heal damage
3. **Recall → Arch Protection:** Escape then prepare

### Utility Combos
1. **Recall → Greater Heal:** Fast travel then heal
2. **Arch Cure → Arch Protection:** Remove debuffs then buff

---

## Technical Notes

### Performance Considerations
- **Fire Field:** Creates 5 items with timers (moderate performance impact)
- **Arch Cure/Protection:** Area scans (2-3 tile radius, minimal impact)
- **Mana Drain:** Timer per target (lightweight)
- **Curse:** Static tracking lists (efficient)

### Code Patterns
- All spells inherit from `MagerySpell`
- Use `InternalTarget` classes for targeting
- Follow `OnCast()` → `Target()` → `FinishSequence()` pattern
- Implement proper sequence checking (`CheckSequence()`, `CheckHSequence()`, `CheckBSequence()`)

### Compatibility
- Supports both AOS and Pre-AOS mechanics (Arch Protection)
- Custom NMS (New Magic System) integration
- Character database integration for spell hues
- World system integration for travel restrictions

---

## Summary

The 4th Circle of Magery introduces:

1. **Area Effects:** Arch Cure, Arch Protection, Fire Field
2. **Advanced Utility:** Recall with complex travel restrictions
3. **Resource Management:** Mana Drain for PvP control
4. **Powerful Healing:** Greater Heal with bonus for others
5. **Direct Damage:** Lightning for reliable damage
6. **Debuffing:** Curse for stat reduction
7. **Control:** Fire Field for area denial

These spells represent a significant power increase and require strategic thinking to use effectively, especially in PvP scenarios where friendly fire, reflection, and resource management become critical factors.

