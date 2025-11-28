using System;
using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for log conversion calculations and mechanics.
	/// Extracted from Log.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class LogConstants
	{
		#region Item IDs

		/// <summary>Base log item ID</summary>
		public const int ITEM_ID_LOG = 0x1BE0;

		/// <summary>Sawmill sound effect ID</summary>
		public const int SOUND_ID_SAWMILL = 0x21C;

		#endregion

		#region Message Colors

		/// <summary>Error message color (red)</summary>
		public const int MSG_COLOR_ERROR = 55;

		/// <summary>System/Cyan message color</summary>
		public const int MSG_COLOR_CYAN = 95;

		#endregion

		#region Sound Constants

		/// <summary>Woohoo sound for female characters</summary>
		public const int SOUND_WOOHOO_FEMALE = 783;

		/// <summary>Woohoo sound for male characters</summary>
		public const int SOUND_WOOHOO_MALE = 1054;

		#endregion

		#region Weight Constants

		/// <summary>Base log weight</summary>
		public const double WEIGHT_BASE = 2.0;

		/// <summary>Standard log weight (Log, AshLog, ElvenLog)</summary>
		public const double WEIGHT_STANDARD = 3.0;

		/// <summary>Medium log weight (EbonyLog, CherryLog, HickoryLog)</summary>
		public const double WEIGHT_MEDIUM = 4.0;

		/// <summary>Heavy log weight (GoldenOakLog, RosewoodLog)</summary>
		public const double WEIGHT_HEAVY = 5.0;

		#endregion

		#region Skill Difficulty Constants

		/// <summary>Default difficulty for regular wood</summary>
		public const double DIFFICULTY_DEFAULT = 40.0;

		/// <summary>Difficulty for AshTree logs</summary>
		public const double DIFFICULTY_ASH = 60.0;

		/// <summary>Difficulty for EbonyTree logs</summary>
		public const double DIFFICULTY_EBONY = 70.0;

		/// <summary>Difficulty for ElvenTree logs</summary>
		public const double DIFFICULTY_ELVEN = 80.0;

		/// <summary>Difficulty for GoldenOakTree logs</summary>
		public const double DIFFICULTY_GOLDEN_OAK = 85.0;

		/// <summary>Difficulty for CherryTree logs</summary>
		public const double DIFFICULTY_CHERRY = 90.0;

		/// <summary>Difficulty for RosewoodTree logs</summary>
		public const double DIFFICULTY_ROSEWOOD = 95.0;

		/// <summary>Difficulty for HickoryTree logs</summary>
		public const double DIFFICULTY_HICKORY = 100.0;

		#endregion

		#region Skill Range Constants

		/// <summary>Skill range offset for min/max skill calculation</summary>
		public const double SKILL_RANGE_OFFSET = 10.0;

		/// <summary>Skill threshold for error message display</summary>
		public const double SKILL_THRESHOLD = 50.0;

		#endregion

		#region Target Range Constants

		/// <summary>Range for targeting sawmill</summary>
		public const int TARGET_RANGE = 2;

		#endregion

		#region Conversion Constants

		/// <summary>Base multiplier for log-to-board conversion (below skill threshold)</summary>
		public const double CONVERSION_MULTIPLIER_BASE = 1.0;

		/// <summary>Minimum skill required for bonus multiplier</summary>
		public const double CONVERSION_SKILL_THRESHOLD = 80.0;

		/// <summary>Multiplier at 80.0 skill</summary>
		public const double CONVERSION_MULTIPLIER_80 = 1.1;

		/// <summary>Multiplier at 90.0 skill</summary>
		public const double CONVERSION_MULTIPLIER_90 = 1.2;

		/// <summary>Multiplier at 100.0 skill</summary>
		public const double CONVERSION_MULTIPLIER_100 = 1.3;

		/// <summary>Multiplier at 110.0 skill</summary>
		public const double CONVERSION_MULTIPLIER_110 = 1.4;

		/// <summary>Multiplier at 120.0 skill (maximum)</summary>
		public const double CONVERSION_MULTIPLIER_120 = 1.5;

		#endregion

		#region Sawmill Item IDs

		/// <summary>HashSet of valid sawmill ItemIDs</summary>
		public static readonly HashSet<int> SAWMILL_ITEM_IDS = new HashSet<int>
		{
			1928,
			4525,
			7130,
			4530,
			7127
		};

		#endregion

		#region Resource Difficulty Dictionary

		/// <summary>
		/// Dictionary mapping CraftResource to conversion difficulty.
		/// Used for log-to-board conversion skill checks.
		/// </summary>
		public static readonly Dictionary<CraftResource, double> RESOURCE_DIFFICULTY = new Dictionary<CraftResource, double>
		{
			{ CraftResource.RegularWood, DIFFICULTY_DEFAULT },
			{ CraftResource.AshTree, DIFFICULTY_ASH },
			{ CraftResource.EbonyTree, DIFFICULTY_EBONY },
			{ CraftResource.ElvenTree, DIFFICULTY_ELVEN },
			{ CraftResource.GoldenOakTree, DIFFICULTY_GOLDEN_OAK },
			{ CraftResource.CherryTree, DIFFICULTY_CHERRY },
			{ CraftResource.RosewoodTree, DIFFICULTY_ROSEWOOD },
			{ CraftResource.HickoryTree, DIFFICULTY_HICKORY }
		};

		#endregion
	}
}

