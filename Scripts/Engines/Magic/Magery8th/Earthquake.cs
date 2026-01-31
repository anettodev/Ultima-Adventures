using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Earthquake - 8th Circle Magery Spell
	/// Area-of-effect damage spell that affects all mobiles within range based on Magery skill.
	/// </summary>
	public class EarthquakeSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Earthquake", "In Vas Por",
				233,
				9012,
				false,
				Reagent.Bloodmoss,
				Reagent.Ginseng,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		#region Constants

		/// <summary>Base damage bonus for earthquake (20% more than ChainLightning's 28)</summary>
		/// ChainLightning: 28 + 2d5 = 30-38 (avg 34)
		/// Earthquake: 20% more = 34 * 1.2 = 40.8, so base = 37 for 1d6 (avg 3.5)
		/// </summary>
		private const int BASE_DAMAGE_BONUS = 37;

		/// <summary>Number of dice for damage calculation</summary>
		private const int DAMAGE_DICE_COUNT = 1;

		/// <summary>Sides per die for damage calculation</summary>
		private const int DAMAGE_DICE_SIDES = 6;

		/// <summary>Minimum damage floor (same as ChainLightning)</summary>
		private const int MIN_DAMAGE_FLOOR = 12;

		/// <summary>Minimum range for earthquake (tiles)</summary>
		private const int MIN_RANGE = 2;

		/// <summary>Maximum range for earthquake (tiles)</summary>
		private const int MAX_RANGE = 8;

		/// <summary>Minimum Magery skill for range calculation</summary>
		private const double MIN_MAGERY_FOR_RANGE = 50.0;

		/// <summary>Maximum Magery skill for range calculation</summary>
		private const double MAX_MAGERY_FOR_RANGE = 120.0;

		/// <summary>Damage multiplier when target resists</summary>
		private const double RESIST_DAMAGE_MULTIPLIER = 0.5;

		/// <summary>One Ring damage multiplier (same as ChainLightning)</summary>
		private const double ONE_RING_DAMAGE_MULTIPLIER = 0.5;

		/// <summary>Maximum EvalInt benefit percentage (23%)</summary>
		private const double MAX_EVALINT_BENEFIT = 0.23;

		/// <summary>Effect ID for earthquake ground particles</summary>
		private const int EARTHQUAKE_EFFECT_ID = 0x3728;

		/// <summary>Effect ID for earthquake tremor</summary>
		private const int EARTHQUAKE_TREMOR_ID = 0x37C4;

		#endregion

		public EarthquakeSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override bool DelayedDamage { get { return !Core.AOS; } }

		public override void OnCast()
		{
			if (SpellHelper.CheckTown(Caster, Caster) && CheckSequence())
			{
				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;
				bool playerVsPlayer = false;

				if (map != null)
				{
					// Calculate range: linear progression from 2 to 8 tiles based on Magery (50-120)
					int range = CalculateEarthquakeRange();

					// Play earthquake sound
					Caster.PlaySound(0x220);

					// Visual effect at caster location (ground tremor)
					EffectItem casterEffect = EffectItem.Create(Caster.Location, Caster.Map, EffectItem.DefaultDuration);
					Effects.SendLocationParticles(casterEffect, EARTHQUAKE_EFFECT_ID, 10, 20, 5042);

					foreach (Mobile m in Caster.GetMobilesInRange(range))
					{
						if (Caster.Region == m.Region && 
						    Caster != m && 
						    SpellHelper.ValidIndirectTarget(Caster, m) && 
						    Caster.CanBeHarmful(m, false) && 
						    (!Core.AOS || Caster.InLOS(m)))
						{
							targets.Add(m);
							if (m.Player)
								playerVsPlayer = true;
						}
					}
				}

				// Calculate damage once (like ChainLightning)
				double damage = CalculateEarthquakeDamage(playerVsPlayer);

				if (targets.Count > 0)
				{
					// Split damage among all targets (like ChainLightning)
					if (targets.Count > 1)
						damage /= targets.Count;

					// Apply minimum damage floor
					if (damage < MIN_DAMAGE_FLOOR)
						damage = MIN_DAMAGE_FLOOR;

					foreach (Mobile m in targets)
					{
						double toDeal = damage;

						// Check resistance
						if (CheckResisted(m))
						{
							toDeal *= RESIST_DAMAGE_MULTIPLIER;
							m.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESIST_HALF_DAMAGE_VICTIM);
						}

						// Check One Ring protection (prevents reveal and reduces damage)
						bool hasOneRing = false;
						if (m is PlayerMobile && m.FindItemOnLayer(Layer.Ring) != null && m.FindItemOnLayer(Layer.Ring) is OneRing)
						{
							hasOneRing = true;
							m.SendMessage(Spell.MSG_COLOR_WARNING, Spell.SpellMessages.ONE_RING_PROTECTION_REVEAL);
							toDeal *= ONE_RING_DAMAGE_MULTIPLIER;
						}

						// Visual effects on target (ground tremor and particles)
						EffectItem targetEffect = EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration);
						Effects.SendLocationParticles(targetEffect, EARTHQUAKE_EFFECT_ID, 10, 15, 5042);
						m.FixedEffect(EARTHQUAKE_TREMOR_ID, 10, 20);
						m.FixedParticles(EARTHQUAKE_EFFECT_ID, 10, 15, 5042, 0, 0, EffectLayer.Head);

						// Always reveal hidden targets (except One Ring protection)
						if (!hasOneRing)
						{
							m.RevealingAction();
						}

						Caster.DoHarmful(m);
						SpellHelper.Damage(TimeSpan.Zero, m, Caster, toDeal, 100, 0, 0, 0, 0);
					}
				}
			}

			FinishSequence();
		}

		#region Helper Methods

		/// <summary>
		/// Calculates earthquake range: linear progression from 2 to 8 tiles based on Magery (50-120)
		/// </summary>
		private int CalculateEarthquakeRange()
		{
			double magery = Caster.Skills[SkillName.Magery].Value;
			
			// Clamp Magery between min and max
			if (magery < MIN_MAGERY_FOR_RANGE)
				magery = MIN_MAGERY_FOR_RANGE;
			else if (magery > MAX_MAGERY_FOR_RANGE)
				magery = MAX_MAGERY_FOR_RANGE;
			
			// Linear interpolation: 2 tiles at Magery 50, 8 tiles at Magery 120
			double range = MIN_RANGE + ((magery - MIN_MAGERY_FOR_RANGE) / (MAX_MAGERY_FOR_RANGE - MIN_MAGERY_FOR_RANGE)) * (MAX_RANGE - MIN_RANGE);
			
			return (int)Math.Round(range);
		}

		/// <summary>
		/// Calculates earthquake damage (like ChainLightning - shared among all targets)
		/// Uses custom EvalInt benefit formula (0% to 23% instead of default)
		/// Damage is 30% more than ChainLightning (8th circle vs 7th circle)
		/// </summary>
		private double CalculateEarthquakeDamage(bool playerVsPlayer)
		{
			int nBenefit = 0;
			// Future benefit calculation can be added here if needed
			// if (Caster is PlayerMobile)
			//     nBenefit = (int)(Caster.Skills[SkillName.Magery].Value / 5);

			// Use GetNMSDamage for base calculation (like ChainLightning)
			double damage = GetNMSDamage(BASE_DAMAGE_BONUS, DAMAGE_DICE_COUNT, DAMAGE_DICE_SIDES, Caster.Player && playerVsPlayer) + nBenefit;
			
			// Custom EvalInt benefit: linear progression from 0% to 23%
			double evalInt = Caster.Skills[SkillName.EvalInt].Value;
			double evalIntBenefit = 1.0 + (evalInt / 100.0) * MAX_EVALINT_BENEFIT;
			
			// Apply EvalInt benefit
			damage = Math.Floor(damage * evalIntBenefit);
			
			return damage;
		}

		#endregion
	}
}
