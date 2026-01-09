using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	[Furniture]
	public class TombStone : Item
	{
		private string TombName;

		private string lastwords;

		private string sbtime;

		private bool soulbound;

		private string lastdeeds;

		private DateTime died = DateTime.UtcNow;
		private Mobile deceased = null;
		
		[Constructable]
		public TombStone( Mobile from, Mobile Killed ) : base( 0x116E )
		{
			
			if ( !(from is PlayerMobile))
				return;
			
			Weight = 75.0;
			Hue = 0x763;
			Movable = true;
			//Passable = false;
			ItemID = Utility.RandomList( 0xED4, 0xED5, 0xED6, 0xED7, 0xED8, 0xEDD, 0xEDE, 0x1165, 0x1166, 0x1167, 0x1168, 0x1169, 0x116A, 0x116B, 0x116C, 0x116D, 0x116E, 0x116F, 0x1170, 0x1171, 0x1172, 0x1173, 0x1174, 0x1175, 0x1176, 0x1177, 0x1178, 0x1179, 0x117A, 0x117B, 0x117C, 0x117D, 0x117E, 0x117F, 0x1180, 0x1181, 0x1182, 0x1183, 0x1184 );
			
			if (Killed == null)
				Killed = from;
			
			string Cause = "";
			string title = "";
			string killertitle = "";

			
            switch ( Utility.Random( 119 ) )
            {
                case 0: Cause = "Despedaçado"; break;
                case 1: Cause = "Decapitado"; break;
                case 2: Cause = "Liquefeito"; break;
                case 3: Cause = "Explodido"; break;
                case 4: Cause = "Morreu de Medo"; break;
                case 5: Cause = "Atropelado"; break;
                case 6: Cause = "Esfolado"; break;
                case 7: Cause = "Imolado"; break;
                case 8: Cause = "Abatido"; break;
                case 9: Cause = "Executado"; break;
                case 10: Cause = "Desperdiçado"; break;
                case 11: Cause = "Crucificado"; break;
                case 12: Cause = "Desmembrado"; break;
                case 13: Cause = "Cortado ao Meio"; break;   
                case 14: Cause = "Processado"; break;
                case 15: Cause = "Eradicado"; break;
                case 16: Cause = "Ligeiramente Usado"; break;
                case 17: Cause = "Expulso"; break;
                case 18: Cause = "Ensinado uma Lição de Vida Valiosa"; break; 
                case 19: Cause = "Humilhado"; break;
                case 20: Cause = "Tea-Bagged"; break;
                case 21: Cause = "Perfurado"; break;   
                case 22: Cause = "Eviscerado"; break;
                case 23: Cause = "Joelhada na Cabeça"; break;
                case 24: Cause = "Desmontado"; break;
                case 25: Cause = "Esmagado"; break;
				case 26: Cause = "culpando lag por sua morte"; break;
				case 27: Cause = "torcido a orelha"; break;
				case 28: Cause = "dado uma maçã envenenada"; break;
				case 29: Cause = "conversado"; break;
				case 30: Cause = "cutucado no nariz"; break;
				case 31: Cause = "corrigido"; break;
				case 32: Cause = "fundido"; break;
				case 33: Cause = "contaminado"; break;
				case 34: Cause = "colocado em seu lugar"; break;
				case 35: Cause = "jogado"; break;
				case 36: Cause = "rebatido para fora do parque"; break;
				case 37: Cause = "coringado"; break;
				case 38: Cause = "rindo tanto de uma piada contada"; break;
				case 39: Cause = "olhando para um presente dado"; break;
				case 40: Cause = "incendiado"; break;
				case 41: Cause = "intimidado"; break;
				case 42: Cause = "amaciado"; break;
				case 43: Cause = "ensinado a ser cauteloso"; break;
				case 44: Cause = "eviscerado"; break;
				case 45: Cause = "socado"; break;
				case 46: Cause = "cutucado"; break;
				case 47: Cause = "dado um enema"; break;
				case 48: Cause = "invadido"; break;
				case 49: Cause = "descoberto"; break;
				case 50: Cause = "interrompido"; break;
				case 51: Cause = "dobrado em dois"; break;
				case 52: Cause = "chamado de nome feio"; break;
				case 53: Cause = "congelado e estilhaçado"; break;
				case 54: Cause = "amadurecido"; break;
				case 55: Cause = "zombado"; break;
				case 56: Cause = "dobrado como um origami"; break;
				case 57: Cause = "dobrado em dois"; break;
				case 58: Cause = "mijado em cima"; break;
				case 59: Cause = "mudado de sexo"; break;
				case 60: Cause = "mastigado"; break;
				case 61: Cause = "peidado em cima"; break;
				case 62: Cause = "dado herpes"; break;
				case 63: Cause = "encaixotado"; break;
				case 64: Cause = "raioado"; break;
				case 65: Cause = "bola de fogo"; break;
				case 66: Cause = "catingado"; break;
				case 67: Cause = "enterrado"; break;
				case 68: Cause = "agarrado pela buceta"; break;
				case 69: Cause = "molestado"; break;
				case 70: Cause = "PwNd"; break;
				case 71: Cause = "tapa de peixe"; break;
				case 72: Cause = "cortado com karatê"; break;
				case 73: Cause = "moinho de vento"; break;
				case 74: Cause = "chute giratório"; break;
				case 75: Cause = "pego traindo sua esposa"; break;
				case 76: Cause = "pego trapaceando no pôquer"; break;
				case 77: Cause = "cuckado"; break;
				case 78: Cause = "não estava pronto para ser testado"; break;
				case 79: Cause = "abandonado quando criança"; break;
				case 80: Cause = "mastigado"; break;
				case 81: Cause = "Willy-Nillied"; break;
				case 82: Cause = "comido"; break;
				case 83: Cause = "eletrocutado"; break;
				case 84: Cause = "cortado com papel"; break;
				case 85: Cause = "pisoteado"; break;
				case 86: Cause = "lamido"; break;
				case 87: Cause = "fodido real"; break;
				case 88: Cause = "afeminado"; break;
				case 89: Cause = "chupado"; break;
				case 90: Cause = "realmente, realmente, admirado"; break;
				case 91: Cause = "encontrado sem máscara"; break;
				case 92: Cause = "pego se beijando"; break;
				case 93: Cause = "beijando um apêndice apresentado"; break;
				case 94: Cause = "encontrado colhendo frutas"; break;
				case 95: Cause = "comendo doce dado"; break;
				case 96: Cause = "mordido"; break;
				case 97: Cause = "concedido um desejo"; break;
				case 98: Cause = "morreu de rir"; break;
				case 99: Cause = "espasmado"; break;
				case 100: Cause = "sequestrado"; break;
				case 101: Cause = "cutucado com o dedo"; break;
				case 102: Cause = "cuspido em cima"; break;
				case 103: Cause = "domado-liberado"; break;
				case 104: Cause = "domado-morto"; break;
				case 105: Cause = "nariz tampado"; break;
				case 106: Cause = "vacinado"; break;
				case 107: Cause = "concedido um desejo"; break;
				case 108: Cause = "massageado na bunda"; break;
				case 109: Cause = "esfregado no corpo"; break;
				case 110: Cause = "mandado calar a boca"; break;
				case 111: Cause = "atormentado"; break;
			case 112: Cause = "pavoneado"; break;
			case 113: Cause = "presumido culpado"; break;
			case 114: Cause = "castigado"; break;
			case 115: Cause = "visto espiando seu vizinho"; break;
			case 116: Cause = "pego tentando lamber o cotovelo"; break;
			case 117: Cause = "pego fazendo número dois"; break;
			case 118: Cause = "visto enfiando lenços nas calças"; break;

				
            }

			died = DateTime.UtcNow;
			deceased = from;
			lastwords = "";
			lastdeeds = "";
			sbtime = "";

			if (from.Title == null && GetPlayerInfo.GetSkillTitle( from ) != null) //|| from.Title == "")
				title = GetPlayerInfo.GetSkillTitle( from );
			else if (from.Title != null)
				title = from.Title;
			else
				title = "";
			
			if (Killed.Title == null )
				killertitle = "";
			else
				killertitle = Killed.Title	;		
			
			
			if (Killed == from)
				TombName = "Aqui Jaz " + from.Name + " o " + title + " que foi " + Cause + " por Suas Próprias Mãos.";
			else
				TombName = "Aqui Jaz " + from.Name + " o " + title + " que foi " + Cause + " por " + Killed.Name + " " +killertitle;
			
			if ( ((PlayerMobile)deceased).lastwords != null && ((PlayerMobile)deceased).lastwords != "")
				lastwords = ((PlayerMobile)deceased).lastwords;

			if (((PlayerMobile)deceased).lastdeeds != null && ((PlayerMobile)deceased).lastdeeds != "")
				lastdeeds = ((PlayerMobile)from).lastdeeds;

			if (((PlayerMobile)deceased).SoulBound)
			{
				TimeSpan xx = died - ((PlayerMobile)deceased).SoulBoundDate;
				sbtime = String.Format("{0:n0} days, {1:n0} hours and {2:n0} minutes", xx.Days, xx.Hours, xx.Minutes);
				soulbound = true;
			}
			else 
				soulbound = false;

			Name = TombName;

			
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile && !m.Alive )
				return true;
			if (m is BaseCreature && m.Fame > 10000)
				return true;

			return !m.Alive;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if (deceased != null)
			{
				if (lastwords != "")//(((PlayerMobile)deceased).lastwords != null && ((PlayerMobile)deceased).lastwords != "" && ((PlayerMobile)deceased).lastwords != " ")
					list.Add("Suas últimas palavras foram " + lastwords );

				if (lastdeeds != "")//(((PlayerMobile)deceased).lastdeeds != null && ((PlayerMobile)deceased).lastdeeds != "" && ((PlayerMobile)deceased).lastdeeds != " ")
					list.Add("Recentemente " + lastdeeds );

				if (soulbound && sbtime != "")//(deceased is PlayerMobile && soulbound )
				{
					list.Add( "Esta pessoa estava SoulBound" );
					list.Add( "Viveu por: " + sbtime );
				}
			}


		}


		public TombStone(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{

			if (lastwords == null )
				lastwords = "";
			if (lastdeeds == null)
				lastdeeds = "";
			if (sbtime == null)
				sbtime = "";
			if (soulbound == null)
				soulbound = false;
			if (TombName == null)
				TombName = "";

			base.Serialize(writer);

			//serialize deser was a mess... fixing with a new version
			if (deceased != null)
				writer.Write((int) 5);
			else 
				writer.Write((int) 4);

			writer.Write( (string)TombName );
			writer.Write( (DateTime)died );

			writer.Write ( (string)lastwords);
			writer.Write ( (string)lastdeeds);
			writer.Write ( (string)sbtime);
			writer.Write ( (bool)soulbound);

			if (deceased != null)
			{
				writer.Write( (Mobile)deceased );
			}

/*
			if (lastdeeds != null && lastwords != null && sbtime != null && lastdeeds != "" && lastwords != "" && sbtime != "" )
				writer.Write((int) 3); // version
			else if (deceased != null)
				writer.Write((int) 2); // version
			else
				writer.Write((int) 1); // version

			writer.Write( (string)TombName );
			writer.Write( (DateTime)died );
			if (deceased != null)
			{
				writer.Write( (Mobile)deceased );
			}
			if (lastdeeds != null && lastwords != null && sbtime != null && lastdeeds != "" && lastwords != "" && sbtime != "")
			{
				writer.Write ( (string)lastwords);
				writer.Write ( (string)lastdeeds);
				writer.Write ( (string)sbtime);
				writer.Write ( (bool)soulbound);
			}
			*/

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			
			TombName = reader.ReadString();
			Name = TombName;


			if (version <= 3) //grandfather
			{

				if (version >= 1)
					died = reader.ReadDateTime();
				if (version >= 2)
					deceased = reader.ReadMobile();
				if (version >= 3)
				{
					lastwords = reader.ReadString();
					lastdeeds = reader.ReadString();
					sbtime = reader.ReadString();
					soulbound = reader.ReadBool();
				}
			}
			else if (version > 3) // ervised
			{
				died = reader.ReadDateTime();
				lastwords = reader.ReadString();
				lastdeeds = reader.ReadString();
				sbtime = reader.ReadString();
				soulbound = reader.ReadBool();
				if (version >= 5)
					deceased = reader.ReadMobile();

			}
		}
	}
}
