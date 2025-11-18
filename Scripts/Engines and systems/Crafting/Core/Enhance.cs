using System;
using Server;
using Server.Targeting;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Result of an enhancement operation
	/// </summary>
	public enum EnhanceResult
	{
		/// <summary>No result (error occurred)</summary>
		None,
		/// <summary>Item is not in backpack</summary>
		NotInBackpack,
		/// <summary>Item cannot be enhanced</summary>
		BadItem,
		/// <summary>Resource is invalid or standard</summary>
		BadResource,
		/// <summary>Item is already enhanced</summary>
		AlreadyEnhanced,
		/// <summary>Enhancement succeeded</summary>
		Success,
		/// <summary>Enhancement failed (partial resource loss)</summary>
		Failure,
		/// <summary>Enhancement catastrophically failed (item destroyed)</summary>
		Broken,
		/// <summary>Insufficient resources</summary>
		NoResources,
		/// <summary>Insufficient skill</summary>
		NoSkill
	}

	/// <summary>
	/// Handles enhancing items with special material properties
	/// </summary>
	public class Enhance
	{
		#region Methods

		/// <summary>
		/// Attempts to enhance an item with special material properties
		/// </summary>
		/// <param name="from">The mobile attempting to enhance</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="tool">The crafting tool being used</param>
		/// <param name="item">The item to enhance</param>
		/// <param name="resource">The special resource to enhance with</param>
		/// <param name="resType">The resource type</param>
		/// <param name="resMessage">Output parameter for error messages</param>
		/// <returns>The result of the enhancement operation</returns>
		public static EnhanceResult Invoke( Mobile from, CraftSystem craftSystem, BaseTool tool, Item item, CraftResource resource, Type resType, ref object resMessage )
		{
			// Validate item
			if ( !IsValidEnhancementTarget( item, from ) )
				return EnhanceResult.BadItem;

			if ( !item.IsChildOf( from.Backpack ) )
				return EnhanceResult.NotInBackpack;

			if ( CraftResources.IsStandard( resource ) )
				return EnhanceResult.BadResource;
			
			// Check if player can craft this item type
			int num = craftSystem.CanCraft( from, tool, item.GetType() );
			
			if ( num > 0 )
			{
				resMessage = num;
				return EnhanceResult.None;
			}

			// Find craft item definition
			CraftItem craftItem = craftSystem.CraftItems.SearchForSubclass(item.GetType());

			if ( craftItem == null || craftItem.Resources.Count == 0 )
				return EnhanceResult.BadItem;

			// Check skill requirements
			bool allRequiredSkills = false;
			if( craftItem.GetSuccessChance( from, resType, craftSystem, false, ref allRequiredSkills ) <= 0.0 )
				return EnhanceResult.NoSkill;

			// Get resource info
			CraftResourceInfo info = CraftResources.GetInfo( resource );

			if ( info == null || info.ResourceTypes.Length == 0 )
				return EnhanceResult.BadResource;

			CraftAttributeInfo attributes = info.AttributeInfo;

			if ( attributes == null )
				return EnhanceResult.BadResource;

			// Check resource availability
			int resHue = 0, maxAmount = 0;

			if ( !craftItem.ConsumeRes( from, resType, craftSystem, ref resHue, ref maxAmount, ConsumeType.None, ref resMessage ) )
				return EnhanceResult.NoResources;

			// Consume Ancient Smithy Hammer if applicable
			ConsumeAncientSmithyHammer( from, craftSystem );

			// Calculate enhancement result
			EnhanceResult result = CalculateEnhancementResult( from, craftSystem, item, attributes );

			// Process result
			return ProcessEnhancementResult( from, craftSystem, item, resource, craftItem, resType, result, ref resHue, ref maxAmount, ref resMessage );
		}

		/// <summary>
		/// Validates if an item can be enhanced
		/// </summary>
		/// <param name="item">The item to validate</param>
		/// <param name="from">The mobile attempting to enhance</param>
		/// <returns>True if the item is a valid enhancement target</returns>
		private static bool IsValidEnhancementTarget( Item item, Mobile from )
		{
			if ( item == null )
				return false;

			if ( item is ILevelable )
				return false;

			if ( !(item is BaseArmor) && !(item is BaseWeapon) )
				return false;

			if ( item is IArcaneEquip )
			{
				IArcaneEquip eq = (IArcaneEquip)item;
				if ( eq.IsArcane )
					return false;
			}

			if ( item is BootsofHermes )
				return false;

			return true;
		}

		/// <summary>
		/// Consumes uses from Ancient Smithy Hammer if present and applicable
		/// </summary>
		/// <param name="from">The mobile performing the enhancement</param>
		/// <param name="craftSystem">The crafting system being used</param>
		private static void ConsumeAncientSmithyHammer( Mobile from, CraftSystem craftSystem )
		{
			if ( craftSystem is DefBlacksmithy )
			{
				AncientSmithyHammer hammer = from.FindItemOnLayer( Layer.OneHanded ) as AncientSmithyHammer;
				if ( hammer != null )
				{
					hammer.UsesRemaining--;
					if ( hammer.UsesRemaining < 1 )
						hammer.Delete();
				}
			}
		}

		/// <summary>
		/// Calculates the result of an enhancement attempt
		/// </summary>
		/// <param name="from">The mobile performing the enhancement</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="item">The item being enhanced</param>
		/// <param name="attributes">The attribute info for the resource</param>
		/// <returns>The enhancement result</returns>
		private static EnhanceResult CalculateEnhancementResult( Mobile from, CraftSystem craftSystem, Item item, CraftAttributeInfo attributes )
		{
			// Check if already enhanced
			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;
				if ( !CraftResources.IsStandard( weapon.Resource ) )
					return EnhanceResult.AlreadyEnhanced;
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;
				if ( !CraftResources.IsStandard( armor.Resource ) )
					return EnhanceResult.AlreadyEnhanced;
			}

			// Calculate base chance
			int baseChance = CalculateBaseChance( from, craftSystem );

			// Get item attributes with bonuses
			EnhancementAttributes itemAttrs = GetItemAttributes( item, attributes );

			// Check each bonus type
			EnhanceResult result = EnhanceResult.Success;

			if ( itemAttrs.PhysBonus )
				CheckResult( ref result, baseChance + itemAttrs.Phys );

			if ( itemAttrs.FireBonus )
				CheckResult( ref result, baseChance + itemAttrs.Fire );

			if ( itemAttrs.ColdBonus )
				CheckResult( ref result, baseChance + itemAttrs.Cold );

			if ( itemAttrs.NrgyBonus )
				CheckResult( ref result, baseChance + itemAttrs.Nrgy );

			if ( itemAttrs.PoisBonus )
				CheckResult( ref result, baseChance + itemAttrs.Pois );

			if ( itemAttrs.DuraBonus )
				CheckResult( ref result, baseChance + (itemAttrs.Dura / EnhanceConstants.DURABILITY_DIVISOR) );

			if ( itemAttrs.LuckBonus )
				CheckResult( ref result, baseChance + EnhanceConstants.LUCK_BASE_BONUS + (itemAttrs.Luck / EnhanceConstants.LUCK_DIVISOR) );

			if ( itemAttrs.LreqBonus )
				CheckResult( ref result, baseChance + (itemAttrs.Lreq / EnhanceConstants.LREQ_DIVISOR) );

			if ( itemAttrs.DincBonus )
				CheckResult( ref result, baseChance + (itemAttrs.Dinc / EnhanceConstants.DAMAGE_INC_DIVISOR) );

			return result;
		}

		/// <summary>
		/// Calculates the base success chance for enhancement
		/// </summary>
		/// <param name="from">The mobile performing the enhancement</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <returns>The base chance value</returns>
		private static int CalculateBaseChance( Mobile from, CraftSystem craftSystem )
		{
			int baseChance = EnhanceConstants.BASE_CHANCE;
			int skill = from.Skills[craftSystem.MainSkill].Fixed / EnhanceConstants.SKILL_FIXED_DIVISOR;

			if ( skill >= EnhanceConstants.SKILL_THRESHOLD )
				baseChance -= (skill - EnhanceConstants.SKILL_BASE) / EnhanceConstants.SKILL_DIVISOR;

			return baseChance;
		}

		/// <summary>
		/// Helper class to hold item attributes for enhancement calculation
		/// </summary>
		private class EnhancementAttributes
		{
			public int Phys;
			public int Fire;
			public int Cold;
			public int Nrgy;
			public int Pois;
			public int Dura;
			public int Luck;
			public int Lreq;
			public int Dinc;

			public bool PhysBonus;
			public bool FireBonus;
			public bool ColdBonus;
			public bool NrgyBonus;
			public bool PoisBonus;
			public bool DuraBonus;
			public bool LuckBonus;
			public bool LreqBonus;
			public bool DincBonus;
		}

		/// <summary>
		/// Gets enhancement attributes and bonuses from item and resource
		/// </summary>
		/// <param name="item">The item being enhanced</param>
		/// <param name="attributes">The resource attribute info</param>
		/// <returns>EnhancementAttributes with bonuses calculated</returns>
		private static EnhancementAttributes GetItemAttributes( Item item, CraftAttributeInfo attributes )
		{
			EnhancementAttributes attrs = new EnhancementAttributes();

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;
				attrs.Dura = weapon.MaxHitPoints;
				attrs.Luck = weapon.Attributes.Luck;
				attrs.Lreq = weapon.WeaponAttributes.LowerStatReq;
				attrs.Dinc = weapon.Attributes.WeaponDamage;

				attrs.FireBonus = ( attributes.WeaponFireDamage > 0 );
				attrs.ColdBonus = ( attributes.WeaponColdDamage > 0 );
				attrs.NrgyBonus = ( attributes.WeaponEnergyDamage > 0 );
				attrs.PoisBonus = ( attributes.WeaponPoisonDamage > 0 );
				attrs.DuraBonus = ( attributes.WeaponDurability > 0 );
				attrs.LuckBonus = ( attributes.WeaponLuck > 0 );
				attrs.LreqBonus = ( attributes.WeaponLowerRequirements > 0 );
				attrs.DincBonus = ( attrs.Dinc > 0 );
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;
				attrs.Phys = armor.PhysicalResistance;
				attrs.Fire = armor.FireResistance;
				attrs.Cold = armor.ColdResistance;
				attrs.Pois = armor.PoisonResistance;
				attrs.Nrgy = armor.EnergyResistance;
				attrs.Dura = armor.MaxHitPoints;
				attrs.Luck = armor.Attributes.Luck;
				attrs.Lreq = armor.ArmorAttributes.LowerStatReq;

				attrs.PhysBonus = ( attributes.ArmorPhysicalResist > 0 );
				attrs.FireBonus = ( attributes.ArmorFireResist > 0 );
				attrs.ColdBonus = ( attributes.ArmorColdResist > 0 );
				attrs.NrgyBonus = ( attributes.ArmorEnergyResist > 0 );
				attrs.PoisBonus = ( attributes.ArmorPoisonResist > 0 );
				attrs.DuraBonus = ( attributes.ArmorDurability > 0 );
				attrs.LuckBonus = ( attributes.ArmorLuck > 0 );
				attrs.LreqBonus = ( attributes.ArmorLowerRequirements > 0 );
				attrs.DincBonus = false;
			}

			return attrs;
		}

		/// <summary>
		/// Processes the enhancement result and applies changes
		/// </summary>
		/// <param name="from">The mobile performing the enhancement</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="item">The item being enhanced</param>
		/// <param name="resource">The resource being applied</param>
		/// <param name="craftItem">The craft item definition</param>
		/// <param name="resType">The resource type</param>
		/// <param name="result">The enhancement result</param>
		/// <param name="resHue">Resource hue (output)</param>
		/// <param name="maxAmount">Maximum amount (output)</param>
		/// <param name="resMessage">Resource message (output)</param>
		/// <returns>The final enhancement result</returns>
		private static EnhanceResult ProcessEnhancementResult( Mobile from, CraftSystem craftSystem, Item item, CraftResource resource, CraftItem craftItem, Type resType, EnhanceResult result, ref int resHue, ref int maxAmount, ref object resMessage )
		{
			switch ( result )
			{
				case EnhanceResult.Broken:
				{
					if ( !craftItem.ConsumeRes( from, resType, craftSystem, ref resHue, ref maxAmount, ConsumeType.Half, ref resMessage ) )
						return EnhanceResult.NoResources;

					item.Delete();
					break;
				}
				case EnhanceResult.Success:
				{
					if ( !craftItem.ConsumeRes( from, resType, craftSystem, ref resHue, ref maxAmount, ConsumeType.All, ref resMessage ) )
						return EnhanceResult.NoResources;

					ApplyEnhancement( item, resource );
					break;
				}
				case EnhanceResult.Failure:
				{
					if ( !craftItem.ConsumeRes( from, resType, craftSystem, ref resHue, ref maxAmount, ConsumeType.Half, ref resMessage ) )
						return EnhanceResult.NoResources;

					break;
				}
			}

			return result;
		}

		/// <summary>
		/// Applies the enhancement resource to an item
		/// </summary>
		/// <param name="item">The item to enhance</param>
		/// <param name="resource">The resource to apply</param>
		private static void ApplyEnhancement( Item item, CraftResource resource )
		{
			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;
				weapon.Resource = resource;

				int hue = weapon.GetElementalDamageHue();
				if ( hue > 0 )
					weapon.Hue = hue;
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;
				armor.Resource = resource;
			}
		}

		/// <summary>
		/// Checks the enhancement result against a chance value
		/// </summary>
		/// <param name="res">The current result (modified by reference)</param>
		/// <param name="chance">The chance value to check against</param>
		public static void CheckResult( ref EnhanceResult res, int chance )
		{
			if ( res != EnhanceResult.Success )
				return; // we've already failed..

			int random = Utility.Random( EnhanceConstants.RANDOM_MAX );

			if ( EnhanceConstants.FAILURE_CHANCE > random )
				res = EnhanceResult.Failure;
			else if ( chance > random )
				res = EnhanceResult.Broken;
		}

		/// <summary>
		/// Begins the targeting process for enhancement
		/// </summary>
		/// <param name="from">The mobile attempting to enhance</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="tool">The crafting tool being used</param>
		public static void BeginTarget( Mobile from, CraftSystem craftSystem, BaseTool tool )
		{
			CraftContext context = craftSystem.GetContext( from );

			if ( context == null )
				return;

			int lastRes = context.LastResourceIndex;
			CraftSubResCol subRes = craftSystem.CraftSubRes;

			if ( lastRes >= 0 && lastRes < subRes.Count )
			{
				CraftSubRes res = subRes.GetAt( lastRes );

				if ( from.Skills[craftSystem.MainSkill].Value < res.RequiredSkill )
				{
					from.SendGump( new CraftGump( from, craftSystem, tool, res.Message ) );
				}
				else
				{
					CraftResource resource = CraftResources.GetFromType( res.ItemType );

					if ( resource != CraftResource.None )
					{
						from.Target = new InternalTarget( craftSystem, tool, res.ItemType, resource );
						from.SendLocalizedMessage( EnhanceConstants.MSG_TARGET_ITEM );
					}
					else
					{
						from.SendGump( new CraftGump( from, craftSystem, tool, EnhanceConstants.MSG_SELECT_SPECIAL_MATERIAL ) );
					}
				}
			}
			else
			{
				from.SendGump( new CraftGump( from, craftSystem, tool, EnhanceConstants.MSG_SELECT_SPECIAL_MATERIAL ) );
			}
		}

		#endregion

		#region Internal Target Class

		/// <summary>
		/// Internal target class for selecting items to enhance
		/// </summary>
		private class InternalTarget : Target
		{
			private CraftSystem m_CraftSystem;
			private BaseTool m_Tool;
			private Type m_ResourceType;
			private CraftResource m_Resource;

			public InternalTarget( CraftSystem craftSystem, BaseTool tool, Type resourceType, CraftResource resource ) :  base ( 2, false, TargetFlags.None )
			{
				m_CraftSystem = craftSystem;
				m_Tool = tool;
				m_ResourceType = resourceType;
				m_Resource = resource;
			}

			/// <summary>
			/// Called when a target is selected
			/// </summary>
			/// <param name="from">The mobile who selected the target</param>
			/// <param name="targeted">The targeted object</param>
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					object message = null;
					EnhanceResult res = Enhance.Invoke( from, m_CraftSystem, m_Tool, (Item)targeted, m_Resource, m_ResourceType, ref message );

					message = GetResultMessage( res );
					from.SendGump( new CraftGump( from, m_CraftSystem, m_Tool, message ) );
				}
			}

			/// <summary>
			/// Gets the localized message for an enhancement result
			/// </summary>
			/// <param name="result">The enhancement result</param>
			/// <returns>The localized message number</returns>
			private int GetResultMessage( EnhanceResult result )
			{
				switch ( result )
				{
					case EnhanceResult.NotInBackpack:
						return EnhanceConstants.MSG_NOT_IN_BACKPACK;
					case EnhanceResult.AlreadyEnhanced:
						return EnhanceConstants.MSG_ALREADY_ENHANCED;
					case EnhanceResult.BadItem:
						return EnhanceConstants.MSG_BAD_ITEM;
					case EnhanceResult.BadResource:
						return EnhanceConstants.MSG_SELECT_SPECIAL_MATERIAL;
					case EnhanceResult.Broken:
						return EnhanceConstants.MSG_BROKEN;
					case EnhanceResult.Failure:
						return EnhanceConstants.MSG_FAILURE;
					case EnhanceResult.Success:
						return EnhanceConstants.MSG_SUCCESS;
					case EnhanceResult.NoSkill:
						return EnhanceConstants.MSG_NO_SKILL;
					default:
						return 0;
				}
			}
		}

		#endregion
	}
}