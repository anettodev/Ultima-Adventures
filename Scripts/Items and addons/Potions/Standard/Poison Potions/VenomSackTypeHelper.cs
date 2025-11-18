using System;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Helper class for VenomSack type determination and operations.
    /// Extracted from VenomSack.cs to improve code organization and eliminate duplication.
    /// </summary>
    public static class VenomSackTypeHelper
    {
        /// <summary>
        /// Configuration for a venom sack type
        /// </summary>
        public class VenomSackConfiguration
        {
            /// <summary>Skill requirement for this venom sack type</summary>
            public int SkillRequirement { get; set; }

            /// <summary>Type of potion to create</summary>
            public Type PotionType { get; set; }

            /// <summary>Type of poison to apply</summary>
            public Poison PoisonType { get; set; }

            /// <summary>Display name for the potion</summary>
            public string PotionName { get; set; }
        }

        /// <summary>
        /// Gets the configuration for a venom sack based on its name
        /// </summary>
        /// <param name="name">The name of the venom sack</param>
        /// <returns>Configuration for the venom sack type, or null if not recognized</returns>
        public static VenomSackConfiguration GetConfiguration( string name )
        {
            if ( name == PoisonPotionItemStringConstants.NAME_LESSER_VENOM )
            {
                return new VenomSackConfiguration
                {
                    SkillRequirement = PoisonPotionConstants.SKILL_LESSER_VENOM,
                    PotionType = typeof( LesserPoisonPotion ),
                    PoisonType = Poison.Lesser,
                    PotionName = PoisonPotionItemStringConstants.INTERNAL_LESSER_POISON
                };
            }
            else if ( name == PoisonPotionItemStringConstants.NAME_VENOM_SACK )
            {
                return new VenomSackConfiguration
                {
                    SkillRequirement = PoisonPotionConstants.SKILL_REGULAR_VENOM,
                    PotionType = typeof( PoisonPotion ),
                    PoisonType = Poison.Regular,
                    PotionName = PoisonPotionItemStringConstants.INTERNAL_POISON
                };
            }
            else if ( name == PoisonPotionItemStringConstants.NAME_GREATER_VENOM )
            {
                return new VenomSackConfiguration
                {
                    SkillRequirement = PoisonPotionConstants.SKILL_GREATER_VENOM,
                    PotionType = typeof( GreaterPoisonPotion ),
                    PoisonType = Poison.Greater,
                    PotionName = PoisonPotionItemStringConstants.INTERNAL_GREATER_POISON
                };
            }
            else if ( name == PoisonPotionItemStringConstants.NAME_DEADLY_VENOM )
            {
                return new VenomSackConfiguration
                {
                    SkillRequirement = PoisonPotionConstants.SKILL_DEADLY_VENOM,
                    PotionType = typeof( DeadlyPoisonPotion ),
                    PoisonType = Poison.Deadly,
                    PotionName = PoisonPotionItemStringConstants.INTERNAL_DEADLY_POISON
                };
            }
            else if ( name == PoisonPotionItemStringConstants.NAME_LETHAL_VENOM )
            {
                return new VenomSackConfiguration
                {
                    SkillRequirement = PoisonPotionConstants.SKILL_LETHAL_VENOM,
                    PotionType = typeof( LethalPoisonPotion ),
                    PoisonType = Poison.Lethal,
                    PotionName = PoisonPotionItemStringConstants.INTERNAL_LETHAL_POISON
                };
            }

            return null;
        }

        /// <summary>
        /// Creates the appropriate potion from a venom sack type
        /// </summary>
        /// <param name="name">The name of the venom sack</param>
        /// <returns>The created potion, or null if type not recognized</returns>
        public static BasePoisonPotion CreatePotion( string name )
        {
            VenomSackConfiguration config = GetConfiguration( name );
            if ( config == null || config.PotionType == null )
                return null;

            return (BasePoisonPotion)Activator.CreateInstance( config.PotionType );
        }

        /// <summary>
        /// Gets the potion name for display purposes
        /// </summary>
        /// <param name="name">The name of the venom sack</param>
        /// <returns>The potion name, or empty string if not recognized</returns>
        public static string GetPotionName( string name )
        {
            VenomSackConfiguration config = GetConfiguration( name );
            return config != null ? config.PotionName : string.Empty;
        }
    }
}

