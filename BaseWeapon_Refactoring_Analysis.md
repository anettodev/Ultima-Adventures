# BaseWeapon.cs - Comprehensive Code Analysis & Refactoring Plan

## Executive Summary

**File**: `Scripts/Items and addons/Weapons/BaseWeapon.cs`  
**Total Lines**: 4,671  
**Class Type**: Abstract base class  
**Complexity Level**: **CRITICAL** - Requires significant refactoring

---

## 1. Code Structure Analysis

### 1.1 Class Responsibilities (Single Responsibility Principle Violations)

The `BaseWeapon` class violates SRP by handling multiple concerns:

1. **Weapon Properties Management** (Lines 108-441)
   - 30+ properties with getters/setters
   - AOS vs Old system compatibility
   - Material damage calculations

2. **Combat Mechanics** (Lines 843-2291)
   - Hit chance calculation (`CheckHit`)
   - Damage calculation (`OnHit`, `ComputeDamage`, `ScaleDamageAOS`, `ScaleDamageOld`)
   - Swing delay calculation (`GetDelay`)
   - Parry/dodge mechanics (`CheckParry`, `CheckDodge`)

3. **Damage Absorption** (Lines 1335-1485)
   - Armor absorption
   - Item durability damage
   - Mount deflection logic

4. **Special Effects** (Lines 2130-2289)
   - Area attacks
   - Magic effects (fireball, lightning, etc.)
   - Poison application
   - Life/mana/stamina leech

5. **Serialization** (Lines 3234-3743)
   - Complex save/load logic
   - Version handling (10 versions)

6. **UI/Display** (Lines 3778-4192)
   - Property list generation
   - Name formatting
   - Status display

7. **Crafting Integration** (Lines 4346-4657)
   - Craft system integration
   - Resource handling
   - Quality assignment

### 1.2 Method Count Analysis

- **Public Methods**: ~50+
- **Private Methods**: ~20+
- **Virtual Methods**: ~30+
- **Property Accessors**: ~80+

### 1.3 Code Duplication Issues (DRY Violations)

#### 1.3.1 Type Checking Patterns (Repeated 50+ times)

**Pattern 1: PlayerMobile Type Checks**
```csharp
// Found 29+ instances
if (attacker is PlayerMobile)
if (defender is PlayerMobile)
if (from is PlayerMobile)
if (m is PlayerMobile)
```

**Pattern 2: BaseCreature Type Checks**
```csharp
// Found 25+ instances
if (attacker is BaseCreature)
if (defender is BaseCreature)
if (m is BaseCreature)
```

**Pattern 3: Weapon Type Checks**
```csharp
// Found 15+ instances
if (this is BaseRanged)
if (this is Harpoon || this is GiftHarpoon || this is LevelHarpoon)
if (this is WizardWand || this is BaseWizardStaff || ...)
```

**Pattern 4: Combined Type Checks with Casting**
```csharp
// Found 55+ instances - EXPENSIVE OPERATION
if (attacker is PlayerMobile && ((PlayerMobile)attacker).Mounted)
if (defender is BaseCreature && ((BaseCreature)defender).Mounted)
```

#### 1.3.2 Property Access Patterns (Repeated 100+ times)

**Pattern 1: Midland Checks** (69 instances)
```csharp
AdventuresFunctions.IsInMidland((object)attacker)
AdventuresFunctions.IsInMidland((object)defender)
AdventuresFunctions.IsInMidland((object)this)
```

**Pattern 2: Agility Access** (39 instances)
```csharp
((PlayerMobile)attacker).Agility()
((BaseCreature)attacker).Agility()
((PlayerMobile)defender).Agility()
((BaseCreature)defender).Agility()
```

**Pattern 3: Mounted Checks** (25+ instances)
```csharp
((PlayerMobile)attacker).Mounted
((BaseCreature)attacker).Mounted
((PlayerMobile)defender).Mounted
```

**Pattern 4: SoulBound Checks** (15+ instances)
```csharp
if (attacker is PlayerMobile && ((PlayerMobile)attacker).SoulBound)
if (defender is PlayerMobile && ((PlayerMobile)defender).SoulBound)
```

**Pattern 5: Avatar Checks** (10+ instances)
```csharp
if (attacker is PlayerMobile && ((PlayerMobile)attacker).Avatar)
```

#### 1.3.3 Bonus Calculation Patterns (Repeated 20+ times)

**Pattern 1: Skill Bonus Calculations** (12 instances)
```csharp
// All use same GetBonus() method with different parameters
double strengthBonus = GetBonus( attacker.Str, 0.100, 100.0, 3.00 );
double anatomyBonus = GetBonus( attacker.Skills[SkillName.Anatomy].Value, 0.300, 100.0, 3.00 );
double tacticsBonus = GetBonus( attacker.Skills[SkillName.Tactics].Value, 0.300, 100.0, 3.00 );
// ... 9 more similar calls
```

**Pattern 2: Conditional Bonus Zeroing** (8 instances)
```csharp
if ( Type != WeaponType.Axe )
    lumberBonus = 0.0;
if ( Type != WeaponType.Bashing )
    miningBonus = 0.0;
if (!( this is Harpoon || this is GiftHarpoon || this is LevelHarpoon ))
    fishingBonus = 0.0;
// ... 5 more similar patterns
```

**Pattern 3: Diminishing Returns Application** (3 instances - but pattern repeated)
```csharp
if (attacker is PlayerMobile && ((PlayerMobile)attacker).Avatar)
    total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 77, 10 ) );
else if (attacker is PlayerMobile)
    total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 53, 10 ) );
else
    total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 225, 10 ) );
```

