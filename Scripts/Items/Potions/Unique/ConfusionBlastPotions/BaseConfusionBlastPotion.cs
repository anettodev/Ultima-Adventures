using System;
using Server;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// Base class for Confusion Blast Potions
	/// Throwable area-effect potions that pacify all nearby creatures for 5 seconds
	/// Does not affect players or RuneGuardians
	/// </summary>
	public abstract class BaseConfusionBlastPotion : BaseThrowablePotion
	{
		#region Abstract Properties
		
		/// <summary>Gets the blast radius in tiles (varies by potion strength)</summary>
		public abstract int Radius { get; }
		
		#endregion
		
		#region BaseThrowablePotion Overrides
		
		/// <summary>Flying potion item ID during throw animation</summary>
		protected override int FlyingPotionItemID 
		{ 
			get { return ConfusionBlastConstants.FLYING_POTION_ITEM_ID; } 
		}
		
		/// <summary>Flying potion effect speed</summary>
		protected override int FlyingPotionSpeed 
		{ 
			get { return ConfusionBlastConstants.FLYING_EFFECT_SPEED; } 
		}
		
		/// <summary>Potion type name for messages</summary>
		protected override string PotionTypeName 
		{ 
			get { return ConfusionBlastStringConstants.NAME_CONFUSION_BLAST; } 
		}
		
		/// <summary>Effect radius in tiles (uses Radius property)</summary>
		protected override int EffectRadius 
		{ 
			get { return Radius; } 
		}
		
		/// <summary>Confusion blast uses instant detonation (no countdown timer)</summary>
		protected override bool InstantDetonation 
		{ 
			get { return true; } 
		}
		
		/// <summary>Cooldown between uses (seconds)</summary>
		protected override double BaseCooldownSeconds 
		{ 
			get { return ConfusionBlastConstants.COOLDOWN_SECONDS; } 
		}
		
		/// <summary>Throw range in tiles</summary>
		protected override int ThrowRange 
		{ 
			get { return ConfusionBlastConstants.THROW_RANGE; } 
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Initializes a new confusion blast potion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseConfusionBlastPotion( PotionEffect effect ) : base( 0x180F, effect )
		{
		}
		
		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public BaseConfusionBlastPotion( Serial serial ) : base( serial )
		{
		}
		
		#endregion
		
		#region Serialization
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)ConfusionBlastConstants.SERIALIZATION_VERSION );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		#endregion
		
		#region Area Effect Implementation
		
		/// <summary>
		/// Applies confusion blast area effect - pacifies all creatures in radius
		/// </summary>
		/// <param name="from">The mobile who threw the potion</param>
		/// <param name="direct">Whether this is a direct throw</param>
		/// <param name="loc">The effect location</param>
		/// <param name="map">The map where effect occurs</param>
		protected override void ApplyAreaEffect( Mobile from, bool direct, Point3D loc, Map map )
		{
			// Pacify all creatures in range
			foreach ( Mobile mobile in map.GetMobilesInRange( loc, Radius ) )
			{
				if ( CanBePacified( mobile ) )
				{
					BaseCreature creature = (BaseCreature)mobile;
					creature.Pacify( from, DateTime.UtcNow + TimeSpan.FromSeconds( ConfusionBlastConstants.PACIFY_DURATION_SECONDS ) );
				}
			}
		}
		
		/// <summary>
		/// Plays visual and sound effects for confusion blast detonation
		/// </summary>
		/// <param name="loc">Effect location</param>
		/// <param name="map">Effect map</param>
		protected override void PlayDetonationEffects( Point3D loc, Map map )
		{
			// Explosion sound
			Effects.PlaySound( loc, map, ConfusionBlastConstants.EXPLOSION_SOUND_ID );
			
			// First circle wave (270° to 90°)
			Geometry.Circle2D( loc, map, Radius, new DoEffect_Callback( BlastEffect ), 
				ConfusionBlastConstants.CIRCLE_START_ANGLE_1, 
				ConfusionBlastConstants.CIRCLE_END_ANGLE_1 );
			
			// Second circle wave after delay (90° to 270°)
			Timer.DelayCall( TimeSpan.FromSeconds( ConfusionBlastConstants.CIRCLE_EFFECT_DELAY ), 
				new TimerStateCallback( CircleEffect2 ), new object[] { loc, map } );
		}
		
		#endregion
		
		#region Visual Effect Helpers
		
		/// <summary>
		/// Creates blast effect particles at specified point
		/// </summary>
		/// <param name="p">Effect location</param>
		/// <param name="map">Effect map</param>
		private void BlastEffect( Point3D p, Map map )
		{
			if ( map.CanFit( p, ConfusionBlastConstants.FIT_CHECK_HEIGHT ) )
			{
				Effects.SendLocationEffect( p, map, ConfusionBlastConstants.BLAST_EFFECT_ID, 
					ConfusionBlastConstants.BLAST_EFFECT_SPEED, ConfusionBlastConstants.BLAST_EFFECT_DURATION );
			}
		}
		
		/// <summary>
		/// Creates second circle wave effect (delayed)
		/// </summary>
		/// <param name="state">State array containing location and map</param>
		private void CircleEffect2( object state )
		{
			object[] states = (object[])state;
			Point3D loc = (Point3D)states[0];
			Map map = (Map)states[1];
			
			Geometry.Circle2D( loc, map, Radius, new DoEffect_Callback( BlastEffect ), 
				ConfusionBlastConstants.CIRCLE_START_ANGLE_2, 
				ConfusionBlastConstants.CIRCLE_END_ANGLE_2 );
		}
		
		#endregion
		
		#region Validation Helpers
		
		/// <summary>
		/// Checks if mobile can be pacified by confusion blast
		/// </summary>
		/// <param name="mobile">Mobile to check</param>
		/// <returns>True if can be pacified, false otherwise</returns>
		private bool CanBePacified( Mobile mobile )
		{
			// Only affect BaseCreatures, but exclude RuneGuardians
			return mobile is BaseCreature && !(mobile is RuneGuardian);
		}
		
		#endregion
	}
}
