using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells.Fifth;

namespace Server.Spells.First
{
	public class NightSightSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Night Sight", "In Lor",
				236,
				9031,
				Reagent.SulfurousAsh,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

	#region Sense Mode Constants
	// Wealth Sense
	private const double WEALTH_SENSE_CHANCE_PER_MAGERY = 0.2;
	private const double WEALTH_SENSE_CHANCE_PER_TASTE_ID = 0.1;

	// Trap Awareness
	private const double TRAP_SENSE_CHANCE_PER_MAGERY = 0.2;
	private const double TRAP_SENSE_CHANCE_PER_REMOVE_TRAP = 0.1;

	// Danger Sense
	private const double DANGER_SENSE_CHANCE_PER_MAGERY = 0.2;
	private const double DANGER_SENSE_CHANCE_PER_FORENSICS = 0.1;
	private const int DANGER_SENSE_RANGE = 3;
	private const double DANGER_SENSE_CHECK_INTERVAL_SECONDS = 5.0;

	// Aura Sense
	private const double AURA_SENSE_CHANCE_PER_MAGERY = 0.2;
	private const double AURA_SENSE_CHANCE_PER_SPIRIT_SPEAK = 0.1;

	// Messages
	private const string MSG_WEALTH_VALUABLE = "Você sente {0} itens dentro. Parece conter riquezas valiosas!";
	private const string MSG_WEALTH_NOTHING = "Você sente {0} itens dentro. Nada de valor aparente.";
	private const string MSG_WEALTH_EMPTY = "O recipiente parece vazio.";
	private const string MSG_TRAP_DETECTED = "Sua visão aguçada nota algo suspeito...";
	private const string MSG_DANGER_SENSE = "Você sente algo nas sombras ou no ar...";
	private const string MSG_AURA_SENSE = "{0} emana uma aura de {1}...";
	private const string MSG_SENSE_MODE_ACTIVATED = "Seus sentidos estão aguçados. Você pode sentir riquezas, perigos e auras.";
	private const string MSG_SENSE_FAILED = "Você falhou em sentir isto.";
	private const string MSG_SENSE_DISTURBED = "Uma perturbação nas forças místicas confundiu seus sentidos. A visão noturna se dissipa...";
	private const string MSG_INVALID_SENSE_TARGET = "Você não pode sentir este item.";
	
	// Message Colors
	private const int MSG_COLOR_YELLOW = 0x35; // Yellow for wealth
	private const int MSG_COLOR_PURPLE = 0x16; // Purple/Magenta for auras
		
		// Visual Effects - Sense Mode
		private const int SENSE_PARTICLE_EFFECT = 0x375A;
		private const int SENSE_PARTICLE_HUE = 1153;
		private const int SENSE_PARTICLE_SPEED = 10;
		private const int SENSE_PARTICLE_DURATION = 20;
		private const double SENSE_PARTICLE_INTERVAL_SECONDS = 4.0;
		private const int SENSE_SOUND_ACTIVATE = 0x1ED;
		
		// Buff Icon
		private const int SENSE_BUFF_ICON_ID = 1075643; // Night Sight icon with custom message
		#endregion

		#region Static Data
		// Tracks who has Sense Mode active (caster, not target)
		private static Dictionary<Mobile, Mobile> m_SenseModeActive = new Dictionary<Mobile, Mobile>();
		
		// Tracks active sense targets for cleanup
		private static Dictionary<Mobile, SenseTarget> m_ActiveTargets = new Dictionary<Mobile, SenseTarget>();
		
		// Tracks visual effect timers for cleanup
		private static Dictionary<Mobile, Timer> m_SenseEffectTimers = new Dictionary<Mobile, Timer>();
		#endregion

