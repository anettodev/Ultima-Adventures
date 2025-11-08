using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Third
{
	/// <summary>
	/// Fireball - 3rd Circle Attack Spell
	/// Deals fire damage to a target
	/// </summary>
	public class FireballSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Fireball", "Vas Flam",
				203,
				9041,
				Reagent.BlackPearl
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Damage Constants
		/// <summary>Base damage bonus added to dice roll (1d6+4 = 5-10 base)</summary>
		private const int DAMAGE_BONUS = 4;
		/// <summary>Number of dice to roll</summary>
		private const int DAMAGE_DICE = 1;
		/// <summary>Number of sides per die</summary>
		private const int DAMAGE_SIDES = 6;

		// Minimum Damage Floor Constants (based on EvalInt skill)
		/// <summary>Minimum damage guaranteed at 120+ EvalInt skill</summary>
		private const int MIN_DAMAGE_EVAL_120 = 9;
		/// <summary>Minimum damage guaranteed at 100+ EvalInt skill</summary>
		private const int MIN_DAMAGE_EVAL_100 = 8;
		/// <summary>Minimum damage guaranteed at 80+ EvalInt skill</summary>
		private const int MIN_DAMAGE_EVAL_80 = 7;
		/// <summary>Base minimum damage for lower skill levels</summary>
		private const int MIN_DAMAGE_BASE = 5;

		// Effect Constants
		/// <summary>Particle effect ID for fireball projectile animation</summary>
		private const int EFFECT_ID = 0x36D4;
		/// <summary>Speed of particle effect animation</summary>
		private const int EFFECT_SPEED = 7;
		/// <summary>Duration of particle effect in milliseconds</summary>
		private const int EFFECT_DURATION = 9502;
		/// <summary>Item ID used for particle effect rendering</summary>
		private const int EFFECT_ITEM_ID = 4019;
		/// <summary>Effect layer for particle rendering</summary>
		private const int EFFECT_LAYER = 0x160;
		/// <summary>Sound ID for Age of Shadows client</summary>
		private const int SOUND_ID_AOS = 0x15E;
		/// <summary>Sound ID for Legacy client</summary>
		private const int SOUND_ID_LEGACY = 0x44B;
		/// <summary>Default hue for spell effects (0 = use character's custom hue)</summary>
		private const int DEFAULT_HUE = 0;

		// Target Constants
		/// <summary>Maximum targeting range for Mondain's Legacy client</summary>
		private const int TARGET_RANGE_ML = 10;
		/// <summary>Maximum targeting range for Legacy client</summary>
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		public FireballSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile target )
		{
			if ( !Caster.CanSee( target ) )
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
				FinishSequence();
				return;
			}

			if ( CheckHSequence( target ) )
			{
				Mobile source = Caster;

				SpellHelper.Turn( source, target );
				SpellHelper.NMSCheckReflect( (int)this.Circle, ref source, ref target );

				double damage = CalculateDamage(target);
				PlayEffects(source, target);

				// Apply damage (0% physical, 100% fire, 0% cold, 0% poison, 0% energy)
				SpellHelper.Damage( this, target, damage, 0, 100, 0, 0, 0 );
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates spell damage using NMS system with skill-based minimum damage floor
		/// </summary>
		/// <param name="target">The target mobile</param>
		/// <returns>Calculated damage amount (guaranteed minimum based on EvalInt skill)</returns>
		private double CalculateDamage(Mobile target)
		{
			double damage = GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
			int minDamage = GetMinimumDamageForSkill();
			return Math.Max(damage, minDamage);
		}

		/// <summary>
		/// Gets the minimum damage floor based on caster's EvalInt skill
		/// Ensures high-skill casters have a guaranteed damage baseline
		/// </summary>
		/// <returns>Minimum damage value based on skill level</returns>
		private int GetMinimumDamageForSkill()
		{
			double evalInt = Caster.Skills[SkillName.EvalInt].Value;
			
			if (evalInt >= 120.0)
				return MIN_DAMAGE_EVAL_120; // 9 minimum at 120+ EvalInt
			else if (evalInt >= 100.0)
				return MIN_DAMAGE_EVAL_100; // 8 minimum at 100+ EvalInt
			else if (evalInt >= 80.0)
				return MIN_DAMAGE_EVAL_80;  // 7 minimum at 80+ EvalInt
			else
				return MIN_DAMAGE_BASE;     // 5 base minimum
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		/// <param name="source">The source mobile</param>
		/// <param name="target">The target mobile</param>
		private void PlayEffects(Mobile source, Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
			source.MovingParticles(target, EFFECT_ID, EFFECT_SPEED, 0, false, true, hue, 0, EFFECT_DURATION, EFFECT_ITEM_ID, EFFECT_LAYER, 0);
			source.PlaySound(Core.AOS ? SOUND_ID_AOS : SOUND_ID_LEGACY);
		}

		private class InternalTarget : Target
		{
			private FireballSpell m_Owner;

			public InternalTarget(FireballSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is Mobile)
			{
				m_Owner.Target((Mobile)o);
			}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
