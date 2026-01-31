using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Third
{
	/// <summary>
	/// Unlock Spell - 3rd Circle Utility Spell
	/// Unlocks doors and containers using magical power
	/// </summary>
	public class UnlockSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Unlock Spell", "Ex Por",
				215,
				9001,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Effect Constants
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_RENDER = 32;
		private const int EFFECT_DURATION = 5024;
		private const int SOUND_ID = 0x1FF;
		private const int DEFAULT_HUE = 0;

		// Door Unlocking Constants
		private const int DOOR_KEY_VALUE_THRESHOLD = 65;
		private const int DOOR_KEY_VALUE_MAX = 100;
		private const int DOOR_SUCCESS_SOUND_FEMALE = 779;
		private const int DOOR_SUCCESS_SOUND_MALE = 1050;
		private const int DOOR_FAIL_SOUND_FEMALE = 811;
		private const int DOOR_FAIL_SOUND_MALE = 1085;

		// Container Unlocking Constants
		private const int CONTAINER_SKILL_BONUS = 20;
		private const int CONTAINER_SUCCESS_SOUND_FEMALE = 783;
		private const int CONTAINER_SUCCESS_SOUND_MALE = 1054;
		private const int CONTAINER_FAIL_SOUND_FEMALE = 799;
		private const int CONTAINER_FAIL_SOUND_MALE = 1071;

		// Special Container Sounds
		private const int SPECIAL_CONTAINER_SOUND_FEMALE = 778;
		private const int SPECIAL_CONTAINER_SOUND_MALE = 1049;

		// Error Sounds
		private const int ERROR_SOUND_FEMALE = 812;
		private const int ERROR_SOUND_MALE = 1086;
		private const int DISAPPOINT_SOUND_FEMALE = 811;
		private const int DISAPPOINT_SOUND_MALE = 1085;

		// Target Constants
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;

		// Success Probability Constants
		private const double INSCRIPTION_DIVISOR = 200.0; // inscriptionMultiplier = 1 + (inscription / 200)
		private const double LOCKPICKING_BONUS_PER_POINT = 0.14; // 0.14% per Lockpicking point above 9
		private const int LOCKPICKING_MINIMUM_SKILL = 10; // Minimum skill to earn bonus
		private const double MAX_SUCCESS_CHANCE = 0.80; // Never exceed 80%
		private const double MAX_SUCCESS_CHANCE_PERCENTAGE = 80.0; // 80% as percentage for calculations

		// TreasureMapChest Special Rules
		private const double TREASURE_CHEST_LEVEL_1_MAX = 0.50; // 50% max for level 1
		private const double TREASURE_CHEST_LEVEL_2_MAX = 0.30; // 30% max for level 2
		#endregion

		public UnlockSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		/// <summary>
		/// Plays the unlock spell effects
		/// </summary>
		/// <param name="location">Location to play effects</param>
		private void PlayUnlockEffects(IPoint3D location)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
			Effects.SendLocationParticles(EffectItem.Create(new Point3D(location), Caster.Map, EffectItem.DefaultDuration),
				EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_DURATION, 0);
			Effects.PlaySound(location, Caster.Map, SOUND_ID);
		}

		/// <summary>
		/// Handles invalid targets with appropriate message and emote
		/// </summary>
		/// <param name="caster">The caster</param>
		/// <param name="message">Error message to display</param>
		private void HandleInvalidTarget(Mobile caster, string message)
		{
			caster.SendMessage(Spell.MSG_COLOR_ERROR, message);
			PlayErrorEmote(caster);
		}

		/// <summary>
		/// Attempts to unlock a dungeon door
		/// </summary>
		/// <param name="caster">The caster</param>
		/// <param name="door">The door to unlock</param>
		private void TryUnlockDoor(Mobile caster, BaseDoor door)
		{
			if (Server.Items.DoorType.IsDungeonDoor(door))
			{
				if (!door.Locked)
				{
					caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ALREADY_UNLOCKED);
					PlayErrorEmote(caster);
				}
				else
				{
					// Calculate success chance based on door difficulty
					int keyValue = (int)door.KeyValue;
					if (keyValue <= DOOR_KEY_VALUE_THRESHOLD &&
						Utility.RandomDouble() < (double)(DOOR_KEY_VALUE_MAX - keyValue) / DOOR_KEY_VALUE_MAX)
					{
						// Success
						door.Locked = false;
						door.KeyValue = 0;
						Server.Items.DoorType.UnlockDoors(door);
						caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.SUCCESS_UNLOCKED);
						PlaySuccessEmote(caster, DOOR_SUCCESS_SOUND_FEMALE, DOOR_SUCCESS_SOUND_MALE, "*ah ha!*");
					}
					else
					{
						// Failure
						caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCK_TOO_COMPLEX);
						PlayDisappointEmote(caster);
					}
				}
			}
			else
			{
				// Not a dungeon door
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ALREADY_UNLOCKED);
				PlayErrorEmote(caster);
			}
		}

		/// <summary>
		/// Calculates the Lockpicking bonus
		/// Formula: (lockpicking - 9) × 0.14 if lockpicking ≥ 10, else 0
		/// This ensures that lockpicking 10 already applies bonus (0.14%)
		/// </summary>
		/// <param name="caster">The caster</param>
		/// <returns>Lockpicking bonus percentage</returns>
		private double CalculateLockpickingBonus(Mobile caster)
		{
			double lockpickingSkill = caster.Skills[SkillName.Lockpicking].Value;
			
			// No bonus if below minimum threshold
			if (lockpickingSkill < LOCKPICKING_MINIMUM_SKILL)
				return 0.0;
			
			// Calculate bonus: (skill - 9) × 0.14
			// This ensures lockpicking 10 gives 0.14% bonus
			double bonus = (lockpickingSkill - (LOCKPICKING_MINIMUM_SKILL - 1)) * LOCKPICKING_BONUS_PER_POINT;
			
			return bonus;
		}

		/// <summary>
		/// Calculates the maximum allowed success chance for TreasureMapChest
		/// </summary>
		/// <param name="container">The container to check</param>
		/// <returns>Maximum allowed chance, or MAX_SUCCESS_CHANCE if not a special chest</returns>
		private double GetTreasureChestMaxChance(LockableContainer container)
		{
			TreasureMapChest treasureChest = container as TreasureMapChest;
			if (treasureChest != null)
			{
				if (treasureChest.Level == 1)
					return TREASURE_CHEST_LEVEL_1_MAX;
				else if (treasureChest.Level == 2)
					return TREASURE_CHEST_LEVEL_2_MAX;
				// Level 3+ cannot be unlocked by spell
			}
			
			return MAX_SUCCESS_CHANCE;
		}

		/// <summary>
		/// Calculates the unlock success chance for a container
		/// Formula: baseChance = (magery / 100) × 28.5
		///          inscriptionMultiplier = 1 + (inscription / 200)
		///          magicPower = baseChance × inscriptionMultiplier
		///          lockpickingBonus = (lockpicking - 9) × 0.14 if lockpicking ≥ 10, else 0
		///          successRate = min(magicPower + lockpickingBonus, 80)
		/// </summary>
		/// <param name="caster">The caster</param>
		/// <param name="container">The container to unlock</param>
		/// <param name="successPercent">Output parameter for the success percentage (0-80)</param>
		/// <returns>Success chance (0.0 to 0.80)</returns>
		private double CalculateUnlockSuccessChance(Mobile caster, LockableContainer container, out int successPercent)
		{
			// Check if unlock skill is sufficient (still need to verify minimum requirement)
			int unlockSkill = (int)NMSUtils.getBeneficialMageryInscribePercentage(caster) + CONTAINER_SKILL_BONUS;
			int requiredSkill = container.RequiredSkill;
			
			// Cannot attempt if skill insufficient
			if (unlockSkill < requiredSkill)
			{
				successPercent = 0;
				return 0.0;
			}
			
			// Get skill values
			double magery = caster.Skills[SkillName.Magery].Value;
			double inscription = caster.Skills[SkillName.Inscribe].Value;
			
			// Calculate baseChance = (magery / 100) × 28.5 (result in percentage, e.g., 28.5 for 100 magery)
			double baseChance = (magery / 100.0) * 28.5;
			
			// Calculate inscriptionMultiplier = 1 + (inscription / 200)
			double inscriptionMultiplier = 1.0 + (inscription / INSCRIPTION_DIVISOR);
			
			// Calculate magicPower = baseChance × inscriptionMultiplier (result in percentage)
			double magicPower = baseChance * inscriptionMultiplier;
			
			// Calculate lockpickingBonus (result in percentage)
			double lockpickingBonus = CalculateLockpickingBonus(caster);
			
			// Calculate successRate = min(magicPower + lockpickingBonus, 80)
			// Convert from percentage to decimal and round to nearest integer percentage
			double totalPercentage = magicPower + lockpickingBonus;
			
			// Apply TreasureMapChest caps if applicable
			double maxChance = GetTreasureChestMaxChance(container);
			double maxChancePercentage = maxChance * 100.0;
			if (totalPercentage > maxChancePercentage)
				totalPercentage = maxChancePercentage;
			
			// Final cap at MAX_SUCCESS_CHANCE_PERCENTAGE (80%)
			if (totalPercentage > MAX_SUCCESS_CHANCE_PERCENTAGE)
				totalPercentage = MAX_SUCCESS_CHANCE_PERCENTAGE;
			
			// Round to nearest integer percentage using Round
			successPercent = (int)Math.Round(totalPercentage);
			
			// Convert back to decimal (0.0 to 0.80)
			return successPercent / 100.0;
		}

		/// <summary>
		/// Attempts to unlock a container using probabilistic success system
		/// </summary>
		/// <param name="caster">The caster</param>
		/// <param name="container">The container to unlock</param>
		private void TryUnlockContainer(Mobile caster, LockableContainer container)
		{
			if (Multis.BaseHouse.CheckSecured(container))
			{
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_USE_MAGIC_ON_SECURED);
				PlayDisappointEmote(caster);
				return;
			}

			if (!container.Locked || container.LockLevel == 0)
			{
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CHEST_NOT_LOCKED);
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ALREADY_UNLOCKED);
				PlayErrorEmote(caster);
				return;
			}

			// Check for special protected containers
			if (container is ParagonChest || container is PirateChest)
			{
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_MAGIC_AURA_PREVENTS_UNLOCK);
				PlaySpecialContainerEmote(caster);
				return;
			}

			// Handle TreasureMapChest
			TreasureMapChest treasureChest = container as TreasureMapChest;
			if (treasureChest != null && treasureChest.Level > 2)
			{
				// Level 3+ cannot be unlocked by spell
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_MAGIC_AURA_PREVENTS_UNLOCK);
				PlaySpecialContainerEmote(caster);
				return;
			}

			// Calculate success chance and percentage
			int successPercent;
			double successChance = CalculateUnlockSuccessChance(caster, container, out successPercent);

			if (successChance <= 0.0)
			{
				// Skill insufficient
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCK_TOO_COMPLEX_FOR_SPELL);
				PlayConfusionEmote(caster);
				return;
			}

			// Attempt unlock with calculated probability
			if (Utility.RandomDouble() < successChance)
			{
				// Success
				container.Locked = false;
				caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.SUCCESS_UNLOCKED);
				
				// Display success chance percentage
				caster.SendMessage(Spell.MSG_COLOR_SYSTEM, string.Format("A sua chance de sucesso foi de: {0}%", successPercent));
				
				PlaySuccessEmote(caster, CONTAINER_SUCCESS_SOUND_FEMALE, CONTAINER_SUCCESS_SOUND_MALE, "*woohoo!*");

				if (container.LockLevel == -255)
					container.LockLevel = container.RequiredSkill - 10;
			}
			else
			{
				// Failure
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCK_TOO_COMPLEX_FOR_SPELL);
				
				// Display success chance percentage (debug message)
				//caster.SendMessage(Spell.MSG_COLOR_ERROR, string.Format("[DEBUG] A sua chance de sucesso foi de: {0}%", successPercent));
				
				PlayConfusionEmote(caster);
			}
		}

		/// <summary>
		/// Plays error emote with sound
		/// </summary>
		/// <param name="caster">The caster</param>
		private void PlayErrorEmote(Mobile caster)
		{
			caster.PlaySound(caster.Female ? ERROR_SOUND_FEMALE : ERROR_SOUND_MALE);
			caster.Say("*oops*");
		}

		/// <summary>
		/// Plays disappointment emote with sound
		/// </summary>
		/// <param name="caster">The caster</param>
		private void PlayDisappointEmote(Mobile caster)
		{
			caster.PlaySound(caster.Female ? DISAPPOINT_SOUND_FEMALE : DISAPPOINT_SOUND_MALE);
			caster.Say("*oooh*");
		}

		/// <summary>
		/// Plays success emote with appropriate sound
		/// </summary>
		/// <param name="caster">The caster</param>
		/// <param name="femaleSound">Sound for female casters</param>
		/// <param name="maleSound">Sound for male casters</param>
		/// <param name="emote">Emote text</param>
		private void PlaySuccessEmote(Mobile caster, int femaleSound, int maleSound, string emote)
		{
			caster.PlaySound(caster.Female ? femaleSound : maleSound);
			caster.Say(emote);
		}

		/// <summary>
		/// Plays confusion emote with sound
		/// </summary>
		/// <param name="caster">The caster</param>
		private void PlayConfusionEmote(Mobile caster)
		{
			caster.PlaySound(caster.Female ? CONTAINER_FAIL_SOUND_FEMALE : CONTAINER_FAIL_SOUND_MALE);
			caster.Say("*huh?*");
		}

		/// <summary>
		/// Plays special container emote with sound
		/// </summary>
		/// <param name="caster">The caster</param>
		private void PlaySpecialContainerEmote(Mobile caster)
		{
			caster.PlaySound(caster.Female ? SPECIAL_CONTAINER_SOUND_FEMALE : SPECIAL_CONTAINER_SOUND_MALE);
			caster.Say("*ah!*");
		}

		private class InternalTarget : Target
		{
			private UnlockSpell m_Owner;

			public InternalTarget(UnlockSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				IPoint3D loc = o as IPoint3D;

				if (loc == null)
					return;

				if (!m_Owner.CheckSequence())
				{
					m_Owner.FinishSequence();
					return;
				}

				SpellHelper.Turn(from, o);
				m_Owner.PlayUnlockEffects(loc);

				// Route to appropriate handler based on target type
				// Check invalid targets first
				if (o is Mobile)
				{
					m_Owner.HandleInvalidTarget(from, Spell.SpellMessages.ERROR_NOTHING_TO_UNLOCK);
				}
				else if (o is BaseHouseDoor)
				{
					m_Owner.HandleInvalidTarget(from, Spell.SpellMessages.ERROR_UNLOCK_WRONG_SPELL);
				}
				else if (o is BookBox)
				{
					m_Owner.HandleInvalidTarget(from, Spell.SpellMessages.ERROR_CURSED_BOX_CANNOT_UNLOCK);
				}
				else if (o is UnidentifiedArtifact || o is UnidentifiedItem || o is CurseItem)
				{
					m_Owner.HandleInvalidTarget(from, Spell.SpellMessages.ERROR_UNLOCK_NO_EFFECT);
				}
				else if (o is BaseDoor)
				{
					m_Owner.TryUnlockDoor(from, (BaseDoor)o);
				}
				else if (o is LockableContainer)
				{
					m_Owner.TryUnlockContainer(from, (LockableContainer)o);
				}
				else
				{
					m_Owner.HandleInvalidTarget(from, Spell.SpellMessages.ERROR_CANNOT_UNLOCK_ITEM);
				}

				m_Owner.FinishSequence();
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}