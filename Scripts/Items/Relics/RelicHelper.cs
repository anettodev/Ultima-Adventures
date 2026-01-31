namespace Server.Items
{
    /// <summary>
    /// Helper class for DDRelic items.
    /// Provides shared functionality for relic naming and selection.
    /// </summary>
    public static class RelicHelper
    {
        /// <summary>
        /// Gets a random quality descriptor string
        /// </summary>
        /// <returns>Random quality descriptor</returns>
        public static string GetRandomQualityDescriptor()
        {
            int index = Utility.RandomMinMax(RelicConstants.RANDOM_QUALITY_MIN, RelicConstants.RANDOM_QUALITY_MAX);
            return RelicStringConstants.QUALITY_DESCRIPTORS[index];
        }

        /// <summary>
        /// Gets a random decorative term string
        /// </summary>
        /// <param name="includeCeremonial">Whether to include ceremonial option (expands range)</param>
        /// <returns>Random decorative term</returns>
        public static string GetRandomDecorativeTerm(bool includeCeremonial = false)
        {
            int max = includeCeremonial 
                ? RelicConstants.RANDOM_DECORATIVE_MAX_WITH_CEREMONIAL 
                : RelicConstants.RANDOM_DECORATIVE_MAX;
            
            int index = Utility.RandomMinMax(RelicConstants.RANDOM_DECORATIVE_MIN, max);
            
            // Handle ceremonial option (index 3-5 map to special terms)
            if (includeCeremonial && index >= 3)
            {
                switch (index)
                {
                    case 3:
                        return ", cerimonial";
                    case 4:
                        return ", ornamental";
                    case 5:
                        return "";  // Empty string
                    default:
                        return RelicStringConstants.DECORATIVE_TERMS[index % RelicStringConstants.DECORATIVE_TERMS.Length];
                }
            }
            
            return RelicStringConstants.DECORATIVE_TERMS[index];
        }
    }
}

