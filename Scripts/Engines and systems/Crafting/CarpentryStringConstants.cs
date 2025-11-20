namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Carpentry-related messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from DefCarpentry.cs to improve maintainability and enable localization.
	/// </summary>
	public static class CarpentryStringConstants
	{
		#region Resource Names (Portuguese)

		/// <summary>Resource name: "Tábuas"</summary>
		public const string RESOURCE_BOARDS = "Tábuas";

		/// <summary>Resource name: "Tecido"</summary>
		public const string RESOURCE_CLOTH = "Tecido";

		/// <summary>Resource name: "Couro"</summary>
		public const string RESOURCE_LEATHER = "Couro";

		/// <summary>Resource name: "Lingotes de Ferro"</summary>
		public const string RESOURCE_IRON_INGOTS = "Lingotes de Ferro";

		/// <summary>Resource name: "Pergaminhos em Branco"</summary>
		public const string RESOURCE_BLANK_SCROLLS = "Pergaminhos em Branco";

		/// <summary>Resource name: "Penas"</summary>
		public const string RESOURCE_FEATHERS = "Penas";

		/// <summary>Resource name: "Hastes"</summary>
		public const string RESOURCE_SHAFTS = "Hastes";

		/// <summary>Resource name: "Aduelas de Barril"</summary>
		public const string RESOURCE_BARREL_STAVES = "Aduelas de Barril";

		/// <summary>Resource name: "Aros de Barril"</summary>
		public const string RESOURCE_BARREL_HOOPS = "Aros de Barril";

		/// <summary>Resource name: "Tampa de Barril"</summary>
		public const string RESOURCE_BARREL_LID = "Tampa de Barril";

		/// <summary>Resource name: "Torneira de Barril"</summary>
		public const string RESOURCE_BARREL_TAP = "Torneira de Barril";

		/// <summary>Resource name: "Tábuas Élficas"</summary>
		public const string RESOURCE_ELVEN_BOARDS = "Tábuas Élficas";

		#endregion

		#region Error Messages (Portuguese)

		/// <summary>Error: "Você não possui madeira suficiente para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_WOOD = "Você não possui madeira suficiente para fazer isso.";

		/// <summary>Error: "Você não pode trabalhar esta madeira estranha e incomum."</summary>
		public const string ERROR_CANNOT_WORK_WOOD = "Você não pode trabalhar esta madeira estranha e incomum.";

		/// <summary>Error: "Você não possui toras de madeira."</summary>
		public const string ERROR_INSUFFICIENT_LOGS = "Você não possui toras de madeira.";

		/// <summary>Error: "Você não tem tecido suficiente para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_CLOTH = "Você não tem tecido suficiente para fazer isso.";

		/// <summary>Error: "Você não tem couro suficiente para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_LEATHER = "Você não tem couro suficiente para fazer isso.";

		/// <summary>Error: "Você não tem metal suficiente para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_METAL = "Você não tem metal suficiente para fazer isso.";

		/// <summary>Error: "Você não tem pergaminhos em branco suficientes para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_BLANK_SCROLLS = "Você não tem pergaminhos em branco suficientes para fazer isso.";

		/// <summary>Error: "Você não tem penas suficientes para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_FEATHERS = "Você não tem penas suficientes para fazer isso.";

		/// <summary>Error: "Você não tem hastes suficientes para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_SHAFTS = "Você não tem hastes suficientes para fazer isso.";

		/// <summary>Error: "Você não tem os componentes necessários para fazer isso."</summary>
		public const string ERROR_INSUFFICIENT_COMPONENTS = "Você não tem os componentes necessários para fazer isso.";

		/// <summary>Error: "Você não tem recursos suficientes para fazer esse item."</summary>
		public const string ERROR_INSUFFICIENT_RESOURCES = "Você não tem recursos suficientes para fazer esse item.";

		#endregion

		#region Craft Result Messages (Portuguese)

		/// <summary>Success: "Você cria o item."</summary>
		public const string RESULT_ITEM_CREATED = "Você cria o item.";

		/// <summary>Success: "Você cria um item de qualidade excepcional."</summary>
		public const string RESULT_EXCEPTIONAL_QUALITY = "Você cria um item de qualidade excepcional.";

		/// <summary>Success: "Você cria um item de qualidade excepcional e assina a sua marca nele."</summary>
		public const string RESULT_EXCEPTIONAL_WITH_MARK = "Você cria um item de qualidade excepcional e assina a sua marca nele.";

		/// <summary>Failure: "Você não conseguiu criar o item, mas nenhum material foi perdido."</summary>
		public const string RESULT_FAILED_NO_MATERIALS_LOST = "Você não conseguiu criar o item, mas nenhum material foi perdido.";

		/// <summary>Failure: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string RESULT_FAILED_LOST_MATERIALS = "Você falhou ao criar o item, e alguns de seus materiais foram perdidos.";

		/// <summary>Low Quality: "You were barely able to make this item. It's quality is below average."</summary>
		public const string RESULT_BARELY_MADE_ITEM = "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média.";

		#endregion

		#region Tool Messages (Portuguese)

		/// <summary>Error: "Você quebrou sua ferramenta!"</summary>
		public const string TOOL_WORN_OUT = "Você quebrou sua ferramenta!";

		/// <summary>Error: "A ferramenta deve estar com você para usar."</summary>
		public const string TOOL_MUST_BE_ON_PERSON = "A ferramenta deve estar com você para usar.";

		#endregion

		#region Craft Group Names (Portuguese)

		/// <summary>Group name: "Armários"</summary>
		public const string GROUP_ARMOIRES = "Armários";

		/// <summary>Group name: "Baús & Caixas"</summary>
		public const string GROUP_CHESTS_AND_BOXES = "Baús & Caixas";

		/// <summary>Group name: "Cômodas & Gaveteiros"</summary>
		public const string GROUP_DRESSERS_AND_DRAWERS = "Cômodas & Gaveteiros";

		/// <summary>Group name: "Estantes"</summary>
		public const string GROUP_SHELVES = "Estantes";

		/// <summary>Group name: "Mesas & Assentos"</summary>
		public const string GROUP_TABLES_AND_SEATS = "Mesas & Assentos";

		/// <summary>Group name: "Instrumentos Musicais"</summary>
		public const string GROUP_MUSICAL_INSTRUMENTS = "Instrumentos Musicais";

		/// <summary>Group name: "Outros & Variados"</summary>
		public const string GROUP_MISC = "Outros & Variados";

		#endregion

		#region Item Names (Portuguese)

		// Armários
		/// <summary>Item name: "Armário de Ripas"</summary>
		public const string ITEM_ARMOIRE_SLATS = "Armário de Ripas";

		/// <summary>Item name: "Armário Bonito"</summary>
		public const string ITEM_ARMOIRE_NICE = "Armário Bonito";

		/// <summary>Item name: "Armário Elegante (Médio)"</summary>
		public const string ITEM_FANCY_CABINET_MEDIUM = "Armário Elegante (Médio)";

		/// <summary>Item name: "Armário Simples (Médio)"</summary>
		public const string ITEM_SIMPLE_CABINET_MEDIUM = "Armário Simples (Médio)";

		/// <summary>Item name: "Cristaleira"</summary>
		public const string ITEM_DISPLAY_CABINET = "Cristaleira";

		/// <summary>Item name: "Guarda-Roupa"</summary>
		public const string ITEM_WARDROBE = "Guarda-Roupa";

		/// <summary>Item name: "Roupeiro"</summary>
		public const string ITEM_CLOSET = "Roupeiro";

		/// <summary>Item name: "Armário de Cozinha"</summary>
		public const string ITEM_KITCHEN_CABINET = "Armário de Cozinha";

		/// <summary>Item name: "Armário Elegante (Pequeno)"</summary>
		public const string ITEM_FANCY_CABINET_SMALL = "Armário Elegante (Pequeno)";

		/// <summary>Item name: "Armário Elegante (Médio)"</summary>
		public const string ITEM_FANCY_CABINET_MEDIUM = "Armário Elegante (Médio)";

		/// <summary>Item name: "Armário Simples (Pequeno)"</summary>
		public const string ITEM_SIMPLE_CABINET_SMALL = "Armário Simples (Pequeno)";

		/// <summary>Item name: "Armário Simples (Médio)"</summary>
		public const string ITEM_SIMPLE_CABINET_MEDIUM = "Armário Simples (Médio)";

		/// <summary>Item name: "Armário Curto"</summary>
		public const string ITEM_SHORT_CABINET = "Armário Curto";

		/// <summary>Item name: "Armário de Livros (Estreito)"</summary>
		public const string ITEM_BOOK_CABINET_NARROW = "Armário de Livros (Estreito)";

		/// <summary>Item name: "Armário de Livros (Grande)"</summary>
		public const string ITEM_BOOK_CABINET_LARGE = "Armário de Livros (Grande)";

		/// <summary>Item name: "Armário de Armazenamento (Pequeno)"</summary>
		public const string ITEM_STORAGE_CABINET_SMALL = "Armário de Armazenamento (Pequeno)";

		/// <summary>Item name: "Armário de Armazenamento (Grande)"</summary>
		public const string ITEM_STORAGE_CABINET_LARGE = "Armário de Armazenamento (Grande)";

		/// <summary>Item name: "Armário Alto Simples"</summary>
		public const string ITEM_TALL_CABINET_SIMPLE = "Armário Alto Simples";

		/// <summary>Item name: "Armário Alto Elegante"</summary>
		public const string ITEM_TALL_CABINET_FANCY = "Armário Alto Elegante";

		// Baús & Caixas
		/// <summary>Item name: "Caixa Simples (Pequena)"</summary>
		public const string ITEM_SIMPLE_CRATE_SMALL = "Caixa Simples (Pequena)";

		/// <summary>Item name: "Caixa Simples (Média)"</summary>
		public const string ITEM_SIMPLE_CRATE_MEDIUM = "Caixa Simples (Média)";

		/// <summary>Item name: "Caixa Simples (Grande)"</summary>
		public const string ITEM_SIMPLE_CRATE_LARGE = "Caixa Simples (Grande)";

		/// <summary>Item name: "Baú de Madeira (Pequeno)"</summary>
		public const string ITEM_WOODEN_CHEST_SMALL = "Baú de Madeira (Pequeno)";

		/// <summary>Item name: "Baú de Madeira (Médio)"</summary>
		public const string ITEM_WOODEN_CHEST_MEDIUM = "Baú de Madeira (Médio)";

		/// <summary>Item name: "Baú de Madeira (Grande)"</summary>
		public const string ITEM_WOODEN_CHEST_LARGE = "Baú de Madeira (Grande)";

		/// <summary>Item name: "Baú de Madeira Real"</summary>
		public const string ITEM_WOODEN_CHEST_ROYAL = "Baú de Madeira Real";

		/// <summary>Item name: "Baú de Madeira (Baixo)"</summary>
		public const string ITEM_WOODEN_FOOT_LOCKER = "Baú de Madeira (Baixo)";

		/// <summary>Item name: "Caixão Simples"</summary>
		public const string ITEM_SIMPLE_COFFIN = "Caixão Simples";

		/// <summary>Item name: "Caixão Elegante"</summary>
		public const string ITEM_ELEGANT_COFFIN = "Caixão Elegante";

		// Cômodas & Gaveteiros
		/// <summary>Item name: "Gabinete Alto"</summary>
		public const string ITEM_TALL_CABINET = "Gabinete Alto";

		/// <summary>Item name: "Gabinete Baixo"</summary>
		public const string ITEM_SHORT_CABINET = "Gabinete Baixo";

		/// <summary>Item name: "Cômoda Simples"</summary>
		public const string ITEM_SIMPLE_DRESSER = "Cômoda Simples";

		/// <summary>Item name: "Cômoda Grande"</summary>
		public const string ITEM_LARGE_DRESSER = "Cômoda Grande";

		/// <summary>Item name: "Cômoda Real"</summary>
		public const string ITEM_ROYAL_DRESSER = "Cômoda Real";

		/// <summary>Item name: "Gaveteiro Simples"</summary>
		public const string ITEM_SIMPLE_DRAWER = "Gaveteiro Simples";

		/// <summary>Item name: "Gaveteiro Elegante"</summary>
		public const string ITEM_ELEGANT_DRAWER = "Gaveteiro Elegante";

		/// <summary>Item name: "Gaveteiro Real"</summary>
		public const string ITEM_ROYAL_DRAWER = "Gaveteiro Real";

		// Estantes
		/// <summary>Item name: "Estante de Bambu (Pequena)"</summary>
		public const string ITEM_BAMBOO_SHELF_SMALL = "Estante de Bambu (Pequena)";

		/// <summary>Item name: "Estante de Bambu (Grande)"</summary>
		public const string ITEM_BAMBOO_SHELF_LARGE = "Estante de Bambu (Grande)";

		/// <summary>Item name: "Estante Rústica (Pequena)"</summary>
		public const string ITEM_RUSTIC_SHELF_SMALL = "Estante Rústica (Pequena)";

		/// <summary>Item name: "Estante Rústica (Grande)"</summary>
		public const string ITEM_RUSTIC_SHELF_LARGE = "Estante Rústica (Grande)";

		/// <summary>Item name: "Estante Maciça (Pequena)"</summary>
		public const string ITEM_SOLID_SHELF_SMALL = "Estante Maciça (Pequena)";

		/// <summary>Item name: "Estante Maciça (Grande)"</summary>
		public const string ITEM_SOLID_SHELF_LARGE = "Estante Maciça (Grande)";

		// Mesas & Assentos
		/// <summary>Item name: "Banquinho"</summary>
		public const string ITEM_FOOT_STOOL = "Banquinho";

		/// <summary>Item name: "Banco"</summary>
		public const string ITEM_STOOL = "Banco";

		/// <summary>Item name: "Banco Rústico"</summary>
		public const string ITEM_RUSTIC_BENCH = "Banco Rústico";

		/// <summary>Item name: "Cadeira de Palha"</summary>
		public const string ITEM_BAMBOO_CHAIR = "Cadeira de Palha";

		/// <summary>Item name: "Cadeira Simples"</summary>
		public const string ITEM_SIMPLE_CHAIR = "Cadeira Simples";

		/// <summary>Item name: "Cadeira Normal"</summary>
		public const string ITEM_NORMAL_CHAIR = "Cadeira Normal";

		/// <summary>Item name: "Cadeira Elegante"</summary>
		public const string ITEM_FANCY_CHAIR = "Cadeira Elegante";

		/// <summary>Item name: "Trono Simples"</summary>
		public const string ITEM_SIMPLE_THRONE = "Trono Simples";

		/// <summary>Item name: "Trono Elegante"</summary>
		public const string ITEM_ELEGANT_THRONE = "Trono Elegante";

		/// <summary>Item name: "Trono Élfico"</summary>
		public const string ITEM_ELVEN_THRONE = "Trono Élfico";

		/// <summary>Item name: "Mesa de Cabeceira"</summary>
		public const string ITEM_NIGHTSTAND = "Mesa de Cabeceira";

		/// <summary>Item name: "Mesa Larga"</summary>
		public const string ITEM_LARGE_TABLE = "Mesa Larga";

		/// <summary>Item name: "Mesa de Estudo"</summary>
		public const string ITEM_WRITING_TABLE = "Mesa de Estudo";

		/// <summary>Item name: "Mesa Simples"</summary>
		public const string ITEM_SIMPLE_TABLE = "Mesa Simples";

		/// <summary>Item name: "Mesa Grande"</summary>
		public const string ITEM_GRAND_TABLE = "Mesa Grande";

		/// <summary>Item name: "Mesa de Centro Simples"</summary>
		public const string ITEM_SIMPLE_LOW_TABLE = "Mesa de Centro Simples";

		/// <summary>Item name: "Mesa de Centro Elegante"</summary>
		public const string ITEM_ELEGANT_LOW_TABLE = "Mesa de Centro Elegante";

		// Instrumentos Musicais
		/// <summary>Item name: "Estante de Partitura (Curta)"</summary>
		public const string ITEM_SHORT_MUSIC_STAND = "Estante de Partitura (Curta)";

		/// <summary>Item name: "Estante de Partitura (Alta)"</summary>
		public const string ITEM_TALL_MUSIC_STAND = "Estante de Partitura (Alta)";

		/// <summary>Item name: "Flauta"</summary>
		public const string ITEM_BAMBOO_FLUTE = "Flauta";

		/// <summary>Item name: "Tambor"</summary>
		public const string ITEM_DRUMS = "Tambor";

		/// <summary>Item name: "Pandeiro"</summary>
		public const string ITEM_TAMBOURINE = "Pandeiro";

		/// <summary>Item name: "Pandeiro com fita"</summary>
		public const string ITEM_TAMBOURINE_TASSEL = "Pandeiro com fita";

		/// <summary>Item name: "Harpa (Pequena)"</summary>
		public const string ITEM_LAP_HARP = "Harpa (Pequena)";

		/// <summary>Item name: "Harpa (Grande)"</summary>
		public const string ITEM_HARP = "Harpa (Grande)";

		/// <summary>Item name: "Alaúde"</summary>
		public const string ITEM_LUTE = "Alaúde";

		/// <summary>Item name: "Gaita"</summary>
		public const string ITEM_PIPES = "Gaita";

		/// <summary>Item name: "Violino"</summary>
		public const string ITEM_FIDDLE = "Violino";

		// Staves and Shields (Weapons)
		/// <summary>Item name: "bastão"</summary>
		public const string ITEM_QUARTER_STAFF = "bastão";

		/// <summary>Item name: "bastão retorcido"</summary>
		public const string ITEM_GNARLED_STAFF = "bastão retorcido";

		// Crates (Special - Commented Items)
		/// <summary>Item name: "Caixa de Açougueiro"</summary>
		public const string ITEM_BUTCHER_CRATE = "Caixa de Açougueiro";

		/// <summary>Item name: "Caixa de Bibliotecário"</summary>
		public const string ITEM_LIBRARIAN_CRATE = "Caixa de Bibliotecário";

		/// <summary>Item name: "Caixa de Músico"</summary>
		public const string ITEM_MUSICIAN_CRATE = "Caixa de Músico";

		// Continued Staves and Shields
		/// <summary>Item name: "cajado de pastor"</summary>
		public const string ITEM_SHEPHERDS_CROOK = "cajado de pastor";

		/// <summary>Item name: "cajado druida"</summary>
		public const string ITEM_WILD_STAFF = "cajado druida";

		/// <summary>Item name: "bastão"</summary>
		public const string ITEM_QUARTER_STAFF = "bastão";

		/// <summary>Item name: "bastão retorcido"</summary>
		public const string ITEM_GNARLED_STAFF = "bastão retorcido";

		/// <summary>Item name: "escudo de madeira"</summary>
		public const string ITEM_WOODEN_SHIELD = "escudo de madeira";

		// Armor
		/// <summary>Item name: "ombreiras de madeira"</summary>
		public const string ITEM_WOODEN_PLATE_ARMS = "ombreiras de madeira";

		/// <summary>Item name: "elmo de madeira"</summary>
		public const string ITEM_WOODEN_PLATE_HELM = "elmo de madeira";

		/// <summary>Item name: "manoplas de madeira"</summary>
		public const string ITEM_WOODEN_PLATE_GLOVES = "manoplas de madeira";

		/// <summary>Item name: "gorgel de madeira"</summary>
		public const string ITEM_WOODEN_PLATE_GORGET = "gorgel de madeira";

		/// <summary>Item name: "calça de madeira"</summary>
		public const string ITEM_WOODEN_PLATE_LEGS = "calça de madeira";

		/// <summary>Item name: "peitoral de madeira"</summary>
		public const string ITEM_WOODEN_PLATE_CHEST = "peitoral de madeira";

		// Outros & Variados
		/// <summary>Item name: "gravetos"</summary>
		public const string ITEM_KINDLING = "gravetos";

		/// <summary>Item name: "lote de gravetos"</summary>
		public const string ITEM_KINDLING_BATCH = "lote de gravetos";

		/// <summary>Item name: "casca de árvore"</summary>
		public const string ITEM_BARK_FRAGMENT = "casca de árvore";

		/// <summary>Item name: "misturador de caldeirão"</summary>
		public const string ITEM_MIXING_SPOON = "misturador de caldeirão";

		/// <summary>Item name: "caldeirão de alquimia"</summary>
		public const string ITEM_ALCHEMY_TUB = "caldeirão de alquimia";

		#endregion

		#region Special Resource Names (Portuguese)

		/// <summary>Resource name: "óleo ceifador"</summary>
		public const string RESOURCE_REAPER_OIL = "óleo ceifador";

		/// <summary>Resource name: "seiva de árvore mística"</summary>
		public const string RESOURCE_MYSTICAL_TREE_SAP = "seiva de árvore mística";

		#endregion

		#region Wood Type Names (Portuguese)

		/// <summary>Wood type: "Madeira Comum"</summary>
		public const string WOOD_TYPE_COMMON = "Madeira Comum";

		/// <summary>Wood type: "Carvalho Cinza"</summary>
		public const string WOOD_TYPE_ASH = "Carvalho Cinza";

		/// <summary>Wood type: "Ébano"</summary>
		public const string WOOD_TYPE_EBONY = "Ébano";

		/// <summary>Wood type: "Élfica"</summary>
		public const string WOOD_TYPE_ELVEN = "Élfica";

		/// <summary>Wood type: "Ipê-Amarelo"</summary>
		public const string WOOD_TYPE_GOLDEN_OAK = "Ipê-Amarelo";

		/// <summary>Wood type: "Cerejeira"</summary>
		public const string WOOD_TYPE_CHERRY = "Cerejeira";

		/// <summary>Wood type: "Pau-Brasil"</summary>
		public const string WOOD_TYPE_ROSEWOOD = "Pau-Brasil";

		/// <summary>Wood type: "Nogueira Branca"</summary>
		public const string WOOD_TYPE_HICKORY = "Nogueira Branca";

		#endregion
	}
}

