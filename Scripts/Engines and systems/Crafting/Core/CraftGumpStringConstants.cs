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

		/// <summary>Continue button label (for maker's mark confirmation)</summary>
		public const string BUTTON_CONTINUE = "CONTINUAR";

		/// <summary>Cancel button label (for maker's mark confirmation)</summary>
		public const string BUTTON_CANCEL = "CANCELAR";

		#endregion

		#region Crafting Messages

		/// <summary>Message displayed when player still has items to craft (format: {0} = remaining quantity)</summary>
		public const string MESSAGE_ITEMS_REMAINING = "Você ainda tem {0} item(s) para criar.";

		/// <summary>Message displayed when player finishes crafting all items</summary>
		public const string MESSAGE_CRAFTING_COMPLETE = "Você terminou de criar o(s) item(s) da lista.";

		#endregion

		#region Notice Messages (PT-BR)

		/// <summary>Message: "You don't have the required skills to attempt this item." (cliloc 1044153)</summary>
		public const string NOTICE_NO_SKILL = "Você não tem as habilidades necessárias para tentar criar este item.";

		/// <summary>Message: "You haven't made anything yet." (cliloc 1044165)</summary>
		public const string NOTICE_HAVENT_MADE_ANYTHING = "Você ainda não fez nada.";

		/// <summary>Message: "You must be near an anvil and a forge to smith items." (cliloc 1044267)</summary>
		public const string NOTICE_MUST_BE_NEAR_ANVIL_AND_FORGE = "Você deve estar perto de uma bigorna e uma forja para forjar itens.";

	/// <summary>Message: "You must learn that recipe from a scroll." (cliloc 1072847)</summary>
	public const string NOTICE_MUST_LEARN_RECIPE = "Você deve aprender essa receita de um pergaminho.";

	/// <summary>Message: "You have not learned this recipe." (cliloc 1073620)</summary>
	public const string NOTICE_RECIPE_NOT_LEARNED = "Você não aprendeu esta receita.";

	/// <summary>Message: "You must be near an anvil" (cliloc 1044266)</summary>
	public const string NOTICE_MUST_BE_NEAR_ANVIL = "Você deve estar perto de uma bigorna.";

		/// <summary>Message: "You must be near a forge." (cliloc 1044265)</summary>
		public const string NOTICE_MUST_BE_NEAR_FORGE = "Você deve estar perto de uma forja.";

		/// <summary>Message: "You cannot work this strange and unusual metal." (cliloc 1044268)</summary>
		public const string NOTICE_CANNOT_WORK_STRANGE_METAL = "Você não pode trabalhar este metal estranho e incomum.";

		/// <summary>Message: "You have no idea how to work this metal." (cliloc 1044269)</summary>
		public const string NOTICE_NO_SKILL_METAL = "Você não tem ideia de como trabalhar este metal.";

		/// <summary>Message: "You can't melt that down into ingots." (cliloc 1044272)</summary>
		public const string NOTICE_CANNOT_SMELT = "Você não pode derreter isso em lingotes.";

		/// <summary>Message: "You don't have the resources required to make that item." / "You do not have enough resources to make that item." (cliloc 502925)</summary>
		public const string NOTICE_INSUFFICIENT_RESOURCES = "Você não tem recursos suficientes para fazer esse item.";

	/// <summary>Message: "You do not have sufficient metal to make that." (cliloc 1044037)</summary>
	public const string NOTICE_INSUFFICIENT_METAL = "Você não tem metal suficiente para fazer isso.";

	/// <summary>Message: "You do not have sufficient wood to make that." (cliloc 1044351)</summary>
	public const string NOTICE_INSUFFICIENT_WOOD = "Você não possui madeira suficiente para fazer isso.";

	/// <summary>Message: "You don't have the components needed to make that." (cliloc 1044253)</summary>
	public const string NOTICE_INSUFFICIENT_COMPONENTS = "Você não tem os componentes necessários para fazer isso.";

	/// <summary>Message: "You don't have any logs." (cliloc 1044465)</summary>
	public const string NOTICE_INSUFFICIENT_LOGS = "Você não possui toras de madeira.";

	/// <summary>Message: "Makes as many as possible at once" (cliloc 1048176)</summary>
	public const string NOTICE_MAKES_AS_MANY_AS_POSSIBLE = "Faz o maior número possível de uma vez";

	/// <summary>Message: "You don't have the resources required to make that item." (cliloc 1042081 - Dragon Scales)</summary>
	public const string NOTICE_INSUFFICIENT_DRAGON_SCALES = "Você não tem recursos suficientes para fazer esse item.";

		/// <summary>Message: "You cannot enhance this type of item with the properties of the selected special material." (cliloc 1061011)</summary>
		public const string NOTICE_CANNOT_ENHANCE_TYPE = "Você não pode aprimorar este tipo de item com as propriedades do material especial selecionado.";

		/// <summary>Message: "That item cannot be repaired." (cliloc 1044277)</summary>
		public const string NOTICE_CANNOT_REPAIR = "Este item não pode ser reparado.";

		/// <summary>Message: "You cannot repair that item with this type of repair contract." (cliloc 1061136)</summary>
		public const string NOTICE_CANNOT_REPAIR_WITH_DEED = "Você não pode reparar este item com este tipo de contrato de reparo.";

		/// <summary>Message: "You can't repair that." (cliloc 500426)</summary>
		public const string NOTICE_CANNOT_REPAIR_GENERIC = "Você não pode reparar isso.";

		/// <summary>Message: "You must select a special material in order to enhance an item with its properties." (cliloc 1061010)</summary>
		public const string NOTICE_SELECT_SPECIAL_MATERIAL = "Você deve selecionar um material especial para aprimorar um item com suas propriedades.";

		/// <summary>Message: "You repair the item." (cliloc 1044279)</summary>
		public const string NOTICE_REPAIR_SUCCESS = "Você reparou o item.";

		/// <summary>Message: "You fail to repair the item." (cliloc 1044280)</summary>
		public const string NOTICE_REPAIR_FAILURE = "Você falhou ao reparar o item.";

		/// <summary>Message: "You melt the item down into ingots." (cliloc 1044270) - Format: {0} = ingot amount</summary>
		public const string NOTICE_SMELT_SUCCESS = "Você derreteu o item em {0} lingote(s).";

		/// <summary>Message: "That item is in full repair" (cliloc 1044281)</summary>
		public const string NOTICE_FULL_REPAIR = "Este item está completamente reparado.";

		/// <summary>Message: "You failed to create the item, and some of your materials are lost." (cliloc 1044043)</summary>
		public const string NOTICE_CRAFT_FAILED_MATERIALS_LOST = "Você falhou ao criar o item, e alguns de seus materiais foram perdidos.";

		/// <summary>Message: "Do you wish to place your maker's mark on this item?" (cliloc 1018317)</summary>
		public const string QUERY_MAKERS_MARK = "Você deseja colocar sua marca de artesão neste item?";

		/// <summary>Message: "The item had no durability reduction!"</summary>
		public const string NOTICE_NO_DURABILITY_REDUCTION = "O item não teve redução de durabilidade!";

		/// <summary>Message: "The item had its durability reduced by {0} points."</summary>
		public const string NOTICE_DURABILITY_REDUCED = "O item teve sua durabilidade reduzida em {0} pontos.";

		/// <summary>Label: "Success Chance:" (cliloc 1044057)</summary>
		public const string LABEL_SUCCESS_CHANCE = "Chance de Sucesso:";

		/// <summary>Message: "The item retains the color of this material" (cliloc 1044152)</summary>
		public const string MESSAGE_RETAINS_COLOR = "* O item retém a cor deste material";

		#endregion
	}
}

