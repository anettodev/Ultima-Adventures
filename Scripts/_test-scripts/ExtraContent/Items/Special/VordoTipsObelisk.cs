using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class VordoTipsObelisk : Item
	{
		private string m_CustomName;
		private string m_TipText;
		private int m_HueOverride;

		[CommandProperty( AccessLevel.GameMaster )]
		public string CustomName
		{
			get { return m_CustomName; }
			set
			{
				m_CustomName = value;
				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string TipText
		{
			get { return m_TipText; }
			set
			{
				m_TipText = value;
				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HueOverride
		{
			get { return m_HueOverride; }
			set
			{
				m_HueOverride = value;
				Hue = value;
				InvalidateProperties();
			}
		}

		public override int LabelNumber
		{
			get
			{
				if ( !string.IsNullOrEmpty( m_CustomName ) )
					return 0; // Use custom name instead
				return 1016474; // "an MoonObelisk"
			}
		}

		public string Name
		{
			get
			{
				if ( !string.IsNullOrEmpty( m_CustomName ) )
					return m_CustomName;
				return base.Name;
			}
			set
			{
				m_CustomName = value;
				InvalidateProperties();
			}
		}

		[Constructable]
		public VordoTipsObelisk() : base(0x115F)
		{
			Movable = false;
			m_CustomName = null;
			m_TipText = "Welcome to Vordo Tips! Speak 'teleport' near MoonObeliskKeywordTeleporters to travel.";
			m_HueOverride = 0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !string.IsNullOrEmpty( m_TipText ) )
			{
				// Add the tip text to the properties
				list.Add( "Dica: {0}", m_TipText );
			}
		}

		public VordoTipsObelisk( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_CustomName );
			writer.Write( m_TipText );
			writer.Write( m_HueOverride );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_CustomName = reader.ReadString();
					m_TipText = reader.ReadString();
					m_HueOverride = reader.ReadInt();

					if ( m_HueOverride > 0 )
						Hue = m_HueOverride;

					break;
				}
			}
		}
	}
}
