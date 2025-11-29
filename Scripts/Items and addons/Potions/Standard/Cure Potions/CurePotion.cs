using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Cure Potion - Standard cure potion.
	/// Can cure Lesser through Lethal poison with decreasing effectiveness.
	/// </summary>
	public class CurePotion : BaseCurePotion
	{
		#region Cure Level Data

		/// <summary>Legacy cure chances for pre-AOS systems</summary>
		private static CureLevelInfo[] m_OldLevelInfo = new CureLevelInfo[]
			{
				new CureLevelInfo( Poison.Lesser,  1.00 ), // 100% chance to cure lesser poison
				new CureLevelInfo( Poison.Regular, 0.65 ), //  65% chance to cure regular poison
				new CureLevelInfo( Poison.Greater, 0.45 ), //  45% chance to cure greater poison
				new CureLevelInfo( Poison.Deadly,  0.15 )  //  15% chance to cure deadly poison
			};

		/// <summary>AOS cure chances (modern system)</summary>
		private static CureLevelInfo[] m_AosLevelInfo = new CureLevelInfo[]
			{
				new CureLevelInfo( Poison.Lesser,  1.00 ), // 100% chance to cure lesser poison
				new CureLevelInfo( Poison.Regular, 0.95 ), //  95% chance to cure regular poison
				new CureLevelInfo( Poison.Greater, 0.75 ), //  75% chance to cure greater poison
				new CureLevelInfo( Poison.Deadly,  0.50 ), //  50% chance to cure deadly poison
				new CureLevelInfo( Poison.Lethal,  0.25 )  //  25% chance to cure lethal poison
			};

		/// <summary>Gets the cure level information based on current system (AOS or Legacy)</summary>
		public override CureLevelInfo[] LevelInfo{ get{ return Core.AOS ? m_AosLevelInfo : m_OldLevelInfo; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new Cure Potion
		/// </summary>
		[Constructable]
		public CurePotion() : base( PotionEffect.Cure )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public CurePotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the cure potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the cure potion
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