---
description: Comprehensive refactoring plan template for any C# file - focuses on readability, performance, complexity reduction, DRY principles, and code organization
---

# Refactor Plan Command

Analyze the specified file and create a comprehensive refactoring plan following established codebase patterns.

## Usage

Type `/refactor-plan` followed by the file path or reference to analyze.

**Example:**
```
/refactor-plan @Scripts/Items and addons/Armor/BaseArmor.cs
```

---

## ANALYSIS INSTRUCTIONS

When this command is invoked, perform a comprehensive analysis of the specified file using the following template:

### 1. Cyclomatic Complexity Analysis

Calculate and report:
- **Method-by-method complexity**: For each method, calculate cyclomatic complexity
- **Overall file complexity**: Sum of all method complexities
- **High complexity methods**: Flag any method with complexity > 10

**Reduction strategies to recommend:**
- Extract conditional logic into helper methods
- Use early returns to reduce nesting
- Replace complex if-else chains with switch statements or dictionaries
- Simplify boolean expressions
- Break large methods into smaller, focused functions

**Complexity Rating Scale:**
- **1-5**: Low - Well-structured, easy to understand
- **6-10**: Moderate - Some nesting, but manageable
- **11-15**: High - Should be refactored
- **16-20**: Very high - Must be refactored
- **21+**: Extreme - Critical refactoring needed

### 2. DRY (Don't Repeat Yourself) Violations

Identify and document:
- **Repeated code patterns**: Duplicated logic blocks
- **Repeated calculations**: Same calculations in multiple places
- **Repeated property patterns**: Similar property implementations
- **Repeated validation logic**: Duplicate validation code
- **Extraction opportunities**: Suggest methods, helper classes, or constants

### 3. Magic Numbers and Constants

Find all hardcoded values:
- **Numeric values**: All numbers without named constants
- **Percentages**: 0.25, 0.5, etc.
- **Thresholds**: Skill thresholds, level caps, etc.
- **Multipliers**: Damage multipliers, scaling factors
- **Delays**: Time delays, cooldowns

**Extraction strategy**: Group related constants following the pattern in `WeaponConstants.cs`

### 4. Hardcoded Strings

Identify all strings:
- **User-facing messages**: Strings displayed to users
- **Error messages**: Error and validation messages
- **Property labels**: Property list labels and descriptions
- **Format strings**: String.Format templates

**Requirements:**
- **Translate to PT-BR** (Portuguese-Brazilian)
- **Centralize** in string constant classes following `WeaponStringConstants.cs` pattern

### 5. Code Organization

Evaluate and recommend:
- **Region organization**: Organize into logical regions:
  - Constants
  - Fields
  - Properties
  - Constructors
  - Serialization
  - Core Logic Methods
  - Helper Methods
  - Event Handlers
  - Nested Classes
- **File length**: Should the file be split?
- **Single Responsibility**: Check for SRP violations
- **Dead code**: Identify commented-out code and unused methods

### 6. Performance Optimizations

Identify opportunities:
- **Caching**: Frequently accessed values that can be cached
- **Redundant calculations**: Operations performed multiple times
- **Property getter optimization**: Expensive operations in getters
- **Collection usage**: Replace `Hashtable` with `Dictionary<TKey, TValue>`
- **Object allocations**: Minimize allocations in hot paths
- **Thread safety**: Check static variables for thread-safety issues

### 7. Readability Improvements

Assess and recommend:
- **Method naming**: Descriptive, verb-noun names
- **Variable naming**: Clear, descriptive names
- **Code comments**: XML documentation for public/protected methods
- **Complex logic**: Comment complex algorithms
- **Code formatting**: Consistent indentation and spacing
- **Line length**: Break long lines

### 8. Type Safety and Modern C# Patterns

⚠️ **IMPORTANT**: This codebase uses **C# 6.0 syntax**. Do NOT recommend:
- Pattern matching with inline variables (`if (x is Type t)`)
- Switch expressions (`value switch { ... }`)
- Nullable reference types (`string?`)
- Local functions
- Tuple literals

**Allowed improvements:**
- Replace `Hashtable` with generic collections
- Use null-conditional operators (`?.`, `??`)
- String interpolation (`$"text {variable}"`)
- Auto-properties
- Expression-bodied members

---

## DELIVERABLES

Provide a comprehensive report with the following sections:

