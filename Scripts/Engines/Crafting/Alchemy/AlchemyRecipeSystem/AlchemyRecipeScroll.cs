using System;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Stackable scroll that teaches an alchemy recipe when used.
	/// Players must have minimum alchemy skill to learn the recipe.
	/// </summary>
	public class AlchemyRecipeScroll : Item
	{
		private int m_RecipeID;

		[CommandProperty(AccessLevel.GameMaster)]
		public int RecipeID
		{
			get { return m_RecipeID; }
			set
			{
				m_RecipeID = value;
				InvalidateProperties();
			}
		}

		[Constructable]
		public AlchemyRecipeScroll() : this(500)
		{
		}

		[Constructable]
		public AlchemyRecipeScroll(int recipeID) : base(Server.Engines.Craft.AlchemyRecipeConstants.SCROLL_ITEM_ID)
		{
			m_RecipeID = recipeID;
			Name = Server.Engines.Craft.AlchemyRecipeStringConstants.SCROLL_NAME;
			Hue = Server.Engines.Craft.AlchemyRecipeConstants.BOOK_HUE;
			Weight = Server.Engines.Craft.AlchemyRecipeConstants.SCROLL_WEIGHT;
			Stackable = true; // Makes scrolls stackable
		}

		public AlchemyRecipeScroll(Serial serial) : base(serial)
		{
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			Server.Engines.Craft.AlchemyRecipeInfo recipe = Server.Engines.Craft.AlchemyRecipeData.GetRecipeByID(m_RecipeID);
			if (recipe != null)
			{
				string recipeText = String.Format(Server.Engines.Craft.AlchemyRecipeStringConstants.PROP_RECIPE, recipe.Name);
				string coloredText = String.Format("<BASEFONT COLOR=#00FFFF>{0}</BASEFONT>", recipeText);
				list.Add(coloredText);
			}
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendMessage(Server.Engines.Craft.AlchemyRecipeConstants.MSG_COLOR_ERROR,
					Server.Engines.Craft.AlchemyRecipeStringConstants.MSG_SCROLL_NOT_IN_BACKPACK);
				return;
			}

			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;
				Server.Engines.Craft.AlchemyRecipeInfo recipe = Server.Engines.Craft.AlchemyRecipeData.GetRecipeByID(m_RecipeID);

				if (recipe == null)
				{
					from.SendMessage(Server.Engines.Craft.AlchemyRecipeConstants.MSG_COLOR_ERROR,
						Server.Engines.Craft.AlchemyRecipeStringConstants.MSG_INVALID_RECIPE);
					return;
				}

				// Check if already knows recipe
				if (pm.HasRecipe(m_RecipeID))
				{
					from.SendMessage(Server.Engines.Craft.AlchemyRecipeConstants.MSG_COLOR_INFO,
						Server.Engines.Craft.AlchemyRecipeStringConstants.MSG_ALREADY_KNOW_RECIPE);
					return;
				}

				// Check if has minimum alchemy skill
				if (from.Skills[SkillName.Alchemy].Base < recipe.SkillMin)
				{
					from.SendMessage(Server.Engines.Craft.AlchemyRecipeConstants.MSG_COLOR_ERROR,
						String.Format(Server.Engines.Craft.AlchemyRecipeStringConstants.MSG_INSUFFICIENT_SKILL, recipe.SkillMin));
					return;
				}

				// Learn recipe
				pm.AcquireRecipe(m_RecipeID);
				from.SendMessage(Server.Engines.Craft.AlchemyRecipeConstants.MSG_COLOR_SUCCESS,
					String.Format(Server.Engines.Craft.AlchemyRecipeStringConstants.MSG_RECIPE_LEARNED, recipe.Name));
				from.PlaySound(Server.Engines.Craft.AlchemyRecipeConstants.SOUND_LEARN_RECIPE);

				// Consume 1 from stack
				Consume(1);
			}
		}

		/// <summary>
		/// Stacking logic - only stack with same recipe ID
		/// </summary>
		public override bool StackWith(Mobile from, Item dropped, bool playSound)
		{
			if (dropped is AlchemyRecipeScroll)
			{
				AlchemyRecipeScroll otherScroll = (AlchemyRecipeScroll)dropped;
				if (otherScroll.RecipeID == this.RecipeID)
				{
					return base.StackWith(from, dropped, playSound);
				}
			}
			return false;
		}

		/// <summary>
		/// Override GetHashCode for proper stacking
		/// </summary>
		public override int GetHashCode()
		{
			return m_RecipeID.GetHashCode();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version

			writer.Write((int)m_RecipeID);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			m_RecipeID = reader.ReadInt();
		}
	}
}

