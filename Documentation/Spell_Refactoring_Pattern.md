# Spell Refactoring Pattern Guide

**Purpose:** Step-by-step guide for refactoring all Magery spells  
**Applies To:** All 64 Magery spells (Circles 1-8)  
**Pattern Established:** November 6, 2025

---

## Overview

This document provides the exact pattern for refactoring all Magery spells using the principles established in the `Spell.cs` refactoring. Two complete examples are provided:
- **MagicArrow.cs** - Attack spell pattern
- **Heal.cs** - Beneficial spell pattern

---

## Refactoring Principles

### Core Principles (Apply to ALL Spells)
1. **DRY** - Don't Repeat Yourself
2. **KISS** - Keep It Simple, Stupid
3. **Extract Constants** - No magic numbers
4. **Centralize Messages** - Use `SpellMessages` class
5. **Extract Methods** - Break complex logic into focused methods
6. **Add Documentation** - XML comments for all public/protected methods
7. **Remove Dead Code** - No commented-out code
8. **EN-US Code** - All variables, methods, comments in English
9. **PT-BR Strings** - User-facing messages only

---

## Step-by-Step Refactoring Process

### Step 1: Add XML Documentation Header

**Before:**
```csharp
namespace Server.Spells.First
{
	public class MagicArrowSpell : MagerySpell
	{
```

**After:**
```csharp
namespace Server.Spells.First
{
	/// <summary>
	/// Magic Arrow - 1st Circle Attack Spell
	/// Fires a magical projectile at the target
	/// </summary>
	public class MagicArrowSpell : MagerySpell
	{
```

**Pattern:**
```csharp
/// <summary>
/// [Spell Name] - [Circle]th Circle [Type] Spell
/// [Brief description of what it does]
/// </summary>
```

---

### Step 2: Extract Constants Region

**Before:**
```csharp
public class MagicArrowSpell : MagerySpell
{
	private static SpellInfo m_Info = new SpellInfo(...);
	
	public override void OnCast()
	{
		damage = GetNMSDamage(2, 1, 3, m) + nBenefit;
		if (damage >= 8) damage = 8;
		
		source.MovingParticles(m, 0x36E4, 5, 0, ...);
		source.PlaySound(0x1E5);
	}
}
```

**After:**
```csharp
public class MagicArrowSpell : MagerySpell
{
	#region Constants
	// Damage Constants
	private const int DAMAGE_BONUS = 2;
	private const int DAMAGE_DICE = 1;
	private const int DAMAGE_SIDES = 3;
	private const int DAMAGE_CAP = 8;
	
	// Effect Constants
	private const int EFFECT_ID = 0x36E4;
	private const int EFFECT_SPEED = 5;
	private const int EFFECT_DURATION = 3600;
	private const int SOUND_ID = 0x1E5;
	
	// Target Constants
	private const int TARGET_RANGE_ML = 10;
	private const int TARGET_RANGE_LEGACY = 12;
	#endregion

	private static SpellInfo m_Info = new SpellInfo(...);
```

**What to Extract:**
- Damage values (bonus, dice, sides, caps)
- Effect IDs (particle effects, animations)
- Sound IDs
- Duration values
- Range values
- Multipliers and divisors
- Magic numbers of any kind

---

### Step 3: Replace Hard-Coded PT-BR Strings

**Before:**
```csharp
if (!Caster.CanSee(m))
{
	Caster.SendMessage(55, "O alvo não pode ser visto.");
}
```

**After:**
```csharp
if (!Caster.CanSee(target))
{
	Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE);
}
```

