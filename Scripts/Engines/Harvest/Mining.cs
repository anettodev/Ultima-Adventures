using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Misc;

namespace Server.Engines.Harvest
{
	/// <summary>
	/// Mining harvest system for extracting ore, stone, and sand from the world.
	/// Supports various ore types with skill-based availability and bonus gem drops.
	/// </summary>
	public class Mining : HarvestSystem
	{
		private static Mining m_System;

	/// <summary>
	/// Gets the singleton instance of the mining system
	/// </summary>
	public static Mining System
	{
		get
		{
			if (m_System == null)
				m_System = new Mining();

			return m_System;
		}
	}

		private HarvestDefinition m_OreAndStone, m_Sand;

	/// <summary>
	/// Gets the harvest definition for ore and stone mining
	/// </summary>
	public HarvestDefinition OreAndStone
	{
		get { return m_OreAndStone; }
	}

	/// <summary>
	/// Gets the harvest definition for sand mining
	/// </summary>
	public HarvestDefinition Sand
	{
		get { return m_Sand; }
	}

		/// <summary>
		/// Initializes the mining system with ore/stone and sand harvest definitions
		/// </summary>
	public Mining()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region Mining for ore and stone
			HarvestDefinition oreAndStone = m_OreAndStone = new HarvestDefinition();

			// Resource banks are every 3x3 tiles
			oreAndStone.BankWidth = HarvestConstants.ORE_BANK_WIDTH;
			oreAndStone.BankHeight = HarvestConstants.ORE_BANK_HEIGHT;

			// Every bank holds from 5-30 ore
			oreAndStone.MinTotal = HarvestConstants.ORE_BANK_MIN_TOTAL;
			oreAndStone.MaxTotal = HarvestConstants.ORE_BANK_MAX_TOTAL;

			// A resource bank will respawn its content every 15 to 30 minutes
			oreAndStone.MinRespawn = TimeSpan.FromMinutes(HarvestConstants.ORE_RESPAWN_MIN_MINUTES);
			oreAndStone.MaxRespawn = TimeSpan.FromMinutes(HarvestConstants.ORE_RESPAWN_MAX_MINUTES);

			// Skill checking is done on the Mining skill
			oreAndStone.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
			oreAndStone.Tiles = m_MountainAndCaveTiles;

			// Players must be within 2 tiles to harvest
			oreAndStone.MaxRange = HarvestConstants.ORE_MAX_RANGE;

			// One ore per harvest action
			oreAndStone.ConsumedPerHarvest = HarvestConstants.ORE_CONSUMED_PER_HARVEST;
			oreAndStone.ConsumedPerFeluccaHarvest = HarvestConstants.ORE_CONSUMED_PER_HARVEST;

			// The digging effect
			oreAndStone.EffectActions = HarvestConstants.ORE_EFFECT_ACTIONS;
			oreAndStone.EffectSounds = HarvestConstants.ORE_EFFECT_SOUNDS;
			oreAndStone.EffectCounts = HarvestConstants.ORE_EFFECT_COUNTS;
			oreAndStone.EffectDelay = TimeSpan.FromSeconds(HarvestConstants.ORE_EFFECT_DELAY);
			oreAndStone.EffectSoundDelay = TimeSpan.FromSeconds(HarvestConstants.ORE_EFFECT_SOUND_DELAY);

			oreAndStone.NoResourcesMessage = HarvestConstants.MSG_NO_METAL_HERE;
			oreAndStone.DoubleHarvestMessage = HarvestConstants.MSG_METAL_TAKEN;
			oreAndStone.TimedOutOfRangeMessage = HarvestConstants.MSG_TOO_FAR_AWAY;
			oreAndStone.OutOfRangeMessage = HarvestConstants.MSG_TOO_FAR_AWAY;
			oreAndStone.FailMessage = HarvestConstants.MSG_FAILED_FIND_ORE;
			oreAndStone.PackFullMessage = HarvestConstants.MSG_BACKPACK_FULL;
			oreAndStone.ToolBrokeMessage = HarvestConstants.MSG_TOOL_WORN_OUT;

