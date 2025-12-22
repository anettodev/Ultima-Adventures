using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Alchemy Rucksack - Specialized container for alchemical crafting items.
	/// Provides 50% weight reduction for reagents, bottles, jars, and alchemy tools.
	/// Allows nesting of other AlchemyPouch containers.
	/// </summary>
	[Flipable( 0x1C10, 0x1CC6 )]
	public class AlchemyPouch : MagicRuckSack
	{
		#region Constants

		/// <summary>Alchemy-themed color hues (magical, potion-related, mystical)</summary>
		private static readonly int[] ALCHEMY_HUES = new int[] 
		{ 
			0x89F,  // Purple (original)
			1912,   // Blue/water
			1956,   // Purple/magic
			2086,   // Red/fire
			2114,   // Orange/amber
			2262    // Cyan/blue
		};

		#endregion

		#region Abstract Property Implementations

		/// <summary>
		/// Array of hue values specific to alchemy (magical, potion-related colors).
		/// </summary>
		protected override int[] SkillHues { get { return ALCHEMY_HUES; } }

		/// <summary>
		/// Display name for alchemy rucksack (PT-BR).
		/// </summary>
		protected override string ItemName { get { return "alchemy rucksack"; } }

		/// <summary>
		/// Tooltip description explaining the weight reduction benefit (PT-BR).
		/// </summary>
		protected override string TooltipDescription { get { return "Esta bolsa reduz o peso dos itens alquímicos pela metade."; } }

		/// <summary>
		/// Error message when invalid item type is attempted (PT-BR).
		/// </summary>
		protected override string RejectionMessage { get { return "Esta bolsa serve apenas para guardar itens alquímicos."; } }

		/// <summary>
		/// Maximum number of different item types (standardized to 12).
		/// </summary>
		protected override int MaxItemsOverride { get { return DEFAULT_MAX_ITEMS; } }

		/// <summary>
		/// Alchemy rucksacks allow nesting of other AlchemyPouch containers.
		/// </summary>
		protected override bool CanNestContainers { get { return true; } }

		/// <summary>
		/// Type of container that can be nested (AlchemyPouch).
		/// </summary>
		protected override Type NestableContainerType { get { return typeof( AlchemyPouch ); } }

		#endregion

		#region Constructors

		[Constructable]
		public AlchemyPouch() : base()
		{
		}

		public AlchemyPouch( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Abstract Method Implementations

		/// <summary>
		/// Validates if an item can be stored in the alchemy rucksack.
		/// Accepts: reagents, bottles, jars, alchemy tools, GodBrewing items, and other AlchemyPouch.
		/// </summary>
		/// <param name="item">The item to validate</param>
		/// <returns>True if the item is valid for alchemy rucksack</returns>
		protected override bool IsValidItem( Item item )
		{
			return Server.Misc.MaterialInfo.IsReagent( item ) || 
				   item is GodBrewing || 
				   item is Bottle || 
				   item is Jar || 
				   item is MortarPestle || 
				   item is SurgeonsKnife || 
				   item is GardenTool || 
				   item is AlchemyPouch;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the alchemy pouch for saving to the world state.
		/// </summary>
		/// <param name="writer">The generic writer to write to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the alchemy pouch from the world state.
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
