using System;

namespace Server.Engines.Harvest
{
	/// <summary>
	/// Represents a harvest resource bank that manages resource availability and respawn timing.
	/// Each bank tracks current resources, maximum capacity, and when resources will respawn.
	/// </summary>
	public class HarvestBank
	{
		#region Fields

		private int m_Current;
		private int m_Maximum;
		private DateTime m_NextRespawn;
		private HarvestVein m_Vein, m_DefaultVein;
		private HarvestDefinition m_Definition;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the harvest definition associated with this bank
		/// </summary>
		public HarvestDefinition Definition
		{
			get { return m_Definition; }
		}

		/// <summary>
		/// Gets the current amount of resources available in the bank
		/// Automatically checks and updates respawn if needed
		/// </summary>
		public int Current
		{
			get
			{
				CheckRespawn();
				return m_Current;
			}
		}

		/// <summary>
		/// Gets or sets the current harvest vein for this bank
		/// Automatically checks and updates respawn if needed
		/// </summary>
		public HarvestVein Vein
		{
			get
			{
				CheckRespawn();
				return m_Vein;
			}
			set
			{
				m_Vein = value;
			}
		}

		/// <summary>
		/// Gets the default harvest vein for this bank
		/// Automatically checks and updates respawn if needed
		/// </summary>
		public HarvestVein DefaultVein
		{
			get
			{
				CheckRespawn();
				return m_DefaultVein;
			}
		}

		#endregion

		#region Core Methods

		/// <summary>
		/// Checks if resources need to respawn and updates the bank if the respawn time has passed
		/// </summary>
		public void CheckRespawn()
		{
			if ( m_Current == m_Maximum || m_NextRespawn > DateTime.UtcNow )
				return;

			m_Current = m_Maximum;

			if ( m_Definition.RandomizeVeins )
			{
				m_DefaultVein = m_Definition.GetVeinFrom( Utility.RandomDouble() );
			}

			m_Vein = m_DefaultVein;
		}

		/// <summary>
		/// Consumes resources from the bank and calculates respawn time if bank is depleted
		/// </summary>
		/// <param name="amount">The amount of resources to consume</param>
		/// <param name="from">The mobile consuming the resources (used for race/region bonuses)</param>
		public void Consume( int amount, Mobile from )
		{
			CheckRespawn();

			if ( m_Current == m_Maximum )
			{
				double min = m_Definition.MinRespawn.TotalMinutes;
				double max = m_Definition.MaxRespawn.TotalMinutes;
				double rnd = Utility.RandomDouble();

				m_Current = m_Maximum - amount;

				double minutes = min + (rnd * (max - min));
				if ( m_Definition.RaceBonus && from.Race == Race.Elf )	//def.RaceBonus = Core.ML
					minutes = ApplyRaceBonus(minutes);

				if (from != null && Server.Misc.AdventuresFunctions.IsInMidland((object)from))
					minutes = ApplyMidlandMultiplier(minutes);

				m_NextRespawn = DateTime.UtcNow + TimeSpan.FromMinutes( minutes );
			}
			else
			{
				m_Current -= amount;
			}

			if ( m_Current < HarvestConstants.MIN_CURRENT_VALUE )
				m_Current = HarvestConstants.MIN_CURRENT_VALUE;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new harvest bank with the specified definition and default vein
		/// </summary>
		/// <param name="def">The harvest definition</param>
		/// <param name="defaultVein">The default harvest vein</param>
		public HarvestBank( HarvestDefinition def, HarvestVein defaultVein )
		{
			m_Maximum = Utility.RandomMinMax( def.MinTotal, def.MaxTotal );
			m_Current = m_Maximum;
			m_DefaultVein = defaultVein;
			m_Vein = m_DefaultVein;

			m_Definition = def;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Applies elf race bonus to respawn time (25% reduction)
		/// </summary>
		/// <param name="minutes">The base respawn time in minutes</param>
		/// <returns>The adjusted respawn time</returns>
		private double ApplyRaceBonus(double minutes)
		{
			return minutes * HarvestConstants.ELF_RACE_RESPAWN_MULTIPLIER;
		}

		/// <summary>
		/// Applies Midland region multiplier to respawn time
		/// </summary>
		/// <param name="minutes">The base respawn time in minutes</param>
		/// <returns>The adjusted respawn time</returns>
		private double ApplyMidlandMultiplier(double minutes)
		{
			return minutes * HarvestConstants.MIDLAND_RESPAWN_MULTIPLIER;
		}

		#endregion
	}
}