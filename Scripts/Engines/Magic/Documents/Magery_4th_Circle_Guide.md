# Magery 4th Circle Spells - Complete Guide

## Overview

The 4th Circle of Magery represents a significant power increase, introducing area-effect spells, advanced utility, and more complex mechanics. These spells require high skill (60.0 Magery) and provide game-changing capabilities for both PvP and PvM.

**Circle Level:** Expert  
**Mana Cost:** 32  
**Difficulty:** 60-80 skill  
**Total Spells:** 8

---

## Spell List

1. **Arch Cure (`Vas An Nox`)** - Area Beneficial/Poison Removal
2. **Arch Protection (`Vas Uus Sanct`)** - Area Beneficial/Defensive Buff
3. **Curse (`Des Sanct`)** - Debuff/Stat Reduction (All Stats)
4. **Fire Field (`In Flam Grav`)** - Area Control/Damage Over Time
5. **Greater Heal (`In Vas Mani`)** - Beneficial/Healing
6. **Lightning (`Por Ort Grav`)** - Direct Damage
7. **Mana Drain (`Ort Rel`)** - Offensive/Resource Denial
8. **Recall (`Kal Ort Por`)** - Transportation

---

## 1. Arch Cure (`Vas An Nox`)

### Description
Area-effect version of Cure that attempts to cure poison from all poisoned targets in a 2-tile radius. 2x more powerful than Cure spell.

### Meaning
The spell words "Vas An Nox" translate to "Great Remove Poison" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Garlic, Ginseng, Mandrake Root
- **Mana Cost:** 32
- **Target:** Ground location (area effect)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Area of Effect:** 2 tiles radius
- **Cast Time Bonus:** 0.25 seconds faster than standard 4th circle

### Cure Chance Formula
```
Base Chance = BeneficialMageryInscribePercentage × 2.0 (2x multiplier)
Healing Bonus = (Healing / 10) × 1% (1% per 10 skill points)
Penalty = Poison Level
Adjusted Chance = (Base × 2.0) + Healing Bonus - Penalty

Success Condition: Adjusted Chance ≥ Random(Poison Level × 2, 100)
```

**Special Rules:**
- **Lethal Poison (Level 4+):** Cannot be cured (returns 0% chance)
- **Area Effect:** Affects all poisoned mobiles in 2-tile radius
- **Karma Reward:** 10 karma per successfully cured target

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Inscribe | Healing | Poison Level | Cure Chance | Notes |
|----------|--------|----------|---------|--------------|-------------|-------|
| Novice | 70 | 60 | 0 | Level 1 | ~90% | Basic |
| Apprentice | 90 | 80 | 50 | Level 2 | ~110% | Improved |
| Adept | 100 | 100 | 70 | Level 3 | ~120% | Good |
| Expert | 120 | 110 | 80 | Level 3 | ~140% | Strong |
| Master | 120 | 120 | 100 | Level 3 | ~150% | Excellent |

**Level 4+ Poison:** 0% chance (cannot cure lethal poison)

### Curiosities & Easter Eggs
- **2x Power:** Twice as powerful as Cure spell
- **Area Effect:** Can cure multiple targets simultaneously
- **Faster Cast:** 0.25 seconds faster than standard 4th circle
- **Karma Bonus:** 10 karma per cured target (can stack)
- **Success Celebration:** Special message and effects when all targets cured
- **Lethal Poison Block:** Cannot cure Level 4+ poison (same as Cure)

---

## 2. Arch Protection (`Vas Uus Sanct`)

### Description
Area-effect version of Protection that applies the Protection spell to multiple targets. Provides spell disruption protection to caster and party members.

### Meaning
The spell words "Vas Uus Sanct" translate to "Great Increase Protection" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Garlic, Ginseng, Mandrake Root, Sulfurous Ash
- **Mana Cost:** 32 (AOS) / 32 (Pre-AOS)
- **Target:** Ground location (area effect)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Area of Effect:** 2 tiles (AOS) / 3 tiles (Pre-AOS)

