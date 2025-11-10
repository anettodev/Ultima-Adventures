# Magery 4th Circle Spells - Refactoring Plan

## Executive Summary

This document provides a comprehensive refactoring plan for all 4th circle Magery spells, focusing on:
- **Cyclomatic Complexity Reduction**
- **Performance Optimization**
- **Readability Improvements**
- **Business Logic Enhancement**
- **Damage/Benefit/Duration Calculations by Skill Level**

---

## Cyclomatic Complexity Analysis

### Current Complexity Scores

| Spell | Method | Complexity | Status | Target |
|-------|--------|------------|--------|--------|
| **Recall** | `Effect()` | **12** | 🔴 High | ≤ 6 |
| **Recall** | `OnTarget()` | **6** | 🟡 Medium | ≤ 4 |
| **ManaDrain** | `Target()` | **8** | 🟡 Medium | ≤ 5 |
| **GreaterHeal** | `Target()` | **9** | 🟡 Medium | ≤ 5 |
| **Lightning** | `Target()` | **4** | 🟢 Low | ≤ 4 |
| **FireField** | `Target()` | **7** | 🟡 Medium | ≤ 5 |
| **FireField** | `doFireDamage()` | **10** | 🔴 High | ≤ 5 |
| **FireField** | `doDamage()` (Timer) | **10** | 🔴 High | ≤ 5 |
| **Curse** | `Target()` | **5** | 🟢 Low | ≤ 5 |
| **ArchCure** | `Target()` | **8** | 🟡 Medium | ≤ 5 |
| **ArchProtection** | `Target()` | **7** | 🟡 Medium | ≤ 5 |

**Total High Complexity Methods:** 3  
**Total Medium Complexity Methods:** 6  
**Total Low Complexity Methods:** 2

### Complexity Reduction Strategy

#### 1. Extract Validation Methods
**Pattern:** Move validation logic to separate methods

**Example - Recall.Effect():**
```csharp
// BEFORE: 12 complexity
public void Effect(Point3D loc, Map map, bool checkMulti)
{
    if (Caster.AccessLevel > AccessLevel.Player) { ... }
    else if (!SpellHelper.CheckTravel(...)) { ... }
    else if (Worlds.AllowEscape(...) == false) { ... }
    // ... 8 more conditions
}

// AFTER: 3 complexity
public void Effect(Point3D loc, Map map, bool checkMulti)
{
    if (IsStaffRecall())
        PerformStaffRecall(loc, map);
    else if (!ValidateRecallConditions(loc, map, checkMulti))
        return;
    else
        PerformRecall(loc, map);
}

private bool ValidateRecallConditions(Point3D loc, Map map, bool checkMulti)
{
    return ValidateTravelRestrictions(loc, map) &&
           ValidateRegionRestrictions(loc, map) &&
           ValidateDiscovery(loc, map) &&
           ValidateWeight() &&
           ValidateLocation(loc, map, checkMulti) &&
           ValidateRunebookCharges();
}
```

#### 2. Extract Business Logic to Helper Classes
**Pattern:** Move complex calculations to dedicated classes

**Example - FireField Damage:**
```csharp
// BEFORE: Duplicated in doFireDamage() and doDamage()
private void doFireDamage(Mobile m) { /* 50 lines */ }
private void doDamage(Mobile m) { /* 50 lines - DUPLICATE */ }

// AFTER: Single source of truth
private void ApplyFireDamage(Mobile m)
{
    FireFieldDamageCalculator.CalculateAndApply(m, m_Caster, m_Damage);
}

// New helper class
public static class FireFieldDamageCalculator
{
    public static void CalculateAndApply(Mobile target, Mobile caster, int baseDamage)
    {
        int damage = CalculateDamage(target, caster, baseDamage);
        if (ShouldApplyDamage())
            ApplyDamage(target, caster, damage);
    }
    
    private static int CalculateDamage(Mobile target, Mobile caster, int baseDamage)
    {
        if (IsFriendlyTarget(target, caster))
            return Utility.RandomMinMax(1, baseDamage / 2);
        // ... rest of logic
    }
}
```

#### 3. Replace Complex Conditionals with Strategy Pattern
**Pattern:** Use strategy pattern for different behaviors

