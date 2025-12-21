using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	/// <summary>
	/// Fortune Teller NPC that provides fortune telling services and sells magical items.
	/// Skilled in various magical arts including Magery, Necromancy, and EvalInt.
	/// </summary>
	public class FortuneTeller : BaseVendor
	{
		#region Fields

		private List<SBInfo> m_SBInfos = new List<SBInfo>();

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of vendor buy/sell information.
		/// </summary>
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FortuneTeller class.
		/// </summary>
		[Constructable]
		public FortuneTeller() : base( FortuneTellerStringConstants.TITLE_FORTUNE_TELLER )
		{
			SetSkill( SkillName.EvalInt, FortuneTellerConstants.SKILL_EVAL_INT_MIN, FortuneTellerConstants.SKILL_EVAL_INT_MAX );
			SetSkill( SkillName.Forensics, FortuneTellerConstants.SKILL_FORENSICS_MIN, FortuneTellerConstants.SKILL_FORENSICS_MAX );
			SetSkill( SkillName.Magery, FortuneTellerConstants.SKILL_MAGERY_MIN, FortuneTellerConstants.SKILL_MAGERY_MAX );
			SetSkill( SkillName.Meditation, FortuneTellerConstants.SKILL_MEDITATION_MIN, FortuneTellerConstants.SKILL_MEDITATION_MAX );
			SetSkill( SkillName.MagicResist, FortuneTellerConstants.SKILL_MAGIC_RESIST_MIN, FortuneTellerConstants.SKILL_MAGIC_RESIST_MAX );
			SetSkill( SkillName.Wrestling, FortuneTellerConstants.SKILL_WRESTLING_MIN, FortuneTellerConstants.SKILL_WRESTLING_MAX );
			SetSkill( SkillName.Necromancy, FortuneTellerConstants.SKILL_NECROMANCY_MIN, FortuneTellerConstants.SKILL_NECROMANCY_MAX );
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public FortuneTeller( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the vendor buy/sell information for this fortune teller.
		/// </summary>
		public override void InitSBInfo()
		{
			SBInfos.Add( new SBMage() );
			SBInfos.Add( new SBFortuneTeller() );
			m_SBInfos.Add( new SBBuyArtifacts() ); 

			if ( Map == Map.Felucca )
				m_SBInfos.Add( new SBElfWizard() );
		}

		/// <summary>
		/// Initializes the fortune teller's outfit with a colorful robe and random headgear.
		/// </summary>
		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Robe( RandomBrightHue() ) );
			AddRandomHeadgear();
			AddItem( new Spellbook() );
		}

		/// <summary>
		/// Adds a random piece of headgear to the outfit.
		/// </summary>
		private void AddRandomHeadgear()
		{
			switch ( Utility.Random( FortuneTellerConstants.OUTFIT_SELECTION_COUNT ) )
			{
				case 0: AddItem( new SkullCap( RandomBrightHue() ) ); break;
				case 1: AddItem( new WizardsHat( RandomBrightHue() ) ); break;
				case 2: AddItem( new Bandana( RandomBrightHue() ) ); break;
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the fortune teller's data.
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) FortuneTellerConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the fortune teller's data.
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		#endregion
	}
}