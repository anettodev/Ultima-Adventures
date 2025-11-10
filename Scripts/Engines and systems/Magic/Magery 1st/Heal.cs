using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.First
{
	/// <summary>
	/// Heal - 1st Circle Beneficial Spell
	/// Restores hit points to the target
	/// </summary>
	public class HealSpell : MagerySpell
	{
	#region Constants
	private const int HEAL_DIVISOR = 3;
	private const double OTHER_TARGET_MULTIPLIER = 1.2; // 20% bonus when healing others
	private const double HEAL_POWER_MODIFIER = 0.7; // 30% reduction from base (70% of original)
	private const int MINIMUM_HEAL_AMOUNT = 1;
	private const double MINIMUM_MAGERY_REQUIRED = 10.0;
	private const int DEADLY_POISON_LEVEL = 3;
	
	// Random variance
	private const double RANDOM_REDUCTION_MIN = 0.04; // 2% minimum reduction
	private const double RANDOM_REDUCTION_MAX = 0.10; // 5% maximum reduction
	private const int MINIMUM_REDUCTION_AMOUNT = 1; // Minimum 1 HP must be reduced
	
	// Consecutive cast penalty (anti-spam)
	private const double CONSECUTIVE_CAST_WINDOW = 2; // 2 seconds window
	private const double CONSECUTIVE_CAST_PENALTY = 0.25; // 25% reduction
	private static readonly System.Collections.Generic.Dictionary<Mobile, DateTime> LastCastTimes = new System.Collections.Generic.Dictionary<Mobile, DateTime>();
	
	private const int EFFECT_ID = 0x376A;
	private const int EFFECT_SPEED = 9;
	private const int EFFECT_RENDER = 32;
	private const int EFFECT_DURATION = 5005;
	private const int SOUND_ID = 0x1F2;
	
	private const int TARGET_RANGE_ML = 11;
	private const int TARGET_RANGE_LEGACY = 12;
	
	private const int OVERHEAD_MESSAGE_HUE = 0x3B2;
	#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Heal", "In Mani",
				224,
				9061,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public HealSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

	public override void OnCast()
	{
		// Check minimum Magery requirement
		if (Caster.Skills[SkillName.Magery].Value < MINIMUM_MAGERY_REQUIRED)
		{
			Caster.SendMessage(MSG_COLOR_ERROR, string.Format("Você precisa de pelo menos {0} pontos de Magery para usar este feitiço.", MINIMUM_MAGERY_REQUIRED));
			return;
		}

		Caster.Target = new InternalTarget(this);
	}

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
			ShowHealAmount(target, healAmount, IsConsecutiveCast());
			
			// Update last cast time for this caster
			UpdateLastCastTime();
		}

		FinishSequence();
	}

	/// <summary>
	/// Validates if target can be healed
	/// </summary>
	private bool CanHealTarget(Mobile target)
	{
		// Check if target is dead/undead
		BaseCreature creature = target as BaseCreature;
		if (target.IsDeadBondedPet || (creature != null && creature.IsAnimatedDead))
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

			// Check if target is a Golem
			if (target is Golem)
			{
				DoFizzle();
				Caster.LocalOverheadMessage(MessageType.Regular, OVERHEAD_MESSAGE_HUE, false, SpellMessages.ERROR_CANNOT_HEAL_GOLEM);
				return false;
			}

			// Check for deadly poison or mortal strike
			if (IsTargetMortallyWounded(target))
			{
				string message = (Caster == target) 
					? SpellMessages.ERROR_TARGET_MORTALLY_POISONED_SELF 
					: SpellMessages.ERROR_TARGET_MORTALLY_POISONED_OTHER;
				Caster.SendMessage(MSG_COLOR_WARNING, message);
				return false;
			}

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

		/// <summary>
		/// Checks if target is mortally wounded (deadly poison or mortal strike)
		/// </summary>
		private bool IsTargetMortallyWounded(Mobile target)
		{
			return (target.Poisoned && target.Poison.Level >= DEADLY_POISON_LEVEL) 
				|| MortalStrike.IsWounded(target);
		}

	/// <summary>
	/// Calculates heal amount using centralized calculator
	/// </summary>
	private int CalculateHealAmount(Mobile target)
	{
		bool isConsecutiveCast = IsConsecutiveCast();
		return SpellHealingCalculator.CalculateHeal(Caster, target, isConsecutiveCast);
	}

	/// <summary>
	/// Plays visual and sound effects for the spell
	/// </summary>
	private void PlayEffects(Mobile target)
	{
		int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
		target.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION, hue, 0, EffectLayer.Waist);
		target.PlaySound(SOUND_ID);
	}

	/// <summary>
	/// Checks if this is a consecutive cast within the penalty window
	/// </summary>
	private bool IsConsecutiveCast()
	{
		if (!LastCastTimes.ContainsKey(Caster))
			return false;

		DateTime lastCast = LastCastTimes[Caster];
		TimeSpan timeSinceLastCast = DateTime.UtcNow - lastCast;
		
		return timeSinceLastCast.TotalSeconds < CONSECUTIVE_CAST_WINDOW;
	}


	/// <summary>
	/// Updates the last cast time for the caster
	/// </summary>
	private void UpdateLastCastTime()
	{
		if (LastCastTimes.ContainsKey(Caster))
			LastCastTimes[Caster] = DateTime.UtcNow;
		else
			LastCastTimes.Add(Caster, DateTime.UtcNow);
	}

	/// <summary>
	/// Displays the amount of hit points healed
	/// </summary>
	private void ShowHealAmount(Mobile target, int healAmount, bool wasConsecutiveCast)
	{
		// Show overhead message on target
		string healText = string.Format(SpellMessages.INFO_HEAL_AMOUNT_FORMAT, healAmount);
		
		// Add indicator for consecutive cast penalty
		if (wasConsecutiveCast)
		{
			healText += ""; // Warning indicator
		}
		
		target.PublicOverheadMessage(MessageType.Regular, MSG_COLOR_HEAL, false, healText);

		// Also send system message to caster if healing someone else
		if (Caster != target)
		{
			string message = string.Format("Você curou {0} com {1} pontos de vida.", target.Name, healAmount);
			if (wasConsecutiveCast)
			{
				message += " [Cura enfraquecida por uso consecutivo]";
			}
			Caster.SendMessage(MSG_COLOR_HEAL, message);
		}
		else if (wasConsecutiveCast)
		{
			// Self-heal with penalty warning
			Caster.SendMessage(MSG_COLOR_WARNING, "Sua cura foi enfraquecida por uso consecutivo!");
		}
	}

		public class InternalTarget : Target
		{
			private HealSpell m_Owner;

			public InternalTarget(HealSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Beneficial)
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
	}
}