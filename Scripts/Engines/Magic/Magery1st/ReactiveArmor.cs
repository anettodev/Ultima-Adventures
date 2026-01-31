using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.First
{
	/// <summary>
	/// Reactive Armor - 1st Circle Defensive Buff Spell
	/// Provides physical damage resistance or damage absorption depending on AOS/Legacy mode
	/// </summary>
	public class ReactiveArmorSpell : MagerySpell
	{
		#region Constants
		// Spell Info
		private const int SPELL_ID = 236;
		private const int SPELL_ACTION = 9011;

		// Effect Constants
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_RENDER = 32;
		private const int EFFECT_EFFECT = 5008;
		private const int EFFECT_HUE_DEFAULT = 0;

		// Sound Constants
		private const int SOUND_ID_ACTIVATE = 0x1E9;
		private const int SOUND_ID_DEACTIVATE = 0x1ED;
		private const int SOUND_ID_LEGACY = 0x1F2;

		// Resistance Calculation (AOS)
		private const int MAGERY_INSCRIBE_DIVISOR = 350;
		private const int INT_RESIST_DIVISOR = 100;
		private const int SORCERER_PHYSICAL_RESIST = 10;
		private const int DEFAULT_PHYSICAL_RESIST = 50;

		// Duration (AOS)
		private const double TOTAL_DURATION_SECONDS = 15.0;

		// Legacy (non-AOS) constants
		private const int LEGACY_SKILL_DIVISOR = 3;
		private const int LEGACY_MIN_ABSORB = 1;
		private const int LEGACY_MAX_ABSORB = 75;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Reactive Armor", "Flam Sanct",
				SPELL_ID,
				SPELL_ACTION,
				Reagent.Garlic,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public ReactiveArmorSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast( Mobile caster )
		{
			if ( Core.AOS )
				return true;

			// Legacy mode validation
			if ( Caster.MeleeDamageAbsorb > 0 )
			{
				Caster.SendMessage( MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_ALREADY_UNDER_EFFECT );
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( MSG_COLOR_ERROR, SpellMessages.ERROR_SPELL_WILL_NOT_ADHERE );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Core.AOS )
			{
				OnCastAOS();
			}
			else
			{
				OnCastLegacy();
			}

			FinishSequence();
		}

		/// <summary>
		/// AOS version - Applies physical resistance modifier
		/// </summary>
		private void OnCastAOS()
		{
			if ( CheckSequence() )
			{
				Mobile target = Caster;

				ResistanceMod[] mods = (ResistanceMod[])m_Table[target];

				if ( mods == null )
				{
					// Apply new reactive armor
					PlayActivationEffects( target );

					int resistValue = CalculateResistanceValue();
					mods = new ResistanceMod[1]
					{
						new ResistanceMod( ResistanceType.Physical, resistValue )
					};

					m_Table[target] = mods;

					for ( int i = 0; i < mods.Length; ++i )
						target.AddResistanceMod( mods[i] );

					new InternalTimer( target, TimeSpan.FromSeconds( TOTAL_DURATION_SECONDS ) ).Start();
				}
				else
				{
					// Remove existing reactive armor
					PlayDeactivationEffects( target );

					m_Table.Remove( target );

					for ( int i = 0; i < mods.Length; ++i )
						target.RemoveResistanceMod( mods[i] );

					BuffInfo.RemoveBuff( Caster, BuffIcon.ReactiveArmor );
				}
			}
		}

		/// <summary>
		/// Legacy (pre-AOS) version - Applies melee damage absorption
		/// </summary>
		private void OnCastLegacy()
		{
			if ( Caster.MeleeDamageAbsorb > 0 )
			{
				Caster.SendMessage( MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_UNDER_SIMILAR_EFFECT );
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( MSG_COLOR_ERROR, SpellMessages.ERROR_SPELL_WILL_NOT_ADHERE_NOW );
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
				{
					int value = CalculateLegacyAbsorbValue();

					Caster.MeleeDamageAbsorb = value;

					Caster.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_EFFECT, EffectLayer.Waist );
					Caster.PlaySound( SOUND_ID_LEGACY );
				}
				else
				{
					Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				}
			}
		}

		/// <summary>
		/// Calculates the physical resistance value for AOS mode
		/// </summary>
		private int CalculateResistanceValue()
		{
			if ( IsSorcerer() )
			{
				return SORCERER_PHYSICAL_RESIST;
			}
			else
			{
				return DEFAULT_PHYSICAL_RESIST;
			}
		}

		/// <summary>
		/// Calculates the damage absorption value for Legacy mode
		/// </summary>
		private int CalculateLegacyAbsorbValue()
		{
			int value = (int)(Caster.Skills[SkillName.Magery].Value + 
			                   Caster.Skills[SkillName.Meditation].Value + 
			                   Caster.Skills[SkillName.Inscribe].Value);
			value /= LEGACY_SKILL_DIVISOR;

			if ( value < LEGACY_MIN_ABSORB )
				value = LEGACY_MIN_ABSORB;
			else if ( value > LEGACY_MAX_ABSORB )
				value = LEGACY_MAX_ABSORB;

			return value;
		}

	/// <summary>
	/// Checks if caster is a Sorcerer class
	/// </summary>
	private bool IsSorcerer()
	{
		if ( Caster is PlayerMobile )
		{
			PlayerMobile playerMobile = (PlayerMobile)Caster;
			return playerMobile.Sorcerer();
		}
		return false;
	}

		/// <summary>
		/// Plays visual and sound effects when armor is activated
		/// </summary>
		private void PlayActivationEffects( Mobile target )
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue( Caster, EFFECT_HUE_DEFAULT );
			target.PlaySound( SOUND_ID_ACTIVATE );
			target.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_EFFECT, hue, EFFECT_HUE_DEFAULT, EffectLayer.Waist );
		}

		/// <summary>
		/// Plays visual and sound effects when armor is deactivated
		/// </summary>
		private void PlayDeactivationEffects( Mobile target )
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue( Caster, EFFECT_HUE_DEFAULT );
			target.PlaySound( SOUND_ID_DEACTIVATE );
			target.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_EFFECT, hue, EFFECT_HUE_DEFAULT, EffectLayer.Waist );
		}

		/// <summary>
		/// Removes reactive armor from a mobile
		/// </summary>
		public static void EndArmor( Mobile m )
		{
			if ( m_Table.Contains( m ) )
			{
				ResistanceMod[] mods = (ResistanceMod[])m_Table[m];

				if ( mods != null )
				{
					for ( int i = 0; i < mods.Length; ++i )
						m.RemoveResistanceMod( mods[i] );
				}

				m_Table.Remove( m );
				BuffInfo.RemoveBuff( m, BuffIcon.ReactiveArmor );
			}
		}

		/// <summary>
		/// Timer to automatically remove reactive armor after duration expires
		/// </summary>
		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;
			private DateTime m_Expire;

			public InternalTimer( Mobile caster, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_Mobile = caster;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					EndArmor( m_Mobile );
					Stop();
				}
			}
		}
	}
}