			res = new HarvestResource[]
				{
					new HarvestResource(HarvestConstants.IRON_SKILL_MIN, HarvestConstants.IRON_SKILL_MIN, HarvestConstants.IRON_SKILL_MAX, HarvestStringConstants.MSG_FOUND_IRON_ORE, typeof(IronOre), typeof(Granite)),
					new HarvestResource(HarvestConstants.DULL_COPPER_SKILL_MIN, HarvestConstants.DULL_COPPER_SKILL_MIN, HarvestConstants.DULL_COPPER_SKILL_MAX, HarvestStringConstants.MSG_FOUND_DULL_COPPER_ORE, typeof(DullCopperOre), typeof(DullCopperGranite), typeof(DullCopperElemental)),
					new HarvestResource(HarvestConstants.COPPER_SKILL_MIN, HarvestConstants.COPPER_SKILL_MIN, HarvestConstants.COPPER_SKILL_MAX, HarvestStringConstants.MSG_FOUND_COPPER_ORE, typeof(CopperOre), typeof(CopperGranite), typeof(CopperElemental)),
					new HarvestResource(HarvestConstants.BRONZE_SKILL_MIN, HarvestConstants.BRONZE_SKILL_MIN, HarvestConstants.BRONZE_SKILL_MAX, HarvestStringConstants.MSG_FOUND_BRONZE_ORE, typeof(BronzeOre), typeof(BronzeGranite), typeof(BronzeElemental)),
					new HarvestResource(HarvestConstants.SHADOW_IRON_SKILL_MIN, HarvestConstants.SHADOW_IRON_SKILL_MIN, HarvestConstants.SHADOW_IRON_SKILL_MAX, HarvestStringConstants.MSG_FOUND_SHADOW_IRON_ORE, typeof(ShadowIronOre), typeof(ShadowIronGranite), typeof(ShadowIronElemental)),
					new HarvestResource(HarvestConstants.PLATINUM_SKILL_MIN, HarvestConstants.PLATINUM_SKILL_MIN, HarvestConstants.PLATINUM_SKILL_MAX, HarvestStringConstants.MSG_FOUND_PLATINUM_ORE, typeof(PlatinumOre), typeof(PlatinumGranite), typeof(EarthElemental)),
					new HarvestResource(HarvestConstants.GOLD_SKILL_MIN, HarvestConstants.GOLD_SKILL_MIN, HarvestConstants.GOLD_SKILL_MAX, HarvestStringConstants.MSG_FOUND_GOLD_ORE, typeof(GoldOre), typeof(GoldGranite), typeof(GoldenElemental)),
					new HarvestResource(HarvestConstants.AGAPITE_SKILL_MIN, HarvestConstants.AGAPITE_SKILL_MIN, HarvestConstants.AGAPITE_SKILL_MAX, HarvestStringConstants.MSG_FOUND_AGAPITE_ORE, typeof(AgapiteOre), typeof(AgapiteGranite), typeof(AgapiteElemental)),
					new HarvestResource(HarvestConstants.VERITE_SKILL_MIN, HarvestConstants.VERITE_SKILL_MIN, HarvestConstants.VERITE_SKILL_MAX, HarvestStringConstants.MSG_FOUND_VERITE_ORE, typeof(VeriteOre), typeof(VeriteGranite), typeof(VeriteElemental)),
					new HarvestResource(HarvestConstants.VALORITE_SKILL_MIN, HarvestConstants.VALORITE_SKILL_MIN, HarvestConstants.VALORITE_SKILL_MAX, HarvestStringConstants.MSG_FOUND_VALORITE_ORE, typeof(ValoriteOre), typeof(ValoriteGranite), typeof(ValoriteElemental)),
					new HarvestResource(HarvestConstants.TITANIUM_SKILL_MIN, HarvestConstants.TITANIUM_SKILL_MIN, HarvestConstants.TITANIUM_SKILL_MAX, HarvestStringConstants.MSG_FOUND_TITANIUM_ORE, typeof(TitaniumOre), typeof(TitaniumGranite), typeof(EarthElemental)),
					new HarvestResource(HarvestConstants.ROSENIUM_SKILL_MIN, HarvestConstants.ROSENIUM_SKILL_MIN, HarvestConstants.ROSENIUM_SKILL_MAX, HarvestStringConstants.MSG_FOUND_ROSENIUM_ORE, typeof(RoseniumOre), typeof(RoseniumGranite), typeof(EarthElemental))
				};
			// the sum chance Needs to be 100%
			veins = new HarvestVein[]
				{
					new HarvestVein(HarvestConstants.IRON_VEIN_CHANCE, 0.0, res[0], null), // Iron
					new HarvestVein(HarvestConstants.DULL_COPPER_VEIN_CHANCE, HarvestConstants.DULL_COPPER_RARITY, res[1], res[0]), // Dull Copper
					new HarvestVein(HarvestConstants.COPPER_VEIN_CHANCE, HarvestConstants.COPPER_RARITY, res[2], res[0]), // Copper
					new HarvestVein(HarvestConstants.BRONZE_VEIN_CHANCE, HarvestConstants.BRONZE_RARITY, res[3], res[0]), // Bronze
					new HarvestVein(HarvestConstants.SHADOW_IRON_VEIN_CHANCE, HarvestConstants.SHADOW_IRON_RARITY, res[4], res[0]), // Shadow Iron
					new HarvestVein(HarvestConstants.PLATINUM_VEIN_CHANCE, HarvestConstants.PLATINUM_RARITY, res[5], res[0]), // Platinum
					new HarvestVein(HarvestConstants.GOLD_VEIN_CHANCE, HarvestConstants.GOLD_RARITY, res[6], res[0]), // Gold
					new HarvestVein(HarvestConstants.AGAPITE_VEIN_CHANCE, HarvestConstants.AGAPITE_RARITY, res[7], res[0]), // Agapite
					new HarvestVein(HarvestConstants.VERITE_VEIN_CHANCE, HarvestConstants.VERITE_RARITY, res[8], res[0]), // Verite
					new HarvestVein(HarvestConstants.VALORITE_VEIN_CHANCE, HarvestConstants.VALORITE_RARITY, res[9], res[0]), // Valorite
					new HarvestVein(HarvestConstants.TITANIUM_VEIN_CHANCE, HarvestConstants.TITANIUM_RARITY, res[10], res[0]), // Titanium
					new HarvestVein(HarvestConstants.ROSENIUM_VEIN_CHANCE, HarvestConstants.ROSENIUM_RARITY, res[11], res[0]) // Rosenium
				};

