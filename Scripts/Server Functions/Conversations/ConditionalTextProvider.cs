using System;

namespace Server.Misc
{
    /// <summary>
    /// Provides conditional text based on game state checks
    /// </summary>
    public class ConditionalTextProvider
    {
        /// <summary>
        /// Function that checks if condition is met
        /// </summary>
        public Func<bool> Condition { get; set; }
        
        /// <summary>
        /// Text to append if condition is met
        /// Placeholders: {MY_NAME} and {YOUR_NAME} will be replaced
        /// </summary>
        public string ConditionalText { get; set; }
        
        /// <summary>
        /// Optional function to generate dynamic conditional text
        /// Returns text to append if condition is met
        /// </summary>
        public Func<string> DynamicConditionalText { get; set; }
    }
}