#### 1.3.4 Master Resolution Pattern (Repeated 5+ times)
```csharp
Mobile atcker = attacker;
if ( attacker is BaseCreature && ((BaseCreature)attacker).GetMaster() is PlayerMobile)
    atcker = ((BaseCreature)attacker).GetMaster();
```

#### 1.3.5 Mount Extraction Pattern (Repeated 8+ times)
```csharp
IMount mount = null;
if (defender is BaseCreature)
    mount = ((BaseCreature)defender).Mount;
else if (defender is PlayerMobile)
    mount = ((PlayerMobile)defender).Mount;
```

#### 1.3.6 Switch Statement Patterns (Repeated 10+ times)

**Pattern 1: Quality/Damage Level Switches**
```csharp
// Similar switch structure repeated in GetDamageBonus, GetHitChanceBonus, OnCraft
switch ( m_Quality )
{
    case WeaponQuality.Low: bonus -= 10; break;
    case WeaponQuality.Exceptional: bonus += 10; break;
}
```

**Pattern 2: Damage Level Switches**
```csharp
switch ( m_DamageLevel )
{
    case WeaponDamageLevel.Ruin: bonus += 5; break;
    case WeaponDamageLevel.Might: bonus += 10; break;
    // ... repeated in multiple places
}
```

#### 1.3.7 Property List Generation Patterns (Repeated 50+ times)
```csharp
// Similar pattern repeated for every property
if ( (prop = m_AosAttributes.SomeProperty) != 0 && !(AdventuresFunctions.IsInMidland((object)this)))
    list.Add( 1060401, prop.ToString() );
```

#### 1.3.8 Stat Mod Creation Pattern (Repeated 6+ times)
```csharp
string modName = this.Serial.ToString();
if ( strBonus != 0 )
    m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );
if ( dexBonus != 0 )
    m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );
if ( intBonus != 0 )
    m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
```

---

## 2. Cyclomatic Complexity Analysis

### 2.1 Complexity Calculation Method

Cyclomatic Complexity = 1 + Number of Decision Points

**Decision Points Include:**
- `if` statements
- `switch` cases (each case counts)
- `while` loops
- `for` loops
- `catch` blocks
- `?:` ternary operators
- `&&` and `||` in conditions (each adds complexity)

### 2.2 Complexity by Method

| Method | Lines | Decision Points | Complexity | Status |
|--------|-------|----------------|------------|--------|
| `OnHit` | 1638-2291 (653) | ~85 | **86** | 🔴 CRITICAL |
| `CheckHit` | 843-972 (129) | ~25 | **26** | 🔴 HIGH |
| `GetDelay` | 974-1099 (125) | ~20 | **21** | 🔴 HIGH |
| `AbsorbDamageAOS` | 1335-1485 (150) | ~35 | **36** | 🔴 HIGH |
| `ScaleDamageAOS` | 2809-2911 (102) | ~15 | **16** | 🟡 MEDIUM |
| `ScaleDamageOld` | 2920-3118 (198) | ~25 | **26** | 🔴 HIGH |
| `OnSwing` | 1126-1183 (57) | ~12 | **13** | 🟡 MEDIUM |
| `OnEquip` | 665-719 (54) | ~10 | **11** | 🟡 MEDIUM |
| `OnCraft` | 4348-4657 (309) | ~45 | **46** | 🔴 HIGH |
| `Deserialize` | 3418-3743 (325) | ~30 | **31** | 🔴 HIGH |
| `GetProperties` | 3937-4192 (255) | ~40 | **41** | 🔴 HIGH |
| `WeaponMaterialDamage` | 36-88 (52) | 1 switch, 40 cases | **41** | 🔴 HIGH |

### 2.3 Overall Class Complexity

**Total Estimated Complexity**: **~400+**

**Complexity Distribution:**
- 🔴 **Critical (>30)**: 6 methods
- 🟡 **High (15-30)**: 8 methods  
- 🟢 **Medium (5-15)**: 15 methods
- ⚪ **Low (<5)**: 30+ methods

**Industry Standards:**
- **Acceptable**: < 10 per method
- **Warning**: 10-20 per method
- **Critical**: > 20 per method
- **Class Total**: Should be < 200

**Current Status**: **CRITICAL** - Multiple methods exceed safe complexity thresholds

---

## 3. Logic & Rules Understanding

### 3.1 Core Combat Flow

```
OnSwing() 
  → CheckHit() 
    → OnHit() 
      → ComputeDamage() 
        → ScaleDamageAOS/Old() 
          → Apply Bonuses 
            → AbsorbDamage() 
              → Apply Effects
```

### 3.2 Damage Calculation Rules

#### AOS System (Primary):
1. **Base Damage**: Random(MinDamage, MaxDamage) + MaterialBonus
2. **Skill Bonuses**:
   - Strength: 0.1% per point, +3% at 100+
   - Anatomy: 0.3% per point, +3% at 100+
   - Tactics: 0.3% per point, +3% at 100+
   - ArmsLore: **0%** (removed)
3. **Weapon Modifiers**:
   - Quality: -10%, 0%, +10%
   - Damage Level: 0%, 5%, 10%, 12%, 15%, 18%
4. **Caps**: 53 (regular), 77 (avatars)

#### Old System (Legacy):
- Different formula with /5.0 calculations
- Halves final damage
- Still maintained for compatibility

### 3.3 Hit Chance Calculation Rules