### 1. File Summary
```
- Total lines of code: [number]
- Number of methods: [number]
- Number of classes: [number]
- Overall complexity score: [number]
```

### 2. Complexity Analysis
```
Method Complexity Breakdown:
- MethodName1: [score] - [LOW/MODERATE/HIGH/CRITICAL]
- MethodName2: [score] - [LOW/MODERATE/HIGH/CRITICAL]
...

Overall File Complexity: [total]
Target Complexity After Refactor: [goal]
Reduction Goal: [percentage]%
```

### 3. Magic Numbers Inventory
```
Found [N] magic numbers:

Location | Value | Suggested Constant Name | Category
---------|-------|------------------------|----------
Line 123 | 0.25  | DAMAGE_MULTIPLIER      | Combat
Line 456 | 100   | MAX_SKILL_VALUE        | Skills
...

Proposed Constants Class: [ClassName]Constants.cs
```

### 4. String Inventory
```
Found [N] hardcoded strings:

Line | Original String (ENU) | PT-BR Translation | Suggested Constant
-----|----------------------|-------------------|--------------------
123  | "You cannot..."      | "Você não pode..." | MSG_CANNOT_USE_SKILL
456  | "Damage: {0}"        | "Dano: {0}"       | LABEL_DAMAGE_FORMAT
...

Proposed String Class: [ClassName]StringConstants.cs
```

### 5. DRY Violations
```
Found [N] code duplication issues:

1. [Description of duplication]
   - Locations: Line X, Line Y, Line Z
   - Suggested extraction: [Helper method/class name]
   - Estimated LOC reduction: [number] lines

2. [Next duplication]
   ...
```

### 6. Refactoring Plan

Provide a prioritized, step-by-step plan:

```
CRITICAL Priority (Must fix immediately):
□ [Task 1] - Estimated effort: [time] - Risk: [LOW/MEDIUM/HIGH]
  Reason: [why critical]
  Dependencies: [none/list]

HIGH Priority (Fix in current refactoring):
□ [Task 2] - Estimated effort: [time] - Risk: [LOW/MEDIUM/HIGH]
  Reason: [justification]
  Dependencies: [none/list]

MEDIUM Priority (Nice to have):
□ [Task 3] - Estimated effort: [time] - Risk: [LOW/MEDIUM/HIGH]

LOW Priority (Future improvements):
□ [Task 4] - Estimated effort: [time] - Risk: [LOW/MEDIUM/HIGH]
```

### 7. Code Organization Plan
```
Proposed file structure:
- [ClassName].cs (main class, [estimated LOC])
- [ClassName]Constants.cs ([estimated LOC])
- [ClassName]StringConstants.cs ([estimated LOC])
- [Helper classes if needed]

Proposed region organization:
#region Constants
#region Fields
#region Properties
...
```

### 8. Performance Improvements
```
Identified bottlenecks:
1. [Description]
   - Current impact: [HIGH/MEDIUM/LOW]
   - Optimization: [specific recommendation]
   - Expected gain: [estimate]
```

### 9. Expected Outcomes
```
Metrics:
- Lines of code reduction: [X]% (from [before] to [after])
- Complexity reduction: [X]% (from [before] to [after])
- Magic numbers eliminated: [N] → 0
- Hardcoded strings eliminated: [N] → 0
- Methods with complexity > 10: [N] → 0

Maintainability improvements:
- [Improvement 1]
- [Improvement 2]

Performance improvements:
- [Expected performance gain]
```

---

## CODE PATTERNS TO FOLLOW

### Pattern 1: Constants Class

Follow the pattern from `WeaponConstants.cs`:

```csharp
namespace Server.Items
{
    /// <summary>
    /// Centralized constants for [Domain] calculations and mechanics.
    /// Extracted from [SourceFile].cs to improve maintainability.
    /// </summary>
    public static class [Domain]Constants
    {
        #region [Category]

        /// <summary>Description of constant</summary>
        public const int CONSTANT_NAME = value;

        /// <summary>Description of constant</summary>
        public const double CONSTANT_NAME = value;

        #endregion
    }
}
```

### Pattern 2: String Constants Class

Follow the pattern from `WeaponStringConstants.cs`:

