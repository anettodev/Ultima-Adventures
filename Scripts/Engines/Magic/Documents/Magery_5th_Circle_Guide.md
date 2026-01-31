# Magery 5th Circle Spells - Complete Guide

## Overview

The 5th Circle of Magery introduces advanced summoning, defensive mechanics, area control, and utility spells. These spells require high skill (80.0 Magery) and provide powerful capabilities for both PvP and PvM.

**Circle Level:** Master  
**Mana Cost:** 40  
**Difficulty:** 80-100 skill  
**Total Spells:** 8

---

## Spell List

1. **Blade Spirits (`In Jux Hur Ylem`)** - Summon/Wild Creature
2. **Summon Creature (`Kal Xen`)** - Summon/Controlled Creature
3. **Magic Reflection (`In Jux Sanct`)** - Defensive/Spell Reflection
4. **Mind Blast (`Por Corp Wis`)** - Direct Damage/INT-Based
5. **Paralyze (`An Ex Por`)** - Control/Movement Prevention
6. **Dispel Field (`An Grav`)** - Utility/Field Removal
7. **Poison Field (`In Nox Grav`)** - Area Control/Damage Over Time
8. **Incognito (`Kal In Ex`)** - Utility/Disguise

---

## 1. Blade Spirits (`In Jux Hur Ylem`)

### Description
Summons a wild, uncontrolled blade spirit at a chosen location. The spirit attacks the nearest target, including the caster. Criminal act - flags caster as gray.

### Meaning
The spell words "In Jux Hur Ylem" translate to "Create Blade Spirit" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Black Pearl, Mandrake Root, Nightshade
- **Mana Cost:** 40
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Cast Delay:** 3x (SE) / 5x (AOS) normal delay

### Behavior
- **Wild Summon:** Attacks nearest target (including caster)
- **Evil Karma:** Always -2500 karma
- **Uncontrolled:** Cannot be commanded
- **Criminal Act:** Caster flagged as criminal
- **Single Limit:** Only 1 spirit per caster

### Duration Formula
```
Duration = Random(10-30) + (EvalInt / 4) + Tier Bonus
```

### Simulation Scenarios

| Scenario | Magery | EvalInt | Duration (sec) | Notes |
|----------|--------|---------|----------------|-------|
| Novice | 80 | 70 | 37-67 | Basic |
| Apprentice | 90 | 80 | 48-77 | Improved |
| Adept | 100 | 90 | 53-85 | Good |
| Expert | 120 | 110 | 60-100 | Strong |
| Master | 120 | 120 | 60-100 | Maximum |

### Curiosities & Easter Eggs
- **Wild Behavior:** Attacks caster if closest target
- **Criminal Flag:** Automatic gray flag on cast
- **No Corpse:** Evaporates with magical effects on death
- **Tag:** Shows "(invocado selvagem)" in name
- **Cannot Tame:** IsWildSummon flag prevents taming

---

## 2. Summon Creature (`Kal Xen`)

### Description
Summons a random animal to fight for the caster at a chosen location. Creature inherits caster's karma alignment. Requires half or fewer followers than maximum.

### Meaning
The spell words "Kal Xen" translate to "Summon Creature" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Bloodmoss, Mandrake Root, Spider's Silk
- **Mana Cost:** 40
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Follower Limit:** Must have ≤ 50% of max followers

### Creature Selection
- **Magery 80-99:** Basic creatures (bears, wolves, etc.)
- **Magery 100+:** Advanced creatures (dragons, daemons, etc.)

### Duration Formula
```
Duration = (Magery.Fixed × 0.1) + (AnimalLore × 0.25) seconds
```

### Simulation Scenarios

| Scenario | Magery | AnimalLore | Duration (sec) | Notes |
|----------|--------|------------|----------------|-------|
| Novice | 80 | 0 | 80 | Basic |
| Apprentice | 90 | 50 | 90 | Improved |
| Adept | 100 | 100 | 125 | Good |
| Expert | 120 | 100 | 145 | Strong |
| Master | 120 | 120 | 150 | Maximum |

