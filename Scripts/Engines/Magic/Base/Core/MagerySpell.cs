using System;
using Server;
using Server.Items;

namespace Server.Spells
{
	public abstract class MagerySpell : Spell
	{
		public MagerySpell( Mobile caster, Item scroll, SpellInfo info )
			: base( caster, scroll, info )
		{
		}

		public abstract SpellCircle Circle { get; }

		public override bool ConsumeReagents()
		{
			if( base.ConsumeReagents() )
				return true;

			if( ArcaneGem.ConsumeCharges( Caster, (Core.SE ? 1 : 1 + (int)Circle) ) )
				return true;

			return false;
		}

		private const double ChanceOffset = 20.0, ChanceLength = 100.0 / 7.0;

		public override void GetCastSkills( out double min, out double max )
		{
			int circle = (int)Circle;

			if( Scroll != null )
				circle -= 2;

			double avg = ChanceLength * circle;

			min = avg - ChanceOffset;
			max = avg + ChanceOffset;
		}

	/// <summary>
	/// Calculates mana cost based on spell circle
	/// Formula: Base mana increases with circle level
	/// </summary>
	public override int GetMana()
	{
		// Mana table: [4, 7, 11, 16, 22, 28, 36, 48] for circles 0-7
		// This can be calculated, but keeping array for exact values
		int[] manaTable = new int[] { 4, 7, 11, 16, 22, 28, 36, 48 };
		int circleIndex = (int)Circle;
		
		if (circleIndex >= 0 && circleIndex < manaTable.Length)
		{
			return manaTable[circleIndex];
		}
		
		// Fallback calculation if circle is out of range
		return 4 + (circleIndex * 6);
	}

	/// <summary>
	/// Calculates the maximum skill level for resistance checks based on circle
	/// </summary>
	private int CalculateMaxResistSkill( SpellCircle circle )
	{
		int maxSkill = (1 + (int)circle) * 10;
		maxSkill += (1 + ((int)circle / 6)) * 25;
		return maxSkill;
	}

	public override double GetResistSkill( Mobile m )
	{
		int maxSkill = CalculateMaxResistSkill( Circle );

		if( m.Skills[SkillName.MagicResist].Value < maxSkill )
		{
			m.CheckSkill( SkillName.MagicResist, 0.0, m.Skills[SkillName.MagicResist].Cap );
		}

		return m.Skills[SkillName.MagicResist].Value;
	}

	public virtual bool CheckResisted( Mobile target )
	{
		double n = GetResistPercent( target );

		n /= 100.0;

		if( n <= 0.0 )
		{
			return false;
		}

		if( n >= 1.0 )
		{
			return true;
		}

		int maxSkill = CalculateMaxResistSkill( Circle );

		if( target.Skills[SkillName.MagicResist].Value < maxSkill )
		{
			target.CheckSkill( SkillName.MagicResist, 0.0, target.Skills[SkillName.MagicResist].Cap );
		}

		return (n >= Utility.RandomDouble());
	}

		public virtual double GetResistPercentForCircle( Mobile target, SpellCircle circle )
		{
			/*double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
			double secondPercent = target.Skills[SkillName.MagicResist].Value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + (int)circle) * 5.0);

			return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.*/
			double firstPercent = (target.Skills[SkillName.MagicResist].Value / 5.0) - (int)circle;
			int rInt;

			if (target.Skills[SkillName.MagicResist].Value >= 100.0)
			{
				rInt = Utility.RandomMinMax(1, 5);
			}
			else
			{
				rInt = Utility.RandomMinMax(0, 3);
			}
			double secondPercent = firstPercent + rInt;
            double final = (secondPercent > 0 ? secondPercent : 1);
            target.SendMessage(95, "Voc� teve " + final + "% de chance de resistir a magia.");
			return final;
        }

		public virtual double GetResistPercent( Mobile target )
		{
			return GetResistPercentForCircle( target, Circle );
		}

		public override TimeSpan GetCastDelay()
		{
			if( !Core.AOS )
				return TimeSpan.FromSeconds( 0.5 + (0.25 * (int)Circle) );

			return base.GetCastDelay();
		}

		public override TimeSpan CastDelayBase
		{
			get
			{
				return TimeSpan.FromSeconds( (3 + (int)Circle) * CastDelaySecondsPerTick );
			}
		}
	}
}
