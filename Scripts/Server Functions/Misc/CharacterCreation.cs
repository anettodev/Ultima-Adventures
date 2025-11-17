using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Accounting;
using System.Collections.Generic;

namespace Server.Misc
{
	/// <summary>
	/// Handles character creation process including stats, skills, equipment, and initial setup.
	/// </summary>
	public class CharacterCreation
	{
		#region Constants

		/// <summary>Default name assigned when name validation fails or name is duplicate.</summary>
		public const string GENERIC_NAME = "Generic Player";

		// Character stat constants
		private const int STAT_MIN = 10;
		private const int STAT_MAX = 60;
		private const int STAT_BASE = 10;
		private const int STAT_ADJUSTMENT = 30;
		private const int STAT_MULTIPLIER = 2;

		// Skill constants
		private const int SKILL_MAX_VALUE = 50;
		private const int SKILL_TOTAL_MAX = 150;
		private const int SKILL_BASE_POINTS = 10;

		// Hue constants
		private const int HUE_MASK = 0x3FFF;
		private const int HUE_OFFSET = 0x8000;
		private const int HUE_CORRECTION_THRESHOLD = 33770;
		private const int HUE_CORRECTION_VALUE = 32768;

		// Starting values
		private const int DEFAULT_HUNGER = 20;
		private const int DEFAULT_THIRST = 20;
		private const int STARTING_GOLD = 10000;

		// Starting location
		private static readonly Point3D START_LOCATION = new Point3D(2008, 1316, 0);
		private static readonly Map START_MAP = Map.Malas;

		// Profession limits
		private const int PROFESSION_BASE_LIMIT = 4;
		private const int PROFESSION_AOS_LIMIT = 6;
		private const int PROFESSION_SE_LIMIT = 8;

		#endregion

		#region Initialization

		/// <summary>
		/// Registers the character creation event handler.
		/// </summary>
		public static void Initialize()
		{
			EventSink.CharacterCreated += new CharacterCreatedEventHandler(EventSink_CharacterCreated);
		}

		#endregion

		#region Main Event Handler

		/// <summary>
		/// Main event handler for character creation. Orchestrates the entire character setup process.
		/// </summary>
		private static void EventSink_CharacterCreated(CharacterCreatedEventArgs args)
		{
			if (!VerifyProfession(args.Profession))
				args.Profession = 0;

			NetState state = args.State;
			if (state == null)
				return;

			Mobile newChar = CreateCharacterInstance(args.Account as Account);
			if (newChar == null)
			{
				Console.WriteLine("Login: {0}: Character creation failed, account full", state);
				return;
			}

			args.Mobile = newChar;

			ConfigureCharacterProperties(newChar, args, state);
			SetupCharacterAppearance(newChar, args);
			EquipStartingItems(newChar, args);
			SetStatsAndSkills(newChar, args, state);
			FinalizeCharacterCreation(newChar, state, args.Account.Username);
		}

		#endregion

		#region Character Instance Creation

		/// <summary>
		/// Creates a new PlayerMobile instance and assigns it to an account slot.
		/// </summary>
		private static Mobile CreateCharacterInstance(Account account)
		{
			if (account == null || account.Count >= account.Limit)
				return null;

			for (int i = 0; i < account.Length; ++i)
			{
				if (account[i] == null)
					return (account[i] = new PlayerMobile());
			}

			return null;
		}

		#endregion

		#region Character Configuration

		/// <summary>
		/// Configures basic character properties including stats, access level, race, and PlayerMobile-specific settings.
		/// </summary>
		private static void ConfigureCharacterProperties(Mobile newChar, CharacterCreatedEventArgs args, NetState state)
		{
			newChar.Player = true;
			newChar.StatCap = MyServerSettings.newstatcap();
			newChar.Skills.Cap = MyServerSettings.skillcap();
			newChar.AccessLevel = args.Account.AccessLevel;
			newChar.Female = args.Female;
			newChar.Race = Race.Human;

			// Configure hue with correction for invalid values
			newChar.Hue = newChar.Race.ClipSkinHue(args.Hue & HUE_MASK) | HUE_OFFSET;
			if (newChar.Hue >= HUE_CORRECTION_THRESHOLD)
				newChar.Hue = newChar.Hue - HUE_CORRECTION_VALUE;

			// Set default hunger and thirst
			newChar.Hunger = DEFAULT_HUNGER;
			newChar.Thirst = DEFAULT_THIRST;

			// Configure PlayerMobile-specific properties
			if (newChar is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)newChar;
				pm.Avatar = true;
				pm.SoulBound = false;
				pm.Young = false;
				pm.BalanceStatus = 0;
			}

			// Set character name
			CharacterNamingHelper.SetName(newChar, args.Name);
		}

