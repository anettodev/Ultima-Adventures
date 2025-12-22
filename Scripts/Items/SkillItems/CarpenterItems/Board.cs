using System;
using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Base class for all board types that can be used in carpentry.
	/// Handles resource properties, tooltip display, and serialization.
	/// </summary>
	public class BaseWoodBoard : Item, ICommodity
	{
		#region Fields

		private CraftResource m_Resource;

		#endregion

		#region Properties

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get { return m_Resource; }
			set { m_Resource = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets the localization number for the board's resource type.
		/// Uses dictionary lookup for better performance than switch statement.
		/// </summary>
		int ICommodity.DescriptionNumber 
		{ 
			get
			{
				int id;
				return BoardConstants.LOCALIZATION_IDS.TryGetValue( m_Resource, out id ) ? id : LabelNumber;
			} 
		}

		bool ICommodity.IsDeedable { get { return true; } }

		#endregion

		[Constructable]
		public BaseWoodBoard() : this( 1 )
		{
			Name = BoardStringConstants.ITEM_NAME_BOARD;
		}

		[Constructable]
		public BaseWoodBoard( int amount ) : this( CraftResource.RegularWood, amount )
		{
            Name = BoardStringConstants.ITEM_NAME_BOARD;
        }

		public BaseWoodBoard( Serial serial ) : base( serial )
		{
            Name = BoardStringConstants.ITEM_NAME_BOARD;
        }

		[Constructable]
		public BaseWoodBoard( CraftResource resource ) : this( resource, 1 )
		{
		}

		[Constructable]
		public BaseWoodBoard( CraftResource resource, int amount ) : base( BoardConstants.ITEM_ID_BOARD )
		{
			Stackable = true;
			Amount = amount;
			Weight = BoardConstants.WEIGHT_CONSTRUCTOR;
			m_Resource = resource;
			Hue = CraftResources.GetHue( resource );
		}

		#region Property Display

		/// <summary>
		/// Gets the resource type display name for the tooltip properties.
		/// Maps CraftResource enum values to PT-BR display names from BoardNameConstants.
		/// Boards use the same wood types as logs, so we reuse LogNameConstants values.
		/// </summary>
		/// <returns>The resource type display name, or null if not found</returns>
		public virtual string GetResourceTypeDisplayName()
		{
			switch ( m_Resource )
			{
				case CraftResource.RegularWood: return BoardNameConstants.REGULAR_WOOD_DISPLAY_NAME;
				case CraftResource.AshTree: return BoardNameConstants.ASH_TREE_DISPLAY_NAME;
				case CraftResource.EbonyTree: return BoardNameConstants.EBONY_TREE_DISPLAY_NAME;
				case CraftResource.ElvenTree: return BoardNameConstants.ELVEN_TREE_DISPLAY_NAME;
				case CraftResource.GoldenOakTree: return BoardNameConstants.GOLDEN_OAK_TREE_DISPLAY_NAME;
				case CraftResource.CherryTree: return BoardNameConstants.CHERRY_TREE_DISPLAY_NAME;
				case CraftResource.RosewoodTree: return BoardNameConstants.ROSEWOOD_TREE_DISPLAY_NAME;
				case CraftResource.HickoryTree: return BoardNameConstants.HICKORY_TREE_DISPLAY_NAME;
				default: return null;
			}
		}

		/// <summary>
		/// Adds properties to the object property list (tooltip).
		/// Shows the resource type using custom PT-BR names (including RegularWood).
		/// </summary>
		/// <param name="list">The object property list to add to</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			// Try to get custom PT-BR display name first (includes RegularWood)
			string customName = GetResourceTypeDisplayName();

			if ( customName != null )
			{
				list.Add( 1053099, ItemNameHue.UnifiedItemProps.SetColor( customName, BoardStringConstants.COLOR_CYAN ) );
			}
			else if ( !CraftResources.IsStandard( m_Resource ) )
			{
				// Fallback to original system if custom name not found (only for non-standard resources)
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
				{
					string resourceName = CraftResources.GetName( m_Resource );
					if ( !string.IsNullOrEmpty( resourceName ) )
						list.Add( resourceName );
				}
			}
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_CURRENT );

			writer.Write( (int)m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				case 2:
					{
						m_Resource = (CraftResource)reader.ReadInt();
						break;
					}
			}

			if ( Weight != BoardConstants.WEIGHT_DESERIALIZED )
				Weight = BoardConstants.WEIGHT_DESERIALIZED;

			if ( version <= 1 )
				m_Resource = CraftResource.RegularWood;

			ItemID = BoardConstants.ITEM_ID_BOARD;
		}

		#endregion
	}

	#region Board Classes

	/// <summary>
	/// Regular wood board - standard board type
	/// </summary>
	public class Board : BaseWoodBoard
	{
		[Constructable]
		public Board() : this( 1 )
		{
		}

		[Constructable]
		public Board( int amount ) : base( CraftResource.RegularWood, amount )
		{
		}

		public Board( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class AshBoard : BaseWoodBoard
	{
		[Constructable]
		public AshBoard() : this( 1 )
		{
		}

		[Constructable]
		public AshBoard( int amount ) : base( CraftResource.AshTree, amount )
		{
		}

		public AshBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CherryBoard : BaseWoodBoard
	{
		[Constructable]
		public CherryBoard() : this( 1 )
		{
		}

		[Constructable]
		public CherryBoard( int amount ) : base( CraftResource.CherryTree, amount )
		{
		}

		public CherryBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class EbonyBoard : BaseWoodBoard
	{
		[Constructable]
		public EbonyBoard() : this( 1 )
		{
		}

		[Constructable]
		public EbonyBoard( int amount ) : base( CraftResource.EbonyTree, amount )
		{
		}

		public EbonyBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class GoldenOakBoard : BaseWoodBoard
	{
		[Constructable]
		public GoldenOakBoard() : this( 1 )
		{
		}

		[Constructable]
		public GoldenOakBoard( int amount ) : base( CraftResource.GoldenOakTree, amount )
		{
		}

		public GoldenOakBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class HickoryBoard : BaseWoodBoard
	{
		[Constructable]
		public HickoryBoard() : this( 1 )
		{
		}

		[Constructable]
		public HickoryBoard( int amount ) : base( CraftResource.HickoryTree, amount )
		{
		}

		public HickoryBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	/*public class MahoganyBoard : BaseWoodBoard
	{
		[Constructable]
		public MahoganyBoard() : this( 1 )
		{
		}

		[Constructable]
		public MahoganyBoard( int amount ) : base( CraftResource.MahoganyTree, amount )
		{
		}

		public MahoganyBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}*/
	public class RosewoodBoard : BaseWoodBoard
	{
		[Constructable]
		public RosewoodBoard() : this( 1 )
		{
		}

		[Constructable]
		public RosewoodBoard( int amount ) : base( CraftResource.RosewoodTree, amount )
		{
		}

		public RosewoodBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	/*public class WalnutBoard : BaseWoodBoard
	{
		[Constructable]
		public WalnutBoard() : this( 1 )
		{
		}

		[Constructable]
		public WalnutBoard( int amount ) : base( CraftResource.WalnutTree, amount )
		{
		}

		public WalnutBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}*/
	public class ElvenBoard : BaseWoodBoard
	{
		[Constructable]
		public ElvenBoard() : this( 1 )
		{
		}

		[Constructable]
		public ElvenBoard( int amount ) : base( CraftResource.ElvenTree, amount )
		{
		}

		public ElvenBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)BoardConstants.SERIALIZATION_VERSION_DEFAULT ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	#endregion
}