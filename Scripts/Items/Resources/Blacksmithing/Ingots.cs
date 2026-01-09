
using System;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Base class for all ingot types.
	/// Provides resource management, serialization, and display properties.
	/// </summary>
	public abstract class BaseIngot : Item, ICommodity
	{
		#region Constants

		/// <summary>
		/// Dictionary mapping CraftResource to label numbers for non-standard resources
		/// </summary>
		private static readonly Dictionary<CraftResource, int> LabelNumberMap = 
			new Dictionary<CraftResource, int>
		{
			{ CraftResource.Titanium, IngotConstants.LABEL_TITANIUM },
			{ CraftResource.Rosenium, IngotConstants.LABEL_ROSENIUM },
			{ CraftResource.Platinum, IngotConstants.LABEL_PLATINUM },
			{ CraftResource.Steel, IngotConstants.LABEL_STEEL },
			{ CraftResource.Brass, IngotConstants.LABEL_BRASS },
			{ CraftResource.Mithril, IngotConstants.LABEL_MITHRIL },
			{ CraftResource.Obsidian, IngotConstants.LABEL_OBSIDIAN },
			{ CraftResource.Nepturite, IngotConstants.LABEL_NEPTURITE },
			{ CraftResource.Xormite, IngotConstants.LABEL_XORMITE },
			{ CraftResource.Dwarven, IngotConstants.LABEL_DWARVEN }
		};

		/// <summary>
		/// Dictionary mapping CraftResource to display names for tooltip properties
		/// </summary>
		private static readonly Dictionary<CraftResource, string> ResourceTypeDisplayNames = 
			new Dictionary<CraftResource, string>
		{
			{ CraftResource.Iron, IngotNameConstants.IRON_DISPLAY_NAME },
			{ CraftResource.DullCopper, IngotNameConstants.DULL_COPPER_DISPLAY_NAME },
			{ CraftResource.ShadowIron, IngotNameConstants.SHADOW_IRON_DISPLAY_NAME },
			{ CraftResource.Copper, IngotNameConstants.COPPER_DISPLAY_NAME },
			{ CraftResource.Bronze, IngotNameConstants.BRONZE_DISPLAY_NAME },
			{ CraftResource.Gold, IngotNameConstants.GOLD_DISPLAY_NAME },
			{ CraftResource.Agapite, IngotNameConstants.AGAPITE_DISPLAY_NAME },
			{ CraftResource.Verite, IngotNameConstants.VERITE_DISPLAY_NAME },
			{ CraftResource.Valorite, IngotNameConstants.VALORITE_DISPLAY_NAME },
			{ CraftResource.Titanium, IngotNameConstants.TITANIUM_DISPLAY_NAME },
			{ CraftResource.Rosenium, IngotNameConstants.ROSENIUM_DISPLAY_NAME },
			{ CraftResource.Platinum, IngotNameConstants.PLATINUM_DISPLAY_NAME },
			{ CraftResource.Nepturite, IngotNameConstants.NEPTURITE_DISPLAY_NAME },
			{ CraftResource.Obsidian, IngotNameConstants.OBSIDIAN_DISPLAY_NAME },
			{ CraftResource.Mithril, IngotNameConstants.MITHRIL_DISPLAY_NAME },
			{ CraftResource.Xormite, IngotNameConstants.XORMITE_DISPLAY_NAME },
			{ CraftResource.Dwarven, IngotNameConstants.DWARVEN_DISPLAY_NAME },
			{ CraftResource.Steel, IngotNameConstants.STEEL_DISPLAY_NAME },
			{ CraftResource.Brass, IngotNameConstants.BRASS_DISPLAY_NAME }
		};

		#endregion

		#region Fields

		private CraftResource m_Resource;

		#endregion

		#region Properties

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

		public override double DefaultWeight
		{
			get { return IngotConstants.DEFAULT_WEIGHT; }
		}
		
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		#endregion

		#region Constructors

		public BaseIngot( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseIngot( CraftResource resource, int amount ) : base( IngotConstants.ITEM_ID_INGOT )
		{
			Stackable = true;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseIngot( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( IngotConstants.SERIALIZATION_VERSION_CURRENT );
			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case IngotConstants.SERIALIZATION_VERSION_CURRENT:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case IngotConstants.SERIALIZATION_VERSION_LEGACY:
				{
					m_Resource = DeserializeVersion0( reader );
					break;
				}
			}
		}

		/// <summary>
		/// Deserializes legacy version 0 format (OreInfo-based)
		/// </summary>
		/// <param name="reader">The generic reader</param>
		/// <returns>The deserialized CraftResource</returns>
		private CraftResource DeserializeVersion0( GenericReader reader )
		{
			OreInfo info;
			int oreInfoIndex = reader.ReadInt();

			switch ( oreInfoIndex )
			{
				case IngotConstants.ORE_INFO_IRON: info = OreInfo.Iron; break;
				case IngotConstants.ORE_INFO_DULL_COPPER: info = OreInfo.DullCopper; break;
				case IngotConstants.ORE_INFO_COPPER: info = OreInfo.Copper; break;
				case IngotConstants.ORE_INFO_BRONZE: info = OreInfo.Bronze; break;
				case IngotConstants.ORE_INFO_SHADOW_IRON: info = OreInfo.ShadowIron; break;
				case IngotConstants.ORE_INFO_PLATINUM: info = OreInfo.Platinum; break;
				case IngotConstants.ORE_INFO_GOLD: info = OreInfo.Gold; break;
				case IngotConstants.ORE_INFO_AGAPITE: info = OreInfo.Agapite; break;
				case IngotConstants.ORE_INFO_VERITE: info = OreInfo.Verite; break;
				case IngotConstants.ORE_INFO_VALORITE: info = OreInfo.Valorite; break;
				case IngotConstants.ORE_INFO_TITANIUM: info = OreInfo.Titanium; break;
				case IngotConstants.ORE_INFO_ROSENIUM: info = OreInfo.Rosenium; break;
				case IngotConstants.ORE_INFO_NEPTURITE: info = OreInfo.Nepturite; break;
				case IngotConstants.ORE_INFO_OBSIDIAN: info = OreInfo.Obsidian; break;
				case IngotConstants.ORE_INFO_MITHRIL: info = OreInfo.Mithril; break;
				case IngotConstants.ORE_INFO_XORMITE: info = OreInfo.Xormite; break;
				case IngotConstants.ORE_INFO_DWARVEN: info = OreInfo.Dwarven; break;
				default: info = null; break;
			}

			return CraftResources.GetFromOreInfo( info );
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Gets the display name for this ingot type.
		/// Can be overridden by derived classes to provide custom names.
		/// </summary>
		/// <returns>The display name for this ingot</returns>
		public virtual string GetIngotDisplayName()
		{
			return IngotNameConstants.GENERIC_INGOT_LABEL;
		}

		/// <summary>
		/// Gets the resource type display name for the tooltip properties.
		/// Maps CraftResource enum values to PT-BR display names from IngotNameConstants.
		/// </summary>
		/// <returns>The resource type display name, or null if not found</returns>
		public virtual string GetResourceTypeDisplayName()
		{
			string displayName;
			return ResourceTypeDisplayNames.TryGetValue( m_Resource, out displayName ) ? displayName : null;
		}

		/// <summary>
		/// Adds the name property to the object property list.
		/// Uses GetIngotDisplayName() to get the custom name if available.
		/// </summary>
		/// <param name="list">The object property list to add to</param>
		public override void AddNameProperty( ObjectPropertyList list )
		{
			string displayName = GetIngotDisplayName();
			
			if ( Amount > 1 )
				list.Add( OreConstants.MSG_ID_MULTIPLE_ITEMS_FORMAT, "{0}\t{1}", Amount, displayName );
			else
				list.Add( displayName );
		}

		/// <summary>
		/// Adds properties to the object property list (tooltip).
		/// Shows the resource type using custom PT-BR names (including Iron).
		/// </summary>
		/// <param name="list">The object property list to add to</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			// Try to get custom PT-BR display name first (includes Iron)
			string customName = GetResourceTypeDisplayName();
			
			if ( customName != null )
			{
				list.Add( IngotConstants.PROPERTY_LABEL_FORMAT_ID, ItemNameHue.UnifiedItemProps.SetColor( customName, OreStringConstants.COLOR_CYAN ) );
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

		public override int LabelNumber
		{
			get
			{
				// Check dictionary first for non-standard resources
				int labelNumber;
				if ( LabelNumberMap.TryGetValue( m_Resource, out labelNumber ) )
					return labelNumber;
				
				// Check DullCopper-Valorite range
				if ( m_Resource >= CraftResource.DullCopper && m_Resource <= CraftResource.Valorite )
					return IngotConstants.LABEL_BASE_DULL_COPPER_TO_VALORITE + (int)(m_Resource - CraftResource.DullCopper);
				
				// Default to Iron
				return IngotConstants.LABEL_DEFAULT_IRON;
			}
		}

		#endregion
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class IronIngot : BaseIngot
	{
		[Constructable]
		public IronIngot() : this( 1 )
		{
		}

		[Constructable]
		public IronIngot( int amount ) : base( CraftResource.Iron, amount )
		{
		}

		public IronIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class DullCopperIngot : BaseIngot
	{
		[Constructable]
		public DullCopperIngot() : this( 1 )
		{
		}

		[Constructable]
		public DullCopperIngot( int amount ) : base( CraftResource.DullCopper, amount )
		{
		}

		public DullCopperIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class ShadowIronIngot : BaseIngot
	{
		[Constructable]
		public ShadowIronIngot() : this( 1 )
		{
		}

		[Constructable]
		public ShadowIronIngot( int amount ) : base( CraftResource.ShadowIron, amount )
		{
		}

		public ShadowIronIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class CopperIngot : BaseIngot
	{
		[Constructable]
		public CopperIngot() : this( 1 )
		{
		}

		[Constructable]
		public CopperIngot( int amount ) : base( CraftResource.Copper, amount )
		{
		}

		public CopperIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class BronzeIngot : BaseIngot
	{
		[Constructable]
		public BronzeIngot() : this( 1 )
		{
		}

		[Constructable]
		public BronzeIngot( int amount ) : base( CraftResource.Bronze, amount )
		{
		}

		public BronzeIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class GoldIngot : BaseIngot
	{
		[Constructable]
		public GoldIngot() : this( 1 )
		{
		}

		[Constructable]
		public GoldIngot( int amount ) : base( CraftResource.Gold, amount )
		{
		}

		public GoldIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class AgapiteIngot : BaseIngot
	{
		[Constructable]
		public AgapiteIngot() : this( 1 )
		{
		}

		[Constructable]
		public AgapiteIngot( int amount ) : base( CraftResource.Agapite, amount )
		{
		}

		public AgapiteIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class VeriteIngot : BaseIngot
	{
		[Constructable]
		public VeriteIngot() : this( 1 )
		{
		}

		[Constructable]
		public VeriteIngot( int amount ) : base( CraftResource.Verite, amount )
		{
		}

		public VeriteIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class ValoriteIngot : BaseIngot
	{
		[Constructable]
		public ValoriteIngot() : this( 1 )
		{
		}

		[Constructable]
		public ValoriteIngot( int amount ) : base( CraftResource.Valorite, amount )
		{
		}

		public ValoriteIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

    [FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
    public class TitaniumIngot : BaseIngot
    {
        public override int Hue { get { return (Server.Misc.MaterialInfo.GetMaterialColor("titanium", "classic", 0)); } }
        
        [Constructable]
        public TitaniumIngot() : this(1)
        {
        }

        [Constructable]
        public TitaniumIngot(int amount) : base(CraftResource.Titanium, amount)
        {
        }

        public TitaniumIngot(Serial serial) : base(serial)
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
    public class RoseniumIngot : BaseIngot
    {
        public override int Hue { get { return (Server.Misc.MaterialInfo.GetMaterialColor("rosenium", "classic", 0)); } }

        [Constructable]
        public RoseniumIngot() : this(1)
        {
        }

        [Constructable]
        public RoseniumIngot(int amount) : base(CraftResource.Rosenium, amount)
        {
        }

        public RoseniumIngot(Serial serial) : base(serial)
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
    public class PlatinumIngot : BaseIngot
    {
        public override int Hue { get { return (Server.Misc.MaterialInfo.GetMaterialColor("platinum", "classic", 0)); } }

        [Constructable]
        public PlatinumIngot() : this(1)
        {
        }

        [Constructable]
        public PlatinumIngot(int amount) : base(CraftResource.Platinum, amount)
        {
        }

        public PlatinumIngot(Serial serial) : base(serial)
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class SteelIngot : BaseIngot
	{
		[Constructable]
		public SteelIngot() : this( 1 )
		{
		}

		[Constructable]
		public SteelIngot( int amount ) : base( CraftResource.Steel, amount )
		{
		}

		public SteelIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class BrassIngot : BaseIngot
	{
		[Constructable]
		public BrassIngot() : this( 1 )
		{
		}

		[Constructable]
		public BrassIngot( int amount ) : base( CraftResource.Brass, amount )
		{
		}

		public BrassIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class MithrilIngot : BaseIngot
	{
		[Constructable]
		public MithrilIngot() : this( 1 )
		{
		}

		[Constructable]
		public MithrilIngot( int amount ) : base( CraftResource.Mithril, amount )
		{
		}

		public MithrilIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class DwarvenIngot : BaseIngot
	{
		[Constructable]
		public DwarvenIngot() : this( 1 )
		{
		}

		[Constructable]
		public DwarvenIngot( int amount ) : base( CraftResource.Dwarven, amount )
		{
		}

		public DwarvenIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class XormiteIngot : BaseIngot
	{
		[Constructable]
		public XormiteIngot() : this( 1 )
		{
		}

		[Constructable]
		public XormiteIngot( int amount ) : base( CraftResource.Xormite, amount )
		{
		}

		public XormiteIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class ObsidianIngot : BaseIngot
	{
		[Constructable]
		public ObsidianIngot() : this( 1 )
		{
		}

		[Constructable]
		public ObsidianIngot( int amount ) : base( CraftResource.Obsidian, amount )
		{
		}

		public ObsidianIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( IngotConstants.ITEM_ID_INGOT, IngotConstants.ITEM_ID_INGOT_FLIPPED )]
	public class NepturiteIngot : BaseIngot
	{
		[Constructable]
		public NepturiteIngot() : this( 1 )
		{
		}

		[Constructable]
		public NepturiteIngot( int amount ) : base( CraftResource.Nepturite, amount )
		{
		}

		public NepturiteIngot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( IngotConstants.SERIALIZATION_VERSION_LEGACY );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}