# Magery 7th Circle Spells - Complete Guide

## Overview

The 7th Circle of Magery represents mastery-level magic, introducing devastating area-of-effect damage spells, powerful utility magic, and advanced transformation abilities. These spells require expert skill (100.0+ Magery) and provide game-changing capabilities for both PvP and PvM.

**Circle Level:** Master  
**Mana Cost:** 40  
**Difficulty:** 100-120 skill  
**Total Spells:** 8

---

## Spell List

1. **Chain Lightning (`Vas Ort Grav`)** - Area-of-Effect Lightning Damage
2. **Energy Field (`In Sanct Grav`)** - Area Control/Movement Blocking
3. **Flame Strike (`Kal Vas Flam`)** - Single-Target Fire Damage
4. **Gate Travel (`Vas Rel Por`)** - Two-Way Portal Creation
5. **Mana Vampire (`Ort Sanct`)** - Mana Drain/Transfer
6. **Mass Dispel (`Vas An Ort`)** - Area Creature/Field Dispel
7. **Meteor Swarm (`Flam Kal Des Ylem`)** - Area-of-Effect Fire Damage
8. **Polymorph (`Vas Ylem Rel`)** - Self-Transformation

---

## 1. Chain Lightning (`Vas Ort Grav`)

### Description
Area-of-effect lightning damage spell that chains between multiple targets in range. Damage is divided among all targets, with a minimum damage floor. Can be resisted, and has special interactions with the One Ring (Lord of the Rings easter egg). Always reveals hidden targets (except One Ring protection). No house protection - damage applies everywhere.

### Meaning
The spell words "Vas Ort Grav" translate to "Great Summon Energy" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Black Pearl, Bloodmoss, Mandrake Root, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Ground location (IPoint3D)
- **Range:** 12 tiles (ML) / 14 tiles (Legacy)
- **AoE Range:** 5 tiles radius
- **Line of Sight:** Required

### Damage Formula
```
Base Damage = GetNMSDamage(28, 2, 5, target)
= 28 + 2d5 = 30-38 base damage (reduced by 25% for balance)
+ EvalInt scaling (Magery does not affect damage)
+ Inscription bonus (currently not implemented)

If Multiple Targets: Damage / Target Count
Minimum Damage Floor: 12 (even after division)
If Resisted: × 0.5
One Ring Protection: × 0.5 (damage reduction)
```

### Special Mechanics
- **Damage Splitting:** Total damage divided by number of targets
- **Minimum Floor:** Each target receives at least 12 damage
- **Always Reveals:** Hidden targets are always revealed (except One Ring protection)
- **No House Protection:** Damage applies everywhere, regardless of region
- **Caster Damage:** Caster can also receive damage (respects all rules)
- **One Ring:** Reduces damage by 50% and prevents reveal
- **Resistance:** Can reduce damage by 50%

### Duration
N/A (instant damage)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Targets | Base Dice | Per Target | Resisted | Final Range | Notes |
|----------|--------|---------|---------|-----------|------------|----------|-------------|-------|
| Novice | 100 | 90 | 1 | 30-38 | 41-52 | 50% | 21-26 | Single target |
| Apprentice | 110 | 100 | 3 | 30-38 | 14-18 | 50% | 12-12 | Floor applied |
| Adept | 120 | 110 | 5 | 30-38 | 9-11 | 50% | 12-12 | Floor applied |
| Expert | 120 | 120 | 2 | 30-38 | 22-28 | 50% | 11-14 | Good split |
| Master | 120 | 120 | 1 | 30-38 | 44-55 | 50% | 22-28 | Maximum single |

### Curiosities & Easter Eggs
- **One Ring Protection:** Reduces damage and prevents reveal (Lord of the Rings reference)
- **Always Reveals:** No house protection - hidden targets always revealed (except One Ring)
- **Damage Floor:** Ensures minimum 12 damage even with many targets
- **Reduced Damage:** Base damage reduced by 25% for balance (28+2d5 instead of 38+2d5)
- **Visual Effect:** Custom particle effect (0x2A4E) when caster has spell hue
- **Bolt Effect:** Standard lightning bolt when no custom hue
- **Sound:** Distinctive lightning crack (0x029)

---

## 2. Energy Field (`In Sanct Grav`)

