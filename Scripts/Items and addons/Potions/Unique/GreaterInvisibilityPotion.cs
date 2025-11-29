using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Greater Invisibility Potion - 90 second duration
	/// 50% chance ON DRINK to enable stealth movement
	/// If successful, grants temporary Hiding/Stealth skill bonus
	/// Movement distance depends on player's Stealth skill
	/// 50% reveal chance (better protection against Reveal/Detect Hidden, but still weaker than GM skills)
	/// </summary>
	public class GreaterInvisibilityPotion : BaseInvisibilityPotion
	{
		/// <summary>Fixed duration: 90 seconds</summary>
		public override int DurationSeconds { get { return 90; } }

		/// <summary>Greater potion can attempt stealth</summary>
		public override bool CanAttemptStealth { get { return true; } }

		/// <summary>50% chance to successfully enable stealth on drink</summary>
		public override double StealthSuccessChance { get { return 0.50; } }

		/// <summary>Type identifier for action locking</summary>
		public override Type PotionType { get { return typeof( GreaterInvisibilityPotion ); } }

		/// <summary>Greater potion: 50% reveal chance (better protection, but still weaker than GM Hiding/Stealth)</summary>
		public override int RevealChance { get { return 50; } }

		[Constructable]
		public GreaterInvisibilityPotion() : base( 0x180F, PotionEffect.InvisibilityGreater )
		{
			Name = "greater invisibility potion";
			ItemID = 0x2406;
		}

		public GreaterInvisibilityPotion( Serial serial ) : base( serial )
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