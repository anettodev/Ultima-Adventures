using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Invisibility Potion (Regular) - 60 second duration
	/// Movement reveals the player immediately
	/// 70% reveal chance (moderate protection against Reveal/Detect Hidden)
	/// </summary>
	public class InvisibilityPotion : BaseInvisibilityPotion
	{
		/// <summary>Fixed duration: 60 seconds</summary>
		public override int DurationSeconds { get { return 60; } }

		/// <summary>Regular potion cannot attempt stealth</summary>
		public override bool CanAttemptStealth { get { return false; } }

		/// <summary>Regular potion has no stealth chance</summary>
		public override double StealthSuccessChance { get { return 0.0; } }

		/// <summary>Type identifier for action locking</summary>
		public override Type PotionType { get { return typeof( InvisibilityPotion ); } }

		/// <summary>Regular potion: 70% reveal chance (moderate protection)</summary>
		public override int RevealChance { get { return 70; } }

		[Constructable]
		public InvisibilityPotion() : base( 0x180F, PotionEffect.Invisibility )
		{
			Name = "invisibility potion";
		}

		public InvisibilityPotion( Serial serial ) : base( serial )
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