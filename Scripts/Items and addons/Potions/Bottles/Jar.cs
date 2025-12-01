using System;

namespace Server.Items
{
	/// <summary>
	/// Jar item that can be stacked and used as a commodity.
	/// Similar to bottles but with different appearance and weight.
	/// </summary>
	public class Jar : Item, ICommodity
	{
		#region ICommodity Implementation

		/// <summary>Gets the description number for commodity system</summary>
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }

		/// <summary>Gets whether this commodity can be converted to a deed (ML feature)</summary>
		bool ICommodity.IsDeedable { get { return (Core.ML); } }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a jar with default amount (1).
		/// </summary>
		[Constructable]
		public Jar() : this( JarConstants.DEFAULT_AMOUNT )
		{
		}

		/// <summary>
		/// Creates a jar with specified amount.
		/// </summary>
		/// <param name="amount">Number of jars in the stack</param>
		[Constructable]
		public Jar( int amount ) : base( JarConstants.ITEM_ID_JAR )
		{
			Name = "jarra";
			Stackable = true;
			Weight = JarConstants.WEIGHT_JAR;
			Amount = amount;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		/// <param name="serial">Serial reader</param>
		public Jar( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the jar to the world save.
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( JarConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the jar from the world save.
		/// Fixes ItemID for legacy jars.
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			ItemID = JarConstants.ITEM_ID_JAR;
		}

		#endregion
	}
}
