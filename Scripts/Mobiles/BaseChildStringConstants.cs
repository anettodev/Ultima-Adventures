namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for BaseChild NPC messages and labels.
	/// Extracted from BaseChild.cs to improve maintainability and enable localization.
	/// </summary>
	public static class BaseChildStringConstants
	{
		#region Player Sighting Messages (Portuguese)

		/// <summary>Message when child sees a knight</summary>
		public const string MSG_KNIGHT = "Uau! Um cavaleiro!";

		/// <summary>Message about wanting to be like someone</summary>
		public const string MSG_WANNA_BE_LIKE = "Quero ser como ele um dia.";

		/// <summary>Message format when recognizing a player</summary>
		public const string MSG_ITS_PLAYER_FORMAT = "É {0}!";

		/// <summary>Message about strange clothes</summary>
		public const string MSG_STRANGE_CLOTHES = "Você usa roupas estranhas.";

		/// <summary>Message about seeing an adventurer</summary>
		public const string MSG_ADVENTURER = "Um aventureiro! Um aventureiro!";

		/// <summary>Message format about daddy saying player is a wimp</summary>
		public const string MSG_DADDY_WIMP_FORMAT = "Papai diz que você é um fraco, {0}.";

		/// <summary>Message format about wanting to be like player when grown</summary>
		public const string MSG_GROW_UP_LIKE_FORMAT = "Quando eu crescer, vou ser igual a {0}.";

		/// <summary>Message format about player being shorter than imagined</summary>
		public const string MSG_SHORTER_FORMAT = "{0} é mais baixo do que eu imaginava...";

		/// <summary>Message about wanting armor</summary>
		public const string MSG_WANT_ARMOR = "Quero uma armadura assim!";

		#endregion

		#region Detection Messages (Portuguese)

		/// <summary>Message when child finds hidden player</summary>
		public const string MSG_FOUND_YOU = "Encontrei você!";

		#endregion

		#region Escape Messages (Portuguese)

		/// <summary>Message when orphan tries to escape</summary>
		public const string MSG_MUST_ESCAPE = "Não... Devo tentar escapar!";

		#endregion

		#region Begging Messages (Portuguese)

		/// <summary>Message begging for food</summary>
		public const string MSG_BEG_FOOD = "Por favor! Por favor, me dê alguma comida!";

		/// <summary>Message asking for coins</summary>
		public const string MSG_BEG_COINS = "Algumas moedas, Senhor?";

		/// <summary>Message format begging for food with player name</summary>
		public const string MSG_BEG_HUNGRY_FORMAT = "Por favor {0}, estou com fome!";

		/// <summary>Message format about coins helping family</summary>
		public const string MSG_BEG_FAMILY_FORMAT = "algumas moedas vão ajudar muito a mim e minha família, {0}";

		/// <summary>Message about not eating yesterday</summary>
		public const string MSG_BEG_NO_EAT = "Eu não comi ontem....";

		/// <summary>Message format begging for coins with player name</summary>
		public const string MSG_BEG_COINS_FORMAT = "Por favor {0}, algumas moedas.";

		/// <summary>Message format about old shoes</summary>
		public const string MSG_BEG_SHOES_FORMAT = "Meus sapatos estão velhos e cheios de buracos, {0}.";

		/// <summary>Message format about having nothing</summary>
		public const string MSG_BEG_NOTHING_FORMAT = "Não tenho nada, {0}.";

		/// <summary>Message asking for coins</summary>
		public const string MSG_BEG_GIVE_COINS = "Me dê algumas moedas, por favor.";

		/// <summary>Message asking why player looks funny</summary>
		public const string MSG_BEG_FUNNY = "Por que você parece tão engraçado?";

		/// <summary>Message about sick mother needing medicine</summary>
		public const string MSG_BEG_MOTHER_SICK = "Minha mãe está doente e precisa de remédio, me ajude...";

		/// <summary>Message about hearing coins in backpack</summary>
		public const string MSG_BEG_HEAR_COINS = "Ouço moedas naquela mochila grande sua.";

		#endregion

		#region Stealing Messages (Portuguese)

		/// <summary>Message when child finds player boring</summary>
		public const string MSG_STEAL_BORING = "Você é chato.";

		/// <summary>Message saying goodbye</summary>
		public const string MSG_STEAL_BYE = "Tchau!";

		/// <summary>Message wishing good day</summary>
		public const string MSG_STEAL_GOOD_DAY = "Tenha um ótimo dia, Senhor!";

		/// <summary>Message about thinking player was parent</summary>
		public const string MSG_STEAL_THOUGHT_PARENT = "Ah, pensei que você fosse meu pai ";

		/// <summary>Message telling player to look over there</summary>
		public const string MSG_STEAL_LOOK_THERE = "Ah, olhe ali!";

		/// <summary>Message saying player is no fun</summary>
		public const string MSG_STEAL_NO_FUN = "Você não é divertido.";

		#endregion

		#region Thank You Messages (Portuguese)

		/// <summary>Message thanking for candy and giving something back</summary>
		public const string MSG_THANK_CANDY_GIFT = "Obrigado, Senhor! Aqui está algo que encontrei!";

		/// <summary>Message thanking for candy</summary>
		public const string MSG_THANK_CANDY = "Obrigado, Senhor!";

		/// <summary>Message thanking for donation</summary>
		public const string MSG_THANK_SIRE = "Obrigado, Senhor!";

		#endregion

		#region Gift Messages (Portuguese)

		/// <summary>Message when giving found item</summary>
		public const string MSG_GIFT_FOUND = "Aqui, encontrei isso - você pode ficar com ele!";

		/// <summary>Message when child slips something in pack</summary>
		public const string MSG_GIFT_SLIPPED = "A criança ágilmente desliza algo em sua mochila.";

		/// <summary>Message when child has nothing to give</summary>
		public const string MSG_GIFT_NOTHING = "Não tenho nada para te dar, porém.";

		#endregion

		#region Request Messages (Portuguese)

		/// <summary>Message format when wanting more gold</summary>
		public const string MSG_REQUEST_MORE_GOLD_FORMAT = "Ah, olhe, {0} de Ouro! Quero mais!";

		/// <summary>Message about still being hungry</summary>
		public const string MSG_REQUEST_STILL_HUNGRY = "Ainda estou com fome!";

		/// <summary>Message format asking for something else</summary>
		public const string MSG_REQUEST_SOMETHING_ELSE_FORMAT = "Ah, {0}, posso ter algo mais?";

		#endregion

		#region Thief Interaction Messages (Portuguese)

		/// <summary>Message when player grabs thief and searches</summary>
		public const string MSG_THIEF_SEARCH = "Você agarra o ladrão e revira seus pertences!";

		#endregion

		#region Molestation Messages (Portuguese)

		/// <summary>Message about person trying to molest</summary>
		public const string MSG_MOLEST_TRYING = "Socorro! Esta pessoa está tentando me molestear!";

		/// <summary>Message about person touching private parts</summary>
		public const string MSG_MOLEST_TOUCHED = "Esta pessoa tentou tocar minhas partes íntimas!";

		/// <summary>Message screaming</summary>
		public const string MSG_MOLEST_SCREAM = "AAAAAAAAAAAH!";

		/// <summary>Message calling for help</summary>
		public const string MSG_MOLEST_HELP = "Socorro! Socorro!";

		/// <summary>Message about stranger touching</summary>
		public const string MSG_MOLEST_STRANGER = "Um estranho me tocou!!";

		/// <summary>Message exclamation</summary>
		public const string MSG_MOLEST_AAH = "aah!";

		#endregion

		#region Annoying Messages (Portuguese)

		/// <summary>Message asking why wearing something</summary>
		public const string MSG_ANNOY_WHY_WEARING = "Por que você está usando isso?";

		/// <summary>Message asking what something does</summary>
		public const string MSG_ANNOY_WHAT_DOES = "O que isso faz?";

		/// <summary>Message format about player dying a lot</summary>
		public const string MSG_ANNOY_DIE_LOT_FORMAT = "Aposto que você morre muito, {0}.";

		/// <summary>Message format asking why player did something</summary>
		public const string MSG_ANNOY_WHY_DID_FORMAT = "Por que você fez isso, {0}??";

		/// <summary>Message about funny ugly hat</summary>
		public const string MSG_ANNOY_FUNNY_HAT = "Você tem um chapéu engraçado! É feio!";

		/// <summary>Message format about father being stronger</summary>
		public const string MSG_ANNOY_FATHER_STRONGER_FORMAT = "Meu pai poderia te enfrentar, {0}. Ele é mais forte!";

		/// <summary>Message format about stinking like old cow</summary>
		public const string MSG_ANNOY_STINK_COW_FORMAT = "Você fede como uma vaca velha, {0}.";

		/// <summary>Message format about not being strong</summary>
		public const string MSG_ANNOY_NOT_STRONG_FORMAT = "você não é tão forte, {0}.";

		/// <summary>Message saying player stinks</summary>
		public const string MSG_ANNOY_STINK = "Você fede!";

		/// <summary>Message saying player is ugly</summary>
		public const string MSG_ANNOY_UGLY = "Você é feio!";

		/// <summary>Message about not being able to kill mongbat</summary>
		public const string MSG_ANNOY_CANT_KILL = "Aposto que você não consegue matar um mongbat!";

		/// <summary>Message asking if father was mongbat</summary>
		public const string MSG_ANNOY_FATHER_MONGBAT = "Seu pai era um Mongbat?";

		/// <summary>Message about imp being better looking</summary>
		public const string MSG_ANNOY_IMP_BETTER = "Um Imp é mais bonito que você - eu já vi um.";

		/// <summary>Message asking why not talking</summary>
		public const string MSG_ANNOY_WHY_NOT_TALKING = "Por que você não está falando comigo?";

		/// <summary>Message demanding</summary>
		public const string MSG_ANNOY_GIMME = "ME DÊ ME DÊ ME DÊ";

		/// <summary>Message about parents being monsters</summary>
		public const string MSG_ANNOY_PARENTS_MONSTERS = "Sua mãe era uma Criatura do Pântano e seu pai era uma gosma!";

		/// <summary>Message wanting something</summary>
		public const string MSG_ANNOY_WANT_SOMETHING = "Eu quero algo!";

		/// <summary>Message asking what player is wearing</summary>
		public const string MSG_ANNOY_WHAT_WEARING = "O que é isso que você está usando aí?";

		#endregion

		#region Speech Response Messages (Portuguese)

		/// <summary>Message format repeating follow</summary>
		public const string MSG_SPEECH_FOLLOW = "Me siga me me me me me me me me me!";

		/// <summary>Message demanding to buy something</summary>
		public const string MSG_SPEECH_BUY = "Compre algo para mim!! Compre algo para mim!! Compre algo para mim!! Compre algo para mim!! Compre algo para mim!!";

		/// <summary>Message demanding now</summary>
		public const string MSG_SPEECH_NOW = "AGOOOORA!!";

		/// <summary>Message about selling</summary>
		public const string MSG_SPEECH_SELL = "VENDER! VENDER! VENDER! UAAAAH NÃO VENDA!";

		/// <summary>Message about not wanting to stay</summary>
		public const string MSG_SPEECH_NO_STAY = "NÃO! NÃO QUERO FICAR AQUI! NÃO NÃO NÃO NÃO NÃO!";

		/// <summary>Message about following forever</summary>
		public const string MSG_SPEECH_FOLLOW_FOREVER = "NÃO! Vou te seguir para sempre!";

		/// <summary>Message responding to profanity</summary>
		public const string MSG_SPEECH_FUCK_YOU = "Não! Vai se foder VOCÊ!";

		/// <summary>Message format asking why something</summary>
		public const string MSG_SPEECH_WHY_FORMAT = "por que {0}?";

		/// <summary>Message format repeating speech</summary>
		public const string MSG_SPEECH_BLAH = " Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah ";

		/// <summary>Message format saying not something</summary>
		public const string MSG_SPEECH_NOT_FORMAT = " Não {0} !";

		/// <summary>Message format with high pitch voice</summary>
		public const string MSG_SPEECH_HIGH_PITCH_FORMAT = " *voz aguda* {0} !";

		/// <summary>Message format repeating speech multiple times (6 times)</summary>
		public const string MSG_SPEECH_REPEAT_FORMAT = "{0}{0}{0}{0}{0}{0}";

		/// <summary>Message format about telling guard</summary>
		public const string MSG_SPEECH_TELL_GUARD_FORMAT = "Vou contar a um guarda que você disse {0} !";

		#endregion
	}
}

