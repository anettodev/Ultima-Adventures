using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Pet Ticket - A tradable item that stores a tamed creature for later redemption.
	/// Expires after 15 days. Anyone can trade and redeem tickets.
	/// </summary>
	public class PetTicket : Item
	{
		#region Constants

		private const int EXPIRATION_DAYS = 15;
		private const int GENERIC_DEED_ITEMID = 0x14F0; // Fallback deed ItemID
		private const int EXPIRATION_CHECK_INTERVAL_HOURS = 8; // Check every 8 hours

		#endregion

		#region Fields

		private Type m_CreatureType;
		private string m_CreatureName;
		private int m_CreatureHue;
		private DateTime m_CreatedDate;
		private Dictionary<string, object> m_CreatureStats;
		private Timer m_ExpirationTimer;

		#endregion

		#region Properties

		[CommandProperty(AccessLevel.GameMaster)]
		public Type CreatureType
		{
			get { return m_CreatureType; }
			set { m_CreatureType = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string CreatureName
		{
			get { return m_CreatureName; }
			set { m_CreatureName = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int CreatureHue
		{
			get { return m_CreatureHue; }
			set { m_CreatureHue = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime CreatedDate
		{
			get { return m_CreatedDate; }
			set { m_CreatedDate = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string ExpirationText
		{
			get { return GetExpirationText(); }
		}

		#endregion

		#region Statue ItemID Mapping

		/// <summary>
		/// Maps creature types to their corresponding statue ItemIDs
		/// Falls back to generic deed if no mapping exists
		/// </summary>
		private static Dictionary<Type, int> s_CreatureStatueIDs = new Dictionary<Type, int>();

		/// <summary>
		/// Initializes the creature type to statue ItemID mapping
		/// </summary>
		static PetTicket()
		{
			// Mount creatures (using ethereal mount statuette IDs)
			s_CreatureStatueIDs[typeof(Horse)] = 0x20DD;
			s_CreatureStatueIDs[typeof(Llama)] = 0x20F6;
			s_CreatureStatueIDs[typeof(Ostard)] = 0x2135;
			s_CreatureStatueIDs[typeof(Ridgeback)] = 0x2615;
			s_CreatureStatueIDs[typeof(Unicorn)] = 0x25CE;
			s_CreatureStatueIDs[typeof(Beetle)] = 0x260F;
			s_CreatureStatueIDs[typeof(Kirin)] = 0x25A0;
			s_CreatureStatueIDs[typeof(SwampDragon)] = 0x2619;
			s_CreatureStatueIDs[typeof(PolarBear)] = 0x20E1;
			s_CreatureStatueIDs[typeof(CuSidhe)] = 0x2D96;
			s_CreatureStatueIDs[typeof(Hiryu)] = 0x276A;
			s_CreatureStatueIDs[typeof(Reptalon)] = 0x2d95;

			// Common animals (using MonsterStatuette IDs)
			s_CreatureStatueIDs[typeof(Cow)] = 0x2103;
			s_CreatureStatueIDs[typeof(Wolf)] = 0x2122;
			s_CreatureStatueIDs[typeof(Gorilla)] = 0x20F5;
			s_CreatureStatueIDs[typeof(Llama)] = 0x20F6; // Already mapped above

			// Add more creature types as needed
			// Note: If a creature type is not in this dictionary, it will use the generic deed ItemID
		}

		/// <summary>
		/// Gets the statue ItemID for a specific creature type
		/// </summary>
		private static int GetStatueItemID(Type creatureType)
		{
			if (creatureType == null)
				return GENERIC_DEED_ITEMID;

			// Check direct type match
			if (s_CreatureStatueIDs.ContainsKey(creatureType))
				return s_CreatureStatueIDs[creatureType];

			// Check if any base type matches
			Type baseType = creatureType.BaseType;
			while (baseType != null && baseType != typeof(object))
			{
				if (s_CreatureStatueIDs.ContainsKey(baseType))
					return s_CreatureStatueIDs[baseType];
				baseType = baseType.BaseType;
			}

			// Fallback to generic deed
			return GENERIC_DEED_ITEMID;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new PetTicket from a creature
		/// </summary>
		public PetTicket(BaseCreature creature) : base(GetStatueItemID(creature.GetType()))
		{
			if (creature == null)
				throw new ArgumentNullException("creature");

			m_CreatureType = creature.GetType();
			m_CreatureName = creature.Name ?? creature.GetType().Name;
			m_CreatureHue = creature.Hue;
			m_CreatedDate = DateTime.UtcNow;
			m_CreatureStats = new Dictionary<string, object>();

			// Save creature stats
			SaveCreatureStats(creature);

			// Set item properties
			Name = string.Format("Ticket de {0}", m_CreatureName);
			Hue = m_CreatureHue;
			Weight = 1.0;
			Movable = true;
			LootType = LootType.Regular; // Not blessed, can be traded

			// Start expiration timer
			StartExpirationTimer();
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public PetTicket(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Expiration System

		/// <summary>
		/// Checks if the ticket has expired
		/// </summary>
		public bool IsExpired()
		{
			if (m_CreatedDate == DateTime.MinValue)
				return false;

			TimeSpan age = DateTime.UtcNow - m_CreatedDate;
			return age.TotalDays >= EXPIRATION_DAYS;
		}

		/// <summary>
		/// Gets the expiration text for display
		/// </summary>
		private string GetExpirationText()
		{
			if (m_CreatedDate == DateTime.MinValue)
				return "Data de criação inválida";

			TimeSpan age = DateTime.UtcNow - m_CreatedDate;
			TimeSpan remaining = TimeSpan.FromDays(EXPIRATION_DAYS) - age;

			if (remaining <= TimeSpan.Zero)
				return "Expirado";

			int days = (int)remaining.TotalDays;
			return string.Format("expira em: {0} dias", days);
		}

		/// <summary>
		/// Checks expiration and deletes ticket if expired
		/// </summary>
		private void CheckExpiration()
		{
			if (IsExpired() && !Deleted)
			{
				Delete();
			}
		}

		/// <summary>
		/// Starts the expiration check timer
		/// </summary>
		private void StartExpirationTimer()
		{
			if (m_ExpirationTimer != null)
				m_ExpirationTimer.Stop();

			m_ExpirationTimer = Timer.DelayCall(TimeSpan.FromHours(EXPIRATION_CHECK_INTERVAL_HOURS), TimeSpan.FromHours(EXPIRATION_CHECK_INTERVAL_HOURS), new TimerCallback(OnExpirationTick));
		}

		/// <summary>
		/// Stops the expiration check timer
		/// </summary>
		private void StopExpirationTimer()
		{
			if (m_ExpirationTimer != null)
			{
				m_ExpirationTimer.Stop();
				m_ExpirationTimer = null;
			}
		}

		/// <summary>
		/// Timer callback for expiration checks
		/// </summary>
		private void OnExpirationTick()
		{
			if (Deleted)
			{
				StopExpirationTimer();
				return;
			}

			CheckExpiration();
		}

		#endregion

		#region Creature Stats Management

		/// <summary>
		/// Saves essential creature stats for later restoration
		/// </summary>
		private void SaveCreatureStats(BaseCreature creature)
		{
			if (creature == null || m_CreatureStats == null)
				return;

			m_CreatureStats["HitPoints"] = creature.Hits;
			m_CreatureStats["MaxHitPoints"] = creature.HitsMax;
			m_CreatureStats["Mana"] = creature.Mana;
			m_CreatureStats["MaxMana"] = creature.ManaMax;
			m_CreatureStats["Stamina"] = creature.Stam;
			m_CreatureStats["MaxStamina"] = creature.StamMax;
			m_CreatureStats["Str"] = creature.RawStr;
			m_CreatureStats["Dex"] = creature.RawDex;
			m_CreatureStats["Int"] = creature.RawInt;
		}

		/// <summary>
		/// Restores creature stats from saved data
		/// </summary>
		private void RestoreCreatureStats(BaseCreature creature)
		{
			if (creature == null || m_CreatureStats == null)
				return;

			if (m_CreatureStats.ContainsKey("MaxHitPoints"))
				creature.HitsMax = (int)m_CreatureStats["MaxHitPoints"];
			if (m_CreatureStats.ContainsKey("HitPoints"))
				creature.Hits = Math.Min(creature.Hits, creature.HitsMax);

			if (m_CreatureStats.ContainsKey("MaxMana"))
				creature.ManaMax = (int)m_CreatureStats["MaxMana"];
			if (m_CreatureStats.ContainsKey("Mana"))
				creature.Mana = Math.Min(creature.Mana, creature.ManaMax);

			if (m_CreatureStats.ContainsKey("MaxStamina"))
				creature.StamMax = (int)m_CreatureStats["MaxStamina"];
			if (m_CreatureStats.ContainsKey("Stamina"))
				creature.Stam = Math.Min(creature.Stam, creature.StamMax);

			if (m_CreatureStats.ContainsKey("Str"))
				creature.RawStr = (int)m_CreatureStats["Str"];
			if (m_CreatureStats.ContainsKey("Dex"))
				creature.RawDex = (int)m_CreatureStats["Dex"];
			if (m_CreatureStats.ContainsKey("Int"))
				creature.RawInt = (int)m_CreatureStats["Int"];
		}

		#endregion

		#region Redemption System

		/// <summary>
		/// Handles dropping the ticket on an AnimalTrainer to redeem the creature
		/// </summary>
		public override bool OnDroppedToMobile(Mobile from, Mobile target)
		{
			if (target is AnimalTrainer)
			{
				AnimalTrainer trainer = (AnimalTrainer)target;
				RedeemTicket(from, trainer);
				return false; // Don't allow normal drop behavior
			}

			return base.OnDroppedToMobile(from, target);
		}

		/// <summary>
		/// Redeems the ticket and recreates the creature for the redeemer
		/// </summary>
		private void RedeemTicket(Mobile redeemer, AnimalTrainer trainer)
		{
			if (redeemer == null || trainer == null || Deleted)
				return;

			// Check expiration (fallback validation)
			if (IsExpired())
			{
				trainer.SayTo(redeemer, "Este ticket expirou e não pode mais ser usado.");
				Delete();
				return;
			}

			// Validate creature type can be created
			if (m_CreatureType == null)
			{
				trainer.SayTo(redeemer, "Este ticket está corrompido e não pode ser usado.");
				Delete();
				return;
			}

			// Check if redeemer has follower slots available
			BaseCreature tempCreature = null;
			try
			{
				tempCreature = (BaseCreature)Activator.CreateInstance(m_CreatureType);
				if (tempCreature == null)
					throw new Exception("Failed to create creature instance");
			}
			catch
			{
				trainer.SayTo(redeemer, "Não foi possível criar este tipo de criatura.");
				Delete();
				return;
			}

			if ((redeemer.Followers + tempCreature.ControlSlots) > redeemer.FollowersMax)
			{
				trainer.SayTo(redeemer, "Você tem seguidores demais para resgatar este animal.");
				tempCreature.Delete();
				return;
			}

			// Create the creature
			BaseCreature creature = tempCreature;
			creature.Name = m_CreatureName;
			creature.Hue = m_CreatureHue;

			// Restore stats
			RestoreCreatureStats(creature);

			// Set control to redeemer
			creature.SetControlMaster(redeemer);
			creature.Controlled = true;
			creature.ControlTarget = redeemer;
			creature.ControlOrder = OrderType.Follow;
			creature.Loyalty = BaseCreature.MaxLoyalty;

			// Move to trainer location
			creature.MoveToWorld(trainer.Location, trainer.Map);

			// Delete ticket
			Delete();

			// Success message
			trainer.SayTo(redeemer, string.Format("Seu animal {0} foi restaurado com sucesso!", creature.Name));
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Adds expiration info to properties
		/// </summary>
		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (m_CreatedDate != DateTime.MinValue)
			{
				list.Add(GetExpirationText());
			}
		}

		/// <summary>
		/// Checks expiration when item is moved
		/// </summary>
		public override void OnLocationChange(Point3D oldLocation)
		{
			base.OnLocationChange(oldLocation);
			CheckExpiration();
		}

		/// <summary>
		/// Stops timer when item is deleted
		/// </summary>
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();
			StopExpirationTimer();
		}

		#endregion

		#region Serialization

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write(m_CreatureType != null ? m_CreatureType.FullName : "");
			writer.Write(m_CreatureName);
			writer.Write(m_CreatureHue);
			writer.Write(m_CreatedDate);

			// Serialize stats dictionary
			writer.Write(m_CreatureStats != null ? m_CreatureStats.Count : 0);
			if (m_CreatureStats != null)
			{
				foreach (KeyValuePair<string, object> kvp in m_CreatureStats)
				{
					writer.Write(kvp.Key);
					writer.Write(kvp.Value != null ? kvp.Value.ToString() : "");
				}
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			string typeName = reader.ReadString();
			if (!string.IsNullOrEmpty(typeName))
			{
				m_CreatureType = ScriptCompiler.FindTypeByFullName(typeName);
			}

			m_CreatureName = reader.ReadString();
			m_CreatureHue = reader.ReadInt();
			m_CreatedDate = reader.ReadDateTime();

			// Deserialize stats dictionary
			int statCount = reader.ReadInt();
			m_CreatureStats = new Dictionary<string, object>();
			for (int i = 0; i < statCount; i++)
			{
				string key = reader.ReadString();
				string valueStr = reader.ReadString();
				int intValue;
				if (int.TryParse(valueStr, out intValue))
				{
					m_CreatureStats[key] = intValue;
				}
				else
				{
					m_CreatureStats[key] = valueStr;
				}
			}

			// Check expiration on deserialize
			CheckExpiration();

			// Restart expiration timer
			if (!Deleted)
				StartExpirationTimer();
		}

		#endregion
	}
}

