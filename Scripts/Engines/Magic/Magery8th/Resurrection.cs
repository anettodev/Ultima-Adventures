using System;
using Server.Targeting;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Resurrection - 8th Circle Magery Spell
	/// Resurrects dead players, pets, and henchmen. Can also create SoulOrb for self-cast.
	/// </summary>
	public class ResurrectionSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Resurrection", "An Corp",
				245,
				9062,
				Reagent.Bloodmoss,
				Reagent.Garlic,
				Reagent.Ginseng
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		#region Constants

		/// <summary>Range check for resurrection (tiles)</summary>
		private const int RESURRECTION_RANGE = 4;

		/// <summary>Map fit check size for resurrection location</summary>
		private const int MAP_FIT_SIZE = 16;

		/// <summary>Sound ID for resurrection effect</summary>
		private const int SOUND_RESURRECT = 0x214;

		/// <summary>Effect ID for resurrection visual</summary>
		private const int EFFECT_RESURRECT = 0x376A;

		/// <summary>Effect duration (ticks)</summary>
		private const int EFFECT_DURATION = 10;

		/// <summary>Effect speed (ticks)</summary>
		private const int EFFECT_SPEED = 16;

		/// <summary>Magery skill contribution to SoulOrb creation chance (0.3% per point)</summary>
		private const double MAGERY_CHANCE_MULTIPLIER = 0.003;

		/// <summary>Inscribe skill contribution to SoulOrb creation chance (0.2% per point)</summary>
		private const double INSCRIBE_CHANCE_MULTIPLIER = 0.002;

		/// <summary>Base expiration time for SoulOrb (seconds)</summary>
		private const int SOULORB_BASE_EXPIRATION = 30;

		/// <summary>Magery skill contribution to expiration (1 second per 10 skill points)</summary>
		private const double MAGERY_EXPIRATION_DIVISOR = 10.0;

		/// <summary>Healing skill contribution to expiration (0,5 seconds per skill point)</summary>
		private const double HEALING_EXPIRATION_MULTIPLIER = 0.5;

		/// <summary>Minimum Veterinary skill required for pet resurrection</summary>
		private const double MIN_VETERINARY_SKILL = 80.0;

		/// <summary>Veterinary skill contribution to pet resurrection chance (0.5% per point)</summary>
		private const double VETERINARY_CHANCE_MULTIPLIER = 0.005;

		/// <summary>Maximum pet resurrection chance (60% cap)</summary>
		private const double MAX_PET_RESURRECTION_CHANCE = 0.60;

		/// <summary>Percentage of stats restored after pet resurrection (10%)</summary>
		private const double PET_RESURRECTION_STATS_PERCENTAGE = 0.10;

		#endregion

		public ResurrectionSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		/// <summary>
		/// Handles resurrection targeting for Mobile targets (players, pets)
		/// </summary>
		/// <param name="m">Target mobile to resurrect</param>
		public void Target(Mobile m)
		{
			if (m == null || Caster == null)
			{
				FinishSequence();
				return;
			}

			if (!Caster.CanSee(m))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_TARGET_NOT_VISIBLE);
				FinishSequence();
				return;
			}

			// Self-cast: Create SoulOrb
			if (m == Caster)
			{
				if (CheckBSequence(m, true))
				{
					if (!HandleSelfCast(m))
					{
						FinishSequence();
						return;
					}
				}
				FinishSequence();
				return;
			}

			// Caster must be alive for other targets
			if (!Caster.Alive)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_MUST_BE_ALIVE);
				FinishSequence();
				return;
			}

			// Target must be dead (for players)
			PlayerMobile playerTarget = m as PlayerMobile;
			if (playerTarget != null && m.Alive)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_TARGET_NOT_DEAD);
				FinishSequence();
				return;
			}

			// Range check
			if (!Caster.InRange(m, RESURRECTION_RANGE))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_TARGET_TOO_FAR);
				FinishSequence();
				return;
			}

			// Location check
			if (m.Map == null || !m.Map.CanFit(m.Location, MAP_FIT_SIZE, false, false))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_LOCATION_BLOCKED_CASTER);
				m.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_LOCATION_BLOCKED_TARGET);
				FinishSequence();
				return;
			}

			// Player resurrection
			if (playerTarget != null && CheckBSequence(m, true))
			{
				HandlePlayerResurrection(m);
				FinishSequence();
				return;
			}

			// Pet resurrection
			BaseCreature pet = m as BaseCreature;
			if (pet != null && pet.GetMaster() != null)
			{
				// Check if pet is summoned (summoned pets cannot be resurrected)
				if (pet.Summoned)
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_PET_FAILED);
					FinishSequence();
					return;
				}

				// Check if pet is bonded (only bonded pets can be resurrected)
				if (!pet.IsBonded)
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_PET_FAILED);
					FinishSequence();
					return;
				}

				// Check if pet is controlled
				if (!pet.Controlled)
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_PET_FAILED);
					FinishSequence();
					return;
				}

				// Check Veterinary skill requirement
				double veterinary = Caster.Skills[SkillName.Veterinary].Value;
				if (veterinary < MIN_VETERINARY_SKILL)
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_PET_VETERINARY_REQUIRED);
					FinishSequence();
					return;
				}

				// Calculate resurrection chance (cap at 60%)
				double resurrectionChance = veterinary * VETERINARY_CHANCE_MULTIPLIER;
				if (resurrectionChance > MAX_PET_RESURRECTION_CHANCE)
					resurrectionChance = MAX_PET_RESURRECTION_CHANCE;

				if (Utility.RandomDouble() <= resurrectionChance && CheckBSequence(m, true))
				{
					HandlePetResurrection(m);
					FinishSequence();
					return;
				}
				else
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_PET_FAILED);
					FinishSequence();
					return;
				}
			}

			FinishSequence();
		}

		/// <summary>
		/// Handles resurrection targeting for Item targets (henchmen)
		/// </summary>
		/// <param name="hench">Henchman item to resurrect</param>
		public void ItemTarget(Item hench)
		{
			if (hench == null)
			{
				FinishSequence();
				return;
			}

			if (!CheckSequence())
			{
				FinishSequence();
				return;
			}

			bool success = false;
			HenchmanItem henchmanItem = hench as HenchmanItem;

			if (hench is HenchmanFighterItem)
			{
				success = ResurrectHenchman((HenchmanFighterItem)hench, "fighter henchman");
			}
			else if (hench is HenchmanWizardItem)
			{
				success = ResurrectHenchman((HenchmanWizardItem)hench, "wizard henchman");
			}
			else if (hench is HenchmanArcherItem)
			{
				success = ResurrectHenchman((HenchmanArcherItem)hench, "archer henchman");
			}
			else if (hench is HenchmanMonsterItem)
			{
				success = ResurrectHenchman((HenchmanMonsterItem)hench, "creature henchman");
			}

			if (!success)
			{
				if (henchmanItem != null)
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_HENCH_NOT_DEAD);
				}
				else
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_SPELL_FAILED);
				}
			}

			FinishSequence();
		}

		#region Helper Methods

		/// <summary>
		/// Applies visual and sound effects for resurrection
		/// </summary>
		/// <param name="target">Mobile to apply effects to</param>
		private void ApplyResurrectionEffects(Mobile target)
		{
			if (target == null)
				return;

			target.PlaySound(SOUND_RESURRECT);
			target.FixedEffect(EFFECT_RESURRECT, EFFECT_DURATION, EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0), 0);
		}

		/// <summary>
		/// Calculates the chance of creating a SoulOrb based on caster skills
		/// </summary>
		/// <param name="caster">Caster mobile</param>
		/// <returns>Chance value between 0.0 and 1.0</returns>
		private double CalculateSoulOrbCreationChance(Mobile caster)
		{
			double mageryChance = caster.Skills[SkillName.Magery].Value * MAGERY_CHANCE_MULTIPLIER;
			double inscribeChance = caster.Skills[SkillName.Inscribe].Value * INSCRIBE_CHANCE_MULTIPLIER;
			double totalChance = mageryChance + inscribeChance;
			
			// Cap at 100%
			if (totalChance > 1.0)
				totalChance = 1.0;
			
			return totalChance;
		}

		/// <summary>
		/// Calculates the expiration time for a SoulOrb based on caster skills
		/// </summary>
		/// <param name="caster">Caster mobile</param>
		/// <returns>Expiration time in seconds</returns>
		private int CalculateSoulOrbExpirationTime(Mobile caster)
		{
			double magery = caster.Skills[SkillName.Magery].Value;
			double healing = caster.Skills[SkillName.Healing].Value;
			
			// Base 30s + (Magery / 10) + (Healing * 0.5)
			int expiration = SOULORB_BASE_EXPIRATION + (int)(magery / MAGERY_EXPIRATION_DIVISOR) + (int)(healing * HEALING_EXPIRATION_MULTIPLIER);
			
			return expiration;
		}

		/// <summary>
		/// Formats a TimeSpan duration into a readable PT-BR string
		/// </summary>
		/// <param name="duration">TimeSpan to format</param>
		/// <returns>Formatted duration string in PT-BR</returns>
		private string FormatDuration(TimeSpan duration)
		{
			int totalSeconds = (int)duration.TotalSeconds;
			int minutes = totalSeconds / 60;
			int seconds = totalSeconds % 60;

			if (minutes > 0 && seconds > 0)
			{
				return String.Format("{0} {1} e {2} {3}",
					minutes,
					minutes == 1 ? "minuto" : "minutos",
					seconds,
					seconds == 1 ? "segundo" : "segundos");
			}
			else if (minutes > 0)
			{
				return String.Format("{0} {1}",
					minutes,
					minutes == 1 ? "minuto" : "minutos");
			}
			else
			{
				return String.Format("{0} {1}",
					seconds,
					seconds == 1 ? "segundo" : "segundos");
			}
		}

		/// <summary>
		/// Handles self-cast resurrection (SoulOrb creation)
		/// </summary>
		/// <param name="m">Caster mobile</param>
		/// <returns>True if SoulOrb was created successfully, false otherwise</returns>
		private bool HandleSelfCast(Mobile m)
		{
			PlayerMobile player = m as PlayerMobile;
			if (player != null && player.SoulBound)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_CANNOT_USE_ON_SELF);
				return false;
			}

			if (m.Backpack == null)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_SPELL_FAILED);
				return false;
			}

			// Calculate creation chance
			double creationChance = CalculateSoulOrbCreationChance(m);
			if (Utility.RandomDouble() > creationChance)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_SPELL_FAILED);
				return false;
			}

			// Remove existing SoulOrbs (replace old one)
			List<Item> targets = new List<Item>();
			foreach (Item item in m.Backpack.Items)
			{
				SoulOrb myOrb = item as SoulOrb;
				if (myOrb != null && myOrb.m_Owner == m)
				{
					targets.Add(item);
				}
			}

			foreach (Item item in targets)
			{
				item.Delete();
			}

			// Calculate expiration time
			int expirationSeconds = CalculateSoulOrbExpirationTime(m);
			TimeSpan expirationTime = TimeSpan.FromSeconds(expirationSeconds);

			// Create new SoulOrb
			ApplyResurrectionEffects(m);
			m.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.RESURRECTION_SOULORB_CREATED);
			
			// Send duration message
			string durationText = FormatDuration(expirationTime);
			m.SendMessage(Spell.MSG_COLOR_SYSTEM, String.Format(Spell.SpellMessages.RESURRECTION_SOULORB_DURATION, durationText));
			
			SoulOrb iOrb = new SoulOrb();
			iOrb.m_Owner = m;
			iOrb.ExpirationTime = expirationTime;
			m.AddToBackpack(iOrb);
			Server.Items.SoulOrb.OnSummoned(m, iOrb);

			return true;
		}

		/// <summary>
		/// Handles player resurrection
		/// </summary>
		/// <param name="m">Dead player mobile to resurrect</param>
		private void HandlePlayerResurrection(Mobile m)
		{
			PlayerMobile player = m as PlayerMobile;
			if (player != null && player.SoulBound)
			{
				player.ResetPlayer(m);
				ApplyResurrectionEffects(m);
				Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.RESURRECTION_SOULBOUND_MESSAGE);
				return;
			}

			SpellHelper.Turn(Caster, m);
			ApplyResurrectionEffects(m);

			m.CloseGump(typeof(ResurrectGump));
			m.SendGump(new ResurrectGump(m, Caster));
		}

		/// <summary>
		/// Handles pet resurrection
		/// </summary>
		/// <param name="m">Dead pet mobile to resurrect</param>
		private void HandlePetResurrection(Mobile m)
		{
			BaseCreature pet = m as BaseCreature;
			if (pet == null)
				return;

			Mobile master = pet.GetMaster();
			if (master == null)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESURRECTION_SPELL_FAILED);
				return;
			}

			SpellHelper.Turn(Caster, m);
			ApplyResurrectionEffects(m);

			// Send success message to caster
			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.RESURRECTION_PET_SUCCESS);

			// Send gump to master with 10% stats scalar
			master.CloseGump(typeof(PetResurrectGump));
			master.SendGump(new PetResurrectGump(master, pet, PET_RESURRECTION_STATS_PERCENTAGE));
		}

		/// <summary>
		/// Resurrects a henchman item
		/// </summary>
		/// <param name="friend">Henchman item to resurrect</param>
		/// <param name="defaultName">Default name to restore</param>
		/// <returns>True if henchman was resurrected, false otherwise</returns>
		private bool ResurrectHenchman(HenchmanItem friend, string defaultName)
		{
			if (friend == null)
				return false;

			if (friend.HenchDead > 0)
			{
				friend.Name = defaultName;
				friend.HenchDead = 0;
				friend.InvalidateProperties();
				Caster.PlaySound(SOUND_RESURRECT);
				return true;
			}
			return false;
		}

		#endregion

		#region Internal Classes

		private class InternalTarget : Target
		{
			private ResurrectionSpell m_Owner;

			public InternalTarget(ResurrectionSpell owner) : base(SpellConstants.GetSpellRange(), false, TargetFlags.Beneficial)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Mobile)
				{
					m_Owner.Target((Mobile)o);
				}
				else if (o is Item)
				{
					m_Owner.ItemTarget((Item)o);
				}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}
