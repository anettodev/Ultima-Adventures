using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for PotionKeg user messages and labels.
	/// Keg names in Portuguese-Brazilian (PT-BR) as requested.
	/// Message numbers reference UO client's built-in localized strings.
	/// Extracted from PotionKeg.cs to enable localization and improve maintainability.
	/// </summary>
	public static class PotionKegStringConstants
	{
		#region Keg Names (PT-BR)
		
		/// <summary>Name for empty potion keg (PT-BR: "barril de poções vazio")</summary>
		public const string KEG_NAME_EMPTY = "barril de poções";
		
		/// <summary>Name for filled potion keg (PT-BR: "barril de poções"). Specific type shown in properties.</summary>
		public const string KEG_NAME_FORMAT = "barril de poções";
		
		#endregion
		
		#region Fill Level Messages (PT-BR)
		
		/// <summary>Fill level message: "O barril está vazio." (white color)</summary>
		public const string MSG_KEG_EMPTY = "O barril está vazio.";
		
		/// <summary>Fill level message: "O barril está quase vazio." (red color)</summary>
		public const string MSG_KEG_NEARLY_EMPTY = "O barril está quase vazio.";
		
		/// <summary>Fill level message: "O barril não está muito cheio." (red color)</summary>
		public const string MSG_KEG_NOT_VERY_FULL = "O barril não está muito cheio.";
		
		/// <summary>Fill level message: "O barril está cerca de um quarto cheio." (orange color)</summary>
		public const string MSG_KEG_QUARTER_FULL = "O barril está cerca de um quarto cheio.";
		
		/// <summary>Fill level message: "O barril está cerca de um terço cheio." (orange color)</summary>
		public const string MSG_KEG_THIRD_FULL = "O barril está cerca de um terço cheio.";
		
		/// <summary>Fill level message: "O barril está quase metade cheio." (orange color)</summary>
		public const string MSG_KEG_ALMOST_HALF = "O barril está quase metade cheio.";
		
		/// <summary>Fill level message: "O barril está aproximadamente metade cheio." (yellow color)</summary>
		public const string MSG_KEG_HALF_FULL = "O barril está aproximadamente metade cheio.";
		
		/// <summary>Fill level message: "O barril está mais da metade cheio." (yellow color)</summary>
		public const string MSG_KEG_MORE_THAN_HALF = "O barril está mais da metade cheio.";
		
		/// <summary>Fill level message: "O barril está cerca de três quartos cheio." (yellow color)</summary>
		public const string MSG_KEG_THREE_QUARTERS = "O barril está cerca de três quartos cheio.";
		
		/// <summary>Fill level message: "O barril está muito cheio." (green color)</summary>
		public const string MSG_KEG_VERY_FULL = "O barril está muito cheio.";
		
		/// <summary>Fill level message: "O líquido está quase no topo do barril." (green color)</summary>
		public const string MSG_KEG_ALMOST_TOP = "O líquido está quase no topo do barril.";
		
		/// <summary>Fill level message: "O barril está completamente cheio." (green color)</summary>
		public const string MSG_KEG_FULL = "O barril está completamente cheio.";
		
		#endregion
		
		#region System Messages to Player (PT-BR)
		
		/// <summary>System message: "Você despeja um pouco do conteúdo do barril em uma garrafa vazia..."</summary>
		public const string MSG_POUR_INTO_BOTTLE = "Você despeja um pouco do conteúdo do barril em uma garrafa vazia...";
		
		/// <summary>System message: "...e coloca na sua mochila."</summary>
		public const string MSG_PLACE_IN_BACKPACK = "...e coloca na sua mochila.";
		
		/// <summary>System message: "...mas não há espaço para a garrafa na sua mochila."</summary>
		public const string MSG_NO_ROOM_FOR_BOTTLE = "...mas não há espaço para a garrafa na sua mochila.";
		
		/// <summary>System message: "O barril está vazio agora."</summary>
		public const string MSG_KEG_NOW_EMPTY = "O barril está vazio agora.";
		
		/// <summary>System message: "O barril não aguenta mais!"</summary>
		public const string MSG_KEG_FULL_CANNOT_ADD = "O barril não aguenta mais!";
		
		/// <summary>System message: "Você coloca a garrafa vazia na sua mochila."</summary>
		public const string MSG_EMPTY_BOTTLE_IN_PACK = "Você coloca a garrafa vazia na sua mochila.";
		
		/// <summary>System message: "Você não tem espaço para a garrafa vazia na sua mochila."</summary>
		public const string MSG_NO_ROOM_FOR_EMPTY = "Você não tem espaço para a garrafa vazia na sua mochila.";
		
		/// <summary>System message: "Você decide que seria uma má ideia misturar diferentes tipos de poções."</summary>
		public const string MSG_CANNOT_MIX_TYPES = "Você decide que seria uma má ideia misturar diferentes tipos de poções.";
		
		/// <summary>System message: "O barril não foi feito para guardar esse tipo de objeto."</summary>
		public const string MSG_INVALID_ITEM_TYPE = "O barril não foi feito para guardar esse tipo de objeto.";
		
		/// <summary>System message: "Não consigo alcançar isso."</summary>
		public const string MSG_CANT_REACH = "Não consigo alcançar isso.";
		
		/// <summary>Localized message for keg description</summary>
		public const int MSG_KEG_DESCRIPTION = 1041084;
		
		#endregion
	}
}

