using System;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Misc;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Mark - 6th Circle Magery Spell
	/// Marks a recall rune with current location for teleportation
	/// </summary>
	public class MarkSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mark", "Kal Por Ylem",
				218,
				9002,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		#region Constants

		/// <summary>Sound effect ID for female caster</summary>
		private const int SOUND_FEMALE = 812;

		/// <summary>Sound effect ID for male caster</summary>
		private const int SOUND_MALE = 1086;

		/// <summary>Overhead message color</summary>
		private const int OVERHEAD_MESSAGE_COLOR = 0x3B2;

		/// <summary>Mark sound effect ID</summary>
		private const int MARK_SOUND_EFFECT = 0x1FA;

		/// <summary>Location effect ID</summary>
		private const int LOCATION_EFFECT_ID = 14201;

		/// <summary>Location effect duration</summary>
		private const int LOCATION_EFFECT_DURATION = 16;

		/// <summary>Fizzle particle effect ID</summary>
		private const int FIZZLE_PARTICLE_EFFECT_ID = 0x3735;

		/// <summary>Fizzle particle count</summary>
		private const int FIZZLE_PARTICLE_COUNT = 1;

		/// <summary>Fizzle particle speed</summary>
		private const int FIZZLE_PARTICLE_SPEED = 30;

		/// <summary>Fizzle particle duration</summary>
		private const int FIZZLE_PARTICLE_DURATION = 9503;

		/// <summary>Fizzle sound effect ID</summary>
		private const int FIZZLE_SOUND_EFFECT = 0x5C;

		/// <summary>Fizzle overhead message color</summary>
		private const int FIZZLE_MESSAGE_COLOR = 0x3B2;

		#endregion

		#region Data Structures

		/// <summary>Starting position when spell casting begins - player must remain here</summary>
		private Point3D m_StartPosition;

		/// <summary>Whether starting position has been set</summary>
		private bool m_PositionSet;

		#endregion

		public MarkSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
			m_PositionSet = false;
		}

		public override void OnCast()
		{
			// Store starting position - player must remain here to mark rune
			m_StartPosition = Caster.Location;
			m_PositionSet = true;
			Caster.Target = new InternalTarget( this );
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			return SpellHelper.CheckTravel( Caster, TravelCheckType.Mark );
		}

		public override bool OnCasterMoving(Direction direction)
		{
			// If spell is in casting or sequencing state, any movement attempt cancels it
			if ( m_PositionSet && (IsCasting || State == SpellState.Sequencing) )
			{
				// Player attempted to move during casting - cancel spell with visual feedback
				DoMovementFizzle();
				Disturb( DisturbType.Unspecified );
				return true; // Allow movement, but spell is cancelled
			}

			// Fall back to base behavior if position not set yet or spell not active
			return base.OnCasterMoving( direction );
		}

		/// <summary>
		/// Handles fizzle when player moves during spell casting
		/// Provides visual feedback and explains the consequence
		/// </summary>
		private void DoMovementFizzle()
		{
			// Show overhead fizzle message
			Caster.LocalOverheadMessage( MessageType.Regular, FIZZLE_MESSAGE_COLOR, 502632 ); // The spell fizzles.

			// Send explanation message to player
			Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Você não pode se mover enquanto lança este feitiço. O feitiço foi cancelado." );

			// Visual particle effect
			if ( Caster.Player )
			{
				if ( Core.AOS )
				{
					Caster.FixedParticles( FIZZLE_PARTICLE_EFFECT_ID, FIZZLE_PARTICLE_COUNT, FIZZLE_PARTICLE_SPEED, FIZZLE_PARTICLE_DURATION, EffectLayer.Waist );
				}
				else
				{
					Caster.FixedEffect( FIZZLE_PARTICLE_EFFECT_ID, 6, FIZZLE_PARTICLE_SPEED );
				}

				// Sound effect
				Caster.PlaySound( FIZZLE_SOUND_EFFECT );
			}
		}

		public override void OnDisturb(DisturbType type, bool message)
		{
			if ( message )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CONCENTRATION_DISTURBED );
			}
			// Reset position tracking when spell is disturbed
			m_PositionSet = false;
		}

		public override void FinishSequence()
		{
			// Reset position tracking when spell sequence finishes
			m_PositionSet = false;
			base.FinishSequence();
		}

		public void Target( RecallRune rune )
		{
			// Check if player moved from starting position before marking
			if ( m_PositionSet && (Caster.X != m_StartPosition.X || Caster.Y != m_StartPosition.Y) )
			{
				// Player moved - show visual feedback and explanation
				DoMovementFizzle();
				FinishSequence();
				return;
			}

			Region reg = Region.Find( Caster.Location, Caster.Map );
					
			if ( !Caster.CanSee( rune ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
				FinishSequence();
				return;
			}

			if ( reg.IsPartOf( typeof( PirateRegion ) ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Estas águas são muito agitadas para lançar este feitiço." );
				FinishSequence();
				return;
			}

			if ( Worlds.RegionAllowedTeleport( Caster.Map, Caster.Location, Caster.X, Caster.Y ) == false || !SpellHelper.CheckTravel(Caster, TravelCheckType.Mark) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Esse feitiço parece não funcionar neste lugar." );
				FinishSequence();
				return;
			}

			if ( SpellHelper.CheckMulti( Caster.Location, Caster.Map, !Core.AOS ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Esse local está bloqueado para lançar este feitiço." );
				FinishSequence();
				return;
			}

			if ( !rune.IsChildOf( Caster.Backpack ) )
			{
				Caster.PlaySound( Caster.Female ? SOUND_FEMALE : SOUND_MALE );
				Caster.LocalOverheadMessage( MessageType.Regular, OVERHEAD_MESSAGE_COLOR, false, "*Oops*" );
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Você deve ter esta runa em sua mochila para marcá-la." );
				FinishSequence();
				return;
			}

		if ( CheckSequence() )
		{
			rune.Mark( Caster );

			Caster.PlaySound( MARK_SOUND_EFFECT );
			Effects.SendLocationEffect( Caster, Caster.Map, LOCATION_EFFECT_ID, LOCATION_EFFECT_DURATION, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
			
			// Inform player of marked location with region name and coordinates
			string regionName = reg.Name;
			Caster.SendMessage( Spell.MSG_COLOR_HEAL, "Você marcou uma runa em " + regionName + " (" + Caster.X + ", " + Caster.Y + ", " + Caster.Z + ")" );
		}

			FinishSequence();
		}

		#region Internal Classes

		private class InternalTarget : Target
		{
			private MarkSpell m_Owner;

			public InternalTarget( MarkSpell owner ) : base( SpellConstants.GetSpellRange(), false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					m_Owner.Target( (RecallRune) o );
				}
				else
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, "Você deve ter utilizar uma runa em sua mochila para utilizar este feitiço." );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}
