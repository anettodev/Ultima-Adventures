using System;
using Server;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Mobiles.Data;
using Server.OneTime;

namespace Server.Mobiles
{
	/// <summary>
	/// Manages the new character introduction play sequence, including actor coordination,
	/// dialogue progression, and player interaction for character setup choices.
	/// </summary>
	public class PlayDirectorNewChar : BaseCreature, IOneTime
	{
		#region Constants

		// Actor positions
		private static readonly Point3D ACTOR1_START_POS = new Point3D(1953, 1327, 0);
		private static readonly Point3D ACTOR1_MOVE_POS = new Point3D(1953, 1325, 0);
		private static readonly Point3D ACTOR2_START_POS = new Point3D(1958, 1323, 0);
		private static readonly Point3D ACTOR2_FRONT_POS = new Point3D(1963, 1323, 0);
		private static readonly Point3D ACTOR2_EXIT_POS = new Point3D(1959, 1332, 0);
		private static readonly Point3D ACTOR3_START_POS = new Point3D(1908, 1321, -50);
		private static readonly Point3D ACTOR3_GATE_POS = new Point3D(1964, 1325, 0);
		private static readonly Point3D ACTOR3_MOVE_POS = new Point3D(1962, 1325, 0);
		private static readonly Point3D ACTOR3_PLAYER_POS = new Point3D(1961, 1323, 0);
		private static readonly Point3D PLAYER_START_POS = new Point3D(1961, 1318, 0);
		private static readonly Point3D PLAYER_EXIT_POS = new Point3D(2008, 1316, 0);
		private static readonly Point3D PLAYER_FINAL_POS = new Point3D(2983, 1043, 25);
		private static readonly Point3D GATE_POS = new Point3D(1964, 1325, 0);
		private static readonly Point3D PLAYER_FACE_POS = new Point3D(1961, 1318, 0);

		// Actor configuration
		private const int ACTOR1_ROBE_HUE = 1635;
		private const int ACTOR1_HAT_HUE = 1633;
		private const int ACTOR1_SPEECH_HUE = 233;
		private const int ACTOR2_SPEECH_HUE = 64;
		private const int ACTOR3_DOUBLET_HUE = 1291;
		private const int ACTOR3_SPEECH_HUE = 50;
		private const int GATE_HUE = 543;
		private const int GATE_ITEM_ID = 0xF6C;

		// Sound IDs
		private const int SOUND_ACTOR1_1 = 0xF9;
		private const int SOUND_ACTOR1_2 = 0x249;
		private const int SOUND_ACTOR1_3 = 0x1FE;
		private const int SOUND_ACTOR2_1 = 0xEC;
		private const int SOUND_GATE = 0x20E;

		// Play step constants
		private const int PLAY_STEP_MAX = 145;
		private const int PLAY_CHECK_INACTIVE = 0;
		private const int PLAY_CHECK_PLAYING = 1;
		private const int PLAY_CHECK_PLEDGE = 2;
		private const int PLAY_CHECK_AVATAR = 3;
		private const int PLAY_CHECK_SOULBOUND = 4;
		private const int PLAY_CHECK_COMPLETE = 5;

		// Dialogue timing
		private const int CHECK_COUNT_REPEAT = 5;
		private const int CHECK_COUNT_AVATAR_1 = 5;
		private const int CHECK_COUNT_AVATAR_2 = 15;
		private const int CHECK_COUNT_AVATAR_3 = 26;
		private const int CHECK_COUNT_AVATAR_4 = 40;
		private const int CHECK_COUNT_SOULBOUND_1 = 3;
		private const int CHECK_COUNT_SOULBOUND_2 = 13;
		private const int CHECK_COUNT_SOULBOUND_3 = 23;
		private const int CHECK_COUNT_SOULBOUND_4 = 29;
		private const int CHECK_COUNT_COMPLETE_1 = 2;
		private const int CHECK_COUNT_COMPLETE_2 = 5;
		private const int CHECK_COUNT_COMPLETE_3 = 8;
		private const int NO_ANSWER_MAX = 10;
		private const int PROMPT_DELAY_MIN = 6;
		private const int PROMPT_DELAY_MAX = 8;

		// Player stats
		private const int AVATAR_STAT_CAP = 250;
		private const int NORMAL_STAT_CAP = 235;
		private const int MIN_HUNGER_THIRST = 20;

		// Detection ranges
		private const int PLAYER_DETECTION_RANGE = 5;
		private const int BUSY_CHECK_RANGE = 10;
		private const int GATE_SEARCH_RANGE = 10;

		// OneTime type
		private const int ONE_TIME_TYPE = 3;

		// Map
		private static readonly Map PLAY_MAP = Map.Malas;
		private static readonly Map FINAL_MAP = Map.Trammel;

		#endregion

		#region Fields

		public static List<Mobile> PlayersWaiting = new List<Mobile>();

		private int m_OneTimeType;
		public int OneTimeType
		{
			get { return m_OneTimeType; }
			set { m_OneTimeType = value; }
		}

		public override bool CanOpenDoors { get { return true; } }
		public override bool Unprovokable { get { return true; } }
		public override bool InitialInnocent { get { return true; } }
		public override bool DeleteCorpseOnDeath { get { return true; } }

		private List<WayPoint> m_WayPoints;
		private bool m_criminalAction = false;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool AttackIsCriminal
		{
			get { return m_criminalAction; }
			set { m_criminalAction = value; }
		}

		private Mobile Actor1;
		private Mobile Actor2;
		private Mobile Actor3;
		private Mobile NewPlayer;
		private int PlayStep;
		private bool ActivePlay;
		private int PlayCheck;
		private int CheckCount;
		private bool Waiting;
		private int NoAnswer;

		#endregion

