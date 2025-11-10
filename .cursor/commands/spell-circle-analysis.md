---
description: Comprehensive spell circle analysis and refactoring guidelines for Magery spells
globs: Scripts/Engines and systems/Magic/**/*
alwaysApply: false
---

# Spell Circle Analysis Command

> **Note**: This command uses the comprehensive analysis guidelines defined in `@spell-circle-analysis.md` rule file.

## Usage

Type `/spell-circle-analysis` followed by the circle folder path to analyze all spells in that circle.

**Example:**
```
/spell-circle-analysis @Magery 5th
```

This command will analyze all spells in the specified circle using the guidelines from `@spell-circle-analysis.md`.

## Analysis Request Template

When analyzing spells, use this comprehensive template:

```
ANALYZE ALL SPELLS IN [CIRCLE FOLDER] WITH COMPREHENSIVE REFACTORING PLAN

## ANALYSIS SCOPE

### 1. Spell Information Analysis
- **Objective**: What is the spell's intended purpose and behavior?
- **Description**: Detailed mechanics, effects, and gameplay impact
- **Requirements**: 
  - Minimum skill level (Magery)
  - Reagent requirements
  - Mana cost
  - Target type (Mobile, Item, Location, Self)
  - Range (tiles) - use SpellConstants.GetSpellRange() for consistency
  - Cast delay multiplier
  - Line of Sight requirements

### 2. Performance Analysis
- **Reflection Overhead**: Check for Activator.CreateInstance, GetType().IsDefined usage
- **Timer Usage**: Identify unnecessary timers, optimize timer creation/destruction
- **Object Allocations**: Minimize unnecessary object creation in hot paths
- **Caching Opportunities**: Static data, pre-calculated values, lookup tables
- **Thread Safety**: Check for static variables used for instance-specific data (use Dictionary<Mobile, Data>)
- **Collection Usage**: Replace Hashtable with Dictionary<TKey, TValue> for type safety

### 3. Readability Improvements
- **Magic Numbers**: Replace all hardcoded values with named const fields
- **Commented Code**: Remove all commented-out code blocks
- **Code Structure**: Organize with #region blocks (Constants, Methods, Data Structures)
- **Method Extraction**: Break down complex methods into smaller, well-named functions
- **Documentation**: Add XML comments for all public/protected methods and complex logic

### 4. Cyclomatic Complexity Assessment
- **Complexity Rating**: Rate each method (1-10 scale)
- **Reduction Strategies**: 
  - Extract conditional logic into helper methods
  - Use early returns to reduce nesting
  - Replace complex if-else chains with switch statements where appropriate
  - Simplify boolean expressions

### 5. Code Consistency & Patterns
- **Message Centralization**: 
  - All user-facing messages must use Spell.SpellMessages constants (PT-BR)
  - No hardcoded strings in spell logic
  - Check Spell.cs for existing message constants before creating new ones
- **Helper Class Usage**: 
  - Leverage existing helpers: SpellHelper, SpellConstants, FieldSpellHelper, PoisonHelper
  - Check @Base folder for reusable patterns and utilities
- **Spell Range**: Use SpellConstants.GetSpellRange() instead of Core.ML ? 10 : 12
- **Duration Calculation**: Use SpellHelper.NMSGetDuration() for consistent duration formulas
- **Damage Calculation**: Use GetNMSDamage() or GetNewAosDamage() based on spell type

### 6. Potential Fixes & Improvements
- **Bug Identification**: Logic errors, edge cases, null reference risks
- **Edge Cases**: Handle invalid targets, dead mobiles, deleted objects
- **Validation**: Add proper validation for spell requirements before casting
- **Error Handling**: Graceful failure with appropriate user messages
- **Game Balance**: Review damage caps, duration limits, cooldown mechanics

### 7. Best Practices Established
- **Constants Organization**: Group related constants in #region blocks with XML comments
- **Thread Safety**: Use Dictionary<Mobile, T> for per-mobile data storage (never static instance variables)
- **Type Safety**: Prefer Dictionary over Hashtable, explicit types over object casting
- **Naming Conventions**: 
  - Constants: UPPER_SNAKE_CASE with descriptive names
  - Methods: PascalCase with verb-noun pattern (CalculateDamage, ApplyEffect)
  - Private fields: m_ prefix (m_Caster, m_Timer)
- **Code Formatting**: Consistent indentation, spacing, and brace placement

## DELIVERABLES

For EACH spell, provide:
1. **Spell Summary**: Objective, description, requirements table
2. **Performance Issues**: List of identified bottlenecks with severity (HIGH/MEDIUM/LOW)
3. **Refactoring Plan**: Step-by-step improvement plan with priority
4. **Complexity Analysis**: Method-by-method complexity rating and reduction suggestions
5. **Code Improvements**: Specific code changes with before/after examples
6. **Consistency Check**: Alignment with Base folder patterns and centralized systems

## OUTPUT FORMAT

- Provide analysis directly in conversation (no document creation unless explicitly requested)
- Use clear sections with headers
- Include code examples for proposed changes
- Prioritize improvements (must-have vs nice-to-have)
- Reference existing code patterns from @Base folder when applicable
```

## Key Patterns Established

### Message Centralization

All user-facing messages must use centralized constants:

```csharp
// ✅ CORRECT
target.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);

// ❌ WRONG
target.SendMessage("You cannot see that target.");
```

### Spell Range Constants

Always use centralized range helper:

