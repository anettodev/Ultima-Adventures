# Magery 3rd Circle Spells - Complete Guide

## Overview

The 3rd Circle of Magery introduces more powerful offensive spells, advanced utility magic, and area control. These spells require higher skill (50.0 Magery) and provide significant power increases over lower circles.

**Circle Level:** Advanced  
**Mana Cost:** 24  
**Difficulty:** 50-70 skill  
**Total Spells:** 8

---

## Spell List

1. **Bless (`Rel Sanct`)** - Buff/Stat Increase (All Stats)
2. **Fireball (`Vas Flam`)** - Attack/Damage
3. **Magic Lock (`An Por`)** - Utility/Security
4. **Poison (`In Nox`)** - Attack/Debuff
5. **Telekinesis (`Ort Por Ylem`)** - Utility
6. **Teleport (`Rel Por`)** - Movement
7. **Unlock (`Ex Por`)** - Utility
8. **Wall of Stone (`In Sanct Ylem`)** - Field/Control

---

## 1. Bless (`Rel Sanct`)

### Description
Increases all three stats (Strength, Dexterity, Intelligence) simultaneously. The most powerful stat buff in the lower circles, providing comprehensive enhancement.

### Meaning
The spell words "Rel Sanct" translate to "Sacred Blessing" in the magical language.

### Requirements
- **Minimum Magery:** 50.0
- **Reagents:** Garlic, Mandrake Root
- **Mana Cost:** 24
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Increase Formula
```
Stat Increase = Target's RawStat × GetOffsetScalar(Caster, Target, false)
GetOffsetScalar = (1 + (Inscribe / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)

Sorcerer Bonus Multiplier = 1 + ((Magery + EvalInt) / 180)
Final Percentage = Base Percentage × Sorcerer Multiplier (if Sorcerer)
```

**All Stats Affected:** Strength, Dexterity, Intelligence

### Duration Formula
```
Base Duration = NMSGetDuration (beneficial spell duration)
Sorcerer Bonus = +10 seconds (if Sorcerer)
Final Duration = Base + Sorcerer Bonus
```

### Simulation Scenarios

| Scenario | Magery | Inscribe | Target Stats | Increase % | Duration (sec) | Notes |
|----------|--------|----------|--------------|------------|----------------|-------|
| Novice | 60 | 50 | 50/50/50 | ~5% each | 20-35 | Basic |
| Apprentice | 80 | 70 | 70/70/70 | ~6% each | 25-45 | Improved |
| Adept | 100 | 90 | 90/90/90 | ~7% each | 30-55 | Good |
| Expert | 120 | 100 | 100/100/100 | ~8% each | 35-65 | Strong |
| Master (Sorcerer) | 120 | 120 | 120/120/120 | ~12% each | 45-75 | Maximum |

### Curiosities & Easter Eggs
- **Curse Removal:** 50% chance to remove existing curses when cast
- **Sorcerer Bonus:** Sorcerers get +10 seconds duration and higher percentage
- **All Stats:** Only spell that buffs all three stats simultaneously
- **Creature Blessing:** Can bless creatures (with special handling)
- **PvP Essential:** Critical buff before engaging in combat

---

## 2. Fireball (`Vas Flam`)

### Description
Hurls an explosive ball of fire at the target, dealing significant fire damage. The most popular mid-level damage spell.

### Meaning
The spell words "Vas Flam" translate to "Great Fire" in the magical language.

### Requirements
- **Minimum Magery:** 50.0
- **Reagents:** Black Pearl
- **Mana Cost:** 24
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Damage Formula
```
Base Damage = Dice(1, 6) + 4 = 5-10 base
Final Damage = Base × EvalInt Benefit
Minimum Damage Floor (EvalInt-based):
  - 120+ EvalInt: 9 minimum
  - 100-119 EvalInt: 8 minimum
  - 80-99 EvalInt: 7 minimum
  - Below 80: 5 minimum
Final = Max(calculated, minimum floor)
```

