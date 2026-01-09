using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Fire Elemental - 8th Circle Magery Spell
	/// Summons a Fire Elemental creature to fight for the caster.
	/// Casters with 100+ Magery and EvalInt have 50% chance to summon Greater Fire Elemental.
	/// </summary>
	public class FireElementalSpell : BaseElementalSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Fire Elemental", "Kal Vas Xen Flam",
				269,
				9050,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		public FireElementalSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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
					elemental = new SummonedFireElementalGreater();
				}
				else
				{
					elemental = new SummonedFireElemental();
				}

				SpellHelper.Summon(elemental, Caster, SUMMON_EFFECT_ID, duration, false, false);
				RegisterElemental(Caster, elemental);
			}

			FinishSequence();
		}
	}
}
