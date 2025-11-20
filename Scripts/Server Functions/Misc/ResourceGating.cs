using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Misc
{
	/// <summary>
	/// Centralized system for gating (disabling) specific CraftResources that are "unknown" to players.
	/// These resources will be enabled in future content releases.
	/// PT-BR: Sistema centralizado para bloquear recursos específicos que são "desconhecidos" para jogadores.
	/// </summary>
	public static class ResourceGating
	{
		#region Gated Resources Configuration

		/// <summary>
		/// HashSet of all gated (unavailable) CraftResources.
		/// ENABLED metals: Iron, DullCopper, ShadowIron, Copper, Bronze, Gold, Agapite, Verite, Valorite, Titanium, Rosenium, Platinum
		/// GATED metals: Nepturite, Obsidian, Mithril, Xormite, Dwarven, Steel, Brass
		/// ENABLED woods: RegularWood, AshTree, EbonyTree, ElvenTree, GoldenOakTree, CherryTree, RosewoodTree, HickoryTree
		/// GATED woods: MahoganyTree, OakTree, PineTree, GhostTree, WalnutTree, PetrifiedTree, DriftwoodTree
		/// </summary>
		private static readonly HashSet<CraftResource> GatedResources = new HashSet<CraftResource>
		{
			// Rare/Unknown metals - not yet introduced to players
			CraftResource.Nepturite,
			CraftResource.Obsidian,
			CraftResource.Mithril,
			CraftResource.Xormite,
			CraftResource.Dwarven,

			// Alloy metals - crafting recipes disabled
			CraftResource.Steel,
			CraftResource.Brass

			// Unknown woods - not yet introduced to players
			// NOTE: These wood types are commented out in CraftResource enum (ResourceInfo.cs)
			// and will be enabled when the enum values are uncommented
			//CraftResource.MahoganyTree,
			//CraftResource.OakTree,
			//CraftResource.PineTree,
			//CraftResource.GhostTree,
			//CraftResource.WalnutTree,
			//CraftResource.PetrifiedTree,
			//CraftResource.DriftwoodTree
		};

		/// <summary>
		/// HashSet of all gated ingot types (by Type).
		/// Used for vendor buy/sell checks and item validation.
		/// </summary>
		private static readonly HashSet<Type> GatedIngotTypes = new HashSet<Type>
		{
			typeof(NepturiteIngot),
			typeof(ObsidianIngot),
			typeof(MithrilIngot),
			typeof(XormiteIngot),
			typeof(DwarvenIngot),
			typeof(SteelIngot),
			typeof(BrassIngot)
		};

		/// <summary>
		/// HashSet of all gated ore types (by Type).
		/// Used for preventing ore smelting and loot drops.
		/// </summary>
		private static readonly HashSet<Type> GatedOreTypes = new HashSet<Type>
		{
			typeof(NepturiteOre),
			typeof(ObsidianOre),
			typeof(MithrilOre),
			typeof(XormiteOre),
			typeof(DwarvenOre)
			// Note: Steel and Brass have no ore forms (they are alloys)
		};

		/// <summary>
		/// HashSet of gated "rare metal stones" names.
		/// Used in RareMetals.cs for smelting prevention.
		/// </summary>
		private static readonly HashSet<string> GatedRareMetalStoneNames = new HashSet<string>
		{
			"mithril stones",
			"xormite stones",
			"obsidian stones",
			"nepturite stones"
			// Note: No "dwarven stones", "steel stones", or "brass stones" in RareMetals.cs
		};

		/// <summary>
		/// HashSet of gated "crystalline" metal names.
		/// Used in HardCrystals.cs for smelting prevention.
		/// </summary>
		private static readonly HashSet<string> GatedCrystallineNames = new HashSet<string>
		{
			"crystalline obsidian",
			"crystalline nepturite",
			"crystalline steel",
			"crystalline brass",
			"crystalline mithril",
			"crystalline xormite"
			// Note: No "crystalline dwarven" found in HardCrystals.cs
		};

		/// <summary>
		/// HashSet of gated metal scale names.
		/// Used in HardScales.cs for smelting prevention.
		/// </summary>
		private static readonly HashSet<string> GatedScaleNames = new HashSet<string>
		{
			"obsidian scales",
			"nepturite scales",
			"steel scales",
			"brass scales",
			"mithril scales",
			"xormite scales"
			// Note: No "dwarven scales" found in HardScales.cs
		};

		#endregion

		#region Public Query Methods

		/// <summary>
		/// Checks if a CraftResource is gated (unavailable to players).
		/// </summary>
		/// <param name="resource">The CraftResource to check</param>
		/// <returns>True if resource is gated, false if available</returns>
		public static bool IsResourceGated(CraftResource resource)
		{
			return GatedResources.Contains(resource);
		}

		/// <summary>
		/// Checks if an ingot Type is gated (unavailable to players).
		/// </summary>
		/// <param name="ingotType">The Type of ingot to check</param>
		/// <returns>True if ingot type is gated, false if available</returns>
		public static bool IsIngotGated(Type ingotType)
		{
			return GatedIngotTypes.Contains(ingotType);
		}

		/// <summary>
		/// Checks if an ore Type is gated (unavailable to players).
		/// </summary>
		/// <param name="oreType">The Type of ore to check</param>
		/// <returns>True if ore type is gated, false if available</returns>
		public static bool IsOreGated(Type oreType)
		{
			return GatedOreTypes.Contains(oreType);
		}

		/// <summary>
		/// Checks if a rare metal stone name is gated (cannot be smelted).
		/// </summary>
		/// <param name="stoneName">The name of the rare metal stones (e.g., "mithril stones")</param>
		/// <returns>True if the stones are gated, false if they can be smelted</returns>
		public static bool IsRareMetalStoneGated(string stoneName)
		{
			return GatedRareMetalStoneNames.Contains(stoneName);
		}

		/// <summary>
		/// Checks if a crystalline metal name is gated (cannot be smelted).
		/// </summary>
		/// <param name="crystallineName">The name of the crystalline metal (e.g., "crystalline mithril")</param>
		/// <returns>True if the crystalline is gated, false if it can be smelted</returns>
		public static bool IsCrystallineGated(string crystallineName)
		{
			return GatedCrystallineNames.Contains(crystallineName);
		}

		/// <summary>
		/// Checks if a metal scale name is gated (cannot be smelted).
		/// </summary>
		/// <param name="scaleName">The name of the metal scales (e.g., "mithril scales")</param>
		/// <returns>True if the scales are gated, false if they can be smelted</returns>
		public static bool IsScaleGated(string scaleName)
		{
			return GatedScaleNames.Contains(scaleName);
		}

		/// <summary>
		/// Checks if an item (BaseWeapon or BaseArmor) is made from gated metal.
		/// Used for vendor repair refusal.
		/// </summary>
		/// <param name="item">The item to check</param>
		/// <returns>True if item is made from gated metal, false otherwise</returns>
		public static bool IsItemMadeFromGatedMetal(Item item)
		{
			if (item == null)
				return false;

			// Check if item is made from a specific resource
			CraftResource resource = CraftResource.None;

			if (item is BaseWeapon)
				resource = ((BaseWeapon)item).Resource;
			else if (item is BaseArmor)
				resource = ((BaseArmor)item).Resource;
			else if (item is BaseIngot)
				resource = ((BaseIngot)item).Resource;

			return IsResourceGated(resource);
		}

		#endregion

		#region Localized Messages

		/// <summary>
		/// Message shown when trying to mine gated ore (should not happen as they're not in HarvestResource list)
		/// PT-BR: "Este minério é desconhecido e não pode ser minerado no momento."
		/// </summary>
		public static string MSG_CANNOT_MINE_GATED_ORE = "Este minério é desconhecido e não pode ser minerado no momento.";

		/// <summary>
		/// Message shown when trying to smelt gated ore
		/// PT-BR: "Você não sabe como derreter este minério estranho."
		/// </summary>
		public static string MSG_CANNOT_SMELT_GATED_ORE = "Você não sabe como derreter este minério estranho.";

		/// <summary>
		/// Message shown when trying to craft with gated ingots
		/// PT-BR: "Este metal é desconhecido e não pode ser usado para fabricação."
		/// </summary>
		public static string MSG_CANNOT_CRAFT_WITH_GATED_METAL = "Este metal é desconhecido e não pode ser usado para fabricação.";

		/// <summary>
		/// Message shown when vendor refuses to repair gated metal items
		/// PT-BR: "Desculpe, não posso reparar itens feitos deste metal desconhecido."
		/// </summary>
		public static string MSG_VENDOR_CANNOT_REPAIR_GATED_METAL = "Desculpe, não posso reparar itens feitos deste metal desconhecido.";

		/// <summary>
		/// Message shown when trying to smelt gated rare metal stones
		/// PT-BR: "Você não possui o conhecimento para trabalhar com estas pedras."
		/// </summary>
		public static string MSG_CANNOT_SMELT_GATED_STONES = "Você não possui o conhecimento para trabalhar com estas pedras.";

		/// <summary>
		/// Message shown when trying to smelt gated crystalline metals
		/// PT-BR: "Você não possui o conhecimento para fundir este metal cristalino."
		/// </summary>
		public static string MSG_CANNOT_SMELT_GATED_CRYSTALLINE = "Você não possui o conhecimento para fundir este metal cristalino.";

		/// <summary>
		/// Message shown when trying to smelt gated metal scales
		/// PT-BR: "Você não possui o conhecimento para fundir estas escamas metálicas."
		/// </summary>
		public static string MSG_CANNOT_SMELT_GATED_SCALES = "Você não possui o conhecimento para fundir estas escamas metálicas.";

		/// <summary>
		/// Message shown when trying to chop gated wood logs (should not happen as they're not harvestable)
		/// PT-BR: "Este tipo de madeira é desconhecido e não pode ser cortado no momento."
		/// </summary>
		public static string MSG_CANNOT_CHOP_GATED_WOOD = "Este tipo de madeira é desconhecido e não pode ser cortado no momento.";

		/// <summary>
		/// Message shown when trying to convert gated logs to boards
		/// PT-BR: "Você não sabe como trabalhar com este tipo de madeira estranha."
		/// </summary>
		public static string MSG_CANNOT_CONVERT_GATED_LOGS = "Você não sabe como trabalhar com este tipo de madeira estranha.";

		/// <summary>
		/// Message shown when trying to craft with gated wood
		/// PT-BR: "Esta madeira é desconhecida e não pode ser usada para fabricação."
		/// </summary>
		public static string MSG_CANNOT_CRAFT_WITH_GATED_WOOD = "Esta madeira é desconhecida e não pode ser usada para fabricação.";

		/// <summary>
		/// Message shown when vendor refuses to repair gated wood items
		/// PT-BR: "Desculpe, não posso reparar itens feitos desta madeira desconhecida."
		/// </summary>
		public static string MSG_VENDOR_CANNOT_REPAIR_GATED_WOOD = "Desculpe, não posso reparar itens feitos desta madeira desconhecida.";

		#endregion

		#region Future Expansion Helper Methods

		/// <summary>
		/// Call this method to "unlock" a resource for players.
		/// This is intended for future content releases.
		/// </summary>
		/// <param name="resource">The resource to unlock</param>
		/// <returns>True if resource was gated and is now unlocked, false if it wasn't gated</returns>
		public static bool UnlockResource(CraftResource resource)
		{
			return GatedResources.Remove(resource);
		}

		/// <summary>
		/// Gets a list of all currently gated resources (for admin/debugging purposes).
		/// </summary>
		/// <returns>Array of all gated CraftResources</returns>
		public static CraftResource[] GetAllGatedResources()
		{
			CraftResource[] gated = new CraftResource[GatedResources.Count];
			GatedResources.CopyTo(gated);
			return gated;
		}

		/// <summary>
		/// Gets a list of all available (non-gated) metal resources (for admin/debugging purposes).
		/// </summary>
		/// <returns>Array of available metal CraftResources</returns>
		public static CraftResource[] GetAvailableMetalResources()
		{
			List<CraftResource> available = new List<CraftResource>();

			// Standard UO metals
			available.Add(CraftResource.Iron);
			available.Add(CraftResource.DullCopper);
			available.Add(CraftResource.ShadowIron);
			available.Add(CraftResource.Copper);
			available.Add(CraftResource.Bronze);
			available.Add(CraftResource.Gold);
			available.Add(CraftResource.Agapite);
			available.Add(CraftResource.Verite);
			available.Add(CraftResource.Valorite);

			// Custom metals (ENABLED)
			available.Add(CraftResource.Titanium);
			available.Add(CraftResource.Rosenium);
			available.Add(CraftResource.Platinum);

			return available.ToArray();
		}

		#endregion
	}
}
