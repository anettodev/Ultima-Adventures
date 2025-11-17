# Claude Code Custom Commands

This directory contains custom slash commands for Claude Code.

## Available Commands

### `/refactor-plan` - Comprehensive Refactoring Analysis

Analyzes a C# file and provides a detailed refactoring plan covering:
- Cyclomatic complexity analysis
- DRY violations
- Magic numbers and constants
- Hardcoded strings (with PT-BR translations)
- Code organization recommendations
- Performance optimizations
- Readability improvements

**Usage:**
```
/refactor-plan @Scripts/Items and addons/Armor/BaseArmor.cs
```

**What it does:**
1. Calculates complexity for each method
2. Identifies all magic numbers and suggests constant names
3. Finds all hardcoded strings and provides PT-BR translations
4. Detects code duplication
5. Recommends performance improvements
6. Provides a prioritized, step-by-step refactoring plan

**Output includes:**
- File summary with metrics
- Method-by-method complexity breakdown
- Complete inventory of magic numbers with suggested constant names
- Complete inventory of strings with PT-BR translations
- DRY violation report
- Prioritized refactoring plan (CRITICAL/HIGH/MEDIUM/LOW)
- Code organization recommendations
- Performance improvement suggestions
- Expected outcomes and metrics

---

## Key Differences: Cursor vs Claude Code Commands

### Cursor Commands (`.cursor/commands/`)
- Use YAML frontmatter with `globs` and `alwaysApply` options
- Can be automatically applied based on file patterns
- More structured with specific glob patterns

### Claude Code Commands (`.claude/commands/`)
- Simpler markdown format
- Use YAML frontmatter for description only
- Manually invoked with `/command-name`
- More conversational and flexible

---

## Creating New Commands

To create a new command:

1. Create a markdown file in `.claude/commands/`
2. Name it `command-name.md` (becomes `/command-name`)
3. Add optional frontmatter:
```markdown
---
description: Brief description of what this command does
---

# Command instructions here
```

4. Use the command by typing `/command-name` in Claude Code

---

## Command Best Practices

1. **Be specific**: Provide clear, actionable instructions
2. **Use examples**: Show expected input/output format
3. **Reference patterns**: Point to existing code patterns to follow
4. **Structure output**: Define clear sections and format for results
5. **Be consistent**: Follow established conventions in the codebase

---

## Related Files

- **Cursor equivalent**: `.cursor/commands/refactor-plan.md`
- **Codebase patterns**:
  - `Scripts/Items and addons/Weapons/Helpers/WeaponConstants.cs`
  - `Scripts/Items and addons/Weapons/Helpers/WeaponStringConstants.cs`
  - `Scripts/Engines and systems/Magic/Base/Constants/SpellConstants.cs`

---

## Future Command Ideas

Consider creating additional commands for:
- `/translate-strings` - Batch translate strings to PT-BR
- `/extract-constants` - Extract magic numbers from a file
- `/complexity-check` - Quick complexity analysis only
- `/generate-docs` - Generate XML documentation for methods
- `/test-plan` - Create test plan for a feature
