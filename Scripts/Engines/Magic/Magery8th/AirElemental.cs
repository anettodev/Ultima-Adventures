using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Air Elemental - 8th Circle Magery Spell
	/// Summons an Air Elemental creature to fight for the caster.
	/// Casters with 100+ Magery and EvalInt have 50% chance to summon Greater Air Elemental.
	/// </summary>
	public class AirElementalSpell : BaseElementalSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Air Elemental", "Kal Vas Xen Hur",
				269,
				9010,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		public AirElementalSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			if (CheckSequence())
			{
				TimeSpan duration = CalculateSummonDuration();
				SendDurationMessage(duration);

				BaseCreature elemental;
				if (ShouldSummonGreaterElemental())
				{
					elemental = new SummonedAirElementalGreater();
				}
				else
				{
					elemental = new SummonedAirElemental();
				}

				SpellHelper.Summon(elemental, Caster, SUMMON_EFFECT_ID, duration, false, false);
				RegisterElemental(Caster, elemental);
			}

			FinishSequence();
		}
	}
}
