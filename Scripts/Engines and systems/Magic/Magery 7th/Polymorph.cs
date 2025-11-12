using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Fifth;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Polymorph - 7th Circle Magery Spell
	/// Transforms caster into selected creature form
	/// </summary>
	public class PolymorphSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Polymorph", "Vas Ylem Rel",
				221,
				9002,
				Reagent.Bloodmoss,
				Reagent.SpidersSilk,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		#region Constants

		/// <summary>Body mod value for paint type 1</summary>
		private const int BODY_MOD_PAINT_1 = 183;

		/// <summary>Body mod value for paint type 2</summary>
		private const int BODY_MOD_PAINT_2 = 184;

		/// <summary>Body ID for human male</summary>
		private const int BODY_HUMAN_MALE = 400;

		/// <summary>Body ID for human female</summary>
		private const int BODY_HUMAN_FEMALE = 401;

		/// <summary>Maximum Magery skill value for duration calculation</summary>
		private const int MAX_MAGERY_SKILL = 120;

		/// <summary>Maximum duration in seconds (always limited)</summary>
		private const int MAX_DURATION_SECONDS = 120;

		/// <summary>Animal Lore skill points per second bonus (10 points = 1 second)</summary>
		private const int ANIMAL_LORE_DIVISOR = 10;

		/// <summary>Duration reduction multiplier (50% reduction)</summary>
		private const double DURATION_REDUCTION_MULTIPLIER = 0.5;

		/// <summary>Purple hue for monster forms (to distinguish from regular mobs)</summary>
		private const int MONSTER_FORM_PURPLE_HUE = 0x10;

		#endregion

		private int m_NewBody;

		public PolymorphSpell( Mobile caster, Item scroll, int body ) : base( caster, scroll, m_Info )
		{
			m_NewBody = body;
		}

		public PolymorphSpell( Mobile caster, Item scroll ) : this( caster, scroll, 0 )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if( TransformationSpellHelper.UnderTransformation( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061633 ); // You cannot polymorph while in that form.
				return false;
			}
			else if ( DisguiseTimers.IsDisguised( Caster ) )
			{
				Caster.SendLocalizedMessage( 502167 ); // You cannot polymorph while disguised.
				return false;
			}
			else if ( Caster.BodyMod == BODY_MOD_PAINT_1 || Caster.BodyMod == BODY_MOD_PAINT_2 )
			{
				Caster.SendLocalizedMessage( 1042512 ); // You cannot polymorph while wearing body paint
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( PolymorphSpell ) ) )
			{
				if( Core.ML )
					EndPolymorph( Caster );
				else 
					Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
				return false;
			}
			else if ( m_NewBody == 0 )
			{
				Gump gump;
				if ( Core.SE )
					gump = new NewPolymorphGump( Caster, Scroll );
				else
					gump = new PolymorphGump( Caster, Scroll );

				Caster.SendGump( gump );
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( !Caster.CanBeginAction( typeof( PolymorphSpell ) ) )
			{
				if( Core.ML )
					EndPolymorph( Caster );
				else
					Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061633 ); // You cannot polymorph while in that form.
			}
			else if ( DisguiseTimers.IsDisguised( Caster ) )
			{
				Caster.SendLocalizedMessage( 502167 ); // You cannot polymorph while disguised.
			}
			else if ( Caster.BodyMod == BODY_MOD_PAINT_1 || Caster.BodyMod == BODY_MOD_PAINT_2 )
			{
				Caster.SendLocalizedMessage( 1042512 ); // You cannot polymorph while wearing body paint
			}
			else if ( !Caster.CanBeginAction( typeof( IncognitoSpell ) ) || Caster.IsBodyMod )
			{
				DoFizzle();
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( PolymorphSpell ) ) )
				{
					if ( m_NewBody != 0 )
					{
						if ( !((Body)m_NewBody).IsHuman )
						{
							Mobiles.IMount mt = Caster.Mount;

							if ( mt != null )
								mt.Rider = null;
						}

						Caster.BodyMod = m_NewBody;

						Body body = (Body)m_NewBody;
						if ( m_NewBody == BODY_HUMAN_MALE || m_NewBody == BODY_HUMAN_FEMALE )
						{
							Caster.HueMod = Server.Misc.RandomThings.GetRandomSkinColor();
						}
						else if ( body.IsMonster )
						{
							// Monster forms get purple color to distinguish from regular mobs
							//Caster.HueMod = MONSTER_FORM_PURPLE_HUE;
						}
						else
						{
							Caster.HueMod = 0;
						}

						BaseArmor.ValidateMobile( Caster );
						BaseClothing.ValidateMobile( Caster );

						// Always create timer to ensure spell expiration (removed Core.ML check)
						StopTimer( Caster );

						// Calculate duration before creating timer (same logic as InternalTimer constructor)
						int magerySkill = (int)Caster.Skills[SkillName.Magery].Value;
						if ( magerySkill > MAX_MAGERY_SKILL )
							magerySkill = MAX_MAGERY_SKILL;

						double baseDuration = magerySkill * DURATION_REDUCTION_MULTIPLIER;

						// Animal Lore bonus: 1 second per 10 skill points (only for animal forms)
						int animalLoreBonus = 0;
						// Reuse 'body' variable already declared above (line 151)
						if ( body.IsAnimal )
						{
							int animalLoreSkill = (int)Caster.Skills[SkillName.AnimalLore].Value;
							animalLoreBonus = animalLoreSkill / ANIMAL_LORE_DIVISOR;
						}

						// Total duration with bonus
						int totalDuration = (int)baseDuration + animalLoreBonus;

						// Always limit duration to maximum
						if ( totalDuration > MAX_DURATION_SECONDS )
							totalDuration = MAX_DURATION_SECONDS;

						Timer t = new InternalTimer( Caster, m_NewBody );

						m_Timers[Caster] = t;

						// Send transformation start message with duration
						Caster.SendMessage( Spell.MSG_COLOR_HEAL, String.Format( Spell.SpellMessages.INFO_POLYMORPH_TRANSFORM_START_FORMAT, totalDuration ) );

						t.Start();
					}
				}
				else
				{
					Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
				}
			}

			FinishSequence();
		}

		private static Dictionary<Mobile, Timer> m_Timers = new Dictionary<Mobile, Timer>();

		public static bool StopTimer( Mobile m )
		{
			Timer t;
			if ( m_Timers.TryGetValue( m, out t ) )
			{
				t.Stop();
				m_Timers.Remove( m );
				return true;
			}

			return false;
		}

		private static void EndPolymorph( Mobile m )
		{
			if( !m.CanBeginAction( typeof( PolymorphSpell ) ) )
			{
				m.BodyMod = 0;
				m.HueMod = -1;
				m.EndAction( typeof( PolymorphSpell ) );

				BaseArmor.ValidateMobile( m );
				BaseClothing.ValidateMobile( m );

				// Send creative transformation end message
				m.SendMessage( Spell.MSG_COLOR_HEAL, Spell.SpellMessages.INFO_POLYMORPH_TRANSFORM_END );
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Owner;

			public InternalTimer( Mobile owner, int bodyID ) : base( TimeSpan.FromSeconds( 0 ) )
			{
				m_Owner = owner;

				// Base duration: Magery skill reduced by half
				int magerySkill = (int)owner.Skills[SkillName.Magery].Value;
				if ( magerySkill > MAX_MAGERY_SKILL )
					magerySkill = MAX_MAGERY_SKILL;

				double baseDuration = magerySkill * DURATION_REDUCTION_MULTIPLIER;

				// Animal Lore bonus: 1 second per 10 skill points (only for animal forms, not monsters or humans)
				int animalLoreBonus = 0;
				Body body = (Body)bodyID;
				if ( body.IsAnimal )
				{
					int animalLoreSkill = (int)owner.Skills[SkillName.AnimalLore].Value;
					animalLoreBonus = animalLoreSkill / ANIMAL_LORE_DIVISOR;
				}

				// Total duration with bonus
				int totalDuration = (int)baseDuration + animalLoreBonus;

				// Always limit duration to maximum
				if ( totalDuration > MAX_DURATION_SECONDS )
					totalDuration = MAX_DURATION_SECONDS;

				Delay = TimeSpan.FromSeconds( totalDuration );
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				EndPolymorph( m_Owner );
			}
		}
	}
}
