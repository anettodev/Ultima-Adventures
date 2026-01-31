using System;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Handlers for applying poison to different target types.
	/// Extracted to reduce OnTick complexity and improve maintainability.
	/// </summary>
	public static class PoisonApplicationHandlers
	{
		/// <summary>
		/// Applies poison to food item.
		/// </summary>
		public static void ApplyToFood( Mobile from, Item target, Poison poison )
		{
			int foodLevel = PoisoningHelpers.ConvertPoisonToFoodLevel( poison );
			
			PoisonFood taintFood = new PoisonFood();
			taintFood.Poisoner = from;
			taintFood.Poison = foodLevel;
			
			from.AddToBackpack( taintFood );
			PoisoningHelpers.ConsumeItem( target );
		}
		
		/// <summary>
		/// Applies poison to drink item.
		/// </summary>
		public static void ApplyToDrink( Mobile from, Item target, Poison poison )
		{
			int drinkLevel = PoisoningHelpers.ConvertPoisonToFoodLevel( poison );
			
			PoisonLiquid taintDrink = new PoisonLiquid();
			taintDrink.Poisoner = from;
			taintDrink.Poison = drinkLevel;
			
			from.AddToBackpack( taintDrink );
			PoisoningHelpers.ConsumeItem( target );
		}
		
		/// <summary>
		/// Applies poison to weapon in classic mode.
		/// </summary>
		public static bool ApplyToWeaponClassicMode( Mobile from, BaseWeapon weapon, Poison poison )
		{
			// Warn if weapon is already poisoned
			if ( PoisoningHelpers.IsWeaponAlreadyPoisoned( weapon ) )
			{
				from.SendMessage( PoisoningConstants.MSG_COLOR_WARNING, 
					PoisoningMessages.WARNING_OVERWRITE_POISON );
			}
			
			double currentSkill = from.Skills[SkillName.Poisoning].Value;
			double successChance = PoisoningCalculations.CalculateSuccessChance( currentSkill, poison.Level );
			
			if ( Utility.RandomDouble() < successChance )
			{
				// Success: Apply poison with skill-based charges
				int maxCharges = PoisoningCalculations.CalculateMaxCharges( currentSkill );
				weapon.Poison = poison;
				weapon.PoisonCharges = maxCharges;
				
				from.SendLocalizedMessage( 1010517 ); // You apply the poison
				from.PlaySound( PoisoningConstants.SOUND_POISON_SUCCESS );
				Misc.Titles.AwardKarma( from, PoisoningConstants.KARMA_PENALTY, true );
				return true;
			}
			else
			{
				// Failure: Poison the user
				from.SendLocalizedMessage( 502148 ); // You make a grave mistake while applying the poison.
				from.PlaySound( PoisoningConstants.SOUND_POISON_FAILURE );
				from.ApplyPoison( from, poison );
				PoisoningHelpers.SendFailureMessage( from, weapon );
				return false;
			}
		}
		
		/// <summary>
		/// Applies poison to weapon in modern mode.
		/// </summary>
		public static void ApplyToWeaponModernMode( Mobile from, BaseWeapon weapon, Poison poison )
		{
			// Warn if weapon is already poisoned
			if ( PoisoningHelpers.IsWeaponAlreadyPoisoned( weapon ) )
			{
				from.SendMessage( PoisoningConstants.MSG_COLOR_WARNING, 
					PoisoningMessages.WARNING_OVERWRITE_POISON );
			}
			
			weapon.Poison = poison;
			weapon.PoisonCharges = PoisoningHelpers.CalculateModernCharges( poison.Level );
		}
		
		/// <summary>
		/// Applies poison to FukiyaDarts.
		/// </summary>
		public static void ApplyToFukiyaDarts( Mobile from, FukiyaDarts darts, Poison poison )
		{
			// Warn if already poisoned
			if ( darts.Poison != null )
			{
				from.SendMessage( PoisoningConstants.MSG_COLOR_WARNING, 
					PoisoningMessages.WARNING_OVERWRITE_POISON );
			}
			
			darts.Poison = poison;
			int maxCharges = PoisoningHelpers.CalculateModernCharges( poison.Level );
			darts.PoisonCharges = Math.Min( maxCharges, darts.UsesRemaining );
		}
		
		/// <summary>
		/// Applies poison to Shuriken.
		/// </summary>
		public static void ApplyToShuriken( Mobile from, Shuriken shuriken, Poison poison )
		{
			// Warn if already poisoned
			if ( shuriken.Poison != null )
			{
				from.SendMessage( PoisoningConstants.MSG_COLOR_WARNING, 
					PoisoningMessages.WARNING_OVERWRITE_POISON );
			}
			
			shuriken.Poison = poison;
			int maxCharges = PoisoningHelpers.CalculateModernCharges( poison.Level );
			shuriken.PoisonCharges = Math.Min( maxCharges, shuriken.UsesRemaining );
		}
	}
}

