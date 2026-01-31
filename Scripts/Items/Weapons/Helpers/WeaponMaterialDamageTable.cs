using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Provides O(1) dictionary lookup for material damage values.
	/// Replaces the large switch statement in WeaponMaterialDamage with faster dictionary access.
	/// </summary>
	public static class WeaponMaterialDamageTable
	{
		private static readonly Dictionary<CraftResource, int> MaterialDamageTable = 
			new Dictionary<CraftResource, int>
		{
			// Metal resources
			{ CraftResource.DullCopper, 1 },
			{ CraftResource.ShadowIron, 2 },
			{ CraftResource.Copper, 3 },
			{ CraftResource.Bronze, 4 },
			{ CraftResource.Gold, 4 },
			{ CraftResource.Platinum, 4 },
			{ CraftResource.Agapite, 5 },
			{ CraftResource.Verite, 5 },
			{ CraftResource.Valorite, 6 },
			{ CraftResource.Nepturite, 6 },
			{ CraftResource.Obsidian, 6 },
			{ CraftResource.Steel, 7 },
			{ CraftResource.Brass, 8 },
			{ CraftResource.Mithril, 9 },
			{ CraftResource.Xormite, 9 },
			{ CraftResource.Titanium, 11 },
			{ CraftResource.Rosenium, 11 },
			{ CraftResource.Dwarven, 15 },
			
			// Leather resources
			{ CraftResource.SpinedLeather, 1 },
			{ CraftResource.HornedLeather, 2 },
			{ CraftResource.BarbedLeather, 3 },
			{ CraftResource.NecroticLeather, 4 },
			{ CraftResource.VolcanicLeather, 4 },
			{ CraftResource.FrozenLeather, 5 },
			{ CraftResource.GoliathLeather, 6 },
			{ CraftResource.DraconicLeather, 8 },
			{ CraftResource.HellishLeather, 9 },
			{ CraftResource.DinosaurLeather, 10 },
			{ CraftResource.AlienLeather, 12 },
			
			// Wood resources
			{ CraftResource.AshTree, 1 },
			{ CraftResource.CherryTree, 1 },
			{ CraftResource.EbonyTree, 2 },
			{ CraftResource.GoldenOakTree, 2 },
			{ CraftResource.HickoryTree, 3 },
			{ CraftResource.RosewoodTree, 5 },
			{ CraftResource.ElvenTree, 8 }
		};
		
		/// <summary>
		/// Gets the damage bonus for a given craft resource.
		/// Returns 0 if the resource is not found in the table.
		/// </summary>
		public static int GetMaterialDamage(CraftResource resource)
		{
			int damage;
			if (MaterialDamageTable.TryGetValue(resource, out damage))
				return damage;
			return 0;
		}
	}
}

