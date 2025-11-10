using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.SkillHandlers;
using Server.Misc;
using Server.Spells.Bushido;
using Server.Spells.Necromancy;

namespace Server.Spells
{
	/// <summary>
	/// Handles all spell damage calculations for both NMS and AOS systems
	/// </summary>
	public static class SpellDamageCalculator
	{
		#region NMS Damage Calculation

		/// <summary>
		/// Calculates damage using the NMS (New Magic System) formula
		/// </summary>
		public static int GetNMSDamage(Spell spell, int bonus, int dice, int sides, Mobile singleTarget)
		{
			bool playerVsPlayer = (spell.Caster.Player && singleTarget != null && singleTarget.Player);
			return GetNMSDamage(spell, bonus, dice, sides, playerVsPlayer);
		}

		/// <summary>
		/// Calculates damage using the NMS (New Magic System) formula
		/// </summary>
		public static int GetNMSDamage(Spell spell, int bonus, int dice, int sides, bool playerVsPlayer)
		{
			int realDamage = Utility.Dice(dice, sides, bonus);
			double evalBenefit = NMSUtils.getDamageEvalBenefit(spell.Caster);
			int finalDamage = (int)Math.Floor(realDamage * evalBenefit);

			#if DEBUG
			SendDebugDamageInfo(spell, realDamage, evalBenefit, finalDamage);
			#endif

			return finalDamage;
		}

		#if DEBUG
		/// <summary>
		/// Sends debug damage calculation information to caster (DEBUG only)
		/// </summary>
		private static void SendDebugDamageInfo(Spell spell, int realDamage, double evalBenefit, int finalDamage)
		{
			spell.Caster.SendMessage(SpellConstants.MSG_COLOR_DEBUG_1, string.Format(Spell.SpellMessages.DEBUG_REAL_DAMAGE_FORMAT, realDamage));
			spell.Caster.SendMessage(SpellConstants.MSG_COLOR_DEBUG_2, string.Format(Spell.SpellMessages.DEBUG_EVAL_BENEFIT_FORMAT, evalBenefit));
			spell.Caster.SendMessage(SpellConstants.MSG_COLOR_DEBUG_3, string.Format(Spell.SpellMessages.DEBUG_FINAL_DAMAGE_FORMAT, finalDamage));
		}
		#endif

		#endregion

		#region AOS Damage Calculation

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula with target
		/// </summary>
		public static int GetNewAosDamage(Spell spell, int bonus, int dice, int sides, Mobile singleTarget)
		{
			if (singleTarget != null)
			{
				bool playerVsPlayer = (spell.Caster.Player && singleTarget.Player);
				double scalar = GetDamageScalar(spell, singleTarget);
				return GetNewAosDamage(spell, bonus, dice, sides, playerVsPlayer, scalar);
			}
			else
			{
				return GetNewAosDamage(spell, bonus, dice, sides, false, 1.0);
			}
		}

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula
		/// </summary>
		public static int GetNewAosDamage(Spell spell, int bonus, int dice, int sides, bool playerVsPlayer)
		{
			return GetNewAosDamage(spell, bonus, dice, sides, playerVsPlayer, 1.0);
		}

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula with all modifiers
		/// </summary>
		public static int GetNewAosDamage(Spell spell, int bonus, int dice, int sides, bool playerVsPlayer, double scalar)
		{
			int damage = Utility.Dice(dice, sides, bonus) * SpellConstants.BASE_DAMAGE_MULTIPLIER;
			int damageBonus = CalculateTotalDamageBonus(spell, playerVsPlayer);

			damage = AOS.Scale(damage, SpellConstants.BASE_DAMAGE_MULTIPLIER + damageBonus);
			damage = ApplyEvalIntScaling(spell, damage);
			damage = AOS.Scale(damage, (int)(scalar * SpellConstants.BASE_DAMAGE_MULTIPLIER));

			return damage / SpellConstants.BASE_DAMAGE_MULTIPLIER;
		}

