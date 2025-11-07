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
					caster.LocalOverheadMessage(MessageType.Regular, 55, false, "Isso não precisava ser destrancado.");
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
				caster.LocalOverheadMessage(MessageType.Regular, 55, false, "Isso não precisava ser destrancado.");
				PlayErrorEmote(caster);
			}
		}

		/// <summary>
		/// Attempts to unlock a container
		/// </summary>
		/// <param name="caster">The caster</param>
		/// <param name="container">The container to unlock</param>
		private void TryUnlockContainer(Mobile caster, LockableContainer container)
		{
			if (Multis.BaseHouse.CheckSecured(container))
			{
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_USE_MAGIC_ON_SECURED);
				PlayDisappointEmote(caster);
			}
			else if (!container.Locked || container.LockLevel == 0)
			{
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CHEST_NOT_LOCKED);
				caster.LocalOverheadMessage(MessageType.Regular, 55, false, "* Aff! Isso parece já estar destrancado! *");
				PlayErrorEmote(caster);
			}
			else if (container is TreasureMapChest || container is ParagonChest || container is PirateChest)
			{
				caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_MAGIC_AURA_PREVENTS_UNLOCK);
				PlaySpecialContainerEmote(caster);
			}
			else
			{
				// Calculate unlock skill
				int unlockSkill = (int)NMSUtils.getBeneficialMageryInscribePercentage(caster) + CONTAINER_SKILL_BONUS;

				if (unlockSkill >= container.RequiredSkill &&
					!(container is TreasureMapChest && ((TreasureMapChest)container).Level > 2))
				{
					// Success
					container.Locked = false;
					caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.SUCCESS_UNLOCKED);
					PlaySuccessEmote(caster, CONTAINER_SUCCESS_SOUND_FEMALE, CONTAINER_SUCCESS_SOUND_MALE, "*woohoo!*");

					if (container.LockLevel == -255)
						container.LockLevel = container.RequiredSkill - 10;
				}
				else
				{
					// Failure
					caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCK_TOO_COMPLEX_FOR_SPELL);
					PlayConfusionEmote(caster);
				}
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

				if (m_Owner.CheckSequence())
				{
					SpellHelper.Turn(from, o);
					m_Owner.PlayUnlockEffects(loc);

					// Route to appropriate handler based on target type
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