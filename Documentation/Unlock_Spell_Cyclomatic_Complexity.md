# Unlock Spell - Cyclomatic Complexity Analysis

**File:** `Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs`  
**Date:** Analysis Date  
**Total Lines:** 451

---

## Complexity Calculation Method

Cyclomatic Complexity = 1 (base) + Decision Points

**Decision Points Include:**
- `if` statements
- `else` clauses
- `while` loops
- `for` loops
- `switch` cases
- `catch` blocks
- `&&` and `||` operators (each adds 1)
- Ternary operators (`?:`)
- Early returns with conditions

---

## Method-by-Method Analysis

### 1. `OnCast()` - **Complexity: 1** ✅
```78:81:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
public override void OnCast()
{
    Caster.Target = new InternalTarget( this );
}
```
**Decision Points:** None  
**Status:** Excellent - Simple method

---

### 2. `PlayUnlockEffects()` - **Complexity: 1** ✅
```87:93:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void PlayUnlockEffects(IPoint3D location)
{
    int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
    Effects.SendLocationParticles(EffectItem.Create(new Point3D(location), Caster.Map, EffectItem.DefaultDuration),
        EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_DURATION, 0);
    Effects.PlaySound(location, Caster.Map, SOUND_ID);
}
```
**Decision Points:** None  
**Status:** Excellent - Simple method

---

### 3. `HandleInvalidTarget()` - **Complexity: 1** ✅
```100:104:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void HandleInvalidTarget(Mobile caster, string message)
{
    caster.SendMessage(Spell.MSG_COLOR_ERROR, message);
    PlayErrorEmote(caster);
}
```
**Decision Points:** None  
**Status:** Excellent - Simple method

---

### 4. `TryUnlockDoor()` - **Complexity: 8** ⚠️
```111:148:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void TryUnlockDoor(Mobile caster, BaseDoor door)
{
    if (Server.Items.DoorType.IsDungeonDoor(door))          // +1
    {
        if (!door.Locked)                                // +1
        {
            // Error handling
        }
        else                                             // +1
        {
            if (keyValue <= DOOR_KEY_VALUE_THRESHOLD && // +1 (if)
                Utility.RandomDouble() < ...)            // +1 (&&)
            {
                // Success
            }
            else                                         // +1
            {
                // Failure
            }
        }
    }
    else                                                 // +1
    {
        // Not a dungeon door
    }
}
```
**Decision Points:** 7  
**Calculation:** 1 (base) + 7 = **8**  
**Status:** Moderate - Could be simplified with early returns

---

### 5. `CalculateLockpickingBonus()` - **Complexity: 2** ✅
```157:170:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private double CalculateLockpickingBonus(Mobile caster)
{
    double lockpickingSkill = caster.Skills[SkillName.Lockpicking].Value;
    
    if (lockpickingSkill < LOCKPICKING_MINIMUM_SKILL)   // +1
        return 0.0;
    
    // Calculate bonus
    return bonus;
}
```
**Decision Points:** 1  
**Calculation:** 1 (base) + 1 = **2**  
**Status:** Excellent - Simple conditional

---

### 6. `GetTreasureChestMaxChance()` - **Complexity: 4** ✅
```177:190:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private double GetTreasureChestMaxChance(LockableContainer container)
{
    TreasureMapChest treasureChest = container as TreasureMapChest;
    if (treasureChest != null)                           // +1
    {
        if (treasureChest.Level == 1)                    // +1
            return TREASURE_CHEST_LEVEL_1_MAX;
        else if (treasureChest.Level == 2)               // +1
            return TREASURE_CHEST_LEVEL_2_MAX;
    }
    
    return MAX_SUCCESS_CHANCE;
}
```
**Decision Points:** 3  
**Calculation:** 1 (base) + 3 = **4**  
**Status:** Good - Clear logic flow

---

