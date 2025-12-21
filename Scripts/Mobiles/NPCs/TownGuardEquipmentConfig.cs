using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	/// <summary>
	/// Equipment configuration for TownGuards based on region/world location.
	/// Extracted from TownGuards.cs to improve maintainability and reduce complexity.
	/// </summary>
	public class TownGuardEquipmentConfig
	{
		/// <summary>Cloth color for the guard</summary>
		public int ClothColor { get; set; }

		/// <summary>Shield item ID (0 = no shield)</summary>
		public int ShieldType { get; set; }

		/// <summary>Helm item ID (0 = no helm)</summary>
		public int HelmType { get; set; }

		/// <summary>Cloak color (0 = no cloak)</summary>
		public int CloakColor { get; set; }

		/// <summary>Weapon type to create</summary>
		public Type WeaponType { get; set; }

		/// <summary>
		/// Gets the equipment configuration for a region or world.
		/// </summary>
		/// <param name="regionName">The region name</param>
		/// <param name="worldName">The world name</param>
		/// <returns>Equipment configuration, or default (Britain) if not found</returns>
		public static TownGuardEquipmentConfig GetConfig(string regionName, string worldName)
		{
			// Try region name first
			if (!string.IsNullOrEmpty(regionName) && s_RegionConfigs.ContainsKey(regionName))
			{
				return s_RegionConfigs[regionName];
			}

			// Try world name
			if (!string.IsNullOrEmpty(worldName) && s_WorldConfigs.ContainsKey(worldName))
			{
				return s_WorldConfigs[worldName];
			}

			// Default to Britain configuration
			return s_DefaultConfig;
		}

		/// <summary>
		/// Checks if a region name matches any of the configured regions (including multi-region entries)
		/// </summary>
		/// <param name="regionName">The region name to check</param>
		/// <returns>Equipment configuration if found, null otherwise</returns>
		public static TownGuardEquipmentConfig GetConfigByRegionName(string regionName)
		{
			if (string.IsNullOrEmpty(regionName))
				return null;

			// Check exact matches first
			if (s_RegionConfigs.ContainsKey(regionName))
			{
				return s_RegionConfigs[regionName];
			}

			// Check multi-region entries
			foreach (var kvp in s_MultiRegionConfigs)
			{
				foreach (string multiRegion in kvp.Key)
				{
					if (regionName == multiRegion)
					{
						return kvp.Value;
					}
				}
			}

			return null;
		}

		#region Configuration Data

		private static readonly Dictionary<string, TownGuardEquipmentConfig> s_RegionConfigs = new Dictionary<string, TownGuardEquipmentConfig>
		{
			{ "the Village of Whisper", new TownGuardEquipmentConfig { ClothColor = 0x96D, ShieldType = TownGuardsConstants.SHIELD_TYPE_1, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x972, WeaponType = typeof(Longsword) } },
			{ "the Town of Glacial Hills", new TownGuardEquipmentConfig { ClothColor = 0x482, ShieldType = TownGuardsConstants.SHIELD_TYPE_2, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x542, WeaponType = typeof(Kryss) } },
			{ "the Village of Springvale", new TownGuardEquipmentConfig { ClothColor = 0x595, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x593, WeaponType = typeof(Pike) } },
			{ "the City of Elidor", new TownGuardEquipmentConfig { ClothColor = 0x665, ShieldType = TownGuardsConstants.SHIELD_TYPE_5, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x664, WeaponType = typeof(Katana) } },
			{ "the Village of Islegem", new TownGuardEquipmentConfig { ClothColor = 0x7D1, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x7D6, WeaponType = typeof(Spear) } },
			{ "Greensky Village", new TownGuardEquipmentConfig { ClothColor = 0x7D7, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x7DA, WeaponType = typeof(Bardiche) } },
			{ "the Port of Dusk", new TownGuardEquipmentConfig { ClothColor = 0x601, ShieldType = TownGuardsConstants.SHIELD_TYPE_3, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x600, WeaponType = typeof(Cutlass) } },
			{ "the Port of Starguide", new TownGuardEquipmentConfig { ClothColor = 0x751, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x758, WeaponType = typeof(BladedStaff) } },
			{ "the Village of Portshine", new TownGuardEquipmentConfig { ClothColor = 0x847, ShieldType = TownGuardsConstants.SHIELD_TYPE_4, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x851, WeaponType = typeof(Mace) } },
			{ "the Ranger Outpost", new TownGuardEquipmentConfig { ClothColor = 0x598, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x83F, WeaponType = typeof(Spear) } },
			{ "the Lunar City of Dawn", new TownGuardEquipmentConfig { ClothColor = 0x9C4, ShieldType = TownGuardsConstants.SHIELD_TYPE_3, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x9C4, WeaponType = typeof(DiamondMace) } },
			{ "The Town of Devil Guard", new TownGuardEquipmentConfig { ClothColor = 0x430, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0, WeaponType = typeof(LargeBattleAxe) } },
			{ "The Farmland of Devil Guard", new TownGuardEquipmentConfig { ClothColor = 0x430, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0, WeaponType = typeof(LargeBattleAxe) } },
			{ "the Town of Moon", new TownGuardEquipmentConfig { ClothColor = 0x8AF, ShieldType = TownGuardsConstants.SHIELD_TYPE_1, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x972, WeaponType = typeof(Longsword) } },
			{ "the Village of Grey", new TownGuardEquipmentConfig { ClothColor = 0, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x763, WeaponType = typeof(Halberd) } },
			{ "the City of Montor", new TownGuardEquipmentConfig { ClothColor = 0x96F, ShieldType = TownGuardsConstants.SHIELD_TYPE_2, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x529, WeaponType = typeof(Broadsword) } },
			{ "the Village of Fawn", new TownGuardEquipmentConfig { ClothColor = 0x59D, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x59C, WeaponType = typeof(DoubleAxe) } },
			{ "the Village of Yew", new TownGuardEquipmentConfig { ClothColor = 0x83C, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x850, WeaponType = typeof(Spear) } },
			{ "the Undercity of Umbra", new TownGuardEquipmentConfig { ClothColor = 0x964, ShieldType = TownGuardsConstants.SHIELD_TYPE_6, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x966, WeaponType = typeof(BoneHarvester) } },
			{ "the Village of Barako", new TownGuardEquipmentConfig { ClothColor = 0x515, ShieldType = TownGuardsConstants.SHIELD_TYPE_1, HelmType = TownGuardsConstants.HELM_TYPE_ORNATE, CloakColor = 0x58D, WeaponType = typeof(WarMace) } },
			{ "ilha de Kuldar", new TownGuardEquipmentConfig { ClothColor = 0xB3B, ShieldType = TownGuardsConstants.SHIELD_TYPE_6, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x845, WeaponType = typeof(Maul) } },
			{ "cidade de Kuldara", new TownGuardEquipmentConfig { ClothColor = 0xB3B, ShieldType = TownGuardsConstants.SHIELD_TYPE_6, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x845, WeaponType = typeof(Maul) } }
		};

		private static readonly Dictionary<string[], TownGuardEquipmentConfig> s_MultiRegionConfigs = new Dictionary<string[], TownGuardEquipmentConfig>
		{
			{ new[] { "Iceclad Fisherman's Village", "the Town of Mountain Crest", "Glacial Coast Village" }, new TownGuardEquipmentConfig { ClothColor = 0x482, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x47E, WeaponType = typeof(Bardiche) } }
		};

		private static readonly Dictionary<string, TownGuardEquipmentConfig> s_WorldConfigs = new Dictionary<string, TownGuardEquipmentConfig>
		{
			{ "the Land of Lodoria", new TownGuardEquipmentConfig { ClothColor = 0x6E4, ShieldType = TownGuardsConstants.SHIELD_TYPE_7, HelmType = TownGuardsConstants.HELM_TYPE_ALTERNATIVE, CloakColor = 0x6E7, WeaponType = typeof(Scimitar) } },
			{ "the Island of Umber Veil", new TownGuardEquipmentConfig { ClothColor = 0xA5D, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x96D, WeaponType = typeof(Halberd) } },
			{ "the Isles of Dread", new TownGuardEquipmentConfig { ClothColor = 0x978, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_ORNATE, CloakColor = 0x973, WeaponType = typeof(OrnateAxe) } },
			{ "the Savaged Empire", new TownGuardEquipmentConfig { ClothColor = 0x515, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_STANDARD, CloakColor = 0x59D, WeaponType = typeof(Spear) } },
			{ "the Serpent Island", new TownGuardEquipmentConfig { ClothColor = 0x515, ShieldType = 0, HelmType = TownGuardsConstants.HELM_TYPE_GARGOYLE, CloakColor = 0, WeaponType = typeof(Halberd) } }
		};

		private static readonly TownGuardEquipmentConfig s_DefaultConfig = new TownGuardEquipmentConfig
		{
			ClothColor = 0x966,
			ShieldType = TownGuardsConstants.SHIELD_TYPE_7,
			HelmType = TownGuardsConstants.HELM_TYPE_STANDARD,
			CloakColor = 2900,
			WeaponType = typeof(VikingSword)
		};

		#endregion
	}
}

