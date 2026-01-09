using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Exception chance adjustment types for crafting systems
	/// </summary>
	public enum CraftECA
	{
		ChanceMinusSixty,
		FiftyPercentChanceMinusTenPercent,
		ChanceMinusSixtyToFourtyFive
	}

	/// <summary>
	/// Abstract base class for all crafting systems in the game.
	/// Manages craftable items, resources, groups, and player crafting context.
	/// </summary>
	public abstract class CraftSystem
	{
		#region Fields

		private int m_MinCraftEffect;
		private int m_MaxCraftEffect;
		private double m_Delay;
		private bool m_Resmelt;
		private bool m_Repair;
		private bool m_MarkOption;
		private bool m_CanEnhance;

		private CraftItemCol m_CraftItems;
		private CraftGroupCol m_CraftGroups;
		private CraftSubResCol m_CraftSubRes;
		private CraftSubResCol m_CraftSubRes2;

		#endregion

		#region Properties

		/// <summary>
		/// Minimum number of craft effect animations
		/// </summary>
		public int MinCraftEffect { get { return m_MinCraftEffect; } }

		/// <summary>
		/// Maximum number of craft effect animations
		/// </summary>
		public int MaxCraftEffect { get { return m_MaxCraftEffect; } }

		/// <summary>
		/// Delay between craft effect animations
		/// </summary>
		public double Delay { get { return m_Delay; } }

		/// <summary>
		/// Collection of all craftable items in this system
		/// </summary>
		public CraftItemCol CraftItems{ get { return m_CraftItems; } }

		/// <summary>
		/// Collection of craft groups (categories) in this system
		/// </summary>
		public CraftGroupCol CraftGroups{ get { return m_CraftGroups; } }

		/// <summary>
		/// Primary sub-resource collection (e.g., metal types for blacksmithy)
		/// </summary>
		public CraftSubResCol CraftSubRes{ get { return m_CraftSubRes; } }

		/// <summary>
		/// Secondary sub-resource collection (e.g., dragon scales)
		/// </summary>
		public CraftSubResCol CraftSubRes2{ get { return m_CraftSubRes2; } }

		/// <summary>
		/// Tracks player locations during crafting to detect movement.
		/// Key: PlayerMobile, Value: Point3D (original location)
		/// </summary>
		public static Dictionary<Mobile, Point3D> PlayerLoc;

		/// <summary>
		/// The primary skill required for this crafting system
		/// </summary>
		public abstract SkillName MainSkill{ get; }

		/// <summary>
		/// Localized gump title number (0 if not used)
		/// </summary>
		public virtual int GumpTitleNumber{ get{ return 0; } }

		/// <summary>
		/// String gump title (empty if using GumpTitleNumber)
		/// </summary>
		public virtual string GumpTitleString{ get{ return ""; } }

		/// <summary>
		/// Exception chance adjustment algorithm for this system
		/// </summary>
		public virtual CraftECA ECA{ get{ return CraftECA.ChanceMinusSixty; } }

		#endregion

		#region Private Fields

		private Dictionary<Mobile, CraftContext> m_ContextTable = new Dictionary<Mobile, CraftContext>();

		#endregion

		#region Abstract Methods

		/// <summary>
		/// Gets the minimum success chance for a craft item at minimum skill
		/// </summary>
		/// <param name="item">The craft item to check</param>
		/// <returns>Minimum success chance (0.0 to 1.0)</returns>
		public abstract double GetChanceAtMin( CraftItem item );

		/// <summary>
		/// Determines if the crafted item should retain color from the resource material
		/// </summary>
		/// <param name="item">The craft item</param>
		/// <param name="type">The resource type</param>
		/// <returns>True if color should be retained</returns>
		public virtual bool RetainsColorFrom( CraftItem item, Type type )
		{
			return false;
		}

		#endregion

		#region Context Management

		/// <summary>
		/// Gets or creates a crafting context for the specified mobile
		/// </summary>
		/// <param name="m">The mobile to get context for</param>
		/// <returns>CraftContext for the mobile, or null if mobile is invalid</returns>
		public CraftContext GetContext( Mobile m )
		{
			if ( m == null )
				return null;

			if ( m.Deleted )
			{
				m_ContextTable.Remove( m );
				return null;
			}

			CraftContext c = null;
			m_ContextTable.TryGetValue( m, out c );

			if ( c == null )
				m_ContextTable[m] = c = new CraftContext();

			return c;
		}

		/// <summary>
		/// Records that a mobile has made a craft item (adds to last 10 list)
		/// </summary>
		/// <param name="m">The mobile who made the item</param>
		/// <param name="item">The craft item that was made</param>
		public void OnMade( Mobile m, CraftItem item )
		{
			CraftContext c = GetContext( m );

			if ( c != null )
				c.OnMade( item );
		}

		#endregion

		#region System Options

		/// <summary>
		/// Whether this system supports resmelting items back into resources
		/// </summary>
		public bool Resmelt
		{
			get { return m_Resmelt; }
			set { m_Resmelt = value; }
		}

		/// <summary>
		/// Whether this system supports repairing items
		/// </summary>
		public bool Repair
		{
			get{ return m_Repair; }
			set{ m_Repair = value; }
		}

		/// <summary>
		/// Whether this system supports maker's mark option
		/// </summary>
		public bool MarkOption
		{
			get{ return m_MarkOption; }
			set{ m_MarkOption = value; }
		}

		/// <summary>
		/// Whether this system supports enhancing items with special materials
		/// </summary>
		public bool CanEnhance
		{
			get{ return m_CanEnhance; }
			set{ m_CanEnhance = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftSystem class
		/// </summary>
		/// <param name="minCraftEffect">Minimum number of craft effect animations</param>
		/// <param name="maxCraftEffect">Maximum number of craft effect animations</param>
		/// <param name="delay">Delay between craft effect animations in seconds</param>
		public CraftSystem( int minCraftEffect, int maxCraftEffect, double delay )
		{
			m_MinCraftEffect = minCraftEffect;
			m_MaxCraftEffect = maxCraftEffect;
			m_Delay = delay;

			m_CraftItems = new CraftItemCol();
			m_CraftGroups = new CraftGroupCol();
			m_CraftSubRes = new CraftSubResCol();
			m_CraftSubRes2 = new CraftSubResCol();

			InitCraftList();
		}

		/// <summary>
		/// Determines whether resources should be consumed on crafting failure
		/// </summary>
		/// <param name="from">The mobile attempting to craft</param>
		/// <param name="resourceType">The type of resource being used</param>
		/// <param name="craftItem">The craft item being attempted</param>
		/// <returns>True if resources should be consumed on failure</returns>
		public virtual bool ConsumeOnFailure( Mobile from, Type resourceType, CraftItem craftItem )
		{
			return true;
		}

		/// <summary>
		/// Creates a craftable item, checking for player movement and validating the craft
		/// </summary>
		/// <param name="from">The mobile creating the item</param>
		/// <param name="type">The type of item to create</param>
		/// <param name="typeRes">The resource type to use</param>
		/// <param name="tool">The crafting tool being used</param>
		/// <param name="realCraftItem">The craft item definition</param>
		public void CreateItem( Mobile from, Type type, Type typeRes, BaseTool tool, CraftItem realCraftItem )
		{	
			// Verify if the type is in the list of the craftable item
			CraftItem craftItem = m_CraftItems.SearchFor( type );

			if ( craftItem != null )
			{
				PlayerMobile pm = from as PlayerMobile;
				
				// Check if player moved during crafting
				if ( HasPlayerMoved( pm ) )
				{
					StopCraftAction( pm );
					pm.SendMessage( CraftSystemConstants.MSG_COLOR_ERROR, CraftSystemStringConstants.MSG_PLAYER_MOVED_STOPPED_CRAFTING );
					return;
				}

				if ( PlayerLoc != null && PlayerLoc.ContainsKey( pm ) )
				{
                    // The item is in the list, try to create it
                    // Test code: items like sextant parts can be crafted either directly from ingots, or from different parts
                    realCraftItem.Craft(from, this, typeRes, tool);
                    //craftItem.Craft( from, this, typeRes, tool );
                }
            }
		}

		/// <summary>
		/// Stops the crafting action for a player and removes them from the location tracking
		/// </summary>
		/// <param name="pm">The player mobile to stop crafting for</param>
		public void StopCraftAction( PlayerMobile pm ) 
		{
			if ( PlayerLoc != null && PlayerLoc.ContainsKey( pm ) )
			{
				PlayerLoc.Remove( pm );
			}

			pm.SendMessage( CraftSystemConstants.MSG_COLOR_ERROR, CraftSystemStringConstants.MSG_STOPPED_CRAFTING );
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Checks if a player has moved from their original crafting location
		/// </summary>
		/// <param name="pm">The player mobile to check</param>
		/// <returns>True if the player has moved, false otherwise</returns>
		private bool HasPlayerMoved( PlayerMobile pm )
		{
			if ( pm == null || PlayerLoc == null )
				return false;

			if ( !PlayerLoc.ContainsKey( pm ) )
				return false;

			Point3D currentLoc = pm.Location;
			Point3D originalLoc = PlayerLoc[pm];

			return ( currentLoc.X != originalLoc.X || currentLoc.Y != originalLoc.Y );
		}

		#endregion

		#region Craft Item Management

		/// <summary>
		/// Adds a craftable item to this system using the main skill
		/// </summary>
		public int AddCraft( Type typeItem, TextDefinition group, TextDefinition name, double minSkill, double maxSkill, Type typeRes, TextDefinition nameRes, int amount )
		{
			return AddCraft( typeItem, group, name, MainSkill, minSkill, maxSkill, typeRes, nameRes, amount, "" );
		}

		/// <summary>
		/// Adds a craftable item to this system using the main skill with a custom message
		/// </summary>
		public int AddCraft( Type typeItem, TextDefinition group, TextDefinition name, double minSkill, double maxSkill, Type typeRes, TextDefinition nameRes, int amount, TextDefinition message )
		{
			return AddCraft( typeItem, group, name, MainSkill, minSkill, maxSkill, typeRes, nameRes, amount, message );
		}

		/// <summary>
		/// Adds a craftable item to this system with a specific skill
		/// </summary>
		public int AddCraft( Type typeItem, TextDefinition group, TextDefinition name, SkillName skillToMake, double minSkill, double maxSkill, Type typeRes, TextDefinition nameRes, int amount )
		{
			return AddCraft( typeItem, group, name, skillToMake, minSkill, maxSkill, typeRes, nameRes, amount, "" );
		}

		/// <summary>
		/// Adds a craftable item to this system with a specific skill and custom message
		/// </summary>
		public int AddCraft( Type typeItem, TextDefinition group, TextDefinition name, SkillName skillToMake, double minSkill, double maxSkill, Type typeRes, TextDefinition nameRes, int amount, TextDefinition message )
		{
			CraftItem craftItem = new CraftItem( typeItem, group, name );
			craftItem.AddRes( typeRes, nameRes, amount, message );
			craftItem.AddSkill( skillToMake, minSkill, maxSkill );

			DoGroup( group, craftItem );
			return m_CraftItems.Add( craftItem );
		}

		/// <summary>
		/// Adds a craft item to the appropriate group, creating the group if it doesn't exist
		/// </summary>
		/// <param name="groupName">The name of the group</param>
		/// <param name="craftItem">The craft item to add</param>
		private void DoGroup( TextDefinition groupName, CraftItem craftItem )
		{
			int index = m_CraftGroups.SearchFor( groupName );

			if ( index == -1)
			{
				CraftGroup craftGroup = new CraftGroup( groupName );
				craftGroup.AddCraftItem( craftItem );
				m_CraftGroups.Add( craftGroup );
			}
			else
			{
				m_CraftGroups.GetAt( index ).AddCraftItem( craftItem );
			}
		}

		/// <summary>
		/// Sets the mana requirement for a craft item
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="mana">Mana amount required</param>
		public void SetManaReq( int index, int mana )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.Mana = mana;
		}

		/// <summary>
		/// Sets the stamina requirement for a craft item
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="stam">Stamina amount required</param>
		public void SetStamReq( int index, int stam )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.Stam = stam;
		}

		/// <summary>
		/// Sets the hit points requirement for a craft item
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="hits">Hit points amount required</param>
		public void SetHitsReq( int index, int hits )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.Hits = hits;
		}

		/// <summary>
		/// Sets whether a craft item uses all available resources at once
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="useAll">True to use all resources, false otherwise</param>
		public void SetUseAllRes( int index, bool useAll )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.UseAllRes = useAll;
		}

		/// <summary>
		/// Sets whether a craft item requires a heat source
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="needHeat">True if heat source is required</param>
		public void SetNeedHeat( int index, bool needHeat )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.NeedHeat = needHeat;
		}

		/// <summary>
		/// Sets whether a craft item requires an oven
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="needOven">True if oven is required</param>
		public void SetNeedOven( int index, bool needOven )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.NeedOven = needOven;
		}

		/// <summary>
		/// Sets whether a craft item requires a mill
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="needMill">True if mill is required</param>
		public void SetNeedMill( int index, bool needMill )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.NeedMill = needMill;
		}

		/// <summary>
		/// Sets the required expansion for a craft item
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="expansion">Required expansion</param>
		public void SetNeededExpansion( int index, Expansion expansion )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.RequiredExpansion = expansion;
		}

		/// <summary>
		/// Adds a resource requirement to a craft item
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="type">Resource type</param>
		/// <param name="name">Resource name</param>
		/// <param name="amount">Amount required</param>
		public void AddRes( int index, Type type, TextDefinition name, int amount )
		{
			AddRes( index, type, name, amount, "" );
		}

		/// <summary>
		/// Adds a resource requirement to a craft item with a custom message
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="type">Resource type</param>
		/// <param name="name">Resource name</param>
		/// <param name="amount">Amount required</param>
		/// <param name="message">Message to display if resource is missing</param>
		public void AddRes( int index, Type type, TextDefinition name, int amount, TextDefinition message )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.AddRes( type, name, amount, message );
		}

		/// <summary>
		/// Adds a skill requirement to a craft item
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="skillToMake">Skill required</param>
		/// <param name="minSkill">Minimum skill value</param>
		/// <param name="maxSkill">Maximum skill value</param>
		public void AddSkill( int index, SkillName skillToMake, double minSkill, double maxSkill )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.AddSkill( skillToMake, minSkill, maxSkill );
		}

		/// <summary>
		/// Sets whether a craft item uses the secondary sub-resource collection
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="val">True to use secondary sub-resource</param>
		public void SetUseSubRes2( int index, bool val )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.UseSubRes2 = val;
		}

		/// <summary>
		/// Adds a recipe to a craft item
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		/// <param name="id">Recipe ID</param>
		public void AddRecipe( int index, int id )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.AddRecipe( id, this );
		}

		/// <summary>
		/// Forces a craft item to never be exceptional quality
		/// </summary>
		/// <param name="index">Index of the craft item</param>
		public void ForceNonExceptional( int index )
		{
			CraftItem craftItem = m_CraftItems.GetAt( index );
			craftItem.ForceNonExceptional = true;
		}

		#endregion

		#region Sub-Resource Management

		/// <summary>
		/// Sets the primary sub-resource type with a string name
		/// </summary>
		/// <param name="type">Base resource type</param>
		/// <param name="name">Resource name string</param>
		public void SetSubRes( Type type, string name )
		{
			m_CraftSubRes.ResType = type;
			m_CraftSubRes.NameString = name;
			m_CraftSubRes.Init = true;
		}

		/// <summary>
		/// Sets the primary sub-resource type with a localized name number
		/// </summary>
		/// <param name="type">Base resource type</param>
		/// <param name="name">Localized name number</param>
		public void SetSubRes( Type type, int name )
		{
			m_CraftSubRes.ResType = type;
			m_CraftSubRes.NameNumber = name;
			m_CraftSubRes.Init = true;
		}

		/// <summary>
		/// Adds a sub-resource to the primary collection with localized name
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Localized name number</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes( Type type, int name, double reqSkill, object message )
		{
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, message );
			m_CraftSubRes.Add( craftSubRes );
		}

		/// <summary>
		/// Adds a sub-resource to the primary collection with localized and generic names
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Localized name number</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="genericName">Generic name number for display</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes( Type type, int name, double reqSkill, int genericName, object message )
		{
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, genericName, message );
			m_CraftSubRes.Add( craftSubRes );
		}

		/// <summary>
		/// Adds a sub-resource to the primary collection with string name
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Resource name string</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes( Type type, string name, double reqSkill, object message )
		{
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, message );
			m_CraftSubRes.Add( craftSubRes );
		}

		/// <summary>
		/// Adds a sub-resource to the primary collection with string name and generic name
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Resource name string</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="genericName">Generic name string for display</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes( Type type, string name, double reqSkill, string genericName, object message )
		{
			// Note: CraftSubRes only supports int for genericName, so we ignore the string genericName parameter
			// This maintains API compatibility while using string names
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, message );
			m_CraftSubRes.Add( craftSubRes );
		}

		/// <summary>
		/// Sets the secondary sub-resource type with a string name
		/// </summary>
		/// <param name="type">Base resource type</param>
		/// <param name="name">Resource name string</param>
		public void SetSubRes2( Type type, string name )
		{
			m_CraftSubRes2.ResType = type;
			m_CraftSubRes2.NameString = name;
			m_CraftSubRes2.Init = true;
		}

		/// <summary>
		/// Sets the secondary sub-resource type with a localized name number
		/// </summary>
		/// <param name="type">Base resource type</param>
		/// <param name="name">Localized name number</param>
		public void SetSubRes2( Type type, int name )
		{
			m_CraftSubRes2.ResType = type;
			m_CraftSubRes2.NameNumber = name;
			m_CraftSubRes2.Init = true;
		}

		/// <summary>
		/// Adds a sub-resource to the secondary collection with localized name
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Localized name number</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes2( Type type, int name, double reqSkill, object message )
		{
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, message );
			m_CraftSubRes2.Add( craftSubRes );
		}

		/// <summary>
		/// Adds a sub-resource to the secondary collection with localized and generic names
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Localized name number</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="genericName">Generic name number for display</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes2( Type type, int name, double reqSkill, int genericName, object message )
		{
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, genericName, message );
			m_CraftSubRes2.Add( craftSubRes );
		}

		/// <summary>
		/// Adds a sub-resource to the secondary collection with string name
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Resource name string</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes2( Type type, string name, double reqSkill, object message )
		{
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, message );
			m_CraftSubRes2.Add( craftSubRes );
		}

		/// <summary>
		/// Adds a sub-resource to the secondary collection with string name and generic name
		/// </summary>
		/// <param name="type">Sub-resource type</param>
		/// <param name="name">Resource name string</param>
		/// <param name="reqSkill">Required skill to use this resource</param>
		/// <param name="genericName">Generic name string for display</param>
		/// <param name="message">Message to display if skill is insufficient</param>
		public void AddSubRes2( Type type, string name, double reqSkill, string genericName, object message )
		{
			// Note: CraftSubRes only supports int for genericName, so we ignore the string genericName parameter
			// This maintains API compatibility while using string names
			CraftSubRes craftSubRes = new CraftSubRes( type, name, reqSkill, message );
			m_CraftSubRes2.Add( craftSubRes );
		}

		#endregion

		#region Abstract Methods

		/// <summary>
		/// Initializes the list of craftable items for this system
		/// </summary>
		public abstract void InitCraftList();

		/// <summary>
		/// Plays the crafting effect animation for the mobile
		/// </summary>
		/// <param name="from">The mobile crafting</param>
		public abstract void PlayCraftEffect( Mobile from );

		/// <summary>
		/// Plays the ending effect and returns the result message
		/// </summary>
		/// <param name="from">The mobile who crafted</param>
		/// <param name="failed">Whether the craft failed</param>
		/// <param name="lostMaterial">Whether materials were lost</param>
		/// <param name="toolBroken">Whether the tool broke</param>
		/// <param name="quality">Quality level (0=normal, 1=exceptional, 2=exceptional with mark)</param>
		/// <param name="makersMark">Whether maker's mark was applied</param>
		/// <param name="item">The craft item that was attempted</param>
		/// <returns>Localized message number to display</returns>
		public abstract int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item );

		/// <summary>
		/// Checks if a mobile can craft with the given tool and item type
		/// </summary>
		/// <param name="from">The mobile attempting to craft</param>
		/// <param name="tool">The crafting tool</param>
		/// <param name="itemType">The type of item to craft (null for general check)</param>
		/// <returns>0 if allowed, localized message number if not allowed</returns>
		public abstract int CanCraft( Mobile from, BaseTool tool, Type itemType );

		#endregion
	}
}