		#region Constructors

		[Constructable]
		public PlayDirectorNewChar()
			: this(Utility.RandomBool(), null, null)
		{
		}

		[Constructable]
		public PlayDirectorNewChar(bool sex)
			: this(sex, null, null)
		{
		}

		[Constructable]
		public PlayDirectorNewChar(bool sex, string name)
			: this(sex, name, null)
		{
		}

		[Constructable]
		public PlayDirectorNewChar(bool sex, string name, string title)
			: base(AIType.AI_PlayActor, FightMode.None, 10, 1, 0.8, 1.6)
		{
			m_WayPoints = new List<WayPoint>();
			m_OneTimeType = ONE_TIME_TYPE;

			Name = name;
			Title = title;

			SetStr(45, 90);
			SetDex(35, 70);
			SetInt(35, 70);
			SetHits(35, 120);
			SetStam(60, 80);
			SetMana(25, 50);
			SetDamage(7, 25);
			SetDamageType(ResistanceType.Physical, 100);
			SetResistance(ResistanceType.Physical, 10, 20);
			VirtualArmor = 30;

			InitializeAppearance(sex);
			InitializePlayState();

			if (PlayersWaiting == null)
				PlayersWaiting = new List<Mobile>();

			InitOutfit();
			InitPlay();
			Console.WriteLine("created");
		}

		/// <summary>
		/// Initializes character appearance including body, hair, and facial hair.
		/// </summary>
		private void InitializeAppearance(bool sex)
		{
			if (!sex)
			{
				Body = 0x191;
				if (Name == null)
					Name = NameList.RandomName("female");
			}
			else
			{
				Body = 0x190;
				if (Name == null)
					Name = NameList.RandomName("male");
				SetFacialHair();
			}

			SetHair();
			Hue = Utility.RandomSkinHue();
			HairHue = Utility.RandomHairHue();
			FacialHairHue = HairHue;
			SpeechHue = Utility.RandomDyedHue();
			Hidden = true;
		}

		/// <summary>
		/// Sets random facial hair style for male characters.
		/// </summary>
		private void SetFacialHair()
		{
			switch (Utility.Random(7))
			{
				default: FacialHairItemID = 0x00; break; // None
				case 0: FacialHairItemID = 0x2041; break; // Mustache
				case 1: FacialHairItemID = 0x203F; break; // ShortBeard
				case 2: FacialHairItemID = 0x204D; break; // Vandyke
				case 3:
					FacialHairItemID = Utility.RandomBool() ? 0x203E : 0x2040; // LongBeard or Goatee
					break;
			}
		}

		/// <summary>
		/// Sets random hair style.
		/// </summary>
		private void SetHair()
		{
			switch (Utility.Random(7))
			{
				case 0: HairItemID = 0x2047; break; // Afro
				case 1: HairItemID = 0x2045; break; // PageboyHair
				case 2: HairItemID = 0x203D; break; // PonyTail
				case 3: HairItemID = 0x203B; break; // ShortHair
				case 4: HairItemID = 0x2049; break; // TwoPigTails
				case 5: HairItemID = 0x203C; break; // LongHair
				case 6:
					HairItemID = Female ? 0x2046 : 0x2048; // BunsHair or ReceedingHair
					break;
			}
		}

