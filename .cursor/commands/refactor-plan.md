---
description: Comprehensive refactoring plan template for any C# file - focuses on readability, performance, complexity reduction, DRY principles, and code organization
globs: Scripts/**/*.cs
alwaysApply: false
---

# Refactor Plan Command

> **Note**: This command provides a comprehensive refactoring analysis template that can be applied to any C# file in the codebase.

## Usage

Type `/refactor-plan` followed by the file path to analyze and create a refactoring plan.

**Example:**
```
/refactor-plan @BaseArmor.cs
```

This command will analyze the specified file and provide a comprehensive refactoring plan following established codebase patterns.

## Analysis Request Template

When analyzing any file, use this comprehensive template:

```
ANALYZE [FILE PATH] WITH COMPREHENSIVE REFACTORING PLAN

## ANALYSIS SCOPE

### 1. Cyclomatic Complexity Analysis
- **Method Complexity**: Calculate cyclomatic complexity for each method
- **Overall File Complexity**: Sum of all method complexities
- **High Complexity Methods**: Identify methods with complexity > 10
- **Reduction Strategies**:
  - Extract conditional logic into helper methods
  - Use early returns to reduce nesting
  - Replace complex if-else chains with switch statements
  - Simplify boolean expressions
  - Break large methods into smaller, focused functions

### 2. DRY (Don't Repeat Yourself) Violations
- **Repeated Code Patterns**: Identify duplicated logic blocks
- **Repeated Calculations**: Find repeated calculations that can be extracted
- **Repeated Property Patterns**: Identify similar property implementations
- **Repeated Validation Logic**: Find duplicate validation code
- **Extraction Opportunities**: Methods, helper classes, or constants that can eliminate duplication

### 3. Magic Numbers and Constants
- **Hardcoded Numeric Values**: All numbers without named constants
- **Hardcoded Percentages**: Percentage values (0.25, 0.5, etc.)
- **Hardcoded Thresholds**: Skill thresholds, level caps, etc.
- **Hardcoded Multipliers**: Damage multipliers, scaling factors
- **Hardcoded Delays**: Time delays, cooldowns
- **Extraction Strategy**: Group related constants in dedicated constant classes/files

### 4. Hardcoded Strings
- **User-Facing Messages**: All strings displayed to users
- **Error Messages**: Error and validation messages
- **Property Labels**: Property list labels and descriptions
- **Format Strings**: String.Format templates
- **Localization**: Translate all strings to PT-BR (Portuguese-Brazilian)
- **Centralization**: Extract to dedicated string constant classes

### 5. Code Organization
- **Region Organization**: Organize code into logical regions:
  - Constants
  - Fields
  - Properties
  - Constructors
  - Serialization
  - Core Logic Methods
  - Helper Methods
  - Event Handlers
  - Nested Classes
- **File Length**: Identify if file should be split into multiple files
- **Class Responsibility**: Check for Single Responsibility Principle violations
- **Dead Code**: Remove commented-out code and unused methods

### 6. Performance Optimizations
- **Caching Opportunities**: Frequently accessed values that can be cached
- **Redundant Calculations**: Calculations performed multiple times
- **Property Getter Optimization**: Expensive operations in property getters
- **Collection Usage**: Replace Hashtable with Dictionary<TKey, TValue>
- **Object Allocations**: Minimize allocations in hot paths
- **Thread Safety**: Check for thread-safety issues with static variables

### 7. Readability Improvements
- **Method Naming**: Ensure methods have descriptive, verb-noun names
- **Variable Naming**: Use clear, descriptive variable names
- **Code Comments**: Add XML documentation for public/protected methods
- **Complex Logic Documentation**: Comment complex algorithms
- **Code Formatting**: Ensure consistent indentation and spacing
- **Line Length**: Break long lines for readability

### 8. Type Safety and Modern C# Patterns
- **Type Checking**: Replace `is` checks with pattern matching where appropriate (C# 7.0+)
- **Null Safety**: Proper null checking and null-conditional operators
- **Collection Types**: Use generic collections over non-generic
- **Explicit Types**: Avoid unnecessary `var` usage for clarity

## DELIVERABLES

For the analyzed file, provide:

1. **File Summary**: 
   - Total lines of code
   - Number of methods
   - Number of classes
   - Overall complexity score

2. **Complexity Analysis**:
   - Method-by-method complexity ratings
   - Overall file complexity
   - Target complexity reduction goals

3. **Magic Numbers Inventory**:
   - Complete list of all magic numbers found
   - Suggested constant names
   - Grouping strategy for constants

4. **String Inventory**:
   - Complete list of all hardcoded strings
   - PT-BR translations
   - Suggested constant names
   - Grouping strategy for string constants

5. **DRY Violations**:
   - List of duplicated code patterns
   - Extraction opportunities
   - Suggested helper methods/classes

6. **Refactoring Plan**:
   - Step-by-step improvement plan with priority
   - Estimated effort for each step
   - Risk assessment (Low/Medium/High)
   - Dependencies between steps

7. **Code Organization Plan**:
   - Proposed file structure
   - Region organization
   - File splitting recommendations

8. **Performance Improvements**:
   - Identified bottlenecks
   - Optimization opportunities
   - Expected performance gains

9. **Expected Outcomes**:
   - Lines of code reduction estimate
   - Complexity reduction estimate
   - Maintainability improvements
   - Performance improvements

## OUTPUT FORMAT

- Provide analysis directly in conversation
- Use clear sections with headers
- Include code examples for proposed changes (before/after)
- Prioritize improvements (CRITICAL/HIGH/MEDIUM/LOW)
- Reference existing codebase patterns when applicable
- Follow established patterns from similar files (e.g., WeaponConstants.cs, SpellConstants.cs)

## Key Patterns Established

### Constants Organization

Follow the pattern established in `WeaponConstants.cs` and `SpellConstants.cs`:

```csharp
namespace Server.Items
{
    /// <summary>
    /// Centralized constants for [domain] calculations and mechanics.
    /// Extracted from [SourceFile].cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class [Domain]Constants
    {
        #region [Category Name]
        
        /// <summary>Brief description of constant</summary>
        public const int CONSTANT_NAME = value;
        
        /// <summary>Brief description of constant</summary>
        public const double CONSTANT_NAME = value;
        
        #endregion
    }
}
```

### String Constants Organization

Follow the pattern established in `WeaponStringConstants.cs`:

```csharp
namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for [domain]-related messages and labels.
    /// Extracted from [SourceFile].cs to improve maintainability and enable localization.
    /// </summary>
    public static class [Domain]StringConstants
    {
        #region User Messages (Portuguese)
        
        /// <summary>Message when [condition]</summary>
        public const string MSG_[CONDITION] = "Mensagem em PT-BR";
        
        /// <summary>Message format for [action]</summary>
        public const string MSG_[ACTION]_FORMAT = "Mensagem com {0} parâmetros";
        
        #endregion
        
        #region Property Labels (Portuguese)
        
        /// <summary>Label for [property]</summary>
        public const string LABEL_[PROPERTY] = "Rótulo em PT-BR";
        
        #endregion
    }
}
```

### Helper Class Extraction

When extracting helper classes, follow this pattern:

```csharp
namespace Server.Items.Helpers
{
    /// <summary>
    /// Helper class for [domain] [specific functionality].
    /// Extracted from [SourceFile].cs to improve code organization and reusability.
    /// </summary>
    public static class [Domain][Functionality]Helper
    {
        /// <summary>
        /// [Method description]
        /// </summary>
        /// <param name="param">Parameter description</param>
        /// <returns>Return value description</returns>
        public static ReturnType MethodName(ParamType param)
        {
            // Implementation
        }
    }
}
```

### Complexity Reduction Patterns

#### Pattern 1: Extract Validation Methods

```csharp
// Before - High complexity
public bool CanPerformAction(Mobile from, object target)
{
    if (from == null)
        return false;
    if (target == null)
        return false;
    if (from.Deleted)
        return false;
    if (target.Deleted)
        return false;
    if (!from.Alive)
        return false;
    // ... more conditions
    return true;
}

