using System;
using Server;
using Server.Items;
using Server.Commands;

namespace Server.Commands
{
	public class AddWeaponStone
	{
		public static void Initialize()
		{
			CommandSystem.Register( "AddWeaponStone", AccessLevel.GameMaster, new CommandEventHandler( AddWeaponStone_OnCommand ) );
		}

		[Usage( "AddWeaponStone <material>" )]
		[Description( "Creates a Weapon Stone that provides weapon pieces made with the specified material (ingot, wood, or leather type)." )]
		private static void AddWeaponStone_OnCommand( CommandEventArgs e )
		{
			if ( e.Arguments == null || e.Arguments.Length == 0 )
			{
				e.Mobile.SendMessage( "Usage: [add weaponstone <material>" );
				e.Mobile.SendMessage( "Valid materials: titanium, rosenium, iron, valorite, elventree, ash, barbedleather, etc." );
				return;
			}

			string materialName = e.ArgString.Trim().ToLower();

			// Try to parse the material name to CraftResource
			CraftResource material = ParseMaterialName( materialName );

			if ( material == CraftResource.None )
			{
				e.Mobile.SendMessage( "Invalid material name: {0}", materialName );
				e.Mobile.SendMessage( "Valid materials include: titanium, rosenium, iron, valorite, elventree, ash, barbedleather, etc." );
				return;
			}

			// Validate that the material is valid for weapons (Metal, Wood, or Leather only)
			CraftResourceType resourceType = CraftResources.GetType( material );
			if ( resourceType != CraftResourceType.Metal && resourceType != CraftResourceType.Wood && resourceType != CraftResourceType.Leather )
			{
				e.Mobile.SendMessage( "Material '{0}' cannot be used for weapons. Only metal ingots, wood types, and leather types are valid.", materialName );
				return;
			}

			// Create the weapon stone
			WeaponStone stone = new WeaponStone( material );
			stone.MoveToWorld( e.Mobile.Location, e.Mobile.Map );

			e.Mobile.SendMessage( "Created {0} Weapon Stone at your location.", material.ToString() );
		}

		/// <summary>
		/// Parses a material name string to a CraftResource enum value.
		/// </summary>
		private static CraftResource ParseMaterialName( string materialName )
		{
			// Normalize the name (remove spaces, handle common variations)
			materialName = materialName.Replace( " ", "" ).Replace( "_", "" ).Replace( "-", "" ).ToLower();

			// Try to parse directly as enum name (case-insensitive)
			try
			{
				// First try exact match
				if ( Enum.IsDefined( typeof( CraftResource ), materialName ) )
				{
					return (CraftResource)Enum.Parse( typeof( CraftResource ), materialName, true );
				}

				// Try common variations
				string normalized = materialName;
				
				// Handle "leather" suffix variations
				if ( normalized.EndsWith( "leather" ) )
				{
					normalized = normalized.Substring( 0, normalized.Length - 7 );
					normalized = normalized + "Leather";
					
					// Capitalize first letter
					if ( normalized.Length > 0 )
					{
						normalized = Char.ToUpper( normalized[0] ) + normalized.Substring( 1 );
					}
				}
				// Handle "ingot" suffix (remove it, materials don't have "ingot" in enum)
				else if ( normalized.EndsWith( "ingot" ) )
				{
					normalized = normalized.Substring( 0, normalized.Length - 5 );
					// Capitalize first letter
					if ( normalized.Length > 0 )
					{
						normalized = Char.ToUpper( normalized[0] ) + normalized.Substring( 1 );
					}
				}
				// Handle "tree" or "wood" suffix
				else if ( normalized.EndsWith( "tree" ) || normalized.EndsWith( "wood" ) )
				{
					if ( normalized.EndsWith( "tree" ) )
					{
						normalized = normalized.Substring( 0, normalized.Length - 4 );
					}
					else
					{
						normalized = normalized.Substring( 0, normalized.Length - 4 );
					}
					
					// Capitalize first letter and add "Tree"
					if ( normalized.Length > 0 )
					{
						normalized = Char.ToUpper( normalized[0] ) + normalized.Substring( 1 );
						if ( normalized != "Regular" )
						{
							normalized = normalized + "Tree";
						}
						else
						{
							normalized = "RegularWood";
						}
					}
				}
				else
				{
					// Capitalize first letter
					if ( normalized.Length > 0 )
					{
						normalized = Char.ToUpper( normalized[0] ) + normalized.Substring( 1 );
					}
				}

				// Try parsing with normalized name
				if ( Enum.IsDefined( typeof( CraftResource ), normalized ) )
				{
					return (CraftResource)Enum.Parse( typeof( CraftResource ), normalized, true );
				}

				// Try common aliases
				switch ( materialName )
				{
					case "titanium":
					case "titaniumingot":
						return CraftResource.Titanium;
					case "rosenium":
					case "roseniumingot":
						return CraftResource.Rosenium;
					case "iron":
					case "ironingot":
						return CraftResource.Iron;
					case "valorite":
					case "valoriteingot":
						return CraftResource.Valorite;
					case "verite":
					case "veriteingot":
						return CraftResource.Verite;
					case "agapite":
					case "agapiteingot":
						return CraftResource.Agapite;
					case "gold":
					case "goldingot":
						return CraftResource.Gold;
					case "barbed":
					case "barbedleather":
						return CraftResource.BarbedLeather;
					case "spined":
					case "spinedleather":
						return CraftResource.SpinedLeather;
					case "horned":
					case "hornedleather":
						return CraftResource.HornedLeather;
					case "necrotic":
					case "necroticleather":
						return CraftResource.NecroticLeather;
					case "volcanic":
					case "volcanicleather":
						return CraftResource.VolcanicLeather;
					case "frozen":
					case "frozenleather":
						return CraftResource.FrozenLeather;
					case "goliath":
					case "goliathleather":
						return CraftResource.GoliathLeather;
					case "draconic":
					case "draconicleather":
						return CraftResource.DraconicLeather;
					case "hellish":
					case "hellishleather":
						return CraftResource.HellishLeather;
					case "dinosaur":
					case "dinosaurleather":
						return CraftResource.DinosaurLeather;
					case "alien":
					case "alienleather":
						return CraftResource.AlienLeather;
					case "regular":
					case "regularleather":
						return CraftResource.RegularLeather;
					case "elven":
					case "elventree":
					case "elvenwood":
						return CraftResource.ElvenTree;
					case "ash":
					case "ashtree":
					case "ashwood":
						return CraftResource.AshTree;
					case "ebony":
					case "ebonytree":
					case "ebonywood":
						return CraftResource.EbonyTree;
					case "cherry":
					case "cherrytree":
					case "cherrywood":
						return CraftResource.CherryTree;
					case "goldenoak":
					case "goldenoaktree":
					case "goldenoakwood":
						return CraftResource.GoldenOakTree;
					case "rosewood":
					case "rosewoodtree":
						return CraftResource.RosewoodTree;
					case "hickory":
					case "hickorytree":
					case "hickorywood":
						return CraftResource.HickoryTree;
					case "regularwood":
					case "wood":
						return CraftResource.RegularWood;
				}
			}
			catch
			{
				// Invalid enum value
			}

			return CraftResource.None;
		}
	}
}

