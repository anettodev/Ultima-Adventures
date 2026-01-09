using System;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Centralized string constants for Spirit Speak skill messages.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// </summary>
	public static class SpiritSpeakStringConstants
	{
		#region Spell Info

		/// <summary>Name of the Spirit Speak spell</summary>
		public const string SPELL_NAME = "Falar com Espíritos";

		#endregion

		#region Success Messages

		/// <summary>Message when offering corpse to evil forces</summary>
		public const string MSG_OFFER_TO_EVIL = "Você oferece os mortos às forças do mal.";

		/// <summary>Message when bones are destroyed</summary>
		public const string MSG_BONES_COMBUST = "Os ossos explodem em uma magnífica rajada de chamas!";

		/// <summary>Message when channeling spiritual energy</summary>
		public const string MSG_CHANNEL_ENERGY = "Você canaliza sua energia espiritual para se restaurar.";

		#endregion

		#region Failure Messages

		/// <summary>Message when caster is poisoned</summary>
		public const string MSG_POISONED = "Você não pode fazer isso enquanto há veneno em suas veias!";

		/// <summary>Message when caster is starving</summary>
		public const string MSG_STARVING = "Você está morrendo de fome e não pode fazer isso!";

		/// <summary>Message when caster is dying of thirst</summary>
		public const string MSG_THIRSTY = "Você está morrendo de sede e não pode fazer isso!";

		#endregion

		#region Ghost Hearing Messages

		/// <summary>Message format when successfully hearing ghosts (includes duration)</summary>
		public const string MSG_CAN_HEAR_GHOSTS = "Você pode falar a lingua dos mortos por {0} segundos";

		#endregion
	}
}
