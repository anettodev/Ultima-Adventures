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
	/// Greater poison potion - stronger poison potion type.
	/// Requires 40-80 poisoning skill.
	/// </summary>
	public class GreaterPoisonPotion : BasePoisonPotion
	{
		#region Properties

		/// <summary>
		/// Gets the poison type for this potion
		/// </summary>
		public override Poison Poison{ get{ return Poison.Greater; } }

		/// <summary>
		/// Gets the minimum poisoning skill required
		/// </summary>
		public override double MinPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MIN_GREATER; } }

		/// <summary>
		/// Gets the maximum poisoning skill required
		/// </summary>
		public override double MaxPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MAX_GREATER; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of GreaterPoisonPotion
		/// </summary>
		[Constructable]
		public GreaterPoisonPotion() : base( PotionEffect.PoisonGreater )
		{
			Name = PoisonPotionItemStringConstants.NAME_GREATER_POISON;
			ItemID = PoisonPotionConstants.ITEM_ID_GREATER_POISON;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public GreaterPoisonPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the GreaterPoisonPotion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( PoisonPotionConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the GreaterPoisonPotion
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
