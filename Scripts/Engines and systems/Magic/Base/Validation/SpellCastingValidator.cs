using System;
using Server.Items;
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

			if (BandageHelpers.IsCurrentlyBandaging(spell.Caster))
			{
				spell.Caster.SendMessage(Server.Items.BandageStringConstants.MSG_ALREADY_HEALING);
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


		// Check if heavy armor blocks Magery spells
		if (spell is MagerySpell && IsWearingHeavyArmorWithoutMageArmor(caster))
		{
			caster.SendMessage(SpellConstants.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ARMOR_BLOCKS_MAGERY);
			return false;
		}

			return true;
		}

		/// <summary>
		/// Checks if the mobile is wearing heavy armor (NONE meditation allowance)
		/// without the Mage Armor or Spell Channeling property
		/// </summary>
		public static bool IsWearingHeavyArmorWithoutMageArmor(Mobile from)
		{
			if (from == null)
				return false;

			// Check all armor slots
			BaseArmor[] armorPieces = new BaseArmor[]
			{
				from.NeckArmor as BaseArmor,
				from.HandArmor as BaseArmor,
				from.HeadArmor as BaseArmor,
				from.ArmsArmor as BaseArmor,
				from.LegsArmor as BaseArmor,
				from.ChestArmor as BaseArmor,
				from.ShieldArmor as BaseArmor,
				from.FindItemOnLayer(Layer.Shoes) as BaseArmor,
				from.FindItemOnLayer(Layer.Cloak) as BaseArmor,
				from.FindItemOnLayer(Layer.OuterTorso) as BaseArmor
			};

			foreach (BaseArmor armor in armorPieces)
			{
				if (armor == null)
					continue;

				// If armor has Mage Armor or Spell Channeling, it doesn't block
				if (armor.ArmorAttributes.MageArmor != 0 || armor.Attributes.SpellChanneling != 0)
					continue;

				// If armor has NONE meditation allowance, it blocks casting
				if (armor.MeditationAllowance == ArmorMeditationAllowance.None)
					return true;
			}

			return false;
		}
	}
}

