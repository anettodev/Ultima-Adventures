using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for Confusion Blast Potion messages.
	/// Extracted from BaseConfusionBlastPotion.cs to improve maintainability and enable localization.
	/// All strings translated to Portuguese-Brazilian.
	/// </summary>
	public static class ConfusionBlastStringConstants
	{
	#region Item Names
	
	/// <summary>Name for regular confusion blast potion</summary>
	public const string NAME_CONFUSION_BLAST = "poção de explosão de confusão";
	
	/// <summary>Name for greater confusion blast potion</summary>
	public const string NAME_GREATER_CONFUSION_BLAST = "poção de explosão de confusão maior";
	
	#endregion
	
	#region Property Display (PT-BR)
	
	/// <summary>Potion type for regular confusion blast</summary>
	public const string TYPE_CONFUSION_BLAST = "confusão";
	
	/// <summary>Potion type for greater confusion blast</summary>
	public const string TYPE_GREATER_CONFUSION_BLAST = "confusão maior";
	
	#endregion
	}
}