### AOS Mode
- **Effect:** Applies Protection spell to caster and party members
- **Targets:** Only caster and party members
- **Radius:** 2 tiles

### Pre-AOS Mode
- **Effect:** Adds Virtual Armor Mod
- **Formula:** `(Magery / 10) + 1` Virtual Armor
- **Duration:** `Magery × 1.2` seconds (max 144 seconds)
- **Targets:** All beneficial mobiles in range
- **Radius:** 3 tiles

### Duration Formula

#### AOS Mode
- **Toggle Spell:** Permanent until removed (same as Protection)

#### Pre-AOS Mode
```
Duration = Magery × 1.2 seconds
Maximum = 144 seconds (2.4 minutes)
```

### Simulation Scenarios

#### AOS Mode
| Scenario | Magery | Effect | Duration | Notes |
|----------|--------|--------|----------|-------|
| Novice | 70 | Protection | Toggle | Standard |
| Apprentice | 90 | Protection | Toggle | Standard |
| Adept | 100 | Protection | Toggle | Standard |
| Expert | 120 | Protection | Toggle | Standard |
| Master | 120 | Protection | Toggle | Standard |

#### Pre-AOS Mode
| Scenario | Magery | Virtual Armor | Duration (sec) | Notes |
|----------|--------|---------------|----------------|-------|
| Novice | 70 | 8 | 84 | Basic |
| Apprentice | 90 | 10 | 108 | Improved |
| Adept | 100 | 11 | 120 | Good |
| Expert | 120 | 13 | 144 (capped) | Strong |
| Master | 120 | 13 | 144 (capped) | Maximum |

### Curiosities & Easter Eggs
- **Dual Mode:** Different mechanics for AOS vs Pre-AOS
- **Party Only (AOS):** Only affects caster and party members
- **Area Effect:** Can buff multiple allies simultaneously
- **Group Preparation:** Essential pre-combat buff for parties
- **Toggle System (AOS):** Uses Protection's toggle system

---

## 3. Curse (`Des Sanct`)

### Description
Reduces all three stats (Strength, Dexterity, Intelligence) simultaneously. The most powerful curse spell, affecting all stats and reducing resistances.

### Meaning
The spell words "Des Sanct" translate to "Decrease Sacred" or "Remove Blessing" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Garlic, Nightshade, Sulfurous Ash
- **Mana Cost:** 32
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Reduction Formula
```
Stat Reduction = Target's RawStat × GetOffsetScalar(Caster, Target, true)
GetOffsetScalar = (6 + (EvalInt / 100) - (Target's MagicResist / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)

All Stats Affected: Strength, Dexterity, Intelligence
Resistance Impact: Reduces target's magic resistances
```

### Duration Formula
```
Base Duration = NMSGetDuration (harmful spell duration)
Timer Application: Only for PvP curses (player-to-player, non-self)
Self-curses and PvM: Permanent until removed (no timer)
```

### Simulation Scenarios

| Scenario | Magery | EvalInt | Target Resist | Reduction % | Duration (sec) | Notes |
|----------|--------|---------|-------------|-------------|----------------|-------|
| Novice | 70 | 60 | 40 | ~8% each | 25-45 | Basic |
| Apprentice | 90 | 80 | 60 | ~10% each | 30-55 | Improved |
| Adept | 100 | 90 | 70 | ~12% each | 35-65 | Good |
| Expert | 120 | 110 | 90 | ~15% each | 40-75 | Strong |
| Master | 120 | 120 | 100 | ~18% each | 45-80 | Maximum |

### Curiosities & Easter Eggs
- **All Stats:** Only curse that affects all three stats
- **Resistance Reduction:** Makes target more vulnerable to other spells
- **PvP Timer:** Only uses timer for player-to-player curses
- **PvM Permanent:** Curses on creatures are permanent until removed
- **Spell Interruption:** Interrupts target's spellcasting
- **Paralysis Removal:** Removes paralysis as a side effect

---

## 4. Fire Field (`In Flam Grav`)

### Description
Creates a 5-tile line of fire that deals damage over time to anyone who steps on it or stands in it. Area control and damage over time spell.