**AOS System:**
- Base: (SkillValue + 20) * (100 + bonuses)
- Bonuses: AttackChance, DivineFury, Animal forms, etc.
- Caps: HitChanceCap (45%), DefendChanceCap (45%)
- Midland: Special agility-based calculations

**Old System:**
- Base: SkillValue + 50
- Simpler calculation

### 3.4 Special Rules & Conditions

1. **Midland Region**: Different combat rules, agility-based
2. **SoulBound Players**: Bypass certain restrictions
3. **Mounted Combat**: Special deflection mechanics
4. **Avatar Players**: Higher damage caps (77 vs 53)
5. **Balance System**: Affects damage for Avatars
6. **Weapon Abilities**: Special moves with damage multipliers
7. **Slayer Weapons**: 75% bonus against specific enemies
8. **EnemyOfOne**: 50% bonus for paladins

---

## 4. Performance Issues

### 4.1 Identified Performance Problems

1. **Repeated Type Checks** (High Frequency):
   ```csharp
   // Called in every combat calculation
   if (attacker is PlayerMobile)
   if (defender is BaseCreature)
   ```
   **Impact**: Type checking is expensive, done 50+ times per hit

2. **Repeated Property Access**:
   ```csharp
   ((PlayerMobile)attacker).Mounted
   ((PlayerMobile)attacker).SoulBound
   ((PlayerMobile)attacker).Avatar
   ```
   **Impact**: Multiple casts and property accesses per calculation

3. **String Concatenation in Hot Paths**:
   ```csharp
   modName + "Str"
   "You perform a sneak attack for " + tellBonus + "% more damage!"
   ```
   **Impact**: String allocation in combat-critical code

4. **Large Switch Statements**:
   - `WeaponMaterialDamage`: 40+ cases
   - `OnCraft`: Multiple nested switches
   **Impact**: Linear search through cases

5. **Complex Nested Conditionals**:
   ```csharp
   if (A && (B || C) && (D is Type || E is Type))
   ```
   **Impact**: Difficult to optimize, high branch prediction misses

6. **Redundant Calculations**:
   - Same bonuses calculated multiple times
   - Agility() called repeatedly
   - IsInMidland() checked multiple times per hit

### 4.2 Memory Allocation Issues

1. **Boxing in Conditionals**:
   ```csharp
   AdventuresFunctions.IsInMidland((object)attacker)
   ```
   **Impact**: Boxing allocation on every check

2. **Temporary Objects**:
   - StatMod creation
   - List allocations in area attacks
   - String allocations for messages

---

## 5. Readability Issues

### 5.1 Code Smells

1. **God Class**: 4,671 lines, too many responsibilities
2. **Long Methods**: `OnHit` (653 lines), `OnCraft` (309 lines)
3. **Deep Nesting**: Up to 6-7 levels deep
4. **Magic Numbers**: Hard-coded values throughout (0.1, 0.3, 53, 77, etc.)
5. **Inconsistent Naming**: Mix of `m_` prefix and camelCase
6. **Commented Code**: Large blocks of commented-out code
7. **Duplicate Code**: Same patterns repeated 20+ times

### 5.2 Maintainability Issues

1. **Tight Coupling**: Direct dependencies on many systems
2. **Hard to Test**: Difficult to unit test due to size
3. **Hard to Extend**: Adding new features requires modifying large methods
4. **Documentation**: Inconsistent comments, some outdated

---

## 6. Refactoring Plan

### Phase 1: Extract Helper Classes (Low Risk)

#### 6.1.1 Create `WeaponDamageCalculator` Class
**Purpose**: Extract damage calculation logic

**Methods to Extract:**
- `ScaleDamageAOS` → `CalculateAOSDamage()`
- `ScaleDamageOld` → `CalculateOldDamage()`
- `GetBaseDamage` → `GetBaseDamage()`
- `GetBonus` → `CalculateSkillBonus()`

**Benefits:**
- Reduces BaseWeapon complexity by ~150 lines
- Makes damage formulas testable
- Centralizes damage rules

#### 6.1.2 Create `WeaponHitChanceCalculator` Class
**Purpose**: Extract hit chance calculation

**Methods to Extract:**
- `CheckHit` → `CalculateHitChance()`
- Hit chance bonus calculations

**Benefits:**
- Reduces complexity by ~130 lines
- Separates hit logic from weapon class

#### 6.1.3 Create `WeaponCombatContext` Class
**Purpose**: Cache frequently accessed data

**Properties:**
```csharp
public class WeaponCombatContext
{
    public bool IsPlayerMobile { get; }
    public bool IsBaseCreature { get; }
    public bool IsMounted { get; }
    public bool IsSoulBound { get; }
    public bool IsAvatar { get; }
    public bool IsInMidland { get; }
    public double Agility { get; }
    // ... cached values
}
```

**Benefits:**
- Eliminates repeated type checks
- Reduces boxing operations
- Improves performance

#### 6.1.4 Create `WeaponMaterialDamageTable` Class
**Purpose**: Replace large switch with dictionary lookup

**Implementation:**
```csharp
private static readonly Dictionary<CraftResource, int> MaterialDamageTable = 
    new Dictionary<CraftResource, int>
{
    { CraftResource.DullCopper, 1 },
    { CraftResource.ShadowIron, 2 },
    // ... all materials
};
```

**Benefits:**
- O(1) lookup vs O(n) switch
- Easier to maintain
- Reduces complexity from 41 to 2

### Phase 2: Extract Combat Handlers (Medium Risk)

