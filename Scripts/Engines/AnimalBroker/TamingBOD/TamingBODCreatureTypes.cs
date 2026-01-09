using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Centralized creature type definitions for Taming BOD contracts.
	/// Defines which creature types belong to each tier and their PT-BR names.
	/// Merged from MonsterContractType - all 136+ creatures organized by rarity-based tiers.
	/// </summary>
	public static class TamingBODCreatureTypes
	{
		#region Tier 2: Normal Wildlife/Domestic Animals (Rarity 0-30)

		/// <summary>
		/// Tier 2 creature types: Normal wildlife and domestic animals.
		/// Rarity range: 0-30 (Common creatures)
		/// </summary>
		public static readonly Type[] Tier2CreatureTypes = new Type[]
		{
			// Rarity 10-14: Very Common
			typeof(Cat),
			typeof(Chicken),
			typeof(Dog),
			typeof(Ferret),
			typeof(JackRabbit),
			typeof(Mongbat),
			typeof(Rabbit),
			typeof(Squirrel),
			typeof(Weasel),
			typeof(Bird), // Rarity 14
			typeof(DesertBird), // Rarity 14
			typeof(SwampBird), // Rarity 14
			typeof(TropicalBird), // Rarity 14
			
			// Rarity 12-15: Common
			typeof(Gorilla), // Rarity 12
			typeof(MotherGorilla), // Rarity 12
			typeof(WhiteRabbit), // Rarity 12
			typeof(Fox), // Rarity 15
			
			// Rarity 20-21: Common Domestic
			typeof(GoldenHen), // Rarity 20
			typeof(MountainGoat), // Rarity 20
			typeof(Mouse), // Rarity 20
			typeof(Rat), // Rarity 20
			typeof(Sewerrat), // Rarity 20
			typeof(Cow), // Rarity 21
			
			// Rarity 23-30: Common Wildlife
			typeof(Jackal), // Rarity 23
			typeof(Goat), // Rarity 30
		};

		/// <summary>
		/// PT-BR names for Tier 2 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier2CreatureNames = new Dictionary<Type, string>
		{
			{ typeof(Cat), "Gatos" },
			{ typeof(Chicken), "Galinhas" },
			{ typeof(Dog), "Cachorros" },
			{ typeof(Ferret), "Furões" },
			{ typeof(JackRabbit), "Lebres" },
			{ typeof(Mongbat), "Mongbats" },
			{ typeof(Rabbit), "Coelhos" },
			{ typeof(Squirrel), "Esquilos" },
			{ typeof(Weasel), "Doninhas" },
			{ typeof(Bird), "Pássaros da Floresta" },
			{ typeof(DesertBird), "Pássaros do Deserto" },
			{ typeof(SwampBird), "Pássaros do Pântano" },
			{ typeof(TropicalBird), "Pássaros Tropicais" },
			{ typeof(Gorilla), "Gorilas" },
			{ typeof(MotherGorilla), "Gorilas Mães" },
			{ typeof(WhiteRabbit), "Coelhos Brancos" },
			{ typeof(Fox), "Raposas" },
			{ typeof(GoldenHen), "Galinhas Douradas" },
			{ typeof(MountainGoat), "Cabras da Montanha" },
			{ typeof(Mouse), "Ratos Pequenos" },
			{ typeof(Rat), "Ratos" },
			{ typeof(Sewerrat), "Ratos de Esgoto" },
			{ typeof(Cow), "Vacas" },
			{ typeof(Jackal), "Chacais" },
			{ typeof(Goat), "Cabras" }
		};

		#endregion

		#region Tier 3: Common Riding/Mounts (Rarity 31-60)

		/// <summary>
		/// Tier 3 creature types: Common riding animals, mounts, and medium-difficulty creatures.
		/// Rarity range: 31-60 (Uncommon creatures)
		/// </summary>
		public static readonly Type[] Tier3CreatureTypes = new Type[]
		{
			// Rarity 29-35: Common Mounts
			typeof(Horse), // Rarity 29
			typeof(LargeSnake), // Rarity 29
			typeof(LargeSpider), // Rarity 29
			typeof(Llama), // Rarity 35
			
			// Rarity 33-39: Medium Wildlife
			typeof(BullFrog), // Rarity 33
			typeof(Frog), // Rarity 33
			typeof(Hind), // Rarity 33
			typeof(BlackBear), // Rarity 35
			typeof(Boar), // Rarity 39
			typeof(GiantRat), // Rarity 39
			
			// Rarity 37-43: Medium Predators
			typeof(Eagle), // Rarity 37
			typeof(Hawk), // Rarity 37
			typeof(Cougar), // Rarity 41
			typeof(Ridgeback), // Rarity 43
			typeof(SavageRidgeback), // Rarity 43
			typeof(Slime), // Rarity 43
			typeof(TimberWolf), // Rarity 43
			typeof(Toad), // Rarity 43
			
			// Rarity 40-50: Mounts and Medium Creatures
			typeof(HatchlingDragon), // Rarity 40
			typeof(LionRiding), // Rarity 40
			typeof(ScourgeBat), // Rarity 40
			typeof(Pig), // Rarity 31
			typeof(Sheep), // Rarity 31
			typeof(Mantis), // Rarity 51
			typeof(Panda), // Rarity 51
			typeof(BrownBear), // Rarity 45
			typeof(Beetle), // Rarity 49
			typeof(DesertOstard), // Rarity 49
			typeof(ForestOstard), // Rarity 49
			typeof(RidableLlama), // Rarity 49
			typeof(Snake), // Rarity 49
			typeof(SnowOstard), // Rarity 49
			typeof(Zebra), // Rarity 49
			
			// Rarity 50-60: Medium-High Difficulty
			typeof(Elephant), // Rarity 50
			typeof(FireWyrmling), // Rarity 50
			typeof(DiseasedRat), // Rarity 52
			typeof(MadDog), // Rarity 53
			typeof(Panther), // Rarity 53
			typeof(PolarBear), // Rarity 55
			typeof(Walrus), // Rarity 55
			typeof(Alligator), // Rarity 57
			typeof(AxeBeak), // Rarity 59
			typeof(GlowBeetleRiding), // Rarity 59
			typeof(GreatHart), // Rarity 59
			typeof(PoisonBeetleRiding), // Rarity 59
			typeof(TigerBeetleRiding), // Rarity 59
			typeof(WaterBeetleRiding), // Rarity 59
			typeof(Bull), // Rarity 60
			typeof(SnowLion), // Rarity 60
		};

		/// <summary>
		/// PT-BR names for Tier 3 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier3CreatureNames = new Dictionary<Type, string>
		{
			{ typeof(Horse), "Cavalos" },
			{ typeof(Llama), "Lhamas" },
			{ typeof(BullFrog), "Sapos Grandes" },
			{ typeof(Frog), "Sapos" },
			{ typeof(Hind), "Corças" },
			{ typeof(BlackBear), "Ursos Negros" },
			{ typeof(Boar), "Javalis" },
			{ typeof(GiantRat), "Ratos Gigantes" },
			{ typeof(Eagle), "Águias" },
			{ typeof(Hawk), "Falcões" },
			{ typeof(Cougar), "Pumas" },
			{ typeof(Ridgeback), "Ridgebacks" },
			{ typeof(SavageRidgeback), "Ridgebacks Selvagens" },
			{ typeof(Slime), "Lamas" },
			{ typeof(TimberWolf), "Lobos do Bosque" },
			{ typeof(Toad), "Sapos" },
			{ typeof(HatchlingDragon), "Filhotes de Dragão" },
			{ typeof(LionRiding), "Leões" },
			{ typeof(ScourgeBat), "Morcegos Flagelantes" },
			{ typeof(Pig), "Porcos" },
			{ typeof(Sheep), "Ovelhas" },
			{ typeof(LargeSnake), "Cobras Grandes" },
			{ typeof(LargeSpider), "Aranhas Grandes" },
			{ typeof(Mantis), "Louva-a-Deus" },
			{ typeof(Panda), "Pandas" },
			{ typeof(BrownBear), "Ursos Marrons" },
			{ typeof(Beetle), "Besouros" },
			{ typeof(DesertOstard), "Ostards do Deserto" },
			{ typeof(ForestOstard), "Ostards da Floresta" },
			{ typeof(RidableLlama), "Lhamas Montáveis" },
			{ typeof(Snake), "Cobras" },
			{ typeof(SnowOstard), "Ostards da Neve" },
			{ typeof(Zebra), "Zebras" },
			{ typeof(Elephant), "Elefantes" },
			{ typeof(FireWyrmling), "Vermes de Fogo Jovens" },
			{ typeof(DiseasedRat), "Ratos Doentes" },
			{ typeof(MadDog), "Cachorros Raivosos" },
			{ typeof(Panther), "Panteras" },
			{ typeof(PolarBear), "Ursos Polares" },
			{ typeof(Walrus), "Morsas" },
			{ typeof(Alligator), "Jacarés" },
			{ typeof(AxeBeak), "Bicos de Machado" },
			{ typeof(GlowBeetleRiding), "Besouros Brilhantes" },
			{ typeof(GreatHart), "Cervos Grandes" },
			{ typeof(PoisonBeetleRiding), "Besouros Venenosos" },
			{ typeof(TigerBeetleRiding), "Besouros Tigre" },
			{ typeof(WaterBeetleRiding), "Besouros Aquáticos" },
			{ typeof(Bull), "Touros" },
			{ typeof(SnowLion), "Leões da Neve" }
		};

		#endregion

		#region Tier 4: Dangerous Creatures (Rarity 61-90)

		/// <summary>
		/// Tier 4 creature types: Dangerous creatures, predators, and rare mounts.
		/// Rarity range: 61-90 (Rare creatures)
		/// </summary>
		public static readonly Type[] Tier4CreatureTypes = new Type[]
		{
			// Rarity 61-69: Dangerous Predators
			typeof(Anhkheg), // Rarity 61
			typeof(DeathwatchBeetle), // Rarity 61
			typeof(LavaSerpent), // Rarity 63
			typeof(LavaSnake), // Rarity 63
			typeof(LesserSeaSnake), // Rarity 63
			typeof(BlackWolf), // Rarity 65
			typeof(Scorpion), // Rarity 67
			typeof(DireBoar), // Rarity 69
			typeof(GriffonRiding), // Rarity 69
			typeof(GrizzlyBearRiding), // Rarity 69
			
			// Rarity 71-79: Rare Predators and Creatures
			typeof(Bobcat), // Rarity 71
			typeof(GreyWolf), // Rarity 73
			typeof(SnowLeopard), // Rarity 73
			typeof(BloodSnake), // Rarity 73
			typeof(Toraxen), // Rarity 77
			typeof(Antelope), // Rarity 79
			typeof(GiantCrab), // Rarity 79
			typeof(GiantHawk), // Rarity 79
			typeof(GiantRaven), // Rarity 79
			typeof(GiantSnake), // Rarity 79
			typeof(GiantSpider), // Rarity 79
			typeof(HippogriffRiding), // Rarity 79
			typeof(SabreclawCub), // Rarity 79
			typeof(SabretoothCub), // Rarity 79
			typeof(SwampGator), // Rarity 79
			
			// Rarity 80-89: High-Difficulty Creatures
			typeof(FireSteed), // Rarity 80
			typeof(HugeLizard), // Rarity 80
			typeof(LavaLizard), // Rarity 80
			typeof(AlienSmall), // Rarity 81
			typeof(Jaguar), // Rarity 81
			typeof(Tiger), // Rarity 81
			typeof(WhiteTiger), // Rarity 81
			typeof(BullradonRiding), // Rarity 83
			typeof(DeepSeaSerpent), // Rarity 83
			typeof(GiantAdder), // Rarity 83
			typeof(GiantSerpent), // Rarity 83
			typeof(GorceratopsRiding), // Rarity 83
			typeof(GoldenSerpent), // Rarity 83
			typeof(IceSerpent), // Rarity 83
			typeof(IceSnake), // Rarity 83
			typeof(JadeSerpent), // Rarity 83
			typeof(JungleViper), // Rarity 83
			typeof(SeaSnake), // Rarity 83
			typeof(SilverSerpent), // Rarity 83
			typeof(Wyverns), // Rarity 83
			typeof(WhiteWolf), // Rarity 85
			typeof(DeadlyScorpion), // Rarity 87
			typeof(Ramadon), // Rarity 87
			typeof(CaveBearRiding), // Rarity 89
			typeof(DireBear), // Rarity 89
			typeof(ElderBlackBearRiding), // Rarity 89
			typeof(ElderBrownBearRiding), // Rarity 89
			typeof(ElderPolarBearRiding), // Rarity 89
			typeof(GreatBear), // Rarity 89
			typeof(KodiakBear), // Rarity 89
			typeof(SabretoothBearRiding) // Rarity 89
		};

		/// <summary>
		/// PT-BR names for Tier 4 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier4CreatureNames = new Dictionary<Type, string>
		{
			{ typeof(Anhkheg), "Anhkhegs" },
			{ typeof(DeathwatchBeetle), "Besouros da Morte" },
			{ typeof(LavaSerpent), "Serpentes de Lava" },
			{ typeof(LavaSnake), "Cobras de Lava" },
			{ typeof(LesserSeaSnake), "Serpentes do Mar Menores" },
			{ typeof(BlackWolf), "Lobos Negros" },
			{ typeof(Scorpion), "Escorpiões" },
			{ typeof(DireBoar), "Javalis Ferozes" },
			{ typeof(GriffonRiding), "Grifos" },
			{ typeof(GrizzlyBearRiding), "Ursos Pardos" },
			{ typeof(Bobcat), "Linces" },
			{ typeof(GreyWolf), "Lobos Cinzentos" },
			{ typeof(SnowLeopard), "Leopardos da Neve" },
			{ typeof(BloodSnake), "Cobras de Sangue" },
			{ typeof(Toraxen), "Toraxens" },
			{ typeof(Antelope), "Antílopes" },
			{ typeof(GiantCrab), "Caranguejos Gigantes" },
			{ typeof(GiantHawk), "Falcões Gigantes" },
			{ typeof(GiantRaven), "Corvos Gigantes" },
			{ typeof(GiantSnake), "Cobras Gigantes" },
			{ typeof(GiantSpider), "Aranhas Gigantes" },
			{ typeof(HippogriffRiding), "Hipogrifos" },
			{ typeof(SabreclawCub), "Filhotes de Garras de Sabre" },
			{ typeof(SabretoothCub), "Filhotes de Dente de Sabre" },
			{ typeof(SwampGator), "Jacarés do Pântano" },
			{ typeof(FireSteed), "Corcéis de Fogo" },
			{ typeof(HugeLizard), "Lagartos Enormes" },
			{ typeof(LavaLizard), "Lagartos de Lava" },
			{ typeof(AlienSmall), "Alienígenas Pequenos" },
			{ typeof(Jaguar), "Jaguares" },
			{ typeof(Tiger), "Tigres" },
			{ typeof(WhiteTiger), "Tigres Brancos" },
			{ typeof(BullradonRiding), "Bullradons" },
			{ typeof(DeepSeaSerpent), "Serpentes do Mar Profundo" },
			{ typeof(GiantAdder), "Víbora Gigante" },
			{ typeof(GiantSerpent), "Serpentes Gigantes" },
			{ typeof(GorceratopsRiding), "Gorceratopses" },
			{ typeof(GoldenSerpent), "Serpentes Douradas" },
			{ typeof(IceSerpent), "Serpentes de Gelo" },
			{ typeof(IceSnake), "Cobras de Gelo" },
			{ typeof(JadeSerpent), "Serpentes de Jade" },
			{ typeof(JungleViper), "Víboras da Selva" },
			{ typeof(SeaSnake), "Cobras do Mar" },
			{ typeof(SilverSerpent), "Serpentes Prateadas" },
			{ typeof(Wyverns), "Wyverns" },
			{ typeof(WhiteWolf), "Lobos Brancos" },
			{ typeof(DeadlyScorpion), "Escorpiões Mortais" },
			{ typeof(Ramadon), "Ramadons" },
			{ typeof(CaveBearRiding), "Ursos das Cavernas" },
			{ typeof(DireBear), "Ursos Ferozes" },
			{ typeof(ElderBlackBearRiding), "Ursos Negros Anciões" },
			{ typeof(ElderBrownBearRiding), "Ursos Marrons Anciões" },
			{ typeof(ElderPolarBearRiding), "Ursos Polares Anciões" },
			{ typeof(GreatBear), "Ursos Grandes" },
			{ typeof(KodiakBear), "Ursos Kodiak" },
			{ typeof(SabretoothBearRiding), "Ursos Dente de Sabre" }
		};

		#endregion

		#region Tier 5: Dragons and Drakes (Rarity 91-126)

		/// <summary>
		/// Tier 5 creature types: Very rare creatures, dragons, drakes, and legendary beasts.
		/// Rarity range: 91-126 (Very Rare / Ultra Rare creatures)
		/// </summary>
		public static readonly Type[] Tier5CreatureTypes = new Type[]
		{
			// Rarity 91-100: Very Rare Creatures
			typeof(AlienSpider), // Rarity 91
			typeof(HellCat), // Rarity 91
			typeof(GorgonRiding), // Rarity 93
			typeof(BabyDragon), // Rarity 95
			typeof(GiantToad), // Rarity 97
			typeof(Hydra), // Rarity 99
			typeof(IceToad), // Rarity 99
			typeof(Wyvra), // Rarity 99
			typeof(Iguana), // Rarity 100
			typeof(MysticalFox), // Rarity 100
			typeof(RaptorRiding), // Rarity 100
			typeof(Slither), // Rarity 100
			
			// Rarity 101-110: Ultra Rare Creatures
			typeof(GiantIceWorm), // Rarity 101
			typeof(Grum), // Rarity 101
			typeof(DireWolf), // Rarity 103
			typeof(FireMephit), // Rarity 103
			typeof(Imp), // Rarity 103
			typeof(ScourgeWolf), // Rarity 103
			typeof(Teradactyl), // Rarity 103
			typeof(Tyranasaur), // Rarity 103
			typeof(AbysmalDrake), // Rarity 104
			typeof(Dragons), // Rarity 104
			typeof(Drake), // Rarity 104
			typeof(FrostSpider), // Rarity 104
			typeof(HellBeast), // Rarity 104
			typeof(Meglasaur), // Rarity 104
			typeof(SeaDrake), // Rarity 104
			typeof(SplendidDrake), // Rarity 104
			typeof(Stegosaurus), // Rarity 104
			typeof(Styguana), // Rarity 104
			typeof(SwampDrakeRiding), // Rarity 104
			typeof(Tarantula), // Rarity 104
			typeof(Watcher), // Rarity 104
			typeof(FrenziedOstard), // Rarity 105
			typeof(HellHound), // Rarity 105
			typeof(Tuskadon), // Rarity 105
			typeof(WinterWolf), // Rarity 105
			typeof(GaiaEnt), // Rarity 108
			typeof(GaiaTree), // Rarity 108
			typeof(WoodlandChurl), // Rarity 108
			typeof(PredatorHellCat), // Rarity 109
			typeof(CragCat), // Rarity 110
			typeof(RavenousRiding), // Rarity 110
			typeof(SabretoothTiger), // Rarity 110
			
			// Rarity 111-120: Legendary Creatures
			typeof(Alien), // Rarity 111
			typeof(WhitePanther), // Rarity 111
			typeof(FireBeetle), // Rarity 113
			typeof(GemDragon), // Rarity 113
			typeof(Iguanodon), // Rarity 113
			typeof(RuneBeetle), // Rarity 113
			typeof(SeaDragon), // Rarity 113
			typeof(SwampDragon), // Rarity 113
			typeof(AncientDrake), // Rarity 114
			typeof(AncientWyvern), // Rarity 113
			typeof(Manticore), // Rarity 114
			typeof(DemonDog), // Rarity 115
			typeof(Dreadhorn), // Rarity 115
			typeof(Kirin), // Rarity 115
			typeof(Nightmare), // Rarity 115
			typeof(Phoenix), // Rarity 115
			typeof(Unicorn), // Rarity 115
			typeof(Wyrms), // Rarity 116
			typeof(Roc), // Rarity 118
			typeof(YoungRoc), // Rarity 118
			typeof(DragonTurtle), // Rarity 119
			typeof(PrimevalAbysmalDragon), // Rarity 119
			typeof(PrimevalAmberDragon), // Rarity 119
			typeof(PrimevalBlackDragon), // Rarity 119
			typeof(PrimevalDragon), // Rarity 119
			typeof(PrimevalFireDragon), // Rarity 119
			typeof(PrimevalGreenDragon), // Rarity 119
			typeof(PrimevalNightDragon), // Rarity 119
			typeof(PrimevalRedDragon), // Rarity 119
			typeof(PrimevalRoyalDragon), // Rarity 119
			typeof(PrimevalRunicDragon), // Rarity 119
			typeof(PrimevalSeaDragon), // Rarity 119
			typeof(PrimevalSilverDragon), // Rarity 119
			typeof(PrimevalSplendidDragon), // Rarity 119
			typeof(PrimevalStygianDragon), // Rarity 119
			typeof(PrimevalVolcanicDragon), // Rarity 119
			typeof(ReanimatedDragon), // Rarity 119
			typeof(SplendidDragon), // Rarity 119
			typeof(Reptalon), // Rarity 121
			typeof(CuSidhe), // Rarity 121
			typeof(AncientNightmareRiding), // Rarity 121
			typeof(SilverSteed), // Rarity 123
			typeof(Cerberus), // Rarity 125
			typeof(DarkUnicornRiding), // Rarity 125
			typeof(IceSteed), // Rarity 126
			typeof(HellSteed) // Rarity 126
		};

		/// <summary>
		/// PT-BR names for Tier 5 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier5CreatureNames = new Dictionary<Type, string>
		{
			{ typeof(AlienSpider), "Aranhas Alienígenas" },
			{ typeof(HellCat), "Gatos do Inferno" },
			{ typeof(GorgonRiding), "Górgonas" },
			{ typeof(BabyDragon), "Bebês Dragão" },
			{ typeof(GiantToad), "Sapos Gigantes" },
			{ typeof(Hydra), "Hidras" },
			{ typeof(IceToad), "Sapos de Gelo" },
			{ typeof(Wyvra), "Wyvras" },
			{ typeof(Iguana), "Iguanas" },
			{ typeof(MysticalFox), "Raposas Místicas" },
			{ typeof(RaptorRiding), "Raptores" },
			{ typeof(Slither), "Deslizantes" },
			{ typeof(GiantIceWorm), "Vermes de Gelo Gigantes" },
			{ typeof(Grum), "Grums" },
			{ typeof(DireWolf), "Lobos Ferozes" },
			{ typeof(FireMephit), "Mephits de Fogo" },
			{ typeof(Imp), "Imps" },
			{ typeof(ScourgeWolf), "Lobos Flagelantes" },
			{ typeof(Teradactyl), "Terodáctilos" },
			{ typeof(Tyranasaur), "Tiranossauros" },
			{ typeof(AbysmalDrake), "Dracos Abismais" },
			{ typeof(Dragons), "Dragões" },
			{ typeof(Drake), "Dracos" },
			{ typeof(FrostSpider), "Aranhas de Gelo" },
			{ typeof(HellBeast), "Feras do Inferno" },
			{ typeof(Meglasaur), "Meglassaurs" },
			{ typeof(SeaDrake), "Dracos do Mar" },
			{ typeof(SplendidDrake), "Dracos Esplêndidos" },
			{ typeof(Stegosaurus), "Estegossauros" },
			{ typeof(Styguana), "Styguanas" },
			{ typeof(SwampDrakeRiding), "Dracos do Pântano" },
			{ typeof(Tarantula), "Tarântulas" },
			{ typeof(Watcher), "Observadores" },
			{ typeof(FrenziedOstard), "Ostards Frenéticos" },
			{ typeof(HellHound), "Cães do Inferno" },
			{ typeof(Tuskadon), "Tuskadons" },
			{ typeof(WinterWolf), "Lobos do Inverno" },
			{ typeof(GaiaEnt), "Ents de Gaia" },
			{ typeof(GaiaTree), "Árvores de Gaia" },
			{ typeof(WoodlandChurl), "Rústicos do Bosque" },
			{ typeof(PredatorHellCat), "Gatos do Inferno Predadores" },
			{ typeof(CragCat), "Gatos do Penhasco" },
			{ typeof(RavenousRiding), "Vorazes" },
			{ typeof(SabretoothTiger), "Tigres Dente de Sabre" },
			{ typeof(Alien), "Alienígenas" },
			{ typeof(WhitePanther), "Panteras Brancas" },
			{ typeof(FireBeetle), "Besouros de Fogo" },
			{ typeof(GemDragon), "Dragyns" },
			{ typeof(Iguanodon), "Iguanodontes" },
			{ typeof(RuneBeetle), "Besouros Rúnicos" },
			{ typeof(SeaDragon), "Dragões do Mar" },
			{ typeof(SwampDragon), "Dragões do Pântano" },
			{ typeof(AncientDrake), "Dracos Ancestrais" },
			{ typeof(AncientWyvern), "Wyverns Ancestrais" },
			{ typeof(Manticore), "Manticoras" },
			{ typeof(DemonDog), "Cães Demônios" },
			{ typeof(Dreadhorn), "Dreadhorns" },
			{ typeof(Kirin), "Kirins" },
			{ typeof(Nightmare), "Pesadelos" },
			{ typeof(Phoenix), "Fênixes" },
			{ typeof(Unicorn), "Unicórnios" },
			{ typeof(Wyrms), "Vermes" },
			{ typeof(DragonTurtle), "Tartarugas Dragão" },
			{ typeof(PrimevalAbysmalDragon), "Dragões Abismais Primevos" },
			{ typeof(PrimevalAmberDragon), "Dragões Âmbar Primevos" },
			{ typeof(PrimevalBlackDragon), "Dragões Negros Primevos" },
			{ typeof(PrimevalDragon), "Dragões Primevos" },
			{ typeof(PrimevalFireDragon), "Dragões de Fogo Primevos" },
			{ typeof(PrimevalGreenDragon), "Dragões Verdes Primevos" },
			{ typeof(PrimevalNightDragon), "Dragões da Noite Primevos" },
			{ typeof(PrimevalRedDragon), "Dragões Vermelhos Primevos" },
			{ typeof(PrimevalRoyalDragon), "Dragões Reais Primevos" },
			{ typeof(PrimevalRunicDragon), "Dragões Rúnicos Primevos" },
			{ typeof(PrimevalSeaDragon), "Dragões do Mar Primevos" },
			{ typeof(PrimevalSilverDragon), "Dragões Prateados Primevos" },
			{ typeof(PrimevalSplendidDragon), "Dragões Esplêndidos Primevos" },
			{ typeof(PrimevalStygianDragon), "Dragões Estigianos Primevos" },
			{ typeof(PrimevalVolcanicDragon), "Dragões Vulcânicos Primevos" },
			{ typeof(ReanimatedDragon), "Dragões Reanimados" },
			{ typeof(SplendidDragon), "Dragões Esplêndidos" },
			{ typeof(Roc), "Rocs" },
			{ typeof(YoungRoc), "Rocs Jovens" },
			{ typeof(Reptalon), "Reptalons" },
			{ typeof(CuSidhe), "Cu Sidhes" },
			{ typeof(AncientNightmareRiding), "Pesadelos Ancestrais" },
			{ typeof(DarkUnicornRiding), "Unicórnios Sombrios" },
			{ typeof(SilverSteed), "Corcéis Prateados" },
			{ typeof(IceSteed), "Corcéis de Gelo" },
			{ typeof(HellSteed), "Corcéis do Inferno" },
			{ typeof(Cerberus), "Cérberos" }
		};

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the creature type name in PT-BR for a given type and tier.
		/// </summary>
		/// <param name="creatureType">The creature type</param>
		/// <param name="tier">The contract tier</param>
		/// <returns>The PT-BR name, or null if not found</returns>
		public static string GetCreatureTypeName(Type creatureType, int tier)
		{
			if (creatureType == null)
				return null;

			switch (tier)
			{
				case 2:
					if (Tier2CreatureNames.ContainsKey(creatureType))
						return Tier2CreatureNames[creatureType];
					break;
				case 3:
					if (Tier3CreatureNames.ContainsKey(creatureType))
						return Tier3CreatureNames[creatureType];
					break;
				case 4:
					if (Tier4CreatureNames.ContainsKey(creatureType))
						return Tier4CreatureNames[creatureType];
					break;
				case 5:
					if (Tier5CreatureNames.ContainsKey(creatureType))
						return Tier5CreatureNames[creatureType];
					break;
			}

			return null;
		}

		/// <summary>
		/// Gets a random creature type for a given tier.
		/// </summary>
		/// <param name="tier">The contract tier (2-5)</param>
		/// <returns>A random creature type, or null for tier 1 (generic)</returns>
		public static Type GetRandomCreatureType(int tier)
		{
			switch (tier)
			{
				case 2:
					if (Tier2CreatureTypes.Length > 0)
						return Tier2CreatureTypes[Utility.Random(Tier2CreatureTypes.Length)];
					break;
				case 3:
					if (Tier3CreatureTypes.Length > 0)
						return Tier3CreatureTypes[Utility.Random(Tier3CreatureTypes.Length)];
					break;
				case 4:
					if (Tier4CreatureTypes.Length > 0)
						return Tier4CreatureTypes[Utility.Random(Tier4CreatureTypes.Length)];
					break;
				case 5:
					if (Tier5CreatureTypes.Length > 0)
						return Tier5CreatureTypes[Utility.Random(Tier5CreatureTypes.Length)];
					break;
			}

			return null; // Tier 1 is generic
		}

		/// <summary>
		/// Checks if a creature type is valid for a given tier.
		/// </summary>
		/// <param name="creatureType">The creature type to check</param>
		/// <param name="tier">The contract tier</param>
		/// <returns>True if valid, false otherwise</returns>
		public static bool IsValidCreatureType(Type creatureType, int tier)
		{
			if (creatureType == null)
				return tier == 1; // Generic contracts allow any type

			switch (tier)
			{
				case 1:
					return true; // Generic accepts any
				case 2:
					return Array.IndexOf(Tier2CreatureTypes, creatureType) >= 0;
				case 3:
					return Array.IndexOf(Tier3CreatureTypes, creatureType) >= 0;
				case 4:
					return Array.IndexOf(Tier4CreatureTypes, creatureType) >= 0;
				case 5:
					return Array.IndexOf(Tier5CreatureTypes, creatureType) >= 0;
				default:
					return false;
			}
		}

		/// <summary>
		/// Gets the tier for a creature type based on its rarity value (from MonsterContractType).
		/// Used for mapping old MonsterContract creatures to new tier system.
		/// </summary>
		/// <param name="rarity">The rarity value (0-126)</param>
		/// <returns>The tier (2-5) based on rarity range</returns>
		public static int GetTierFromRarity(int rarity)
		{
			if (rarity <= 30)
				return 2; // Tier 2: Common (Rarity 0-30)
			else if (rarity <= 60)
				return 3; // Tier 3: Uncommon (Rarity 31-60)
			else if (rarity <= 90)
				return 4; // Tier 4: Rare (Rarity 61-90)
			else
				return 5; // Tier 5: Very Rare (Rarity 91-126)
		}

		#endregion
	}
}