### Description
Creates a 5-tile line of blocking energy that prevents movement through it. Allies and the caster can pass freely, but enemies are blocked. Field orientation automatically adjusts based on caster position and facing direction (especially when self-targeting). Duration scales with Magery and Inscription skills.

### Meaning
The spell words "In Sanct Grav" translate to "Create Sacred Field" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Black Pearl, Mandrake Root, Spider's Silk, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Field Length:** 5 tiles (-2 to +2 from target point)

### Field Duration Formula
```
Duration = FieldSpellHelper.GetFieldDuration(caster)
= NMSGetDuration(caster, caster, beneficial=false)
Typically: 20-70 seconds based on Magery + Inscription
```

### Field Mechanics
- **5-Tile Line:** East-West or North-South orientation
- **Auto-Orientation:** Uses FieldSpellHelper.GetFieldOrientation
- **Self-Targeting:** Uses caster's facing direction when targeting self
- **Allies Pass:** Caster, allies, and staff can pass freely
- **Enemies Blocked:** Hostile mobiles cannot pass
- **Item ID:** 0x3946 (E-W) / 0x3956 (N-S)

### Duration
**Field:** 20-70 seconds (based on skills)

### Simulation Scenarios

| Scenario | Magery | Inscription | Field Duration (s) | Notes |
|----------|--------|-------------|-------------------|-------|
| Novice | 100 | 80 | 30-40 | Basic |
| Apprentice | 110 | 100 | 40-50 | Improved |
| Adept | 120 | 110 | 50-60 | Good |
| Expert | 120 | 120 | 55-65 | Strong |
| Master | 120 | 120 | 60-70 | Maximum |

### Easter Egg Chance Scenarios

| Scenario | Magery | EvalInt | Base Chance | EvalInt Bonus | Total Chance | Notes |
|----------|--------|---------|-------------|---------------|--------------|-------|
| Novice | 100 | 90 | 20.0% | 9.0% | 29.0% | Basic |
| Apprentice | 110 | 100 | 22.0% | 10.0% | 32.0% | Improved |
| Adept | 120 | 110 | 24.0% | 11.0% | 35.0% | Good |
| Expert | 120 | 120 | 24.0% | 12.0% | 36.0% | Strong |
| Master | 120 | 120 | 24.0% | 12.0% | 36.0% | Maximum |

### Special Easter Egg Feature
**Purple Damaging Energy Field:**
- **Chance Calculation:**
  - Base: 0.2% per Magery skill point
  - Bonus: +1% per 10 EvalInt points
  - Example: 120 Magery + 120 EvalInt = 24% + 12% = 36% chance
- **Special Properties:**
  - Field color: Purple (hue 0x10)
  - Deals energy damage to enemies within 1 tile
  - Damage calculation: Same as Lightning spell (4th circle), then halved
  - Damage is resistible (4th circle resistance check)
  - Proximity check: Every 3 seconds
  - Damage prevention: Only damages once per second per mobile
- **Visual Effects:**
  - Purple energy particles (0x2A4E) on damage
  - Distinctive sound effect (0x029)
  - Special message to caster when created

### Curiosities & Easter Eggs
- **Self-Targeting Fix:** Uses facing direction when casting on yourself
- **Notoriety Check:** Allies pass freely, enemies blocked
- **Light Source:** Field emits Circle300 light
- **Line of Sight:** Field only visible if caster has LOS
- **Dispellable:** Can be removed with Dispel Field or Mass Dispel
- **Visual Effect:** Distinctive energy particles (0x376A)
- **Sound:** Energy field creation sound (0x20B)
- **Purple Damaging Field:** Rare easter egg feature (see Special Easter Egg Feature above)

---

## 3. Flame Strike (`Kal Vas Flam`)

### Description
Single-target fire damage spell with high base damage. One of the strongest direct damage spells in the 7th circle. Pure fire damage with full resistance application. Can be reflected. Works with Soul Shard scrolls.

### Meaning
The spell words "Kal Vas Flam" translate to "Summon Great Flame" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Spider's Silk, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Damage Formula
```
Damage = GetNMSDamage(32, 1, 6, target)
Base: 32 + 1d6 = 33-38 base damage (reduced by 20% for balance)
+ Magery/EvalInt scaling
+ Inscription bonus
```

