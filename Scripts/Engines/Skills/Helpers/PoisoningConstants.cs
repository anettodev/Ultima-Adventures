namespace Server.SkillHandlers
{
	/// <summary>
	/// Centralized constants for Poisoning skill system.
	/// Extracted to improve maintainability and enable localization.
	/// </summary>
	public static class PoisoningConstants
	{
		#region Timing Constants
		
		/// <summary>Delay before skill can be reused (seconds)</summary>
		public const double SKILL_REUSE_DELAY_SECONDS = 5.0;
		
		/// <summary>Timer delay for poison application (seconds)</summary>
		public const double POISON_APPLY_DELAY_SECONDS = 2.0;
		
		#endregion
		
		#region Skill Requirements
		
		/// <summary>Required skill for Lesser poison (90% success)</summary>
		public const double SKILL_REQUIRED_LESSER = 70.0;
		
		/// <summary>Required skill for Regular poison (90% success)</summary>
		public const double SKILL_REQUIRED_REGULAR = 80.0;
		
		/// <summary>Required skill for Greater poison (90% success)</summary>
		public const double SKILL_REQUIRED_GREATER = 90.0;
		
		/// <summary>Required skill for Deadly poison (90% success)</summary>
		public const double SKILL_REQUIRED_DEADLY = 110.0;
		
		/// <summary>Required skill for Lethal poison (80% success)</summary>
		public const double SKILL_REQUIRED_LETHAL = 120.0;
		
		#endregion
		
		#region Success Chance Constants
		
		/// <summary>Base success chance cap for most poisons</summary>
		public const double BASE_SUCCESS_CHANCE = 0.90;
		
		/// <summary>Success chance cap for Lethal poison (more dangerous)</summary>
		public const double LETHAL_SUCCESS_CHANCE = 0.80;
		
		#endregion
		
		#region Charge Calculation Constants
		
		/// <summary>Skill points per charge (each 10 points = 1 charge)</summary>
		public const double SKILL_POINTS_PER_CHARGE = 10.0;
		
		/// <summary>Maximum poison charges</summary>
		public const int MAX_POISON_CHARGES = 12;
		
		#endregion
		
		#region Modern Mode Constants
		
		/// <summary>Base charges for modern mode</summary>
		public const int MODERN_MODE_BASE_CHARGES = 18;
		
		/// <summary>Charges reduction per poison level</summary>
		public const int CHARGES_PER_LEVEL = 2;
		
		/// <summary>Minimum skill for modern mode failure poison chance</summary>
		public const double MODERN_FAILURE_SKILL_THRESHOLD = 80.0;
		
		/// <summary>Chance of poisoning self on failure (1 in 20 = 5%)</summary>
		public const int MODERN_FAILURE_POISON_CHANCE = 20;
		
		#endregion
		
		#region Message Colors
		
		/// <summary>Error message color</summary>
		public const int MSG_COLOR_ERROR = 38;
		
		/// <summary>Warning message color</summary>
		public const int MSG_COLOR_WARNING = 33;
		
		/// <summary>Overhead message color</summary>
		public const int MSG_COLOR_OVERHEAD = 1150;
		
		#endregion
		
		#region Sound Effects
		
		/// <summary>Sound effect when starting poison application</summary>
		public const int SOUND_POISON_APPLY = 0x242;
		
		/// <summary>Sound effect when successfully applying poison to weapon (same as Harm spell)</summary>
		public const int SOUND_POISON_SUCCESS = 0x0FC;
		
		/// <summary>Sound effect when failing to apply poison (same as poison damage sound)</summary>
		public const int SOUND_POISON_FAILURE = 0x420;
		
		#endregion
		
		#region Karma
		
		/// <summary>Karma penalty for poisoning</summary>
		public const int KARMA_PENALTY = -20;
		
		#endregion
	}
	
	/// <summary>
	/// Centralized PT-BR user messages for Poisoning skill.
	/// </summary>
	public static class PoisoningMessages
	{
		#region Error Messages
		
		/// <summary>Error: Can only poison metal or ranged weapons</summary>
		public const string ERROR_ONLY_METAL_OR_RANGED = "Você só pode envenenar armas de metal ou armas à distância.";
		
		/// <summary>Error: Cannot poison that item</summary>
		public const string ERROR_CANNOT_POISON_WEAPON = "Você não pode envenenar isso! Você só pode envenenar armas de metal, armas à distância, comida ou bebida.";
		
		/// <summary>Error: Cannot poison that item (generic)</summary>
		public const string ERROR_CANNOT_POISON_GENERIC = "Você não pode envenenar isso! Você só pode envenenar certas armas, comida ou bebida.";
		
		/// <summary>Error: Not enough water in container</summary>
		public const string ERROR_NOT_ENOUGH_WATER = "Não há água suficiente aqui para aplicar a quantidade certa de veneno.";
		
		#endregion
		
		#region Warning Messages
		
		/// <summary>Warning: Weapon already has poison, will be overwritten</summary>
		public const string WARNING_OVERWRITE_POISON = "Esta arma já está envenenada! O veneno existente será substituído pelo novo.";
		
		#endregion
		
		#region Mode Toggle Messages
		
		/// <summary>Message when enabling classic poisoning mode</summary>
		public const string MSG_CLASSIC_MODE_ENABLED = "Modo clássico de envenenamento ativado! Você pode envenenar armas de metal e armas à distância. A chance de sucesso e cargas são baseadas na sua skill.";
		
		/// <summary>Message when disabling classic poisoning mode (modern mode)</summary>
		public const string MSG_MODERN_MODE_ENABLED = "Modo moderno de envenenamento ativado! Você pode envenenar armas com habilidade InfectiousStrike, armas de metal e armas à distância.";
		
		#endregion
	}
}

