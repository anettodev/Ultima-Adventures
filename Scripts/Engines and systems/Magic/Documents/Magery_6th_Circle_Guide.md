# Magery 6th Circle Spells - Complete Guide

## Overview

The 6th Circle of Magery represents the pinnacle of magical achievement, introducing powerful offensive spells, advanced utility magic, and sophisticated area control. These spells require expert skill (90.0 Magery) and provide gamechanging capabilities for both PvP and PvM.

**Circle Level:** Expert  
**Mana Cost:** 40  
**Difficulty:** 90-110 skill  
**Total Spells:** 8

---

## Spell List

1. **Dispel (`An Ort`)** - Utility/Creature Dispel
2. **Energy Bolt (`Corp Por`)** - Direct Damage/High Energy Damage
3. **Explosion (`Vas Ort Flam`)** - Delayed Area Damage
4. **Invisibility (`An Lor Xen`)** - Utility/Stealth
5. **Mark (`Kal Por Ylem`)** - Utility/Rune Marking
6. **Mass Curse (`Vas Des Sanct`)** - Area Debuff/Stat Reduction
7. **Paralyze Field (`In Ex Grav`)** - Area Control/Movement Denial
8. **Reveal (`Wis Quas`)** - Utility/Detection

---

## 1. Dispel (`An Ort`)

### Description
Dispels summoned creatures with a chance based on the caster's skills and the target's health. Low-health summoned creatures have a better chance of being dispelled. Includes chaos momentum mechanic for dynamic dispel chances.

### Meaning
The spell words "An Ort" translate to "Dispel Summon" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Garlic, Mandrake Root, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Dispel Mechanics
```
Chaos Momentum = Random(0-10)
Dispel Chance = NMSUtils.getDispelChance(caster, target, chaos)
Success Threshold = NMSUtils.getSummonDispelPercentage(target, chaos)

Low Health Bonus:
If Health < 20%: Additional chance = Health / 10
```

### Special Cases
- **Wizard Creatures (ControlSlots == 666):** Always fail
- **Low Health:** Double chance if below 20% health
- **Success:** Creature disappears with effects
- **Failure:** Particle effect and sound

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Target Health | Chaos | Base Chance | Bonus | Result |
|----------|--------|---------------|-------|-------------|-------|--------|
| Novice | 90 | 100% | 5 | 45% | 0% | Medium |
| Apprentice | 100 | 100% | 5 | 55% | 0% | Good |
| Adept | 110 | 50% | 5 | 65% | 0% | Better |
| Expert | 120 | 20% | 5 | 75% | 30% | High |
| Master | 120 | 10% | 8 | 80% | 40% | Very High |

### Curiosities & Easter Eggs
- **Chaos Momentum:** Random 0-10 factor affects every cast
- **Low Health Advantage:** Wounded summons easier to dispel
- **Wizard Protection:** Special creatures with ControlSlots=666 immune
- **Visual Feedback:** Different effects for success/failure
- **No Corpse:** Dispelled creatures evaporate

---

## 2. Energy Bolt (`Corp Por`)

### Description
Instant high-damage energy attack spell. One of the strongest direct damage spells in the 6th circle. Pure energy damage with no resistance reduction.

### Meaning
The spell words "Corp Por" translate to "Energy Strike" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Black Pearl, Nightshade
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Damage Formula
```
Damage = GetNMSDamage(23, 1, 4, target)
Base: 23 + 1d4 = 24-27 base damage
+ Magery/EvalInt scaling
+ Inscription bonus
```

### Duration
N/A (instant damage)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Inscription | Base Damage | Final Range | Notes |
|----------|--------|---------|-------------|-------------|-------------|-------|
| Novice | 90 | 80 | 0 | 24-27 | 24-35 | Basic |
| Apprentice | 100 | 90 | 50 | 24-27 | 28-40 | Improved |
| Adept | 110 | 100 | 80 | 24-27 | 32-45 | Good |
| Expert | 120 | 110 | 100 | 24-27 | 36-50 | Strong |
| Master | 120 | 120 | 120 | 24-27 | 40-55 | Maximum |

