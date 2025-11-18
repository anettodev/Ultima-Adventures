using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	/// <summary>
	/// Base class for all poison potions (Lesser, Regular, Greater, Deadly, Lethal).
	/// Handles drinking, throwing, and self-poisoning mechanics.
	/// Using poison potions is a criminal action.
	/// </summary>
	public abstract class BasePoisonPotion : BasePotion
	{
		#region Abstract Properties

		/// <summary>Gets the poison type for this potion</summary>
		public abstract Poison Poison{ get; }

		/// <summary>Gets the minimum poisoning skill required</summary>
		public abstract double MinPoisoningSkill{ get; }

		/// <summary>Gets the maximum poisoning skill required</summary>
		public abstract double MaxPoisoningSkill{ get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BasePoisonPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BasePoisonPotion( PotionEffect effect ) : base( PoisonPotionConstants.ITEM_ID, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BasePoisonPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the poison potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( PoisonPotionConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the poison potion
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Applies poison to the specified mobile
		/// </summary>
		/// <param name="from">The mobile to poison</param>
		public void DoPoison( Mobile from )
		{
			from.ApplyPoison( from, Poison );
			
			int skillLevel = GetSkillLevel( PoisonPotionConstants.SKILL_LEVEL_DEFAULT_DO_POISON );
			from.CheckTargetSkill( SkillName.Poisoning, from, skillLevel - PoisonPotionConstants.SKILL_CHECK_RANGE, skillLevel + PoisonPotionConstants.SKILL_CHECK_RANGE );
		}

		/// <summary>
		/// Handles the drink action when player uses the potion
		/// </summary>
		/// <param name="from">The mobile drinking the potion</param>
		public override void Drink( Mobile from )
		{
			int skillLevel = GetSkillLevel( PoisonPotionConstants.SKILL_LEVEL_DEFAULT_DRINK );

			if ( from.Skills[SkillName.Poisoning].Value >= skillLevel )
			{
				HandleHighSkillThrow( from );
			}
			else
			{
				HandleLowSkillDrink( from );
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the skill level requirement for this poison potion type
		/// </summary>
		/// <param name="defaultLevel">Default skill level if type not recognized</param>
		/// <returns>Skill level requirement</returns>
		private int GetSkillLevel( int defaultLevel )
		{
			if ( this is PoisonPotion )
				return PoisonPotionConstants.SKILL_LEVEL_REGULAR;
			if ( this is GreaterPoisonPotion )
				return PoisonPotionConstants.SKILL_LEVEL_GREATER;
			if ( this is DeadlyPoisonPotion )
				return PoisonPotionConstants.SKILL_LEVEL_DEADLY;
			if ( this is LethalPoisonPotion )
				return PoisonPotionConstants.SKILL_LEVEL_LETHAL;
			
			return defaultLevel;
		}

		/// <summary>
		/// Attempts to gain poisoning skill based on potion type
		/// </summary>
		/// <param name="from">The mobile attempting skill gain</param>
		private void TrySkillGain( Mobile from )
		{
			if ( this is PoisonPotion )
			{
				if ( Utility.RandomDouble() < PoisonPotionConstants.SKILL_GAIN_CHANCE_REGULAR )
					from.CheckSkill( SkillName.Poisoning, PoisonPotionConstants.SKILL_CHECK_MIN_LESSER, PoisonPotionConstants.SKILL_CHECK_MAX_REGULAR );
			}
			else if ( this is GreaterPoisonPotion )
			{
				if ( Utility.RandomDouble() < PoisonPotionConstants.SKILL_GAIN_CHANCE_GREATER )
					from.CheckSkill( SkillName.Poisoning, PoisonPotionConstants.SKILL_CHECK_MAX_REGULAR, PoisonPotionConstants.SKILL_CHECK_MAX_GREATER );
			}
			else if ( this is DeadlyPoisonPotion )
			{
				if ( Utility.RandomDouble() < PoisonPotionConstants.SKILL_GAIN_CHANCE_DEADLY )
					from.CheckSkill( SkillName.Poisoning, PoisonPotionConstants.SKILL_CHECK_MAX_GREATER, PoisonPotionConstants.SKILL_CHECK_MAX_DEADLY );
			}
			else if ( this is LethalPoisonPotion )
			{
				if ( Utility.RandomDouble() < PoisonPotionConstants.SKILL_GAIN_CHANCE_LETHAL )
					from.CheckSkill( SkillName.Poisoning, PoisonPotionConstants.SKILL_CHECK_MAX_DEADLY, PoisonPotionConstants.SKILL_CHECK_MAX_LETHAL );
			}
		}

		/// <summary>
		/// Handles high skill path - allows player to throw poison
		/// </summary>
		/// <param name="from">The mobile with high skill</param>
		private void HandleHighSkillThrow( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
				return;
			}

			if ( !from.Region.AllowHarmful( from, from ) )
			{
				from.SendMessage( PoisonPotionStringConstants.MSG_REGION_NOT_ALLOWED );
				return;
			}

			if ( Server.Items.MonsterSplatter.TooMuchSplatter( from ) )
			{
				from.SendMessage( PoisonPotionStringConstants.MSG_TOO_MUCH_SPLATTER );
				return;
			}

			from.SendMessage( PoisonPotionStringConstants.MSG_SELECT_DUMP_LOCATION );
			ThrowTarget targ = from.Target as ThrowTarget;

			if ( targ != null && targ.Potion == this )
				return;

			from.RevealingAction();
			from.Target = new ThrowTarget( this );
		}

		/// <summary>
		/// Handles low skill path - player drinks and poisons themselves
		/// </summary>
		/// <param name="from">The mobile with low skill</param>
		private void HandleLowSkillDrink( Mobile from )
		{
			DoPoison( from );
			from.CriminalAction( false ); // Criminal action for drinking poison
			BasePotion.PlayDrinkEffect( from );
			TrySkillGain( from );
			this.Consume();
		}

		/// <summary>
		/// Validates if the throw can be performed
		/// </summary>
		/// <param name="from">The mobile throwing the potion</param>
		/// <param name="target">The target location</param>
		/// <returns>True if throw is valid, false otherwise</returns>
		private static bool ValidateThrow( Mobile from, Point3D target )
		{
			if ( from.GetDistanceToSqrt( target ) > PoisonPotionConstants.MAX_THROW_DISTANCE )
			{
				from.SendMessage( PoisonPotionStringConstants.MSG_TARGET_TOO_FAR );
				return false;
			}

			if ( !from.CanSee( target ) )
			{
				from.SendLocalizedMessage( 500237 ); // Target can not be seen.
				return false;
			}

			if ( from.Paralyzed || from.Blessed || from.Frozen || (from.Spell != null && from.Spell.IsCasting) )
			{
				from.SendMessage( PoisonPotionStringConstants.MSG_CANNOT_ACT_YET );
				return false;
			}

			return true;
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target class for throwing poison potions
		/// </summary>
		private class ThrowTarget : Target
		{
			private BasePoisonPotion m_Potion;

			/// <summary>
			/// Gets the potion being thrown
			/// </summary>
			public BasePoisonPotion Potion
			{
				get{ return m_Potion; }
			}

			/// <summary>
			/// Initializes a new instance of ThrowTarget
			/// </summary>
			/// <param name="potion">The poison potion to throw</param>
			public ThrowTarget( BasePoisonPotion potion ) : base( PoisonPotionConstants.TARGET_RANGE, true, TargetFlags.None )
			{
				m_Potion = potion;
			}

			/// <summary>
			/// Handles the target selection
			/// </summary>
			/// <param name="from">The mobile throwing the potion</param>
			/// <param name="targeted">The targeted object or location</param>
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Potion.Deleted || m_Potion.Map == Map.Internal )
					return;

				// If targeting yourself, poison yourself instead of creating a splatter
				if ( targeted is Mobile && (Mobile)targeted == from )
				{
					m_Potion.DoPoison( from );
					from.CriminalAction( false ); // Criminal action for self-poisoning
					BasePotion.PlayDrinkEffect( from );
					from.RevealingAction();
					m_Potion.Consume();
					from.AddToBackpack( new Bottle() );
					return;
				}
					
				IPoint3D p = targeted as IPoint3D;
				Point3D d = new Point3D( p );

				if ( p == null || from.Map == null )
					return;

				SpellHelper.GetSurfaceTop( ref p );

				int nThrown = 1;

				if ( !ValidateThrow( from, d ) )
				{
					nThrown = 0;
				}
				else
				{
					MonsterSplatter.AddSplatter( p.X, p.Y, p.Z, from.Map, d, from, m_Potion.Name, PoisonPotionConstants.SPLATTER_HUE, PoisonPotionConstants.SPLATTER_GLOW );
				}

				if ( nThrown > 0 )
				{
					from.RevealingAction();
					from.CriminalAction( false ); // Criminal action for throwing poison
					m_Potion.Consume();
					from.AddToBackpack( new Bottle() );
					Misc.Titles.AwardKarma( from, PoisonPotionConstants.KARMA_PENALTY_THROW, true );

					// 50% chance to poison yourself when throwing poison potion
					if ( Utility.RandomDouble() < PoisonPotionConstants.SELF_POISON_CHANCE )
					{
						m_Potion.DoPoison( from );
					}
				}
			}

			/// <summary>
			/// Called when target selection is finished
			/// </summary>
			/// <param name="from">The mobile that was targeting</param>
			protected override void OnTargetFinish( Mobile from )
			{
				// Target finish handling if needed
			}
		}

		#endregion
	}
}
