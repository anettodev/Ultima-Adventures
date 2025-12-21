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

namespace Server.Mobiles
{
	/// <summary>
	/// Thief Guildmaster NPC that manages the Thieves Guild.
	/// Provides job assignments (ThiefNote) and tithe services for flagged players.
	/// Tracks flagged players who have used the tithe service to prevent abuse.
	/// </summary>
	public class ThiefGuildmaster : BaseGuildmaster
	{
		#region Fields

		/// <summary>Set of mobiles that have used the tithe service today. Uses HashSet for O(1) lookup performance.</summary>
		static HashSet<Mobile> flaggedtargets = new HashSet<Mobile>();

		#endregion

		#region Properties

		/// <summary>Gets the NPC guild this guildmaster belongs to</summary>
		public override NpcGuild NpcGuild{ get{ return NpcGuild.ThievesGuild; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ThiefGuildmaster NPC.
		/// </summary>
		[Constructable]
		public ThiefGuildmaster() : base( ThiefGuildmasterConstants.TITLE_THIEF )
		{
			Job = JobFragment.thief;
			Karma = Utility.RandomMinMax( VendorConstants.KARMA_MIN, VendorConstants.KARMA_MAX );
			SetSkill( SkillName.DetectHidden, ThiefGuildmasterConstants.SKILL_DETECT_HIDDEN_MIN, ThiefGuildmasterConstants.SKILL_DETECT_HIDDEN_MAX );
			SetSkill( SkillName.Hiding, ThiefGuildmasterConstants.SKILL_HIDING_MIN, ThiefGuildmasterConstants.SKILL_HIDING_MAX );
			SetSkill( SkillName.Lockpicking, ThiefGuildmasterConstants.SKILL_LOCKPICKING_MIN, ThiefGuildmasterConstants.SKILL_LOCKPICKING_MAX );
			SetSkill( SkillName.Snooping, ThiefGuildmasterConstants.SKILL_SNOOPING_MIN, ThiefGuildmasterConstants.SKILL_SNOOPING_MAX );
			SetSkill( SkillName.Stealing, ThiefGuildmasterConstants.SKILL_STEALING_MIN, ThiefGuildmasterConstants.SKILL_STEALING_MAX );
			SetSkill( SkillName.Fencing, ThiefGuildmasterConstants.SKILL_FENCING_MIN, ThiefGuildmasterConstants.SKILL_FENCING_MAX );
			SetSkill( SkillName.Stealth, ThiefGuildmasterConstants.SKILL_STEALTH_MIN, ThiefGuildmasterConstants.SKILL_STEALTH_MAX );
			SetSkill( SkillName.RemoveTrap, ThiefGuildmasterConstants.SKILL_REMOVE_TRAP_MIN, ThiefGuildmasterConstants.SKILL_REMOVE_TRAP_MAX );
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the vendor's shop information.
		/// </summary>
		public override void InitSBInfo()
		{
			SBInfos.Add( new SBThiefGuild() ); 
			SBInfos.Add( new SBBuyArtifacts() ); 
		}

		/// <summary>
		/// Initializes the NPC's outfit with random thievery-themed clothing.
		/// </summary>
		public override void InitOutfit()
		{
			base.InitOutfit();

			int color = Utility.RandomNeutralHue();
			switch ( Utility.RandomMinMax( ThiefGuildmasterConstants.OUTFIT_SELECTION_MIN, ThiefGuildmasterConstants.OUTFIT_SELECTION_MAX ) )
			{
				case 0: AddItem( new Server.Items.Bandana( color ) ); break;
				case 1: AddItem( new Server.Items.SkullCap( color ) ); break;
				case 2: AddItem( new Server.Items.ClothCowl( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
				case 3: AddItem( new Server.Items.ClothHood( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
				case 4: AddItem( new Server.Items.FancyHood( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
			}
		}

		#endregion

		#region Flagged Targets Management

		/// <summary>
		/// Checks if a target is in the flagged targets set, or adds it if requested.
		/// Uses HashSet for O(1) lookup performance.
		/// </summary>
		/// <param name="target">The mobile to check or add</param>
		/// <param name="check">If true, adds target to set and returns false. If false, checks if target is flagged.</param>
		/// <returns>True if target is not flagged (can proceed), false if flagged or being added to set</returns>
		public static bool CheckFlaggedTargets(Mobile target, bool check)
		{
			if (check)
			{
				flaggedtargets.Add(target);
				return false;
			}
			else
			{
				return !flaggedtargets.Contains(target);
			}
		}

		/// <summary>
		/// Clears the flagged targets set.
		/// </summary>
		public static void WipeFlaggedList()
		{
			flaggedtargets.Clear();
		}

		#endregion

		#region Job System

		/// <summary>
		/// Welcomes a mobile to the guild with a localized message.
		/// </summary>
		/// <param name="m">The mobile to welcome</param>
		public override void SayWelcomeTo( Mobile m )
		{
			SayTo( m, ThiefGuildmasterConstants.LOCALIZED_MSG_WELCOME );
		}

		/// <summary>
		/// Finds or creates a job note for the player.
		/// If player already has a job, provides a copy. Otherwise creates a new job.
		/// </summary>
		/// <param name="m">The mobile requesting a job</param>
		public void FindMessage( Mobile m )
		{
			if ( Deleted || !m.Alive )
				return;

			Item note = Server.Items.ThiefNote.GetMyCurrentJob( m );

			if ( note != null )
			{
				ThiefNote job = (ThiefNote)note;
				m.AddToBackpack( note );
				m.PlaySound( ThiefGuildmasterConstants.SOUND_NOTE_RECEIVED );
				SayTo(m, string.Format( ThiefGuildmasterStringConstants.MSG_JOB_EXISTS_FORMAT, job.NoteItemPerson ) );
			}
			else
			{
				ThiefNote task = new ThiefNote();
				Server.Items.ThiefNote.SetupNote( task, m );
				m.AddToBackpack( task );
				m.PlaySound( ThiefGuildmasterConstants.SOUND_NOTE_RECEIVED );
				SayTo(m, ThiefGuildmasterStringConstants.MSG_JOB_NEW );
			}
		}

		#endregion

		#region Tithe System

		/// <summary>
		/// Processes the tithe service for a flagged player.
		/// Removes the flagged status and applies a fame penalty.
		/// </summary>
		/// <param name="mobile">The player mobile requesting tithe service</param>
		private static void ProcessTithe( PlayerMobile mobile )
		{
			mobile.flagged = false;
			mobile.SendMessage( ThiefGuildmasterStringConstants.MSG_TITHE_SUCCESS );
			Misc.Titles.AwardFame( mobile, -(Utility.RandomMinMax( ThiefGuildmasterConstants.TITHE_FAME_PENALTY_MIN, ThiefGuildmasterConstants.TITHE_FAME_PENALTY_MAX )), true );
			CheckFlaggedTargets( mobile, true );
		}

		#endregion

		#region Context Menu

		/// <summary>
		/// Adds custom context menu entries for job assignments and tithe service.
		/// </summary>
		/// <param name="from">The mobile viewing the context menu</param>
		/// <param name="list">The list to add entries to</param>
		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && !from.Blessed )
			{
				list.Add( new JobEntry( this, from ) );
				if ( IsFlaggedPlayer( from ) )
					list.Add( new TitheEntry( from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		/// <summary>
		/// Checks if a mobile is a flagged PlayerMobile.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if mobile is a flagged PlayerMobile, false otherwise</returns>
		private static bool IsFlaggedPlayer( Mobile from )
		{
			return from is PlayerMobile && ((PlayerMobile)from).flagged;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes this ThiefGuildmaster NPC.
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes this ThiefGuildmaster NPC.
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Context menu entry for requesting a job assignment.
		/// </summary>
		private class JobEntry : ContextMenuEntry
		{
			private ThiefGuildmaster m_ThiefGuildmaster;
			private Mobile m_From;

			/// <summary>
			/// Initializes a new instance of the JobEntry.
			/// </summary>
			/// <param name="ThiefGuildmaster">The ThiefGuildmaster NPC</param>
			/// <param name="from">The mobile viewing the menu</param>
			public JobEntry( ThiefGuildmaster ThiefGuildmaster, Mobile from ) : base( ThiefGuildmasterConstants.CONTEXT_MENU_JOB_ID, ThiefGuildmasterConstants.CONTEXT_MENU_JOB_RANGE )
			{
				m_ThiefGuildmaster = ThiefGuildmaster;
				m_From = from;
			}

			/// <summary>
			/// Requests a job assignment when clicked.
			/// </summary>
			public override void OnClick()
			{
				m_ThiefGuildmaster.FindMessage( m_From );
			}
		}

		/// <summary>
		/// Context menu entry for tithe service (removing flagged status).
		/// </summary>
		private class TitheEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;

			/// <summary>
			/// Initializes a new instance of the TitheEntry.
			/// </summary>
			/// <param name="mobile">The mobile viewing the menu</param>
			public TitheEntry( Mobile mobile ) : base( ThiefGuildmasterConstants.CONTEXT_MENU_TITHE_ID, ThiefGuildmasterConstants.CONTEXT_MENU_TITHE_RANGE )
			{
				m_Mobile = mobile;
				Enabled = m_Mobile.Alive;
			}

			/// <summary>
			/// Processes the tithe service when clicked.
			/// </summary>
			public override void OnClick()
			{
				if ( !(m_Mobile is PlayerMobile) )
					return;

				PlayerMobile pm = (PlayerMobile)m_Mobile;

				if ( !pm.flagged )
				{
					m_Mobile.SendMessage( ThiefGuildmasterStringConstants.MSG_NOT_FLAGGED );
					return;
				}

				if ( CheckFlaggedTargets( m_Mobile, false ) )
				{
					ProcessTithe( pm );
				}
				else
				{
					m_Mobile.SendMessage( ThiefGuildmasterStringConstants.MSG_TITHE_LIMIT_REACHED );
				}
			}
		}

		#endregion
	}
}