**Example - ArchProtection:**
```csharp
// BEFORE: 7 complexity with AOS/Pre-AOS branching
if (Core.AOS) { /* AOS logic */ }
else { /* Pre-AOS logic */ }

// AFTER: Strategy pattern
private IProtectionStrategy GetProtectionStrategy()
{
    return Core.AOS 
        ? new AOSProtectionStrategy() 
        : new PreAOSProtectionStrategy();
}

public void Target(IPoint3D p)
{
    var strategy = GetProtectionStrategy();
    strategy.ApplyProtection(Caster, GetTargets(p));
}
```

---

## Performance Optimization

### Critical Performance Issues

#### 1. FireField - Duplicated Damage Logic
**Issue:** `doFireDamage()` and `doDamage()` contain identical 50-line methods  
**Impact:** Code duplication, maintenance burden, potential bugs  
**Fix:** Extract to shared helper class

#### 2. CharacterDatabase.GetMySpellHue() - Repeated Calls
**Issue:** Called multiple times per spell cast  
**Impact:** Database/Character lookup overhead  
**Fix:** Cache result in local variable

```csharp
// BEFORE
m.FixedParticles(0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0), 0, EffectLayer.Waist);
m.PlaySound(0x202);
// ... called again later

// AFTER
int spellHue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
m.FixedParticles(0x376A, 9, 32, spellHue, 0, EffectLayer.Waist);
m.PlaySound(0x202);
```

#### 3. Curse - Outdated Collections
**Issue:** Uses `Hashtable` and `ArrayList` (non-generic, slower)  
**Impact:** Boxing/unboxing overhead, no type safety  
**Fix:** Replace with `Dictionary<Mobile, Timer>` and `HashSet<Mobile>`

```csharp
// BEFORE
private static Hashtable m_UnderEffect = new Hashtable();
public static ArrayList Cursed = new ArrayList();

// AFTER
private static Dictionary<Mobile, Timer> m_UnderEffect = new Dictionary<Mobile, Timer>();
public static HashSet<Mobile> Cursed = new HashSet<Mobile>();
```

#### 4. Recall - Redundant Weight Check
**Issue:** Weight checked in both `CheckCast()` and `Effect()`  
**Impact:** Unnecessary duplicate validation  
**Fix:** Remove from `Effect()` (already checked in `CheckCast()`)

#### 5. FireField - Queue Allocation
**Issue:** Static Queue reused but not cleared properly  
**Impact:** Potential memory issues, thread safety concerns  
**Fix:** Use local Queue or proper cleanup

```csharp
// BEFORE
private static Queue m_Queue = new Queue(); // Static, shared

// AFTER
private Queue m_Queue = new Queue(); // Instance-based, or use List<Mobile>
```

#### 6. ArchCure - Unused Helper Methods
**Issue:** `IsAggressor()`, `IsAggressed()`, `IsInnocentTo()`, `IsAllyTo()` defined but never used  
**Impact:** Dead code, maintenance burden  
**Fix:** Remove or implement if needed

---

## Readability Improvements

### 1. Extract Magic Numbers to Constants

```csharp
// BEFORE
int toDrain = (int)((magebonus / 10) * 1.5);
if (m is PlayerMobile && toDrain > (m.ManaMax * 0.3))
    toDrain = (int)(m.ManaMax * 0.3);

// AFTER
private const double MANA_DRAIN_MULTIPLIER = 1.5;
private const double MANA_DRAIN_DIVISOR = 10.0;
private const double PLAYER_MANA_DRAIN_LIMIT = 0.3;
private const double CREATURE_MANA_DRAIN_LIMIT = 0.5;

int toDrain = CalculateManaDrain(magebonus);
toDrain = ApplyManaDrainLimits(toDrain, m);
```

### 2. Extract Portuguese Messages to Constants

```csharp
// BEFORE
Caster.SendMessage(55, "O alvo não pode ser visto.");

// AFTER
private const string MSG_TARGET_NOT_VISIBLE = "O alvo não pode ser visto.";
Caster.SendMessage(MSG_COLOR_ERROR, MSG_TARGET_NOT_VISIBLE);
```

### 3. Remove Commented Code
**Issue:** Multiple files contain large blocks of commented code  
**Impact:** Code clutter, confusion  
**Fix:** Remove or convert to documentation

### 4. Improve Method Naming
```csharp
// BEFORE
private void doFireDamage(Mobile m)
private void doDamage(Mobile m)

// AFTER
private void ApplyFireDamageToTarget(Mobile target)
private void ProcessPeriodicFireDamage(Mobile target)
```

---

## Business Logic Improvements

### 1. Centralize Validation Logic

**Create:** `SpellValidationHelper` class