#### 6.2.1 Create `CombatEffectHandler` Class
**Purpose**: Handle all special effects

**Methods to Extract:**
- Area attack logic
- Magic effect triggers (fireball, lightning, etc.)
- Poison application
- Life/mana/stamina leech

**Benefits:**
- Reduces `OnHit` complexity significantly
- Makes effects testable independently

#### 6.2.2 Create `CombatDamageModifier` Class
**Purpose**: Handle damage multipliers

**Methods to Extract:**
- Slayer bonus calculation
- EnemyOfOne bonus
- Pack instinct bonus
- Honor bonuses
- Sneak attack bonus

**Benefits:**
- Separates damage modification from core calculation
- Makes modifiers easier to add/remove

#### 6.2.3 Create `CombatDefenseHandler` Class
**Purpose**: Handle defensive mechanics

**Methods to Extract:**
- `CheckParry`
- `CheckDodge`
- `AbsorbDamageAOS`
- Mount deflection logic

**Benefits:**
- Reduces complexity in `OnHit`
- Makes defense mechanics testable

### Phase 3: Refactor Large Methods (High Risk)

#### 6.3.1 Refactor `OnHit` Method
**Current**: 653 lines, Complexity 86

**Breakdown:**
1. Extract `ProcessSneakAttack()` (lines 1645-1673)
2. Extract `ProcessMirrorImage()` (lines 1675-1691)
3. Extract `ProcessMountDeflection()` (lines 1701-1720)
4. Extract `ProcessDodge()` (lines 1724-1749)
5. Extract `ApplyBalanceModifier()` (lines 1758-1785)
6. Extract `CalculateDamageMultipliers()` (lines 1787-1910)
7. Extract `ApplyCombatEffects()` (lines 2130-2289)

**Target**: 8-10 methods, each < 50 lines, complexity < 10

#### 6.3.2 Refactor `OnCraft` Method
**Current**: 309 lines, Complexity 46

**Breakdown:**
1. Extract `ApplyCraftResourceBonuses()` (switch statements)
2. Extract `ApplyQualityBonuses()`
3. Extract `ApplyRunicToolAttributes()`

**Target**: 3-4 methods, each < 80 lines

#### 6.3.3 Refactor `GetProperties` Method
**Current**: 255 lines, Complexity 41

**Breakdown:**
1. Extract `AddWeaponAttributes()`
2. Extract `AddElementalDamages()`
3. Extract `AddSkillRequirements()`

**Target**: 4-5 methods, each < 60 lines

### Phase 4: Performance Optimizations (Low Risk)

#### 6.4.1 Cache Type Checks
```csharp
// Before:
if (attacker is PlayerMobile && ((PlayerMobile)attacker).Mounted)

// After:
var context = GetCombatContext(attacker);
if (context.IsPlayerMobile && context.IsMounted)
```

#### 6.4.2 Replace String Concatenation
```csharp
// Before:
attacker.SendMessage("You perform a sneak attack for " + tellBonus + "% more damage!");

// After:
attacker.SendMessage(string.Format("You perform a sneak attack for {0}% more damage!", tellBonus));
// Or use StringBuilder for complex cases
```

#### 6.4.3 Optimize Material Damage Lookup
```csharp
// Replace switch with Dictionary (already planned in Phase 1)
```

#### 6.4.4 Cache Repeated Calculations
```csharp
// Cache agility, midland status, etc. in CombatContext
```

### Phase 5: Code Quality Improvements (Low Risk)

#### 6.5.1 Extract Magic Numbers to Constants
```csharp
private const double STRENGTH_BONUS_SCALAR = 0.100;
private const double STRENGTH_BONUS_OFFSET = 3.00;
private const double ANATOMY_BONUS_SCALAR = 0.300;
private const double ANATOMY_BONUS_OFFSET = 3.00;
private const double TACTICS_BONUS_SCALAR = 0.300;
private const double TACTICS_BONUS_OFFSET = 3.00;
private const int DAMAGE_CAP_REGULAR = 53;
private const int DAMAGE_CAP_AVATAR = 77;
// ... etc
```

#### 6.5.2 Remove Dead Code
- Remove commented-out code blocks
- Remove `#if false` sections if not needed
- Clean up unused variables

#### 6.5.3 Improve Naming
- Use consistent naming conventions
- Add XML documentation to public methods
- Clarify ambiguous variable names

#### 6.5.4 Add Unit Tests
- Test damage calculations
- Test hit chance calculations
- Test special effects
- Test edge cases

---

## 6.5 DRY Refactoring - Centralized Generic Functions

### 6.5.1 Generic Type Checking & Property Access

#### Create `MobileExtensions` Helper Class
**Purpose**: Centralize all Mobile type checking and property access