		/// <summary>
		/// Initializes play state variables.
		/// </summary>
		private void InitializePlayState()
		{
			PlayStep = 0;
			PlayCheck = PLAY_CHECK_INACTIVE;
			CheckCount = 0;
			Waiting = false;
			ActivePlay = false;
			NoAnswer = 0;
			Actor1 = null;
			Actor2 = null;
			Actor3 = null;
			NewPlayer = null;
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes character outfit with random clothing.
		/// </summary>
		public virtual void InitOutfit()
		{
			EquipShoes();
			EquipClothing();
		}

		/// <summary>
		/// Equips random shoes based on wealth level.
		/// </summary>
		private void EquipShoes()
		{
			switch (Utility.Random(9))
			{
				case 1: break; // barefoot poor
				case 2: AddItem(new Shoes(GetShoeHue())); break; // poor, normal
				case 3: AddItem(new Sandals(GetShoeHue())); break; // poor, rich
				default:
				case 4: AddItem(new Shoes(GetShoeHue())); break; // normal
				case 6: AddItem(new Boots(GetShoeHue())); break; // normal, rich
				case 9: AddItem(new ThighBoots(GetShoeHue())); break; // rich
			}
		}

		/// <summary>
		/// Equips gender-appropriate clothing with coordinated colors.
		/// </summary>
		private void EquipClothing()
		{
			if (Female)
				EquipFemaleClothing();
			else
				EquipMaleClothing();
		}

		/// <summary>
		/// Equips female clothing with color coordination.
		/// </summary>
		private void EquipFemaleClothing()
		{
			int hueRange = Utility.Random(5);

			// Main clothing
			switch (Utility.Random(4))
			{
				case 0: AddItem(new PlainDress(GetRandomHueRange(hueRange))); break;
				case 1:
					AddItem(new Skirt(GetRandomHueRange(hueRange)));
					AddItem(new Shirt(GetRandomHueRange(hueRange)));
					break;
				case 2:
					AddItem(new LongPants(GetRandomHueRange(hueRange)));
					DoShirt(hueRange);
					break;
				case 3:
					AddItem(new ShortPants(GetRandomHueRange(hueRange)));
					DoShirt(hueRange);
					break;
			}

			// Hat
			switch (Utility.Random(5))
			{
				case 0: AddItem(new Bonnet(GetRandomHueRange(hueRange))); break;
				case 1: AddItem(new FloppyHat(GetRandomHueRange(hueRange))); break;
				case 2: AddItem(new Cap(GetRandomHueRange(hueRange))); break;
			}

			// Optional accessories
			if (Utility.RandomDouble() < 0.08)
				AddItem(new FullApron(Utility.RandomNeutralHue()));

			if (Utility.RandomBool())
				AddItem(new GoldRing());
		}

		/// <summary>
		/// Equips male clothing with color coordination.
		/// </summary>
		private void EquipMaleClothing()
		{
			int hueRange = Utility.Random(3);

			// Shirt
			switch (Utility.Random(3))
			{
				case 0: AddItem(new FancyShirt(GetRandomHueRange(hueRange))); break;
				case 1: AddItem(new Doublet(GetRandomHueRange(hueRange))); break;
				case 2: AddItem(new Shirt(GetRandomHueRange(hueRange))); break;
			}

			// Pants
			switch (Utility.Random(2))
			{
				case 0: AddItem(new LongPants(GetRandomHueRange(hueRange))); break;
				case 1: AddItem(new ShortPants(GetRandomHueRange(hueRange))); break;
			}

			// Hat
			switch (Utility.Random(5))
			{
				case 0: AddItem(new FloppyHat(Utility.RandomNeutralHue())); break;
				case 1: AddItem(new FeatheredHat(GetRandomHueRange(hueRange))); break;
			}

			// Optional accessories
			if (Utility.RandomDouble() < 0.16)
				AddItem(new FullApron(Utility.RandomNeutralHue()));

			if (Utility.RandomBool())
				AddItem(new GoldRing());
		}

		/// <summary>
		/// Initializes the play by creating actors and setting up the scene.
		/// </summary>
		public void InitPlay()
		{
			RemovePlayerFromQueue();
			CleanupActors();
			ResetPlayState();
			CleanupWayPoints();
			CreateActors();
			HandleWaitingPlayers();
		}

		/// <summary>
		/// Removes the current new player from the waiting queue if present.
		/// </summary>
		private void RemovePlayerFromQueue()
		{
			if (NewPlayer != null && PlayersWaiting.Contains(NewPlayer))
				PlayersWaiting.Remove(NewPlayer);
		}

		/// <summary>
		/// Deletes existing actors if they exist.
		/// </summary>
		private void CleanupActors()
		{
			if (Actor1 != null)
				Actor1.Delete();
			if (Actor2 != null)
				Actor2.Delete();
			if (Actor3 != null)
				Actor3.Delete();
		}

		/// <summary>
		/// Resets play state variables.
		/// </summary>
		private void ResetPlayState()
		{
			if (ActivePlay)
			{
				ActivePlay = false;
				PlayStep = 0;
			}

			NewPlayer = null;
			PlayCheck = PLAY_CHECK_INACTIVE;
			NoAnswer = 0;
		}

		/// <summary>
		/// Cleans up waypoints used for actor movement.
		/// </summary>
		private void CleanupWayPoints()
		{
			if (m_WayPoints == null)
				m_WayPoints = new List<WayPoint>();

			foreach (WayPoint wp in m_WayPoints)
			{
				if (wp != null)
					wp.Delete();
			}

			m_WayPoints.Clear();
		}

		/// <summary>
		/// Creates and configures the three play actors.
		/// </summary>
		private void CreateActors()
		{
			CreateActor1();
			CreateActor2();
			CreateActor3();
			EnsureActorPositions();
		}

		/// <summary>
		/// Creates Actor1 (Peter Grimm, the wizard).
		/// </summary>
		private void CreateActor1()
		{
			Actor1 = new PlayActor(true);
			Actor1.AddItem(new Boots());
			
			Item robe = new Robe();
			robe.Hue = ACTOR1_ROBE_HUE;
			Actor1.AddItem(robe);
			
			Item hat = new WizardsHat();
			hat.Hue = ACTOR1_HAT_HUE;
			Actor1.AddItem(hat);
			
			Actor1.Name = "Peter Grimm";
			Actor1.Title = "the Dastardly";
			Actor1.SpeechHue = ACTOR1_SPEECH_HUE;
			Actor1.MoveToWorld(ACTOR1_START_POS, PLAY_MAP);
		}

		/// <summary>
		/// Creates Actor2 (Sygun, the guard).
		/// </summary>
		private void CreateActor2()
		{
			Actor2 = new PlayActor(true);
			Actor2.AddItem(new PlateChest());
			Actor2.AddItem(new PlateLegs());
			Actor2.AddItem(new PlateGorget());
			Actor2.AddItem(new PlateGloves());
			Actor2.AddItem(new PlateHelm());
			Actor2.AddItem(new Boots());
			Actor2.Name = "Sygun";
			Actor2.Title = "the Subservient";
			Actor2.SpeechHue = ACTOR2_SPEECH_HUE;
			Actor2.MoveToWorld(ACTOR2_START_POS, PLAY_MAP);
		}

		/// <summary>
		/// Creates Actor3 (FinalTwist, the admin).
		/// </summary>
		private void CreateActor3()
		{
			Actor3 = new PlayActor(true);
			Actor3.Name = "FinalTwist";
			Actor3.Title = "the admin";
			Actor3.SpeechHue = ACTOR3_SPEECH_HUE;

			Item doublet = new Doublet();
			doublet.Hue = ACTOR3_DOUBLET_HUE;
			Actor3.AddItem(doublet);

			Item halberd = new Halberd();
			halberd.Hue = ACTOR3_DOUBLET_HUE;
			Actor3.AddItem(halberd);

			Item kilt = new Kilt();
			kilt.Hue = ACTOR3_DOUBLET_HUE;
			Actor3.AddItem(kilt);

			Item boots = new Boots();
			boots.Hue = ACTOR3_DOUBLET_HUE;
			Actor3.AddItem(boots);

			Actor3.MoveToWorld(ACTOR3_START_POS, PLAY_MAP);
		}

		/// <summary>
		/// Ensures all actors are in their correct starting positions.
		/// </summary>
		private void EnsureActorPositions()
		{
			if (Actor1 != null && Actor1.X != ACTOR1_START_POS.X)
				Actor1.MoveToWorld(ACTOR1_START_POS, PLAY_MAP);

			if (Actor2 != null && Actor2.X != ACTOR2_START_POS.X)
				Actor2.MoveToWorld(ACTOR2_START_POS, PLAY_MAP);

			if (Actor3 != null && Actor3.X != ACTOR3_START_POS.X)
				Actor3.MoveToWorld(ACTOR3_START_POS, PLAY_MAP);
		}

		/// <summary>
		/// Handles players waiting in the queue by moving them to the play area.
		/// </summary>
		private void HandleWaitingPlayers()
		{
			if (!CheckWaiting(null, false))
				return;

			Mobile nextPlayer = GetNextWaitingPlayer();
			if (nextPlayer != null && nextPlayer.Map != Map.Trammel)
			{
				PlayersWaiting.Remove(nextPlayer);
				nextPlayer.MoveToWorld(PLAYER_START_POS, PLAY_MAP);
			}
		}

		/// <summary>
		/// Gets the next player from the waiting queue.
		/// </summary>
		private Mobile GetNextWaitingPlayer()
		{
			foreach (Mobile m in PlayersWaiting)
			{
				if (m != null)
					return m;
			}
			return null;
		}

		#endregion

		#region Player Queue Management

		/// <summary>
		/// Checks if there are players waiting or adds a player to the queue.
		/// </summary>
		public static bool CheckWaiting(Mobile player, bool add)
		{
			if (add && player != null && player is PlayerMobile)
			{
				PlayersWaiting.Add(player);
				return false;
			}

			foreach (Mobile m in PlayersWaiting)
			{
				if (m != null)
					return true;
			}

			return false;
		}

		#endregion

		#region Play Management

		/// <summary>
		/// Starts the play sequence for a new player.
		/// </summary>
		public void StartPlay(Mobile mob)
		{
			if (!(mob is PlayerMobile))
				return;

			NewPlayer = mob;

			PlayerMobile pm = (PlayerMobile)NewPlayer;
			pm.Direction = NewPlayer.GetDirectionTo(Actor2.Location);

			if (mob.AccessLevel == AccessLevel.Player)
				mob.Frozen = true;

			ActivePlay = true;
			PlayCheck = PLAY_CHECK_PLAYING;
			PlayStep = 0;
		}

		/// <summary>
		/// Main tick handler for the play director. Manages play progression and player interaction.
		/// </summary>
		public void OneTimeTick()
		{
			if (Actor1 == null || Actor2 == null || Actor3 == null)
				InitPlay();

			if (!ActivePlay && NewPlayer != null)
				NewPlayer = null;

			if (ActivePlay)
			{
				if (NewPlayer == null)
				{
					InitPlay();
					return;
				}

				MaintainPlayerNeeds();
				ProcessPlaySequence();
			}
		}

		/// <summary>
		/// Maintains player hunger and thirst at minimum levels during the play.
		/// </summary>
		private void MaintainPlayerNeeds()
		{
			if (NewPlayer.Hunger < MIN_HUNGER_THIRST)
				NewPlayer.Hunger = MIN_HUNGER_THIRST;
			if (NewPlayer.Thirst < MIN_HUNGER_THIRST)
				NewPlayer.Thirst = MIN_HUNGER_THIRST;
		}

		/// <summary>
		/// Processes the current play sequence based on play check state.
		/// </summary>
		private void ProcessPlaySequence()
		{
			if (PlayCheck == PLAY_CHECK_PLAYING)
				ProcessMainPlay();
			else if (PlayCheck == PLAY_CHECK_PLEDGE && !Waiting)
				ProcessPledgeDialogue();
			else if (PlayCheck == PLAY_CHECK_AVATAR && !Waiting)
				ProcessAvatarDialogue();
			else if (PlayCheck == PLAY_CHECK_SOULBOUND && !Waiting)
				ProcessSoulBoundDialogue();
			else if (PlayCheck == PLAY_CHECK_COMPLETE)
				ProcessCompletion();
			else if (Waiting && NewPlayer != null)
				ProcessWaitingForResponse();
		}

		/// <summary>
		/// Processes the main play sequence with actor movements and dialogue.
		/// </summary>
		private void ProcessMainPlay()
		{
			PlayStepActionHandler.ExecuteStep(this, PlayStep);

			if (PlayStep < PLAY_STEP_MAX)
				PlayStep++;
			else
			{
				PlayStep = 0;
				PlayCheck = PLAY_CHECK_PLEDGE;
				Waiting = true;
			}
		}

		/// <summary>
		/// Processes the pledge dialogue sequence.
		/// </summary>
		private void ProcessPledgeDialogue()
		{
			if (CheckCount == CHECK_COUNT_REPEAT)
				Actor3.Say("Do you pledge to keep this world clean, and respect other adventurers?");

			if (CheckCount >= CHECK_COUNT_REPEAT)
			{
				Waiting = true;
				CheckCount = 0;
			}
			else
				CheckCount++;
		}

		/// <summary>
		/// Processes the avatar choice dialogue sequence.
		/// </summary>
		private void ProcessAvatarDialogue()
		{
			if (CheckCount == CHECK_COUNT_AVATAR_1)
				Actor3.Say("A Powerful Balance affects all things in this world.  The actions of avatars affects whether it moves towards evil, or good.");
			if (CheckCount == CHECK_COUNT_AVATAR_2)
				Actor3.Say("Avatars can choose to pledge for either side of the Balance, and affect a large number of things, like gold rewards, monster difficulty, and shop prices.");
			if (CheckCount == CHECK_COUNT_AVATAR_3)
				Actor3.Say("This comes with benefits like more attributes and faster skillgain, but also at a cost: death will carry a very real penalty.");
			if (CheckCount == CHECK_COUNT_AVATAR_4)
				Actor3.Say("Do you wish to become an avatar of the balance?  Be warned that this may be a more difficult experience.");

			if (CheckCount >= CHECK_COUNT_AVATAR_4)
			{
				Waiting = true;
				CheckCount = 0;
			}
			else
				CheckCount++;
		}

		/// <summary>
		/// Processes the soulbound choice dialogue sequence.
		/// </summary>
		private void ProcessSoulBoundDialogue()
		{
			if (CheckCount == CHECK_COUNT_SOULBOUND_1)
				Actor3.Say("Next, you may choose to bind your soul to the very fabric of this world. ");
			if (CheckCount == CHECK_COUNT_SOULBOUND_2)
				Actor3.Say("Doing so means death will be permanent and you will return as a new person every time you die.");
			if (CheckCount == CHECK_COUNT_SOULBOUND_3)
				Actor3.Say("Adventuring this way can be very hard, but very rewarding - it is said that SoulBound can bind properties of items into their very beings.  ");
			if (CheckCount == CHECK_COUNT_SOULBOUND_4)
				Actor3.Say("Do you wish to be a SoulBound? *This is Not recommended for new adventurers*");

			if (CheckCount >= CHECK_COUNT_SOULBOUND_4)
			{
				Waiting = true;
				CheckCount = 0;
			}
			else
				CheckCount++;
		}

		/// <summary>
		/// Processes the completion sequence, teleporting the player to the final location.
		/// </summary>
		private void ProcessCompletion()
		{
			if (CheckCount == CHECK_COUNT_COMPLETE_1)
				Actor3.Say("See you out there!");
			if (CheckCount == CHECK_COUNT_COMPLETE_2)
				Actor3.Say("An Vam Trav");

			if (CheckCount == CHECK_COUNT_COMPLETE_3)
			{
				NewPlayer.Frozen = false;
				NewPlayer.MoveToWorld(PLAYER_FINAL_POS, FINAL_MAP);
				RemovePlayerFromQueue();
				InitPlay();
			}

			if (CheckCount >= CHECK_COUNT_COMPLETE_3)
				CheckCount = 0;
			else
				CheckCount++;
		}

		/// <summary>
		/// Processes waiting for player response, with timeout handling.
		/// </summary>
		private void ProcessWaitingForResponse()
		{
			if (NoAnswer >= NO_ANSWER_MAX)
			{
				NewPlayer.MoveToWorld(PLAYER_EXIT_POS, PLAY_MAP);
				NewPlayer.Frozen = false;
				InitPlay();
				return;
			}

			if (CheckCount > Utility.RandomMinMax(PROMPT_DELAY_MIN, PROMPT_DELAY_MAX))
			{
				PlayDialogueHelper.SayPrompt(Actor3);
				CheckCount = 0;
				NoAnswer++;
			}
			else
				CheckCount++;
		}

		#endregion

		#region Actor Movement

		/// <summary>
		/// Moves an actor to a specified location using waypoints.
		/// </summary>
		public void MoveActor(Mobile actor, Point3D destination)
		{
			if (actor == null || actor.Map == null)
				return;

			if (m_WayPoints == null)
				m_WayPoints = new List<WayPoint>();

			WayPoint wayPoint = new WayPoint();
			wayPoint.Map = actor.Map;
			wayPoint.Location = destination;
			m_WayPoints.Add(wayPoint);

			PlayActor playActor = actor as PlayActor;
			if (playActor != null)
				playActor.CurrentWayPoint = wayPoint;
		}

		#endregion

		#region Utility Methods

		/// <summary>
		/// Gets a random shoe hue.
		/// </summary>
		public virtual int GetShoeHue()
		{
			return Utility.RandomNeutralHue();
		}

		/// <summary>
		/// Gets a random hue within a specified color range for coordinated outfits.
		/// </summary>
		public virtual int GetRandomHueRange(int range)
		{
			switch (range % 10)
			{
				default:
				case 0: return Utility.RandomNeutralHue();
				case 1: return Utility.RandomBlueHue();
				case 2: return Utility.RandomGreenHue();
				case 3: return Utility.RandomRedHue();
				case 4: return Utility.RandomYellowHue();
			}
		}

		/// <summary>
		/// Adds a random shirt type with the specified hue range.
		/// </summary>
		public virtual void DoShirt(int hues)
		{
			switch (Utility.Random(2))
			{
				case 0: AddItem(new Doublet(GetRandomHueRange(hues))); break;
				case 1: AddItem(new Shirt(GetRandomHueRange(hues))); break;
			}
		}

		/// <summary>
		/// Checks if the area is busy with players.
		/// </summary>
		public bool BusyCheck()
		{
			foreach (Mobile m in this.GetMobilesInRange(BUSY_CHECK_RANGE))
			{
				if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
					return true;
			}
			return false;
		}

		#endregion

		#region Overrides

		public override void OnAfterDelete()
		{
			if (Actor1 != null)
				Actor1.Delete();
			if (Actor2 != null)
				Actor2.Delete();
			if (Actor3 != null)
				Actor3.Delete();
		}

		public override void AggressiveAction(Mobile aggressor, bool criminal)
		{
			// Intentionally empty - play director is non-aggressive
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			// Intentionally empty - play director cannot be damaged
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from.AccessLevel > AccessLevel.Player)
				InitPlay();
		}