		/// <summary>
		/// Calculates total damage bonus from Inscribe, Int, and SDI
		/// </summary>
		private static int CalculateTotalDamageBonus(Spell spell, bool playerVsPlayer)
		{
			int inscribeBonus = CalculateInscribeBonus(spell);
			int intBonus = CalculateIntBonus(spell);
			int sdiBonus = CalculateSDIBonus(spell, playerVsPlayer);

			int totalBonus = inscribeBonus + intBonus + sdiBonus;

			// Apply Midland modifications if applicable
			if (MidlandSpellModifier.IsInMidland(spell.Caster))
			{
				totalBonus = MidlandSpellModifier.ApplyDamageModifications(spell, totalBonus);
			}

			return totalBonus;
		}

		/// <summary>
		/// Calculates Inscription skill bonus for damage
		/// </summary>
		private static int CalculateInscribeBonus(Spell spell)
		{
			int inscribeSkill = spell.GetInscribeFixed(spell.Caster);
			return (inscribeSkill + (SpellConstants.INSCRIBE_MULTIPLIER * (inscribeSkill / SpellConstants.INSCRIBE_MULTIPLIER))) / SpellConstants.INSCRIBE_DAMAGE_DIVISOR;
		}

		/// <summary>
		/// Calculates Intelligence bonus for damage
		/// </summary>
		private static int CalculateIntBonus(Spell spell)
		{
			return spell.Caster.Int / SpellConstants.INT_BONUS_DIVISOR;
		}

		/// <summary>
		/// Calculates Spell Damage Increase bonus with caps
		/// </summary>
		private static int CalculateSDIBonus(Spell spell, bool playerVsPlayer)
		{
			int sdiBonus = AosAttributes.GetValue(spell.Caster, AosAttribute.SpellDamage);
			int sdiCap = MyServerSettings.RealSpellDamageCap();

			if (MidlandSpellModifier.IsInMidland(spell.Caster))
			{
				return 0; // SDI disabled in Midland
			}

			if (sdiBonus > sdiCap)
			{
				sdiBonus = sdiCap;
			}

			// PvP specific cap
			if (playerVsPlayer && Server.Misc.MyServerSettings.SpellDamageIncreaseVsPlayers() > 0)
			{
				int pvpCap = Server.Misc.MyServerSettings.SpellDamageIncreaseVsPlayers();
				if (sdiBonus > pvpCap)
				{
					sdiBonus = pvpCap;
				}
			}

			return sdiBonus;
		}

		/// <summary>
		/// Applies EvalInt scaling to damage
		/// </summary>
		private static int ApplyEvalIntScaling(Spell spell, int damage)
		{
			int evalSkill = spell.GetDamageFixed(spell.Caster);
			int evalScale = SpellConstants.EVAL_SCALE_BASE + ((SpellConstants.EVAL_SCALE_MULTIPLIER * evalSkill) / SpellConstants.EVAL_SCALE_DIVISOR);
			return AOS.Scale(damage, evalScale);
		}

		#endregion

		#region Damage Scalar Calculation

		/// <summary>
		/// Gets the damage scalar for a target considering various modifiers
		/// </summary>
		public static double GetDamageScalar(Spell spell, Mobile target)
		{
			double scalar = 1.0;

			if (!Core.AOS) // Pre-AOS EvalInt mechanics
			{
				double casterEI = spell.GetDamageSkill(spell.Caster);
				double targetRS = spell.GetResistSkill(target);

				if (casterEI > targetRS)
				{
					scalar = (1.0 + ((casterEI - targetRS) / 500.0));
				}
				else
				{
					scalar = (1.0 + ((casterEI - targetRS) / 200.0));
				}

				// Magery damage bonus: -25% at 0 skill, +0% at 100 skill, +5% at 120 skill
				scalar += (spell.Caster.Skills[spell.CastSkill].Value - 100.0) / 400.0;

				if (!target.Player && !target.Body.IsHuman)
				{
					scalar *= 2.0; // Double magery damage to monsters/animals if not AOS
				}
			}

			// Creature alterations
			if (target is BaseCreature)
			{
				BaseCreature creature = (BaseCreature)target;
				creature.AlterDamageScalarFrom(spell.Caster, ref scalar);
			}

			if (spell.Caster is BaseCreature)
			{
				BaseCreature casterCreature = (BaseCreature)spell.Caster;
				casterCreature.AlterDamageScalarTo(target, ref scalar);
			}

			// Slayer damage
			if (Core.SE)
			{
				scalar *= GetSlayerDamageScalar(spell, target);
			}

			// Region modifications
			target.Region.SpellDamageScalar(spell.Caster, target, ref scalar);

			// Evasion check
			if (Evasion.CheckSpellEvasion(target))
			{
				scalar = 0;
			}

			return scalar;
		}