```csharp
public static class MobileExtensions
{
    // Generic type checking
    public static bool IsPlayerMobile(this Mobile mobile)
    {
        return mobile is PlayerMobile;
    }
    
    public static bool IsBaseCreature(this Mobile mobile)
    {
        return mobile is BaseCreature;
    }
    
    // Generic property access with caching
    public static PlayerMobile AsPlayerMobile(this Mobile mobile)
    {
        return mobile as PlayerMobile;
    }
    
    public static BaseCreature AsBaseCreature(this Mobile mobile)
    {
        return mobile as BaseCreature;
    }
    
    // Centralized property access
    public static bool IsMounted(this Mobile mobile)
    {
        if (mobile is PlayerMobile)
            return ((PlayerMobile)mobile).Mounted;
        if (mobile is BaseCreature)
            return ((BaseCreature)mobile).Mounted;
        return false;
    }
    
    public static bool IsSoulBound(this Mobile mobile)
    {
        PlayerMobile pm = mobile as PlayerMobile;
        return pm != null && pm.SoulBound;
    }
    
    public static bool IsAvatar(this Mobile mobile)
    {
        PlayerMobile pm = mobile as PlayerMobile;
        return pm != null && pm.Avatar;
    }
    
    public static double GetAgility(this Mobile mobile)
    {
        if (mobile is PlayerMobile)
            return ((PlayerMobile)mobile).Agility();
        if (mobile is BaseCreature)
            return ((BaseCreature)mobile).Agility();
        return 0.0;
    }
    
    public static bool IsInMidland(this Mobile mobile)
    {
        return AdventuresFunctions.IsInMidland(mobile);
    }
    
    public static IMount GetMount(this Mobile mobile)
    {
        if (mobile is BaseCreature)
            return ((BaseCreature)mobile).Mount;
        if (mobile is PlayerMobile)
            return ((PlayerMobile)mobile).Mount;
        return null;
    }
    
    public static Mobile GetEffectiveAttacker(this Mobile attacker)
    {
        BaseCreature bc = attacker as BaseCreature;
        if (bc != null)
        {
            Mobile master = bc.GetMaster();
            if (master is PlayerMobile)
                return master;
        }
        return attacker;
    }
}
```

**Usage Example:**
```csharp
// Before (55+ instances):
if (attacker is PlayerMobile && ((PlayerMobile)attacker).Mounted)

// After:
if (attacker.IsPlayerMobile() && attacker.IsMounted())
```

**Benefits:**
- Eliminates 100+ type checks and casts
- Single point of maintenance
- Better performance (can add caching later)
- More readable code

#### Create `WeaponTypeExtensions` Helper Class
**Purpose**: Centralize weapon type checking

```csharp
public static class WeaponTypeExtensions
{
    public static bool IsRanged(this BaseWeapon weapon)
    {
        return weapon is BaseRanged;
    }
    
    public static bool IsHarpoon(this BaseWeapon weapon)
    {
        return weapon is Harpoon || weapon is GiftHarpoon || weapon is LevelHarpoon;
    }
    
    public static bool IsWizardStaff(this BaseWeapon weapon)
    {
        return weapon is WizardWand || 
               weapon is BaseWizardStaff || 
               weapon is BaseLevelStave || 
               weapon is BaseGiftStave || 
               weapon is GiftScepter || 
               weapon is LevelScepter || 
               weapon is Scepter;
    }
    
    public static bool IsWoodRanged(this BaseWeapon weapon)
    {
        return weapon is BaseRanged && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem(weapon);
    }
    
    public static bool SupportsBushido(this BaseWeapon weapon)
    {
        return weapon.Type == WeaponType.Axe || 
               weapon.Type == WeaponType.Slashing || 
               weapon.Type == WeaponType.Polearm;
    }
    
    public static bool SupportsLumberjacking(this BaseWeapon weapon)
    {
        return weapon.Type == WeaponType.Axe;
    }
    
    public static bool SupportsMining(this BaseWeapon weapon)
    {
        return weapon.Type == WeaponType.Bashing;
    }
}
```

**Usage Example:**
```csharp
// Before:
if ( Type != WeaponType.Axe && Type != WeaponType.Slashing && Type != WeaponType.Polearm )
    bushidoBonus = 0.0;

// After:
if (!this.SupportsBushido())
    bushidoBonus = 0.0;
```

### 6.5.2 Generic Bonus Calculation System

#### Create `SkillBonusCalculator` Class
**Purpose**: Centralize all skill bonus calculations using configuration

