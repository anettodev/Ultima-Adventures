# Spell System Folder Structure Guide (SoC)

## Overview

This document describes the recommended folder structure for the spell system following Separation of Concerns (SoC) principles, explains the purpose of each file, and provides guidance on cyclomatic complexity and performance optimization.

## Current Structure Issues

- All files in `Base/` folder mixed together
- Hard to find specific functionality
- No clear separation of concerns
- Helper classes mixed with core classes

## Recommended Folder Structure

```
Scripts/Engines/Magic/
├── Base/
│   ├── Core/                          # Core spell classes
│   │   ├── Spell.cs
│   │   ├── MagerySpell.cs
│   │   └── SpecialMove.cs
│   │
│   ├── Enums/                         # Type definitions
│   │   ├── SpellCircle.cs
│   │   ├── SpellState.cs
│   │   └── DisturbType.cs
│   │
│   ├── Data/                          # Data structures
│   │   ├── SpellInfo.cs
│   │   └── Reagent.cs
│   │
│   ├── Registry/                      # Registration system
│   │   ├── SpellRegistry.cs
│   │   └── Initializer.cs
│   │
│   ├── Calculations/                  # Calculation logic
│   │   ├── SpellDamageCalculator.cs
│   │   └── SpellHelper.cs
│   │
│   ├── Validation/                    # Validation logic
│   │   └── SpellCastingValidator.cs
│   │
│   ├── Movement/                      # Movement handling
│   │   └── SpellMovementHandler.cs
│   │
│   ├── Modifiers/                     # Effect modifiers
│   │   └── MidlandSpellModifier.cs
│   │
│   ├── Timers/                        # Timer classes
│   │   └── UnsummonTimer.cs
│   │
│   └── Constants/                     # Constants
│       └── SpellConstants.cs
│
└── [Spell implementation folders: Magery 1st-8th, Necromancy, etc.]
```

## File Descriptions

### Core Classes

#### `Spell.cs`
**Purpose:** Abstract base class for all spells in the system  
**Responsibilities:**
- Manages spell lifecycle (casting, sequencing, finishing)
- Handles spell state transitions
- Manages timers (cast timer, animation timer)
- Provides virtual methods for spell-specific behavior
- Delegates to helper classes for calculations, validation, and movement

**Key Features:**
- Spell state management (None, Casting, Sequencing)
- Mana consumption and scaling
- Reagent consumption
- Spell fizzle and disturbance handling
- Mantra speech (including drunk state handling)

#### `MagerySpell.cs`
**Purpose:** Base class for all magery circle spells  
**Responsibilities:**
- Extends `Spell` with magery-specific functionality
- Manages spell circle (First through Eighth)
- Calculates mana costs based on circle
- Handles resistance calculations
- Provides circle-based skill requirements

**Key Features:**
- Circle-based mana table
- Resistance percentage calculations
- Cast delay based on circle
- Arcane gem support for reagent substitution

#### `SpecialMove.cs`
**Purpose:** Base class for special combat moves (Bushido, Ninjitsu)  
**Responsibilities:**
- Manages special move context and timers
- Validates move requirements (skill, mana)
- Handles move execution and cleanup
- Tracks active moves per mobile

**Key Features:**
- Move validation and skill checks
- Mana scaling with Lower Mana Cost
- Context management for delayed moves
- Move clearing and state management

### Enums

#### `SpellCircle.cs`
**Purpose:** Defines magery spell circles  
**Values:** First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth  
**Usage:** Used to determine mana costs, skill requirements, and cast delays

#### `SpellState.cs`
**Purpose:** Defines spell casting states  
**Values:**
- `None` - No active spell
- `Casting` - Spell is being cast (can be interrupted)
- `Sequencing` - Casting complete, waiting for target/sequence

#### `DisturbType.cs`
**Purpose:** Defines types of spell disturbances  
**Values:** Unspecified, EquipRequest, UseRequest, Hurt, Kill, NewCast  
**Usage:** Determines how spell interruption is handled

