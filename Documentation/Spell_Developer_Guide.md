# Spell.cs Developer Quick Reference

**Last Updated:** November 6, 2025  
**File:** `Scripts/Engines and systems/Magic/Base/Spell.cs`

---

## Quick Start

### Creating a New Spell

Your spell class should inherit from `MagerySpell` (or appropriate spell type):

```csharp
using Server.Spells;

namespace Server.Spells.Seventh
{
    public class MyNewSpell : MagerySpell
    {
        public override SpellCircle Circle { get { return SpellCircle.Seventh; } }
        
        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }
        
        public void Target(Mobile target)
        {
            if (CheckHSequence(target))
            {
                // Calculate damage using NMS or AOS system
                int damage = GetNMSDamage(25, 1, 5, target);
                
                // Apply damage
                SpellHelper.Damage(this, target, damage, 0, 100, 0, 0, 0);
            }
            
            FinishSequence();
        }
    }
}
```

---

## Damage Calculation Systems

### Option 1: NMS System (Recommended for New Spells)

```csharp
// Simple calculation based on EvalInt
int damage = GetNMSDamage(
    bonus: 25,        // Base damage bonus
    dice: 1,          // Number of dice
    sides: 5,         // Sides per die
    target            // Target mobile
);
```

**Formula:** `Dice(dice, sides, bonus) * EvalIntBenefit`

### Option 2: AOS System (Legacy, More Complex)

```csharp
// Complex calculation with Inscribe, Int, SDI, EvalInt
int damage = GetNewAosDamage(
    bonus: 25,        // Base damage bonus
    dice: 1,          // Number of dice
    sides: 5,         // Sides per die
    target,           // Target mobile
    scalar: 1.0       // Additional scalar (optional)
);
```

**Formula:** `Dice * (100 + Inscribe + Int + SDI) * EvalInt * Scalar / 100`

---

## Constants Reference

### Commonly Used Constants

```csharp
// Timing
NEXT_SPELL_DELAY_SECONDS = 0.75      // Delay between spell casts
ANIMATE_DELAY_SECONDS = 1.5          // Animation timing

// Damage
BASE_DAMAGE_MULTIPLIER = 100         // Damage calculation base
INT_BONUS_DIVISOR = 10               // Intelligence bonus calculation

// Magery Thresholds
MAGERY_SKILL_40 to MAGERY_SKILL_120  // Skill level constants

// Message Colors
MSG_COLOR_SYSTEM = 95                // System messages
MSG_COLOR_ERROR = 55                 // Error messages
MSG_COLOR_WARNING = 33               // Warning messages
```

### Adding New Constants

Add constants to the appropriate region at the top of the file:

```csharp
#region Constants
// Your new constant group
private const int MY_NEW_CONSTANT = 42;
#endregion
```

---

## User Messages

### Using Existing Messages

```csharp
// Error message
caster.SendMessage(MSG_COLOR_ERROR, UserMessages.ERROR_CANNOT_CAST_IN_STATE);

// Info message with formatting
string message = string.Format(UserMessages.INFO_SPELL_UNSTABLE_AFTER_STEPS_FORMAT, maxSteps);
caster.SendMessage(MSG_COLOR_SYSTEM, message);
```

### Adding New Messages

Add to the `UserMessages` class:

```csharp
#region PT-BR User Messages
private static class UserMessages
{
    // Your new message
    public const string MY_NEW_MESSAGE = "Sua nova mensagem em PT-BR";
    public const string MY_FORMAT_MESSAGE = "Mensagem com {0} parâmetros";
}
#endregion
```

---

## Frequently Used Methods

### Validation Methods

```csharp
// Check if spell can be cast
public virtual bool CheckCast(Mobile caster)

// Check if target can be harmed
public bool CheckHSequence(Mobile target)

// Check if target can receive beneficial spell
public bool CheckBSequence(Mobile target, bool allowDead = false)

// Validate casting sequence
public virtual bool CheckSequence()
```

### Damage Methods

```csharp
// Get damage scalar for target (considers resist, slayers, etc.)
public virtual double GetDamageScalar(Mobile target)

// Calculate slayer damage bonus
public virtual double GetSlayerDamageScalar(Mobile defender)
```

### Timing Methods