### Duration
N/A (instant damage)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Inscription | Base Damage | Final Range | Notes |
|----------|--------|---------|-------------|-------------|-------------|-------|
| Novice | 100 | 90 | 80 | 33-38 | 37-47 | Basic |
| Apprentice | 110 | 100 | 100 | 33-38 | 42-52 | Improved |
| Adept | 120 | 110 | 110 | 33-38 | 47-57 | Good |
| Expert | 120 | 120 | 120 | 33-38 | 52-62 | Strong |
| Master | 120 | 120 | 120 | 33-38 | 57-67 | Maximum |

### Curiosities & Easter Eggs
- **High Base Damage:** One of strongest single-target spells
- **Fire Damage:** Benefits from fire slayer spellbooks
- **Reflectable:** Can be reflected with Magic Reflection
- **Soul Shard:** Tracks successful casts for scrolls
- **Visual Effect:** Distinctive fire particles at feet (0x3709)
- **Sound:** Fire impact sound (0x208)
- **Delayed Damage:** Uses delayed damage system

---

## 4. Gate Travel (`Vas Rel Por`)

### Description
Creates a two-way portal between the caster's current location and a target location (marked rune, runebook, or house deed). Gates last 30 seconds and can be used by anyone. Both gates must be in teleport-allowed regions. Includes chaos momentum for dispel resistance. Champion gates have special properties.

### Meaning
The spell words "Vas Rel Por" translate to "Great Portal Stone" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Black Pearl, Mandrake Root, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Recall Rune, Runebook, or House Raffle Deed
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Gate Duration:** 30 seconds

### Gate Mechanics
- **Two-Way Portal:** Creates gate at caster location and destination
- **Destination Gate:** Only created if caster location allows teleport
- **Gate Duration:** 30 seconds (both gates)
- **Stacking Prevention:** SE mode prevents multiple gates at same location
- **Champion Gates:** Special gates for champion spawn participants
- **Dispellable:** Can be removed with Dispel Field
- **Confirmation Gump:** Simple yes/no confirmation required before travel (prevents accidental travel)
- **Gate Color System:** Visual safety indicator based on destination
  - **Blue Gate (Hue 0):** Leads to safe/guarded area (towns, protected regions)
  - **Red Gate (Hue 0x0021):** Leads to unsafe/unguarded area
  - Each gate shows the safety of where it leads TO
  - Example: Gate from safe city to dungeon = Red gate (at city), Blue gate (at dungeon)

### Travel Restrictions
- Cannot gate from blocked regions (prisons, no-recall zones)
- Cannot gate to blocked regions
- Cannot open gates inside houses (both origin and destination)
- Destination must allow teleport
- Cannot stack gates (SE mode)
- Destination must be spawnable location

### Duration
**Gate Lifetime:** 30 seconds per gate

### Simulation Scenarios

| Scenario | Magery | Success Rate | Gate Duration (s) | Notes |
|----------|--------|--------------|-------------------|-------|
| Novice | 100 | 100%* | 30 | *If valid destination |
| Apprentice | 110 | 100%* | 30 | *If valid destination |
| Adept | 120 | 100%* | 30 | *If valid destination |
| Expert | 120 | 100%* | 30 | *If valid destination |
| Master | 120 | 100%* | 30 | *If valid destination |

*Success depends on region permissions, not skill level*

### Scrying Gate Chance Scenarios

| Scenario | Tracking | Base Chance | Skill Bonus | Total Chance | Notes |
|----------|----------|-------------|-------------|--------------|-------|
| Novice | 80.0 | 15.0% | 0.0% | 15.0% | Minimum requirement |
| Apprentice | 90.0 | 15.0% | 5.0% | 20.0% | +10 skill points |
| Adept | 100.0 | 15.0% | 10.0% | 25.0% | +20 skill points |
| Expert | 110.0 | 15.0% | 15.0% | 30.0% | +30 skill points |
| Master | 120.0+ | 15.0% | 20.0% | 35.0% | Maximum chance |

### Origin Gate Chance Scenarios

| Scenario | Cartography | Base Chance | Skill Bonus | Total Chance | Notes |
|----------|------------|-------------|-------------|--------------|-------|
| Novice | 80.0 | 5.0% | 0.0% | 5.0% | Minimum requirement |
| Apprentice | 90.0 | 5.0% | 5.0% | 10.0% | +10 skill points |
| Adept | 100.0 | 5.0% | 10.0% | 15.0% | +20 skill points |
| Expert | 110.0 | 5.0% | 15.0% | 20.0% | +30 skill points |
| Master | 120.0+ | 5.0% | 20.0% | 25.0% | Maximum chance (capped at 30%) |

