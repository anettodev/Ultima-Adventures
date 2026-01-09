using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Masonry crafting system.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Replaces hardcoded English strings with meaningful translations.
	/// </summary>
	public static class MasonryStringConstants
	{
		#region Category Names

		public const string CATEGORY_CONTAINERS = "Recipientes";
		public const string CATEGORY_DECORATIONS = "Decorações";
		public const string CATEGORY_FURNITURE = "Mobiliário";
		public const string CATEGORY_SMALL_STATUES = "Estátuas Pequenas";
		public const string CATEGORY_MEDIUM_STATUES = "Estátuas Médias";
		public const string CATEGORY_LARGE_STATUES = "Estátuas Grandes";
		public const string CATEGORY_HUGE_STATUES = "Estátuas Enormes";
		public const string CATEGORY_GIANT_STATUES = "Estátuas Gigantes";
		public const string CATEGORY_TOMBSTONES = "Lápides";

		#endregion

		#region Item Names - Containers

		public const string ITEM_SARCOPHAGUS_WOMAN = "sarcófago, mulher";
		public const string ITEM_SARCOPHAGUS_MAN = "sarcófago, homem";
		public const string ITEM_URN = "urna";
		public const string ITEM_VASE = "vaso";
		public const string ITEM_URN_ORNATE = "urna";
		public const string ITEM_TALL_VASE_ORNATE = "vaso";

		#endregion

		#region Item Names - Decorations

		public const string ITEM_VASE_SMALL = "vaso";
		public const string ITEM_VASE_LARGE = "vaso, grande";
		public const string ITEM_AMPHORA = "ânfora";
		public const string ITEM_AMPHORA_LARGE = "ânfora, grande";
		public const string ITEM_VASE_ORNATE = "vaso, ornamentado";
		public const string ITEM_AMPHORA_ORNATE = "ânfora, ornamentada";
		public const string ITEM_VASE_GARGOYLE = "vaso, gárgula";

		public const string ITEM_SCULPTURE_BUDDHIST = "escultura, Budista";
		public const string ITEM_SCULPTURE_MING = "escultura, Ming";
		public const string ITEM_SCULPTURE_YUAN = "escultura, Yuan";
		public const string ITEM_SCULPTURE_QIN = "escultura, Qin";

		public const string ITEM_URN_MING = "urna, Ming";
		public const string ITEM_URN_QIN = "urna, Qin";
		public const string ITEM_URN_YUAN = "urna, Yuan";

		#endregion

		#region Item Names - Furniture

		// Note: Stone Chairs uses cliloc 1024635
		public const string ITEM_BENCH_LONG = "banco, longo";
		public const string ITEM_BENCH_SHORT = "banco, curto";
		public const string ITEM_TABLE_LONG = "mesa, longa";
		public const string ITEM_TABLE_SHORT = "mesa, curta";
		public const string ITEM_TABLE_WIZARD = "mesa, mago";

		public const string ITEM_STEPS = "degraus";
		public const string ITEM_BLOCK = "bloco";
		public const string ITEM_SARCOPHAGUS = "sarcófago";

		public const string ITEM_COLUMN = "coluna";
		public const string ITEM_COLUMN_GOTHIC = "coluna, gótica";
		public const string ITEM_PEDESTAL = "pedestal";
		public const string ITEM_PEDESTAL_FANCY = "pedestal, ornamentado";
		public const string ITEM_PILLAR = "pilar";

		#endregion

		#region Item Names - Small Statues

		public const string ITEM_STATUE_ANGEL = "estátua de anjo";
		public const string ITEM_STATUE_DRAGON = "estátua de dragão";
		public const string ITEM_STATUE_GARGOYLE_BUST = "busto de gárgula";
		public const string ITEM_STATUE_GARGOYLE = "estátua de gárgula";
		public const string ITEM_STATUE_MAN_BUST = "busto de homem";
		public const string ITEM_STATUE_MAN = "estátua de homem";
		public const string ITEM_STATUE_NOBLE = "estátua de nobre";
		public const string ITEM_STATUE_PEGASUS = "estátua de pégaso";
		public const string ITEM_STATUE_SKULL_IDOL = "ídolo de caveira";
		public const string ITEM_STATUE_WOMAN = "estátua de mulher";

		#endregion

		#region Item Names - Medium Statues

		public const string ITEM_STATUE_ADVENTURER = "estátua de aventureiro";
		public const string ITEM_STATUE_AMAZON = "estátua de amazona";
		public const string ITEM_STATUE_DEMONIC_FACE = "face demoníaca";
		public const string ITEM_STATUE_DRUID = "estátua de druida";
		public const string ITEM_STATUE_ELF_KNIGHT = "estátua de cavaleiro elfo";
		public const string ITEM_STATUE_ELF_PRIESTESS = "estátua de sacerdotisa elfa";
		public const string ITEM_STATUE_ELF_SORCERESS = "estátua de feiticeira elfa";
		public const string ITEM_STATUE_ELF_WARRIOR = "estátua de guerreiro elfo";
		public const string ITEM_STATUE_FIGHTER = "estátua de lutador";
		public const string ITEM_STATUE_GARGOYLE_TALL = "estátua de gárgula";
		public const string ITEM_STATUE_GARGOYLE_FLIGHT = "estátua de gárgula";
		public const string ITEM_STATUE_GRYPHON = "estátua de grifo";
		public const string ITEM_STATUE_LION = "estátua de leão";
		public const string ITEM_STATUE_MEDUSA = "estátua de medusa";
		public const string ITEM_STATUE_MERMAID = "estátua de sereia";
		public const string ITEM_STATUE_NOBLE_MEDIUM = "estátua de nobre";
		public const string ITEM_STATUE_PRIEST = "estátua de sacerdote";
		public const string ITEM_STATUE_SEA_HORSE = "estátua de cavalo-marinho";
		public const string ITEM_STATUE_SPHINX = "estátua de esfinge";
		public const string ITEM_STATUE_SWORDSMAN = "estátua de espadachim";
		public const string ITEM_STATUE_WOLF_WINGED = "estátua de lobo alado";
		public const string ITEM_STATUE_WIZARD = "estátua de mago";

		#endregion

		#region Item Names - Large Statues

		public const string ITEM_STATUE_DWARF = "estátua de anão";
		public const string ITEM_STATUE_GOD = "estátua de deus";
		public const string ITEM_STATUE_HORSE_RIDER = "cavaleiro a cavalo";
		public const string ITEM_STATUE_LION_MEDIUM = "estátua de leão";
		public const string ITEM_STATUE_MINOTAUR_DEFEND = "estátua de minotauro, defesa";
		public const string ITEM_STATUE_MINOTAUR_ATTACK = "estátua de minotauro, ataque";
		public const string ITEM_STATUE_PEGASUS_LARGE = "estátua de pégaso";
		public const string ITEM_STATUE_WOMAN_WARRIOR_PILLAR = "estátua de mulher guerreira";

		#endregion

		#region Item Names - Huge Statues

		public const string ITEM_STATUE_ANGEL_TALL = "estátua de anjo";
		public const string ITEM_STATUE_DAEMON_TALL = "estátua de demônio, alta";
		public const string ITEM_STATUE_LION_LARGE = "estátua de leão";
		public const string ITEM_STATUE_LION_TALL = "estátua de leão, alta";
		public const string ITEM_STATUE_WARRIOR_CAPE = "estátua de guerreiro";
		public const string ITEM_STATUE_WISE_MAN_TALL = "estátua de sábio";
		public const string ITEM_STATUE_WOLF_LARGE = "estátua de lobo";
		public const string ITEM_STATUE_WOMAN_TALL = "estátua de mulher";

		#endregion

		#region Item Names - Giant Statues

		public const string ITEM_STATUE_GATE_GUARDIAN = "estátua de guardião do portão";
		public const string ITEM_STATUE_GUARDIAN = "estátua de guardião";
		public const string ITEM_STATUE_WARRIOR_GIANT = "estátua de guerreiro";

		#endregion

		#region Item Names - Tombstones

		public const string ITEM_TOMBSTONE = "lápide";

		#endregion
	}
}
