using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Spells.Fifth
{
	/// <summary>
	/// Summon Creature - 5th Circle Summon Spell
	/// Summons a random animal to fight for the caster at a chosen location
	/// Duration based on Magery skill + Animal Lore bonus
	/// High Magery (80+) unlocks more powerful creatures
	/// Requires half or fewer followers than maximum
	/// Caster can target where to summon the creature
	/// Creature inherits caster's karma alignment
	/// LIMIT: Only 1 summoned creature per caster (new summon dismisses old one)
	/// </summary>
	public class SummonCreatureSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Summon Creature", "Kal Xen",
				16,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		#region Single Summon Tracking

		/// <summary>
		/// Tracks the currently active summoned creature for each caster
		/// Only one creature allowed per caster at a time
		/// </summary>
		private static Dictionary<Mobile, BaseCreature> m_ActiveCreatures = new Dictionary<Mobile, BaseCreature>();

		/// <summary>
		/// Registers a newly summoned creature and dismisses any existing one
		/// </summary>
		private static void RegisterCreature(Mobile caster, BaseCreature creature)
		{
			// Dismiss old creature if exists
			DismissExistingCreature(caster);

			// Register new creature
			m_ActiveCreatures[caster] = creature;
		}

		/// <summary>
		/// Dismisses the caster's existing summoned creature if present
		/// </summary>
		private static void DismissExistingCreature(Mobile caster)
		{
			BaseCreature existingCreature;
			if (m_ActiveCreatures.TryGetValue(caster, out existingCreature))
			{
			if (existingCreature != null && !existingCreature.Deleted)
			{
				existingCreature.Delete();
				caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_PREVIOUS_SUMMON_DISMISSED);
			}
				m_ActiveCreatures.Remove(caster);
			}
		}

		/// <summary>
		/// Unregisters a creature when it's deleted/expires
		/// Called from creature mobile or cleanup
		/// </summary>
		public static void UnregisterCreature(Mobile caster, BaseCreature creature)
		{
			BaseCreature registeredCreature;
			if (m_ActiveCreatures.TryGetValue(caster, out registeredCreature))
			{
				if (registeredCreature == creature)
				{
					m_ActiveCreatures.Remove(caster);
				}
			}
		}

		#endregion

		#region Creature Type Arrays

		/// <summary>
		/// Standard creatures - summoned with any Magery level
		/// List based on 1hr of summon/release testing on OSI
		/// Includes: common animals and wildlife
		/// </summary>
		private static Type[] m_Types = new Type[]
			{
				typeof(GrizzlyBear),
				typeof(BlackBear),
				typeof(Walrus),
				typeof(Chicken),
				typeof(GiantSerpent),
				typeof(Alligator),
				typeof(GreyWolf),
				typeof(Slime),
				typeof(Eagle),
				typeof(Gorilla),
				typeof(SnowLeopard),
				typeof(Pig),
				typeof(Hind),
				typeof(Rabbit),
				typeof(Dog),
				typeof(WildCat),
				typeof(Sheep)
			};

		/// <summary>
		/// Special creatures - summoned with 80+ Magery
		/// More powerful and useful than standard creatures
		/// Includes: rideable mounts and stronger combatants
		/// </summary>
		private static Type[] m_SpecialTypes = new Type[]
			{
				typeof(PolarBear),
				typeof(Horse),
				typeof(Scorpion),
				typeof(GiantSpider),
				typeof(DireWolf),
				typeof(DireBear),
				typeof(RidableLlama)
			};

		#endregion

		#region Constants

		// Duration calculation constants
		/// <summary>Duration multiplier for Magery skill (seconds per 0.1 skill)</summary>
		private const double DURATION_MULTIPLIER = 0.1;

		/// <summary>Animal Lore bonus multiplier (adds 0.25s per skill point)</summary>
		private const double ANIMAL_LORE_BENEFIT_MULTIPLIER = 0.25;

		/// <summary>Magery threshold to unlock special creatures</summary>
		private const double MAGERY_THRESHOLD_FOR_SPECIAL = 80.0;

		// Follower limit constants
		/// <summary>Follower limit divisor (must have half or fewer followers)</summary>
		private const int FOLLOWER_LIMIT_DIVISOR = 2;

		// Cast delay constants
		/// <summary>Cast delay multiplier for AOS system</summary>
		private const int CAST_DELAY_MULTIPLIER_AOS = 2;

		/// <summary>Additional cast delay for legacy system (seconds)</summary>
		private const double CAST_DELAY_LEGACY_SECONDS = 2.0;

		// Audio constants
		/// <summary>Summon sound effect ID</summary>
		private const int SUMMON_SOUND = 0x215;

		/// <summary>Female "oops" sound when spell fails</summary>
		private const int FEMALE_OOPS_SOUND = 812;

		/// <summary>Male "oops" sound when spell fails</summary>
		private const int MALE_OOPS_SOUND = 1086;

		// Failure messages
		/// <summary>Spoken message when spell fails</summary>
		private const string OOPS_MESSAGE = "*oops*";

		#endregion

		public SummonCreatureSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		/// <summary>
		/// Validates caster can summon a creature
		/// Checks: base cast requirements and follower limit (must have ≤ 50% followers)
		/// </summary>
		/// <param name="caster">Spell caster</param>
		/// <returns>True if cast can proceed</returns>
		public override bool CheckCast(Mobile caster)
		{
			if (!base.CheckCast(caster))
				return false;

			if (Caster.Followers > Caster.FollowersMax / FOLLOWER_LIMIT_DIVISOR)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TOO_MANY_FOLLOWERS);
				PlayFailureFeedback();
				return false;
			}

			return true;
		}

	public override void OnCast()
	{
		Caster.Target = new InternalTarget(this);
	}

	/// <summary>
	/// Attempts to summon a creature at the target location
	/// Creature will guard the caster and inherit their karma alignment
	/// </summary>
	/// <param name="p">Target location</param>
	public void Target(IPoint3D p)
	{
		Map map = Caster.Map;

		SpellHelper.GetSurfaceTop(ref p);

		if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
		{
			DoFizzle();
			Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCATION_BLOCKED);
			PlayFailureFeedback();
		}
		else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
		{
			try
			{
				BaseCreature creature = SelectCreatureType();
				TimeSpan duration = CalculateSummonDuration();

			// Set creature karma to match caster's alignment
			ApplyKarmaAlignment(creature);

			// Notify caster of duration
			Caster.SendMessage(
				Spell.MSG_COLOR_SYSTEM,
				String.Format(Spell.SpellMessages.INFO_SUMMON_DURATION_FORMAT, (int)duration.TotalSeconds)
			);

		// Summon the creature at the target location
		SpellHelper.Summon(creature, Caster, SUMMON_SOUND, duration, false, false);
		
		// Move creature to target location
		creature.MoveToWorld(new Point3D(p), Caster.Map);

		// Set creature to guard mode (actively protects and follows caster)
		//creature.ControlOrder = OrderType.Guard;

		// Register creature for single-summon tracking (dismisses old one)
		RegisterCreature(Caster, creature);
			}
			catch (Exception)
			{
				// Silently fail if creature instantiation fails
				// This prevents server crashes from missing creature types
			}
		}

		FinishSequence();
	}

		/// <summary>
		/// Selects and instantiates a random creature type based on caster's Magery
		/// 80+ Magery: Special creatures (stronger, rideable)
		/// Below 80 Magery: Standard creatures (common animals)
		/// </summary>
		/// <returns>Instantiated BaseCreature</returns>
		private BaseCreature SelectCreatureType()
		{
			// Check if caster qualifies for special creatures
			if (Caster.Skills.Magery.Value >= MAGERY_THRESHOLD_FOR_SPECIAL)
			{
				Type creatureType = m_SpecialTypes[Utility.Random(m_SpecialTypes.Length)];
				return (BaseCreature)Activator.CreateInstance(creatureType);
			}
			else
			{
				Type creatureType = m_Types[Utility.Random(m_Types.Length)];
				return (BaseCreature)Activator.CreateInstance(creatureType);
			}
		}

	/// <summary>
	/// Calculates summon duration based on caster's skills
	/// Formula: (Magery.Fixed * 0.1) + (AnimalLore * 0.25) seconds
	/// Animal Lore bonus only applies to PlayerMobiles
	/// </summary>
	/// <returns>TimeSpan duration for summoned creature</returns>
	private TimeSpan CalculateSummonDuration()
	{
		double baseDuration = Caster.Skills.Magery.Fixed * DURATION_MULTIPLIER;
		int animalLoreBonus = 0;

		// PlayerMobiles get Animal Lore bonus
		if (Caster is PlayerMobile)
		{
			animalLoreBonus = (int)(Caster.Skills[SkillName.AnimalLore].Value * ANIMAL_LORE_BENEFIT_MULTIPLIER);
		}

		return TimeSpan.FromSeconds(baseDuration + animalLoreBonus);
	}

	/// <summary>
	/// Sets creature's karma to match caster's alignment
	/// Good casters (positive karma) summon good creatures
	/// Evil casters (negative karma) summon evil creatures
	/// Neutral casters summon neutral creatures
	/// </summary>
	/// <param name="creature">The creature to apply karma to</param>
	private void ApplyKarmaAlignment(BaseCreature creature)
	{
		if (Caster.Karma > 0)
		{
			// Good alignment - positive karma
			creature.Karma = Caster.Karma;
		}
		else if (Caster.Karma < 0)
		{
			// Evil alignment - negative karma
			creature.Karma = Caster.Karma;
		}
		else
		{
			// Neutral alignment
			creature.Karma = 0;
		}
	}

		/// <summary>
		/// Plays audio and visual feedback when spell fails
		/// </summary>
		private void PlayFailureFeedback()
		{
			Caster.PlaySound(Caster.Female ? FEMALE_OOPS_SOUND : MALE_OOPS_SOUND);
			Caster.Say(OOPS_MESSAGE);
		}

	/// <summary>
	/// Override cast delay to make summon creature slower to cast
	/// AOS: 3x normal delay
	/// Legacy: base delay + 3 seconds
	/// </summary>
	/// <returns>Modified cast delay timespan</returns>
	public override TimeSpan GetCastDelay()
	{
		if (Core.AOS)
			return TimeSpan.FromTicks(base.GetCastDelay().Ticks * CAST_DELAY_MULTIPLIER_AOS);

		return base.GetCastDelay() + TimeSpan.FromSeconds(CAST_DELAY_LEGACY_SECONDS);
	}

	private class InternalTarget : Target
	{
		private SummonCreatureSpell m_Owner;

		public InternalTarget(SummonCreatureSpell owner) : base(SpellConstants.GetSpellRange(), true, TargetFlags.None)
		{
			m_Owner = owner;
		}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is IPoint3D)
				m_Owner.Target((IPoint3D)o);
		}

		/// <summary>
		/// Handles out of line-of-sight targeting
		/// Allows caster to retry the target
		/// </summary>
		/// <param name="from">Caster</param>
		/// <param name="o">Target object</param>
		protected override void OnTargetOutOfLOS(Mobile from, object o)
		{
			from.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			from.Target = new InternalTarget(m_Owner);
			from.Target.BeginTimeout(from, TimeoutTime - DateTime.UtcNow);
			m_Owner = null;
		}

		protected override void OnTargetFinish(Mobile from)
		{
			if (m_Owner != null)
				m_Owner.FinishSequence();
		}
	}
}
}
