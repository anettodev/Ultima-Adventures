namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for TownHerald and DungeonGuide NPC messages and labels.
	/// Extracted from TownHerald.cs to improve maintainability and enable localization.
	/// </summary>
	public static class TownHeraldStringConstants
	{
		#region Titles (Portuguese)

		/// <summary>Title for DungeonGuide NPC</summary>
		public const string TITLE_DUNGEON_GUIDE = "o guia das masmorras";

		/// <summary>Title for TownHerald NPC</summary>
		public const string TITLE_TOWN_CRIER = "o arauto da cidade";

		#endregion

		#region Region Messages (Portuguese)

		/// <summary>Message for cave regions</summary>
		public const string MSG_REGION_CAVE = "Cuidado com desmoronamentos e criaturas venenosas nestas profundezas! Fique alerta e observe onde pisa.";

		/// <summary>Message for dead regions</summary>
		public const string MSG_REGION_DEAD = "Os mortos caminham por estas terras! Use magia sagrada e procure solo consagrado para sua segurança.";

		/// <summary>Message for gargoyle regions</summary>
		public const string MSG_REGION_GARGOYLE = "Gargulas habitam aqui! Sua pele semelhante a pedra resiste a armas normais. Magia e armas abençoadas são sua melhor defesa.";

		/// <summary>Message for necromancer regions</summary>
		public const string MSG_REGION_NECROMANCER = "Necromantes praticam suas artes sombrias nas proximidades! Cuidado com lacaios mortos-vivos e magia amaldiçoada.";

		/// <summary>Message for pirate regions</summary>
		public const string MSG_REGION_PIRATE = "Piratas navegam estas águas! Proteja sua carga e cuidado com grupos de abordagem.";

		/// <summary>Message for maze regions</summary>
		public const string MSG_REGION_MAZE = "Perdido no labirinto? Acompanhe seu caminho e cuidado com ilusões que podem enganar você!";

		/// <summary>Message for abyss regions</summary>
		public const string MSG_REGION_ABYSS = "Bem-vindo ao Abismo! As profundezas mais profundas guardam grandes perigos e maiores tesouros. Prepare-se!";

		/// <summary>Message for ice/frozen regions</summary>
		public const string MSG_REGION_ICE = "O frio morde profundamente aqui! Vista-se com roupas quentes e cuidado com queimaduras de frio.";

		/// <summary>Message for fire/volcano regions</summary>
		public const string MSG_REGION_FIRE = "Fogo e lava cercam você! Armaduras resistentes ao calor e poções de proteção contra fogo são essenciais aqui.";

		/// <summary>Message for swamp/bog regions</summary>
		public const string MSG_REGION_SWAMP = "Estas águas turvas escondem muitos perigos! Cuidado com plantas venenosas e criaturas que atacam por baixo.";

		/// <summary>Message for castle/keep regions</summary>
		public const string MSG_REGION_CASTLE = "Castelos antigos guardam segredos esquecidos! Esteja preparado para armadilhas, enigmas e poderosos guardiões.";

		/// <summary>Message for ruins regions</summary>
		public const string MSG_REGION_RUINS = "Estas ruínas em ruínas são instáveis! Cuidado com detritos caindo e maldições antigas.";

		/// <summary>Message for tomb/crypt regions</summary>
		public const string MSG_REGION_TOMB = "Tumbas guardam espíritos inquietos! Magia sagrada e armas abençoadas lhe servirão bem aqui.";

		/// <summary>Message for mine regions</summary>
		public const string MSG_REGION_MINE = "Túneis de mineração podem desmoronar! Traga picaretas, lanternas e esteja preparado para se desenterrar se necessário.";

		#endregion

		#region Dungeon Messages (Portuguese)

		/// <summary>Message for Deceit dungeon</summary>
		public const string MSG_DUNGEON_DECEIT = "Bem-vindo a Deceit! As ilusões aqui podem enganar até o aventureiro mais sábio. Não confie em nada que vê!";

		/// <summary>Message for Despise dungeon</summary>
		public const string MSG_DUNGEON_DESPISE = "Despise aguarda! As piscinas ácidas aqui podem derreter armadura e carne. Pule com cuidado e traga resistência a ácido!";

		/// <summary>Message for Destard dungeon</summary>
		public const string MSG_DUNGEON_DESTARD = "As chamas de Destard queimam forte! Poções de resistência ao fogo e roupas frescas são seus melhores amigos aqui.";

		/// <summary>Message for Shame dungeon</summary>
		public const string MSG_DUNGEON_SHAME = "Shame guarda muitos segredos! Procure portas escondidas e baús armadilhados, mas cuidado com os guardiões.";

		/// <summary>Message for Hythloth dungeon</summary>
		public const string MSG_DUNGEON_HYTHLOTH = "Hythloth, o submundo! A masmorra mais profunda guarda os segredos mais sombrios. Traga muitos suprimentos!";

		/// <summary>Message for Covetous dungeon</summary>
		public const string MSG_DUNGEON_COVETOUS = "Covetous está cheio de armadilhas e truques! Teste cada passo e verifique falsos pisos.";

		/// <summary>Message for Wind dungeon</summary>
		public const string MSG_DUNGEON_WIND = "A masmorra do vento! Correntes fortes podem jogá-lo para fora de penhascos. Mantenha-se no chão e mova-se com cuidado.";

		/// <summary>Message for Fire dungeon</summary>
		public const string MSG_DUNGEON_FIRE = "Masmorra de fogo! Tudo queima aqui. Traga resistência ao fogo e extintores.";

		/// <summary>Message for Ice dungeon</summary>
		public const string MSG_DUNGEON_ICE = "Masmorra de gelo! O frio é mortal. Roupas quentes e armas baseadas em fogo ajudarão.";

		/// <summary>Message for Doom dungeon</summary>
		public const string MSG_DUNGEON_DOOM = "Luva de Doom! Apenas os mais bravos entram aqui. Os artefatos dentro são lendários, mas os perigos também!";

		/// <summary>Message for Bedlam dungeon</summary>
		public const string MSG_DUNGEON_BEDLAM = "O enigma de Bedlam! Resolva os enigmas para prosseguir, mas respostas erradas trazem consequências mortais.";

		/// <summary>Message for Labyrinth dungeon</summary>
		public const string MSG_DUNGEON_LABYRINTH = "O Labirinto! Este labirinto de loucura testará sua sanidade. Traga um mapa ou se perca para sempre.";

		/// <summary>Message for Sanctuary dungeon</summary>
		public const string MSG_DUNGEON_SANCTUARY = "Um santuário nas profundezas! Descanse aqui com segurança, mas cuidado - a segurança frequentemente atrai perigo.";

		/// <summary>Message for Prison/Jail dungeon</summary>
		public const string MSG_DUNGEON_PRISON = "Complexo prisional! Cuidado com presos que podem ter escapado, e tenha cuidado com patrulhas de guardas.";

		/// <summary>Default message for unknown dungeons</summary>
		public const string MSG_DUNGEON_DEFAULT = "Bem-vindo a esta masmorra! Fique alerta, trabalhe em conjunto e lembre-se - a discrição é a melhor parte da bravura!";

		#endregion

		#region Infection Messages (Portuguese)

		/// <summary>Message format when carrier is spreading infection</summary>
		public const string MSG_CARRIER_SPREADING_FORMAT = "Ouvi falar de um ser chamado {0} espalhando uma infecção misteriosa nas terras!";

		/// <summary>Message when infected are seen</summary>
		public const string MSG_INFECTED_SEEN = "Infectados foram vistos nas terras! Ajuda é necessária!";

		/// <summary>Message when all is well</summary>
		public const string MSG_ALL_WELL = "Tudo está bem nas terras!";

		/// <summary>Message when infected are contained</summary>
		public const string MSG_INFECTED_CONTAINED = "Graças ao equilíbrio! Os infectados estão contidos.";

		/// <summary>Message when infected are spotted</summary>
		public const string MSG_INFECTED_SPOTTED = "Ouçam! Infectados foram avistados em ";

		/// <summary>Separator for location list</summary>
		public const string MSG_LOCATION_SEPARATOR = ", e ";

		/// <summary>Message for unknown location</summary>
		public const string MSG_UNKNOWN_LOCATION = "um local desconhecido, e ";

		/// <summary>Message that help is needed</summary>
		public const string MSG_HELP_NEEDED = "ajuda é urgentemente necessária!";

		#endregion

		#region Greeting Messages (Portuguese)

		/// <summary>Greeting message format</summary>
		public const string MSG_GOOD_DAY_FORMAT = "Bom dia para você, {0}.";

		#endregion
	}
}

