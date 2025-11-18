using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Commands;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Represents a crafting recipe that can be learned by players
	/// </summary>
	public class Recipe
	{
		#region Constants

		/// <summary>Target range for recipe commands (-1 = unlimited)</summary>
		private const int TARGET_RANGE = -1;

		#endregion

		#region Static Fields

		private static Dictionary<int, Recipe> m_Recipes = new Dictionary<int, Recipe>();
		private static int m_LargestRecipeID;

		#endregion

		#region Static Properties

		/// <summary>
		/// Dictionary of all registered recipes, keyed by recipe ID
		/// </summary>
		public static Dictionary<int, Recipe> Recipes { get { return m_Recipes; } }

		/// <summary>
		/// The largest recipe ID currently registered
		/// </summary>
		public static int LargestRecipeID{ get{ return m_LargestRecipeID; } }

		#endregion

		#region Fields

		private CraftSystem m_System;
		private CraftItem m_CraftItem;
		private int m_ID;
		private TextDefinition m_TD;

		#endregion

		#region Properties

		/// <summary>
		/// The crafting system this recipe belongs to
		/// </summary>
		public CraftSystem CraftSystem
		{
			get { return m_System; }
			set { m_System = value; }
		}

		/// <summary>
		/// The craft item this recipe creates
		/// </summary>
		public CraftItem CraftItem
		{
			get { return m_CraftItem; }
			set { m_CraftItem = value; }
		}

		/// <summary>
		/// The unique ID of this recipe
		/// </summary>
		public int ID
		{
			get { return m_ID; }
		}

		/// <summary>
		/// The text definition for this recipe (name)
		/// </summary>
		public TextDefinition TextDefinition
		{
			get
			{
				if( m_TD == null )
					m_TD = new TextDefinition( m_CraftItem.NameNumber, m_CraftItem.NameString );

				return m_TD;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Recipe class
		/// </summary>
		/// <param name="id">The unique ID for this recipe</param>
		/// <param name="system">The crafting system this recipe belongs to</param>
		/// <param name="item">The craft item this recipe creates</param>
		/// <exception cref="Exception">Thrown if a recipe with the same ID already exists</exception>
		public Recipe( int id, CraftSystem system, CraftItem item )
		{
			m_ID = id;
			m_System = system;
			m_CraftItem = item;

			if( m_Recipes.ContainsKey( id ) )
				throw new Exception( "Attempting to create recipe with preexisting ID." );

			m_Recipes.Add( id, this );
			m_LargestRecipeID = Math.Max( id, m_LargestRecipeID );
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Initializes recipe-related commands
		/// </summary>
		public static void Initialize()
		{
			CommandSystem.Register( "LearnAllRecipes", AccessLevel.GameMaster, new CommandEventHandler( LearnAllRecipes_OnCommand ) );
			CommandSystem.Register( "ForgetAllRecipes", AccessLevel.GameMaster, new CommandEventHandler( ForgetAllRecipes_OnCommand ) );
		}

		/// <summary>
		/// Command handler for learning all recipes
		/// </summary>
		[Usage( "LearnAllRecipes" )]
		[Description( "Teaches a player all available recipes." )]
		private static void LearnAllRecipes_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			m.SendMessage( "Target a player to teach them all of the recipes." );

			m.BeginTarget( TARGET_RANGE, false, Server.Targeting.TargetFlags.None, new TargetCallback(
				delegate( Mobile from, object targeted )
				{
					if( targeted is PlayerMobile )
					{
						PlayerMobile pm = (PlayerMobile)targeted;
						foreach( KeyValuePair<int, Recipe> kvp in m_Recipes )
							pm.AcquireRecipe( kvp.Key );

						m.SendMessage( "You teach them all of the recipes." );
					}
					else
					{
						m.SendMessage( "That is not a player!" );
					}
				}
			) );
		}

		/// <summary>
		/// Command handler for forgetting all recipes
		/// </summary>
		[Usage( "ForgetAllRecipes" )]
		[Description( "Makes a player forget all the recipes they've learned." )]
		private static void ForgetAllRecipes_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			m.SendMessage( "Target a player to have them forget all of the recipes they've learned." );

			m.BeginTarget( TARGET_RANGE, false, Server.Targeting.TargetFlags.None, new TargetCallback(
				delegate( Mobile from, object targeted )
				{
					if( targeted is PlayerMobile )
					{
						PlayerMobile pm = (PlayerMobile)targeted;
						pm.ResetRecipes();

						m.SendMessage( "They forget all their recipes." );
					}
					else
					{
						m.SendMessage( "That is not a player!" );
					}
				}
			) );
		}

		#endregion
	}
}
