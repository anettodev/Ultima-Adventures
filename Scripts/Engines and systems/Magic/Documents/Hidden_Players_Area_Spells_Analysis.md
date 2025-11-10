# Hidden Players and Area Spell Interaction Analysis

## Issue

Area damage spells have **inconsistent behavior** when targeting hidden players.

## Current Behavior

### Field Spells (Indirect Damage)
- **Fire Field** (`Magery 4th/FireField.cs`)
- **Poison Field** (`Magery 5th/PoisonField.cs`)
- **Paralyze Field** (`Magery 6th/ParalyzeField.cs`)

**Status**: ❌ **CANNOT** damage hidden players

**Reason**: Uses `SpellHelper.ValidIndirectTarget()` which returns `false` for hidden mobiles:
```csharp
public static bool ValidIndirectTarget( Mobile from, Mobile to )
{
    if( to.Hidden || to.AccessLevel >= AccessLevel.Counselor) 
        return false;
    return true;
}
```

**Location**: `Scripts/Engines and systems/Magic/Base/Calculations/SpellHelper.cs` line 516

---

### Chain Lightning (7th Circle)
**File**: `Scripts/Engines and systems/Magic/Magery 7th/ChainLightning.cs`

**Status**: ✅ **CAN** damage hidden players

**Target Selection** (line 64):
```csharp
if ( Caster.Region == m.Region && Caster != m )
{
    targets.Add(m);
}
```

**Action**: Calls `m.RevealingAction()` at line 119 - reveals and damages hidden players

**Issue**: Does NOT use `ValidIndirectTarget` check

---

### Meteor Swarm (7th Circle)
**File**: `Scripts/Engines and systems/Magic/Magery 7th/MeteorSwarm.cs`

**Status**: ✅ **CAN** damage hidden players

**Target Selection** (line 65):
```csharp
if ( Caster.Region == m.Region && Caster != m && Caster.CanBeHarmful( m, true ) )
{
    targets.Add( m );
}
```

**Action**: Calls `m.RevealingAction()` at line 114 - reveals and damages hidden players

**Issue**: Does NOT use `ValidIndirectTarget` check

---

### Earthquake (8th Circle)
**File**: `Scripts/Engines and systems/Magic/Magery 8th/Earthquake.cs`

**Status**: ❌ **CANNOT** damage hidden players

**Target Selection** (line 41):
```csharp
if ( Caster.Region == m.Region && Caster != m && 
     SpellHelper.ValidIndirectTarget( Caster, m ) && 
     Caster.CanBeHarmful( m, false ) && 
     (!Core.AOS || Caster.InLOS( m )) )
{
    targets.Add(m);
}
```

**Action**: Uses `ValidIndirectTarget` - hidden players are excluded

---

## Summary Table

| Spell Type | Circle | Can Damage Hidden? | Reveals Hidden? | Uses ValidIndirectTarget? |
|------------|--------|-------------------|-----------------|---------------------------|
| **Fire Field** | 4th | ❌ No | - | ✅ Yes |
| **Poison Field** | 5th | ❌ No | - | ✅ Yes |
| **Paralyze Field** | 6th | ❌ No | - | ✅ Yes |
| **Chain Lightning** | 7th | ✅ Yes | ✅ Yes | ❌ No |
| **Meteor Swarm** | 7th | ✅ Yes | ✅ Yes | ❌ No |
| **Earthquake** | 8th | ❌ No | - | ✅ Yes |

## Recommended Action

**Decision needed**: Should area damage spells damage hidden players?

### Option A: All area spells CANNOT damage hidden
- Add `ValidIndirectTarget` check to Chain Lightning and Meteor Swarm
- **Consistent with**: Fields, Earthquake
- **Gameplay**: Hidden = safe from area spells

### Option B: All area spells CAN damage hidden
- Remove `ValidIndirectTarget` check from Earthquake and Field spells
- **Consistent with**: Chain Lightning, Meteor Swarm
- **Gameplay**: Area damage reveals hidden players

## Related Files

- **SpellHelper.cs**: `Scripts/Engines and systems/Magic/Base/Calculations/SpellHelper.cs`
- **Invisibility System**: `Scripts/Engines and systems/Magic/Magery 6th/Invisibility.cs`
- **PlayerMobile Damage**: `Scripts/Mobiles/PlayerMobile.cs` (line 4440 - reveals on damage)

---

**Date Identified**: 2025-11-09  
**Status**: Pending Review