### Curiosities & Easter Eggs
- **Pure Energy:** No elemental resistance applies
- **Instant Cast:** No delay, immediate damage
- **High Damage:** One of strongest 6th circle spells
- **Visual Effect:** Distinctive energy bolt particles (0x3818)
- **Sound:** Recognizable energy crackle (0x20A)

---

## 3. Explosion (`Vas Ort Flam`)

### Description
Delayed area-of-effect fire damage spell. Causes a massive explosion after a short delay (2.8-3.3 seconds). Can be held for up to 10 seconds before targeting. High damage with splash potential.

### Meaning
The spell words "Vas Ort Flam" translate to "Great Summon Flame" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Bloodmoss, Mandrake Root
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Hold Time:** Max 10 seconds
- **Explosion Delay:** 2.8s (Legacy) / 3.3s (AOS)

### Damage Formula
```
AOS Mode:
Base = GetNMSDamage(25, 1, 5, target)
= 25 + 1d5 = 26-30 base
+ Magery/EvalInt scaling
If Resisted: × 0.5

Legacy Mode:
Base = Random(23-44)
If Resisted: × 0.75
```

### Duration
**Delay:** 2.8-3.3 seconds before explosion  
**Hold:** Can hold spell for up to 10 seconds before targeting

### Simulation Scenarios

| Scenario | Magery | EvalInt | Delay (s) | Base Damage | Resisted | Final Range | Notes |
|----------|--------|---------|-----------|-------------|----------|-------------|-------|
| Novice | 90 | 80 | 3.3 | 26-30 | 50% | 28-42 | Basic |
| Apprentice | 100 | 90 | 3.3 | 26-30 | 50% | 32-48 | Improved |
| Adept | 110 | 100 | 3.0 | 26-30 | 50% | 36-54 | Good |
| Expert | 120 | 110 | 2.8 | 26-30 | 50% | 40-60 | Strong |
| Master | 120 | 120 | 2.8 | 26-30 | 50% | 45-65 | Maximum |

### Curiosities & Easter Eggs
- **Delayed Explosion:** Time to react or move
- **Hold Mechanic:** Can hold spell up to 10 seconds
- **Head Particles:** Distinctive swirling particles (0x36BD)
- **Area Effect:** Explosion visuals at target location
- **Fire Damage:** Benefits from slayer spellbooks
- **Spell Hold Timeout:** Auto-cancels after 10 seconds

---

## 4. Invisibility (`An Lor Xen`)

### Description
Makes target mobile invisible for an extended duration. Duration scales with Magery, Inscription, and Hiding skills. Target becomes visible upon taking damage or performing actions. Optional pet hiding (default: off).

### Meaning
The spell words "An Lor Xen" translate to "Create Shadow Being" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Bloodmoss, Nightshade
- **Mana Cost:** 40
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Duration Formula
```
Base Duration = NMSGetDuration(caster, target, beneficial=true)
Hiding Bonus = (Hiding Skill / 10) × 2 seconds
Total Duration = Base + Hiding Bonus

Example at GM:
Base: ~40-50 seconds (Magery + Inscription)
Hiding Bonus at 100: +20 seconds
Total: ~60-70 seconds
```

### Breaking Invisibility
- Taking any damage
- Performing hostile actions
- Moving items
- Opening doors
- Speaking
- Using skills

### Simulation Scenarios

| Scenario | Magery | Inscription | Hiding | Base (s) | Bonus (s) | Total (s) | Notes |
|----------|--------|-------------|--------|----------|-----------|-----------|-------|
| Novice | 90 | 60 | 0 | 30-40 | 0 | 30-40 | Basic |
| Apprentice | 100 | 80 | 50 | 40-50 | +10 | 50-60 | Improved |
| Adept | 110 | 100 | 80 | 45-55 | +16 | 61-71 | Good |
| Expert | 120 | 120 | 100 | 50-60 | +20 | 70-80 | Strong |
| Master | 120 | 120 | 120 | 50-60 | +24 | 74-84 | Maximum |

