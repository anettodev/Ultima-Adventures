using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Sea shell item with random visual variations.
	/// Can be found while fishing in deep waters.
	/// </summary>
	public class SeaShell : Item
	{
		#region Constants

		private const int DEFAULT_WEIGHT = SeaArtifactConstants.SEA_SHELL_DEFAULT_WEIGHT;
		private const int RANDOM_MIN = SeaArtifactConstants.SEA_SHELL_RANDOM_MIN;
		private const int RANDOM_MAX = SeaArtifactConstants.SEA_SHELL_RANDOM_MAX;

		#endregion

		#region Fields

		/// <summary>
		/// Array of shell variants. Each entry contains [ItemID, NameType] where:
		/// NameType: 0 = singular ("concha do mar"), 1 = plural ("conchas do mar")
		/// </summary>
		private static readonly int[,] ShellVariants = new int[,]
		{
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_BASE, 0 }, // sea shell (singular)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_1, 1 },    // sea shells (plural)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_2, 1 },    // sea shells (plural)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_3, 0 },    // sea shell (singular)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_4, 0 },    // sea shell (singular)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_5, 1 },    // sea shells (plural)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_6, 1 },     // sea shells (plural)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_7, 0 },   // sea shell (singular)
			{ SeaArtifactConstants.SEA_SHELL_ITEM_ID_8, 0 }     // sea shell (singular)
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new sea shell with random visual variation
		/// </summary>
		[Constructable]
		public SeaShell() : base(SeaArtifactConstants.SEA_SHELL_ITEM_ID_BASE)
		{
			Weight = DEFAULT_WEIGHT;

			int variant = Utility.RandomMinMax(RANDOM_MIN, RANDOM_MAX);
			ItemID = ShellVariants[variant, 0];
			Name = ShellVariants[variant, 1] == 0
				? SeaArtifactStringConstants.NAME_SEA_SHELL_SINGULAR
				: SeaArtifactStringConstants.NAME_SEA_SHELL_PLURAL;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public SeaShell(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the sea shell
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(SeaArtifactConstants.SERIALIZATION_VERSION);
		}

		/// <summary>
		/// Deserializes the sea shell
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}