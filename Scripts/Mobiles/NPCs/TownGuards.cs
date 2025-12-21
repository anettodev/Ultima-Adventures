using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles 
{ 
	/// <summary>
	/// Town Guards - NPCs that protect towns and cities, accept bounties, and enforce law.
	/// Equipment and appearance vary by region/world location.
	/// </summary>
	public class TownGuards : BasePerson
    {
		#region Fields

		/// <summary>Flag to prevent multiple combat messages in quick succession</summary>
		private bool m_Talked;

		#endregion

		#region Constructors

        [Constructable] 
		public TownGuards() : base() 
		{
			Title = TownGuardsStringConstants.TITLE_GUARD;
			NameHue = TownGuardsConstants.NAME_HUE;
			SetStr(TownGuardsConstants.STAT_STR_MIN, TownGuardsConstants.STAT_STR_MAX);
			SetDex(TownGuardsConstants.STAT_DEX_MIN, TownGuardsConstants.STAT_DEX_MAX);
			SetInt(TownGuardsConstants.STAT_INT_MIN, TownGuardsConstants.STAT_INT_MAX);
			SetHits(TownGuardsConstants.HITS_MIN, TownGuardsConstants.HITS_MAX);
			SetDamage(TownGuardsConstants.DAMAGE_MIN, TownGuardsConstants.DAMAGE_MAX);
			VirtualArmor = TownGuardsConstants.VIRTUAL_ARMOR;

			SetSkill(SkillName.Anatomy, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.MagicResist, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.Parry, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.Fencing, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.Macing, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.DetectHidden, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.Wrestling, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.Swords, TownGuardsConstants.SKILL_VALUE);
			SetSkill(SkillName.Tactics, TownGuardsConstants.SKILL_VALUE);
		}

		#endregion

		#region Properties

		public override bool BardImmune { get { return true; } }
		public override Poison PoisonImmune { get { return Poison.Deadly; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable { get { return true; } }

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles item drag and drop for bounty rewards
		/// </summary>
		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			if (IntelligentAction.GetMyEnemies(from, this, false) == true)
			{
				this.PrivateOverheadMessage(MessageType.Regular, TownGuardsConstants.MESSAGE_COLOR, false, TownGuardsStringConstants.MSG_ENEMY_CANNOT_DROP, from.NetState);
				return base.OnDragDrop(from, dropped);
            }

                int karma = 0;
                int gold = 0;
				int fame = 0;

				if (dropped is PirateBounty)
				{
					PirateBounty bounty = (PirateBounty)dropped;
				fame = (int)(bounty.BountyValue / TownGuardsConstants.PIRATE_BOUNTY_FAME_DIVISOR);
				karma = TownGuardsConstants.PIRATE_BOUNTY_KARMA_MULTIPLIER * fame;
					gold = bounty.BountyValue;
				}
				else if (dropped is Head && !from.Blessed)
				{
					Head head = (Head)dropped;
				BountyReward reward = CalculateHeadBountyReward(head.m_Job);

				if (reward == null)
				{
					this.PrivateOverheadMessage(MessageType.Regular, TownGuardsConstants.MESSAGE_COLOR, false, TownGuardsStringConstants.MSG_UNKNOWN_HEAD_TYPE, from.NetState);
					return base.OnDragDrop(from, dropped);
				}

				karma = reward.Karma;
				gold = reward.Gold;
					}
					else
					{
						return base.OnDragDrop(from, dropped);
				}

			string message = GetBountyDialog(gold);
                Titles.AwardKarma(from, karma, true);
                Titles.AwardFame(from, fame, true);

			from.SendSound(TownGuardsConstants.SOUND_BOUNTY_REWARD);
                from.AddToBackpack(new Gold(gold));

			this.PrivateOverheadMessage(MessageType.Regular, TownGuardsConstants.MESSAGE_COLOR, false, message, from.NetState);
                dropped.Delete();
                return true;
		}

		/// <summary>
		/// Configures guard equipment based on spawn location
		/// </summary>
		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			string regionName = Server.Misc.Worlds.GetRegionName(this.Map, this.Location);
			string worldName = Server.Misc.Worlds.GetMyWorld(this.Map, this.Location, this.X, this.Y);

			TownGuardEquipmentConfig config = TownGuardEquipmentConfig.GetConfigByRegionName(regionName);
			if (config == null)
			{
				config = TownGuardEquipmentConfig.GetConfig(regionName, worldName);
			}

			Item weapon = CreateWeapon(config.WeaponType);
			ConfigureWeapon(weapon);
			AddItem(weapon);

			AddStandardArmor(worldName);

			if (config.HelmType > 0)
			{
				AddItem(CreateHelm(config.HelmType));
			}

			if (config.ShieldType > 0)
			{
				AddItem(CreateShield(config.ShieldType));
			}

			MorphingTime.ColorMyClothes(this, config.ClothColor);

			if (config.CloakColor > 0)
			{
				AddItem(CreateCloak(config.CloakColor));
			}

			Server.Misc.MorphingTime.CheckMorph(this);
		}

		/// <summary>
		/// Determines if a mobile is an enemy and handles guard response
		/// </summary>
		public override bool IsEnemy(Mobile m)
		{
			if (IntelligentAction.GetMyEnemies(m, this, true) == false)
				return false;

			if (m.Region != this.Region && !(m is PlayerMobile))
				return false;

			m.Criminal = true;
			PlayTeleportEffects(this.Location);
			this.Location = m.Location;
			this.Combatant = m;
			this.Warmode = true;
			PlayTeleportEffects(this.Location);

			return true;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Calculates bounty reward for a head based on job type
		/// </summary>
		/// <param name="job">The job type of the head</param>
		/// <returns>Bounty reward with karma and gold, or null if unknown job</returns>
		private BountyReward CalculateHeadBountyReward(string job)
		{
			if (job == "Thief")
			{
				return new BountyReward
				{
					Karma = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_THIEF_KARMA_MIN, TownGuardsConstants.BOUNTY_THIEF_KARMA_MAX),
					Gold = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_THIEF_GOLD_MIN, TownGuardsConstants.BOUNTY_THIEF_GOLD_MAX)
				};
			}
			else if (job == "Bandit")
			{
				return new BountyReward
				{
					Karma = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_BANDIT_KARMA_MIN, TownGuardsConstants.BOUNTY_BANDIT_KARMA_MAX),
					Gold = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_BANDIT_GOLD_MIN, TownGuardsConstants.BOUNTY_BANDIT_GOLD_MAX)
				};
			}
			else if (job == "Brigand")
			{
				return new BountyReward
				{
					Karma = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_BRIGAND_KARMA_MIN, TownGuardsConstants.BOUNTY_BRIGAND_KARMA_MAX),
					Gold = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_BRIGAND_GOLD_MIN, TownGuardsConstants.BOUNTY_BRIGAND_GOLD_MAX)
				};
			}
			else if (job == "Pirate")
			{
				return new BountyReward
				{
					Karma = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_PIRATE_KARMA_MIN, TownGuardsConstants.BOUNTY_PIRATE_KARMA_MAX),
					Gold = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_PIRATE_GOLD_MIN, TownGuardsConstants.BOUNTY_PIRATE_GOLD_MAX)
				};
			}
			else if (job == "Assassin")
			{
				return new BountyReward
				{
					Karma = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_ASSASSIN_KARMA_MIN, TownGuardsConstants.BOUNTY_ASSASSIN_KARMA_MAX),
					Gold = Utility.RandomMinMax(TownGuardsConstants.BOUNTY_ASSASSIN_GOLD_MIN, TownGuardsConstants.BOUNTY_ASSASSIN_GOLD_MAX)
				};
			}

			return null;
		}

		/// <summary>
		/// Gets a random bounty dialog message
		/// </summary>
		/// <param name="gold">The gold amount to include in the message</param>
		/// <returns>Formatted dialog message</returns>
		private string GetBountyDialog(int gold)
		{
			string rewardMessage = GetRewardMessage(gold);
			string dialogMessage = GetDialogMessage(rewardMessage);
			return dialogMessage;
		}

		/// <summary>
		/// Gets a random reward message with gold amount
		/// </summary>
		/// <param name="gold">The gold amount</param>
		/// <returns>Formatted reward message</returns>
		private string GetRewardMessage(int gold)
		{
			int index = Utility.RandomMinMax(TownGuardsConstants.REWARD_MESSAGE_MIN, TownGuardsConstants.REWARD_MESSAGE_MAX);
			
			switch (index)
			{
				case 0: return string.Format(TownGuardsStringConstants.BOUNTY_REWARD_1_FORMAT, gold);
				case 1: return string.Format(TownGuardsStringConstants.BOUNTY_REWARD_2_FORMAT, gold);
				case 2: return string.Format(TownGuardsStringConstants.BOUNTY_REWARD_3_FORMAT, gold);
				case 3: return string.Format(TownGuardsStringConstants.BOUNTY_REWARD_4_FORMAT, gold);
				default: return string.Format(TownGuardsStringConstants.BOUNTY_REWARD_1_FORMAT, gold);
			}
		}

		/// <summary>
		/// Gets a random dialog message with reward message appended
		/// </summary>
		/// <param name="rewardMessage">The reward message to append</param>
		/// <returns>Formatted dialog message</returns>
		private string GetDialogMessage(string rewardMessage)
		{
			int index = Utility.RandomMinMax(TownGuardsConstants.DIALOG_MESSAGE_MIN, TownGuardsConstants.DIALOG_MESSAGE_MAX);
			
			switch (index)
			{
				case 0: return string.Format(TownGuardsStringConstants.BOUNTY_DIALOG_1_FORMAT, rewardMessage);
				case 1: return string.Format(TownGuardsStringConstants.BOUNTY_DIALOG_2_FORMAT, rewardMessage);
				case 2: return string.Format(TownGuardsStringConstants.BOUNTY_DIALOG_3_FORMAT, rewardMessage);
				case 3: return string.Format(TownGuardsStringConstants.BOUNTY_DIALOG_4_FORMAT, rewardMessage);
				case 4: return string.Format(TownGuardsStringConstants.BOUNTY_DIALOG_5_FORMAT, rewardMessage);
				default: return string.Format(TownGuardsStringConstants.BOUNTY_DIALOG_1_FORMAT, rewardMessage);
			}
		}

		/// <summary>
		/// Creates a weapon of the specified type
		/// </summary>
		/// <param name="weaponType">The type of weapon to create</param>
		/// <returns>Created weapon item</returns>
		private Item CreateWeapon(Type weaponType)
		{
			if (weaponType == null)
			{
				return new VikingSword();
			}

			try
			{
				Item weapon = Activator.CreateInstance(weaponType) as Item;
				if (weapon == null)
				{
					return new VikingSword();
				}

				return weapon;
			}
			catch
			{
				return new VikingSword();
			}
		}

		/// <summary>
		/// Configures weapon stats (damage, durability, etc.)
		/// </summary>
		/// <param name="weapon">The weapon to configure</param>
		private void ConfigureWeapon(Item weapon)
		{
			weapon.Movable = false;
			
			BaseWeapon baseWeapon = weapon as BaseWeapon;
			if (baseWeapon != null)
			{
				baseWeapon.MaxHitPoints = TownGuardsConstants.WEAPON_MAX_HIT_POINTS;
				baseWeapon.HitPoints = TownGuardsConstants.WEAPON_HIT_POINTS;
				baseWeapon.MinDamage = TownGuardsConstants.WEAPON_MIN_DAMAGE;
				baseWeapon.MaxDamage = TownGuardsConstants.WEAPON_MAX_DAMAGE;
			}
		}

		/// <summary>
		/// Adds standard armor pieces to the guard
		/// </summary>
		/// <param name="worldName">The world name (used for special cases like Serpent Island)</param>
		private void AddStandardArmor(string worldName)
		{
			AddItem(new PlateChest());
			
			if (worldName == "the Serpent Island")
			{
				AddItem(new RingmailArms()); // FOR GARGOYLES
			}
			else
			{
				AddItem(new PlateArms());
			}
			
			AddItem(new PlateLegs());
			AddItem(new PlateGorget());
			AddItem(new PlateGloves());
			AddItem(new Boots());
		}

		/// <summary>
		/// Creates a helm with the specified item ID
		/// </summary>
		/// <param name="helmType">The helm item ID</param>
		/// <returns>Created helm item</returns>
		private PlateHelm CreateHelm(int helmType)
			{
				PlateHelm helm = new PlateHelm();
					helm.ItemID = helmType;
			helm.Name = TownGuardsStringConstants.EQUIPMENT_NAME_HELM;
			return helm;
		}

		/// <summary>
		/// Creates a shield with the specified item ID
		/// </summary>
		/// <param name="shieldType">The shield item ID</param>
		/// <returns>Created shield item</returns>
		private ChaosShield CreateShield(int shieldType)
			{
				ChaosShield shield = new ChaosShield();
					shield.ItemID = shieldType;
			shield.Name = TownGuardsStringConstants.EQUIPMENT_NAME_SHIELD;
			return shield;
		}

		/// <summary>
		/// Creates a cloak with the specified color
		/// </summary>
		/// <param name="cloakColor">The cloak color hue</param>
		/// <returns>Created cloak item</returns>
		private Cloak CreateCloak(int cloakColor)
			{
				Cloak cloak = new Cloak();
					cloak.Hue = cloakColor;
			return cloak;
		}

		/// <summary>
		/// Plays teleportation visual and sound effects
		/// </summary>
		/// <param name="location">The location where effects should play</param>
		private void PlayTeleportEffects(Point3D location)
		{
			Effects.SendLocationParticles(
				EffectItem.Create(location, this.Map, EffectItem.DefaultDuration),
				TownGuardsConstants.EFFECT_PARTICLE_ID,
				TownGuardsConstants.EFFECT_PARTICLE_COUNT,
				TownGuardsConstants.EFFECT_PARTICLE_SPEED,
				TownGuardsConstants.EFFECT_PARTICLE_HUE);
			Effects.PlaySound(this, this.Map, TownGuardsConstants.EFFECT_SOUND_ID);
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Handles movement events to check if guard should return to post
		/// </summary>
		public override void OnMovement(Mobile m, Point3D oldLocation)
		{
			WalkAwayCombatTimer t = new WalkAwayCombatTimer(this);
			t.Start();
		}

		/// <summary>
		/// Handles melee attack events to say combat messages
		/// </summary>
		public override void OnGaveMeleeAttack(Mobile defender)
		{
			if (m_Talked == false)
			{
				int index = Utility.Random(TownGuardsConstants.COMBAT_MESSAGE_MAX);
				string message = GetCombatMessage(index, defender.Name);
				Say(message);
			}
		}

		/// <summary>
		/// Gets a combat message based on index and defender name
		/// </summary>
		/// <param name="index">The message index</param>
		/// <param name="defenderName">The defender's name</param>
		/// <returns>Formatted combat message</returns>
		private string GetCombatMessage(int index, string defenderName)
		{
			switch (index)
			{
				case 0: return TownGuardsStringConstants.COMBAT_STOP_LAW;
				case 1: return TownGuardsStringConstants.COMBAT_SHOW_JUSTICE;
				case 2: return string.Format(TownGuardsStringConstants.COMBAT_HISTORY_ENDS_FORMAT, defenderName);
				case 3: return string.Format(TownGuardsStringConstants.COMBAT_AFTER_YOU_FORMAT, defenderName);
				case 4: return string.Format(TownGuardsStringConstants.COMBAT_SOLDIERS_ALERT_FORMAT, defenderName);
				case 5: return string.Format(TownGuardsStringConstants.COMBAT_TRAINED_HUNT_FORMAT, defenderName);
				case 6: return string.Format(TownGuardsStringConstants.COMBAT_GIVE_UP_FORMAT, defenderName);
				case 7: return string.Format(TownGuardsStringConstants.COMBAT_SENTENCE_DEATH_FORMAT, defenderName);
				default: return TownGuardsStringConstants.COMBAT_STOP_LAW;
			}
		}

		/// <summary>
		/// Prevents guard death and restores health
		/// </summary>
		public override bool OnBeforeDeath()
		{
			Say(TownGuardsStringConstants.MSG_DEATH_PREVENTION);
			this.Hits = this.HitsMax;
			this.FixedParticles(
				TownGuardsConstants.EFFECT_DEATH_PARTICLE_ID,
				TownGuardsConstants.EFFECT_DEATH_PARTICLE_COUNT,
				TownGuardsConstants.EFFECT_DEATH_PARTICLE_SPEED,
				TownGuardsConstants.EFFECT_DEATH_PARTICLE_HUE,
				EffectLayer.Waist);
			this.PlaySound(TownGuardsConstants.SOUND_DEATH_PREVENTION);
				return false;
		}

		#endregion

		#region Context Menu

		/// <summary>
		/// Adds context menu entries for the guard
		/// </summary>
		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list) 
		{ 
			base.GetContextMenuEntries(from, list); 
			list.Add(new SpeechGumpEntry(from, this)); 
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Timer to check if guard should return to post after moving away
		/// </summary>
		private class WalkAwayCombatTimer : Timer
		{
			private TownGuards m_Owner;

			public WalkAwayCombatTimer(TownGuards owner) : base(TimeSpan.FromSeconds(TownGuardsConstants.TIMER_DELAY_SECONDS))
			{
				m_Owner = owner;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if (m_Owner == null || m_Owner.Deleted)
					return;

				if ((int)m_Owner.GetDistanceToSqrt(m_Owner.Home) > (m_Owner.RangeHome + TownGuardsConstants.HOME_RANGE_OFFSET))
				{
					m_Owner.PrivateOverheadMessage(MessageType.Regular, TownGuardsConstants.MESSAGE_COLOR, false, TownGuardsStringConstants.MSG_RETURNING_TO_POST, m_Owner.NetState);
					m_Owner.Location = m_Owner.Home;
					Effects.SendLocationParticles(
						EffectItem.Create(m_Owner.Location, m_Owner.Map, EffectItem.DefaultDuration),
						TownGuardsConstants.EFFECT_PARTICLE_ID,
						TownGuardsConstants.EFFECT_PARTICLE_COUNT,
						TownGuardsConstants.EFFECT_PARTICLE_SPEED,
						TownGuardsConstants.EFFECT_PARTICLE_HUE);
					Effects.PlaySound(m_Owner, m_Owner.Map, TownGuardsConstants.EFFECT_SOUND_ID);
				}

				m_Owner.m_Talked = false;
			}
		}

		/// <summary>
		/// Context menu entry for speech gump
		/// </summary>
        public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry(Mobile from, Mobile giver) : base(TownGuardsConstants.CONTEXT_MENU_ENTRY_ID, TownGuardsConstants.CONTEXT_MENU_RANGE)
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				if (!(m_Mobile is PlayerMobile))
				return;
				
				PlayerMobile mobile = (PlayerMobile)m_Mobile;
				if (!mobile.HasGump(typeof(SpeechGump)))
				{
					mobile.SendGump(new SpeechGump(TownGuardsStringConstants.GUMP_TITLE_MILITARY_CONDUCT, SpeechFunctions.SpeechText(m_Giver.Name, m_Mobile.Name, "Guard")));
				}

				List<CharacterDatabase> wanted = new List<CharacterDatabase>();
				foreach (Item item in World.Items.Values)
				{
					if (item is CharacterDatabase)
					{
						CharacterDatabase DB = (CharacterDatabase)item;
						if (DB.CharacterWanted != null && DB.CharacterWanted != "")
						{
							wanted.Add(DB);
						}
					}
				}

				if (wanted.Count > 0)
				{
					int wChoice = Utility.RandomMinMax(1, wanted.Count);
					CharacterDatabase DB = wanted[wChoice - 1];
						GuardNote note = new GuardNote();
						note.ScrollText = DB.CharacterWanted;
					m_Mobile.AddToBackpack(note);
					m_Giver.Say(TownGuardsStringConstants.MSG_CITIZEN_ALERT);
				}
			}
		}

		/// <summary>
		/// Helper class to hold bounty reward information
		/// </summary>
		private class BountyReward
		{
			public int Karma { get; set; }
			public int Gold { get; set; }
		}

		#endregion

		#region Serialization

		public TownGuards(Serial serial) : base(serial) 
		{ 
		} 

		public override void Serialize(GenericWriter writer) 
		{ 
			base.Serialize(writer); 
			writer.Write((int)0); // version 
		} 

		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize(reader); 
			int version = reader.ReadInt(); 
		} 

		#endregion
	} 
}   
