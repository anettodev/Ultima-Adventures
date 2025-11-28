using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Lumberjack Pouch - Specialized container for lumberjacking resources.
	/// Provides 50% weight reduction for logs and boards.
	/// </summary>
	[Flipable( 0x1C10, 0x1CC6 )]
	public class LumberjackPouch : MagicRuckSack
	{
		#region Constants

		/// <summary>Lumberjack-themed color hues (nature, wood, forest)</summary>
		private static readonly int[] LUMBERJACK_HUES = new int[] 
		{ 
			1151,   // Green/forest
			0x3bf,  // Brown (wood)
			1788,   // Gray (bark)
			2114,   // Amber (sap)
			2193    // Gold (autumn leaves)
		};

		#endregion

		#region Abstract Property Implementations

		/// <summary>
		/// Array of hue values specific to lumberjacking (nature, wood, forest colors).
		/// </summary>
		protected override int[] SkillHues { get { return LUMBERJACK_HUES; } }

		/// <summary>
		/// Display name for lumberjack pouch (PT-BR).
		/// </summary>
		protected override string ItemName { get { return "Bolsa mágica de madeiras"; } }

		/// <summary>
		/// Tooltip description explaining the weight reduction benefit (PT-BR).
		/// </summary>
		protected override string TooltipDescription { get { return "Esta bolsa reduz o peso das toras e tábuas de madeira pela metade."; } }

		/// <summary>
		/// Error message when invalid item type is attempted (PT-BR).
		/// </summary>
		protected override string RejectionMessage { get { return "Esta bolsa serve apenas para guardar toras e tábuas de madeira."; } }

		/// <summary>
		/// Maximum number of different item types (standardized to 12).
		/// </summary>
		protected override int MaxItemsOverride { get { return DEFAULT_MAX_ITEMS; } }

		#endregion

		#region Constructors

		[Constructable]
		public LumberjackPouch() : base()
		{
		}

		public LumberjackPouch( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Abstract Method Implementations

		/// <summary>
		/// Validates if an item can be stored in the lumberjack pouch.
		/// Accepts: all log types (BaseLog) and all board types (BaseWoodBoard).
		/// </summary>
		/// <param name="item">The item to validate</param>
		/// <returns>True if the item is valid for lumberjack pouch</returns>
		protected override bool IsValidItem( Item item )
		{
			return item is BaseLog || 
				   item is BaseWoodBoard;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the lumberjack pouch for saving to the world state.
		/// </summary>
		/// <param name="writer">The generic writer to write to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the lumberjack pouch from the world state.
		/// </summary>
		/// <param name="reader">The generic reader to read from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion
	}
}
