using System;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Regions;
using Server.Spells.Necromancy;
using Server.Misc;

namespace Server.Spells.Fourth
{
	public class RecallSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Recall", "Kal Ort Por",
				239,
				9031,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		#region Constants
		// Effect Constants
		private const int SOUND_ID = 0x1FC;
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_COUNT = 9;
		private const int EFFECT_SPEED = 32;
		private const int EFFECT_DURATION = 5024;
		private const int DEFAULT_HUE = 0;
		
		// Target Constants
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		private const int TARGET_MESSAGE_HUE = 0x3B2;
		private const int TARGET_MESSAGE_CLILOC = 501029; // Select Marked item.
		#endregion

		private RunebookEntry m_Entry;
		private Runebook m_Book;

		public RecallSpell( Mobile caster, Item scroll ) : this( caster, scroll, null, null )
		{
		}

		public RecallSpell( Mobile caster, Item scroll, RunebookEntry entry, Runebook book ) : base( caster, scroll, m_Info )
		{
			m_Entry = entry;
			m_Book = book;
		}

		public override void GetCastSkills( out double min, out double max )
		{
			if ( TransformationSpellHelper.UnderTransformation( Caster, typeof( WraithFormSpell ) ) )
				min = max = 0;
			else if( Core.SE && m_Book != null )	//recall using Runebook charge
				min = max = 0;
			else
				base.GetCastSkills( out min, out max );
		}

