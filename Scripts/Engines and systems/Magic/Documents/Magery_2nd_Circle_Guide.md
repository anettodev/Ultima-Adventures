# Magery 2nd Circle Spells - Complete Guide

## Overview

The 2nd Circle of Magery introduces more specialized spells, including stat buffs, poison curing, and utility magic. These spells require moderate skill (40.0 Magery) and provide essential support capabilities for both solo and group play.

**Circle Level:** Intermediate  
**Mana Cost:** 16  
**Difficulty:** 40-60 skill  
**Total Spells:** 8

---

## Spell List

1. **Agility (`Ex Uus`)** - Buff/Stat Increase
2. **Cunning (`Uus Wis`)** - Buff/Stat Increase
3. **Cure (`An Nox`)** - Beneficial/Poison Removal
4. **Harm (`An Mani`)** - Attack/Damage
5. **Magic Trap (`In Jux`)** - Utility/Security
6. **Remove Trap (`An Jux`)** - Utility/Security
7. **Protection (`Uus Sanct`)** - Defensive Buff
8. **Strength (`Uus Mani`)** - Buff/Stat Increase

---

## 1. Agility (`Ex Uus`)

### Description
Increases the target's Dexterity temporarily, improving movement speed, weapon swing speed, and dexterity-based actions. The counterpart to Clumsy.

### Meaning
The spell words "Ex Uus" translate to "Increase Dexterity" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Bloodmoss, Mandrake Root
- **Mana Cost:** 16
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Increase Formula
```
Stat Increase = Target's RawDex × GetOffsetScalar(Caster, Target, false)
GetOffsetScalar = (1 + (Inscribe / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)
```

**Minimum Increase:** 1% of target's stat

### Duration Formula
```
Base Duration = Random(10-30) seconds
Duration Bonus = Ceiling(Inscribe × 0.25) + Random bonus based on Inscribe tier
Final Duration = (Base + Bonus) × 0.7 (30% reduction for beneficial)
Capped: 15-90 seconds
```

**Inscribe Bonus Tiers:**
- 120+: +20-40 seconds
- 100-119: +18-30 seconds
- 80-99: +16-25 seconds
- 60-79: +10-20 seconds
- Below 60: Base only

### Simulation Scenarios

| Scenario | Magery | Inscribe | Target Base Dex | Increase % | Duration (sec) |
|----------|--------|----------|-----------------|------------|----------------|
| Novice | 50 | 40 | 50 | ~4% | 15-25 |
| Apprentice | 70 | 60 | 70 | ~5% | 20-30 |
| Adept | 90 | 80 | 90 | ~6% | 25-40 |
| Expert | 100 | 100 | 100 | ~7% | 30-50 |
| Master | 120 | 120 | 120 | ~8% | 35-60 |

### Curiosities & Easter Eggs
- **Counteracts Clumsy:** Can remove or reduce Clumsy curse effects
- **Movement Speed:** Noticeably improves target's movement
- **Weapon Speed:** Faster weapon swings
- **No Reflection:** Beneficial spells cannot be reflected

---

## 2. Cunning (`Uus Wis`)

### Description
Increases the target's Intelligence temporarily, expanding their mana pool and improving intelligence-based skills. The counterpart to Feeblemind.

### Meaning
The spell words "Uus Wis" translate to "Increase Wisdom/Intelligence" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Mandrake Root, Nightshade
- **Mana Cost:** 16
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Increase Formula
```
Stat Increase = Target's RawInt × GetOffsetScalar(Caster, Target, false)
GetOffsetScalar = (1 + (Inscribe / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)
```

### Duration Formula
Same as Agility (see above).

### Simulation Scenarios

| Scenario | Magery | Inscribe | Target Base Int | Increase % | Duration (sec) |
|----------|--------|----------|-----------------|------------|----------------|
| Novice | 50 | 40 | 50 | ~4% | 15-25 |
| Apprentice | 70 | 60 | 70 | ~5% | 20-30 |
| Adept | 90 | 80 | 90 | ~6% | 25-40 |
| Expert | 100 | 100 | 100 | ~7% | 30-50 |
| Master | 120 | 120 | 120 | ~8% | 35-60 |

