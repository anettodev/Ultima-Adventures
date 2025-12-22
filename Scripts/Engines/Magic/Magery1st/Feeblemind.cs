using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.First
{
	/// <summary>
	/// Feeblemind - 1st Circle Curse Spell
	/// Temporarily reduces the target's Intelligence
	/// </summary>
	public class FeeblemindSpell : MagerySpell
	{
		#region Constants
		// Spell Info
		private const int SPELL_ID = 212;
		private const int SPELL_ACTION = 9031;

		// Effect Constants
		private const int EFFECT_ID = 0x3779;
		private const int EFFECT_SPEED = 10;
		private const int EFFECT_RENDER = 15;
		private const int EFFECT_EFFECT = 5004;
		private const int EFFECT_HUE_DEFAULT = 0;

		// Sound Constants
		private const int SOUND_ID = 0x1E4;

		// Target Constants
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Feeblemind", "Rel Wis",
				SPELL_ID,
				SPELL_ACTION,
				Reagent.Ginseng,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public FeeblemindSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile target )
		{
			// Check for Sorcerer immunity (SkirtOfPower)
			if ( HasSorcererImmunity( target ) )
			{
				return;
			}

			if ( !Caster.CanSee( target ) )
			{
				Caster.SendMessage( MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( CheckHSequence( target ) )
			{
				SpellHelper.Turn( Caster, target );

				SpellHelper.CheckReflect( (int)Circle, Caster, ref target );

				SpellHelper.AddStatCurse( Caster, target, StatType.Int );

				if ( target.Spell != null )
					target.Spell.OnCasterHurt();

				target.Paralyzed = false;

				PlayEffects( target );

				int percentage = (int)(SpellHelper.GetOffsetScalar( Caster, target, true ) * 100);
				TimeSpan length = SpellHelper.NMSGetDuration( Caster, target, false );

				BuffInfo.AddBuff( target, new BuffInfo( BuffIcon.FeebleMind, 1075833, length, target, percentage.ToString() ) );

				HarmfulSpell( target );
			}

			FinishSequence();
		}

	/// <summary>
	/// Checks if target has Sorcerer immunity via SkirtOfPower
	/// </summary>
	private bool HasSorcererImmunity( Mobile target )
	{
		if ( target is PlayerMobile )
		{
			PlayerMobile playerMobile = (PlayerMobile)target;
			if ( playerMobile.Sorcerer() )
			{
				Item pants = playerMobile.FindItemOnLayer( Layer.OuterLegs );
				if ( pants != null && pants is SkirtOfPower )
				{
					return true;
				}
			}
		}
		return false;
	}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		private void PlayEffects( Mobile target )
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue( Caster, EFFECT_HUE_DEFAULT );
			target.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_EFFECT, hue, EFFECT_HUE_DEFAULT, EffectLayer.Head );
			target.PlaySound( SOUND_ID );
		}

		private class InternalTarget : Target
		{
			private FeeblemindSpell m_Owner;

			public InternalTarget( FeeblemindSpell owner ) : base( Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

		protected override void OnTarget( Mobile from, object o )
		{
			if ( o is Mobile )
			{
				m_Owner.Target( (Mobile)o );
			}
		}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