		/// <summary>
		/// Sets up character appearance including hair and facial hair.
		/// </summary>
		private static void SetupCharacterAppearance(Mobile newChar, CharacterCreatedEventArgs args)
		{
			Race race = newChar.Race;

			if (race.ValidateHair(newChar, args.HairID))
			{
				newChar.HairItemID = args.HairID;
				newChar.HairHue = race.ClipHairHue(args.HairHue & HUE_MASK);
			}

			if (race.ValidateFacialHair(newChar, args.BeardID))
			{
				newChar.FacialHairItemID = args.BeardID;
				newChar.FacialHairHue = race.ClipHairHue(args.BeardHue & HUE_MASK);
			}
		}

		/// <summary>
		/// Equips starting items including backpack contents, clothing, and character database.
		/// </summary>
		private static void EquipStartingItems(Mobile newChar, CharacterCreatedEventArgs args)
		{
			CharacterEquipmentHelper.SetupBackpack(newChar);
			CharacterEquipmentHelper.EquipClothing(newChar, args.ShirtHue, args.PantsHue);
		}

		/// <summary>
		/// Sets character stats and skills based on creation parameters.
		/// </summary>
		private static void SetStatsAndSkills(Mobile newChar, CharacterCreatedEventArgs args, NetState state)
		{
			CharacterStatsHelper.SetStats(newChar, state, args.Str, args.Dex, args.Int);

			// Set resource pools based on stats
			newChar.Mana = args.Int * STAT_MULTIPLIER;
			newChar.Hits = args.Str * STAT_MULTIPLIER;
			newChar.Stam = args.Dex * STAT_MULTIPLIER;

			CharacterSkillsHelper.SetSkills(newChar, args.Skills, args.Profession);
		}

		/// <summary>
		/// Finalizes character creation by moving to starting location and starting welcome timer.
		/// </summary>
		private static void FinalizeCharacterCreation(Mobile newChar, NetState state, string username)
		{
			newChar.MoveToWorld(START_LOCATION, START_MAP);
			Console.WriteLine("Login: {0}: New character being created (account={1})", state, username);
			new WelcomeTimer(newChar).Start();
		}

		#endregion

		#region Profession Validation

		/// <summary>
		/// Verifies if the given profession ID is valid based on server expansion support.
		/// </summary>
		public static bool VerifyProfession(int profession)
		{
			if (profession < 0)
				return false;

			if (profession < PROFESSION_BASE_LIMIT)
				return true;

			if (Core.AOS && profession < PROFESSION_AOS_LIMIT)
				return true;

			if (Core.SE && profession < PROFESSION_SE_LIMIT)
				return true;

			return false;
		}

		#endregion

		#region Public Name Validation

		/// <summary>
		/// Public method for checking name duplication. Used by other systems for name validation.
		/// </summary>
		public static bool CheckDupe(Mobile m, string name)
		{
			return CharacterNamingHelper.CheckDupe(m, name);
		}

		#endregion

		#region Helper Classes

		/// <summary>
		/// Helper class for handling character equipment and clothing.
		/// </summary>
		private static class CharacterEquipmentHelper
		{
			/// <summary>
			/// Sets up the character's backpack with starting items and creates character database.
			/// </summary>
			public static void SetupBackpack(Mobile m)
			{
				Container pack = m.Backpack;
				if (pack == null)
				{
					pack = new Backpack();
					pack.Movable = false;
					m.AddItem(pack);
				}

				// Add starting items to backpack
				ItemHelper.PackItem(m, new Spellbook());
				ItemHelper.PackItem(m, new LoreGuidetoAdventure());
				ItemHelper.PackItem(m, new Dagger());
				ItemHelper.PackItem(m, new Gold(STARTING_GOLD));
				ItemHelper.PackItem(m, new Waterskin());
				ItemHelper.PackItem(m, new Torch());
				ItemHelper.PackItem(m, new StartCombatsRandomStudyBook());
				ItemHelper.PackItem(m, new StartWorkRandomStudyBook());

				// Create and place character database in bank
				CharacterDatabase characterDB = new CharacterDatabase();
				characterDB.CharacterOwner = m;
				m.BankBox.DropItem(characterDB);
			}

			/// <summary>
			/// Equips starting clothing (shirt, pants, shoes) on the character.
			/// </summary>
			public static void EquipClothing(Mobile m, int shirtHue, int pantsHue)
			{
				EquipShirt(m, shirtHue);
				EquipPants(m, pantsHue);
				EquipShoes(m);
			}

			/// <summary>
			/// Equips a random shirt type with the specified hue.
			/// </summary>
			private static void EquipShirt(Mobile m, int shirtHue)
			{
				int hue = Utility.ClipDyedHue(shirtHue & HUE_MASK);
				Item[] shirtTypes = new Item[]
				{
					new Shirt(hue),
					new FancyShirt(hue),
					new Doublet(hue)
				};

				Item shirt = ItemHelper.SelectRandom(shirtTypes);
				ItemHelper.EquipItem(m, shirt, true);
			}

