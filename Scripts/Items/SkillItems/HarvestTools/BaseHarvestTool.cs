using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Craft;
using Server.Engines.Harvest;
using Server.ContextMenus;
using Server.Misc;
using System.Globalization;

namespace Server.Items
{
	/// <summary>
	/// Interface for items that track remaining uses
	/// </summary>
	public interface IUsesRemaining
	{
		int UsesRemaining{ get; set; }
		bool ShowUsesRemaining{ get; set; }
	}

	/// <summary>
	/// Base class for all harvest tools (pickaxes, shovels, axes, etc.).
	/// Provides durability tracking, quality management, and harvest system integration.
	/// </summary>
	public abstract class BaseHarvestTool : Item, IUsesRemaining, ICraftable
	{
		#region Fields

		private Mobile m_Crafter;
		private ToolQuality m_Quality;
		private int m_UsesRemaining;
        private CraftResource m_Resource;

		#endregion

		#region Properties

        [CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set { m_Resource = value; Hue = CraftResources.GetHue(m_Resource); InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
		public ToolQuality Quality
		{
			get{ return m_Quality; }
			set{ UnscaleUses(); m_Quality = value; InvalidateProperties(); ScaleUses(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		public bool ShowUsesRemaining{ get{ return true; } set{} }

		public abstract HarvestSystem HarvestSystem{ get; }

        public virtual CraftResource DefaultResource { get { return CraftResource.Iron; } }

		#endregion

		#region Constructors

		public BaseHarvestTool( int itemID ) : this( HarvestToolConstants.DEFAULT_USES_REMAINING, itemID )
		{
		}

        public BaseHarvestTool( int usesRemaining, int itemID ) : base( itemID )
		{
			m_UsesRemaining = usesRemaining;
			m_Quality = ToolQuality.Regular;
		}

		public BaseHarvestTool( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Scales uses remaining based on quality change
		/// </summary>
		public void ScaleUses()
		{
			m_UsesRemaining = (m_UsesRemaining * GetUsesScalar()) / HarvestToolConstants.SCALING_DIVISOR;
			InvalidateProperties();
		}

		/// <summary>
		/// Unscales uses remaining before quality change
		/// </summary>
		public void UnscaleUses()
		{
			m_UsesRemaining = (m_UsesRemaining * HarvestToolConstants.SCALING_DIVISOR) / GetUsesScalar();
		}

		/// <summary>
		/// Gets the scalar multiplier for uses based on quality
		/// </summary>
		/// <returns>Scalar value (100 for regular, 200 for exceptional)</returns>
		public int GetUsesScalar()
		{
			if ( m_Quality == ToolQuality.Exceptional )
				return HarvestToolConstants.QUALITY_SCALAR_EXCEPTIONAL;

			return HarvestToolConstants.QUALITY_SCALAR_REGULAR;
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds properties to the object property list (tooltip)
		/// </summary>
		/// <param name="list">The object property list to add to</param>
		public override void GetProperties( ObjectPropertyList list )
		{
            base.GetProperties(list);

            AddCrafterProperty(list);
            AddQualityProperty(list);
            AddResourceProperty(list);
            AddUsesRemainingProperty(list);
        }

		/// <summary>
		/// Adds crafter name property to the list
		/// </summary>
		/// <param name="list">The object property list</param>
		private void AddCrafterProperty(ObjectPropertyList list)
		{
			if (m_Crafter != null)
			{
				list.Add(HarvestToolConstants.MSG_ID_CRAFTED_BY, 
						 ItemNameHue.UnifiedItemProps.SetColor(m_Crafter.Name, HarvestToolStringConstants.COLOR_CYAN));
			}
		}

		/// <summary>
		/// Adds quality property to the list if exceptional
		/// </summary>
		/// <param name="list">The object property list</param>
		private void AddQualityProperty(ObjectPropertyList list)
		{
			if (m_Quality == ToolQuality.Exceptional)
			{
				list.Add(HarvestToolConstants.MSG_ID_PROPERTY_LABEL_FORMAT, 
						 ItemNameHue.UnifiedItemProps.SetColor(HarvestToolStringConstants.LABEL_EXCEPTIONAL, 
																HarvestToolStringConstants.COLOR_ORANGE));
			}
		}

		/// <summary>
		/// Adds resource name property to the list
		/// </summary>
		/// <param name="list">The object property list</param>
		private void AddResourceProperty(ObjectPropertyList list)
		{
			string resourceName = CraftResources.GetName(m_Resource);
			if (!string.IsNullOrEmpty(resourceName))
			{
				list.Add(HarvestToolConstants.MSG_ID_PROPERTY_LABEL_FORMAT, 
						 ItemNameHue.UnifiedItemProps.SetColor(resourceName, HarvestToolStringConstants.COLOR_CYAN));
			}
		}

		/// <summary>
		/// Adds uses remaining property to the list
		/// </summary>
		/// <param name="list">The object property list</param>
		private void AddUsesRemainingProperty(ObjectPropertyList list)
		{
			list.Add(HarvestToolConstants.MSG_ID_USES_REMAINING, m_UsesRemaining.ToString());
		}

        /// <summary>
        /// Displays durability information to a mobile
        /// </summary>
        /// <param name="m">The mobile to display to</param>
        public virtual void DisplayDurabilityTo( Mobile m )
		{
			LabelToAffix( m, HarvestToolConstants.MSG_ID_DURABILITY, AffixType.Append, ": " + m_UsesRemaining.ToString() );
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Handles single-click display
		/// </summary>
		/// <param name="from">The mobile clicking</param>
		public override void OnSingleClick( Mobile from )
		{
			DisplayDurabilityTo( from );
			base.OnSingleClick( from );
		}

		/// <summary>
		/// Handles double-click to begin harvesting
		/// </summary>
		/// <param name="from">The mobile attempting to harvest</param>
		public override void OnDoubleClick( Mobile from )
		{
			if (IsPlayerAutomated(from))
			{
				from.SendMessage(55, HarvestToolStringConstants.MSG_CANNOT_USE_WHILE_AUTOMATED);
				return;
			}

			if ( IsChildOf( from.Backpack ) || Parent == from )
				HarvestSystem.BeginHarvesting( from, this );
			else
				from.SendLocalizedMessage( HarvestToolConstants.MSG_ID_MUST_BE_IN_PACK );
		}

		/// <summary>
		/// Checks if the player is currently automated
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if player is automated, false otherwise</returns>
		private bool IsPlayerAutomated(Mobile from)
		{
			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;
				return pm.GetFlag(PlayerFlag.IsAutomated);
			}
			return false;
		}

		/// <summary>
		/// Gets context menu entries for the tool
		/// </summary>
		/// <param name="from">The mobile requesting the menu</param>
		/// <param name="list">The context menu entry list</param>
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			AddContextMenuEntries( from, this, list, HarvestSystem );
		}

		/// <summary>
		/// Adds mining-specific context menu entries
		/// </summary>
		/// <param name="from">The mobile requesting the menu</param>
		/// <param name="item">The harvest tool item</param>
		/// <param name="list">The context menu entry list</param>
		/// <param name="system">The harvest system</param>
		public static void AddContextMenuEntries( Mobile from, Item item, List<ContextMenuEntry> list, HarvestSystem system )
		{
			if ( system != Mining.System )
				return;

			if ( !item.IsChildOf( from.Backpack ) && item.Parent != from )
				return;

			PlayerMobile pm = from as PlayerMobile;

			if ( pm == null )
				return;

			ContextMenuEntry miningEntry = new ContextMenuEntry( pm.ToggleMiningStone ? HarvestToolConstants.MSG_ID_MINING_BOTH : HarvestToolConstants.MSG_ID_MINING_ORE_ONLY );
			miningEntry.Color = HarvestToolConstants.CONTEXT_MENU_COLOR_MINING;
			list.Add( miningEntry );

			list.Add( new ToggleMiningStoneEntry( pm, false, HarvestToolConstants.MSG_ID_TOGGLE_STONE_FALSE ) );
			list.Add( new ToggleMiningStoneEntry( pm, true, HarvestToolConstants.MSG_ID_TOGGLE_STONE_TRUE ) );
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Context menu entry for toggling stone mining
		/// </summary>
		private class ToggleMiningStoneEntry : ContextMenuEntry
		{
			private PlayerMobile m_Mobile;
			private bool m_Value;

			/// <summary>
			/// Initializes a new instance of ToggleMiningStoneEntry
			/// </summary>
			/// <param name="mobile">The player mobile</param>
			/// <param name="value">True to enable stone mining, false to disable</param>
			/// <param name="number">The message ID for the menu entry</param>
			public ToggleMiningStoneEntry( PlayerMobile mobile, bool value, int number ) : base( number )
			{
				m_Mobile = mobile;
				m_Value = value;

				bool stoneMining = ValidateStoneMiningSkill(mobile);

				if ( mobile.ToggleMiningStone == value || ( value && !stoneMining ) )
					this.Flags |= CMEFlags.Disabled;
			}

			/// <summary>
			/// Handles the context menu click
			/// </summary>
			public override void OnClick()
			{
				if (m_Value)
					HandleEnableStoneMining();
				else
					HandleDisableStoneMining();
			}

			/// <summary>
			/// Handles enabling stone mining toggle
			/// </summary>
			private void HandleEnableStoneMining()
			{
				if (m_Mobile.ToggleMiningStone)
				{
					m_Mobile.SendLocalizedMessage(HarvestToolConstants.MSG_ID_ALREADY_MINING_BOTH);
					return;
				}
				
				if (!ValidateStoneMiningSkill(m_Mobile))
				{
					m_Mobile.SendLocalizedMessage(HarvestToolConstants.MSG_ID_CANNOT_MINE_STONE);
					return;
				}
				
				m_Mobile.ToggleMiningStone = true;
				m_Mobile.SendLocalizedMessage(HarvestToolConstants.MSG_ID_NOW_MINING_BOTH);
			}

			/// <summary>
			/// Handles disabling stone mining toggle
			/// </summary>
			private void HandleDisableStoneMining()
			{
				if (!m_Mobile.ToggleMiningStone)
				{
					m_Mobile.SendLocalizedMessage(HarvestToolConstants.MSG_ID_ALREADY_MINING_ORE_ONLY);
					return;
				}
				
				m_Mobile.ToggleMiningStone = false;
				m_Mobile.SendLocalizedMessage(HarvestToolConstants.MSG_ID_NOW_MINING_ORE_ONLY);
			}

			/// <summary>
			/// Validates if player has required skill for stone mining
			/// </summary>
			/// <param name="mobile">The player mobile to validate</param>
			/// <returns>True if player can mine stone, false otherwise</returns>
			private bool ValidateStoneMiningSkill(PlayerMobile mobile)
			{
				return mobile.StoneMining && 
					   mobile.Skills[SkillName.Mining].Base >= HarvestToolConstants.STONE_MINING_MIN_SKILL;
			}
		}

		#endregion

		#region Serialization

        /// <summary>
        /// Helper method to set save flags (unused but kept for potential future use)
        /// </summary>
        private static void SetSaveFlag(ref SaveFlag flags, SaveFlag toSet, bool setIf)
        {
            if (setIf)
                flags |= toSet;
        }

        /// <summary>
        /// Helper method to get save flags (unused but kept for potential future use)
        /// </summary>
        private static bool GetSaveFlag(SaveFlag flags, SaveFlag toGet)
        {
            return ((flags & toGet) != 0);
        }

        /// <summary>
        /// Save flags for serialization (unused but kept for potential future use)
        /// </summary>
        [Flags]
        private enum SaveFlag
        {
            None = HarvestToolConstants.SAVE_FLAG_NONE,
            Quality = HarvestToolConstants.SAVE_FLAG_QUALITY,
            Crafter = HarvestToolConstants.SAVE_FLAG_CRAFTER,
            Resource = HarvestToolConstants.SAVE_FLAG_RESOURCE,
        }

        /// <summary>
        /// Serializes the harvest tool
        /// </summary>
        /// <param name="writer">The generic writer</param>
        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( HarvestToolConstants.SERIALIZATION_VERSION_CURRENT );

            writer.Write(m_Crafter);
            writer.WriteEncodedInt((int)m_Quality);
            writer.WriteEncodedInt((int)m_Resource);
            writer.WriteEncodedInt((int)UsesRemaining);
        }

		/// <summary>
		/// Deserializes the harvest tool
		/// </summary>
		/// <param name="reader">The generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            m_Crafter = reader.ReadMobile();
            m_Quality = (ToolQuality)reader.ReadEncodedInt();
            m_Resource = (CraftResource)reader.ReadEncodedInt();
            UsesRemaining = reader.ReadEncodedInt();
        }

		#endregion

        #region ICraftable

        /// <summary>
        /// Handles crafting of the tool
        /// </summary>
        /// <param name="quality">The quality of the crafted item</param>
        /// <param name="makersMark">Whether to add maker's mark</param>
        /// <param name="from">The crafter</param>
        /// <param name="craftSystem">The craft system</param>
        /// <param name="typeRes">The resource type</param>
        /// <param name="tool">The crafting tool</param>
        /// <param name="craftItem">The craft item</param>
        /// <param name="resHue">The resource hue</param>
        /// <returns>The quality value</returns>
        public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ToolQuality)quality;

			if ( makersMark )
				Crafter = from;

			return quality;
		}

		#endregion
	}
}
