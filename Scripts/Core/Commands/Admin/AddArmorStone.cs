using System;
using Server;
using Server.Items;
using Server.Commands;

namespace Server.Commands
{
	public class AddArmorStone
	{
		public static void Initialize()
		{
			CommandSystem.Register( "AddArmorStone", AccessLevel.GameMaster, new CommandEventHandler( AddArmorStone_OnCommand ) );
		}

		[Usage( "AddArmorStone <material>" )]
		[Description( "Creates an Armor Stone that provides armor pieces made with the specified material (ingot or leather type)." )]
		private static void AddArmorStone_OnCommand( CommandEventArgs e )
		{
			if ( e.Arguments == null || e.Arguments.Length == 0 )
			{
				e.Mobile.SendMessage( "Usage: [add armorstone <material>" );
				e.Mobile.SendMessage( "Valid materials: titanium, rosenium, iron, valorite, barbedleather, spinedleather, etc." );
				return;
			}

			string materialName = e.ArgString.Trim().ToLower();

			// Try to parse the material name to CraftResource
			CraftResource material = ParseMaterialName( materialName );

			if ( material == CraftResource.None )
			{
				e.Mobile.SendMessage( "Invalid material name: {0}", materialName );
				e.Mobile.SendMessage( "Valid materials include: titanium, rosenium, iron, valorite, barbedleather, spinedleather, hornedleather, etc." );
				return;
			}

			// Validate that the material is valid for armor (Metal or Leather only)
			CraftResourceType resourceType = CraftResources.GetType( material );
			if ( resourceType != CraftResourceType.Metal && resourceType != CraftResourceType.Leather )
			{
				e.Mobile.SendMessage( "Material '{0}' cannot be used for armor. Only metal ingots and leather types are valid.", materialName );
				return;
			}

			// Create the armor stone
			ArmorStone stone = new ArmorStone( material );
			stone.MoveToWorld( e.Mobile.Location, e.Mobile.Map );

			e.Mobile.SendMessage( "Created {0} Armor Stone at your location.", material.ToString() );
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