### Duration
N/A (instant damage, delayed projectile)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Base Damage | Final Damage | Min Floor | Notes |
|----------|--------|---------|-------------|--------------|-----------|-------|
| Novice | 60 | 50 | 5-10 | 5-11 | 5 | Low |
| Apprentice | 80 | 70 | 5-10 | 6-13 | 7 | Moderate |
| Adept | 100 | 90 | 5-10 | 7-15 | 8 | Good |
| Expert | 120 | 100 | 5-10 | 8-17 | 8 | Strong |
| Master | 120 | 120 | 5-10 | 9-20 | 9 | Maximum |

### Curiosities & Easter Eggs
- **100% Fire Damage:** All damage is fire type
- **Minimum Damage Floor:** High EvalInt guarantees minimum damage
- **Delayed Damage:** Projectile travels to target (can be dodged)
- **Reflection:** Can be reflected back at caster
- **Popular Spell:** Most commonly used mid-level damage spell

---

## 3. Magic Lock (`An Por`)

### Description
Locks containers and doors with magical energy. Also enables soul trapping of creatures (advanced feature requiring 100+ Magery/EvalInt).

### Meaning
The spell words "An Por" translate to "Lock" in the magical language.

### Requirements
- **Minimum Magery:** 50.0 (100+ for soul trapping)
- **Reagents:** Bloodmoss, Garlic, Sulfurous Ash
- **Mana Cost:** 24
- **Target:** Container, Door, or Creature (for soul trapping)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Lock Strength Formula
```
Container Lock Level = Magery (capped at 75, minimum 0)
Door Lock Duration = 10 + (Magery × (90-10) / 120) seconds
Door Lock Range: 10-90 seconds
```

### Soul Trapping Formula
```
Requirements: Magery 100+, EvalInt 100+, Intelligence 100+
Success Rate = 5% + ((Average(Magery, EvalInt) - 100) × 0.5%)
Maximum Success Rate: 15% (at 120/120 skills)
```

### Duration
- **Container Lock:** Permanent until removed
- **Door Lock:** 10-90 seconds (based on Magery)
- **Soul Trap:** Permanent (creature becomes flask item)

### Simulation Scenarios

#### Container Locking
| Scenario | Magery | Lock Level | Notes |
|----------|--------|------------|-------|
| Novice | 60 | 60 | Basic |
| Apprentice | 80 | 75 (capped) | Maximum |
| Adept | 100 | 75 (capped) | Maximum |
| Expert | 120 | 75 (capped) | Maximum |
| Master | 120 | 75 (capped) | Maximum |

#### Door Locking
| Scenario | Magery | Duration (sec) | Notes |
|----------|--------|----------------|-------|
| Novice | 60 | ~50 | Moderate |
| Apprentice | 80 | ~63 | Good |
| Adept | 100 | ~77 | Strong |
| Expert | 120 | ~90 | Maximum |
| Master | 120 | ~90 | Maximum |

#### Soul Trapping
| Scenario | Magery | EvalInt | Success % | Notes |
|----------|--------|---------|-----------|-------|
| Expert | 100 | 100 | 5% | Minimum |
| Expert | 110 | 110 | 10% | Improved |
| Master | 120 | 120 | 15% | Maximum |

### Curiosities & Easter Eggs
- **Soul Trapping:** Advanced feature to capture creature souls in flasks
- **Human Soul = Murder:** Capturing humanoid souls counts as murder (karma penalty)
- **Epic Creatures Fight Back:** Epic creatures can kill the caster if attempted
- **Door Duration:** Linear scaling from 10-90 seconds based on Magery
- **Container Cap:** Lock level capped at 75 regardless of skill

---

## 4. Poison (`In Nox`)

### Description
Applies poison to the target, dealing damage over time. Poison level depends on caster's Magery and Poisoning skills.

### Meaning
The spell words "In Nox" translate to "Poison" in the magical language.

### Requirements
- **Minimum Magery:** 50.0 (higher for better poison levels)
- **Reagents:** Nightshade
- **Mana Cost:** 24
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Poison Level Formula
```
Level 4 (Lethal): Magery + Poisoning ≥ 240
Level 3 (Deadly): Magery ≥ 120 AND Poisoning ≥ 100
Level 2 (Greater): Magery ≥ 100 AND (Poisoning ≥ 80 OR EvalInt ≥ 100)
Level 1 (Regular): Magery ≥ 80 AND (Poisoning ≥ 60 OR EvalInt ≥ 80)
Level 0 (Lesser): Below Level 1 requirements
```

