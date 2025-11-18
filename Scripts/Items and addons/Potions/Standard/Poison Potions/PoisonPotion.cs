using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	/// <summary>
	/// Regular poison potion - standard poison potion type.
	/// Requires 20-60 poisoning skill.
	/// </summary>
	public class PoisonPotion : BasePoisonPotion
	{
		#region Properties

		/// <summary>
		/// Gets the poison type for this potion
		/// </summary>
		public override Poison Poison{ get{ return Poison.Regular; } }

		/// <summary>
		/// Gets the minimum poisoning skill required
		/// </summary>
		public override double MinPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MIN_REGULAR; } }

		/// <summary>
		/// Gets the maximum poisoning skill required
		/// </summary>
		public override double MaxPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MAX_REGULAR; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of PoisonPotion
		/// </summary>
		[Constructable]
		public PoisonPotion() : base( PotionEffect.Poison )
		{
			Name = PoisonPotionItemStringConstants.NAME_POISON_POTION;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public PoisonPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the PoisonPotion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( PoisonPotionConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the PoisonPotion
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion
	}
}
