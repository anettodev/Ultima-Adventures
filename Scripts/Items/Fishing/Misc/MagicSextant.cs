using System;
using Server.Network;
using Server.Misc;
using Server.Regions;
using Server.Targeting;

namespace Server.Items
{
	public class MagicSextant : Sextant
	{
		[Constructable]
		public MagicSextant()
		{
			Name = "magic sextant";
			Weight = 4.0;
			ItemID = 0x26A0;
		}

		public MagicSextant( Serial serial ) : base( serial )
		{
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

		public override void OnDoubleClick( Mobile from )
		{
			string world = Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y );

			// If in Underworld, use base sextant behavior
			if ( world == "the Underworld" )
			{
				base.OnDoubleClick( from );
				return;
			}

			// Otherwise, allow targeting SOS items
			from.SendMessage( 0x3B2, "Selecione um SOS para rastrear sua localização!" );
			from.Target = new SOSLocationTarget();
		}

		private class SOSLocationTarget : Target
		{
			public SOSLocationTarget() : base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is SOS )
				{
					SOS sos = (SOS)targeted;

					Point3D loc = sos.TargetLocation;
					Map map = sos.TargetMap;

					if ( map == null || loc == Point3D.Zero )
					{
						from.SendMessage( 55, "Este SOS não possui informações de localização válidas." );
						return;
					}

					// Get region name
					string regionName = Worlds.GetRegionName( map, loc );
					if ( string.IsNullOrEmpty( regionName ) )
					{
						regionName = "águas desconhecidas";
					}

					// Get world name
					string worldName = sos.MapWorld;
					if ( string.IsNullOrEmpty( worldName ) )
					{
						worldName = Worlds.GetMyWorld( map, loc, loc.X, loc.Y );
					}

					// Get sextant format coordinates
					int xLong = 0, yLat = 0;
					int xMins = 0, yMins = 0;
					bool xEast = false, ySouth = false;
					string sextantFormat = "?????";

					if ( Sextant.Format( loc, map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
					{
						sextantFormat = String.Format( "{0}°{1}'{2}, {3}°{4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
					}

					// Create ludic message
					string shipName = sos.ShipName;
					if ( string.IsNullOrEmpty( shipName ) )
					{
						shipName = "navio naufragado";
					}

					// Random ludic messages
					string[] ludicMessages = new string[]
					{
						"*O sextante mágico brilha intensamente!* As estrelas sussurram que o {0} está perdido em {1}, nas coordenadas {2}! Que destino trágico!",
						"*O cristal do sextante pulsa com energia arcana!* A magia revela que o {0} afundou perto de {1} em {2}! Os espíritos do mar choram por ele!",
						"*Uma aura mística envolve o sextante!* A localização mágica aponta para {1} nas coordenadas {2}, onde o {0} descansa nas profundezas!",
						"*O sextante emite uma luz etérea!* A magia antiga mostra que o {0} está em {1}, precisamente em {2}! Que mistério fascinante!",
						"*O poder mágico do sextante se manifesta!* As coordenadas {2} em {1} revelam o paradeiro do {0}! As correntes oceânicas o guardam!",
						"*O sextante mágico vibra com poder!* A localização do {0} está marcada em {1} nas coordenadas {2}! O mar guarda seus segredos!"
					};

					string message = ludicMessages[Utility.Random( ludicMessages.Length )];
					message = String.Format( message, shipName, regionName, sextantFormat );

					from.SendMessage( 0x35, message );

					// Also show map name and coordinates
					if ( map != null && map.Name != null )
					{
						from.SendMessage( 0x3F, "O mapa indica que este local está em {0} nas coordenadas X={1}, Y={2}, Z={3}.", map.Name, loc.X, loc.Y, loc.Z );
					}
				}
				else
				{
					from.SendMessage( 55, "Você precisa selecionar um SOS (Pedido de Socorro) para rastrear sua localização!" );
				}
			}
		}
	}
}