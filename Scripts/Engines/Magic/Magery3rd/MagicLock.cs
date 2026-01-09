using System;
using Server;
using Server.Misc;
using Server.Targeting;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Felladrin.Automations;

namespace Server.Spells.Third
{
	/// <summary>
	/// Magic Lock - 3rd Circle Utility Spell
	/// Locks containers and doors, and enables soul trapping of creatures
	/// </summary>
	public class MagicLockSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Lock", "An Por",
				215,
				9001,
				Reagent.Garlic,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Effect Constants
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_RENDER = 32;
		private const int EFFECT_DURATION = 5020;
		private const int SOUND_ID = 0x1FA;
		private const int DEFAULT_HUE = 0;

		// Door Locking Constants
		private const int DOOR_LOCK_DURATION_MIN = 10; // Minimum duration in seconds
		private const int DOOR_LOCK_DURATION_MAX = 90; // Maximum duration in seconds
		private const int DOOR_LOCK_SKILL_MAX = 120; // Maximum skill level for calculation

		// Soul Trapping Requirements
		private const int SOUL_TRAP_MAGERY_MIN = 100;
		private const int SOUL_TRAP_EVAL_INT_MIN = 100;
		private const int SOUL_TRAP_INT_MIN = 100;
		private const int SOUL_TRAP_MANA_COST = 80;

		// Soul Trap Success Rates (Linear scaling)
		private const double SOUL_TRAP_SUCCESS_BASE = 5.0; // 5% base at skill 100
		private const double SOUL_TRAP_SUCCESS_MAX = 15.0; // 15% maximum
		private const double SOUL_TRAP_SUCCESS_MULTIPLIER = 0.5; // 0.5% per skill point above 100 (10% range over 20 skill points)
		private const int SOUL_TRAP_SKILL_BASE = 100; // Base skill level for calculations

		// Karma/Fame Penalties
		private const int HUMAN_SOUL_KILLS = 1;
		private const int FAME_AWARDED = 30;
		private const int KARMA_PENALTY = -100;

		// Stats Reduction
		private const double STATS_REDUCTION_MULTIPLIER = 0.8;

		// Flask Items
		private const int EMPTY_FLASK_ITEM_ID = 0x282E;
		private const int FILLED_FLASK_ITEM_ID = 0x282D;
		private const int FLASK_HUE = 2778;
		private const int EMPTY_FLASK_WEIGHT = 5;

		// Target Constants
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;

		// Locked Creature Constants
		private const int LOCKED_CREATURE_DELETE_DELAY = 10;
		private const int LOCKED_CREATURE_CONTROL_SLOTS = 3;
		private const int LOCKED_CREATURE_FAME = 0;
		private const int LOCKED_CREATURE_KARMA = 0;

		// Humanoid Body IDs
		private const int BODY_HUMAN_MALE = 400;
		private const int BODY_HUMAN_FEMALE = 401;
		private const int BODY_ELF_MALE = 605;
		private const int BODY_ELF_FEMALE = 606;
		private const int BODY_GHOST = 0x3CA;
		private const int SOUND_GHOST = 0x482;
		#endregion

		public MagicLockSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target(object target)
		{
			if (target is LockableContainer)
			{
				TryLockContainer((LockableContainer)target);
			}
			else if (target is BaseDoor)
			{
				TryLockDoor((BaseDoor)target);
			}
			else if (target is BaseCreature)
			{
				TryCaptureSoul((BaseCreature)target);
			}
			else if (target is PlayerMobile)
			{
				HandleSoulCaptureFailure("Esta alma é forte demais para ficar presa no frasco!");
			}
			else
			{
				// Invalid target
				Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, "Este feitiço não tem efeito sobre isso!");
				PlayErrorEmote();
			}

