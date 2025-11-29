using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for Frostbite Potion messages and labels.
	/// Extracted from BaseFrostbitePotion.cs to improve maintainability and enable PT-BR localization.
	/// </summary>
	public static class FrostbiteStringConstants
	{
		#region Error Messages (PT-BR)

		/// <summary>Message when player is paralyzed, frozen, or casting</summary>
		public const string ERROR_CANNOT_USE_YET = "Você não pode fazer isso ainda.";

		/// <summary>Message when region doesn't allow harmful actions</summary>
		public const string ERROR_BAD_IDEA = "Isso não parece uma boa ideia.";

		/// <summary>Message format for cooldown (param: seconds remaining)</summary>
		public const string MSG_COOLDOWN_FORMAT = "Você deve esperar {0} segundos antes de usar outra poção de congelamento.";

		/// <summary>Message for throw cooldown (param: seconds remaining)</summary>
		public const string MSG_THROW_COOLDOWN = "Você deve esperar um pouco antes de jogar outra poção de congelamento. ({0:F1}s)";

		#endregion

		#region Item Names (PT-BR)

		/// <summary>Regular frostbite potion name</summary>
		public const string NAME_FROSTBITE = "poção de congelamento";

		/// <summary>Greater frostbite potion name</summary>
		public const string NAME_GREATER_FROSTBITE = "poção de congelamento maior";

		#endregion

		#region Feedback Messages (PT-BR)

		/// <summary>Message when ice patches are created</summary>
		public const string MSG_ICE_PATCHES_CREATED = "Manchas de gelo aparecem no chão!";

		/// <summary>Message when player takes ice damage</summary>
		public const string MSG_ICE_DAMAGE = "Você sente o frio congelante do gelo!";

		#endregion

		#region Property Display (PT-BR)

		/// <summary>Potion type for regular frostbite</summary>
		public const string TYPE_FROSTBITE = "congelamento";

		/// <summary>Potion type for greater frostbite</summary>
		public const string TYPE_GREATER_FROSTBITE = "congelamento maior";

		#endregion
	}
}