### Curiosities & Easter Eggs
- **Mana Boost:** Increases both current and maximum mana
- **Counteracts Feeblemind:** Can remove or reduce Feeblemind curse
- **Spellcasting Bonus:** More mana means more spells can be cast
- **Skill Bonus:** Improves intelligence-based skill checks

---

## 3. Cure (`An Nox`)

### Description
Attempts to cure poison from the target. Essential for survival in poison-heavy areas and PvP encounters.

### Meaning
The spell words "An Nox" translate to "Remove Poison" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Garlic, Ginseng
- **Mana Cost:** 16
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Cure Chance Formula
```
Base Chance = BeneficialMageryInscribePercentage
Healing Bonus = (Healing / 10) × 1% (1% per 10 skill points)
Penalty = Poison Level
Adjusted Chance = Base + Healing Bonus - Penalty

Success Condition: Adjusted Chance ≥ Random(Poison Level × 2, 100)
```

**Special Rules:**
- **Lethal Poison (Level 4+):** Cannot be cured (returns 0% chance)
- **Mortal Strike:** Cannot cure if target has Mortal Strike wound

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Inscribe | Healing | Poison Level | Cure Chance | Notes |
|----------|--------|----------|---------|--------------|-------------|-------|
| Novice | 50 | 40 | 0 | Level 1 | ~45% | Basic |
| Apprentice | 70 | 60 | 50 | Level 2 | ~55% | Improved |
| Adept | 90 | 80 | 70 | Level 3 | ~60% | Good |
| Expert | 100 | 100 | 80 | Level 3 | ~70% | Strong |
| Master | 120 | 120 | 100 | Level 3 | ~80% | Excellent |

**Level 4+ Poison:** 0% chance (cannot cure lethal poison)

### Curiosities & Easter Eggs
- **Lethal Poison Block:** Cannot cure Level 4+ poison (requires higher circle spells)
- **Healing Skill Bonus:** Having Healing skill significantly improves cure chance
- **Karma Reward:** Successfully curing grants 10 karma
- **Visual Feedback:** Different particle effects for success (green) vs failure (purple)
- **Mortal Strike Block:** Cannot cure targets with Mortal Strike wound

---

## 4. Harm (`An Mani`)

### Description
Deals cold damage to the target at close range. Instant damage spell useful for finishing weakened enemies.

### Meaning
The spell words "An Mani" translate to "Harm" or "Damage" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Nightshade, Spider's Silk
- **Mana Cost:** 16
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Damage Formula
```
Base Damage = Dice(1, 4) + 4 = 5-8 base
Final Damage = Base × EvalInt Benefit
```

**EvalInt Benefit:** Scales with EvalInt skill and Inscription

### Duration
N/A (instant damage, no delay)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Base Damage | Final Damage | Notes |
|----------|--------|---------|-------------|--------------|-------|
| Novice | 50 | 40 | 5-8 | 5-9 | Low |
| Apprentice | 70 | 60 | 5-8 | 6-10 | Moderate |
| Adept | 90 | 80 | 5-8 | 7-12 | Good |
| Expert | 100 | 90 | 5-8 | 8-13 | Strong |
| Master | 120 | 120 | 5-8 | 9-15 | Maximum |

### Curiosities & Easter Eggs
- **100% Cold Damage:** All damage is cold type (no physical component)
- **Instant Damage:** No travel time (unlike Magic Arrow)
- **No Slayer Bonus:** This spell isn't affected by slayer spellbooks
- **Reflection:** Can be reflected back at caster
- **Close Range:** Effective for melee-range combat

---

## 5. Magic Trap (`In Jux`)

### Description
Places a magical trap on a container or creates a ground trap. Traps trigger when opened/stepped on, dealing damage and potentially poisoning the victim.

