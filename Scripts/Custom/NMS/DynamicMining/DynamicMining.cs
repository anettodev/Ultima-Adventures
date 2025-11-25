/*
 * 
 * By Gargouille
 * Date: 21/08/2013
 * 
 * 
 */

using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Misc;
using Server.Regions;

namespace Server.Engines.Harvest
{
	/// <summary>
	/// Dynamic mining harvest system that provides custom ore types at specific locations marked by MineSpirit mobiles.
	/// Allows GameMasters to place custom mining spots without creating regions.
	/// </summary>
	public class DynamicMining : HarvestSystem
	{
		#region Static Methods

		/// <summary>
		/// Gets the appropriate harvest system for the given tool location.
		/// Checks for nearby MineSpirit instances and returns their custom system if found.
		/// </summary>
		/// <param name="axe">The mining tool</param>
		/// <returns>Custom harvest system if MineSpirit found, null otherwise</returns>
		public static HarvestSystem GetSystem(Item axe)
		{
			// Null check for tool
			if (axe == null || axe.Deleted)
				return null;

			Map map;
			Point3D loc;

			Mobile mobile = (Mobile)axe.RootParentEntity;
			object root = axe.RootParent;

			if (root == null)
			{
				map = axe.Map;
				loc = axe.Location;
			}
			else
			{
				IEntity entity = root as IEntity;
				if (entity == null)
					return null;
					
				map = entity.Map;
				loc = entity.Location;
			}
			
			// Null check for map before using it
			if (map == null || map == Map.Internal)
				return null;
			
			IPooledEnumerable eable = map.GetMobilesInRange(loc, DynamicMiningConstants.MINE_SPIRIT_SEARCH_RANGE);
			
			try
			{
				foreach (Mobile mob in eable)
				{
					if (mob is MineSpirit)
					{
						MineSpirit mine = (MineSpirit)mob;
						if (mine.GetDistanceToSqrt(loc) <= mine.Range)
						{
							return mine.HarvestSystem;
						}
					}
				}
			}
			finally
			{
				eable.Free();
			}
			
			return null;
		}

		#endregion
		
		#region Fields

