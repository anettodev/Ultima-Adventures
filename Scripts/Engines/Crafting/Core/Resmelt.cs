using System;
using Server;
using Server.Targeting;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Result of a resmelting operation
	/// </summary>
	public enum SmeltResult
	{
		/// <summary>Resmelting was successful</summary>
		Success,
		/// <summary>Item cannot be resmelted (invalid type or resource)</summary>
		Invalid,
		/// <summary>Player lacks required mining skill</summary>
		NoSkill
	}

	/// <summary>
	/// Handles resmelting items back into their resource materials
	/// </summary>
	public class Resmelt
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Resmelt class
		/// </summary>
		public Resmelt()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initiates the resmelting process by setting up targeting
		/// </summary>
		/// <param name="from">The mobile attempting to resmelt</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="tool">The crafting tool being used</param>
		public static void Do( Mobile from, CraftSystem craftSystem, BaseTool tool )
		{
			int num = craftSystem.CanCraft( from, tool, null );

			if ( num > 0 && num != ResmeltConstants.MSG_MUST_BE_NEAR_FORGE_AND_ANVIL )
			{
				from.SendGump( new CraftGump( from, craftSystem, tool, num ) );
			}
			else
			{
				from.Target = new InternalTarget( craftSystem, tool );
				from.SendLocalizedMessage( ResmeltConstants.MSG_TARGET_ITEM_TO_RECYCLE );
			}
		}

		#endregion

		#region Internal Target Class

		/// <summary>
		/// Internal target class for selecting items to resmelt
		/// </summary>
		private class InternalTarget : Target
		{
			#region Fields

			private CraftSystem m_CraftSystem;
			private BaseTool m_Tool;

			#endregion

			#region Constructors

			/// <summary>
			/// Initializes a new instance of the InternalTarget class
			/// </summary>
			/// <param name="craftSystem">The crafting system being used</param>
			/// <param name="tool">The crafting tool being used</param>
			public InternalTarget( CraftSystem craftSystem, BaseTool tool ) : base( 2, false, TargetFlags.None )
			{
				m_CraftSystem = craftSystem;
				m_Tool = tool;
			}

			/// <summary>
			/// Attempts to resmelt an item back into its resource material
			/// </summary>
			/// <param name="from">The mobile attempting to resmelt</param>
			/// <param name="item">The item to resmelt</param>
			/// <param name="resource">The resource type of the item</param>
			/// <param name="ingotAmount">Output parameter: the amount of ingots created (only valid if result is Success)</param>
			/// <returns>The result of the resmelting operation</returns>
			private SmeltResult Resmelt( Mobile from, Item item, CraftResource resource, out int ingotAmount )
			{
				ingotAmount = 0;
				
				try
				{
					if ( CraftResources.GetType( resource ) != CraftResourceType.Metal )
						return SmeltResult.Invalid;

					CraftResourceInfo info = CraftResources.GetInfo( resource );

					if ( info == null || info.ResourceTypes.Length == 0 )
						return SmeltResult.Invalid;

					CraftItem craftItem = m_CraftSystem.CraftItems.SearchFor( item.GetType() );

					if ( craftItem == null || craftItem.Resources.Count == 0 )
						return SmeltResult.Invalid;

					CraftRes craftResource = craftItem.Resources.GetAt( 0 );

					if ( craftResource.Amount < ResmeltConstants.MIN_RESOURCE_AMOUNT )
						return SmeltResult.Invalid;

					double difficulty = GetResourceDifficulty( resource );

					if ( difficulty > from.Skills[ SkillName.Mining ].Value )
						return SmeltResult.NoSkill;

					Type resourceType = info.ResourceTypes[0];
					Item ingot = (Item)Activator.CreateInstance( resourceType );

					// Player-constructed items yield 50% of original resources
					if ( IsPlayerConstructed( item ) )
						ingot.Amount = craftResource.Amount / ResmeltConstants.PLAYER_CONSTRUCTED_DIVISOR;
					else
						ingot.Amount = ResmeltConstants.STORE_BOUGHT_AMOUNT;

					ingotAmount = ingot.Amount;

					item.Delete();
					from.AddToBackpack( ingot );

					from.PlaySound( ResmeltConstants.SOUND_SMELT_1 );
					from.PlaySound( ResmeltConstants.SOUND_SMELT_2 );
					return SmeltResult.Success;
				}
				catch
				{
				}

				return SmeltResult.Invalid;
			}

			/// <summary>
			/// Gets the mining skill difficulty required to resmelt a resource type
			/// </summary>
			/// <param name="resource">The resource type</param>
			/// <returns>The required mining skill, or 0.0 if resource is not resmeltable</returns>
			private double GetResourceDifficulty( CraftResource resource )
			{
				switch ( resource )
				{
					case CraftResource.DullCopper: return ResmeltConstants.DIFFICULTY_DULL_COPPER;
					case CraftResource.Copper: return ResmeltConstants.DIFFICULTY_COPPER;
					case CraftResource.Bronze: return ResmeltConstants.DIFFICULTY_BRONZE;
					case CraftResource.ShadowIron: return ResmeltConstants.DIFFICULTY_SHADOW_IRON;
					case CraftResource.Platinum: return ResmeltConstants.DIFFICULTY_PLATINUM;
					case CraftResource.Gold: return ResmeltConstants.DIFFICULTY_GOLD;
					case CraftResource.Agapite: return ResmeltConstants.DIFFICULTY_AGAPITE;
					case CraftResource.Verite: return ResmeltConstants.DIFFICULTY_VERITE;
					case CraftResource.Valorite: return ResmeltConstants.DIFFICULTY_VALORITE;
					case CraftResource.Titanium: return ResmeltConstants.DIFFICULTY_TITANIUM;
					case CraftResource.Rosenium: return ResmeltConstants.DIFFICULTY_ROSENIUM;
					case CraftResource.Nepturite: return ResmeltConstants.DIFFICULTY_NEPTURITE;
					case CraftResource.Obsidian: return ResmeltConstants.DIFFICULTY_OBSIDIAN;
					case CraftResource.Steel: return ResmeltConstants.DIFFICULTY_STEEL;
					case CraftResource.Brass: return ResmeltConstants.DIFFICULTY_BRASS;
					case CraftResource.Mithril: return ResmeltConstants.DIFFICULTY_MITHRIL;
					case CraftResource.Xormite: return ResmeltConstants.DIFFICULTY_XORMITE;
					case CraftResource.Dwarven: return ResmeltConstants.DIFFICULTY_DWARVEN;
					default: return 0.0;
				}
			}

			/// <summary>
			/// Checks if an item is player-constructed (yields 50% resources when resmelted)
			/// </summary>
			/// <param name="item">The item to check</param>
			/// <returns>True if the item is player-constructed</returns>
			private bool IsPlayerConstructed( Item item )
			{
				if ( item is DragonBardingDeed )
					return true;

				if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;
					return armor.PlayerConstructed;
				}

				if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;
					return weapon.PlayerConstructed;
				}

				if ( item is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)item;
					return clothing.PlayerConstructed;
				}

				return false;
			}

			/// <summary>
			/// Called when a target is selected for resmelting
			/// </summary>
			/// <param name="from">The mobile who selected the target</param>
			/// <param name="targeted">The targeted object</param>
			protected override void OnTarget( Mobile from, object targeted )
			{
				int num = m_CraftSystem.CanCraft( from, m_Tool, null );

				if ( num > 0 )
				{
					if ( num == ResmeltConstants.MSG_MUST_BE_NEAR_FORGE_AND_ANVIL )
					{
						bool anvil, forge;
			
						DefBlacksmithy.CheckAnvilAndForge( from, 2, out anvil, out forge );

						if ( !anvil )
							num = ResmeltConstants.MSG_MUST_BE_NEAR_ANVIL;
						else if ( !forge )
							num = ResmeltConstants.MSG_MUST_BE_NEAR_FORGE;
					}
					
					from.SendGump( new CraftGump( from, m_CraftSystem, m_Tool, num ) );
				}
				else
				{
					SmeltResult result = SmeltResult.Invalid;
					bool isStoreBought = false;
					int ingotAmount = 0;
					object message;

					if ( targeted is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)targeted;
						result = Resmelt( from, armor, armor.Resource, out ingotAmount );
						isStoreBought = !armor.PlayerConstructed;
					}
					else if ( targeted is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)targeted;
						result = Resmelt( from, weapon, weapon.Resource, out ingotAmount );
						isStoreBought = !weapon.PlayerConstructed;
					}
					else if ( targeted is DragonBardingDeed )
					{
						DragonBardingDeed deed = (DragonBardingDeed)targeted;
						result = Resmelt( from, deed, deed.Resource, out ingotAmount );
						isStoreBought = false;
					}

					message = GetResultMessage( result, isStoreBought, ingotAmount );
					from.SendGump( new CraftGump( from, m_CraftSystem, m_Tool, message ) );
				}
			}

			/// <summary>
			/// Gets the localized message for a resmelt result
			/// </summary>
			/// <param name="result">The resmelt result</param>
			/// <param name="isStoreBought">Whether the item was store-bought</param>
			/// <param name="ingotAmount">The amount of ingots created (only used for success messages)</param>
			/// <returns>The message (int for cliloc, or string for PT-BR with amount)</returns>
			private object GetResultMessage( SmeltResult result, bool isStoreBought, int ingotAmount )
			{
				switch ( result )
				{
					case SmeltResult.NoSkill:
						return ResmeltConstants.MSG_NO_SKILL;

					case SmeltResult.Success:
						// Return PT-BR string with ingot amount instead of cliloc
						return String.Format( CraftGumpStringConstants.NOTICE_SMELT_SUCCESS, ingotAmount );

					case SmeltResult.Invalid:
					default:
						return ResmeltConstants.MSG_CANNOT_SMELT;
				}
			}

			#endregion
		}

		#endregion
	}
}