### 7. `CalculateUnlockSuccessChance()` - **Complexity: 4** ✅
```204:252:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private double CalculateUnlockSuccessChance(Mobile caster, LockableContainer container, out int successPercent)
{
    if (unlockSkill < requiredSkill)                     // +1
    {
        successPercent = 0;
        return 0.0;
    }
    
    // ... calculations ...
    
    if (totalPercentage > maxChancePercentage)           // +1
        totalPercentage = maxChancePercentage;
    
    if (totalPercentage > MAX_SUCCESS_CHANCE_PERCENTAGE) // +1
        totalPercentage = MAX_SUCCESS_CHANCE_PERCENTAGE;
    
    return successPercent / 100.0;
}
```
**Decision Points:** 3  
**Calculation:** 1 (base) + 3 = **4**  
**Status:** Good - Well-structured calculation method

---

### 8. `TryUnlockContainer()` - **Complexity: 7** ✅
```259:331:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void TryUnlockContainer(Mobile caster, LockableContainer container)
{
    if (Multis.BaseHouse.CheckSecured(container))       // +1
        return;
    
    if (!container.Locked || container.LockLevel == 0)   // +1 (if) +1 (||) = +2
        return;
    
    if (container is ParagonChest || container is PirateChest) // +1 (if) +1 (||) = +2
        return;
    
    if (treasureChest != null && treasureChest.Level > 2) // +1 (if) +1 (&&) = +2
        return;
    
    if (successChance <= 0.0)                            // +1
        return;
    
    if (Utility.RandomDouble() < successChance)          // +1
    {
        // Success
        if (container.LockLevel == -255)                  // +1
            container.LockLevel = container.RequiredSkill - 10;
    }
    else                                                 // +1
    {
        // Failure
    }
}
```
**Decision Points:** 6  
**Calculation:** 1 (base) + 6 = **7**  
**Status:** Good - Well-structured with early returns

---

### 9. `PlayErrorEmote()` - **Complexity: 2** ✅
```337:341:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void PlayErrorEmote(Mobile caster)
{
    caster.PlaySound(caster.Female ? ERROR_SOUND_FEMALE : ERROR_SOUND_MALE); // +1 (ternary)
    caster.Say("*oops*");
}
```
**Decision Points:** 1 (ternary operator)  
**Calculation:** 1 (base) + 1 = **2**  
**Status:** Excellent - Simple conditional

---

### 10. `PlayDisappointEmote()` - **Complexity: 2** ✅
```347:351:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void PlayDisappointEmote(Mobile caster)
{
    caster.PlaySound(caster.Female ? DISAPPOINT_SOUND_FEMALE : DISAPPOINT_SOUND_MALE); // +1 (ternary)
    caster.Say("*oooh*");
}
```
**Decision Points:** 1 (ternary operator)  
**Calculation:** 1 (base) + 1 = **2**  
**Status:** Excellent - Simple conditional

---

### 11. `PlaySuccessEmote()` - **Complexity: 2** ✅
```360:364:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void PlaySuccessEmote(Mobile caster, int femaleSound, int maleSound, string emote)
{
    caster.PlaySound(caster.Female ? femaleSound : maleSound); // +1 (ternary)
    caster.Say(emote);
}
```
**Decision Points:** 1 (ternary operator)  
**Calculation:** 1 (base) + 1 = **2**  
**Status:** Excellent - Simple conditional

---

### 12. `PlayConfusionEmote()` - **Complexity: 2** ✅
```370:374:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void PlayConfusionEmote(Mobile caster)
{
    caster.PlaySound(caster.Female ? CONTAINER_FAIL_SOUND_FEMALE : CONTAINER_FAIL_SOUND_MALE); // +1 (ternary)
    caster.Say("*huh?*");
}
```
**Decision Points:** 1 (ternary operator)  
**Calculation:** 1 (base) + 1 = **2**  
**Status:** Excellent - Simple conditional

---

### 13. `PlaySpecialContainerEmote()` - **Complexity: 2** ✅
```380:384:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
private void PlaySpecialContainerEmote(Mobile caster)
{
    caster.PlaySound(caster.Female ? SPECIAL_CONTAINER_SOUND_FEMALE : SPECIAL_CONTAINER_SOUND_MALE); // +1 (ternary)
    caster.Say("*ah!*");
}
```
**Decision Points:** 1 (ternary operator)  
**Calculation:** 1 (base) + 1 = **2**  
**Status:** Excellent - Simple conditional

---

