using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Future resource definitions for log types that are currently gated/unavailable.
	/// These resources are preserved for future content releases.
	/// When these resources are enabled, uncomment the corresponding entries and add them to LogConstants.RESOURCE_DIFFICULTY.
	/// </summary>
	public static class LogFutureResources
	{
		#region Future Difficulty Values

		/// <summary>
		/// Future difficulty values for gated wood types.
		/// These are preserved from the original commented-out switch cases in Log.cs.
		/// </summary>
		public static class FutureDifficulties
		{
			/// <summary>Future difficulty for MahoganyTree logs</summary>
			public const double DIFFICULTY_MAHOGANY = 80.0;

			/// <summary>Future difficulty for DriftwoodTree logs</summary>
			public const double DIFFICULTY_DRIFTWOOD = 80.0;

			/// <summary>Future difficulty for OakTree logs</summary>
			public const double DIFFICULTY_OAK = 85.0;

			/// <summary>Future difficulty for PineTree logs</summary>
			public const double DIFFICULTY_PINE = 90.0;

			/// <summary>Future difficulty for GhostTree logs</summary>
			public const double DIFFICULTY_GHOST = 90.0;

			/// <summary>Future difficulty for WalnutTree logs</summary>
			public const double DIFFICULTY_WALNUT = 99.0;

			/// <summary>Future difficulty for PetrifiedTree logs</summary>
			public const double DIFFICULTY_PETRIFIED = 99.9;
		}

		#endregion

		#region Future Log Class Templates

		/*
		 * FUTURE LOG CLASSES - Uncomment when CraftResource enum values are enabled
		 * 
		 * These classes are preserved from the original Log.cs for future implementation.
		 * When enabling these resources:
		 * 1. Uncomment the CraftResource enum values in ResourceInfo.cs
		 * 2. Add difficulty mappings to LogConstants.RESOURCE_DIFFICULTY
		 * 3. Uncomment the appropriate log class below
		 * 4. Create corresponding Board classes (e.g., MahoganyBoard, OakBoard, etc.)
		 */

		/*
		public class PetrifiedLog : BaseLog
		{
			[Constructable]
			public PetrifiedLog() : this(1)
			{
			}
			[Constructable]
			public PetrifiedLog(int amount) : base(CraftResource.PetrifiedTree, amount)
			{
			}
			public PetrifiedLog(Serial serial) : base(serial)
			{
			}
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0); // version
			}
			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
			public override BaseWoodBoard GetLog()
			{
				return new PetrifiedBoard();
			}
		}
		*/

		/*
		public class MahoganyLog : BaseLog
		{
			[Constructable]
			public MahoganyLog() : this(1)
			{
			}
			[Constructable]
			public MahoganyLog(int amount) : base(CraftResource.MahoganyTree, amount)
			{
			}
			public MahoganyLog(Serial serial) : base(serial)
			{
			}
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0); // version
			}
			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
			public override BaseWoodBoard GetLog()
			{
				return new MahoganyBoard();
			}
		}
		*/

		/*
		public class OakLog : BaseLog
		{
			[Constructable]
			public OakLog() : this(1)
			{
			}
			[Constructable]
			public OakLog(int amount) : base(CraftResource.OakTree, amount)
			{
			}
			public OakLog(Serial serial) : base(serial)
			{
			}
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0); // version
			}
			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
			public override BaseWoodBoard GetLog()
			{
				return new OakBoard();
			}
		}
		*/

		/*
		public class PineLog : BaseLog
		{
			[Constructable]
			public PineLog() : this(1)
			{
			}
			[Constructable]
			public PineLog(int amount) : base(CraftResource.PineTree, amount)
			{
			}
			public PineLog(Serial serial) : base(serial)
			{
			}
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0); // version
			}
			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
			public override BaseWoodBoard GetLog()
			{
				return new PineBoard();
			}
		}
		*/

		/*
		public class WalnutLog : BaseLog
		{
			[Constructable]
			public WalnutLog() : this(1)
			{
			}
			[Constructable]
			public WalnutLog(int amount) : base(CraftResource.WalnutTree, amount)
			{
			}
			public WalnutLog(Serial serial) : base(serial)
			{
			}
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0); // version
			}
			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
			public override BaseWoodBoard GetLog()
			{
				return new WalnutBoard();
			}
		}
		*/

		/*
		public class DriftwoodLog : BaseLog
		{
			[Constructable]
			public DriftwoodLog() : this(1)
			{
			}
			[Constructable]
			public DriftwoodLog(int amount) : base(CraftResource.DriftwoodTree, amount)
			{
			}
			public DriftwoodLog(Serial serial) : base(serial)
			{
			}
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0); // version
			}
			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
			public override BaseWoodBoard GetLog()
			{
				return new DriftwoodBoard();
			}
		}
		*/

		/*
		public class GhostLog : BaseLog
		{
			[Constructable]
			public GhostLog() : this(1)
			{
			}
			[Constructable]
			public GhostLog(int amount) : base(CraftResource.GhostTree, amount)
			{
			}
			public GhostLog(Serial serial) : base(serial)
			{
			}
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0); // version
			}
			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
			public override BaseWoodBoard GetLog()
			{
				return new GhostBoard();
			}
		}
		*/

		#endregion
	}
}