### Meaning
The spell words "In Jux" translate to "Create Trap" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Garlic, Spider's Silk, Sulfurous Ash
- **Mana Cost:** 16
- **Target:** Container or Ground Location
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Trap Power Formula
```
Container Trap:
  Trap Power = (Magery / Divisor) + Random(0-2)
  Divisor = 3 (Level 1), 4 (Level 2), 5 (Level 3)
  Maximum Traps: 3 per container

Ground Trap:
  Creates trap at target location
  Damage based on Magery skill
```

### Duration
- **Container Trap:** Permanent until triggered or removed
- **Ground Trap:** Permanent until triggered

### Simulation Scenarios

| Scenario | Magery | Container Trap Level | Ground Trap Damage | Notes |
|----------|--------|---------------------|-------------------|-------|
| Novice | 50 | Level 1 (16-18) | Low | Basic |
| Apprentice | 70 | Level 2 (17-19) | Moderate | Improved |
| Adept | 90 | Level 2-3 (18-20) | Good | Strong |
| Expert | 100 | Level 3 (20-22) | Strong | Powerful |
| Master | 120 | Level 3 (24-26) | Maximum | Best |

### Curiosities & Easter Eggs
- **Dual Mode:** Can target containers or ground locations
- **Trap Limit:** Maximum 3 traps per container
- **Poison Chance:** Higher level traps can apply poison
- **Security:** Useful for protecting valuable containers
- **PvP Tool:** Ground traps can be used strategically in combat

---

## 6. Remove Trap (`An Jux`)

### Description
Removes traps from containers or creates a protection wand when targeting self. Essential for safely opening trapped containers.

### Meaning
The spell words "An Jux" translate to "Remove Trap" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Bloodmoss, Sulfurous Ash
- **Mana Cost:** 16
- **Target:** Container or Self
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Success Formula
```
Success Chance = (Magery / 3) - Random(0-1)
Minimum Success: 0% (if skill too low)
```

**Special Mode - Protection Wand:**
- **Target Self:** Creates a protection wand instead
- **Wand Power:** 20-50 (based on Magery)
- **Balance Nerf:** Wand creation is intentionally limited

### Duration
N/A (instant effect)

### Simulation Scenarios

| Scenario | Magery | Success % | Wand Power | Notes |
|----------|--------|-----------|------------|-------|
| Novice | 50 | ~16% | 20-30 | Low |
| Apprentice | 70 | ~23% | 25-35 | Moderate |
| Adept | 90 | ~30% | 30-40 | Good |
| Expert | 100 | ~33% | 35-45 | Strong |
| Master | 120 | ~40% | 40-50 | Maximum |

### Curiosities & Easter Eggs
- **Dual Function:** Remove traps OR create protection wand
- **Wand Creation:** Targeting self creates a wand (balance nerf applied)
- **Skill Scaling:** Higher Magery = better success rate
- **Risk/Reward:** Failed attempts can trigger the trap
- **Protection Wand:** Useful for storing protection charges

---

## 7. Protection (`Uus Sanct`)

### Description
A toggle buff that reduces all resistances but prevents spell disruption. Trade-off: lower defenses but cannot be interrupted while casting.

### Meaning
The spell words "Uus Sanct" translate to "Increase Protection" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Garlic, Ginseng, Sulfurous Ash
- **Mana Cost:** 16
- **Target:** Self only
- **Toggle Spell:** Cast again to remove

### Resistance Penalties
```
Each Resistance: Random(-8 to -2)
Affects: Physical, Fire, Cold, Poison, Energy
```

### Disruption Protection
```
First Hit: 100% protection (spell cannot be interrupted)
After First: 50% protection (balance nerf)
```

### Duration
- **AOS Mode:** Toggle (permanent until removed)
- **Legacy Mode:** Based on Magery skill (15-30 seconds)

### Simulation Scenarios

| Scenario | Magery | Resist Penalty | Disruption Protection | Notes |
|----------|--------|----------------|----------------------|-------|
| Novice | 50 | -2 to -8 each | 100% / 50% | Standard |
| Apprentice | 70 | -2 to -8 each | 100% / 50% | Standard |
| Adept | 90 | -2 to -8 each | 100% / 50% | Standard |
| Expert | 100 | -2 to -8 each | 100% / 50% | Standard |
| Master | 120 | -2 to -8 each | 100% / 50% | Standard |

