# Magery 1st Circle Spells - Complete Guide

## Overview

The 1st Circle of Magery represents the foundation of magical knowledge. These spells are accessible to beginners and provide essential utility, healing, and basic offensive capabilities. All spells in this circle require minimal skill (0.0 Magery) and serve as the building blocks for more advanced magic.

**Circle Level:** Beginner  
**Mana Cost:** 4  
**Difficulty:** 0-20 skill  
**Total Spells:** 8

---

## Spell List

1. **Clumsy (`Uus Jux`)** - Curse/Debuff
2. **Create Food (`In Mani Ylem`)** - Utility
3. **Feeblemind (`Rel Wis`)** - Curse/Debuff
4. **Heal (`In Mani`)** - Beneficial/Healing
5. **Magic Arrow (`In Por Ylem`)** - Attack/Damage
6. **Night Sight (`In Lor`)** - Utility/Buff
7. **Reactive Armor (`Flam Sanct`)** - Defensive Buff
8. **Weaken (`Des Mani`)** - Curse/Debuff

---

## 1. Clumsy (`Uus Jux`)

### Description
Reduces the target's Dexterity temporarily, making them slower and less agile. This curse affects movement speed, weapon swing speed, and overall dexterity-based actions.

### Meaning
The spell words "Uus Jux" translate to "Decrease Dexterity" in the magical language.

### Requirements
- **Minimum Magery:** 0.0
- **Reagents:** Bloodmoss, Nightshade
- **Mana Cost:** 4
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Reduction Formula
```
Stat Reduction = Target's RawDex × GetOffsetScalar(Caster, Target, true)
GetOffsetScalar = (6 + (EvalInt / 100) - (Target's MagicResist / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)
```

**Minimum Reduction:** 1% of target's stat

### Duration Formula
```
Base Duration = Random(10-30) seconds
Duration Bonus = Ceiling(EvalInt × 0.25) + Random bonus based on EvalInt tier
Final Duration = Base + Bonus (in seconds)
```

**EvalInt Bonus Tiers:**
- 120+: +20-40 seconds
- 100-119: +18-30 seconds
- 80-99: +16-25 seconds
- 60-79: +10-20 seconds
- Below 60: Base only

### Simulation Scenarios

| Scenario | Magery | EvalInt | Target Resist | Reduction % | Duration (sec) |
|----------|--------|---------|---------------|--------------|--------------|
| Novice | 20 | 20 | 20 | ~6% | 15-25 |
| Apprentice | 50 | 40 | 30 | ~8% | 20-35 |
| Adept | 80 | 70 | 50 | ~10% | 30-50 |
| Expert | 100 | 90 | 70 | ~12% | 35-60 |
| Master | 120 | 120 | 100 | ~15% | 40-70 |

### Curiosities & Easter Eggs
- **Sorcerer Immunity:** Sorcerers wearing `SkirtOfPower` are immune to Clumsy (but not Weaken)
- **Paralysis Removal:** Casting Clumsy removes paralysis from the target
- **Spell Interruption:** If target is casting, their spell is interrupted
- **Reflection:** Can be reflected back at the caster via Magic Reflect

---

## 2. Create Food (`In Mani Ylem`)

### Description
Conjures food items directly into the caster's backpack. Essential for survival and exploration, especially in remote areas where food is scarce.

### Meaning
The spell words "In Mani Ylem" translate to "Create Food" in the magical language.

### Requirements
- **Minimum Magery:** 0.0
- **Reagents:** Garlic, Ginseng, Mandrake Root
- **Mana Cost:** 4
- **Target:** Self (no targeting required)
- **Backpack Space:** Required

### Food Creation
- **Food Types:** Random selection from 11 food types:
  - Stale Bread, Grapes, Ham, Cheese Wedge, Muffins
  - Fish Steak, Ribs, Cooked Bird, Sausage, Apple, Peach
- **Quantity:** 1 food item per cast
- **Water Flask Bonus:** 30% chance to also create a Water Flask

### Simulation Scenarios

| Scenario | Magery | Food Created | Water Flask % | Notes |
|----------|--------|--------------|---------------|-------|
| Novice | 20 | 1 item | 30% | Basic survival |
| Apprentice | 50 | 1 item | 30% | Reliable food source |
| Adept | 80 | 1 item | 30% | Consistent supply |
| Expert | 100 | 1 item | 30% | Same as all levels |
| Master | 120 | 1 item | 30% | No skill scaling |