### Meaning
The spell words "In Flam Grav" translate to "Create Fire Field" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Black Pearl, Spider's Silk, Sulfurous Ash
- **Mana Cost:** 32
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Damage Formula
```
Base Damage = GetNMSDamage(1, 2, 6, Caster) = 3-8 per tick
Damage Chance: 50% per tick (Random(0, 1) > 0)
Tick Interval: 1.0 second
```

### Friendly Fire Reduction
```
Self: 50% damage reduction
Guildmates: 50% damage reduction
Allied Guilds: 50% damage reduction
Party Members: 50% damage reduction
Own Pets: 50-100% damage (variable)
```

### Duration
```
Duration = NMSGetDuration(Caster, Caster, false)
Initial Delay = Math.Abs(tile_position) × 0.2 seconds
```

### Duration
Based on NMSGetDuration (typically 20-70 seconds for harmful spells)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Damage/Tick | Total Potential | Notes |
|----------|--------|---------|-------------|-----------------|-------|
| Novice | 70 | 60 | 3-6 | 60-180 | Basic |
| Apprentice | 90 | 80 | 4-7 | 80-280 | Improved |
| Adept | 100 | 90 | 5-8 | 100-360 | Good |
| Expert | 120 | 110 | 6-9 | 120-450 | Strong |
| Master | 120 | 120 | 7-10 | 140-560 | Maximum |

*Total potential assumes standing in field for entire duration*

### Curiosities & Easter Eggs
- **5-Tile Line:** Creates a line of 5 fire tiles
- **Orientation:** Automatically determines East-West or North-South
- **Friendly Fire:** 50% damage reduction for allies
- **Movement Block:** Blocks passage (BlocksFit = true)
- **Light Source:** Provides light (LightType.Circle300)
- **Area Control:** Essential for controlling enemy movement

---

## 5. Greater Heal (`In Vas Mani`)

### Description
Restores significantly more hit points than Heal. 2.0x more powerful than Heal spell, using the same formula with a multiplier.

### Meaning
The spell words "In Vas Mani" translate to "Great Heal" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Garlic, Ginseng, Mandrake Root, Spider's Silk
- **Mana Cost:** 32
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Healing Formula
```
Step 1: Base Calculation (same as Heal)
  BeneficialMageryInscribePercentage = (Magery × Influence) / 3
  Base Heal = BeneficialMageryInscribePercentage / 3

Step 2: Skill Bonuses (same as Heal)
  Inscription Bonus = Ceiling((Inscribe / 10) × 0.3)
  Healing Bonus = (Healing / 10) × 1

Step 3: Apply 2.0x Multiplier
  After Skill Bonuses: Multiply by 2.0

Step 4: Apply Modifiers (same as Heal)
  Other Target Bonus: × 1.15 (if healing others)
  Power Reduction: × 0.7 (30% reduction)
  Random Variance: - (2-5% of current value)
  Consecutive Cast Penalty: - 25% (if within 2 seconds)
  Self-Heal Bonus: +5% (Ceiling) when healing yourself

Final: Max(calculated, 1)
```

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Inscribe | Healing | Self-Heal | Other-Heal | Notes |
|----------|--------|----------|---------|-----------|------------|-------|
| Novice | 70 | 60 | 50 | 14-19 HP | 16-22 HP | Basic |
| Apprentice | 90 | 80 | 70 | 18-25 HP | 21-29 HP | Improved |
| Adept | 100 | 100 | 80 | 22-31 HP | 25-36 HP | Good |
| Expert | 120 | 110 | 90 | 26-37 HP | 30-43 HP | Strong |
| Master | 120 | 120 | 100 | 28-41 HP | 32-47 HP | Maximum |

*Values shown are approximate ranges after all modifiers*
*Power ratio vs Heal: 2.0x to 2.5x depending on skill level*

