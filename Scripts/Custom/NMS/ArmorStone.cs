using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// A stone that provides armor pieces made with a specific material (ingot or leather type).
	/// Created via [add armorstone <material> command.
	/// </summary>
	public class ArmorStone : Item
	{
		#region Constants

		/// <summary>Item ID for the armor stone appearance.</summary>
		private const int ITEM_ID = 0xED4;

		/// <summary>Default hue color for the armor stone.</summary>
		private const int STONE_HUE = 0x0;

		/// <summary>Serialization version number.</summary>
		private const int SERIALIZATION_VERSION = 0;

		#endregion

		#region Properties

		private CraftResource m_Material;

		/// <summary>
		/// Gets or sets the material type this stone provides armor for.
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Material
		{
			get { return m_Material; }
			set
			{
				m_Material = value;
				// Update the stone's hue to match the material color
				if ( m_Material != CraftResource.None )
				{
					Hue = CraftResources.GetHue( m_Material );
				}
				else
				{
					Hue = STONE_HUE;
				}
				InvalidateProperties();
			}
		}

		/// <summary>
		/// Gets the default name of the armor stone.
		/// </summary>
		public override string DefaultName
		{
			get
			{
				if ( m_Material == CraftResource.None )
					return "Armor Stone";
				
				string materialName = m_Material.ToString();
				return String.Format( "{0} Armor Stone", materialName );
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ArmorStone class.
		/// </summary>
		[Constructable]
		public ArmorStone()
			: this( CraftResource.None )
		{
		}

		/// <summary>
		/// Initializes a new instance of the ArmorStone class with a specific material.
		/// </summary>
		[Constructable]
		public ArmorStone( CraftResource material )
			: base( ITEM_ID )
		{
			Movable = false;
			m_Material = material;
			// Set the stone's hue to match the material color
			if ( m_Material != CraftResource.None )
			{
				Hue = CraftResources.GetHue( m_Material );
			}
			else
			{
				Hue = STONE_HUE;
			}
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public ArmorStone( Serial serial )
			: base( serial )
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Handles double-click interaction. Opens gump for armor selection.
		/// </summary>
		public override void OnDoubleClick( Mobile from )
		{
			if ( from == null || from.Deleted )
				return;

			if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
				return;
			}

			if ( m_Material == CraftResource.None )
			{
				from.SendMessage( "This armor stone has no material assigned." );
				return;
			}

			// Validate that the material is valid for armor (Metal or Leather)
			CraftResourceType resourceType = CraftResources.GetType( m_Material );
			if ( resourceType != CraftResourceType.Metal && resourceType != CraftResourceType.Leather )
			{
				from.SendMessage( "This material type cannot be used for armor." );
				return;
			}

			from.SendGump( new ArmorStoneGump( this ) );
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) SERIALIZATION_VERSION );
			writer.Write( (int) m_Material );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			if ( version >= 0 )
			{
				m_Material = (CraftResource)reader.ReadInt();
				// Restore the stone's hue to match the material color
				if ( m_Material != CraftResource.None )
				{
					Hue = CraftResources.GetHue( m_Material );
				}
			}
		}

		#endregion
	}
}