### Data Classes

#### `SpellInfo.cs`
**Purpose:** Contains metadata for spell definitions  
**Properties:**
- Name, Mantra (spoken words)
- Reagents and amounts required
- Animation action ID
- Hand effect IDs (left/right)
- AllowTown flag

**Usage:** Created once per spell type, passed to Spell constructor

#### `Reagent.cs`
**Purpose:** Static class providing reagent type definitions  
**Features:**
- Defines all 13 reagent types
- Provides static properties for each reagent
- Returns defensive copy of types array
- Allows runtime reagent substitution via setters

### Registry System

#### `SpellRegistry.cs`
**Purpose:** Central registry for all spells and special moves  
**Responsibilities:**
- Maps spell IDs to spell types
- Provides spell instance creation
- Manages special move registry
- Caches spell name lookups for performance

**Key Features:**
- O(1) spell lookup by ID
- Cached name-to-type lookups
- Lazy spell count calculation
- Type-to-ID reverse lookup

#### `Initializer.cs`
**Purpose:** Registers all spells at server startup  
**Responsibilities:**
- Calls `SpellRegistry.Register()` for all spells
- Organizes registration by spell type
- Handles all spell circles and special spell types

### Calculation Classes

#### `SpellDamageCalculator.cs`
**Purpose:** Handles all spell damage calculations  
**Responsibilities:**
- NMS (New Magic System) damage calculations
- AOS (Age of Shadows) damage calculations
- Damage scalar calculations (resistance, slayer, etc.)
- EvalInt and Inscription bonus calculations

**Key Methods:**
- `GetNMSDamage()` - New magic system formula
- `GetNewAosDamage()` - Age of Shadows formula
- `GetDamageScalar()` - Applies all damage modifiers
- `GetSlayerDamageScalar()` - Slayer weapon bonuses

#### `SpellHelper.cs`
**Purpose:** General utility functions for spells  
**Responsibilities:**
- Travel/teleport validation
- Multi/house checking
- Stat bonus calculations
- Transformation helpers
- Various spell utility functions

### Validation Classes

#### `SpellCastingValidator.cs`
**Purpose:** Validates spell casting conditions  
**Responsibilities:**
- Checks if caster can cast (alive, not frozen, etc.)
- Validates transformation restrictions
- Checks spell cooldown
- Validates calm/peace status
- Configures step tracking for movement

**Key Methods:**
- `ValidateCanCast()` - Main validation entry point
- `CheckCast()` - Basic cast validation
- Helper methods for specific checks

### Movement Classes

#### `SpellMovementHandler.cs`
**Purpose:** Handles movement during spell casting  
**Responsibilities:**
- Calculates allowed steps based on Magery skill
- Tracks and deducts steps during casting
- Validates spell hold time (concentration)
- Notifies when concentration is lost

**Key Methods:**
- `CalculateAllowedStepsByMagery()` - Step calculation
- `ProcessRemainingSteps()` - Step deduction
- `ValidateSpellHoldTime()` - Concentration check

### Modifier Classes

#### `MidlandSpellModifier.cs`
**Purpose:** Applies Midland region-specific spell modifications  
**Responsibilities:**
- Checks if caster is in Midland region
- Calculates Lucidity-based bonuses
- Applies Fast Cast and Fast Cast Recovery bonuses
- Modifies damage calculations for Midland

**Key Features:**
- Lucidity threshold checks (Low, Med, High)
- Fast Cast Recovery bonus (up to +3)
- Fast Cast bonus (up to +2)
- Damage modification with Lucidity multiplier

### Timer Classes

#### `UnsummonTimer.cs`
**Purpose:** Timer for automatically unsummoning creatures  
**Responsibilities:**
- Deletes summoned creature after delay
- Simple timer wrapper for creature cleanup

### Constants

