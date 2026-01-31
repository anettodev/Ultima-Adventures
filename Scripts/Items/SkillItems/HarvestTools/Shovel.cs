using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	/// <summary>
	/// Shovel harvest tool for mining. Supports DeepMining, DynamicMining, and standard Mining systems.
	/// </summary>
	public class Shovel : BaseHarvestTool
    {
		#region Properties

		/// <summary>
		/// Gets the appropriate harvest system based on location and context.
		/// Priority: DeepMining (Midland/Underground) > DynamicMining (MineSpirit locations) > Standard Mining
		/// </summary>
		public override HarvestSystem HarvestSystem
		{ 
			get
			{
				if (IsMidlandOrUndergroundMap())
					return DeepMine.DeepMining.GetSystem(this);
				
				HarvestSystem dynamicSystem = GetDynamicMiningSystem();
				return dynamicSystem != null ? dynamicSystem : Mining.System;
            } 
		}

		#endregion

		#region Constructors

		[Constructable]
		public Shovel() : this(HarvestToolConstants.DEFAULT_USES_REMAINING)
		{
		}

		[Constructable]
		public Shovel( int uses ) : base( uses, HarvestToolConstants.ITEM_ID_SHOVEL )
		{
            Name = HarvestToolStringConstants.ITEM_NAME_SHOVEL;
            Weight = HarvestToolConstants.WEIGHT_SHOVEL;
        }

		public Shovel( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Property Display
		
		/// <summary>
		/// Adds name properties to the tooltip
		/// </summary>
		/// <param name="list">The object property list</param>
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );	
			
			if (!(this is Monocle))
				list.Add(1053099, ItemNameHue.UnifiedItemProps.SetColor(HarvestToolStringConstants.MSG_AUTOMATION_HINT_MINING, HarvestToolStringConstants.COLOR_ORANGE)); 
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Checks if the tool or its owner is in Midland or Underground map
		/// </summary>
		/// <returns>True if in Midland or Underground, false otherwise</returns>
		private bool IsMidlandOrUndergroundMap()
		{
			if (Map == Map.Midland || Map == Map.Underground)
				return true;
			
			if (RootParentEntity is Mobile)
			{
				Mobile owner = (Mobile)RootParentEntity;
				return owner.Map == Map.Midland || owner.Map == Map.Underground;
			}
			
			return false;
		}

		/// <summary>
		/// Gets the dynamic mining system for this tool, if available
		/// </summary>
		/// <returns>DynamicMining system if MineSpirit found, null otherwise</returns>
		private HarvestSystem GetDynamicMiningSystem()
		{
			return (HarvestSystem)DynamicMining.GetSystem(this);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the shovel
		/// </summary>
		/// <param name="writer">The generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( HarvestToolConstants.SERIALIZATION_VERSION_LEGACY );
		}

		/// <summary>
		/// Deserializes the shovel
		/// </summary>
		/// <param name="reader">The generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion
	}
}