**Available Messages in `Spell.SpellMessages`:**
```csharp
// Common Errors
ERROR_TARGET_NOT_VISIBLE
ERROR_TARGET_ALREADY_DEAD
ERROR_CANNOT_HEAL_DEAD
ERROR_CANNOT_HEAL_GOLEM
ERROR_TARGET_MORTALLY_POISONED_SELF
ERROR_TARGET_MORTALLY_POISONED_OTHER
ERROR_SOMETHING_PREVENTED_CAST

// Resist Messages
RESIST_SPELL_EFFECTS
RESIST_HALF_DAMAGE_VICTIM
RESIST_HALF_DAMAGE_ATTACKER

// One Ring Messages
ONE_RING_PREVENTED_SPELL
ONE_RING_PROTECTION_REVEAL
```

**If you need a NEW message:**
1. Add it to `Spell.cs` in the `SpellMessages` class
2. Group it logically (errors, info, resist, etc.)
3. Then use it in your spell

---

### Step 4: Extract Complex Logic into Methods

**Before:**
```csharp
public void Target(Mobile m)
{
	if (!Caster.CanSee(m))
	{
		Caster.SendMessage(55, "O alvo não pode ser visto.");
	}
	else if (m.IsDeadBondedPet || m is BaseCreature && ((BaseCreature)m).IsAnimatedDead)
	{
		Caster.SendMessage(55, "Você não pode curar aquilo que já está morto.");
	}
	else if (m is PlayerMobile && m.FindItemOnLayer(Layer.Ring) != null && m.FindItemOnLayer(Layer.Ring) is OneRing)
	{
		Caster.SendMessage(33, "O UM ANEL desfez o feitiço...");
		DoFizzle();
		return;
	}
	else if (m is Golem)
	{
		DoFizzle();
		Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, false, "* Não sei como curar isso *");
	}
	else if ((m.Poisoned && m.Poison.Level >= 3) || MortalStrike.IsWounded(m))
	{
		Caster.SendMessage(33, ((Caster == m) ? "Você sente o veneno..." : "O seu alvo está..."));
	}
	else if (CheckBSequence(m))
	{
		// actual spell logic
	}
	
	FinishSequence();
}
```

**After:**
```csharp
public void Target(Mobile target)
{
	if (!Caster.CanSee(target))
	{
		Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE);
	}
	else if (!CanHealTarget(target))
	{
		// Error messages handled in CanHealTarget
	}
	else if (CheckBSequence(target))
	{
		SpellHelper.Turn(Caster, target);
		
		int healAmount = CalculateHealAmount(target);
		SpellHelper.Heal(healAmount, target, Caster);
		
		PlayEffects(target);
	}
	
	FinishSequence();
}

/// <summary>
/// Validates if target can be healed
/// </summary>
private bool CanHealTarget(Mobile target)
{
	// Check if target is dead/undead
	if (target.IsDeadBondedPet || (target is BaseCreature creature && creature.IsAnimatedDead))
	{
		Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_CANNOT_HEAL_DEAD);
		return false;
	}
	
	// Check for One Ring (Easter Egg)
	if (HasOneRing(target))
	{
		Caster.SendMessage(MSG_COLOR_WARNING, SpellMessages.ONE_RING_PREVENTED_SPELL);
		DoFizzle();
		return false;
	}
	
	// ... other checks
	
	return true;
}

/// <summary>
/// Checks if target has One Ring equipped
/// </summary>
private bool HasOneRing(Mobile target)
{
	return target is PlayerMobile 
		&& target.FindItemOnLayer(Layer.Ring) is OneRing;
}
```

**Methods to Extract:**
- Validation logic → `CanHealTarget()`, `IsValidTarget()`, etc.
- Damage calculations → `CalculateDamage()`
- Effect playing → `PlayEffects()`
- Complex conditionals → Named boolean methods

---

### Step 5: Add Method Documentation

**Before:**
```csharp
private double CalculateDamage(Mobile target)
{
	double damage = GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
	if (damage >= DAMAGE_CAP) damage = DAMAGE_CAP;
	return damage;
}
```

**After:**
```csharp
/// <summary>
/// Calculates spell damage with cap
/// </summary>
/// <param name="target">The target mobile</param>
/// <returns>Calculated damage amount</returns>
private double CalculateDamage(Mobile target)
{
	double damage = GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
	
	// Apply damage cap for balance
	if (damage >= DAMAGE_CAP)
	{
		damage = DAMAGE_CAP;
	}
	
	return damage;
}
```