### Curiosities & Easter Eggs
- **2x Power:** Exactly 2.0x more powerful than Heal (same formula × 2.0)
- **Same Bonuses:** Uses identical skill bonuses and modifiers as Heal
- **Consecutive Cast Penalty:** Same 25% penalty if cast within 2 seconds
- **Self-Heal Bonus:** 5% bonus when healing yourself (you know your body better)
- **Overhead Display:** Shows healed amount on target
- **Cannot Heal:** Same restrictions as Heal (golems, lethal poison, etc.)

---

## 6. Lightning (`Por Ort Grav`)

### Description
Strikes the target with instant lightning damage. Fast, reliable energy damage that cannot be dodged.

### Meaning
The spell words "Por Ort Grav" translate to "Lightning Strike" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Mandrake Root, Sulfurous Ash
- **Mana Cost:** 32
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Damage Formula
```
Base Damage = Dice(1, 6) + 6 = 7-12 base (4th circle parameters)
Final Damage = Base × EvalInt Benefit
Minimum Damage Floor (EvalInt-based):
  - 120+ EvalInt: 6 minimum
  - 100-119 EvalInt: 5 minimum
  - 80-99 EvalInt: 4 minimum
  - Below 80: 3 minimum
Final = Max(calculated, minimum floor)
```

### Duration
N/A (instant damage, no delay)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Base Damage | Final Damage | Min Floor | Notes |
|----------|--------|---------|-------------|--------------|-----------|-------|
| Novice | 70 | 60 | 7-12 | 7-14 | 3 | Low |
| Apprentice | 90 | 80 | 7-12 | 8-16 | 4 | Moderate |
| Adept | 100 | 90 | 7-12 | 9-18 | 5 | Good |
| Expert | 120 | 110 | 7-12 | 10-20 | 5 | Strong |
| Master | 120 | 120 | 7-12 | 11-22 | 6 | Maximum |

### Curiosities & Easter Eggs
- **100% Energy Damage:** All damage is energy type
- **Instant Damage:** No travel time (cannot be dodged)
- **Visual Effect:** Custom spell hue creates blast effect (0x2A4E)
- **Minimum Damage Floor:** High EvalInt guarantees minimum damage
- **Reflection:** Can be reflected back at caster
- **Circle Progression:** 4th circle damage (1d6+6) is stronger than 3rd (1d6+4)

---

## 7. Mana Drain (`Ort Rel`)

### Description
Drains mana from the target and returns it after a duration. Resource denial spell essential for PvP mage combat.

### Meaning
The spell words "Ort Rel" translate to "Drain Mana" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Black Pearl, Mandrake Root, Spider's Silk
- **Mana Cost:** 32
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Mana Drain Formula
```
Mage Bonus = Magery × EvalInt Benefit
Drain Amount = (Mage Bonus / 10) × 1.5

Limits:
  Players: Maximum 30% of target's ManaMax
  Creatures: Maximum 50% of target's ManaMax
  Cannot exceed target's current mana
  If already under effect: 0 drain (no stacking)
```

### Duration Formula
```
Duration = (5 × EvalInt Benefit) + Random(1-3) seconds
```

### Simulation Scenarios

| Scenario | Magery | EvalInt | Drain Amount | Duration (sec) | Notes |
|----------|--------|---------|--------------|---------------|-------|
| Novice | 70 | 60 | ~10-12 | 6-8 | Basic |
| Apprentice | 90 | 80 | ~13-16 | 7-9 | Improved |
| Adept | 100 | 90 | ~15-18 | 8-10 | Good |
| Expert | 120 | 110 | ~18-22 | 9-11 | Strong |
| Master | 120 | 120 | ~20-24 | 10-12 | Maximum |

*Drain amounts assume target has sufficient mana and are capped by limits*

### Curiosities & Easter Eggs
- **Resource Denial:** Essential for PvP mage vs mage combat
- **Mana Return:** Drained mana is returned after duration expires
- **No Stacking:** Cannot drain target already under effect
- **Player Limit:** Players can only lose 30% max mana (creatures 50%)
- **Spell Interruption:** Interrupts target's spellcasting
- **Paralysis Removal:** Removes paralysis as a side effect

---

## 8. Recall (`Kal Ort Por`)