// After - Reduced complexity
public bool CanPerformAction(Mobile from, object target)
{
    if (!ValidateActor(from))
        return false;
    if (!ValidateTarget(target))
        return false;
    return true;
}

private bool ValidateActor(Mobile from)
{
    return from != null && !from.Deleted && from.Alive;
}

private bool ValidateTarget(object target)
{
    return target != null && !target.Deleted;
}
```

#### Pattern 2: Extract Calculation Methods

```csharp
// Before - Repeated calculation
public int GetValue1() { return baseValue + bonus * multiplier / divisor; }
public int GetValue2() { return baseValue + bonus * multiplier / divisor; }
public int GetValue3() { return baseValue + bonus * multiplier / divisor; }

// After - DRY
private int CalculateValue(int baseValue, int bonus)
{
    return baseValue + bonus * MULTIPLIER / DIVISOR;
}

public int GetValue1() { return CalculateValue(m_BaseValue1, m_Bonus1); }
public int GetValue2() { return CalculateValue(m_BaseValue2, m_Bonus2); }
public int GetValue3() { return CalculateValue(m_BaseValue3, m_Bonus3); }
```

#### Pattern 3: Extract Switch Statements

```csharp
// Before - Large switch in method
public int GetResourceBonus(CraftResource resource)
{
    switch (resource)
    {
        case CraftResource.DullCopper: return 6;
        case CraftResource.ShadowIron: return 12;
        // ... 30+ cases
    }
}

// After - Extracted to dictionary or helper
private static readonly Dictionary<CraftResource, int> ResourceBonuses = 
    new Dictionary<CraftResource, int>
{
    { CraftResource.DullCopper, 6 },
    { CraftResource.ShadowIron, 12 },
    // ... all cases
};

public int GetResourceBonus(CraftResource resource)
{
    int bonus;
    return ResourceBonuses.TryGetValue(resource, out bonus) ? bonus : 0;
}
```

### Code Organization Template

```csharp
namespace Server.Items
{
    /// <summary>
    /// [Class description]
    /// </summary>
    public class ClassName : BaseClass
    {
        #region Constants
        // All constants here with XML comments
        #endregion