### Curiosities & Easter Eggs
- **Karma Inheritance:** Good casters summon good creatures
- **Targeted Summon:** Caster chooses spawn location
- **Single Limit:** Only 1 creature per caster
- **No Guard:** Creature doesn't guard by default
- **No Corpse:** Evaporates with magical effects on death
- **Tag:** Shows "(invocado)" in name

---

## 3. Magic Reflection (`In Jux Sanct`)

### Description
Creates a temporary shield that reflects the first hostile spell back to its caster. Shield power determined by Magery + Inscription. Cooldown after shield expires.

### Meaning
The spell words "In Jux Sanct" translate to "Create Reflection Shield" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Garlic, Mandrake Root, Spider's Silk
- **Mana Cost:** 40
- **Target:** Self
- **Cooldown:** 60 seconds after shield expires

### Shield Duration Formula
```
Duration = 15 + ((Magery + Inscription) / 240) × 75 seconds
Minimum: 15 seconds
Maximum: 90 seconds
```

### Shield Power Formula
```
Power = Magery + Inscription
Used for: Power comparison in dual-shield scenarios
```

### Reflection Mechanics
- **Single-Target Spells:** Reflected back to caster
- **Multi-Target Spells:** Arcane Interference (all shields dispel, no damage)
- **Dual Shields:** Power comparison determines winner
- **1-Second Delay:** Reflected spell bounces after 1 second
- **Target Validation:** Dead/invalid targets nullify reflection

### Simulation Scenarios

| Scenario | Magery | Inscription | Duration (sec) | Power | Notes |
|----------|--------|-------------|----------------|-------|-------|
| Novice | 80 | 60 | 29 | 140 | Basic |
| Apprentice | 90 | 80 | 38 | 170 | Improved |
| Adept | 100 | 100 | 50 | 200 | Good |
| Expert | 120 | 110 | 77 | 230 | Strong |
| Master | 120 | 120 | 90 | 240 | Maximum |

### Curiosities & Easter Eggs
- **Visual Feedback:**** Shield activation and dismissal effects
- **Sound Effects:** Distinct sounds for activation/expiration
- **Cooldown System:** 60-second cooldown prevents spam
- **Power Comparison:** Higher skill = stronger shield
- **Arcane Interference:** Multi-target spells cancel all shields

---

## 4. Mind Blast (`Por Corp Wis`)

### Description
Deals direct mental damage based on INT difference between caster and target. Can backfire if target has higher INT. Not affected by slayer spellbooks.

### Meaning
The spell words "Por Corp Wis" translate to "Strike Mind" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Black Pearl, Mandrake Root, Nightshade, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Damage Formula
```
Base Damage = (Caster INT - Target INT) / 2
Minimum: 3 damage
Maximum: 35 damage
Random Variance: -0 to -3
AOS Bonus: +0 to +4 (AOS mode only)

If Resisted: Damage / 2.0
```

### Duration
N/A (instant damage)

### Simulation Scenarios

| Scenario | Caster INT | Target INT | Base Damage | Final Range | Notes |
|----------|------------|------------|-------------|-------------|-------|
| Novice | 80 | 60 | 10 | 7-14 | Basic |
| Apprentice | 100 | 80 | 10 | 7-18 | Improved |
| Adept | 120 | 100 | 10 | 7-20 | Good |
| Expert | 120 | 80 | 20 | 17-24 | Strong |
| Master | 120 | 60 | 30 | 27-33 | Maximum |

*Backfire: If target INT > caster INT, damage reverses*

### Curiosities & Easter Eggs
- **INT-Based:** Unique damage calculation
- **Backfire Risk:** Higher INT targets can reverse damage
- **No Slayer Bonus:** Not affected by spellbook slayers
- **Resistance:** 50% damage when resisted
- **Visual Effect:** Custom particle effect (0x374A)

---

## 5. Paralyze (`An Ex Por`)

