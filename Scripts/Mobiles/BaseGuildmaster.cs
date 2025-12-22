using System;
using Server;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.ContextMenus;

namespace Server.Mobiles
{
	public abstract class BaseGuildmaster : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override bool IsActiveVendor{ get{ return true; } }

		public override bool ClickTitle{ get{ return true; } }

		public virtual int JoinCost{ get{ return 2000; } }

		public override void InitSBInfo()
		{
		}

		public virtual void SayGuildTo( Mobile m )
		{
			SayTo( m, 1008055 + (int)NpcGuild );
		}

		public virtual void SayWelcomeTo( Mobile m )
		{
			SayTo( m, BaseGuildmasterStringConstants.MSG_WELCOME_TO_GUILD );
		}

		public static void SayPriceTo( Mobile m, Mobile guildmaster )
		{
			m.Send( new MessageLocalizedAffix( guildmaster.Serial, guildmaster.Body, MessageType.Regular, guildmaster.SpeechHue, 3, 1008052, guildmaster.Name, AffixType.Append, JoiningFee( m ).ToString(), "" ) );
		}

		public static int JoiningFee( Mobile m )
		{
			int fee = 2000;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB != null )
			{
				fee = DB.CharacterGuilds;
			}

			if ( fee < 2000 ){ fee = 2000; }

			if ( GetPlayerInfo.isFromSpace( m ) ){ fee = fee * 4; }

			return fee;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{
			PlayerMobile pm = (PlayerMobile)from;
			base.GetContextMenuEntries( from, list ); 
			list.Add( new JoinEntry( from, this ) ); 
			if ( pm.NpcGuild == this.NpcGuild ){ list.Add( new ResignEntry( from, this ) ); }
		} 

		public class JoinEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Guildmaster;
			
