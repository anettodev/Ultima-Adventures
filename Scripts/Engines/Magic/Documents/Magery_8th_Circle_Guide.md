# Magery 8th Circle Spells - Complete Guide

## Overview

The 8th Circle of Magery represents the pinnacle of magical mastery, introducing the most powerful and complex spells in the game. These spells require expert skill (100.0+ Magery) and provide game-changing capabilities for both PvP and PvM. This circle features devastating area-of-effect damage, powerful summons, and life-saving resurrection magic.

**Circle Level:** Grandmaster  
**Mana Cost:** 40-60  
**Difficulty:** 100-120 skill  
**Total Spells:** 8

---

## Spell List

1. **Air Elemental (`Kal Vas Xen Hur`)** - Summons Air Elemental
2. **Earth Elemental (`Kal Vas Xen Ylem`)** - Summons Earth Elemental
3. **Earthquake (`In Vas Por`)** - Area-of-Effect Ground Damage
4. **Energy Vortex (`Vas Corp Por`)** - Wild Uncontrolled Summon
5. **Fire Elemental (`Kal Vas Xen Flam`)** - Summons Fire Elemental
6. **Resurrection (`An Corp`)** - Resurrects Players, Pets, and Creates SoulOrb
7. **Summon Daemon (`Kal Vas Xen Corp`)** - Summons Daemon
8. **Water Elemental (`Kal Vas Xen An Flam`)** - Summons Water Elemental

---

## 1. Air Elemental (`Kal Vas Xen Hur`)

### Description
Summons a powerful Air Elemental creature to fight alongside the caster. The elemental is a controlled summon that follows the caster's commands. Casters with 100+ Magery and 100+ Inscription have a chance to summon a Greater Air Elemental (Super Elemental do Ar) instead of a regular one. Only one elemental per caster is allowed - casting another elemental dismisses the previous one.

### Meaning
The spell words "Kal Vas Xen Hur" translate to "Summon Great Air Creature" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Mandrake Root, Spider's Silk
- **Mana Cost:** 269
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Duration Formula
```
Duration = (Magery.Fixed × 0.1) × Inscription_Bonus
Base: Magery skill × 0.1 seconds
Bonus: Inscription benefit multiplier (NMSUtils.getBonusIncriptBenefit)
Formula: ((Inscription × 3) / 100) + 1) / 10) + 1
```

### Greater Elemental Chance Formula
```
Requirements: Magery ≥ 100.0 AND Inscription ≥ 100.0
Base Chance: 20%
EvalInt Bonus: +0.2% per EvalInt point
Total Chance: 20% + (EvalInt × 0.2%)
Maximum: 40% (at EvalInt 100)
```

**Examples:**
- Magery 100, Inscription 100, EvalInt 0: **20%** chance
- Magery 100, Inscription 100, EvalInt 50: **30%** chance
- Magery 100, Inscription 100, EvalInt 100: **40%** chance
- Magery 120, Inscription 120, EvalInt 120: **44%** chance

### Creature Names (PT-BR)
- **Regular:** "an air elemental" (English)
- **Greater:** "Super Elemental do Ar" (PT-BR)
- **Corpse (Regular):** "an air elemental corpse"
- **Corpse (Greater):** "corpo de Super Elemental do Ar"

### Visual Properties
- **Hue (Regular):** 2229 (light blue/air)
- **Hue (Greater):** 2229 (light blue/air)
- **Body:** 13 (Regular) / 273 (Greater)

### Dispel Resistance
- **DispelDifficulty:** 117.5
- **DispelFocus:** 45.0
- **Base Difficulty:** 16.25
- **Note:** Regular and Greater have identical dispel resistance

### Special Mechanics
- **Controlled Summon:** Follows caster's commands
- **Single Elemental Limit:** Only one elemental per caster (any type: Air, Earth, Fire, Water)
  - Casting a new elemental automatically dismisses the previous one
  - Works across all elemental types (cannot have Air + Fire simultaneously)
- **Greater Elemental:** Requires Magery ≥ 100 AND Inscription ≥ 100
- **Duration:** Scales with Magery and Inscription skills
- **Can be Dispelled:** Standard dispel mechanics apply (see Dispel System Guide)
- **Centralized Messages:** All duration and dismissal messages use Spell.SpellMessages

---

## 2. Earth Elemental (`Kal Vas Xen Ylem`)

### Description
Summons a powerful Earth Elemental creature to fight alongside the caster. The elemental is a controlled summon that follows the caster's commands. Casters with 100+ Magery and 100+ Inscription have a chance to summon a Greater Earth Elemental (Super Elemental da Terra) instead of a regular one. Only one elemental per caster is allowed - casting another elemental dismisses the previous one.

### Meaning
The spell words "Kal Vas Xen Ylem" translate to "Summon Great Earth Creature" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Mandrake Root, Spider's Silk
- **Mana Cost:** 269
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Duration Formula
```
Duration = (Magery.Fixed × 0.1) × Inscription_Bonus
Base: Magery skill × 0.1 seconds
Bonus: Inscription benefit multiplier (NMSUtils.getBonusIncriptBenefit)
Formula: ((Inscription × 3) / 100) + 1) / 10) + 1
```

### Greater Elemental Chance Formula
```
Requirements: Magery ≥ 100.0 AND Inscription ≥ 100.0
Base Chance: 20%
EvalInt Bonus: +0.2% per EvalInt point
Total Chance: 20% + (EvalInt × 0.2%)
Maximum: 40% (at EvalInt 100)
```

**Examples:**
- Magery 100, Inscription 100, EvalInt 0: **20%** chance
- Magery 100, Inscription 100, EvalInt 50: **30%** chance
- Magery 100, Inscription 100, EvalInt 100: **40%** chance
- Magery 120, Inscription 120, EvalInt 120: **44%** chance

