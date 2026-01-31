using System;
using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Items
{
    /// <summary>
    /// Centralized constants for armor calculations and mechanics.
    /// Extracted from BaseArmor.cs to improve maintainability.
    /// All numeric values previously hardcoded are now defined here.
    /// </summary>
    public static class ArmorConstants
    {
        #region Armor Rating Constants

        /// <summary>Base bonus added to armor rating for protection levels</summary>
        public const int PROTECTION_BASE_BONUS = 10;

        /// <summary>Multiplier for protection level when calculating armor rating</summary>
        public const int PROTECTION_LEVEL_MULTIPLIER = 5;

        /// <summary>Penalty applied to poor quality armor rating</summary>
        public const int QUALITY_POOR_PENALTY = -8;

        /// <summary>Bonus applied per quality level for armor rating</summary>
        public const int QUALITY_LEVEL_BONUS = 8;

        #endregion

        #region Durability Constants

        /// <summary>Base scale value for durability calculations (100%)</summary>
        public const int DURABILITY_SCALE_BASE = 100;

        /// <summary>Durability bonus for exceptional quality armor</summary>
        public const int EXCEPTIONAL_DURABILITY_BONUS = 20;

        /// <summary>Durability bonus for Durable durability level</summary>
        public const int DURABILITY_DURABLE_BONUS = 20;

        /// <summary>Durability bonus for Substantial durability level</summary>
        public const int DURABILITY_SUBSTANTIAL_BONUS = 50;

        /// <summary>Durability bonus for Massive durability level</summary>
        public const int DURABILITY_MASSIVE_BONUS = 70;

        /// <summary>Durability bonus for Fortified durability level</summary>
        public const int DURABILITY_FORTIFIED_BONUS = 100;

        /// <summary>Durability bonus for Indestructible durability level</summary>
        public const int DURABILITY_INDESTRUCTIBLE_BONUS = 120;

        /// <summary>Rounding offset for durability scaling calculations</summary>
        public const int DURABILITY_ROUNDING_OFFSET = 99;

        #endregion

        #region Damaged Armor Scaling

        /// <summary>Minimum effectiveness percentage for damaged armor (50%)</summary>
        public const int DAMAGED_ARMOR_MIN_SCALE = 50;

        /// <summary>Maximum effectiveness percentage for damaged armor (50% + current HP ratio)</summary>
        public const int DAMAGED_ARMOR_MAX_SCALE = 50;

        #endregion

        #region Protection Level Resistance Offsets

        /// <summary>Resistance bonus for Guarding protection level</summary>
        public const int PROTECTION_GUARDING_OFFSET = 1;

        /// <summary>Resistance bonus for Hardening protection level</summary>
        public const int PROTECTION_HARDENING_OFFSET = 2;

        /// <summary>Resistance bonus for Fortification protection level</summary>
        public const int PROTECTION_FORTIFICATION_OFFSET = 3;

        /// <summary>Resistance bonus for Invulnerability protection level</summary>
        public const int PROTECTION_INVULNERABILITY_OFFSET = 4;

        #endregion

        #region Stat Requirements

        /// <summary>Base percentage for stat requirement calculations (100%)</summary>
        public const int STAT_REQUIREMENT_BASE = 100;

        /// <summary>Maximum lower stat requirement percentage cap (100%)</summary>
        public const int LOWER_STAT_REQ_CAP = 100;

        #endregion

        #region Scissor/Crafting Constants

        /// <summary>Minimum resource amount required for scissoring</summary>
        public const int SCISSOR_MIN_RESOURCES = 2;

        /// <summary>Divisor for calculating scissored resources from player-crafted items</summary>
        public const int SCISSOR_RESOURCE_DIVISOR = 2;

        /// <summary>Resource amount returned for non-player-crafted items</summary>
        public const int SCISSOR_NON_PLAYER_CRAFTED_AMOUNT = 1;

        #endregion

        #region Crafting Quality Bonuses

        /// <summary>Resist bonus points distributed for exceptional quality (with runic tool)</summary>
        public const int EXCEPTIONAL_RESIST_BONUS_RUNIC = 8;

        /// <summary>Resist bonus points distributed for exceptional quality (without runic tool)</summary>
        public const int EXCEPTIONAL_RESIST_BONUS_STANDARD = 5;

        /// <summary>Divisor for calculating Arms Lore bonus on crafting</summary>
        public const int ARMSLORE_BONUS_DIVISOR = 20;

        #endregion

        #region Balance System Constants

        /// <summary>Cooldown in seconds for champion challenge</summary>
        public const double CHALLENGE_COOLDOWN_SECONDS = 1.5;

        /// <summary>Maximum range in tiles for issuing a challenge</summary>
        public const int CHALLENGE_MAX_RANGE = 7;

        /// <summary>Chance to miss when throwing challenge glove (25%)</summary>
        public const double CHALLENGE_MISS_CHANCE = 0.25;

        /// <summary>Penalty multiplier when champion is defeated (75% remaining)</summary>
        public const double CHAMPION_DEFEAT_PENALTY = 0.75;

        /// <summary>Ratio of balance effect transferred from defeated champion (50%)</summary>
        public const double CHAMPION_GAIN_RATIO = 0.5;

        /// <summary>Balance effect transferred to winner in non-champion fights (10%)</summary>
        public const double BALANCE_TRANSFER_WINNER = 0.1;

        /// <summary>Balance effect remaining for loser in non-champion fights (90%)</summary>
        public const double BALANCE_TRANSFER_LOSER = 0.9;

        #endregion

        #region Combat Constants

        /// <summary>Divisor for armor absorption calculation (armor rating divided by 4)</summary>
        public const double ARMOR_ABSORPTION_DIVISOR = 4.0;

        /// <summary>Minimum absorbed damage value</summary>
        public const int MIN_ABSORBED_DAMAGE = 2;

        /// <summary>Base chance percentage for armor durability loss on hit (25%)</summary>
        public const int DURABILITY_LOSS_CHANCE = 75;

        /// <summary>Reduced durability loss chance for metal armor (50%)</summary>
        public const int METAL_DURABILITY_LOSS_CHANCE = 50;

        /// <summary>Random range divisor for durability loss checks</summary>
        public const int DURABILITY_RANDOM_RANGE = 100;

        /// <summary>Divisor for self-repair chance calculations</summary>
        public const int SELF_REPAIR_CHANCE_DIVISOR = 20;

        /// <summary>Divisor for bashing weapon wear calculation</summary>
        public const int BASHING_WEAR_DIVISOR = 2;

        /// <summary>Maximum random wear range for non-bashing weapons</summary>
        public const int RANDOM_WEAR_RANGE = 2;

        #endregion

        #region Wear System Constants

        /// <summary>Maximum wear value (100%)</summary>
        public const int WEAR_MAX_VALUE = 100;

        /// <summary>Multiplier for wear bonus calculation based on max durability</summary>
        public const double WEAR_BONUS_MULTIPLIER = 0.50;

        /// <summary>Maximum durability reference value for wear calculations</summary>
        public const int WEAR_MAX_DURABILITY = 255;

        /// <summary>Divisor for random wear chance calculation</summary>
        public const int WEAR_RANDOM_DIVISOR = 2;

        #endregion

        #region Armor Scalars by Body Position

        /// <summary>Armor effectiveness scalar for Gorget (neck) position</summary>
        public const double SCALAR_GORGET = 0.07;

        /// <summary>Armor effectiveness scalar for Shield position</summary>
        public const double SCALAR_SHIELD = 0.07;

        /// <summary>Armor effectiveness scalar for Gloves position</summary>
        public const double SCALAR_GLOVES = 0.14;

        /// <summary>Armor effectiveness scalar for Helmet position</summary>
        public const double SCALAR_HELMET = 0.15;

        /// <summary>Armor effectiveness scalar for Arms position</summary>
        public const double SCALAR_ARMS = 0.22;

        /// <summary>Armor effectiveness scalar for Legs/Chest position</summary>
        public const double SCALAR_LEGS_CHEST = 0.35;

        #endregion

        #region CraftResource Armor Rating Bonuses

        /// <summary>
        /// Dictionary mapping CraftResource types to their armor rating bonuses.
        /// Replaces the massive switch statement in ArmorRating property (lines 132-183).
        /// </summary>
        public static readonly Dictionary<CraftResource, int> ResourceArmorBonuses =
            new Dictionary<CraftResource, int>
        {
            // Metal Resources
            { CraftResource.DullCopper, 6 },
            { CraftResource.ShadowIron, 12 },
            { CraftResource.Copper, 18 },
            { CraftResource.Bronze, 24 },
            { CraftResource.Platinum, 28 },
            { CraftResource.Gold, 30 },
            { CraftResource.Agapite, 36 },
            { CraftResource.Verite, 42 },
            { CraftResource.Valorite, 48 },
            { CraftResource.Titanium, 50 },
            { CraftResource.Rosenium, 50 },
            { CraftResource.Nepturite, 48 },
            { CraftResource.Obsidian, 48 },
            { CraftResource.Steel, 53 },
            { CraftResource.Brass, 60 },
            { CraftResource.Mithril, 66 },
            { CraftResource.Xormite, 66 },
            { CraftResource.Dwarven, 108 },

            // Leather Resources
            { CraftResource.SpinedLeather, 8 },
            { CraftResource.HornedLeather, 12 },
            { CraftResource.BarbedLeather, 16 },
            { CraftResource.NecroticLeather, 18 },
            { CraftResource.VolcanicLeather, 20 },
            { CraftResource.FrozenLeather, 24 },
            { CraftResource.GoliathLeather, 28 },
            { CraftResource.DraconicLeather, 30 },
            { CraftResource.HellishLeather, 32 },
            { CraftResource.DinosaurLeather, 36 },
            { CraftResource.AlienLeather, 36 },

            // Wood Resources
            { CraftResource.AshTree, 6 },
            { CraftResource.CherryTree, 12 },
            { CraftResource.EbonyTree, 18 },
            { CraftResource.GoldenOakTree, 24 },
            { CraftResource.HickoryTree, 30 },
            { CraftResource.RosewoodTree, 42 },
            { CraftResource.ElvenTree, 90 },

            // Scale Resources
            { CraftResource.YellowScales, 32 },
            { CraftResource.RedScales, 36 },
            { CraftResource.GreenScales, 38 },
            { CraftResource.BlackScales, 42 },
            { CraftResource.WhiteScales, 46 },
            { CraftResource.BlueScales, 48 }
        };

        /// <summary>
        /// Gets the armor rating bonus for a given craft resource.
        /// Returns 0 if the resource is not found in the dictionary.
        /// </summary>
        /// <param name="resource">The craft resource to look up</param>
        /// <returns>Armor rating bonus for the resource</returns>
        public static int GetResourceArmorBonus(CraftResource resource)
        {
            int bonus;
            return ResourceArmorBonuses.TryGetValue(resource, out bonus) ? bonus : 0;
        }

        #endregion

        #region Durability Level Bonuses

        /// <summary>
        /// Gets the durability bonus percentage for a given durability level.
        /// Used in GetDurabilityBonus() method.
        /// </summary>
        /// <param name="level">The armor durability level</param>
        /// <returns>Durability bonus percentage</returns>
        public static int GetDurabilityLevelBonus(ArmorDurabilityLevel level)
        {
            switch (level)
            {
                case ArmorDurabilityLevel.Durable: return DURABILITY_DURABLE_BONUS;
                case ArmorDurabilityLevel.Substantial: return DURABILITY_SUBSTANTIAL_BONUS;
                case ArmorDurabilityLevel.Massive: return DURABILITY_MASSIVE_BONUS;
                case ArmorDurabilityLevel.Fortified: return DURABILITY_FORTIFIED_BONUS;
                case ArmorDurabilityLevel.Indestructible: return DURABILITY_INDESTRUCTIBLE_BONUS;
                default: return 0;
            }
        }

        #endregion

        #region Armor Scalar Array

        /// <summary>
        /// Gets the armor scalar array for different body positions.
        /// Order: Gorget, Shield, Gloves, Helmet, Arms, Legs/Chest
        /// </summary>
        public static double[] GetArmorScalars()
        {
            return new double[]
            {
                SCALAR_GORGET,
                SCALAR_SHIELD,
                SCALAR_GLOVES,
                SCALAR_HELMET,
                SCALAR_ARMS,
                SCALAR_LEGS_CHEST
            };
        }

        #endregion
    }
}