		public override void OnMovement(Mobile m, Point3D oldLocation)
		{
			// Intentionally empty
		}

		public override bool HandlesOnSpeech(Mobile from)
		{
			return true;
		}

		public override void OnSpeech(SpeechEventArgs e)
		{
			if (!(e.Mobile is PlayerMobile))
				return;

			Mobile m = e.Mobile;

			// Admin command to start play
			if (m.AccessLevel > AccessLevel.Player && Insensitive.Contains(e.Speech, "start"))
			{
				StartPlay(this);
				return;
			}

			// Handle player responses during waiting periods
			if (Waiting)
			{
				if (PlayDialogueHelper.IsPositiveResponse(e.Speech))
					HandlePositiveResponse();
				else if (PlayDialogueHelper.IsNegativeResponse(e.Speech))
					HandleNegativeResponse();
				else if (Insensitive.Contains(e.Speech, "repeat"))
				{
					NoAnswer = 0;
					Waiting = false;
				}
			}

			base.OnSpeech(e);
		}

		/// <summary>
		/// Handles positive responses from the player during dialogue sequences.
		/// </summary>
		private void HandlePositiveResponse()
		{
			if (PlayCheck == PLAY_CHECK_PLEDGE)
			{
				Actor3.Say("Very well!  Welcome, friend.");
				CheckCount = 0;
				// Skip Avatar and SoulBound choices - set defaults and go to completion
				PlayCheck = PLAY_CHECK_COMPLETE;
				PlayerMobile pm = (PlayerMobile)NewPlayer;
				pm.Avatar = false;
				pm.SoulBound = false;
				NewPlayer.StatCap = NORMAL_STAT_CAP;
			}
			else if (PlayCheck == PLAY_CHECK_AVATAR)
			{
				Actor3.Say("Good Choice.");
				CheckCount = 0;
				PlayCheck = PLAY_CHECK_SOULBOUND;
				PlayerMobile pm = (PlayerMobile)NewPlayer;
				pm.Avatar = true;
				NewPlayer.StatCap = AVATAR_STAT_CAP;
			}
			else if (PlayCheck == PLAY_CHECK_SOULBOUND)
			{
				Actor3.Say("Good Luck!");
				CheckCount = 0;
				PlayCheck = PLAY_CHECK_COMPLETE;
				PlayerMobile pm = (PlayerMobile)NewPlayer;
				pm.SoulBound = true;
				pm.Avatar = true;
				NewPlayer.Backpack.AddItem(new SoulTome());
				pm.SbRes = true;
				pm.ResetPlayer(NewPlayer, true);
				NewPlayer.Frozen = false;
				NewPlayer = null;
				InitPlay();
			}

			NoAnswer = 0;
			Waiting = false;
		}