#### `SpellConstants.cs`
**Purpose:** Centralized constants for the spell system  
**Categories:**
- Timing constants (delays, recovery)
- Damage calculation constants
- Skill thresholds for movement
- Step costs and limits
- Drunk mantra constants
- Message colors
- Midland lucidity thresholds
- Mana/reagent caps
- Pre-calculated TimeSpans

## Cyclomatic Complexity Management

### What is Cyclomatic Complexity?

Cyclomatic complexity measures the number of linearly independent paths through code. High complexity makes code harder to understand, test, and maintain.

### Complexity Guidelines

- **1-10:** Simple, easy to understand
- **11-20:** Moderate complexity, acceptable
- **21-30:** High complexity, consider refactoring
- **31+:** Very high complexity, refactor immediately

### Refactoring Strategies

#### 1. Extract Methods
**Before:**
```csharp
public int CalculateDamage() {
    if (condition1) {
        if (condition2) {
            // complex logic
        }
    }
    // more nested conditions
}
```

**After:**
```csharp
public int CalculateDamage() {
    if (!ValidateConditions()) return 0;
    return ApplyDamageModifiers(GetBaseDamage());
}

private bool ValidateConditions() { /* ... */ }
private int GetBaseDamage() { /* ... */ }
private int ApplyDamageModifiers(int base) { /* ... */ }
```

#### 2. Use Early Returns
**Before:**
```csharp
if (condition1) {
    if (condition2) {
        if (condition3) {
            // do work
        }
    }
}
```

**After:**
```csharp
if (!condition1) return;
if (!condition2) return;
if (!condition3) return;
// do work
```

#### 3. Extract Helper Classes
- Move complex calculations to dedicated classes
- Separate validation logic
- Isolate movement handling

#### 4. Use Strategy Pattern
For complex conditional logic, use strategy pattern:
```csharp
// Instead of large switch/if chains
IDamageCalculator calculator = GetDamageCalculator(spellType);
int damage = calculator.Calculate(spell, target);
```

### Current Complexity Status

After refactoring:
- **Spell.cs:** Reduced from ~1,795 lines to ~1,384 lines
- **Damage calculations:** Extracted to `SpellDamageCalculator` (complexity ~8-12 per method)
- **Validation:** Extracted to `SpellCastingValidator` (complexity ~5-8 per method)
- **Movement:** Extracted to `SpellMovementHandler` (complexity ~3-6 per method)

### Monitoring Complexity

**Tools:**
- Visual Studio Code Metrics
- NDepend
- SonarQube

**Best Practices:**
- Review complexity during code reviews
- Set complexity thresholds in CI/CD
- Refactor when complexity exceeds 20

## Performance Optimization Workflow

### 1. Identify Hot Paths

**Common Hot Paths in Spell System:**
- Spell lookup (`SpellRegistry.NewSpell()`)
- Damage calculations (called every spell cast)
- Validation checks (called before every cast)
- Movement tracking (called on every step)

### 2. Optimization Techniques

#### Caching
**Example: Spell Name Lookup**
```csharp
// Before: O(n) search every time
for (int i = 0; i < circles.Length; ++i) {
    Type t = FindType(circles[i], name);
}

// After: O(1) lookup after first search
if (cache.TryGetValue(name, out type)) {
    return CreateSpell(type);
}
```

#### Lazy Evaluation
**Example: Spell Count**
```csharp
// Before: Recalculated every access
public int Count {
    get {
        int count = 0;
        for (int i = 0; i < types.Length; ++i) {
            if (types[i] != null) count++;
        }
        return count;
    }
}

// After: Calculated once, marked dirty on changes
private int m_Count = -1;
private bool m_CountDirty = true;

public int Count {
    get {
        if (m_CountDirty) {
            m_Count = CalculateCount();
            m_CountDirty = false;
        }
        return m_Count;
    }
}
```

#### Dictionary Lookups
**Before:**
```csharp
if (dictionary.ContainsKey(key)) {
    value = dictionary[key];
}
```

**After:**
```csharp
if (dictionary.TryGetValue(key, out value)) {
    // use value
}
```