		public NightSightSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new NightSightTarget( this );
		}

		/// <summary>
		/// Checks if a mobile has Sense Mode active
		/// </summary>
		public static bool HasSenseMode(Mobile m)
		{
			return m_SenseModeActive.ContainsKey(m);
		}

		/// <summary>
		/// Activates Sense Mode for a mobile
		/// </summary>
		public static void ActivateSenseMode(Mobile m)
		{
			if (!m_SenseModeActive.ContainsKey(m))
			{
				m_SenseModeActive[m] = m;
				
				// Visual activation feedback
				m.FixedParticles(SENSE_PARTICLE_EFFECT, 1, 30, 9964, SENSE_PARTICLE_HUE, 3, EffectLayer.Head);
				m.PlaySound(SENSE_SOUND_ACTIVATE);
				m.SendMessage(Spell.MSG_COLOR_HEAL, MSG_SENSE_MODE_ACTIVATED);
				
				// Add buff icon
				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.NightSight, 1075643, 1075644, "Modo Sensitivo Ativo\nVocê pode sentir riquezas, perigos e auras."));
				
				// Start periodic particle effects
				Timer effectTimer = new SenseEffectTimer(m);
				effectTimer.Start();
				m_SenseEffectTimers[m] = effectTimer;
				
				// Set targeting cursor
				SenseTarget target = new SenseTarget(m);
				m_ActiveTargets[m] = target;
				m.Target = target;
			}
		}

		/// <summary>
		/// Deactivates Sense Mode for a mobile
		/// </summary>
		/// <param name="m">Mobile to deactivate</param>
		/// <param name="cancelSpell">If true, also cancels the Night Sight spell effect</param>
		public static void DeactivateSenseMode(Mobile m, bool cancelSpell = false)
		{
			if (m_SenseModeActive.ContainsKey(m))
			{
				m_SenseModeActive.Remove(m);
			}
			
			// Cancel active target cursor - force cancellation
			if (m_ActiveTargets.ContainsKey(m))
			{
				SenseTarget target = m_ActiveTargets[m];
				// Explicitly cancel the target if it's still active
				if (m.Target == target)
				{
					target.Cancel(m, TargetCancelType.Canceled);
					m.Target = null;
				}
				m_ActiveTargets.Remove(m);
			}
			else
			{
				// Also check if current target is a SenseTarget (in case dictionary is out of sync)
				if (m.Target is SenseTarget)
				{
					((SenseTarget)m.Target).Cancel(m, TargetCancelType.Canceled);
					m.Target = null;
				}
			}
			
			// Stop periodic effect timer
			if (m_SenseEffectTimers.ContainsKey(m))
			{
				Timer timer = m_SenseEffectTimers[m];
				if (timer != null && timer.Running)
				{
					timer.Stop();
				}
				m_SenseEffectTimers.Remove(m);
			}
			
			// Remove buff icon
			BuffInfo.RemoveBuff(m, BuffIcon.NightSight);
			
			// Cancel the Night Sight spell if requested
			if (cancelSpell)
			{
				m.EndAction(typeof(LightCycle));
				m.LightLevel = 0;
			}
		}

		private class NightSightTarget : Target
		{
			private Spell m_Spell;

			public NightSightTarget( Spell spell ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Spell = spell;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile && m_Spell.CheckBSequence( (Mobile) targeted ) )
				{
					Mobile targ = (Mobile)targeted;

					SpellHelper.Turn( m_Spell.Caster, targ );

					if ( targ.BeginAction( typeof( LightCycle ) ) )
					{
						// Calculate duration using NMSGetDuration
						TimeSpan duration = SpellHelper.NMSGetDuration(from, targ, true);
						
						// Start timer with calculated duration
						new EnhancedNightSightTimer( targ, from, duration ).Start();
						
						int level = (int)( LightCycle.DungeonLevel * ( (Core.AOS ? targ.Skills[SkillName.Magery].Value : from.Skills[SkillName.Magery].Value )/ 100 ) );

						if ( level < 0 )
							level = 0;

						targ.LightLevel = level;

						targ.FixedParticles( 0x376A, 9, 32, 5007, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0, EffectLayer.Waist );
						targ.PlaySound( 0x1E3 );

						BuffInfo.AddBuff( targ, new BuffInfo( BuffIcon.NightSight, 1075643 ) );	//Night Sight/You ignore lighting effects
					}
					else
					{
						// Already has Night Sight - check if re-casting on self for Sense Mode
						if (from == targ)
						{
							// Re-casting on self - activate Sense Mode
							NightSightSpell.ActivateSenseMode(from);
						}
						else
						{
							// Re-casting on friend - just notify
							from.SendMessage( "Ele já possui visão noturna." );
						}
					}
				}

				m_Spell.FinishSequence();
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Spell.FinishSequence();
			}
		}

		#region Enhanced Night Sight Timer
		private class EnhancedNightSightTimer : Timer
		{
			private Mobile m_Target;
			private Mobile m_Caster;
			private DateTime m_End;
			private DateTime m_NextDangerCheck;

			public EnhancedNightSightTimer(Mobile target, Mobile caster, TimeSpan duration) : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
			{
				m_Target = target;
				m_Caster = caster;
				m_End = DateTime.UtcNow + duration;
				m_NextDangerCheck = DateTime.UtcNow + TimeSpan.FromSeconds(DANGER_SENSE_CHECK_INTERVAL_SECONDS);
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if (DateTime.UtcNow >= m_End)
				{
					// Timer expired - clean up
					m_Target.EndAction(typeof(LightCycle));
					m_Target.LightLevel = 0;
					BuffInfo.RemoveBuff(m_Target, BuffIcon.NightSight);
					
					// Deactivate Sense Mode if active
					NightSightSpell.DeactivateSenseMode(m_Target);
					
					Stop();
					return;
				}

				// Danger Sense check (only for caster who cast on themselves)
				if (m_Target == m_Caster && DateTime.UtcNow >= m_NextDangerCheck)
				{
					PerformDangerSenseCheck();
					m_NextDangerCheck = DateTime.UtcNow + TimeSpan.FromSeconds(DANGER_SENSE_CHECK_INTERVAL_SECONDS);
				}
			}

			private void PerformDangerSenseCheck()
			{
				if (m_Target == null || m_Target.Deleted || m_Target.Map == null)
					return;

				// Calculate chance
				double mageryChance = m_Caster.Skills[SkillName.Magery].Value * DANGER_SENSE_CHANCE_PER_MAGERY;
				double forensicsBonus = m_Caster.Skills[SkillName.Forensics].Value * DANGER_SENSE_CHANCE_PER_FORENSICS;
				double totalChance = mageryChance + forensicsBonus;

				if (Utility.RandomDouble() * 100.0 >= totalChance)
					return;

				// Check for hidden hostiles within range
				IPooledEnumerable eable = m_Target.GetMobilesInRange(DANGER_SENSE_RANGE);
				bool foundDanger = false;

				try
				{
					foreach (Mobile m in eable)
					{
						if (m == m_Target || !m.Hidden || !m.Alive)
							continue;

						// Check if mobile is hostile
						if (m is BaseCreature)
						{
							BaseCreature bc = (BaseCreature)m;
							if (bc.Controlled && bc.ControlMaster == m_Target)
								continue; // Skip own pets

							foundDanger = true;
							break;
						}
						else if (m.Player)
						{
							// Hidden player nearby
							foundDanger = true;
							break;
						}
					}
				}
				finally
				{
					eable.Free();
				}

				if (foundDanger)
				{
					m_Target.SendMessage(Spell.MSG_COLOR_WARNING, MSG_DANGER_SENSE);
				}
			}
		}
		#endregion

		#region Sense Effect Timer
		private class SenseEffectTimer : Timer
		{
			private Mobile m_Mobile;

			public SenseEffectTimer(Mobile m) : base(TimeSpan.FromSeconds(SENSE_PARTICLE_INTERVAL_SECONDS), TimeSpan.FromSeconds(SENSE_PARTICLE_INTERVAL_SECONDS))
			{
				m_Mobile = m;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				if (m_Mobile == null || m_Mobile.Deleted || !NightSightSpell.HasSenseMode(m_Mobile))
				{
					Stop();
					return;
				}

				// Periodic subtle particle effect to show Sense Mode is active
				m_Mobile.FixedParticles(SENSE_PARTICLE_EFFECT, 1, 15, 9964, SENSE_PARTICLE_HUE, 0, EffectLayer.Waist);
			}
		}
		#endregion

		#region Sense Target
		private class SenseTarget : Target
		{
			private Mobile m_Caster;

			public SenseTarget(Mobile caster) : base(12, false, TargetFlags.Beneficial)
			{
				m_Caster = caster;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (!NightSightSpell.HasSenseMode(from))
					return;

				bool handled = false;

				if (targeted is Container)
				{
					HandleContainerSense((Container)targeted);
					handled = true;
				}
				else if (targeted is Mobile)
				{
					HandleAuraSense((Mobile)targeted);
					handled = true;
				}

				if (!handled)
				{
					from.SendMessage(Spell.MSG_COLOR_WARNING, MSG_INVALID_SENSE_TARGET);
				}

				// Allow re-targeting
				SenseTarget newTarget = new SenseTarget(from);
				m_ActiveTargets[from] = newTarget;
				from.Target = newTarget;
			}

		private void HandleContainerSense(Container container)
		{
			bool successfullySensed = false;

			// Calculate Wealth Sense chance
			double mageryChance = m_Caster.Skills[SkillName.Magery].Value * WEALTH_SENSE_CHANCE_PER_MAGERY;
			double tasteBonus = m_Caster.Skills[SkillName.TasteID].Value * WEALTH_SENSE_CHANCE_PER_TASTE_ID;
			double totalChance = mageryChance + tasteBonus;

			if (Utility.RandomDouble() * 100.0 < totalChance)
			{
				// Wealth Sense success
				int itemCount = container.Items.Count;
				bool hasValuables = false;

				foreach (Item item in container.Items)
				{
					// Check for gold, jewelry, or any gem types
					if (item is Gold || item is BaseJewel)
					{
						hasValuables = true;
						break;
					}
					
					// Check for common gem types (no base class exists)
					Type itemType = item.GetType();
					string typeName = itemType.Name;
					if (typeName == "Amber" || typeName == "Amethyst" || typeName == "Citrine" || 
					    typeName == "Diamond" || typeName == "Emerald" || typeName == "Ruby" || 
					    typeName == "Sapphire" || typeName == "StarSapphire" || typeName == "Tourmaline")
					{
						hasValuables = true;
						break;
					}
				}

				if (itemCount == 0)
				{
					// Gray for empty
					m_Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, MSG_WEALTH_EMPTY);
				}
				else if (hasValuables)
				{
					// Yellow for success/valuables
					m_Caster.SendMessage(MSG_COLOR_YELLOW, string.Format(MSG_WEALTH_VALUABLE, itemCount));
				}
				else
				{
					// Gray for nothing valuable
					m_Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, string.Format(MSG_WEALTH_NOTHING, itemCount));
				}
				
				successfullySensed = true;
			}

			// Trap Awareness (skill-based detection)
			TrapableContainer trapContainer = container as TrapableContainer;
			if (trapContainer != null && trapContainer.TrapType != TrapType.None)
			{
				// Calculate Trap Sense chance
				double trapMageryChance = m_Caster.Skills[SkillName.Magery].Value * TRAP_SENSE_CHANCE_PER_MAGERY;
				double removeTrapBonus = m_Caster.Skills[SkillName.RemoveTrap].Value * TRAP_SENSE_CHANCE_PER_REMOVE_TRAP;
				double trapTotalChance = trapMageryChance + removeTrapBonus;

				if (Utility.RandomDouble() * 100.0 < trapTotalChance)
				{
					// Orange warning for trap
					m_Caster.SendMessage(Spell.MSG_COLOR_WARNING, MSG_TRAP_DETECTED);
					m_Caster.PlaySound(0x1F5); // Danger sound
					successfullySensed = true;
				}
			}

			// If any sense failed, cancel the entire spell
			if (!successfullySensed)
			{
				m_Caster.SendMessage(Spell.MSG_COLOR_ERROR, MSG_SENSE_FAILED);
				m_Caster.SendMessage(Spell.MSG_COLOR_WARNING, MSG_SENSE_DISTURBED);
				
				// Cancel Night Sight spell and deactivate Sense Mode
				NightSightSpell.DeactivateSenseMode(m_Caster, cancelSpell: true);
			}
		}

		private void HandleAuraSense(Mobile target)
		{
			if (target == m_Caster)
				return;

			// Calculate Aura Sense chance
			double mageryChance = m_Caster.Skills[SkillName.Magery].Value * AURA_SENSE_CHANCE_PER_MAGERY;
			double spiritBonus = m_Caster.Skills[SkillName.SpiritSpeak].Value * AURA_SENSE_CHANCE_PER_SPIRIT_SPEAK;
			double totalChance = mageryChance + spiritBonus;

			if (Utility.RandomDouble() * 100.0 < totalChance)
			{
				// Detect active buffs/debuffs
				List<string> auras = new List<string>();

				// Check for common buffs (this is simplified - expand as needed)
				if (target.Poisoned)
					auras.Add("veneno");
				if (target.Paralyzed)
					auras.Add("paralisia");
				if (target.Blessed)
					auras.Add("proteção divina");
				
				// Check for Magic Reflection
				if (MagicReflectSpell.HasActiveShield(target))
					auras.Add("reflexão mágica");

				if (auras.Count > 0)
				{
					string auraList = string.Join(", ", auras.ToArray());
					// Purple for aura detection success
					m_Caster.SendMessage(MSG_COLOR_PURPLE, string.Format(MSG_AURA_SENSE, target.Name, auraList));
				}
				else
				{
					// Gray for no auras
					m_Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, target.Name + " não parece ter auras ativas detectáveis.");
				}
			}
			else
			{
				// Aura sense failed - cancel spell
				m_Caster.SendMessage(Spell.MSG_COLOR_ERROR, MSG_SENSE_FAILED);
				m_Caster.SendMessage(Spell.MSG_COLOR_WARNING, MSG_SENSE_DISTURBED);
				
				// Cancel Night Sight spell and deactivate Sense Mode
				NightSightSpell.DeactivateSenseMode(m_Caster, cancelSpell: true);
			}
		}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				// User cancelled - deactivate Sense Mode
				NightSightSpell.DeactivateSenseMode(from);
			}
		}
		#endregion
	}
}
