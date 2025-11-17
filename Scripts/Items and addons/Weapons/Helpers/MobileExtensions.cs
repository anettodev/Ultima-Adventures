using System;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// Extension methods for Mobile class to centralize type checking and property access.
	/// Eliminates 100+ repeated type checks and casts throughout BaseWeapon.
	/// </summary>
	public static class MobileExtensions
	{
		#region Type Checking
		
		/// <summary>
		/// Checks if the mobile is a PlayerMobile instance.
		/// </summary>
		public static bool IsPlayerMobile(this Mobile mobile)
		{
			return mobile is PlayerMobile;
		}
		
		/// <summary>
		/// Checks if the mobile is a BaseCreature instance.
		/// </summary>
		public static bool IsBaseCreature(this Mobile mobile)
		{
			return mobile is BaseCreature;
		}
		
		/// <summary>
		/// Safely casts mobile to PlayerMobile, returns null if not a PlayerMobile.
		/// </summary>
		public static PlayerMobile AsPlayerMobile(this Mobile mobile)
		{
			return mobile as PlayerMobile;
		}
		
		/// <summary>
		/// Safely casts mobile to BaseCreature, returns null if not a BaseCreature.
		/// </summary>
		public static BaseCreature AsBaseCreature(this Mobile mobile)
		{
			return mobile as BaseCreature;
		}
		
		#endregion
		
		#region Property Access
		
		/// <summary>
		/// Checks if the mobile is mounted (works for both PlayerMobile and BaseCreature).
		/// </summary>
		public static bool IsMounted(this Mobile mobile)
		{
			if (mobile is PlayerMobile)
				return ((PlayerMobile)mobile).Mounted;
			if (mobile is BaseCreature)
				return ((BaseCreature)mobile).Mounted;
			return false;
		}
		
		/// <summary>
		/// Checks if the mobile is SoulBound (PlayerMobile only).
		/// </summary>
		public static bool IsSoulBound(this Mobile mobile)
		{
			PlayerMobile pm = mobile as PlayerMobile;
			return pm != null && pm.SoulBound;
		}
		
		/// <summary>
		/// Checks if the mobile is an Avatar (PlayerMobile only).
		/// </summary>
		public static bool IsAvatar(this Mobile mobile)
		{
			PlayerMobile pm = mobile as PlayerMobile;
			return pm != null && pm.Avatar;
		}
		
		/// <summary>
		/// Gets the agility value for the mobile (works for both PlayerMobile and BaseCreature).
		/// </summary>
		public static double GetAgility(this Mobile mobile)
		{
			if (mobile is PlayerMobile)
				return ((PlayerMobile)mobile).Agility();
			if (mobile is BaseCreature)
				return ((BaseCreature)mobile).Agility();
			return 0.0;
		}
		
		/// <summary>
		/// Checks if the mobile is in Midland region.
		/// </summary>
		public static bool IsInMidland(this Mobile mobile)
		{
			return AdventuresFunctions.IsInMidland(mobile);
		}
		
		/// <summary>
		/// Gets the mount for the mobile (works for both PlayerMobile and BaseCreature).
		/// </summary>
		public static IMount GetMount(this Mobile mobile)
		{
			if (mobile is BaseCreature)
				return ((BaseCreature)mobile).Mount;
			if (mobile is PlayerMobile)
				return ((PlayerMobile)mobile).Mount;
			return null;
		}
		
		/// <summary>
		/// Gets the effective attacker (returns master if attacker is a BaseCreature with PlayerMobile master).
		/// </summary>
		public static Mobile GetEffectiveAttacker(this Mobile attacker)
		{
			BaseCreature bc = attacker as BaseCreature;
			if (bc != null)
			{
				Mobile master = bc.GetMaster();
				if (master is PlayerMobile)
					return master;
			}
			return attacker;
		}
		
		#endregion
	}
}

