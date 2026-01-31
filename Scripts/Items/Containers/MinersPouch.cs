using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Miner's Pouch - Specialized container for mining resources.
	/// Provides 50% weight reduction for ores, granites, and ingots.
	/// </summary>
	[Flipable( 0x1C10, 0x1CC6 )]
	public class MinersPouch : MagicRuckSack
	{
		#region Constants

		/// <summary>Mining-themed color hues (earth, stone, metals)</summary>
		private static readonly int[] MINING_HUES = new int[] 
		{ 
			0x3bf,  // Dark brown/earth
			1788,   // Gray/stone
			2193,   // Yellow/gold
			1912,   // Blue (for rare metals)
			1956    // Purple (for rare metals)
		};

		#endregion

		#region Abstract Property Implementations

		/// <summary>
		/// Array of hue values specific to mining (earth, stone, metal colors).
		/// </summary>
		protected override int[] SkillHues { get { return MINING_HUES; } }

		/// <summary>
		/// Display name for miner's pouch (PT-BR).
		/// </summary>
		protected override string ItemName { get { return "Bolsa Mágica de Minérios"; } }

		/// <summary>
		/// Tooltip description explaining the weight reduction benefit (PT-BR).
		/// </summary>
		protected override string TooltipDescription { get { return "Esta bolsa reduz o peso dos minérios, granitos e lingotes pela metade."; } }

		/// <summary>
		/// Error message when invalid item type is attempted (PT-BR).
		/// </summary>
		protected override string RejectionMessage { get { return "Esta bolsa serve apenas para guardar minérios, granitos e lingotes."; } }

		/// <summary>
		/// Maximum number of different item types (12).
		/// </summary>
		protected override int MaxItemsOverride { get { return DEFAULT_MAX_ITEMS; } }

		#endregion

		#region Constructors

		[Constructable]
		public MinersPouch() : base()
		{
		}

		public MinersPouch( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Abstract Method Implementations

		/// <summary>
		/// Validates if an item can be stored in the miner's pouch.
		/// Accepts: all ore types (BaseOre), all granite types (BaseGranite), and all ingot types (BaseIngot).
		/// </summary>
		/// <param name="item">The item to validate</param>
		/// <returns>True if the item is valid for miner's pouch</returns>
		protected override bool IsValidItem( Item item )
		{
			return item is BaseOre || 
				   item is BaseGranite || 
				   item is BaseIngot;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the miner's pouch for saving to the world state.
		/// </summary>
		/// <param name="writer">The generic writer to write to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the miner's pouch from the world state.
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