		/// <summary>
		/// Handles negative responses from the player during dialogue sequences.
		/// </summary>
		private void HandleNegativeResponse()
		{
			if (PlayCheck == PLAY_CHECK_PLEDGE)
			{
				Actor3.Say("Okay, your choice, GoodBye!");
				NewPlayer.MoveToWorld(PLAYER_EXIT_POS, PLAY_MAP);
				InitPlay();
			}
			else if (PlayCheck == PLAY_CHECK_AVATAR)
			{
				Actor3.Say("Thats fine... you prefer an easier experience.");
				CheckCount = 0;
				PlayCheck = PLAY_CHECK_SOULBOUND;
				PlayerMobile pm = (PlayerMobile)NewPlayer;
				pm.Avatar = false;
				NewPlayer.StatCap = NORMAL_STAT_CAP;
			}
			else if (PlayCheck == PLAY_CHECK_SOULBOUND)
			{
				Actor3.Say("Fair enough!");
				CheckCount = 0;
				PlayCheck = PLAY_CHECK_COMPLETE;
				PlayerMobile pm = (PlayerMobile)NewPlayer;
				pm.SoulBound = false;
			}

			NoAnswer = 0;
			Waiting = false;
		}

		public override void OnThink()
		{
			if (Actor1 == null || Actor2 == null || Actor3 == null)
				InitPlay();

			if (!Hidden)
				Hidden = true;

			if (!ActivePlay && NewPlayer == null)
			{
				foreach (Mobile mob in this.GetMobilesInRange(PLAYER_DETECTION_RANGE))
				{
					if (mob is PlayerMobile && mob.AccessLevel == AccessLevel.Player)
					{
						StartPlay(mob);
						break;
					}
				}
			}

			base.OnThink();
		}