			public JoinEntry( Mobile from, Mobile guildmaster ) : base( 6116, 3 )
			{
				m_Mobile = from;
				m_Guildmaster = guildmaster;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				JoinGuild( m_Mobile, m_Guildmaster );
            }
        }

		public class ResignEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Guildmaster;
			
			public ResignEntry( Mobile from, Mobile guildmaster ) : base( 6115, 3 )
			{
				m_Mobile = from;
				m_Guildmaster = guildmaster;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				ResignGuild( m_Mobile, m_Guildmaster );
            }
        }

		public static void JoinGuild( Mobile player, Mobile guildmaster )
		{
			PlayerMobile pm = (PlayerMobile)player;

			if ( player.Blessed )
				guildmaster.SayTo( player, BaseGuildmasterStringConstants.MSG_BLESSED_CANNOT_JOIN );
			else if ( ((PlayerMobile)player).Profession == 1 )
				guildmaster.SayTo( player, BaseGuildmasterStringConstants.MSG_PROFESSION_CANNOT_JOIN );
			else if ( ((BaseVendor)guildmaster).NpcGuild == NpcGuild.ThievesGuild && pm.Karma > -500 )
				guildmaster.SayTo( player, BaseGuildmasterStringConstants.MSG_KARMA_TOO_HIGH_THIEVES );
			else if ( pm.NpcGuild == ((BaseVendor)guildmaster).NpcGuild )
				guildmaster.SayTo( player, 501047 ); // Thou art already a member of our guild.
			else if ( pm.NpcGuild != NpcGuild.None )
				guildmaster.SayTo( player, 501046 ); // Thou must resign from thy other guild first.
			else
				SayPriceTo( player, guildmaster );
		}

		public static void ResignGuild( Mobile player, Mobile guildmaster )
		{
			PlayerMobile pm = (PlayerMobile)player;

			if ( pm.NpcGuild != ((BaseVendor)guildmaster).NpcGuild )
			{
				guildmaster.SayTo( player, 501052 ); // Thou dost not belong to my guild!
			}
			else
			{
				guildmaster.SayTo( player, 501054 ); // I accept thy resignation.
				pm.NpcGuild = NpcGuild.None;

				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( player );

				if ( DB.CharacterGuilds > 0 )
				{
					int fees = DB.CharacterGuilds;
					DB.CharacterGuilds = (int)(fees * 1.5);
				}
				else
				{
					DB.CharacterGuilds = 4000;
				}

				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( item is GuildRings )
				{
					GuildRings guildring = (GuildRings)item;
					if ( guildring.RingOwner == player )
					{
						targets.Add( item );
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}
			}

		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			PlayerMobile pm = (PlayerMobile)from;
			if ( dropped is BankCheck && from is PlayerMobile && !from.Blessed && ((PlayerMobile)from).Profession != 1 )
			{
				if ( ((BankCheck)dropped).Worth == JoiningFee( from ) )
				{
					if ( pm.NpcGuild == this.NpcGuild )
					{
						SayTo( from, 501047 ); // Thou art already a member of our guild.
					}
					else if ( pm.NpcGuild != NpcGuild.None )
					{
						SayTo( from, 501046 ); // Thou must resign from thy other guild first.
					}
					else
					{
						SayWelcomeTo( from );

						pm.NpcGuild = this.NpcGuild;
						pm.NpcGuildJoinTime = DateTime.UtcNow;
						pm.NpcGuildGameTime = pm.GameTime;

						dropped.Delete();

						ArrayList targets = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is GuildRings )
						{
							GuildRings guildring = (GuildRings)item;
							if ( guildring.RingOwner == from )
							{
								targets.Add( item );
							}
						}
						for ( int i = 0; i < targets.Count; ++i )
						{
							Item item = ( Item )targets[ i ];
							item.Delete();
						}

						int GuildType = 1;
						if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
						else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
						else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
						else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
						else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
						else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
						else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
						else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
						else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
						else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
						else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
						else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
						else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
						else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
						else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
						else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
						else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
						else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
						else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
						else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
						else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

						from.AddToBackpack( new GuildRings( from, GuildType ) );
						from.SendSound( 0x3D );

						return true;
					}

					return false;
				}
				if ( from is PlayerMobile && ((BankCheck)dropped).Worth == 400 && pm.NpcGuild == this.NpcGuild )
				{
					dropped.Delete();

					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					if ( item is GuildRings )
					{
						GuildRings guildring = (GuildRings)item;
						if ( guildring.RingOwner == from )
						{
							targets.Add( item );
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];
						item.Delete();
					}

					int GuildType = 1;
					if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
					else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
					else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
					else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
					else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
					else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
					else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
					else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
					else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
					else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
					else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
					else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
					else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
					else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
					else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
					else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
					else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
					else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
					else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
					else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
					else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

					this.Say( BaseGuildmasterStringConstants.MSG_REPLACEMENT_RING );
					from.AddToBackpack( new GuildRings( from, GuildType ) );

					return true;
				}
			}
			
			return base.OnDragDrop( from, dropped );

		}


				
		public override bool OnGoldGiven( Mobile from, Gold dropped )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( from is PlayerMobile && !from.Blessed && dropped.Amount == JoiningFee( from ) && ((PlayerMobile)from).Profession != 1 )
			{
				if ( pm.NpcGuild == this.NpcGuild )
				{
					SayTo( from, 501047 ); // Thou art already a member of our guild.
				}
				else if ( pm.NpcGuild != NpcGuild.None )
				{
					SayTo( from, 501046 ); // Thou must resign from thy other guild first.
				}
				else
				{
					SayWelcomeTo( from );

					pm.NpcGuild = this.NpcGuild;
					pm.NpcGuildJoinTime = DateTime.UtcNow;
					pm.NpcGuildGameTime = pm.GameTime;

					dropped.Delete();

					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					if ( item is GuildRings )
					{
						GuildRings guildring = (GuildRings)item;
						if ( guildring.RingOwner == from )
						{
							targets.Add( item );
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];
						item.Delete();
					}

					int GuildType = 1;
					if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
					else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
					else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
					else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
					else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
					else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
					else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
					else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
					else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
					else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
					else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
					else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
					else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
					else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
					else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
					else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
					else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
					else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
					else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
					else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
					else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

					from.AddToBackpack( new GuildRings( from, GuildType ) );
					from.SendSound( 0x3D );

					return true;
				}

				return false;
			}

			if ( from is PlayerMobile && dropped.Amount == 400 && pm.NpcGuild == this.NpcGuild )
			{
				dropped.Delete();

				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( item is GuildRings )
				{
					GuildRings guildring = (GuildRings)item;
					if ( guildring.RingOwner == from )
					{
						targets.Add( item );
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}

				int GuildType = 1;
				if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
				else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
				else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
				else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
				else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
				else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
				else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
				else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
				else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
				else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
				else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
				else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
				else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
				else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
				else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
				else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
				else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
				else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
				else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
				else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
				else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

				this.Say( BaseGuildmasterStringConstants.MSG_REPLACEMENT_RING );
				from.AddToBackpack( new GuildRings( from, GuildType ) );

				return true;
			}

			return base.OnGoldGiven( from, dropped );
		}

		public BaseGuildmaster( string title ) : base( title )
		{
			Title = FormatTitleWithPronoun( title );
		}

		/// <summary>
		/// Formats the title with the appropriate Portuguese pronoun (a/o) based on gender.
		/// Replaces existing pronouns if present and transforms gender endings.
		/// </summary>
		/// <param name="title">The title to format</param>
		/// <returns>Formatted title with appropriate pronoun and gender ending</returns>
		private string FormatTitleWithPronoun( string title )
		{
			if ( string.IsNullOrEmpty( title ) )
				return title;

			// Determine the correct pronoun based on gender
			string pronoun = this.Female ? "a" : "o";

			// Remove existing pronouns if present (case-insensitive)
			string trimmedTitle = title.TrimStart();
			if ( trimmedTitle.StartsWith( "a ", System.StringComparison.OrdinalIgnoreCase ) )
			{
				trimmedTitle = trimmedTitle.Substring( 2 ).TrimStart();
			}
			else if ( trimmedTitle.StartsWith( "o ", System.StringComparison.OrdinalIgnoreCase ) )
			{
				trimmedTitle = trimmedTitle.Substring( 2 ).TrimStart();
			}

			// Transform gender endings if female (transform only the last word)
			if ( this.Female )
			{
				trimmedTitle = TransformTitleToFeminine( trimmedTitle );
			}

			// Format with the correct pronoun
			return string.Format( " {0} {1}", pronoun, trimmedTitle );
		}

		/// <summary>
		/// Transforms a title to feminine form by transforming the last word.
		/// Handles multi-word titles by only transforming the final word.
		/// </summary>
		/// <param name="title">The title to transform</param>
		/// <returns>Transformed title with feminine ending on the last word</returns>
		private string TransformTitleToFeminine( string title )
		{
			if ( string.IsNullOrEmpty( title ) )
				return title;

			// Split into words
			string[] words = title.Split( new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries );
			
			if ( words.Length == 0 )
				return title;

			// Transform only the last word
			int lastIndex = words.Length - 1;
			words[lastIndex] = TransformToFeminine( words[lastIndex] );

			// Rejoin words
			return string.Join( " ", words );
		}

		/// <summary>
		/// Transforms Portuguese masculine word endings to feminine endings.
		/// Handles common PT-BR gender transformation patterns.
		/// </summary>
		/// <param name="word">The word to transform</param>
		/// <returns>Transformed word with feminine ending, or original if no transformation applies</returns>
		private string TransformToFeminine( string word )
		{
			if ( string.IsNullOrEmpty( word ) )
				return word;

			// Handle common PT-BR masculine endings (check more specific patterns first)
			// -eiro → -eira (e.g., "Marinheiro" → "Marinheira", "Cavaleiro" → "Cavaleira")
			if ( word.EndsWith( "eiro" ) && !word.EndsWith( "eira" ) )
			{
				return word.Substring( 0, word.Length - 4 ) + "eira";
			}
			// -dor → -dora (e.g., "Domador" → "Domadora", "Meditador" → "Meditadora")
			else if ( word.EndsWith( "dor" ) && !word.EndsWith( "dora" ) )
			{
				return word.Substring( 0, word.Length - 3 ) + "dora";
			}
			// Special case: "Brigão" → "Brigona"
			else if ( word.EndsWith( "Brigão" ) )
			{
				return word.Replace( "Brigão", "Brigona" );
			}
			// -rão → -ra (e.g., "Ladrão" → "Ladra")
			else if ( word.EndsWith( "rão" ) && !word.EndsWith( "ra" ) )
			{
				return word.Substring( 0, word.Length - 3 ) + "ra";
			}
			// -ão → -ã (e.g., "Capitão" → "Capitã")
			else if ( word.EndsWith( "ão" ) && !word.EndsWith( "ã" ) && !word.EndsWith( "ra" ) )
			{
				return word.Substring( 0, word.Length - 2 ) + "ã";
			}
			// -ino → -ina (e.g., "Ladino" → "Ladina")
			else if ( word.EndsWith( "ino" ) && !word.EndsWith( "ina" ) )
			{
				return word.Substring( 0, word.Length - 3 ) + "ina";
			}
			// -o → -a (e.g., "Mago" → "Maga", "Ladino" → "Ladina")
			// Skip if already feminine, ends with -ista (gender-neutral), or matches other patterns above
			else if ( word.EndsWith( "o" ) && !word.EndsWith( "a" ) && !word.EndsWith( "ista" ) && !word.EndsWith( "eiro" ) && !word.EndsWith( "dor" ) && !word.EndsWith( "ão" ) && !word.EndsWith( "rão" ) && !word.EndsWith( "ino" ) )
			{
				return word.Substring( 0, word.Length - 1 ) + "a";
			}

			// No transformation needed (already feminine or gender-neutral)
			return word;
		}

		public BaseGuildmaster( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}