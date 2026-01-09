using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// Centralizes diminishing returns logic for damage calculations.
	/// Applies appropriate damage caps based on attacker type (Regular Player, Avatar, or Creature).
	/// </summary>
	public static class DamageCapCalculator
	{
		#region Constants
		
		/// <summary>
		/// Damage cap for regular players (non-Avatar).
		/// </summary>
		private const int CAP_REGULAR_PLAYER = 64;
		
		/// <summary>
		/// Damage cap for Avatar players.
		/// </summary>
		private const int CAP_AVATAR = 92;
		
		/// <summary>
		/// Damage cap for creatures (NPCs).
		/// </summary>
		private const int CAP_CREATURE = 225;
		
		/// <summary>
		/// Diminishing returns factor used in the calculation.
		/// </summary>
		private const int DIMINISHING_RETURNS_FACTOR = 10;
		
		#endregion
		
		/// <summary>
		/// Applies diminishing returns to damage based on the attacker type.
		/// </summary>
		/// <param name="attacker">The mobile dealing damage</param>
		/// <param name="damage">The calculated damage before caps</param>
		/// <param name="weapon">The weapon being used (optional, for wood weapon cap reduction)</param>
		/// <returns>The damage after applying diminishing returns</returns>
		public static double ApplyDiminishingReturns(Mobile attacker, double damage, BaseWeapon weapon = null)
		{
			int cap;
			
			if (attacker.IsPlayerMobile() && attacker.IsAvatar())
				cap = CAP_AVATAR;
			else if (attacker.IsPlayerMobile())
				cap = CAP_REGULAR_PLAYER;
			else
				cap = CAP_CREATURE;
			
			// Reduce cap by 10% for wood weapons
			if (weapon != null && CraftResources.GetType(weapon.Resource) == CraftResourceType.Wood)
			{
				cap = (int)(cap * 0.9); // 10% reduction
			}
			
			return (double)AdventuresFunctions.DiminishingReturns((int)damage, cap, DIMINISHING_RETURNS_FACTOR);
		}
	}
}

