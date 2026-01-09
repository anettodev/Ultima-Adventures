using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Regular Confusion Blast Potion
	/// Pacifies creatures in a 5-tile radius for 5 seconds
	/// </summary>
	public class ConfusionBlastPotion : BaseConfusionBlastPotion
	{
		#region Properties
		
		/// <summary>Blast radius for regular confusion blast potion</summary>
		public override int Radius 
		{ 
			get { return ConfusionBlastConstants.REGULAR_BLAST_RADIUS; } 
		}
		
		public override int LabelNumber 
		{ 
			get { return 1072105; } 
		}
		
		#endregion
		
	#region Constructors
	
	[Constructable]
	public ConfusionBlastPotion() : base( PotionEffect.ConfusionBlast )
	{
		Name = "Poção de Alquimia"; // Generic alchemy potion name
		Hue = Server.Items.PotionKeg.GetPotionColor( this );
	}
	
	public ConfusionBlastPotion( Serial serial ) : base( serial )
	{
	}
	
	#endregion
	
	#region Property Display
	
	/// <summary>
	/// Adds purple bracket property display for potion type
	/// </summary>
	public override void AddNameProperty( ObjectPropertyList list )
	{
		base.AddNameProperty( list );
		list.Add( 1070722, string.Format( "<BASEFONT COLOR=#{0:X6}>[{1}]<BASEFONT COLOR=#FFFFFF>", 
			ConfusionBlastConstants.TYPE_BRACKET_COLOR, 
			ConfusionBlastStringConstants.TYPE_CONFUSION_BLAST ) );
	}
	
	#endregion
		
		#region Serialization
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		#endregion
	}
}