### Description
Teleports caster and pets to a marked location (rune, runebook, boat key, or house deed). Fast travel spell with complex travel restrictions.

### Meaning
The spell words "Kal Ort Por" translate to "Travel to Marked Location" in the magical language.

### Requirements
- **Minimum Magery:** 60.0
- **Reagents:** Black Pearl, Bloodmoss, Mandrake Root
- **Mana Cost:** 32
- **Target:** Recall Rune, Runebook, Boat Key, or House Raffle Deed
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Weight Check:** Cannot recall if overloaded

### Travel Restrictions

#### From Location
- Cannot recall if overloaded
- Must pass `TravelCheckType.RecallFrom`
- Region must allow escape
- Region must allow recall

#### To Location
- Must pass `TravelCheckType.RecallTo`
- Destination region must allow teleport
- Must have discovered the destination world
- Cannot recall to blocked locations (multi-check)

#### Special Cases
- **Staff:** Bypasses all restrictions
- **Runebook:** No skill check required (uses item charge)
- **Wraith Form:** No skill check required

### Duration
N/A (instant teleportation)

### Simulation Scenarios

| Scenario | Magery | Success Rate | Notes |
|----------|--------|--------------|-------|
| Novice | 70 | High* | Basic |
| Apprentice | 90 | High* | Improved |
| Adept | 100 | High* | Good |
| Expert | 120 | High* | Strong |
| Master | 120 | High* | Maximum |

*Success rate depends on travel restrictions and discovery, not skill level*

### Curiosities & Easter Eggs
- **Multiple Targets:** Can target runes, runebooks, boat keys, house deeds
- **Discovery System:** Must have discovered destination world
- **Criminal Handling:** Special handling in DarkMoor and Temple of Praetoria
- **Pet Teleport:** Pets teleport with caster automatically
- **Visual Effects:** Custom spell hue affects teleport particles
- **Staff Bypass:** Staff members bypass all restrictions
- **Runebook:** No skill check when using runebook (item charge)

---

## Common Patterns

### Area Effect Spells
4th circle introduces area effects:
- **Arch Cure:** 2-tile radius
- **Arch Protection:** 2-3 tile radius (depending on mode)
- **Fire Field:** 5-tile line

### Power Scaling
4th circle shows significant power increases:
- **Greater Heal:** 2.0x more powerful than Heal
- **Arch Cure:** 2.0x more powerful than Cure + area effect
- **Lightning:** Higher base damage (1d6+6 vs 1d6+4 for Fireball)

### Duration System
- **Harmful Spells:** Use EvalInt for duration bonus
- **Beneficial Spells:** Use Inscription for duration bonus
- **Beneficial Reduction:** 30% reduction applied (15-90 second cap)

---

## Strategic Applications

### PvP Strategies
1. **Mana Drain:** Deny enemy mages their primary resource
2. **Curse:** Weaken enemies before engaging
3. **Fire Field:** Area denial and control
4. **Lightning:** Fast, reliable damage
5. **Recall:** Escape mechanism

### PvM Strategies
1. **Greater Heal:** Primary healing for mid-level content
2. **Arch Cure:** Essential for poison-heavy areas
3. **Arch Protection:** Pre-battle group preparation
4. **Recall:** Fast travel between locations

### Group Play
1. **Arch Cure + Arch Protection:** Standard group preparation combo
2. **Fire Field + Mana Drain:** Control and resource denial
3. **Greater Heal:** Support healing for party members

---

## Summary

The 4th Circle provides game-changing capabilities:
- **Area Effects:** Arch Cure, Arch Protection, Fire Field
- **Advanced Utility:** Recall with complex travel restrictions
- **Resource Management:** Mana Drain for PvP control
- **Powerful Healing:** Greater Heal (2.0x Heal)
- **Direct Damage:** Lightning for reliable energy damage
- **Debuffing:** Curse for comprehensive stat reduction
- **Control:** Fire Field for area denial

These spells represent a significant power increase and require strategic thinking to use effectively, especially in PvP scenarios where friendly fire, reflection, and resource management become critical factors.

