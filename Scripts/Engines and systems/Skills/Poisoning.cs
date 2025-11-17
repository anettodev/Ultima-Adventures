using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.SkillHandlers
{
	public class Poisoning
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Poisoning].Callback = new SkillUseCallback( OnUse );
		}

		/// <summary>
		/// Main entry point for Poisoning skill.
		/// </summary>
		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new InternalTargetPoison();
			m.SendLocalizedMessage( 502137 ); // Select the poison you wish to use
			return TimeSpan.FromSeconds( PoisoningConstants.SKILL_REUSE_DELAY_SECONDS );
		}

		/// <summary>
		/// Validates if a target can be poisoned and returns true if timer should start.
		/// </summary>
		private static bool ValidatePoisonTarget( Mobile from, object targeted )
		{
			Item targetItem = targeted as Item;
			if ( targetItem == null )
				return false;

			// Ninja weapons can always be poisoned
			if ( PoisoningHelpers.IsPoisonableNinjaWeapon( targetItem ) )
				return true;

			// Food and drinks can always be poisoned
			if ( PoisoningHelpers.IsPoisonableFood( targetItem ) || 
				 PoisoningHelpers.IsPoisonableDrink( targetItem ) )
				return true;

			// Weapons require special validation
			BaseWeapon weapon = targetItem as BaseWeapon;
			if ( weapon != null )
			{
				// BaseBashing weapons (mace fighting) cannot be poisoned in any mode
				if ( weapon is BaseBashing )
					return false;
				
				// Pickaxe and all pickaxe variants cannot be poisoned in any mode
				if ( weapon is Pickaxe || 
					 weapon is SturdyPickaxe || 
					 weapon is GargoylesPickaxe || 
					 weapon is RubyPickaxe || 
					 weapon is LevelPickaxe || 
					 weapon is GiftPickaxe )
					return false;
				
				// Check global classic poisoning mode setting
				// Global setting takes precedence over per-player setting
				bool isClassicMode = ( Server.Misc.MyServerSettings.ClassicPoisoningMode() == 1 );
				
				if ( !isClassicMode )
				{
					// Modern mode: Requires InfectiousStrike ability
					return PoisoningHelpers.HasInfectiousStrikeAbility( weapon );
				}
				else
				{
					// Classic mode: Metal or ranged weapons
					if ( PoisoningHelpers.CanPoisonInClassicMode( weapon ) )
						return true;
					else
					{
						from.SendMessage( PoisoningConstants.MSG_COLOR_ERROR, 
							PoisoningMessages.ERROR_ONLY_METAL_OR_RANGED );
						return false;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Handles failure case for poison application.
		/// </summary>
		private static void HandlePoisonFailure( Mobile from, Item target, Poison poison, bool isClassicMode )
		{
			// Modern mode: 5% chance of poisoning self if skill < 80
			if ( !isClassicMode && 
				 from.Skills[SkillName.Poisoning].Base < PoisoningConstants.MODERN_FAILURE_SKILL_THRESHOLD && 
				 Utility.Random( PoisoningConstants.MODERN_FAILURE_POISON_CHANCE ) == 0 )
			{
				from.SendLocalizedMessage( 502148 ); // You make a grave mistake while applying the poison.
				from.PlaySound( PoisoningConstants.SOUND_POISON_FAILURE );
				from.ApplyPoison( from, poison );
			}
			else
			{
				// Send appropriate failure message
				BaseWeapon weapon = target as BaseWeapon;
				PoisoningHelpers.SendFailureMessage( from, weapon );
			}
		}

		/// <summary>
		/// Applies poison in modern mode (non-classic).
		/// </summary>
		private static void ApplyPoisonModernMode( Mobile from, Item target, Poison poison )
		{
			if ( PoisoningHelpers.IsPoisonableFood( target ) )
			{
				PoisonApplicationHandlers.ApplyToFood( from, target, poison );
			}
			else if ( PoisoningHelpers.IsPoisonableDrink( target ) )
			{
				PoisonApplicationHandlers.ApplyToDrink( from, target, poison );
			}
			else if ( target is BaseWeapon )
			{
				PoisonApplicationHandlers.ApplyToWeaponModernMode( from, (BaseWeapon)target, poison );
			}
			else if ( target is FukiyaDarts )
			{
				PoisonApplicationHandlers.ApplyToFukiyaDarts( from, (FukiyaDarts)target, poison );
			}
			else if ( target is Shuriken )
			{
				PoisonApplicationHandlers.ApplyToShuriken( from, (Shuriken)target, poison );
			}

			from.SendLocalizedMessage( 1010517 ); // You apply the poison
			
			// Play success sound for weapons only
			if ( target is BaseWeapon || target is FukiyaDarts || target is Shuriken )
			{
				from.PlaySound( PoisoningConstants.SOUND_POISON_SUCCESS );
			}
			
			Misc.Titles.AwardKarma( from, PoisoningConstants.KARMA_PENALTY, true );
		}

		#region Target Classes

		private class InternalTargetPoison : Target
		{
			public InternalTargetPoison() : base( 2, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BasePoisonPotion )
				{
					from.SendLocalizedMessage( 502142 ); // To what do you wish to apply the poison?
					from.Target = new InternalTarget( (BasePoisonPotion)targeted );
				}
				else
				{
					from.SendLocalizedMessage( 502139 ); // That is not a poison potion.
				}
			}

			private class InternalTarget : Target
			{
				private BasePoisonPotion m_Potion;

				public InternalTarget( BasePoisonPotion potion ) : base( 2, false, TargetFlags.None )
				{
					m_Potion = potion;
				}

				protected override void OnTarget( Mobile from, object targeted )
				{
					if ( m_Potion.Deleted )
						return;

					bool startTimer = ValidatePoisonTarget( from, targeted );

					if ( startTimer )
					{
						new InternalTimer( from, (Item)targeted, m_Potion ).Start();
						from.PlaySound( PoisoningConstants.SOUND_POISON_APPLY );
						m_Potion.Consume();
						from.AddToBackpack( new Bottle() );
					}
					else if ( targeted is WaterFlask || targeted is WaterVial )
					{
						from.PrivateOverheadMessage( MessageType.Regular, PoisoningConstants.MSG_COLOR_OVERHEAD, false, 
							PoisoningMessages.ERROR_NOT_ENOUGH_WATER, from.NetState );
					}
					else
					{
						// Target can't be poisoned
						if ( targeted is BaseWeapon )
						{
							from.SendMessage( PoisoningConstants.MSG_COLOR_ERROR, 
								PoisoningMessages.ERROR_CANNOT_POISON_WEAPON );
						}
						else
						{
							from.SendMessage( PoisoningConstants.MSG_COLOR_ERROR, 
								PoisoningMessages.ERROR_CANNOT_POISON_GENERIC );
						}
					}
				}

				private class InternalTimer : Timer
				{
					private Mobile m_From;
					private Item m_Target;
					private Poison m_Poison;
					private double m_MinSkill, m_MaxSkill;

					public InternalTimer( Mobile from, Item target, BasePoisonPotion potion ) 
						: base( TimeSpan.FromSeconds( PoisoningConstants.POISON_APPLY_DELAY_SECONDS ) )
					{
						m_From = from;
						m_Target = target;
						m_Poison = potion.Poison;
						m_MinSkill = potion.MinPoisoningSkill;
						m_MaxSkill = potion.MaxPoisoningSkill;
						Priority = TimerPriority.TwoFiftyMS;
					}

					protected override void OnTick()
					{
						// Use global classic poisoning mode setting
						bool isClassicMode = ( Server.Misc.MyServerSettings.ClassicPoisoningMode() == 1 );

						// Classic mode weapon handling
						if ( m_Target is BaseWeapon && isClassicMode )
						{
							BaseWeapon weapon = (BaseWeapon)m_Target;
							PoisonApplicationHandlers.ApplyToWeaponClassicMode( m_From, weapon, m_Poison );
							return;
						}

						// Modern mode or non-weapon targets
						if ( m_From.CheckTargetSkill( SkillName.Poisoning, m_Target, m_MinSkill, m_MaxSkill ) )
						{
							ApplyPoisonModernMode( m_From, m_Target, m_Poison );
						}
						else
						{
							HandlePoisonFailure( m_From, m_Target, m_Poison, isClassicMode );
						}
					}
				}
			}
		}

		#endregion
	}
}
