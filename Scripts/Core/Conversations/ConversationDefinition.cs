using System;
using System.Collections.Generic;

namespace Server.Misc
{
    /// <summary>
    /// Represents a conversation definition with support for static text, 
    /// conditional text, and dynamic text generation.
    /// </summary>
    public class ConversationDefinition
    {
        /// <summary>
        /// Base text for the conversation (required)
        /// Placeholders: {MY_NAME} will be replaced with speaker's name
        /// Placeholders: {YOUR_NAME} will be replaced with listener's name
        /// </summary>
        public string BaseText { get; set; }
        
        /// <summary>
        /// Optional conditional text providers that check game state
        /// </summary>
        public List<ConditionalTextProvider> ConditionalTexts { get; set; }
        
        /// <summary>
        /// Optional dynamic text generator (for conversations like Jester)
        /// Parameters: (myName, yourName) -> returns full conversation text
        /// </summary>
        public Func<string, string, string> DynamicTextGenerator { get; set; }
        
        public ConversationDefinition()
        {
            ConditionalTexts = new List<ConditionalTextProvider>();
        }
    }
}