### Duration
Poison duration is handled by the poison system (not spell duration).

### Simulation Scenarios

| Scenario | Magery | Poisoning | EvalInt | Poison Level | Notes |
|----------|--------|-----------|---------|--------------|-------|
| Novice | 60 | 0 | 60 | Level 0 | Lesser |
| Apprentice | 80 | 50 | 80 | Level 1 | Regular |
| Adept | 100 | 70 | 90 | Level 2 | Greater |
| Expert | 120 | 100 | 100 | Level 3 | Deadly |
| Master | 120 | 120 | 120 | Level 4 | Lethal |

### Curiosities & Easter Eggs
- **Skill Requirements:** Higher poison levels require specific skill combinations
- **EvalInt Alternative:** Can use EvalInt instead of Poisoning for Level 1-2
- **Lethal Poison:** Level 4 poison cannot be cured by Cure or Arch Cure
- **High-Level Sound:** Level 3-4 poison plays audible sound to nearby players
- **Resistance Check:** Target can resist the poison application

---

## 5. Telekinesis (`Ort Por Ylem`)

### Description
Allows manipulation of objects and containers at a distance. Can open containers, grab items, and activate switches remotely. **Easter Egg:** Advanced mages can attempt to telekinetically grab nearby mobiles and players!

### Meaning
The spell words "Ort Por Ylem" translate to "Move Object" in the magical language.

### Requirements
- **Minimum Magery:** 50.0
- **Reagents:** Bloodmoss, Mandrake Root
- **Mana Cost:** 24 (normal) / 48 (mobile grab attempt)
- **Target:** Item, Container, ITelekinesisable object, **or Mobile**
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Weight Limit Formula (Items)
```
Maximum Weight = Intelligence / 20
Example: 100 Intelligence = 5 stones maximum
```

### Mobile Grab (Easter Egg Feature)

**Activation:** Target a mobile/player within 1 tile range

#### Mobile Grab Requirements
- Target must be **alive**
- Target must **not be hidden**
- Target must **not be blessed**
- Target must be **AccessLevel.Player**
- Must respect **safe zones**
- **Cooldown:** 10 seconds between attempts
- **Mana Cost:** 2× normal cost (48 mana total)
- **Cannot target self**

#### Mobile Grab Success Formula
```
Base Chance = Magery × 0.3% per point
Inscription Bonus = Inscription × 0.15% per point
Total Chance = Base + Bonus (capped at 100%)

Example at GM (100/100):
Base: 100 × 0.3% = 30%
Bonus: 100 × 0.15% = 15%
Total: 45% chance
```

#### Mobile Grab Effects
- **Success:** Target teleported 1 tile away from caster
- **Success (Caster):** "Você puxou {name} com telecinesia!"
- **Success (Target):** "Você foi puxado por {name} com telecinesia!"
- **Failure (Caster):** "Você não conseguiu puxar {name}."
- **Failure (Target - Player):** "Você sentiu uma leve tontura. Como se algo estivesse lhe puxando."
- **Cooldown:** "Você ainda está se recuperando do último uso de telecinesia avançada."
- **Self-Target:** "Você não pode usar telecinesia em si mesmo."

### Duration
N/A (instant effect)

### Simulation Scenarios

#### Item Manipulation

| Scenario | Magery | Intelligence | Max Weight | Notes |
|----------|--------|--------------|------------|-------|
| Novice | 60 | 50 | 2.5 stones | Limited |
| Apprentice | 80 | 70 | 3.5 stones | Moderate |
| Adept | 100 | 90 | 4.5 stones | Good |
| Expert | 120 | 100 | 5.0 stones | Strong |
| Master | 120 | 120 | 6.0 stones | Maximum |

#### Mobile Grab Success Rates

