using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class SturdyShovel : BaseHarvestTool
    {
		public override int LabelNumber{ get{ return 1045125; } } // sturdy shovel
		//public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

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

		[Constructable]
		public SturdyShovel() : this(150)
		{
		}

		[Constructable]
        public SturdyShovel(int uses) : base(uses, 0xF39)
        {
            Name = "Pá Resistente";
            Weight = 5.0;
			Hue = 0x973;
        }

		public SturdyShovel( Serial serial ) : base( serial )
		{
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add(1053099, ItemNameHue.UnifiedItemProps.SetColor(HarvestToolStringConstants.MSG_AUTOMATION_HINT_MINING, HarvestToolStringConstants.COLOR_ORANGE)); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