### Safety Detection
Gates automatically detect safe regions:
- **Safe Regions:** SafeRegion, ProtectedRegion, PublicRegion, GuardedRegion, BardTownRegion, VillageRegion, TownRegion
- **Unsafe Regions:** All other regions (dungeons, wilderness, etc.)
- **Color Logic:** Each gate's color reflects the safety of its destination
  - Gate at origin shows destination safety
  - Gate at destination shows origin safety

### Special Features

#### Scrying Gate (Tracking Skill)
- **Skill Requirement:** Tracking 80.0+ to unlock
- **Chance System:**
  - Base: 15% chance at 80.0 Tracking
  - Increase: +5% per 10 skill points above 80.0
  - Maximum: 35% chance at 120+ Tracking
- **Effect:** When activated, provides destination intelligence before travel:
  - Shows destination region name and safety status
  - Detects nearby hostile creatures (within 10 tiles)
  - Displays safety status: "Protegido" (Protected) or "Não Protegido" (Unprotected)
  - Color-coded messages:
    - Destination info: Blue (safe) or Red (unsafe)
    - Creature detection: Purple
    - Clear area: White
- **Visual Effects:** Special scrying effect (0x373A) with purple hue and sound (0x1F9)
- **Messages:** All messages in Portuguese (pt-br)

#### Origin Gate (Cartography Skill)
- **Skill Requirement:** Cartography 80.0+ to unlock
- **Chance System:**
  - Base: 5% chance
  - Increase: +0.5% per skill point above 80.0
  - Maximum: 25% at 120 skill (capped at 30% total)
- **Effect:** When triggered, creates a "Gate Marker" item:
  - **Marker Properties:**
    - Item ID: 0x1F14 (small rune appearance)
    - Hue: Light green (0x0059)
    - Name: "Marcador de Portal"
    - Duration: 1 hour or until manually deleted
    - Location: Created in caster's backpack (falls to ground if backpack full)
    - Movable: Yes (can be picked up, moved, traded)
    - Cannot be added to Runebooks
    - One-time use: Deleted after activation
  - **Marker Activation:**
    - Double-click to create one-way gate back to origin location
    - Cannot be activated inside houses (marker not consumed if blocked)
    - Gate color follows safety pattern (blue for safe, red for unsafe)
    - Does NOT have Scrying Gate benefits
    - Extensive validation ensures destination is travel-allowed
  - **Validation:** Origin location must allow GateTo, Teleport, and Recall
- **Creative Message:** Caster receives Portuguese message about cartography mastery

### Curiosities & Easter Eggs
- **Runebook Support:** Can target runebooks for default entry
- **House Deed Support:** Can gate to house plot locations
- **Champion Gates:** Special gates for Evil/Good champions
- **Safety Color System:** Blue for safe destinations, Red for unsafe (see Safety Detection above)
- **Confirmation Gump:** Simple dialog with moongate icon prevents accidental travel
- **Two-Way:** Both locations get gates (if allowed)
- **Sound Effect:** Distinctive gate creation sound (0x20E)
- **Auto-Delete:** Gates deserialize and delete (safety feature)
- **Scrying Gate:** Tracking skill provides destination intelligence (see Special Features above)
- **Origin Gate:** Cartography skill creates return markers (see Special Features above)
- **Marker Storage:** Origin Gate markers created in backpack for easy access
- **Zoom Prevention:** Origin Gate uses InternalItem (custom Moongate) to prevent camera zoom issues
- **House Restrictions:** Gates cannot be opened inside houses (both regular gates and Origin Gate markers)
- **Runebook Restriction:** Origin Gate markers cannot be added to Runebooks

---

## 5. Mana Vampire (`Ort Sanct`)

### Description
Drains mana from target and transfers it to the caster. Very high resistance (98%) makes it difficult to resist. Removes paralysis from target. Interrupts target's spellcasting. Mana transfer is limited by caster's available mana capacity.

### Meaning
The spell words "Ort Sanct" translate to "Energy Sacred" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Black Pearl, Bloodmoss, Mandrake Root, Spider's Silk
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Resistance:** Dynamic check (Pareto principle: 80/20 balance)