			oreAndStone.Resources = res;
			oreAndStone.Veins = veins;

            //PlayerMobile pm = from as PlayerMobile;
            //TreasureMap map = new TreasureMap(1, pm.Map, pm.Location, pm.X, pm.Y);

			oreAndStone.BonusResources = new BonusHarvestResource[]
			{
				new BonusHarvestResource(0, HarvestConstants.BONUS_NOTHING_CHANCE, null, null), // Nothing
				new BonusHarvestResource(60, HarvestConstants.BONUS_SCROLL_CHANCE, 1074542, typeof(BlankScroll)),
				new BonusHarvestResource(60, HarvestConstants.BONUS_LOCAL_MAP_CHANCE, 1074542, typeof(LocalMap)),
				new BonusHarvestResource(60, HarvestConstants.BONUS_INDECIPHERABLE_MAP_CHANCE, 1074542, typeof(IndecipherableMap)),
				new BonusHarvestResource(60, HarvestConstants.BONUS_BLANK_MAP_CHANCE, 1074542, typeof(BlankMap)),
				new BonusHarvestResource(70, HarvestConstants.BONUS_AMBER_CHANCE, 1074542, typeof(Amber)),
				new BonusHarvestResource(75, HarvestConstants.BONUS_AMETHYST_CHANCE, 1074542, typeof(Amethyst)),
				new BonusHarvestResource(75, HarvestConstants.BONUS_CITRINE_CHANCE, 1074542, typeof(Citrine)),
				new BonusHarvestResource(80, HarvestConstants.BONUS_DIAMOND_CHANCE, 1074542, typeof(Diamond)),
				new BonusHarvestResource(85, HarvestConstants.BONUS_EMERALD_CHANCE, 1074542, typeof(Emerald)),
				new BonusHarvestResource(85, HarvestConstants.BONUS_RUBY_CHANCE, 1074542, typeof(Ruby)),
				new BonusHarvestResource(85, HarvestConstants.BONUS_SAPPHIRE_CHANCE, 1074542, typeof(Sapphire)),
				new BonusHarvestResource(90, HarvestConstants.BONUS_STAR_SAPPHIRE_CHANCE, 1074542, typeof(StarSapphire)),
				new BonusHarvestResource(90, HarvestConstants.BONUS_TOURMALINE_CHANCE, 1074542, typeof(Tourmaline)),
				new BonusHarvestResource(100, HarvestConstants.BONUS_BLUE_DIAMOND_CHANCE, 1072562, typeof(BlueDiamond)),
				new BonusHarvestResource(100, HarvestConstants.BONUS_DARK_SAPPHIRE_CHANCE, 1072567, typeof(DarkSapphire)),
				new BonusHarvestResource(100, HarvestConstants.BONUS_ECRU_CITRINE_CHANCE, 1072570, typeof(EcruCitrine)),
				new BonusHarvestResource(100, HarvestConstants.BONUS_FIRE_RUBY_CHANCE, 1072564, typeof(FireRuby)),
				new BonusHarvestResource(100, HarvestConstants.BONUS_PERFECT_EMERALD_CHANCE, 1072566, typeof(PerfectEmerald))
			};