| Scenario | Magery | Inscription | Success Chance | Mana Cost | Cooldown |
|----------|--------|-------------|----------------|-----------|----------|
| Novice | 50 | 0 | 15% | 48 | 10s |
| Apprentice | 75 | 50 | 30% | 48 | 10s |
| Adept | 100 | 80 | 42% | 48 | 10s |
| Expert | 120 | 100 | 51% | 48 | 10s |
| Master | 120 | 120 | 54% | 48 | 10s |

### Curiosities & Easter Eggs
- **Trap Avoidance:** Can open trapped containers from safe distance
- **Item Grabbing:** Can grab movable items (weight limit applies)
- **Switch Activation:** Can activate levers and switches remotely
- **Container Access:** Opens containers without being next to them
- **Weight Limit:** Intelligence-based weight restriction
- **Mobile Grab:** Hidden feature to pull mobiles/players (1 tile away)
- **Double Mana Cost:** Mobile grab costs 2× mana even on failure
- **Cooldown System:** 10-second cooldown prevents spam
- **Target Awareness:** Failed attempts notify the target (subtle "tontura")
- **Inscription Synergy:** Inscription skill boosts mobile grab chance
- **Safe Zone Respect:** Cannot grab in protected areas
- **Validation Messages:** Clear PT-BR feedback for all scenarios

---

## 6. Teleport (`Rel Por`)

### Description
Instantly transports the caster to a targeted location. Essential for fast travel and escape.

### Meaning
The spell words "Rel Por" translate to "Move Location" in the magical language.

### Requirements
- **Minimum Magery:** 50.0
- **Reagents:** Bloodmoss, Mandrake Root
- **Mana Cost:** 24
- **Target:** Ground location
- **Range:** 11 tiles (ML) / 12 tiles (Legacy)
- **Weight Check:** Cannot teleport if overloaded

### Travel Restrictions
- **From Location:** Must pass `TravelCheckType.TeleportFrom`
- **To Location:** Must pass `TravelCheckType.TeleportTo`
- **Cursed Creatures:** Cannot teleport if cursed creatures nearby (high karma only)
- **Multi Check:** Cannot teleport to blocked locations

### Pet Teleport
- **Chance:** 50% chance pets teleport with caster
- **Range:** Same as caster

### Duration
N/A (instant teleportation)

### Simulation Scenarios

| Scenario | Magery | Success Rate | Pet Teleport % | Notes |
|----------|--------|--------------|----------------|-------|
| Novice | 60 | High | 50% | Basic |
| Apprentice | 80 | High | 50% | Reliable |
| Adept | 100 | High | 50% | Good |
| Expert | 120 | High | 50% | Strong |
| Master | 120 | High | 50% | Maximum |

*Success rate depends on travel restrictions, not skill*

### Curiosities & Easter Eggs
- **Pet Teleport:** 50% chance pets follow (random)
- **Cursed Creature Block:** High karma players blocked by cursed creatures nearby
- **Field Trigger:** Teleporting into fields triggers their effects
- **Escape Tool:** Essential for escaping dangerous situations
- **Visual Effects:** Different effects for players vs NPCs

---

## 7. Unlock (`Ex Por`)

### Description
Unlocks doors and containers using magical power. Success rate depends on Magery, Inscription, and Lockpicking skills.

### Meaning
The spell words "Ex Por" translate to "Unlock" in the magical language.

### Requirements
- **Minimum Magery:** 50.0
- **Reagents:** Bloodmoss, Sulfurous Ash
- **Mana Cost:** 24
- **Target:** Door or Container
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Success Formula
```
Base Chance = (Magery / 100) × 28.5%
Inscription Multiplier = 1 + (Inscription / 200)
Magic Power = Base Chance × Inscription Multiplier
Lockpicking Bonus = (Lockpicking - 9) × 0.14% (if Lockpicking ≥ 10)
Success Rate = Min(Magic Power + Lockpicking Bonus, 80%)
```