### Curiosities & Easter Eggs
- **No Skill Scaling:** Food creation doesn't improve with skill level (balance decision)
- **Water Flask Bonus:** The 30% chance for water flask is independent of skill
- **Can Cast in Towns:** Unlike some utility spells, this can be cast anywhere
- **No Cooldown:** Can be spammed for unlimited food (mana permitting)

---

## 3. Feeblemind (`Rel Wis`)

### Description
Reduces the target's Intelligence temporarily, affecting their mana pool, spellcasting ability, and intelligence-based skills.

### Meaning
The spell words "Rel Wis" translate to "Reduce Wisdom/Intelligence" in the magical language.

### Requirements
- **Minimum Magery:** 0.0
- **Reagents:** Nightshade, Ginseng
- **Mana Cost:** 4
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Reduction Formula
```
Stat Reduction = Target's RawInt × GetOffsetScalar(Caster, Target, true)
GetOffsetScalar = (6 + (EvalInt / 100) - (Target's MagicResist / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)
```

### Duration Formula
Same as Clumsy (see above).

### Simulation Scenarios

| Scenario | Magery | EvalInt | Target Resist | Reduction % | Duration (sec) |
|----------|--------|---------|---------------|--------------|----------------|
| Novice | 20 | 20 | 20 | ~6% | 15-25 |
| Apprentice | 50 | 40 | 30 | ~8% | 20-35 |
| Adept | 80 | 70 | 50 | ~10% | 30-50 |
| Expert | 100 | 90 | 70 | ~12% | 35-60 |
| Master | 120 | 120 | 100 | ~15% | 40-70 |

### Curiosities & Easter Eggs
- **Sorcerer Immunity:** Sorcerers wearing `SkirtOfPower` are immune to Feeblemind
- **Mana Reduction:** Affects target's current and maximum mana pool
- **Spell Interruption:** Interrupts target's spellcasting
- **Paralysis Removal:** Removes paralysis as a side effect

---

## 4. Heal (`In Mani`)

### Description
Restores hit points to the target. The most essential healing spell for any mage, providing reliable health restoration for yourself and allies.

### Meaning
The spell words "In Mani" translate to "Heal" in the magical language.

