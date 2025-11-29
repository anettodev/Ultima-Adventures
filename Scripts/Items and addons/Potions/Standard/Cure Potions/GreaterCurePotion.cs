using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Greater Cure Potion - Strongest cure potion.
	/// Can cure all poison levels with high effectiveness.
	/// </summary>
	public class GreaterCurePotion : BaseCurePotion
	{
		#region Cure Level Data

		/// <summary>Legacy cure chances for pre-AOS systems</summary>
		private static CureLevelInfo[] m_OldLevelInfo = new CureLevelInfo[]
			{
				new CureLevelInfo( Poison.Lesser,  1.00 ), // 100% chance to cure lesser poison
				new CureLevelInfo( Poison.Regular, 1.00 ), // 100% chance to cure regular poison
				new CureLevelInfo( Poison.Greater, 0.65 ), //  65% chance to cure greater poison
				new CureLevelInfo( Poison.Deadly,  0.45 ), //  45% chance to cure deadly poison
				new CureLevelInfo( Poison.Lethal,  0.15 )  //  15% chance to cure lethal poison
			};

		/// <summary>AOS cure chances (modern system)</summary>
		private static CureLevelInfo[] m_AosLevelInfo = new CureLevelInfo[]
			{
				new CureLevelInfo( Poison.Lesser,  1.00 ), // 100% chance to cure lesser poison
				new CureLevelInfo( Poison.Regular, 1.00 ), // 100% chance to cure regular poison
				new CureLevelInfo( Poison.Greater, 1.00 ), // 100% chance to cure greater poison
				new CureLevelInfo( Poison.Deadly,  0.95 ), //  95% chance to cure deadly poison
				new CureLevelInfo( Poison.Lethal,  0.75 )  //  75% chance to cure lethal poison
			};

		/// <summary>Gets the cure level information based on current system (AOS or Legacy)</summary>
		public override CureLevelInfo[] LevelInfo{ get{ return Core.AOS ? m_AosLevelInfo : m_OldLevelInfo; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new Greater Cure Potion
		/// </summary>
		[Constructable]
		public GreaterCurePotion() : base( PotionEffect.CureGreater )
		{
			ItemID = 0x24EA;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public GreaterCurePotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the greater cure potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the greater cure potion
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