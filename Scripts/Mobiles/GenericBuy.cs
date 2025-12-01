using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles.Vendors;

namespace Server.Mobiles
{
	public class GenericBuyInfo : IBuyItemInfo
	{
		private class DisplayCache : Container
		{
			private static DisplayCache m_Cache;

			public static DisplayCache Cache {
				get {
					if ( m_Cache == null || m_Cache.Deleted )
						m_Cache = new DisplayCache();

					return m_Cache;
				}
			}

			private Dictionary<Type, IEntity> m_Table;
			private List<Mobile> m_Mobiles;

			public DisplayCache() : base( 0 )
			{
				m_Table = new Dictionary<Type, IEntity>();
				m_Mobiles = new List<Mobile>();
			}

			public IEntity Lookup( Type key )
			{
				IEntity e = null;
				m_Table.TryGetValue( key, out e );
				return e;
			}

			public void Store( Type key, IEntity obj, bool cache )
			{
				if ( cache )
					m_Table[key] = obj;

				if ( obj is Item )
					AddItem( (Item)obj );
				else if ( obj is Mobile )
					m_Mobiles.Add( (Mobile)obj );
			}

			public DisplayCache( Serial serial ) : base( serial )
			{
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				for ( int i = 0; i < m_Mobiles.Count; ++i )
					m_Mobiles[i].Delete();

				m_Mobiles.Clear();

				for ( int i = Items.Count - 1; i >= 0; --i )
					if ( i < Items.Count )
						Items[i].Delete();

				if ( m_Cache == this )
					m_Cache = null;
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version

				writer.Write( m_Mobiles );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				m_Mobiles = reader.ReadStrongMobileList();

				for ( int i = 0; i < m_Mobiles.Count; ++i )
					m_Mobiles[i].Delete();

				m_Mobiles.Clear();

				for ( int i = Items.Count - 1; i >= 0; --i )
					if ( i < Items.Count )
						Items[i].Delete();

				if ( m_Cache == null )
					m_Cache = this;
				else
					Delete();

				m_Table = new Dictionary<Type, IEntity>();
			}
		}

		private Type m_Type;
		private string m_Name;
		private int m_Price;
		private int m_MaxAmount, m_Amount;
		private int m_ItemID;
		private int m_Hue;
		private object[] m_Args;
		private IEntity m_DisplayEntity;

		public virtual int ControlSlots{ get{ return 0; } }

		public virtual bool CanCacheDisplay{ get{ return false; } } //return ( m_Args == null || m_Args.Length == 0 ); } 

		private bool IsDeleted( IEntity obj )
		{
			if ( obj is Item )
				return ((Item)obj).Deleted;
			else if ( obj is Mobile )
				return ((Mobile)obj).Deleted;

			return false;
		}

		public void DeleteDisplayEntity()
		{
			if ( m_DisplayEntity == null )
				return;

			m_DisplayEntity.Delete();
			m_DisplayEntity = null;
		}

		public IEntity GetDisplayEntity()
		{
			if ( m_DisplayEntity != null && !IsDeleted( m_DisplayEntity ) )
			{
				// Update name if we have a custom name and the entity is an Item
				if ( m_DisplayEntity is Item )
				{
					Item item = (Item)m_DisplayEntity;
					string displayName = GetDisplayName( item );
					if ( !String.IsNullOrEmpty( displayName ) && item.Name != displayName )
						item.Name = displayName;
				}
				return m_DisplayEntity;
			}

			bool canCache = this.CanCacheDisplay;

			if ( canCache )
				m_DisplayEntity = DisplayCache.Cache.Lookup( m_Type );

			if ( m_DisplayEntity == null || IsDeleted( m_DisplayEntity ) )
				m_DisplayEntity = GetEntity();

			// Set custom name on display entity
			if ( m_DisplayEntity is Item )
			{
				Item item = (Item)m_DisplayEntity;
				string displayName = GetDisplayName( item );
				if ( !String.IsNullOrEmpty( displayName ) )
					item.Name = displayName;
			}

			DisplayCache.Cache.Store( m_Type, m_DisplayEntity, canCache );

			return m_DisplayEntity;
		}

		private string GetDisplayName( Item item )
		{
			// If we have a custom name, use it
			if ( !String.IsNullOrEmpty( m_Name ) && m_Name != VendorStringConstants.DEFAULT_ITEM_NAME )
				return m_Name;

			// For ingots, build name from resource type
			if ( item is BaseIngot )
			{
				BaseIngot ingot = (BaseIngot)item;
				string resourceName = CraftResources.GetName( ingot.Resource );
				
				if ( !String.IsNullOrEmpty( resourceName ) )
				{
					// Map resource names to PT-BR using centralized constants
					switch ( resourceName.ToLower() )
					{
						case "iron": return VendorStringConstants.INGOT_NAME_IRON;
						case "dull copper": return VendorStringConstants.INGOT_NAME_DULL_COPPER;
						case "copper": return VendorStringConstants.INGOT_NAME_COPPER;
						case "shadow iron": return VendorStringConstants.INGOT_NAME_SHADOW_IRON;
						case "bronze": return VendorStringConstants.INGOT_NAME_BRONZE;
						case "gold": return VendorStringConstants.INGOT_NAME_GOLD;
						case "agapite": return VendorStringConstants.INGOT_NAME_AGAPITE;
						case "verite": return VendorStringConstants.INGOT_NAME_VERITE;
						case "valorite": return VendorStringConstants.INGOT_NAME_VALORITE;
						case "rosenium": return VendorStringConstants.INGOT_NAME_ROSENIUM;
						case "titanium": return VendorStringConstants.INGOT_NAME_TITANIUM;
						case "platinum": return VendorStringConstants.INGOT_NAME_PLATINUM;
						default: return String.Format(VendorStringConstants.INGOT_NAME_FORMAT, resourceName);
					}
				}
			}

			// For items with their own name, use it (like books)
			if ( !String.IsNullOrEmpty( item.Name ) )
				return item.Name;

			return null;
		}

