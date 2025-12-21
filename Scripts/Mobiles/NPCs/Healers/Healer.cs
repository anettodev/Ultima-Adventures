using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;

namespace Server.Mobiles
{
	/// <summary>
	/// Healer NPC that provides healing services and teaches healing-related skills.
	/// Can train players in Forensics, Healing, SpiritSpeak, and Swords skills.
	/// </summary>
	public class Healer : BaseHealer
	{
		#region Properties

		/// <summary>
		/// Indicates whether this healer can teach skills.
		/// </summary>
		public override bool CanTeach{ get{ return true; } }

		/// <summary>
		/// Indicates whether this healer is an active vendor.
		/// </summary>
		public override bool IsActiveVendor{ get{ return true; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Healer class.
		/// </summary>
		[Constructable]
		public Healer()
		{
			Title = HealerStringConstants.TITLE_HEALER;

			if ( !Core.AOS )
				NameHue = HealerConstants.NAME_HUE_CLASSIC;

			SetSkill( SkillName.Forensics, HealerConstants.SKILL_MIN, HealerConstants.SKILL_MAX );
			SetSkill( SkillName.SpiritSpeak, HealerConstants.SKILL_MIN, HealerConstants.SKILL_MAX );
			SetSkill( SkillName.Swords, HealerConstants.SKILL_MIN, HealerConstants.SKILL_MAX );
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public Healer( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the vendor buy/sell information for this healer.
		/// </summary>
		public override void InitSBInfo()
		{
			SBInfos.Add( new SBHealer() );

			if ( Map == Map.Felucca )
				SBInfos.Add( new SBElfHealer() );
		}

		/// <summary>
		/// Gets a random robe color for the healer's outfit.
		/// </summary>
		/// <returns>A random yellow hue value</returns>
		public virtual int GetRobeColor()
		{
			return Utility.RandomYellowHue();
		}

		/// <summary>
		/// Initializes the healer's outfit, adding a robe with a random color.
		/// </summary>
		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Robe( GetRobeColor() ) );
		}

		#endregion

		#region Teaching System

		/// <summary>
		/// Checks whether the healer can teach the specified skill to the given mobile.
		/// </summary>
		/// <param name="skill">The skill to check</param>
		/// <param name="from">The mobile requesting training</param>
		/// <returns>True if the skill can be taught, false otherwise</returns>
		public override bool CheckTeach( SkillName skill, Mobile from )
		{
			if ( !base.CheckTeach( skill, from ) )
				return false;

			return ( skill == SkillName.Forensics )
				|| ( skill == SkillName.Healing )
				|| ( skill == SkillName.SpiritSpeak )
				|| ( skill == SkillName.Swords );
		}

		#endregion

		#region Context Menu

		/// <summary>
		/// Adds context menu entries for the healer, including the speech gump option.
		/// </summary>
		/// <param name="from">The mobile viewing the context menu</param>
		/// <param name="list">The list of context menu entries to populate</param>
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the healer's data.
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) HealerConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the healer's data.
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Core.AOS && NameHue == HealerConstants.NAME_HUE_CLASSIC )
				NameHue = HealerConstants.NAME_HUE_RESET;
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Context menu entry for opening the healer's speech gump.
		/// </summary>
		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			/// <summary>
			/// Initializes a new instance of the SpeechGumpEntry class.
			/// </summary>
			/// <param name="from">The mobile that will receive the gump</param>
			/// <param name="giver">The healer NPC providing the speech</param>
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( HealerConstants.CONTEXT_MENU_SPEECH_ID, HealerConstants.CONTEXT_MENU_SPEECH_RANGE )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			/// <summary>
			/// Handles the click event, opening the speech gump if the mobile is a player and doesn't already have the gump open.
			/// </summary>
			public override void OnClick()
			{
				if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				
				if ( !mobile.HasGump( typeof( SpeechGump ) ) )
				{
					mobile.SendGump( new SpeechGump( HealerStringConstants.SPEECH_GUMP_TITLE, SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, HealerConstants.SPEECH_NPC_TYPE ) ) );
				}
			}
		}

		#endregion
	}
}