```csharp
// ✅ CORRECT
public InternalTarget(Spell owner) : base(SpellConstants.GetSpellRange(), false, TargetFlags.Harmful)

// ❌ WRONG
public InternalTarget(Spell owner) : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
```

### Constants Organization

Group all constants in #region blocks:

```csharp
#region Constants

/// <summary>Minimum damage dealt</summary>
private const int MIN_DAMAGE = 3;

/// <summary>Maximum damage cap</summary>
private const int MAX_DAMAGE = 35;

#endregion
```

### Thread Safety

Never use static variables for instance-specific data:

```csharp
// ✅ CORRECT - Thread-safe per-mobile storage
private static Dictionary<Mobile, SpellData> m_ActiveSpells = new Dictionary<Mobile, SpellData>();

// ❌ WRONG - Not thread-safe
private static Mobile m_CurrentCaster;
private static int m_CurrentDamage;
```

### Helper Class Usage

Leverage existing helpers from Base folder:

```csharp
// Duration calculation
TimeSpan duration = SpellHelper.NMSGetDuration(caster, target, isBeneficial);

// Damage calculation
int damage = GetNMSDamage(bonus: 25, dice: 1, sides: 5, target);

// Field spell duration
TimeSpan fieldDuration = FieldSpellHelper.GetFieldDuration(caster);

// Poison level determination
Poison poison = PoisonHelper.DeterminePoisonLevel(caster, target);
```

### Code Structure Template

```csharp
namespace Server.Spells.Fifth
{
    /// <summary>
    /// Spell Name - Circle Description
    /// Brief explanation of spell purpose
    /// </summary>
    public class SpellNameSpell : MagerySpell
    {
        private static SpellInfo m_Info = new SpellInfo(/* ... */);

        public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

        #region Constants
        // All constants here with XML comments
        #endregion

        #region Data Structures
        // Static dictionaries, helper classes, etc.
        #endregion

        public SpellNameSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            // Implementation
        }

        #region Helper Methods
        // Private helper methods
        #endregion

        #region Internal Classes
        // InternalTarget, Timers, etc.
        #endregion
    }
}
```

## Common Refactoring Patterns

### 1. Extract Magic Numbers

```csharp
// Before
if (damage > 42)
    damage = 42;

// After
private const int MAX_DAMAGE = 42;
if (damage > MAX_DAMAGE)
    damage = MAX_DAMAGE;
```

### 2. Centralize Messages

```csharp
// Before
caster.SendMessage("You cannot see that target.");

// After
caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
```

### 3. Reduce Complexity

```csharp
// Before - High complexity
public void ComplexMethod()
{
    if (condition1)
    {
        if (condition2)
        {
            if (condition3)
            {
                // Deep nesting
            }
        }
    }
}

// After - Reduced complexity
public void ComplexMethod()
{
    if (!ValidateConditions())
        return;
    
    ProcessValidated();
}

private bool ValidateConditions()
{
    return condition1 && condition2 && condition3;
}
```

### 4. Use Helper Methods

```csharp
// Before - Duplicated logic
int duration1 = CalculateDuration(caster1);
int duration2 = CalculateDuration(caster2);

// After - Centralized helper
TimeSpan duration = SpellHelper.NMSGetDuration(caster, target, isBeneficial);
```

## Performance Optimization Checklist

- [ ] Replaced `Activator.CreateInstance` with direct instantiation where possible
- [ ] Minimized timer creation/destruction overhead
- [ ] Cached frequently accessed static data
- [ ] Used `Dictionary<TKey, TValue>` instead of `Hashtable`
- [ ] Avoided unnecessary object allocations in hot paths
- [ ] Implemented thread-safe per-mobile data storage
- [ ] Removed reflection-based type checking where not needed

## Readability Checklist

- [ ] All magic numbers replaced with named constants
- [ ] All commented-out code removed
- [ ] Code organized with #region blocks
- [ ] Complex methods broken into smaller functions
- [ ] XML documentation added for public/protected methods
- [ ] Consistent naming conventions followed
- [ ] Consistent code formatting applied

## Consistency Checklist

- [ ] All messages use `Spell.SpellMessages` constants
- [ ] Spell range uses `SpellConstants.GetSpellRange()`
- [ ] Duration uses `SpellHelper.NMSGetDuration()`
- [ ] Damage uses `GetNMSDamage()` or `GetNewAosDamage()`
- [ ] Helper classes from Base folder leveraged
- [ ] Patterns match existing spell implementations

## Output Priority Levels

When providing refactoring recommendations, prioritize:

1. **CRITICAL** (Must fix immediately):
   - Thread safety issues
   - Memory leaks
   - Logic errors
   - Security vulnerabilities

2. **HIGH** (Fix in current refactoring):
   - Performance bottlenecks
   - Code duplication
   - Message centralization
   - Magic number replacement

3. **MEDIUM** (Nice to have):
   - Complexity reduction
   - Code organization
   - Documentation improvements
   - Minor optimizations

4. **LOW** (Future improvements):
   - Style consistency
   - Minor refactoring
   - Additional comments
   - Code cleanup

---

## Related Documentation

- **Rule File**: `@spell-circle-analysis.md` - Contains the comprehensive analysis guidelines and patterns used by this command
- **Base Folder**: Check `Scripts/Engines and systems/Magic/Base/` for existing patterns and helpers before creating new solutions

**Remember**: Always check the @Base folder for existing patterns and helpers before creating new solutions.
