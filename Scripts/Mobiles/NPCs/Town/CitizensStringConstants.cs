namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for Citizens NPC messages and labels.
	/// Extracted from Citizens.cs to improve maintainability and enable localization.
	/// All strings are in PT-BR (Portuguese - Brazil) for consistency with the server.
	/// </summary>
	public static class CitizensStringConstants
	{
		#region Placeholder Strings

		/// <summary>Placeholder for player name in phrases</summary>
		public const string PLACEHOLDER_PLAYER_NAME = "Z~Z~Z~Z~Z";

		/// <summary>Placeholder for region name in phrases</summary>
		public const string PLACEHOLDER_REGION_NAME = "Y~Y~Y~Y~Y";

		/// <summary>Placeholder for gold amount in phrases</summary>
		public const string PLACEHOLDER_GOLD_AMOUNT = "G~G~G~G~G";

		#endregion

		#region Greeting Phrases

		/// <summary>Greeting phrase 1</summary>
		public const string GREETING_1 = "Saudações, Z~Z~Z~Z~Z.";

		/// <summary>Greeting phrase 2</summary>
		public const string GREETING_2 = "Olá, Z~Z~Z~Z~Z.";

		/// <summary>Greeting phrase 3</summary>
		public const string GREETING_3 = "Bom dia para você, Z~Z~Z~Z~Z.";

		/// <summary>Greeting phrase 4</summary>
		public const string GREETING_4 = "Olá, Z~Z~Z~Z~Z.";

		/// <summary>Greeting phrase 5 - exploring</summary>
		public const string GREETING_5_FORMAT = "Estamos aqui apenas para descansar após explorar {0}.";

		/// <summary>Greeting phrase 6 - first time</summary>
		public const string GREETING_6_FORMAT = "Esta é a primeira vez que estou em Y~Y~Y~Y~Y.";

		/// <summary>Greeting phrase 7 - welcome</summary>
		public const string GREETING_7 = "Olá, Z~Z~Z~Z~Z. Bem-vindo a Y~Y~Y~Y~Y.";

		#endregion

		#region Preface Phrases

		/// <summary>Preface: I found</summary>
		public const string PREFACE_FOUND = "Eu encontrei";

		/// <summary>Preface: I heard rumours about</summary>
		public const string PREFACE_HEARD_RUMOURS = "Eu ouvi rumores sobre";

		/// <summary>Preface: I heard a story about</summary>
		public const string PREFACE_HEARD_STORY = "Eu ouvi uma história sobre";

		/// <summary>Preface: I overheard someone tell of</summary>
		public const string PREFACE_OVERHEARD = "Eu ouvi alguém contar sobre";

		/// <summary>Preface: Some {adventurer} found</summary>
		public const string PREFACE_ADVENTURER_FOUND_FORMAT = "Algum {0} encontrou";

		/// <summary>Preface: Some {adventurer} heard rumours about</summary>
		public const string PREFACE_ADVENTURER_HEARD_RUMOURS_FORMAT = "Algum {0} ouviu rumores sobre";

		/// <summary>Preface: Some {adventurer} heard a story about</summary>
		public const string PREFACE_ADVENTURER_HEARD_STORY_FORMAT = "Algum {0} ouviu uma história sobre";

		/// <summary>Preface: Some {adventurer} overheard another tell of</summary>
		public const string PREFACE_ADVENTURER_OVERHEARD_FORMAT = "Algum {0} ouviu outro contar sobre";

		/// <summary>Preface: Some {adventurer} is spreading rumors about</summary>
		public const string PREFACE_ADVENTURER_SPREADING_FORMAT = "Algum {0} está espalhando rumores sobre";

		/// <summary>Preface: Some {adventurer} is telling tales about</summary>
		public const string PREFACE_ADVENTURER_TELLING_TALES_FORMAT = "Algum {0} está contando histórias sobre";

		/// <summary>Preface: We found</summary>
		public const string PREFACE_WE_FOUND = "Nós encontramos";

		/// <summary>Preface: We heard rumours about</summary>
		public const string PREFACE_WE_HEARD_RUMOURS = "Nós ouvimos rumores sobre";

		/// <summary>Preface: We heard a story about</summary>
		public const string PREFACE_WE_HEARD_STORY = "Nós ouvimos uma história sobre";

		/// <summary>Preface: We overheard someone tell of</summary>
		public const string PREFACE_WE_OVERHEARD = "Nós ouvimos alguém contar sobre";

		#endregion

		#region Rumor Templates

		/// <summary>Rumor template: I heard that {item} can be obtained in {locale}</summary>
		public const string RUMOR_TEMPLATE_1_FORMAT = "Eu ouvi que {0} pode ser obtido em {1}.";

		/// <summary>Rumor template: I heard something about {item} and {locale}</summary>
		public const string RUMOR_TEMPLATE_2_FORMAT = "Eu ouvi algo sobre {0} e {1}.";

		/// <summary>Rumor template: Someone told me that {locale} is where you would look for {item}</summary>
		public const string RUMOR_TEMPLATE_3_FORMAT = "Alguém me disse que {0} é onde você procuraria por {1}.";

		/// <summary>Rumor template: I heard many tales of adventurers going to {locale} and seeing {item}</summary>
		public const string RUMOR_TEMPLATE_4_FORMAT = "Eu ouvi muitas histórias de aventureiros indo para {0} e vendo {1}.";

		/// <summary>Rumor template: {name} was in the tavern talking about {item} and {locale}</summary>
		public const string RUMOR_TEMPLATE_5_FORMAT = "{0} estava na taverna falando sobre {1} e {2}.";

		/// <summary>Rumor template: I was talking with the local {job}, and they mentioned {item} and {locale}</summary>
		public const string RUMOR_TEMPLATE_6_FORMAT = "Eu estava conversando com o {0} local, e eles mencionaram {1} e {2}.";

		/// <summary>Rumor template: I met with {name} and they told me to bring back {item} from {locale}</summary>
		public const string RUMOR_TEMPLATE_7_FORMAT = "Eu me encontrei com {0} e eles me disseram para trazer {1} de {2}.";

		/// <summary>Rumor template: I heard that {item} can be found in {locale}</summary>
		public const string RUMOR_TEMPLATE_8_FORMAT = "Eu ouvi que {0} pode ser encontrado em {1}.";

		/// <summary>Rumor template: Someone from {city} died in {locale} searching for {item}</summary>
		public const string RUMOR_TEMPLATE_9_FORMAT = "Alguém de {0} morreu em {1} procurando por {2}.";

		#endregion

		#region Service Phrases - Wand Recharge

		/// <summary>Wand recharge service phrase</summary>
		public const string SERVICE_WAND_RECHARGE = "Eu posso recarregar qualquer varinha que você tenha com você, mas apenas até uma certa quantidade. Se você quiser minha ajuda, então simplesmente me entregue sua varinha para que eu possa realizar o ritual necessário.";

		#endregion

		#region Service Phrases - Blacksmith

		/// <summary>Blacksmith metal armor repair phrase</summary>
		public const string SERVICE_BLACKSMITH_ARMOR = "Eu sou um ferreiro bastante habilidoso, então se você precisar de qualquer armadura de metal reparada, posso fazer isso por você por 7.500 ouro. Apenas me entregue a armadura e eu verei o que posso fazer.";

		/// <summary>Blacksmith metal weapon repair phrase</summary>
		public const string SERVICE_BLACKSMITH_WEAPON = "Eu sou um ferreiro bastante habilidoso, então se você precisar de qualquer arma de metal reparada, posso fazer isso por você por 7.500 ouro. Apenas me entregue a arma e eu verei o que posso fazer.";

		#endregion

		#region Service Phrases - Leather Worker

		/// <summary>Leather worker repair phrase</summary>
		public const string SERVICE_LEATHER_WORKER = "Eu sou um trabalhador de couro bastante habilidoso, então se você precisar de qualquer item de couro reparado, posso fazer isso por você por 7.500 ouro. Apenas me entregue o item e eu verei o que posso fazer.";

		#endregion

		#region Service Phrases - Wood Worker

		/// <summary>Wood worker weapon repair phrase</summary>
		public const string SERVICE_WOOD_WORKER_WEAPON = "Eu sou um carpinteiro bastante habilidoso, então se você precisar de qualquer arma de madeira reparada, posso fazer isso por você por 7.500 ouro. Apenas me entregue a arma e eu verei o que posso fazer.";

		/// <summary>Wood worker armor repair phrase</summary>
		public const string SERVICE_WOOD_WORKER_ARMOR = "Eu sou um carpinteiro bastante habilidoso, então se você precisar de qualquer armadura de madeira reparada, posso fazer isso por você por 7.500 ouro. Apenas me entregue a armadura e eu verei o que posso fazer.";

		#endregion

		#region Service Phrases - Unlock

		/// <summary>Unlock service phrase</summary>
		public const string SERVICE_UNLOCK = "Se você precisar de um baú ou caixa desbloqueado, posso ajudá-lo com isso. Apenas me entregue o recipiente e eu verei o que posso fazer. Prometo devolvê-lo.";

		#endregion

		#region Service Phrases - Magic Item Sale

		/// <summary>Item description: magic item</summary>
		public const string ITEM_DESC_MAGIC = "um item mágico";

		/// <summary>Item description: enchanted item</summary>
		public const string ITEM_DESC_ENCHANTED = "um item encantado";

		/// <summary>Item description: special item</summary>
		public const string ITEM_DESC_SPECIAL = "um item especial";

		/// <summary>Action verb: found</summary>
		public const string ACTION_FOUND = "encontrei";

		/// <summary>Action verb: discovered</summary>
		public const string ACTION_DISCOVERED = "descobri";

		/// <summary>Willing phrase: willing to part with</summary>
		public const string WILLING_PART_WITH = "disposto a me separar";

		/// <summary>Willing phrase: willing to trade</summary>
		public const string WILLING_TRADE = "disposto a trocar";

		/// <summary>Willing phrase: willing to sell</summary>
		public const string WILLING_SELL = "disposto a vender";

		/// <summary>Magic item sale phrase template 1</summary>
		public const string MAGIC_ITEM_SALE_1_FORMAT = "Eu tenho {0} que eu {1} enquanto explorava {2} que estou {3} por G~G~G~G~G ouro.";

		/// <summary>Magic item sale phrase template 2</summary>
		public const string MAGIC_ITEM_SALE_2_FORMAT = "Eu ganhei {0} de um jogo de cartas em {1} que estou {2} por G~G~G~G~G ouro.";

		/// <summary>Magic item sale phrase template 3</summary>
		public const string MAGIC_ITEM_SALE_3_FORMAT = "Eu tenho {0} que eu {1} nos restos de algum {2} que estou {3} por G~G~G~G~G ouro.";

		/// <summary>Magic item sale phrase template 4</summary>
		public const string MAGIC_ITEM_SALE_4_FORMAT = "Eu tenho {0} que eu {1} de um baú em {2} que estou {3} por G~G~G~G~G ouro.";

		/// <summary>Magic item sale phrase template 5</summary>
		public const string MAGIC_ITEM_SALE_5_FORMAT = "Eu tenho {0} que eu {1} em uma besta que matei em {2} que estou {3} por G~G~G~G~G ouro.";

		/// <summary>Magic item sale phrase template 6</summary>
		public const string MAGIC_ITEM_SALE_6_FORMAT = "Eu tenho {0} que eu {1} em algum {2} em {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Magic item sale closing phrase</summary>
		public const string MAGIC_ITEM_SALE_CLOSING = "Você pode olhar na minha mochila para examinar o item se desejar. Se você quiser trocar, então me entregue o ouro e eu lhe darei o item.";

		#endregion

		#region Service Phrases - Material Vendors

		/// <summary>Action verb: mined</summary>
		public const string ACTION_MINED = "minerei";

		/// <summary>Action verb: smelted</summary>
		public const string ACTION_SMELTED = "fundi";

		/// <summary>Action verb: forged</summary>
		public const string ACTION_FORGED = "forjei";

		/// <summary>Action verb: dug up</summary>
		public const string ACTION_DUG_UP = "cavei";

		/// <summary>Action verb: excavated</summary>
		public const string ACTION_EXCAVATED = "escavei";

		/// <summary>Action verb: formed</summary>
		public const string ACTION_FORMED = "formei";

		/// <summary>Action verb: chopped</summary>
		public const string ACTION_CHOPPED = "cortei";

		/// <summary>Action verb: cut</summary>
		public const string ACTION_CUT = "cortei";

		/// <summary>Action verb: logged</summary>
		public const string ACTION_LOGGED = "desmatei";

		/// <summary>Action verb: skinned</summary>
		public const string ACTION_SKINNED = "esfolei";

		/// <summary>Action verb: tanned</summary>
		public const string ACTION_TANNED = "curti";

		/// <summary>Action verb: gathered</summary>
		public const string ACTION_GATHERED = "coletei";

		/// <summary>Location descriptor: cave</summary>
		public const string LOCATION_CAVE = "caverna";

		/// <summary>Location descriptor: mine</summary>
		public const string LOCATION_MINE = "mina";

		/// <summary>Location descriptor: woods</summary>
		public const string LOCATION_WOODS = "floresta";

		/// <summary>Location descriptor: forest</summary>
		public const string LOCATION_FOREST = "bosque";

		/// <summary>Location preposition: near</summary>
		public const string LOCATION_NEAR = "perto de";

		/// <summary>Location preposition: outside of</summary>
		public const string LOCATION_OUTSIDE = "fora de";

		/// <summary>Location preposition: by</summary>
		public const string LOCATION_BY = "próximo a";

		/// <summary>Material vendor phrase template</summary>
		public const string MATERIAL_VENDOR_PHRASE_FORMAT = "Eu tenho {0} {1} {2} que eu {3} em {4} {5} {6} que estou {7} por G~G~G~G~G ouro.";

		/// <summary>Material vendor closing phrase</summary>
		public const string MATERIAL_VENDOR_CLOSING_FORMAT = "Você pode olhar na minha mochila para examinar {0} se desejar. Se você quiser trocar, então me entregue o ouro e eu lhe darei {0}.";

		#endregion

		#region Service Phrases - Reagent Vendor

		/// <summary>Action verb: bought</summary>
		public const string ACTION_BOUGHT = "comprei";

		/// <summary>Action verb: acquired</summary>
		public const string ACTION_ACQUIRED = "adquiri";

		/// <summary>Action verb: purchased</summary>
		public const string ACTION_PURCHASED = "adquiri";

		/// <summary>Action verb: found (reagents)</summary>
		public const string ACTION_FOUND_REAGENT = "encontrei";

		/// <summary>Action verb: discovered (reagents)</summary>
		public const string ACTION_DISCOVERED_REAGENT = "descobri";

		/// <summary>Action verb: came upon</summary>
		public const string ACTION_CAME_UPON = "encontrei";

		/// <summary>Reagent vendor phrase template - found</summary>
		public const string REAGENT_VENDOR_FOUND_FORMAT = "Eu tenho {0} {1} que eu {2} em {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Reagent vendor phrase template - found deep within</summary>
		public const string REAGENT_VENDOR_FOUND_DEEP_FORMAT = "Eu tenho {0} {1} que eu {2} nas profundezas de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Reagent vendor phrase template - found somewhere</summary>
		public const string REAGENT_VENDOR_FOUND_SOMEWHERE_FORMAT = "Eu tenho {0} {1} que eu {2} em algum lugar de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Reagent vendor phrase template - bought in city</summary>
		public const string REAGENT_VENDOR_BOUGHT_CITY_FORMAT = "Eu tenho {0} {1} que eu {2} em {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Reagent vendor phrase template - bought near city</summary>
		public const string REAGENT_VENDOR_BOUGHT_NEAR_FORMAT = "Eu tenho {0} {1} que eu {2} perto de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Reagent vendor phrase template - bought somewhere</summary>
		public const string REAGENT_VENDOR_BOUGHT_SOMEWHERE_FORMAT = "Eu tenho {0} {1} que eu {2} em algum lugar de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Reagent vendor closing phrase</summary>
		public const string REAGENT_VENDOR_CLOSING = "Você pode olhar na minha mochila para examinar os reagentes se desejar. Se você quiser trocar, então me entregue o ouro e eu lhe darei os reagentes.";

		#endregion

		#region Service Phrases - Potion Vendor

		/// <summary>Action verb: brewed</summary>
		public const string ACTION_BREWED = "preparei";

		/// <summary>Action verb: concocted</summary>
		public const string ACTION_CONCOCTED = "concebi";

		/// <summary>Action verb: prepared</summary>
		public const string ACTION_PREPARED = "preparei";

		/// <summary>Potion vendor phrase template - found</summary>
		public const string POTION_VENDOR_FOUND_FORMAT = "Eu tenho {0} {1} que eu {2} em {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Potion vendor phrase template - found deep within</summary>
		public const string POTION_VENDOR_FOUND_DEEP_FORMAT = "Eu tenho {0} {1} que eu {2} nas profundezas de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Potion vendor phrase template - found somewhere</summary>
		public const string POTION_VENDOR_FOUND_SOMEWHERE_FORMAT = "Eu tenho {0} {1} que eu {2} em algum lugar de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Potion vendor phrase template - bought in city</summary>
		public const string POTION_VENDOR_BOUGHT_CITY_FORMAT = "Eu tenho {0} {1} que eu {2} em {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Potion vendor phrase template - bought near city</summary>
		public const string POTION_VENDOR_BOUGHT_NEAR_FORMAT = "Eu tenho {0} {1} que eu {2} perto de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Potion vendor phrase template - bought somewhere</summary>
		public const string POTION_VENDOR_BOUGHT_SOMEWHERE_FORMAT = "Eu tenho {0} {1} que eu {2} em algum lugar de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Potion vendor closing phrase</summary>
		public const string POTION_VENDOR_CLOSING = "Você pode olhar na minha mochila para examinar as poções se desejar. Se você quiser trocar, então me entregue o ouro e eu lhe darei as poções.";

		#endregion

		#region Service Phrases - Food Vendor

		/// <summary>Action verb: cooked</summary>
		public const string ACTION_COOKED = "cozinhei";

		/// <summary>Action verb: baked</summary>
		public const string ACTION_BAKED = "assei";

		/// <summary>Food vendor phrase template</summary>
		public const string FOOD_VENDOR_PHRASE_FORMAT = "Eu tenho {0} {1} que eu {2} em {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Food vendor phrase template - near</summary>
		public const string FOOD_VENDOR_PHRASE_NEAR_FORMAT = "Eu tenho {0} {1} que eu {2} perto de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Food vendor phrase template - somewhere</summary>
		public const string FOOD_VENDOR_PHRASE_SOMEWHERE_FORMAT = "Eu tenho {0} {1} que eu {2} em algum lugar de {3} que estou {4} por G~G~G~G~G ouro.";

		/// <summary>Food vendor closing phrase</summary>
		public const string FOOD_VENDOR_CLOSING_FORMAT = "Você pode olhar na minha mochila para examinar {0} se desejar. Se você quiser trocar, então me entregue o ouro e eu lhe darei {0}.";

		#endregion

		#region Service Phrases - Wizard Items

		/// <summary>Wizard item: jar of wizard reagents</summary>
		public const string WIZARD_ITEM_JAR_WIZARD = "um pote de reagentes de mago";

		/// <summary>Wizard item: jar of necromancer reagents</summary>
		public const string WIZARD_ITEM_JAR_NECRO = "um pote de reagentes de necromante";

		/// <summary>Wizard item: jar of alchemical reagents</summary>
		public const string WIZARD_ITEM_JAR_ALCHEMICAL = "um pote de reagentes alquímicos";

		/// <summary>Wizard item: book</summary>
		public const string WIZARD_ITEM_BOOK = "um livro";

		/// <summary>Wizard item: scroll</summary>
		public const string WIZARD_ITEM_SCROLL = "um pergaminho";

		/// <summary>Wizard item: wand</summary>
		public const string WIZARD_ITEM_WAND = "uma varinha";

		/// <summary>Wizard item sale phrase template</summary>
		public const string WIZARD_ITEM_SALE_FORMAT = "Eu tenho {0} que estou {1} por G~G~G~G~G ouro.";

		/// <summary>Wizard item sale closing phrase</summary>
		public const string WIZARD_ITEM_SALE_CLOSING = "Você pode olhar na minha mochila para examinar o item se desejar. Se você quiser trocar, então me entregue o ouro e eu lhe darei o item.";

		#endregion

		#region Error Messages

		/// <summary>Error: Wand does not need recharging</summary>
		public const string ERROR_WAND_NO_RECHARGE = "Isso não precisa ser recarregado.";

		/// <summary>Success: Wand is charged</summary>
		public const string SUCCESS_WAND_CHARGED = "Sua varinha está carregada.";

		/// <summary>Error: Wand has too many charges</summary>
		public const string ERROR_WAND_TOO_MANY_CHARGES = "Essa varinha já tem muitas cargas.";

		/// <summary>Success: Item unlocked</summary>
		public const string SUCCESS_UNLOCKED = "Eu desbloqueei para você.";

		/// <summary>Success: Armor repaired</summary>
		public const string SUCCESS_ARMOR_REPAIRED = "Isso está reparado e pronto para a batalha.";

		/// <summary>Success: Weapon repaired</summary>
		public const string SUCCESS_WEAPON_REPAIRED = "Isso está reparado e pronto para a batalha.";

		/// <summary>Error: Not enough gold</summary>
		public const string ERROR_NOT_ENOUGH_GOLD = "Olhe amigo, não parece que você tem ouro suficiente na sua mochila ou banco... ";

		/// <summary>Success: Fair trade</summary>
		public const string SUCCESS_FAIR_TRADE = "Isso é uma troca justa.";

		#endregion

		#region Death Message

		/// <summary>Death spell phrase</summary>
		public const string DEATH_SPELL = "In Vas Mani";

		#endregion
	}
}

