using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// A stone that provides weapon pieces made with a specific material (ingot, wood, or leather type).
	/// Created via [add weaponstone <material> command.
	/// </summary>
	public class WeaponStone : Item
	{
		#region Constants

		/// <summary>Item ID for the weapon stone appearance.</summary>
		private const int ITEM_ID = 0xED4;

		/// <summary>Default hue color for the weapon stone.</summary>
		private const int STONE_HUE = 0x0;

		/// <summary>Serialization version number.</summary>
		private const int SERIALIZATION_VERSION = 0;

		#endregion

		#region Properties

		private CraftResource m_Material;

		/// <summary>
		/// Gets or sets the material type this stone provides weapons for.
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
		/// Gets the default name of the weapon stone.
		/// </summary>
		public override string DefaultName
		{
			get
			{
				if ( m_Material == CraftResource.None )
					return "Weapon Stone";
				
				string materialName = m_Material.ToString();
				return String.Format( "{0} Weapon Stone", materialName );
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the WeaponStone class.
		/// </summary>
		[Constructable]
		public WeaponStone()
			: this( CraftResource.None )
		{
		}

		/// <summary>
		/// Initializes a new instance of the WeaponStone class with a specific material.
		/// </summary>
		[Constructable]
		public WeaponStone( CraftResource material )
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
		public WeaponStone( Serial serial )
			: base( serial )
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Handles double-click interaction. Opens gump for weapon selection.
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
				from.SendMessage( "This weapon stone has no material assigned." );
				return;
			}

			// Validate that the material is valid for weapons (Metal, Wood, or Leather)
			CraftResourceType resourceType = CraftResources.GetType( m_Material );
			if ( resourceType != CraftResourceType.Metal && resourceType != CraftResourceType.Wood && resourceType != CraftResourceType.Leather )
			{
				from.SendMessage( "This material type cannot be used for weapons." );
				return;
			}

			from.SendGump( new WeaponStoneGump( this ) );
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