            oreAndStone.RaceBonus = false;//Core.ML;
			oreAndStone.RandomizeVeins = true;//Core.ML;

			Definitions.Add( oreAndStone );
			#endregion

			#region Mining for sand
			HarvestDefinition sand = m_Sand = new HarvestDefinition();

			// Resource banks are every 3x3 tiles
			sand.BankWidth = HarvestConstants.SAND_BANK_WIDTH;
			sand.BankHeight = HarvestConstants.SAND_BANK_HEIGHT;

			// Every bank holds from 9 to 18 sand
			sand.MinTotal = HarvestConstants.SAND_BANK_MIN_TOTAL;
			sand.MaxTotal = HarvestConstants.SAND_BANK_MAX_TOTAL;

			// A resource bank will respawn its content every 10 to 20 minutes
			sand.MinRespawn = TimeSpan.FromMinutes(HarvestConstants.SAND_RESPAWN_MIN_MINUTES);
			sand.MaxRespawn = TimeSpan.FromMinutes(HarvestConstants.SAND_RESPAWN_MAX_MINUTES);

			// Skill checking is done on the Mining skill
			sand.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
			sand.Tiles = m_SandTiles;

			// Players must be within 2 tiles to harvest
			sand.MaxRange = HarvestConstants.SAND_MAX_RANGE;

			// One sand per harvest action
			sand.ConsumedPerHarvest = HarvestConstants.SAND_CONSUMED_PER_HARVEST;
			sand.ConsumedPerFeluccaHarvest = HarvestConstants.SAND_CONSUMED_PER_HARVEST;

			// The digging effect
			sand.EffectActions = HarvestConstants.ORE_EFFECT_ACTIONS;
			sand.EffectSounds = HarvestConstants.ORE_EFFECT_SOUNDS;
			sand.EffectCounts = HarvestConstants.SAND_EFFECT_COUNTS;
			sand.EffectDelay = TimeSpan.FromSeconds(HarvestConstants.SAND_EFFECT_DELAY);
			sand.EffectSoundDelay = TimeSpan.FromSeconds(HarvestConstants.SAND_EFFECT_SOUND_DELAY);

