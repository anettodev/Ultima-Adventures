using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Water Elemental - 8th Circle Magery Spell
	/// Summons a Water Elemental creature to fight for the caster.
	/// Casters with 100+ Magery and EvalInt have 50% chance to summon Greater Water Elemental.
	/// </summary>
	public class WaterElementalSpell : BaseElementalSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Water Elemental", "Kal Vas Xen An Flam",
				269,
				9070,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		public WaterElementalSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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
					elemental = new SummonedWaterElementalGreater();
				}
				else
				{
					elemental = new SummonedWaterElemental();
				}

				SpellHelper.Summon(elemental, Caster, SUMMON_EFFECT_ID, duration, false, false);
				RegisterElemental(Caster, elemental);
			}

			FinishSequence();
		}
	}
}
