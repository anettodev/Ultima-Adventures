using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Taming BOD (Bulk Order Deed) - Contract for taming creatures.
	/// Players can add tamed pets to complete the contract and receive rewards.
	/// </summary>
	[Flipable(TamingBODConstants.ITEM_ID_NORMAL, TamingBODConstants.ITEM_ID_FLIPPED)]
	public class TamingBOD : Item
	{
		#region Fields

		private int m_Reward;
		private int m_AmountToTame;
		private int m_AmountTamed;
		private int m_Tier;
		private Type m_CreatureType;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the reward amount for completing this contract.
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public int Reward
		{
			get { return m_Reward; }
			set { m_Reward = value; }
		}
		
		/// <summary>
		/// Gets or sets the amount of creatures required to complete this contract.
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public int AmountToTame
		{
			get { return m_AmountToTame; }
			set { m_AmountToTame = value; }
		}
		
		/// <summary>
		/// Gets or sets the amount of creatures already tamed for this contract.
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public int AmountTamed
		{
			get { return m_AmountTamed; }
			set { m_AmountTamed = value; }
		}

		/// <summary>
		/// Gets or sets the contract tier (1-5).
		/// Tier 1 = Generic, Tier 2-5 = Specific creature types.
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public int Tier
		{
			get { return m_Tier; }
			set { m_Tier = value; }
		}

		/// <summary>
		/// Gets or sets the specific creature type for this contract.
		/// Null for tier 1 (generic contracts).
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public Type CreatureType
		{
			get { return m_CreatureType; }
			set { m_CreatureType = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TamingBOD class with random tier and amount.
		/// </summary>
		[Constructable]
		public TamingBOD() : base(TamingBODConstants.ITEM_ID_NORMAL)
		{
			Weight = TamingBODConstants.ITEM_WEIGHT;
			Movable = true;

			InitializeContract();
		}

		/// <summary>
		/// Initializes a new instance of the TamingBOD class with specified amount.
		/// Uses tier 1 (generic) contract.
		/// </summary>
		/// <param name="amountToTame">The amount of creatures to tame</param>
		[Constructable]
		public TamingBOD(int amountToTame) : base(TamingBODConstants.ITEM_ID_FLIPPED)
		{
			Weight = TamingBODConstants.ITEM_WEIGHT;
			Movable = true;

			m_Tier = 1;
			m_CreatureType = null;
			AmountToTame = amountToTame;
			Reward = 0;
			AmountTamed = 0;
			UpdateName();
		}

		/// <summary>
		/// Initializes a new instance of the TamingBOD class with all parameters.
		/// Uses tier 1 (generic) contract.
		/// </summary>
		/// <param name="amountTamed">The amount already tamed</param>
		/// <param name="amountToTame">The amount required to tame</param>
		/// <param name="reward">The reward amount</param>
		[Constructable]
		public TamingBOD(int amountTamed, int amountToTame, int reward) : this(amountToTame)
		{
			AmountTamed = amountTamed;
			Reward = reward;
		}

		/// <summary>
		/// Initializes a new instance of the TamingBOD class with all parameters including tier and creature type.
		/// Used when restoring contracts from TamingBODBook to preserve tier and creature type information.
		/// </summary>
		/// <param name="amountTamed">The amount already tamed</param>
		/// <param name="amountToTame">The amount required to tame</param>
		/// <param name="reward">The reward amount</param>
		/// <param name="tier">The contract tier (1-5)</param>
		/// <param name="creatureType">The creature type, or null for generic</param>
		public TamingBOD(int amountTamed, int amountToTame, int reward, int tier, Type creatureType) : this(amountToTame)
		{
			AmountTamed = amountTamed;
			Reward = reward;
			m_Tier = tier;
			m_CreatureType = creatureType;
			UpdateName(); // Update name to reflect tier and creature type
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public TamingBOD(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Serializes the TamingBOD.
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)TamingBODConstants.SERIALIZATION_VERSION);

			writer.Write(m_Reward);
			writer.Write(m_AmountToTame);
			writer.Write(m_AmountTamed);
			writer.Write(m_Tier);
			writer.Write(m_CreatureType != null ? m_CreatureType.FullName : (string)null);
		}

		/// <summary>
		/// Deserializes the TamingBOD.
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_Reward = reader.ReadInt();
			m_AmountToTame = reader.ReadInt();
			m_AmountTamed = reader.ReadInt();

			if (version >= 1)
			{
				m_Tier = reader.ReadInt();
				string creatureTypeName = reader.ReadString();
				if (creatureTypeName != null && creatureTypeName.Length > 0)
				{
					// Use FindTypeByFullName since we serialize FullName
					m_CreatureType = ScriptCompiler.FindTypeByFullName(creatureTypeName);
					if (m_CreatureType == null)
					{
						// Fallback to FindTypeByName if FullName lookup fails (for backward compatibility)
						m_CreatureType = ScriptCompiler.FindTypeByName(creatureTypeName);
					}
				}
				else
				{
					m_CreatureType = null;
				}
			}
			else
			{
				// Legacy: Default to tier 1 (generic)
				m_Tier = 1;
				m_CreatureType = null;
			}

			UpdateName();
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to open the contract gump.
		/// </summary>
		/// <param name="from">The player using the contract</param>
		public override void OnDoubleClick(Mobile from)
		{
			if (IsChildOf(from.Backpack))
			{
				from.SendGump(new TamingBODGump(from, this));
			}
			else
			{
				from.SendLocalizedMessage(1047012); // This contract must be in your backpack to use it
			}
		}

		/// <summary>
		/// Adds name properties to the property list.
		/// </summary>
		/// <param name="list">The property list</param>
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			// Add creature type property
			if (m_Tier == 1 || m_CreatureType == null)
			{
				list.Add(TamingBODStringConstants.PROPERTY_GENERIC);
			}
			else
			{
				string creatureTypeName = TamingBODCreatureTypes.GetCreatureTypeName(m_CreatureType, m_Tier);
				if (creatureTypeName != null)
				{
					list.Add(string.Format(TamingBODStringConstants.PROPERTY_CREATURE_TYPE_FORMAT, creatureTypeName));
				}
			}

			list.Add(string.Format(TamingBODStringConstants.PROPERTY_WORTH_FORMAT, Reward));
		
			// Add completion status if contract is complete
			if (m_AmountTamed >= m_AmountToTame)
			{
				list.Add(TamingBODStringConstants.PROPERTY_COMPLETE);
			}
			else 
			{
				list.Add(TamingBODStringConstants.PROPERTY_ADD_MORE);
			}
		}

		/// <summary>
		/// Pays the reward to the player when the contract is completed.
		/// </summary>
		/// <param name="from">The player receiving the reward</param>
		/// <param name="tamingBOD">The completed contract</param>
		/// <returns>True if reward was paid, false otherwise</returns>
		public static bool PayRewardTo(Mobile from, TamingBOD tamingBOD)
		{
			if (from == null || tamingBOD == null)
			{
				return false;
			}

			if (tamingBOD.AmountTamed < tamingBOD.AmountToTame)
			{
				from.SendMessage(TamingBODStringConstants.MSG_DEED_ERROR);
				return false;
			}

			Item rewardItem = GenerateRewardItem(tamingBOD.Reward);
			Container backpack = from.Backpack;

			if (backpack == null)
			{
				return false;
			}

			if (rewardItem != null)
			{
				backpack.DropItem(rewardItem);
				from.SendMessage(TamingBODStringConstants.MSG_SPECIAL_DROP);
			}

			backpack.DropItem(new BankCheck(tamingBOD.Reward));
			from.SendMessage(TamingBODStringConstants.MSG_REWARD_PLACED);

			return true;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Initializes a new contract with random tier and appropriate settings.
		/// </summary>
		private void InitializeContract()
		{
			// Random tier selection (1-5)
			m_Tier = Utility.RandomMinMax(1, 5);

			// Set creature type based on tier
			if (m_Tier == 1)
			{
				// Tier 1: Generic contract
				m_CreatureType = null;
				AmountToTame = Utility.RandomMinMax(TamingBODConstants.TIER_1_MIN, TamingBODConstants.TIER_1_MAX);
			}
			else
			{
				// Tier 2-5: Specific creature type
				m_CreatureType = TamingBODCreatureTypes.GetRandomCreatureType(m_Tier);
				
				switch (m_Tier)
				{
					case 2:
						AmountToTame = Utility.RandomMinMax(TamingBODConstants.TIER_2_MIN, TamingBODConstants.TIER_2_MAX);
						break;
					case 3:
						AmountToTame = Utility.RandomMinMax(TamingBODConstants.TIER_3_MIN, TamingBODConstants.TIER_3_MAX);
						break;
					case 4:
						AmountToTame = Utility.RandomMinMax(TamingBODConstants.TIER_4_MIN, TamingBODConstants.TIER_4_MAX);
						break;
					case 5:
						AmountToTame = Utility.RandomMinMax(TamingBODConstants.TIER_5_MIN, TamingBODConstants.TIER_5_MAX);
						break;
				}
			}

			Reward = 0;
			AmountTamed = 0;
			UpdateName();
		}

		/// <summary>
		/// Updates the contract name based on tier and creature type.
		/// </summary>
		private void UpdateName()
		{
			if (m_Tier == 1 || m_CreatureType == null)
			{
				// Generic contract
				Name = string.Format(TamingBODStringConstants.NAME_FORMAT_GENERIC, AmountToTame);
			}
			else
			{
				// Specific creature type contract
				string creatureTypeName = TamingBODCreatureTypes.GetCreatureTypeName(m_CreatureType, m_Tier);
				if (creatureTypeName != null)
				{
					Name = string.Format(TamingBODStringConstants.NAME_FORMAT_SPECIFIC, AmountToTame, creatureTypeName);
				}
				else
				{
					// Fallback to generic if name not found
					Name = string.Format(TamingBODStringConstants.NAME_FORMAT_GENERIC, AmountToTame);
				}
			}
		}

		/// <summary>
		/// Calculates the contract amount based on rarity.
		/// DEPRECATED: Use InitializeContract() instead.
		/// </summary>
		/// <returns>The calculated amount to tame</returns>
		private int CalculateContractAmount()
		{
			double rarityRoll = Utility.RandomDouble();

			if (rarityRoll >= TamingBODConstants.RARITY_THRESHOLD_ULTRA_RARE)
			{
				return Utility.RandomMinMax(TamingBODConstants.ULTRA_RARE_MIN, TamingBODConstants.ULTRA_RARE_MAX);
			}
			else if (rarityRoll >= TamingBODConstants.RARITY_THRESHOLD_VERY_RARE)
			{
				return Utility.RandomMinMax(TamingBODConstants.VERY_RARE_MIN, TamingBODConstants.VERY_RARE_MAX);
			}
			else if (rarityRoll >= TamingBODConstants.RARITY_THRESHOLD_RARE)
			{
				return Utility.RandomMinMax(TamingBODConstants.RARE_MIN, TamingBODConstants.RARE_MAX);
			}
			else
			{
				return Utility.RandomMinMax(TamingBODConstants.COMMON_MIN, TamingBODConstants.COMMON_MAX);
			}
		}

		/// <summary>
		/// Calculates the reward tier based on contract reward amount.
		/// </summary>
		/// <param name="rewardAmount">The contract reward amount</param>
		/// <returns>The reward tier (1-8)</returns>
		private static int CalculateRewardTier(int rewardAmount)
		{
			if (rewardAmount < TamingBODConstants.REWARD_THRESHOLD_TIER_1)
				return 1;
			else if (rewardAmount < TamingBODConstants.REWARD_THRESHOLD_TIER_2)
				return 2;
			else if (rewardAmount < TamingBODConstants.REWARD_THRESHOLD_TIER_3)
				return 3;
			else if (rewardAmount < TamingBODConstants.REWARD_THRESHOLD_TIER_4)
				return 4;
			else if (rewardAmount < TamingBODConstants.REWARD_THRESHOLD_TIER_5)
				return 5;
			else if (rewardAmount < TamingBODConstants.REWARD_THRESHOLD_TIER_6)
				return 6;
			else if (rewardAmount < TamingBODConstants.REWARD_THRESHOLD_TIER_7)
				return 7;
			else
				return 8;
		}

		/// <summary>
		/// Gets the reward range for a specific tier.
		/// </summary>
		/// <param name="tier">The reward tier</param>
		/// <returns>Tuple with min and max reward values</returns>
		private static Tuple<int, int> GetRewardRange(int tier)
		{
			switch (tier)
			{
				case 1:
					return new Tuple<int, int>(TamingBODConstants.TIER_1_REWARD_MIN, TamingBODConstants.TIER_1_REWARD_MAX);
				case 2:
					return new Tuple<int, int>(TamingBODConstants.TIER_2_REWARD_MIN, TamingBODConstants.TIER_2_REWARD_MAX);
				case 3:
					return new Tuple<int, int>(TamingBODConstants.TIER_3_REWARD_MIN, TamingBODConstants.TIER_3_REWARD_MAX);
				case 4:
					return new Tuple<int, int>(TamingBODConstants.TIER_4_REWARD_MIN, TamingBODConstants.TIER_4_REWARD_MAX);
				case 5:
					return new Tuple<int, int>(TamingBODConstants.TIER_5_REWARD_MIN, TamingBODConstants.TIER_5_REWARD_MAX);
				case 6:
					return new Tuple<int, int>(TamingBODConstants.TIER_6_REWARD_MIN, TamingBODConstants.TIER_6_REWARD_MAX);
				case 7:
					return new Tuple<int, int>(TamingBODConstants.TIER_7_REWARD_MIN, TamingBODConstants.TIER_7_REWARD_MAX);
				case 8:
					return new Tuple<int, int>(TamingBODConstants.TIER_8_REWARD_MIN, TamingBODConstants.TIER_8_REWARD_MAX);
				default:
					return new Tuple<int, int>(0, 0);
			}
		}

		/// <summary>
		/// Generates a reward item based on the contract reward amount.
		/// </summary>
		/// <param name="rewardAmount">The contract reward amount</param>
		/// <returns>The generated reward item, or null if no item</returns>
		private static Item GenerateRewardItem(int rewardAmount)
		{
			int tier = CalculateRewardTier(rewardAmount);
			Tuple<int, int> range = GetRewardRange(tier);
			int rewardValue = Utility.RandomMinMax(range.Item1, range.Item2);

			if (rewardValue <= TamingBODConstants.REWARD_THRESHOLD_NOTHING)
			{
				return null;
			}
			else if (rewardValue <= TamingBODConstants.REWARD_THRESHOLD_EASY)
			{
				return GenerateEasyRewardItem();
			}
			else if (rewardValue <= TamingBODConstants.REWARD_THRESHOLD_MEDIUM)
			{
				return GenerateMediumRewardItem();
			}
			else if (rewardValue <= TamingBODConstants.REWARD_THRESHOLD_RARE)
			{
				return GenerateRareRewardItem();
			}
			else if (rewardValue <= TamingBODConstants.REWARD_THRESHOLD_IMPOSSIBLE_1)
			{
				return GenerateImpossibleRewardItem(1);
			}
			else if (rewardValue <= TamingBODConstants.REWARD_THRESHOLD_IMPOSSIBLE_2)
			{
				return GenerateImpossibleRewardItem(2);
			}

			return null;
		}

		/// <summary>
		/// Generates an easy tier reward item.
		/// </summary>
		/// <returns>The generated item</returns>
		private static Item GenerateEasyRewardItem()
		{
			switch (Utility.Random(TamingBODConstants.SWITCH_EASY_RANDOM))
			{
				case 0: return new BluePetDye();
				case 1: return new GreenPetDye();
				case 2: return new OrangePetDye();
				case 3: return new PurplePetDye();
				case 4: return new RedPetDye();
				case 5: return new YellowPetDye();
				case 6: return new BlackPetDye();
				case 7: return new WhitePetDye();
				case 8: return new BloodPetDye();
				case 9: return new GoldPetDye();
				case 10: return new PinkPetDye();
				case 11: return new PowderOfTranslocation(TamingBODConstants.POWDER_QUANTITY);
				case 12: return new PetGrowthDeedWeak();
				case 13: return Construct(m_LowMorph);
				case 14: return Construct(m_LowMorph);
				default: return null;
			}
		}

		/// <summary>
		/// Generates a medium tier reward item.
		/// </summary>
		/// <returns>The generated item</returns>
		private static Item GenerateMediumRewardItem()
		{
			switch (Utility.Random(TamingBODConstants.SWITCH_MEDIUM_RANDOM))
			{
				case 0: return new PetTrainer();
				case 1: return new MossGreenPetDye();
				case 2: return new FrostBluePetDye();
				case 3: return new BlazePetDye();
				case 4: return new IceWhitePetDye();
				case 5: return new IceBluePetDye();
				case 6: return new IceGreenPetDye();
				case 7: return new PetControlDeed();
				case 8: return new PowerScroll(SkillName.AnimalLore, TamingBODConstants.POWER_SCROLL_105);
				case 9: return new PowerScroll(SkillName.AnimalTaming, TamingBODConstants.POWER_SCROLL_105);
				case 10: return new EarthyEgg();
				case 11: return new PetGrowthDeedWeak();
				case 12: return Construct(m_LowMorph);
				case 13: return Construct(m_MidMorph);
				default: return null;
			}
		}

		/// <summary>
		/// Generates a rare tier reward item.
		/// </summary>
		/// <returns>The generated item</returns>
		private static Item GenerateRareRewardItem()
		{
			switch (Utility.Random(TamingBODConstants.SWITCH_RARE_RANDOM))
			{
				case 0: return new PetEasingDeed();
				case 1: return new PetBondDeed();
				case 2: return new BallOfSummoning();
				case 3: return new BraceletOfBinding();
				case 4: return new PowerScroll(SkillName.AnimalLore, TamingBODConstants.POWER_SCROLL_110);
				case 5: return new PowerScroll(SkillName.AnimalTaming, TamingBODConstants.POWER_SCROLL_110);
				case 6: return new AlienEgg();
				case 7: return new CorruptedEgg();
				case 8: return new DragonEgg();
				case 9: return new FeyEgg();
				case 10: return new ReptilianEgg();
				case 11: return new PrehistoricEgg();
				case 12: return new PetGrowthDeedMid();
				case 13: return Construct(m_MidMorph);
				case 14: return Construct(m_RareMorph);
				default: return null;
			}
		}

		/// <summary>
		/// Generates an impossible tier reward item.
		/// </summary>
		/// <param name="tier">The impossible tier (1 or 2)</param>
		/// <returns>The generated item</returns>
		private static Item GenerateImpossibleRewardItem(int tier)
		{
			if (tier == 1)
			{
				switch (Utility.Random(TamingBODConstants.SWITCH_IMPOSSIBLE_1_RANDOM))
				{
					case 0: return new ParagonPetDeed();
					case 1: return new PetSlotDeed();
					case 2: return new PetDyeTub();
					case 3: return new PowerScroll(SkillName.AnimalLore, TamingBODConstants.POWER_SCROLL_115);
					case 4: return new PowerScroll(SkillName.AnimalTaming, TamingBODConstants.POWER_SCROLL_115);
					case 11: return new PetGrowthDeedStrong();
					case 12: return Construct(m_MegaRareMorph);
					case 13: return Construct(m_RareMorph);
					default: return null;
				}
			}
			else // tier == 2
			{
				switch (Utility.Random(TamingBODConstants.SWITCH_IMPOSSIBLE_2_RANDOM))
				{
					case 0: return new ParagonPetDeed();
					case 1: return new PetSlotDeed();
					case 2: return new PowerScroll(SkillName.AnimalLore, TamingBODConstants.POWER_SCROLL_120);
					case 3: return new PowerScroll(SkillName.AnimalTaming, TamingBODConstants.POWER_SCROLL_120);
					case 4: return Construct(m_MegaRareMorph);
					default: return null;
				}
			}
		}

		#endregion

		#region Static Data

		/// <summary>
		/// Low tier morph statue types.
		/// </summary>
		private static Type[] m_LowMorph = new Type[]
			{
			typeof(BodyChangeBlackBearStatue),
			typeof(BodyChangeBrownBearStatue),
			typeof(BodyChangeCatStatue),
			typeof(BodyChangeChickenStatue),
			typeof(BodyChangeCowStatue),
			typeof(BodyChangeDogStatue),
			typeof(BodyChangeEagleStatue),
			typeof(BodyChangeFoxStatue),
			typeof(BodyChangeGiantRatStatue),
			typeof(BodyChangeGoatStatue),
			typeof(BodyChangeGorillaStatue),
			typeof(BodyChangeHindStatue),
			typeof(BodyChangeLlamaStatue),
			typeof(BodyChangeOstardStatue),
			typeof(BodyChangePantherStatue),
			typeof(BodyChangePigStatue),
			typeof(BodyChangeRabbitStatue),
			typeof(BodyChangeRatStatue),
			typeof(BodyChangeSheepStatue),
			typeof(BodyChangeSquirrelStatue),
			typeof(BodyChangeStagStatue),
			typeof(BodyChangeLizardStatue)
		};

		/// <summary>
		/// Mid tier morph statue types.
		/// </summary>
		private static Type[] m_MidMorph = new Type[]
			{
			typeof(BodyChangeCraneStatue),
			typeof(BodyChangeFerretStatue),
			typeof(BodyChangeGiantSnakeStatue),
			typeof(BodyChangeGiantToadStatue),
			typeof(BodyChangeHellHoundStatue),
			typeof(BodyChangeKirinStatue),
			typeof(BodyChangeLionStatue),
			typeof(BodyChangePolarBearStatue),
			typeof(BodyChangeScorpionStatue),
			typeof(BodyChangeSlimeStatue),
			typeof(BodyChangeSnakeStatue),
			typeof(BodyChangeSpiderStatue),
			typeof(BodyChangeToadStatue),
			typeof(BodyChangeKongStatue),
			typeof(BodyChangeTurtleStatue)
		};

		/// <summary>
		/// Rare tier morph statue types.
		/// </summary>
		private static Type[] m_RareMorph = new Type[]
			{
			typeof(BodyChangeFrenziedOstardStatue),
			typeof(BodyChangeFrostSpiderStatue),
			typeof(BodyChangeGremlinStatue),
			typeof(BodyChangeMysticalFoxStatue),
			typeof(BodyChangePlainsBeastStatue),
			typeof(BodyChangeRockLobsterStatue),
			typeof(BodyChangeShadowLionStatue),
			typeof(BodyChangeTigerBeetleStatue),
			typeof(BodyChangeWidowSpiderStatue),
			typeof(BodyChangeScorpoidStatue),
			typeof(BodyChangeFlyingFangsStatue),
                typeof(BodyChangeGryphonStatue)
			};

		/// <summary>
		/// Mega rare tier morph statue types.
		/// </summary>
		private static Type[] m_MegaRareMorph = new Type[]
			{
			typeof(BodyChangeCerberusStatue),
			typeof(BodyChangeDepthsBeastStatue),
			typeof(BodyChangeGazerHoundStatue),
			typeof(BodyChangeGlassSpiderStatue),
			typeof(BodyChangeHornedBeetleStatue),
			typeof(BodyChangeMagmaHoundStatue),
			typeof(BodyChangeRaptorStatue),
			typeof(BodyChangeRuneBearStatue),
			typeof(BodyChangeStalkerStatue),
			typeof(BodyChangeVerminBeastStatue),
			typeof(BodyChangeWeaver)
		};

		#endregion

		#region Static Helper Methods

		/// <summary>
		/// Constructs a random item from a type array.
		/// </summary>
		/// <param name="types">The array of types</param>
		/// <returns>The constructed item, or null if failed</returns>
		public static Item Construct(Type[] types)
		{
			if (types != null && types.Length > 0)
			{
				return Construct(types, Utility.Random(types.Length));
			}

			return null;
		}

		/// <summary>
		/// Constructs an item from a type array at a specific index.
		/// </summary>
		/// <param name="types">The array of types</param>
		/// <param name="index">The index to use</param>
		/// <returns>The constructed item, or null if failed</returns>
		public static Item Construct(Type[] types, int index)
		{
			if (types != null && index >= 0 && index < types.Length)
			{
				return Construct(types[index]);
			}

			return null;
		}

		/// <summary>
		/// Constructs an item from a specific type.
		/// </summary>
		/// <param name="type">The type to construct</param>
		/// <returns>The constructed item, or null if failed</returns>
		public static Item Construct(Type type)
		{
			if (type == null)
			{
				return null;
			}

			try
			{
				return Activator.CreateInstance(type) as Item;
			}
			catch
			{
				return null;
			}
		}

		#endregion
	}
}
