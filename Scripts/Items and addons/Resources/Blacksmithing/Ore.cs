using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseOre : Item, ICommodity
	{
        public override double DefaultWeight
        {
            get { return 4.0; }
        }

        private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		public abstract BaseIngot GetIngot();

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case 0:
				{
					OreInfo info;

					switch ( reader.ReadInt() )
					{
						case 0: info = OreInfo.Iron; break;
						case 1: info = OreInfo.DullCopper; break;
						case 2: info = OreInfo.Copper; break;
						case 3: info = OreInfo.Bronze; break;
                        case 4: info = OreInfo.ShadowIron; break;
                        case 5: info = OreInfo.Platinum; break;
                        case 6: info = OreInfo.Gold; break;
						case 7: info = OreInfo.Agapite; break;
						case 8: info = OreInfo.Verite; break;
						case 9: info = OreInfo.Valorite; break;
                        case 10: info = OreInfo.Titanium; break;
                        case 11: info = OreInfo.Rosenium; break;
                        case 12: info = OreInfo.Nepturite; break;
						case 13: info = OreInfo.Obsidian; break;
						case 14: info = OreInfo.Mithril; break;
						case 15: info = OreInfo.Xormite; break;
						case 16: info = OreInfo.Dwarven; break;

						default: info = null; break;
					}

					m_Resource = CraftResources.GetFromOreInfo( info );
					break;
				}
			}
		}

		public BaseOre( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseOre( CraftResource resource, int amount ) : base( OreConstants.ITEM_ID_STANDARD_PILE )
		{
			Stackable = true;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseOre( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Gets the display name for this ore type.
		/// Can be overridden by derived classes to provide custom names.
		/// </summary>
		/// <returns>The display name for this ore</returns>
		public virtual string GetOreDisplayName()
		{
			return OreNameConstants.GENERIC_ORE_LABEL;
		}

		/// <summary>
		/// Gets the resource type display name for the tooltip properties.
		/// Maps CraftResource enum values to PT-BR display names from OreNameConstants.
		/// </summary>
		/// <returns>The resource type display name, or null if not found</returns>
		public virtual string GetResourceTypeDisplayName()
		{
			switch ( m_Resource )
			{
				case CraftResource.Iron: return OreNameConstants.IRON_DISPLAY_NAME;
				case CraftResource.DullCopper: return OreNameConstants.DULL_COPPER_DISPLAY_NAME;
				case CraftResource.ShadowIron: return OreNameConstants.SHADOW_IRON_DISPLAY_NAME;
				case CraftResource.Copper: return OreNameConstants.COPPER_DISPLAY_NAME;
				case CraftResource.Bronze: return OreNameConstants.BRONZE_DISPLAY_NAME;
				case CraftResource.Gold: return OreNameConstants.GOLD_DISPLAY_NAME;
				case CraftResource.Agapite: return OreNameConstants.AGAPITE_DISPLAY_NAME;
				case CraftResource.Verite: return OreNameConstants.VERITE_DISPLAY_NAME;
				case CraftResource.Valorite: return OreNameConstants.VALORITE_DISPLAY_NAME;
				case CraftResource.Titanium: return OreNameConstants.TITANIUM_DISPLAY_NAME;
				case CraftResource.Rosenium: return OreNameConstants.ROSENIUM_DISPLAY_NAME;
				case CraftResource.Platinum: return OreNameConstants.PLATINUM_DISPLAY_NAME;
				case CraftResource.Nepturite: return OreNameConstants.NEPTURITE_DISPLAY_NAME;
				case CraftResource.Obsidian: return OreNameConstants.OBSIDIAN_DISPLAY_NAME;
				case CraftResource.Mithril: return OreNameConstants.MITHRIL_DISPLAY_NAME;
				case CraftResource.Xormite: return OreNameConstants.XORMITE_DISPLAY_NAME;
				case CraftResource.Dwarven: return OreNameConstants.DWARVEN_DISPLAY_NAME;
				default: return null;
			}
		}

		/// <summary>
		/// Adds the name property to the object property list.
		/// Uses GetOreDisplayName() to get the custom name if available.
		/// </summary>
		/// <param name="list">The object property list to add to</param>
		public override void AddNameProperty( ObjectPropertyList list )
		{
			string displayName = GetOreDisplayName();
			
			if ( Amount > 1 )
				list.Add( OreConstants.MSG_ID_MULTIPLE_ITEMS_FORMAT, "{0}\t{1}", Amount, displayName );
			else
				list.Add( displayName );
		}

		/// <summary>
		/// Adds properties to the object property list (tooltip).
		/// Shows the resource type using custom PT-BR names (including Iron).
		/// </summary>
		/// <param name="list">The object property list to add to</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			// Try to get custom PT-BR display name first (includes Iron)
			string customName = GetResourceTypeDisplayName();
			
			if ( customName != null )
			{
				list.Add( 1053099, ItemNameHue.UnifiedItemProps.SetColor( customName, OreStringConstants.COLOR_CYAN ) );
			}
			else if ( !CraftResources.IsStandard( m_Resource ) )
			{
				// Fallback to original system if custom name not found (only for non-standard resources)
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
				{
					string resourceName = CraftResources.GetName( m_Resource );
					if ( !string.IsNullOrEmpty( resourceName ) )
						list.Add( resourceName );
				}
			}
		}

		public override int LabelNumber
		{
			get
			{
				if (m_Resource >= CraftResource.DullCopper && m_Resource <= CraftResource.Valorite)
					return 1042845 + (int)(m_Resource - CraftResource.DullCopper);
				//else if (m_Resource == CraftResource.Titanium)
					//return 6661002;

                return 1042853; // iron ore;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;
			
			if ( RootParent is BaseCreature )
			{
				from.SendLocalizedMessage( 500447 ); // That is not accessible
				return;
			}
			else if ( from.InRange( this.GetWorldLocation(), OreConstants.INTERACTION_RANGE ) )
			{
				from.SendLocalizedMessage( OreConstants.MSG_ID_SELECT_FORGE );
				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( OreConstants.MSG_ID_ORE_TOO_FAR );
			}
		}

		#region Helper Methods

		/// <summary>
		/// Calculates the worth of an ore pile based on its amount and item ID
		/// </summary>
		/// <param name="ore">The ore pile to calculate worth for</param>
		/// <returns>The calculated worth value</returns>
		private static int CalculateOreWorth( BaseOre ore )
		{
			int worth = ore.Amount;
			
			if ( ore.ItemID == OreConstants.ITEM_ID_STANDARD_PILE )
				worth *= OreConstants.WORTH_MULTIPLIER_STANDARD_PILE;
			else if ( ore.ItemID == OreConstants.ITEM_ID_SMALL_PILE )
				worth *= OreConstants.WORTH_MULTIPLIER_SMALL_PILE;
			else 
				worth *= OreConstants.WORTH_MULTIPLIER_MEDIUM_LARGE_PILE;
			
			return worth;
		}

		/// <summary>
		/// Determines the new pile ID and additional weight when combining ores with different weights
		/// </summary>
		/// <param name="ore1">First ore pile</param>
		/// <param name="ore2">Second ore pile</param>
		/// <param name="plusWeight">Output parameter for additional weight</param>
		/// <returns>The new item ID for the combined pile</returns>
		private static int DetermineCombinedPileID( BaseOre ore1, BaseOre ore2, out int plusWeight )
		{
			plusWeight = 0;
			int newID = ore2.ItemID;
			
			if ( ore1.DefaultWeight != ore2.DefaultWeight )
			{
				if ( ore1.ItemID == OreConstants.ITEM_ID_SMALL_PILE || ore2.ItemID == OreConstants.ITEM_ID_SMALL_PILE )
				{
					newID = OreConstants.ITEM_ID_SMALL_PILE;
				}
				else if ( ore2.ItemID == OreConstants.ITEM_ID_STANDARD_PILE )
				{
					newID = ore1.ItemID;
					plusWeight = ore2.Amount * 2;
				}
				else
				{
					plusWeight = ore1.Amount * 2;
				}
			}
			
			return newID;
		}

		/// <summary>
		/// Validates if two ore piles can be combined
		/// </summary>
		/// <param name="from">The mobile attempting to combine</param>
		/// <param name="ore1">First ore pile</param>
		/// <param name="ore2">Second ore pile</param>
		/// <param name="totalWorth">Total worth of combined ores</param>
		/// <param name="plusWeight">Additional weight from combination</param>
		/// <returns>True if combination is valid, false otherwise</returns>
		private static bool ValidateOreCombination( Mobile from, BaseOre ore1, BaseOre ore2, int totalWorth, int plusWeight )
		{
			// Check maximum worth limits
			if ( (ore2.ItemID == OreConstants.ITEM_ID_STANDARD_PILE && totalWorth > OreConstants.MAX_WORTH_STANDARD_PILE) || 
			     (( ore2.ItemID == OreConstants.ITEM_ID_MEDIUM_PILE || ore2.ItemID == OreConstants.ITEM_ID_LARGE_PILE ) && totalWorth > OreConstants.MAX_WORTH_MEDIUM_LARGE_PILE) || 
			     (ore2.ItemID == OreConstants.ITEM_ID_SMALL_PILE && totalWorth > OreConstants.MAX_WORTH_SMALL_PILE))
			{
				from.SendLocalizedMessage( OreConstants.MSG_ID_TOO_MUCH_ORE );
				return false;
			}
			
			// Check weight limit if in backpack
			if ( ore2.RootParent is Mobile )
			{
				Mobile owner = (Mobile)ore2.RootParent;
				if ( (plusWeight + owner.Backpack.TotalWeight) > owner.Backpack.MaxWeight )
				{ 
					from.SendLocalizedMessage( OreConstants.MSG_ID_WEIGHT_TOO_GREAT );
					return false;
				}
			}
			
			return true;
		}

		/// <summary>
		/// Combines two ore piles into one
		/// </summary>
		/// <param name="ore1">Source ore pile (will be deleted)</param>
		/// <param name="ore2">Target ore pile (will be updated)</param>
		/// <param name="totalWorth">Total worth of combined ores</param>
		/// <param name="newID">New item ID for the combined pile</param>
		private static void CombineOrePiles( BaseOre ore1, BaseOre ore2, int totalWorth, int newID )
		{
			ore2.ItemID = newID;
			
			if ( ore2.ItemID == OreConstants.ITEM_ID_STANDARD_PILE )
			{
				ore2.Amount = totalWorth / OreConstants.WORTH_MULTIPLIER_STANDARD_PILE;
				ore1.Delete();
			}
			else if ( ore2.ItemID == OreConstants.ITEM_ID_SMALL_PILE )
			{
				ore2.Amount = totalWorth / OreConstants.WORTH_MULTIPLIER_SMALL_PILE;
				ore1.Delete();
			}
			else
			{
				ore2.Amount = totalWorth / OreConstants.WORTH_MULTIPLIER_MEDIUM_LARGE_PILE;
				ore1.Delete();
			}
		}

		/// <summary>
		/// Gets the smelting difficulty for a given resource type
		/// </summary>
		/// <param name="resource">The craft resource type</param>
		/// <returns>The smelting difficulty value</returns>
		private static double GetSmeltingDifficulty( CraftResource resource )
		{
			switch ( resource )
			{
				case CraftResource.DullCopper: return OreConstants.SMELT_DIFFICULTY_DULL_COPPER;
				case CraftResource.Copper: return OreConstants.SMELT_DIFFICULTY_COPPER;
				case CraftResource.Bronze: return OreConstants.SMELT_DIFFICULTY_BRONZE;
				case CraftResource.ShadowIron: return OreConstants.SMELT_DIFFICULTY_SHADOW_IRON;
				case CraftResource.Platinum: return OreConstants.SMELT_DIFFICULTY_PLATINUM;
				case CraftResource.Gold: return OreConstants.SMELT_DIFFICULTY_GOLD;
				case CraftResource.Agapite: return OreConstants.SMELT_DIFFICULTY_AGAPITE;
				case CraftResource.Verite: return OreConstants.SMELT_DIFFICULTY_VERITE;
				case CraftResource.Valorite: return OreConstants.SMELT_DIFFICULTY_VALORITE;
				case CraftResource.Titanium: return OreConstants.SMELT_DIFFICULTY_TITANIUM;
				case CraftResource.Rosenium: return OreConstants.SMELT_DIFFICULTY_ROSENIUM;
				case CraftResource.Nepturite: return OreConstants.SMELT_DIFFICULTY_NEPTURITE;
				case CraftResource.Obsidian: return OreConstants.SMELT_DIFFICULTY_OBSIDIAN;
				case CraftResource.Mithril: return OreConstants.SMELT_DIFFICULTY_MITHRIL;
				case CraftResource.Xormite: return OreConstants.SMELT_DIFFICULTY_XORMITE;
				case CraftResource.Dwarven: return OreConstants.SMELT_DIFFICULTY_DWARVEN;
				default: return OreConstants.SMELT_DIFFICULTY_DEFAULT;
			}
		}

		/// <summary>
		/// Validates if the player can smelt the ore
		/// </summary>
		/// <param name="from">The mobile attempting to smelt</param>
		/// <param name="ore">The ore to smelt</param>
		/// <param name="difficulty">The smelting difficulty</param>
		/// <returns>True if smelting can proceed, false otherwise</returns>
		private static bool ValidateSmeltingConditions( Mobile from, BaseOre ore, double difficulty )
		{
			// Check skill requirement
			if ( difficulty > OreConstants.SMELT_MIN_SKILL_THRESHOLD && difficulty > from.Skills[SkillName.Mining].Value )
			{
				from.SendMessage(55, OreStringConstants.MSG_CANNOT_SMELT_ORE);
				return false;
			}
			
			// Check minimum amount for small pile
			if ( ore.Amount <= 1 && ore.ItemID == OreConstants.ITEM_ID_SMALL_PILE )
			{
				from.SendMessage(55, OreStringConstants.MSG_NOT_ENOUGH_ORE_FOR_INGOT);
				return false;
			}
			
			return true;
		}

		/// <summary>
		/// Calculates the amount of ingots to create based on ore pile type
		/// </summary>
		/// <param name="ore">The ore pile to smelt</param>
		/// <param name="baseAmount">Base amount of ore</param>
		/// <returns>The calculated ingot amount</returns>
		private static int CalculateSmeltAmount( BaseOre ore, int baseAmount )
		{
			if ( ore.ItemID == OreConstants.ITEM_ID_SMALL_PILE )
			{
				if ( ore.Amount % 2 == 0 )
				{
					baseAmount /= 2;
					ore.Delete();
				}
				else
				{
					baseAmount /= 2;
					ore.Amount = 1;
				}
			}
			else if ( ore.ItemID == OreConstants.ITEM_ID_STANDARD_PILE )
			{
				baseAmount *= 2;
				ore.Delete();
			}
			else
			{
				baseAmount /= 1;
				ore.Delete();
			}
			
			return baseAmount;
		}

		/// <summary>
		/// Processes successful smelting operation
		/// </summary>
		/// <param name="from">The mobile who smelted</param>
		/// <param name="ore">The ore that was smelted</param>
		private static void ProcessSuccessfulSmelt( Mobile from, BaseOre ore )
		{
			if ( ore.Amount <= 0 )
			{
				from.SendMessage(55, OreStringConstants.MSG_NOT_ENOUGH_ORE_FOR_INGOT);
				return;
			}
			
			int amount = ore.Amount;
			if ( amount > OreConstants.MAX_SMELT_AMOUNT )
				amount = OreConstants.MAX_SMELT_AMOUNT;

			BaseIngot ingot = ore.GetIngot();
			ingot.Amount = CalculateSmeltAmount( ore, amount );
			
			from.AddToBackpack( ingot );
			from.PlaySound( OreConstants.SOUND_ID_SMELT );
			from.SendMessage(55, OreStringConstants.MSG_SMELTED_ORE_SUCCESS);
		}

		/// <summary>
		/// Processes failed smelting attempt (burns away impurities)
		/// </summary>
		/// <param name="from">The mobile who attempted to smelt</param>
		/// <param name="ore">The ore that failed to smelt</param>
		private static void ProcessFailedSmelt( Mobile from, BaseOre ore )
		{
			if ( ore.Amount < 2 && ore.ItemID == OreConstants.ITEM_ID_STANDARD_PILE )
			{
				from.SendLocalizedMessage( OreConstants.MSG_ID_BURN_IMPURITIES );
				ore.ItemID = OreConstants.ITEM_ID_MEDIUM_PILE;
				from.PlaySound( OreConstants.SOUND_ID_SMELT );
			}
			else if ( ore.Amount < 2 && (ore.ItemID == OreConstants.ITEM_ID_MEDIUM_PILE || ore.ItemID == OreConstants.ITEM_ID_LARGE_PILE) )
			{
				from.SendLocalizedMessage( OreConstants.MSG_ID_BURN_IMPURITIES );
				ore.ItemID = OreConstants.ITEM_ID_SMALL_PILE;
				from.PlaySound( OreConstants.SOUND_ID_SMELT );
			}
			else
			{
				from.SendLocalizedMessage( OreConstants.MSG_ID_BURN_IMPURITIES );
				ore.Amount /= 2;
				from.PlaySound( OreConstants.SOUND_ID_SMELT );
			}
		}

		#endregion

		private class InternalTarget : Target
		{
			private BaseOre m_Ore;

			public InternalTarget( BaseOre ore ) : base( OreConstants.TARGET_RANGE, false, TargetFlags.None )
			{
				m_Ore = ore;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Ore.Deleted )
					return;

				if ( !from.InRange( m_Ore.GetWorldLocation(), OreConstants.INTERACTION_RANGE ) )
				{
					from.SendLocalizedMessage( OreConstants.MSG_ID_ORE_TOO_FAR );
					return;
				}
				
				// Handle ore combining
				if ( targeted is BaseOre )
				{
					HandleOreCombining( from, (BaseOre)targeted );
					return;
				}

				// Handle smelting
				if ( Server.Engines.Craft.DefBlacksmithy.IsForge( targeted ) )
				{
					HandleSmelting( from, targeted );
				}
			}

			/// <summary>
			/// Handles combining two ore piles
			/// </summary>
			/// <param name="from">The mobile attempting to combine</param>
			/// <param name="targetOre">The target ore pile to combine with</param>
			private void HandleOreCombining( Mobile from, BaseOre targetOre )
			{
				if ( !targetOre.Movable )
					return;
					
				if ( m_Ore == targetOre )
				{
					from.SendLocalizedMessage( OreConstants.MSG_ID_SELECT_ANOTHER_PILE );
					from.Target = new InternalTarget( targetOre );
					return;
				}
				
				if ( targetOre.Resource != m_Ore.Resource )
				{
					from.SendLocalizedMessage( OreConstants.MSG_ID_CANNOT_COMBINE_DIFFERENT );
					return;
				}

				int totalWorth = CalculateOreWorth( targetOre ) + CalculateOreWorth( m_Ore );
				int plusWeight;
				int newID = DetermineCombinedPileID( m_Ore, targetOre, out plusWeight );

				if ( !ValidateOreCombination( from, m_Ore, targetOre, totalWorth, plusWeight ) )
					return;

				CombineOrePiles( m_Ore, targetOre, totalWorth, newID );
			}

			/// <summary>
			/// Handles smelting ore at a forge
			/// </summary>
			/// <param name="from">The mobile attempting to smelt</param>
			/// <param name="forge">The forge target</param>
			private void HandleSmelting( Mobile from, object forge )
			{
				// Check if ore is gated (unknown metal)
				if ( Server.Misc.ResourceGating.IsResourceGated( m_Ore.Resource ) )
				{
					from.SendMessage(55, Server.Misc.ResourceGating.MSG_CANNOT_SMELT_GATED_ORE);
					return;
				}

				double difficulty = GetSmeltingDifficulty( m_Ore.Resource );
				double minSkill = difficulty - OreConstants.SMELT_SKILL_VARIANCE;
				double maxSkill = difficulty + OreConstants.SMELT_SKILL_VARIANCE;

				if ( !ValidateSmeltingConditions( from, m_Ore, difficulty ) )
					return;

				if ( from.CheckTargetSkill( SkillName.Mining, forge, minSkill, maxSkill ) )
				{
					ProcessSuccessfulSmelt( from, m_Ore );
				}
				else
				{
					ProcessFailedSmelt( from, m_Ore );
				}
			}
		}
	}

	public class IronOre : BaseOre
	{
		[Constructable]
		public IronOre() : this( 1 )
		{
		}

		[Constructable]
		public IronOre( int amount ) : base( CraftResource.Iron, amount )
		{
		}

		public IronOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_STANDARD; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new IronIngot();
		}
	}

	public class DullCopperOre : BaseOre
	{
		[Constructable]
		public DullCopperOre() : this( 1 )
		{
		}

		[Constructable]
		public DullCopperOre( int amount ) : base( CraftResource.DullCopper, amount )
		{
		}

		public DullCopperOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_COPPER_BASED; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new DullCopperIngot();
		}
	}

	public class ShadowIronOre : BaseOre
	{
		[Constructable]
		public ShadowIronOre() : this( 1 )
		{
		}

		[Constructable]
		public ShadowIronOre( int amount ) : base( CraftResource.ShadowIron, amount )
		{
		}

		public ShadowIronOre( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Gets the custom display name for ShadowIron ore (Ferro-Negro in PT-BR)
		/// </summary>
		/// <returns>The PT-BR display name "Ferro-Negro"</returns>
		//public override string GetOreDisplayName()
		//{
			//return OreNameConstants.SHADOW_IRON_DISPLAY_NAME;
		//}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_STANDARD; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new ShadowIronIngot();
		}
	}

	public class CopperOre : BaseOre
	{
		[Constructable]
		public CopperOre() : this( 1 )
		{
		}

		[Constructable]
		public CopperOre( int amount ) : base( CraftResource.Copper, amount )
		{
		}

		public CopperOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_COPPER_BASED; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new CopperIngot();
		}
	}

	public class BronzeOre : BaseOre
	{
		[Constructable]
		public BronzeOre() : this( 1 )
		{
		}

		[Constructable]
		public BronzeOre( int amount ) : base( CraftResource.Bronze, amount )
		{
		}

		public BronzeOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return 4.5; } // Density of real bronze (~9g/cm³) / 2;
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new BronzeIngot();
		}
	}

	public class GoldOre : BaseOre
	{
		[Constructable]
		public GoldOre() : this( 1 )
		{
		}

		[Constructable]
		public GoldOre( int amount ) : base( CraftResource.Gold, amount )
		{
		}

		public GoldOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_GOLD; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new GoldIngot();
		}
	}

	public class AgapiteOre : BaseOre
	{
		[Constructable]
		public AgapiteOre() : this( 1 )
		{
		}

		[Constructable]
		public AgapiteOre( int amount ) : base( CraftResource.Agapite, amount )
		{
		}

		public AgapiteOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return 4; } // Density of real iron (~8g/cm³) / 2;
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new AgapiteIngot();
		}
	}

	public class VeriteOre : BaseOre
	{
		[Constructable]
		public VeriteOre() : this( 1 )
		{
		}

		[Constructable]
		public VeriteOre( int amount ) : base( CraftResource.Verite, amount )
		{
		}

		public VeriteOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return 4; } // Density of real iron (~8g/cm³) / 2;
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new VeriteIngot();
		}
	}

	public class ValoriteOre : BaseOre
	{
		[Constructable]
		public ValoriteOre() : this( 1 )
		{

		}

		[Constructable]
		public ValoriteOre( int amount ) : base( CraftResource.Valorite, amount )
		{
		}

		public ValoriteOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return 4; } // Density of real iron (~8g/cm³) / 2;
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new ValoriteIngot();
		}
	}

    public class TitaniumOre : BaseOre
    {
        public override int Hue { get { return (Server.Misc.MaterialInfo.GetMaterialColor("titanium", "classic", 0)); } }
        //protected override CraftResource DefaultResource { get { return CraftResource.Titanium;  } }

        [Constructable]
        public TitaniumOre() : this(1)
        {
        }

        [Constructable]
        public TitaniumOre(int amount) : base(CraftResource.Titanium, amount)
        {
        }

        public TitaniumOre(Serial serial) : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_TITANIUM_ROSENIUM; }
        }

        public virtual int GetLabelNumber()
        {
            return 6661002;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new TitaniumIngot();
        }
    }

    public class RoseniumOre : BaseOre
    {
        public override int Hue { get { return (Server.Misc.MaterialInfo.GetMaterialColor("rosenium", "classic", 0)); } }
        //protected override CraftResource DefaultResource { get { return CraftResource.Titanium;  } }
        [Constructable]
        public RoseniumOre() : this(1)
        {
        }

        [Constructable]
        public RoseniumOre(int amount) : base(CraftResource.Rosenium, amount)
        {
        }

        public RoseniumOre(Serial serial) : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_TITANIUM_ROSENIUM; }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new RoseniumIngot();
        }
    }

    public class PlatinumOre : BaseOre
    {
        public override int Hue { get { return (Server.Misc.MaterialInfo.GetMaterialColor("platinum", "classic", 0)); } }
        //protected override CraftResource DefaultResource { get { return CraftResource.Titanium;  } }
        [Constructable]
        public PlatinumOre() : this(1)
        {
        }

        [Constructable]
        public PlatinumOre(int amount) : base(CraftResource.Platinum, amount)
        {
        }

        public PlatinumOre(Serial serial) : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_PLATINUM; }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new PlatinumIngot();
        }
    }

    public class ObsidianOre : BaseOre
	{
		[Constructable]
		public ObsidianOre() : this( 1 )
		{
		}

		[Constructable]
		public ObsidianOre( int amount ) : base( CraftResource.Obsidian, amount )
		{
		}

		public ObsidianOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_OBSIDIAN; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new ObsidianIngot();
		}
	}

	public class MithrilOre : BaseOre
	{
		[Constructable]
		public MithrilOre() : this( 1 )
		{
		}

		[Constructable]
		public MithrilOre( int amount ) : base( CraftResource.Mithril, amount )
		{
		}

		public MithrilOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_MITHRIL; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new MithrilIngot();
		}
	}

	public class DwarvenOre : BaseOre
	{
		[Constructable]
		public DwarvenOre() : this( 1 )
		{
		}

		[Constructable]
		public DwarvenOre( int amount ) : base( CraftResource.Dwarven, amount )
		{
		}

		public DwarvenOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_DWARVEN; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new DwarvenIngot();
		}
	}

	public class XormiteOre : BaseOre
	{
		[Constructable]
		public XormiteOre() : this( 1 )
		{
		}

		[Constructable]
		public XormiteOre( int amount ) : base( CraftResource.Xormite, amount )
		{
		}

		public XormiteOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_XORMITE; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new XormiteIngot();
		}
	}

	public class NepturiteOre : BaseOre
	{
		[Constructable]
		public NepturiteOre() : this( 1 )
		{
		}

		[Constructable]
		public NepturiteOre( int amount ) : base( CraftResource.Nepturite, amount )
		{
		}

		public NepturiteOre( Serial serial ) : base( serial )
		{
		}

        public override double DefaultWeight
        {
            get { return OreConstants.DENSITY_NEPTURITE; }
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new NepturiteIngot();
		}
	}
}