### Curiosities & Easter Eggs
- **Hiding Synergy:** Hiding skill adds +2s per 10 points
- **Pet Hiding:** Optional feature (default: false)
- **Damage Reveal:** ANY damage instantly reveals
- **Thread-Safe:** Uses Dictionary for timer storage
- **Combat Break:** Clears combatant and war mode
- **Pet Search Range:** 12 tiles for pet hiding
- **Instant Reveal:** No delay when damage taken

---

## 5. Mark (`Kal Por Ylem`)

### Description
Marks a recall rune with the caster's current location for later teleportation via Recall or Gate Travel. Requires caster to remain completely still during casting. Any movement cancels the spell with visual feedback. Displays marked location information.

### Meaning
The spell words "Kal Por Ylem" translate to "Summon Portal Stone" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Black Pearl, Bloodmoss, Mandrake Root
- **Mana Cost:** 40
- **Target:** Recall Rune
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Restriction:** Must remain stationary

### Marking Restrictions
- Cannot mark in certain regions (prisons, no-recall zones)
- Caster must not move during casting
- Rune must be unmarked or owned by caster
- Some areas block marking entirely

### Movement Cancellation
```
If player moves during casting:
- Visual fizzle effect (0x3735)
- Sound effect (0x5C)
- Overhead message: "Fizzle!"
- PT-BR message: "Você não pode se mover enquanto lança este feitiço."
- Spell cancelled
```

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Success Rate | Notes |
|----------|--------|--------------|-------|
| Novice | 90 | 100%* | *If still during cast |
| Apprentice | 100 | 100%* | *If still during cast |
| Adept | 110 | 100%* | *If still during cast |
| Expert | 120 | 100%* | *If still during cast |
| Master | 120 | 100%* | *If still during cast |

*Movement = automatic failure with visual feedback*

### Curiosities & Easter Eggs
- **Movement Detection:** Tracks exact starting position
- **Fizzle Feedback:** Visual/audio/text confirmation
- **Location Display:** Shows "Você marcou uma runa em {Region} (X, Y, Z)"
- **Gender Sounds:** Different sounds for male/female casters
- **Overhead Message:** Color-coded feedback (0x3B2)
- **Region Awareness:** Displays current region name
- **No Tolerance:** Even 1 tile movement cancels

---

## 6. Mass Curse (`Vas Des Sanct`)

### Description
Area-effect debuff that reduces Strength, Dexterity, and Intelligence of all valid targets in range. Powerful group debuff for PvP and PvM. Sorcerers get extended range. Duration calculated once to prevent message spam.

### Meaning
The spell words "Vas Des Sanct" translate to "Great Lower Sanctity" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Garlic, Nightshade, Mandrake Root, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Effect Radius:** 2 tiles (normal) / 5 tiles (sorcerer)

### Stat Reduction Formula
```
Duration = NMSGetDuration(caster, caster, harmful=false)
Each stat reduced by curse amount
Based on caster's Magery + EvalInt

Resistance reduces duration
```

### Area Effect
- **Normal:** 2-tile radius from target point
- **Sorcerer:** 5-tile radius from target point
- **Valid Targets:** All hostiles in range
- **Invalid:** Caster (AOS mode)

### Duration
Based on NMSGetDuration (typically 40-90 seconds)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Area (tiles) | Duration (s) | Targets | Notes |
|----------|--------|---------|--------------|-------------|---------|-------|
| Novice | 90 | 80 | 2 | 40-60 | 2-5 | Basic |
| Apprentice | 100 | 90 | 2 | 50-70 | 3-7 | Improved |
| Adept (Sorcerer) | 110 | 100 | 5 | 60-80 | 5-15 | Extended |
| Expert | 120 | 110 | 2 | 70-90 | 4-10 | Strong |
| Master (Sorcerer) | 120 | 120 | 5 | 75-95 | 8-20 | Maximum |