		public Type Type
		{
			get{ return m_Type; }
			set{ m_Type = value; }
		}

		public string Name
		{
			get{ return m_Name; }
			set{ m_Name = value; }
		}

		public int DefaultPrice{ get{ return m_PriceScalar; } }

		private int m_PriceScalar;

		public int PriceScalar
		{
			get{ return m_PriceScalar; }
			set{ m_PriceScalar = value; }
		}

		public int Price
		{
			get
			{
				// Use centralized price manager for consistent pricing
				// Note: BaseVendor will pass itself and buyer context when calling this
				// For now, we return the base price; BaseVendor will apply final pricing
				return m_Price;
			}
			set{ m_Price = value; }
		}

		public int ItemID
		{
			get{ return m_ItemID; }
			set{ m_ItemID = value; }
		}

		public int Hue
		{
			get{ return m_Hue; }
			set{ m_Hue = value; }
		}

		public int Amount
		{
			get{ return m_Amount; }
			set{ if ( value < 0 ) value = 0; m_Amount = value; }
		}

		public int MaxAmount
		{
			get{ return m_MaxAmount; }
			set{ m_MaxAmount = value; }
		}

		public object[] Args
		{
			get{ return m_Args; }
			set{ m_Args = value; }
		}

		public GenericBuyInfo( Type type, int price, int amount, int itemID, int hue ) : this( null, type, price, amount, itemID, hue, null )
		{
		}

		public GenericBuyInfo( string name, Type type, int price, int amount, int itemID, int hue ) : this( name, type, price, amount, itemID, hue, null )
		{
            m_Name = name;
        }

		public GenericBuyInfo( Type type, int price, int amount, int itemID, int hue, object[] args ) : this( null, type, price, amount, itemID, hue, args )
		{
		}

		public GenericBuyInfo( string name, Type type, int price, int amount, int itemID, int hue, object[] args )
		{
			m_Type = type;
			m_Price = price;// GetRandBuyPriceFor( price );
			m_MaxAmount = m_Amount = amount;//GetRandBuyAmountFor( amount );

            m_ItemID = itemID;
			m_Hue = hue;
			m_Args = args;

			if (name == null)
				m_Name = "indefinido";//itemID < 0x4000 ? (1020000 + itemID).ToString() : (1078872 + itemID).ToString();
			else
				m_Name = name;
		}

		//get a new instance of an object (we just bought it)
		public virtual IEntity GetEntity()
		{
			if ( m_Args == null || m_Args.Length == 0 )
				return (IEntity)Activator.CreateInstance( m_Type );

			return (IEntity)Activator.CreateInstance( m_Type, m_Args );
			//return (Item)Activator.CreateInstance( m_Type );
		}

		//Attempt to restock with item, (return true if restock sucessful)
		public bool Restock( Item item, int amount )
		{
			return false;
			/*if ( item.GetType() == m_Type )
			{
				if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;

					if ( weapon.Quality == WeaponQuality.Low || weapon.Quality == WeaponQuality.Exceptional || (int)weapon.DurabilityLevel > 0 || (int)weapon.DamageLevel > 0 || (int)weapon.AccuracyLevel > 0 )
						return false;
				}

				if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;

					if ( armor.Quality == ArmorQuality.Low || armor.Quality == ArmorQuality.Exceptional || (int)armor.Durability > 0 || (int)armor.ProtectionLevel > 0 )
						return false;
				}

				m_Amount += amount;

				return true;
			}
			else
			{
				return false;
			}*/
		}

		public static int GetRandBuyPriceFor( int itemPrice )
		{
			// Use centralized price manager for random price calculation
			return VendorPriceManager.GetRandomBuyPrice(itemPrice);
		}

		public static int GetRandBuyAmountFor( int itemAmount )
		{ 
			
			int lowAmount = 75;
			int highAmount = 125;	

			int amount = 0;
			for ( int i = 1; i <= 3; i++ )
			{
				amount += Utility.RandomMinMax( lowAmount, highAmount );
			}
			amount /= 3;

			if ( amount < 1 )
				amount = 1;

			double finalamount = ((double)amount/100) * (double)itemAmount;

			return Convert.ToInt32(finalamount); 

		}

		public void OnRestock()
		{
			if ( m_Amount <= 0 )
			{
				m_MaxAmount *= 2;

				if ( m_MaxAmount >= 999 )
					m_MaxAmount = 999;
			}
			else
			{
				/* NOTE: According to UO.com, the quantity is halved if the item does not reach 0
				 * Here we implement differently: the quantity is halved only if less than half
				 * of the maximum quantity was bought. That is, if more than half is sold, then
				 * there's clearly a demand and we should not cut down on the stock.
				 */

				int halfQuantity = m_MaxAmount;

				if ( halfQuantity >= 999 )
					halfQuantity = 640;
				else if ( halfQuantity > 20 )
					halfQuantity /= 2;

				if ( m_Amount >= halfQuantity )
					m_MaxAmount = halfQuantity;
			}

			m_Amount = m_MaxAmount;
		}
	}
}