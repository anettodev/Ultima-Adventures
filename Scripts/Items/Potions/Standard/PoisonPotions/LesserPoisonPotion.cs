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
	/// Lesser poison potion - the weakest poison potion type.
	/// Requires 0-40 poisoning skill.
	/// </summary>
	public class LesserPoisonPotion : BasePoisonPotion
	{
		#region Properties

		/// <summary>
		/// Gets the poison type for this potion
		/// </summary>
		public override Poison Poison{ get{ return Poison.Lesser; } }

		/// <summary>
		/// Gets the minimum poisoning skill required
		/// </summary>
		public override double MinPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MIN_LESSER; } }

		/// <summary>
		/// Gets the maximum poisoning skill required
		/// </summary>
		public override double MaxPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MAX_LESSER; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of LesserPoisonPotion
		/// </summary>
	[Constructable]
	public LesserPoisonPotion() : base( PotionEffect.PoisonLesser )
	{
		Name = "Poção de Alquimia";
		ItemID = PoisonPotionConstants.ITEM_ID_LESSER_POISON;
	}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public LesserPoisonPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the LesserPoisonPotion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( PoisonPotionConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the LesserPoisonPotion
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
