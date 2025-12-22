using System;
using Server.Items;

namespace Server.Spells.First
{
	/// <summary>
	/// Create Food - 1st Circle Utility Spell
	/// Conjures food (and occasionally water) into the caster's backpack
	/// </summary>
	public class CreateFoodSpell : MagerySpell
	{
		#region Constants
		// Effect Constants
		private const int EFFECT_ID = 0;
		private const int EFFECT_SPEED = 10;
		private const int EFFECT_RENDER = 5;
		private const int EFFECT_EFFECT = 2003;
		private const int EFFECT_HUE_DEFAULT = 0;

		// Sound Constants
		private const int SOUND_ID = 0x1E2;

		// Water Flask Creation
		private const int WATER_FLASK_CHANCE_MIN = 1;
		private const int WATER_FLASK_CHANCE_MAX = 10;
		private const int WATER_FLASK_CHANCE_THRESHOLD = 3; // 30% chance (1-3 out of 10)
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Create Food", "In Mani Ylem",
				224,
				9011,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public CreateFoodSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		private static FoodInfo[] m_Food = new FoodInfo[]
			{
				new FoodInfo( typeof( FoodStaleBread ), "pão mágico" ),
				new FoodInfo( typeof( Grapes ), "cacho de uvas" ),
				new FoodInfo( typeof( Ham ), "presunto" ),
				new FoodInfo( typeof( CheeseWedge ), "fatia de queijo" ),
				new FoodInfo( typeof( Muffins ), "muffins" ),
				new FoodInfo( typeof( FishSteak ), "filé de peixe" ),
				new FoodInfo( typeof( Ribs ), "costelinhas" ),
				new FoodInfo( typeof( CookedBird ), "frango cozido" ),
				new FoodInfo( typeof( Sausage ), "salsicha" ),
				new FoodInfo( typeof( Apple ), "maçã" ),
				new FoodInfo( typeof( Peach ), "pera" )
			};

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				FoodInfo foodInfo = SelectRandomFood();
				Item food = foodInfo.Create();

				if ( food != null )
				{
					CreateAndAddFood( food, foodInfo );
					PlayEffects();
					TryCreateWaterFlask();
				}
			}

			FinishSequence();
		}

		/// <summary>
		/// Selects a random food from the available options
		/// </summary>
		private FoodInfo SelectRandomFood()
		{
			return m_Food[Utility.Random( m_Food.Length )];
		}

		/// <summary>
		/// Adds the created food to the caster's backpack and notifies them
		/// </summary>
		private void CreateAndAddFood( Item food, FoodInfo foodInfo )
		{
			Caster.AddToBackpack( food );
			Caster.SendMessage( MSG_COLOR_ERROR, String.Format( SpellMessages.INFO_FOOD_CREATED_FORMAT, foodInfo.Name ) );
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		private void PlayEffects()
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue( Caster, EFFECT_HUE_DEFAULT );
			Caster.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_EFFECT, hue, EFFECT_HUE_DEFAULT, EffectLayer.RightHand );
			Caster.PlaySound( SOUND_ID );
		}

		/// <summary>
		/// 30% chance to create a water flask in addition to food
		/// </summary>
		private void TryCreateWaterFlask()
		{
			int roll = Utility.RandomMinMax( WATER_FLASK_CHANCE_MIN, WATER_FLASK_CHANCE_MAX );
			
			if ( roll <= WATER_FLASK_CHANCE_THRESHOLD )
			{
				Caster.AddToBackpack( new WaterFlask() );
				Caster.SendMessage( MSG_COLOR_SYSTEM, SpellMessages.INFO_WATER_FLASK_CREATED );
			}
		}
	}

	/// <summary>
	/// Food information helper class
	/// Contains food type and localized name
	/// </summary>
	public class FoodInfo
	{
		private Type m_Type;
		private string m_Name;

		public Type Type { get { return m_Type; } set { m_Type = value; } }
		public string Name { get { return m_Name; } set { m_Name = value; } }

		public FoodInfo( Type type, string name )
		{
			m_Type = type;
			m_Name = name;
		}

		/// <summary>
		/// Creates an instance of the food item
		/// </summary>
		public Item Create()
		{
			Item item;

			try
			{
				item = (Item)Activator.CreateInstance( m_Type );
			}
			catch
			{
				item = null;
			}

			return item;
		}
	}
}