*Resistance penalties are random and independent of skill*

### Curiosities & Easter Eggs
- **Balance Nerf:** Disruption protection reduced from 100% to 50% after first hit
- **Resistance Trade-off:** Lower resistances for spell protection
- **Toggle Spell:** Cast again to remove (no duration in AOS)
- **PvP Essential:** Critical for mages in player vs player combat
- **Random Penalties:** Each resistance gets independent random penalty

---

## 8. Strength (`Uus Mani`)

### Description
Increases the target's Strength temporarily, improving melee damage, carrying capacity, and strength-based actions. The counterpart to Weaken.

### Meaning
The spell words "Uus Mani" translate to "Increase Strength" in the magical language.

### Requirements
- **Minimum Magery:** 40.0
- **Reagents:** Mandrake Root, Nightshade
- **Mana Cost:** 16
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Increase Formula
```
Stat Increase = Target's RawStr × GetOffsetScalar(Caster, Target, false)
GetOffsetScalar = (1 + (Inscribe / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)
```

### Duration Formula
Same as Agility (see above).

### Simulation Scenarios

| Scenario | Magery | Inscribe | Target Base Str | Increase % | Duration (sec) |
|----------|--------|----------|-----------------|------------|----------------|
| Novice | 50 | 40 | 50 | ~4% | 15-25 |
| Apprentice | 70 | 60 | 70 | ~5% | 20-30 |
| Adept | 90 | 80 | 90 | ~6% | 25-40 |
| Expert | 100 | 100 | 100 | ~7% | 30-50 |
| Master | 120 | 120 | 120 | ~8% | 35-60 |

### Curiosities & Easter Eggs
- **Damage Boost:** Increases melee damage output
- **Carrying Capacity:** Allows target to carry more weight
- **Counteracts Weaken:** Can remove or reduce Weaken curse
- **Warrior Support:** Essential buff for melee fighters
- **Group Play:** Commonly cast on party members before combat

---

## Common Patterns

### Stat Buff System
All stat buffs (Agility, Cunning, Strength) use the same formula:
- **Base Calculation:** `GetOffsetScalar` with Inscription skill
- **Duration:** Beneficial spell duration (30% reduction, 15-90 second cap)
- **Counteract Curses:** Can remove or reduce corresponding curse effects

### Duration System
Beneficial spells have reduced duration:
- **30% Reduction:** Applied to all beneficial buff durations
- **Cap:** Minimum 15 seconds, maximum 90 seconds
- **Inscription Scaling:** Higher Inscription = longer duration

### Cure System
- **Lethal Poison Block:** Level 4+ poison cannot be cured
- **Healing Skill Bonus:** 1% cure chance per 10 Healing skill
- **Karma Reward:** 10 karma per successful cure

---

## Strategic Applications

### Support Mage Build
1. **Stat Buffs:** Agility, Cunning, Strength for party members
2. **Cure:** Essential for poison-heavy areas
3. **Protection:** Self-buff for spell protection

### Solo Play
1. **Harm:** Reliable damage spell
2. **Magic Trap:** Protect containers and create ground traps
3. **Remove Trap:** Safely open trapped containers

### PvP Applications
1. **Protection:** Critical for preventing spell interruption
2. **Cure:** Remove poison from yourself or allies
3. **Stat Buffs:** Enhance combat effectiveness

---

## Summary

The 2nd Circle provides essential support capabilities:
- **Stat Buffs:** Agility, Cunning, Strength for enhancing allies
- **Healing Support:** Cure for poison removal
- **Offense:** Harm for reliable damage
- **Utility:** Magic Trap and Remove Trap for security
- **Defense:** Protection for spell interruption prevention

These spells are essential for any support-focused mage and remain valuable throughout the game, especially Protection which is critical for PvP mages.

