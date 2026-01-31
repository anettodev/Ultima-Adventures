namespace Server.Misc
{
	/// <summary>
	/// Centralized string constants for food decay system messages and labels.
	/// Extracted from FoodDecay.cs to improve maintainability and enable localization.
	/// </summary>
	public static class FoodDecayStringConstants
	{
		#region Hunger Messages (Portuguese)

		/// <summary>Message when player is extremely hungry (hunger < 5)</summary>
		public const string MSG_HUNGER_EXTREME = "Você está extremamente faminto.";

		/// <summary>Emote when player is extremely hungry (hunger < 5)</summary>
		public const string EMOTE_HUNGER_EXTREME = "Estou extremamente faminto.";

		/// <summary>Message when player is getting very hungry (hunger < 10)</summary>
		public const string MSG_HUNGER_VERY = "Você está ficando muito faminto.";

		/// <summary>Emote when player is getting very hungry (hunger < 10)</summary>
		public const string EMOTE_HUNGER_VERY = "Estou ficando muito faminto.";

		/// <summary>Message when player is starving (hunger = 0)</summary>
		public const string MSG_HUNGER_STARVING = "Você está morrendo de fome!";

		/// <summary>Emote when player is starving (hunger = 0)</summary>
		public const string EMOTE_HUNGER_STARVING = "Estou morrendo de fome!";

		#endregion

		#region Thirst Messages (Portuguese)

		/// <summary>Message when player is extremely thirsty (thirst < 5)</summary>
		public const string MSG_THIRST_EXTREME = "Você está extremamente sedento.";

		/// <summary>Emote when player is extremely thirsty (thirst < 5)</summary>
		public const string EMOTE_THIRST_EXTREME = "Estou extremamente sedento.";

		/// <summary>Message when player is getting thirsty (thirst < 10)</summary>
		public const string MSG_THIRST_GETTING = "Você está ficando sedento.";

		/// <summary>Emote when player is getting thirsty (thirst < 10)</summary>
		public const string EMOTE_THIRST_GETTING = "Estou ficando sedento.";

		/// <summary>Message when player is exhausted from thirst (thirst = 0)</summary>
		public const string MSG_THIRST_EXHAUSTED = "Você está exausto de sede";

		/// <summary>Emote when player is exhausted from thirst (thirst = 0)</summary>
		public const string EMOTE_THIRST_EXHAUSTED = "Estou exausto de sede!";

		#endregion
	}
}