### Creature Names (PT-BR)
- **Regular:** "an earth elemental" (English)
- **Greater:** "Super Elemental da Terra" (PT-BR)
- **Corpse (Regular):** "an earth elemental corpse"
- **Corpse (Greater):** "corpo de Super Elemental da Terra"

### Visual Properties
- **Hue (Regular):** Default (0)
- **Hue (Greater):** Default (0)
- **Body:** 14 (Regular) / 142 (Greater)

### Dispel Resistance
- **DispelDifficulty:** 117.5
- **DispelFocus:** 45.0
- **Base Difficulty:** 16.25
- **Note:** Regular and Greater have identical dispel resistance

### Special Mechanics
- **Controlled Summon:** Follows caster's commands
- **Single Elemental Limit:** Only one elemental per caster (any type: Air, Earth, Fire, Water)
  - Casting a new elemental automatically dismisses the previous one
  - Works across all elemental types (cannot have Earth + Fire simultaneously)
- **Greater Elemental:** Requires Magery ≥ 100 AND Inscription ≥ 100
- **Duration:** Scales with Magery and Inscription skills
- **Can be Dispelled:** Standard dispel mechanics apply (see Dispel System Guide)
- **Centralized Messages:** All duration and dismissal messages use Spell.SpellMessages

---

## 3. Earthquake (`In Vas Por`)

### Description
Area-of-effect ground damage spell that affects all mobiles within a dynamic range based on Magery skill. Damage is calculated once and divided among all targets (similar to Chain Lightning). Creates spectacular ground tremor and particle effects. Can be resisted, and has special interactions with the One Ring. Always reveals hidden targets (except One Ring protection). No house protection - damage applies everywhere.

### Meaning
The spell words "In Vas Por" translate to "Great Summon Stone" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Ginseng, Mandrake Root, Sulfurous Ash
- **Mana Cost:** 233
- **Target:** Self (affects area around caster)
- **Range:** 2-8 tiles (linear progression based on Magery 50-120)
- **Line of Sight:** Required (AOS)

### Damage Formula
```
Base Damage = GetNMSDamage(BASE_DAMAGE_BONUS, DAMAGE_DICE_COUNT, DAMAGE_DICE_SIDES, playerVsPlayer)
Constants:
  - BASE_DAMAGE_BONUS = 37
  - DAMAGE_DICE_COUNT = 1
  - DAMAGE_DICE_SIDES = 6
  - playerVsPlayer = Caster.Player && targetIsPlayer

Base Calculation: 37 + 1d6 = 38-43 base damage (20% more than ChainLightning)

Custom EvalInt Benefit: 0% to 23% (linear progression)
  - MAX_EVALINT_BENEFIT = 0.23 (23%)
  - evalIntBenefit = 1.0 + (EvalInt / 100.0) × MAX_EVALINT_BENEFIT
  - Damage = Math.Floor(Base × evalIntBenefit)

If Multiple Targets: Damage / Target Count
Minimum Damage Floor: 12 (MIN_DAMAGE_FLOOR) - applied after division
If Resisted: × 0.5 (RESIST_DAMAGE_MULTIPLIER)
One Ring Protection: × 0.5 (ONE_RING_DAMAGE_MULTIPLIER) - also prevents reveal
```

### Range Formula
```
Range = Linear interpolation from 2 to 8 tiles
Constants:
  - MIN_RANGE = 2
  - MAX_RANGE = 8
  - MIN_MAGERY_FOR_RANGE = 50.0
  - MAX_MAGERY_FOR_RANGE = 120.0

Formula: MIN_RANGE + ((Magery - MIN_MAGERY_FOR_RANGE) / (MAX_MAGERY_FOR_RANGE - MIN_MAGERY_FOR_RANGE)) × (MAX_RANGE - MIN_RANGE)
Result: Math.Round(range) - rounded to nearest integer

Magery is clamped between 50.0 and 120.0 before calculation
```

### Special Mechanics
- **Damage Splitting:** Total damage divided by number of targets (like ChainLightning)
  - Damage calculated once, then divided by target count
  - Minimum floor applied after division (MIN_DAMAGE_FLOOR = 12)
- **Minimum Floor:** Each target receives at least 12 damage (MIN_DAMAGE_FLOOR)
  - Applied after damage division, before resistance/One Ring calculations
- **Dynamic Range:** Range scales from 2 tiles (Magery 50) to 8 tiles (Magery 120)
  - Uses `CalculateEarthquakeRange()` method
  - Magery clamped between 50.0 and 120.0
  - Result rounded to nearest integer
- **Custom EvalInt:** Uses 0-23% benefit instead of default formula
  - MAX_EVALINT_BENEFIT = 0.23 (23% maximum)
  - Formula: `1.0 + (EvalInt / 100.0) × MAX_EVALINT_BENEFIT`
  - Final damage uses `Math.Floor()` to round down
- **Always Reveals:** Hidden targets are always revealed (except One Ring protection)
  - Calls `m.RevealingAction()` on all targets
  - One Ring prevents reveal (hasOneRing check)
- **No House Protection:** Damage applies everywhere, regardless of region
  - No region checks for damage application
- **Caster Damage:** Caster excluded from targets (`Caster != m` check)
  - Caster cannot damage themselves with Earthquake
- **One Ring:** Reduces damage by 50% and prevents reveal
  - Checks for OneRing on Layer.Ring
  - Sends protection message (Spell.SpellMessages.ONE_RING_PROTECTION_REVEAL)
  - Applies ONE_RING_DAMAGE_MULTIPLIER (0.5)
  - Prevents `RevealingAction()` call