### Curiosities & Easter Eggs
- **Sorcerer Bonus:** 2.5× larger radius (5 vs 2 tiles)
- **Single Duration:** Calculated once, no spam
- **Summary Message:** "Você amaldiçoou X alvo(s)"
- **Stat Curse:** Uses SpellHelper.AddStatCurse
- **Area Denial:** Strong PvP control spell
- **Town Restrictions:** Respects town rules

---

## 7. Paralyze Field (`In Ex Grav`)

### Description
Creates a 5-tile line of paralyzing energy that freezes anyone walking over it, including the caster. Duration scales with Magery and EvalInt. NPCs receive double duration. Uses centralized field helper for consistency.

### Meaning
The spell words "In Ex Grav" translate to "Create Stop Field" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Black Pearl, Ginseng, Spider's Silk
- **Mana Cost:** 40
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Field Duration Formula
```
Field Duration = FieldSpellHelper.GetFieldDuration(caster)
Typically: 20-70 seconds based on Magery + Inscription
```

### Paralyze Duration Formula
```
Base = 3.0 seconds
Magery Bonus = Magery × 0.1
EvalInt Bonus = EvalInt × 0.1
Total = Base + Magery + EvalInt

NPCs: Total × 2.0
```

### Field Mechanics
- **5-Tile Line:** East-West or North-South orientation
- **Auto-Orientation:** Uses FieldSpellHelper.GetFieldOrientation
- **Caster Vulnerability:** Caster CAN be paralyzed
- **Item ID:** 0x3967 (E-W) / 0x3979 (N-S)

### Duration
**Field:** 20-70 seconds (based on skills)  
**Paralyze:** 3-17 seconds per trigger (based on skills)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Field (s) | Paralyze (s) | NPC Paralyze (s) | Notes |
|----------|--------|---------|-----------|--------------|------------------|-------|
| Novice | 90 | 80 | 25-35 | 11 | 22 | Basic |
| Apprentice | 100 | 90 | 35-45 | 12 | 24 | Improved |
| Adept | 110 | 100 | 45-55 | 14 | 28 | Good |
| Expert | 120 | 110 | 55-65 | 16 | 32 | Strong |
| Master | 120 | 120 | 60-70 | 17 | 34 | Maximum |

### Curiosities & Easter Eggs
- **Self-Paralyze:** Caster can freeze in own field
- **Double Duration:** NPCs paralyzed 2× longer
- **Field Orientation:** Auto-calculates based on caster position
- **5-Tile Line:** Creates wall of paralyzing energy
- **Visual Effect:** Distinct particles per freeze (0x376A)
- **Sound Effects:** Paralyze sound on trigger (0x204)

---

## 8. Reveal (`Wis Quas`)

### Description
Reveals hidden mobiles, traps, and hidden containers in an area around a target location. Detection range scales with Magery. Can detect Hiding/Stealth users with skill-based chance. Trap detection shows chest location and generates loot. Reduced detection chance (20% multiplier) rewards Hiding/Stealth investment.

### Meaning
The spell words "Wis Quas" translate to "Know Illusion" in the magical language.

### Requirements
- **Minimum Magery:** 90.0
- **Reagents:** Bloodmoss, Sulfurous Ash
- **Mana Cost:** 40
- **Target:** Ground location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Detection Range:** 1 + (Magery / 50) tiles

### Detection Range Formula
```
Detection Range = 1 + (Magery / 50) tiles
```

| Magery | Detection Range |
|--------|-----------------|
| 50 | 2 tiles |
| 75 | 2.5 tiles |
| 100 | 3 tiles |
| 120 | 3.4 tiles |

### Hiding/Stealth Detection Formula
```
Caster Power = (Magery + DetectHidden) × 20%
Target Defense = Hiding + Stealth
Chance = Caster Power - Target Defense

If Chance > 0: Reveal target
Else: Target remains hidden
```