```csharp
// Get cast delay with FC modifiers
public virtual TimeSpan GetCastDelay()

// Get recovery time after cast
public virtual TimeSpan GetCastRecovery()

// Get mana cost with LMC/LRC
public virtual int ScaleMana(int mana)
```

### Region Checks

```csharp
// Check if in Midland (private helper, use via public methods)
private bool IsInMidland()
```

---

## Common Patterns

### Pattern 1: Damage Spell with Target

```csharp
public override void OnCast()
{
    Caster.Target = new InternalTarget(this);
}

public void Target(Mobile m)
{
    if (!Caster.CanSee(m))
    {
        Caster.SendMessage(MSG_COLOR_ERROR, "O alvo não pode ser visto.");
        return;
    }
    
    if (CheckHSequence(m))
    {
        Mobile source = Caster;
        
        // Check for magic reflect
        SpellHelper.NMSCheckReflect((int)Circle, ref source, ref m);
        
        // Calculate damage
        int damage = GetNMSDamage(25, 1, 5, m);
        
        // Visual effects
        source.MovingParticles(m, 0x36E4, 5, 0, false, false, 0, 0, 3600, 0, 0, 0);
        source.PlaySound(0x1E5);
        
        // Apply damage (phys, fire, cold, poison, energy percentages)
        SpellHelper.Damage(this, m, damage, 0, 100, 0, 0, 0);
    }
    
    FinishSequence();
}
```

### Pattern 2: Beneficial Spell

```csharp
public void Target(Mobile m)
{
    if (CheckBSequence(m))
    {
        // Apply buff/heal
        m.Heal(50);
        
        // Visual effects
        m.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
        m.PlaySound(0x202);
    }
    
    FinishSequence();
}
```

### Pattern 3: Area Effect Spell

```csharp
public override void OnCast()
{
    if (CheckSequence())
    {
        Map map = Caster.Map;
        List<Mobile> targets = new List<Mobile>();
        
        // Get targets in range
        foreach (Mobile m in Caster.GetMobilesInRange(5))
        {
            if (Caster.CanBeHarmful(m, false))
                targets.Add(m);
        }
        
        // Apply effect to each target
        foreach (Mobile m in targets)
        {
            Caster.DoHarmful(m);
            int damage = GetNMSDamage(15, 1, 4, m);
            SpellHelper.Damage(this, m, damage, 0, 100, 0, 0, 0);
        }
    }
    
    FinishSequence();
}
```

---

## Debugging

### Enable Debug Messages

Debug messages are only compiled in DEBUG builds:

```csharp
#if DEBUG
// Your debug code here
Caster.SendMessage(MSG_COLOR_DEBUG_1, "Debug info");
#endif
```

### Common Debug Scenarios

```csharp
#if DEBUG
// Damage calculation debugging
Caster.SendMessage(20, $"Base Damage: {baseDamage}");
Caster.SendMessage(21, $"After Bonuses: {finalDamage}");
Caster.SendMessage(22, $"Scalar Applied: {scalar}");
#endif
```

---

## Midland Region Special Rules

Midland has special spell mechanics based on Lucidity:

### Automatic Midland Handling

The base `Spell` class automatically handles Midland:
- **SDI** is disabled in Midland
- **FC/FCR** bonuses based on Lucidity
- **Damage** multipliers based on Lucidity

### Lucidity Thresholds

```csharp
MIDLAND_LUCIDITY_THRESHOLD_LOW = 0.50   // First tier bonus
MIDLAND_LUCIDITY_THRESHOLD_MED = 0.70   // Second tier bonus
MIDLAND_LUCIDITY_THRESHOLD_HIGH = 0.90  // Third tier bonus
```

**You don't need to check for Midland manually** - it's handled automatically in damage and timing calculations.

---

## Scroll Consumption

### Standard Scrolls

Automatically consumed when spell succeeds via `CheckSequence()`.

### Special Scroll Types

Some scrolls return a jar when consumed. The list is maintained in:

```csharp
private static readonly HashSet<Type> SCROLLS_CONSUMABLE_WITH_JAR
```

To add a new scroll type that returns a jar:

```csharp
private static readonly HashSet<Type> SCROLLS_CONSUMABLE_WITH_JAR = new HashSet<Type>
{
    // ... existing scrolls
    typeof(MyNewScroll),  // Add your scroll type here
};
```

