using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// A static stone that provides magery reagents to players for testing purposes.
	/// Includes cooldown system and smart reagent management to prevent abuse.
	/// </summary>
    public class ReagStone : Item
    {
		#region Constants

		/// <summary>Item ID for the reagent stone appearance.</summary>
		private const int ITEM_ID = 0xED4;

		/// <summary>Hue color for the reagent stone (green).</summary>
		private const int STONE_HUE = 0x2D1;

		/// <summary>Default name displayed for the reagent stone.</summary>
		private const string DEFAULT_NAME = "Pedra de Reagentes";

		/// <summary>Target amount of each reagent to maintain.</summary>
		private const int TARGET_REAGENT_AMOUNT = 55;

		/// <summary>Cooldown duration in minutes between uses.</summary>
		private const int COOLDOWN_MINUTES = 3;

		/// <summary>Maximum total reagents a player can have before being blocked (guard rail).</summary>
		private const int MAX_TOTAL_REAGENTS = 300;

		/// <summary>Message sent when player receives reagents.</summary>
		private const string MESSAGE_RECEIVED = "Você recebeu reagentes de magia.";

		/// <summary>Message sent when player is on cooldown.</summary>
		private const string MESSAGE_COOLDOWN = "Você deve esperar {0} minuto(s) antes de usar a pedra novamente.";

		/// <summary>Message sent when player has too many reagents (guard rail).</summary>
		private const string MESSAGE_TOO_MANY_REAGENTS = "Você já possui muitos reagentes. Use alguns antes de solicitar mais.";

		/// <summary>Message sent when no reagents are needed.</summary>
		private const string MESSAGE_ALREADY_FULL = "Você já possui reagentes suficientes (55 de cada tipo).";

		/// <summary>Message sent when reagents are replenished.</summary>
		private const string MESSAGE_REPLENISHED = "Seus reagentes foram completados até {0} de cada tipo.";

		/// <summary>Message sent when bag is created.</summary>
		private const string MESSAGE_BAG_CREATED = "Uma bolsa de reagentes foi criada em sua mochila.";

		/// <summary>Target amount of blank scrolls to maintain.</summary>
		private const int TARGET_BLANK_SCROLLS = 50;

		/// <summary>Message sent when spellbook is provided.</summary>
		private const string MESSAGE_SPELLBOOK_PROVIDED = "Um livro de magia completo foi adicionado à sua mochila.";

		/// <summary>Message sent when blank scrolls are provided.</summary>
		private const string MESSAGE_SCROLLS_PROVIDED = "Pergaminhos em branco foram adicionados à sua mochila.";

		/// <summary>Serialization version number.</summary>
		private const int SERIALIZATION_VERSION = 0;

		#endregion

		#region Static Cooldown Tracking

		/// <summary>
		/// Dictionary tracking the last use time for each player to enforce cooldown.
		/// </summary>
		private static Dictionary<Mobile, DateTime> m_Cooldowns = new Dictionary<Mobile, DateTime>();

		/// <summary>
		/// Locks cooldown dictionary access for thread safety.
		/// </summary>
		private static readonly object m_CooldownLock = new object();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReagStone class.
		/// </summary>
        [Constructable]
        public ReagStone()
			: base(ITEM_ID)
        {
			Movable = false;
			Hue = STONE_HUE;
        }

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
        public ReagStone(Serial serial)
            : base(serial)
        {
        }

		#endregion

		#region Properties

		/// <summary>
		/// Gets the default name of the reagent stone.
		/// </summary>
        public override string DefaultName
        {
			get { return DEFAULT_NAME; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Handles double-click interaction. Opens gump for item selection.
		/// Cooldown and guard rails are checked per-item in the gump (allows first-time setup without cooldown).
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			if (from == null || from.Deleted)
				return;

			if (!from.InRange(GetWorldLocation(), 2))
			{
				from.SendLocalizedMessage(500446); // That is too far away.
				return;
			}

			if (!ValidatePlayerAccess(from))
				return;

			// Don't check cooldown here - allow players to open gump anytime
			// Cooldown will be checked per-item in the gump:
			// - BagOfReagents: Always checks cooldown + guard rails
			// - Other items: Only checks cooldown if player already has the item

			// Show the gump
			from.CloseGump(typeof(ReagStoneGump));
			from.SendGump(new ReagStoneGump(this));
		}

		#endregion

		#region Validation Methods

		/// <summary>
		/// Validates that the mobile is a valid player.
		/// </summary>
		private bool ValidatePlayerAccess(Mobile from)
		{
			if (!(from is PlayerMobile))
			{
				from.SendMessage("Apenas jogadores podem usar esta pedra.");
				return false;
			}

			if (from.Backpack == null)
			{
				from.SendMessage("Você precisa de uma mochila para usar esta pedra.");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if player is on cooldown and returns true if they should be blocked.
		/// </summary>
		private bool CheckCooldown(Mobile from)
		{
			return CheckCooldownForGump(from);
		}

		/// <summary>
		/// Public method for gump to check cooldown. Returns true if player should be blocked.
		/// </summary>
		public bool CheckCooldownForGump(Mobile from)
		{
			lock (m_CooldownLock)
			{
				if (m_Cooldowns.ContainsKey(from))
				{
					DateTime lastUse = m_Cooldowns[from];
					TimeSpan elapsed = DateTime.Now - lastUse;
					TimeSpan cooldown = TimeSpan.FromMinutes(COOLDOWN_MINUTES);

					if (elapsed < cooldown)
					{
						TimeSpan remaining = cooldown - elapsed;
						int minutesRemaining = (int)Math.Ceiling(remaining.TotalMinutes);
						from.SendMessage(string.Format(MESSAGE_COOLDOWN, minutesRemaining));
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Checks guard rails to prevent reagent accumulation abuse.
		/// </summary>
		private bool CheckGuardRails(Mobile from)
		{
			return CheckGuardRailsForGump(from);
		}

		/// <summary>
		/// Public method for gump to check guard rails. Returns true if player should be blocked.
		/// </summary>
		public bool CheckGuardRailsForGump(Mobile from)
		{
			int totalReagents = CountTotalReagents(from);

			if (totalReagents >= MAX_TOTAL_REAGENTS)
			{
				from.SendMessage(MESSAGE_TOO_MANY_REAGENTS);
				return true;
			}

			return false;
		}

		#endregion

		#region Reagent Management

		/// <summary>
		/// Processes the reagent request: creates bag if needed, replenishes reagents, and provides magery items.
		/// </summary>
		private void ProcessReagentRequest(Mobile from)
		{
			Container backpack = from.Backpack;

			// Check if player has a BagOfReagents, create one if not
			BagOfReagents reagentBag = FindOrCreateReagentBag(backpack);

			if (reagentBag == null)
			{
				from.SendMessage("Não foi possível criar ou encontrar uma bolsa de reagentes.");
				return;
			}

			// Count and replenish reagents
			bool replenished = ReplenishReagents(backpack, reagentBag);

			// Provide full spellbook if needed
			bool spellbookProvided = ProvideFullSpellbook(backpack);

			// Provide blank scrolls if needed
			bool scrollsProvided = ProvideBlankScrolls(backpack);

			if (replenished || spellbookProvided || scrollsProvided)
			{
				UpdateCooldown(from);
				from.SendMessage(MESSAGE_RECEIVED);
			}
			else
			{
				from.SendMessage(MESSAGE_ALREADY_FULL);
			}
		}

		/// <summary>
		/// Finds an existing BagOfReagents in backpack or creates a new one.
		/// </summary>
		private BagOfReagents FindOrCreateReagentBag(Container backpack)
		{
			return FindOrCreateReagentBagForGump(backpack);
		}

		/// <summary>
		/// Public method for gump to find or create reagent bag.
		/// </summary>
		public BagOfReagents FindOrCreateReagentBagForGump(Container backpack)
		{
			if (backpack == null)
				return null;

			// Look for existing bag
			BagOfReagents existingBag = backpack.FindItemByType(typeof(BagOfReagents)) as BagOfReagents;
			if (existingBag != null)
				return existingBag;

			// Create new bag
			BagOfReagents newBag = new BagOfReagents();
			backpack.DropItem(newBag);
			
			// Check if item was successfully added
			if (newBag.Parent == backpack)
			{
				return newBag;
			}

			// If backpack is full, try to place at player's feet
			Mobile owner = backpack.RootParent as Mobile;
			if (owner != null)
			{
				newBag.MoveToWorld(owner.Location, owner.Map);
				owner.SendMessage(MESSAGE_BAG_CREATED);
				return newBag;
			}

			return null;
		}

		/// <summary>
		/// Replenishes reagents in the backpack to reach target amount for each type.
		/// </summary>
		private bool ReplenishReagents(Container backpack, BagOfReagents reagentBag)
		{
			return ReplenishReagentsForGump(backpack, reagentBag);
		}

		/// <summary>
		/// Public method for gump to replenish reagents.
		/// </summary>
		public bool ReplenishReagentsForGump(Container backpack, BagOfReagents reagentBag)
		{
			if (backpack == null)
				return false;

			bool anyReplenished = false;
			Type[] reagentTypes = GetMageryReagentTypes();

			foreach (Type reagentType in reagentTypes)
			{
				int currentAmount = backpack.GetAmount(reagentType, true);
				int needed = TARGET_REAGENT_AMOUNT - currentAmount;

				if (needed > 0)
				{
					Item newReagent = CreateReagentItem(reagentType, needed);
					if (newReagent != null)
					{
						// Try to add to bag first, then backpack
						if (reagentBag != null)
						{
							reagentBag.DropItem(newReagent);
							if (newReagent.Parent == reagentBag)
							{
								anyReplenished = true;
							}
							else
							{
								// Bag full, try backpack
								backpack.DropItem(newReagent);
								if (newReagent.Parent == backpack)
								{
									anyReplenished = true;
								}
								else
								{
									// Backpack full, place at feet
									Mobile owner = backpack.RootParent as Mobile;
									if (owner != null)
									{
										newReagent.MoveToWorld(owner.Location, owner.Map);
										anyReplenished = true;
									}
									else
									{
										newReagent.Delete();
									}
								}
							}
						}
						else
						{
							// No bag, try backpack directly
							backpack.DropItem(newReagent);
							if (newReagent.Parent == backpack)
							{
								anyReplenished = true;
							}
							else
							{
								// Backpack full, place at feet
								Mobile owner = backpack.RootParent as Mobile;
								if (owner != null)
								{
									newReagent.MoveToWorld(owner.Location, owner.Map);
									anyReplenished = true;
								}
								else
								{
									newReagent.Delete();
								}
							}
						}
					}
				}
			}

			if (anyReplenished)
			{
				Mobile owner = backpack.RootParent as Mobile;
				if (owner != null)
				{
					owner.SendMessage(string.Format(MESSAGE_REPLENISHED, TARGET_REAGENT_AMOUNT));
				}
			}

			return anyReplenished;
		}

		/// <summary>
		/// Gets the list of magery reagent types.
		/// </summary>
		private Type[] GetMageryReagentTypes()
		{
			return new Type[]
			{
				typeof(BlackPearl),
				typeof(Bloodmoss),
				typeof(Garlic),
				typeof(Ginseng),
				typeof(MandrakeRoot),
				typeof(Nightshade),
				typeof(SulfurousAsh),
				typeof(SpidersSilk)
			};
		}

		/// <summary>
		/// Creates a reagent item of the specified type and amount.
		/// </summary>
		private Item CreateReagentItem(Type reagentType, int amount)
		{
			if (reagentType == typeof(BlackPearl))
				return new BlackPearl(amount);
			if (reagentType == typeof(Bloodmoss))
				return new Bloodmoss(amount);
			if (reagentType == typeof(Garlic))
				return new Garlic(amount);
			if (reagentType == typeof(Ginseng))
				return new Ginseng(amount);
			if (reagentType == typeof(MandrakeRoot))
				return new MandrakeRoot(amount);
			if (reagentType == typeof(Nightshade))
				return new Nightshade(amount);
			if (reagentType == typeof(SulfurousAsh))
				return new SulfurousAsh(amount);
			if (reagentType == typeof(SpidersSilk))
				return new SpidersSilk(amount);

			return null;
		}

		/// <summary>
		/// Counts the total number of magery reagents in the player's backpack.
		/// </summary>
		private int CountTotalReagents(Mobile from)
		{
			if (from == null || from.Backpack == null)
				return 0;

			int total = 0;
			Type[] reagentTypes = GetMageryReagentTypes();

			foreach (Type reagentType in reagentTypes)
			{
				total += from.Backpack.GetAmount(reagentType, true);
			}

			return total;
		}

		/// <summary>
		/// Updates the cooldown timestamp for the player.
		/// </summary>
		private void UpdateCooldown(Mobile from)
		{
			ApplyCooldown(from);
		}

		/// <summary>
		/// Public method for gump to apply cooldown after item creation.
		/// </summary>
		public void ApplyCooldown(Mobile from)
		{
			lock (m_CooldownLock)
			{
				// Clean up old entries periodically
				if (m_Cooldowns.Count > 1000)
				{
					CleanupCooldowns();
				}

				m_Cooldowns[from] = DateTime.Now;
			}
		}

		/// <summary>
		/// Removes expired cooldown entries to prevent memory leaks.
		/// </summary>
		private static void CleanupCooldowns()
		{
			List<Mobile> toRemove = new List<Mobile>();
			DateTime cutoff = DateTime.Now.AddMinutes(-COOLDOWN_MINUTES * 2);

			foreach (KeyValuePair<Mobile, DateTime> entry in m_Cooldowns)
			{
				if (entry.Key == null || entry.Key.Deleted || entry.Value < cutoff)
				{
					toRemove.Add(entry.Key);
				}
			}

			foreach (Mobile m in toRemove)
			{
				m_Cooldowns.Remove(m);
			}
		}

		#endregion

		#region Magery Items Management

		/// <summary>
		/// Provides a full spellbook with all spells if the player doesn't have one.
		/// </summary>
		private bool ProvideFullSpellbook(Container backpack)
		{
			if (backpack == null)
				return false;

			// Check if player already has a full spellbook
			Spellbook existingBook = backpack.FindItemByType(typeof(Spellbook)) as Spellbook;
			if (existingBook != null)
			{
				// Check if it's already full (64 spells for regular magery)
				if (existingBook.BookCount == 64 && existingBook.Content == ulong.MaxValue)
				{
					return false; // Already has full spellbook
				}

				// Fill existing spellbook
				if (existingBook.BookCount == 64)
				{
					existingBook.Content = ulong.MaxValue;
				}
				else
				{
					existingBook.Content = (1ul << existingBook.BookCount) - 1;
				}

				Mobile owner = backpack.RootParent as Mobile;
				if (owner != null)
				{
					owner.SendMessage("Seu livro de magia foi preenchido com todos os feitiços.");
				}

				return true;
			}

			// Create new full spellbook
			Spellbook newBook = new Spellbook();
			if (newBook.BookCount == 64)
			{
				newBook.Content = ulong.MaxValue;
			}
			else
			{
				newBook.Content = (1ul << newBook.BookCount) - 1;
			}

			backpack.DropItem(newBook);
			
			// Check if item was successfully added
			if (newBook.Parent == backpack)
			{
				Mobile owner = backpack.RootParent as Mobile;
				if (owner != null)
				{
					owner.SendMessage(MESSAGE_SPELLBOOK_PROVIDED);
				}
				return true;
			}

			// If backpack is full, try to place at player's feet
			Mobile owner2 = backpack.RootParent as Mobile;
			if (owner2 != null)
			{
				newBook.MoveToWorld(owner2.Location, owner2.Map);
				owner2.SendMessage(MESSAGE_SPELLBOOK_PROVIDED);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Provides blank scrolls up to the target amount.
		/// </summary>
		private bool ProvideBlankScrolls(Container backpack)
		{
			if (backpack == null)
				return false;

			int currentAmount = backpack.GetAmount(typeof(BlankScroll), true);
			int needed = TARGET_BLANK_SCROLLS - currentAmount;

			if (needed <= 0)
				return false; // Already has enough

			BlankScroll newScrolls = new BlankScroll(needed);

			backpack.DropItem(newScrolls);
			
			// Check if item was successfully added
			if (newScrolls.Parent == backpack)
			{
				Mobile owner = backpack.RootParent as Mobile;
				if (owner != null)
				{
					owner.SendMessage(MESSAGE_SCROLLS_PROVIDED);
				}
				return true;
			}

			// If backpack is full, try to place at player's feet
			Mobile owner2 = backpack.RootParent as Mobile;
			if (owner2 != null)
			{
				newScrolls.MoveToWorld(owner2.Location, owner2.Map);
				owner2.SendMessage(MESSAGE_SCROLLS_PROVIDED);
				return true;
			}

			return false;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the reagent stone data.
		/// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
			writer.Write((int)SERIALIZATION_VERSION);
        }

		/// <summary>
		/// Deserializes the reagent stone data.
		/// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
			int version = reader.ReadInt();

			// Future version handling can be added here if needed
			if (version > SERIALIZATION_VERSION)
			{
				// Handle future version upgrades
        }
		}

		#endregion
    }
}