```csharp
public static class SpellValidationHelper
{
    public static bool CanSeeTarget(Mobile caster, object target)
    {
        if (!caster.CanSee(target))
        {
            caster.SendMessage(MSG_COLOR_ERROR, MSG_TARGET_NOT_VISIBLE);
            return false;
        }
        return true;
    }
    
    public static bool CanHealTarget(Mobile caster, Mobile target)
    {
        if (target.IsDeadBondedPet || (target is BaseCreature && ((BaseCreature)target).IsAnimatedDead))
        {
            caster.SendMessage(MSG_COLOR_ERROR, MSG_CANNOT_HEAL_DEAD);
            return false;
        }
        
        if (target is Golem)
        {
            caster.SendMessage(MSG_COLOR_ERROR, MSG_CANNOT_HEAL_GOLEM);
            return false;
        }
        
        // ... other checks
        return true;
    }
}
```

### 2. Extract Damage Calculation Logic

**Create:** `SpellCalculationHelper` class

```csharp
public static class SpellCalculationHelper
{
    public static int CalculateManaDrain(Mobile caster, Mobile target)
    {
        double magebonus = caster.Skills.Magery.Value * NMSUtils.getDamageEvalBenefit(caster);
        int toDrain = (int)((magebonus / MANA_DRAIN_DIVISOR) * MANA_DRAIN_MULTIPLIER);
        
        if (toDrain < 0)
            toDrain = 0;
        else if (target is PlayerMobile)
            toDrain = Math.Min(toDrain, (int)(target.ManaMax * PLAYER_MANA_DRAIN_LIMIT));
        else if (target is BaseCreature)
            toDrain = Math.Min(toDrain, (int)(target.ManaMax * CREATURE_MANA_DRAIN_LIMIT));
        else
            toDrain = Math.Min(toDrain, target.Mana);
            
        return toDrain;
    }
    
    public static int CalculateGreaterHeal(Mobile caster, Mobile target)
    {
        int baseHeal = (int)(NMSUtils.getBeneficialMageryInscribePercentage(caster) / 1.5);
        return (caster == target) ? baseHeal : (int)(baseHeal * 1.15);
    }
}
```

### 3. Extract Friendly Fire Logic

**Create:** `FriendlyFireHelper` class

```csharp
public static class FriendlyFireHelper
{
    public static bool IsFriendlyTarget(Mobile caster, Mobile target)
    {
        if (caster == target)
            return true;
            
        Guild fromGuild = SpellHelper.GetGuildFor(caster);
        Guild toGuild = SpellHelper.GetGuildFor(target);
        
        if (fromGuild != null && toGuild != null && 
            (fromGuild == toGuild || fromGuild.IsAlly(toGuild)))
            return true;
            
        Party party = Party.Get(caster);
        if (party != null && party.Contains(target))
            return true;
            
        return false;
    }
    
    public static int ApplyFriendlyFireReduction(int damage, Mobile caster, Mobile target)
    {
        if (IsFriendlyTarget(caster, target))
            return Utility.RandomMinMax(1, damage / 2);
        return damage;
    }
}
```

---

## Damage/Benefit/Duration Calculations by Skill Level

### Calculation Formulas

#### NMS Damage Formula
```csharp
// Base: Utility.Dice(dice, sides, bonus)
// EvalInt Benefit: ((EvalInt * 3) / 100) + 1) / 10 + 1
// Final: Floor(baseDamage * evalBenefit)
```

#### Beneficial Magery Inscribe Percentage
```csharp
// maxPercent = Inscribe / 3 (min 1)
// influence = (maxPercent / 100) + 1
// points = (Magery * influence) / 3
```

#### Duration Formula (NMSGetDuration)
```csharp
// Base: Random(10, 30)
// Bonus: Ceiling(Skill * 0.25) + Random based on skill tier
// Beneficial: total * 0.70 (30% reduction), capped 15-90 seconds
// Harmful: No reduction, no cap
```

### Skill Level Scenarios

#### Scenario 1: Novice (Magery 50, EvalInt 50, Inscribe 50)

**Lightning Damage:**
```
Base: Dice(1, 6, 4) = 5-10 damage
EvalInt Benefit: ((50 * 3) / 100 + 1) / 10 + 1 = 1.15
Final: 5-10 * 1.15 = 5-11 damage
```

**Greater Heal:**
```
Beneficial Percentage: (50 * ((50/3)/100 + 1)) / 3 = 20.8 points
Base Heal: 20.8 / 1.5 = 13 HP (self), 15 HP (others)
```

