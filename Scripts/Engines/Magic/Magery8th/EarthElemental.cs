using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Earth Elemental - 8th Circle Magery Spell
	/// Summons an Earth Elemental creature to fight for the caster.
	/// Casters with 100+ Magery and EvalInt have 50% chance to summon Greater Earth Elemental.
	/// </summary>
	public class EarthElementalSpell : BaseElementalSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Earth Elemental", "Kal Vas Xen Ylem",
				269,
				9020,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		public EarthElementalSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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
					elemental = new SummonedEarthElementalGreater();
				}
				else
				{
					elemental = new SummonedEarthElemental();
				}

				SpellHelper.Summon(elemental, Caster, SUMMON_EFFECT_ID, duration, false, false);
				RegisterElemental(Caster, elemental);
			}

			FinishSequence();
		}
	}
}
