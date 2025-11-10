using System;
using Server.Mobiles;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;

namespace Server.Spells
{
	/// <summary>
	/// Handles validation logic for spell casting
	/// </summary>
	public static class SpellCastingValidator
	{
		/// <summary>
		/// Validates various conditions that prevent casting
		/// </summary>
		public static bool ValidateCanCast(Spell spell)
		{
			if (spell.Caster.Blessed)
			{
				spell.Caster.SendMessage("You cannot do that while in this state.");
				return false;
			}

			if (!spell.Caster.CheckAlive())
			{
				return false;
			}

			if (spell.Caster.Spell != null && spell.Caster.Spell.IsCasting)
			{
				spell.Caster.SendLocalizedMessage(502642); // You are already casting a spell.
				return false;
			}

			if (IsBlockedByTransformation(spell))
			{
				spell.Caster.SendLocalizedMessage(1061091); // You cannot cast that spell in this form.
				return false;
			}

			if (spell.Caster.Paralyzed || spell.Caster.Frozen)
			{
				spell.Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
				return false;
			}

			if (IsOnCooldown(spell))
			{
				spell.Caster.SendLocalizedMessage(502644); // You have not yet recovered from casting a spell.
				return false;
			}

			if (IsCalmed(spell))
			{
				spell.Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
				return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if spell is blocked by transformation
		/// </summary>
		private static bool IsBlockedByTransformation(Spell spell)
		{
			if (spell.BlockedByHorrificBeast && TransformationSpellHelper.UnderTransformation(spell.Caster, typeof(HorrificBeastSpell)))
			{
				return true;
			}

			if (spell.BlockedByAnimalForm && AnimalForm.UnderTransformation(spell.Caster))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Checks if spell is on cooldown
		/// </summary>
		private static bool IsOnCooldown(Spell spell)
		{
			if (!spell.CheckNextSpellTime)
			{
				return false;
			}

			return Core.TickCount - spell.Caster.NextSpellTime < 0;
		}

		/// <summary>
		/// Checks if caster is calmed
		/// </summary>
		private static bool IsCalmed(Spell spell)
		{
			if (!(spell.Caster is PlayerMobile))
			{
				return false;
			}

			PlayerMobile playerMobile = (PlayerMobile)spell.Caster;
			return playerMobile.PeacedUntil > DateTime.UtcNow;
		}

		/// <summary>
		/// Validates basic cast conditions (blessed state and step configuration)
		/// </summary>
		public static bool CheckCast(Spell spell, Mobile caster)
		{
			if (caster.Blessed)
			{
				caster.SendMessage(SpellConstants.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_CAST_IN_STATE);
				return false;
			}

			PlayerMobile playerMobile = caster as PlayerMobile;
			if (playerMobile != null)
			{
				SpellMovementHandler.ConfigureAllowedSteps(spell, playerMobile);
			}

			return true;
		}
	}
}

