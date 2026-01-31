using System;

namespace Server.Items
{
	/// <summary>
	/// Empty bottle item that can be stacked and used as a commodity.
	/// Commonly obtained from drinking potions or purchased from vendors.
	/// Can be used in alchemy crafting.
	/// </summary>
	public class Bottle : Item, ICommodity
	{
		#region ICommodity Implementation

		/// <summary>Gets the description number for commodity system</summary>
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }

		/// <summary>Gets whether this commodity can be converted to a deed (ML feature)</summary>
		bool ICommodity.IsDeedable { get { return (Core.ML); } }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a bottle with default amount (1).
		/// </summary>
		[Constructable]
		public Bottle() : this( BottleConstants.DEFAULT_AMOUNT )
		{
		}

		/// <summary>
		/// Creates a bottle with specified amount.
		/// </summary>
		/// <param name="amount">Number of bottles in the stack</param>
		[Constructable]
		public Bottle( int amount ) : base( BottleConstants.ITEM_ID_BOTTLE )
		{
			Name = "garrafa(s) vazia(s)"; // Empty bottle
			Stackable = true;
			Weight = BottleConstants.WEIGHT_BOTTLE;
			Amount = amount;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		/// <param name="serial">Serial reader</param>
		public Bottle( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the bottle to the world save.
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( BottleConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the bottle from the world save.
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
