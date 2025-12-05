using Server.Misc;

namespace Server.Mobiles
{
	/// <summary>
	/// Helper class for vendor barter skill calculation logic.
	/// Extracted from BaseVendor.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class VendorBarterHelper
	{
		/// <summary>
		/// Calculates the effective barter skill for a player selling items to a vendor.
		/// Takes into account guild membership and begging status.
		/// </summary>
		/// <param name="vendor">The vendor the player is selling to</param>
		/// <param name="player">The player selling items</param>
		/// <param name="isGuildMember">Output parameter indicating if player is a guild member</param>
		/// <returns>The effective barter skill value to use for price calculations</returns>
		public static int CalculateBarterSkill(BaseVendor vendor, Mobile player, out int isGuildMember)
		{
			isGuildMember = 0;
			int barter = (int)player.Skills[SkillName.ItemID].Value;

			// Check if player is a guild member
			if (player is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)player;
				if (barter < BaseVendorConstants.GUILD_BARTER_CAP && 
					vendor.NpcGuild != NpcGuild.None && 
					vendor.NpcGuild == pm.NpcGuild)
				{
					barter = BaseVendorConstants.GUILD_BARTER_CAP;
					isGuildMember = 1;
				}
			}

			// Check if player is begging (only if not a guild member)
			if (BaseVendor.BeggingPose(player) > 0 && isGuildMember == 0)
			{
				Titles.AwardKarma(player, -BaseVendor.BeggingKarma(player), true);
				barter = (int)player.Skills[SkillName.Begging].Value;
			}

			return barter;
		}

		/// <summary>
		/// Calculates the effective barter skill for a player selling items to a vendor.
		/// Simplified version that doesn't track guild membership status.
		/// </summary>
		/// <param name="vendor">The vendor the player is selling to</param>
		/// <param name="player">The player selling items</param>
		/// <returns>The effective barter skill value to use for price calculations</returns>
		public static int CalculateBarterSkill(BaseVendor vendor, Mobile player)
		{
			int isGuildMember;
			return CalculateBarterSkill(vendor, player, out isGuildMember);
		}
	}
}

