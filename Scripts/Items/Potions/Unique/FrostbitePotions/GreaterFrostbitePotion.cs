using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Greater frostbite potion - 50% chance to paralyze enemies and creates ice patches with increased effect
	/// 5 second paralyze effect (50% chance, no direct damage)
	/// Ice patches deal 20-30 cold damage over time (benefits from Alchemy skill)
	/// </summary>
	public class GreaterFrostbitePotion : BaseFrostbitePotion
	{
	#region Properties

	/// <summary>Minimum damage for greater frostbite potion</summary>
	public override int MinDamage{ get{ return FrostbiteConstants.GREATER_MIN_DAMAGE; } }

	/// <summary>Maximum damage for greater frostbite potion</summary>
	public override int MaxDamage{ get{ return FrostbiteConstants.GREATER_MAX_DAMAGE; } }

	/// <summary>Paralyze duration for greater frostbite potion (5 seconds)</summary>
	public override double ParalyzeDuration{ get{ return 5.0; } }

	/// <summary>Ice patch duration for greater frostbite potion (15 seconds)</summary>
	public override double IcePatchDuration{ get{ return 15.0; } }

	#endregion

		#region Constructors

	[Constructable]
	public GreaterFrostbitePotion() : base( PotionEffect.FrostbiteGreater )
	{
		Name = "Poção de Alquimia"; // Generic alchemy potion name
		ItemID = 0x2406;
		Hue = Server.Items.PotionKeg.GetPotionColor( this );
	}

		public GreaterFrostbitePotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds cyan bracket property display for potion type
		/// </summary>
		public override void AddNameProperty( ObjectPropertyList list )
		{
			base.AddNameProperty( list );
			list.Add( 1070722, string.Format( "<BASEFONT COLOR=#{0:X6}>[{1}]<BASEFONT COLOR=#FFFFFF>", 0x8be4fc, FrostbiteStringConstants.TYPE_GREATER_FROSTBITE ) );
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
		}

		#endregion
	}
}
