using System.Collections.Generic;
using Server.Misc;
using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Centralized resource mapping for weapons.
	/// Eliminates duplicate switch statements in BaseWeapon.cs by providing dictionary-based lookups.
	/// Replaces switch statements in AddNameProperty, GetMaterialHue, and OnCraft methods.
	/// </summary>
	public static class WeaponResourceMapper
	{
		/// <summary>
		/// Information about a craft resource for weapons
		/// </summary>
		public class ResourceInfo
		{
			/// <summary>Localized string ID for the resource name (oreType)</summary>
			public int OreType { get; set; }
			
			/// <summary>Material name for color lookup (e.g., "dull copper", "ash")</summary>
			public string MaterialName { get; set; }
			
			/// <summary>Style for material color lookup ("classic" for metals, "" for others)</summary>
			public string Style { get; set; }
			
			/// <summary>Durability level when crafted with this resource (for non-AOS crafting)</summary>
			public WeaponDurabilityLevel? DurabilityLevel { get; set; }
			
			/// <summary>Damage level when crafted with this resource (for non-AOS crafting)</summary>
			public WeaponDamageLevel? DamageLevel { get; set; }
			
			/// <summary>Accuracy level when crafted with this resource (for non-AOS crafting)</summary>
			public WeaponAccuracyLevel? AccuracyLevel { get; set; }
		}
		
		#region Resource Mappings
		
		/// <summary>
		/// Dictionary mapping CraftResource to ResourceInfo.
		/// Provides O(1) lookup instead of O(n) switch statements.
		/// </summary>
		private static readonly Dictionary<CraftResource, ResourceInfo> ResourceMap = 
			new Dictionary<CraftResource, ResourceInfo>
		{
			// Metals
			{ CraftResource.DullCopper, new ResourceInfo {
				OreType = 1053108,
				MaterialName = "dull copper",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Durable,
				DamageLevel = null,
				AccuracyLevel = WeaponAccuracyLevel.Accurate
			}},
			{ CraftResource.ShadowIron, new ResourceInfo {
				OreType = 1053107,
				MaterialName = "shadow iron",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Durable,
				DamageLevel = WeaponDamageLevel.Ruin,
				AccuracyLevel = null
			}},
			{ CraftResource.Copper, new ResourceInfo {
				OreType = 1053106,
				MaterialName = "copper",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Fortified,
				DamageLevel = WeaponDamageLevel.Ruin,
				AccuracyLevel = WeaponAccuracyLevel.Surpassingly
			}},
			{ CraftResource.Bronze, new ResourceInfo {
				OreType = 1053105,
				MaterialName = "bronze",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Fortified,
				DamageLevel = WeaponDamageLevel.Might,
				AccuracyLevel = WeaponAccuracyLevel.Surpassingly
			}},
			{ CraftResource.Platinum, new ResourceInfo {
				OreType = 6663002,
				MaterialName = "platinum",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Force,
				AccuracyLevel = WeaponAccuracyLevel.Eminently
			}},
			{ CraftResource.Gold, new ResourceInfo {
				OreType = 1053104,
				MaterialName = "gold",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Force,
				AccuracyLevel = WeaponAccuracyLevel.Eminently
			}},
			{ CraftResource.Agapite, new ResourceInfo {
				OreType = 1053103,
				MaterialName = "agapite",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Power,
				AccuracyLevel = WeaponAccuracyLevel.Eminently
			}},
			{ CraftResource.Verite, new ResourceInfo {
				OreType = 1053102,
				MaterialName = "verite",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Power,
				AccuracyLevel = WeaponAccuracyLevel.Exceedingly
			}},
			{ CraftResource.Valorite, new ResourceInfo {
				OreType = 1053101,
				MaterialName = "valorite",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Titanium, new ResourceInfo {
				OreType = 6661002,
				MaterialName = "titanium",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Rosenium, new ResourceInfo {
				OreType = 6662002,
				MaterialName = "rosenium",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Nepturite, new ResourceInfo {
				OreType = 1036175,
				MaterialName = "nepturite",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Obsidian, new ResourceInfo {
				OreType = 1036165,
				MaterialName = "obsidian",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Steel, new ResourceInfo {
				OreType = 1036146,
				MaterialName = "steel",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Brass, new ResourceInfo {
				OreType = 1036154,
				MaterialName = "brass",
				Style = "classic",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.Mithril, new ResourceInfo {
				OreType = 1036139,
				MaterialName = "mithril",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Xormite, new ResourceInfo {
				OreType = 1034439,
				MaterialName = "xormite",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			{ CraftResource.Dwarven, new ResourceInfo {
				OreType = 1036183,
				MaterialName = "dwarven",
				Style = "classic",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}},
			
			// Leather
			{ CraftResource.SpinedLeather, new ResourceInfo {
				OreType = 1061118,
				MaterialName = "deep sea",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.HornedLeather, new ResourceInfo {
				OreType = 1061117,
				MaterialName = "lizard",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.BarbedLeather, new ResourceInfo {
				OreType = 1061116,
				MaterialName = "serpent",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.NecroticLeather, new ResourceInfo {
				OreType = 1034413,
				MaterialName = "necrotic",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.VolcanicLeather, new ResourceInfo {
				OreType = 1034424,
				MaterialName = "volcanic",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.FrozenLeather, new ResourceInfo {
				OreType = 1034435,
				MaterialName = "frozen",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.GoliathLeather, new ResourceInfo {
				OreType = 1034380,
				MaterialName = "goliath",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.DraconicLeather, new ResourceInfo {
				OreType = 1034391,
				MaterialName = "draconic",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.HellishLeather, new ResourceInfo {
				OreType = 1034402,
				MaterialName = "hellish",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.DinosaurLeather, new ResourceInfo {
				OreType = 1036161,
				MaterialName = "dinosaur",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.AlienLeather, new ResourceInfo {
				OreType = 1034454,
				MaterialName = "alien",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			
			// Scales
			{ CraftResource.RedScales, new ResourceInfo {
				OreType = 1060814,
				MaterialName = "",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.YellowScales, new ResourceInfo {
				OreType = 1060818,
				MaterialName = "",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.BlackScales, new ResourceInfo {
				OreType = 1060820,
				MaterialName = "",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.GreenScales, new ResourceInfo {
				OreType = 1060819,
				MaterialName = "",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.WhiteScales, new ResourceInfo {
				OreType = 1060821,
				MaterialName = "",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			{ CraftResource.BlueScales, new ResourceInfo {
				OreType = 1060815,
				MaterialName = "",
				Style = "",
				DurabilityLevel = null,
				DamageLevel = null,
				AccuracyLevel = null
			}},
			
			// Wood
			{ CraftResource.AshTree, new ResourceInfo {
				OreType = 1095399,
				MaterialName = "ash",
				Style = "",
				DurabilityLevel = WeaponDurabilityLevel.Durable,
				DamageLevel = null,
				AccuracyLevel = WeaponAccuracyLevel.Accurate
			}},
			{ CraftResource.CherryTree, new ResourceInfo {
				OreType = 1095400,
				MaterialName = "cherry",
				Style = "",
				DurabilityLevel = WeaponDurabilityLevel.Durable,
				DamageLevel = WeaponDamageLevel.Ruin,
				AccuracyLevel = WeaponAccuracyLevel.Accurate
			}},
			{ CraftResource.EbonyTree, new ResourceInfo {
				OreType = 1095401,
				MaterialName = "ebony",
				Style = "",
				DurabilityLevel = WeaponDurabilityLevel.Durable,
				DamageLevel = WeaponDamageLevel.Might,
				AccuracyLevel = WeaponAccuracyLevel.Accurate
			}},
			{ CraftResource.GoldenOakTree, new ResourceInfo {
				OreType = 1095402,
				MaterialName = "golden oak",
				Style = "",
				DurabilityLevel = WeaponDurabilityLevel.Durable,
				DamageLevel = WeaponDamageLevel.Might,
				AccuracyLevel = WeaponAccuracyLevel.Surpassingly
			}},
			{ CraftResource.HickoryTree, new ResourceInfo {
				OreType = 1095403,
				MaterialName = "hickory",
				Style = "",
				DurabilityLevel = WeaponDurabilityLevel.Fortified,
				DamageLevel = WeaponDamageLevel.Force,
				AccuracyLevel = WeaponAccuracyLevel.Surpassingly
			}},
			{ CraftResource.RosewoodTree, new ResourceInfo {
				OreType = 1095407,
				MaterialName = "rosewood",
				Style = "",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Exceedingly
			}},
			{ CraftResource.ElvenTree, new ResourceInfo {
				OreType = 1095537,
				MaterialName = "elven",
				Style = "",
				DurabilityLevel = WeaponDurabilityLevel.Indestructible,
				DamageLevel = WeaponDamageLevel.Vanq,
				AccuracyLevel = WeaponAccuracyLevel.Supremely
			}}
		};
		
		#endregion
		
		#region Public Methods
		
		/// <summary>
		/// Gets the localized string ID (oreType) for a resource.
		/// Replaces switch statement in AddNameProperty.
		/// </summary>
		/// <param name="resource">The craft resource</param>
		/// <returns>The oreType ID, or 0 if not found</returns>
		public static int GetOreType(CraftResource resource)
		{
			ResourceInfo info;
			if (ResourceMap.TryGetValue(resource, out info))
				return info.OreType;
			return 0;
		}
		
		/// <summary>
		/// Gets the material hue for a resource.
		/// Replaces switch statement in GetMaterialHue.
		/// </summary>
		/// <param name="resource">The craft resource</param>
		/// <returns>The material hue, or 0 if not found or resource is None/Iron</returns>
		public static int GetMaterialHue(CraftResource resource)
		{
			if (resource == CraftResource.None || resource == CraftResource.Iron)
				return 0;
			
			ResourceInfo info;
			if (ResourceMap.TryGetValue(resource, out info) && !string.IsNullOrEmpty(info.MaterialName))
			{
				return MaterialInfo.GetMaterialColor(info.MaterialName, info.Style, 0);
			}
			
			return 0;
		}
		
		/// <summary>
		/// Gets the craft attributes for a resource (for non-AOS crafting).
		/// Replaces switch statement in OnCraft.
		/// </summary>
		/// <param name="resource">The craft resource</param>
		/// <param name="durabilityLevel">Output: Durability level, or null if not set</param>
		/// <param name="damageLevel">Output: Damage level, or null if not set</param>
		/// <param name="accuracyLevel">Output: Accuracy level, or null if not set</param>
		/// <returns>True if resource has craft attributes, false otherwise</returns>
		public static bool GetCraftAttributes(CraftResource resource, 
			out WeaponDurabilityLevel? durabilityLevel,
			out WeaponDamageLevel? damageLevel,
			out WeaponAccuracyLevel? accuracyLevel)
		{
			ResourceInfo info;
			if (ResourceMap.TryGetValue(resource, out info))
			{
				durabilityLevel = info.DurabilityLevel;
				damageLevel = info.DamageLevel;
				accuracyLevel = info.AccuracyLevel;
				return durabilityLevel.HasValue || damageLevel.HasValue || accuracyLevel.HasValue;
			}
			
			durabilityLevel = null;
			damageLevel = null;
			accuracyLevel = null;
			return false;
		}
		
		/// <summary>
		/// Gets complete resource information.
		/// </summary>
		/// <param name="resource">The craft resource</param>
		/// <returns>ResourceInfo object, or null if not found</returns>
		public static ResourceInfo GetResourceInfo(CraftResource resource)
		{
			ResourceInfo info;
			ResourceMap.TryGetValue(resource, out info);
			return info;
		}
		
		#endregion
	}
}