			sand.NoResourcesMessage = HarvestConstants.MSG_NO_SAND_HERE;
			sand.DoubleHarvestMessage = HarvestConstants.MSG_NO_SAND_HERE;
			sand.TimedOutOfRangeMessage = HarvestConstants.MSG_TOO_FAR_AWAY;
			sand.OutOfRangeMessage = HarvestConstants.MSG_TOO_FAR_AWAY;
			sand.FailMessage = HarvestConstants.MSG_FAILED_FIND_SAND;
			sand.PackFullMessage = HarvestConstants.MSG_SAND_BACKPACK_FULL;
			sand.ToolBrokeMessage = HarvestConstants.MSG_TOOL_WORN_OUT;

			res = new HarvestResource[]
				{
					new HarvestResource(HarvestConstants.SAND_SKILL_MIN, HarvestConstants.SAND_SKILL_MIN, HarvestConstants.SAND_SKILL_MAX, HarvestStringConstants.MSG_FOUND_SAND, typeof(Sand))
				};

			veins = new HarvestVein[]
				{
					new HarvestVein(HarvestConstants.SAND_VEIN_CHANCE, 0.0, res[0], null)
				};

			sand.Resources = res;
			sand.Veins = veins;

			Definitions.Add( sand );
			#endregion
		}

		public override Type GetResourceType( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
		{
			if ( def == m_OreAndStone )
			{
                #region Void Pool Items
                /*                HarvestMap hmap = HarvestMap.CheckMapOnHarvest(from, loc, def);

                                if (hmap != null && hmap.Resource >= CraftResource.Iron && hmap.Resource <= CraftResource.Dwarven)
                                {
                                    hmap.UsesRemaining--;
                                    hmap.InvalidateProperties();

                                    CraftResourceInfo info = CraftResources.GetInfo(hmap.Resource);

                                    if (info != null)
                                        return info.ResourceTypes[1];
                                }*/
                #endregion
				PlayerMobile pm = from as PlayerMobile;
				if (pm != null &&
					pm.StoneMining &&
					pm.ToggleMiningStone &&
					from.Skills[SkillName.Mining].Base >= HarvestConstants.STONE_MINING_SKILL_REQUIRED &&
					HarvestConstants.STONE_MINING_PROBABILITY > Utility.RandomDouble())
				{
					return resource.Types[1];
				}

                return resource.Types[0];
			}

            return base.GetResourceType( from, tool, def, map, loc, resource );
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) )
				return false;

			if (from.Mounted)
			{
				from.SendLocalizedMessage(HarvestConstants.MSG_CANNOT_MINE_RIDING);
				return false;
			}
			else if (from.IsBodyMod && !from.Body.IsHuman)
			{
				from.SendLocalizedMessage(HarvestConstants.MSG_CANNOT_MINE_POLYMORPHED);
				return false;
			}

