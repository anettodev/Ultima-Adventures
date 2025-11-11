using System;
using System.Collections.Generic;
using Server.Misc;
using Server.Targeting;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Regions;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Reveal - 6th Circle Magery Spell
	/// Reveals hidden mobiles, traps, and hidden containers in area
	/// </summary>
	public class RevealSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Reveal", "Wis Quas",
				206,
				9002,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		#region Constants

		/// <summary>Base detection range</summary>
		private const int BASE_DETECTION_RANGE = 1;

		/// <summary>Magery skill divisor for detection range</summary>
		private const double MAGERY_RANGE_DIVISOR = 50.0;

		/// <summary>Magery skill divisor for level calculation</summary>
		private const double MAGERY_LEVEL_DIVISOR = 16.0;

		/// <summary>Minimum level</summary>
		private const int MIN_LEVEL = 1;

		/// <summary>Maximum level</summary>
		private const int MAX_LEVEL = 6;

		/// <summary>Minimum money amount</summary>
		private const int MONEY_MIN = 300;

		/// <summary>Maximum money amount</summary>
		private const int MONEY_MAX = 1000;

		/// <summary>Hidden chest reveal chance denominator</summary>
		private const int HIDDEN_CHEST_CHANCE_DENOMINATOR = 3;

		/// <summary>High level threshold</summary>
		private const int HIGH_LEVEL_THRESHOLD = 4;

		/// <summary>Level 5 converted to level</summary>
		private const int LEVEL_5_CONVERTED = 1;

		/// <summary>Level 6 converted to level</summary>
		private const int LEVEL_6_CONVERTED = 2;

		/// <summary>Particle effect ID</summary>
		private const int PARTICLE_EFFECT_ID = 0x376A;

		/// <summary>Particle effect count</summary>
		private const int PARTICLE_COUNT = 9;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_SPEED = 32;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_DURATION = 5024;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x1FA;

		/// <summary>Mobile reveal particle effect ID</summary>
		private const int MOBILE_PARTICLE_EFFECT_ID = 0x375A;

		/// <summary>Mobile reveal particle count</summary>
		private const int MOBILE_PARTICLE_COUNT = 9;

		/// <summary>Mobile reveal particle speed</summary>
		private const int MOBILE_PARTICLE_SPEED = 20;

		/// <summary>Mobile reveal particle duration</summary>
		private const int MOBILE_PARTICLE_DURATION = 5049;

		/// <summary>Mobile reveal sound effect ID</summary>
		private const int MOBILE_SOUND_EFFECT = 0x1FD;

	/// <summary>Reveal chance base multiplier (reduced to reward Hiding/Stealth investment)</summary>
	private const int REVEAL_CHANCE_MULTIPLIER = 20;

	/// <summary>Reveal chance maximum</summary>
	private const int REVEAL_CHANCE_MAX = 100;

		#endregion

		#region Data Structures

		/// <summary>Trap type names mapping</summary>
		private static Dictionary<Type, string> m_TrapTypeNames = new Dictionary<Type, string>
		{
			{ typeof(FireColumnTrap), "(fire column trap)" },
			{ typeof(FlameSpurtTrap), "(fire spurt trap)" },
			{ typeof(GasTrap), "(poison gas trap)" },
			{ typeof(GiantSpikeTrap), "(giant spike trap)" },
			{ typeof(MushroomTrap), "(mushroom trap)" },
			{ typeof(SawTrap), "(saw blade trap)" },
			{ typeof(SpikeTrap), "(spike trap)" },
			{ typeof(StoneFaceTrap), "(stone face trap)" }
		};

		/// <summary>Container trap type names mapping</summary>
		private static Dictionary<TrapType, string> m_ContainerTrapTypeNames = new Dictionary<TrapType, string>
		{
			{ TrapType.MagicTrap, "(magic trap)" },
			{ TrapType.ExplosionTrap, "(explosion trap)" },
			{ TrapType.DartTrap, "(dart trap)" },
			{ TrapType.PoisonTrap, "(poison trap)" }
		};

		#endregion

		public RevealSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( CheckSequence() )
			{
				// Reveal traps and hidden containers
				RevealItems( p );

				// Reveal hidden mobiles
				RevealMobiles( p );
			}

			FinishSequence();
		}

		#region Helper Methods

		/// <summary>
		/// Reveals traps and hidden containers in range
		/// </summary>
		private void RevealItems( IPoint3D p )
		{
			int range = BASE_DETECTION_RANGE + (int)(Caster.Skills[SkillName.Magery].Value / MAGERY_RANGE_DIVISOR);
			IPooledEnumerable itemsInRange = Caster.Map.GetItemsInRange( new Point3D( p ), range );

			foreach ( Item item in itemsInRange )
			{
				if ( item is BaseTrap )
				{
					RevealTrap( (BaseTrap)item );
				}
				else if ( item is HiddenTrap )
				{
					RevealHiddenTrap( item );
				}
				else if ( item is HiddenChest )
				{
					RevealHiddenChest( (HiddenChest)item );
				}
			}

			itemsInRange.Free();
		}

		/// <summary>
		/// Reveals a trap and displays its type
		/// </summary>
		private void RevealTrap( BaseTrap trap )
		{
			string trapName = GetTrapTypeName( trap.GetType() );

			Effects.SendLocationParticles( EffectItem.Create( trap.Location, trap.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION, 0 );
			Effects.PlaySound( trap.Location, trap.Map, SOUND_EFFECT );
			Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Existe uma(s) armadilha(s) próxima a você! " + trapName );
		}

		/// <summary>
		/// Reveals a hidden trap
		/// </summary>
		private void RevealHiddenTrap( Item item )
		{
			Effects.SendLocationParticles( EffectItem.Create( item.Location, item.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION, 0 );
			Effects.PlaySound( item.Location, item.Map, SOUND_EFFECT );
			Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Existe uma armadilha escondida no chão próxima a você!" );
		}

		/// <summary>
		/// Reveals a hidden chest and potentially creates rewards
		/// </summary>
		private void RevealHiddenChest( HiddenChest chest )
		{
			Caster.SendMessage( "Sua intuição mágica percebe que há algo escondido ao seu redor!" );
			string where = Server.Misc.Worlds.GetRegionName( Caster.Map, Caster.Location );

			int money = Utility.RandomMinMax( MONEY_MIN, MONEY_MAX );
			int level = CalculateLevel();

			// Random chance to reveal (33% chance)
			if ( Utility.RandomMinMax( 1, HIDDEN_CHEST_CHANCE_DENOMINATOR ) == 1 )
			{
				if ( level > HIGH_LEVEL_THRESHOLD )
				{
					// Create HiddenBox for high level
					int boxLevel = ( level == 5 ) ? LEVEL_5_CONVERTED : LEVEL_6_CONVERTED;
					HiddenBox mBox = new HiddenBox( boxLevel, where, Caster );

					Point3D loc = chest.Location;
					mBox.MoveToWorld( loc, Caster.Map );
					Effects.SendLocationParticles( EffectItem.Create( mBox.Location, mBox.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION, 0 );
					Effects.PlaySound( mBox.Location, mBox.Map, SOUND_EFFECT );
				}
				else
				{
					// Create gold for lower levels
					Gold coins = new Gold( money * level );

					Point3D loc = chest.Location;
					coins.MoveToWorld( loc, Caster.Map );
					Effects.SendLocationParticles( EffectItem.Create( coins.Location, coins.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION, 0 );
					Effects.PlaySound( coins.Location, coins.Map, SOUND_EFFECT );
				}
			}

			chest.Delete();
		}

		/// <summary>
		/// Calculates the level based on Magery skill
		/// </summary>
		private int CalculateLevel()
		{
			int level = (int)(Caster.Skills[SkillName.Magery].Value / MAGERY_LEVEL_DIVISOR);
			
			// Clamp level between min and max
			if ( level < MIN_LEVEL )
				level = MIN_LEVEL;
			if ( level > MAX_LEVEL )
				level = MAX_LEVEL;

			// Randomize level within calculated range
			return Utility.RandomMinMax( MIN_LEVEL, level );
		}

		/// <summary>
		/// Gets the trap type name from dictionary
		/// </summary>
		private string GetTrapTypeName( Type trapType )
		{
			string name;
			if ( m_TrapTypeNames.TryGetValue( trapType, out name ) )
				return name;
			return "";
		}

		/// <summary>
		/// Gets the container trap type name from dictionary
		/// </summary>
		private string GetContainerTrapTypeName( TrapType trapType )
		{
			string name;
			if ( m_ContainerTrapTypeNames.TryGetValue( trapType, out name ) )
				return name;
			return "";
		}

		/// <summary>
		/// Reveals a container trap and displays its type
		/// </summary>
		private void RevealContainerTrap( TrapableContainer container )
		{
			string trapName = GetContainerTrapTypeName( container.TrapType );
			
			// Check if container is on ground or in inventory
			bool isOnGround = ( container.Parent == null );
			
			if ( isOnGround )
			{
				// Show visual effects and sound at container location
				Effects.SendLocationParticles( 
					EffectItem.Create( container.Location, container.Map, EffectItem.DefaultDuration ), 
					PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, 
					Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION, 0 );
				Effects.PlaySound( container.Location, container.Map, SOUND_EFFECT );
			}
			// If in backpack/inventory, skip visual effects (just show message)
			
			Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Existe uma(s) armadilha(s) próxima a você! " + trapName );
		}

		/// <summary>
		/// Reveals hidden mobiles in range
		/// </summary>
		private void RevealMobiles( IPoint3D p )
		{
			SpellHelper.Turn( Caster, p );
			SpellHelper.GetSurfaceTop( ref p );

			List<Mobile> targets = new List<Mobile>();
			Dictionary<Mobile, int> revealChances = new Dictionary<Mobile, int>();

			Map map = Caster.Map;

			if ( map != null )
			{
				int range = BASE_DETECTION_RANGE + (int)(Caster.Skills[SkillName.Magery].Value / MAGERY_RANGE_DIVISOR);
				IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), range );

				foreach ( Mobile m in eable )
				{
					if ( m.Hidden && (m.AccessLevel == AccessLevel.Player || Caster.AccessLevel > m.AccessLevel) )
					{
						int chance;
						if ( CheckDifficulty( Caster, m, out chance ) )
						{
							targets.Add( m );
							revealChances[m] = chance;
						}
					}
				}

				eable.Free();
			}

			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = targets[i];
				int chance = revealChances[m];

				m.RevealingAction();

				m.FixedParticles( MOBILE_PARTICLE_EFFECT_ID, MOBILE_PARTICLE_COUNT, MOBILE_PARTICLE_SPEED, MOBILE_PARTICLE_DURATION, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Head );
				m.PlaySound( MOBILE_SOUND_EFFECT );

				// Send success messages to both caster and revealed player
				SendRevealSuccessMessages( Caster, m, chance );
			}
		}

		/// <summary>
		/// Sends creative PT-BR messages to caster and revealed player with success rate
		/// </summary>
		private void SendRevealSuccessMessages( Mobile caster, Mobile revealed, int chance )
		{
			// Determine message tone based on chance
			string casterMessage;
			string revealedMessage;

			if ( chance >= 70 )
			{
				// High chance - confident detection
				casterMessage = String.Format( "Sua magia penetra facilmente através da ilusão! Você revelou {0} com {1}% de precisão mágica.", 
					revealed.Name, chance );
				revealedMessage = String.Format( "A magia de {0} rasga sua camuflagem! Você foi revelado com {1}% de chance de detecção.", 
					caster.Name, chance );
			}
			else if ( chance >= 50 )
			{
				// Medium-high chance - good detection
				casterMessage = String.Format( "Sua intuição mágica encontra o alvo! Você revelou {0} com {1}% de eficácia.", 
					revealed.Name, chance );
				revealedMessage = String.Format( "A aura mágica de {0} dissipa sua invisibilidade! Você foi detectado com {1}% de probabilidade.", 
					caster.Name, chance );
			}
			else if ( chance >= 30 )
			{
				// Medium chance - moderate detection
				casterMessage = String.Format( "Com esforço, sua magia encontra o alvo oculto! Você revelou {0} com {1}% de sucesso.", 
					revealed.Name, chance );
				revealedMessage = String.Format( "A magia de {0} consegue penetrar parcialmente sua camuflagem! Você foi revelado com {1}% de chance.", 
					caster.Name, chance );
			}
			else
			{
				// Low chance - lucky detection
				casterMessage = String.Format( "Por sorte, sua magia encontra o alvo! Você revelou {0} com apenas {1}% de chance de sucesso.", 
					revealed.Name, chance );
				revealedMessage = String.Format( "Apesar de sua habilidade, a magia de {0} te encontra! Você foi revelado com {1}% de probabilidade.", 
					caster.Name, chance );
			}

			// Send messages
			caster.SendMessage( Spell.MSG_COLOR_SYSTEM, casterMessage );
			revealed.SendMessage( Spell.MSG_COLOR_ERROR, revealedMessage );
		}

		/// <summary>
		/// Reveal uses DetectHidden vs. Hiding and Stealth skills
		/// Invisibility spell always reveals (100%)
		/// Formula: 50 - ((Hiding - DetectHidden) / 2) - (Stealth / 10), clamped 10-80%
		/// </summary>
		private static bool CheckDifficulty( Mobile from, Mobile m, out int chance )
		{
			// Reveal always reveals vs. invisibility spell
			if ( !Core.AOS || InvisibilitySpell.HasTimer( m ) )
			{
				chance = 100;
				return true;
			}

			int detectHidden = from.Skills[SkillName.DetectHidden].Fixed;
			int hiding = m.Skills[SkillName.Hiding].Fixed;
			int stealth = m.Skills[SkillName.Stealth].Fixed;

			// Formula: 50 - ((Hiding - DetectHidden) / 2) - (Stealth / 10)
			// Hiding impact: each point difference = 0.5%
			// Stealth impact: each 10 points = -1%
			chance = 50 - ((hiding - detectHidden) / 2) - (stealth / 10);

			// Clamp between 10% (minimum) and 80% (maximum)
			if ( chance < 10 )
				chance = 10;
			if ( chance > 80 )
				chance = 80;

			return chance > Utility.Random( 100 );
		}

		#endregion

		#region Internal Classes

		private class InternalTarget : Target
		{
			private RevealSpell m_Owner;

			public InternalTarget( RevealSpell owner ) : base( SpellConstants.GetSpellRange(), true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				// Check if target is a container (1-to-1 detection)
				TrapableContainer container = o as TrapableContainer;
				if ( container != null )
				{
					// Check if container has a trap
					if ( container.TrapType != TrapType.None )
					{
						m_Owner.RevealContainerTrap( container );
					}
					
					// Continue with area-based reveal ONLY if container is on ground
					// (containers in backpacks don't trigger area reveal)
					if ( container.Parent == null )
					{
						IPoint3D containerLocation = container as IPoint3D;
						if ( containerLocation != null )
							m_Owner.Target( containerLocation );
					}
					return;
				}
				
				// Existing behavior: area-based reveal for ground/location targets
				IPoint3D p = o as IPoint3D;
				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}
