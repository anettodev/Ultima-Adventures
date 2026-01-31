using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	/// <summary>
	/// Gump for selecting weapon pieces from WeaponStone, filtered by material type.
	/// </summary>
	public class WeaponStoneGump : BaseItemSelectionGump<WeaponStone>
	{
		public WeaponStoneGump( WeaponStone stone ) : base( stone )
		{
		}

		protected override List<ItemEntry> GetItemEntries()
		{
			if ( m_Token != null && !m_Token.Deleted )
			{
				return InitializeItems( m_Token.Material );
			}
			return new List<ItemEntry>();
		}

		/// <summary>
		/// Initializes the list of weapon items based on the material type.
		/// </summary>
		private static List<ItemEntry> InitializeItems( CraftResource material )
		{
			List<ItemEntry> items = new List<ItemEntry>();
			CraftResourceType resourceType = CraftResources.GetType( material );

			if ( resourceType == CraftResourceType.Metal )
			{
				// Metal materials can be used for: All melee weapons (swords, axes, maces, daggers, etc.)
				AddMetalWeaponItems( items, material );
			}
			else if ( resourceType == CraftResourceType.Wood )
			{
				// Wood materials can be used for: Bows, Crossbows, Staves
				AddWoodWeaponItems( items, material );
			}
			else if ( resourceType == CraftResourceType.Leather )
			{
				// Leather materials can be used for: Some weapons (less common)
				AddLeatherWeaponItems( items, material );
			}

			return items;
		}

		/// <summary>
		/// Adds metal weapon items (swords, axes, maces, daggers, etc.) to the list.
		/// </summary>
		private static void AddMetalWeaponItems( List<ItemEntry> items, CraftResource material )
		{
			int buttonID = 0x100;

			// Swords
			AddItem( items, ref buttonID, typeof( Broadsword ), 0x0F5E );
			AddItem( items, ref buttonID, typeof( Longsword ), 0x0F61 );
			AddItem( items, ref buttonID, typeof( Katana ), 0x13FF );
			AddItem( items, ref buttonID, typeof( Scimitar ), 0x13B6 );
			AddItem( items, ref buttonID, typeof( Cutlass ), 0x1441 );
			AddItem( items, ref buttonID, typeof( VikingSword ), 0x13B9 );
			AddItem( items, ref buttonID, typeof( Kryss ), 0x1401 );

			// Axes
			AddItem( items, ref buttonID, typeof( Axe ), 0x0F49 );
			AddItem( items, ref buttonID, typeof( BattleAxe ), 0x0F47 );
			AddItem( items, ref buttonID, typeof( DoubleAxe ), 0x0F4B );
			AddItem( items, ref buttonID, typeof( ExecutionersAxe ), 0x0F45 );
			AddItem( items, ref buttonID, typeof( LargeBattleAxe ), 0x13FB );
			AddItem( items, ref buttonID, typeof( TwoHandedAxe ), 0x1443 );
			AddItem( items, ref buttonID, typeof( WarAxe ), 0x13B0 );
			AddItem( items, ref buttonID, typeof( Hatchet ), 0x0F43 );

			// Maces and Hammers
			AddItem( items, ref buttonID, typeof( Mace ), 0x0F5C );
			AddItem( items, ref buttonID, typeof( Maul ), 0x143B );
			AddItem( items, ref buttonID, typeof( WarHammer ), 0x1439 );
			AddItem( items, ref buttonID, typeof( WarMace ), 0x1407 );
			AddItem( items, ref buttonID, typeof( HammerPick ), 0x143D );
			AddItem( items, ref buttonID, typeof( Club ), 0x13B4 );

			// Daggers and Knives
			AddItem( items, ref buttonID, typeof( Dagger ), 0x0F52 );
			AddItem( items, ref buttonID, typeof( ButcherKnife ), 0x13F6 );
			AddItem( items, ref buttonID, typeof( Cleaver ), 0x0EC3 );
			AddItem( items, ref buttonID, typeof( SkinningKnife ), 0x0EC4 );

			// Polearms and Spears
			AddItem( items, ref buttonID, typeof( Bardiche ), 0x0F4D );
			AddItem( items, ref buttonID, typeof( Halberd ), 0x143E );
			AddItem( items, ref buttonID, typeof( Spear ), 0x0F62 );
			AddItem( items, ref buttonID, typeof( ShortSpear ), 0x1403 );
			AddItem( items, ref buttonID, typeof( WarFork ), 0x1405 );
			AddItem( items, ref buttonID, typeof( Pitchfork ), 0x0E87 );

			// Tools that can be weapons
			AddItem( items, ref buttonID, typeof( Pickaxe ), 0x0E86 );

			// Shields (can be made with metal)
			AddItem( items, ref buttonID, typeof( Buckler ), 0x1B73 );
			AddItem( items, ref buttonID, typeof( BronzeShield ), 0x1B72 );
			AddItem( items, ref buttonID, typeof( MetalShield ), 0x1B7B );
			AddItem( items, ref buttonID, typeof( MetalKiteShield ), 0x1B74 );
			AddItem( items, ref buttonID, typeof( HeaterShield ), 0x1B76 );
			AddItem( items, ref buttonID, typeof( ChaosShield ), 0x1BC3 );
			AddItem( items, ref buttonID, typeof( OrderShield ), 0x1BC4 );
			AddItem( items, ref buttonID, typeof( ChampionShield ), 0x2B74 );
			AddItem( items, ref buttonID, typeof( JeweledShield ), 0x2B75 );
			AddItem( items, ref buttonID, typeof( CrestedShield ), 0x2FC9 );
			AddItem( items, ref buttonID, typeof( DarkShield ), 0x2FC8 );
			AddItem( items, ref buttonID, typeof( ElvenShield ), 0x2FCA );
			AddItem( items, ref buttonID, typeof( GuardsmanShield ), 0x2FCB );
		}

		/// <summary>
		/// Adds wood weapon items (bows, crossbows, staves) to the list.
		/// </summary>
		private static void AddWoodWeaponItems( List<ItemEntry> items, CraftResource material )
		{
			int buttonID = 0x100;

			// Bows
			AddItem( items, ref buttonID, typeof( Bow ), 0x13B2 );
			AddItem( items, ref buttonID, typeof( CompositeBow ), 0x26C2 );
			AddItem( items, ref buttonID, typeof( RepeatingCrossbow ), 0x26C3 );

			// Crossbows
			AddItem( items, ref buttonID, typeof( Crossbow ), 0x0F50 );
			AddItem( items, ref buttonID, typeof( HeavyCrossbow ), 0x13FD );

			// Staves
			AddItem( items, ref buttonID, typeof( QuarterStaff ), 0x0E89 );
			AddItem( items, ref buttonID, typeof( GnarledStaff ), 0x13F8 );
			AddItem( items, ref buttonID, typeof( BlackStaff ), 0x0DF1 );
			AddItem( items, ref buttonID, typeof( ShepherdsCrook ), 0x0E81 );

			// Shields (can be made with wood)
			AddItem( items, ref buttonID, typeof( WoodenShield ), 0x1B7A );
			AddItem( items, ref buttonID, typeof( WoodenKiteShield ), 0x1B79 );
		}

		/// <summary>
		/// Adds leather weapon items (less common, but some weapons can use leather).
		/// </summary>
		private static void AddLeatherWeaponItems( List<ItemEntry> items, CraftResource material )
		{
			int buttonID = 0x100;

			// Some weapons that might use leather (whips, etc.)
			// Note: Most weapons use metal or wood, so this list is smaller
			// If there are specific leather weapons in your shard, add them here
			
			// For now, we'll include some that might be crafted with leather
			// This is a placeholder - adjust based on your shard's weapon types
		}

		/// <summary>
		/// Helper method to add a weapon item to the list.
		/// </summary>
		private static void AddItem( List<ItemEntry> items, ref int buttonID, Type weaponType, int itemID )
		{
			ItemEntry entry = new ItemEntry( buttonID, weaponType, 0, 0, itemID, 0x0, 18, 8 );
			items.Add( entry );
			buttonID++;
		}

		protected override void OnItemSelected( Mobile from, WeaponStone stone, ItemEntry entry )
		{
			if ( stone == null || stone.Deleted || from == null )
				return;

			if ( entry.ItemTypes == null || entry.ItemTypes.Length == 0 )
				return;

			Container backpack = from.Backpack;
			if ( backpack == null )
			{
				from.SendMessage( "You need a backpack to receive weapons." );
				return;
			}

			// Create and give the weapon item
			Item item = null;
			foreach ( Type type in entry.ItemTypes )
			{
				try
				{
					item = Activator.CreateInstance( type ) as Item;
					if ( item != null )
					{
						// Set the resource for weapon items
						if ( item is BaseWeapon )
						{
							BaseWeapon weapon = (BaseWeapon)item;
							weapon.Resource = stone.Material;
							// Mark as made by Developer
							weapon.Crafter = Server.Mobiles.DeveloperMobile.Instance;
							weapon.PlayerConstructed = true;
							
							// Staff weapons (QuarterStaff, GnarledStaff, BlackStaff, ShepherdsCrook) 
							// should have SpellChanneling to prevent unequipping when casting spells
							if ( weapon is QuarterStaff || weapon is GnarledStaff || weapon is BlackStaff || weapon is ShepherdsCrook )
							{
								weapon.Attributes.SpellChanneling = 1;
							}
						}
						// Set the resource for shield items (shields are armor)
						else if ( item is BaseArmor )
						{
							BaseArmor armor = (BaseArmor)item;
							armor.Resource = stone.Material;
							// Mark as made by Developer
							armor.Crafter = Server.Mobiles.DeveloperMobile.Instance;
							armor.PlayerConstructed = true;
							
							// Buckler shield should have SpellChanneling to prevent unequipping when casting spells
							if ( armor is Buckler )
							{
								armor.Attributes.SpellChanneling = 1;
							}
						}

						from.AddToBackpack( item );
						string itemName = item.Name;
						if ( String.IsNullOrEmpty( itemName ) && item.LabelNumber > 0 )
							itemName = String.Format( "#{0}", item.LabelNumber );
						
						from.SendMessage( "You received: {0}", itemName );
					}
				}
				catch ( Exception ex )
				{
					Console.WriteLine( ex.Message );
					Console.WriteLine( ex.StackTrace );
				}
			}
		}

		protected override void AddItemEntry( ItemEntry entry, int x, int y )
		{
			AddImageTiledButton( x, y, ButtonNormalID, ButtonPressedID, entry.ButtonID, GumpButtonType.Reply, 0, entry.ImageID, entry.ImageHue, entry.ImageOffsetX, entry.ImageOffsetY );
			
			// Get item name for display
			string itemName = "Unknown Item";
			if ( entry.ItemTypes != null && entry.ItemTypes.Length > 0 )
			{
				try
				{
					Item tempItem = Activator.CreateInstance( entry.ItemTypes[0] ) as Item;
					if ( tempItem != null )
					{
						itemName = tempItem.Name;
						if ( String.IsNullOrEmpty( itemName ) && tempItem.LabelNumber > 0 )
							itemName = String.Format( "#{0}", tempItem.LabelNumber );
						tempItem.Delete();
					}
				}
				catch
				{
					// Use type name as fallback
					itemName = entry.ItemTypes[0].Name;
				}
			}

			AddHtml( x + 84, y, ItemWidth, ItemHeight, String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", TextColor, itemName ), false, false );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Token == null || m_Token.Deleted || info.ButtonID == 0 )
				return;

			ItemEntry entry;
			if ( m_ItemLookup.TryGetValue( info.ButtonID, out entry ) )
			{
				if ( entry.ItemTypes != null && entry.ItemTypes.Length > 0 )
				{
					OnItemSelected( sender.Mobile, m_Token, entry );
				}
				else
				{
					sender.Mobile.SendLocalizedMessage( 501311 ); // This option is currently disabled, while we evaluate it for game balance.
				}
			}
		}
	}
}

