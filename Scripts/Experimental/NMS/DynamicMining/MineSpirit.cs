/*
 * 
 * By Gargouille
 * Date: 07/06/2014
 * 
 * 
 */

using System;
using Server.Items;
using Server.Engines.Harvest;
using Server.DeepMine;

namespace Server.Mobiles
{
	/// <summary>
	/// MineSpirit mobile that marks a location on the map with a point and range.
	/// This is the easiest way for GameMasters to place custom mining spots without creating regions.
	/// Implemented as a mobile rather than an item to have access to Mobile.GetDistanceToSqrt in DynamicMining.cs.
	/// </summary>
	public class MineSpirit : StaticElemental
	{
		#region Constants

		/// <summary>Base type for ore validation (change for custom ore base class)</summary>
		private static Type m_BaseType = typeof(BaseOre);

		#endregion

		#region Fields

		private HarvestSystem m_HarvestSystem;
		private int m_Range = MineSpiritConstants.DEFAULT_RANGE;
		private Type m_OreType = m_BaseType;
		private double m_ReqSkill = MineSpiritConstants.DEFAULT_REQ_SKILL;
		private double m_MinSkill = MineSpiritConstants.DEFAULT_MIN_SKILL;
		private double m_MaxSkill = MineSpiritConstants.DEFAULT_MAX_SKILL;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new MineSpirit instance
		/// </summary>
		[Constructable]
		public MineSpirit()
		{
			// Make invisible and non-interactive for players
			Hidden = true;
			Blessed = true;
			Frozen = true;
			CantWalk = true;
			Hue = 1150;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public MineSpirit(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the custom harvest system for this MineSpirit location
		/// </summary>
		public HarvestSystem HarvestSystem
		{
			get
			{
				if (m_HarvestSystem == null)
				{
					HarvestResource[] res = new HarvestResource[]
					{
						new HarvestResource(
							MineSpiritConstants.FALLBACK_REQ_SKILL,
							MineSpiritConstants.FALLBACK_MIN_SKILL,
							MineSpiritConstants.FALLBACK_MAX_SKILL,
							MineSpiritStringConstants.MSG_FOUND_IRON_ORE_FALLBACK,
							typeof(IronOre),
							typeof(Granite))
					};

					DynamicMining system = new DynamicMining();
					
					system.Ore.Resources = new HarvestResource[]
					{
						new HarvestResource(m_ReqSkill, m_MinSkill, m_MaxSkill, GetHarvestMessage(), m_OreType)
					};
					
					system.Ore.Veins = new HarvestVein[]
					{
						new HarvestVein(MineSpiritConstants.CUSTOM_VEIN_CHANCE, MineSpiritConstants.CUSTOM_VEIN_RARITY, system.Ore.Resources[0], res[0]),
						new HarvestVein(MineSpiritConstants.FALLBACK_VEIN_CHANCE, MineSpiritConstants.FALLBACK_VEIN_RARITY, res[0], null)
					};
					
					m_HarvestSystem = system;
				}
				return m_HarvestSystem;
			}
		}

		/// <summary>
		/// Gets or sets the detection range for this MineSpirit (0-3 tiles)
		/// </summary>
		[CommandProperty(AccessLevel.Administrator)]
		public int Range
		{
			get { return m_Range; }
			set
			{
				m_Range = ClampRange(value);
			}
		}
		
		/// <summary>
		/// Gets or sets the ore type that can be mined at this location
		/// </summary>
		[CommandProperty(AccessLevel.Administrator)]
		public Type OreType
		{
			get { return m_OreType; }
			set
			{
				Type t = value;
				if (m_BaseType.IsAssignableFrom(t))
				{
					m_OreType = t;
					InvalidateHarvestSystem();
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the required skill to mine at this location (0-120)
		/// </summary>
		[CommandProperty(AccessLevel.Administrator)]
		public double ReqSkill
		{
			get { return m_ReqSkill; }
			set
			{
				m_ReqSkill = ClampSkill(value);
				InvalidateHarvestSystem();
			}
		}
		
		/// <summary>
		/// Gets or sets the minimum skill for mining at this location (0-120)
		/// </summary>
		[CommandProperty(AccessLevel.Administrator)]
		public double MinSkill
		{
			get { return m_MinSkill; }
			set
			{
				m_MinSkill = ClampSkill(value);
				InvalidateHarvestSystem();
			}
		}
		
		/// <summary>
		/// Gets or sets the maximum skill for mining at this location (0-120)
		/// </summary>
		[CommandProperty(AccessLevel.Administrator)]
		public double MaxSkill
		{
			get { return m_MaxSkill; }
			set
			{
				m_MaxSkill = ClampSkill(value);
				InvalidateHarvestSystem();
			}
		}

		#endregion

		#region Overrides - Visibility

		/// <summary>
		/// Overrides Hidden property to always be true for players
		/// Staff can still see it via CanSee logic (AccessLevel check)
		/// </summary>
		[CommandProperty(AccessLevel.Administrator)]
		public override bool Hidden
		{
			get
			{
				// Always return true - this mobile should be invisible to players
				// Staff can see it because CanSee checks AccessLevel
				return true;
			}
			set
			{
				// Always keep it hidden - prevent any attempts to unhide
				// Only set base if trying to set to true (redundant but safe)
				if (value == true)
				{
					base.Hidden = true;
				}
				// If trying to set to false, ignore it - stay hidden
			}
		}

		/// <summary>
		/// Prevents MineSpirit from being revealed by actions
		/// </summary>
		public override void RevealingAction()
		{
			// Do nothing - MineSpirit should never be revealed to players
		}

		/// <summary>
		/// Ensures MineSpirit stays hidden after spawning
		/// </summary>
		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			
			// Ensure mobile remains hidden and non-interactive after spawning
			Hidden = true;
			Blessed = true;
			Frozen = true;
			CantWalk = true;
		}

		#endregion

		#region Overrides - Invulnerability

		/// <summary>
		/// Prevents MineSpirit from being damaged
		/// </summary>
		/// <returns>Always returns false to prevent all damage</returns>
		public override bool CanBeDamaged()
		{
			return false;
		}

		/// <summary>
		/// Prevents MineSpirit from being killed
		/// </summary>
		/// <returns>Always returns false to prevent death</returns>
		public override bool OnBeforeDeath()
		{
			return false;
		}

		/// <summary>
		/// Prevents any damage processing that might reveal the mobile
		/// </summary>
		/// <param name="amount">Damage amount (ignored)</param>
		/// <param name="from">Damage source (ignored)</param>
		/// <param name="willKill">Whether damage would kill (ignored)</param>
		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			// Do nothing - prevent any damage processing
			// This ensures Hidden stays true and mobile remains invisible
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the harvest message for the configured ore type
		/// </summary>
		/// <returns>Formatted harvest message in PT-BR</returns>
		private string GetHarvestMessage()
		{
			string oreName = m_OreType.Name.Substring(0, m_OreType.Name.Length - MineSpiritConstants.ORE_NAME_SUFFIX_LENGTH);
			return string.Format(MineSpiritStringConstants.MSG_FOUND_ORE_FORMAT, oreName);
		}

		/// <summary>
		/// Clamps the range value to valid bounds
		/// </summary>
		/// <param name="value">The range value to clamp</param>
		/// <returns>Clamped range value</returns>
		private int ClampRange(int value)
		{
			return Math.Max(MineSpiritConstants.RANGE_MIN, Math.Min(MineSpiritConstants.RANGE_MAX, value));
		}

		/// <summary>
		/// Clamps the skill value to valid bounds
		/// </summary>
		/// <param name="value">The skill value to clamp</param>
		/// <returns>Clamped skill value</returns>
		private double ClampSkill(double value)
		{
			return Math.Max(MineSpiritConstants.SKILL_MIN, Math.Min(MineSpiritConstants.SKILL_MAX, value));
		}

		/// <summary>
		/// Invalidates the cached harvest system, forcing it to be recreated on next access
		/// </summary>
		private void InvalidateHarvestSystem()
		{
			m_HarvestSystem = null;
		}

		#endregion

		#region Serialization
		/// <summary>
		/// Serializes the MineSpirit instance
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0); // version

			writer.Write((int)m_Range);
			writer.Write((string)m_OreType.FullName);
			writer.Write((double)m_MinSkill);
			writer.Write((double)m_MaxSkill);
			writer.Write((double)m_ReqSkill);
		}

		/// <summary>
		/// Deserializes the MineSpirit instance
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_Range = reader.ReadInt();
			
			Type t = ScriptCompiler.FindTypeByFullName(reader.ReadString());
			if (t != null && m_BaseType.IsAssignableFrom(t))
				m_OreType = t;
			
			m_MinSkill = reader.ReadDouble();
			m_MaxSkill = reader.ReadDouble();
			m_ReqSkill = reader.ReadDouble();

			// Ensure mobile remains hidden and non-interactive after deserialization
			Hidden = true;
			Blessed = true;
			Frozen = true;
			CantWalk = true;
		}

		#endregion
	}
}