---

### Step 6: Add Inline Comments for Damage Types

**Before:**
```csharp
SpellHelper.Damage(this, target, damage, 0, 100, 0, 0, 0);
```

**After:**
```csharp
// Apply damage (0% physical, 100% fire, 0% cold, 0% poison, 0% energy)
SpellHelper.Damage(this, target, damage, 0, 100, 0, 0, 0);
```

**Damage Type Reference:**
- Position 1: Physical (0-100%)
- Position 2: Fire (0-100%)
- Position 3: Cold (0-100%)
- Position 4: Poison (0-100%)
- Position 5: Energy (0-100%)

---

### Step 7: Remove Commented-Out Code

**Before:**
```csharp
int nBenefit = 0;
/*if (Caster is PlayerMobile) // WIZARD
{
	nBenefit = CalculateMobileBenefit(Caster, 60, 5);
}*/

damage = GetNMSDamage(2, 1, 3, m) + nBenefit;
```

**After:**
```csharp
double damage = CalculateDamage(target);
```

**What to Remove:**
- Commented WIZARD code
- Old commented formulas
- Commented TODOs (document them separately if needed)
- Any other dead code

---

### Step 8: Improve Target Class

**Before:**
```csharp
private class InternalTarget : Target
{
	private MagicArrowSpell m_Owner;
	
	public InternalTarget(MagicArrowSpell owner) : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
	{
		m_Owner = owner;
	}
	
	protected override void OnTarget(Mobile from, object o)
	{
		if (o is Mobile)
		{
			m_Owner.Target((Mobile)o);
		}
	}
	
	protected override void OnTargetFinish(Mobile from)
	{
		m_Owner.FinishSequence();
	}
}
```

**After:**
```csharp
private class InternalTarget : Target
{
	private MagicArrowSpell m_Owner;
	
	public InternalTarget(MagicArrowSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful)
	{
		m_Owner = owner;
	}
	
	protected override void OnTarget(Mobile from, object o)
	{
		if (o is Mobile mobile)
		{
			m_Owner.Target(mobile);
		}
	}
	
	protected override void OnTargetFinish(Mobile from)
	{
		m_Owner.FinishSequence();
	}
}
```

**Changes:**
- Use constants for ranges
- Use pattern matching (`is Mobile mobile`)
- Keep structure consistent

---

## Complete Examples

### Attack Spell Pattern: MagicArrow.cs

```csharp
using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.First
{
	/// <summary>
	/// Magic Arrow - 1st Circle Attack Spell
	/// Fires a magical projectile at the target
	/// </summary>
	public class MagicArrowSpell : MagerySpell
	{
		#region Constants
		private const int DAMAGE_BONUS = 2;
		private const int DAMAGE_DICE = 1;
		private const int DAMAGE_SIDES = 3;
		private const int DAMAGE_CAP = 8;
		
		private const int EFFECT_ID = 0x36E4;
		private const int EFFECT_SPEED = 5;
		private const int EFFECT_DURATION = 3600;
		private const int SOUND_ID = 0x1E5;
		
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Magic Arrow", "In Por Ylem",
				212,
				9041,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public MagicArrowSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override bool DelayedDamageStacking { get { return !Core.AOS; } }
		public override bool DelayedDamage { get { return true; } }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public void Target(Mobile target)
		{
			if (!Caster.CanSee(target))
			{
				Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (CheckHSequence(target))
			{
				Mobile source = Caster;

				SpellHelper.Turn(source, target);
				SpellHelper.NMSCheckReflect((int)Circle, ref source, ref target);

				double damage = CalculateDamage(target);
				PlayEffects(source, target);

				// Apply damage (0% physical, 100% fire, 0% cold, 0% poison, 0% energy)
				SpellHelper.Damage(this, target, damage, 0, 100, 0, 0, 0);
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates spell damage with cap
		/// </summary>
		private double CalculateDamage(Mobile target)
		{
			double damage = GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
			
			// Apply damage cap for balance
			if (damage >= DAMAGE_CAP)
			{
				damage = DAMAGE_CAP;
			}

			return damage;
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		private void PlayEffects(Mobile source, Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			source.MovingParticles(target, EFFECT_ID, EFFECT_SPEED, 0, false, false, hue, 0, EFFECT_DURATION, 0, 0, 0);
			source.PlaySound(SOUND_ID);
		}

		private class InternalTarget : Target
		{
			private MagicArrowSpell m_Owner;

			public InternalTarget(MagicArrowSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Mobile mobile)
				{
					m_Owner.Target(mobile);
				}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
```

