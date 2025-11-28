using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Future resource definitions for board types that are currently gated/unavailable.
	/// These resources are preserved for future content releases.
	/// When these resources are enabled, uncomment the corresponding entries and add them to BoardConstants.LOCALIZATION_IDS.
	/// </summary>
	public static class BoardFutureResources
	{
		#region Future Localization IDs

		/// <summary>
		/// Future localization IDs for gated wood types.
		/// These are preserved from the original commented-out switch cases in Board.cs.
		/// </summary>
		public static class FutureLocalizationIds
		{
			/// <summary>Future localization ID for MahoganyTree boards</summary>
			public const int LOCALIZATION_ID_MAHOGANY = 1095394;

			/// <summary>Future localization ID for DriftwoodTree boards</summary>
			public const int LOCALIZATION_ID_DRIFTWOOD = 1095410;

			/// <summary>Future localization ID for OakTree boards</summary>
			public const int LOCALIZATION_ID_OAK = 1095395;

			/// <summary>Future localization ID for PineTree boards</summary>
			public const int LOCALIZATION_ID_PINE = 1095396;

			/// <summary>Future localization ID for GhostTree boards</summary>
			public const int LOCALIZATION_ID_GHOST = 1095512;

			/// <summary>Future localization ID for WalnutTree boards</summary>
			public const int LOCALIZATION_ID_WALNUT = 1095398;

			/// <summary>Future localization ID for PetrifiedTree boards</summary>
			public const int LOCALIZATION_ID_PETRIFIED = 1095533;
		}

		#endregion

		#region Future Board Class Templates

		/*
		 * FUTURE BOARD CLASSES - Uncomment when CraftResource enum values are enabled
		 * 
		 * These classes are preserved from the original Board.cs for future implementation.
		 * When enabling these resources:
		 * 1. Uncomment the CraftResource enum values in ResourceInfo.cs
		 * 2. Add localization ID mappings to BoardConstants.LOCALIZATION_IDS
		 * 3. Uncomment the appropriate board class below
		 * 4. Ensure corresponding Log classes exist (e.g., MahoganyLog, OakLog, etc.)
		 */

		/*
		public class MahoganyBoard : BaseWoodBoard
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
		}
		*/

		/*
		public class OakBoard : BaseWoodBoard
		{
			[Constructable]
			public OakBoard() : this( 1 )
			{
			}

			[Constructable]
			public OakBoard( int amount ) : base( CraftResource.OakTree, amount )
			{
			}

			public OakBoard( Serial serial ) : base( serial )
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
		*/

		/*
		public class PineBoard : BaseWoodBoard
		{
			[Constructable]
			public PineBoard() : this( 1 )
			{
			}

			[Constructable]
			public PineBoard( int amount ) : base( CraftResource.PineTree, amount )
			{
			}

			public PineBoard( Serial serial ) : base( serial )
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
		*/

		/*
		public class WalnutBoard : BaseWoodBoard
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
		}
		*/

		/*
		public class DriftwoodBoard : BaseWoodBoard
		{
			[Constructable]
			public DriftwoodBoard() : this( 1 )
			{
			}

			[Constructable]
			public DriftwoodBoard( int amount ) : base( CraftResource.DriftwoodTree, amount )
			{
			}

			public DriftwoodBoard( Serial serial ) : base( serial )
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
		*/

		/*
		public class GhostBoard : BaseWoodBoard
		{
			[Constructable]
			public GhostBoard() : this( 1 )
			{
			}

			[Constructable]
			public GhostBoard( int amount ) : base( CraftResource.GhostTree, amount )
			{
			}

			public GhostBoard( Serial serial ) : base( serial )
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
		*/

		/*
		public class PetrifiedBoard : BaseWoodBoard
		{
			[Constructable]
			public PetrifiedBoard() : this( 1 )
			{
			}

			[Constructable]
			public PetrifiedBoard( int amount ) : base( CraftResource.PetrifiedTree, amount )
			{
			}

			public PetrifiedBoard( Serial serial ) : base( serial )
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
		*/

		#endregion
	}
}

