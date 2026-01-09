namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for weapon-related messages and labels.
	/// Extracted from BaseWeapon.cs to improve maintainability and enable localization.
	/// </summary>
	public static class WeaponStringConstants
	{
		#region User Messages (Portuguese)
		
		/// <summary>Message when race requirement is not met</summary>
		public const string MSG_ONLY_RACE_CAN_USE = "Apenas {0} podem usar isso.";
		
		/// <summary>Message when dexterity requirement is not met</summary>
		public const string MSG_NOT_AGILE_ENOUGH = "Você não é ágil o suficiente para equipar isso.";
		
		/// <summary>Message when intelligence requirement is not met</summary>
		public const string MSG_NOT_INTELLIGENT_ENOUGH = "Você não é inteligente o suficiente para equipar isso.";
		
		/// <summary>Message when defender dodges and counter-attacks (riposte)</summary>
		public const string MSG_DODGE_AND_COUNTER = "Você desvia o golpe e rapidamente contra-ataca!";
		
		/// <summary>Message when attacker performs a sneak attack</summary>
		public const string MSG_SNEAK_ATTACK = "Você executa um ataque furtivo causando {0}% de dano adicional!";
		
		/// <summary>Message when mount is hit instead of rider</summary>
		public const string MSG_MOUNT_HIT = "O golpe erra você, mas atinge a montaria!";
		
		/// <summary>Message when mount is hit on miss</summary>
		public const string MSG_MOUNT_HIT_ON_MISS = "O golpe erra você e é desviado para sua montaria!";
		
		/// <summary>Message when opponent dodges attack</summary>
		public const string MSG_OPPONENT_DODGES = "Seu oponente desvia rapidamente do seu ataque!";
		
		/// <summary>Message when defender dodges attack</summary>
		public const string MSG_YOU_DODGE = "Você se esquiva em um movimento rápido e desvia o ataque!";
		
		/// <summary>Message when attacker adjusts and hits after dodge</summary>
		public const string MSG_ADJUST_AND_HIT = "Você ajusta habilmente o golpe e acerta o oponente!";
		
		/// <summary>Message when opponent adjusts and hits after dodge</summary>
		public const string MSG_OPPONENT_ADJUSTS = "Seu oponente ajusta habilmente e acerta um golpe!";
		
		/// <summary>Message when slayer weapon is effective</summary>
		public const string MSG_SLAYER_EFFECTIVE = "Esta arma parece estar funcionando muito bem contra este inimigo.";
		
		#endregion
		
		#region Property Labels (Portuguese)
		
		/// <summary>Label for exceptional quality</summary>
		public const string LABEL_EXCEPTIONAL = "Excepcional";
		
		/// <summary>Label for wear percentage</summary>
		public const string LABEL_WEAR = "Desgaste: {0}%";
		
		/// <summary>Label for swords skill</summary>
		public const string LABEL_SKILL_SWORDS = "Skill: swords";
		
		/// <summary>Label for macing skill</summary>
		public const string LABEL_SKILL_MACING = "Skill: mace fighting";
		
		/// <summary>Label for fencing skill</summary>
		public const string LABEL_SKILL_FENCING = "Skill: fencing";
		
		/// <summary>Label for archery skill (throwing)</summary>
		public const string LABEL_SKILL_ARCHERY_THROWING = "Skill: throwing";
		
		/// <summary>Label for archery skill (bow)</summary>
		public const string LABEL_SKILL_ARCHERY_BOW = "Skill: archery";
		
		/// <summary>Label for wrestling skill</summary>
		public const string LABEL_SKILL_WRESTLING = "Skill: wrestling";
		
		/// <summary>Label for two-handed weapon</summary>
		public const string LABEL_TWO_HANDED = "Arma de duas mãos";
		
		/// <summary>Label for one-handed weapon</summary>
		public const string LABEL_ONE_HANDED = "Arma de uma mão";
		
		#endregion
		
		#region Color Hex Values
		
		/// <summary>Default cyan color for tooltips</summary>
		public const string COLOR_CYAN = "#8be4fc";
		
		/// <summary>Yellow/gold color for special properties</summary>
		public const string COLOR_YELLOW = "#ffe066";
		
		/// <summary>Green color for poison information</summary>
		public const string COLOR_GREEN = "#90EE90";
		
		/// <summary>Orange color for high damage weapons (min > 20)</summary>
		public const string COLOR_ORANGE = "#FFA500";
		
		/// <summary>Red color for very high damage weapons (min > 30)</summary>
		public const string COLOR_RED = "#FF0000";
		
		/// <summary>Pink color for spell-related properties</summary>
		public const string COLOR_PINK = "#FF69B4";
		
		/// <summary>Blue color for mana-related properties</summary>
		public const string COLOR_BLUE = "#4169E1";
		
		/// <summary>Purple color for artifact rarity</summary>
		public const string COLOR_PURPLE = "#9370DB";
		
		#endregion
		
		#region Stat Mod Suffixes
		
		/// <summary>Suffix for strength stat mod</summary>
		public const string STAT_MOD_STR = "Str";
		
		/// <summary>Suffix for dexterity stat mod</summary>
		public const string STAT_MOD_DEX = "Dex";
		
		/// <summary>Suffix for intelligence stat mod</summary>
		public const string STAT_MOD_INT = "Int";
		
		#endregion
		
		#region Resource Name Filters
		
		/// <summary>Resource name filter: none</summary>
		public const string RESOURCE_NONE = "none";
		
		/// <summary>Resource name filter: normal</summary>
		public const string RESOURCE_NORMAL = "regular";
		
		/// <summary>Resource name filter: iron</summary>
		public const string RESOURCE_IRON = "ferro";
		
		/// <summary>Empty string for resource name</summary>
		public const string RESOURCE_EMPTY = "";
		
		#endregion
		
		#region Format Strings
		
		/// <summary>Format string for item label number</summary>
		public const string FORMAT_LABEL_NUMBER = "#{0}";
		
		/// <summary>Format string for hex color</summary>
		public const string FORMAT_HEX_COLOR = "#{0:X2}{1:X2}{2:X2}";
		
		/// <summary>Format string for weapon damage display</summary>
		public const string FORMAT_WEAPON_DAMAGE = "{0}\t{1}";
		
		/// <summary>Format string for weapon speed display</summary>
		public const string FORMAT_WEAPON_SPEED = "{0}s";
		
		/// <summary>Format string for durability display</summary>
		public const string FORMAT_DURABILITY = "{0}\t{1}";
		
		#endregion
		
		#region Skill Bonus Dictionary Keys
		
		/// <summary>Dictionary key for strength bonus</summary>
		public const string SKILL_BONUS_STRENGTH = "Strength";
		
		#endregion
		
		#region Phylactery Essence Names
		
		/// <summary>Phylactery essence: VampireEssence</summary>
		public const string PHYLACTERY_VAMPIRE_ESSENCE = "VampireEssence";
		
		/// <summary>Phylactery essence: SpringEssence</summary>
		public const string PHYLACTERY_SPRING_ESSENCE = "SpringEssence";
		
		/// <summary>Phylactery essence: SacredEssence</summary>
		public const string PHYLACTERY_SACRED_ESSENCE = "SacredEssence";
		
		#endregion
	}

}

