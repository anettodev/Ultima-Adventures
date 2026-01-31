using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Prompts;
using Server.Network;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
    public class SoulOrb : Item
    {
		private static Dictionary<Mobile, SoulOrb> m_ResList;
		
        public Mobile m_Owner;

        [CommandProperty( AccessLevel.GameMaster )]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; InvalidateProperties(); }
        }

        private Timer m_Timer;
        private Timer m_ExpirationTimer;
        private static TimeSpan m_Delay = TimeSpan.FromSeconds( 30.0 ); /*TimeSpan.Zero*/
        private TimeSpan m_ExpirationTime = TimeSpan.FromSeconds(30.0);

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan Delay { get { return m_Delay; } set { m_Delay = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan ExpirationTime
        {
            get { return m_ExpirationTime; }
            set
            {
                m_ExpirationTime = value;
                StartExpirationTimer();
            }
        }

        /// <summary>Karma threshold for positive karma (gold color)</summary>
        private const int POSITIVE_KARMA_THRESHOLD = 1000;

        /// <summary>Karma threshold for negative karma (red color)</summary>
        private const int NEGATIVE_KARMA_THRESHOLD = -1000;

        /// <summary>Hue for positive karma (gold)</summary>
        private const int HUE_POSITIVE_KARMA = 1174;

        /// <summary>Hue for negative karma (red)</summary>
        private const int HUE_NEGATIVE_KARMA = 1800;
	
        public static void Initialize()
        {
            EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_Death);
        }

        [Constructable]
        public SoulOrb() : base( 0x2C84 ) 
        {
            Name = "orbe da alma";
            LootType = LootType.Blessed;
			Movable = false;
            Weight = 1.0;
            UpdateHueBasedOnKarma();
		}

        /// <summary>
        /// Updates the hue of the SoulOrb based on owner's karma
        /// </summary>
        private void UpdateHueBasedOnKarma()
        {
            if (m_Owner == null)
            {
                Hue = 0; // Default
                return;
            }

            int karma = m_Owner.Karma;
            if (karma >= POSITIVE_KARMA_THRESHOLD)
            {
                Hue = HUE_POSITIVE_KARMA; // Gold
            }
            else if (karma <= NEGATIVE_KARMA_THRESHOLD)
            {
                Hue = HUE_NEGATIVE_KARMA; // Red
            }
            else
            {
                Hue = 0; // Default
            }
        }

        /// <summary>
        /// Starts the expiration timer for the SoulOrb
        /// </summary>
        private void StartExpirationTimer()
        {
            if (m_ExpirationTimer != null)
            {
                m_ExpirationTimer.Stop();
                m_ExpirationTimer = null;
            }

            if (m_ExpirationTime > TimeSpan.Zero)
            {
                m_ExpirationTimer = Timer.DelayCall(m_ExpirationTime, new TimerCallback(OnExpiration));
            }
        }

        /// <summary>
        /// Called when the SoulOrb expires
        /// </summary>
        private void OnExpiration()
        {
            if (m_Owner != null && !m_Owner.Deleted)
            {
                m_Owner.SendMessage("Seu orbe da alma expirou e desapareceu.");
            }

            if (m_ResList != null && m_Owner != null && m_ResList.ContainsKey(m_Owner))
            {
                m_ResList.Remove(m_Owner);
            }

            Delete();
        }

        public SoulOrb(Serial serial) : base(serial){}

		public static void OnSummoned( Mobile from, SoulOrb orb )
		{
			if( m_ResList != null )
				m_ResList.Remove( from );

			if( m_ResList == null )
				m_ResList = new Dictionary<Mobile, SoulOrb>();

			if(from != null && orb != null && !m_ResList.ContainsValue(orb))
			{
				m_ResList.Add(from, orb);
				orb.UpdateHueBasedOnKarma();
				orb.InvalidateProperties();
			}
		}
		
		private static void EventSink_Death(PlayerDeathEventArgs e)
        {
            PlayerMobile owner = e.Mobile as PlayerMobile;

            if (owner != null && !owner.Deleted)
            {
                if (owner.Alive)
                    return;
				
				if(m_ResList != null && m_ResList.ContainsKey(owner))
				{
					SoulOrb arp = m_ResList[owner];
					if(arp == null || arp.Deleted)
					{
						m_ResList.Remove(owner);
						return;
					}
					BuffInfo.AddBuff( owner, new BuffInfo( BuffIcon.GiftOfLife, 1015222,TimeSpan.FromSeconds(30), owner, String.Format("Você será ressuscitado em 30 segundos após sua morte"),true ) );
					arp.m_Timer = Timer.DelayCall(m_Delay, new TimerStateCallback(Resurrect_OnTick), new object[] { owner, arp });
					m_ResList.Remove(owner);
				}
            }
        }

        private static void Resurrect_OnTick(object state)
        {
            object[] states = (object[])state;
            PlayerMobile owner = (PlayerMobile)states[0];
			SoulOrb arp = (SoulOrb)states[1];
            if (owner != null && !owner.Deleted && arp != null && !arp.Deleted)
            {
                if (owner.Alive)
                    return;

				if ( arp.Name == "blood of a vampire" ){ owner.SendMessage("O sangue derrama da garrafa, restaurando sua vida."); }
				else if ( arp.Name == "cloning crystal" ){ owner.SendMessage("O cristal forma um clone do seu corpo, restaurando sua vida."); }
                else { owner.SendMessage("O orbe brilha, liberando sua alma."); }
                owner.Resurrect();
                owner.FixedEffect( 0x376A, 10, 16, Server.Items.CharacterDatabase.GetMySpellHue( owner, 0 ), 0 );
                Server.Misc.Death.Penalty( owner, false );
                BuffInfo.RemoveBuff(owner, BuffIcon.GiftOfLife);
                arp.Delete();
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

            string ownerName = m_Owner != null ? m_Owner.Name : "Desconhecido";
            string description = "";

			if ( this.Name == "blood of a vampire" )
            {
                description = "Contém sangue de vampiro de " + ownerName;
            }
			else if ( this.Name == "cloning crystal" )
            {
                description = "Contém padrões genéticos de " + ownerName;
            }
			else
            {
                description = "Contém a Alma de " + ownerName;
            }

            // Apply color based on karma (same as item hue)
            if (Hue == HUE_POSITIVE_KARMA)
            {
                list.Add(1049644, String.Format("<BASEFONT COLOR=#{0}>{1}</BASEFONT>", GetColorFromHue(HUE_POSITIVE_KARMA), description));
            }
            else if (Hue == HUE_NEGATIVE_KARMA)
            {
                list.Add(1049644, String.Format("<BASEFONT COLOR=#{0}>{1}</BASEFONT>", GetColorFromHue(HUE_NEGATIVE_KARMA), description));
            }
            else
            {
                list.Add(1049644, description);
            }
        }

        /// <summary>
        /// Converts a hue value to HTML color code
        /// </summary>
        private string GetColorFromHue(int hue)
        {
            // Gold (1174) -> #FFD700
            if (hue == HUE_POSITIVE_KARMA)
                return "FFD700";
            
            // Red (1800) -> #DC143C
            if (hue == HUE_NEGATIVE_KARMA)
                return "DC143C";
            
            return "FFFFFF"; // Default white
        } 

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write( (int) 1 ); // version
            writer.Write(m_Owner);
            writer.Write(m_ExpirationTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			
            if (version >= 1)
            {
                m_Owner = reader.ReadMobile();
                m_ExpirationTime = reader.ReadTimeSpan();
            }
            else
            {
                m_ExpirationTime = TimeSpan.FromSeconds(30.0);
            }

            // Delete on world load (old behavior)
            if (version == 0)
            {
                this.Delete();
                return;
            }

            // Start expiration timer if orb exists
            if (m_Owner != null && !Deleted)
            {
                UpdateHueBasedOnKarma();
                StartExpirationTimer();
            }
        }

        public override void OnDelete()
        {
            if (m_ExpirationTimer != null)
            {
                m_ExpirationTimer.Stop();
                m_ExpirationTimer = null;
            }

            if (m_Timer != null)
            {
                m_Timer.Stop();
                m_Timer = null;
            }

            if (m_ResList != null && m_Owner != null && m_ResList.ContainsKey(m_Owner))
            {
                m_ResList.Remove(m_Owner);
            }

            base.OnDelete();
        }
    }
}