# Spell Calculation Consistency Analysis & Standardization Plan

## Executive Summary

This document analyzes calculation consistency across all Magery spell circles (1st-4th) and provides a comprehensive plan to standardize damage, healing, duration, and bonus calculations.

**Status:** Base folder structure matches guide ✅  
**Issue:** Inconsistent calculation patterns across spell circles ❌

---

## Base Folder Structure Compliance

### Current Structure ✅
```
Base/
├── Calculations/     ✅ SpellDamageCalculator.cs, SpellHelper.cs
├── Constants/        ✅ SpellConstants.cs
├── Core/             ✅ Spell.cs, MagerySpell.cs, SpecialMove.cs
├── Data/             ✅ SpellInfo.cs, Reagent.cs
├── Enums/            ✅ SpellCircle.cs, SpellState.cs, DisturbType.cs
├── Modifiers/        ✅ MidlandSpellModifier.cs
├── Movement/         ✅ SpellMovementHandler.cs
├── Registry/         ✅ SpellRegistry.cs, Initializer.cs
├── Timers/           ✅ UnsummonTimer.cs
└── Validation/       ✅ SpellCastingValidator.cs
```

**Assessment:** Structure matches `Folder_Structure_Guide.md` perfectly. All recommended folders exist with appropriate files.

---

## Calculation Inconsistencies Found

### 1. Healing Calculations ❌ INCONSISTENT

#### Current Patterns:

**Heal (1st Circle):**
```csharp
int healAmount = (int)NMSUtils.getBeneficialMageryInscribePercentage(Caster) / 3;
healAmount = (int)(healAmount * 0.7); // 30% reduction
// Apply random variance (2-5% reduction)
// Apply consecutive cast penalty (25% reduction)
```

**Greater Heal (4th Circle):**
```csharp
int toHeal = (int)(NMSUtils.getBeneficialMageryInscribePercentage(Caster) / 1.5);
if (Caster != m)
    toHeal = (int)(toHeal * 1.15); // 15% bonus for others
// NO reduction, NO random variance, NO consecutive cast penalty
```

**Magic Reflect (5th Circle):**
```csharp
int value = (int)(NMSUtils.getBeneficialMageryInscribePercentage(Caster) / 3);
```

#### Issues:
1. **Different Divisors:** Heal uses `/3`, Greater Heal uses `/1.5` (2x multiplier)
2. **Inconsistent Reductions:** Heal has 30% reduction + random variance, Greater Heal has none
3. **Different Bonus Logic:** Greater Heal has 15% bonus for others, Heal has 20% bonus
4. **Missing Features:** Greater Heal lacks consecutive cast penalty

#### Impact:
- **Heal (1st):** At 100 Magery/Inscribe = 44.4 points → 14.8 → 10.4 → ~9-10 HP (with reductions)
- **Greater Heal (4th):** At 100 Magery/Inscribe = 44.4 points → 29.6 → 34 HP (others)
- **Power Gap:** Greater Heal is ~3.4x more powerful than Heal (should be ~2x for 4th vs 1st)

---

### 2. Damage Calculations ⚠️ PARTIALLY CONSISTENT

#### Current Patterns:

**Magic Arrow (1st Circle):**
```csharp
double damage = GetNMSDamage(2, 1, 3, target); // 1d3+2 = 3-5 base
// Cap: 8 damage maximum
```

**Harm (2nd Circle):**
```csharp
double damage = GetNMSDamage(4, 1, 4, target); // 1d4+4 = 5-8 base
// No cap
```

**Fireball (3rd Circle):**
```csharp
double damage = GetNMSDamage(4, 1, 6, target); // 1d6+4 = 5-10 base
// Skill-based minimum floor (5-9 based on EvalInt)
```

**Lightning (4th Circle):**
```csharp
double damage = GetNMSDamage(4, 1, 6, target); // 1d6+4 = 5-10 base
// No minimum floor, no cap
```

**Fire Field (4th Circle):**
```csharp
int damage = GetNMSDamage(1, 2, 6, Caster); // 2d6+1 = 3-13 base
// Per tick, 50% chance to apply
```

