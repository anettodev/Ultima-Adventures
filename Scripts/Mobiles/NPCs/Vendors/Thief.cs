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
	/// Thief NPC vendor that provides training services and container unlocking.
	/// Can train players in the Stealing skill when located in "the Basement" region.
	/// Offers unlock service for LockableContainers at a cost, with discounts for begging players.
	/// </summary>
	public class Thief : BaseVendor
	{
		#region Fields

		private List<SBInfo> m_SBInfos = new List<SBInfo>();

		#endregion

		#region Properties

		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		/// <summary>Gets the NPC guild this vendor belongs to</summary>
		public override NpcGuild NpcGuild{ get{ return NpcGuild.ThievesGuild; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Thief NPC.
		/// </summary>
		[Constructable]
		public Thief() : base( ThiefConstants.TITLE_THIEF )
		{
			Job = JobFragment.thief;
			Karma = Utility.RandomMinMax( VendorConstants.KARMA_MIN, VendorConstants.KARMA_MAX );
			SetSkill( SkillName.Fencing, ThiefConstants.SKILL_FENCING_MIN, ThiefConstants.SKILL_FENCING_MAX );
			SetSkill( SkillName.DetectHidden, ThiefConstants.SKILL_DETECT_HIDDEN_MIN, ThiefConstants.SKILL_DETECT_HIDDEN_MAX );
			SetSkill( SkillName.Hiding, ThiefConstants.SKILL_HIDING_MIN, ThiefConstants.SKILL_HIDING_MAX );
			SetSkill( SkillName.RemoveTrap, ThiefConstants.SKILL_REMOVE_TRAP_MIN, ThiefConstants.SKILL_REMOVE_TRAP_MAX );
			SetSkill( SkillName.Lockpicking, ThiefConstants.SKILL_LOCKPICKING_MIN, ThiefConstants.SKILL_LOCKPICKING_MAX );
			SetSkill( SkillName.Snooping, ThiefConstants.SKILL_SNOOPING_MIN, ThiefConstants.SKILL_SNOOPING_MAX );
			SetSkill( SkillName.Stealing, ThiefConstants.SKILL_STEALING_MIN, ThiefConstants.SKILL_STEALING_MAX );
			SetSkill( SkillName.Stealth, ThiefConstants.SKILL_STEALTH_MIN, ThiefConstants.SKILL_STEALTH_MAX );
		}

		/// <summary>
		/// Deserialization constructor for Thief NPC.
		/// </summary>
		/// <param name="serial">The serialization reader</param>
		public Thief( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the vendor's shop information.
		/// </summary>
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBThief() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		/// <summary>
		/// Initializes the NPC's outfit with random thievery-themed clothing.
		/// </summary>
		public override void InitOutfit()
		{
			base.InitOutfit();

			int color = Utility.RandomNeutralHue();
			switch ( Utility.RandomMinMax( ThiefConstants.OUTFIT_SELECTION_MIN, ThiefConstants.OUTFIT_SELECTION_MAX ) )
			{
				case 0: AddItem( new Server.Items.Bandana( color ) ); break;
				case 1: AddItem( new Server.Items.SkullCap( color ) ); break;
				case 2: AddItem( new Server.Items.ClothCowl( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
				case 3: AddItem( new Server.Items.ClothHood( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
				case 4: AddItem( new Server.Items.FancyHood( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
			}

			AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
			AddItem( new Server.Items.LongPants( Utility.RandomNeutralHue() ) );
			AddItem( new Server.Items.ThighBoots( Utility.RandomNeutralHue() ) );
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds custom properties to the property list, including training instruction when in Basement region.
		/// </summary>
		/// <param name="list">The property list to add to</param>
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );	
			Region reg = Region.Find( this.Location, this.Map );
			if ( reg.IsPartOf( ThiefConstants.REGION_NAME_BASEMENT ) )
			{
				list.Add( ThiefStringConstants.PROPERTY_TRAIN_INSTRUCTION ); 
			}
		}

		#endregion

		#region Training System

		/// <summary>
		/// Handles double-click interaction, providing training when in Basement region.
		/// </summary>
		/// <param name="from">The mobile interacting with the NPC</param>
		public override void OnDoubleClick( Mobile from )
		{
			if ( CanTrain( from ) )
			{
				HandleTraining( from );
				return;
			}
			base.OnDoubleClick(from);
		}

		/// <summary>
		/// Checks if a mobile can train with this NPC.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if training is available, false otherwise</returns>
		private bool CanTrain( Mobile from )
		{
			if ( !IsInTrainingRegion() )
				return false;

			if ( !(from is PlayerMobile) )
				return false;

			PlayerMobile pm = (PlayerMobile)from;
			if ( !pm.SoulBound || !from.Alive )
				return false;

			if ( from.GetDistanceToSqrt( this ) > ThiefConstants.TRAINING_RANGE )
				return false;

			return true;
		}

		/// <summary>
		/// Checks if this NPC is in the training region (Basement).
		/// </summary>
		/// <returns>True if in Basement region, false otherwise</returns>
		private bool IsInTrainingRegion()
		{
			Region reg = Region.Find( this.Location, this.Map );
			return reg.IsPartOf( ThiefConstants.REGION_NAME_BASEMENT );
		}

		/// <summary>
		/// Handles the training attempt logic.
		/// </summary>
		/// <param name="from">The mobile attempting to train</param>
		private void HandleTraining( Mobile from )
		{
			if ( Utility.RandomDouble() > ThiefConstants.TRAINING_SUCCESS_CHANCE )
			{
				if ( from.CheckSkill( SkillName.Stealing, ThiefConstants.TRAINING_SKILL_MIN, ThiefConstants.TRAINING_SKILL_MAX ) )
					from.SendMessage( string.Format( ThiefStringConstants.MSG_TRAINING_SUCCESS_FORMAT, this.Name ) );
				else
					this.Say( ThiefStringConstants.MSG_TRAINING_CAUGHT );
			}
			else 
			{
				from.SendMessage( ThiefStringConstants.MSG_TRAINING_FAIL );
			}
		}

		#endregion

		#region Unlock Service

		/// <summary>
		/// Begins the unlock service by calculating cost and prompting for target.
		/// </summary>
		/// <param name="from">The mobile requesting the service</param>
		public void BeginRepair(Mobile from)
		{
			if ( Deleted || !from.Alive )
				return;

			int cost = CalculateUnlockCost( from, ThiefConstants.UNLOCK_COST_BASE );

			if ( BeggingPose(from) > 0 )
			{
				SayTo(from, string.Format( ThiefStringConstants.MSG_UNLOCK_BEGGING_FORMAT, cost ) );
			}
			else 
			{ 
				SayTo(from, string.Format( ThiefStringConstants.MSG_UNLOCK_NORMAL_FORMAT, cost ) ); 
			}

			from.Target = new RepairTarget(this);
		}

		/// <summary>
		/// Calculates the unlock cost with potential begging discount.
		/// </summary>
		/// <param name="from">The mobile requesting the service</param>
		/// <param name="baseCost">The base cost before discounts</param>
		/// <returns>The final cost after applying any discounts</returns>
		private static int CalculateUnlockCost(Mobile from, int baseCost)
		{
			if ( BeggingPose(from) > 0 )
			{
				int discount = (int)((from.Skills[SkillName.Begging].Value * ThiefConstants.BEGGING_DISCOUNT_MULTIPLIER) * baseCost);
				int finalCost = baseCost - discount;
				return finalCost < ThiefConstants.MIN_UNLOCK_COST ? ThiefConstants.MIN_UNLOCK_COST : finalCost;
			}
			return baseCost;
		}

		/// <summary>
		/// Handles successful unlock operation.
		/// </summary>
		/// <param name="from">The mobile who requested the unlock</param>
		/// <param name="box">The container that was unlocked</param>
		/// <param name="cost">The cost that was paid</param>
		private void HandleUnlockSuccess( Mobile from, LockableContainer box, int cost )
		{
			if ( BeggingPose(from) > 0 )
			{ 
				Titles.AwardKarma( from, -BeggingKarma( from ), true ); 
			}

			SayTo(from, ThiefStringConstants.MSG_UNLOCK_SUCCESS );
			from.SendMessage( string.Format( ThiefStringConstants.MSG_PAYMENT_FORMAT, cost ) );
			Effects.PlaySound(from.Location, from.Map, ThiefConstants.SOUND_UNLOCK );

			box.Locked = false;
			box.TrapPower = 0;
			box.TrapLevel = 0;
			box.LockLevel = 0;
			box.MaxLockLevel = 0;
			box.RequiredSkill = 0;
			box.TrapType = TrapType.None;
		}

		/// <summary>
		/// Handles failed unlock operation due to insufficient funds.
		/// </summary>
		/// <param name="from">The mobile who requested the unlock</param>
		/// <param name="cost">The cost that was required</param>
		private void HandleUnlockFailure( Mobile from, int cost )
		{
			SayTo(from, string.Format( ThiefStringConstants.MSG_UNLOCK_COST_FORMAT, cost ) );
			from.SendMessage( ThiefStringConstants.MSG_INSUFFICIENT_GOLD );
		}

		#endregion

		#region Context Menu

		/// <summary>
		/// Gets context menu entries for this NPC.
		/// </summary>
		/// <param name="from">The mobile viewing the context menu</param>
		/// <param name="list">The list to add entries to</param>
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		/// <summary>
		/// Adds custom context menu entries for unlock service.
		/// </summary>
		/// <param name="from">The mobile viewing the context menu</param>
		/// <param name="list">The list to add entries to</param>
		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && !from.Blessed )
			{
				list.Add( new FixEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes this Thief NPC.
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes this Thief NPC.
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
		/// Context menu entry for opening the speech gump.
		/// </summary>
		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			/// <summary>
			/// Initializes a new instance of the SpeechGumpEntry.
			/// </summary>
			/// <param name="from">The mobile viewing the menu</param>
			/// <param name="giver">The NPC providing the speech</param>
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( ThiefConstants.CONTEXT_MENU_SPEECH_ID, ThiefConstants.CONTEXT_MENU_SPEECH_RANGE )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			/// <summary>
			/// Opens the speech gump when clicked.
			/// </summary>
			public override void OnClick()
			{
				if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
				{
					mobile.SendGump(new SpeechGump( ThiefStringConstants.SPEECH_GUMP_TITLE, SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, ThiefConstants.SPEECH_NPC_TYPE ) ));
				}
			}
		}

		/// <summary>
		/// Context menu entry for unlock service.
		/// </summary>
		private class FixEntry : ContextMenuEntry
		{
			private Thief m_Thief;
			private Mobile m_From;

			/// <summary>
			/// Initializes a new instance of the FixEntry.
			/// </summary>
			/// <param name="Thief">The Thief NPC</param>
			/// <param name="from">The mobile viewing the menu</param>
			public FixEntry( Thief Thief, Mobile from ) : base( ThiefConstants.CONTEXT_MENU_REPAIR_ID, ThiefConstants.CONTEXT_MENU_REPAIR_RANGE )
			{
				m_Thief = Thief;
				m_From = from;
			}

			/// <summary>
			/// Begins the unlock service when clicked.
			/// </summary>
			public override void OnClick()
			{
				m_Thief.BeginRepair( m_From );
			}
		}

		/// <summary>
		/// Target class for selecting containers to unlock.
		/// </summary>
		private class RepairTarget : Target
		{
			private Thief m_Thief;

			/// <summary>
			/// Initializes a new instance of the RepairTarget.
			/// </summary>
			/// <param name="thief">The Thief NPC providing the service</param>
			public RepairTarget(Thief thief) : base(ThiefConstants.TARGET_RANGE_UNLOCK, false, TargetFlags.None)
			{
				m_Thief = thief;
			}

			/// <summary>
			/// Handles the target selection for unlock service.
			/// </summary>
			/// <param name="from">The mobile who selected the target</param>
			/// <param name="targeted">The object that was targeted</param>
			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BookBox && from.Backpack != null)
				{
					m_Thief.SayTo(from, ThiefStringConstants.MSG_CANNOT_UNLOCK_CURSED );
					return;
				}

				if (targeted is LockableContainer && from.Backpack != null)
				{
					LockableContainer box = (LockableContainer)targeted;
					Container pack = from.Backpack;

					int cost = CalculateUnlockCost( from, ThiefConstants.UNLOCK_COST_BASE );

					if (cost == 0)
						return;

					if (pack.ConsumeTotal(typeof(Gold), cost))
					{
						m_Thief.HandleUnlockSuccess( from, box, cost );
					}
					else
					{
						m_Thief.HandleUnlockFailure( from, cost );
					}
					return;
				}

				m_Thief.SayTo(from, ThiefStringConstants.MSG_NO_SERVICE_NEEDED );
			}
		}

		#endregion
	}
}