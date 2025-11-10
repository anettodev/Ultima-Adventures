using System;
using Server.Targeting;
using Server.Network;
using Server.Items;
using System.Collections.Generic;

namespace Server.Spells.Second
{
	/// <summary>
	/// Remove Trap - 2nd Circle Utility Spell
	/// Removes traps from containers or creates a protection wand when targeting self
	/// </summary>
	public class RemoveTrapSpell : MagerySpell
	{
		#region Constants
		// Skill Check Constants
		private const int MAGERY_LEVEL_DIVISOR = 3;
		private const int SKILL_CHECK_VARIANCE = 1; // 0-1 random penalty

		// Trap Wand Constants (Balance Nerf)
		private const int WAND_BASE_POWER = 20;
		private const int WAND_MAX_POWER = 50;

		// Effect Constants
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_RENDER = 32;
		private const int EFFECT_DURATION_SUCCESS = 5015;
		private const int EFFECT_DURATION_WAND = 5008;
		private const int SOUND_SUCCESS = 0x1F0;
		private const int SOUND_SUCCESS_ALT = 61;
		private const int SOUND_FAIL = 10;
		private const int SOUND_WAND = 0x1ED;

		// Message Colors
		private const int MSG_COLOR_INSTRUCTION = 95;
		private const int MSG_COLOR_SUCCESS = 2253;

		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Remove Trap", "An Jux",
				212,
				9001,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public RemoveTrapSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
			Caster.SendMessage(MSG_COLOR_INSTRUCTION, Spell.SpellMessages.REMOVE_TRAP_INSTRUCTION);
		}

		public void Target(TrapableContainer container)
		{
			if (!Caster.CanSee(container))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (CheckSequence())
			{
				if (TryRemoveTrap(container))
				{
					HandleTrapRemovalSuccess(container);
				}
				else
				{
					HandleTrapRemovalFailed(container);
				}
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates caster's trap removal skill level
		/// </summary>
		private int CalculateRemovalSkillLevel()
		{
			int skillLevel = (int)((Caster.Skills[SkillName.Magery].Value) / MAGERY_LEVEL_DIVISOR);
			skillLevel -= Utility.RandomMinMax(0, SKILL_CHECK_VARIANCE);
			return skillLevel;
		}

		/// <summary>
		/// Calculates success percentage for trap removal
		/// Formula: Effective skill = (Magery / 3) - Random(0-1)
		/// Success when: Effective skill >= TrapLevel
		/// 
		/// Base Calculation:
		/// - Base skill = Magery / 3
		/// - Min effective = Base - 1
		/// - Max effective = Base
		/// - Success when: Base - random >= TrapLevel, so random <= Base - TrapLevel
		/// - If Base - 1 >= TrapLevel: 100% success
		/// - If Base >= TrapLevel: (Base - TrapLevel) * 100%
		/// - If Base < TrapLevel: (Base / TrapLevel) * 30% * 100% (diminishing returns)
		/// 
		/// Remove Trap Skill Bonus:
		/// - Each 10 points of Remove Trap skill = +1% success chance
		/// - Maximum bonus: 12% (at 120 skill)
		/// - Bonus is added to base success percentage
		/// </summary>
		private double CalculateSuccessPercentage(int trapLevel)
		{
			double mageryValue = Caster.Skills[SkillName.Magery].Value;
			double baseSkill = mageryValue / MAGERY_LEVEL_DIVISOR;
			double minEffectiveSkill = baseSkill - SKILL_CHECK_VARIANCE;

			double baseSuccessPercent = 0.0;

			// Calculate base success percentage
			if (minEffectiveSkill >= trapLevel)
			{
				// Guaranteed success: minimum effective skill exceeds trap level
				baseSuccessPercent = 100.0;
			}
			else if (baseSkill > trapLevel)
			{
				// Partial success: base skill exceeds trap level
				// Success % = (Base - TrapLevel) * 100%
				double successChance = baseSkill - trapLevel;
				baseSuccessPercent = Math.Max(0.0, Math.Min(100.0, successChance * 100.0));
			}
			else
			{
				// Base skill <= trap level: use diminishing returns formula
				// Success % = (Base Skill / TrapLevel) * 30%
				// This handles both exact equality (Base = TrapLevel) and below (Base < TrapLevel)
				// Example: Magery 120 (Base 40) vs Trap Level 40 = (40/40) * 30% = 30%
				// Example: Magery 120 (Base 40) vs Trap Level 50 = (40/50) * 30% = 24%
				double skillRatio = baseSkill / trapLevel;
				baseSuccessPercent = skillRatio * 30.0; // 30% scaling factor
			}

			// Add Remove Trap skill bonus (1% per 10 skill points, max 12% at 120)
			double removeTrapSkill = Caster.Skills[SkillName.RemoveTrap].Value;
			double removeTrapBonus = Math.Floor(removeTrapSkill / 10.0); // 1% per 10 points
			double finalSuccessPercent = baseSuccessPercent + removeTrapBonus;

			// Cap at 100%
			return Math.Max(0.0, Math.Min(100.0, finalSuccessPercent));
		}

		/// <summary>
		/// Attempts to remove trap from container
		/// </summary>
		private bool TryRemoveTrap(TrapableContainer container)
		{
			int casterSkill = CalculateRemovalSkillLevel();
			return casterSkill >= container.TrapLevel;
		}

		/// <summary>
		/// Handles successful trap removal
		/// </summary>
		private void HandleTrapRemovalSuccess(TrapableContainer container)
		{
			Point3D loc = container.GetWorldLocation();

			PlayRemovalSuccessEffects(loc, container.Map);
			
			// Calculate and display success percentage
			double successPercent = CalculateSuccessPercentage(container.TrapLevel);
			string successMessage = string.Format(Spell.SpellMessages.REMOVE_TRAP_SUCCESS_WITH_PERCENT, (int)successPercent);
			Caster.SendMessage(MSG_COLOR_SUCCESS, successMessage);

			// Clear trap
			container.TrapType = TrapType.None;
			container.TrapPower = 0;
			container.TrapLevel = 0;
		}

		/// <summary>
		/// Handles failed trap removal
		/// </summary>
		private void HandleTrapRemovalFailed(TrapableContainer container)
		{
			Point3D loc = container.GetWorldLocation();

			Caster.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_ERROR, true, "* aff! *");
			Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.REMOVE_TRAP_FAILED);
			Effects.PlaySound(loc, container.Map, SOUND_FAIL);
		}