**Mana Drain:**
```
Magebonus: 50 * 1.15 = 57.5
ToDrain: (57.5 / 10) * 1.5 = 8 mana
Duration: (5 * 1.15) + 1-3 = 6-8 seconds
```

**Duration (Beneficial):**
```
Base: 10-30 seconds
Inscribe Bonus: Ceiling(50 * 0.25) = 13
Tier Bonus (60-79): Random(10, 20) = 10-20
Total: 23-63 seconds
After 30% reduction: 16-44 seconds (capped 15-90)
Final: 16-44 seconds
```

#### Scenario 2: Adept (Magery 80, EvalInt 80, Inscribe 80)

**Lightning Damage:**
```
Base: Dice(1, 6, 4) = 5-10 damage
EvalInt Benefit: ((80 * 3) / 100 + 1) / 10 + 1 = 1.24
Final: 5-10 * 1.24 = 6-12 damage
```

**Greater Heal:**
```
Beneficial Percentage: (80 * ((80/3)/100 + 1)) / 3 = 35.5 points
Base Heal: 35.5 / 1.5 = 23 HP (self), 27 HP (others)
```

**Mana Drain:**
```
Magebonus: 80 * 1.24 = 99.2
ToDrain: (99.2 / 10) * 1.5 = 14 mana
Duration: (5 * 1.24) + 1-3 = 7-9 seconds
```

**Duration (Beneficial):**
```
Base: 10-30 seconds
Inscribe Bonus: Ceiling(80 * 0.25) = 20
Tier Bonus (80-99): Random(16, 25) = 16-25
Total: 46-75 seconds
After 30% reduction: 32-52 seconds
Final: 32-52 seconds
```

#### Scenario 3: Expert (Magery 100, EvalInt 100, Inscribe 100)

**Lightning Damage:**
```
Base: Dice(1, 6, 4) = 5-10 damage
EvalInt Benefit: ((100 * 3) / 100 + 1) / 10 + 1 = 1.30
Final: 5-10 * 1.30 = 6-13 damage
```

**Greater Heal:**
```
Beneficial Percentage: (100 * ((100/3)/100 + 1)) / 3 = 44.4 points
Base Heal: 44.4 / 1.5 = 29 HP (self), 34 HP (others)
```

**Mana Drain:**
```
Magebonus: 100 * 1.30 = 130
ToDrain: (130 / 10) * 1.5 = 19 mana
Duration: (5 * 1.30) + 1-3 = 7-9 seconds
```

**Duration (Beneficial):**
```
Base: 10-30 seconds
Inscribe Bonus: Ceiling(100 * 0.25) = 25
Tier Bonus (100-119): Random(18, 30) = 18-30
Total: 53-85 seconds
After 30% reduction: 37-59 seconds
Final: 37-59 seconds
```

#### Scenario 4: Master (Magery 120, EvalInt 120, Inscribe 120)

**Lightning Damage:**
```
Base: Dice(1, 6, 4) = 5-10 damage
EvalInt Benefit: ((120 * 3) / 100 + 1) / 10 + 1 = 1.36
Final: 5-10 * 1.36 = 6-13 damage
```

**Greater Heal:**
```
Beneficial Percentage: (120 * ((120/3)/100 + 1)) / 3 = 56 points
Base Heal: 56 / 1.5 = 37 HP (self), 43 HP (others)
```

**Mana Drain:**
```
Magebonus: 120 * 1.36 = 163.2
ToDrain: (163.2 / 10) * 1.5 = 24 mana
Duration: (5 * 1.36) + 1-3 = 7-9 seconds
```

**Duration (Beneficial):**
```
Base: 10-30 seconds
Inscribe Bonus: Ceiling(120 * 0.25) = 30
Tier Bonus (120+): Random(20, 40) = 20-40
Total: 60-100 seconds
After 30% reduction: 42-70 seconds
Final: 42-70 seconds
```

### Fire Field Damage Scenarios

**Base Damage:** `GetNMSDamage(1, 2, 6, Caster)`

| Skill Level | Base Dice | EvalInt Benefit | Per Tick Damage | 50% Chance | 30s Total Potential |
|-------------|-----------|-----------------|-----------------|------------|---------------------|
| 50 | 3-8 | 1.15 | 3-9 | 1.5-4.5 | 45-135 |
| 80 | 3-8 | 1.24 | 3-9 | 1.5-4.5 | 45-135 |
| 100 | 3-8 | 1.30 | 3-10 | 1.5-5 | 45-150 |
| 120 | 3-8 | 1.36 | 4-10 | 2-5 | 60-150 |

