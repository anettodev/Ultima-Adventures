using System;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells
{
	/// <summary>
	/// Handles Midland-specific spell modifications based on Lucidity
	/// </summary>
	public static class MidlandSpellModifier
	{
		/// <summary>
		/// Checks if caster is in Midland region
		/// </summary>
		public static bool IsInMidland(Mobile caster)
		{
			return AdventuresFunctions.IsInMidland((object)caster);
		}

		/// <summary>
		/// Gets Fast Cast Recovery bonus for Midland based on Lucidity
		/// </summary>
		public static int GetFastCastRecoveryBonus(Mobile caster)
		{
			if (!IsInMidland(caster) || !(caster is PlayerMobile))
			{
				return 0;
			}

			PlayerMobile playerMobile = (PlayerMobile)caster;
			double lucidity = playerMobile.Lucidity();
			int bonus = 0;

			if (lucidity > SpellConstants.MIDLAND_LUCIDITY_THRESHOLD_LOW)
			{
				bonus++;
			}
			if (lucidity > SpellConstants.MIDLAND_LUCIDITY_THRESHOLD_MED)
			{
				bonus++;
			}
			if (lucidity > SpellConstants.MIDLAND_LUCIDITY_THRESHOLD_HIGH)
			{
				bonus++;
			}

			return bonus;
		}

		/// <summary>
		/// Gets Fast Cast bonus for Midland based on Lucidity
		/// </summary>
		public static int GetFastCastBonus(Mobile caster)
		{
			if (!IsInMidland(caster) || !(caster is PlayerMobile))
			{
				return 0;
			}

			PlayerMobile playerMobile = (PlayerMobile)caster;
			double lucidity = playerMobile.Lucidity();
			int bonus = 0;

			if (lucidity > SpellConstants.MIDLAND_LUCIDITY_THRESHOLD_MED)
			{
				bonus++;
			}
			if (lucidity > SpellConstants.MIDLAND_LUCIDITY_THRESHOLD_HIGH)
			{
				bonus++;
			}

			return bonus;
		}

		/// <summary>
		/// Applies Midland-specific damage modifications
		/// </summary>
		public static int ApplyDamageModifications(Spell spell, int currentBonus)
		{
			if (!(spell.Caster is PlayerMobile))
			{
				return currentBonus;
			}

			PlayerMobile playerMobile = (PlayerMobile)spell.Caster;

			// Apply Lucidity multiplier
			int modifiedBonus = (int)((double)currentBonus * (playerMobile.Lucidity() * SpellConstants.MIDLAND_LUCIDITY_DAMAGE_MULTIPLIER));

			// Add extra int emphasis in Midland
			modifiedBonus += spell.Caster.Int / SpellConstants.INT_BONUS_DIVISOR;

			return modifiedBonus;
		}
	}
}

