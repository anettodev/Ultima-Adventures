using System;
using Server;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Regions;
using Server.Spells;
using Server.Network;
using Server.Multis;
using System.Collections;

namespace Server.Items 
{
	public class TrapWand : Item
	{
		public int WandPower;

		[CommandProperty(AccessLevel.Owner)]
		public int Wand_Power { get { return WandPower; } set { WandPower = value; InvalidateProperties(); } }

		public Mobile owner;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return owner; }
			set{ owner = value; }
		}

		[Constructable]
		public TrapWand() : this( null )
		{
		}
		
		[Constructable]
		public TrapWand( Mobile from ) : base( 0x4FD6 )
		{
			this.owner = from;
			Weight = 1.0;
			LootType = LootType.Blessed;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);
			Light = LightType.Circle150;
			RenameWand();
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this );
			thisTimer.Start();
		}

		/// <summary>
		/// Prevents wand from being dropped or traded
		/// </summary>
		public override bool DropToWorld( Mobile from, Point3D p )
		{
			from.SendMessage( 0x3B2, "O orbe anti-armadilha não pode sair da sua mochila." );
			return false;
		}

		/// <summary>
		/// Prevents wand from being given to others
		/// </summary>
		public override bool OnDragLift( Mobile from )
		{
			if ( from != owner && owner != null )
			{
				from.SendMessage( 0x3B2, "Este orbe não pertence a você." );
				return false;
			}
			return base.OnDragLift( from );
		}

		/// <summary>
		/// Checks if wand is in owner's backpack, deletes if not
		/// </summary>
		public override void OnLocationChange( Point3D oldLocation )
		{
			base.OnLocationChange( oldLocation );

			if ( owner != null && this.Parent != owner.Backpack )
			{
				owner.SendMessage( 0x3B2, "* Seu orbe anti-armadilha desapareceu ao sair da mochila. *" );
				owner.PlaySound( 0x1F0 );
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Evita armadilhas em paredes e pisos em " + WandPower + "%");
			list.Add( 1049644, "Deve estar na mochila e dura 5 minutos"); // PARENTHESIS
			list.Add( 1070722, "Não pode ser transferido ou removido da mochila");
        }

		private void RenameWand()
		{
			if ( owner != null )
			{
				this.Name = "orbe anti-armadilha de " + owner.Name;
			}
			else
			{
				this.Name = "orbe anti-armadilha";
			}		
		}

		public TrapWand( Serial serial ) : base( serial )
		{ 
		} 
		
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
			writer.Write( (Mobile)owner);
            writer.Write( WandPower );
			RenameWand();
		} 
		
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
            WandPower = reader.ReadInt();
			this.Delete(); // none when the world starts 
			RenameWand();
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromMinutes( 5.0 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
				if (( i_item != null ) && ( !i_item.Deleted ))
				{
					TrapWand wands = (TrapWand)i_item;
					Mobile from = wands.owner;
					from.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "* Seu orbe anti-armadilha desapareceu. *");
					from.PlaySound( 0x1F0 );
					i_item.Delete();
				}
			} 
		} 
	}
}