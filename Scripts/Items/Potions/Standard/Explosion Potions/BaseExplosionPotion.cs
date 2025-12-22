using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Base class for all explosion potions.
	/// Provides area damage with throwable targeting.
	/// Now uses BaseThrowablePotion for consistent countdown and targeting mechanics.
	/// </summary>
	public abstract class BaseExplosionPotion : BaseThrowablePotion
	{
	#region Constants

	/// <summary>Should explosion potions trigger chain explosions with nearby potions?</summary>
	private static bool LeveledExplosion = false;

	/// <summary>Blast radius in tiles</summary>
	private const int ExplosionRange = 2;

	/// <summary>Message color for countdown display (red)</summary>
	private const int COUNTDOWN_MESSAGE_COLOR = 0x22;

	#endregion

		#region Abstract Properties

		/// <summary>Gets the minimum damage for this explosion</summary>
		public abstract int MinDamage { get; }

		/// <summary>Gets the maximum damage for this explosion</summary>
		public abstract int MaxDamage { get; }

		#endregion

		#region BaseThrowablePotion Implementation

		/// <summary>Flying potion item ID during throw</summary>
		protected override int FlyingPotionItemID { get { return 0xF0D; } }

		/// <summary>Potion type name for messages</summary>
		protected override string PotionTypeName { get { return "poção explosiva"; } }

		/// <summary>Effect radius (2 tiles)</summary>
		protected override int EffectRadius { get { return ExplosionRange; } }

		/// <summary>Countdown message color (red for explosion)</summary>
		protected override int CountdownMessageColor { get { return COUNTDOWN_MESSAGE_COLOR; } }

		/// <summary>
		/// Displays explosion visual effect on each countdown tick
		/// </summary>
		protected override void OnCountdownTick( Point3D loc, Map map, int timer )
		{
			// Red explosion sparkle effect during countdown
			Effects.SendLocationEffect( loc, map, 0x36BD, 20, 10, COUNTDOWN_MESSAGE_COLOR, 0 );
		}

		/// <summary>
		/// Plays detonation visual and sound effects
		/// </summary>
		protected override void PlayDetonationEffects( Point3D loc, Map map )
		{
			// Explosion sound and large fire blast visual
			Effects.PlaySound( loc, map, 0x307 );
			Effects.SendLocationEffect( loc, map, 0x3822, 20, 10, 0, 0 );
		}

		/// <summary>
		/// Applies area effect: Explosion damage to all mobiles in range
		/// </summary>
		protected override void ApplyAreaEffect( Mobile from, bool direct, Point3D loc, Map map )
		{
			Explode( from, direct, loc, map );
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseExplosionPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseExplosionPotion( PotionEffect effect ) : base( 0xF0D, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseExplosionPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the explosion potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the explosion potion
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds custom properties to the object property list
		/// </summary>
		/// <param name="list">The object property list</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			// Display potion type in custom cyan color (#8be4fc) with brackets
			string potionName = PotionMetadata.GetKegName( this.PotionEffect );
			if ( potionName != null )
			{
				list.Add( 1070722, string.Format( "<BASEFONT COLOR=#8be4fc>[{0}]", potionName ) ); // Custom cyan color #8be4fc
			}
		}
																																																																															
																																																																														
		#endregion

		#region Explosion Damage Logic

		/// <summary>
		/// Performs the explosion at the specified location
		/// Damage is always visible to all affected players (like area damage spells)
		/// PUBLIC for backward compatibility with external code (FrankenFighter, BaseRed, etc.)
		/// </summary>
		/// <param name="from">The mobile who threw the potion</param>
		/// <param name="direct">Whether this is a direct throw (affects alchemy bonus)</param>
		/// <param name="loc">The explosion location</param>
		/// <param name="map">The map where explosion occurs</param>
		public void Explode( Mobile from, bool direct, Point3D loc, Map map )
		{
			if ( map == null )
				return;
			
			int alchemyBonus = 0;

			if ( direct )
				alchemyBonus = (int)(from.Skills.Alchemy.Value / (Core.AOS ? 5 : 10));

			IPooledEnumerable eable;

			// Explosion Potions have a wider range based on EnhancePotions for Chemists
			int scalar = 0;
			double scalarx = 0;
			if (from is PlayerMobile && ((PlayerMobile)from).Alchemist())
			{
				scalarx = 1.0 + (0.02 * EnhancePotions(from));
				scalar = Convert.ToInt32(scalarx);
			}

			if (LeveledExplosion)
			{
				eable = map.GetObjectsInRange(loc, ExplosionRange + scalar);
			}
			else
			{
				eable = map.GetMobilesInRange(loc, ExplosionRange + scalar);
			}
			
			ArrayList toExplode = new ArrayList();
			int toDamage = 0;

			foreach ( object o in eable )
			{
				if ( o is Mobile )
				{
					toExplode.Add( o );
					++toDamage;
				}
			}

			eable.Free();

			int min = Scale( from, MinDamage );
			int max = Scale( from, MaxDamage );

			// Apply damage to all targets in range
			for ( int i = 0; i < toExplode.Count; ++i )
			{
				object o = toExplode[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					if ( from != null )
						from.DoHarmful( m );

					int damage = Utility.RandomMinMax( min, max );
					
					damage += alchemyBonus;

					// Troubadour bonus against discorded targets
					if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() && SkillHandlers.Discordance.IsDiscorded(m))
						damage += (int)((double)damage * 0.5);

					// Reduce damage if multiple targets
					if ( Core.AOS && toDamage > 2 )
						damage /= toDamage - 1;

				// Apply damage with visible damage numbers (like area spells)
				// Damage type: 0% physical, 100% fire, 0% cold, 0% poison, 0% energy
				SpellHelper.Damage( TimeSpan.Zero, m, from, damage, 0, 100, 0, 0, 0 );

				// Visual feedback for victim (explosion hit effect)
				// Skip visual effects for the thrower (cleaner visuals, but they still take damage)
				if ( m != from )
				{
					m.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
					m.PlaySound( 0x207 );
				}
				}
				else if ( o is BaseExplosionPotion )
				{
					// Chain explosion with nearby potions
					BaseExplosionPotion pot = (BaseExplosionPotion)o;
					pot.Explode( from, false, pot.GetWorldLocation(), pot.Map );
				}
			}
		}

		#endregion
	}
}