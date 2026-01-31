using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Instrument relic item that can be flipped between two ItemID states.
	/// Supports harp and lute with quality descriptors.
	/// </summary>
	public class DDRelicInstrument : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x41FD;
		private const int RANDOM_INSTRUMENT_MIN = 0;
		private const int RANDOM_INSTRUMENT_MAX = 1;
		private const int RANDOM_DECORATIVE_MIN = 0;
		private const int RANDOM_DECORATIVE_MAX = 2;

		#endregion

		#region Fields

		/// <summary>First ItemID for flipping</summary>
		public int RelicFlipID1;

		/// <summary>Second ItemID for flipping</summary>
		public int RelicFlipID2;

		/// <summary>
		/// Structure for instrument variant data
		/// </summary>
		private struct InstrumentVariant
		{
			public int ItemID;
			public int FlipID1;
			public int FlipID2;
			public string TypeName;

			public InstrumentVariant(int itemID, int flipID1, int flipID2, string typeName)
			{
				this.ItemID = itemID;
				this.FlipID1 = flipID1;
				this.FlipID2 = flipID2;
				this.TypeName = typeName;
			}
		}

		/// <summary>
		/// Array of instrument variants
		/// </summary>
		private static readonly InstrumentVariant[] InstrumentVariants = new InstrumentVariant[]
		{
			new InstrumentVariant(0x41FD, 0x41FD, 0x41FC, "harpa"),
			new InstrumentVariant(0x420C, 0x420C, 0x420D, "alaúde")
		};

		/// <summary>Decorative terms for instruments</summary>
		private static readonly string[] DECORATIVE_TERMS = new[]
		{
			"decorativa", "cerimonial", "ornamental"
		};

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the first flip ItemID
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID1
		{
			get { return RelicFlipID1; }
			set { RelicFlipID1 = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets or sets the second flip ItemID
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID2
		{
			get { return RelicFlipID2; }
			set { RelicFlipID2 = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instrument relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicInstrument() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_MEDIUM;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int variant = Utility.RandomMinMax(RANDOM_INSTRUMENT_MIN, RANDOM_INSTRUMENT_MAX);
			InstrumentVariant instrument = InstrumentVariants[variant];

			ItemID = instrument.ItemID;
			RelicFlipID1 = instrument.FlipID1;
			RelicFlipID2 = instrument.FlipID2;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			int decorativeIndex = Utility.RandomMinMax(RANDOM_DECORATIVE_MIN, RANDOM_DECORATIVE_MAX);
			string decorative = DECORATIVE_TERMS[decorativeIndex];

			Name = quality + ", " + decorative + " " + instrument.TypeName;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicInstrument(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to flip instrument or show identification message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendMessage(RelicStringConstants.MSG_IDENTIFY_VALUE);
				from.SendMessage(RelicStringConstants.MSG_MUST_BE_IN_PACK);
			}
			else
			{
				if (ItemID == RelicFlipID1)
				{
					ItemID = RelicFlipID2;
				}
				else
				{
					ItemID = RelicFlipID1;
				}
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the instrument relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicFlipID1);
			writer.Write(RelicFlipID2);
		}

		/// <summary>
		/// Deserializes the instrument relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicFlipID1 = reader.ReadInt();
			RelicFlipID2 = reader.ReadInt();
		}

		#endregion
	}
}
