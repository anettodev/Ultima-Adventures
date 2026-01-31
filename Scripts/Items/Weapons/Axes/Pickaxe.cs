using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute( 0xE86, 0xE85 )]
	public class Pickaxe : BaseAxe, IUsesRemaining
	{
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

		public override WeaponAbility PrimaryAbility { get { return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility { get { return null; } }
		/*public override WeaponAbility ThirdAbility { get { return WeaponAbility.MagicProtection2; } }
		public override WeaponAbility FourthAbility { get { return WeaponAbility.ZapDexStrike; } }
		public override WeaponAbility FifthAbility { get { return WeaponAbility.ShadowStrike; } }*/

		public override int AosStrengthReq { get { return 25; } }
		public override int AosMinDamage { get { return 7; } }
		public override int AosMaxDamage { get { return 15; } }
		public override int AosSpeed { get { return 35; } }
		public override float MlSpeed { get { return 4.50f; } }

		public override int OldStrengthReq { get { return 25; } }
		public override int OldMinDamage { get { return 1; } }
		public override int OldMaxDamage { get { return 15; } }
		public override int OldSpeed { get { return 35; } }

		public override int InitMinHits { get { return 31; } }
		public override int InitMaxHits { get { return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public Pickaxe() : base( 0xE86 )
		{
            Name = "Picareta";
            Weight = 2.0;
            Layer = Layer.OneHanded;
            UsesRemaining = 50;
			ShowUsesRemaining = true;
            //AosElementDamages.Physical = 60;
        }

		public Pickaxe( Serial serial ) : base( serial )
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
			ShowUsesRemaining = true;
		}
	}
}