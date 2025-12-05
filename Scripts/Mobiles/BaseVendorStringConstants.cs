namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for BaseVendor messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from BaseVendor.cs to improve maintainability and enable localization.
	/// </summary>
	public static class BaseVendorStringConstants
	{
		#region Vendor Greeting Messages (Portuguese)

		/// <summary>Greeting message 1</summary>
		public const string GREETING_1 = "Saudações";

		/// <summary>Greeting message 2</summary>
		public const string GREETING_2 = "Olá você";

		/// <summary>Greeting message 3</summary>
		public const string GREETING_3 = "Eu tenho coisas boas aqui.";

		/// <summary>Greeting message 4</summary>
		public const string GREETING_4 = "Venha olhe!";

		/// <summary>Greeting message 5</summary>
		public const string GREETING_5 = "Compre aqui!";

		/// <summary>Greeting message 6</summary>
		public const string GREETING_6 = "Opa! Em que posso te ajudar?";

		/// <summary>Greeting message 7</summary>
		public const string GREETING_7 = "Talvez eu tenha o que você procura...";

		/// <summary>Greeting message 8</summary>
		public const string GREETING_8 = "Opa!";

		/// <summary>Greeting message 9</summary>
		public const string GREETING_9 = "Os melhores preços aqui!";

		/// <summary>Greeting message 10</summary>
		public const string GREETING_10 = "sim?";

		/// <summary>Greeting message 11</summary>
		public const string GREETING_11 = "Rápido, Estou ocupado!";

		/// <summary>Greeting message 12</summary>
		public const string GREETING_12 = "Meu estoque está cheio!";

		/// <summary>Greeting message 13</summary>
		public const string GREETING_13 = "Bem-vindo(a) a minha loja";

		/// <summary>Greeting message 14</summary>
		public const string GREETING_14 = "*Hmm?*";

		/// <summary>Greeting message 15</summary>
		public const string GREETING_15 = "O tempo está bom para compras";

		/// <summary>Greeting message 16</summary>
		public const string GREETING_16 = "Bela vestimenta. Está vendendo?";

		#endregion

		#region Error Messages (Portuguese)

		/// <summary>Error: Vendor refuses to do business</summary>
		public const string ERROR_NO_BUSINESS = "Eu não faço negócios com você!";

		/// <summary>Error: Vendor not trading in Midland</summary>
		public const string ERROR_MIDLAND_NO_TRADE = "Me desculpe mas eu não estou negociando nada, milorde.";

		/// <summary>Error: Cannot trade while blessed</summary>
		public const string ERROR_BLESSED = "Eu não posso negociar com você enquanto você estiver neste estado.";

		/// <summary>Error: Cannot accept from enemy</summary>
		public const string ERROR_ENEMY = "Não acredito que possa aceitar isto de você.";

		/// <summary>Error: No items of interest</summary>
		public const string ERROR_NO_INTEREST = "Não acredito que você tenha algo do meu interesse.";

		#endregion

		#region Combat Messages (English - Game Standard)

		/// <summary>Combat message 1</summary>
		public const string COMBAT_1 = "Leave this place!";

		/// <summary>Combat message 2 format: "{0}, we have heard of you!"</summary>
		public const string COMBAT_2_FORMAT = "{0}, we have heard of you!";

		/// <summary>Combat message 3 format: "We have been told to watch for you, {0}!"</summary>
		public const string COMBAT_3_FORMAT = "We have been told to watch for you, {0}!";

		/// <summary>Combat message 4 format: "Guards, {0} is here!"</summary>
		public const string COMBAT_4_FORMAT = "Guards, {0} is here!";

		#endregion

		#region Death Messages

		/// <summary>Death cry for help</summary>
		public const string DEATH_CRY = "Help! Guards!";

		#endregion

		#region Begging Words (English - Game Standard)

		/// <summary>Begging phrase 1</summary>
		public const string BEGGING_1 = "Please give me a good price as I am so poor.";

		/// <summary>Begging phrase 2</summary>
		public const string BEGGING_2 = "I have very little gold so whatever you can give...";

		/// <summary>Begging phrase 3</summary>
		public const string BEGGING_3 = "I have not eaten in days so your gold will surely help.";

		/// <summary>Begging phrase 4</summary>
		public const string BEGGING_4 = "Will thou give a poor soul more for these?";

		/// <summary>Begging phrase 5</summary>
		public const string BEGGING_5 = "I have fallen on hard times, will thou be kind?";

		/// <summary>Begging phrase 6</summary>
		public const string BEGGING_6 = "Whatever you can give for these will surely help.";

		#endregion

		#region Relic Reward Messages (English - Game Standard)

		/// <summary>Relic reward message 1 format: "I have been looking for something like this. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_1_FORMAT = "I have been looking for something like this. Here is {0} gold for you.";

		/// <summary>Relic reward message 2 format: "I have heard of this item before. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_2_FORMAT = "I have heard of this item before. Here is {0} gold for you.";

		/// <summary>Relic reward message 3 format: "I never thought I would see one of these. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_3_FORMAT = "I never thought I would see one of these. Here is {0} gold for you.";

		/// <summary>Relic reward message 4 format: "I have never seen one of these. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_4_FORMAT = "I have never seen one of these. Here is {0} gold for you.";

		/// <summary>Relic reward message 5 format: "What a rare item. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_5_FORMAT = "What a rare item. Here is {0} gold for you.";

		/// <summary>Relic reward message 6 format: "This is quite rare. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_6_FORMAT = "This is quite rare. Here is {0} gold for you.";

		/// <summary>Relic reward message 7 format: "This will go nicely in my collection. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_7_FORMAT = "This will go nicely in my collection. Here is {0} gold for you.";

		/// <summary>Relic reward message 8 format: "I have only heard tales about such items. Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_8_FORMAT = "I have only heard tales about such items. Here is {0} gold for you.";

		/// <summary>Relic reward message 9 format: "How did you come across this? Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_9_FORMAT = "How did you come across this? Here is {0} gold for you.";

		/// <summary>Relic reward message 10 format: "Where did you find this? Here is {0} gold for you."</summary>
		public const string RELIC_REWARD_10_FORMAT = "Where did you find this? Here is {0} gold for you.";

		#endregion

		#region Bottle Reward Messages (English - Game Standard)

		/// <summary>Bottle reward message 1 format: "Hmmmm...I needed some of this. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_1_FORMAT = "Hmmmm...I needed some of this. Here is {0} gold for you.";

		/// <summary>Bottle reward message 2 format: "I'll take that. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_2_FORMAT = "I'll take that. Here is {0} gold for you.";

		/// <summary>Bottle reward message 3 format: "I assume this is fresh? Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_3_FORMAT = "I assume this is fresh? Here is {0} gold for you.";

		/// <summary>Bottle reward message 4 format: "You are better than some of the undertakers I know. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_4_FORMAT = "You are better than some of the undertakers I know. Here is {0} gold for you.";

		/// <summary>Bottle reward message 5 format: "This is a good bottle you found here. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_5_FORMAT = "This is a good bottle you found here. Here is {0} gold for you.";

		/// <summary>Bottle reward message 6 format: "Keep this up and my lab will be stocked. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_6_FORMAT = "Keep this up and my lab will be stocked. Here is {0} gold for you.";

		/// <summary>Bottle reward message 7 format: "How did you manage to get this bottle? Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_7_FORMAT = "How did you manage to get this bottle? Here is {0} gold for you.";

		/// <summary>Bottle reward message 8 format: "You seem to be good with a surgeons knife. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_8_FORMAT = "You seem to be good with a surgeons knife. Here is {0} gold for you.";

		/// <summary>Bottle reward message 9 format: "I have seen bottles like this before. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_9_FORMAT = "I have seen bottles like this before. Here is {0} gold for you.";

		/// <summary>Bottle reward message 10 format: "I have never seen such a nice bottle of this before. Here is {0} gold for you."</summary>
		public const string BOTTLE_REWARD_10_FORMAT = "I have never seen such a nice bottle of this before. Here is {0} gold for you.";

		#endregion

		#region Tomb Raid Messages (English - Game Standard)

		/// <summary>Tomb raid message 1 format: "Hmmmm...someone has been busy. Here is {0} gold for you."</summary>
		public const string TOMB_RAID_1_FORMAT = "Hmmmm...someone has been busy. Here is {0} gold for you.";

		/// <summary>Tomb raid message 2 format: "I'll take that. Here is {0} gold for you."</summary>
		public const string TOMB_RAID_2_FORMAT = "I'll take that. Here is {0} gold for you.";

		/// <summary>Tomb raid message 3 format: "I assume the traps were well avoided? Here is {0} gold for you."</summary>
		public const string TOMB_RAID_3_FORMAT = "I assume the traps were well avoided? Here is {0} gold for you.";

		/// <summary>Tomb raid message 4 format: "You are better than some of the thieves I have met. Here is {0} gold for you."</summary>
		public const string TOMB_RAID_4_FORMAT = "You are better than some of the thieves I have met. Here is {0} gold for you.";

		/// <summary>Tomb raid message 5 format: "This is a good one you stole here. Here is {0} gold for you."</summary>
		public const string TOMB_RAID_5_FORMAT = "This is a good one you stole here. Here is {0} gold for you.";

		/// <summary>Tomb raid message 6 format: "Keep this up and we will both be rich. Here is {0} gold for you."</summary>
		public const string TOMB_RAID_6_FORMAT = "Keep this up and we will both be rich. Here is {0} gold for you.";

		/// <summary>Tomb raid message 7 format: "How did you manage to steal this one? Here is {0} gold for you."</summary>
		public const string TOMB_RAID_7_FORMAT = "How did you manage to steal this one? Here is {0} gold for you.";

		/// <summary>Tomb raid message 8 format: "You seem to be avoiding the dangers out there. Here is {0} gold for you."</summary>
		public const string TOMB_RAID_8_FORMAT = "You seem to be avoiding the dangers out there. Here is {0} gold for you.";

		/// <summary>Tomb raid message 9 format: "I haven't seen one like this before. Here is {0} gold for you."</summary>
		public const string TOMB_RAID_9_FORMAT = "I haven't seen one like this before. Here is {0} gold for you.";

		/// <summary>Tomb raid message 10 format: "Why earn when you can take? Here is {0} gold for you."</summary>
		public const string TOMB_RAID_10_FORMAT = "Why earn when you can take? Here is {0} gold for you.";

		#endregion

		#region Thief Messages (English - Game Standard)

		/// <summary>Thief rejection message</summary>
		public const string THIEF_REJECTION = "I only deal with fellow thieves.";

		/// <summary>Thief message 1 format: "Hmmmm...someone has been busy. Here is {0} gold for you."</summary>
		public const string THIEF_1_FORMAT = "Hmmmm...someone has been busy. Here is {0} gold for you.";

		/// <summary>Thief message 2 format: "I'll take that. Here is {0} gold for you."</summary>
		public const string THIEF_2_FORMAT = "I'll take that. Here is {0} gold for you.";

		/// <summary>Thief message 3 format: "I assume the traps were well avoided? Here is {0} gold for you."</summary>
		public const string THIEF_3_FORMAT = "I assume the traps were well avoided? Here is {0} gold for you.";

		/// <summary>Thief message 4 format: "You are better than some of the thieves I have met. Here is {0} gold for you."</summary>
		public const string THIEF_4_FORMAT = "You are better than some of the thieves I have met. Here is {0} gold for you.";

		/// <summary>Thief message 5 format: "This is a good one you stole here. Here is {0} gold for you."</summary>
		public const string THIEF_5_FORMAT = "This is a good one you stole here. Here is {0} gold for you.";

		/// <summary>Thief message 6 format: "Keep this up and we will both be rich. Here is {0} gold for you."</summary>
		public const string THIEF_6_FORMAT = "Keep this up and we will both be rich. Here is {0} gold for you.";

		/// <summary>Thief message 7 format: "How did you manage to steal this one? Here is {0} gold for you."</summary>
		public const string THIEF_7_FORMAT = "How did you manage to steal this one? Here is {0} gold for you.";

		/// <summary>Thief message 8 format: "You seem to be avoiding the dangers out there. Here is {0} gold for you."</summary>
		public const string THIEF_8_FORMAT = "You seem to be avoiding the dangers out there. Here is {0} gold for you.";

		/// <summary>Thief message 9 format: "I have seen one like this before. Here is {0} gold for you."</summary>
		public const string THIEF_9_FORMAT = "I have seen one like this before. Here is {0} gold for you.";

		/// <summary>Thief message 10 format: "Why earn when you can take? Here is {0} gold for you."</summary>
		public const string THIEF_10_FORMAT = "Why earn when you can take? Here is {0} gold for you.";

		#endregion

		#region Henchman Messages (English - Game Standard)

		/// <summary>Henchman message 1</summary>
		public const string HENCHMAN_1 = "So, this follower is not working out for you?";

		/// <summary>Henchman message 2</summary>
		public const string HENCHMAN_2 = "Looking for a replacement henchman eh?";

		/// <summary>Henchman message 3</summary>
		public const string HENCHMAN_3 = "Well...this one is looking for fame and fortune.";

		/// <summary>Henchman message 4</summary>
		public const string HENCHMAN_4 = "Maybe this one will be a better fit in your group.";

		/// <summary>Henchman message 5</summary>
		public const string HENCHMAN_5 = "Not all relationships work out.";

		/// <summary>Henchman message 6</summary>
		public const string HENCHMAN_6 = "At you least you parted ways amiably.";

		/// <summary>Henchman message 7</summary>
		public const string HENCHMAN_7 = "This one has been hanging out around here.";

		/// <summary>Henchman message 8</summary>
		public const string HENCHMAN_8 = "This one also seeks great treasure.";

		/// <summary>Henchman rejection message</summary>
		public const string HENCHMAN_REJECTION = "This is not a graveyard! Bury them somewhere else!";

		#endregion

		#region Currency Exchange Messages (English - Game Standard)

		/// <summary>Currency exchange error: not enough for single gold</summary>
		public const string CURRENCY_ERROR_NOT_ENOUGH = "Sorry, you do not have enough here to exchange for even a single gold coin.";

		/// <summary>Currency exchange message format: "Here is {0} gold for you, and {1} {2} back in change."</summary>
		public const string CURRENCY_EXCHANGE_WITH_CHANGE_FORMAT = "Here is {0} gold for you, and {1} {2} back in change.";

		/// <summary>Currency exchange message format: "Here is {0} gold for you."</summary>
		public const string CURRENCY_EXCHANGE_FORMAT = "Here is {0} gold for you.";

		/// <summary>Currency type: silver</summary>
		public const string CURRENCY_TYPE_SILVER = "silver";

		/// <summary>Currency type: copper</summary>
		public const string CURRENCY_TYPE_COPPER = "copper";

		#endregion

		#region Special Item Messages (Portuguese/English)

		/// <summary>Coffee acceptance message (Portuguese)</summary>
		public const string COFFEE_ACCEPTANCE = "Ohhh... sim... isso vai servir!";

		/// <summary>Magic carpet alteration message</summary>
		public const string CARPET_ALTERED = "I altered your magic carpet.";

		/// <summary>World map purchase message</summary>
		public const string MAP_PURCHASE = "Thank you. Here is your world map.";

		/// <summary>Curse removal from books</summary>
		public const string CURSE_REMOVED_BOOKS = "The curse has been lifted from the books.";

		/// <summary>Curse removal format: "The curse has been lifted from the {0}."</summary>
		public const string CURSE_REMOVED_FORMAT = "The curse has been lifted from the {0}.";

		/// <summary>Item cleaned message</summary>
		public const string ITEM_CLEANED = "The item has been cleaned.";

		/// <summary>Weeds removed message</summary>
		public const string WEEDS_REMOVED = "The weeds have been removed.";

		/// <summary>Default item name for curse removal</summary>
		public const string DEFAULT_ITEM_NAME = "item";

		#endregion

		#region BOD Messages (English - Game Standard)

		/// <summary>BOD credits earned format: "Thank you!  You've earned {0} credits with the Guild."</summary>
		public const string BOD_CREDITS_EARNED_FORMAT = "Thank you!  You've earned {0} credits with the Guild.";

		/// <summary>BOD credits info</summary>
		public const string BOD_CREDITS_INFO = "You can always ask me about your credits total or to redeem them.";

		#endregion

		#region Credit System Messages (English - Game Standard)

		/// <summary>Credit system thank you</summary>
		public const string CREDITS_THANK_YOU = "Thank you for your help!";

		/// <summary>Credit system looking</summary>
		public const string CREDITS_LOOKING = "Let me see what I can find for you... ";

		/// <summary>Credit system reward given</summary>
		public const string CREDITS_REWARD_GIVEN = "Here you go.";

		/// <summary>Credit system redeemed format: "You have redeemed {0} credits from the Guild."</summary>
		public const string CREDITS_REDEEMED_FORMAT = "You have redeemed {0} credits from the Guild.";

		#endregion

		#region Look For Reward Messages (English - Game Standard)

		/// <summary>Look for reward message 1</summary>
		public const string LOOK_REWARD_1 = "I have this... ";

		/// <summary>Look for reward message 2</summary>
		public const string LOOK_REWARD_2 = "Perhaps I could part with that there...";

		/// <summary>Look for reward message 3</summary>
		public const string LOOK_REWARD_3 = "Points to an item on a shelf and changes his mind.";

		/// <summary>Look for reward message 4</summary>
		public const string LOOK_REWARD_4 = "I can't give that soo....";

		/// <summary>Look for reward message 5</summary>
		public const string LOOK_REWARD_5 = "ah... maybe you could use this.";

		#endregion

		#region Purchase Messages (English - Game Standard)

		/// <summary>Purchase message GM format: "I would not presume to charge thee anything.  Here are the goods you requested."</summary>
		public const string PURCHASE_GM_FULL = "I would not presume to charge thee anything.  Here are the goods you requested.";

		/// <summary>Purchase message bank format: "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage."</summary>
		public const string PURCHASE_BANK_FORMAT = "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.";

		/// <summary>Purchase message format: "The total of thy purchase is {0} gold.  My thanks for the patronage."</summary>
		public const string PURCHASE_FORMAT = "The total of thy purchase is {0} gold.  My thanks for the patronage.";

		/// <summary>Purchase message GM partial format: "I would not presume to charge thee anything.  Unfortunately, I could not sell you all the goods you requested."</summary>
		public const string PURCHASE_GM_PARTIAL = "I would not presume to charge thee anything.  Unfortunately, I could not sell you all the goods you requested.";

		/// <summary>Purchase message bank partial format: "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested."</summary>
		public const string PURCHASE_BANK_PARTIAL_FORMAT = "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.";

		/// <summary>Purchase message partial format: "The total of thy purchase is {0} gold.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested."</summary>
		public const string PURCHASE_PARTIAL_FORMAT = "The total of thy purchase is {0} gold.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.";

		#endregion

		#region Sell Messages (English - Game Standard)

		/// <summary>Sell limit message format: "You may only sell {0} items at a time!"</summary>
		public const string SELL_LIMIT_FORMAT = "You may only sell {0} items at a time!";

		#endregion

		#region Property Messages (English - Game Standard)

		/// <summary>Property message: Bulk orders give credits</summary>
		public const string PROPERTY_BULK_ORDERS = "Bulk orders give credits, say 'credits' to see how many you have.";

		/// <summary>Property message: How to redeem credits</summary>
		public const string PROPERTY_REDEEM_CREDITS = "To redeem your credits, say 'claim'.";

		#endregion

		#region Carpenter Special Messages (English - Game Standard)

		/// <summary>Carpenter message 1</summary>
		public const string CARPENTER_1 = "Help Sire!  Someone keeps stealing my SawMill!";

		/// <summary>Carpenter message 2</summary>
		public const string CARPENTER_2 = "Welp!  There's a Sawmill thief around!";

		#endregion

		#region Region Names (Hardcoded - Used for Logic)

		/// <summary>Region: Doom Gauntlet</summary>
		public const string REGION_DOOM_GAUNTLET = "Doom Gauntlet";

		/// <summary>Region: Doom</summary>
		public const string REGION_DOOM = "Doom";

		/// <summary>Region: City of Britain</summary>
		public const string REGION_CITY_OF_BRITAIN = "the City of Britain";

		/// <summary>Region: Midkemia</summary>
		public const string REGION_MIDKEMIA = "Midkemia";

		/// <summary>Region: Calypso</summary>
		public const string REGION_CALYPSO = "Calypso";

		/// <summary>Region: Dungeon Room</summary>
		public const string REGION_DUNGEON_ROOM = "the Dungeon Room";

		/// <summary>Region: Camping Tent</summary>
		public const string REGION_CAMPING_TENT = "the Camping Tent";

		/// <summary>Region: Forgotten Lighthouse</summary>
		public const string REGION_FORGOTTEN_LIGHTHOUSE = "the Forgotten Lighthouse";

		/// <summary>Region: Savage Sea Docks</summary>
		public const string REGION_SAVAGE_SEA_DOCKS = "Savage Sea Docks";

		/// <summary>Region: Serpent Sail Docks</summary>
		public const string REGION_SERPENT_SAIL_DOCKS = "Serpent Sail Docks";

		/// <summary>Region: Anchor Rock Docks</summary>
		public const string REGION_ANCHOR_ROCK_DOCKS = "Anchor Rock Docks";

		/// <summary>Region: Kraken Reef Docks</summary>
		public const string REGION_KRAKEN_REEF_DOCKS = "Kraken Reef Docks";

		/// <summary>Region: The Port</summary>
		public const string REGION_THE_PORT = "the Port";

		/// <summary>Region: Thieves Guild</summary>
		public const string REGION_THIEVES_GUILD = "the Thieves Guild";

		/// <summary>Region: Ship's Lower Deck</summary>
		public const string REGION_SHIP_LOWER_DECK = "the Ship's Lower Deck";

		/// <summary>Region: Wizards Guild</summary>
		public const string REGION_WIZARDS_GUILD = "the Wizards Guild";

		/// <summary>Region: The Pit</summary>
		public const string REGION_THE_PIT = "The Pit";

		/// <summary>Region: Ruins of Tenebrae</summary>
		public const string REGION_RUINS_OF_TENEBRAE = "the Ruins of Tenebrae";

		/// <summary>Region: Temple of Praetoria</summary>
		public const string REGION_TEMPLE_OF_PRAETORIA = "the Temple of Praetoria";

		#endregion

		#region Title Strings (Hardcoded - Used for Logic)

		/// <summary>Title: the merchant</summary>
		public const string TITLE_MERCHANT = "the merchant";

		/// <summary>Title: the dock worker</summary>
		public const string TITLE_DOCK_WORKER = "the dock worker";

		/// <summary>Title: the sailor</summary>
		public const string TITLE_SAILOR = "the sailor";

		/// <summary>Title: the cooper</summary>
		public const string TITLE_COOPER = "the cooper";

		/// <summary>Title: the cabin boy</summary>
		public const string TITLE_CABIN_BOY = "the cabin boy";

		/// <summary>Title: the serving wench</summary>
		public const string TITLE_SERVING_WENCH = "the serving wench";

		/// <summary>Title: the master-at-arms</summary>
		public const string TITLE_MASTER_AT_ARMS = "the master-at-arms";

		/// <summary>Title: the harpooner</summary>
		public const string TITLE_HARPOONER = "the harpooner";

		/// <summary>Title: the boatswain</summary>
		public const string TITLE_BOATSWAIN = "the boatswain";

		/// <summary>Title: the fence</summary>
		public const string TITLE_FENCE = "the fence";

		/// <summary>Title: the quartermaster</summary>
		public const string TITLE_QUARTERMASTER = "the quartermaster";

		/// <summary>Title: the butler</summary>
		public const string TITLE_BUTLER = "the butler";

		/// <summary>Title: the maid</summary>
		public const string TITLE_MAID = "the maid";

		#endregion

		#region Item Type Strings (Hardcoded - Used for Logic)

		/// <summary>Item type: coffee</summary>
		public const string ITEM_TYPE_COFFEE = "coffee";

		/// <summary>Item type: espresso</summary>
		public const string ITEM_TYPE_ESPRESSO = "espresso";

		/// <summary>Item type: americano</summary>
		public const string ITEM_TYPE_AMERICANO = "americano";

		/// <summary>Item type: cappucino</summary>
		public const string ITEM_TYPE_CAPPUCINO = "cappucino";

		#endregion
	}
}