- **Resistance:** Can reduce damage by 50%
  - Uses `CheckResisted(m)` method
  - Applies RESIST_DAMAGE_MULTIPLIER (0.5)
  - Sends resist message (Spell.SpellMessages.RESIST_HALF_DAMAGE_VICTIM)
- **Equal Damage:** Non-player targets use same formula as players (no multiplier)
  - `GetNMSDamage` uses `Caster.Player && playerVsPlayer` parameter
  - Same damage calculation for all target types
- **Target Validation:**
  - Must be in same region as caster (`Caster.Region == m.Region`)
  - Must pass `SpellHelper.ValidIndirectTarget(Caster, m)`
  - Must pass `Caster.CanBeHarmful(m, false)`
  - Line of Sight required in AOS (`Caster.InLOS(m)`)
- **Centralized Messages:** All messages use Spell.SpellMessages constants

### Visual Effects
- **Caster Location:** 
  - Ground tremor particles (EARTHQUAKE_EFFECT_ID = 0x3728)
  - Particle count: 10, speed: 20, duration: 5042
  - Sound: 0x220 (earthquake rumble)
- **Target Locations:** 
  - Ground tremor particles (EARTHQUAKE_EFFECT_ID = 0x3728) - 10 particles, speed 15
  - Tremor effect (EARTHQUAKE_TREMOR_ID = 0x37C4) - FixedEffect, duration 10, speed 20
  - Head particles (EARTHQUAKE_EFFECT_ID = 0x3728) - 10 particles, speed 15, EffectLayer.Head
- **Effect Duration:** EffectItem.DefaultDuration for location particles

### Duration
N/A (instant damage)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Range | Targets | Base Damage | Per Target | Resisted | Final Range | Notes |
|----------|--------|---------|-------|---------|-------------|------------|----------|-------------|-------|
| Novice | 50 | 90 | 2 tiles | 1 | 38-43 | 46-57 | 50% | 23-29 | Minimum range |
| Apprentice | 85 | 100 | 5 tiles | 3 | 38-43 | 15-19 | 50% | 12-12 | Floor applied |
| Adept | 100 | 110 | 6 tiles | 5 | 38-43 | 9-11 | 50% | 12-12 | Floor applied |
| Expert | 120 | 120 | 8 tiles | 2 | 38-43 | 24-30 | 50% | 12-15 | Maximum range |
| Master | 120 | 120 | 8 tiles | 1 | 38-43 | 50-63 | 50% | 25-32 | Maximum single |

### Range Simulation Table

| Magery | Range (tiles) | Notes |
|--------|--------------|-------|
| 50 | 2 | Minimum |
| 60 | 2.86 | ~3 tiles |
| 70 | 3.71 | ~4 tiles |
| 80 | 4.57 | ~5 tiles |
| 90 | 5.43 | ~5 tiles |
| 100 | 6.29 | ~6 tiles |
| 110 | 7.14 | ~7 tiles |
| 120 | 8 | Maximum |

### Curiosities & Easter Eggs
- **20% More Damage:** Base damage is 20% higher than ChainLightning (8th vs 7th circle)
  - ChainLightning: 28 + 2d5 = 30-38 (avg 34)
  - Earthquake: 37 + 1d6 = 38-43 (avg 40.5) ≈ 20% more
- **Custom EvalInt:** Uses 0-23% benefit instead of standard formula
  - MAX_EVALINT_BENEFIT constant = 0.23 (23% maximum)
  - Linear progression from 0% (EvalInt 0) to 23% (EvalInt 100+)
  - Uses `Math.Floor()` for final damage calculation
- **Dynamic Range:** Only spell with skill-based range scaling
  - Range calculated dynamically based on Magery skill
  - Linear interpolation from 2 to 8 tiles
  - Result rounded to nearest integer
- **One Ring Protection:** Reduces damage by 50% and prevents reveal (Lord of the Rings reference)
  - Checks for OneRing item on Layer.Ring
  - Sends protection message to target
  - Prevents reveal action
- **Always Reveals:** No house protection - hidden targets always revealed (except One Ring)
  - Calls `RevealingAction()` on all valid targets
  - One Ring is the only exception
- **Damage Floor:** Ensures minimum 12 damage even with many targets
  - MIN_DAMAGE_FLOOR = 12
  - Applied after damage division
- **Visual Effects:** Spectacular ground tremor and particle effects
  - Multiple particle effects per target
  - Ground tremor at caster location
  - Tremor effect + head particles on targets
- **Sound:** Distinctive earthquake rumble (0x220)
  - Played at caster location when spell is cast
- **Equal Treatment:** Players and non-players use identical damage formula
  - No special multipliers for different target types
  - Same calculation for all mobiles
- **Self-Protection:** Caster cannot damage themselves
  - Explicit check: `Caster != m` in target validation

---

## 4. Energy Vortex (`Vas Corp Por`)

### Description
Summons a WILD and DANGEROUS Energy Vortex creature at target location. The vortex is UNCONTROLLED and will attack the NEAREST target (including the caster!). This is a criminal act that flags the caster as gray. Only one vortex per caster is allowed - casting another vortex dismisses the previous one. The vortex has multiple variants (Energy, Poison, Ice, Fire) with unique abilities. Energy Vortex can resist dispel attempts and retaliate, and has a chance to explode on death.

### Meaning
The spell words "Vas Corp Por" translate to "Great Summon Body Stone" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Black Pearl, Mandrake Root, Nightshade
- **Mana Cost:** 260
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Duration Formula
```
Duration = (Magery.Fixed × 0.05) × EvalInt_Bonus
Base: Magery skill × 0.05 seconds (half of elemental duration)
Bonus: EvalInt benefit multiplier (NMSUtils.getDamageEvalBenefit)
Formula: ((EvalInt × 3) / 100) + 1) / 10) + 1
Example: 120 Magery + 120 EvalInt ≈ 3-6 seconds (shorter than elementals)
```

