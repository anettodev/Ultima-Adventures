using System;
using System.Collections.Generic;
using Server.Misc;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Mass Curse - 6th Circle Magery Spell
	/// Area-effect curse that reduces Str/Dex/Int of all targets in range
	/// </summary>
	public class MassCurseSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mass Curse", "Vas Des Sanct",
				218,
				9031,
				false,
				Reagent.Garlic,
				Reagent.Nightshade,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		#region Constants

		/// <summary>Standard area effect range (tiles)</summary>
		private const int AREA_RANGE_NORMAL = 2;

		/// <summary>Sorcerer area effect range (tiles)</summary>
		private const int AREA_RANGE_SORCERER = 5;

		/// <summary>Particle effect ID</summary>
		private const int PARTICLE_EFFECT_ID = 0x374A;

		/// <summary>Particle effect count</summary>
		private const int PARTICLE_COUNT = 10;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_SPEED = 15;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_DURATION = 5028;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x1FB;

		#endregion

		public MassCurseSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				if ( map != null )
				{
					// Sorcerers get a larger radius
					int range = (Caster is PlayerMobile) && ((PlayerMobile)Caster).Sorcerer() ? AREA_RANGE_SORCERER : AREA_RANGE_NORMAL;
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), range );

					foreach ( Mobile m in eable )
					{
						if ( Core.AOS && m == Caster )
							continue;

						if ( SpellHelper.ValidIndirectTarget( Caster, m ) && Caster.CanSee( m ) && Caster.CanBeHarmful( m, false ) )
							targets.Add( m );
					}

					eable.Free();
				}

				// Calculate duration once to avoid duplicate messages from NMSGetDuration
				// Use caster as representative target for duration calculation
				TimeSpan duration = SpellHelper.NMSGetDuration( Caster, Caster, false );

				int affectedCount = 0;

				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = targets[i];

					Caster.DoHarmful( m );

					// Use 5-parameter overload to pass pre-calculated duration
					// This prevents NMSGetDuration from being called 3 times per target and sending duplicate messages
					SpellHelper.AddStatCurse( Caster, m, StatType.Str, SpellHelper.GetOffset( Caster, m, StatType.Str, true ), duration ); 
					SpellHelper.DisableSkillCheck = true;
					SpellHelper.AddStatCurse( Caster, m, StatType.Dex, SpellHelper.GetOffset( Caster, m, StatType.Dex, true ), duration );
					SpellHelper.AddStatCurse( Caster, m, StatType.Int, SpellHelper.GetOffset( Caster, m, StatType.Int, true ), duration ); 
					SpellHelper.DisableSkillCheck = false;

					m.FixedParticles( PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, PARTICLE_DURATION, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
					m.PlaySound( SOUND_EFFECT );
					
					HarmfulSpell( m );
					affectedCount++;
				}

				// Send single summary message instead of one per target
				if ( affectedCount > 0 )
				{
					Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Você amaldiçoou " + affectedCount + " alvo(s) com sucesso." );
				}
			}

			FinishSequence();
		}

		#region Internal Classes

		private class InternalTarget : Target
		{
			private MassCurseSpell m_Owner;

			public InternalTarget( MassCurseSpell owner ) : base( SpellConstants.GetSpellRange(), true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}