### Mana Drain Formula
```
Mana Drain = (Magery × 0.16) × EvalInt Benefit (reduced by 20% for balance)
If Resisted: × 0.5 (halved drain on resistance)
Capped at: Target's current mana
Capped at: (Caster ManaMax - Caster Mana) - 1

Transfer: Caster receives mana with level modifier
```

### Special Mechanics
- **Paralysis Removal:** Removes paralysis from target
- **Spell Interruption:** Interrupts target's current spell
- **Reflectable:** Can be reflected with Magic Reflection
- **Resistance:** Uses standard dynamic resistance check (Pareto principle: 80/20 balance)
- **Resist Penalty:** If target resists, mana drain is halved (50% reduction)
- **Mana Cap:** Cannot exceed caster's mana capacity

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Target Mana | Drain Amount | Resisted | Notes |
|----------|--------|---------|-------------|--------------|--------|-------|
| Novice | 100 | 90 | 100 | 14-18 | 7-9 | Basic |
| Apprentice | 110 | 100 | 100 | 18-22 | 9-11 | Improved |
| Adept | 120 | 110 | 100 | 21-26 | 11-13 | Good |
| Expert | 120 | 120 | 100 | 23-28 | 12-14 | Strong |
| Master | 120 | 120 | 200 | 23-28 | 12-14 | Capped by target |

### Curiosities & Easter Eggs
- **Dynamic Resistance:** Uses standard resistance check (Pareto principle balance)
- **Resist Penalty:** Halved drain on resistance (50% reduction)
- **Paralysis Cure:** Side effect removes paralysis
- **Spell Disruption:** Interrupts enemy spellcasting
- **Visual Effects:** Different particles for caster and target
- **Sound:** Distinctive mana drain sound (0x1F9)
- **Level Modifier:** Transferred mana modified by player level
- **Mage Killer:** Essential for PvP mage duels

---

## 6. Mass Dispel (`Vas An Ort`)

### Description
Area-effect dispel that removes multiple summoned creatures and field spells in range. Uses chaos momentum system for dynamic dispel chances. Low-health creatures have better dispel chances. Failed dispels anger creatures instead of removing them. Fields are always dispelled instantly.

### Meaning
The spell words "Vas An Ort" translate to "Great Dispel Summon" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Garlic, Mandrake Root, Black Pearl, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Mobile Range:** 8 tiles radius
- **Field Range:** 4 tiles radius

### Dispel Mechanics
```
Chaos Momentum = Random(0-10)
Dispel Chance = NMSUtils.getDispelChance(caster, target, chaos)
Success Threshold = NMSUtils.getSummonDispelPercentage(target, chaos)

Low Health Bonus:
If Health < 20%: Additional chance = Health / 10

Fields: Always dispelled instantly (100% success)
```

### Special Cases
- **Wizard Creatures (ControlSlots == 666):** Always fail
- **Low Health:** Better chance if below 20% health
- **Success:** Creature disappears with effects
- **Failure:** Creature becomes angry (particle effect)
- **Fields:** Always dispelled (no chance system)

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Targets | Fields | Chaos | Success Rate | Notes |
|----------|--------|---------|--------|-------|--------------|-------|
| Novice | 100 | 3-5 | 2-4 | 5 | 50-60% | Basic |
| Apprentice | 110 | 4-7 | 3-5 | 5 | 60-70% | Improved |
| Adept | 120 | 5-10 | 4-6 | 8 | 70-80% | Good |
| Expert | 120 | 6-12 | 5-8 | 8 | 75-85% | Strong |
| Master | 120 | 8-15 | 6-10 | 10 | 80-90% | Maximum |

### Curiosities & Easter Eggs
- **Chaos Momentum:** Random 0-10 factor affects every cast
- **Low Health Advantage:** Wounded summons easier to dispel
- **Wizard Protection:** Special creatures with ControlSlots=666 immune
- **Field Priority:** Fields always dispelled first
- **Anger Mechanic:** Failed dispels make creatures hostile
- **Visual Feedback:** Different effects for success/failure/anger
- **Area Effect:** Can clear entire battlefields

---

## 7. Meteor Swarm (`Flam Kal Des Ylem`)

### Description
Area-of-effect fire damage spell affecting multiple targets in 5-tile radius. Damage is divided among all targets, with a minimum damage floor. Creates spectacular meteor impact effects at each target location. Can be resisted. Reveals hidden targets (except One Ring protection).

