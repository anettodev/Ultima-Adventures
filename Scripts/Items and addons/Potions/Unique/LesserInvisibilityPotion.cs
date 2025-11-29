using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Lesser Invisibility Potion - 30 second duration
	/// Movement reveals the player immediately
	/// 100% reveal chance (easily detected by Reveal/Detect Hidden)
	/// </summary>
	public class LesserInvisibilityPotion : BaseInvisibilityPotion
	{
		/// <summary>Fixed duration: 30 seconds</summary>
		public override int DurationSeconds { get { return 30; } }

		/// <summary>Lesser potion cannot attempt stealth</summary>
		public override bool CanAttemptStealth { get { return false; } }

		/// <summary>Lesser potion has no stealth chance</summary>
		public override double StealthSuccessChance { get { return 0.0; } }

		/// <summary>Type identifier for action locking</summary>
		public override Type PotionType { get { return typeof( LesserInvisibilityPotion ); } }

		/// <summary>Lesser potion: 100% reveal chance (easily detected)</summary>
		public override int RevealChance { get { return 100; } }

		[Constructable]
		public LesserInvisibilityPotion() : base( 0x23BD, PotionEffect.InvisibilityLesser )
		{
			Name = "lesser invisibility potion";
		}

		public LesserInvisibilityPotion( Serial serial ) : base( serial )
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