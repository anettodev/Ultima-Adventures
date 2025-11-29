using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Regular frostbite potion - 50% chance to paralyze enemies and creates ice patches
	/// 3 second paralyze effect (50% chance, no direct damage)
	/// Ice patches deal 10-20 cold damage over time (benefits from Alchemy skill)
	/// </summary>
	public class FrostbitePotion : BaseFrostbitePotion
	{
	#region Properties

	/// <summary>Minimum damage for regular frostbite potion</summary>
	public override int MinDamage{ get{ return FrostbiteConstants.REGULAR_MIN_DAMAGE; } }

	/// <summary>Maximum damage for regular frostbite potion</summary>
	public override int MaxDamage{ get{ return FrostbiteConstants.REGULAR_MAX_DAMAGE; } }

	/// <summary>Paralyze duration for regular frostbite potion (3 seconds)</summary>
	public override double ParalyzeDuration{ get{ return 3.0; } }

	/// <summary>Ice patch duration for regular frostbite potion (10 seconds)</summary>
	public override double IcePatchDuration{ get{ return 10.0; } }

	#endregion

		#region Constructors

		[Constructable]
		public FrostbitePotion() : base( PotionEffect.Frostbite )
		{
			Name = FrostbiteStringConstants.NAME_FROSTBITE;
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		public FrostbitePotion( Serial serial ) : base( serial )
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
			list.Add( 1070722, string.Format( "<BASEFONT COLOR=#{0:X6}>[ {1} ]<BASEFONT COLOR=#FFFFFF>", 0x8be4fc, FrostbiteStringConstants.TYPE_FROSTBITE ) );
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
