using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class GargoylesPickaxe : BaseAxe, IUsesRemaining
	{
		public override int LabelNumber{ get{ return 1041281; } } // a gargoyle's pickaxe
		//public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override HarvestSystem HarvestSystem
		{ 
			get
			{
				if (IsMidlandOrUndergroundMap())
					return DeepMine.DeepMining.GetSystem(this);
				
				return Mining.System;
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

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.MagicProtection; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.StunningStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.MeleeProtection; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }
		
		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public GargoylesPickaxe() : this( Utility.RandomMinMax( 101, 125 ))
		{
		}

		[Constructable]
		public GargoylesPickaxe( int uses ) : base( 0xE85 + Utility.Random( 2 ))
		{
			Weight = 11.0;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add(1053099, ItemNameHue.UnifiedItemProps.SetColor(HarvestToolStringConstants.MSG_AUTOMATION_HINT_MINING, HarvestToolStringConstants.COLOR_ORANGE)); 
		}

		public GargoylesPickaxe( Serial serial ) : base( serial )
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
			
			if ( Hue == 0x973 )
				Hue = 0x0;
		}
	}
}
