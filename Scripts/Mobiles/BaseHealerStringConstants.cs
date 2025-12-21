namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for BaseHealer NPC messages and labels.
	/// Extracted from BaseHealer.cs to improve maintainability and enable localization.
	/// </summary>
	public static class BaseHealerStringConstants
	{
		#region Resurrection Messages (Portuguese)

		/// <summary>Message when EvilHealer refuses to resurrect good karma players</summary>
		public const string MSG_EVIL_HEALER_REFUSE = "Vá para outro lugar, eu não ressuscito pessoas como você.";

		/// <summary>Message when offering to resurrect henchman</summary>
		public const string MSG_OFFER_HENCHMAN_RESURRECTION = "Um de seus lacaios sofreu um destino mortal? Posso ressuscitá-los para você.";

		#endregion

		#region Henchman Resurrection Messages (Portuguese)

		/// <summary>Message when henchman is not dead</summary>
		public const string MSG_HENCHMAN_NOT_DEAD = "Seu amigo não está morto.";

		/// <summary>Message format when player pays for resurrection</summary>
		public const string MSG_PAY_GOLD_FORMAT = "Você paga {0} de ouro.";

		/// <summary>Message when henchman is successfully resurrected</summary>
		public const string MSG_HENCHMAN_RESURRECTED = "Seu lacaio voltou à terra dos vivos.";

		/// <summary>Message format when telling player the resurrection cost</summary>
		public const string MSG_RESURRECTION_COST_FORMAT = "Custaria {0} de ouro para ressuscitá-los.";

		/// <summary>Message when player doesn't have enough gold</summary>
		public const string MSG_NOT_ENOUGH_GOLD = "Você não tem ouro suficiente.";

		/// <summary>Message when target doesn't need healer services</summary>
		public const string MSG_DOES_NOT_NEED_SERVICES = "Isso não precisa dos meus serviços.";

		#endregion

		#region Henchman Names (Portuguese)

		/// <summary>Name for fighter henchman item</summary>
		public const string HENCHMAN_NAME_FIGHTER = "lacaio lutador";

		/// <summary>Name for wizard henchman item</summary>
		public const string HENCHMAN_NAME_WIZARD = "lacaio mago";

		/// <summary>Name for archer henchman item</summary>
		public const string HENCHMAN_NAME_ARCHER = "lacaio arqueiro";

		/// <summary>Name for creature henchman item</summary>
		public const string HENCHMAN_NAME_CREATURE = "lacaio criatura";

		#endregion
	}
}