			return true;
		}

		public override void SendSuccessTo(Mobile from, Item item, HarvestResource resource)
		{
			if (item is BaseGranite)
				from.SendMessage(HarvestStringConstants.MSG_EXTRACTED_STONE);
			else
				base.SendSuccessTo(from, item, resource);
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) )
				return false;

			if (def == m_Sand && !(from is PlayerMobile && from.Skills[SkillName.Mining].Base >= HarvestConstants.STONE_MINING_SKILL_REQUIRED && ((PlayerMobile)from).SandMining))
			{
				OnBadHarvestTarget(from, tool, toHarvest);
				return false;
			}
			else if (from.Mounted)
			{
				from.SendLocalizedMessage(HarvestConstants.MSG_CANNOT_MINE_RIDING);
				return false;
			}
			else if (from.IsBodyMod && !from.Body.IsHuman)
			{
				from.SendLocalizedMessage(HarvestConstants.MSG_CANNOT_MINE_POLYMORPHED);
				return false;
			}

			return true;
		}

		public override HarvestVein MutateVein( Mobile from, Item tool, HarvestDefinition def, HarvestBank bank, object toHarvest, HarvestVein vein )
		{
			if ( tool is GargoylesPickaxe && def == m_OreAndStone )
			{
				int veinIndex = Array.IndexOf( def.Veins, vein );

				if ( veinIndex >= 0 && veinIndex < (def.Veins.Length - 1) )
					return def.Veins[veinIndex + 1];
			}
			else if ( tool is OreShovel && def == m_OreAndStone ) // WIZARD ADDED
			{
				int veinIndex = Array.IndexOf( def.Veins, vein );
				return def.Veins[0];
			} 

			return base.MutateVein( from, tool, def, bank, toHarvest, vein );
		}

		private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				 0, -1,
				 0,  1,
				 1, -1,
				 1,  0,
				 1,  1
			};

		public override void OnHarvestFinished(Mobile from, Item tool, HarvestDefinition def, HarvestVein vein, HarvestBank bank, HarvestResource resource, object harvested)
		{
			if (tool is GargoylesPickaxe && def == m_OreAndStone && HarvestConstants.ELEMENTAL_SPAWN_CHANCE > Utility.RandomDouble())
			{
				HarvestResource res = vein.PrimaryResource;

				if ( res == resource && res.Types.Length >= 3 )
				{
					try
					{
						Map map = from.Map;

						if ( map == null )
							return;

						BaseCreature spawned = Activator.CreateInstance(res.Types[2], new object[] { HarvestConstants.ELEMENTAL_STRENGTH }) as BaseCreature;

						if ( spawned != null )
						{
						int offset = Utility.Random(HarvestConstants.SPAWN_OFFSET_COUNT) * 2;

						for (int i = 0; i < m_Offsets.Length; i += 2)
							{
								int x = from.X + m_Offsets[(offset + i) % m_Offsets.Length];
								int y = from.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

								if ( map.CanSpawnMobile( x, y, from.Z ) )
								{
									spawned.OnBeforeSpawn( new Point3D( x, y, from.Z ), map );
									spawned.MoveToWorld( new Point3D( x, y, from.Z ), map );
									spawned.Combatant = from;
									return;
								}
								else
								{
									int z = map.GetAverageZ( x, y );

									if ( map.CanSpawnMobile( x, y, z ) )
									{
										spawned.OnBeforeSpawn( new Point3D( x, y, z ), map );
										spawned.MoveToWorld( new Point3D( x, y, z ), map );
										spawned.Combatant = from;
										return;
									}
								}
							}

							spawned.OnBeforeSpawn( from.Location, from.Map );
							spawned.MoveToWorld( from.Location, from.Map );
							spawned.Combatant = from;
						}
					}
					catch
					{
					}
				}
			}
		}

		public override bool BeginHarvesting(Mobile from, Item tool)
		{
			if (!base.BeginHarvesting(from, tool))
				return false;

			from.SendLocalizedMessage(503033); // Where do you wish to dig?
			return true;
		}

		public override void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			base.OnHarvestStarted( from, tool, def, toHarvest );

			if ( Core.ML )
				from.RevealingAction();
		}

		public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
		{
			if (toHarvest is LandTarget)
				from.SendLocalizedMessage(HarvestConstants.MSG_CANNOT_MINE_THERE);
			else
				from.SendLocalizedMessage(HarvestConstants.MSG_CANNOT_MINE_THAT);
		}

		#region Tile lists
		public static int[] m_MountainAndCaveTiles = new int[]
			{
				220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
				230, 231, 236, 237, 238, 239, 240, 241, 242, 243,
				244, 245, 246, 247, 252, 253, 254, 255, 256, 257,
				258, 259, 260, 261, 262, 263, 268, 269, 270, 271,
				272, 273, 274, 275, 276, 277, 278, 279, 286, 287,
				288, 289, 290, 291, 292, 293, 294, 296, 296, 297,
				321, 322, 323, 324, 467, 468, 469, 470, 471, 472,
				473, 474, 476, 477, 478, 479, 480, 481, 482, 483,
				484, 485, 486, 487, 492, 493, 494, 495, 543, 544,
				545, 546, 547, 548, 549, 550, 551, 552, 553, 554,
				555, 556, 557, 558, 559, 560, 561, 562, 563, 564,
				565, 566, 567, 568, 569, 570, 571, 572, 573, 574,
				575, 576, 577, 578, 579, 581, 582, 583, 584, 585,
				586, 587, 588, 589, 590, 591, 592, 593, 594, 595,
				596, 597, 598, 599, 600, 601, 610, 611, 612, 613,

				1010, 1741, 1742, 1743, 1744, 1745, 1746, 1747, 1748, 1749,
				1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757, 1771, 1772,
				1773, 1774, 1775, 1776, 1777, 1778, 1779, 1780, 1781, 1782,
				1783, 1784, 1785, 1786, 1787, 1788, 1789, 1790, 1801, 1802,
				1803, 1804, 1805, 1806, 1807, 1808, 1809, 1811, 1812, 1813,
				1814, 1815, 1816, 1817, 1818, 1819, 1820, 1821, 1822, 1823,
				1824, 1831, 1832, 1833, 1834, 1835, 1836, 1837, 1838, 1839,
				1840, 1841, 1842, 1843, 1844, 1845, 1846, 1847, 1848, 1849,
				1850, 1851, 1852, 1853, 1854, 1861, 1862, 1863, 1864, 1865,
				1866, 1867, 1868, 1869, 1870, 1871, 1872, 1873, 1874, 1875,
				1876, 1877, 1878, 1879, 1880, 1881, 1882, 1883, 1884, 1981,
				1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991,
				1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001,
				2002, 2003, 2004, 2028, 2029, 2030, 2031, 2032, 2033, 2100,
				2101, 2102, 2103, 2104, 2105,

				0x453B, 0x453C, 0x453D, 0x453E, 0x453F, 0x4540, 0x4541,
				0x4542, 0x4543, 0x4544,	0x4545, 0x4546, 0x4547, 0x4548,
				0x4549, 0x454A, 0x454B, 0x454C, 0x454D, 0x454E,	0x454F//, 0x8E0, 0x8E3, 0x8E1, 0x8E5, 0x8E8
            };

		public static int[] m_SandTiles = new int[]
			{
				22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
				32, 33, 34, 35, 36, 37, 38, 39, 40, 41,
				42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
				52, 53, 54, 55, 56, 57, 58, 59, 60, 61,
				62, 68, 69, 70, 71, 72, 73, 74, 75,

				286, 287, 288, 289, 290, 291, 292, 293, 294, 295,
				296, 297, 298, 299, 300, 301, 402, 424, 425, 426,
				427, 441, 442, 443, 444, 445, 446, 447, 448, 449,
				450, 451, 452, 453, 454, 455, 456, 457, 458, 459,
				460, 461, 462, 463, 464, 465, 642, 643, 644, 645,
				650, 651, 652, 653, 654, 655, 656, 657, 821, 822,
				823, 824, 825, 826, 827, 828, 833, 834, 835, 836,
				845, 846, 847, 848, 849, 850, 851, 852, 857, 858,
				859, 860, 951, 952, 953, 954, 955, 956, 957, 958,
				967, 968, 969, 970,

				1447, 1448, 1449, 1450, 1451, 1452, 1453, 1454, 1455,
				1456, 1457, 1458, 1611, 1612, 1613, 1614, 1615, 1616,
				1617, 1618, 1623, 1624, 1625, 1626, 1635, 1636, 1637,
				1638, 1639, 1640, 1641, 1642, 1647, 1648, 1649, 1650
			};
		#endregion
	}
}