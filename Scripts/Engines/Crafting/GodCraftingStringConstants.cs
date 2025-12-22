using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for God Crafting systems (Smithing, Sewing, Brewing).
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Replaces hardcoded English strings with meaningful translations.
	/// </summary>
	public static class GodCraftingStringConstants
	{
		#region Gump Titles

		/// <summary>Gump title for God Smithing menu</summary>
		public const string GUMP_TITLE_SMITHING = "<BASEFONT Color=#FBFBFB><CENTER>MENU DE FORJARIA MÁGICA</CENTER></BASEFONT>";

		/// <summary>Gump title for God Sewing menu</summary>
		public const string GUMP_TITLE_SEWING = "<BASEFONT Color=#FBFBFB><CENTER>MENU DE COSTURA MÁGICA</CENTER></BASEFONT>";

		/// <summary>Gump title for God Brewing menu</summary>
		public const string GUMP_TITLE_BREWING = "<BASEFONT Color=#FBFBFB><CENTER>MENU DE ALQUIMIA MÁGICA</CENTER></BASEFONT>";

		#endregion

		#region Category Names - Smithing

		public const string CATEGORY_AMETHYST_EMERALD = "Ametista & Esmeralda";
		public const string CATEGORY_GARNET_ICE = "Granada & Gelo";
		public const string CATEGORY_JADE_MARBLE = "Jade & Mármore";
		public const string CATEGORY_ONYX_QUARTZ = "Ônix & Quartzo";
		public const string CATEGORY_RUBY_SAPPHIRE = "Rubi & Safira";
		public const string CATEGORY_SILVER_SPINEL = "Prata & Espinélio";
		public const string CATEGORY_STAR_RUBY_TOPAZ = "Rubi Estrela & Topázio";
		public const string CATEGORY_CADDELLITE = "Cadelita";

		#endregion

		#region Category Names - Sewing

		public const string CATEGORY_DEMON_SKIN = "Pele de Demônio";
		public const string CATEGORY_DRAGON_SKIN = "Pele de Dragão";
		public const string CATEGORY_NIGHTMARE_SKIN = "Pele de Pesadelo";
		public const string CATEGORY_SERPENT_SKIN = "Pele de Serpente";
		public const string CATEGORY_TROLL_SKIN = "Pele de Troll";
		public const string CATEGORY_UNICORN_SKIN = "Pele de Unicórnio";

		#endregion

		#region Category Names - Brewing

		public const string CATEGORY_INVISIBILITY = "Invisibilidade";
		public const string CATEGORY_INVULNERABILITY = "Invulnerabilidade";
		public const string CATEGORY_MANA = "Mana";
		public const string CATEGORY_REJUVENATE = "Rejuvenescer";
		public const string CATEGORY_RESURRECTION = "Ressurreição";
		public const string CATEGORY_REPAIR = "Reparo";

		#endregion

		#region Item Names - Gemstone Armor (Generic Terms)

		public const string ITEM_ARMS = "Braçadeiras";
		public const string ITEM_GAUNTLETS = "Manoplas";
		public const string ITEM_GORGET = "Gargantilha";
		public const string ITEM_LEGGINGS = "Perneiras";
		public const string ITEM_TUNIC = "Túnica";
		public const string ITEM_FEMALE_TUNIC = "Túnica Feminina";
		public const string ITEM_HELM = "Elmo";
		public const string ITEM_SHIELD = "Escudo";
		public const string ITEM_OIL = "Óleo";

		#endregion

		#region Item Names - Amethyst

		public const string ITEM_AMETHYST_ARMS = "Braçadeiras de Ametista";
		public const string ITEM_AMETHYST_GAUNTLETS = "Manoplas de Ametista";
		public const string ITEM_AMETHYST_GORGET = "Gargantilha de Ametista";
		public const string ITEM_AMETHYST_LEGGINGS = "Perneiras de Ametista";
		public const string ITEM_AMETHYST_TUNIC = "Túnica de Ametista";
		public const string ITEM_AMETHYST_FEMALE_TUNIC = "Túnica Feminina de Ametista";
		public const string ITEM_AMETHYST_HELM = "Elmo de Ametista";
		public const string ITEM_AMETHYST_SHIELD = "Escudo de Ametista";
		public const string ITEM_AMETHYST_OIL = "Óleo de Ametista";

		#endregion

		#region Item Names - Emerald

		public const string ITEM_EMERALD_ARMS = "Braçadeiras de Esmeralda";
		public const string ITEM_EMERALD_GAUNTLETS = "Manoplas de Esmeralda";
		public const string ITEM_EMERALD_GORGET = "Gargantilha de Esmeralda";
		public const string ITEM_EMERALD_LEGGINGS = "Perneiras de Esmeralda";
		public const string ITEM_EMERALD_TUNIC = "Túnica de Esmeralda";
		public const string ITEM_EMERALD_FEMALE_TUNIC = "Túnica Feminina de Esmeralda";
		public const string ITEM_EMERALD_HELM = "Elmo de Esmeralda";
		public const string ITEM_EMERALD_SHIELD = "Escudo de Esmeralda";
		public const string ITEM_EMERALD_OIL = "Óleo de Esmeralda";

		#endregion

		#region Item Names - Garnet

		public const string ITEM_GARNET_ARMS = "Braçadeiras de Granada";
		public const string ITEM_GARNET_GAUNTLETS = "Manoplas de Granada";
		public const string ITEM_GARNET_GORGET = "Gargantilha de Granada";
		public const string ITEM_GARNET_LEGGINGS = "Perneiras de Granada";
		public const string ITEM_GARNET_TUNIC = "Túnica de Granada";
		public const string ITEM_GARNET_FEMALE_TUNIC = "Túnica Feminina de Granada";
		public const string ITEM_GARNET_HELM = "Elmo de Granada";
		public const string ITEM_GARNET_SHIELD = "Escudo de Granada";
		public const string ITEM_GARNET_OIL = "Óleo de Granada";

		#endregion

		#region Item Names - Ice

		public const string ITEM_ICE_ARMS = "Braçadeiras de Gelo";
		public const string ITEM_ICE_GAUNTLETS = "Manoplas de Gelo";
		public const string ITEM_ICE_GORGET = "Gargantilha de Gelo";
		public const string ITEM_ICE_LEGGINGS = "Perneiras de Gelo";
		public const string ITEM_ICE_TUNIC = "Túnica de Gelo";
		public const string ITEM_ICE_FEMALE_TUNIC = "Túnica Feminina de Gelo";
		public const string ITEM_ICE_HELM = "Elmo de Gelo";
		public const string ITEM_ICE_SHIELD = "Escudo de Gelo";
		public const string ITEM_ICE_OIL = "Óleo de Gelo";

		#endregion

		#region Item Names - Jade

		public const string ITEM_JADE_ARMS = "Braçadeiras de Jade";
		public const string ITEM_JADE_GAUNTLETS = "Manoplas de Jade";
		public const string ITEM_JADE_GORGET = "Gargantilha de Jade";
		public const string ITEM_JADE_LEGGINGS = "Perneiras de Jade";
		public const string ITEM_JADE_TUNIC = "Túnica de Jade";
		public const string ITEM_JADE_FEMALE_TUNIC = "Túnica Feminina de Jade";
		public const string ITEM_JADE_HELM = "Elmo de Jade";
		public const string ITEM_JADE_SHIELD = "Escudo de Jade";
		public const string ITEM_JADE_OIL = "Óleo de Jade";

		#endregion

		#region Item Names - Marble

		public const string ITEM_MARBLE_ARMS = "Braçadeiras de Mármore";
		public const string ITEM_MARBLE_GAUNTLETS = "Manoplas de Mármore";
		public const string ITEM_MARBLE_GORGET = "Gargantilha de Mármore";
		public const string ITEM_MARBLE_LEGGINGS = "Perneiras de Mármore";
		public const string ITEM_MARBLE_TUNIC = "Túnica de Mármore";
		public const string ITEM_MARBLE_FEMALE_TUNIC = "Túnica Feminina de Mármore";
		public const string ITEM_MARBLE_HELM = "Elmo de Mármore";
		public const string ITEM_MARBLE_SHIELD = "Escudo de Mármore";
		public const string ITEM_MARBLE_OIL = "Óleo de Mármore";

		#endregion

		#region Item Names - Onyx

		public const string ITEM_ONYX_ARMS = "Braçadeiras de Ônix";
		public const string ITEM_ONYX_GAUNTLETS = "Manoplas de Ônix";
		public const string ITEM_ONYX_GORGET = "Gargantilha de Ônix";
		public const string ITEM_ONYX_LEGGINGS = "Perneiras de Ônix";
		public const string ITEM_ONYX_TUNIC = "Túnica de Ônix";
		public const string ITEM_ONYX_FEMALE_TUNIC = "Túnica Feminina de Ônix";
		public const string ITEM_ONYX_HELM = "Elmo de Ônix";
		public const string ITEM_ONYX_SHIELD = "Escudo de Ônix";
		public const string ITEM_ONYX_OIL = "Óleo de Ônix";

		#endregion

		#region Item Names - Quartz

		public const string ITEM_QUARTZ_ARMS = "Braçadeiras de Quartzo";
		public const string ITEM_QUARTZ_GAUNTLETS = "Manoplas de Quartzo";
		public const string ITEM_QUARTZ_GORGET = "Gargantilha de Quartzo";
		public const string ITEM_QUARTZ_LEGGINGS = "Perneiras de Quartzo";
		public const string ITEM_QUARTZ_TUNIC = "Túnica de Quartzo";
		public const string ITEM_QUARTZ_FEMALE_TUNIC = "Túnica Feminina de Quartzo";
		public const string ITEM_QUARTZ_HELM = "Elmo de Quartzo";
		public const string ITEM_QUARTZ_SHIELD = "Escudo de Quartzo";
		public const string ITEM_QUARTZ_OIL = "Óleo de Quartzo";

		#endregion

		#region Item Names - Ruby

		public const string ITEM_RUBY_ARMS = "Braçadeiras de Rubi";
		public const string ITEM_RUBY_GAUNTLETS = "Manoplas de Rubi";
		public const string ITEM_RUBY_GORGET = "Gargantilha de Rubi";
		public const string ITEM_RUBY_LEGGINGS = "Perneiras de Rubi";
		public const string ITEM_RUBY_TUNIC = "Túnica de Rubi";
		public const string ITEM_RUBY_FEMALE_TUNIC = "Túnica Feminina de Rubi";
		public const string ITEM_RUBY_HELM = "Elmo de Rubi";
		public const string ITEM_RUBY_SHIELD = "Escudo de Rubi";
		public const string ITEM_RUBY_OIL = "Óleo de Rubi";

		#endregion

		#region Item Names - Sapphire

		public const string ITEM_SAPPHIRE_ARMS = "Braçadeiras de Safira";
		public const string ITEM_SAPPHIRE_GAUNTLETS = "Manoplas de Safira";
		public const string ITEM_SAPPHIRE_GORGET = "Gargantilha de Safira";
		public const string ITEM_SAPPHIRE_LEGGINGS = "Perneiras de Safira";
		public const string ITEM_SAPPHIRE_TUNIC = "Túnica de Safira";
		public const string ITEM_SAPPHIRE_FEMALE_TUNIC = "Túnica Feminina de Safira";
		public const string ITEM_SAPPHIRE_HELM = "Elmo de Safira";
		public const string ITEM_SAPPHIRE_SHIELD = "Escudo de Safira";
		public const string ITEM_SAPPHIRE_OIL = "Óleo de Safira";

		#endregion

		#region Item Names - Silver

		public const string ITEM_SILVER_ARMS = "Braçadeiras de Prata";
		public const string ITEM_SILVER_GAUNTLETS = "Manoplas de Prata";
		public const string ITEM_SILVER_GORGET = "Gargantilha de Prata";
		public const string ITEM_SILVER_LEGGINGS = "Perneiras de Prata";
		public const string ITEM_SILVER_TUNIC = "Túnica de Prata";
		public const string ITEM_SILVER_FEMALE_TUNIC = "Túnica Feminina de Prata";
		public const string ITEM_SILVER_HELM = "Elmo de Prata";
		public const string ITEM_SILVER_SHIELD = "Escudo de Prata";
		public const string ITEM_SILVER_OIL = "Óleo de Prata";

		#endregion

		#region Item Names - Spinel

		public const string ITEM_SPINEL_ARMS = "Braçadeiras de Espinélio";
		public const string ITEM_SPINEL_GAUNTLETS = "Manoplas de Espinélio";
		public const string ITEM_SPINEL_GORGET = "Gargantilha de Espinélio";
		public const string ITEM_SPINEL_LEGGINGS = "Perneiras de Espinélio";
		public const string ITEM_SPINEL_TUNIC = "Túnica de Espinélio";
		public const string ITEM_SPINEL_FEMALE_TUNIC = "Túnica Feminina de Espinélio";
		public const string ITEM_SPINEL_HELM = "Elmo de Espinélio";
		public const string ITEM_SPINEL_SHIELD = "Escudo de Espinélio";
		public const string ITEM_SPINEL_OIL = "Óleo de Espinélio";

		#endregion

		#region Item Names - Star Ruby

		public const string ITEM_STAR_RUBY_ARMS = "Braçadeiras de Rubi Estrela";
		public const string ITEM_STAR_RUBY_GAUNTLETS = "Manoplas de Rubi Estrela";
		public const string ITEM_STAR_RUBY_GORGET = "Gargantilha de Rubi Estrela";
		public const string ITEM_STAR_RUBY_LEGGINGS = "Perneiras de Rubi Estrela";
		public const string ITEM_STAR_RUBY_TUNIC = "Túnica de Rubi Estrela";
		public const string ITEM_STAR_RUBY_FEMALE_TUNIC = "Túnica Feminina de Rubi Estrela";
		public const string ITEM_STAR_RUBY_HELM = "Elmo de Rubi Estrela";
		public const string ITEM_STAR_RUBY_SHIELD = "Escudo de Rubi Estrela";
		public const string ITEM_STAR_RUBY_OIL = "Óleo de Rubi Estrela";

		#endregion

		#region Item Names - Topaz

		public const string ITEM_TOPAZ_ARMS = "Braçadeiras de Topázio";
		public const string ITEM_TOPAZ_GAUNTLETS = "Manoplas de Topázio";
		public const string ITEM_TOPAZ_GORGET = "Gargantilha de Topázio";
		public const string ITEM_TOPAZ_LEGGINGS = "Perneiras de Topázio";
		public const string ITEM_TOPAZ_TUNIC = "Túnica de Topázio";
		public const string ITEM_TOPAZ_FEMALE_TUNIC = "Túnica Feminina de Topázio";
		public const string ITEM_TOPAZ_HELM = "Elmo de Topázio";
		public const string ITEM_TOPAZ_SHIELD = "Escudo de Topázio";
		public const string ITEM_TOPAZ_OIL = "Óleo de Topázio";

		#endregion

		#region Item Names - Caddellite

		public const string ITEM_CADDELLITE_ARMS = "Braçadeiras de Cadelita";
		public const string ITEM_CADDELLITE_GAUNTLETS = "Manoplas de Cadelita";
		public const string ITEM_CADDELLITE_GORGET = "Gargantilha de Cadelita";
		public const string ITEM_CADDELLITE_LEGGINGS = "Perneiras de Cadelita";
		public const string ITEM_CADDELLITE_TUNIC = "Túnica de Cadelita";
		public const string ITEM_CADDELLITE_FEMALE_TUNIC = "Túnica Feminina de Cadelita";
		public const string ITEM_CADDELLITE_HELM = "Elmo de Cadelita";
		public const string ITEM_CADDELLITE_SHIELD = "Escudo de Cadelita";
		public const string ITEM_CADDELLITE_OIL = "Óleo de Cadelita";

		#endregion

		#region Item Names - Creature Skin Armor (Generic Terms)

		public const string ITEM_SKIN_ARMS = "Braçadeiras de Pele";
		public const string ITEM_SKIN_CAP = "Capuz de Pele";
		public const string ITEM_SKIN_GLOVES = "Luvas de Pele";
		public const string ITEM_SKIN_GORGET = "Gargantilha de Pele";
		public const string ITEM_SKIN_LEGGINGS = "Perneiras de Pele";
		public const string ITEM_SKIN_TUNIC = "Túnica de Pele";

		#endregion

		#region Item Names - Demon Skin

		public const string ITEM_DEMON_SKIN_ARMS = "Braçadeiras de Pele de Demônio";
		public const string ITEM_DEMON_SKIN_CAP = "Capuz de Pele de Demônio";
		public const string ITEM_DEMON_SKIN_GLOVES = "Luvas de Pele de Demônio";
		public const string ITEM_DEMON_SKIN_GORGET = "Gargantilha de Pele de Demônio";
		public const string ITEM_DEMON_SKIN_LEGGINGS = "Perneiras de Pele de Demônio";
		public const string ITEM_DEMON_SKIN_TUNIC = "Túnica de Pele de Demônio";

		#endregion

		#region Item Names - Dragon Skin

		public const string ITEM_DRAGON_SKIN_ARMS = "Braçadeiras de Pele de Dragão";
		public const string ITEM_DRAGON_SKIN_CAP = "Capuz de Pele de Dragão";
		public const string ITEM_DRAGON_SKIN_GLOVES = "Luvas de Pele de Dragão";
		public const string ITEM_DRAGON_SKIN_GORGET = "Gargantilha de Pele de Dragão";
		public const string ITEM_DRAGON_SKIN_LEGGINGS = "Perneiras de Pele de Dragão";
		public const string ITEM_DRAGON_SKIN_TUNIC = "Túnica de Pele de Dragão";

		#endregion

		#region Item Names - Nightmare Skin

		public const string ITEM_NIGHTMARE_SKIN_ARMS = "Braçadeiras de Pele de Pesadelo";
		public const string ITEM_NIGHTMARE_SKIN_CAP = "Capuz de Pele de Pesadelo";
		public const string ITEM_NIGHTMARE_SKIN_GLOVES = "Luvas de Pele de Pesadelo";
		public const string ITEM_NIGHTMARE_SKIN_GORGET = "Gargantilha de Pele de Pesadelo";
		public const string ITEM_NIGHTMARE_SKIN_LEGGINGS = "Perneiras de Pele de Pesadelo";
		public const string ITEM_NIGHTMARE_SKIN_TUNIC = "Túnica de Pele de Pesadelo";

		#endregion

		#region Item Names - Serpent Skin

		public const string ITEM_SERPENT_SKIN_ARMS = "Braçadeiras de Pele de Serpente";
		public const string ITEM_SERPENT_SKIN_CAP = "Capuz de Pele de Serpente";
		public const string ITEM_SERPENT_SKIN_GLOVES = "Luvas de Pele de Serpente";
		public const string ITEM_SERPENT_SKIN_GORGET = "Gargantilha de Pele de Serpente";
		public const string ITEM_SERPENT_SKIN_LEGGINGS = "Perneiras de Pele de Serpente";
		public const string ITEM_SERPENT_SKIN_TUNIC = "Túnica de Pele de Serpente";

		#endregion

		#region Item Names - Troll Skin

		public const string ITEM_TROLL_SKIN_ARMS = "Braçadeiras de Pele de Troll";
		public const string ITEM_TROLL_SKIN_CAP = "Capuz de Pele de Troll";
		public const string ITEM_TROLL_SKIN_GLOVES = "Luvas de Pele de Troll";
		public const string ITEM_TROLL_SKIN_GORGET = "Gargantilha de Pele de Troll";
		public const string ITEM_TROLL_SKIN_LEGGINGS = "Perneiras de Pele de Troll";
		public const string ITEM_TROLL_SKIN_TUNIC = "Túnica de Pele de Troll";

		#endregion

		#region Item Names - Unicorn Skin

		public const string ITEM_UNICORN_SKIN_ARMS = "Braçadeiras de Pele de Unicórnio";
		public const string ITEM_UNICORN_SKIN_CAP = "Capuz de Pele de Unicórnio";
		public const string ITEM_UNICORN_SKIN_GLOVES = "Luvas de Pele de Unicórnio";
		public const string ITEM_UNICORN_SKIN_GORGET = "Gargantilha de Pele de Unicórnio";
		public const string ITEM_UNICORN_SKIN_LEGGINGS = "Perneiras de Pele de Unicórnio";
		public const string ITEM_UNICORN_SKIN_TUNIC = "Túnica de Pele de Unicórnio";

		#endregion

		#region Item Names - Potions

		public const string ITEM_LESSER_INVISIBILITY_POTION = "Poção de Invisibilidade Menor";
		public const string ITEM_INVISIBILITY_POTION = "Poção de Invisibilidade";
		public const string ITEM_GREATER_INVISIBILITY_POTION = "Poção de Invisibilidade Maior";
		public const string ITEM_INVULNERABILITY_POTION = "Poção de Invulnerabilidade";
		public const string ITEM_LESSER_MANA_POTION = "Poção de Mana Menor";
		public const string ITEM_MANA_POTION = "Poção de Mana";
		public const string ITEM_GREATER_MANA_POTION = "Poção de Mana Maior";
		public const string ITEM_LESSER_REJUVENATE_POTION = "Poção de Rejuvenescimento Menor";
		public const string ITEM_REJUVENATE_POTION = "Poção de Rejuvenescimento";
		public const string ITEM_GREATER_REJUVENATE_POTION = "Poção de Rejuvenescimento Maior";
		public const string ITEM_SUPER_POTION = "Poção Superior";
		public const string ITEM_AUTO_RES_POTION = "Poção de Auto-Ressurreição";
		public const string ITEM_RESURRECT_POTION = "Poção de Ressurreição";
		public const string ITEM_REPAIR_POTION = "Poção de Reparo";

		#endregion

		#region Resource Names - Ingots

		public const string RESOURCE_AMETHYST_INGOT = "Bloco de Ametista";
		public const string RESOURCE_EMERALD_INGOT = "Bloco de Esmeralda";
		public const string RESOURCE_GARNET_INGOT = "Bloco de Granada";
		public const string RESOURCE_ICE_INGOT = "Bloco de Gelo";
		public const string RESOURCE_JADE_INGOT = "Bloco de Jade";
		public const string RESOURCE_MARBLE_INGOT = "Bloco de Mármore";
		public const string RESOURCE_ONYX_INGOT = "Bloco de Ônix";
		public const string RESOURCE_QUARTZ_INGOT = "Bloco de Quartzo";
		public const string RESOURCE_RUBY_INGOT = "Bloco de Rubi";
		public const string RESOURCE_SAPPHIRE_INGOT = "Bloco de Safira";
		public const string RESOURCE_SILVER_INGOT = "Bloco de Prata";
		public const string RESOURCE_SPINEL_INGOT = "Bloco de Espinélio";
		public const string RESOURCE_STAR_RUBY_INGOT = "Bloco de Rubi Estrela";
		public const string RESOURCE_TOPAZ_INGOT = "Bloco de Topázio";
		public const string RESOURCE_CADDELLITE_INGOT = "Bloco de Cadelita";

		#endregion

		#region Resource Names - Skins

		public const string RESOURCE_DEMON_SKIN = "Pele de Demônio";
		public const string RESOURCE_DRAGON_SKIN = "Pele de Dragão";
		public const string RESOURCE_NIGHTMARE_SKIN = "Pele de Pesadelo";
		public const string RESOURCE_SERPENT_SKIN = "Pele de Serpente";
		public const string RESOURCE_TROLL_SKIN = "Pele de Troll";
		public const string RESOURCE_UNICORN_SKIN = "Pele de Unicórnio";

		#endregion

		#region Resource Names - Alchemy Ingredients

		public const string RESOURCE_BOTTLE = "Garrafa";
		public const string RESOURCE_SILVER_SERPENT_VENOM = "Veneno de Serpente Prateada";
		public const string RESOURCE_DRAGON_BLOOD = "Sangue de Dragão";
		public const string RESOURCE_ENCHANTED_SEAWEED = "Alga Marinha Encantada";
		public const string RESOURCE_DRAGON_TOOTH = "Dente de Dragão";
		public const string RESOURCE_GOLDEN_SERPENT_VENOM = "Veneno de Serpente Dourada";
		public const string RESOURCE_LICH_DUST = "Pó de Lich";
		public const string RESOURCE_DEMON_CLAW = "Garra de Demônio";
		public const string RESOURCE_UNICORN_HORN = "Chifre de Unicórnio";
		public const string RESOURCE_DEMIGOD_BLOOD = "Sangue de Semideus";
		public const string RESOURCE_GHOSTLY_DUST = "Pó Fantasmagórico";

		#endregion

		#region Error Messages

		/// <summary>Cliloc 1044038: "You have worn out your tool!"</summary>
		public const string MSG_TOOL_WORN_OUT = "Você desgastou sua ferramenta!";

		/// <summary>Cliloc 1044263: "The tool must be on your person to use."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = "A ferramenta deve estar com você para usar.";

		/// <summary>Cliloc 1044043: "You failed to create the item, and some of your materials are lost."</summary>
		public const string MSG_FAILED_MATERIAL_LOST = "Você falhou em criar o item e alguns materiais foram perdidos.";

		/// <summary>Cliloc 1044157: "You failed to create the item, but no materials were lost."</summary>
		public const string MSG_FAILED_NO_MATERIAL_LOST = "Você falhou em criar o item, mas nenhum material foi perdido.";

		/// <summary>Cliloc 1044154: "You create the item."</summary>
		public const string MSG_ITEM_CREATED = "Você cria o item.";

		#endregion

		#region Tool Names and Descriptions

		// GodSmithing Tool
		public const string TOOL_SMITHING_NAME = "Martelo de Metalurgia Divina";
		public const string TOOL_SMITHING_DESC = "O Lendário Martelo de Metalurgia";
		public const string TOOL_SMITHING_LOCATION = "Só pode ser usado na forja do dragão";

		// GodSewing Tool
		public const string TOOL_SEWING_NAME = "Tesoura Divina";
		public const string TOOL_SEWING_DESC = "A lendária tesoura de costura dos deuses";
		public const string TOOL_SEWING_LOCATION = "Só pode ser usado na roda giratória encantada";

		// GodBrewing Tool
		public const string TOOL_BREWING_NAME = "Garrafa Divina de Alquimia";
		public const string TOOL_BREWING_DESC = "A lendária garrafa de alquimia";
		public const string TOOL_BREWING_LOCATION = "Só pode ser usado no Santuário de Alquimia";

		#endregion
	}
}
