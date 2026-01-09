using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Tailoring Pouch - Specialized container for tailoring resources.
	/// Provides 50% weight reduction for cloth, leather, hides, yarns, threads, and bolts.
	/// </summary>
	[Flipable( 0x1C10, 0x1CC6 )]
	public class TailoringPouch : MagicRuckSack
	{
		#region Constants

		/// <summary>Tailoring-themed color hues (fabric, thread, leather colors)</summary>
		private static readonly int[] TAILORING_HUES = new int[] 
		{ 
			1912,   // Purple (royal fabric)
			1956,   // Pink (silk)
			2086,   // Blue (cotton)
			2114,   // Amber (leather)
			2193,   // Gold (fine fabric)
			2262    // Orange (wool)
		};

		#endregion

		#region Abstract Property Implementations

		/// <summary>
		/// Array of hue values specific to tailoring (fabric, thread, leather colors).
		/// </summary>
		protected override int[] SkillHues { get { return TAILORING_HUES; } }

		/// <summary>
		/// Display name for tailoring pouch (PT-BR).
		/// </summary>
		protected override string ItemName { get { return "Bolsa Mágica de Custura"; } }

		/// <summary>
		/// Tooltip description explaining the weight reduction benefit (PT-BR).
		/// </summary>
		protected override string TooltipDescription { get { return "Esta bolsa reduz o peso dos recursos de alfaiataria pela metade."; } }

		/// <summary>
		/// Error message when invalid item type is attempted (PT-BR).
		/// </summary>
		protected override string RejectionMessage { get { return "Esta bolsa serve apenas para guardar recursos de alfaiataria."; } }

		/// <summary>
		/// Maximum number of different item types (standardized to 12).
		/// </summary>
		protected override int MaxItemsOverride { get { return DEFAULT_MAX_ITEMS; } }

		#endregion

		#region Constructors

		[Constructable]
		public TailoringPouch() : base()
		{
		}

		public TailoringPouch( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Abstract Method Implementations

		/// <summary>
		/// Validates if an item can be stored in the tailoring pouch.
		/// Accepts: Cloth, BoltOfCloth, Cotton, Flax, Wool, Silk, BaseClothMaterial (yarns/threads),
		/// BaseHides, and BaseLeather.
		/// </summary>
		/// <param name="item">The item to validate</param>
		/// <returns>True if the item is valid for tailoring pouch</returns>
		protected override bool IsValidItem( Item item )
		{
			return item is Cloth ||
				   item is BoltOfCloth ||
				   item is Cotton ||
				   item is Flax ||
				   item is Wool ||
				   item is Silk ||
				   item is BaseClothMaterial ||
				   item is BaseHides ||
				   item is BaseLeather;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the tailoring pouch for saving to the world state.
		/// </summary>
		/// <param name="writer">The generic writer to write to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the tailoring pouch from the world state.
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