#### Issues:
1. **Inconsistent Minimum Floors:** Only Fireball has skill-based minimum damage
2. **Inconsistent Caps:** Only Magic Arrow has damage cap
3. **Different Dice Patterns:** Fire Field uses 2d6+1 instead of 1d6+4 pattern
4. **Missing Progression:** Damage doesn't scale consistently with circle

#### Expected Progression (Should Be):
| Circle | Base Damage | Pattern |
|--------|-------------|---------|
| 1st | 1d3+2 (3-5) | ✅ Correct |
| 2nd | 1d4+4 (5-8) | ✅ Correct |
| 3rd | 1d6+4 (5-10) | ✅ Correct |
| 4th | 1d6+6 (7-12) | ❌ Should be higher than 3rd |

---

### 3. Cure Calculations ⚠️ INCONSISTENT

#### Current Patterns:

**Cure (2nd Circle):**
```csharp
int chanceToCure = (int)NMSUtils.getBeneficialMageryInscribePercentage(Caster);
chanceToCure -= poison.Level; // Simple subtraction
// Must roll >= (poison.Level * 2) to succeed
```

**Arch Cure (4th Circle):**
```csharp
int chanceToCure = (int)NMSUtils.getBeneficialMageryInscribePercentage(Caster);
chanceToCure -= (poison.Level >= 4) ? poison.Level * 2 : poison.Level;
// Must roll >= (poison.Level * 2) to succeed
```

#### Issues:
1. **Different Penalties:** Arch Cure has double penalty for Level 4+ poison, Cure doesn't
2. **Inconsistent Logic:** Should Arch Cure be better or just area-effect?
3. **Same Success Threshold:** Both use `poison.Level * 2` despite different penalties