**Special Rules:**
- **Treasure Chest Level 1:** Maximum 50% success
- **Treasure Chest Level 2:** Maximum 30% success
- **Treasure Chest Level 3+:** Cannot unlock
- **Protected Containers:** ParagonChest, PirateChest cannot be unlocked

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Inscribe | Lockpicking | Success % | Notes |
|----------|--------|----------|-------------|-----------|-------|
| Novice | 60 | 50 | 0 | ~20% | Low |
| Apprentice | 80 | 70 | 50 | ~28% | Moderate |
| Adept | 100 | 90 | 80 | ~38% | Good |
| Expert | 120 | 100 | 100 | ~48% | Strong |
| Master | 120 | 120 | 120 | ~58% | Maximum |

*Values assume standard containers (not treasure chests)*

### Curiosities & Easter Eggs
- **Lockpicking Bonus:** Having Lockpicking skill significantly improves success
- **80% Cap:** Maximum 80% success rate (balance decision)
- **Treasure Chest Limits:** Special containers have lower caps
- **Protected Containers:** Some containers are immune to magical unlocking
- **Door Unlocking:** Can unlock dungeon doors (different formula)

---

## 8. Wall of Stone (`In Sanct Ylem`)

### Description
Creates a temporary wall of stone that blocks movement. Creates a 5-tile barrier for area control.

### Meaning
The spell words "In Sanct Ylem" translate to "Create Sacred Wall" in the magical language.

### Requirements
- **Minimum Magery:** 50.0
- **Reagents:** Bloodmoss, Garlic
- **Mana Cost:** 24
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Wall Creation
- **Length:** 5 tiles
- **Orientation:** East-West or North-South (based on cast angle)
- **Segments:** 5 individual wall items created

### Duration Formula
```
Base Duration = 5 seconds
Inscribe Bonus = Inscribe / 4 (seconds)
Final Duration = Base + Inscribe Bonus
```

### Duration
5-35 seconds (based on Inscription skill)

### Simulation Scenarios

| Scenario | Magery | Inscribe | Duration (sec) | Notes |
|----------|--------|----------|---------------|-------|
| Novice | 60 | 50 | ~17 | Short |
| Apprentice | 80 | 70 | ~22 | Moderate |
| Adept | 100 | 90 | ~27 | Good |
| Expert | 120 | 100 | ~30 | Strong |
| Master | 120 | 120 | ~35 | Maximum |

### Curiosities & Easter Eggs
- **Movement Block:** Prevents passage (BlocksFit = true)
- **Light Source:** Wall provides light (LightType.Circle300)
- **Orientation:** Automatically determines best orientation
- **Dispellable:** Can be dispelled by Dispel Field spell
- **Area Control:** Essential for controlling enemy movement in PvP

---

## Common Patterns

### Stat Buff System
Bless uses the same stat buff formula as 2nd circle buffs:
- **All Stats:** Only spell that buffs all three stats
- **Sorcerer Bonus:** Sorcerers get enhanced duration and percentage
- **Curse Removal:** 50% chance to remove existing curses

### Damage Scaling
3rd circle damage spells scale better:
- **Fireball:** Minimum damage floor based on EvalInt
- **Consistent Progression:** 1d6+4 base damage (better than 2nd circle)

### Utility Spells
Advanced utility options:
- **Telekinesis:** Remote manipulation
- **Teleport:** Fast travel
- **Unlock:** Magical lockpicking
- **Magic Lock:** Security and soul trapping

---

## Strategic Applications

### Combat Mage
1. **Fireball:** Primary damage spell
2. **Poison:** Damage over time
3. **Bless:** Pre-combat buff
4. **Teleport:** Escape mechanism

### Utility Mage
1. **Telekinesis:** Remote container access
2. **Unlock:** Magical lockpicking
3. **Magic Lock:** Security
4. **Wall of Stone:** Area control

### Support Mage
1. **Bless:** Comprehensive stat buff
2. **Teleport:** Group mobility
3. **Wall of Stone:** Area control for party

---

## Summary

The 3rd Circle provides significant power increases:
- **Offense:** Fireball and Poison for damage
- **Support:** Bless for comprehensive buffing
- **Utility:** Telekinesis, Teleport, Unlock for convenience
- **Control:** Wall of Stone for area denial
- **Advanced:** Magic Lock with soul trapping capability

These spells represent a major step up in power and utility, making 3rd circle mages significantly more capable than lower circles.