		private HarvestDefinition m_Ore;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the ore harvest definition
		/// </summary>
		public HarvestDefinition Ore
		{
			get { return m_Ore; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes the dynamic mining system with default harvest definition
		/// </summary>
		public DynamicMining()
		{
			HarvestDefinition ore = m_Ore = new HarvestDefinition();

			// Resource banks are every 3x3 tiles
			ore.BankWidth = HarvestConstants.ORE_BANK_WIDTH;
			ore.BankHeight = HarvestConstants.ORE_BANK_HEIGHT;

			// Every bank holds from 4 to 28 ore
			ore.MinTotal = DynamicMiningConstants.DYNAMIC_ORE_BANK_MIN_TOTAL;
			ore.MaxTotal = DynamicMiningConstants.DYNAMIC_ORE_BANK_MAX_TOTAL;

			// A resource bank will respawn its content every 10 to 30 minutes
			ore.MinRespawn = TimeSpan.FromMinutes(DynamicMiningConstants.DYNAMIC_ORE_RESPAWN_MIN_MINUTES);
			ore.MaxRespawn = TimeSpan.FromMinutes(DynamicMiningConstants.DYNAMIC_ORE_RESPAWN_MAX_MINUTES);

			// Skill checking is done on the Mining skill
			ore.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
			ore.Tiles = m_MountainAndCaveTiles;

			// Players must be within 2 tiles to harvest
			ore.MaxRange = HarvestConstants.ORE_MAX_RANGE;

			// One ore per harvest action
			ore.ConsumedPerHarvest = HarvestConstants.ORE_CONSUMED_PER_HARVEST;
			ore.ConsumedPerFeluccaHarvest = HarvestConstants.ORE_CONSUMED_PER_HARVEST;

			// The digging effect
			ore.EffectActions = new int[] { DynamicMiningConstants.DYNAMIC_EFFECT_ACTION_ID };
			ore.EffectSounds = HarvestConstants.ORE_EFFECT_SOUNDS;
			ore.EffectCounts = new int[] { DynamicMiningConstants.DYNAMIC_EFFECT_COUNT };
			ore.EffectDelay = TimeSpan.FromSeconds(HarvestConstants.ORE_EFFECT_DELAY);
			ore.EffectSoundDelay = TimeSpan.FromSeconds(HarvestConstants.ORE_EFFECT_SOUND_DELAY);

			ore.NoResourcesMessage = HarvestConstants.MSG_NO_METAL_HERE;
			ore.DoubleHarvestMessage = HarvestConstants.MSG_METAL_TAKEN;
			ore.TimedOutOfRangeMessage = HarvestConstants.MSG_TOO_FAR_AWAY;
			ore.OutOfRangeMessage = HarvestConstants.MSG_TOO_FAR_AWAY;
			ore.FailMessage = HarvestConstants.MSG_FAILED_FIND_ORE;
			ore.PackFullMessage = HarvestConstants.MSG_BACKPACK_FULL;
			ore.ToolBrokeMessage = HarvestConstants.MSG_TOOL_WORN_OUT;

			if (Core.ML)
			{
				ore.BonusResources = new BonusHarvestResource[]
				{
					new BonusHarvestResource(0, HarvestConstants.BONUS_NOTHING_CHANCE, null, null),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_SCROLL_MAP_SKILL_REQ, HarvestConstants.BONUS_SCROLL_CHANCE, 1074542, typeof(BlankScroll)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_SCROLL_MAP_SKILL_REQ, HarvestConstants.BONUS_LOCAL_MAP_CHANCE, 1074542, typeof(LocalMap)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_SCROLL_MAP_SKILL_REQ, HarvestConstants.BONUS_INDECIPHERABLE_MAP_CHANCE, 1074542, typeof(IndecipherableMap)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_SCROLL_MAP_SKILL_REQ, HarvestConstants.BONUS_BLANK_MAP_CHANCE, 1074542, typeof(BlankMap)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_AMBER_SKILL_REQ, HarvestConstants.BONUS_AMBER_CHANCE, 1074542, typeof(Amber)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_GEM_SKILL_REQ, HarvestConstants.BONUS_AMETHYST_CHANCE, 1074542, typeof(Amethyst)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_GEM_SKILL_REQ, HarvestConstants.BONUS_CITRINE_CHANCE, 1074542, typeof(Citrine)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_DIAMOND_SKILL_REQ, HarvestConstants.BONUS_DIAMOND_CHANCE, 1074542, typeof(Diamond)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_HIGH_GEM_SKILL_REQ, HarvestConstants.BONUS_EMERALD_CHANCE, 1074542, typeof(Emerald)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_HIGH_GEM_SKILL_REQ, HarvestConstants.BONUS_RUBY_CHANCE, 1074542, typeof(Ruby)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_HIGH_GEM_SKILL_REQ, HarvestConstants.BONUS_SAPPHIRE_CHANCE, 1074542, typeof(Sapphire)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_RARE_GEM_SKILL_REQ, HarvestConstants.BONUS_STAR_SAPPHIRE_CHANCE, 1074542, typeof(StarSapphire)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_RARE_GEM_SKILL_REQ, HarvestConstants.BONUS_TOURMALINE_CHANCE, 1074542, typeof(Tourmaline)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_LEGENDARY_GEM_SKILL_REQ, HarvestConstants.BONUS_BLUE_DIAMOND_CHANCE, 1072562, typeof(BlueDiamond)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_LEGENDARY_GEM_SKILL_REQ, HarvestConstants.BONUS_DARK_SAPPHIRE_CHANCE, 1072567, typeof(DarkSapphire)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_LEGENDARY_GEM_SKILL_REQ, HarvestConstants.BONUS_ECRU_CITRINE_CHANCE, 1072570, typeof(EcruCitrine)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_LEGENDARY_GEM_SKILL_REQ, HarvestConstants.BONUS_FIRE_RUBY_CHANCE, 1072564, typeof(FireRuby)),
					new BonusHarvestResource((int)DynamicMiningConstants.BONUS_LEGENDARY_GEM_SKILL_REQ, HarvestConstants.BONUS_PERFECT_EMERALD_CHANCE, 1072566, typeof(PerfectEmerald))
				};
			}

			ore.RaceBonus = false;
			ore.RandomizeVeins = true;

			Definitions.Add(ore);
		}

		#endregion

		#region Core Harvest Methods

		/// <summary>
		/// Gets the resource type to harvest
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The location</param>
		/// <param name="resource">The harvest resource</param>
		/// <returns>The resource type to create</returns>
		public override Type GetResourceType(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource)
		{
			if (def == m_Ore)
			{
				return resource.Types[0];
			}

			return base.GetResourceType(from, tool, def, map, loc, resource);
		}

		/// <summary>
		/// Checks if the mobile can harvest with the given tool
		/// </summary>
		/// <param name="from">The mobile attempting to harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <returns>True if harvesting is allowed, false otherwise</returns>
		public override bool CheckHarvest(Mobile from, Item tool)
		{
			if (!base.CheckHarvest(from, tool))
				return false;

			return ValidateMiningConditions(from);
		}

		/// <summary>
		/// Sends success message to the mobile
		/// </summary>
		/// <param name="from">The mobile that harvested</param>
		/// <param name="item">The item that was harvested</param>
		/// <param name="resource">The harvest resource</param>
		public override void SendSuccessTo(Mobile from, Item item, HarvestResource resource)
		{
			base.SendSuccessTo(from, item, resource);
		}

		/// <summary>
		/// Checks if the mobile can harvest the specific target
		/// </summary>
		/// <param name="from">The mobile attempting to harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target to harvest</param>
		/// <returns>True if harvesting is allowed, false otherwise</returns>
		public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			if (!base.CheckHarvest(from, tool, def, toHarvest))
				return false;

			return ValidateMiningConditions(from);
		}

