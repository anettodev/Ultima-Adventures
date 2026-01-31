using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Entry in a TamingBODBook representing a single contract.
	/// Stores contract progress and tier/creature type information.
	/// </summary>
	public class TamingBODEntry : IComparable
	{
		#region Fields

		private int reward;
		private int m_amount;
		private int m_tamed;
		private int m_tier;
		private Type m_creatureType;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the reward amount for this contract.
		/// </summary>
		public int Reward
		{
			get { return reward; }
			set { reward = value; }
		}

		/// <summary>
		/// Gets or sets the amount of creatures required to complete this contract.
		/// </summary>
		public int AmountToTame
		{
			get { return m_amount; }
			set { m_amount = value; }
		}

		/// <summary>
		/// Gets or sets the amount of creatures already tamed for this contract.
		/// </summary>
		public int AmountTamed
		{
			get { return m_tamed; }
			set { m_tamed = value; }
		}

		/// <summary>
		/// Gets or sets the contract tier (1-5).
		/// Tier 1 = Generic, Tier 2-5 = Specific creature types.
		/// </summary>
		public int Tier
		{
			get { return m_tier; }
			set { m_tier = value; }
		}

		/// <summary>
		/// Gets or sets the specific creature type for this contract.
		/// Null for tier 1 (generic contracts).
		/// </summary>
		public Type CreatureType
		{
			get { return m_creatureType; }
			set { m_creatureType = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TamingBODEntry class.
		/// </summary>
		/// <param name="ak">Amount already tamed</param>
		/// <param name="atk">Amount to tame</param>
		/// <param name="gpreward">Reward amount</param>
		public TamingBODEntry(int ak, int atk, int gpreward)
		{
			AmountToTame = atk;
			AmountTamed = ak;
			Reward = gpreward;
			m_tier = 1; // Default to tier 1 (generic)
			m_creatureType = null;
		}

		/// <summary>
		/// Initializes a new instance of the TamingBODEntry class with tier information.
		/// </summary>
		/// <param name="ak">Amount already tamed</param>
		/// <param name="atk">Amount to tame</param>
		/// <param name="gpreward">Reward amount</param>
		/// <param name="tier">Contract tier (1-5)</param>
		/// <param name="creatureType">Creature type, or null for generic</param>
		public TamingBODEntry(int ak, int atk, int gpreward, int tier, Type creatureType)
		{
			AmountToTame = atk;
			AmountTamed = ak;
			Reward = gpreward;
			m_tier = tier;
			m_creatureType = creatureType;
		}

		/// <summary>
		/// Initializes a new instance of the TamingBODEntry class from a TamingBOD.
		/// </summary>
		/// <param name="bod">The TamingBOD to create entry from</param>
		public TamingBODEntry(TamingBOD bod)
		{
			if (bod != null)
			{
				AmountToTame = bod.AmountToTame;
				AmountTamed = bod.AmountTamed;
				Reward = bod.Reward;
				m_tier = bod.Tier;
				m_creatureType = bod.CreatureType;
			}
			else
			{
				m_tier = 1;
				m_creatureType = null;
			}
		}

		/// <summary>
		/// IComparable implementation.
		/// </summary>
		public int CompareTo(object obj)
		{
			int ok = 5;
			return ok;
		}

		#endregion
		
		
		public TamingBODEntry( GenericReader reader )
		{ 
			Deserialize( reader );
		} 

		#region Serialization

		/// <summary>
		/// Serializes the TamingBODEntry.
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public void Serialize(GenericWriter writer)
		{
			writer.Write((int)1); // version

			writer.Write(reward);
			writer.Write(m_amount);
			writer.Write(m_tamed);
			writer.Write(m_tier);
			writer.Write(m_creatureType != null ? m_creatureType.FullName : (string)null);
		}

		/// <summary>
		/// Deserializes the TamingBODEntry.
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public void Deserialize(GenericReader reader)
		{
			int version = reader.ReadInt();

			reward = reader.ReadInt();
			m_amount = reader.ReadInt();
			m_tamed = reader.ReadInt();

			if (version >= 1)
			{
				m_tier = reader.ReadInt();
				string creatureTypeName = reader.ReadString();
				if (creatureTypeName != null && creatureTypeName.Length > 0)
				{
					// Use FindTypeByFullName since we serialize FullName
					m_creatureType = ScriptCompiler.FindTypeByFullName(creatureTypeName);
					if (m_creatureType == null)
					{
						// Fallback to FindTypeByName if FullName lookup fails (for backward compatibility)
						m_creatureType = ScriptCompiler.FindTypeByName(creatureTypeName);
					}
				}
				else
				{
					m_creatureType = null;
				}
			}
			else
			{
				// Legacy: Default to tier 1 (generic)
				m_tier = 1;
				m_creatureType = null;
			}

			if (m_tamed > m_amount)
				m_tamed = m_amount;
		}

		#endregion
	}
}
