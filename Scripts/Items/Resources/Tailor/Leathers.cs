using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Base class for all leather items.
	/// Provides common functionality for leather resource items including serialization, property display, and resource type mapping.
	/// </summary>
	public abstract class BaseLeather : Item, ICommodity
	{
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
		
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseLeather with default amount
		/// </summary>
		/// <param name="resource">The craft resource type</param>
		public BaseLeather( CraftResource resource ) : this( resource, LeatherConstants.DEFAULT_AMOUNT )
		{
		}

		/// <summary>
		/// Initializes a new instance of BaseLeather with specified amount
		/// </summary>
		/// <param name="resource">The craft resource type</param>
		/// <param name="amount">The amount of leather</param>
		public BaseLeather( CraftResource resource, int amount ) : base( LeatherConstants.ITEM_ID_LEATHER )
		{
			Stackable = true;
			Weight = LeatherConstants.WEIGHT_LEATHER;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public BaseLeather( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the leather item
		/// </summary>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_CURRENT );

			writer.Write( (int) m_Resource );
		}

		/// <summary>
		/// Deserializes the leather item
		/// </summary>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case LeatherConstants.SERIALIZATION_VERSION_CURRENT:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case LeatherConstants.SERIALIZATION_VERSION_LEGACY:
				{
					OreInfo info = new OreInfo( reader.ReadInt(), reader.ReadInt(), reader.ReadString() );

					m_Resource = CraftResources.GetFromOreInfo( info );
					break;
				}
			}
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds the name property to the object property list
		/// </summary>
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( LeatherConstants.LABEL_MULTIPLE_ITEMS_FORMAT, "{0}\t{1}", Amount, LeatherNameConstants.GENERIC_LEATHER_LABEL );
			else
				list.Add( LeatherNameConstants.GENERIC_LEATHER_LABEL );
		}

		/// <summary>
		/// Adds properties to the object property list (tooltip).
		/// Shows the resource type using custom PT-BR names in cyan color.
		/// </summary>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			// Try to get custom PT-BR display name first (includes RegularLeather)
			string customName = GetResourceTypeDisplayName();
			
			if ( customName != null )
			{
				list.Add( LeatherConstants.LABEL_PROPERTY_DISPLAY, ItemNameHue.UnifiedItemProps.SetColor( customName, LeatherStringConstants.COLOR_CYAN ) );
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

		/// <summary>
		/// Gets the label number for the leather type
		/// </summary>
		public override int LabelNumber
		{
			get
			{
				if ( m_Resource >= CraftResource.SpinedLeather && m_Resource <= CraftResource.BarbedLeather )
					return LeatherConstants.LABEL_SPINED_LEATHER_BASE + (int)(m_Resource - CraftResource.SpinedLeather);

				if ( m_Resource == CraftResource.DinosaurLeather )
					return LeatherConstants.LABEL_DINOSAUR_LEATHER;

				return LeatherConstants.LABEL_REGULAR_LEATHER;
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the resource type display name for the tooltip properties.
		/// Maps CraftResource enum values to PT-BR display names from LeatherNameConstants.
		/// </summary>
		/// <returns>The resource type display name, or null if not found</returns>
		public virtual string GetResourceTypeDisplayName()
		{
			switch ( m_Resource )
			{
				case CraftResource.RegularLeather: return LeatherNameConstants.REGULAR_DISPLAY_NAME;
				case CraftResource.SpinedLeather: return LeatherNameConstants.SPINED_DISPLAY_NAME;
				case CraftResource.HornedLeather: return LeatherNameConstants.HORNED_DISPLAY_NAME;
				case CraftResource.BarbedLeather: return LeatherNameConstants.BARBED_DISPLAY_NAME;
				case CraftResource.NecroticLeather: return LeatherNameConstants.NECROTIC_DISPLAY_NAME;
				case CraftResource.VolcanicLeather: return LeatherNameConstants.VOLCANIC_DISPLAY_NAME;
				case CraftResource.FrozenLeather: return LeatherNameConstants.FROZEN_DISPLAY_NAME;
				case CraftResource.GoliathLeather: return LeatherNameConstants.GOLIATH_DISPLAY_NAME;
				case CraftResource.DraconicLeather: return LeatherNameConstants.DRACONIC_DISPLAY_NAME;
				case CraftResource.HellishLeather: return LeatherNameConstants.HELLISH_DISPLAY_NAME;
				case CraftResource.DinosaurLeather: return LeatherNameConstants.DINOSAUR_DISPLAY_NAME;
				case CraftResource.AlienLeather: return LeatherNameConstants.ALIEN_DISPLAY_NAME;
				default: return null;
			}
		}

		#endregion
	}

	#region Leather Classes

	[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
	public class Leather : BaseLeather
		{
			[Constructable]
			public Leather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
				Name = LeatherStringConstants.ITEM_NAME_LEATHER;
			}

			[Constructable]
			public Leather( int amount ) : base( CraftResource.RegularLeather, amount )
			{
				Name = LeatherStringConstants.ITEM_NAME_LEATHER;
			}

			public Leather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class SpinedLeather : BaseLeather
		{
			[Constructable]
			public SpinedLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
				Name = LeatherStringConstants.ITEM_NAME_SPINED;
			}

			[Constructable]
			public SpinedLeather( int amount ) : base( CraftResource.SpinedLeather, amount )
			{
				Name = LeatherStringConstants.ITEM_NAME_SPINED;
			}

			public SpinedLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class HornedLeather : BaseLeather
		{
			[Constructable]
			public HornedLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
				Name = LeatherStringConstants.ITEM_NAME_HORNED;
			}

			[Constructable]
			public HornedLeather( int amount ) : base( CraftResource.HornedLeather, amount )
			{
				Name = LeatherStringConstants.ITEM_NAME_HORNED;
			}

			public HornedLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class BarbedLeather : BaseLeather
		{
			[Constructable]
			public BarbedLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
				Name = LeatherStringConstants.ITEM_NAME_BARBED;
			}

			[Constructable]
			public BarbedLeather( int amount ) : base( CraftResource.BarbedLeather, amount )
			{
				Name = LeatherStringConstants.ITEM_NAME_BARBED;
			}

			public BarbedLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class NecroticLeather : BaseLeather
		{
			[Constructable]
			public NecroticLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public NecroticLeather( int amount ) : base( CraftResource.NecroticLeather, amount )
			{
			}

			public NecroticLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class VolcanicLeather : BaseLeather
		{
			[Constructable]
			public VolcanicLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
				Name = LeatherStringConstants.ITEM_NAME_VOLCANIC;
			}

			[Constructable]
			public VolcanicLeather( int amount ) : base( CraftResource.VolcanicLeather, amount )
			{
				Name = LeatherStringConstants.ITEM_NAME_VOLCANIC;
			}

			public VolcanicLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class FrozenLeather : BaseLeather
		{
			[Constructable]
			public FrozenLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public FrozenLeather( int amount ) : base( CraftResource.FrozenLeather, amount )
			{
			}

			public FrozenLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class GoliathLeather : BaseLeather
		{
			[Constructable]
			public GoliathLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public GoliathLeather( int amount ) : base( CraftResource.GoliathLeather, amount )
			{
			}

			public GoliathLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class DraconicLeather : BaseLeather
		{
			[Constructable]
			public DraconicLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public DraconicLeather( int amount ) : base( CraftResource.DraconicLeather, amount )
			{
			}

			public DraconicLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class HellishLeather : BaseLeather
		{
			[Constructable]
			public HellishLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public HellishLeather( int amount ) : base( CraftResource.HellishLeather, amount )
			{
			}

			public HellishLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class DinosaurLeather : BaseLeather
		{
			[Constructable]
			public DinosaurLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public DinosaurLeather( int amount ) : base( CraftResource.DinosaurLeather, amount )
			{
			}

			public DinosaurLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_LEATHER, LeatherConstants.ITEM_ID_LEATHER_FLIPABLE )]
		public class AlienLeather : BaseLeather
		{
			[Constructable]
			public AlienLeather() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public AlienLeather( int amount ) : base( CraftResource.AlienLeather, amount )
			{
			}

			public AlienLeather( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_LEGACY );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

	#endregion
}
