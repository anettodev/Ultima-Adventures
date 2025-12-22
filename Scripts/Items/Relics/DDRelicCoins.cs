using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Coin relic item that can be flipped between two ItemID states.
	/// Features complex naming with age and symbol descriptions.
	/// </summary>
	public class DDRelicCoins : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0xE1A;
		private const int RANDOM_COIN_TYPE_MIN = 0;
		private const int RANDOM_COIN_TYPE_MAX = 1;
		private const int RANDOM_AGE_MIN = 0;
		private const int RANDOM_AGE_MAX = 13;
		private const int RANDOM_SYMBOL_MIN = 0;
		private const int RANDOM_SYMBOL_MAX = 33;

		#endregion

		#region Fields

		/// <summary>First ItemID for flipping</summary>
		public int RelicFlipID1;

		/// <summary>Second ItemID for flipping</summary>
		public int RelicFlipID2;

		/// <summary>
		/// Structure for coin variant data
		/// </summary>
		private struct CoinVariant
		{
			public int ItemID;
			public int FlipID1;
			public int FlipID2;

			public CoinVariant(int itemID, int flipID1, int flipID2)
			{
				this.ItemID = itemID;
				this.FlipID1 = flipID1;
				this.FlipID2 = flipID2;
			}
		}

		/// <summary>
		/// Array of coin variants
		/// </summary>
		private static readonly CoinVariant[] CoinVariants = new CoinVariant[]
		{
			new CoinVariant(0xE1A, 0xE1A, 0xFA4),
			new CoinVariant(0xE1B, 0xE1B, 0xFA5)
		};

		/// <summary>Age descriptions for coins</summary>
		private static readonly string[] AGE_DESCRIPTIONS = new[]
		{
			"de uma civilização há muito morta",
			"de uma raça antiga",
			"de uma ordem secreta",
			"de uma terra distante",
			"de origem desconhecida",
			"de muito tempo atrás",
			"de uma cidade perdida",
			"de uma terra misteriosa",
			"dos tempos sombrios",
			"de uma raça antiga",
			"de uma raça perdida",
			"de uma terra desaparecida",
			"de uma era desconhecida",
			"usadas séculos atrás"
		};

		/// <summary>Symbol descriptions for coins</summary>
		private static readonly string[] SYMBOL_DESCRIPTIONS = new[]
		{
			"torre", "grifo", "coroa", "espada", "machado", "leão", "urso", "morcego", "javali", "búfalo",
			"quimera", "cobra", "demônio", "diabo", "anjo", "dragão", "cão", "águia", "falcão", "hipogrifo",
			"cavalo", "lobo", "pégaso", "carneiro", "caveira", "aranha", "unicórnio", "escorpião", "mão", "punho",
			"olho", "cruz", "mulher", "homem"
		};

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the first flip ItemID
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID1
		{
			get { return RelicFlipID1; }
			set { RelicFlipID1 = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets or sets the second flip ItemID
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID2
		{
			get { return RelicFlipID2; }
			set { RelicFlipID2 = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new coin relic with random type, age, and symbol
		/// </summary>
		[Constructable]
		public DDRelicCoins() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_LIGHT;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int coinType = Utility.RandomMinMax(RANDOM_COIN_TYPE_MIN, RANDOM_COIN_TYPE_MAX);
			CoinVariant coin = CoinVariants[coinType];

			ItemID = coin.ItemID;
			RelicFlipID1 = coin.FlipID1;
			RelicFlipID2 = coin.FlipID2;

			int ageIndex = Utility.RandomMinMax(RANDOM_AGE_MIN, RANDOM_AGE_MAX);
			string age = AGE_DESCRIPTIONS[ageIndex];

			int symbolIndex = Utility.RandomMinMax(RANDOM_SYMBOL_MIN, RANDOM_SYMBOL_MAX);
			string symbol = SYMBOL_DESCRIPTIONS[symbolIndex];

			Name = "moedas " + age + " com símbolos de " + symbol + " nelas";
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicCoins(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to flip coin or show identification message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendMessage(RelicStringConstants.MSG_IDENTIFY_VALUE);
				from.SendMessage(RelicStringConstants.MSG_MUST_BE_IN_PACK);
			}
			else
			{
				if (ItemID == RelicFlipID1)
				{
					ItemID = RelicFlipID2;
				}
				else
				{
					ItemID = RelicFlipID1;
				}
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the coin relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicFlipID1);
			writer.Write(RelicFlipID2);
		}

		/// <summary>
		/// Deserializes the coin relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicFlipID1 = reader.ReadInt();
			RelicFlipID2 = reader.ReadInt();
		}

		#endregion
	}
}
