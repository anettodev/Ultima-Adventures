using System;
using Server;
using Server.Mobiles;

namespace Server.Mobiles
{
	/// <summary>
	/// A static mobile used to mark items as "made by Developer".
	/// This mobile is never actually spawned in the world.
	/// </summary>
	public class DeveloperMobile : Mobile
	{
		private static DeveloperMobile m_Instance;

		/// <summary>
		/// Gets the singleton instance of DeveloperMobile.
		/// </summary>
		public static DeveloperMobile Instance
		{
			get
			{
				if ( m_Instance == null || m_Instance.Deleted )
				{
					m_Instance = new DeveloperMobile();
				}
				return m_Instance;
			}
		}

		private DeveloperMobile()
			: base()
		{
			Name = "Developer";
			AccessLevel = AccessLevel.Developer;
			// Set to Internal map so it's not in the world - this is just a marker
			Map = Map.Internal;
		}

		public DeveloperMobile( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}