**Note:** Energy Vortex uses `getDamageEvalBenefit` (EvalInt-based) instead of `getBonusIncriptBenefit` (Inscription-based) used by elementals/daemons. Duration is also half (0.05 vs 0.1 multiplier).

### Variant Selection
The vortex type is randomly selected based on caster's Poisoning skill:

**If Poisoning >= 80:**
- Energy Vortex: 40% chance (Hue 0x707 - purple)
- Poison Vortex: 40% chance
- Ice Vortex: 10% chance
- Fire Vortex: 10% chance

**If Poisoning < 80:**
- Energy Vortex: 50% chance (Hue 0x707 - purple)
- Ice Vortex: 25% chance
- Fire Vortex: 25% chance

### Variant Abilities

#### Energy Vortex (40-50% chance)
- **Hue:** 0x707 (purple/energy)
- **Dispel Resistance:** 20% base + linear scaling with EvalInt (up to 50% cap)
- **Retaliation:** If dispel resisted, casts Energy Bolt on dispeller
- **Death Explosion:** 10-30% chance to explode on death (area damage like Chain Lightning)
- **Messages:** PT-BR messages for explosion and retaliation

#### Poison Vortex (40% chance, requires Poisoning >= 80)
- **Poison Application:** 30% chance to poison on melee hit
- **Poison Type:** Scales with Poisoning skill (Lesser to Deadly)
  - Default: 10% Deadly, 20% Greater, 30% Regular, 40% Lesser
  - Linear scaling: Higher skill = more powerful poisons
- **Visual Effects:** Poison particles on hit

#### Fire Vortex (10-25% chance)
- **Additional Fire Damage:** 30% chance for 8-23 additional fire damage on melee hit
- **Visual Effects:** Fire particles and sound on hit

#### Ice Vortex (10-25% chance)
- **Paralysis:** 30% chance to paralyze on melee hit
- **Paralysis Duration:** 
  - If EvalInt > 0: Linear scaling from 2s (EvalInt 0) to 5s (EvalInt 100)
  - If EvalInt = 0: Random 1-3 seconds
- **Visual Effects:** Ice particles and sound on hit

### Special Mechanics
- **Wild Summon:** Uncontrolled, aggressive, attacks nearest target (including caster)
  - **IsWildSummon:** Set to `true` (bypasses relationship protections in BaseAI)
  - **FightMode:** `FightMode.Closest` (attacks nearest target)
  - **Karma:** -2500 (evil, always hostile)
  - **Perception Range:** 10 tiles
  - **Fight Range:** 1 tile
- **Single Limit:** Only one vortex per caster (new vortex dismisses old one)
  - Uses `Dictionary<Mobile, EnergyVortex>` for tracking
  - Sends dismissal message when old vortex is replaced
- **Criminal Act:** Flags caster as criminal (gray) - `Caster.Criminal = true`
- **Dispel Resistance:** Energy Vortex can resist Dispel/Mass Dispel (20-50% chance)
  - Stores caster's EvalInt in `vortex.CasterEvalInt` for resistance calculation
- **Retaliation:** Energy Vortex casts Energy Bolt on failed dispel attempt
- **Death Explosion:** Energy Vortex has 10-30% chance to explode on death
- **Variant Selection:** Based on caster's Poisoning skill
  - Stores caster's Poisoning in `vortex.CasterPoisoning` for variant selection
- **On-Hit Effects:** Each variant has unique 30% chance effects
- **Duration:** Uses `getDamageEvalBenefit` (EvalInt-based) instead of Inscription-based
- **Centralized Messages:** All messages use Spell.SpellMessages

### Dispel Resistance Formula
```
Base Resistance: 20%
EvalInt Bonus: Linear scaling from 0% to 30% (EvalInt 0-100)
Total: 20% + (EvalInt / 100.0) × 30%
Cap: 50% maximum
```

### Death Explosion Formula
```
Chance: Random 10-30%
Damage: Similar to Chain Lightning (area damage)
Range: Area-of-effect around vortex
No Requirements: Happens regardless of vortex mana/skills
```