### Description
Freezes target in place, preventing movement and actions. Duration based on caster's EvalInt. Can be resisted for half duration.

### Meaning
The spell words "An Ex Por" translate to "Stop Movement" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Garlic, Mandrake Root, Spider's Silk
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Duration Formula
```
Base Duration = 5.0 seconds
EvalInt Bonus = EvalInt × 0.1
Total = Base + EvalInt Bonus

If Resisted: Duration × 0.5
```

### Simulation Scenarios

| Scenario | Magery | EvalInt | Base | Bonus | Total | Resisted | Notes |
|----------|--------|---------|------|-------|-------|----------|-------|
| Novice | 80 | 70 | 5 | 7 | 12 | 6 | Basic |
| Apprentice | 90 | 80 | 5 | 8 | 13 | 6.5 | Improved |
| Adept | 100 | 90 | 5 | 9 | 14 | 7 | Good |
| Expert | 120 | 110 | 5 | 11 | 16 | 8 | Strong |
| Master | 120 | 120 | 5 | 12 | 17 | 8.5 | Maximum |

### Curiosities & Easter Eggs
- **Movement Lock:** Prevents all movement and actions
- **Resistance:** 50% duration when resisted
- **Visual Effect:** Resistance shows barrier effect
- **PvP Essential:** Critical for PvP control
- **Spell Interruption:** Interrupts target's spellcasting

---

## 6. Dispel Field (`An Grav`)

### Description
Removes magical fields (fire, poison, energy) and dispels moongate destinations within range. Utility spell for area control.

### Meaning
The spell words "An Grav" translate to "Remove Field" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Black Pearl, Spider's Silk, Sulfurous Ash, Garlic
- **Mana Cost:** 40
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Dispel Range:** 2 tiles radius

### Field Types Dispelled
- Fire Field
- Poison Field
- Energy Field
- Moongate Destinations (2-tile range)

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Fields Dispelled | Notes |
|----------|--------|------------------|-------|
| Novice | 80 | All in range | Basic |
| Apprentice | 90 | All in range | Improved |
| Adept | 100 | All in range | Good |
| Expert | 120 | All in range | Strong |
| Master | 120 | All in range | Maximum |

*Success rate: 100% (no skill check)*

### Curiosities & Easter Eggs
- **Area Effect:** Dispels all fields in 2-tile radius
- **Moongate Dispel:** Can remove moongate destinations
- **Visual Effect:** Distinct dispel particle effect
- **Sound Effects:** Gender-specific reaction sounds
- **No Skill Check:** Always succeeds if field exists

---

## 7. Poison Field (`In Nox Grav`)

### Description
Creates a 5-tile line of poison that applies poison to anyone who steps on it or stands in it. Caster can be poisoned. Magic Resistance can reduce poison level.

### Meaning
The spell words "In Nox Grav" translate to "Create Poison Field" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Black Pearl, Nightshade, Spider's Silk
- **Mana Cost:** 40
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Poison Level Formula
```
Base Poison = Calculated from caster's Poisoning skill
Friendly Fire: Reduced level for allies
Magic Resistance: (MR / 2.5) - (Caster Magery / 5) chance to reduce level
```

### Resistance Formula
```
Resist Chance % = (Magic Resistance / 2.5) - (Caster Magery / 5)
Training Cap: MR can train up to 60.0 via poison field
```

### Duration
Based on NMSGetDuration (typically 20-70 seconds for harmful spells)

### Simulation Scenarios

| Scenario | Magery | Poisoning | Poison Level | MR Training | Notes |
|----------|--------|-----------|--------------|-------------|-------|
| Novice | 80 | 60 | Lesser | Up to 60 | Basic |
| Apprentice | 90 | 80 | Regular | Up to 60 | Improved |
| Adept | 100 | 100 | Greater | Up to 60 | Good |
| Expert | 120 | 120 | Deadly | Up to 60 | Strong |
| Master | 120 | 120 | Deadly | Up to 60 | Maximum |

