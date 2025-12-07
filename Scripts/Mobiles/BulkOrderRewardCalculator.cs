using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
    /// <summary>
    /// Helper class for calculating bulk order rewards.
    /// Extracted from BaseVendor.cs GiveReward method to improve maintainability and reduce complexity.
    /// </summary>
    public static class BulkOrderRewardCalculator
    {
        /// <summary>
        /// Calculates and returns a blacksmith reward item based on credit amount
        /// </summary>
        /// <param name="credits">The number of credits to calculate reward for</param>
        /// <returns>The reward item</returns>
        public static Item CalculateBlacksmithReward(int credits)
        {
            int basecred = BaseVendorConstants.BASE_CREDIT;

            if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_1))
                return GetRandomBlacksmithTier1Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_2))
                return GetRandomBlacksmithTier2Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_3))
                return GetRandomBlacksmithTier3Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_4))
                return GetRandomBlacksmithTier4Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_5))
                return GetRandomBlacksmithTier5Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_6))
                return GetRandomBlacksmithTier6Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_7))
                return GetRandomBlacksmithTier7Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_8))
                return GetRandomBlacksmithTier8Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_9))
                return GetRandomBlacksmithTier9Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_10))
                return GetRandomBlacksmithTier10Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_11))
                return GetRandomBlacksmithTier11Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_12))
                return GetRandomBlacksmithTier12Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_13))
                return GetRandomBlacksmithTier13Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_14))
                return GetRandomBlacksmithTier14Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_15))
                return GetRandomBlacksmithTier15Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_16))
                return GetRandomBlacksmithTier16Reward();
            else
                return GetRandomBlacksmithHighTierReward();
        }

        /// <summary>
        /// Calculates and returns a tailor reward item based on credit amount
        /// </summary>
        /// <param name="credits">The number of credits to calculate reward for</param>
        /// <returns>The reward item</returns>
        public static Item CalculateTailorReward(int credits)
        {
            int basecred = BaseVendorConstants.BASE_CREDIT;

            if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_1))
                return GetRandomTailorTier1Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_2))
                return GetRandomTailorTier2Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_3))
                return GetRandomTailorTier3Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_4))
                return GetRandomTailorTier4Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_5))
                return GetRandomTailorTier5Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_6))
                return GetRandomTailorTier6Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_7))
                return GetRandomTailorTier7Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_8))
                return GetRandomTailorTier8Reward();
            else if (credits <= (basecred * 182)) // Tier 9 - hardcoded value from original
                return GetRandomTailorTier9Reward();
            else if (credits <= (basecred * 220)) // Tier 10 - hardcoded value from original
                return GetRandomTailorTier10Reward();
            else if (credits <= (basecred * 220)) // Tier 11 - duplicate in original
                return GetRandomTailorTier11Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_12))
                return GetRandomTailorTier12Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_13))
                return GetRandomTailorTier13Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_14))
                return GetRandomTailorTier14Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_15))
                return GetRandomTailorTier15Reward();
            else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_16))
                return GetRandomTailorTier16Reward();
            else
                return GetRandomTailorHighTierReward();
        }

        #region Blacksmith Reward Methods

        private static Item GetRandomBlacksmithTier1Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new SturdyShovel();
                case 1: return new SturdyPickaxe();
                case 2: return new LeatherGlovesOfMining(1);
                default: return new SturdyShovel();
            }
        }

        private static Item GetRandomBlacksmithTier2Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new GargoylesPickaxe();
                case 1: return new ProspectorsTool();
                case 2: return new StuddedGlovesOfMining(3);
                default: return new GargoylesPickaxe();
            }
        }

        private static Item GetRandomBlacksmithTier3Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new ColoredAnvil();
                case 1: return new ProspectorsTool();
                case 2: return new RingmailGlovesOfMining(5);
                default: return new ColoredAnvil();
            }
        }

        private static Item GetRandomBlacksmithTier4Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new RunicHammer(CraftResource.Iron + 1, BaseVendorConstants.RUNIC_CHARGES_BASE - (1 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                case 1: return new ProspectorsTool();
                case 2: return new RunicHammer(CraftResource.Iron + 2, BaseVendorConstants.RUNIC_CHARGES_BASE - (2 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new RunicHammer(CraftResource.Iron + 1, BaseVendorConstants.RUNIC_CHARGES_BASE - (1 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
            }
        }

        private static Item GetRandomBlacksmithTier5Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new RunicHammer(CraftResource.Iron + 2, BaseVendorConstants.RUNIC_CHARGES_BASE - (2 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                case 1: return new ColoredAnvil();
                case 2: return new RunicHammer(CraftResource.Iron + 3, BaseVendorConstants.RUNIC_CHARGES_BASE - (3 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new RunicHammer(CraftResource.Iron + 2, BaseVendorConstants.RUNIC_CHARGES_BASE - (2 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
            }
        }

        private static Item GetRandomBlacksmithTier6Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new RunicHammer(CraftResource.Iron + 2, BaseVendorConstants.RUNIC_CHARGES_BASE - (2 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                case 1: return new ColoredAnvil();
                case 2: return new RunicHammer(CraftResource.Iron + 3, BaseVendorConstants.RUNIC_CHARGES_BASE - (3 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new RunicHammer(CraftResource.Iron + 2, BaseVendorConstants.RUNIC_CHARGES_BASE - (2 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
            }
        }

        private static Item GetRandomBlacksmithTier7Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new RunicHammer(CraftResource.Iron + 3, BaseVendorConstants.RUNIC_CHARGES_BASE - (3 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                case 1: return new ColoredAnvil();
                case 2: return new RunicHammer(CraftResource.Iron + 4, BaseVendorConstants.RUNIC_CHARGES_BASE - (4 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new RunicHammer(CraftResource.Iron + 3, BaseVendorConstants.RUNIC_CHARGES_BASE - (3 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
            }
        }

        private static Item GetRandomBlacksmithTier8Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new RunicHammer(CraftResource.Iron + 4, BaseVendorConstants.RUNIC_CHARGES_BASE - (4 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                case 1: return new DwarvenForge();
                case 2: return new EnhancementDeed();
                default: return new RunicHammer(CraftResource.Iron + 4, BaseVendorConstants.RUNIC_CHARGES_BASE - (4 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
            }
        }

        private static Item GetRandomBlacksmithTier9Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new AncientSmithyHammer(10);
                case 1: return new DwarvenForge();
                case 2: return new EnhancementDeed();
                default: return new AncientSmithyHammer(10);
            }
        }

        private static Item GetRandomBlacksmithTier10Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new AncientSmithyHammer(15);
                case 1: return new DwarvenForge();
                case 2: return new EnhancementDeed();
                default: return new AncientSmithyHammer(15);
            }
        }

        private static Item GetRandomBlacksmithTier11Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new AncientSmithyHammer(15);
                case 1: return new ElvenForgeDeed();
                case 2: return new RunicHammer(CraftResource.Iron + 5, BaseVendorConstants.RUNIC_CHARGES_BASE - (5 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new AncientSmithyHammer(15);
            }
        }

        private static Item GetRandomBlacksmithTier12Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new AncientSmithyHammer(20);
                case 1: return new ElvenForgeDeed();
                case 2: return new RunicHammer(CraftResource.Iron + 6, BaseVendorConstants.RUNIC_CHARGES_BASE - (6 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new AncientSmithyHammer(20);
            }
        }

        private static Item GetRandomBlacksmithTier13Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new AncientSmithyHammer(20);
                case 1: return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1);
                case 2: return new RunicHammer(CraftResource.Iron + 7, BaseVendorConstants.RUNIC_CHARGES_BASE - (7 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new AncientSmithyHammer(20);
            }
        }

        private static Item GetRandomBlacksmithTier14Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new AncientSmithyHammer(25);
                case 1: return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1);
                case 2: return new RunicHammer(CraftResource.Iron + 8, BaseVendorConstants.RUNIC_CHARGES_BASE - (8 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new AncientSmithyHammer(25);
            }
        }

        private static Item GetRandomBlacksmithTier15Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new OneHandedDeed();
                case 1: return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2);
                case 2: return new RunicHammer(CraftResource.Iron + 8, BaseVendorConstants.RUNIC_CHARGES_BASE - (8 * BaseVendorConstants.RUNIC_CHARGE_REDUCTION));
                default: return new OneHandedDeed();
            }
        }

        private static Item GetRandomBlacksmithTier16Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new OneHandedDeed();
                case 1: return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2);
                case 2: return new ItemBlessDeed();
                default: return new OneHandedDeed();
            }
        }

        private static Item GetRandomBlacksmithHighTierReward()
        {
            double odds = Utility.RandomDouble();
            if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_1)
                return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_5);
            else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_2)
                return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_4);
            else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_3)
                return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_3);
            else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_4)
                return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2);
            else
                return new PowerScroll(SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1);
        }

        #endregion

        #region Tailor Reward Methods

        private static Item GetRandomTailorTier1Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new SmallStretchedHideEastDeed();
                case 1: return new SmallStretchedHideSouthDeed();
                case 2: return new TallBannerEast();
                default: return new SmallStretchedHideEastDeed();
            }
        }

        private static Item GetRandomTailorTier2Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new MediumStretchedHideEastDeed();
                case 1: return new MediumStretchedHideSouthDeed();
                case 2: return new TallBannerNorth();
                default: return new MediumStretchedHideEastDeed();
            }
        }

        private static Item GetRandomTailorTier3Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new LightFlowerTapestryEastDeed();
                case 1: return new LightFlowerTapestrySouthDeed();
                case 2: return new SalvageBag();
                default: return new LightFlowerTapestryEastDeed();
            }
        }

        private static Item GetRandomTailorTier4Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new BrownBearRugEastDeed();
                case 1: return new BrownBearRugSouthDeed();
                case 2: return new SalvageBag();
                default: return new BrownBearRugEastDeed();
            }
        }

        private static Item GetRandomTailorTier5Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new PolarBearRugEastDeed();
                case 1: return new PolarBearRugSouthDeed();
                case 2: return new SalvageBag();
                default: return new PolarBearRugEastDeed();
            }
        }

        private static Item GetRandomTailorTier6Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new DarkFlowerTapestryEastDeed();
                case 1: return new DarkFlowerTapestrySouthDeed();
                case 2: return new RunicSewingKit(CraftResource.RegularLeather + 1, BaseVendorConstants.RUNIC_CHARGES_SEWING - (1 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                default: return new DarkFlowerTapestryEastDeed();
            }
        }

        private static Item GetRandomTailorTier7Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new MagicScissors();
                case 1: return new TallBannerEast();
                case 2: return new GuildedTallBannerEast();
                default: return new MagicScissors();
            }
        }

        private static Item GetRandomTailorTier8Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new GuildedTallBannerNorth();
                case 1: return new TallBannerNorth();
                case 2: return new MagicScissors();
                default: return new GuildedTallBannerNorth();
            }
        }

        private static Item GetRandomTailorTier9Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new EnhancementDeed();
                case 1: return new GuildedTallBannerEast();
                case 2: return new EnhancementDeed();
                default: return new EnhancementDeed();
            }
        }

        private static Item GetRandomTailorTier10Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new RunicSewingKit(CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                case 1: return new GuildedTallBannerNorth();
                case 2: return new EnhancementDeed();
                default: return new RunicSewingKit(CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
            }
        }

        private static Item GetRandomTailorTier11Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new EnhancementDeed();
                case 1: return new RunicSewingKit(CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                case 2: return new CathedralWindow1();
                default: return new EnhancementDeed();
            }
        }

        private static Item GetRandomTailorTier12Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new CathedralWindow3();
                case 1: return new RunicSewingKit(CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                case 2: return new CathedralWindow2();
                default: return new CathedralWindow3();
            }
        }

        private static Item GetRandomTailorTier13Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new CathedralWindow4();
                case 1: return new RunicSewingKit(CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                case 2: return new CathedralWindow5();
                default: return new CathedralWindow4();
            }
        }

        private static Item GetRandomTailorTier14Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new LegsOfMusicalPanache();
                case 1: return new RunicSewingKit(CraftResource.RegularLeather + 3, BaseVendorConstants.RUNIC_CHARGES_SEWING - (3 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                case 2: return new SkirtOfPower();
                default: return new LegsOfMusicalPanache();
            }
        }

        private static Item GetRandomTailorTier15Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new ClothingBlessDeed();
                case 1: return new PowerScroll(SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1);
                case 2: return new RunicSewingKit(CraftResource.RegularLeather + 3, BaseVendorConstants.RUNIC_CHARGES_SEWING - (3 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                default: return new ClothingBlessDeed();
            }
        }

        private static Item GetRandomTailorTier16Reward()
        {
            switch (Utility.Random(3))
            {
                case 0: return new ItemBlessDeed();
                case 1: return new PowerScroll(SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1);
                case 2: return new RunicSewingKit(CraftResource.RegularLeather + 3, BaseVendorConstants.RUNIC_CHARGES_SEWING - (3 * BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION));
                default: return new ItemBlessDeed();
            }
        }

        private static Item GetRandomTailorHighTierReward()
        {
            double odds = Utility.RandomDouble();
            if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_1)
                return new PowerScroll(SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_5);
            else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_2)
                return new PowerScroll(SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_4);
            else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_3)
                return new PowerScroll(SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_3);
            else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_4)
                return new PowerScroll(SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2);
            else
                return new PowerScroll(SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1);
        }

        /// <summary>
        /// Calculates and returns a bulk order reward item based on credit amount and type
        /// </summary>
        /// <param name="credits">The number of credits to calculate reward for</param>
        /// <param name="creditType">1 for Blacksmith, 2 for Tailor</param>
        /// <returns>The reward item, or null if no reward is given</returns>
        public static Item CalculateReward(int credits, int creditType)
        {
            if (creditType == 1) // Blacksmith
            {
                return CalculateBlacksmithReward(credits);
            }
            else if (creditType == 2) // Tailor
            {
                return CalculateTailorReward(credits);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
