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
	/// Deadly poison potion - very strong poison potion type.
	/// Requires 60-100 poisoning skill.
	/// </summary>
	public class DeadlyPoisonPotion : BasePoisonPotion
	{
		#region Properties

		/// <summary>
		/// Gets the poison type for this potion
		/// </summary>
		public override Poison Poison{ get{ return Poison.Deadly; } }

		/// <summary>
		/// Gets the minimum poisoning skill required
		/// </summary>
		public override double MinPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MIN_DEADLY; } }

		/// <summary>
		/// Gets the maximum poisoning skill required
		/// </summary>
		public override double MaxPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MAX_DEADLY; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of DeadlyPoisonPotion
		/// </summary>
	[Constructable]
	public DeadlyPoisonPotion() : base( PotionEffect.PoisonDeadly )
	{
		Name = "Poção de Alquimia";
		ItemID = PoisonPotionConstants.ITEM_ID_DEADLY_POISON;
	}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public DeadlyPoisonPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the DeadlyPoisonPotion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( PoisonPotionConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the DeadlyPoisonPotion
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