		public override void OnCast()
		{
			if ( m_Entry == null )
				Caster.Target = new InternalTarget( this );
			else
				Effect( m_Entry.Location, m_Entry.Map, true );
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
                DoFizzle();
                Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TOO_HEAVY_TELEPORT);
				return false;
			}

			return SpellHelper.CheckTravel( Caster, TravelCheckType.RecallFrom );
		}

		public void Effect( Point3D loc, Map map, bool checkMulti )
		{
			// Staff recall bypasses all validations
			if ( Caster.AccessLevel > AccessLevel.Player )
			{
				ExecuteRecall( loc, map, true );
				return;
			}

			// Validate recall destination
			if ( !ValidateRecallDestination( loc, map, checkMulti ) )
			{
				FinishSequence();
				return;
			}

			// Execute recall if sequence check passes
			if ( CheckSequence() )
			{
				ExecuteRecall( loc, map, false );
			}

			FinishSequence();
		}

		/// <summary>
		/// Validates if the recall destination is accessible
		/// Returns false if validation fails (error message already sent)
		/// </summary>
		private bool ValidateRecallDestination( Point3D loc, Map map, bool checkMulti )
		{
			// Check travel restrictions
			if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.RecallFrom ) || 
				 !SpellHelper.CheckTravel( Caster, map, loc, TravelCheckType.RecallTo ) )
			{
				DoFizzle();
				return false;
			}

			// Check escape/recall region restrictions
			if ( !Worlds.AllowEscape( Caster, Caster.Map, Caster.Location, Caster.X, Caster.Y ) || 
				 !Worlds.RegionAllowedRecall( Caster.Map, Caster.Location, Caster.X, Caster.Y ) )
			{
				SendError( Spell.SpellMessages.ERROR_SPELL_DOES_NOT_WORK_HERE );
				return false;
			}

			// Check destination teleport region restrictions
			if ( !Worlds.RegionAllowedTeleport( map, loc, loc.X, loc.Y ) )
			{
				SendError( Spell.SpellMessages.ERROR_DESTINATION_MAGICALLY_INACCESSIBLE );
				return false;
			}

			// Check if location is discovered (only for PlayerMobile)
			string world = Worlds.GetMyWorld( map, loc, loc.X, loc.Y );
			if ( Caster is PlayerMobile && !CharacterDatabase.GetDiscovered( Caster, world ) )
			{
				SendError( Spell.SpellMessages.ERROR_LOCATION_NOT_DISCOVERED );
				return false;
			}

			// Check if location is blocked by multi
			if ( checkMulti && SpellHelper.CheckMulti( loc, map ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCATION_BLOCKED_TELEPORT );
				return false;
			}

			// Check runebook charges
			if ( m_Book != null && m_Book.CurCharges <= 0 )
			{
				SendError( Spell.SpellMessages.ERROR_NO_CHARGES_LEFT );
				return false;
			}

			return true;
		}

		/// <summary>
		/// Sends error message and fizzles the spell
		/// </summary>
		private void SendError( string message )
		{
			DoFizzle();
			Caster.SendMessage( Spell.MSG_COLOR_ERROR, message );
		}

		/// <summary>
		/// Executes the recall teleportation
		/// </summary>
		private void ExecuteRecall( Point3D loc, Map map, bool isStaffRecall )
		{
			// Teleport pets
			BaseCreature.TeleportPets( Caster, loc, map, isStaffRecall );

			// Consume runebook charge
			if ( m_Book != null )
				--m_Book.CurCharges;

			// Handle criminal status for specific regions
			HandleCriminalStatus( loc, map );

			// Play effects and sounds
			PlayRecallEffects( loc, map );
		}

		/// <summary>
		/// Handles criminal status changes for specific regions
		/// </summary>
		private void HandleCriminalStatus( Point3D loc, Map map )
		{
			if ( Caster is PlayerMobile && Caster.Karma < -500 && ((PlayerMobile)Caster).Criminal && 
				 map == Map.Ilshenar )
			{
				string regionName = Server.Misc.Worlds.GetRegionName( map, loc );
				if ( regionName == "DarkMoor" || regionName == "the Temple of Praetoria" )
				{
					((PlayerMobile)Caster).Criminal = false; // Prevent insta-kill in RED/CRIMINAL allowed places
				}
			}
		}

		/// <summary>
		/// Plays visual and sound effects for recall
		/// </summary>
		private void PlayRecallEffects( Point3D loc, Map map )
		{
			int spellHue = Server.Items.CharacterDatabase.GetMySpellHue( Caster, DEFAULT_HUE );

			// Play sound and particles at source location
			Caster.PlaySound( SOUND_ID );
			Effects.SendLocationParticles( 
				EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 
				EFFECT_ID, EFFECT_COUNT, EFFECT_SPEED, spellHue, 0, EFFECT_DURATION, 0 );

			// Move to destination
			Caster.MoveToWorld( loc, map );

			// Play sound and particles at destination location
			Caster.PlaySound( SOUND_ID );
			Effects.SendLocationParticles( 
				EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 
				EFFECT_ID, EFFECT_COUNT, EFFECT_SPEED, spellHue, 0, EFFECT_DURATION, 0 );
		}

		private class InternalTarget : Target
		{
			private RecallSpell m_Owner;

			public InternalTarget( RecallSpell owner ) : base( Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.None )
			{
				m_Owner = owner;

				owner.Caster.LocalOverheadMessage( MessageType.Regular, TARGET_MESSAGE_HUE, TARGET_MESSAGE_CLILOC );
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					RecallRune rune = (RecallRune)o;

					if ( rune.Marked )
						m_Owner.Effect( rune.Target, rune.TargetMap, true );
					else
						from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_RUNE_NOT_MARKED );
				}
				else if ( o is Runebook )
				{
					RunebookEntry e = ((Runebook)o).Default;

					if ( e != null )
						m_Owner.Effect( e.Location, e.Map, true );
					else
						from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_MARKED );
				}
				else if ( o is Key && ((Key)o).KeyValue != 0 && ((Key)o).Link is BaseBoat )
				{
					BaseBoat boat = ((Key)o).Link as BaseBoat;

					if ( !boat.Deleted && boat.CheckKey( ((Key)o).KeyValue ) )
						m_Owner.Effect( boat.GetMarkedLocation(), boat.Map, false );
					else
						from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_RECALL_FROM_OBJECT );
				}
				else if ( o is HouseRaffleDeed && ((HouseRaffleDeed)o).ValidLocation() )
				{
					HouseRaffleDeed deed = (HouseRaffleDeed)o;

					m_Owner.Effect( deed.PlotLocation, deed.PlotFacet, true );
				}
				else
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_RECALL_FROM_OBJECT );
				}
			}
			
			protected override void OnNonlocalTarget( Mobile from, object o )
			{
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