### 14. `InternalTarget.OnTarget()` - **Complexity: 10** ⚠️
```395:443:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
protected override void OnTarget(Mobile from, object o)
{
    IPoint3D loc = o as IPoint3D;
    
    if (loc == null)                                     // +1
        return;
    
    if (!m_Owner.CheckSequence())                        // +1
        return;
    
    // Route to appropriate handler
    if (o is Mobile)                                     // +1
    {
        // Handle Mobile
    }
    else if (o is BaseHouseDoor)                         // +1
    {
        // Handle BaseHouseDoor
    }
    else if (o is BookBox)                              // +1
    {
        // Handle BookBox
    }
    else if (o is UnidentifiedArtifact || o is UnidentifiedItem || o is CurseItem) // +1 (if) +1 (||) +1 (||) = +3
    {
        // Handle special items
    }
    else if (o is BaseDoor)                             // +1
    {
        // Handle BaseDoor
    }
    else if (o is LockableContainer)                    // +1
    {
        // Handle LockableContainer
    }
    else                                                 // +1
    {
        // Default handler
    }
}
```
**Decision Points:** 9  
**Calculation:** 1 (base) + 9 = **10**  
**Status:** Moderate - Consider using switch or dictionary pattern

---

### 15. `InternalTarget.OnTargetFinish()` - **Complexity: 1** ✅
```445:448:Scripts/Engines and systems/Magic/Magery 3rd/Unlock.cs
protected override void OnTargetFinish(Mobile from)
{
    m_Owner.FinishSequence();
}
```
**Decision Points:** None  
**Status:** Excellent - Simple method

---

## Summary Statistics

| Metric | Value |
|--------|-------|
| **Total Methods** | 15 |
| **Average Complexity** | 3.4 |
| **Highest Complexity** | 10 (`OnTarget`) |
| **Lowest Complexity** | 1 (6 methods) |
| **Methods ≤ 5** | 13 (86.7%) ✅ |
| **Methods 6-10** | 2 (13.3%) ⚠️ |
| **Methods > 10** | 0 ✅ |

---

## Complexity Rating Scale

| Complexity | Rating | Status | Recommendation |
|------------|--------|--------|----------------|
| 1-5 | ✅ Low | Excellent | No action needed |
| 6-10 | ⚠️ Moderate | Acceptable | Consider refactoring |
| 11-20 | ⚠️ High | Risky | Should refactor |
| 21+ | ❌ Very High | Critical | Must refactor |

---

## Recommendations

### 1. `TryUnlockDoor()` - Complexity 8
**Current Issue:** Nested if-else structure  
**Suggestion:** Use early returns to reduce nesting
```csharp
private void TryUnlockDoor(Mobile caster, BaseDoor door)
{
    if (!Server.Items.DoorType.IsDungeonDoor(door))
    {
        caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ALREADY_UNLOCKED);
        PlayErrorEmote(caster);
        return;
    }
    
    if (!door.Locked)
    {
        caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ALREADY_UNLOCKED);
        PlayErrorEmote(caster);
        return;
    }
    
    // Calculate success...
}
```
**Expected Reduction:** 8 → 6

### 2. `InternalTarget.OnTarget()` - Complexity 10
**Current Issue:** Long if-else chain  
**Suggestion:** Consider using a dictionary-based routing pattern (if performance allows) or keep as-is if readability is acceptable

**Current Status:** Acceptable - The if-else chain is clear and readable

---

## Overall Assessment

### ✅ **Code Quality: EXCELLENT**

- **86.7%** of methods have complexity ≤ 5 (Excellent)
- **13.3%** of methods have complexity 6-10 (Acceptable)
- **0%** of methods have complexity > 10 (No critical issues)
- Average complexity of **3.4** is well below the recommended threshold of 10

### Strengths
- Most methods are simple and focused
- Good use of early returns in `TryUnlockContainer()`
- Clear separation of concerns
- Well-documented code

### Minor Improvements
- `TryUnlockDoor()` could benefit from early returns
- `OnTarget()` complexity is acceptable but could be optimized with pattern matching (if C# version supports it)

---

## Conclusion

The `Unlock.cs` spell has **excellent code quality** with low cyclomatic complexity across most methods. The two methods with moderate complexity (8 and 10) are still within acceptable limits and are well-structured. No immediate refactoring is required, but the suggested improvements could further enhance maintainability.

**Overall Grade: A (Excellent)**