			/// <summary>
			/// Equips gender-appropriate pants with the specified hue.
			/// </summary>
			private static void EquipPants(Mobile m, int pantsHue)
			{
				int hue = Utility.ClipDyedHue(pantsHue & HUE_MASK);
				Item[] pantsTypes;

				if (m.Female)
				{
					pantsTypes = new Item[]
					{
						new Skirt(hue),
						new Kilt(hue)
					};
				}
				else
				{
					pantsTypes = new Item[]
					{
						new LongPants(hue),
						new ShortPants(hue)
					};
				}

				Item pants = ItemHelper.SelectRandom(pantsTypes);
				ItemHelper.EquipItem(m, pants, true);
			}

			/// <summary>
			/// Equips shoes with a random yellow hue.
			/// </summary>
			private static void EquipShoes(Mobile m)
			{
				Item shoes = new Shoes(Utility.RandomYellowHue());
				ItemHelper.EquipItem(m, shoes, true);
			}
		}

		/// <summary>
		/// Helper class for handling character stats calculation and validation.
		/// </summary>
		private static class CharacterStatsHelper
		{
			/// <summary>
			/// Sets character stats after validation and normalization.
			/// </summary>
			public static void SetStats(Mobile m, NetState state, int str, int dex, int intel)
			{
				int max = state.NewCharacterCreation ? 90 : 80;
				FixStats(ref str, ref dex, ref intel, max);

				// Validate stats are within acceptable range
				if (str < STAT_MIN || str > STAT_MAX ||
					dex < STAT_MIN || dex > STAT_MAX ||
					intel < STAT_MIN || intel > STAT_MAX ||
					(str + dex + intel) != max)
				{
					str = STAT_BASE;
					dex = STAT_BASE;
					intel = STAT_BASE;
				}

				m.InitStats(str, dex, intel);
			}

			/// <summary>
			/// Normalizes stats to ensure they sum to the maximum allowed value.
			/// </summary>
			private static void FixStats(ref int str, ref int dex, ref int intel, int max)
			{
				int vMax = max - STAT_ADJUSTMENT;

				int vStr = Math.Max(0, str - STAT_BASE);
				int vDex = Math.Max(0, dex - STAT_BASE);
				int vInt = Math.Max(0, intel - STAT_BASE);

				int total = vStr + vDex + vInt;

				if (total == 0 || total == vMax)
					return;

				// Scale values to fit within maximum
				double scalar = vMax / (double)total;

				vStr = (int)(vStr * scalar);
				vDex = (int)(vDex * scalar);
				vInt = (int)(vInt * scalar);

				// Fix any rounding differences
				FixStat(ref vStr, (vStr + vDex + vInt) - vMax, vMax);
				FixStat(ref vDex, (vStr + vDex + vInt) - vMax, vMax);
				FixStat(ref vInt, (vStr + vDex + vInt) - vMax, vMax);

				str = vStr + STAT_BASE;
				dex = vDex + STAT_BASE;
				intel = vInt + STAT_BASE;
			}

			/// <summary>
			/// Adjusts a single stat value to ensure it stays within bounds.
			/// </summary>
			private static void FixStat(ref int stat, int diff, int max)
			{
				stat += diff;

				if (stat < 0)
					stat = 0;
				else if (stat > max)
					stat = max;
			}
		}

		/// <summary>
		/// Helper class for handling character skills setup and validation.
		/// </summary>
		private static class CharacterSkillsHelper
		{
			/// <summary>
			/// Dictionary mapping profession IDs to their starting skill sets.
			/// </summary>
			private static readonly Dictionary<int, SkillNameValue[]> ProfessionSkills = new Dictionary<int, SkillNameValue[]>
			{
				{ 1, new SkillNameValue[] { new SkillNameValue(SkillName.Anatomy, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Tactics, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Swords, SKILL_MAX_VALUE) } }, // Warrior
				{ 2, new SkillNameValue[] { new SkillNameValue(SkillName.Inscribe, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Magery, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Meditation, SKILL_MAX_VALUE) } }, // Magician
				{ 3, new SkillNameValue[] { new SkillNameValue(SkillName.ArmsLore, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Mining, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Blacksmith, SKILL_MAX_VALUE) } }, // Blacksmith
				{ 4, new SkillNameValue[] { new SkillNameValue(SkillName.Necromancy, SKILL_MAX_VALUE), new SkillNameValue(SkillName.SpiritSpeak, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Meditation, SKILL_MAX_VALUE) } }, // Necromancer
				{ 5, new SkillNameValue[] { new SkillNameValue(SkillName.Chivalry, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Swords, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Focus, SKILL_MAX_VALUE) } }, // Paladin
				{ 6, new SkillNameValue[] { new SkillNameValue(SkillName.Bushido, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Swords, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Tactics, SKILL_MAX_VALUE) } }, // Samurai
				{ 7, new SkillNameValue[] { new SkillNameValue(SkillName.Ninjitsu, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Hiding, SKILL_MAX_VALUE), new SkillNameValue(SkillName.Fencing, SKILL_MAX_VALUE) } }  // Ninja
			};

