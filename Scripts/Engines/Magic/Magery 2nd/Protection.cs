using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
	/// <summary>
	/// Protection - 2nd Circle Toggle Buff Spell
	/// Reduces resistance stats but prevents spell disruption
	/// </summary>
	public class ProtectionSpell : MagerySpell
	{
		#region Constants
		// Resistance Modifiers (randomized between -2 and -8 per resistance)
		private const int RESISTANCE_PENALTY_MIN = -8;
		private const int RESISTANCE_PENALTY_MAX = -2;

		// Disruption Protection (Balance Nerf)
		private const double DISRUPTION_PROTECTION_FIRST_HIT = 100.0; // 100% protection on first hit
		private const double DISRUPTION_PROTECTION_AFTER_FIRST = 50.0; // 50% protection after first hit

		// Timer Constants (Legacy Mode)
		private const double MIN_DURATION_SECONDS = 15.0;
		private const double MAX_DURATION_SECONDS = 30.0;
		private const double DURATION_MAGERY_MULTIPLIER = 2.0;

		// Effects
		private const int EFFECT_ID = 0x375A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_DURATION = 20;
		private const int EFFECT_RENDER = 5016;
		private const int SOUND_ADD = 0x1E9;
		private const int SOUND_REMOVE = 0x1ED;
		private const int DEFAULT_HUE = 0;
		#endregion

		private static Hashtable m_Registry = new Hashtable();
		public static Hashtable Registry { get { return m_Registry; } }

		/// <summary>
		/// Tracks Protection disruption prevention state
		/// </summary>
		public class ProtectionEntry
		{
			private bool m_FirstHitAbsorbed;
			private Mobile m_Target;

			public ProtectionEntry(Mobile target)
			{
				m_Target = target;
				m_FirstHitAbsorbed = false;
			}

			/// <summary>
			/// Gets the current disruption protection percentage
			/// Returns 100% for first hit, 50% after that
			/// </summary>
			public double GetProtectionValue()
			{
				if (!m_FirstHitAbsorbed)
				{
					m_FirstHitAbsorbed = true;
					return DISRUPTION_PROTECTION_FIRST_HIT;
				}
				
				return DISRUPTION_PROTECTION_AFTER_FIRST;
			}

			/// <summary>
			/// Implicit conversion to double for backward compatibility
			/// </summary>
			public static implicit operator double(ProtectionEntry entry)
			{
				return entry.GetProtectionValue();
			}
		}

		private static SpellInfo m_Info = new SpellInfo(
				"Protection", "Uus Sanct",
				236,
				9011,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public ProtectionSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if (Core.AOS)
				return true;

			if (m_Registry.ContainsKey(Caster))
			{
				Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.PROTECTION_ALREADY_ACTIVE);
				return false;
			}
			else if (!Caster.CanBeginAction(typeof(DefensiveSpell)))
			{
				Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.PROTECTION_CANNOT_APPLY);
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public static void Toggle(Mobile caster, Mobile target)
		{
			ResistanceMod[] mods = (ResistanceMod[])m_Table[target];

			if (mods == null)
			{
				ActivateProtection(caster, target);
			}
			else
			{
				DeactivateProtection(caster, target);
			}
		}

		/// <summary>
		/// Creates resistance modifier array for Protection spell
		/// Each resistance gets a random penalty between -2 and -8 (Balance Nerf)
		/// </summary>
		private static ResistanceMod[] CreateResistanceMods()
		{
			return new ResistanceMod[5]
			{
				new ResistanceMod(ResistanceType.Physical, Utility.RandomMinMax(RESISTANCE_PENALTY_MIN, RESISTANCE_PENALTY_MAX)),
				new ResistanceMod(ResistanceType.Fire, Utility.RandomMinMax(RESISTANCE_PENALTY_MIN, RESISTANCE_PENALTY_MAX)),
				new ResistanceMod(ResistanceType.Cold, Utility.RandomMinMax(RESISTANCE_PENALTY_MIN, RESISTANCE_PENALTY_MAX)),
				new ResistanceMod(ResistanceType.Poison, Utility.RandomMinMax(RESISTANCE_PENALTY_MIN, RESISTANCE_PENALTY_MAX)),
				new ResistanceMod(ResistanceType.Energy, Utility.RandomMinMax(RESISTANCE_PENALTY_MIN, RESISTANCE_PENALTY_MAX))
			};
		}

		/// <summary>
		/// Activates Protection spell on target
		/// </summary>
		private static void ActivateProtection(Mobile caster, Mobile target)
		{
			target.PlaySound(SOUND_ADD);
			PlayEffects(caster, target);

			ResistanceMod[] mods = CreateResistanceMods();
			m_Table[target] = mods;

			// Apply all resistance modifications
			foreach (ResistanceMod mod in mods)
			{
				target.AddResistanceMod(mod);
			}

			// Register disruption protection (new tracking system)
			Registry[target] = new ProtectionEntry(target);

			// Create buff info display
			string args = FormatBuffArguments(mods);
			TimeSpan length = SpellHelper.NMSGetDuration(caster, target, true);
			BuffInfo.AddBuff(target, new BuffInfo(BuffIcon.Protection, 1075814, length, target, args));
		}

		/// <summary>
		/// Deactivates Protection spell from target
		/// </summary>
		private static void DeactivateProtection(Mobile caster, Mobile target)
		{
			ResistanceMod[] mods = (ResistanceMod[])m_Table[target];

			target.PlaySound(SOUND_REMOVE);
			PlayEffects(caster, target);

			m_Table.Remove(target);
			Registry.Remove(target); // Remove disruption protection tracking

			// Remove all resistance modifications
			foreach (ResistanceMod mod in mods)
			{
				target.RemoveResistanceMod(mod);
			}

			BuffInfo.RemoveBuff(target, BuffIcon.Protection);
		}

		/// <summary>
		/// Formats buff arguments for display showing actual resistance penalties
		/// </summary>
		private static string FormatBuffArguments(ResistanceMod[] mods)
		{
			return String.Format("{0}\t{1}\t{2}\t{3}\t{4}", 
				Math.Abs(mods[0].Offset), // Physical
				Math.Abs(mods[1].Offset), // Fire
				Math.Abs(mods[2].Offset), // Cold
				Math.Abs(mods[3].Offset), // Poison
				Math.Abs(mods[4].Offset)); // Energy
		}

		/// <summary>
		/// Plays visual effects for Protection spell
		/// </summary>
		private static void PlayEffects(Mobile caster, Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(caster, DEFAULT_HUE);
			target.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_DURATION, EFFECT_RENDER, hue, 0, EffectLayer.Waist);
		}

		public static void EndProtection(Mobile m)
		{
			if (m_Table.Contains(m))
			{
				object[] mods = (object[])m_Table[m];

				m_Table.Remove(m);
				Registry.Remove(m);

				m.RemoveResistanceMod((ResistanceMod)mods[0]);
				m.PlaySound(SOUND_REMOVE);
				BuffInfo.RemoveBuff(m, BuffIcon.Protection);
			}
		}

		public override void OnCast()
		{
			if (Core.AOS)
			{
				if (CheckSequence())
					Toggle(Caster, Caster);

				FinishSequence();
			}
			else
			{
				// Legacy mode
				if (m_Registry.ContainsKey(Caster))
				{
					Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.PROTECTION_ALREADY_ACTIVE);
				}
				else if (!Caster.CanBeginAction(typeof(DefensiveSpell)))
				{
					Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.PROTECTION_CANNOT_APPLY);
				}
				else if (CheckSequence())
				{
					if (Caster.BeginAction(typeof(DefensiveSpell)))
					{
						double value = (int)(Caster.Skills[SkillName.Inscribe].Value);
						value /= 4;

						if (value < 0)
							value = 0;
						else if (value > 30)
							value = 30.0;

						Registry.Add(Caster, value);
						new InternalTimer(Caster).Start();

						Caster.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_DURATION, EFFECT_RENDER, 
							Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE), 0, EffectLayer.Waist);
						Caster.PlaySound(SOUND_REMOVE);
					}
					else
					{
						Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.PROTECTION_CANNOT_APPLY);
					}
				}

				FinishSequence();
			}
		}

		/// <summary>
		/// Calculates protection duration for legacy mode
		/// </summary>
		private double CalculateLegacyDuration()
		{
			double duration = Caster.Skills[SkillName.Magery].Value * DURATION_MAGERY_MULTIPLIER;

			if (duration < MIN_DURATION_SECONDS)
				duration = MIN_DURATION_SECONDS;
			else if (duration > MAX_DURATION_SECONDS)
				duration = MAX_DURATION_SECONDS;

			return duration;
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Caster;

			public InternalTimer(Mobile caster) : base(TimeSpan.FromSeconds(0))
			{
				double duration = CalculateDuration(caster);
				m_Caster = caster;
				Delay = TimeSpan.FromSeconds(duration);
				Priority = TimerPriority.OneSecond;
			}

			/// <summary>
			/// Calculates duration for legacy Protection spell
			/// </summary>
			private double CalculateDuration(Mobile caster)
			{
				double duration = caster.Skills[SkillName.Magery].Value * DURATION_MAGERY_MULTIPLIER;

				if (duration < MIN_DURATION_SECONDS)
					duration = MIN_DURATION_SECONDS;
				else if (duration > MAX_DURATION_SECONDS)
					duration = MAX_DURATION_SECONDS;

				return duration;
			}

			protected override void OnTick()
			{
				ProtectionSpell.Registry.Remove(m_Caster);
				DefensiveSpell.Nullify(m_Caster);
			}
		}
	}
}