```csharp
public class SkillBonusConfig
{
    public SkillName Skill { get; set; }
    public double Scalar { get; set; }
    public double Threshold { get; set; }
    public double Offset { get; set; }
    public Func<BaseWeapon, bool> WeaponTypeValidator { get; set; }
}

public static class SkillBonusCalculator
{
    private static readonly Dictionary<SkillName, SkillBonusConfig> SkillConfigs = 
        new Dictionary<SkillName, SkillBonusConfig>
    {
        { SkillName.Anatomy, new SkillBonusConfig 
            { Skill = SkillName.Anatomy, Scalar = 0.300, Threshold = 100.0, Offset = 3.00, WeaponTypeValidator = null } },
        { SkillName.Tactics, new SkillBonusConfig 
            { Skill = SkillName.Tactics, Scalar = 0.300, Threshold = 100.0, Offset = 3.00, WeaponTypeValidator = null } },
        { SkillName.Lumberjacking, new SkillBonusConfig 
            { Skill = SkillName.Lumberjacking, Scalar = 0.200, Threshold = 100.0, Offset = 10.00, 
              WeaponTypeValidator = w => w.SupportsLumberjacking() } },
        { SkillName.Mining, new SkillBonusConfig 
            { Skill = SkillName.Mining, Scalar = 0.200, Threshold = 100.0, Offset = 10.00,
              WeaponTypeValidator = w => w.SupportsMining() } },
        { SkillName.Fishing, new SkillBonusConfig 
            { Skill = SkillName.Fishing, Scalar = 0.200, Threshold = 100.0, Offset = 10.00,
              WeaponTypeValidator = w => w.IsHarpoon() } },
        { SkillName.Bushido, new SkillBonusConfig 
            { Skill = SkillName.Bushido, Scalar = 0.625, Threshold = 100.0, Offset = 6.25,
              WeaponTypeValidator = w => w.SupportsBushido() } },
        { SkillName.Ninjitsu, new SkillBonusConfig 
            { Skill = SkillName.Ninjitsu, Scalar = 0.625, Threshold = 100.0, Offset = 6.25, WeaponTypeValidator = null } },
        { SkillName.Necromancy, new SkillBonusConfig 
            { Skill = SkillName.Necromancy, Scalar = 0.625, Threshold = 100.0, Offset = 6.25,
              WeaponTypeValidator = w => w.IsWizardStaff() } },
        { SkillName.Magery, new SkillBonusConfig 
            { Skill = SkillName.Magery, Scalar = 0.625, Threshold = 100.0, Offset = 6.25,
              WeaponTypeValidator = w => w.IsWizardStaff() } },
        { SkillName.Fletching, new SkillBonusConfig 
            { Skill = SkillName.Fletching, Scalar = 0.625, Threshold = 100.0, Offset = 6.25,
              WeaponTypeValidator = w => w.IsWoodRanged() } }
    };
    
    public static double CalculateSkillBonus(Mobile attacker, SkillName skill, BaseWeapon weapon)
    {
        if (!SkillConfigs.ContainsKey(skill))
            return 0.0;
            
        SkillBonusConfig config = SkillConfigs[skill];
        
        // Check weapon type validator
        if (config.WeaponTypeValidator != null && !config.WeaponTypeValidator(weapon))
            return 0.0;
        
        double skillValue = attacker.Skills[skill].Value;
        return CalculateBonus(skillValue, config.Scalar, config.Threshold, config.Offset);
    }
    
    public static double CalculateStrengthBonus(int strength)
    {
        return CalculateBonus(strength, 0.100, 100.0, 3.00);
    }
    
    private static double CalculateBonus(double value, double scalar, double threshold, double offset)
    {
        double bonus = value * scalar;
        if (value >= threshold)
            bonus += offset;
        return bonus / 100.0;
    }
    
    public static Dictionary<SkillName, double> CalculateAllSkillBonuses(Mobile attacker, BaseWeapon weapon)
    {
        Dictionary<SkillName, double> bonuses = new Dictionary<SkillName, double>();
        
        // Strength bonus (special case)
        bonuses[SkillName.Strength] = CalculateStrengthBonus(attacker.Str);
        
        // All skill bonuses
        foreach (SkillName skill in SkillConfigs.Keys)
        {
            bonuses[skill] = CalculateSkillBonus(attacker, skill, weapon);
        }
        
        return bonuses;
    }
}
```

**Usage Example:**
```csharp
// Before (12 separate calls):
double strengthBonus = GetBonus( attacker.Str, 0.100, 100.0, 3.00 );
double anatomyBonus = GetBonus( attacker.Skills[SkillName.Anatomy].Value, 0.300, 100.0, 3.00 );
double tacticsBonus = GetBonus( attacker.Skills[SkillName.Tactics].Value, 0.300, 100.0, 3.00 );
// ... 9 more

// After (single call):
var bonuses = SkillBonusCalculator.CalculateAllSkillBonuses(attacker, this);
double totalBonus = bonuses[SkillName.Strength] + 
                    bonuses[SkillName.Anatomy] + 
                    bonuses[SkillName.Tactics] + 
                    // ... etc
```

### 6.5.3 Generic Diminishing Returns Application

#### Create `DamageCapCalculator` Class
**Purpose**: Centralize diminishing returns logic

```csharp
public static class DamageCapCalculator
{
    private const int CAP_REGULAR_PLAYER = 53;
    private const int CAP_AVATAR = 77;
    private const int CAP_CREATURE = 225;
    private const int DIMINISHING_RETURNS_FACTOR = 10;
    
    public static double ApplyDiminishingReturns(Mobile attacker, double damage)
    {
        int cap;
        
        if (attacker.IsPlayerMobile() && attacker.IsAvatar())
            cap = CAP_AVATAR;
        else if (attacker.IsPlayerMobile())
            cap = CAP_REGULAR_PLAYER;
        else
            cap = CAP_CREATURE;
        
        return (double)AdventuresFunctions.DiminishingReturns((int)damage, cap, DIMINISHING_RETURNS_FACTOR);
    }
}
```

**Usage Example:**
```csharp
// Before:
if (attacker is PlayerMobile && ((PlayerMobile)attacker).Avatar)
    total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 77, 10 ) );
else if (attacker is PlayerMobile)
    total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 53, 10 ) );
else
    total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 225, 10 ) );

// After:
total = DamageCapCalculator.ApplyDiminishingReturns(attacker, total);
```

### 6.5.4 Generic Switch Statement Replacements

#### Create `WeaponBonusTables` Class
**Purpose**: Replace switch statements with dictionary lookups

