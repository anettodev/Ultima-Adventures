# Ultima Adventures Player Guide

> **Quick Start Guide for New Players**  
> A comprehensive guide to help you get started and thrive in Ultima Adventures

---

## Table of Contents

- [Introduction](#introduction)
  - [What is this Guide for?](#what-is-this-guide-for)
  - [Ultima Adventures at a Glance](#ultima-adventures-at-a-glance)
- [Player Commands & Controls](#player-commands--controls)
  - [Essential Player Commands](#essential-player-commands)
  - [NPC Speech Keywords](#npc-speech-keywords)
  - [Pet Commands](#pet-commands)
  - [Boat Navigation Commands](#boat-navigation-commands)
  - [House Commands](#house-commands)
- [Getting Started](#getting-started)
  - [Recommended Game Settings](#recommended-game-settings)
  - [Player Commands Reference](#player-commands-reference)
  - [Building Your Character](#building-your-character)
  - [Immediate Steps for All Characters](#immediate-steps-for-all-characters)
- [Character Builds](#character-builds)
  - [Ranger - Strong](#ranger---strong)
  - [Wizard](#wizard)
  - [Fighter](#fighter)
  - [Smith - Reliable](#smith---reliable)
  - [Tailor - Powergamey](#tailor---powergamey)
- [Essential Skills & Items](#essential-skills--items)
  - [Skills Everybody Needs](#skills-everybody-needs)
  - [Items Everybody Needs](#items-everybody-needs)
  - [NPC Interaction & Speech Keywords](#npc-interaction--speech-keywords)
- [Gameplay Systems](#gameplay-systems)
  - [Stat Gains](#stat-gains)
  - [Making Money](#making-money)
  - [Evil Playstyle](#evil-playstyle)
- [Advanced Topics](#advanced-topics)
  - [Notable Changes from Standard UO](#notable-changes-from-standard-uo)
  - [Universal Next Steps](#universal-next-steps)
  - [Potential Long-Term Goals](#potential-long-term-goals)
- [Quick Reference](#quick-reference)
  - [Essential Commands Cheat Sheet](#essential-commands-cheat-sheet)
  - [Recommended Macros](#recommended-macros)
  - [Critical Early Game Items](#critical-early-game-items)
  - [Stat Gain Schedule](#stat-gain-schedule)
  - [Pet Slot Calculation](#pet-slot-calculation)
  - [Death Loss Prevention](#death-loss-prevention)
  - [Essential Vendor Locations](#essential-vendor-locations)
  - [Skill Training Costs](#skill-training-costs)
  - [Dungeon Difficulty Guide](#dungeon-difficulty-guide)
  - [Karma Reference](#karma-reference)
  - [House Ownership](#house-ownership)
  - [Special Systems Quick Access](#special-systems-quick-access)
- [Resources](#resources)
  - [Weblinks](#weblinks)
  - [Community](#community)

---

## Introduction

### What is this Guide for?

This guide aims to help new players get fully immersed in and adventuring around the world as quickly as possible—nearly immediately after spawning.

**Key Assumptions:**
- You are familiar with the basics of Ultima Online
- You can Google generic basics of skills/abilities and what they do
- This guide assumes you'll be working on your own (though the community is helpful!)
- You may be playing online or offline

**Important Notes:**
- ⚠️ This guide is **NOT recommended** for soul bound/perm death characters as your first character
- Because Ultima Adventures is an "all skills" game, it differs considerably from standard shards
- The startup, build choices, and progression are likely very different than what you're used to

> 💡 **Tip:** Join the Discord if you haven't already and say hello! The community is filled with helpful and charitable players.

---

## Ultima Adventures at a Glance

### Core Features

- ✅ **NO non-consensual PvP, NO griefing** (online server)
- ✅ **Multi-account, macro/botting, multi-houses all allowed** within reason (online server)
- ✅ **Unlimited skills** (but limited to 2.4k until you finish a quest)
- ✅ **Stat cap of 250** (or 225 if you're restricted)
- ✅ **Completely different map and world** to Ultima Online
- ✅ **Easier to acquire good items** (artifacts) and upgrade them, like a single-player game

### Custom Systems

- 🎯 **MANY custom systems**: Extra spell systems, new uses for skills, new crafts and monsters
- 📚 **Engage with the world**: Read books, talk to NPCs to discover secrets
- ⚖️ **Battle of Good and Evil**: Affects prices, monster strength, death loss, grants Balance points
- 💀 **Loss on death**: Unless revived by a player (costs Balance Points) or recall/gate
- 🎲 **Random non-static spawns**: NPC Player Killer gangs can appear anywhere
- 🦠 **Simulated random global biohazard event**

### Unique Mechanics

- **Auto-rez system**: After 30 seconds, option to auto-rez (increased death loss, but first few are free)
- **Pet slots**: Start at 2 by default, can go much higher than 5
- **Hunger/Thirst system**: You need to eat and drink to avoid stamina and health loss
- **Edited gain systems**: Skills designed to be fun to level without macroing

---

## Player Commands & Controls

### Essential Player Commands

These are commands available to all players. Commands are typed in the chat and usually start with a bracket `[` for system commands or are spoken for NPC interaction.

#### System Commands

| Command | Usage | Description |
|---------|-------|-------------|
| `Help` | `[Help` | Opens the help menu showing all available commands |
| `HelpInfo` | `[HelpInfo [command]` | Displays detailed information about a specific command, or shows a gump with all commands when no argument is provided |
| `Time` | `[Time` | Returns the server's local time |
| `Where` | `[Where` | Tells you your coordinates, region name, and current facet |
| `Stuck` | `[Stuck` | Opens a menu of towns for teleporting when physically unable to move |
| `Ratings` | `[Ratings` | Views the Balance leaderboard (Avatar rankings for Order/Chaos) - only available if pledged to good or evil |

> 💡 **Tip:** Type `[HelpInfo` without arguments to see a comprehensive gump menu with all available commands organized by category.

### NPC Speech Keywords

The game uses an advanced speech recognition system. NPCs respond to keywords and phrases you say near them. You don't need perfect syntax - keywords work even in sentences!

#### Greeting & Basic Interaction

Use these to start conversations with NPCs:

- **Greetings:** `hi`, `hello`, `hail`, `greetings`, `hey`, `yo`
- **Job/Info:** `job`, `occupation`, `profession`, `what do you do`
- **Farewell:** `bye`, `farewell`, `see ya`
- **General Questions:** `how are you`, `how art thou`, `are you well`, `art thou well`

#### Vendor Keywords

**Buying & Selling:**
- `vendor buy` or `buy` - Opens buy menu
- `vendor sell` or `sell` - Opens sell menu  
- `vendor browse`, `vendor look`, `vendor view` - Browse vendor's goods
- `vendor collect`, `vendor get`, `vendor gold` - Collect gold from your vendor
- `vendor info`, `vendor status` - Check vendor status
- `vendor dismiss`, `vendor replace` - Dismiss a vendor
- `vendor cycle` - Cycle through vendor options

**Special Vendor Interactions:**
- `total` or `credits` - Check your BOD credits with guild vendors (Blacksmith/Tailor guilds)
- `claim [number]`, `cash in [number]`, `redeem [number]` - Claim BOD credits from guild vendors
- `[vendor name] explain` or `vendor explain` - Get detailed information about what a vendor does

> 💡 **Pro Tip:** Create a macro that says `Vendor Buy Bank Guards!` - this is the most efficient way to interact with towns (vendor buy opens shops, bank accesses your bank, guards calls for help if needed).

#### Guild & Service Keywords

- **Banking:** `bank` - Opens your bank box
- **Stabling:** `stable` - Opens animal stabling menu
- **Claiming:** `claim` - Claim stabled pets
- **Guild:** `guild`, `guilds`, `join`, `member` - Guild-related interactions
- **Quit Guild:** `quit`, `resign`, `i resign from my guild`

#### Training Keywords

Say `train [skill name]` to any appropriate NPC trainer. Examples:
- `train magery`, `train magic`, `train wizard`
- `train blacksmith`, `train smithing`
- `train taming`, `train animal`
- `train healing`, `train first aid`
- `train swords`, `train fencing`, `train maces`
- `train archery`, `train shooting`

> 📝 **Note:** Most skills can be trained to 30.0 for gold. The trainer must teach that skill (check their job).

#### Location Keywords

Ask NPCs for directions using `where is the [location]`:

**Towns:**
- `where is britain`, `where is yew`, `where is trinsic`
- `where is vesper`, `where is moonglow`, `where is minoc`
- `where is jhelom`, `where is magincia`, `where is skara`

**Services:**
- `where is the bank`, `where is the smith`, `where is the stable`
- `where is the inn`, `where is the tavern`, `where is the healer`
- `where is the mage`, `where is the tailor`, `where is the carpenter`
- `where is the tinker`, `where is the alchemist`
- `where is the weaponsmith`, `where is the armorer`

**Dungeons & Landmarks:**
- `covetous`, `deceit`, `despise`, `destard`, `hythloth`, `shame`, `wrong`
- `where is the shrine`, `where is the cemetery`

### Pet Commands

Control your pets and followers with these voice commands:

#### Single Pet Commands
Target a specific pet and say:
- `come` - Come to you
- `follow` - Follow you
- `follow me` - Follow you
- `guard` - Guard you
- `guard me` - Guard you  
- `kill` or `attack` - Attack your target
- `stop` - Stop current action
- `stay` - Stay in current location
- `drop` - Drop what they're carrying
- `friend` - Make another mobile a friend
- `transfer` - Transfer pet to another player
- `release` - Release the pet

#### All Pets Commands
Control all your pets at once:
- `all come` - All pets come to you
- `all follow` - All pets follow you
- `all follow me` - All pets follow you
- `all guard` - All pets guard you
- `all guard me` - All pets guard you
- `all kill` or `all attack` - All pets attack your target
- `all stop` - All pets stop
- `all stay` - All pets stay in place

> ⚠️ **Important:** Create macros for `all guard`, `all kill`, `all follow me`, and `all stay` - these are essential for effective pet control!

### Boat Navigation Commands

When on a boat, speak these commands to control navigation:

**Movement:**
- `forward` - Move forward
- `back`, `backward`, `backwards` - Move backward
- `left`, `drift left` - Drift left
- `right`, `drift right` - Drift right
- `port` - Turn to port (left)
- `starboard` - Turn to starboard (right)
- `stop` - Stop the boat

**Speed Control:**
- `slow forward`, `slow back`, `slow left`, `slow right` - Slow movement
- `forward one`, `back one`, `left one`, `right one` - Move one tile

**Turning:**
- `turn left`, `turn right` - Turn the boat
- `turn around`, `come about` - Turn 180 degrees

**Sailing:**
- `unfurl sail` - Raise sails
- `furl sail` - Lower sails
- `drop anchor`, `lower anchor` - Drop anchor
- `raise anchor`, `hoist anchor`, `lift anchor` - Raise anchor

**Navigation:**
- `start` - Start navigation
- `continue` - Continue navigation
- `nav` - Navigation options
- `goto [location]` - Navigate to location
- `single` - Single navigation command

### House Commands

When in your house, you can use these speech commands:

- `i wish to lock this down` - Lock down an item
- `i wish to release this` - Release a locked down item
- `i wish to secure this` - Secure a container
- `i wish to unsecure this` - Unsecure a container
- `i wish to place a strongbox` - Place a strongbox
- `i wish to place a trash barrel` - Place a trash barrel

---

## Getting Started

### Recommended Game Settings

Enable or check out these options. Leave everything else as-is unless you know you want to change it.

#### PaperDoll - Options

**General:**
- ✅ Auto run
- ✅ Auto open doors
- ⚠️ Maybe auto open corpses (be careful—can land you a criminal accidentally)

**Mobiles:**
- ✅ Show HP (prefer line)

**Gumps and Context:**
- ✅ Use Custom Healthbars gump
- ✅ Save health bars on logout
- ✅ Close healthbar gump when mobile is dead
- ✅ Grid Loot = both

**Misc:**
- ✅ Enable Drag-select to open health bars
- ✅ Inform when skills change by = set to 0

**Sound:**
- 🔊 Turn the volume and music down—it's deafening in my experience

**Macros:**
Create macro buttons that say each of the following:
- `Vendor Buy Bank Guards!`
- `Sell`
- `All guard`
- `All follow me`
- `All kill`
- `All stay`

**Combat-Spells:**
- ✅ Cast spells by one click

**Info Bar:**
- ✅ Show info bar
- ✅ Add item and set the new item to followers

**Containers:**
- ✅ Container scale = as big as you can live with (max recommended)
- ✅ Double click to loot items

**Paperdoll - Help:**
- ✅ Click Quick bar - very helpful menu

**Settings:**
- ✅ Loot options - simple auto looter

**Immersion Breaking:**
- ✅ Enable Circle of transparency
- ✅ Trees to stumps

---

## Player Commands Reference

Adventures includes many helpful player commands to enhance your gameplay experience. All commands are activated in the chat window.

### Emote Commands

Use `[emote` or `[e` to access the emote system with sound effects and animations:

**Social Emotes:**
- `[e ah`, `[e ahha`, `[e applaud`, `[e bow`, `[e kiss`, `[e laugh`
- `[e hey`, `[e no`, `[e oh`, `[e oooh`, `[e oops`, `[e yea`, `[e yell`

**Physical Reactions:**
- `[e blownose`, `[e burp`, `[e clearthroat`, `[e cough`, `[e bscough`
- `[e cry`, `[e faint`, `[e gasp`, `[e giggle`, `[e groan`, `[e growl`
- `[e hiccup`, `[e huh`, `[e scream`, `[e shush`, `[e sigh`
- `[e sneeze`, `[e sniff`, `[e snore`, `[e yawn`

**Actions:**
- `[e fart`, `[e puke`, `[e punch`, `[e slap`, `[e spit`
- `[e stickouttongue`, `[e tapfoot`, `[e wistle`, `[e woohoo`

> 💡 **Tip:** Type `[emote` with no parameter to see a full emote menu, or use `[e` for a compact version!

### Utility Commands

**Essential Commands:**
- `[help` - Opens the help menu with game information
- `[stuck` - Teleports you to a safe location if trapped
- `[save` - Manually saves your character (if enabled by server)

**Interface Commands:**
- `[skillmenu` - Opens your skills interface
- `[spellbar` - Manage spell hotbars for quick casting
- `[quickbar` - Opens quick access toolbar
- `[wealthbar` - Display your gold count
- `[reagent` - Shows reagent counter

**Social Commands:**
- `[afk` - Mark yourself as Away From Keyboard
- `[party` - Opens party management interface

**Music & Entertainment:**
- `[music` - Opens music player
- `[playlist` - Manage your music playlist
- `[playbarbaric` - Play barbaric-style music
- `[playevil` - Play dark/evil-style music
- `[playoriental` - Play oriental-style music

**Utility Features:**
- `[autosheathe` - Toggle automatic weapon sheathing
- `[bandself` - Quick self-bandaging command
- `[loot` - Enhanced looting interface
- `[corpse` - Search corpses more efficiently
- `[moon` - Search for moongate destinations
- `[spellhue` - Customize your spell effect colors

> 💡 **Pro Tip:** Set up macros for commonly used commands to save time!

---

## Building Your Character

### Important First Decisions

**Remember:** Your starting build doesn't affect your character in the long run (except for Bushido because of its effect on parrying and shields). It just affects which skills you don't have to get from 30 (purchasable) to 50.

**During Character Creation:**
- ✅ **Always select Advanced Character** - defaults are generally not as good
- 📝 **Choose your starting skills** based on the builds below

### Immediate Steps for All Characters

1. **Press Ctrl+Shift in-game** to see how the game identifies all usable items on screen—VERY helpful for finding interactable items.

2. **During the intro sequence**, you'll be asked questions by FinalTwist:
   - **Normal mode character**: Answer `yes, yes, no`
   - **Easy mode character**: Answer `yes, no, no`
   - **Soulbound (NOT RECOMMENDED)**: Answer `yes` to the third question

---

## Stat Gains

### Starting Stat Gains

Each stat can only gain once every **15 minutes**, so it's important you start gaining them all straight away.

**Recommended approach:**
- Gain everything to start with
- Turn something down when you hit the limit
- All stats are beneficial in some way

**Camping** is the number one way to gain all 3 stats, and is an amazing skill anyway in Adventures.

> ⚠️ **NEVER pay for the starting 30 of Camping**

### How to Gain Stats

**Immediate Action:**
1. Equip the dagger in your bag
2. Double-click it, then click on a nearby tree
3. Repeat until you have some kindling in your bag
4. Double-click the kindling repeatedly and watch the gains
5. As soon as you gain 1 of each stat, stop clicking
6. Gather more kindling for later
7. Check the time—every 15 minutes do this again

**Stat Gain Methods:**

| Stat | Skills That Grant Gains |
|------|------------------------|
| **STR** | Camping, Mining, Lumberjacking |
| **DEX** | Camping, Alchemy, Hiding |
| **INT** | Camping, Forensic Eval, Music |

---

## Essential Skills & Items

### Skills Everybody Needs

Since you have an unlimited skill cap, there's no reason not to take advantage of utility skills. These are recommended immediately on all characters:

#### Animal Taming and Animal Lore

- **Why:** Why walk when you can ride? Why buy horses when you can tame them?
- **Cost:** Only ~250gp more than a horse
- **Useful for:** Basic combat pets, riding animals
- **Best tames around starting area:** Brown bears and Jaguars (excellent for buyable taming levels)

#### Healing and Anatomy

- **Why:** Any method of restoring HP is useful, especially if it doesn't need mana
- **Starting supply:** Go to tailors, buy scissors and a single bolt of cloth to cut up into bandages
- **Pro tip:** Always start bandaging if you have lost even a single HP to get gains

#### Alchemy

- **Why:** Can be used to cure poison with shop-bought skill levels
- **Purchase:** Buy the skill, pestle and mortar, and at least 10 bottles (more is better)
- **Make:** 5 lesser cure and 5 lesser heal minimum
- **Bonus:** Access to explosion pots right at the start (excellent ranged AoE attacks)

> ⚠️ **CAREFUL: Explosions can be hazardous to your health!**

- **Money tip:** Don't buy any regs for potions yet—you get plenty when buying magery stuff

#### Magery

- **Why:** Useful skill for everyone as a backup, buffs, and last-minute saving spells
- **Essential when:** You have a spell channeling weapon
- **Purchase:**
  - Mage book
  - Look in mage vendor for a "bag" (~200gp) - prepacked bag of 50 of each reagent

**Recommended Spells to Buy:**

1. **Reactive Armour** - Buff that lasts forever, adds 15% to physical resist
2. **Cure** - Cure poison on others (pets) or yourself in a pinch
3. **Heal** - Heal others from afar (useful if you don't have chivalry)
4. **Protection** - Stops casting being disrupted (useful even for melee with chivalry)
5. **Magic Untrap** - Creates trap-protecting orb in your bag (essential for Normal+ dungeons)
6. **Wall of Stone** - Separate groups of monsters from each other
7. **Poison** - Damage over time
8. **Magic Arrow** - Basic ranged damage
9. **Fireball** - Area damage

#### Hiding

- **Why:** Good for survival, to disengage from combat
- **Use:** Escape monsters if it looks bad

---

### Items Everybody Needs

**Stuff the above skills need:**
- Bandages, pair of scissors
- Pestle and mortar
- Glass bottles
- Mage Book and spells
- Bag of reagents
- Dagger

> ⚠️ **Never leave town without the above items**

**Additional Items:**

- **Leather boots, Robe and Cloak** - Resist from slots you don't normally wear armor in (CHEAP)
- **Monocle** - Using on bookcases in dungeons yields story books, lore, free scrolls, and gains for inscription (FREE EXTRA LOOT)
- **Gardening scissors** - Gain cooking and stay nourished from wildlife (FREE STUFF)

---

## NPC Interaction & Speech Keywords

NPCs in Adventures respond to specific keywords, making interaction more immersive and rewarding. Learning these keywords will help you navigate the world more efficiently.

### Vendor Interaction

**Buying & Selling:**
- `vendor buy` or `vendor purchase` - Opens buy interface
- `vendor browse`, `vendor look`, `vendor view` - Browse inventory
- `vendor sell` - Opens sell interface
- `buy` - Quick purchase
- `sell` - Quick sell

**Vendor Management:**
- `vendor collect`, `vendor get`, `vendor gold` - Collect gold from your vendor
- `vendor info`, `vendor status` - Check vendor status
- `vendor dismiss`, `vendor replace` - Dismiss/replace vendor
- `vendor cycle` - Cycle vendor inventory

### Training & Skills

To train a skill with an NPC trainer, use `*train [skillname]*`:

**Examples:**
- `train blacksmith`, `train smithing` - Blacksmithing
- `train magery`, `train magic`, `train wizard` - Magery
- `train taming`, `train tame` - Animal Taming
- `train healing`, `train heal`, `train first aid` - Healing
- `train hiding`, `train hide` - Hiding
- `train stealing`, `train steal` - Stealing
- `train archery`, `train archer`, `train shooting` - Archery
- `train swords`, `train swordsman` - Swordsmanship
- `train mining`, `train mine` - Mining
- `train lumberjacking`, `train lumberjack` - Lumberjacking

> 💡 **Tip:** Most skills have multiple keyword variations - try different phrasings if one doesn't work!

### Pet Commands

**Basic Commands:**
- `all follow me` - All pets follow you
- `all follow` - All pets follow targeted creature
- `all guard me` - All pets guard you
- `all guard` - All pets guard area
- `all kill` - All pets attack target
- `all attack` - Same as kill
- `all stop` - All pets stop current action
- `all stay` - All pets stay in place
- `all come` - All pets come to you

**Individual Pet Commands:**
- `[petname] follow me` - Single pet follows you
- `[petname] guard me` - Single pet guards you
- `[petname] kill` - Single pet attacks
- `[petname] stay` - Single pet stays
- `[petname] stop` - Single pet stops

**Pet Management:**
- `release` - Release pet to wild
- `transfer` - Transfer pet to another player

### Social Keywords

**Greetings:**
- `hello`, `hi`, `greetings`, `hail`, `hey`, `yo` - Greet NPCs

**Farewells:**
- `bye`, `farewell`, `see ya`, `ciao` - Say goodbye

**Thanks:**
- `thanks`, `thank you`, `thank thee`, `appreciate` - Express gratitude

**Questions:**
- `job`, `occupation`, `profession`, `what do you do` - Ask about NPC's job
- `where am I`, `lost` - Get location help
- `where is the [location]` - Ask for directions
- `what is your name`, `who are you` - Ask NPC's name

### Location Inquiries

Use `where is the [place]` to get directions:

**Vendors & Services:**
- `where is the bank`, `where is the smith`, `where is the mage`
- `where is the inn`, `where is the tavern`, `where is the pub`
- `where is the stable`, `where is the healer`, `where is the alchemist`
- `where is the tailor`, `where is the tinker`, `where is the carpenter`
- `where is the baker`, `where is the butcher`, `where is the fisherman`

**Facilities:**
- `where is the library`, `where is the castle`, `where is the temple`
- `where is the dock`, `where is the farm`, `where is the mill`
- `where is the cemetery`, `where is the jail`

### Guild & Service Keywords

**Banking:**
- `bank` - Access bank
- `balance`, `statement` - Check balance
- `withdraw` - Withdraw gold
- `check` - Convert gold to check

**Guilds:**
- `guild`, `guilds` - Ask about guilds
- `join`, `member` - Join a guild
- `quit`, `resign` - Leave guild

**Other Services:**
- `stable` - Access animal stabling
- `claim` - Claim stabled pets
- `guard`, `guards` - Call town guards (in emergencies!)
- `quest` - Ask about quests
- `job` - Ask about work/tasks

### Advanced NPC Interaction

**Lore & Information:**
- Talk to NPCs about `troubles`, `concerns`, `weather`, `news`
- Ask about `britannian`, `lord british`, `ruler`, `king`
- Inquire about `virtue`, `virtues`, `shrines`, `avatar`
- Discuss `moongates`, `gold`, `treasure`, `adventure`

**Profession-Specific:**
- Ask smiths about `weapons`, `armor`, `forge`, `steel`
- Ask mages about `magic`, `spells`, `reagents`, `scrolls`
- Ask healers about `heal`, `cure`, `poison`, `aid`
- Ask tamers about `animals`, `taming`, `training`, `pets`

> ⚠️ **Important:** NPCs remember conversations! Some may become friendly or hostile based on your interactions.

> 💡 **Tip:** Many secrets and quests are discovered through NPC conversations. Take time to chat with NPCs you meet!

---

## Character Builds

These setups are tested options for being able to buy skills and a few items and hit the world's easy dungeons immediately. All of them should get all the above skills that they don't already have.

### Ranger - Strong

**Starting Stats:** 10 STR, 10 DEX, 60 INT  
**Starting Skills:** 50 Taming, 50 Vet

**Why This Build:**
- Takes taming at start enables access to rideable pets with pact instinct
- Very strong straight away once you can survive the run to get them
- Extra pet slot as soon as you log in (from 60 INT)

**⚠️ Important:** As this setup initially relies on pets to tank, you will be very weak and low on HP personally at first. Get to gaining STR fast!

#### Skills to Buy

- Chivalry (remember to tithe 2-300gp to start off with)
- Archery
- Evaluate Intelligence
- Poisoning
- Musician
- Peacemaking

#### Items/Armor to Buy

- Buy Leather armor fit for 25 STR or below
- Throwing gloves from leather workers
- Throwing weapons (often called throwing axes) from tinkers/provisioners/monk
- Drum

#### Go Time

1. **ENSURE** anything spare like gold etc. is in the BANK
2. Make sure you have 10+ of lesser cure and heal potions + explosion potions
3. Cast protection so your heals and cure spells won't get disrupted
4. Go outside Brit on your horse and tame 2 jaguars to start off
5. Head to an easy dungeon or pursue better pets

> ⚠️ **BE VERY CAREFUL** until you are at 25 STR and can equip a full set of leather armor at least

#### How to Fight

- Create macro buttons for "all guard", "all kill" and "all follow me"
- Set your pets to guard you
- At long range, go war mode and attack enemies
- Your pets should run over and solve the issue while you safely close the gap
- Use spells and ranged attacks to help
- Discord or peace enemies
- Use bandages + spells to support your pets

#### Advanced Pets to Aim For

- **Grizzly bears** - Randomly spawn in woods further from town (buy tracking to find them)
- **Polar bears** - Better for combat, need higher Lore (use a moongate to frozen isles, head West)

#### Next Steps

- Join the Druids guild in Montor to get a ring that boosts taming skill + lore
- Seek stronger tames (stick to polar bears for a while)
- Aim for more pet slots ASAP (get INT, get herding, etc.)
- Once you can get mammoths, use gangs of them to tank for you
- Get into tailoring (see tailor crafter section)

---

### Wizard

**Starting Stats:** 40 STR, 10 DEX, 30 INT  
**Starting Skills:** 50 Archery, 50 Alchemy

**Why This Build:**
- While you might expect to start with Magery, more INT, and Eval Int, this actually leaves you very weak
- This setup gives you spell channeling ranged weapons that look like your hands
- Feels very wizardy as you kite enemies around
- PUMPS out damage if you also throw explosion potions, fireballs, and ranged attacks

#### Skills to Buy

- Chivalry (remember to tithe 2-300gp to start)
- Evaluate Intelligence
- Poisoning
- Musician
- Discordance
- Peacemaking
- Inscription (if you find advanced level scrolls, you can make more scrolls—scrolls are 2 levels easier to cast)

#### Items/Armor to Buy

- Buy Leather Armor NOT studded
- Throwing gloves from leather workers
- Throwing weapons (throwing axes, stars, axes, stones, or darts)
- Drum

#### Go Time

1. **ENSURE** anything spare is in the BANK
2. Make sure you have 10+ of cure and heal potions + explosion potions
3. Cast protection, and possibly reactive armor as well
4. Be weary of Gnome Magicians in easy dungeons

#### How to Fight

- Pre-cast fireball and run up
- Shoot that at them and let a ranged attack shoot
- Run around for a few seconds for your swing timer to cooldown
- Repeat
- Try and discord single enemies
- Peace anything extra coming at you
- Use explosion potions if you can manage all the clicks
- Use potions to cure poison so you can use spells for damage

#### Notes

- Using necromancy will tank your karma fast—if you hit -4k karma, blue guards will attack you

#### Next Steps

- Keep shooting things to get archery
- Use spells a lot even if it seems like a waste
- Evaluate random NPCs' intelligence etc. to get gains
- Collect cotton/cloth and pursue tailoring

---

### Fighter

**Starting Stats:** 60 STR, 10 DEX, 10 INT  
**Starting Skills:** 50 Alchemy, 50 Chivalry

**Why This Build:**
- Have weapons, hit things, and don't die easy

#### Skills to Buy

- Any weapon skill
- Resisting Spells (as you won't usually need protection cast, it's worth buying)
- Musician
- Discordance
- Peacemaking
- **Parry + Bushido** - As shields at the start are terrible for stats and low level parry is unlikely to help as much as extra damage a 2-hander provides, it's best to avoid buying parry unless you also buy bushido

#### Items/Armor to Buy

- Metal armor to get as high physical resist as you can
- Check the monk across from weaponsmith—often cheaper
- Buy a **TWO-HANDED WEAPON**
- Or if you want to use wrestling: Get pugilist gloves from leather worker (spell channeling)
- Drum

#### Go Time

1. **ENSURE** anything spare is in the BANK
2. Make sure you have 10+ of cure and heal potions and some explosion potions
3. Cast reactive armor for extra physical protection
4. Get equipped and head to an easy dungeon or graveyard
5. Be weary of Gnome Magicians in easy dungeons

#### How to Fight

- Your swing speed is likely slow with a two-handed weapon
- Don't stand in combat while you get hit between swings
- Run past opponents, wait for swing to happen, then run on
- Wait a few seconds and run back
- Try and throw explosion potions, drink potions, cast chiv spells, or use discordance as you get away
- **Consecrate arms** and **divine fury** are excellent if you need to stand in combat
- **Enemy of one** is AMAZING
- If an enemy is tough, kite it and shoot spells at it instead

#### Next Steps

- Follow the steps in the smith setup and get into mining and crafting weapons
- Join the warriors guild (extra tactics and weapon skill from the ring)
- Seek a spell channeling weapon urgently—even if it's a different weapon type, change weapon and learn the new one

---

### Smith - Reliable

**Starting Stats:** 60 STR, 10 DEX, 10 INT  
**Starting Skills:** 50 Blacksmithing, 50 Alchemy

**Why This Build:**
- Very similar to the fighter generally
- Bit more to fall back on if you die
- Can get quite good self-made armor quickly
- Generate gold to fall back on

#### CRUCIAL STEP

- **Join the blacksmiths guild** - Costs 2k but makes chance for exceptional items higher

#### Skills to Buy

- Any weapon skill
- Mining
- Arms Lore

**Secondary skills (wait to buy after you have crafted the armor):**
- Resisting Spells
- Chivalry (remember to tithe 2-300gp)
- Musician
- Discordance
- Peacemaking
- **Parry + Bushido** (see Fighter section)

#### Items/Armor to Buy

- 2x Non-exceptional blacksmith hammer or tongs
- 2x Pickaxes or Iron Ore spades
- Pack horse
- Drum

#### Prep Time

1. There is a mine in Britain next to the mining trainer (south-east corner)
2. **Be careful** going too deep—it can spawn liches
3. Mine at the entrance if you can
4. If there's no iron ore, consider buying it, or run to a moongate and go to frozen isles
5. West along the isles there are lots of caves to mine in
6. Put the iron ore on the horse (likely multiple trips to forge and back)
7. Make all the chain mail you can that's exceptional
8. Then try and make an exceptional weapon (suggest double axe)

#### Go Time

1. **ENSURE** anything spare is in the BANK
2. Make sure you have 10+ of cure and heal potions and some explosion potions
3. Cast reactive armor for extra physical protection
4. With best set you can make, expect around 45-47 physical resist
5. Get equipped and head to an easy dungeon or graveyard

#### Notes

- Save all your colored ore for BODs
- Sell all the metal armor you make for gold
- Buy colored ore from vendor in bank with "craft" in his name
- Specifically bronze for armor, agapite and valorite for weapons at 90 and GM smithing

#### Next Steps

- Valorite is the metal you can use at GM
- First thing you make should be a weapon when you have it
- Seek a runic hammer tier 1 from BODs
- Make weapons over and over till you get a **spell channeling** one
- **NOTHING ELSE MATTERS AS MUCH** - Spell channeling!

---

### Tailor - Powergamey

**Starting Stats:** 40 STR, 10 DEX, 30 INT  
**Starting Skills:** 50 Tailoring, 50 Archery/Wrestling (either is fine)

**Why This Build:**
- A bit specific and powergamey
- Based on things you can do early if you go directly there
- Strong because tailoring can be gained fast, guild gives big leg up
- Unlike colored ore, hides can be acquired by hunting the right creatures

> ⚠️ **You shouldn't do this as your first toon** - It's less fun than working things out for yourself

#### CRUCIAL STEP

- **Bank your starting cash** before you leave starting town
- You have to run cross-country and may die on the way

#### Skills to Buy

- Just get the universally required skills to start
- Make sure you keep at least 2.5k spare

#### Items/Armor to Buy

- Sewing kits
- Scissors

#### Prep Time

1. As soon as you have basic skills, horse, sewing kit or 3, scissors, and have banked spare cash
2. Click help and select "moongate search"
3. Follow the arrow through gates until you see a moongate or large black rock
4. If it's a rock, double-click it; if it's a moongate, go into it
5. Select **Sosaria - Montor**
6. Zoom out on world map and look for nearby town east down the coast
7. **RUN there** (be careful—things could spawn)
8. Find tailors to the south of the gate and join the guild from the guild master
9. Put on the epic ring of cloth's making
10. Go outside town and tame a jaguar, gorilla, or wolf
11. Find something weak that will give leather (like a goat), say "all guard" and attack
12. Once you get hides, cut them up and make yourself leather armor
13. Once you have one of everything, use misc menu to make **Pugilist gloves** and throwing gloves
14. **Always make the pugilist gloves** - Don't worry about making exceptional yet
15. Release your pet and run back to the moongate
16. Go to the **Frozen Isle**
17. Find some snow leopards, release your horse, tame 2 leopards
18. Use them to kill snow leopards and polar bears
19. Skin the creatures and use the frozen leather to make a full set of **EXCEPTIONAL** leather armor, shoes, robe, cloak
20. Get some fur and make a fur sarong
21. Make some frozen pugilist gloves and throwing gloves and equip yourself

#### Go Time

1. Once equipped as above, go to a leather worker and sell any non-exceptional leather
2. Use any spare cash to buy skills that are useful
3. At this stage, try and emulate the wizard startup above

#### How to Fight

- Same as wizard setup
- Unless you took wrestling, then do a mix of wizard and fighter setup (you'll have spell channeling pugilist gloves for melee)

#### Next Steps

- Get better leathers from giant creatures, zombies, and dragons (different resists)
- Get better gloves, get to other facets for even better leathers
- Get to GM tailoring with the ring
- Do BODs for a runic sewing kit
- Ideally you want a swing speed boosting throwing gloves

---

## Universal Next Steps

After a period of time in easy dungeons and graveyards, things might feel easy. Here are suggestions for what to do next.

### Setup a Disaster Recovery Backup

In case you die and are unable to recover your stuff:
- Get enough gold and items together in a bag in the bank
- Weapons, armor, spare potions, bandages, crafting items
- A PICKAXE, A HATCHET, etc.

### Visit the Library

- **Location:** Central town of Sarth Abbey, library just south of main building
- **Second library:** Westshire
- **Contains:** Many hints, details, and secrets on adventures, crafting systems, custom game elements
- **Vendor:** Sells offline training manuals

> 💡 **Remember:** It's a shared resource—return books when you're done, or even add spare books you find!

### Join a Guild

NPC guilds in Adventures provide powerful benefits and are essential for character progression.

**Guild System Basics:**
- **Initial cost**: 2,000gp for your first guild
- **Changing guilds**: Doubles the cost (4,000gp for second guild, 8,000gp for third, etc.)
- **Tip**: Choose carefully and join early to save gold!
- **All guilds provide**: A ring with significant skill boosts for relevant skills
- **Craft guilds**: Give access to special tools and increased success rates

**Available Guilds:**

*Combat Guilds:*
- **Warriors Guild** - Boosts tactics and weapon skills
- **Rangers Guild** - Boosts archery and tracking
- **Archers Guild** - Specialized archery bonuses

*Magic Guilds:*
- **Mages Guild** - Boosts magery and evaluate intelligence
- **Necromancers Guild** - Boosts necromancy and spirit speak
- **Druids Guild** - Boosts animal taming and animal lore (essential for tamers!)

*Healing & Support:*
- **Healers Guild** - Boosts healing and anatomy

*Crafting Guilds:*
- **Blacksmiths Guild** - Boosts blacksmithing, better exceptional chance, access to GM tools
- **Tailors Guild** - Boosts tailoring, better exceptional chance, access to GM tools
- **Carpenters Guild** - Boosts carpentry and woodworking
- **Tinkerers Guild** - Boosts tinkering
- **Alchemists Guild** - Boosts alchemy

*Resource & Trade Guilds:*
- **Miners Guild** - Boosts mining
- **Merchants Guild** - Trade bonuses
- **Fishermens Guild** - Boosts fishing

*Specialty Guilds:*
- **Bards Guild** - Boosts all bard skills (musicianship, peacemaking, provocation, discordance)
- **Thieves Guild** - Boosts stealing, snooping, hiding, stealth (requires -500 karma to join)
- **Assassins Guild** - Combat and stealth bonuses for evil characters
- **Cartographers Guild** - Boosts cartography
- **Librarians Guild** - Boosts inscription and lore skills
- **Culinarians Guild** - Boosts cooking

**Guild Benefits:**

1. **Skill Bonus Ring**: Worn ring provides +10 to +15 to relevant skills
2. **Better Vendor Prices**: Guild members get discounts from guild-affiliated vendors
3. **Special Tools** (Craft Guilds): Access to GM tools for enhancing items with gold
4. **Higher Success Rates** (Craft Guilds): Increased chance of exceptional items
5. **Exclusive Quests**: Some guilds offer special quests and rewards
6. **Social Recognition**: NPCs react differently to guild members

**Which Guild to Choose First:**

- **Tamers**: Druids Guild (ESSENTIAL - +15 to taming and lore makes huge difference)
- **Crafters**: Your primary craft guild (access to GM tools is game-changing)
- **Bards**: Bards Guild (boosts all four bard skills)
- **Combat**: Warriors or Rangers depending on your style
- **Mages**: Mages Guild or Necromancers Guild

> ⚠️ **Important**: The Thieves Guild requires -500 karma to join and enables player theft!

> 💡 **Pro Tip**: Join your primary guild early, then plan future guilds carefully as costs double each time!

#### Guild Interactions & BOD Credits

**Crafting Guilds (Blacksmith & Tailor):**

These guilds have a special credit system tied to Bulk Order Deeds (BODs):

**Checking Your Credits:**
- Say `total` or `credits` to a guild vendor
- They will tell you: "You have a total of X credits with the Guild."
- Only works with Blacksmiths/Blacksmith Guildmasters and Tailors/Tailor Guildmasters

**Claiming Credits:**
- Say `claim [amount]`, `cash in [amount]`, or `redeem [amount]`
- Example: `claim 100` will deduct 100 credits and give you the equivalent value
- Credits are earned by completing Bulk Order Deeds (BODs)
- Credits can be exchanged for rewards, special items, and upgrades

**How BODs Work:**
1. Vendors give you BODs when you buy items from them (random chance)
2. Complete the BOD by filling it with requested items
3. Return completed BOD to any vendor of that type
4. Receive rewards: credits, special deeds, runic tools, clothing bless deeds, etc.
5. Credits accumulate on your character permanently

**BOD Rewards Include:**
- Runic hammers (smithing) - Create magical weapons and armor
- Runic sewing kits (tailoring) - Create magical leather armor and clothing
- Clothing bless deeds - Make any item blessed (keeps on death)
- Powder of Fortifying - Increase item durability
- Special crafting materials
- Guild-specific tools and bonuses

> 💡 **Pro Tip:** Save colored ore and special leather for BODs. Don't sell them to vendors - they're far more valuable for completing BOD requirements!

### Buy Remaining Skills Worth Buying

Nearly all skills are worth having in some capacity. Here are additional recommendations:

- **Armslore** - To identify found weapons and armor
- **Item Identification** - To identify random items you find
- **Taste Identification** - To identify random bottles you find
- **All bard skills** - Because bard spells and skills are good
- **Bushido** - Beware the effects on shields (buy if you're sure)
- **Chivalry** - Very helpful skills unless you're evil
- **Necromancy** - Vampiric form
- **Ninjutsu** - Random skills to make stealth more useful
- **Detect Hidden** - Find traps and chests
- **Lockpicking** - Open locked chests and doors in dungeons
- **Evaluate Intelligence** - Better magery
- **Spirit Speak** - Another way to heal more
- **Parry** - Eventually shields will be useful
- **Poisoning** - Makes spells that cause it better
- **Resist Spells** - Paralyze sucks
- **Remove Traps** - Gives passive resistance to traps, and removes them
- **Stealth** - Moving while hidden can be helpful
- **Tracking** - Find specific pets
- **Veterinary** - Keep pets alive
- **Inscription** - At GM gives boost to spell damage; before that lets you turn free scrolls into GOLD
- **Any other craft skills** you want to do

### Explore the Roads and Cities

- If you're mounted, run away from red names
- Don't approach monsters you're not sure you can take
- Roads are generally very safe
- You're likely to come across randomly spawned dead adventurers (NPCs) and abandoned carts
- Excellent source of loot (sometimes rare)
- Spam random skills and gardening scissors while you run around for gains

### Do Quests from the Boards in the Inn

- Read all the boards inside the tavern/bank area
- One gives quests to retrieve items from dungeons
- You get **PAID**
- **To abandon quest:** Drop the same gold value as the reward onto the board

### Go Sailing

- Going island hopping around Sosaria gives you new places to go
- Many islands have cool features or unlockable extra moongates or dungeons
- All are worth a visit
- Or you can just go fishing once you reach 60 fishing from land
- **Deep sea fishing:** Good way to find powerful items, but also to get killed if you're not prepared

### Pledge to Good or Evil in Dungeon Time

- **For normal mode characters (unrestricted):** You can pledge yourself to good or evil
- **⚠️ Warning:** This is a **permanent choice** and will LOCK your karma either above or below 0
- **How to pledge:** Journey to the dungeon known as "Time Awaits" and seek the Lord of Time in its depths
- **⚠️ Don't attack him** - He gets shirty about that
- Once pledged, you accumulate Balance points
- Your Balance score adds to the Balance total globally
- Spend balance points at the time lord on various things
- **Check global balance:** Inside the bank, colored pillar stands in the middle (red = evil, green = good)
- Double-click to check your personal score and who the champion of good or evil is
- **Champion benefits:** Can recall at will without fear of loss, has no death penalties

#### Balance System Details

**Checking Rankings:**
- Type `[Ratings` command (only available if pledged)
- Opens the Avatar Leaderboard showing top players
- Displays either Order (good) or Chaos (evil) rankings based on your pledge
- Shows champion status and top 13 players by Balance points

**Balance Shards:**
- Special items dropped by certain creatures
- Can only be used by pledged characters
- Creates a portal based on your alignment:
  - **Good Portal** (BalanceStatus > 0) - Created for Order pledged
  - **Evil Portal** (BalanceStatus < 0) - Created for Chaos pledged
- Cannot be placed in cities, safe zones, or easy dungeons
- Provides strategic advantages in dangerous areas

**Earning Balance Points:**
- Complete quests and objectives aligned with your pledge
- Defeat enemies of the opposite alignment
- Participate in facet-specific activities
- Points contribute to global Balance and your personal ranking

**Global Balance Effects:**
- The pillar in banks shows current world balance (red/evil vs green/good)
- Global balance affects:
  - Monster spawn difficulty
  - Vendor prices in certain areas
  - Death penalties severity
  - Champion spawn availability
  - Special event triggers

> 💡 **Tip:** Champion status is highly competitive but offers significant benefits. Check the leaderboard regularly with `[Ratings` to see your progress!

---

## Gameplay Systems

### General Tips

#### Basic Game Systems Everyone Needs to Know About

**Keep an eye on your negative karma:**
- Various things will lower your Karma (murdering people, raising the dead, begging, etc.)
- If you hit **-4k karma**, the guards will attack you on site
- You are now **EVIL** - unless you're looking to become an outlaw, Social Outcast, or Dark Lord, be wary

**Random portals in the wild:**
- Just like other random things, generally they're dangerous
- If you see a glistening shimmering portal of magical energy, would you dive right in?
- If yes, then disregard this advice and get on adventuring!

**Read everything you find:**
- Considerable amount of lore and **VITAL clues** to the game's larger mysteries
- Found in books or scrolls (random, on bookshelves via monocle, or looted from monsters)
- Some facets and most powerful items can only be found this way

**Listen to NPCs:**
- NPCs chat and will discuss subjects that can't be found anywhere else
- Hidden treasure, puzzle pieces to quests, funny details about character backgrounds
- Sit around a campfire with friendly NPCs

**Fake News:**
- In direct opposition to the above—there is a considerable amount of **fake news** in Adventures
- Many hints, texts, books, scraps of paper, and NPCs are wrong, mistaken, or outright lies by design
- Those that can see through the mists of fate may be able to help you see the truth

**Unidentified doesn't mean it's good:**
- A good 20-30% of items you find will be unidentified
- When you use them, you can double-click to attempt to identify them
- **Bottles:** 100% worth identifying (only source of some rare upgrade materials, but also likely to poison you)
- **Scrolls:** Can turn into spell scrolls or blow you up
- **Wands:** Can blast you
- Make sure you're safe, healthy, and have a good way of curing major poisons before you start experimenting

**Dungeons have traps and treasure (Normal and above difficulty):**
- Use **Detect Hidden** often in dungeons (like all the time, hotkey it)
- Places can be crawling with hidden treasure chests and traps
- Traps appear as bright lights on the floor when revealed
- **Remove Traps** not only allows you to remove traps but gives passive resistance
- **Other trap-fighting options:** 10-foot pole, casting remove traps, high resists, luck, or resisting spells
- **⚠️ Traps are NO JOKE:** Can instantly destroy equipment, loot in your bag, kill you, or teleport you somewhere far more dangerous

---

## Notable Changes from Standard UO

### World

**Greatly accelerated skill gain in dungeons:**
- All non-craft related skills get much faster gains in dungeons
- More dangerous dungeons give faster gains (up to 200% faster)
- Rather than macro those skills, why not get out and use them?

**Random Vendor desires and stock:**
- NPC vendors in towns will at different times sell and want to buy different things
- A blacksmith might not want your 30 pairs of chainmail trousers now, but a few hours from now he could
- If you can wait to sell them, or can't find what you want, try again in a few hours

**Random Encounters:**
- As you travel, you may find yourself experiencing a random encounter
- Sometimes safe: Traveling healers, banks, or drug dealers
- Sometimes dangerous: Orc warbands, portaling demons, red/blue PK gank squads
- More common and dangerous on later facets but can appear anywhere that's not explicitly a safezone
- ⚠️ **Be careful when AFK** in dungeons, mining caves, or along the road
- ⚠️ **BEING HIDDEN DOES NOT PROTECT YOU FROM THESE ENCOUNTERS RELIABLY**

**NO item decay:**
- Items and player corpses that end up on the ground stay on the ground **forever**
- You can set up tables, chairs, fireplaces, forges, rugs, create art, stockpile things
- ⚠️ **Littering is a potential issue** - Please avoid emptying your bank onto the floor
- If something could be good for another player, put it in the noob chests along the west wall of the bank

**Random Traps and Chests:**
- In dungeons, random traps and chests spawn
- Random chests will despawn and move to new locations over time
- **BUT** if you pick it up and move it, then it will stay there forever

**Semi-random animal/monster stats:**
- Some monsters will have far greater stats than others around them
- You may find a bear that's 3-5 times stronger than another right next to him
- Always be on your guard when going into combat

### Character

**STR and HP:**
- Unlike standard UO, each point of strength grants **more than 1 HP**
- The gain is modified by the next point

**DEX and Bandaging:**
- Dexterity has **far less effect** on bandaging speed than usual
- By stacking dexterity, you'll see far less benefit
- Bandage speed will increase but you'll need hundreds of DEX to reach same speeds
- With exceptional gear easily available, hundreds is not as much as it might seem
- Many healing options available (alchemy, spirit speak, chivalry, new spell sets, vampire form, etc.)

**INT and Follower Slots:**
- Many pets take a different amount of slots than you're used to
- Value is dynamically based on how strong they are (stronger = more slots)
- Maximum of **7 slots for a single strong pet** (can be ridiculous at the very top end)
- **Default in Adventures is 2**, but maximum is **16**

**Pet Slot Gains:**
- **4x from RAW INT** at 60, 80, 100, and 120
- **3x from Herding skill** at 80, 100, and 120
- **2x from combination** of Animal Lore + Taming + Vet skill at 80 and 120
- **5x from reward deeds** gained rarely and at random from selling animals to animal brokers (VERY expensive animals have higher chance)

**No Virtues:**
- Adventures has **no virtues system**
- None of the abilities that they perform are available
- Most noticeably affects Bushido (cannot honour opponents)

**Power Scrolls:**
- Can be acquired as usual from champion spawns
- Can also be **purchased directly for gold** from vendors in special places
- Expensive but available up to 120 from vendors
- Can get 125 scrolls from champions (items can give you that 5-point bonus)
- Craft power scrolls are acquired via BODs

**Offline Training Manuals:**
- Each character starts with **2 training manuals**
- When you double-click them, it sets you to read the book when you next log out (needs doing every time you log in)
- Up to 70 points, you'll get opportunities to gain skill points over time the longer you're logged out
- When you log back in, if you're still below 70, you'll receive faster gains for a short period
- New books can be purchased from Sarth library or found as loot
- Players can craft these books via inscription
- There are also books that go to GM and VERY rare books that go above this

**CAMPING!!:**
- Camping is officially the **most useful skill in the game now**
- Reduces the rate you need to eat and drink
- Can let you **insta-teleport** to a special tent area (safe spot with vendors and bank)
- Tent area is **SHARED** - can be used to meet other players for exchanges
- Don't rush learning the skill though—it's the best way to gain all of the stats
- Just slowly learn a bit as you climb the stats ladder **EVERY 15 minutes**
- It won't take long—it levels FAST

**Camping Skill Breakpoints:**

- **Below 66 skill**: Can only use tents in safe outdoor areas (villages, roads)
- **66-89 skill**: Can use tents in all outdoor areas including dangerous wilderness
  - Unlocks upgraded tent area with better vendors
- **90+ skill**: Can use tents in DUNGEONS as a pause button or safe logout spot
  - Creates a private dungeon room teleporter
  - Absolutely game-changing for dungeon exploration
  - Essentially gives you a portable safe room anywhere

**Tent System Details:**

- Tents have **limited charges** (uses) before wearing out
- Each use provides camping skill gains
- Tent locations include:
  - **Basic Tent Area** (0-65 skill): Small safe area with basic vendors
  - **Upgraded Tent Area** (66+ skill): Larger area with more vendors and banking
  - **Dungeon Room** (90+ skill): Private safe room in dungeons, shared meeting spot
- The tent areas are on a shared map—you might see other players there!
- Great for trading with other players in a neutral, safe location
- Double-click tent or have it in backpack to use when you have sufficient skill

**Forensic Evaluation:**
- When leveled, increases the amount of hides and other skinning resources you can get
- If you use a surgeon's knife and carry jars, allows you to take other parts for... reasons

### Items

**Diminishing Returns:**
- Stats are subject to diminishing returns
- Most noticeable on armour resists but affects all stats
- Example: Having 70 points in physical resist will not get you to 70 actual resist
- This is because of easy availability of powerful gear and multitude of ways to enhance it
- You can eventually get perfect set with maximum of every attribute, but this slows the process

**Item Enhancement:**
- Equipment can be enhanced in a variety of ways
- **Most basic:** GM guild tool - GM crafters can directly spend GP to add specific stats to items
- **GM crafters can also:**
  - Apply rare oils (found from random bottles in loot) to replace metal qualities
  - Use ingots themselves to enhance an item, editing its base stats to those of the new material
- **From rare sources:** Acquire deeds that directly add stats to items

**Blessed Items:**
- Most items you would normally expect to be blessed are **not now**
- Some are, and all can be with bless deeds
- Bless deeds can be acquired via BODs

### Followers

**Pets:**
- Pet loyalty drops as they use special abilities and if you issue lots of commands fast
- Once their loyalty drops, they may ignore commands
- **⚠️ ANY AoE ATTACK THEY DO GRADUALLY DOES MORE DAMAGE TO YOU WHEN IT IS USED**
- It will always do some damage, but lower loyalty increases this till it reaches 100% of the damage enemies take
- Pets can also be provoked by NPCs (red town guards especially) to attack you
- This AoE damage can also affect your other pets and can turn them against each other **OR EVEN YOU**
- If this happens, your pet can go gray for a period
- Pet loyalty can be increased by feeding the pets (place food on floor and run them over it)
- You can also stable them for a loyalty boost
- **Best places to gain taming:** Snowy areas
- **Good way to gain taming past 45ish:** Take pets adventuring as they level up

**Henchmen:**
- Low-powered but interesting human and monster followers
- Can buy from inn for 5k or find in dungeons and rescue from cages
- Range from wizards and warriors to orcs, ratmen, or Ophidians
- Generally not very strong
- **Limited to two of them at a time**
- Take up **one pet slot**
- Caster ones can be quite powerful
- Some archer ones dodge away from enemies
- Require paying per minute that they fight for you (accept items or stacks of coins)
- When they die, return to item form in your bag to be resurrected at a healer

**Squires:**
- Can be acquired inside the inn area of the tavern from squire-related vendors
- Once purchased, they are complex levelable followers
- Should also buy a manual and a squire spyglass to help manage them
- At first quite weak but can be leveled to be extremely strong
- Can use healing and chivalry to protect you
- You can equip them and use them to carry loot (essentially making them a pet player)
- **Take 3 slots**
- When stabled **AT THE INN**, get converted to a pigeon-like item in your bag
- The manual will guide you more on squire care

**Squire Commands:**

Squires respond to many voice commands, making them incredibly versatile:

*Basic Commands:*
- `stop`, `stay`, `follow`, `follow me`, `come`
- `guard`, `guard me`, `attack`, `kill`
- `restyle` - Change appearance

*Equipment Management:*
- `dress`, `undress` - Equip/unequip armor
- `arm`, `unarm` - Equip/unequip weapons
- `mount`, `dismount` - Handle mounts
- `quiver` - Manage ammunition
- `create set one/two/three` - Save equipment sets
- `equip set one/two/three` - Load saved equipment sets

*Utility Commands:*
- `backpack` - Access squire's inventory
- `stats` - View squire statistics
- `skills` - Check squire skills
- `grab`, `grab all` - Pick up items
- `loot`, `loot all` - Loot corpses
- `unload` - Unload carried items
- `list` - List inventory

*Combat Support:*
- `heal` - Heal you or others
- `throw` - Throw weapons/potions
- `throw explosion` - Throw explosion potions
- `poison` - Poison weapons
- `weapon ability`, `weapon ability one/two` - Use weapon special moves

*Chivalry Spells:*
- `consecrate weapon`, `divine fury`, `dispel evil`
- `enemy of one`, `holy light`, `noble sacrifice`
- `cleanse by fire`, `close wounds`, `remove curse`

*Bushido Abilities:*
- `confidence`, `lightning strike`, `evasion`
- `counter attack`, `momentum strike`, `honorable execution`

*Necromancy Spells:*
- `pain spike`, `poison strike`, `wraith form`
- `curse weapon`, `wither`, `lich form`, `vampiric embrace`

*Bard Skills:*
- `play music`, `provoke`, `discord`, `make peace`

*Other Skills:*
- `steal` - Use stealing skill
- `lockpick` - Pick locks
- `hide` - Become hidden
- `spirit speak` - Use spirit speak
- `meditate` - Meditate for mana

*Potion Management:*
- `drink agility`, `drink strength`, `drink refresh`
- `drink cure`, `drink poison`, `drink health`

*Social:*
- `be quiet` - Stop talking
- `talk again` - Resume talking
- `change my nickname` - Change what squire calls you
- `change your nickname` - Change squire's nickname
- `change title` - Change squire's title
- `rename yourself` - Rename the squire

*Advanced:*
- `tithe` - Manage tithing points
- `check tithing points` - View tithing
- `set team` - Set team alignment
- `switches` - Toggle various settings

> 💡 **Pro Tip:** Squires can be trained in multiple skills and become incredibly powerful late-game companions. They're essentially a second character!

---

## Making Money

Making money is a great way to achieve basically everything in-game, but without a real in-game economy, it can be hard to turn your rares, drops, and other items into cold hard cash.

**Note:** Copper, silver, piles of gemstones, and crystals can all be put into the bank and double-clicked to turn them into gold.

> 💡 Many things will sell better to members of a guild you are part of (e.g., blacksmiths guild masters will give better prices if you're a smith yourself)

### Peacefully

#### Go Mining, Make Metal Armor

- With less than 20gp you can buy a pickaxe and get started
- If you're prepared and have a pack animal stabled or can tame an elephant, you can make a stockpile of ingots
- Those ingots can be turned into gold via vendors by making armor quite well
- Weapons don't tend to be worth a lot by ratio to ingots consumed
- There are a lot of blacksmiths and armor smiths around the world (usually together)
- If one doesn't want your huge heap of chainmail, another won't be far away

#### Kill Things, Skin Them, and Make Leather Armor

- Once you have enough tailoring to make leather trousers and especially robes
- Use animal skin to make some money
- Use giants and other stronger creatures to make better armor to sell for even more
- Makes a good boost to the money you'll already make dungeoning
- Can be combined with many other money-making methods as you run about

#### Go Lumberjack, Make Shields

- Although slower and less profitable than smithing
- Trees are more plentiful in general
- Ideal if you're SO poor you can't even buy a pickaxe but do have an axe
- Good opportunity to tame wildlife and sell them
- **In my experience:** Easiest to make and best selling wood items are shields

#### Tame Animals and Sell Them

- Animals can be sold to an animal broker
- All sales have a very small chance of gifting insta-tame, insta-bond deeds that give you an extra pet slot permanently
- More difficult animals sell better (elephants, mammoths, polar bears all good options)
- ⚠️ **Beware:** Animals that can aggro when you try to tame them (many gray animals will do this and pull in nearby similar animals)
- **Tip:** Save yourself a slot for more animals to sell if you gain ninjutsu and use animal form to become a llama or get magic speedy boots

#### Shoppees

- If you own your own home and have over 50 skill in a craft or related skill
- Acquire a Shoppee from the tradesmen in towns
- Essentially a furniture item that you can get mini quests from
- When locked down, fill them with materials and tools (e.g., ingots and smith hammers)
- Over time they passively generate missions
- Missions can be failed but get less likely to fail as you get better skill
- Failure takes money away from the "pot"; success adds to it
- They also generate some fame
- If all jobs are done, return later for more
- When you need/want to, you can take the pot out as a cheque
- Shoppees also gain skill in the craft and are a good non-macroish way to get to GM and beyond
- A character can have multiple Shoppees from all different crafts

#### Vendors

- When placed in the bank or a player home, vendors can and will randomly sell items to pretend players passively if items are reasonably priced (i.e., cheap)
- The higher the stats on the item, or more charges the wand has, the better
- Sometimes items will remain unsold for ages, or sell immediately for much higher prices
- Pretend players will almost always pay better than a town's vendor will (sometimes drastically so)
- **To prevent this:** Place a bag that's not for sale, with another bag that's not for sale inside it—items inside the second bag will not be looked at by pretend players

#### Nick Stuff

- If your morals are loose, Stealing can be used to generate cash from NPCs
- Stealing can at higher levels generate powerful items or even artifacts that you can sell to vendors
- If you're part of the thieves guild, you can steal from other players
- ⚠️ **Beware:** While they can't attack you to kill you, thieves can be double-clicked to freeze them for a limited time
- The capturing player doesn't need to be the player stolen from
- When frozen, the capturing player can take items freely from the thief's backpack for a short time
- The thieves guild also offers quests to make money

### Violently

If you are equipped and raring to go, making money need not be just craft-related.

**Vendor Trash Worth Taking:**

- **Jewelry / Gemstones:** All jewelry is worth picking up for vendor sale; 95% of it will be worth selling (autoloot feature will pick up the good stuff)
- **Weapons:** Weapons with any amount of the "Damage Increase" stat are worth a lot more to vendors than anything else
- **Armor:** Any armor with the "Durability %" stat seems to sell to vendors better than other armor
- **Potions:** Stacks accumulate; normally always worth using or drinking, but things like guards or NPC PKs will drop stacks of them

**Mini Quest Trash:**

- **Decorations / Furniture:** Paintings, some furniture like clocks and carpets, and even sunken treasure can be sold to an antique dealer (heavy however and often doesn't sell for enough)
- **Rare cloth, books, etc.:** Sometimes you'll find items that look useful but have no stats/charges—when double-clicked they'll tell you they can be sold to somebody (use item identification to find out who)

---

## Evil Playstyle

### The Biggest Overall Note

You become evil at **-4k karma** or **3 murder counts**. Before that, you're a scallywag or rascal officially.

- **-4k/3 counts means:** Blue guards will attack you on site (they have bolas, so this can be a huge pain for travel)
- **Murder counts take 40 hours of in-game time to decay**

### Becoming Evil

As Dungeon Keeper told us years ago, "it's good to be bad." If the pull of the wild side suits you, here are some tips:

- **Begging** will slowly give you negative karma and is a weird form of peacemaking when used on enemies
- **Necromancy** will tank your karma hard and fast but is also an excellent source of powerful magic
- **Stealing** will lower your Karma; at -500 karma you can join the thieves guild and steal from players
- **Killing purple named NPCs** will lower your karma
- **Killing children or Blue named NPCs** will land you a murder count and lower karma
- **Looking through corpses** of things you didn't kill is also frowned upon
- If a blue attacks you for something you did wrong, it's still bad to kill them in self-defense

### Stopping Being Evil

To return to the world of the Blues, you'll need to rid yourself of bad vibes (karma) or murder counts.

**To Reduce Murder Counts:**
- Wait it out (40 hours in-game each)
- Pay a specific NPC to remove them (I believe for 10k per kill)
- Complete a secret, rare to find, and long quest chain that appears randomly in dungeons (will remove all murder counts)

**Improve Your Karma:**
- Kill grey monsters
- Kill orange named monsters
- Kill red named monsters
- Hand player bones to gravediggers

### Where to Go Now You Are Evil

Now that you're red or generally horrible:
- Town guards are less safe and cuddly and more deadly
- Stay away from blue towns unless you're up to mischief
- You'll need to find yourself some new friends and go to new places

**The primary place for the truly evil:**
- Easy to see on the map—it's got the word **CHAOS** literally written on it
- If you're still struggling, take the moongate to central and run west
- ⚠️ **Beware:** You need to be a murderer as well to be welcome here I believe

**Other options:**
- There is a cave near where the town of Umbra lies that welcomes all
- Explore the mountains east of Sarth Abbey along the river to discover it

### Pros of Being Evil

- Necromancy and karma-damaging skills can be freely used
- Access to an evil-only facet
- You can do something practical about the orphan problem in the lands
- Cool Wings are available for your back
- Become a Sith
- Less moral conundrums in your daily life
- Generally less competition for champion status (currently at least)
- Vendor-bought Corrupted Moongates

### Cons of Being Evil

- No easy access to chivalry (similar secret skill/spell set exists)
- No access to most towns (without force)
- Less access to vendors means when something isn't in stock it's more of a pain
- People may think you're a bad person till they get to know you

---

## Potential Long-Term Goals

So now you are established, you've been around a bit, and maybe are looking towards your future. Here are some alternatives to the rat race.

> ⚠️ **Be aware:** There are many things to do not mentioned at all in this guide. Feel free to ignore this list entirely and explore at your own rate.

### Relax

#### Become a Craftsman

- Many of the crafts are a lot less macro/boring to level than you might think now
- Get an offline training manual
- Once you hit 50, get a Shoppee and keep going till you're a GM or beyond
- Then do BODs—the SKY is the limit (BODs can get you very powerful rewards)
- Crafters can make fun new things, and many crafts have new items available

#### Build a House

- Got money? Get a place to live
- Get more? Get a bigger place, or a second place, or a castle
- If it's looking drab, go out and get some decorations from dungeons using the stealing skill
- Don't have stealing? Buy an offline training book to avoid the karma loss
- If you're living in a 9x9 box house, get rid of it and build something people will see and think "wow, what a nice bit of scenery"

### Expand Your Mind

#### Become a Monk

- Custom spell system that requires GM meditation and focus
- Most notably gives you access to a **monk's robe**—a customizable item that is EXCELLENT when you're a GM wrestler
- **To become one:** Go to the monk vendor in Brit and buy the book, open it and **TURN THE PAGE**
- Each spell requires you to go to a special place with a scroll and pen to get it
- Focuses on martial arts enhancement and spiritual abilities

#### Become a Jester

- Custom spell system that gives you access to playing cards as thrown weapons and exploding balloons
- Visit the jester in Brit castle to hear more about this fantastic fun way to kill things with jolly japes
- Fun, chaotic playstyle with unique weapons and effects
- Great for entertaining while deadly

#### Become a Priest

- Custom spell system available from the priest in Brit castle
- Requires you to kill A LOT of vampires and to stake their corpses to get in
- Each spell requires you to journey the lands to pay homage to the priests of the past
- Gives new healing and restoration options largely
- Holy magic focused on undead destruction and protection

#### Collect the Bard Spells

- Custom spell system that you can just buy from bard shops
- Powered off of the bard skills
- Has some truly excellent group buffs and spells that can't be found anywhere else
- Some of the spells are very rare
- Essential for support-oriented characters

#### Master the Jedi Arts

- Custom spell system based on psychic and force powers
- Requires focus, meditation, and mental discipline
- **10 Jedi Spells Available:**
  - **Force Grip** - Telekinetically grip enemies
  - **Mind's Eye** - See hidden and invisible
  - **Mirage** - Create illusions
  - **Throw Sabre** - Throw your weapon like a lightsaber
  - **Celerity** - Enhanced speed and reflexes
  - **Psychic Aura** - Mental protection
  - **Deflection** - Deflect projectiles
  - **Soothing Touch** - Heal through the force
  - **Stasis Field** - Freeze enemies in place
  - **Replicate** - Create duplicates
- Requires special book and training
- Light side force powers

#### Embrace the Syth Path

- Dark side equivalent to Jedi system
- Similar power structure but with corrupted, evil variants
- Focuses on domination, fear, and destruction
- For evil-aligned characters only
- Requires negative karma and specific training

#### Learn Mystic Arts

- Additional mysticism spell set
- Combines elements of nature magic with arcane power
- Bridges gap between druidic and traditional magic
- Unique utility and combat spells

#### Herbalist Magic

- Nature-based spell system
- Focuses on plant life, earth magic, and natural forces
- Includes spells like:
  - **Shield of Earth** - Earth-based protection
  - Various plant growth and manipulation spells
  - Nature's wrath abilities
- Great for characters who want druid-like powers

#### Death Magic (Expanded Necromancy)

Beyond standard necromancy, Adventures offers 16 additional death magic spells:

**Offensive Death Magic:**
- **Spectre Shadow** - Summon shadow spectre
- **Hell's Brand** - Mark enemies with hellfire
- **Retched Air** - Poison cloud of death
- **Wall of Spikes** - Create barrier of bone spikes
- **Necro Poison** - Enhanced poison magic
- **Hell's Gate** - Summon demons from the abyss
- **Pain Spike** - Inflict excruciating pain

**Utility Death Magic:**
- **Mana Leech** - Drain mana from living
- **Undead Cure Poison** - Cure poison on undead
- **Graveyard Gateway** - Teleport via graveyards
- **Undead Eyes** - See like the undead
- **Necro Unlock** - Unlock via death magic
- **Phantasm** - Create ghostly illusions

**Transformation Death Magic:**
- **Ghost Phase** - Phase through objects
- **Ghostly Images** - Create ghost duplicates
- **Vampire Gift** - Enhanced vampiric powers
- **Blood Pact** - Create blood magic contracts

> ⚠️ **Warning**: Using death magic tanks your karma quickly. Be prepared for consequences!

> 💡 **Tip**: Mix and match spell systems! You're not limited to just one custom system.

### Adventure

#### Start the LONG Road to Archmage

- If you really like new spells, you're going to love (or hate) this one
- Take 500gp and give it to a librarian to receive a spell research pouch
- This allows you to start a crazy long slog towards an eventual 8 full spell sets of 8 spells
- Some are pretty useless (also available from other systems), others are interesting, amazing, funny, and in a few very limited instances at the very end extremely powerful
- **⚠️ Get started early**—this is a LONG road to travel
- Will take you all over the place and will be a main quest to have alongside your own other goals
- **You can also use this system to get mage or necromancy spells, but you shouldn't**—the process is the same and essentially wastes time on that journey for something you can get normally

#### Tribute Quests

- Around the world, in towns and cities, or hidden in dungeons for evil players, there are NPCs with green names
- These NPCs will, for an amount of fame, an item found from a dungeon boss, and some gold, give you a customizable item that has some skill bonuses on it related to their profession
- Examples: A ninja will give ninjutsu skills; a wizard will give wizardry skills
- The stats and slot of this item will be yours to choose
- Good options are slots you can't normally get good items for (like shirts or skirts)
- All tribute quest givers will want the same item, and once a quest is handed in to any of them, it will change

#### Sage Quests

- In towns there are NPCs called sages
- These sages can give you rumours as to the location of specific artifact items, chosen from a book
- **To get the book and start:** Hand them between 5-20k gold (the more you pay, the better quality of the rumour)
- Once selected from the book, you'll be given a scrap of paper with a location on it
- **READ the scroll** and it will direct you to a dungeon to search for the item
- In the dungeon there are chests atop stone pedestals
- Some will be regular puzzle boxes, but **ONE of them** will be the objective for the quest
- The specific one will change, so if you get another quest to the same place, it could be any of the pedestal chests
- Sometimes pedestals themselves will move around to other fixed locations
- **To abandon a quest:** Give the sage money again

#### Seek Legendary Items

- Legendary items are some of the most powerful items you can get
- They are levelable items that start out basic but as they level slowly will surpass even the most powerful artifacts with customizable chosen stats
- While through upgrade processes better items can be created, it's time-consuming, EXPENSIVE, and rare to be able to get the materials to surpass these
- They can fill any slot or come in the form of any equippable item
- **To get them:** You must be as famous and glorious/villainous as you can possibly be, and have 20k
- Then seek out the **Hall of Legends** to acquire each legendary item

#### Beyond Sosaria - Unlock the Other Facets

- Out there are numerous other lands, each more dangerous and better rewarding than Sosaria in turn
- Each with weird and wonderful people to meet, be lied to by, steal from, and buy stuff from
- Dungeons to explore, loot, and add tombstones to
- Monsters to greet, defeat, tame, or run away from
- Accessed via dungeons, quests, clicking on mysterious objects, and talking at scenery
- There's no greater way to expand your horizons than getting out there and throwing caution to the wind

#### Investigate Skara Brea

- A destroyed island town holds the key to unraveling the mystery of one of the best and most rewarding experiences of the whole Adventures game
- While technically achievable by a brand new player without any skills, I'd recommend taking this challenge on once you're feeling confident and well-equipped
- Otherwise you're likely to need to macro a lot to skill up before you can complete it
- **Once you buy the ticket, you're on this ride baby**
- This is a real man/woman/person's quest, and like nothing your average Ultima Online quest
- Takes time, dedication, and puzzle solving to complete
- You likely **WILL die a lot** before you're done, but it is an **EXCELLENT experience** and really shows the best things the game has to offer

---

## Quick Reference

### Essential Commands Cheat Sheet

| Purpose | Command/Speech | Notes |
|---------|---------------|-------|
| **Help System** | `[Help` or `[HelpInfo` | View all commands |
| **Check Location** | `[Where` | Shows coordinates, region, facet |
| **Check Time** | `[Time` | Server local time |
| **Get Unstuck** | `[Stuck` | Teleport to town |
| **View Rankings** | `[Ratings` | Balance leaderboard (if pledged) |
| **Open Bank** | Say `bank` near banker | Access your bank |
| **Buy from Vendor** | `vendor buy` or just `buy` | Opens buy menu |
| **Sell to Vendor** | `vendor sell` or just `sell` | Opens sell menu |
| **Check BOD Credits** | `total` or `credits` | At guild vendor |
| **Claim BOD Credits** | `claim [number]` | Redeem guild credits |
| **Control All Pets** | `all guard`, `all follow me`, `all kill`, `all stop` | Essential pet macros |
| **Stable Pet** | Say `stable` near stable master | Store your pet |
| **Claim Pet** | Say `claim` near stable master | Retrieve your pet |
| **Train Skills** | `train [skill name]` | At appropriate NPC |
| **Ask Directions** | `where is the [location]` | NPCs give directions |
| **Lock Down Item** | `i wish to lock this down` | In your house |

### Recommended Macros

Create these macros for maximum efficiency:

1. **Town Interaction:** `Vendor Buy Bank Guards!`
   - Opens shops, accesses bank, calls guards if needed

2. **Pet Control:**
   - `all guard`
   - `all follow me`
   - `all kill`
   - `all stay`

3. **Vendor Interaction:**
   - `Sell`
   - `Buy`

### Critical Early Game Items

Always carry these items when leaving town:

- ✅ Bandages (healing)
- ✅ Scissors (for making bandages)
- ✅ Pestle and mortar (alchemy)
- ✅ Glass bottles (for potions)
- ✅ Mage book with essential spells
- ✅ Reagents (bag of 50 each)
- ✅ Dagger (for camping/kindling)
- ✅ Lesser cure potions (10+)
- ✅ Lesser heal potions (10+)
- ✅ Food and water

### Stat Gain Schedule

Set a timer for every 15 minutes to gain stats:

1. Equip dagger
2. Double-click dagger
3. Target a tree
4. Collect kindling
5. Double-click kindling repeatedly
6. Stop after gaining 1 of each stat (STR, DEX, INT)
7. Wait 15 minutes and repeat

**Skills that grant stat gains:**
- **STR:** Camping, Mining, Lumberjacking
- **DEX:** Camping, Alchemy, Hiding
- **INT:** Camping, Forensic Eval, Music

### Pet Slot Calculation

You start with **2 follower slots** and can gain more:

- **+1 slot at INT:** 60, 80, 100, 120 (4 total)
- **+1 slot at Herding:** 80, 100, 120 (3 total)
- **+1 slot at Taming+Lore+Vet combined:** 80, 120 (2 total)
- **+1 slot from deeds:** Rarely from animal broker sales (5 maximum)

**Maximum possible:** 16 follower slots

### Death Loss Prevention

To minimize death loss:

1. ✅ Get revived by another player (costs them Balance points)
2. ✅ Recall/gate back to your corpse quickly
3. ✅ Become Balance champion (no death penalties)
4. ⚠️ Auto-rez increases loss (but first few are free)

### Essential Vendor Locations

| Vendor Type | What They Sell | Keywords |
|-------------|---------------|----------|
| **Provisioner** | Food, drinks, tools, backpacks | `vendor buy` |
| **Mage** | Spells, reagents, books | `vendor buy`, look for "bag" |
| **Blacksmith** | Weapons, armor, ore | `vendor buy` |
| **Tailor** | Cloth, leather, scissors, sewing kits | `vendor buy` |
| **Alchemist** | Potions, bottles, mortar & pestle | `vendor buy` |
| **Stable Master** | Pet storage | `stable`, `claim` |
| **Banker** | Bank access | `bank` |
| **Animal Broker** | Buys tamed animals | Drag animal to them |

### Skill Training Costs

- **0 to 30:** Can buy from NPCs (varies by skill, ~200-2000gp)
- **30+:** Must gain through use
- **⚠️ Never buy Camping to 30** - gain it through stat gains instead
- **Tip:** Join profession guilds early for skill boost rings

### Dungeon Difficulty Guide

| Difficulty | Death Loss | Skill Gain Bonus | Traps/Chests |
|-----------|------------|------------------|--------------|
| **Easy** | Minimal | 0% | No |
| **Normal** | Moderate | 50% | Yes |
| **Hard** | Significant | 100% | Yes |
| **Very Hard** | Severe | 150% | Yes |
| **Extreme** | Maximum | 200% | Yes |

**Tip:** Skills level 50-200% faster in dungeons!

### Karma Reference

- **+10,000 to 0:** Good standing, guards protect you
- **0 to -4,000:** Scallywag/Rascal status
- **-4,000 or 3 murders:** EVIL - guards attack on sight
- **⚠️ Murder counts take 40 hours in-game to decay (each)**

**How to lose Karma:**
- Begging
- Necromancy
- Stealing
- Killing purple/blue NPCs
- Looting corpses you didn't kill

**How to gain Karma:**
- Kill gray/orange/red monsters
- Hand player bones to gravediggers
- Complete good-aligned quests

### House Ownership

- **No limit on houses** (multi-house allowed)
- **No decay** - items stay forever
- **⚠️ Don't litter** - put spare items in bank noob chests
- **Custom designs** encouraged
- **Use house commands:** `i wish to lock this down`, etc.

### Special Systems Quick Access

| System | How to Access | Location/Method |
|--------|---------------|-----------------|
| **Camping Tent** | Use Camping at 90+ skill | Creates safe portable area |
| **Spell Research** | Pay librarian 500gp | Start Archmage quest line |
| **Legendary Items** | 20k gold + max fame | Hall of Legends |
| **Sage Quests** | Pay sage 5-20k | Get artifact location rumor |
| **Tribute Quests** | Find green-named NPCs | Turn in boss items + fame + gold |
| **BOD System** | Buy from vendors | Random chance to receive BOD |
| **Shoppees** | Buy from tradesmen | Passive crafting in your house |
| **Study Books** | Buy from library | Offline skill training |

---

## Resources

### Weblinks

**Discord:**
- For online server related chat, help, and general fun times
- https://discord.gg/C2MzjCUG

**Reddit:**
- New and shiny, spread the good word!
- https://www.reddit.com/r/UltimaAdventures/

**Reddit Old:**
- For the original Odyssey/adventures shared content (may be out of date for adventures)
- https://www.reddit.com/r/uodyssey/

**ServUO - Where to Download:**
- Please leave a review to help others decide if this is for them!
- https://www.servuo.com/archive/ultima-adventures-a-full-featured-content-packed-offline-online-server.1374/

---

## Community

Please join us on the social spaces to help bring this wonderful project to life!

- Join the Discord for real-time chat and help
- Share your experiences on Reddit
- Leave reviews and feedback to help others discover Ultima Adventures

---

**Last Updated:** 2025-11-05  
**Guide Version:** 3.0 (Complete Commands & Systems Reference)

**What's New in v3.0:**
- ✅ Complete player commands reference
- ✅ Comprehensive NPC speech keywords guide
- ✅ Pet, boat, and house command documentation
- ✅ Expanded guild system with BOD credit details
- ✅ Enhanced Balance system mechanics
- ✅ Quick Reference section with cheat sheets
- ✅ Detailed vendor interaction guide
- ✅ All player-accessible systems documented

---

*This guide is a community resource. If you find errors or have suggestions for improvements, please contribute to the community!*

