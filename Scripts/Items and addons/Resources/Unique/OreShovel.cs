using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class OreShovel : BaseHarvestTool
    {
		public override int Hue { get{ return 0x96D; } }
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
		public OreShovel() : this(50)
		{
		}

		[Constructable]
		public OreShovel( int uses ) : base( uses, 0xF3A )
		{
			Name = "Pá de Ferro";
			Weight = 2.0;
        }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add(1053099, ItemNameHue.UnifiedItemProps.SetColor(HarvestToolStringConstants.MSG_ORE_SHOVEL_SPECIAL, HarvestToolStringConstants.COLOR_CYAN));
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add(1053099, ItemNameHue.UnifiedItemProps.SetColor(HarvestToolStringConstants.MSG_AUTOMATION_HINT_MINING, HarvestToolStringConstants.COLOR_ORANGE)); 
		}

		public OreShovel( Serial serial ) : base( serial )
		{
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