		/// <summary>
		/// Mutates the harvest vein (not used in dynamic mining)
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="bank">The harvest bank</param>
		/// <param name="toHarvest">The target to harvest</param>
		/// <param name="vein">The current vein</param>
		/// <returns>The vein to use</returns>
		public override HarvestVein MutateVein(Mobile from, Item tool, HarvestDefinition def, HarvestBank bank, object toHarvest, HarvestVein vein)
		{
			return vein;
		}

		/// <summary>
		/// Begins the harvesting process
		/// </summary>
		/// <param name="from">The mobile attempting to harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <returns>True if harvesting can begin, false otherwise</returns>
		public override bool BeginHarvesting(Mobile from, Item tool)
		{
			if (!base.BeginHarvesting(from, tool))
				return false;

			from.SendLocalizedMessage(DynamicMiningConstants.MSG_WHERE_TO_DIG);
			return true;
		}

		/// <summary>
		/// Called when harvesting has started
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target to harvest</param>
		public override void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			base.OnHarvestStarted(from, tool, def, toHarvest);

			if (Core.ML)
				from.RevealingAction();
		}

		/// <summary>
		/// Called when a bad harvest target is selected
		/// </summary>
		/// <param name="from">The mobile attempting to harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="toHarvest">The invalid target</param>
		public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
		{
			if (toHarvest is LandTarget)
				from.SendLocalizedMessage(DynamicMiningConstants.MSG_CANNOT_MINE_THERE);
			else
				from.SendLocalizedMessage(DynamicMiningConstants.MSG_CANNOT_MINE_THAT);
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Validates if the mobile meets the conditions for mining
		/// </summary>
		/// <param name="from">The mobile to validate</param>
		/// <returns>True if conditions are met, false otherwise</returns>
		private bool ValidateMiningConditions(Mobile from)
		{
			if (from.Mounted)
			{
				from.SendLocalizedMessage(DynamicMiningConstants.MSG_CANNOT_MINE_RIDING);
				return false;
			}

			if (from.IsBodyMod && !from.Body.IsHuman)
			{
				from.SendLocalizedMessage(DynamicMiningConstants.MSG_CANNOT_MINE_POLYMORPHED);
				return false;
			}

			return true;
		}

		#endregion

		#region Tile Lists

		/// <summary>
		/// Array of tile IDs that can be mined (mountain and cave tiles)
		/// </summary>
		private static int[] m_MountainAndCaveTiles = new int[]
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
			0x4542, 0x4543, 0x4544, 0x4545, 0x4546, 0x4547, 0x4548,
			0x4549, 0x454A, 0x454B, 0x454C, 0x454D, 0x454E, 0x454F
		};

		#endregion
	}
}