---

## Custom Movement Behavior

### Override Movement Checks

```csharp
public override bool OnCasterMoving(Direction direction)
{
    // Your custom logic
    if (myCustomCondition)
    {
        Caster.SendMessage("Cannot move while channeling!");
        return false;
    }
    
    return base.OnCasterMoving(direction);
}
```

### Disable Movement Blocking

```csharp
public override bool BlocksMovement { get { return false; } }
```

---

## Performance Tips

### 1. Cache Heavy Calculations

```csharp
private int? m_CachedDamage = null;

private int GetCachedDamage()
{
    if (m_CachedDamage == null)
        m_CachedDamage = GetNMSDamage(25, 1, 5, false);
    
    return m_CachedDamage.Value;
}
```

### 2. Use HashSet for Type Checks

```csharp
private static readonly HashSet<Type> MY_SPECIAL_TYPES = new HashSet<Type>
{
    typeof(Type1), typeof(Type2), typeof(Type3)
};

// Fast O(1) lookup
if (MY_SPECIAL_TYPES.Contains(m_Scroll.GetType()))
{
    // Special handling
}
```

### 3. Avoid String Concatenation in Loops

```csharp
// Bad
string result = "";
for (int i = 0; i < 100; i++)
    result += words[i] + " ";

// Good
StringBuilder result = new StringBuilder();
for (int i = 0; i < 100; i++)
    result.Append(words[i]).Append(" ");
```

---

## Common Mistakes to Avoid

### ❌ DON'T: Modify Public Method Signatures

```csharp
// This will break 95+ spells!
public virtual int GetNMSDamage(int bonus, int dice, Mobile target) // Missing 'sides' parameter
```

### ❌ DON'T: Remove Virtual/Override Keywords

```csharp
// This breaks inheritance chain
public int CheckCast(Mobile caster) // Should be 'public virtual'
```

### ❌ DON'T: Hardcode User Messages

```csharp
// Bad - hard to maintain
Caster.SendMessage("Você não pode fazer isso");

// Good - use constants
Caster.SendMessage(MSG_COLOR_ERROR, UserMessages.ERROR_CANNOT_CAST_IN_STATE);
```

### ❌ DON'T: Use Magic Numbers

```csharp
// Bad
if (mageryValue >= 120.0) return 20;

// Good
if (mageryValue >= MAGERY_SKILL_120) return 20;
```

---

## Testing Your Spell

### Checklist

- [ ] Spell compiles without errors
- [ ] Spell works in PvE combat
- [ ] Spell works in PvP combat
- [ ] Magic Reflect works correctly
- [ ] Spell respects movement rules
- [ ] Spell consumes correct mana
- [ ] Spell consumes correct reagents
- [ ] Scrolls/wands work correctly
- [ ] Drunk state handled (if applicable)
- [ ] Midland mechanics work (if applicable)
- [ ] Effects and sounds play correctly
- [ ] No linter warnings

### Test Commands

```
[add <spell scroll name>
[cast <spell number>
[set magery 120
[set evalint 120
```

---

## Getting Help

### Documentation
- **This file**: Developer quick reference
- **Spell_Refactoring_Summary.md**: Detailed refactoring documentation
- **Administrator_Guide.md**: Server admin documentation

### Code Examples
- **MagicArrow.cs**: Simple damage spell
- **EnergyBolt.cs**: Advanced damage spell with effects
- **Explosion.cs**: Delayed damage spell

### Common Issues

**Issue:** "Cannot convert Mobile to bool"  
**Solution:** Check `GetNMSDamage()` overload - use `GetNMSDamage(bonus, dice, sides, target)` not `(bonus, dice, target)`

**Issue:** "Spell damage seems wrong"  
**Solution:** Verify you're using correct damage type percentages in `SpellHelper.Damage()`

**Issue:** "Spell doesn't work in Midland"  
**Solution:** Base class handles Midland automatically. Check `IsInMidland()` isn't being overridden incorrectly.

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 2.0 | Nov 6, 2025 | Major refactoring - constants, helpers, documentation |
| 1.0 | Prior | Original implementation |

---

**Need more help?** Check the inline XML documentation in Spell.cs - most methods now have detailed documentation comments.

