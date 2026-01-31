using System;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Base class for all hide items.
	/// Provides common functionality for hide resource items including serialization, property display, resource type mapping, and scissor operations.
	/// </summary>
	public abstract class BaseHides : Item, ICommodity
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
		/// Initializes a new instance of BaseHides with default amount
		/// </summary>
		/// <param name="resource">The craft resource type</param>
		public BaseHides( CraftResource resource ) : this( resource, LeatherConstants.DEFAULT_AMOUNT )
		{
		}

		/// <summary>
		/// Initializes a new instance of BaseHides with specified amount
		/// </summary>
		/// <param name="resource">The craft resource type</param>
		/// <param name="amount">The amount of hides</param>
		public BaseHides( CraftResource resource, int amount ) : base( LeatherConstants.ITEM_ID_HIDES )
		{
			Stackable = true;
			Weight = LeatherConstants.WEIGHT_HIDES;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public BaseHides( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the hide item
		/// </summary>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) LeatherConstants.SERIALIZATION_VERSION_CURRENT );

			writer.Write( (int) m_Resource );
		}

		/// <summary>
		/// Deserializes the hide item
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
				list.Add( LeatherConstants.LABEL_MULTIPLE_ITEMS_FORMAT, "{0}\t{1}", Amount, LeatherStringConstants.ITEM_NAME_HIDES );
			else
				list.Add( LeatherStringConstants.ITEM_NAME_HIDES );
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
		/// Gets the label number for the hide type
		/// </summary>
		public override int LabelNumber
		{
			get
			{
				if ( m_Resource >= CraftResource.SpinedLeather && m_Resource <= CraftResource.BarbedLeather )
					return LeatherConstants.LABEL_SPINED_HIDES_BASE + (int)(m_Resource - CraftResource.SpinedLeather);

				if ( m_Resource == CraftResource.DinosaurLeather )
					return LeatherConstants.LABEL_DINOSAUR_HIDES;

				return LeatherConstants.LABEL_REGULAR_HIDES;
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

		/// <summary>
		/// Creates the appropriate leather type for this hide.
		/// Override in derived classes to return the correct leather type.
		/// </summary>
		/// <returns>The leather item to create when cutting this hide</returns>
		protected virtual BaseLeather CreateLeather()
		{
			return new Leather();
		}

		/// <summary>
		/// Cuts hides into leather using a dictionary factory pattern.
		/// Validates the hide and player, then creates the appropriate leather type.
		/// </summary>
		/// <param name="from">The mobile performing the cut</param>
		/// <param name="hide">The hide to cut</param>
		/// <returns>True if the cut was successful, false otherwise</returns>
		public static bool CutHides(Mobile from, BaseHides hide)
		{
			if (hide == null || hide.Deleted || !from.CanSee( hide ))
				return false;
				
			if ( !from.InRange( hide.GetWorldLocation(), LeatherConstants.SCISSOR_RANGE ) || !from.InLOS( hide ) )
				return false;
			
			BaseLeather leather = hide.CreateLeather();
			if (leather != null)
			{
				hide.ScissorHelper( from, leather, LeatherConstants.SCISSOR_AMOUNT );
			}
				
			return true;
		}

		#endregion
	}

	#region Hide Classes

	[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
	public class Hides : BaseHides, IScissorable
		{
			[Constructable]
			public Hides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public Hides( int amount ) : base( CraftResource.RegularLeather, amount )
			{
			}

			public Hides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE ) ) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates regular leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new Leather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class SpinedHides : BaseHides, IScissorable
		{
			[Constructable]
			public SpinedHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public SpinedHides( int amount ) : base( CraftResource.SpinedLeather, amount )
			{
			}

			public SpinedHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates spined leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new SpinedLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class HornedHides : BaseHides, IScissorable
		{
			[Constructable]
			public HornedHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public HornedHides( int amount ) : base( CraftResource.HornedLeather, amount )
			{
			}

			public HornedHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;
			
				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates horned leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new HornedLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class BarbedHides : BaseHides, IScissorable
		{
			[Constructable]
			public BarbedHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public BarbedHides( int amount ) : base( CraftResource.BarbedLeather, amount )
			{
			}

			public BarbedHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates barbed leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new BarbedLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class NecroticHides : BaseHides, IScissorable
		{
			[Constructable]
			public NecroticHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public NecroticHides( int amount ) : base( CraftResource.NecroticLeather, amount )
			{
			}

			public NecroticHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates necrotic leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new NecroticLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class VolcanicHides : BaseHides, IScissorable
		{
			[Constructable]
			public VolcanicHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public VolcanicHides( int amount ) : base( CraftResource.VolcanicLeather, amount )
			{
			}

			public VolcanicHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates volcanic leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new VolcanicLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class FrozenHides : BaseHides, IScissorable
		{
			[Constructable]
			public FrozenHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public FrozenHides( int amount ) : base( CraftResource.FrozenLeather, amount )
			{
			}

			public FrozenHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates frozen leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new FrozenLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class GoliathHides : BaseHides, IScissorable
		{
			[Constructable]
			public GoliathHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public GoliathHides( int amount ) : base( CraftResource.GoliathLeather, amount )
			{
			}

			public GoliathHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates goliath leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new GoliathLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class DraconicHides : BaseHides, IScissorable
		{
			[Constructable]
			public DraconicHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public DraconicHides( int amount ) : base( CraftResource.DraconicLeather, amount )
			{
			}

			public DraconicHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates draconic leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new DraconicLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class HellishHides : BaseHides, IScissorable
		{
			[Constructable]
			public HellishHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public HellishHides( int amount ) : base( CraftResource.HellishLeather, amount )
			{
			}

			public HellishHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates hellish leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new HellishLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class DinosaurHides : BaseHides, IScissorable
		{
			[Constructable]
			public DinosaurHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public DinosaurHides( int amount ) : base( CraftResource.DinosaurLeather, amount )
			{
			}

			public DinosaurHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates dinosaur leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new DinosaurLeather();
			}
		}

		[FlipableAttribute( LeatherConstants.ITEM_ID_HIDES, LeatherConstants.ITEM_ID_HIDES_FLIPABLE )]
		public class AlienHides : BaseHides, IScissorable
		{
			[Constructable]
			public AlienHides() : this( LeatherConstants.DEFAULT_AMOUNT )
			{
			}

			[Constructable]
			public AlienHides( int amount ) : base( CraftResource.AlienLeather, amount )
			{
			}

			public AlienHides( Serial serial ) : base( serial )
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

			/// <summary>
			/// Handles scissor operations on hides
			/// </summary>
			public bool Scissor( Mobile from, Scissors scissors )
			{
				if ( Deleted || !from.CanSee( this ) || !from.InRange( GetWorldLocation(), LeatherConstants.SCISSOR_RANGE )) 
					return false;

				base.ScissorHelper( from, CreateLeather(), LeatherConstants.SCISSOR_AMOUNT );

				return true;
			}

			/// <summary>
			/// Creates alien leather when cutting hides
			/// </summary>
			protected override BaseLeather CreateLeather()
			{
				return new AlienLeather();
			}
		}

	#endregion
}