        #region Fields
        // Private instance fields
        #endregion

        #region Properties
        // Public/virtual properties
        #endregion

        #region Constructors
        // Constructors
        #endregion

        #region Serialization
        // Serialize/Deserialize methods
        #endregion

        #region Core Logic
        // Main business logic methods
        #endregion

        #region Helper Methods
        // Private helper methods
        #endregion

        #region Event Handlers
        // OnAdded, OnRemoved, OnEquip, etc.
        #endregion

        #region Property Display
        // GetProperties, AddNameProperty, etc.
        #endregion

        #region Nested Classes
        // Internal helper classes, targets, timers
        #endregion
    }
}
```

## Refactoring Priority Levels

When providing refactoring recommendations, prioritize:

1. **CRITICAL** (Must fix immediately):
   - Thread safety issues
   - Memory leaks
   - Logic errors
   - Security vulnerabilities
   - Breaking changes to public API

2. **HIGH** (Fix in current refactoring):
   - Performance bottlenecks
   - Code duplication (DRY violations)
   - Message centralization
   - Magic number replacement
   - High complexity methods (>15)

3. **MEDIUM** (Nice to have):
   - Complexity reduction (methods 10-15)
   - Code organization
   - Documentation improvements
   - Minor optimizations
   - Helper class extraction

4. **LOW** (Future improvements):
   - Style consistency
   - Minor refactoring
   - Additional comments
   - Code cleanup
   - Low complexity methods (5-10)

## Complexity Rating Scale

- **1-5**: Low complexity - Well-structured, easy to understand
- **6-10**: Moderate complexity - Some nesting, but manageable
- **11-15**: High complexity - Should be refactored
- **16-20**: Very high complexity - Must be refactored
- **21+**: Extreme complexity - Critical refactoring needed

## Refactoring Checklist

### Pre-Refactoring
- [ ] Analyze current code structure
- [ ] Identify all magic numbers
- [ ] Identify all hardcoded strings
- [ ] Calculate cyclomatic complexity
- [ ] Identify DRY violations
- [ ] Review existing patterns in codebase

### Constants Extraction
- [ ] Create Constants class file
- [ ] Extract all magic numbers
- [ ] Group constants by category
- [ ] Add XML documentation
- [ ] Update all references

### String Extraction
- [ ] Create StringConstants class file
- [ ] Extract all hardcoded strings
- [ ] Translate to PT-BR
- [ ] Group by category
- [ ] Add XML documentation
- [ ] Update all references

### Code Organization
- [ ] Organize into regions
- [ ] Remove dead code
- [ ] Remove commented code
- [ ] Split large files if needed
- [ ] Extract helper classes

### Complexity Reduction
- [ ] Extract high-complexity methods
- [ ] Break down large methods
- [ ] Extract validation logic
- [ ] Extract calculation logic
- [ ] Simplify conditional logic

### Performance
- [ ] Identify caching opportunities
- [ ] Optimize property getters
- [ ] Reduce redundant calculations
- [ ] Replace Hashtable with Dictionary
- [ ] Minimize object allocations

### Documentation
- [ ] Add XML comments to public methods
- [ ] Add XML comments to protected methods
- [ ] Document complex algorithms
- [ ] Add class-level documentation

### Testing
- [ ] Verify all functionality works
- [ ] Test edge cases
- [ ] Performance benchmark
- [ ] Regression testing

## Expected Improvements Metrics

After refactoring, track:

- **Lines of Code**: Target 15-25% reduction
- **Cyclomatic Complexity**: Target 40-50% reduction
- **Magic Numbers**: 100% extracted to constants
- **Hardcoded Strings**: 100% extracted and localized
- **Code Duplication**: Eliminate all major DRY violations
- **Method Complexity**: All methods < 10 complexity
- **File Organization**: Clear region structure
- **Documentation**: All public/protected methods documented

## Risk Assessment Guidelines

- **Low Risk**: Constants extraction, string extraction, documentation
- **Medium Risk**: Helper class extraction, method refactoring, code organization
- **High Risk**: Complex logic refactoring, serialization changes, public API changes

## Related Documentation

- **Weapon Pattern**: Check `Scripts/Items and addons/Weapons/Helpers/` for constants and string patterns
- **Spell Pattern**: Check `Scripts/Engines and systems/Magic/Base/Constants/` for constants patterns
- **Base Classes**: Check base classes for established patterns before creating new solutions

## C# Syntax Compatibility

**IMPORTANT**: This codebase uses C# 6.0 syntax. Do NOT use:
- Pattern matching with inline variables (`if (x is Type t)`)
- Switch expressions (`value switch { ... }`)
- Nullable reference types (`string?`)
- Local functions
- Tuple literals

Always use traditional C# syntax compatible with C# 6.0.

---

**Remember**: Always check existing patterns in the codebase before creating new solutions. Follow established conventions from similar files (WeaponConstants, SpellConstants, etc.).