### Duration
**Vortex Lifetime:** 3-6 seconds (based on Magery and EvalInt)
- **Shorter than Elementals:** Uses 0.05 multiplier (half of elementals' 0.1)
- **EvalInt-based:** Uses `getDamageEvalBenefit` instead of Inscription-based bonus

### Simulation Scenarios

| Scenario | Magery | EvalInt | Poisoning | Duration | Variant Chance | Notes |
|----------|--------|---------|-----------|----------|----------------|-------|
| Novice | 100 | 90 | 0 | ~2.3s | Energy 50% | No Poison variant |
| Apprentice | 110 | 100 | 50 | ~2.8s | Energy 50% | No Poison variant |
| Adept | 120 | 110 | 80 | ~3.3s | Energy 40%, Poison 40% | Poison variant available |
| Expert | 120 | 120 | 100 | ~3.0s | Energy 40%, Poison 40% | All variants available |
| Master | 120 | 120 | 120 | ~3.0s | Energy 40%, Poison 40% | Maximum chance |

### Curiosities & Easter Eggs
- **Wild and Dangerous:** Attacks anything including the caster
- **Criminal Act:** Always flags caster as gray
- **Single Summon:** Only one vortex per caster (prevents spam)
- **Dispel Resistance:** Energy Vortex can resist dispel attempts
- **Retaliation:** Energy Vortex fights back when dispel fails
- **Death Explosion:** Energy Vortex can explode on death
- **Variant System:** Four different vortex types with unique abilities
- **Skill Requirements:** Poison Vortex requires 80+ Poisoning skill
- **On-Hit Effects:** Each variant has 30% chance for special effects
- **Visual Variety:** Different colors and effects per variant
- **PT-BR Messages:** Creative messages for explosion and retaliation

---

## 5. Fire Elemental (`Kal Vas Xen Flam`)

### Description
Summons a powerful Fire Elemental creature to fight alongside the caster. The elemental is a controlled summon that follows the caster's commands. Casters with 100+ Magery and 100+ Inscription have a chance to summon a Greater Fire Elemental (Super Elemental do Fogo) instead of a regular one. Only one elemental per caster is allowed - casting another elemental dismisses the previous one.

### Meaning
The spell words "Kal Vas Xen Flam" translate to "Summon Great Fire Creature" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Mandrake Root, Spider's Silk, Sulfurous Ash
- **Mana Cost:** 269
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Duration Formula
```
Duration = (Magery.Fixed × 0.1) × Inscription_Bonus
Base: Magery skill × 0.1 seconds
Bonus: Inscription benefit multiplier (NMSUtils.getBonusIncriptBenefit)
Formula: ((Inscription × 3) / 100) + 1) / 10) + 1
```

### Greater Elemental Chance Formula
```
Requirements: Magery ≥ 100.0 AND Inscription ≥ 100.0
Base Chance: 20%
EvalInt Bonus: +0.2% per EvalInt point
Total Chance: 20% + (EvalInt × 0.2%)
Maximum: 40% (at EvalInt 100)
```

**Examples:**
- Magery 100, Inscription 100, EvalInt 0: **20%** chance
- Magery 100, Inscription 100, EvalInt 50: **30%** chance
- Magery 100, Inscription 100, EvalInt 100: **40%** chance
- Magery 120, Inscription 120, EvalInt 120: **44%** chance

### Creature Names (PT-BR)
- **Regular:** "a fire elemental" (English)
- **Greater:** "Super Elemental do Fogo" (PT-BR)
- **Corpse (Regular):** "a fire elemental corpse"
- **Corpse (Greater):** "corpo de Super Elemental do Fogo"

### Visual Properties
- **Hue (Regular):** Default (0)
- **Hue (Greater):** Default (0)
- **Body:** 15 (Regular) / 755 (Greater)

### Dispel Resistance
- **DispelDifficulty:** 117.5
- **DispelFocus:** 45.0
- **Base Difficulty:** 16.25
- **Note:** Regular and Greater have identical dispel resistance

### Special Mechanics
- **Controlled Summon:** Follows caster's commands
- **Single Elemental Limit:** Only one elemental per caster (any type: Air, Earth, Fire, Water)
  - Casting a new elemental automatically dismisses the previous one
  - Works across all elemental types (cannot have Fire + Water simultaneously)
- **Greater Elemental:** Requires Magery ≥ 100 AND Inscription ≥ 100
- **Duration:** Scales with Magery and Inscription skills
- **Can be Dispelled:** Standard dispel mechanics apply (see Dispel System Guide)
- **Centralized Messages:** All duration and dismissal messages use Spell.SpellMessages

---

## 6. Resurrection (`An Corp`)

### Description
Multi-purpose resurrection spell with four distinct modes of operation: Self-cast (SoulOrb creation), Player Resurrection, Pet Resurrection, and Henchman Resurrection. The spell can create a SoulOrb for auto-resurrection, resurrect dead players with gump confirmation, resurrect bonded pets with Veterinary skill requirements, or restore henchman items. Each mode has specific requirements, validations, and mechanics.

### Meaning
The spell words "An Corp" translate to "Body" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Garlic, Ginseng
- **Mana Cost:** 245
- **Target:** Mobile (Player/Pet) or Item (Henchman) or Self
- **Range:** 4 tiles (resurrection) / SpellConstants.GetSpellRange() (targeting)
- **Line of Sight:** Required

### Mode 1: Self-Cast (SoulOrb Creation)

#### Description
When cast on self, creates a SoulOrb that automatically resurrects the player 30 seconds after death. The orb has a chance-based creation system and dynamic expiration timer.

#### Creation Chance Formula
```
Chance = (Magery × 0.3%) + (Inscribe × 0.2%)
Magery Contribution: Magery × 0.003 (0.3% per point)
Inscribe Contribution: Inscribe × 0.002 (0.2% per point)
Total: mageryChance + inscribeChance
Example: 100 Magery + 100 Inscribe = 30% + 20% = 50% chance
Example: 120 Magery + 120 Inscribe = 36% + 24% = 60% chance
Cap: 100% maximum
```

#### Expiration Timer Formula
```
Timer = 30s + (Magery / 10) + (Healing × 0.5)
Base: 30 seconds (SOULORB_BASE_EXPIRATION)
Magery Bonus: +1 second per 10 skill points (magery / 10.0)
Healing Bonus: +0.5 seconds per skill point (healing × 0.5)
Total: SOULORB_BASE_EXPIRATION + (int)(magery / 10.0) + (int)(healing × 0.5)
Example: 120 Magery + 100 Healing = 30 + 12 + 50 = 92 seconds
Example: 100 Magery + 0 Healing = 30 + 10 + 0 = 40 seconds
```

#### SoulOrb Properties
- **Karma-Based Colors:**
  - Karma ≥ 1000: Gold (Hue 1174)
  - Karma ≤ -1000: Red (Hue 1800)
  - Karma -999 to 999: Default (Hue 0)
- **Title Color:** Matches orb hue (gold/red/default)
- **Auto-Resurrection:** Resurrects player 30 seconds after death
- **Expiration:** Orb deletes after timer expires
- **Replacement:** New orb replaces old one (only one orb per player)

#### Restrictions
- Cannot create if `SoulBound == true` (checks `player.SoulBound`)
- Requires `Backpack != null`
- Chance-based creation (can fail - uses `Utility.RandomDouble()`)
- **Replacement:** Existing SoulOrbs are deleted before creating new one (only one orb per player)

### Mode 2: Player Resurrection

#### Description
Resurrects a dead player with gump confirmation. Player can accept or refuse resurrection. Special handling for SoulBound players.

#### Requirements
- Caster must be alive
- Target must be dead
- Range: 4 tiles
- Location must be free (16 tiles)
- Line of Sight: Required

#### Resurrection Values
| Atributo | Valor Padrão | Com Compassion Seeker | Com Compassion Follower | Com Compassion Knight |
|----------|--------------|----------------------|------------------------|----------------------|
| **Hits** | 1 | 20% do HitsMax | 40% do HitsMax | 80% do HitsMax |
| **Stamina** | 1 | 1 | 1 | 1 |
| **Mana** | 1 | 1 | 1 | 1 |
| **Hunger** | 10 | 10 | 10 | 10 |
| **Thirst** | 10 | 10 | 10 | 10 |

#### Special Cases
- **SoulBound Players:** Auto-resurrect without gump (calls `ResetPlayer()`)
- **Compassion Virtue:** Healer's Compassion level affects hits restored

### Mode 3: Pet Resurrection

#### Description
Resurrects bonded and controlled pets with Veterinary skill requirements and chance-based success.

#### Requirements
- **Veterinary:** Minimum 80.0 skill required
- **Pet Must Be:**
  - Bonded (`IsBonded == true`)
  - Controlled (`Controlled == true`)
  - Not Summoned (`Summoned == false`)
  - Has Master (`GetMaster() != null`)
- **Range:** 4 tiles
- **Location:** Must be free (16 tiles)

#### Resurrection Chance Formula
```
Chance = Veterinary × 0.5%
Formula: veterinary × 0.005 (VETERINARY_CHANCE_MULTIPLIER)
Example: 80 Veterinary = 40% chance (minimum)
Example: 100 Veterinary = 50% chance
Example: 120 Veterinary = 60% chance (cap)
Cap: 60% maximum (MAX_PET_RESURRECTION_CHANCE)
```

#### Pet Stats After Resurrection
- **Hits:** 10% of HitsMax (PET_RESURRECTION_STATS_PERCENTAGE = 0.10)
- **Stamina:** 10% of StamMax (PET_RESURRECTION_STATS_PERCENTAGE = 0.10)
- **Mana:** 10% of ManaMax (PET_RESURRECTION_STATS_PERCENTAGE = 0.10)
- **Note:** Stats are restored via PetResurrectGump with 10% scalar

#### Messages
- **Success:** Uses `Spell.SpellMessages.RESURRECTION_PET_SUCCESS` (centralized)
- **Failure:** Uses `Spell.SpellMessages.RESURRECTION_PET_FAILED` (centralized)
- **Veterinary Required:** Uses `Spell.SpellMessages.RESURRECTION_PET_VETERINARY_REQUIRED` (centralized)
- **All messages:** Centralized in Spell.SpellMessages (PT-BR)

### Mode 4: Henchman Resurrection

#### Description
Resurrects henchman items (Fighter, Wizard, Archer, Monster) by resetting their death status.

#### Requirements
- Target must be HenchmanItem
- `HenchDead > 0` (henchman must be dead)
- Valid henchman types: Fighter, Wizard, Archer, Monster

#### Effect
- Sets `HenchDead = 0` (resets death status)
- Restores default name (based on henchman type: "fighter henchman", "wizard henchman", etc.)
- Calls `InvalidateProperties()` (updates item properties)
- Plays resurrection sound (SOUND_RESURRECT = 0x214)
- **Supported Types:** HenchmanFighterItem, HenchmanWizardItem, HenchmanArcherItem, HenchmanMonsterItem

### Duration
N/A (instant effect, except SoulOrb timer)

### Simulation Scenarios

#### SoulOrb Creation
| Scenario | Magery | Inscribe | Healing | Chance | Timer | Notes |
|----------|--------|----------|---------|--------|-------|-------|
| Novice | 50 | 0 | 0 | 15% | 35s | Low chance |
| Apprentice | 100 | 100 | 0 | 50% | 40s | Good chance |
| Adept | 120 | 120 | 100 | 60% | 92s | High chance, long timer |
| Master | 200 | 200 | 200 | 100% | 130s | Guaranteed, maximum timer |

#### Pet Resurrection
| Scenario | Veterinary | Pet Type | Chance | Result | Notes |
|----------|------------|----------|--------|--------|-------|
| Novice | 80 | Bonded | 40% | ✅/❌ | Minimum |
| Apprentice | 100 | Bonded | 50% | ✅/❌ | GM skill |
| Adept | 120 | Bonded | 60% | ✅/❌ | Cap reached |
| Master | 200 | Bonded | 60% | ✅/❌ | Cap (no increase) |

### Curiosities & Easter Eggs
- **Four Modes:** One spell, four completely different functions
- **SoulOrb System:** Unique auto-resurrection mechanic with skill-based chance
- **Karma Colors:** SoulOrb color reflects caster's karma (gold/red/default)
- **Pet Restrictions:** Only bonded, controlled, non-summoned pets
- **Veterinary Requirement:** Pet resurrection requires 80+ Veterinary
- **Chance System:** Both SoulOrb and Pet resurrection use chance mechanics
- **Compassion Bonus:** Healer's Compassion affects player resurrection hits
- **SoulBound Handling:** Special auto-resurrect for SoulBound players
- **PT-BR Messages:** All messages in Portuguese
- **Visual Effects:** Distinctive resurrection particles (0x376A)
- **Sound:** Resurrection sound (0x214)

---

## 7. Summon Daemon (`Kal Vas Xen Corp`)

### Description
Summons a powerful Daemon creature to fight alongside the caster. The daemon is a controlled summon that follows the caster's commands. Casters with 100+ Magery and 100+ Inscription have a chance to summon a Greater Daemon (Super Demônio) instead of a regular one. Only one daemon per caster is allowed - casting another daemon dismisses the previous one. Includes special particle effects on summon.

### Meaning
The spell words "Kal Vas Xen Corp" translate to "Summon Great Body Creature" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Mandrake Root, Spider's Silk, Sulfurous Ash
- **Mana Cost:** 269
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Duration Formula
```
Duration = (Magery.Fixed × 0.1) × Inscription_Bonus
Base: Magery skill × 0.1 seconds
Bonus: Inscription benefit multiplier (NMSUtils.getBonusIncriptBenefit)
Formula: ((Inscription × 3) / 100) + 1) / 10) + 1
```

### Greater Daemon Chance Formula
```
Requirements: Magery ≥ 100.0 AND Inscription ≥ 100.0
Base Chance: 20%
EvalInt Bonus: +0.2% per EvalInt point
Total Chance: 20% + (EvalInt × 0.2%)
Maximum: 40% (at EvalInt 100)
```

**Examples:**
- Magery 100, Inscription 100, EvalInt 0: **20%** chance
- Magery 100, Inscription 100, EvalInt 50: **30%** chance
- Magery 100, Inscription 100, EvalInt 100: **40%** chance
- Magery 120, Inscription 120, EvalInt 120: **44%** chance

### Creature Names (PT-BR)
- **Regular:** Random name from "daemon" list + Title "o demônio"
- **Greater:** Random name from "devil" list + Title "o balron" (Name: "Super Demônio")
- **Corpse (Regular):** "corpo de um demônio"
- **Corpse (Greater):** "corpo de Demônio"

### Visual Properties
- **Hue (Regular):** Default (0)
- **Hue (Greater):** 1194 (red/demonic)
- **Body:** 9 (Regular) / 427 (Greater - Balron body)
- **Special Particles:** Unique particle effects on summon (0x3728)

### Dispel Resistance
- **DispelDifficulty:** 125.0
- **DispelFocus:** 45.0
- **Base Difficulty:** 17.0
- **Note:** Regular and Greater have identical dispel resistance. Daemons are slightly harder to dispel than Elementals (+0.75 base difficulty)

### Special Mechanics
- **Controlled Summon:** Follows caster's commands
- **Single Daemon Limit:** Only one daemon per caster
  - Casting a new daemon automatically dismisses the previous one
  - Independent from elemental limit (can have 1 daemon + 1 elemental)
- **Greater Daemon:** Requires Magery ≥ 100 AND Inscription ≥ 100
- **Duration:** Scales with Magery and Inscription skills
- **Special Particles:** Unique visual effects applied on summon (particle effect 0x3728)
- **Can be Dispelled:** Standard dispel mechanics apply (see Dispel System Guide)
- **Centralized Messages:** All duration and dismissal messages use Spell.SpellMessages

---

## 8. Water Elemental (`Kal Vas Xen An Flam`)

### Description
Summons a powerful Water Elemental creature to fight alongside the caster. The elemental is a controlled summon that follows the caster's commands. Casters with 100+ Magery and 100+ Inscription have a chance to summon a Greater Water Elemental (Super Elemental da Água) instead of a regular one. Only one elemental per caster is allowed - casting another elemental dismisses the previous one.

### Meaning
The spell words "Kal Vas Xen An Flam" translate to "Summon Great Water Fire Creature" in the magical language.

### Requirements
- **Minimum Magery:** 100.0
- **Reagents:** Bloodmoss, Mandrake Root, Spider's Silk
- **Mana Cost:** 269
- **Target:** Ground location (IPoint3D)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)