### Meaning
The spell words "Flam Kal Des Ylem" translate to "Flame Summon Great Matter" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Mandrake Root, Sulfurous Ash, Spider's Silk
- **Mana Cost:** 40
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **AoE Range:** 5 tiles radius

### Damage Formula
```
Base Damage = GetNMSDamage(38, 2, 5, target)
= 38 + 2d5 = 40-48 base damage
+ Magery/EvalInt scaling
+ Inscription bonus

If Multiple Targets: Damage / Target Count
Minimum Damage Floor: 12 (even after division)
If Resisted: × 0.5
```

### Special Mechanics
- **Damage Splitting:** Total damage divided by number of targets
- **Minimum Floor:** Each target receives at least 12 damage
- **Always Reveals:** Hidden targets are always revealed (except One Ring protection)
- **No House Protection:** Damage applies everywhere, regardless of region
- **Caster Damage:** Caster can also receive damage (respects all rules)
- **One Ring:** Reduces damage by 50% and prevents reveal
- **Resistance:** Can reduce damage by 50%
- **Visual Effects:** 5 impact locations per target (center + 4 directions)

### Duration
N/A (instant damage)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Targets | Base Damage | Per Target | Resisted | Final Range | Notes |
|----------|--------|---------|---------|-------------|------------|----------|-------------|-------|
| Novice | 100 | 90 | 1 | 40-48 | 40-48 | 50% | 40-48 | Single target |
| Apprentice | 110 | 100 | 3 | 45-53 | 15-18 | 50% | 15-18 | Split damage |
| Adept | 120 | 110 | 5 | 50-58 | 12-12 | 50% | 12 | Floor applied |
| Expert | 120 | 120 | 2 | 55-63 | 28-32 | 50% | 28-32 | Good split |
| Master | 120 | 120 | 1 | 60-68 | 60-68 | 50% | 60-68 | Maximum single |

### Curiosities & Easter Eggs
- **Spectacular Effects:** 5 impact locations per target create impressive visuals
- **One Ring Protection:** Reduces damage by 50% and prevents reveal (Lord of the Rings reference)
- **Always Reveals:** No house protection - hidden targets always revealed (except One Ring)
- **Damage Floor:** Ensures minimum 12 damage even with many targets
- **Random Particles:** Uses random selection between two particle types
- **Sound Effects:** Distinctive cast sound (0x160) and impact sound (0x15F)
- **Delayed Damage:** Uses delayed damage system
- **Fire Damage:** Benefits from fire slayer spellbooks

---

## 8. Polymorph (`Vas Ylem Rel`)

### Description
Transforms the caster into a selected creature form. Duration is 50% of Magery skill in seconds (max 120 seconds), with Animal Lore bonus for animal forms. Always expires after duration. Cannot be used while transformed, disguised, or wearing body paint. Human forms get random skin color. Non-human forms dismount the caster.

### Meaning
The spell words "Vas Ylem Rel" translate to "Great Matter Change" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Spider's Silk, Mandrake Root
- **Mana Cost:** 40
- **Target:** Self (with body selection gump)
- **Duration:** 50% of Magery skill in seconds (max 120s) + Animal Lore bonus for animal forms

### Duration Formula
```
Base Duration = (Magery skill × 0.5) seconds (50% reduction)
Animal Lore Bonus = +1 second per 10 skill points (only for animal forms)
Total Duration = Base + Animal Lore Bonus
Capped at: 120 seconds maximum

Always expires after duration (timer always created)
```

### Transformation Restrictions
- Cannot polymorph while under transformation spell
- Cannot polymorph while disguised
- Cannot polymorph while wearing body paint (183 or 184)
- Cannot polymorph if already polymorphed
- Human forms: Get random skin color
- Animal forms: Receive Animal Lore duration bonus (1s per 10 skill points)
- Non-human forms: Automatically dismount

### Duration
**Base:** 1-60 seconds (50% of Magery skill)  
**With Animal Lore (Animal Forms):** +1-12 seconds bonus  
**Maximum:** 120 seconds (always capped)

### Simulation Scenarios

| Scenario | Magery | Animal Lore | Base Duration | Animal Bonus | Total Duration | Notes |
|----------|--------|-------------|---------------|--------------|----------------|-------|
| Novice | 100 | 80 | 50 | +8 | 58 | Animal form |
| Apprentice | 110 | 100 | 55 | +10 | 65 | Animal form |
| Adept | 120 | 120 | 60 | +12 | 72 | Animal form |
| Expert | 120 | 0 | 60 | 0 | 60 | Monster/Human form |
| Master | 120 | 120 | 60 | +12 | 72 | Animal form (capped) |

