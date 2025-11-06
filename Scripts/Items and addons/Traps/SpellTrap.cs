using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class SpellTrap : Item
	{
		#region Constants
		// PT-BR Messages
		private const string TRAP_NAME = "uma armadilha mágica";
		private const string MSG_AVOIDED = "Você chegou perto de uma armadilha mágica, mas foi inteligente o suficiente para evitar os efeitos.";
		private const string MSG_TRIGGERED = "Você ativou uma armadilha mágica!";
		private const string MSG_TRIGGERED_FREEZE = "Você ativou uma armadilha congelante!";
		
		// Message Colors
		private const int MSG_COLOR_AVOIDED = 0x3B2;
		private const int MSG_COLOR_TRIGGERED = 0xB1F;
		private const int MSG_COLOR_FREEZE = 0x4F2;
		#endregion

		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		public int power;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Power { get{ return power; } set{ power = value; } }

		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		// Balance Nerf: Duration now 10-120 seconds based on caster's Magery skill
		public virtual TimeSpan DecayDelay
		{ 
			get
			{ 
				if (owner != null)
				{
					double magery = owner.Skills[SkillName.Magery].Value;
					// 10 seconds at 0 skill, 120 seconds at 120 skill
					double seconds = 10 + (magery * 0.917); // 0.917 = 110/120
					return TimeSpan.FromSeconds(seconds);
				}
				return TimeSpan.FromSeconds(10.0);
			} 
		}

	[Constructable]
	public SpellTrap( Mobile source, int level ) : base( 0x0702 )
	{
		ItemID = Utility.RandomList( 0xE68, 0xE65, 0xE62, 0xE5F, 0xE5C );

		Hue = Utility.RandomList( 0x489, 0x490, 0x48F, 0x480, 0x48E, 0x4F2 );
								// FIRE, ENERGY, POISON, COLD, PHYSICAL, FREEZE
		Movable = false;
		Name = TRAP_NAME;
		Light = LightType.Circle300;
		owner = source;
		power = level;
		RefreshDecay( true );
	}

		public SpellTrap(Serial serial) : base(serial)
		{
		}

	public override bool OnMoveOver( Mobile m )
	{
		if ( owner != m )
		{
			// Balance: Skill-based damage scaling
			double magery = owner != null ? owner.Skills[SkillName.Magery].Value : 50;
			int StrMax, StrMin;
			
			if (magery < 50)
			{
				// Low skill: 1-10 damage
				StrMax = 10;
				StrMin = 1;
			}
			else if (magery < 100)
			{
				// Mid skill: 20 min, scales up to 35 max
				StrMin = 20;
				StrMax = 20 + (int)((magery - 50) * 0.3); // 20-35 range
			}
			else
			{
				// High skill: 20-45 damage
				StrMin = 20;
				StrMax = 35 + (int)((magery - 100) * 0.5); // 35-45 at 120 skill
				if (StrMax > 45) StrMax = 45;
			}

				if ( m is PlayerMobile && Spells.Research.ResearchAirWalk.UnderEffect( m ) )
				{
					Point3D air = new Point3D( ( m.X+1 ), ( m.Y+1 ), ( m.Z+5 ) );
					Effects.SendLocationParticles(EffectItem.Create(air, m.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( m, 0 ), 0, 5022, 0);
					m.PlaySound( 0x014 );
				}
				else if (
				( m is PlayerMobile && m.Blessed == false && m.Alive && m.AccessLevel == AccessLevel.Player && Server.Misc.SeeIfGemInBag.GemInPocket( m ) == false && Server.Misc.SeeIfJewelInBag.JewelInPocket( m ) == false ) 
				|| 
				( m is BaseCreature && m.Blessed == false && !(m is PlayerMobile ) ) 
				)
				{
					int Sprung = 1;

					if ( m is PlayerMobile ){ Sprung = Server.Items.HiddenTrap.CheckTrapAvoidance( m, this ); }

				if ( Sprung > 0 )
				{
					if ( m.CheckSkill( SkillName.EvalInt, 0, 125 ) )
					{
						if ( m is PlayerMobile ){ m.LocalOverheadMessage(Network.MessageType.Emote, MSG_COLOR_AVOIDED, false, MSG_AVOIDED); }
						Sprung = 0;
					}
				}

					if ( Sprung > 0 )
					{
						if ( this.Hue == 0x48F ) // POISON TRAP
						{
							int itHurts = m.PoisonResistance;
							int itSicks = 0;

							if ( itHurts >= 70 ){ itSicks = 1; }
							else if ( itHurts >= 50 ){ itSicks = 2; }
							else if ( itHurts >= 30 ){ itSicks = 3; }
							else if ( itHurts >= 10 ){ itSicks = 4; }
							else { itSicks = 5; }

							switch( Utility.RandomMinMax( 1, itSicks ) )
							{
								case 1: m.ApplyPoison( m, Poison.Lesser );	break;
								case 2: m.ApplyPoison( m, Poison.Regular );	break;
								case 3: m.ApplyPoison( m, Poison.Greater );	break;
								case 4: m.ApplyPoison( m, Poison.Deadly );	break;
								case 5: m.ApplyPoison( m, Poison.Lethal );	break;
							}

					Effects.SendLocationEffect( this.Location, this.Map, 0x11A8 - 2, 16, 3, 0, 0 );
					Effects.PlaySound( this.Location, this.Map, 0x231 );
					if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_TRIGGERED, true, MSG_TRIGGERED); }
					itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.PoisonResistance ) ) / 100 );
					m.Damage( itHurts, m );
				}
				else if ( this.Hue == 0x489 ) // FLAME TRAP
				{
					Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
					Effects.PlaySound( this.Location, this.Map, 0x225 );
					if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_TRIGGERED, true, MSG_TRIGGERED); }
					int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.FireResistance ) ) / 100 );
					m.Damage( itHurts, m );
				}
				else if ( this.Hue == 0x48E ) // EXPLOSION TRAP
				{
					m.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
					m.PlaySound( 0x307 );
					if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_TRIGGERED, true, MSG_TRIGGERED); }
					int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.PhysicalResistance ) ) / 100 );
					m.Damage( itHurts, m );
				}
				else if ( this.Hue == 0x490 ) // ELECTRICAL TRAP
				{
					m.BoltEffect( 0 );
					if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_TRIGGERED, true, MSG_TRIGGERED); }
					int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.EnergyResistance ) ) / 100 );
					m.Damage( itHurts, m );
				}
			else if ( this.Hue == 0x480 ) // BLIZZARD TRAP
			{
				Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z );
				Effects.SendLocationEffect( blast, m.Map, 0x375A, 30, 10, 0x481, 0 );
				m.PlaySound( 0x10B );
				if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_TRIGGERED, true, MSG_TRIGGERED); }
					int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.ColdResistance ) ) / 100 );
					m.Damage( itHurts, m );
				}
			else if ( this.Hue == 0x4F2 ) // FREEZE TRAP (NEW!)
			{
				// Cyan/Light Blue trap that freezes (paralyzes) target
				Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z );
				Effects.SendLocationEffect( blast, m.Map, 0x376A, 9, 32, 0x4F2, 0 );
				m.PlaySound( 0x204 ); // Ice sound
				
				if ( m is PlayerMobile )
				{ 
					m.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_FREEZE, true, MSG_TRIGGERED_FREEZE); 
				}
						
					// Calculate freeze duration based on caster's Magery (2-6 seconds)
					double freezeDuration = 2.0;
					if (owner != null)
					{
						// Reuse magery variable from parent scope
						freezeDuration = 2.0 + (magery / 30.0); // 2 seconds at 0, 6 seconds at 120
						if (freezeDuration > 6.0) freezeDuration = 6.0;
					}
						
						// Apply paralyze effect
						m.Paralyze( TimeSpan.FromSeconds( freezeDuration ) );
						m.FixedParticles( 0x376A, 9, 32, 5008, 0x4F2, 0, EffectLayer.Waist );
						
						// Small cold damage
						int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.ColdResistance ) ) / 100 );
						m.Damage( itHurts, m );
					}
				}
				this.Delete();
				}
			}
			return true;
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteDeltaTime( m_DecayTime );
			writer.Write( (Mobile)owner );
			writer.Write( (int)power );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
			owner = reader.ReadMobile();
			power = reader.ReadInt();
		}
	}
}