```csharp
public static class WeaponBonusTables
{
    private static readonly Dictionary<WeaponQuality, int> QualityBonuses = 
        new Dictionary<WeaponQuality, int>
    {
        { WeaponQuality.Low, -10 },
        { WeaponQuality.Regular, 0 },
        { WeaponQuality.Exceptional, 10 }
    };
    
    private static readonly Dictionary<WeaponDamageLevel, int> DamageLevelBonuses = 
        new Dictionary<WeaponDamageLevel, int>
    {
        { WeaponDamageLevel.Regular, 0 },
        { WeaponDamageLevel.Ruin, 5 },
        { WeaponDamageLevel.Might, 10 },
        { WeaponDamageLevel.Force, 12 },
        { WeaponDamageLevel.Power, 15 },
        { WeaponDamageLevel.Vanq, 18 }
    };
    
    private static readonly Dictionary<WeaponAccuracyLevel, int> AccuracyLevelBonuses = 
        new Dictionary<WeaponAccuracyLevel, int>
    {
        { WeaponAccuracyLevel.Regular, 0 },
        { WeaponAccuracyLevel.Accurate, 2 },
        { WeaponAccuracyLevel.Surpassingly, 4 },
        { WeaponAccuracyLevel.Eminently, 6 },
        { WeaponAccuracyLevel.Exceedingly, 8 },
        { WeaponAccuracyLevel.Supremely, 10 }
    };
    
    public static int GetQualityBonus(WeaponQuality quality)
    {
        return QualityBonuses.ContainsKey(quality) ? QualityBonuses[quality] : 0;
    }
    
    public static int GetDamageLevelBonus(WeaponDamageLevel level)
    {
        return DamageLevelBonuses.ContainsKey(level) ? DamageLevelBonuses[level] : 0;
    }
    
    public static int GetAccuracyLevelBonus(WeaponAccuracyLevel level)
    {
        return AccuracyLevelBonuses.ContainsKey(level) ? AccuracyLevelBonuses[level] : 0;
    }
}
```

**Usage Example:**
```csharp
// Before (switch statement):
switch ( m_Quality )
{
    case WeaponQuality.Low: bonus -= 10; break;
    case WeaponQuality.Exceptional: bonus += 10; break;
}

// After:
bonus += WeaponBonusTables.GetQualityBonus(m_Quality);
```

### 6.5.5 Generic Property List Generation

#### Create `PropertyListBuilder` Class
**Purpose**: Centralize property list generation patterns

```csharp
public static class PropertyListBuilder
{
    public static void AddPropertyIfNonZero(ObjectPropertyList list, int value, int cliloc, bool checkMidland, object context)
    {
        if (value != 0 && (!checkMidland || !AdventuresFunctions.IsInMidland(context)))
            list.Add(cliloc, value.ToString());
    }
    
    public static void AddPropertyIfNonZero(ObjectPropertyList list, int value, int cliloc, BaseWeapon weapon)
    {
        AddPropertyIfNonZero(list, value, cliloc, true, weapon);
    }
    
    public static void AddPropertyIfNonZero(ObjectPropertyList list, int value, int cliloc)
    {
        AddPropertyIfNonZero(list, value, cliloc, false, null);
    }
}
```

**Usage Example:**
```csharp
// Before (50+ similar patterns):
if ( (prop = m_AosAttributes.WeaponDamage) != 0 && !(AdventuresFunctions.IsInMidland((object)this)))
    list.Add( 1060401, prop.ToString() );

// After:
PropertyListBuilder.AddPropertyIfNonZero(list, m_AosAttributes.WeaponDamage, 1060401, this);
```

### 6.5.6 Generic Stat Mod Creation

#### Create `StatModHelper` Class
**Purpose**: Centralize stat mod creation

```csharp
public static class StatModHelper
{
    public static void ApplyStatMods(Mobile mobile, string baseName, int strBonus, int dexBonus, int intBonus)
    {
        if (strBonus != 0)
            mobile.AddStatMod(new StatMod(StatType.Str, baseName + "Str", strBonus, TimeSpan.Zero));
        if (dexBonus != 0)
            mobile.AddStatMod(new StatMod(StatType.Dex, baseName + "Dex", dexBonus, TimeSpan.Zero));
        if (intBonus != 0)
            mobile.AddStatMod(new StatMod(StatType.Int, baseName + "Int", intBonus, TimeSpan.Zero));
    }
    
    public static void ApplyStatModsFromWeapon(Mobile mobile, BaseWeapon weapon, int strBonus, int dexBonus, int intBonus)
    {
        string modName = weapon.Serial.ToString();
        ApplyStatMods(mobile, modName, strBonus, dexBonus, intBonus);
    }
}
```

**Usage Example:**
```csharp
// Before:
string modName = this.Serial.ToString();
if ( strBonus != 0 )
    m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );
if ( dexBonus != 0 )
    m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );
if ( intBonus != 0 )
    m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );

// After:
StatModHelper.ApplyStatModsFromWeapon(m, this, strBonus, dexBonus, intBonus);
```

### 6.5.7 Summary of DRY Improvements

| Pattern | Instances | Solution | Reduction |
|---------|-----------|----------|-----------|
| Type Checks | 100+ | `MobileExtensions` | 90% |
| Property Access | 150+ | `MobileExtensions` | 85% |
| Skill Bonuses | 12 | `SkillBonusCalculator` | 100% |
| Switch Statements | 10+ | Dictionary Lookups | 100% |
| Diminishing Returns | 3+ | `DamageCapCalculator` | 100% |
| Property Lists | 50+ | `PropertyListBuilder` | 80% |
| Stat Mods | 6+ | `StatModHelper` | 100% |

**Total Code Reduction**: ~400-500 lines of duplicated code eliminated

**Performance Improvement**: 
- Eliminates 200+ type checks per combat calculation
- Eliminates 100+ boxing operations
- O(1) dictionary lookups vs O(n) switch statements

---

## 7. Implementation Priority

