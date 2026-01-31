using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Handles item repair functionality for all crafting systems
	/// </summary>
	public class Repair
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Repair class
		/// </summary>
		public Repair()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initiates the repair process using a crafting tool
		/// </summary>
		/// <param name="from">The mobile attempting to repair</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="tool">The crafting tool being used</param>
		public static void Do( Mobile from, CraftSystem craftSystem, BaseTool tool )
		{
			from.Target = new InternalTarget( craftSystem, tool );
			from.SendLocalizedMessage( RepairConstants.MSG_TARGET_ITEM );
		}

		/// <summary>
		/// Initiates the repair process using a repair deed
		/// </summary>
		/// <param name="from">The mobile attempting to repair</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="deed">The repair deed being used</param>
		public static void Do( Mobile from, CraftSystem craftSystem, RepairDeed deed )
		{
			from.Target = new InternalTarget( craftSystem, deed );
			from.SendLocalizedMessage( RepairConstants.MSG_TARGET_ITEM );
		}

		#endregion

		#region Internal Target Class

		/// <summary>
		/// Internal target class for selecting items to repair
		/// </summary>
		private class InternalTarget : Target
		{
			#region Fields

			private CraftSystem m_CraftSystem;
			private BaseTool m_Tool;
			private RepairDeed m_Deed;

			#endregion

			#region Constructors

			/// <summary>
			/// Initializes a new instance of the InternalTarget class with a tool
			/// </summary>
			/// <param name="craftSystem">The crafting system being used</param>
			/// <param name="tool">The crafting tool being used</param>
			public InternalTarget( CraftSystem craftSystem, BaseTool tool ) : base( RepairConstants.TARGET_RANGE, false, TargetFlags.None )
			{
				m_CraftSystem = craftSystem;
				m_Tool = tool;
			}

			/// <summary>
			/// Initializes a new instance of the InternalTarget class with a repair deed
			/// </summary>
			/// <param name="craftSystem">The crafting system being used</param>
			/// <param name="deed">The repair deed being used</param>
			public InternalTarget( CraftSystem craftSystem, RepairDeed deed ) : base( RepairConstants.TARGET_RANGE, false, TargetFlags.None )
			{
				m_CraftSystem = craftSystem;
				m_Deed = deed;
			}

			#endregion

			#region Helper Methods

			/// <summary>
			/// Ends the golem repair action after cooldown
			/// </summary>
			/// <param name="state">The mobile that was repairing</param>
			private static void EndGolemRepair( object state )
			{
				Mobile mobile = (Mobile)state;
				mobile.EndAction( typeof( Golem ) );
			}

			/// <summary>
			/// Calculates the chance that an item will be weakened during repair
			/// Formula: 40% - (1% per hp lost) - (1% per 10 craft skill)
			/// </summary>
			/// <param name="mob">The mobile performing the repair</param>
			/// <param name="skill">The skill being used</param>
			/// <param name="curHits">Current hit points</param>
			/// <param name="maxHits">Maximum hit points</param>
			/// <returns>The weaken chance percentage</returns>
			private int GetWeakenChance( Mobile mob, SkillName skill, int curHits, int maxHits )
			{
				double skillValue = ( m_Deed != null ) ? m_Deed.SkillLevel : mob.Skills[skill].Value;
				return RepairConstants.WEAKEN_BASE_CHANCE + (maxHits - curHits) - (int)(skillValue / RepairConstants.WEAKEN_SKILL_DIVISOR);
			}

			/// <summary>
			/// Checks if an item should be weakened during repair
			/// At 100+ skill, there's a 50% chance to completely bypass weakening
			/// </summary>
			/// <param name="mob">The mobile performing the repair</param>
			/// <param name="skill">The skill being used</param>
			/// <param name="curHits">Current hit points</param>
			/// <param name="maxHits">Maximum hit points</param>
			/// <returns>True if the item should be weakened</returns>
			private bool CheckWeaken( Mobile mob, SkillName skill, int curHits, int maxHits )
			{
				double skillLevel = ( m_Deed != null ) ? m_Deed.SkillLevel : mob.Skills[skill].Value;

				// At 100+ skill, 50% chance to completely bypass weakening
				if ( skillLevel >= RepairConstants.SKILL_NO_REDUCTION )
				{
					if ( Utility.Random( 2 ) == 0 ) // 50% chance (0 or 1, returns false for 0)
						return false; // Skip weakening entirely
				}

				return GetWeakenChance( mob, skill, curHits, maxHits ) > Utility.Random( RepairConstants.WEAKEN_RANDOM_MAX );
			}

			/// <summary>
			/// Calculates the repair difficulty based on item condition
			/// </summary>
			/// <param name="curHits">Current hit points</param>
			/// <param name="maxHits">Maximum hit points</param>
			/// <returns>The repair difficulty value</returns>
			private int GetRepairDifficulty( int curHits, int maxHits )
			{
				return (((maxHits - curHits) * RepairConstants.DIFFICULTY_MULTIPLIER) / Math.Max( maxHits, 1 )) - RepairConstants.DIFFICULTY_SUBTRACTOR;
			}

			/// <summary>
			/// Checks if the mobile has sufficient skill to repair the item
			/// </summary>
			/// <param name="mob">The mobile performing the repair</param>
			/// <param name="skill">The skill being used</param>
			/// <param name="curHits">Current hit points</param>
			/// <param name="maxHits">Maximum hit points</param>
			/// <returns>True if the repair attempt succeeds</returns>
			private bool CheckRepairDifficulty( Mobile mob, SkillName skill, int curHits, int maxHits )
			{
				double difficulty = GetRepairDifficulty( curHits, maxHits ) * RepairConstants.DIFFICULTY_SCALAR;

				if ( m_Deed != null )
				{
					double value = m_Deed.SkillLevel;
					double minSkill = difficulty - RepairConstants.DIFFICULTY_VARIANCE;
					double maxSkill = difficulty + RepairConstants.DIFFICULTY_VARIANCE;

					if ( value < minSkill )
						return false; // Too difficult
					else if ( value >= maxSkill )
						return true; // No challenge

					double chance = (value - minSkill) / (maxSkill - minSkill);
					return (chance >= Utility.RandomDouble());
				}
				else
				{
					return mob.CheckSkill( skill, difficulty - RepairConstants.DIFFICULTY_VARIANCE, difficulty + RepairConstants.DIFFICULTY_VARIANCE );
				}
			}

			/// <summary>
			/// Gets the amount an item should be weakened based on skill level
			/// Linear formula: reduction = 8 - (skill / 100) * 8
			/// Higher skill = less reduction (0 at 100+ skill, 8 at 0 skill)
			/// </summary>
			/// <param name="from">The mobile performing the repair</param>
			/// <param name="skill">The skill being used</param>
			/// <returns>The weaken amount (0 to 8, linearly distributed by skill, 0 at 100+ skill)</returns>
			private int GetWeakenAmount( Mobile from, SkillName skill )
			{
				double skillLevel = ( m_Deed != null ) ? m_Deed.SkillLevel : from.Skills[skill].Base;

				// If skill is 100 or higher, no reduction
				if ( skillLevel >= RepairConstants.SKILL_NO_REDUCTION )
					return RepairConstants.WEAKEN_MIN_AMOUNT;

				// Clamp skill level between 0 and 100 for calculation
				skillLevel = Math.Max( 0.0, Math.Min( RepairConstants.SKILL_NO_REDUCTION, skillLevel ) );

				// Linear formula: reduction = 8 - (skill / 100) * 8
				// At skill 0: 8 - 0 = 8
				// At skill 50: 8 - 4 = 4
				// At skill 100: 8 - 8 = 0
				double reduction = RepairConstants.WEAKEN_MAX_AMOUNT - ( skillLevel / RepairConstants.SKILL_NO_REDUCTION ) * ( RepairConstants.WEAKEN_MAX_AMOUNT - RepairConstants.WEAKEN_MIN_AMOUNT );

				// Round to nearest integer and clamp between min and max
				int weakenAmount = (int)Math.Round( reduction );
				return Math.Max( RepairConstants.WEAKEN_MIN_AMOUNT, Math.Min( RepairConstants.WEAKEN_MAX_AMOUNT, weakenAmount ) );
			}

			/// <summary>
			/// Checks if a repair deed is valid for use
			/// </summary>
			/// <param name="from">The mobile using the deed</param>
			/// <returns>True if the deed is valid or no deed is being used</returns>
			private bool CheckDeed( Mobile from )
			{
				if ( m_Deed != null )
				{
					return m_Deed.Check( from );
				}

				return true;
			}

			/// <summary>
			/// Checks if clothing is a special item that can be repaired but not crafted
			/// </summary>
			/// <param name="clothing">The clothing item to check</param>
			/// <returns>True if the item is special and repairable</returns>
			private bool IsSpecialClothing( BaseClothing clothing )
			{
				if ( m_CraftSystem is DefTailoring )
				{
					return (clothing is BearMask)
						|| (clothing is DeerMask)
						|| (clothing is BaseLevelClothing);
				}

				return false;
			}

			/// <summary>
			/// Checks if armor is a special item that can be repaired but not crafted
			/// </summary>
			/// <param name="armor">The armor item to check</param>
			/// <returns>True if the item is special and repairable</returns>
			private bool IsSpecialArmor( BaseArmor armor )
			{
				if ( m_CraftSystem is DefCarpentry )
				{
					if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ((Item)armor) ) )
						return true;
				}
				else if ( m_CraftSystem is DefTailoring )
				{
					if ( Server.Misc.MaterialInfo.IsAnyKindOfClothItem( ((Item)armor) ) )
						return true;
				}
				else if ( m_CraftSystem is DefBlacksmithy )
				{
					if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ((Item)armor) ) )
						return true;
				}
				return false;
			}

			/// <summary>
			/// Checks if a weapon is a special item that can be repaired but not crafted
			/// </summary>
			/// <param name="weapon">The weapon item to check</param>
			/// <returns>True if the item is special and repairable</returns>
			private bool IsSpecialWeapon( BaseWeapon weapon )
			{
				if ( m_CraftSystem is DefTinkering )
				{
					return ( weapon is Cleaver )
						|| ( weapon is Hatchet )
						|| ( weapon is Pickaxe )
						|| ( weapon is AssassinSpike )
						|| ( weapon is ButcherKnife )
						|| ( weapon is SkinningKnife )
						|| ( weapon is BaseGiftStave )
						|| ( weapon is BaseWizardStaff )
						|| ( weapon is KilrathiHeavyGun )
						|| ( weapon is KilrathiGun )
						|| ( weapon is LightSword )
						|| ( weapon is DoubleLaserSword );
				}
				else if ( m_CraftSystem is DefCarpentry )
				{
					if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ((Item)weapon) ) && !( weapon is BaseRanged ) )
						return true;
				}
				else if ( m_CraftSystem is DefBowFletching )
				{
					if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ((Item)weapon) ) && ( weapon is BaseRanged ) )
						return true;
				}
				else if ( m_CraftSystem is DefBlacksmithy )
				{
					if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ((Item)weapon) ) )
						return true;
				}
				else if ( m_CraftSystem is DefTailoring )
				{
					if ( Server.Misc.MaterialInfo.IsAnyKindOfClothItem( ((Item)weapon) ) )
						return true;

					return ( weapon is PugilistGlove ) 
						|| ( weapon is ThrowingGloves ) 
						|| ( weapon is LevelPugilistGloves ) 
						|| ( weapon is LevelThrowingGloves ) 
						|| ( weapon is GiftPugilistGloves ) 
						|| ( weapon is GiftThrowingGloves ) 
						|| ( weapon is PugilistGloves );
				}

				return false;
			}

			/// <summary>
			/// Called when a target is selected for repair
			/// </summary>
			/// <param name="from">The mobile who selected the target</param>
			/// <param name="targeted">The targeted object</param>
			protected override void OnTarget( Mobile from, object targeted )
			{
				int number = 0;

				if ( !CheckDeed( from ) )
					return;

				bool usingDeed = (m_Deed != null);
				bool toDelete = false;

				// Check forge/anvil requirement
				int craftCheck = m_CraftSystem.CanCraft( from, m_Tool, targeted.GetType() );
				if ( craftCheck == RepairConstants.MSG_FORGE_ANVIL_CHECK )
				{
					number = RepairConstants.MSG_MUST_BE_NEAR_FORGE_AND_ANVIL;
				}
				// Handle BaseJewel repair (Tinkering only)
				else if ( m_CraftSystem is DefTinkering && targeted is BaseJewel )
				{
					BaseJewel jewel = (BaseJewel)targeted;
					SkillName skill = m_CraftSystem.MainSkill;
					int toWeaken = GetWeakenAmount( from, skill );

					number = TryRepairItem( from, jewel, skill, toWeaken, usingDeed, ref toDelete );
				}
				// Handle Golem repair (Tinkering only)
				else if ( m_CraftSystem is DefTinkering && targeted is Golem )
				{
					Golem golem = (Golem)targeted;
					number = TryRepairGolem( from, golem, usingDeed, ref toDelete );
				}
				// Handle BaseWeapon repair
				else if ( targeted is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)targeted;
					SkillName skill = m_CraftSystem.MainSkill;
					int toWeaken = GetWeakenAmount( from, skill );

					if ( m_CraftSystem.CraftItems.SearchForSubclass( weapon.GetType() ) == null && !IsSpecialWeapon( weapon ) )
					{
						number = usingDeed ? RepairConstants.MSG_CANNOT_REPAIR_WITH_DEED : RepairConstants.MSG_CANNOT_REPAIR;
					}
					else
					{
						number = TryRepairItem( from, weapon, skill, toWeaken, usingDeed, ref toDelete );
					}
				}
				// Handle BaseArmor repair
				else if ( targeted is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)targeted;
					SkillName skill = m_CraftSystem.MainSkill;
					int toWeaken = GetWeakenAmount( from, skill );

					if ( m_CraftSystem.CraftItems.SearchForSubclass( armor.GetType() ) == null && !IsSpecialArmor( armor ) )
					{
						number = usingDeed ? RepairConstants.MSG_CANNOT_REPAIR_WITH_DEED : RepairConstants.MSG_CANNOT_REPAIR;
					}
					else
					{
						number = TryRepairItem( from, armor, skill, toWeaken, usingDeed, ref toDelete );
					}
				}
				// Handle BaseClothing repair
				else if ( targeted is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)targeted;
					SkillName skill = m_CraftSystem.MainSkill;
					int toWeaken = GetWeakenAmount( from, skill );

					bool isSpecialClothing = (targeted is BaseLevelClothing) || (targeted is TribalMask) || (targeted is HornedTribalMask);

					if ( m_CraftSystem.CraftItems.SearchForSubclass(clothing.GetType()) == null && !IsSpecialClothing(clothing) && !isSpecialClothing )
					{
						number = usingDeed ? RepairConstants.MSG_CANNOT_REPAIR_WITH_DEED : RepairConstants.MSG_CANNOT_REPAIR;
					}
					else
					{
						number = TryRepairItem( from, clothing, skill, toWeaken, usingDeed, ref toDelete );
					}
				}
				// Handle BlankScroll (create repair deed)
				else if ( !usingDeed && targeted is BlankScroll )
				{
					number = TryCreateRepairDeed( from, (BlankScroll)targeted );
				}
				// Handle generic Item
				else if ( targeted is Item )
				{
					number = usingDeed ? RepairConstants.MSG_CANNOT_REPAIR_WITH_DEED : RepairConstants.MSG_CANNOT_REPAIR;
				}
				// Invalid target
				else
				{
					number = RepairConstants.MSG_CANNOT_REPAIR_GENERIC;
				}

				// Send result to player (only if not using timer - timer will send its own message)
				if ( number != 0 )
				{
					if ( !usingDeed )
					{
						CraftContext context = m_CraftSystem.GetContext( from );
						from.SendGump( new CraftGump( from, m_CraftSystem, m_Tool, number ) );
					}
					else if ( toDelete )
					{
						from.SendLocalizedMessage( number );
						m_Deed.Delete();
					}
				}
			}

			#endregion

			#region Repair Helper Methods

			/// <summary>
			/// Attempts to repair an item with hit points (BaseJewel, BaseWeapon, BaseArmor, BaseClothing)
			/// Starts a repair timer with delay, sound, and visual effects
			/// </summary>
			/// <param name="from">The mobile performing the repair</param>
			/// <param name="item">The item to repair (must implement IEntityWithHitPoints)</param>
			/// <param name="skill">The skill being used</param>
			/// <param name="toWeaken">The amount to weaken if repair fails</param>
			/// <param name="usingDeed">Whether a repair deed is being used</param>
			/// <param name="toDelete">Output parameter indicating if deed should be deleted</param>
			/// <returns>The message number to display (0 if timer started successfully)</returns>
			private int TryRepairItem( Mobile from, Item item, SkillName skill, int toWeaken, bool usingDeed, ref bool toDelete )
			{
				// Get item hit points (all repairable items implement MaxHitPoints/HitPoints)
				int maxHits = 0;
				int curHits = 0;

				if ( item is BaseJewel )
				{
					BaseJewel jewel = (BaseJewel)item;
					maxHits = jewel.MaxHitPoints;
					curHits = jewel.HitPoints;
				}
				else if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;
					maxHits = weapon.MaxHitPoints;
					curHits = weapon.HitPoints;
				}
				else if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;
					maxHits = armor.MaxHitPoints;
					curHits = armor.HitPoints;
				}
				else if ( item is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)item;
					maxHits = clothing.MaxHitPoints;
					curHits = clothing.HitPoints;
				}

				// Validation checks
				if ( !item.IsChildOf( from.Backpack ) )
				{
					return RepairConstants.MSG_MUST_BE_IN_BACKPACK;
				}

				if ( maxHits <= 0 || curHits == maxHits )
				{
					return RepairConstants.MSG_FULL_REPAIR;
				}

				if ( maxHits <= toWeaken )
				{
					return RepairConstants.MSG_TOO_MANY_REPAIRS;
				}

				// Start repair timer with delay, sound, and visual effects
				new RepairTimer( from, item, skill, toWeaken, usingDeed, m_CraftSystem, m_Tool, m_Deed ).Start();
				return 0; // Timer started, message will be sent by timer
			}

			/// <summary>
			/// Performs the actual repair logic (called by RepairTimer after delay)
			/// </summary>
			/// <param name="from">The mobile performing the repair</param>
			/// <param name="item">The item to repair</param>
			/// <param name="skill">The skill being used</param>
			/// <param name="toWeaken">The amount to weaken if repair fails</param>
			/// <param name="usingDeed">Whether a repair deed is being used</param>
			/// <param name="toDelete">Output parameter indicating if deed should be deleted</param>
			/// <returns>The message number to display</returns>
			private int PerformRepair( Mobile from, Item item, SkillName skill, int toWeaken, bool usingDeed, ref bool toDelete )
			{
				// Get item hit points
				int maxHits = 0;
				int curHits = 0;

				if ( item is BaseJewel )
				{
					BaseJewel jewel = (BaseJewel)item;
					maxHits = jewel.MaxHitPoints;
					curHits = jewel.HitPoints;
				}
				else if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;
					maxHits = weapon.MaxHitPoints;
					curHits = weapon.HitPoints;
				}
				else if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;
					maxHits = armor.MaxHitPoints;
					curHits = armor.HitPoints;
				}
				else if ( item is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)item;
					maxHits = clothing.MaxHitPoints;
					curHits = clothing.HitPoints;
				}

				// Re-validate (item might have been moved/deleted during delay)
				if ( item.Deleted || !item.IsChildOf( from.Backpack ) )
				{
					return RepairConstants.MSG_MUST_BE_IN_BACKPACK;
				}

				if ( maxHits <= 0 || curHits == maxHits )
				{
					return RepairConstants.MSG_FULL_REPAIR;
				}

				// Attempt repair
				if ( CheckWeaken( from, skill, curHits, maxHits ) )
				{
					// Check if reduction is zero (no actual reduction)
					if ( toWeaken == 0 )
					{
						// No durability reduction - send message and play woohoo emote
						from.SendMessage( CraftGumpStringConstants.NOTICE_NO_DURABILITY_REDUCTION );
						from.PlaySound( from.Female ? 783 : 1054 );
						from.Say( "*woohoo!*" );
					}
					else
					{
						// Durability was reduced - send message with amount
						from.SendMessage( String.Format( CraftGumpStringConstants.NOTICE_DURABILITY_REDUCED, toWeaken ) );
					}

					maxHits -= toWeaken;
					curHits = Math.Max( 0, curHits - toWeaken );

					// Update item hit points
					if ( item is BaseJewel )
					{
						BaseJewel jewel = (BaseJewel)item;
						jewel.MaxHitPoints = maxHits;
						jewel.HitPoints = curHits;
					}
					else if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;
						weapon.MaxHitPoints = maxHits;
						weapon.HitPoints = curHits;
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;
						armor.MaxHitPoints = maxHits;
						armor.HitPoints = curHits;
					}
					else if ( item is BaseClothing )
					{
						BaseClothing clothing = (BaseClothing)item;
						clothing.MaxHitPoints = maxHits;
						clothing.HitPoints = curHits;
					}
				}

				// Check repair success
				if ( CheckRepairDifficulty( from, skill, curHits, maxHits ) )
				{
					// Repair successful - restore to full
					if ( item is BaseJewel )
					{
						BaseJewel jewel = (BaseJewel)item;
						jewel.HitPoints = jewel.MaxHitPoints;
					}
					else if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;
						weapon.HitPoints = weapon.MaxHitPoints;
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;
						armor.HitPoints = armor.MaxHitPoints;
					}
					else if ( item is BaseClothing )
					{
						BaseClothing clothing = (BaseClothing)item;
						clothing.HitPoints = clothing.MaxHitPoints;
					}

					m_CraftSystem.PlayCraftEffect( from );
					toDelete = true;
					return RepairConstants.MSG_REPAIR_SUCCESS;
				}
				else
				{
					// Repair failed
					m_CraftSystem.PlayCraftEffect( from );
					toDelete = true;
					return usingDeed ? RepairConstants.MSG_REPAIR_FAILURE_DEED : RepairConstants.MSG_REPAIR_FAILURE;
				}
			}

			/// <summary>
			/// Attempts to repair a golem
			/// </summary>
			/// <param name="from">The mobile performing the repair</param>
			/// <param name="golem">The golem to repair</param>
			/// <param name="usingDeed">Whether a repair deed is being used</param>
			/// <param name="toDelete">Output parameter indicating if deed should be deleted</param>
			/// <returns>The message number to display</returns>
			private int TryRepairGolem( Mobile from, Golem golem, bool usingDeed, ref bool toDelete )
			{
				if ( golem.IsDeadBondedPet )
				{
					return RepairConstants.MSG_CANNOT_REPAIR_GENERIC;
				}

				int damage = golem.HitsMax - golem.Hits;

				if ( damage <= 0 )
				{
					return RepairConstants.MSG_ALREADY_FULL_REPAIR;
				}

				double skillValue = usingDeed ? m_Deed.SkillLevel : from.Skills[SkillName.Tinkering].Value;

				if ( skillValue < RepairConstants.SKILL_MIN_FOR_GOLEM )
				{
					return RepairConstants.MSG_NO_SKILL;
				}

				if ( !from.CanBeginAction( typeof( Golem ) ) )
				{
					return RepairConstants.MSG_MUST_WAIT;
				}

				// Calculate repair amount
				if ( damage > (int)(skillValue * RepairConstants.GOLEM_DAMAGE_PERCENT) )
					damage = (int)(skillValue * RepairConstants.GOLEM_DAMAGE_PERCENT);

				damage += RepairConstants.GOLEM_BASE_DAMAGE;

				if ( !from.CheckSkill( SkillName.Tinkering, RepairConstants.GOLEM_SKILL_MIN, RepairConstants.GOLEM_SKILL_MAX ) )
					damage /= RepairConstants.GOLEM_FAILURE_DIVISOR;

				// Consume ingots
				Container pack = from.Backpack;

				if ( pack != null )
				{
					int ingotsConsumed = pack.ConsumeUpTo( typeof( IronIngot ), (damage + RepairConstants.GOLEM_INGOT_OFFSET) / RepairConstants.GOLEM_INGOT_DIVISOR );

					if ( ingotsConsumed > 0 )
					{
						golem.Hits += ingotsConsumed * RepairConstants.GOLEM_HITS_PER_INGOT;

						from.BeginAction( typeof( Golem ) );
						Timer.DelayCall( TimeSpan.FromSeconds( RepairConstants.GOLEM_COOLDOWN_SECONDS ), new TimerStateCallback( EndGolemRepair ), from );

						toDelete = true;
						return RepairConstants.MSG_REPAIR_SUCCESS;
					}
					else
					{
						return RepairConstants.MSG_INSUFFICIENT_METAL;
					}
				}
				else
				{
					return RepairConstants.MSG_INSUFFICIENT_METAL;
				}
			}

			/// <summary>
			/// Attempts to create a repair deed from a blank scroll
			/// </summary>
			/// <param name="from">The mobile creating the deed</param>
			/// <param name="scroll">The blank scroll being used</param>
			/// <returns>The message number to display</returns>
			private int TryCreateRepairDeed( Mobile from, BlankScroll scroll )
			{
				SkillName skill = m_CraftSystem.MainSkill;

				if ( from.Skills[skill].Value >= RepairConstants.SKILL_MIN_FOR_DEED )
				{
					scroll.Consume( 1 );
					RepairDeed deed = new RepairDeed( RepairDeed.GetTypeFor( m_CraftSystem ), from.Skills[skill].Value, from );
					from.AddToBackpack( deed );

					return RepairConstants.MSG_CREATE_REPAIR_DEED;
				}
				else
				{
					return RepairConstants.MSG_APPRENTICE_REQUIRED;
				}
			}

			#endregion
		}

		#endregion

		#region Repair Timer

		/// <summary>
		/// Timer class for repair actions with delay, sound, and visual feedback
		/// </summary>
		private class RepairTimer : Timer
		{
			private Mobile m_From;
			private Item m_Item;
			private SkillName m_Skill;
			private int m_ToWeaken;
			private bool m_UsingDeed;
			private CraftSystem m_CraftSystem;
			private BaseTool m_Tool;
			private RepairDeed m_Deed;
			private int m_Count;
			private int m_CountMax;

			/// <summary>
			/// Initializes a new instance of the RepairTimer class
			/// </summary>
			/// <param name="from">The mobile performing the repair</param>
			/// <param name="item">The item to repair</param>
			/// <param name="skill">The skill being used</param>
			/// <param name="toWeaken">The amount to weaken if repair fails</param>
			/// <param name="usingDeed">Whether a repair deed is being used</param>
			/// <param name="craftSystem">The crafting system</param>
			/// <param name="tool">The crafting tool</param>
			/// <param name="deed">The repair deed (if using deed)</param>
			public RepairTimer( Mobile from, Item item, SkillName skill, int toWeaken, bool usingDeed, CraftSystem craftSystem, BaseTool tool, RepairDeed deed )
				: base( TimeSpan.Zero, TimeSpan.FromSeconds( RepairConstants.REPAIR_DELAY_SECONDS ), RepairConstants.REPAIR_EFFECT_COUNT )
			{
				m_From = from;
				m_Item = item;
				m_Skill = skill;
				m_ToWeaken = toWeaken;
				m_UsingDeed = usingDeed;
				m_CraftSystem = craftSystem;
				m_Tool = tool;
				m_Deed = deed;
				m_Count = 0;
				m_CountMax = RepairConstants.REPAIR_EFFECT_COUNT;
			}

			/// <summary>
			/// Called on each timer tick
			/// </summary>
			protected override void OnTick()
			{
				m_Count++;

				// Check if item or mobile is still valid
				if ( m_From == null || m_From.Deleted || m_Item == null || m_Item.Deleted )
				{
					Stop();
					return;
				}

				m_From.DisruptiveAction();

				if ( m_Count < m_CountMax )
				{
					// Play sound effect during delay
					m_From.PlaySound( RepairConstants.SOUND_REPAIR );
				}
				else
				{
					// Delay complete - perform actual repair
					Stop();

					// Get reference to InternalTarget to call PerformRepair
					// We need to access the private method, so we'll use reflection or make it accessible
					// For now, let's create a helper method in InternalTarget that we can call
					
					// Actually, we need to restructure this - let's make PerformRepair accessible
					// For simplicity, we'll duplicate the repair logic here or make it a static helper
					
					// Perform the repair
					bool toDelete = false;
					int number = PerformRepairLogic( m_From, m_Item, m_Skill, m_ToWeaken, m_UsingDeed, ref toDelete );

					// Send result to player
					if ( !m_UsingDeed )
					{
						CraftContext context = m_CraftSystem.GetContext( m_From );
						m_From.SendGump( new CraftGump( m_From, m_CraftSystem, m_Tool, number ) );
					}
					else if ( toDelete )
					{
						m_From.SendLocalizedMessage( number );
						if ( m_Deed != null && !m_Deed.Deleted )
							m_Deed.Delete();
					}
				}
			}

			/// <summary>
			/// Performs the repair logic (extracted from PerformRepair for timer use)
			/// </summary>
			private int PerformRepairLogic( Mobile from, Item item, SkillName skill, int toWeaken, bool usingDeed, ref bool toDelete )
			{
				// Get item hit points
				int maxHits = 0;
				int curHits = 0;

				if ( item is BaseJewel )
				{
					BaseJewel jewel = (BaseJewel)item;
					maxHits = jewel.MaxHitPoints;
					curHits = jewel.HitPoints;
				}
				else if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;
					maxHits = weapon.MaxHitPoints;
					curHits = weapon.HitPoints;
				}
				else if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;
					maxHits = armor.MaxHitPoints;
					curHits = armor.HitPoints;
				}
				else if ( item is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)item;
					maxHits = clothing.MaxHitPoints;
					curHits = clothing.HitPoints;
				}

				// Re-validate
				if ( item.Deleted || !item.IsChildOf( from.Backpack ) )
				{
					return RepairConstants.MSG_MUST_BE_IN_BACKPACK;
				}

				if ( maxHits <= 0 || curHits == maxHits )
				{
					return RepairConstants.MSG_FULL_REPAIR;
				}

				// Calculate weaken chance
				double skillValue = ( m_Deed != null ) ? m_Deed.SkillLevel : from.Skills[skill].Value;
				
				// At 100+ skill, 50% chance to completely bypass weakening
				bool shouldWeaken = false;
				if ( skillValue >= RepairConstants.SKILL_NO_REDUCTION )
				{
					// 50% chance to skip weakening entirely
					if ( Utility.Random( 2 ) != 0 ) // If not bypassed (50% chance)
					{
						int weakenChance = RepairConstants.WEAKEN_BASE_CHANCE + (maxHits - curHits) - (int)(skillValue / RepairConstants.WEAKEN_SKILL_DIVISOR);
						shouldWeaken = weakenChance > Utility.Random( RepairConstants.WEAKEN_RANDOM_MAX );
					}
				}
				else
				{
					int weakenChance = RepairConstants.WEAKEN_BASE_CHANCE + (maxHits - curHits) - (int)(skillValue / RepairConstants.WEAKEN_SKILL_DIVISOR);
					shouldWeaken = weakenChance > Utility.Random( RepairConstants.WEAKEN_RANDOM_MAX );
				}

				// Attempt repair
				if ( shouldWeaken )
				{
					// Check if reduction is zero (no actual reduction)
					if ( toWeaken == 0 )
					{
						// No durability reduction - send message and play woohoo emote
						from.SendMessage( CraftGumpStringConstants.NOTICE_NO_DURABILITY_REDUCTION );
						from.PlaySound( from.Female ? 783 : 1054 );
						from.Say( "*woohoo!*" );
					}
					else
					{
						// Durability was reduced - send message with amount
						from.SendMessage( String.Format( CraftGumpStringConstants.NOTICE_DURABILITY_REDUCED, toWeaken ) );
					}

					maxHits -= toWeaken;
					curHits = Math.Max( 0, curHits - toWeaken );

					// Update item hit points
					if ( item is BaseJewel )
					{
						BaseJewel jewel = (BaseJewel)item;
						jewel.MaxHitPoints = maxHits;
						jewel.HitPoints = curHits;
					}
					else if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;
						weapon.MaxHitPoints = maxHits;
						weapon.HitPoints = curHits;
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;
						armor.MaxHitPoints = maxHits;
						armor.HitPoints = curHits;
					}
					else if ( item is BaseClothing )
					{
						BaseClothing clothing = (BaseClothing)item;
						clothing.MaxHitPoints = maxHits;
						clothing.HitPoints = curHits;
					}
				}

				// Check repair success
				double difficulty = (((maxHits - curHits) * RepairConstants.DIFFICULTY_MULTIPLIER) / Math.Max( maxHits, 1 )) - RepairConstants.DIFFICULTY_SUBTRACTOR;
				difficulty *= RepairConstants.DIFFICULTY_SCALAR;

				bool repairSuccess = false;
				if ( m_Deed != null )
				{
					double value = m_Deed.SkillLevel;
					double minSkill = difficulty - RepairConstants.DIFFICULTY_VARIANCE;
					double maxSkill = difficulty + RepairConstants.DIFFICULTY_VARIANCE;

					if ( value < minSkill )
						repairSuccess = false;
					else if ( value >= maxSkill )
						repairSuccess = true;
					else
					{
						double chance = (value - minSkill) / (maxSkill - minSkill);
						repairSuccess = (chance >= Utility.RandomDouble());
					}
				}
				else
				{
					repairSuccess = from.CheckSkill( skill, difficulty - RepairConstants.DIFFICULTY_VARIANCE, difficulty + RepairConstants.DIFFICULTY_VARIANCE );
				}

				if ( repairSuccess )
				{
					// Repair successful - restore to full
					if ( item is BaseJewel )
					{
						BaseJewel jewel = (BaseJewel)item;
						jewel.HitPoints = jewel.MaxHitPoints;
					}
					else if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;
						weapon.HitPoints = weapon.MaxHitPoints;
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;
						armor.HitPoints = armor.MaxHitPoints;
					}
					else if ( item is BaseClothing )
					{
						BaseClothing clothing = (BaseClothing)item;
						clothing.HitPoints = clothing.MaxHitPoints;
					}

					m_CraftSystem.PlayCraftEffect( from );
					toDelete = true;
					return RepairConstants.MSG_REPAIR_SUCCESS;
				}
				else
				{
					// Repair failed
					m_CraftSystem.PlayCraftEffect( from );
					toDelete = true;
					return usingDeed ? RepairConstants.MSG_REPAIR_FAILURE_DEED : RepairConstants.MSG_REPAIR_FAILURE;
				}
			}
		}

		#endregion
	}
}