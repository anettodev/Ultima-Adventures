namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for CraftGump UI labels and buttons.
	/// Extracted from CraftGump.cs to improve maintainability and enable localization.
	/// </summary>
	public static class CraftGumpStringConstants
	{
		#region Main Menu Labels

		/// <summary>Categories section header</summary>
		public const string LABEL_CATEGORIES = "<CENTER>CATEGORIAS</CENTER>";

		/// <summary>Selections section header</summary>
		public const string LABEL_SELECTIONS = "<CENTER>SELEÇÕES</CENTER>";

		/// <summary>Notices section header</summary>
		public const string LABEL_NOTICES = "<CENTER>AVISOS</CENTER>";

		#endregion

		#region Button Labels

		/// <summary>Exit button label</summary>
		public const string BUTTON_EXIT = "SAIR";

		/// <summary>Make last item button label</summary>
		public const string BUTTON_MAKE_LAST = "FAZER ÚLTIMO";

		/// <summary>Mark item button label (always mark)</summary>
		public const string BUTTON_MARK_ITEM = "MARCAR ITEM";

		/// <summary>Do not mark item button label (never mark)</summary>
		public const string BUTTON_DO_NOT_MARK_ITEM = "NÃO MARCAR ITEM";

		/// <summary>Prompt for mark button label (ask each time)</summary>
		public const string BUTTON_PROMPT_FOR_MARK = "PERGUNTAR MARCA";

		/// <summary>Smelt item button label</summary>
		public const string BUTTON_SMELT_ITEM = "FUNDIR ITEM";

		/// <summary>Repair item button label</summary>
		public const string BUTTON_REPAIR_ITEM = "REPARAR ITEM";

		/// <summary>Enhance item button label</summary>
		public const string BUTTON_ENHANCE_ITEM = "APRIMORAR ITEM";

		/// <summary>Last ten items button label</summary>
		public const string BUTTON_LAST_TEN = "ÚLTIMOS DEZ";

		/// <summary>Next page button label</summary>
		public const string BUTTON_NEXT_PAGE = "PRÓXIMA";

		/// <summary>Previous page button label</summary>
		public const string BUTTON_PREV_PAGE = "ANTERIOR";

		/// <summary>Quantity label prefix</summary>
		public const string LABEL_QUANTITY = "QTD:";

		/// <summary>Use resource color button label (when enabled)</summary>
		public const string BUTTON_USE_RESOURCE_COLOR = "USAR COR";

		/// <summary>Do not use resource color button label (when disabled)</summary>
		public const string BUTTON_DO_NOT_USE_RESOURCE_COLOR = "NÃO USAR COR";

		#endregion

		#region Crafting Messages

		/// <summary>Message displayed when player still has items to craft (format: {0} = remaining quantity)</summary>
		public const string MESSAGE_ITEMS_REMAINING = "Você ainda tem {0} item(s) para criar.";

		/// <summary>Message displayed when player finishes crafting all items</summary>
		public const string MESSAGE_CRAFTING_COMPLETE = "Você terminou de criar o(s) item(s) da lista.";

		#endregion
	}
}