		/// <summary>
		/// Gets slayer damage scalar from equipped spellbook
		/// </summary>
		public static double GetSlayerDamageScalar(Spell spell, Mobile defender)
		{
			Spellbook atkBook = Spellbook.FindEquippedSpellbook(spell.Caster);
			double scalar = 1.0;

			if (atkBook != null)
			{
				SlayerEntry atkSlayer = SlayerGroup.GetEntryByName(atkBook.Slayer);
				SlayerEntry atkSlayer2 = SlayerGroup.GetEntryByName(atkBook.Slayer2);

				if ((atkSlayer != null && atkSlayer.Slays(defender)) || (atkSlayer2 != null && atkSlayer2.Slays(defender)))
				{
					defender.FixedEffect(0x37B9, 10, 5);
					scalar = 2.0;
				}

				TransformContext context = TransformationSpellHelper.GetContext(defender);

				if ((atkBook.Slayer == SlayerName.Silver || atkBook.Slayer2 == SlayerName.Silver) && context != null && context.Type != typeof(HorrificBeastSpell))
				{
					scalar += .25; // Every necromancer transformation other than horrific beast takes an additional 25% damage
				}

				if (scalar != 1.0)
				{
					return scalar;
				}
			}

			// Check defender's slayer
			ISlayer defISlayer = Spellbook.FindEquippedSpellbook(defender);

			if (defISlayer == null)
			{
				defISlayer = defender.Weapon as ISlayer;
			}

			if (defISlayer != null)
			{
				SlayerEntry defSlayer = SlayerGroup.GetEntryByName(defISlayer.Slayer);
				SlayerEntry defSlayer2 = SlayerGroup.GetEntryByName(defISlayer.Slayer2);

				if ((defSlayer != null && defSlayer.Group.OppositionSuperSlays(spell.Caster)) || (defSlayer2 != null && defSlayer2.Group.OppositionSuperSlays(spell.Caster)))
				{
					scalar = 2.0;
				}
			}

			return scalar;
		}

		#endregion

		#region Circle-Based Damage Scaling

		/// <summary>
		/// Gets base damage parameters for a spell circle
		/// Ensures consistent damage progression across circles
		/// </summary>
		public static void GetCircleDamageParams(SpellCircle circle, out int bonus, out int dice, out int sides)
		{
			switch (circle)
			{
				case SpellCircle.First:
					bonus = 2; dice = 1; sides = 3; // 1d3+2 = 3-5
					break;
				case SpellCircle.Second:
					bonus = 4; dice = 1; sides = 4; // 1d4+4 = 5-8
					break;
				case SpellCircle.Third:
					bonus = 4; dice = 1; sides = 6; // 1d6+4 = 5-10
					break;
				case SpellCircle.Fourth:
					bonus = 6; dice = 1; sides = 6; // 1d6+6 = 7-12 (IMPROVED)
					break;
				case SpellCircle.Fifth:
					bonus = 6; dice = 1; sides = 8; // 1d8+6 = 7-14
					break;
				case SpellCircle.Sixth:
					bonus = 8; dice = 1; sides = 8; // 1d8+8 = 9-16
					break;
				case SpellCircle.Seventh:
					bonus = 8; dice = 1; sides = 10; // 1d10+8 = 9-18
					break;
				case SpellCircle.Eighth:
					bonus = 10; dice = 1; sides = 10; // 1d10+10 = 11-20
					break;
				default:
					bonus = 4; dice = 1; sides = 6; // Default
					break;
			}
		}

		/// <summary>
		/// Gets minimum damage floor based on EvalInt skill
		/// Should be applied to all damage spells for consistency
		/// </summary>
		public static int GetMinimumDamageFloor(Mobile caster, int baseMinDamage)
		{
			double evalInt = caster.Skills[SkillName.EvalInt].Value;
			
			if (evalInt >= 120.0)
				return baseMinDamage + 4; // +4 at 120+
			else if (evalInt >= 100.0)
				return baseMinDamage + 3; // +3 at 100+
			else if (evalInt >= 80.0)
				return baseMinDamage + 2; // +2 at 80+
			else
				return baseMinDamage; // Base minimum
		}

		#endregion
	}
}