---

### Beneficial Spell Pattern: Heal.cs

See `Heal.cs` for complete beneficial spell example with complex validation logic.

---

## Spell-Specific Patterns

### For Area Effect Spells

```csharp
/// <summary>
/// Gets valid targets in area
/// </summary>
private List<Mobile> GetTargetsInArea()
{
	List<Mobile> targets = new List<Mobile>();
	
	foreach (Mobile m in Caster.GetMobilesInRange(EFFECT_RANGE))
	{
		if (IsValidTarget(m))
		{
			targets.Add(m);
		}
	}
	
	return targets;
}

/// <summary>
/// Validates if mobile is a valid target
/// </summary>
private bool IsValidTarget(Mobile target)
{
	return Caster.Region == target.Region 
		&& Caster != target 
		&& SpellHelper.ValidIndirectTarget(Caster, target) 
		&& Caster.CanBeHarmful(target, false) 
		&& (!Core.AOS || Caster.InLOS(target));
}
```

### For Field Spells

```csharp
#region Constants
private const int FIELD_LENGTH = 5;
private const int FIELD_DURATION_BASE = 30;
private const int DAMAGE_PER_TICK = 10;
#endregion

// Field creation logic with constants
```

### For Summon Spells

```csharp
#region Constants
private const double DURATION_SCALE_BASE = 1.0;
private const double SORCERER_SCALE_BONUS = 0.50;
private const int SUMMON_SOUND = 0x217;
#endregion

/// <summary>
/// Calculates summon duration with sorcerer bonus
/// </summary>
private TimeSpan CalculateSummonDuration()
{
	double scale = DURATION_SCALE_BASE + ((Caster.Skills[SkillName.Magery].Value - 90.0) / 100.0);
	
	if (Caster is PlayerMobile playerMobile && playerMobile.Sorcerer())
	{
		scale += SORCERER_SCALE_BONUS;
	}
	
	return TimeSpan.FromSeconds(BASE_DURATION * scale);
}
```

---

## Checklist for Each Spell

Use this checklist when refactoring each spell:

- [ ] Add XML documentation header
- [ ] Create Constants region
- [ ] Extract all magic numbers to constants
- [ ] Replace PT-BR strings with `SpellMessages`
- [ ] Rename parameter `m` to `target` (for consistency)
- [ ] Extract validation logic to methods
- [ ] Extract damage calculation to method
- [ ] Extract effects to method
- [ ] Add XML documentation to all methods
- [ ] Add inline comment for damage type
- [ ] Remove all commented-out code
- [ ] Update InternalTarget to use constants
- [ ] Use pattern matching where possible
- [ ] Test compilation
- [ ] Verify spell still works correctly

---

## Common Patterns Reference

### Damage Calculation Pattern
```csharp
/// <summary>
/// Calculates spell damage
/// </summary>
private double CalculateDamage(Mobile target)
{
	return GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
}
```

### Effect Playing Pattern
```csharp
/// <summary>
/// Plays visual and sound effects
/// </summary>
private void PlayEffects(Mobile target)
{
	int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
	target.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION, hue, 0, EffectLayer.Waist);
	target.PlaySound(SOUND_ID);
}
```