```csharp
namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for [Domain] messages and labels.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// </summary>
    public static class [Domain]StringConstants
    {
        #region User Messages

        /// <summary>Message when [condition]</summary>
        public const string MSG_[NAME] = "Mensagem em PT-BR";

        /// <summary>Format string for [purpose]</summary>
        public const string MSG_[NAME]_FORMAT = "Mensagem com {0} parâmetros";

        #endregion

        #region Property Labels

        /// <summary>Label for [property]</summary>
        public const string LABEL_[PROPERTY] = "Rótulo em PT-BR";

        #endregion
    }
}
```

### Pattern 3: Complexity Reduction - Extract Validation

```csharp
// ❌ BEFORE - High complexity (6 paths)
public bool CanPerformAction(Mobile from, object target)
{
    if (from == null) return false;
    if (target == null) return false;
    if (from.Deleted) return false;
    if (!from.Alive) return false;
    if (from.Frozen) return false;
    return true;
}

// ✅ AFTER - Reduced complexity (2 paths)
public bool CanPerformAction(Mobile from, object target)
{
    if (!ValidateActor(from)) return false;
    if (!ValidateTarget(target)) return false;
    return true;
}

private bool ValidateActor(Mobile from)
{
    return from != null && !from.Deleted && from.Alive && !from.Frozen;
}

private bool ValidateTarget(object target)
{
    return target != null && !target.Deleted;
}
```

### Pattern 4: DRY - Extract Repeated Calculations

```csharp
// ❌ BEFORE - Repeated calculation
public int GetPhysicalDamage()
{
    return m_BaseDamage + m_Bonus * 2 / 100;
}

public int GetFireDamage()
{
    return m_FireDamage + m_Bonus * 2 / 100;
}

// ✅ AFTER - DRY with constants
private const int BONUS_MULTIPLIER = 2;
private const int BONUS_DIVISOR = 100;

private int CalculateDamageWithBonus(int baseDamage)
{
    return baseDamage + m_Bonus * BONUS_MULTIPLIER / BONUS_DIVISOR;
}

public int GetPhysicalDamage()
{
    return CalculateDamageWithBonus(m_BaseDamage);
}

public int GetFireDamage()
{
    return CalculateDamageWithBonus(m_FireDamage);
}
```

### Pattern 5: Dictionary Instead of Large Switch

```csharp
// ❌ BEFORE - High complexity switch statement
public int GetResourceBonus(CraftResource resource)
{
    switch (resource)
    {
        case CraftResource.DullCopper: return 6;
        case CraftResource.ShadowIron: return 12;
        case CraftResource.Copper: return 18;
        // ... 20+ more cases
        default: return 0;
    }
}

// ✅ AFTER - Dictionary lookup (complexity = 1)
private static readonly Dictionary<CraftResource, int> ResourceBonuses =
    new Dictionary<CraftResource, int>
{
    { CraftResource.DullCopper, 6 },
    { CraftResource.ShadowIron, 12 },
    { CraftResource.Copper, 18 },
    // ... all cases
};

public int GetResourceBonus(CraftResource resource)
{
    int bonus;
    return ResourceBonuses.TryGetValue(resource, out bonus) ? bonus : 0;
}
```

---

## PRIORITY LEVELS

**CRITICAL** (Security/Bugs/Breaking):
- Thread safety issues
- Memory leaks
- Logic errors
- Security vulnerabilities

**HIGH** (Current Refactoring):
- Performance bottlenecks
- Code duplication (DRY)
- Magic numbers
- String centralization
- Methods with complexity > 15

**MEDIUM** (Nice to Have):
- Complexity 10-15
- Code organization
- Documentation
- Helper class extraction

**LOW** (Future):
- Style consistency
- Minor refactoring
- Code cleanup

---

## RISK ASSESSMENT

- **Low Risk**: Constants extraction, string extraction, documentation
- **Medium Risk**: Helper classes, method refactoring, organization
- **High Risk**: Complex logic changes, serialization, public API changes

---

## OUTPUT FORMAT

1. Use clear markdown sections with headers
2. Include **before/after code examples** for major changes
3. Provide **specific line numbers** when referencing code
4. Use tables for inventories (magic numbers, strings)
5. Use checkboxes for action items
6. Prioritize with **CRITICAL/HIGH/MEDIUM/LOW** labels
7. Reference existing patterns in the codebase
8. Be specific and actionable

---

**Remember**: Always check existing patterns in the codebase before recommending new solutions. Follow established conventions from `WeaponConstants.cs`, `SpellConstants.cs`, and similar files.