### Detection Types
1. **Invisibility Spell:** 100% detection
2. **Hiding Skill:** Skill-based chance (reduced 50%)
3. **Hidden Traps:** Level-based detection
4. **Hidden Containers:** 1-in-3 chance, generates loot

### Simulation Scenarios

#### Mobile Detection

| Caster Skills | Target Skills | Detection Chance | Notes |
|---------------|---------------|------------------|-------|
| 100 Mag + 0 DH | 100 Hiding | 0% | Pure hider safe |
| 100 Mag + 50 DH | 100 Hiding | 10% | Slight chance |
| 100 Mag + 100 DH | 100 Hiding | 40% | Moderate |
| 120 Mag + 100 DH | 100/100 Hide/Stealth | 4% | Skilled stealth |
| 120 Mag + 100 DH | 120/120 Hide/Stealth | 0% | Master stealth safe |

#### Trap Detection

| Magery | Level Range | Success Rate | Loot Generated |
|--------|-------------|--------------|----------------|
| 90 | 1-4 | High | 300-1000 gold |
| 100 | 1-5 | High | 300-1000 gold |
| 110 | 1-5 | Very High | 300-1000 gold |
| 120 | 1-6 | Very High | 300-1000 gold |

### Curiosities & Easter Eggs
- **Reduced Detection:** 20% multiplier (was 40%) rewards stealth
- **Trap Loot:** Reveals hidden chests with gold
- **Hiding Investment:** High Hiding/Stealth hard to detect
- **DetectHidden Synergy:** Bonus from DH skill
- **Invisibility Spell:** Always 100% reveal
- **Visual Feedback:** Different effects for mobile vs trap
- **Area Effect:** Scales with caster's Magery skill
- **Stealth Advantage:** Stealth adds full value to defense

---

## Common Patterns

### Offensive Spells
The 6th circle features two premier damage dealers:
- **Energy Bolt:** Instant high-damage single target
- **Explosion:** Delayed area-effect with highest potential damage

### Utility Spells
Advanced utility for various situations:
- **Mark:** Essential for travel network setup
- **Invisibility:** Extended stealth with skill synergy
- **Reveal:** Detection and trap finding
- **Dispel:** Creature removal

### Area Control
Powerful group manipulation:
- **Mass Curse:** Area-wide stat debuff
- **Paralyze Field:** 5-tile movement denial line

---

## Strategic Applications

### PvP Strategies
1. **Explosion + Energy Bolt:** Combo for massive burst damage
2. **Mass Curse:** Debuff enemy group before engagement
3. **Paralyze Field:** Block escape routes or entry points
4. **Invisibility:** Escape or repositioning
5. **Mark:** Quick escape runes pre-marked
6. **Reveal:** Counter stealth assassins

### PvM Strategies
1. **Dispel:** Remove enemy summons quickly
2. **Energy Bolt:** High single-target DPS
3. **Explosion:** Area damage for groups
4. **Paralyze Field:** Control enemy movement
5. **Mass Curse:** Weaken tough enemy groups

### Group Play
1. **Mass Curse:** Powerful group debuff opener
2. **Paralyze Field:** Area denial for team
3. **Reveal:** Trap detection for party safety
4. **Dispel:** Remove enemy summons
5. **Mark:** Mark strategic locations for group

---

## Summary

The 6th Circle represents the pinnacle of magical mastery:
- **Damage:** Highest damage spells (Explosion, Energy Bolt)
- **Control:** Powerful area denial (Paralyze Field, Mass Curse)
- **Utility:** Essential tools (Mark, Invisibility, Reveal, Dispel)
- **Complexity:** Advanced mechanics (chaos momentum, skill synergies)
- **Strategy:** Requires tactical thinking and skill investment

These spells define expert-level gameplay and separate masters from novices. Proper use of 6th circle magic combined with skill development creates truly formidable mages.

