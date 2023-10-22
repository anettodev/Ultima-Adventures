using System;
using Server;
namespace Server.Items
{
    public class MinersPickaxe : Pickaxe, ITokunoDyable
	{
		/*public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }*/


        [Constructable]
        public MinersPickaxe()
        {
            Name = "Miner's Pickaxe";
			Hue = 974;
            /*Attributes.WeaponDamage = 25;
            Attributes.AttackChance = 25;
            Attributes.DefendChance = 25;
            WeaponAttributes.HitLowerAttack = 35;
            WeaponAttributes.SelfRepair = 3;
            Attributes.Luck = 100;
            Attributes.ReflectPhysical = 15;
            Attributes.WeaponSpeed = 20;*/
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


        /*public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
        {
            phys = 100;
            cold = 0;
            fire = 0;
            nrgy = 0;
            pois = 0;
            chaos = 0;
            direct = 0;
        }*/
        public MinersPickaxe( Serial serial )
            : base( serial )
        {
        }
        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );
        }
        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }
}
