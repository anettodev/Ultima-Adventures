using System;
using Server.Mobiles;
using Server.Guilds;
using Server.Engines.PartySystem;

namespace Server.Spells
{
	/// <summary>
	/// Helper class for field spell mechanics (Fire Field, Poison Field, Energy Field, etc.)
	/// Provides common functionality for field-based area spells
	/// </summary>
	public static class FieldSpellHelper
	{
		#region Constants
		/// <summary>
		/// Standard field range: -2 to +2 tiles
		/// </summary>
		public const int FIELD_RANGE = 2;

		/// <summary>
		/// Base duration for field spells (seconds) - Used as fallback only
		/// Prefer using NMSGetDuration for actual calculations
		/// </summary>
		public const double BASE_DURATION_FALLBACK = 3.0;

		/// <summary>
		/// Multiplier for determining field orientation (UO client specific)
		/// </summary>
		private const int ORIENTATION_MULTIPLIER = 44;
		#endregion

		#region Duration Calculation

		/// <summary>
		/// Standard field spell duration calculation using NMS system
		/// Automatically sends duration message to caster
		/// </summary>
		/// <param name="caster">The spell caster</param>
		/// <returns>TimeSpan for field duration</returns>
		public static TimeSpan GetFieldDuration(Mobile caster)
		{
			// Use NMS duration system for debuffs/harmful fields
			// isBeneficial = false means it uses EvalInt for duration scaling
			return SpellHelper.NMSGetDuration(caster, caster, false);
		}

		#endregion

	#region Field Orientation

	/// <summary>
	/// Determines field orientation (East-West vs North-South) based on caster and target positions
	/// When targeting self, uses caster's facing direction to create perpendicular field
	/// This matches the UO client's field rendering logic
	/// </summary>
	/// <param name="caster">The spell caster (used for direction when self-targeting)</param>
	/// <param name="targetLoc">Target location for field placement</param>
	/// <returns>True for East-West orientation, False for North-South</returns>
	public static bool GetFieldOrientation(Mobile caster, IPoint3D targetLoc)
	{
		int dx = caster.Location.X - targetLoc.X;
		int dy = caster.Location.Y - targetLoc.Y;

		// If targeting self (dx=0, dy=0), use caster's facing direction
		// This fixes orientation when casting on yourself while moving
		if (dx == 0 && dy == 0)
		{
			Direction facing = caster.Direction & Direction.Mask;

			// If facing North/South (vertical), create East-West field (horizontal/perpendicular)
			if (facing == Direction.North || facing == Direction.South)
				return true;  // East-West

			// If facing East/West (horizontal), create North-South field (vertical/perpendicular)
			if (facing == Direction.East || facing == Direction.West)
				return false; // North-South

			// Diagonal directions: use geometric calculation
			// This will fall through to the original logic below
		}

		// Original geometric calculation for non-self targeting
		int rx = (dx - dy) * ORIENTATION_MULTIPLIER;
		int ry = (dx + dy) * ORIENTATION_MULTIPLIER;

		// Determine orientation based on relative position
		if (rx >= 0 && ry >= 0)
			return false; // North-South
		else if (rx >= 0)
			return true;  // East-West
		else if (ry >= 0)
			return true;  // East-West
		else
			return false; // North-South
	}

	#endregion

		#region Friendly Fire Detection

		/// <summary>
		/// Determines if a target should be considered "friendly" for damage/poison reduction
		/// Checks: self, guild/allies, party members, own pets/summons
		/// </summary>
		/// <param name="caster">The spell caster</param>
		/// <param name="target">The potential target</param>
		/// <returns>True if target is friendly, False otherwise</returns>
		public static bool IsFriendlyTarget(Mobile caster, Mobile target)
		{
			// Self
			if (target == caster)
				return true;

			// Guild and allies
			Guild fromGuild = SpellHelper.GetGuildFor(caster);
			Guild toGuild = SpellHelper.GetGuildFor(target);
			
			if (fromGuild != null && toGuild != null && 
				(fromGuild == toGuild || fromGuild.IsAlly(toGuild)))
				return true;

			// Party members
			Party p = Party.Get(caster);
			if (p != null && p.Contains(target))
				return true;

			// Own pets/summons
			if (target is BaseCreature)
			{
				BaseCreature c = (BaseCreature)target;
				if ((c.Controlled || c.Summoned) && 
					(c.ControlMaster == caster || c.SummonMaster == caster))
					return true;
			}

			return false;
		}

		#endregion

		#region Damage Calculation

		/// <summary>
		/// Applies friendly fire damage reduction
		/// Friendlies take 50% reduced damage (randomized between 1 and half damage)
		/// </summary>
		/// <param name="damage">Base damage amount</param>
		/// <param name="caster">The spell caster</param>
		/// <param name="target">The damage target</param>
		/// <returns>Modified damage amount</returns>
		public static int ApplyFriendlyFireReduction(int damage, Mobile caster, Mobile target)
		{
			if (IsFriendlyTarget(caster, target))
			{
				// Friendly fire: 50% damage reduction with randomization
				return Utility.RandomMinMax(1, damage / 2);
			}

			// Non-friendly: slight randomization for variety
			if (target is BaseCreature)
			{
				return damage - Utility.RandomMinMax(0, 1);
			}

			return damage;
		}

		/// <summary>
		/// Ensures minimum damage of 1
		/// </summary>
		/// <param name="damage">Calculated damage</param>
		/// <returns>Damage with minimum floor of 1</returns>
		public static int EnsureMinimumDamage(int damage)
		{
			return damage < 1 ? 1 : damage;
		}

		#endregion
	}
}