### High Priority (Do First) - DRY & Performance
1. ✅ **Create `MobileExtensions` helper class** (DRY - Eliminates 100+ type checks)
2. ✅ **Create `WeaponTypeExtensions` helper class** (DRY - Eliminates 15+ weapon type checks)
3. ✅ **Create `WeaponMaterialDamageTable`** (Performance - O(1) lookup vs O(n) switch)
4. ✅ **Create `WeaponBonusTables`** (DRY - Replace 10+ switch statements with dictionaries)
5. ✅ **Create `SkillBonusCalculator`** (DRY - Centralize 12 skill bonus calculations)
6. ✅ **Create `DamageCapCalculator`** (DRY - Centralize diminishing returns logic)
7. ✅ **Extract constants for magic numbers** (Readability)
8. ✅ **Create `WeaponCombatContext`** (Performance + Readability - Cache type checks)

### Medium Priority (Do Second)
9. **Create `PropertyListBuilder`** (DRY - Centralize 50+ property list patterns)
10. **Create `StatModHelper`** (DRY - Centralize stat mod creation)
11. Extract `WeaponDamageCalculator` (Uses SkillBonusCalculator)
12. Extract `WeaponHitChanceCalculator`
13. Extract `CombatEffectHandler`
14. Refactor `OnCraft` method

### Low Priority (Do Third)
15. Extract `CombatDefenseHandler`
16. Refactor `GetProperties` method (Uses PropertyListBuilder)
17. Add comprehensive unit tests
18. Performance profiling and optimization

---

## 8. Risk Assessment

### Low Risk Refactorings
- ✅ Extracting constants
- ✅ Creating helper classes
- ✅ Caching type checks
- ✅ Replacing switch with dictionary

**Mitigation**: These are additive changes, don't modify existing logic

### Medium Risk Refactorings
- Extracting damage calculation classes
- Extracting combat handlers

**Mitigation**: 
- Maintain same method signatures
- Extensive testing required
- Gradual migration

### High Risk Refactorings
- Breaking up `OnHit` method
- Refactoring serialization

**Mitigation**:
- Create comprehensive test suite first
- Use feature flags if possible
- Incremental refactoring with validation at each step

---

## 9. Expected Benefits

### Performance Improvements
- **20-30% faster** combat calculations (cached type checks)
- **Reduced memory allocations** (eliminate boxing, string concatenation)
- **Better CPU cache usage** (smaller, focused methods)

### Maintainability Improvements
- **50% reduction** in average method complexity
- **Easier to test** (smaller, focused units)
- **Easier to extend** (clear separation of concerns)
- **Better documentation** (self-documenting code structure)

### Code Quality Improvements
- **Reduced duplication** (DRY principle)
- **Better readability** (smaller methods, clear names)
- **Easier debugging** (isolated concerns)

---

## 10. Testing Strategy

### 10.1 Unit Tests Required

1. **Damage Calculation Tests**:
   - Test all skill bonus formulas
   - Test quality/damage level modifiers
   - Test diminishing returns caps
   - Test material bonuses

2. **Hit Chance Tests**:
   - Test AOS vs Old system
   - Test all bonus sources
   - Test caps

3. **Special Effects Tests**:
   - Test each effect trigger
   - Test effect stacking
   - Test edge cases

### 10.2 Integration Tests Required

1. **Full Combat Flow**:
   - Test complete OnSwing → OnHit flow
   - Test with different weapon types
   - Test with different character types

2. **Compatibility Tests**:
   - Ensure AOS and Old systems work
   - Test serialization/deserialization
   - Test crafting integration

---

## 11. Migration Plan

### Step 1: Preparation (Week 1)
1. Create comprehensive test suite
2. Document current behavior
3. Create helper classes (non-breaking)

### Step 2: Low-Risk Refactorings (Week 2)
1. Extract constants
2. Create CombatContext
3. Replace switch with dictionary
4. Add caching

### Step 3: Medium-Risk Refactorings (Week 3-4)
1. Extract damage calculator
2. Extract hit chance calculator
3. Extract effect handlers
4. Extensive testing

### Step 4: High-Risk Refactorings (Week 5-6)
1. Break up OnHit method
2. Refactor OnCraft
3. Refactor GetProperties
4. Final testing and validation

### Step 5: Cleanup (Week 7)
1. Remove dead code
2. Improve documentation
3. Performance profiling
4. Final optimizations

---

## 12. Success Metrics

### Complexity Metrics
- **Target**: Average method complexity < 10
- **Target**: No method > 20 complexity
- **Target**: Class total complexity < 200

### Performance Metrics
- **Target**: 20% reduction in combat calculation time
- **Target**: 30% reduction in memory allocations per hit
- **Target**: No regression in existing functionality

### Code Quality Metrics
- **Target**: 30% reduction in code duplication
- **Target**: 50% reduction in average method length
- **Target**: 100% test coverage for critical paths

---

## 13. Notes & Considerations

### 13.1 C# 6.0 Compatibility
- Must maintain C# 6.0 syntax (no pattern matching, no tuples, etc.)
- Use traditional type checking: `if (x is Type) { Type t = (Type)x; }`

### 13.2 Backward Compatibility
- Must maintain existing method signatures
- Must preserve serialization format
- Must not break existing weapon subclasses

### 13.3 Game Balance
- Damage calculations are critical to game balance
- Any changes must be validated against current behavior
- Consider A/B testing for major changes

---

## Conclusion

The `BaseWeapon` class is a **critical component** that requires **systematic refactoring** to improve:
- **Maintainability** (reduce complexity)
- **Performance** (optimize hot paths)
- **Readability** (extract and organize code)
- **Testability** (isolate concerns)

The refactoring should be done **incrementally** with **extensive testing** at each phase to ensure no regressions.

**Estimated Total Effort**: 6-8 weeks  
**Risk Level**: Medium-High  
**Priority**: High (affects core game mechanics)

