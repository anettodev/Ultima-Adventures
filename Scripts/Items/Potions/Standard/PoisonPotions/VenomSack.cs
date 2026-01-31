using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Venom sack item that can be used to extract poison potions.
	/// Requires poisoning skill and an empty bottle.
	/// Using a venom sack has a chance to poison the user on failure.
	/// </summary>
	public class VenomSack : Item
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of VenomSack
		/// </summary>
		[Constructable]
		public VenomSack() : base( PoisonPotionConstants.ITEM_ID_VENOM_SACK )
		{
			Name = PoisonPotionItemStringConstants.NAME_VENOM_SACK;
			Weight = PoisonPotionConstants.WEIGHT_VENOM_SACK;
			Amount = 1;
			Stackable = true;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public VenomSack( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click interaction with the venom sack
		/// </summary>
		/// <param name="from">The mobile using the venom sack</param>
		public override void OnDoubleClick( Mobile from )
		{
			VenomSackTypeHelper.VenomSackConfiguration config = VenomSackTypeHelper.GetConfiguration( this.Name );
			
			if ( config == null )
			{
				// Default to lethal if name not recognized
				config = VenomSackTypeHelper.GetConfiguration( PoisonPotionItemStringConstants.NAME_LETHAL_VENOM );
			}

			if ( from.CheckSkill( SkillName.Poisoning, config.SkillRequirement, PoisonPotionConstants.SKILL_MAX_CHECK ) )
			{
				HandleSuccessfulExtraction( from, config );
			}
			else
			{
				HandleFailedExtraction( from, config );
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Handles successful venom extraction
		/// </summary>
		/// <param name="from">The mobile extracting venom</param>
		/// <param name="config">The venom sack configuration</param>
		private void HandleSuccessfulExtraction( Mobile from, VenomSackTypeHelper.VenomSackConfiguration config )
		{
			if ( !from.Backpack.ConsumeTotal( typeof( Bottle ), 1 ) )
			{
				from.SendMessage( PoisonPotionItemStringConstants.MSG_NEED_BOTTLE );
				return;
			}

			from.PlaySound( PoisonPotionConstants.SOUND_ID_VENOM_SUCCESS );

			BasePoisonPotion potion = VenomSackTypeHelper.CreatePotion( this.Name );
			if ( potion != null )
			{
				from.AddToBackpack( potion );
			}

			string potionName = config.PotionName;

			// Second skill check determines if sack is consumed
			if ( from.CheckSkill( SkillName.Poisoning, config.SkillRequirement, PoisonPotionConstants.SKILL_MAX_CHECK ) )
			{
				from.SendMessage( string.Format( PoisonPotionItemStringConstants.MSG_GET_POTION_PARTIAL, potionName ) );
			}
			else
			{
				from.SendMessage( string.Format( PoisonPotionItemStringConstants.MSG_GET_POTION_COMPLETE, potionName ) );
				this.Consume();
			}
		}

		/// <summary>
		/// Handles failed venom extraction
		/// </summary>
		/// <param name="from">The mobile attempting extraction</param>
		/// <param name="config">The venom sack configuration</param>
		private void HandleFailedExtraction( Mobile from, VenomSackTypeHelper.VenomSackConfiguration config )
		{
			from.PlaySound( PoisonPotionConstants.SOUND_ID_VENOM_FAILURE );

			// Chance to poison the user
			if ( Utility.RandomMinMax( PoisonPotionConstants.POISON_CHANCE_MIN, PoisonPotionConstants.POISON_CHANCE_MAX ) > PoisonPotionConstants.POISON_CHANCE_THRESHOLD )
			{
				from.Say( PoisonPotionItemStringConstants.MSG_POISONED );
				from.ApplyPoison( from, config.PoisonType );
				from.SendMessage( PoisonPotionItemStringConstants.MSG_POISONED );
			}
			else
			{
				from.SendMessage( PoisonPotionItemStringConstants.MSG_FAILED_EXTRACTION );
			}

			this.Consume();
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds name properties to the property list
		/// </summary>
		/// <param name="list">The property list</param>
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, PoisonPotionItemStringConstants.PROP_USE_DESCRIPTION );
			list.Add( 1049644, PoisonPotionItemStringConstants.PROP_NEED_BOTTLE ); // PARENTHESIS
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the VenomSack
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( PoisonPotionConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the VenomSack
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Stackable = true;
			Hue = PoisonPotionConstants.HUE_DEFAULT;
		}

		#endregion
	}
}
