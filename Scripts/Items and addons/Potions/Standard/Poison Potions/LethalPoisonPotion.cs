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
	/// Lethal poison potion - the strongest poison potion type.
	/// Requires 80-120 poisoning skill.
	/// </summary>
	public class LethalPoisonPotion : BasePoisonPotion
	{
		#region Properties

		/// <summary>
		/// Gets the poison type for this potion
		/// </summary>
		public override Poison Poison{ get{ return Poison.Lethal; } }

		/// <summary>
		/// Gets the minimum poisoning skill required
		/// </summary>
		public override double MinPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MIN_LETHAL; } }

		/// <summary>
		/// Gets the maximum poisoning skill required
		/// </summary>
		public override double MaxPoisoningSkill{ get{ return PoisonPotionConstants.SKILL_MAX_LETHAL; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of LethalPoisonPotion
		/// </summary>
		[Constructable]
		public LethalPoisonPotion() : base( PotionEffect.PoisonLethal )
		{
			Name = PoisonPotionItemStringConstants.NAME_LETHAL_POISON;
			ItemID = PoisonPotionConstants.ITEM_ID_LETHAL_POISON;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public LethalPoisonPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the LethalPoisonPotion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( PoisonPotionConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the LethalPoisonPotion
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