### Duration Formula
```
Duration = (Magery.Fixed × 0.1) × Inscription_Bonus
Base: Magery skill × 0.1 seconds
Bonus: Inscription benefit multiplier (NMSUtils.getBonusIncriptBenefit)
Formula: ((Inscription × 3) / 100) + 1) / 10) + 1
```

### Greater Elemental Chance Formula
```
Requirements: Magery ≥ 100.0 AND Inscription ≥ 100.0
Base Chance: 20%
EvalInt Bonus: +0.2% per EvalInt point
Total Chance: 20% + (EvalInt × 0.2%)
Maximum: 40% (at EvalInt 100)
```

**Examples:**
- Magery 100, Inscription 100, EvalInt 0: **20%** chance
- Magery 100, Inscription 100, EvalInt 50: **30%** chance
- Magery 100, Inscription 100, EvalInt 100: **40%** chance
- Magery 120, Inscription 120, EvalInt 120: **44%** chance

### Creature Names (PT-BR)
- **Regular:** "a water elemental" (English)
- **Greater:** "Super Elemental da Água" (PT-BR)
- **Corpse (Regular):** "a water elemental corpse"
- **Corpse (Greater):** "corpo de Super Elemental da Água"

### Visual Properties
- **Hue (Regular):** Default (0)
- **Hue (Greater):** Default (0)
- **Body:** 16 (Regular) / 707 (Greater)
- **Special:** Greater Water Elemental can swim (CanSwim = true)