### Validation Pattern
```csharp
/// <summary>
/// Validates if target can be affected by spell
/// </summary>
private bool IsValidTarget(Mobile target)
{
	if (condition1)
	{
		SendErrorMessage();
		return false;
	}
	
	if (condition2)
	{
		SendErrorMessage();
		return false;
	}
	
	return true;
}
```

---

## Remaining Work

### Completed (2/64 spells)
- ✅ MagicArrow.cs - 1st Circle Attack
- ✅ Heal.cs - 1st Circle Beneficial

### Remaining by Circle

**First Circle (6 remaining):**
- Clumsy.cs
- CreateFood.cs
- Feeblemind.cs
- NightSight.cs
- ReactiveArmor.cs
- Weaken.cs

**Second Circle (8 remaining):**
- Agility.cs
- Cunning.cs
- Cure.cs
- Harm.cs
- MagicTrap.cs
- Protection.cs
- RemoveTrap.cs
- Strength.cs

**Third Circle (8 remaining):**
- Bless.cs
- Fireball.cs
- MagicLock.cs
- Poison.cs
- Telekinesis.cs
- Teleport.cs
- Unlock.cs
- WallOfStone.cs

**Fourth Circle (8 remaining):**
- ArchCure.cs
- ArchProtection.cs
- Curse.cs
- FireField.cs
- GreaterHeal.cs
- Lightning.cs
- ManaDrain.cs
- Recall.cs

**Fifth Circle (8 remaining):**
- BladeSpirits.cs
- DispelField.cs
- Incognito.cs
- MagicReflect.cs
- MindBlast.cs
- Paralyze.cs
- PoisonField.cs
- SummonCreature.cs

**Sixth Circle (8 remaining):**
- Dispel.cs
- EnergyBolt.cs
- Explosion.cs
- Invisibility.cs
- Mark.cs
- MassCurse.cs
- ParalyzeField.cs
- Reveal.cs

**Seventh Circle (8 remaining):**
- ChainLightning.cs
- EnergyField.cs
- FlameStrike.cs
- GateTravel.cs
- ManaVampire.cs
- MassDispel.cs
- MeteorSwarm.cs
- Polymorph.cs

**Eighth Circle (8 remaining):**
- AirElemental.cs
- Earthquake.cs
- EarthElemental.cs
- EnergyVortex.cs
- FireElemental.cs
- Resurrection.cs
- SummonDaemon.cs
- WaterElemental.cs

**Total Remaining: 62 spells**

---

## Time Estimate

- **Per Spell:** 5-10 minutes (simple to complex)
- **Simple Spells:** 5 minutes (basic attack/buff)
- **Complex Spells:** 10 minutes (multiple validations, special mechanics)
- **Total Remaining Time:** 5-10 hours

---

## Next Steps

### Option 1: Continue Automated Refactoring
Continue refactoring all 62 remaining spells using this pattern.

### Option 2: Manual Refactoring
Use this pattern guide to refactor spells manually or in smaller batches.

### Option 3: Hybrid Approach
Refactor one complete circle at a time, test, then continue.

---

## Quality Assurance

After refactoring each spell:

1. **Compile Check** - Ensure no syntax errors
2. **Logic Check** - Verify formula matches original
3. **Message Check** - All PT-BR strings use `SpellMessages`
4. **Constant Check** - No magic numbers remain
5. **Documentation Check** - All methods have XML docs
6. **Clean Code Check** - No commented code, good naming

---

## Conclusion

This pattern provides a complete, repeatable process for refactoring all Magery spells to match the quality standards established in the `Spell.cs` refactoring. By following this pattern consistently, all spells will have:

- ✅ Clear, self-documenting code
- ✅ Centralized message management
- ✅ No magic numbers
- ✅ Consistent structure
- ✅ Better maintainability
- ✅ Improved readability

The pattern is proven with two complete examples (MagicArrow and Heal) and ready for application across all remaining spells.