#### Avoid Allocations in Hot Paths
- Cache frequently accessed values
- Reuse StringBuilder instances
- Pool timer objects where possible
- Avoid LINQ in performance-critical code

### 3. Performance Monitoring

**Key Metrics:**
- Spell lookup time (should be < 1ms)
- Damage calculation time (should be < 0.5ms)
- Validation time (should be < 0.3ms)
- Memory allocations per spell cast

**Profiling Tools:**
- Visual Studio Profiler
- JetBrains dotMemory
- PerfView

### 4. Optimization Checklist

When optimizing spell system code:

- [ ] Identify the hot path (most frequently called code)
- [ ] Profile before making changes
- [ ] Cache expensive calculations
- [ ] Use appropriate data structures (Dictionary vs List)
- [ ] Minimize allocations in hot paths
- [ ] Use TryGetValue instead of ContainsKey + indexer
- [ ] Avoid repeated type checks (cache as field)
- [ ] Profile after changes to verify improvement
- [ ] Document performance characteristics

### 5. Performance Anti-Patterns to Avoid

**❌ Don't:**
- Create new Random() instances in methods (use Utility.Random)
- Use LINQ in hot paths
- Repeatedly access properties that do calculations
- Allocate objects in frequently called methods
- Use string concatenation in loops

**✅ Do:**
- Cache calculated values
- Use static/readonly where possible
- Pre-calculate constants
- Use StringBuilder for string building
- Profile before optimizing

## Migration Strategy

### Phase 1: Create Structure
1. Create subdirectories under `Base/`
2. Keep all files in `Base/` initially (no breaking changes)

### Phase 2: Move Files Gradually
1. **Start with Enums** (lowest risk, no dependencies)
2. **Move Constants** (used by many, but simple)
3. **Move Utilities** (helper classes)
4. **Update namespaces** if needed

### Phase 3: Update References
1. Update using statements
2. Test compilation
3. Verify runtime behavior
4. Run full test suite

### Phase 4: Cleanup
1. Remove old files
2. Update documentation
3. Update build scripts if needed

## Namespace Organization

Keep namespaces consistent with folder structure:

```csharp
namespace Server.Spells                    // Core classes
namespace Server.Spells.Calculations      // Calculations
namespace Server.Spells.Validation        // Validation
namespace Server.Spells.Movement          // Movement
namespace Server.Spells.Modifiers         // Modifiers
namespace Server.Spells.Registry          // Registry
namespace Server.Spells.Data             // Data classes
namespace Server.Spells.Constants        // Constants
```

## Benefits of This Structure

### 1. Clear Separation of Concerns
- Core classes separate from utilities
- Calculations separate from validation
- Data separate from logic

### 2. Easy Navigation
- Developers know where to find specific functionality
- Related files grouped together
- Clear naming conventions

### 3. Maintainability
- Changes to calculations don't affect validation
- Easy to add new validators or calculators
- Constants centralized

### 4. Scalability
- Easy to add new calculation methods
- Simple to extend validation rules
- New modifiers can be added without cluttering

### 5. Testability
- Each concern can be tested independently
- Mock dependencies easily
- Clear boundaries for unit tests

### 6. Performance
- Clear hot paths identified
- Easy to optimize specific areas
- Caching strategies obvious

## Quick Reference

**Finding Code:**
- Damage calculation → `Base/Calculations/SpellDamageCalculator.cs`
- Validation → `Base/Validation/SpellCastingValidator.cs`
- Constants → `Base/Constants/SpellConstants.cs`
- Core spell logic → `Base/Core/Spell.cs`
- Movement handling → `Base/Movement/SpellMovementHandler.cs`
- Registry → `Base/Registry/SpellRegistry.cs`

**Complexity Management:**
- Keep methods under 20 complexity
- Extract complex logic to helper classes
- Use early returns to reduce nesting
- Apply strategy pattern for conditional logic

**Performance:**
- Cache expensive lookups
- Use TryGetValue for dictionaries
- Avoid allocations in hot paths
- Profile before and after optimizations