### Curiosities & Easter Eggs
- **5-Tile Line:** Creates a line of 5 poison tiles
- **Caster Vulnerability:** Caster can be poisoned by own field
- **Magic Resistance:** Can resist and reduce poison level
- **MR Training:** Trains Magic Resistance up to 60.0
- **Friendly Fire:** 50% reduction for allies
- **No Corpse:** Summoned creatures evaporate on death

---

## 8. Incognito (`Kal In Ex`)

### Description
Disguises the caster as another player or creature. Changes name, gender, skin hue, hair, and facial hair. Cannot be used with Polymorph, Disguise Kit, or Body Paint.

### Meaning
The spell words "Kal In Ex" translate to "Change Appearance" in the magical language.

### Requirements
- **Minimum Magery:** 80.0
- **Reagents:** Bloodmoss, Garlic, Nightshade
- **Mana Cost:** 40
- **Target:** Player or Creature (same body type)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Appearance Changes
- Name (with "[fake]" title)
- Gender
- Skin Hue
- Hair Style & Color
- Facial Hair Style & Color

### Restrictions
- Cannot use with Polymorph
- Cannot use with Disguise Kit
- Cannot use with Body Paint
- Body type must match (human → human, gargoyle → gargoyle)

### Duration Formula
```
Duration = NMSGetDuration (harmful spell duration)
Typically: 40-90 seconds
```

### Simulation Scenarios

| Scenario | Magery | EvalInt | Duration (sec) | Notes |
|----------|--------|---------|-------------|-------|
| Novice | 80 | 70 | 40-60 | Basic |
| Apprentice | 90 | 80 | 45-70 | Improved |
| Adept | 100 | 90 | 50-75 | Good |
| Expert | 120 | 110 | 55-85 | Strong |
| Master | 120 | 120 | 60-90 | Maximum |

### Curiosities & Easter Eggs
- **Player & Creature:** Can copy both players and NPCs
- **Body Type Match:** Must match body type (human/gargoyle)
- **Equipment Unchanged:** Armor/clothes remain yours
- **"[fake]" Title:** Visible indicator of disguise
- **Transform Effects:** Visual and audio effects on transform/revert
- **Revert Sound:** "*oops*" message when disguise ends

---

## Common Patterns

### Summoning Spells
5th circle introduces advanced summoning:
- **Blade Spirits:** Wild, uncontrolled, attacks caster
- **Summon Creature:** Controlled, inherits karma, friendly

### Defensive Mechanics
- **Magic Reflection:** First spell reflection shield
- **Cooldown System:** 60-second cooldown after shield expires

### Area Control
- **Poison Field:** 5-tile line with poison application
- **Dispel Field:** Removes all fields in 2-tile radius

### Utility Spells
- **Incognito:** Full appearance disguise
- **Paralyze:** Movement and action prevention

---

## Strategic Applications

### PvP Strategies
1. **Magic Reflection:** Essential defensive shield
2. **Paralyze:** Control and movement lock
3. **Poison Field:** Area denial and damage over time
4. **Mind Blast:** INT-based damage (bypasses armor)
5. **Blade Spirits:** Area control (dangerous!)

### PvM Strategies
1. **Summon Creature:** Additional DPS and tank
2. **Poison Field:** Area control for groups
3. **Paralyze:** Control dangerous enemies
4. **Dispel Field:** Remove enemy fields

### Group Play
1. **Magic Reflection:** Pre-battle defensive prep
2. **Summon Creature:** Additional party member
3. **Poison Field + Dispel Field:** Area control combo
4. **Incognito:** Stealth and roleplay

---

## Summary

The 5th Circle provides advanced capabilities:
- **Summoning:** Wild and controlled creatures
- **Defense:** Magic Reflection shield system
- **Control:** Paralyze and area denial
- **Utility:** Disguise and field manipulation
- **Damage:** INT-based Mind Blast
- **Area Effects:** Poison Field for control

These spells represent master-level power and require strategic thinking, especially with wild summons, criminal acts, and complex defensive mechanics.