### Available Forms
- **Skill-Based Filtering:** Forms are cumulative based on Magery skill (0-40, 40-50, 50-60, 60-70, 70-80, 80-90, 90-100, 100-110, 110-120)
- **Total Forms:** 26 forms available (Cerberus removed from menu)
- **Form Categories:** Animals and Monsters
- **Visual Tiers:** Color-coded by skill requirement (Beginner to Mythic)

### Curiosities & Easter Eggs
- **Body Selection:** Gump interface for choosing form (skill-based cumulative filtering)
- **Human Forms:** Random skin color applied (400/401)
- **Animal Lore Bonus:** +1 second per 10 skill points (animal forms only)
- **Duration Reduction:** Base duration reduced by 50% for balance
- **Always Expires:** Timer always created, spell always expires
- **Transformation Messages:** Start message (with duration) and end message in pt-br
- **Dismount:** Non-human forms automatically dismount
- **Thread-Safe:** Uses Dictionary<Mobile, Timer> for storage
- **Armor Validation:** Automatically validates armor/clothing fit
- **Gump System:** SE uses NewPolymorphGump, Legacy uses PolymorphGump
- **Cerberus Removed:** Cerberus form has been removed from the available forms list

---

## Common Patterns

### Area-of-Effect Damage Spells
The 7th circle features two premier AoE damage dealers:
- **Meteor Swarm:** Fire damage with spectacular visuals
- **Chain Lightning:** Lightning damage with special mechanics

### Single-Target Damage Spells
High-damage direct attacks:
- **Flame Strike:** Strongest single-target fire spell
- **Mana Vampire:** Unique mana drain mechanic

### Utility Spells
Advanced utility for various situations:
- **Gate Travel:** Two-way portal creation
- **Polymorph:** Self-transformation
- **Mass Dispel:** Area creature/field removal

### Area Control
Powerful movement and field manipulation:
- **Energy Field:** 5-tile blocking field
- **Mass Dispel:** Area field removal

---

## Strategic Applications

### PvP Strategies
1. **Mana Vampire + Flame Strike:** Drain enemy mana then finish with high damage
2. **Meteor Swarm/Chain Lightning:** Area damage for group fights
3. **Energy Field:** Block escape routes or entry points
4. **Gate Travel:** Quick escape or group transport
   - **Scrying Gate (Tracking):** Check destination safety before travel
   - **Origin Gate (Cartography):** Create return markers for strategic positioning
5. **Mass Dispel:** Remove enemy summons and fields
6. **Polymorph:** Disguise for stealth or roleplay

### PvM Strategies
1. **Meteor Swarm:** Area damage for large groups
2. **Chain Lightning:** Multi-target lightning damage
3. **Flame Strike:** High single-target DPS
4. **Mass Dispel:** Remove enemy summons quickly
5. **Energy Field:** Control enemy movement
6. **Gate Travel:** Quick travel between locations
   - **Scrying Gate (Tracking):** Scout dangerous areas before entering
   - **Origin Gate (Cartography):** Create safe return points from dungeons

### Group Play
1. **Gate Travel:** Transport entire groups
   - **Scrying Gate (Tracking):** Team leader scouts destination safety
   - **Origin Gate (Cartography):** Create group return markers for coordinated retreats
2. **Meteor Swarm/Chain Lightning:** Area damage for team
3. **Mass Dispel:** Clear enemy fields and summons
4. **Energy Field:** Area denial for team
5. **Mana Vampire:** Support by draining enemy casters

---

## Summary

The 7th Circle represents mastery-level magical power:
- **Damage:** Highest damage spells (Meteor Swarm, Chain Lightning, Flame Strike)
- **Control:** Powerful area denial (Energy Field, Mass Dispel)
- **Utility:** Essential tools (Gate Travel, Polymorph, Mana Vampire)
- **Complexity:** Advanced mechanics (chaos momentum, damage splitting, field orientation)
- **Strategy:** Requires tactical thinking and skill investment

These spells define master-level gameplay and separate experts from novices. Proper use of 7th circle magic combined with skill development creates truly formidable mages capable of dominating both PvP and PvM encounters.