		/// <summary>
		/// Plays effects for successful trap removal
		/// </summary>
		private void PlayRemovalSuccessEffects(Point3D loc, Map map)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			Effects.SendLocationParticles(
				EffectItem.Create(loc, map, EffectItem.DefaultDuration), 
				EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_DURATION_SUCCESS, 0);
			Effects.PlaySound(loc, map, SOUND_SUCCESS);
			Effects.PlaySound(loc, map, SOUND_SUCCESS_ALT);
		}

		/// <summary>
		/// Creates protection wand when targeting self
		/// </summary>
		public void CreateProtectionWand()
		{
			// Delete existing wands
			DeleteExistingWands();

			// Play effects
			Caster.PlaySound(SOUND_WAND);
			PlayWandCreationEffects();
			Caster.SendMessage(MSG_COLOR_SUCCESS, SpellMessages.REMOVE_TRAP_WAND_CREATED);

			// Create new wand
			TrapWand wand = new TrapWand(Caster);
			wand.WandPower = CalculateWandPower();
			Caster.AddToBackpack(wand);
		}

		/// <summary>
		/// Deletes all existing trap wands owned by caster
		/// </summary>
		private void DeleteExistingWands()
		{
			List<Item> existingWands = new List<Item>();

		foreach (Item item in World.Items.Values)
		{
			if (item is TrapWand)
			{
				TrapWand wand = (TrapWand)item;
				if (wand.owner == Caster)
				{
					existingWands.Add(item);
				}
			}
		}

			foreach (Item wand in existingWands)
			{
				wand.Delete();
			}
		}

		/// <summary>
		/// Calculates power level for created trap wand
		/// </summary>
		private int CalculateWandPower()
		{
			int power = (int)(NMSUtils.getBeneficialMageryInscribePercentage(Caster) + WAND_BASE_POWER);

			if (power > WAND_MAX_POWER)
				power = WAND_MAX_POWER;

			return power;
		}

		/// <summary>
		/// Plays effects for wand creation
		/// </summary>
		private void PlayWandCreationEffects()
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			Caster.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION_WAND, hue, 0, EffectLayer.Waist);
		}

		private class InternalTarget : Target
		{
			private RemoveTrapSpell m_Owner;

			public InternalTarget(RemoveTrapSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.None)
			{
				m_Owner = owner;
			}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is TrapableContainer)
			{
				m_Owner.Target((TrapableContainer)o);
			}
			else if (from == o) // Self-target for wand creation
				{
					if (m_Owner.CheckSequence())
					{
						m_Owner.CreateProtectionWand();
					}
					m_Owner.FinishSequence();
				}
				else
				{
					from.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.REMOVE_TRAP_INVALID);
				}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