			FinishSequence();
		}

		/// <summary>
		/// Attempts to lock a container with magic
		/// </summary>
		/// <param name="container">The container to lock</param>
		private void TryLockContainer(LockableContainer container)
		{
			string failureReason;
			if (!CanLockContainer(container, out failureReason))
			{
				Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, failureReason);
				PlayErrorEmote();
				return;
			}

			if (!CheckSequence())
				return;

			SpellHelper.Turn(Caster, container);
			PlayLockEffects(container.GetWorldLocation());

			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, "O baú foi trancado com magia!");
			PlaySuccessEmote();

			int magerySkill = (int)Caster.Skills[SkillName.Magery].Value;
			container.LockLevel = magerySkill >= 75 ? 75 : magerySkill;
			if (container.LockLevel <= 0) container.LockLevel = 0;
			container.RequiredSkill = container.LockLevel;
			container.MaxLockLevel = 120;
			container.Locked = true;
		}

		/// <summary>
		/// Validates if a container can be locked
		/// </summary>
		/// <param name="container">The container to validate</param>
		/// <param name="failureReason">Output parameter for failure reason</param>
		/// <returns>True if container can be locked, false otherwise</returns>
		private bool CanLockContainer(LockableContainer container, out string failureReason)
		{
			failureReason = null;

			if (Multis.BaseHouse.CheckLockedDownOrSecured(container))
			{
				failureReason = "Você não pode lançar um feitiço em um baú seguro!";
				return false;
			}

			if (container.Locked)
			{
				failureReason = "Você não pode lançar um feitiço em um baú já trancado.";
				return false;
			}

			if (container is ParagonChest || container is TreasureMapChest || container is PirateChest)
			{
				failureReason = "Você não pode lançar um feitiço em um baú já trancado.";
				return false;
			}

			return true;
		}

		/// <summary>
		/// Attempts to lock a dungeon door
		/// </summary>
		/// <param name="door">The door to lock</param>
		private void TryLockDoor(BaseDoor door)
		{
			string failureReason;
			if (!CanLockDoor(door, out failureReason))
			{
				Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, failureReason);
				PlayErrorEmote();
				return;
			}

			SpellHelper.Turn(Caster, door);
			PlayLockEffects(door.GetWorldLocation());

			// Calculate and display duration
			double duration = CalculateDoorLockDuration(Caster);
			int durationSeconds = (int)Math.Round(duration);
			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, String.Format("Essa porta agora foi trancada com magia! A porta ficará trancada por aproximadamente {0} segundo{1}.", durationSeconds, durationSeconds != 1 ? "s" : ""));

			door.Locked = true;
			Server.Items.DoorType.LockDoors(door);

			new InternalTimer(door, Caster, this).Start();
		}

		/// <summary>
		/// Calculates the door lock duration based on caster's Magery skill
		/// Linear progression: 10 seconds at skill 0, 90 seconds at skill 120
		/// </summary>
		/// <param name="caster">The caster to calculate duration for</param>
		/// <returns>Duration in seconds (10-90)</returns>
		private double CalculateDoorLockDuration(Mobile caster)
		{
			double magery = caster.Skills[SkillName.Magery].Value;
			double duration = DOOR_LOCK_DURATION_MIN + (magery * (DOOR_LOCK_DURATION_MAX - DOOR_LOCK_DURATION_MIN)) / DOOR_LOCK_SKILL_MAX;
			
			// Clamp between min and max
			if (duration < DOOR_LOCK_DURATION_MIN)
				duration = DOOR_LOCK_DURATION_MIN;
			else if (duration > DOOR_LOCK_DURATION_MAX)
				duration = DOOR_LOCK_DURATION_MAX;

			return duration;
		}

		/// <summary>
		/// Validates if a door can be locked
		/// </summary>
		/// <param name="door">The door to validate</param>
		/// <param name="failureReason">Output parameter for failure reason</param>
		/// <returns>True if door can be locked, false otherwise</returns>
		private bool CanLockDoor(BaseDoor door, out string failureReason)
		{
			failureReason = null;

			if (!Server.Items.DoorType.IsDungeonDoor(door))
			{
				failureReason = "Este feitiço não tem efeito sobre essa porta!";
				return false;
			}

			if (door.Locked)
			{
				failureReason = "Essa porta já está trancada!";
				return false;
			}

			return true;
		}

		/// <summary>
		/// Attempts to capture a creature's soul
		/// </summary>
		/// <param name="creature">The creature to capture</param>
		private void TryCaptureSoul(BaseCreature creature)
		{
			string failureReason;
			if (!CanCaptureSoul(creature, out failureReason))
			{
				if (failureReason != null)
				{
					HandleSoulCaptureFailure(failureReason);
				}
				return;
			}

			// Check for epic creatures that fight back
			if (IsEpicCreature(creature))
			{
				HandleEpicCreatureReaction(creature);
				return;
			}

			PerformSoulCapture(creature);
		}

		/// <summary>
		/// Validates if a creature's soul can be captured
		/// </summary>
		/// <param name="creature">The creature to validate</param>
		/// <param name="failureReason">Output parameter for failure reason</param>
		/// <returns>True if creature can be captured, false otherwise</returns>
		private bool CanCaptureSoul(BaseCreature creature, out string failureReason)
		{
			failureReason = null;

			if (Caster.Backpack.FindItemByType(typeof(ElectrumFlask)) == null)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, "Você precisa de um frasco de electrum vazio!");
				return false;
			}

			if (!creature.Alive)
			{
				failureReason = "Você não pode capturar algo que está morto!";
				return false;
			}

			if (creature is LockedCreature)
			{
				failureReason = "Este ser já foi preso uma vez e não se deixará ser subjugado novamente!";
				return false;
			}

			if (creature.Controlled)
			{
				failureReason = "Este ser já está sob o controle de outro!";
				return false;
			}

			if (IsProtectedCreature(creature))
			{
				failureReason = "Este ser é protegido por uma aura misteriosa.";
				return false;
			}

			if (creature.EmoteHue == 505 || creature.ControlSlots >= 100)
			{
				failureReason = "Você não é capaz o suficiente para captura-lo!";
				return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if a creature is protected from soul capture
		/// </summary>
		/// <param name="creature">The creature to check</param>
		/// <returns>True if creature is protected, false otherwise</returns>
		private bool IsProtectedCreature(BaseCreature creature)
		{
			if (creature.Blessed)
				return true;

			if (creature is CloneCharacterOnLogout.CharacterClone)
				return true;

			if (creature is BaseVendor)
			{
				BaseVendor vendor = (BaseVendor)creature;
				if (vendor.IsInvulnerable)
					return true;
			}

			if (creature.IsHitchStabled)
				return true;

			return false;
		}

		/// <summary>
		/// Checks if a creature is an epic creature that fights back
		/// </summary>
		/// <param name="creature">The creature to check</param>
		/// <returns>True if creature is epic, false otherwise</returns>
		private bool IsEpicCreature(BaseCreature creature)
		{
			if (creature is EpicCharacter)
				return true;

			if (creature is TimeLord)
				return true;

			if (creature is TownGuards)
				return true;

			return false;
		}

		/// <summary>
		/// Handles epic creature reaction to soul capture attempt
		/// </summary>
		/// <param name="creature">The epic creature</param>
		private void HandleEpicCreatureReaction(BaseCreature creature)
		{
			creature.Say("Você acha que pode capturar minha alma?");
			creature.Say("Deixe-me mostrar o que posso fazer com você, seu inseto!");
			Caster.PlaySound(Caster.Female ? 799 : 1071);
			Caster.Say("*huh?*");
			HandleSoulCaptureFailure("Você sente uma força poderosa bater em você!");

			Timer.DelayCall(TimeSpan.FromSeconds(2), () => Caster.Kill());
		}

		/// <summary>
		/// Performs the actual soul capture logic
		/// </summary>
		/// <param name="creature">The creature to capture</param>
		private void PerformSoulCapture(BaseCreature creature)
		{
			// Cache skill values for performance
			int playerMagery = (int)Caster.Skills[SkillName.Magery].Value;
			int playerEval = (int)Caster.Skills[SkillName.EvalInt].Value;
			int playerInt = (int)Caster.RawInt;

			// Cache flask lookup
			Item flask = Caster.Backpack.FindItemByType(typeof(ElectrumFlask));

			Caster.CriminalAction(true); // This is a criminal action

			if (!MeetsSkillRequirements(playerMagery, playerEval, playerInt))
			{
				HandleSoulCaptureFailure("Você não possui habilidades o suficiente para capturar essa alma.");
				return;
			}

			if (Caster.Mana <= SOUL_TRAP_MANA_COST)
			{
				HandleSoulCaptureFailure("Você não possui mana o suficiente!");
				return;
			}

			// Calculate linear success chance based on skills
			double successPercentage = CalculateSoulCaptureSuccessRate(playerMagery, playerEval);
			int random = Utility.RandomMinMax(1, 100);

			if (successPercentage >= random)
			{
				ExecuteSuccessfulSoulCapture(creature, flask);
			}
			else
			{
				HandleSoulCaptureFailure("Você falha na tentativa de capturar a alma deste ser!");
			}
		}

		/// <summary>
		/// Checks if caster meets minimum skill requirements for soul capture
		/// </summary>
		/// <param name="magery">Caster's Magery skill</param>
		/// <param name="evalInt">Caster's EvalInt skill</param>
		/// <param name="intelligence">Caster's Intelligence stat</param>
		/// <returns>True if requirements are met, false otherwise</returns>
		private bool MeetsSkillRequirements(int magery, int evalInt, int intelligence)
		{
			return magery >= SOUL_TRAP_MAGERY_MIN && 
			       evalInt >= SOUL_TRAP_EVAL_INT_MIN && 
			       intelligence >= SOUL_TRAP_INT_MIN;
		}

		/// <summary>
		/// Calculates linear success rate for soul capture based on Magery and EvalInt
		/// Formula: Base (5%) + (average skill - 100) * 0.5%, capped at 15%
		/// Examples: Skill 100 = 5%, Skill 110 = 10%, Skill 120+ = 15%
		/// </summary>
		/// <param name="magery">Caster's Magery skill</param>
		/// <param name="evalInt">Caster's EvalInt skill</param>
		/// <returns>Success percentage (5.0 to 15.0)</returns>
		private double CalculateSoulCaptureSuccessRate(int magery, int evalInt)
		{
			double averageSkill = (magery + evalInt) / 2.0;
			double skillBonus = (averageSkill - SOUL_TRAP_SKILL_BASE) * SOUL_TRAP_SUCCESS_MULTIPLIER;
			double successRate = SOUL_TRAP_SUCCESS_BASE + skillBonus;

			// Clamp between base and max
			if (successRate < SOUL_TRAP_SUCCESS_BASE)
				successRate = SOUL_TRAP_SUCCESS_BASE;
			if (successRate > SOUL_TRAP_SUCCESS_MAX)
				successRate = SOUL_TRAP_SUCCESS_MAX;

			return successRate;
		}

		/// <summary>
		/// Executes successful soul capture sequence
		/// </summary>
		/// <param name="creature">The creature being captured</param>
		/// <param name="flask">The empty flask to consume</param>
		private void ExecuteSuccessfulSoulCapture(BaseCreature creature, Item flask)
		{
			// Award fame
			Misc.Titles.AwardFame(Caster, FAME_AWARDED, true);

			// Consume empty flask
			if (flask != null)
				flask.Consume();

			// Handle human soul capture (murder)
			if (IsHumanoidBody(creature.Body))
			{
				Caster.Kills += HUMAN_SOUL_KILLS;
				Caster.SendMessage(Spell.MSG_COLOR_WARNING, "Aprisionar uma alma humana significa assassinato!");
				Misc.Titles.AwardKarma(Caster, KARMA_PENALTY, true);
			}

			// Create the captured soul flask
			ElectrumFlaskFilled flaskWithSoul = CreateCapturedSoulInAFlask(creature);

			// Effects and cleanup
			creature.BoltEffect(0);
			creature.PlaySound(0x665);
			creature.Delete();

			Caster.BoltEffect(0);
			Caster.PlaySound(0x665);
			Caster.Mana -= SOUL_TRAP_MANA_COST;
			Caster.AddToBackpack(flaskWithSoul);
			Caster.SendMessage(Spell.MSG_COLOR_ERROR, String.Format("Você capturou a alma de {0} em um frasco de electrum!", creature.Name));
		}

		/// <summary>
		/// Handles soul capture failure
		/// </summary>
		/// <param name="message">The failure message</param>
		private void HandleSoulCaptureFailure(string message)
		{
			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, message);
			Server.Misc.IntelligentAction.FizzleSpell(Caster);
		}

		/// <summary>
		/// Plays the lock spell effects
		/// </summary>
		/// <param name="location">Location to play effects</param>
		private void PlayLockEffects(IPoint3D location)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
			Effects.SendLocationParticles(EffectItem.Create(new Point3D(location), Caster.Map, EffectItem.DefaultDuration),
				EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_DURATION, 0);
			Effects.PlaySound(location, Caster.Map, SOUND_ID);
		}

		/// <summary>
		/// Plays error emote with sound
		/// </summary>
		private void PlayErrorEmote()
		{
			Caster.PlaySound(Caster.Female ? 812 : 1086);
			Caster.Say("*oops*");
		}

		/// <summary>
		/// Plays success emote with sound
		/// </summary>
		private void PlaySuccessEmote()
		{
			Caster.PlaySound(Caster.Female ? 779 : 1050);
			Caster.Say("*ah ha!*");
		}

		/// <summary>
		/// Creates a flask containing the captured soul
		/// </summary>
		/// <param name="creature">The creature whose soul was captured</param>
		/// <returns>The filled flask item</returns>
		private ElectrumFlaskFilled CreateCapturedSoulInAFlask(BaseCreature creature)
		{
			int creatureLevel = Server.Misc.IntelligentAction.GetCreatureLevel(creature);
			ElectrumFlaskFilled flask = new ElectrumFlaskFilled();

			// Set basic properties
			flask.TrappedName = creature.Name;
			flask.TrappedTitle = creature.Title;
			flask.TrappedHue = FLASK_HUE;
			flask.TrappedSkills = creatureLevel;
			flask.TrappedAI = (creature.AI == AIType.AI_Mage) ? 1 : 2;
			flask.TrappedPoison = GetPoisonLevel(creature.HitPoison);
			flask.TrappedImmune = GetPoisonLevel(creature.PoisonImmune);
			flask.TrappedCanSwim = creature.CanSwim;
			flask.TrappedCantWalk = creature.CantWalk;

			// Set sound properties
			flask.TrappedAngerSound = creature.GetAngerSound();
			flask.TrappedIdleSound = creature.GetIdleSound();
			flask.TrappedDeathSound = creature.GetDeathSound();
			flask.TrappedAttackSound = creature.GetAttackSound();
			flask.TrappedHurtSound = creature.GetHurtSound();

			// Set reduced stats (80% of original)
			SetReducedStats(flask, creature);

			// Handle body and sound (humanoids become ghosts)
			if (IsHumanoidBody(creature.Body))
			{
				flask.TrappedBody = BODY_GHOST;
				flask.TrappedBaseSoundID = SOUND_GHOST;
			}
			else
			{
				flask.TrappedBody = creature.Body;
				flask.TrappedBaseSoundID = creature.BaseSoundID;
			}

			return flask;
		}

		/// <summary>
		/// Converts Poison enum to numeric level
		/// </summary>
		/// <param name="poison">The poison to convert</param>
		/// <returns>Poison level (0-5, 0 = none)</returns>
		private int GetPoisonLevel(Poison poison)
		{
			if (poison == Poison.Lesser) return 1;
			if (poison == Poison.Regular) return 2;
			if (poison == Poison.Greater) return 3;
			if (poison == Poison.Deadly) return 4;
			if (poison == Poison.Lethal) return 5;
			return 0;
		}

		/// <summary>
		/// Checks if a body ID represents a humanoid
		/// </summary>
		/// <param name="bodyID">The body ID to check</param>
		/// <returns>True if humanoid, false otherwise</returns>
		private bool IsHumanoidBody(int bodyID)
		{
			return bodyID == BODY_HUMAN_MALE || 
			       bodyID == BODY_HUMAN_FEMALE || 
			       bodyID == BODY_ELF_MALE || 
			       bodyID == BODY_ELF_FEMALE;
		}

		/// <summary>
		/// Sets reduced stats on flask (80% of creature's original stats)
		/// </summary>
		/// <param name="flask">The flask to set stats on</param>
		/// <param name="creature">The creature to get stats from</param>
		private void SetReducedStats(ElectrumFlaskFilled flask, BaseCreature creature)
		{
			flask.TrappedStr = (int)Math.Round(creature.RawStr * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedDex = (int)Math.Round(creature.RawDex * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedInt = (int)Math.Round(creature.RawInt * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedHits = (int)Math.Round(creature.HitsMax * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedStam = (int)Math.Round(creature.StamMax * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedMana = (int)Math.Round(creature.ManaMax * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedDmgMin = (int)Math.Round(creature.DamageMin * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedDmgMax = (int)Math.Round(creature.DamageMax * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedColdDmg = (int)Math.Round(creature.ColdDamage * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedEnergyDmg = (int)Math.Round(creature.EnergyDamage * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedFireDmg = (int)Math.Round(creature.FireDamage * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedPhysicalDmg = (int)Math.Round(creature.PhysicalDamage * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedPoisonDmg = (int)Math.Round(creature.PoisonDamage * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedColdRst = (int)Math.Round(creature.ColdResistSeed * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedEnergyRst = (int)Math.Round(creature.EnergyResistSeed * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedFireRst = (int)Math.Round(creature.FireResistSeed * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedPhysicalRst = (int)Math.Round(creature.PhysicalResistanceSeed * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedPoisonRst = (int)Math.Round(creature.PoisonResistSeed * STATS_REDUCTION_MULTIPLIER);
			flask.TrappedVirtualArmor = (int)Math.Round(creature.VirtualArmor * STATS_REDUCTION_MULTIPLIER);
		}


		private class InternalTarget : Target
		{
			private MagicLockSpell m_Owner;

			public InternalTarget( MagicLockSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				m_Owner.Target( o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		private class InternalTimer : Timer
		{
			private BaseDoor m_Door;

			public InternalTimer( BaseDoor door, Mobile caster, MagicLockSpell spell ) : base( TimeSpan.FromSeconds( 0 ) )
			{
				m_Door = door;
				
				// Calculate duration using the same method as the spell
				double duration = spell.CalculateDoorLockDuration(caster);
				
				Delay = TimeSpan.FromSeconds( duration );
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( m_Door.Locked == true )
				{
					m_Door.Locked = false;
					Server.Items.DoorType.UnlockDoors( m_Door );
					Effects.PlaySound( m_Door.Location, m_Door.Map, 0x3E4 );
				}
			}
		}
	}
}

namespace Server.Items
{
	public class ElectrumFlask : Item
	{
		[Constructable]
		public ElectrumFlask() : base( 0x282E )
		{
			Name = "frasco de electrum";
			Weight = 5.0;
            Hue = 2822;//2369
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage(55, "Esse frasco está vazio." );
			}
		}

		public ElectrumFlask(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
		}
	}


	public class ElectrumFlaskFilled : Item
	{
		[Constructable]
		public ElectrumFlaskFilled() : base( 0x282D )
		{
			Name = "frasco de electrum";
			Weight = 5.0;
			Hue = 2778;//2369
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			string trapped;
			string prisoner;

			if ( TrappedBody > 0 )
			{
				trapped = "Contém uma alma presa";
				list.Add( 1070722, trapped );

				prisoner = TrappedName;
					if ( TrappedTitle != "" && TrappedTitle != null ){ prisoner = TrappedName + " " + TrappedTitle; }
			
				list.Add( 1049644, prisoner );
			}
        }

		public override void OnDoubleClick( Mobile from )
		{
		int nFollowers = from.FollowersMax - from.Followers;
		const int TRAPPED_SOUL_CONTROL_SLOTS = 4;
		const int MAX_TRAPPED_SOULS = 3;

			// Count existing trapped souls
			int existingTrappedSouls = 0;
			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;
				foreach (Mobile follower in pm.AllFollowers)
				{
					if (follower is LockedCreature)
					{
						existingTrappedSouls++;
					}
				}
			}

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage(95, "Isso deve estar em sua mochila para usar.");
			}
			else if ( existingTrappedSouls >= MAX_TRAPPED_SOULS )
			{
				from.SendMessage(55, String.Format("Você já está controlando o máximo de {0} almas presas!", MAX_TRAPPED_SOULS));
			}
			else if ( nFollowers < TRAPPED_SOUL_CONTROL_SLOTS )
			{
				from.SendMessage(55, String.Format("Você precisa de pelo menos {0} slots de controle livres para liberar uma alma presa!", TRAPPED_SOUL_CONTROL_SLOTS));
			}
			else if ( HenchmanFunctions.IsInRestRegion( from ) == false )
			{
				Map map = from.Map;

				int magery = (int)(from.Skills[SkillName.Magery].Value);

				BaseCreature prisoner = new LockedCreature( this.TrappedAI, this.TrappedSkills, magery, this.TrappedHits, this.TrappedStam, this.TrappedMana, this.TrappedStr, this.TrappedDex, this.TrappedInt, this.TrappedPoison, this.TrappedImmune, this.TrappedAngerSound, this.TrappedIdleSound, this.TrappedDeathSound, this.TrappedAttackSound, this.TrappedHurtSound );

				bool validLocation = false;
				Point3D loc = from.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				prisoner.ControlMaster = from;
				prisoner.Controlled = true;
				prisoner.ControlOrder = OrderType.Come;

				prisoner.Name = this.TrappedName;
				prisoner.Title = this.TrappedTitle;
				prisoner.Body = this.TrappedBody;
				prisoner.BaseSoundID = this.TrappedBaseSoundID;
				prisoner.Hue = this.TrappedHue;
				prisoner.AI = AIType.AI_Mage; if ( this.TrappedAI == 2 ){ prisoner.AI = AIType.AI_Melee; }
				prisoner.DamageMin = this.TrappedDmgMin;
				prisoner.DamageMax = this.TrappedDmgMax;
				prisoner.ColdDamage = this.TrappedColdDmg;
				prisoner.EnergyDamage = this.TrappedEnergyDmg;
				prisoner.FireDamage = this.TrappedFireDmg;
				prisoner.PhysicalDamage = this.TrappedPhysicalDmg;
				prisoner.PoisonDamage = this.TrappedPoisonDmg;
				prisoner.ColdResistSeed = this.TrappedColdRst;
				prisoner.EnergyResistSeed = this.TrappedEnergyRst;
				prisoner.FireResistSeed = this.TrappedFireRst;
				prisoner.PhysicalResistanceSeed = this.TrappedPhysicalRst;
				prisoner.PoisonResistSeed = this.TrappedPoisonRst;
				prisoner.VirtualArmor = this.TrappedVirtualArmor;
				prisoner.CanSwim = this.TrappedCanSwim;
				prisoner.CantWalk = this.TrappedCantWalk;

				from.BoltEffect( 0 );
				from.PlaySound(0x665);
				from.PlaySound(0x03E);
				prisoner.MoveToWorld( loc, map );
				from.SendMessage(95, "Você quebra o frasco e libera " + prisoner.Name + "!" );
				this.Delete();
			}
			else
			{
				from.SendMessage(55, "Você não acha que seria uma boa ideia fazer isso aqui.");
			}
		}

		public ElectrumFlaskFilled(Serial serial) : base(serial)
		{
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( TrappedName );
			writer.Write( TrappedTitle );
			writer.Write( TrappedBody );
			writer.Write( TrappedBaseSoundID );
			writer.Write( TrappedHue );
			writer.Write( TrappedAI );
			writer.Write( TrappedStr );
			writer.Write( TrappedDex );
			writer.Write( TrappedInt );
			writer.Write( TrappedHits );
			writer.Write( TrappedStam );
			writer.Write( TrappedMana );
			writer.Write( TrappedDmgMin );
			writer.Write( TrappedDmgMax );
			writer.Write( TrappedColdDmg );
			writer.Write( TrappedEnergyDmg );
			writer.Write( TrappedFireDmg );
			writer.Write( TrappedPhysicalDmg );
			writer.Write( TrappedPoisonDmg );
			writer.Write( TrappedColdRst );
			writer.Write( TrappedEnergyRst );
			writer.Write( TrappedFireRst );
			writer.Write( TrappedPhysicalRst );
			writer.Write( TrappedPoisonRst );
			writer.Write( TrappedVirtualArmor );
			writer.Write( TrappedCanSwim );
			writer.Write( TrappedCantWalk );
			writer.Write( TrappedSkills );
			writer.Write( TrappedPoison );
			writer.Write( TrappedImmune );
			writer.Write( TrappedAngerSound );
			writer.Write( TrappedIdleSound );
			writer.Write( TrappedDeathSound );
			writer.Write( TrappedAttackSound );
			writer.Write( TrappedHurtSound );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
			TrappedName = reader.ReadString();
			TrappedTitle = reader.ReadString();
			TrappedBody = reader.ReadInt();
			TrappedBaseSoundID = reader.ReadInt();
			TrappedHue = reader.ReadInt();
			TrappedAI = reader.ReadInt();
			TrappedStr = reader.ReadInt();
			TrappedDex = reader.ReadInt();
			TrappedInt = reader.ReadInt();
			TrappedHits = reader.ReadInt();
			TrappedStam = reader.ReadInt();
			TrappedMana = reader.ReadInt();
			TrappedDmgMin = reader.ReadInt();
			TrappedDmgMax = reader.ReadInt();
			TrappedColdDmg = reader.ReadInt();
			TrappedEnergyDmg = reader.ReadInt();
			TrappedFireDmg = reader.ReadInt();
			TrappedPhysicalDmg = reader.ReadInt();
			TrappedPoisonDmg = reader.ReadInt();
			TrappedColdRst = reader.ReadInt();
			TrappedEnergyRst = reader.ReadInt();
			TrappedFireRst = reader.ReadInt();
			TrappedPhysicalRst = reader.ReadInt();
			TrappedPoisonRst = reader.ReadInt();
			TrappedVirtualArmor = reader.ReadInt();
			TrappedCanSwim = reader.ReadBool();
			TrappedCantWalk = reader.ReadBool();
			TrappedSkills = reader.ReadInt();
			TrappedPoison = reader.ReadInt();
			TrappedImmune = reader.ReadInt();
			TrappedAngerSound = reader.ReadInt();
			TrappedIdleSound = reader.ReadInt();
			TrappedDeathSound = reader.ReadInt();
			TrappedAttackSound = reader.ReadInt();
			TrappedHurtSound = reader.ReadInt();
		}

		public string TrappedName;
		public string TrappedTitle;
		public int TrappedBody;
		public int TrappedBaseSoundID;
		public int TrappedHue;
		public int TrappedAI; // 1 Mage, 2 Fighter
		public int TrappedStr;
		public int TrappedDex;
		public int TrappedInt;
		public int TrappedHits;
		public int TrappedStam;
		public int TrappedMana;
		public int TrappedDmgMin;
		public int TrappedDmgMax;
		public int TrappedColdDmg;
		public int TrappedEnergyDmg;
		public int TrappedFireDmg;
		public int TrappedPhysicalDmg;
		public int TrappedPoisonDmg;
		public int TrappedColdRst;
		public int TrappedEnergyRst;
		public int TrappedFireRst;
		public int TrappedPhysicalRst;
		public int TrappedPoisonRst;
		public int TrappedVirtualArmor;
		public bool TrappedCanSwim;
		public bool TrappedCantWalk;
		public int TrappedSkills;
		public int TrappedPoison;
		public int TrappedImmune;
		public int TrappedAngerSound;
		public int TrappedIdleSound;
		public int TrappedDeathSound;
		public int TrappedAttackSound;
		public int TrappedHurtSound;

		[CommandProperty(AccessLevel.Owner)]
		public string Trapped_Name { get { return TrappedName; } set { TrappedName = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Trapped_Title { get { return TrappedTitle; } set { TrappedTitle = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Body { get { return TrappedBody; } set { TrappedBody = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_BaseSoundID { get { return TrappedBaseSoundID; } set { TrappedBaseSoundID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Hue { get { return TrappedHue; } set { TrappedHue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_AI { get { return TrappedAI; } set { TrappedAI = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Str { get { return TrappedStr; } set { TrappedStr = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Dex { get { return TrappedDex; } set { TrappedDex = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Int { get { return TrappedInt; } set { TrappedInt = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Hits { get { return TrappedHits; } set { TrappedHits = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Stam { get { return TrappedStam; } set { TrappedStam = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Mana { get { return TrappedMana; } set { TrappedMana = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_DmgMin { get { return TrappedDmgMin; } set { TrappedDmgMin = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_DmgMax { get { return TrappedDmgMax; } set { TrappedDmgMax = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_ColdDmg { get { return TrappedColdDmg; } set { TrappedColdDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_EnergyDmg { get { return TrappedEnergyDmg; } set { TrappedEnergyDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_FireDmg { get { return TrappedFireDmg; } set { TrappedFireDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PhysicalDmg { get { return TrappedPhysicalDmg; } set { TrappedPhysicalDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PoisonDmg { get { return TrappedPoisonDmg; } set { TrappedPoisonDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_ColdRst { get { return TrappedColdRst; } set { TrappedColdRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_EnergyRst { get { return TrappedEnergyRst; } set { TrappedEnergyRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_FireRst { get { return TrappedFireRst; } set { TrappedFireRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PhysicalRst { get { return TrappedPhysicalRst; } set { TrappedPhysicalRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PoisonRst { get { return TrappedPoisonRst; } set { TrappedPoisonRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_VirtualArmor { get { return TrappedVirtualArmor; } set { TrappedVirtualArmor = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public bool Trapped_CanSwim { get { return TrappedCanSwim; } set { TrappedCanSwim = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public bool Trapped_CantWalk { get { return TrappedCantWalk; } set { TrappedCantWalk = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Skills { get { return TrappedSkills; } set { TrappedSkills = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Poison { get { return TrappedPoison; } set { TrappedPoison = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Immune { get { return TrappedImmune; } set { TrappedImmune = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_AngerSound { get { return TrappedAngerSound; } set { TrappedAngerSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_IdleSound { get { return TrappedIdleSound; } set { TrappedIdleSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_DeathSound { get { return TrappedDeathSound; } set { TrappedDeathSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_AttackSound { get { return TrappedAttackSound; } set { TrappedAttackSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_HurtSound { get { return TrappedHurtSound; } set { TrappedHurtSound = value; InvalidateProperties(); } }
	}
}

namespace Server.Mobiles
{
	[CorpseName( "corpo espiritual" )]
	public class LockedCreature : BaseCreature
	{
		public int BCPoison;
		public int BCImmune;
		public int BCAngerSound;
		public int BCIdleSound;
		public int BCDeathSound;
		public int BCAttackSound;
		public int BCHurtSound;

		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public LockedCreature( int job, int skills, int time, int maxhits, int maxstam, int maxmana, int str, int dex, int iq, int poison, int immune, int anger, int idle, int death, int attack, int hurt ): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			BCPoison = poison+0;
			BCImmune = immune+0;
			BCAngerSound = anger+0;
			BCIdleSound = idle+0;
			BCDeathSound = death+0;
		BCAttackSound = attack+0;
		BCHurtSound = hurt+0;

		double duration = (double)(10+(3*time)) / 2.0;
		Timer.DelayCall( TimeSpan.FromSeconds( Math.Max(10.0, duration) ), new TimerCallback( Delete ) );

			Name = "um prisioneiro espiritual";
			Body = 2;

			SetStr( str );
			SetDex( dex);
			SetInt( iq );

			SetHits( maxhits );
			SetStam( maxstam );
			SetMana( maxmana );

			if ( job == 1 )
			{
				SetSkill( SkillName.EvalInt, (double)skills );
				SetSkill( SkillName.Magery, (double)skills );
				SetSkill( SkillName.Meditation, (double)skills );
				SetSkill( SkillName.MagicResist, (double)skills );
				SetSkill( SkillName.Wrestling, (double)skills );
			}
			else
			{
				SetSkill( SkillName.Anatomy, (double)skills );
				SetSkill( SkillName.MagicResist, (double)skills );
				SetSkill( SkillName.Tactics, (double)skills );
				SetSkill( SkillName.Wrestling, (double)skills );
			}

		Fame = 0;
		Karma = 0;

		ControlSlots = 4; // 4 slots per soul allows max 3 souls (requires 12 FollowersMax for max)
		}

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

        public override int GetIdleSound(){ return BCIdleSound; }
        public override int GetAngerSound(){ return BCAngerSound; }
        public override int GetHurtSound(){ return BCHurtSound; }
        public override int GetDeathSound(){ return BCDeathSound; }
        public override int GetAttackSound(){ return BCAttackSound; }

		public override Poison PoisonImmune
		{
			get
			{
				if ( BCImmune == 1 ){ return Poison.Lesser; }
				else if ( BCImmune == 2 ){ return Poison.Regular; }
				else if ( BCImmune == 3 ){ return Poison.Greater; }
				else if ( BCImmune == 4 ){ return Poison.Deadly; }
				else if ( BCImmune == 5 ){ return Poison.Lethal; }

				return null;
			}
		}

		public override Poison HitPoison
		{
			get
			{
				if ( BCPoison == 1 ){ return Poison.Lesser; }
				else if ( BCPoison == 2 ){ return Poison.Regular; }
				else if ( BCPoison == 3 ){ return Poison.Greater; }
				else if ( BCPoison == 4 ){ return Poison.Deadly; }
				else if ( BCPoison == 5 ){ return Poison.Lethal; }

				return null;
			}
		}

		public LockedCreature( Serial serial ): base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            //Caster.SendMessage(55, "Seu prisioneiro espiritual sumiu do mundo fisico!");
            Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerCallback( Delete ) );
		}
	}
}