### Requirements
- **Minimum Magery:** 10.0 (special requirement)
- **Reagents:** Garlic, Ginseng, Spider's Silk
- **Mana Cost:** 4
- **Target:** Single target (Mobile)
- **Range:** 11 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Healing Formula
```
Step 1: Base Calculation
  BeneficialMageryInscribePercentage = (Magery × Influence) / 3
  where Influence = 1 + (Inscribe / 3) / 100
  Base Heal = BeneficialMageryInscribePercentage / 3

Step 2: Skill Bonuses
  Inscription Bonus = Ceiling((Inscribe / 10) × 0.3)
  Healing Bonus = (Healing / 10) × 1

Step 3: Apply Modifiers
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
| Novice | 50 | 50 | 50 | 6-8 HP | 7-9 HP | Basic healing |
| Apprentice | 70 | 60 | 60 | 8-11 HP | 9-13 HP | Improved |
| Adept | 90 | 80 | 70 | 12-16 HP | 14-18 HP | Reliable |
| Expert | 100 | 100 | 80 | 14-19 HP | 16-22 HP | Strong |
| Master | 120 | 120 | 100 | 18-25 HP | 21-29 HP | Maximum |

*Values shown are approximate ranges after all modifiers*

### Curiosities & Easter Eggs
- **One Ring Block:** The One Ring item prevents healing (Lord of the Rings reference)
- **Cannot Heal Golems:** Golems are immune to magical healing
- **Deadly Poison Block:** Cannot heal targets with Level 3+ poison or Mortal Strike
- **Consecutive Cast Penalty:** Spamming heals within 2 seconds reduces effectiveness by 25%
- **Self-Heal Bonus:** You know your body better - 5% bonus when healing yourself
- **Overhead Display:** Shows healed amount on target with color-coded message

---

## 5. Magic Arrow (`In Por Ylem`)

### Description
Fires a magical projectile at the target, dealing fire damage. The first offensive spell available to mages, providing reliable ranged damage.

### Meaning
The spell words "In Por Ylem" translate to "Fire Arrow" in the magical language.

### Requirements
- **Minimum Magery:** 0.0
- **Reagents:** Sulfurous Ash
- **Mana Cost:** 4
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Damage Formula
```
Base Damage = Dice(1, 3) + 2 = 3-5 base
Final Damage = Base × EvalInt Benefit
Damage Cap = 8 (maximum damage regardless of skill)
```

**EvalInt Benefit Calculation:**
- Based on EvalInt skill and Inscription
- Scales damage multiplier (typically 1.0-1.5x)

### Duration
N/A (instant damage, delayed projectile)

### Simulation Scenarios

| Scenario | Magery | EvalInt | Base Damage | Final Damage | Notes |
|----------|--------|---------|-------------|--------------|-------|
| Novice | 20 | 20 | 3-5 | 3-5 | Low damage |
| Apprentice | 50 | 40 | 3-5 | 4-6 | 4-6 | Moderate |
| Adept | 80 | 70 | 3-5 | 5-7 | Good |
| Expert | 100 | 90 | 3-5 | 6-8 | Strong |
| Master | 120 | 120 | 3-5 | 6-8 | Capped at 8 |

### Curiosities & Easter Eggs
- **Damage Cap:** Hard-capped at 8 damage for balance (prevents overpowered low-level spell)
- **100% Fire Damage:** All damage is fire type (no physical component)
- **Delayed Damage:** Projectile travels to target (can be dodged if target moves)
- **Reflection:** Can be reflected back at caster via Magic Reflect
- **Visual Effect:** Customizable spell hue affects projectile appearance

---

## 6. Night Sight (`In Lor`)

### Description
Grants temporary night vision with enhanced sensory abilities. Basic cast provides light vision. Re-casting on yourself activates **Sense Mode** - an advanced detection system that allows sensing valuables, traps, auras, and hidden dangers. Duration scales with Magery and Inscription skills.

### Meaning
The spell words "In Lor" translate to "Light" or "Illuminate" in the magical language.

### Requirements
- **Minimum Magery:** 0.0
- **Reagents:** Spider's Silk, Sulfurous Ash
- **Mana Cost:** 4
- **Target:** Single target (Mobile, can target self or others)
- **Range:** 12 tiles
- **Line of Sight:** Required

### Light Level Formula
```
Light Level = DungeonLevel × (Magery / 100)
Minimum Light Level = 0
```

**DungeonLevel:** Based on current location (dungeon = higher level, surface = lower)

### Duration Formula
```
Duration = NMSGetDuration(caster, target, beneficial=true)
Typically: 25-55 seconds based on Magery + Inscription
```

### Sense Mode (Easter Egg Feature)

**Activation:** Re-cast Night Sight on yourself while already under its effect.

#### Sense Mode Features

| Feature | Trigger | Success Rate | Skill Bonuses | Description |
|---------|---------|--------------|---------------|-------------|
| **Wealth Sense** | Target Container | 0.2% per Magery | +0.1% per Taste ID | Detects gold, jewelry, gems |
| **Trap Awareness** | Target Container | Always (if trapped) | - | Warns of traps |
| **Aura Sense** | Target Mobile | 0.3% per Magery | +0.2% per Spirit Speak | Detects active buffs/debuffs |
| **Danger Sense** | Passive (every 5s) | 0.2% per Magery | +0.1% per Forensics | Detects hidden entities within 3 tiles |

#### Sense Mode Visual Feedback
- **Activation:** Blue particle burst + sound + buff icon
- **Periodic:** Subtle blue glow every 4 seconds
- **Cursor:** Blue targeting cursor (beneficial flag)
- **Buff Icon:** Shows "Modo Sensitivo Ativo"
- **Trap Detection:** Warning sound (0x1F5)

#### Sense Mode Messages
- **Wealth (Valuable):** Green message + item count
- **Wealth (Empty/Nothing):** Gray message
- **Trap:** Orange warning message
- **Aura:** Cyan message listing buffs
- **Danger:** Orange warning of hidden presence
- **Failed Sense:** Orange "Você falhou em sentir isto."
- **Invalid Target:** Orange "Você não pode sentir este item."

### Simulation Scenarios

#### Light Effect

| Scenario | Magery | Inscription | Duration (s) | Surface Light | Dungeon Light | Notes |
|----------|--------|-------------|--------------|---------------|---------------|-------|
| Novice | 20 | 0 | 25-35 | 0-1 | 1-2 | Minimal |
| Apprentice | 50 | 30 | 30-40 | 0-2 | 2-3 | Basic |
| Adept | 80 | 60 | 35-45 | 0-3 | 3-4 | Good |
| Expert | 100 | 80 | 40-50 | 0-4 | 4-5 | Excellent |
| Master | 120 | 120 | 45-55 | 0-5 | 5-6 | Maximum |

#### Sense Mode Detection Chances

| Magery | Taste ID | Wealth | Forensics | Danger | Spirit Speak | Aura |
|--------|----------|--------|-----------|--------|--------------|------|
| 50 | 50 | 15% | 50 | 15% | 50 | 25% |
| 75 | 75 | 23% | 75 | 23% | 75 | 37% |
| 100 | 100 | 30% | 100 | 30% | 100 | 50% |
| 120 | 120 | 36% | 120 | 36% | 120 | 60% |

### Curiosities & Easter Eggs
- **Sense Mode Only for Caster:** Friends get light, not sense abilities
- **Blue Cursor:** Distinctive beneficial targeting in Sense Mode
- **Periodic Ambient Effects:** Subtle glow shows Sense Mode active
- **Buff Icon:** Visual indicator in buff bar
- **Multi-Skill Synergy:** Benefits from 4 different skills
- **Passive Danger Sense:** Auto-checks every 5 seconds
- **Trap Warning Sound:** Audio cue for trapped containers
- **Duration Scaling:** Uses centralized NMSGetDuration
- **Auto-Dismiss Cursor:** Cursor removed when spell expires
- **Thread-Safe Timers:** Proper cleanup and tracking

---

## 7. Reactive Armor (`Flam Sanct`)

### Description
Provides physical damage resistance or damage absorption depending on game mode. A defensive buff that reduces incoming physical damage.

### Meaning
The spell words "Flam Sanct" translate to "Flame Protection" or "Sacred Flame" in the magical language.

### Requirements
- **Minimum Magery:** 0.0
- **Reagents:** Garlic, Spider's Silk, Sulfurous Ash
- **Mana Cost:** 4
- **Target:** Self only
- **Toggle Spell:** Cast again to remove

### AOS Mode (Age of Shadows)
```
Physical Resistance = 50 (default) or 10 (Sorcerer)
Duration = 15 seconds (fixed)
```

### Legacy Mode (Pre-AOS)
```
Damage Absorption = (Magery + Meditation + Inscribe) / 3
Minimum = 1
Maximum = 75
Duration = Until removed or caster dies
```

### Duration Formula
- **AOS:** Fixed 15 seconds
- **Legacy:** Permanent until removed

### Simulation Scenarios

#### AOS Mode
| Scenario | Magery | Physical Resist | Duration | Notes |
|----------|--------|-----------------|----------|-------|
| Novice | 20 | 50 | 15s | Standard |
| Apprentice | 50 | 50 | 15s | Standard |
| Adept | 80 | 50 | 15s | Standard |
| Expert | 100 | 50 | 15s | Standard |
| Master (Sorcerer) | 120 | 10 | 15s | Lower for Sorcerers |

#### Legacy Mode
| Scenario | Magery | Meditation | Inscribe | Absorption | Duration |
|----------|--------|------------|----------|------------|----------|
| Novice | 20 | 20 | 20 | 20 | Permanent |
| Apprentice | 50 | 40 | 30 | 40 | Permanent |
| Adept | 80 | 70 | 60 | 70 | Permanent |
| Expert | 100 | 90 | 80 | 87 | Permanent |
| Master | 120 | 100 | 100 | 75 (capped) | Permanent |

### Curiosities & Easter Eggs
- **Sorcerer Penalty:** Sorcerers get only 10 physical resist in AOS mode (balance nerf)
- **Toggle Spell:** Cast again to remove the effect
- **Legacy Mode:** Much more powerful in pre-AOS (permanent, higher values)
- **Cannot Stack:** Only one Reactive Armor can be active at a time

---

## 8. Weaken (`Des Mani`)

### Description
Reduces the target's Strength temporarily, making them weaker and less capable of dealing physical damage or carrying heavy loads.

### Meaning
The spell words "Des Mani" translate to "Decrease Strength" in the magical language.

### Requirements
- **Minimum Magery:** 0.0
- **Reagents:** Garlic, Nightshade
- **Mana Cost:** 4
- **Target:** Single target (Mobile)
- **Range:** 10 tiles (ML) / 12 tiles (Legacy)
- **Line of Sight:** Required

### Stat Reduction Formula
```
Stat Reduction = Target's RawStr × GetOffsetScalar(Caster, Target, true)
GetOffsetScalar = (6 + (EvalInt / 100) - (Target's MagicResist / 100)) × 0.01 × 0.6 (or 0.8 if Inscribe ≥ 120)
```

### Duration Formula
Same as Clumsy (see above).

### Simulation Scenarios

| Scenario | Magery | EvalInt | Target Resist | Reduction % | Duration (sec) |
|----------|--------|---------|---------------|--------------|----------------|
| Novice | 20 | 20 | 20 | ~6% | 15-25 |
| Apprentice | 50 | 40 | 30 | ~8% | 20-35 |
| Adept | 80 | 70 | 50 | ~10% | 30-50 |
| Expert | 100 | 90 | 70 | ~12% | 35-60 |
| Master | 120 | 120 | 100 | ~15% | 40-70 |

### Curiosities & Easter Eggs
- **No Sorcerer Immunity:** Unlike Clumsy and Feeblemind, Weaken affects Sorcerers (by design)
- **Damage Reduction:** Reduces target's melee damage output
- **Weight Capacity:** Affects how much the target can carry
- **Spell Interruption:** Interrupts target's spellcasting
- **Paralysis Removal:** Removes paralysis as a side effect

---

## Common Patterns

### Duration System
All curse/buff spells use `NMSGetDuration()`:
- **Beneficial Spells:** Use Inscription skill for duration bonus
- **Harmful Spells:** Use EvalInt skill for duration bonus
- **Base Duration:** Random(10-30) seconds
- **Final Duration:** Base + Skill Bonus (with 30% reduction for beneficial spells, capped 15-90 seconds)

### Reflection System
Harmful spells can be reflected:
- **Magic Reflect:** Can bounce spells back at caster
- **Check:** `SpellHelper.CheckReflect()` or `SpellHelper.NMSCheckReflect()`

### Spell Interruption
Many spells interrupt target's casting:
- **OnCasterHurt:** Called when target is hit during spellcasting
- **Prevents Completion:** Target's spell is cancelled

### Custom Spell Hues
All spells support custom coloring:
- **Character Database:** `CharacterDatabase.GetMySpellHue(Caster, 0)`
- **Visual Customization:** Players can customize spell appearance

---

## Strategic Applications

### Beginner Strategies
1. **Heal + Create Food:** Essential survival combo
2. **Night Sight:** Required for dungeon exploration
3. **Magic Arrow:** First offensive option
4. **Reactive Armor:** Early defensive buff

### PvP Applications
1. **Curse Combo:** Clumsy → Weaken → Feeblemind for maximum debuff
2. **Magic Arrow Spam:** Low mana cost allows sustained damage
3. **Heal Timing:** Avoid consecutive cast penalty (wait 2+ seconds)

### PvM Applications
1. **Heal Rotation:** Primary healing for low-level content
2. **Curse Stacking:** Weaken enemies before engaging
3. **Utility Spells:** Create Food and Night Sight for exploration

---

## Summary

The 1st Circle provides essential tools for any mage:
- **Healing:** Heal spell for health restoration
- **Offense:** Magic Arrow for ranged damage
- **Defense:** Reactive Armor for damage reduction
- **Utility:** Create Food and Night Sight for exploration
- **Debuffs:** Clumsy, Weaken, Feeblemind for weakening enemies

These spells form the foundation of magical knowledge and remain useful throughout a mage's career, especially Heal and Night Sight which see constant use even at higher levels.