		#endregion

		#region Serialization

		public PlayDirectorNewChar(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)1);
			writer.Write((bool)m_criminalAction);
			writer.Write((Mobile)Actor1);
			writer.Write((Mobile)Actor2);
			writer.Write((Mobile)Actor3);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
					{
						m_criminalAction = reader.ReadBool();
						Actor1 = reader.ReadMobile();
						Actor2 = reader.ReadMobile();
						Actor3 = reader.ReadMobile();
						goto case 0;
					}
				case 0:
					{
						break;
					}
			}

			m_OneTimeType = ONE_TIME_TYPE;
			m_WayPoints = new List<WayPoint>();
		}

		#endregion

		#region Helper Classes

		/// <summary>
		/// Handles play step actions using a dictionary-based approach for better maintainability.
		/// </summary>
		private static class PlayStepActionHandler
		{
			/// <summary>
			/// Executes the action for a specific play step.
			/// </summary>
			public static void ExecuteStep(PlayDirectorNewChar director, int step)
			{
				if (step == 0)
					EnsureActorsInPosition(director);
				else if (step == 1)
					MoveActor2ToFront(director);
				else if (step == 4)
					MoveActor2BackAndSound(director);
				else if (step == 7)
					MoveActor2FrontAndBurp(director);
				else if (step == 10)
					MoveActor2Back(director);
				else if (step == 11)
					MoveActor2FrontAndSound2(director);
				else if (step == 14)
					MoveActor2BackAndDialogue(director);
				else if (step == 17)
					MoveActor2ToFront(director);
				else if (step == 20)
					MoveActor2Back(director);
				else if (step == 22)
					FaceActor2ToPlayer(director);
				else if (step == 24)
					FaceActor2ToActor1AndDialogue(director);
				else if (step == 28)
					SayDialogue(director.Actor1, "Darnit, my new sleep spell exploit failed again.");
				else if (step == 34)
					SayDialogue(director.Actor1, "I'll need you to get me more of that special ingredient.");
				else if (step == 40)
					SayDialogue(director.Actor2, "But M'Lord");
				else if (step == 43)
					SayDialogue(director.Actor1, "No buts, you lazy lout.");
				else if (step == 45)
					SayDialogue(director.Actor1, "Go, or I'll sic my paragon pets on you again.");
				else if (step == 50)
					SayDialogue(director.Actor2, "Yes M'Lord");
				else if (step == 52)
					MoveActor2ToExit(director);
				else if (step == 57)
					RemoveActor2FromPlay(director);
				else if (step == 58)
					SayDialogue(director.Actor1, "*Grumbles*");
				else if (step == 60)
					CreateGate(director);
				else if (step == 62)
					MoveActor1Away(director);
				else if (step == 63)
					FaceActor1ToGateAndDialogue(director);
				else if (step == 66)
					MoveActor3ToGate(director);
				else if (step == 67)
					MoveActor3Closer(director);
				else if (step == 68)
					FaceActor3ToActor1AndDialogue(director);
				else if (step == 72)
					SayDialogue(director.Actor1, "Dastardly Barnacles!  Defences have been breached!");
				else if (step == 77)
					SayDialogue(director.Actor3, "I'll stop you from using any more exploits!");
				else if (step == 82)
					SayDialogue(director.Actor3, "Vas Flam");
				else if (step == 83)
					SayDialogue(director.Actor1, "We shall meet again, Final!");
				else if (step == 84)
					Actor1TeleportEffect(director);
				else if (step == 85)
					RemoveActor1AndGate(director);
				else if (step == 90)
					MoveActor3ToPlayer(director);
				else if (step == 92)
					FaceActor3ToPlayer(director);
				else if (step == 95)
					SayDialogue(director.Actor3, "Well well... what do we have here... a new Adventurer!");
				else if (step == 100)
					SayDialogue(director.Actor3, "Stuck in jail are we?");
				else if (step == 104)
					SayDialogue(director.Actor3, "Well... I guess I can let you out.");
				else if (step == 108)
					SayDialogue(director.Actor3, "But I need to know a few things first.");
				else if (step == 114)
					SayDialogue(director.Actor3, "This world was created by a being called Djeryv long ago.");
				else if (step == 120)
					SayDialogue(director.Actor3, "But it has changed, morphed... for better or worse.");
				else if (step == 130)
					SayDialogue(director.Actor3, "Forces of Good and Evil battle for dominance, and the world is plagued by horrible beings of power.");
				else if (step == 140)
					SayDialogue(director.Actor3, "If I let you out, I need to know a few things...");
				else if (step == 145)
					FinalPledgeQuestion(director);
			}

			private static void EnsureActorsInPosition(PlayDirectorNewChar director)
			{
				if (director.Actor1.X != ACTOR1_START_POS.X)
					director.MoveActor(director.Actor1, ACTOR1_START_POS);
				if (director.Actor2.X != ACTOR2_START_POS.X)
					director.MoveActor(director.Actor2, ACTOR2_START_POS);
				if (director.Actor3.X != ACTOR3_START_POS.X)
					director.Actor3.MoveToWorld(ACTOR3_START_POS, PLAY_MAP);
			}

			private static void MoveActor2ToFront(PlayDirectorNewChar director)
			{
				director.MoveActor(director.Actor2, ACTOR2_FRONT_POS);
			}

			private static void MoveActor2Back(PlayDirectorNewChar director)
			{
				director.MoveActor(director.Actor2, ACTOR2_START_POS);
			}

			private static void MoveActor2BackAndSound(PlayDirectorNewChar director)
			{
				MoveActor2Back(director);
				director.Actor1.PlaySound(SOUND_ACTOR1_1);
			}

			private static void MoveActor2FrontAndBurp(PlayDirectorNewChar director)
			{
				MoveActor2ToFront(director);
				SayDialogue(director.Actor2, "*burps*");
			}

			private static void MoveActor2FrontAndSound2(PlayDirectorNewChar director)
			{
				MoveActor2ToFront(director);
				director.Actor1.PlaySound(SOUND_ACTOR1_2);
			}

			private static void MoveActor2BackAndDialogue(PlayDirectorNewChar director)
			{
				MoveActor2Back(director);
				SayDialogue(director.Actor1, "Yes... Yes....");
			}

			private static void FaceActor2ToPlayer(PlayDirectorNewChar director)
			{
				PlayActor actor2 = director.Actor2 as PlayActor;
				if (actor2 != null)
					actor2.Direction = director.Actor2.GetDirectionTo(director.NewPlayer.Location);
			}

			private static void FaceActor2ToActor1AndDialogue(PlayDirectorNewChar director)
			{
				PlayActor actor2 = director.Actor2 as PlayActor;
				if (actor2 != null)
					actor2.Direction = director.Actor2.GetDirectionTo(director.Actor1.Location);
				SayDialogue(director.Actor2, "M'Lord, the Prisoner woke up.");
			}

			private static void MoveActor2ToExit(PlayDirectorNewChar director)
			{
				director.MoveActor(director.Actor2, ACTOR2_EXIT_POS);
			}

			private static void RemoveActor2FromPlay(PlayDirectorNewChar director)
			{
				director.Actor2.MoveToWorld(ACTOR3_START_POS, PLAY_MAP);
				director.Actor2.PlaySound(SOUND_ACTOR2_1);
			}

			private static DummyGate m_CurrentGate = null;

			private static void CreateGate(PlayDirectorNewChar director)
			{
				Effects.PlaySound(ACTOR1_START_POS, PLAY_MAP, SOUND_GATE);
				m_CurrentGate = new DummyGate();
				m_CurrentGate.Hue = GATE_HUE;
				m_CurrentGate.MoveToWorld(GATE_POS, PLAY_MAP);
			}

			private static void MoveActor1Away(PlayDirectorNewChar director)
			{
				director.MoveActor(director.Actor1, ACTOR1_MOVE_POS);
			}

			private static void FaceActor1ToGateAndDialogue(PlayDirectorNewChar director)
			{
				PlayActor actor1 = director.Actor1 as PlayActor;
				if (actor1 != null)
					actor1.Direction = director.Actor1.GetDirectionTo(GATE_POS);
				SayDialogue(director.Actor1, "What's this?");
			}

			private static void MoveActor3ToGate(PlayDirectorNewChar director)
			{
				director.Actor3.MoveToWorld(ACTOR3_GATE_POS, PLAY_MAP);
			}

			private static void MoveActor3Closer(PlayDirectorNewChar director)
			{
				director.MoveActor(director.Actor3, ACTOR3_MOVE_POS);
			}

			private static void FaceActor3ToActor1AndDialogue(PlayDirectorNewChar director)
			{
				PlayActor actor3 = director.Actor3 as PlayActor;
				if (actor3 != null)
					actor3.Direction = director.Actor3.GetDirectionTo(director.Actor1.Location);
				SayDialogue(director.Actor3, "AHA! Found you!");
			}

			private static void Actor1TeleportEffect(PlayDirectorNewChar director)
			{
				SayDialogue(director.Actor1, "Lord of Exploits, Take me!");
				Effects.SendLocationParticles(EffectItem.Create(director.Actor1.Location, director.Actor1.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 0, 0, 2023, 0);
				director.Actor1.PlaySound(SOUND_ACTOR1_3);
			}

			private static void RemoveActor1AndGate(PlayDirectorNewChar director)
			{
				director.Actor1.MoveToWorld(ACTOR3_START_POS, PLAY_MAP);

				if (m_CurrentGate != null)
				{
					m_CurrentGate.Delete();
					m_CurrentGate = null;
				}
				else
				{
					Item gate = FindGateNearActor3(director);
					if (gate != null)
						gate.Delete();
				}

				SayDialogue(director.Actor3, "That Peter Grimm... Always trying to find new exploits for unlimited gold.");
			}

			private static Item FindGateNearActor3(PlayDirectorNewChar director)
			{
				foreach (object o in director.Actor3.GetObjectsInRange(GATE_SEARCH_RANGE))
				{
					Item item = o as Item;
					if (item != null && item.ItemID == GATE_ITEM_ID)
						return item;
				}
				return null;
			}

			private static void MoveActor3ToPlayer(PlayDirectorNewChar director)
			{
				director.MoveActor(director.Actor3, ACTOR3_PLAYER_POS);
			}

			private static void FaceActor3ToPlayer(PlayDirectorNewChar director)
			{
				PlayActor actor3 = director.Actor3 as PlayActor;
				if (actor3 != null)
					actor3.Direction = director.Actor3.GetDirectionTo(PLAYER_FACE_POS);
			}

			private static void FinalPledgeQuestion(PlayDirectorNewChar director)
			{
				SayDialogue(director.Actor3, "Do you pledge to keep this world clean, and respect other adventurers?");
				director.PlayCheck = PLAY_CHECK_PLEDGE;
				director.Waiting = true;
			}

			private static void SayDialogue(Mobile actor, string text)
			{
				if (actor != null)
					actor.Say(text);
			}
		}

		/// <summary>
		/// Helper class for managing dialogue and player responses.
		/// </summary>
		private static class PlayDialogueHelper
		{
			private static readonly string[] PositiveResponses = new string[]
			{
				"i pledge", "i do", "aye", "yes", "sure", "okay"
			};

			private static readonly string[] NegativeResponses = new string[]
			{
				"nay", "no", "nope", "naw"
			};

			private static readonly string[] PromptMessages = new string[]
			{
				"Hello?",
				"So, what will it be, yes or no?",
				"You can just tell me your answer.",
				"Did you want me to repeat?  Just say repeat.",
				"What'll it be, friend?",
				"I know... tough decision and all.",
				"If you want me to repeat what I wrote, just ask me to repeat :)"
			};

			/// <summary>
			/// Checks if the speech contains a positive response.
			/// </summary>
			public static bool IsPositiveResponse(string speech)
			{
				foreach (string response in PositiveResponses)
				{
					if (Insensitive.Contains(speech, response))
						return true;
				}
				return false;
			}

			/// <summary>
			/// Checks if the speech contains a negative response.
			/// </summary>
			public static bool IsNegativeResponse(string speech)
			{
				foreach (string response in NegativeResponses)
				{
					if (Insensitive.Contains(speech, response))
						return true;
				}
				return false;
			}

			/// <summary>
			/// Says a random prompt message to encourage player response.
			/// </summary>
			public static void SayPrompt(Mobile actor)
			{
				if (actor != null && PromptMessages.Length > 0)
				{
					string message = PromptMessages[Utility.Random(PromptMessages.Length)];
					actor.Say(message);
				}
			}
		}

		#endregion
	}
}