### Dispel Resistance
- **DispelDifficulty:** 117.5
- **DispelFocus:** 45.0
- **Base Difficulty:** 16.25
- **Note:** Regular and Greater have identical dispel resistance

### Special Mechanics
- **Controlled Summon:** Follows caster's commands
- **Single Elemental Limit:** Only one elemental per caster (any type: Air, Earth, Fire, Water)
  - Casting a new elemental automatically dismisses the previous one
  - Works across all elemental types (cannot have Water + Air simultaneously)
- **Greater Elemental:** Requires Magery ≥ 100 AND Inscription ≥ 100
- **Duration:** Scales with Magery and Inscription skills
- **Can be Dispelled:** Standard dispel mechanics apply (see Dispel System Guide)
- **Centralized Messages:** All duration and dismissal messages use Spell.SpellMessages

---

## Common Patterns

### Area-of-Effect Damage Spells
The 8th circle features the most powerful AoE damage spell:
- **Earthquake:** Ground-based damage with dynamic range scaling

### Summon Spells
Advanced summoning magic:
- **Energy Vortex:** Wild uncontrolled summon (unique)
- **Elemental Summons:** Air, Earth, Fire, Water (controlled)
  - **Single Limit:** Only one elemental per caster (any type)
  - **Greater Chance:** 20% base + (EvalInt × 0.2%) - requires Magery 100+ AND Inscription 100+
  - **Duration:** Scales with Magery and Inscription
  - **Dispel Resistance:** 16.25 base difficulty (all types, regular and greater)
