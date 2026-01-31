using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Abstract base class for skill-specific magical rucksacks.
	/// Provides standardized weight reduction (50%), weight limit protection (800 stones),
	/// and item validation framework for specialized resource storage.
	/// </summary>
	[Flipable( 0x1C10, 0x1CC6 )]
	public abstract class MagicRuckSack : LargeSack
	{
		#region Constants

		/// <summary>Weight reduction percentage (50% = items weigh half)</summary>
		protected const double WEIGHT_REDUCTION = 0.5;

		/// <summary>Maximum weight limit in stones (after reduction)</summary>
		protected const int MAX_WEIGHT_LIMIT = 800;

		/// <summary>Default maximum number of different item types</summary>
		protected const int DEFAULT_MAX_ITEMS = 12;

		/// <summary>Cyan color for tooltip description (#8be4fc)</summary>
		protected const string COLOR_CYAN = "#8be4fc";

		/// <summary>Format ID for custom tooltip text (1053099)</summary>
		protected const int TOOLTIP_FORMAT_ID = 1053099;

		#endregion

		#region Abstract Properties

		/// <summary>
		/// Array of hue values specific to this skill type.
		/// One will be randomly selected when the rucksack is created.
		/// </summary>
		protected abstract int[] SkillHues { get; }

		/// <summary>
		/// Display name for this rucksack type (PT-BR).
		/// </summary>
		protected abstract string ItemName { get; }

		/// <summary>
		/// Tooltip description explaining the weight reduction benefit (PT-BR).
		/// </summary>
		protected abstract string TooltipDescription { get; }

		/// <summary>
		/// Error message when invalid item type is attempted (PT-BR).
		/// </summary>
		protected abstract string RejectionMessage { get; }

		/// <summary>
		/// Maximum number of different item types this rucksack can hold.
		/// Override to customize (default is 12).
		/// </summary>
		protected virtual int MaxItemsOverride { get { return DEFAULT_MAX_ITEMS; } }

		/// <summary>
		/// Whether this rucksack allows nesting of same-type containers.
		/// Default is false (only AlchemyPouch allows nesting).
		/// </summary>
		protected virtual bool CanNestContainers { get { return false; } }

		/// <summary>
		/// Type of container that can be nested (if CanNestContainers is true).
		/// </summary>
		protected virtual Type NestableContainerType { get { return null; } }

		#endregion

		#region Abstract Methods

		/// <summary>
		/// Validates if an item can be stored in this rucksack.
		/// Must be implemented by derived classes to check item types.
		/// </summary>
		/// <param name="item">The item to validate</param>
		/// <returns>True if the item is valid for this rucksack type</returns>
		protected abstract bool IsValidItem( Item item );

		#endregion

		#region Properties

		/// <summary>
		/// Maximum weight limit for this rucksack (after weight reduction).
		/// </summary>
		public override int MaxWeight { get { return MAX_WEIGHT_LIMIT; } }

		#endregion

		#region Constructors

		[Constructable]
		public MagicRuckSack() : base()
		{
			Weight = 1.0;
			MaxItems = MaxItemsOverride;
			Name = ItemName;
			Hue = Utility.RandomList( SkillHues );
		}

		public MagicRuckSack( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds tooltip description explaining the weight reduction benefit.
		/// Description is displayed in cyan color and appears as the last item in the tooltip.
		/// </summary>
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			// Add tooltip description in cyan color as the last item
			list.Add( TOOLTIP_FORMAT_ID, ItemNameHue.UnifiedItemProps.SetColor( TooltipDescription, COLOR_CYAN ) );
		}

		#endregion

		#region Container Behavior

		/// <summary>
		/// Opens the rucksack with weight limit protection.
		/// If weight exceeds limit, automatically removes first item to prevent bag destruction.
		/// </summary>
		public override void Open( Mobile from )
		{
			double totalWeight = TotalItemWeights() * WEIGHT_REDUCTION;
			if ( totalWeight > MAX_WEIGHT_LIMIT )
			{
				// Auto-remove first item to prevent bag destruction
				foreach ( Item item in Items )
				{
					from.AddToBackpack( item );
					break;
				}
				from.SendMessage( 55, "Você percebe que a bolsa está com o peso máximo suportado e remove algum item antes que ela rasgue." );
			}
			else
			{
				DisplayTo( from );
			}
		}

		/// <summary>
		/// Handles drag-and-drop into specific position.
		/// Validates item type and weight before allowing drop.
		/// </summary>
		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
		{
			if ( ValidateAndAddItem( from, dropped ) )
			{
				Open( from );
				return base.OnDragDropInto( from, dropped, p );
			}

			return false;
		}

		/// <summary>
		/// Handles drag-and-drop into container.
		/// Validates item type and weight before allowing drop.
		/// </summary>
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( ValidateAndAddItem( from, dropped ) )
			{
				Open( from );
				return base.OnDragDrop( from, dropped );
			}

			return false;
		}

		#endregion

		#region Validation Logic

		/// <summary>
		/// Validates item type, weight limit, and item count before adding.
		/// </summary>
		/// <param name="from">The mobile attempting to add the item</param>
		/// <param name="dropped">The item being added</param>
		/// <returns>True if item can be added, false otherwise</returns>
		private bool ValidateAndAddItem( Mobile from, Item dropped )
		{
			// Check for container nesting (if allowed)
			if ( dropped is Container )
			{
				if ( CanNestContainers && NestableContainerType != null && dropped.GetType() == NestableContainerType )
				{
					// Allow nesting of same-type container
				}
				else
				{
					from.SendMessage( 55, CanNestContainers ? 
						"Você só pode usar outro " + ItemName + " dentro desta bolsa." : 
						"Você não pode adicionar um recipiente nesta bolsa." );
					return false;
				}
			}

			// Check item type validity
			if ( !IsValidItem( dropped ) )
			{
				from.SendMessage( 55, RejectionMessage );
				return false;
			}

			// Check item count limit
			int totalItems = TotalItems();
			if ( totalItems >= MaxItemsOverride )
			{
				from.SendMessage( 55, "A bolsa já está cheia de itens." );
				return false;
			}

			// Check weight limit (after reduction)
			double totalWeight = TotalItemWeights() * WEIGHT_REDUCTION;
			double itemWeight = ( dropped.Weight * dropped.Amount ) * WEIGHT_REDUCTION;
			int itemPlusBagWeight = (int)( totalWeight + itemWeight );

			if ( itemPlusBagWeight > MAX_WEIGHT_LIMIT )
			{
				from.SendMessage( 55, "Adicionar este item na bolsa irá ultrapassar o peso máximo suportado." );
				return false;
			}

			return true;
		}

		#endregion

		#region Weight Calculation

		/// <summary>
		/// Calculates total weight of all items in the rucksack (before reduction).
		/// </summary>
		/// <returns>Total weight in stones</returns>
		private double TotalItemWeights()
		{
			double weight = 0.0;

			foreach ( Item item in Items )
				weight += ( item.Weight * (double)( item.Amount ) );

			return weight;
		}

		/// <summary>
		/// Counts total number of different item types in the rucksack.
		/// </summary>
		/// <returns>Number of different item types</returns>
		private int TotalItems()
		{
			int total = 0;

			foreach ( Item item in Items )
				total += 1;

			return total;
		}

		/// <summary>
		/// Gets the total weight of the rucksack (after 50% reduction).
		/// </summary>
		public override int GetTotal( TotalType type )
		{
			if ( type != TotalType.Weight )
				return base.GetTotal( type );
			else
			{
				return (int)( TotalItemWeights() * WEIGHT_REDUCTION );
			}
		}

		/// <summary>
		/// Updates the total weight when items are added/removed (applies 50% reduction).
		/// </summary>
		public override void UpdateTotal( Item sender, TotalType type, int delta )
		{
			if ( type != TotalType.Weight )
				base.UpdateTotal( sender, type, delta );
			else
				base.UpdateTotal( sender, type, (int)( delta * WEIGHT_REDUCTION ) );
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Weight = 1.0;
			MaxItems = MaxItemsOverride;
			Name = ItemName;
		}

		#endregion
	}
}

