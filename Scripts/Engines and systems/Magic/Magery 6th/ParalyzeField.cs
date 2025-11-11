using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Paralyze Field - 6th Circle Magery Spell
	/// Creates a field that paralyzes mobiles walking over it
	/// </summary>
	public class ParalyzeFieldSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Paralyze Field", "In Ex Grav",
				230,
				9012,
				false,
				Reagent.BlackPearl,
				Reagent.Ginseng,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		#region Constants

		/// <summary>Field item ID for East-West orientation</summary>
		private const int FIELD_ITEM_ID_EAST_WEST = 0x3967;

		/// <summary>Field item ID for North-South orientation</summary>
		private const int FIELD_ITEM_ID_NORTH_SOUTH = 0x3979;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x20B;

		/// <summary>Magery benefit multiplier for field duration</summary>
		private const double MAGERY_BENEFIT_MULTIPLIER = 0.25;

		/// <summary>Base paralyze duration in seconds</summary>
		private const double PARALYZE_BASE_DURATION = 3.0;

		/// <summary>Magery skill multiplier for paralyze duration</summary>
		private const double PARALYZE_MAGERY_MULTIPLIER = 0.1;

		/// <summary>EvalInt benefit multiplier for paralyze duration</summary>
		private const double PARALYZE_EVALINT_BENEFIT_MULTIPLIER = 0.1;

		/// <summary>Non-player duration multiplier</summary>
		private const double PARALYZE_NON_PLAYER_MULTIPLIER = 2.0;

		/// <summary>Field adjustment range</summary>
		private const int FIELD_ADJUSTMENT_RANGE = 12;

		/// <summary>Particle effect ID</summary>
		private const int PARTICLE_EFFECT_ID = 0x376A;

		/// <summary>Particle effect count</summary>
		private const int PARTICLE_COUNT = 9;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_SPEED = 10;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_DURATION = 5048;

		/// <summary>Paralyze sound effect ID</summary>
		private const int PARALYZE_SOUND_EFFECT = 0x204;

		/// <summary>Paralyze effect ID</summary>
		private const int PARALYZE_EFFECT_ID = 0x376A;

		/// <summary>Paralyze effect count</summary>
		private const int PARALYZE_EFFECT_COUNT = 10;

		/// <summary>Paralyze effect duration</summary>
		private const int PARALYZE_EFFECT_DURATION = 16;

		#endregion

		public ParalyzeFieldSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
			SpellHelper.Turn( Caster, p );

			SpellHelper.GetSurfaceTop( ref p );

			// Use centralized field orientation calculation (includes self-targeting fix)
			bool eastToWest = FieldSpellHelper.GetFieldOrientation( Caster, p );

				Effects.PlaySound( p, Caster.Map, SOUND_EFFECT );

				int itemID = eastToWest ? FIELD_ITEM_ID_EAST_WEST : FIELD_ITEM_ID_NORTH_SOUTH;

				int nBenefit = 0;
				if ( Caster is PlayerMobile )
				{
					nBenefit = (int)(Caster.Skills[SkillName.Magery].Value * MAGERY_BENEFIT_MULTIPLIER);
				}

				// Use FieldSpellHelper for consistent duration calculation
				TimeSpan duration = FieldSpellHelper.GetFieldDuration( Caster );

				// Create field items in a line (-2 to +2 range)
				for ( int i = -FieldSpellHelper.FIELD_RANGE; i <= FieldSpellHelper.FIELD_RANGE; ++i )
				{
					Point3D loc = new Point3D( eastToWest ? p.X + i : p.X, eastToWest ? p.Y : p.Y + i, p.Z );
					bool canFit = SpellHelper.AdjustField( ref loc, Caster.Map, FIELD_ADJUSTMENT_RANGE, false );

					if ( !canFit )
						continue;

					Item item = new InternalItem( Caster, itemID, loc, Caster.Map, duration );
					item.ProcessDelta();

					Effects.SendLocationParticles( EffectItem.Create( loc, Caster.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION, 0 );
				}
			}

			FinishSequence();
		}

		#region Internal Classes

		[DispellableField]
		public class InternalItem : Item
		{
			private Timer m_Timer;
			private Mobile m_Caster;
			private DateTime m_End;

			public override bool BlocksFit{ get{ return true; } }

			public InternalItem( Mobile caster, int itemID, Point3D loc, Map map, TimeSpan duration ) : base( itemID )
			{
				Visible = false;
				Movable = false;
				Light = LightType.Circle300;
				Hue = 0; 
				if ( Server.Items.CharacterDatabase.GetMySpellHue( caster, 0 ) >= 0 )
				{ 
					Hue = Server.Items.CharacterDatabase.GetMySpellHue( caster, 0 ) + 1; 
				}

				MoveToWorld( loc, map );

				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();

				if ( Deleted )
					return;

				m_Caster = caster;

				m_Timer = new InternalTimer( this, duration );
				m_Timer.Start();

				m_End = DateTime.UtcNow + duration;
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version

				writer.Write( m_Caster );
				writer.WriteDeltaTime( m_End );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				switch ( version )
				{
					case 0:
					{
						m_Caster = reader.ReadMobile();
						m_End = reader.ReadDeltaTime();

						m_Timer = new InternalTimer( this, m_End - DateTime.UtcNow );
						m_Timer.Start();

						break;
					}
				}
			}

			public override bool OnMoveOver( Mobile m )
			{
				// Caster can be paralyzed by their own field - no special exclusion
				if ( Visible && m_Caster != null && SpellHelper.ValidIndirectTarget( m_Caster, m ) && m_Caster.CanBeHarmful( m, false ) )
				{
					if ( SpellHelper.CanRevealCaster( m ) )
						m_Caster.RevealingAction();

					m_Caster.DoHarmful( m );

					double duration;

					int nBenefit = 0;
					if ( m_Caster is PlayerMobile )
					{
						nBenefit = (int)(m_Caster.Skills[SkillName.EvalInt].Value * PARALYZE_EVALINT_BENEFIT_MULTIPLIER);
					}

					duration = PARALYZE_BASE_DURATION + (m_Caster.Skills[SkillName.Magery].Value * PARALYZE_MAGERY_MULTIPLIER) + nBenefit;
					if ( !m.Player )
						duration *= PARALYZE_NON_PLAYER_MULTIPLIER;

					m.Paralyze( TimeSpan.FromSeconds( duration ) );
					m.PlaySound( PARALYZE_SOUND_EFFECT );
					m.FixedEffect( PARALYZE_EFFECT_ID, PARALYZE_EFFECT_COUNT, PARALYZE_EFFECT_DURATION, Server.Items.CharacterDatabase.GetMySpellHue( m_Caster, 0 ), 0 );
					
					if ( m is BaseCreature )
						((BaseCreature) m).OnHarmfulSpell( m_Caster );
				}

				return true;
			}

			private class InternalTimer : Timer
			{
				private Item m_Item;

				public InternalTimer( Item item, TimeSpan duration ) : base( duration )
				{
					Priority = TimerPriority.OneSecond;
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}

		public class InternalTarget : Target
		{
			private ParalyzeFieldSpell m_Owner;

			public InternalTarget( ParalyzeFieldSpell owner ) : base( SpellConstants.GetSpellRange(), true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}