- **Summon Daemon:** Powerful demonic summon
  - **Single Limit:** Only one daemon per caster
  - **Greater Chance:** 20% base + (EvalInt × 0.2%) - requires Magery 100+ AND Inscription 100+
  - **Duration:** Scales with Magery and Inscription
  - **Dispel Resistance:** 17.0 base difficulty (slightly harder than elementals)
  - **Special Particles:** Unique visual effects on summon

### Utility Spells
Life-saving and support magic:
- **Resurrection:** Multi-mode resurrection system

---

## Strategic Applications

### PvP Strategies
1. **Earthquake:** Area damage with dynamic range (2-8 tiles based on skill)
2. **Energy Vortex:** Wild summon for area denial (dangerous but effective)
3. **Resurrection:** Support teammates or create SoulOrb for safety
4. **Elemental Summons:** Controlled fighters for combat support

### PvM Strategies
1. **Earthquake:** Area damage for large groups of monsters
2. **Energy Vortex:** Wild summon for crowd control (use carefully)
3. **Resurrection:** Support for group play
4. **Elemental Summons:** Tank and DPS support
5. **SoulOrb:** Auto-resurrection insurance for dangerous areas

### Group Play
1. **Resurrection:** Essential support for group survival
   - **SoulOrb:** Pre-cast before dangerous encounters
   - **Player Resurrection:** Support fallen teammates
   - **Pet Resurrection:** Restore bonded pets (requires Veterinary)
2. **Earthquake:** Area damage for team fights
3. **Energy Vortex:** Area denial (use with caution - attacks everyone)
4. **Elemental Summons:** Additional fighters for group

### Solo Play
1. **SoulOrb:** Essential for solo exploration
   - Maximize Magery and Inscribe for better creation chance
   - Maximize Healing for longer timer
2. **Earthquake:** Powerful area damage for solo PvM
3. **Energy Vortex:** Use carefully - can attack you
4. **Elemental Summons:** Combat support

---

## Skill Requirements Summary

### Core Skills
- **Magery:** 100.0+ (all spells)
- **EvalInt:** Affects damage, duration, and special mechanics

### Supporting Skills
- **Inscribe:** 
  - Affects SoulOrb creation chance (Resurrection)
  - **Required for Greater Elemental/Daemon chance** (100.0+ required)
  - **Affects summon duration** (bonus multiplier for all elemental/daemon summons)
- **Healing:** Affects SoulOrb expiration timer (Resurrection)
- **Veterinary:** Required for Pet Resurrection (80.0+ minimum)
- **Poisoning:** Unlocks Poison Vortex variant (80.0+ required)
- **EvalInt:** 
  - Affects Greater Elemental/Daemon chance (+0.2% per point)
  - Affects damage and duration for various spells

### Skill Synergies
- **Magery + EvalInt:** Core damage and duration scaling
- **Magery + Inscribe:** 
  - SoulOrb creation chance
  - **Greater Elemental/Daemon chance requirement** (both must be 100+)
  - **Summon duration bonus** (all elemental/daemon summons)
- **Magery + Inscription + EvalInt:** Maximum Greater summon chance (20% base + EvalInt bonus)
- **Magery + Healing:** SoulOrb expiration timer
- **Veterinary:** Pet resurrection capability
- **Poisoning:** Energy Vortex variant selection

---

## Summary

The 8th Circle represents the pinnacle of magical power:
- **Damage:** Most powerful area damage (Earthquake with 30% more than Chain Lightning)
- **Summoning:** Advanced summons including wild uncontrolled creatures (Energy Vortex)
- **Utility:** Life-saving resurrection magic with multiple modes
- **Complexity:** Advanced mechanics (chance systems, skill requirements, variant selection)
- **Strategy:** Requires tactical thinking, skill investment, and risk management

These spells define grandmaster-level gameplay and separate true masters from experts. Proper use of 8th circle magic combined with skill development creates legendary mages capable of dominating both PvP and PvM encounters while providing essential support capabilities.

### Key Features
- **Earthquake:** Dynamic range scaling, damage splitting, 20% more damage than Chain Lightning
- **Energy Vortex:** Wild summon with variants, dispel resistance, retaliation, death explosion
- **Resurrection:** Four modes (SoulOrb, Player, Pet, Henchman) with skill-based mechanics
- **SoulOrb:** Chance-based creation, dynamic timer, karma-based colors
- **Pet Resurrection:** Veterinary requirement, chance-based, bonded-only restriction