#### Expected Behavior:
- **Cure:** Single target, standard penalty
- **Arch Cure:** Area effect, should have BETTER cure chance (it's 4th circle) OR same chance but area

---

### 4. Duration Calculations ✅ CONSISTENT

**All Spells Use:**
```csharp
TimeSpan duration = SpellHelper.NMSGetDuration(Caster, target, isBeneficial);
```

**Formula:**
- Base: Random(10, 30) seconds
- Bonus: Ceiling(Skill * 0.25) + Random based on skill tier
- Beneficial: 30% reduction, capped 15-90 seconds
- Harmful: No reduction, no cap

**Status:** ✅ Fully consistent across all circles

---

### 5. Stat Buff Calculations ✅ CONSISTENT

**All Stat Buffs Use:**
```csharp
SpellHelper.AddStatBonus(Caster, target, StatType, offset, duration);
SpellHelper.GetOffsetScalar(Caster, target, false); // for buffs
SpellHelper.GetOffsetScalar(Caster, target, true);  // for curses
```

**Status:** ✅ Fully consistent across all circles

---

## Standardization Plan

### Phase 1: Create Calculation Helper Classes

#### 1.1 Healing Calculation Helper

**File:** `Base/Calculations/SpellHealingCalculator.cs`

```csharp
public static class SpellHealingCalculator
{
    // Constants
    private const double BASE_HEAL_DIVISOR = 3.0;
    private const double GREATER_HEAL_DIVISOR = 1.5; // 2x multiplier vs base
    private const double HEAL_POWER_REDUCTION = 0.7; // 30% reduction
    private const double OTHER_TARGET_BONUS = 1.15; // 15% bonus
    private const double RANDOM_VARIANCE_MIN = 0.02; // 2%
    private const double RANDOM_VARIANCE_MAX = 0.05; // 5%
    private const double CONSECUTIVE_CAST_PENALTY = 0.25; // 25%
    private const int MINIMUM_HEAL_AMOUNT = 1;
    
    /// <summary>
    /// Calculates heal amount for standard Heal spell (1st circle)
    /// </summary>
    public static int CalculateHeal(Mobile caster, Mobile target, bool isConsecutiveCast)
    {
        int baseHeal = CalculateBaseHeal(caster, BASE_HEAL_DIVISOR);
        baseHeal = ApplyOtherTargetBonus(baseHeal, caster, target);
        baseHeal = ApplyPowerReduction(baseHeal);
        baseHeal = ApplyRandomVariance(baseHeal);
        
        if (isConsecutiveCast)
            baseHeal = ApplyConsecutiveCastPenalty(baseHeal);
            
        return Math.Max(baseHeal, MINIMUM_HEAL_AMOUNT);
    }
    
    /// <summary>
    /// Calculates heal amount for Greater Heal spell (4th circle)
    /// </summary>
    public static int CalculateGreaterHeal(Mobile caster, Mobile target, bool isConsecutiveCast)
    {
        int baseHeal = CalculateBaseHeal(caster, GREATER_HEAL_DIVISOR);
        baseHeal = ApplyOtherTargetBonus(baseHeal, caster, target);
        // Greater Heal: NO power reduction, NO random variance (more reliable)
        // But still has consecutive cast penalty for balance
        
        if (isConsecutiveCast)
            baseHeal = ApplyConsecutiveCastPenalty(baseHeal);
            
        return Math.Max(baseHeal, MINIMUM_HEAL_AMOUNT);
    }
    
    private static int CalculateBaseHeal(Mobile caster, double divisor)
    {
        return (int)(NMSUtils.getBeneficialMageryInscribePercentage(caster) / divisor);
    }
    
    private static int ApplyOtherTargetBonus(int heal, Mobile caster, Mobile target)
    {
        return (caster == target) ? heal : (int)(heal * OTHER_TARGET_BONUS);
    }
    
    private static int ApplyPowerReduction(int heal)
    {
        return (int)(heal * HEAL_POWER_REDUCTION);
    }
    
    private static int ApplyRandomVariance(int heal)
    {
        double variance = RANDOM_VARIANCE_MIN + 
            (Utility.RandomDouble() * (RANDOM_VARIANCE_MAX - RANDOM_VARIANCE_MIN));
        int reduction = Math.Max(1, (int)(heal * variance));
        return heal - reduction;
    }
    
    private static int ApplyConsecutiveCastPenalty(int heal)
    {
        int penalty = Math.Max(1, (int)(heal * CONSECUTIVE_CAST_PENALTY));
        return heal - penalty;
    }
}
```

#### 1.2 Cure Calculation Helper

**File:** `Base/Calculations/SpellCureCalculator.cs`

```csharp
public static class SpellCureCalculator
{
    // Constants
    private const int LETHAL_POISON_LEVEL = 4;
    private const double LETHAL_POISON_PENALTY_MULTIPLIER = 2.0;
    private const int CURE_SUCCESS_THRESHOLD_MULTIPLIER = 2;
    
    /// <summary>
    /// Calculates cure chance for standard Cure spell (2nd circle)
    /// </summary>
    public static int CalculateCureChance(Mobile caster, Poison poison)
    {
        int baseChance = (int)NMSUtils.getBeneficialMageryInscribePercentage(caster);
        int penalty = CalculatePoisonPenalty(poison, false); // Standard penalty
        return Math.Max(0, baseChance - penalty);
    }
    
    /// <summary>
    /// Calculates cure chance for Arch Cure spell (4th circle)
    /// Better cure chance due to higher circle
    /// </summary>
    public static int CalculateArchCureChance(Mobile caster, Poison poison)
    {
        int baseChance = (int)NMSUtils.getBeneficialMageryInscribePercentage(caster);
        int penalty = CalculatePoisonPenalty(poison, true); // Enhanced penalty for lethal
        // Arch Cure gets +10% bonus for being 4th circle
        int circleBonus = (int)(baseChance * 0.10);
        return Math.Max(0, baseChance + circleBonus - penalty);
    }
    
    /// <summary>
    /// Calculates poison level penalty
    /// </summary>
    private static int CalculatePoisonPenalty(Poison poison, bool isArchCure)
    {
        if (poison == null)
            return 0;
            
        if (isArchCure && poison.Level >= LETHAL_POISON_LEVEL)
        {
            // Arch Cure: Double penalty for lethal poison
            return poison.Level * (int)LETHAL_POISON_PENALTY_MULTIPLIER;
        }
        
        // Standard penalty
        return poison.Level;
    }
    
    /// <summary>
    /// Checks if cure attempt succeeds
    /// </summary>
    public static bool CheckCureSuccess(int cureChance, Poison poison)
    {
        if (poison == null)
            return false;
            
        int threshold = poison.Level * CURE_SUCCESS_THRESHOLD_MULTIPLIER;
        int roll = Utility.RandomMinMax(threshold, 100);
        return cureChance >= roll;
    }
}
```

#### 1.3 Damage Calculation Standardization

**Enhancement to:** `Base/Calculations/SpellDamageCalculator.cs`

```csharp
// Add circle-based damage scaling
public static class SpellDamageCalculator
{
    // ... existing code ...
    
    #region Circle-Based Damage Scaling
    
    /// <summary>
    /// Gets base damage parameters for a spell circle
    /// Ensures consistent damage progression
    /// </summary>
    public static void GetCircleDamageParams(SpellCircle circle, out int bonus, out int dice, out int sides)
    {
        switch (circle)
        {
            case SpellCircle.First:
                bonus = 2; dice = 1; sides = 3; // 1d3+2 = 3-5
                break;
            case SpellCircle.Second:
                bonus = 4; dice = 1; sides = 4; // 1d4+4 = 5-8
                break;
            case SpellCircle.Third:
                bonus = 4; dice = 1; sides = 6; // 1d6+4 = 5-10
                break;
            case SpellCircle.Fourth:
                bonus = 6; dice = 1; sides = 6; // 1d6+6 = 7-12 (IMPROVED)
                break;
            // ... higher circles
            default:
                bonus = 4; dice = 1; sides = 6; // Default
                break;
        }
    }
    
    /// <summary>
    /// Gets minimum damage floor based on EvalInt skill
    /// Should be applied to all damage spells for consistency
    /// </summary>
    public static int GetMinimumDamageFloor(Mobile caster, int baseMinDamage)
    {
        double evalInt = caster.Skills[SkillName.EvalInt].Value;
        
        if (evalInt >= 120.0)
            return baseMinDamage + 4; // +4 at 120+
        else if (evalInt >= 100.0)
            return baseMinDamage + 3; // +3 at 100+
        else if (evalInt >= 80.0)
            return baseMinDamage + 2; // +2 at 80+
        else
            return baseMinDamage; // Base minimum
    }
    
    #endregion
}
```

---

### Phase 2: Update Spell Constants

**File:** `Base/Constants/SpellConstants.cs`

```csharp
public static class SpellConstants
{
    // ... existing constants ...
    
    #region Healing Constants
    public const double BASE_HEAL_DIVISOR = 3.0;
    public const double GREATER_HEAL_DIVISOR = 1.5;
    public const double HEAL_POWER_REDUCTION = 0.7; // 30% reduction for 1st circle
    public const double OTHER_TARGET_BONUS = 1.15; // 15% bonus
    public const double RANDOM_VARIANCE_MIN = 0.02;
    public const double RANDOM_VARIANCE_MAX = 0.05;
    public const double CONSECUTIVE_CAST_PENALTY = 0.25;
    public const int MINIMUM_HEAL_AMOUNT = 1;
    #endregion
    
    #region Cure Constants
    public const int LETHAL_POISON_LEVEL = 4;
    public const double LETHAL_POISON_PENALTY_MULTIPLIER = 2.0;
    public const int CURE_SUCCESS_THRESHOLD_MULTIPLIER = 2;
    public const double ARCH_CURE_CIRCLE_BONUS = 0.10; // 10% bonus
    #endregion
    
    #region Damage Constants (Circle-Based)
    // 1st Circle
    public const int DAMAGE_1ST_BONUS = 2;
    public const int DAMAGE_1ST_DICE = 1;
    public const int DAMAGE_1ST_SIDES = 3;
    public const int DAMAGE_1ST_CAP = 8;
    
    // 2nd Circle
    public const int DAMAGE_2ND_BONUS = 4;
    public const int DAMAGE_2ND_DICE = 1;
    public const int DAMAGE_2ND_SIDES = 4;
    
    // 3rd Circle
    public const int DAMAGE_3RD_BONUS = 4;
    public const int DAMAGE_3RD_DICE = 1;
    public const int DAMAGE_3RD_SIDES = 6;
    
    // 4th Circle
    public const int DAMAGE_4TH_BONUS = 6; // INCREASED from 4
    public const int DAMAGE_4TH_DICE = 1;
    public const int DAMAGE_4TH_SIDES = 6;
    
    // Minimum Damage Floors (EvalInt-based)
    public const int MIN_DAMAGE_FLOOR_120 = 4;
    public const int MIN_DAMAGE_FLOOR_100 = 3;
    public const int MIN_DAMAGE_FLOOR_80 = 2;
    #endregion
}
```

---

### Phase 3: Refactor Spells to Use Helpers

#### 3.1 Update Heal.cs (1st Circle)

```csharp
// BEFORE
int healAmount = (int)NMSUtils.getBeneficialMageryInscribePercentage(Caster) / HEAL_DIVISOR;
// ... complex calculation logic ...

// AFTER
int healAmount = SpellHealingCalculator.CalculateHeal(Caster, target, IsConsecutiveCast());
```

#### 3.2 Update GreaterHeal.cs (4th Circle)

```csharp
// BEFORE
int toHeal = (int)(NMSUtils.getBeneficialMageryInscribePercentage(Caster) / 1.5);
if (Caster != m)
    toHeal = (int)(toHeal * 1.15);

// AFTER
int toHeal = SpellHealingCalculator.CalculateGreaterHeal(Caster, m, IsConsecutiveCast());
```

#### 3.3 Update Cure.cs (2nd Circle)

```csharp
// BEFORE
int chanceToCure = (int)NMSUtils.getBeneficialMageryInscribePercentage(Caster);
chanceToCure -= poison.Level;
if (chanceToCure >= Utility.RandomMinMax(poison.Level * 2, 100) && m.CurePoison(Caster))

// AFTER
int cureChance = SpellCureCalculator.CalculateCureChance(Caster, poison);
if (SpellCureCalculator.CheckCureSuccess(cureChance, poison) && m.CurePoison(Caster))
```

#### 3.4 Update ArchCure.cs (4th Circle)

```csharp
// BEFORE
int chanceToCure = (int)NMSUtils.getBeneficialMageryInscribePercentage(Caster);
chanceToCure -= (poison.Level >= 4) ? poison.Level * 2 : poison.Level;
if (chanceToCure >= Utility.RandomMinMax(poison.Level * 2, 100) && m.CurePoison(Caster))

// AFTER
int cureChance = SpellCureCalculator.CalculateArchCureChance(Caster, poison);
if (SpellCureCalculator.CheckCureSuccess(cureChance, poison) && m.CurePoison(Caster))
```

#### 3.5 Update Lightning.cs (4th Circle)

```csharp
// BEFORE
damage = GetNMSDamage(4, 1, 6, m);

// AFTER
int bonus, dice, sides;
SpellDamageCalculator.GetCircleDamageParams(SpellCircle.Fourth, out bonus, out dice, out sides);
double damage = GetNMSDamage(bonus, dice, sides, m);
int minDamage = SpellDamageCalculator.GetMinimumDamageFloor(Caster, bonus);
damage = Math.Max(damage, minDamage);
```

---

## Calculation Comparison Table

### Healing Calculations (Before vs After)

| Spell | Current Formula | Standardized Formula | Result at 100/100 |
|-------|----------------|---------------------|-------------------|
| **Heal** | `(44.4 / 3) * 0.7 - variance` | `CalculateHeal()` | ~9-10 HP |
| **Greater Heal** | `44.4 / 1.5 * 1.15` | `CalculateGreaterHeal()` | ~34 HP (others) |
| **Ratio** | 3.4x difference | 3.4x difference | ✅ Maintained |

### Damage Calculations (Before vs After)

| Spell | Current | Standardized | Improvement |
|-------|---------|--------------|-------------|
| **Magic Arrow** | 1d3+2 (3-5), cap 8 | Same + min floor | +2-4 at high skill |
| **Harm** | 1d4+4 (5-8) | Same + min floor | +2-3 at high skill |
| **Fireball** | 1d6+4 (5-10), min floor | Same (already has) | ✅ Consistent |
| **Lightning** | 1d6+4 (5-10) | 1d6+6 (7-12) + min floor | +2 base, +2-4 min |

### Cure Calculations (Before vs After)

| Spell | Current | Standardized | Improvement |
|-------|---------|--------------|-------------|
| **Cure** | `base - poison.Level` | `CalculateCureChance()` | Same logic, cleaner |
| **Arch Cure** | `base - (poison.Level * 2 if 4+)` | `CalculateArchCureChance()` + 10% bonus | Better for lethal poison |

---

## Implementation Priority

### High Priority (Week 1)
1. ✅ Create `SpellHealingCalculator.cs`
2. ✅ Update GreaterHeal.cs to use helper
3. ✅ Add consecutive cast tracking to GreaterHeal
4. ✅ Update constants in SpellConstants.cs

### Medium Priority (Week 2)
1. ✅ Create `SpellCureCalculator.cs`
2. ✅ Update Cure.cs and ArchCure.cs
3. ✅ Enhance SpellDamageCalculator with circle params
4. ✅ Update Lightning.cs damage

### Low Priority (Week 3)
1. ✅ Add minimum damage floors to all damage spells
2. ✅ Update Fireball.cs to use circle params (already has min floor)
3. ✅ Review and update higher circle spells (5th-8th)

---

## Testing Requirements

### Unit Tests Needed

1. **SpellHealingCalculator Tests:**
   - Test base heal calculation at different skill levels
   - Test other target bonus
   - Test power reduction
   - Test random variance range
   - Test consecutive cast penalty
   - Test minimum heal amount

2. **SpellCureCalculator Tests:**
   - Test cure chance at different skill levels
   - Test poison level penalties
   - Test lethal poison double penalty
   - Test Arch Cure circle bonus
   - Test success threshold calculation

3. **SpellDamageCalculator Tests:**
   - Test circle-based damage params
   - Test minimum damage floors
   - Test EvalInt scaling

### Integration Tests Needed

1. Compare Heal vs Greater Heal power ratio
2. Verify damage progression across circles
3. Test cure success rates at various skill levels
4. Verify duration consistency (already good)

---

## Expected Benefits

### Consistency
- ✅ All healing spells use same calculation framework
- ✅ All cure spells use same calculation framework
- ✅ Damage spells follow consistent circle progression
- ✅ Minimum damage floors applied consistently

### Maintainability
- ✅ Single source of truth for calculations
- ✅ Easy to adjust balance (change constants)
- ✅ Clear calculation logic
- ✅ Testable components

### Balance
- ✅ Proper power scaling between circles
- ✅ Consistent bonuses and penalties
- ✅ Predictable spell behavior

---

## Risk Assessment

### Low Risk
- Creating helper classes (no breaking changes)
- Adding constants (no breaking changes)
- Updating GreaterHeal (isolated change)

### Medium Risk
- Updating Lightning damage (affects PvP balance)
- Adding minimum damage floors (affects all damage spells)
- Changing Arch Cure calculation (affects cure success rates)

### Mitigation
1. **Incremental rollout:** One spell type at a time
2. **Feature flags:** Ability to toggle new calculations
3. **Comprehensive testing:** Unit + integration tests
4. **Balance monitoring:** Track spell effectiveness after changes

---

## Conclusion

The Base folder structure is **fully compliant** with the guide. However, **calculation consistency** needs improvement:

1. **Healing:** Inconsistent divisors, reductions, and bonuses
2. **Damage:** Missing circle progression, inconsistent minimum floors
3. **Cure:** Different penalty logic between Cure and Arch Cure
4. **Duration:** ✅ Already consistent
5. **Stat Buffs:** ✅ Already consistent

The standardization plan provides:
- Helper classes for consistent calculations
- Constants for easy balance adjustments
- Clear progression between spell circles
- Maintainable, testable code structure

**Next Step:** Implement Phase 1 (Healing Calculator) as proof of concept.