### Arch Cure Success Scenarios

**Formula:** `chanceToCure = BeneficialMageryInscribePercentage - (poison.Level * 2 if Level 4+, else poison.Level)`

| Skill Level | Base Chance | Level 1 Poison | Level 4 Poison | Level 5 Poison |
|-------------|-------------|----------------|----------------|----------------|
| 50 | 20.8 | 19.8% (vs 2) | 12.8% (vs 8) | 10.8% (vs 10) |
| 80 | 35.5 | 34.5% (vs 2) | 27.5% (vs 8) | 25.5% (vs 10) |
| 100 | 44.4 | 43.4% (vs 2) | 36.4% (vs 8) | 34.4% (vs 10) |
| 120 | 56.0 | 55.0% (vs 2) | 48.0% (vs 8) | 46.0% (vs 10) |

---

## Refactoring Implementation Plan

### Phase 1: Low-Risk Improvements (Week 1)
1. ✅ Extract magic numbers to constants
2. ✅ Extract Portuguese messages to constants
3. ✅ Cache `GetMySpellHue()` calls
4. ✅ Remove commented code
5. ✅ Replace `Hashtable`/`ArrayList` with generics

### Phase 2: Medium-Risk Refactoring (Week 2)
1. ✅ Extract validation methods
2. ✅ Extract calculation helpers
3. ✅ Extract friendly fire logic
4. ✅ Remove duplicate weight check in Recall
5. ✅ Fix FireField queue allocation

### Phase 3: High-Risk Refactoring (Week 3)
1. ✅ Refactor Recall.Effect() complexity
2. ✅ Consolidate FireField damage logic
3. ✅ Implement strategy pattern for ArchProtection
4. ✅ Remove unused helper methods in ArchCure

### Phase 4: Testing & Validation (Week 4)
1. ✅ Unit tests for calculation helpers
2. ✅ Integration tests for each spell
3. ✅ Performance benchmarking
4. ✅ Code review

---

## Expected Improvements

### Cyclomatic Complexity
- **Before:** 3 high, 6 medium, 2 low
- **After:** 0 high, 2 medium, 9 low
- **Reduction:** 60% reduction in high complexity

### Performance
- **CharacterDatabase calls:** 50% reduction (caching)
- **Collection operations:** 20% faster (generics)
- **Code duplication:** 100% elimination (FireField)

### Maintainability
- **Code duplication:** Eliminated
- **Magic numbers:** All extracted to constants
- **Error messages:** Centralized
- **Test coverage:** 80%+ target

### Business Logic
- **Validation:** Centralized and reusable
- **Calculations:** Testable and documented
- **Friendly fire:** Consistent across spells

---

## Risk Assessment

### Low Risk
- Extracting constants
- Caching spell hue
- Removing commented code
- Replacing collections

### Medium Risk
- Extracting validation methods
- Extracting calculation helpers
- Removing duplicate checks

### High Risk
- Refactoring Recall.Effect() (complex validation chain)
- Consolidating FireField damage (duplicated logic)
- Strategy pattern implementation (behavioral change)

### Mitigation Strategies
1. **Incremental refactoring:** One spell at a time
2. **Comprehensive testing:** Unit + integration tests
3. **Feature flags:** Ability to rollback changes
4. **Code review:** Peer review before merge

---

## Success Metrics

### Code Quality
- ✅ Cyclomatic complexity ≤ 5 for all methods
- ✅ Zero code duplication
- ✅ 100% of magic numbers extracted
- ✅ All error messages centralized

### Performance
- ✅ 20% reduction in spell cast overhead
- ✅ 50% reduction in database lookups
- ✅ Zero memory leaks

### Maintainability
- ✅ 80%+ test coverage
- ✅ All calculations documented
- ✅ Consistent patterns across spells

---

## Conclusion

This refactoring plan addresses critical issues in the 4th circle spell implementation:
- **Complexity:** Reduces high-complexity methods by 60%
- **Performance:** Optimizes critical paths
- **Maintainability:** Improves code organization and testability
- **Business Logic:** Centralizes and standardizes calculations

The phased approach minimizes risk while delivering incremental improvements. Each phase builds on the previous, ensuring stability throughout the refactoring process.

