using System;
using Server;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Represents a recipe scroll that can be used to learn crafting recipes.
	/// Displays the recipe name in cyan and can be consumed to learn the recipe.
	/// </summary>
	public class RecipeScroll : Item
	{
		#region Properties

		/// <summary>
		/// Label number for recipe scroll
		/// </summary>
		public override int LabelNumber 
		{ 
			get { return RecipeScrollConstants.LABEL_NUMBER_RECIPE_SCROLL; } 
		}

		private int m_RecipeID;

		/// <summary>
		/// Gets or sets the recipe ID for this scroll
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public int RecipeID
		{
			get { return m_RecipeID; }
			set { m_RecipeID = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets the recipe associated with this scroll
		/// </summary>
		public Recipe Recipe
		{
			get
			{
				if( Recipe.Recipes.ContainsKey( m_RecipeID ) )
					return Recipe.Recipes[m_RecipeID];

				return null;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a recipe scroll from a Recipe object
		/// </summary>
		public RecipeScroll( Recipe r )
			: this( r.ID )
		{
		}

		/// <summary>
		/// Creates a recipe scroll with the specified recipe ID
		/// </summary>
		[Constructable]
		public RecipeScroll( int recipeID )
			: base( RecipeScrollConstants.ITEM_ID_RECIPE_SCROLL )
		{
			m_RecipeID = recipeID;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public RecipeScroll( Serial serial )
			: base( serial )
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Displays properties of the recipe scroll, including the recipe name in cyan
		/// </summary>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			Recipe r = this.Recipe;

			if( r != null ) 
			{
				string recipeName = r.TextDefinition.ToString();
				list.Add( RecipeScrollConstants.CLILOC_RECIPE_NAME, 
					ItemNameHue.UnifiedItemProps.SetColor( recipeName, RecipeScrollStringConstants.COLOR_RECIPE_NAME_CYAN ) );
			}
		}

		/// <summary>
		/// Handles double-clicking the scroll to learn the recipe.
		/// Checks range, skills, and whether player already knows the recipe.
		/// </summary>
		public override void OnDoubleClick( Mobile from )
		{
			if( !from.InRange( this.GetWorldLocation(), RecipeScrollConstants.INTERACT_RANGE ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, RecipeScrollConstants.MSG_COLOR_CANT_REACH, 
					RecipeScrollConstants.CLILOC_CANT_REACH );
				return;
			}

			Recipe r = this.Recipe;

			if( r != null && from is PlayerMobile )
			{
				PlayerMobile pm = from as PlayerMobile;

				if( !pm.HasRecipe( r ) )
				{
					bool allRequiredSkills = true;
					double chance = r.CraftItem.GetSuccessChance( from, null, r.CraftSystem, false, ref allRequiredSkills );

					if ( allRequiredSkills && chance >= 0.0 )
					{
						pm.SendLocalizedMessage( RecipeScrollConstants.CLILOC_RECIPE_LEARNED, r.TextDefinition.ToString() );
						pm.AcquireRecipe( r );
						this.Delete();
					}
					else
					{
						pm.SendLocalizedMessage( RecipeScrollConstants.CLILOC_INSUFFICIENT_SKILLS );
					}
				}
				else
				{
					pm.SendLocalizedMessage( RecipeScrollConstants.CLILOC_ALREADY_KNOW_RECIPE );
				}
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the recipe scroll data
		/// </summary>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)RecipeScrollConstants.SERIALIZATION_VERSION );
			writer.Write( (int)m_RecipeID );
		}

		/// <summary>
		/// Deserializes the recipe scroll data
		/// </summary>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
					{
						m_RecipeID = reader.ReadInt();
						break;
					}
			}
		}

		#endregion
	}
}

