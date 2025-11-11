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

						if ( m_NewBody == BODY_HUMAN_MALE || m_NewBody == BODY_HUMAN_FEMALE )
							Caster.HueMod = Server.Misc.RandomThings.GetRandomSkinColor();
						else
							Caster.HueMod = 0;

						BaseArmor.ValidateMobile( Caster );
						BaseClothing.ValidateMobile( Caster );

						if( !Core.ML )
						{
							StopTimer( Caster );

							Timer t = new InternalTimer( Caster );

							m_Timers[Caster] = t;

							t.Start();
						}
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
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Owner;

			public InternalTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 0 ) )
			{
				m_Owner = owner;

				int val = (int)owner.Skills[SkillName.Magery].Value;

				if ( val > MAX_MAGERY_SKILL )
					val = MAX_MAGERY_SKILL;

				Delay = TimeSpan.FromSeconds( val );
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				EndPolymorph( m_Owner );
			}
		}
	}
}