			/// <summary>
			/// Sets character skills based on profession or custom skill selection.
			/// </summary>
			public static void SetSkills(Mobile m, SkillNameValue[] skills, int profession)
			{
				// Use profession skills if available, otherwise use custom skills
				if (ProfessionSkills.ContainsKey(profession))
				{
					skills = ProfessionSkills[profession];
				}
				else
				{
					if (!ValidateSkills(skills))
						return;
				}

				// Apply skills to character
				for (int i = 0; i < skills.Length; ++i)
				{
					SkillNameValue snv = skills[i];

					// Skip invalid skills or restricted skills (unless Ninja profession for Stealth)
					if (snv.Value > 0 &&
						(snv.Name != SkillName.Stealth || profession == 7) &&
						snv.Name != SkillName.RemoveTrap &&
						snv.Name != SkillName.Spellweaving)
					{
						Skill skill = m.Skills[snv.Name];
						if (skill != null)
						{
							skill.BaseFixedPoint = snv.Value * SKILL_BASE_POINTS;
						}
					}
				}
			}

			/// <summary>
			/// Validates that custom skill selection is within acceptable limits.
			/// </summary>
			private static bool ValidateSkills(SkillNameValue[] skills)
			{
				if (skills == null)
					return false;

				int total = 0;
				HashSet<SkillName> usedSkills = new HashSet<SkillName>();

				for (int i = 0; i < skills.Length; ++i)
				{
					SkillNameValue snv = skills[i];

					// Check individual skill value range
					if (snv.Value < 0 || snv.Value > SKILL_MAX_VALUE)
						return false;

					// Check for duplicate skills
					if (snv.Value > 0 && usedSkills.Contains(snv.Name))
						return false;

					if (snv.Value > 0)
						usedSkills.Add(snv.Name);

					total += snv.Value;
				}

				return total <= SKILL_TOTAL_MAX;
			}
		}

		/// <summary>
		/// Helper class for handling character name validation and duplication checks.
		/// </summary>
		private static class CharacterNamingHelper
		{
			/// <summary>
			/// Sets the character name after validation and duplicate checking.
			/// </summary>
			public static void SetName(Mobile m, string name)
			{
				name = name.Trim();

				if (!CheckDupe(m, name))
					m.Name = GENERIC_NAME;
				else
					m.Name = name;
			}

			/// <summary>
			/// Checks if a name is valid and not already in use by another player.
			/// </summary>
			public static bool CheckDupe(Mobile m, string name)
			{
				if (m == null || name == null || name.Length == 0)
					return false;

				name = name.Trim();

				// Validate name format
				if (!NameVerification.Validate(name, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote))
					return false;

				// Check for duplicate names among player mobiles
				foreach (Mobile wm in World.Mobiles.Values)
				{
					if (wm != m && !wm.Deleted && wm is PlayerMobile && Insensitive.Equals(wm.RawName, name))
						return false;
				}

				return true;
			}
		}

		/// <summary>
		/// Generic helper class for item operations.
		/// </summary>
		private static class ItemHelper
		{
			/// <summary>
			/// Selects a random item from an array.
			/// </summary>
			public static T SelectRandom<T>(T[] items) where T : class
			{
				if (items == null || items.Length == 0)
					return null;

				return items[Utility.Random(items.Length)];
			}

			/// <summary>
			/// Attempts to equip an item on a mobile, falling back to backpack if equip fails.
			/// </summary>
			public static void EquipItem(Mobile m, Item item, bool mustEquip)
			{
				if (m == null || item == null)
					return;

				if (!Core.AOS)
					item.LootType = LootType.Newbied;

				// Try to equip the item
				if (m.EquipItem(item))
					return;

				// If equip failed and not required, try to pack it
				Container pack = m.Backpack;
				if (!mustEquip && pack != null)
				{
					pack.DropItem(item);
				}
				else
				{
					item.Delete();
				}
			}

			/// <summary>
			/// Adds an item to the mobile's backpack, marking it as blessed.
			/// </summary>
			public static void PackItem(Mobile m, Item item)
			{
				if (m == null || item == null)
					return;

				item.LootType = LootType.Blessed;

				Container pack = m.Backpack;
				if (pack != null)
					pack.DropItem(item);
				else
					item.Delete();
			}
		}

		#endregion
	}
}
