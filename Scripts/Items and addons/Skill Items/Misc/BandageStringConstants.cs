using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for bandage-related messages.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from Bandage.cs to improve maintainability.
	/// </summary>
	public static class BandageStringConstants
	{
		#region User Messages

		/// <summary>Message when OneRing prevents bandage use</summary>
		public const string MSG_ONERING_PREVENTS_ACTION = "O anel te convence a não fazer isso, e você o escuta...";

		/// <summary>Message when player is blessed and cannot use bandages</summary>
		public const string MSG_CANNOT_USE_BLESSED = "Você não pode usar bandagens neste estado.";

		/// <summary>Message when henchman is not dead and cannot be resurrected</summary>
		public const string MSG_HENCHMAN_NOT_DEAD = "Eles não estão mortos.";

		/// <summary>Message when target is too hungry to be healed</summary>
		public const string MSG_CANNOT_HEAL_HUNGRY = "Você não pode curar aqueles que estão extremamente famintos.";

		/// <summary>Message format for healing result with concentration penalty</summary>
		public const string MSG_HEALING_RESULT_WITH_PENALTY = "Você curou {0} pontos de vida. Devido ao dano sofrido, a perda de concentração foi de {1}%.";

		/// <summary>Message format for healing result without penalty</summary>
		public const string MSG_HEALING_RESULT_NO_PENALTY = "Você curou {0} pontos de vida.";

		/// <summary>Message when trying to cast spell while bandaging</summary>
		public const string MSG_ALREADY_HEALING = "Você está realizando uma ação de cura!";

		/// <summary>Message when healing is cancelled because healer became frozen or paralyzed</summary>
		public const string MSG_HEALING_CANCELLED_FROZEN = "A cura foi interrompida porque você foi paralizado!";

		/// <summary>Message when healing is cancelled because healer became paralyzed (spell)</summary>
		public const string MSG_HEALING_CANCELLED_PARALYZED = "A cura foi interrompida porque você foi paralizado!";

		/// <summary>Message when fingers slip during bandaging</summary>
		public const string MSG_FINGERS_SLIP = "Seus dedos escorregam!";

		#endregion

		#region Henchman Names

		/// <summary>Name for fighter henchman</summary>
		public const string HENCHMAN_NAME_FIGHTER = "fighter henchman";

		/// <summary>Name for wizard henchman</summary>
		public const string HENCHMAN_NAME_WIZARD = "wizard henchman";

		/// <summary>Name for archer henchman</summary>
		public const string HENCHMAN_NAME_ARCHER = "archer henchman";

		/// <summary>Name for creature/monster henchman</summary>
		public const string HENCHMAN_NAME_CREATURE = "creature henchman";

		#endregion
	}
}
