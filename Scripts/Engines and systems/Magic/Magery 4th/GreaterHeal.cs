using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.Fourth
{
	public class GreaterHealSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Greater Heal", "In Vas Mani",
				204,
				9061,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		#region Consecutive Cast Tracking
		private const double CONSECUTIVE_CAST_WINDOW = 2.0; // 2 seconds window
		private static readonly System.Collections.Generic.Dictionary<Mobile, DateTime> LastCastTimes = new System.Collections.Generic.Dictionary<Mobile, DateTime>();
		
		/// <summary>
		/// Checks if this is a consecutive cast within the penalty window
		/// </summary>
		private bool IsConsecutiveCast()
		{
			if (!LastCastTimes.ContainsKey(Caster))
				return false;

			DateTime lastCast = LastCastTimes[Caster];
			TimeSpan timeSinceLastCast = DateTime.UtcNow - lastCast;
			
			return timeSinceLastCast.TotalSeconds < CONSECUTIVE_CAST_WINDOW;
		}
		
		/// <summary>
		/// Updates the last cast time for the caster
		/// </summary>
		private void UpdateLastCastTime()
		{
			if (LastCastTimes.ContainsKey(Caster))
				LastCastTimes[Caster] = DateTime.UtcNow;
			else
				LastCastTimes.Add(Caster, DateTime.UtcNow);
		}
		#endregion

		#region Constants
		private const int MSG_COLOR_HEAL = 68; // Light blue for healing
		private const int MSG_COLOR_WARNING = 33;
		private const int OVERHEAD_MESSAGE_HUE = 0x3B2;
		#endregion

		/// <summary>
		/// Displays the amount of hit points healed
		/// </summary>
		private void ShowHealAmount(Mobile target, int healAmount, bool wasConsecutiveCast)
		{
			// Show overhead message on target
			string healText = string.Format(Spell.SpellMessages.INFO_HEAL_AMOUNT_FORMAT, healAmount);
			
			// Add indicator for consecutive cast penalty
			if (wasConsecutiveCast)
			{
				healText += ""; // Warning indicator
			}
			
			target.PublicOverheadMessage(MessageType.Regular, MSG_COLOR_HEAL, false, healText);

			// Also send system message to caster if healing someone else
			if (Caster != target)
			{
				string message = string.Format("Você curou {0} com {1} pontos de vida.", target.Name, healAmount);
				if (wasConsecutiveCast)
				{
					message += " [Cura enfraquecida por uso consecutivo]";
				}
				Caster.SendMessage(MSG_COLOR_HEAL, message);
			}
			else if (wasConsecutiveCast)
			{
				// Self-heal with penalty warning
				Caster.SendMessage(MSG_COLOR_WARNING, "Sua cura foi enfraquecida por uso consecutivo!");
			}
		}

		public GreaterHealSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
                Caster.SendMessage(55, "O alvo n�o pode ser visto.");
            }
			else if (m.IsDeadBondedPet || m is BaseCreature && ((BaseCreature)m).IsAnimatedDead )
			{
                Caster.SendMessage(55, "Voc� n�o pode curar aquilo que j� est� morto.");
            }
			else if ( m is PlayerMobile && m.FindItemOnLayer( Layer.Ring ) != null && m.FindItemOnLayer( Layer.Ring ) is OneRing)
			{
                Caster.SendMessage(33, "O UM ANEL desfez o feiti�o e te diz para n�o fazer isso... ");
                DoFizzle();
                return;
			}
			else if ( m is Golem )
			{
                DoFizzle();
                Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, false, "* N�o sei como curar isso *"); // You cannot heal that.
            }
            else if ((m.Poisoned && m.Poison.Level >= 4) || Server.Items.MortalStrike.IsWounded(m))
            {
                Caster.SendMessage(33, ((Caster == m) ? "Voc� sente o veneno penetrar mais em suas veias." : "O seu alvo est� letalmente envenenado e n�o poder� ser curado com esse feiti�o!"));
                //Caster.LocalOverheadMessage( MessageType.Regular, 0x22, (Caster == m) ? 1005000 : 1010398 );
            }
            else if ( CheckBSequence( m ) )
			{
                SpellHelper.Turn(Caster, m);

                // Use centralized healing calculator with consecutive cast tracking
                bool isConsecutiveCast = IsConsecutiveCast();
                int toHeal = SpellHealingCalculator.CalculateGreaterHeal(Caster, m, isConsecutiveCast);

				SpellHelper.Heal( toHeal, m, Caster );
				
				// Cache spell hue for performance
				int spellHue = Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 );
				m.FixedParticles( 0x376A, 9, 32, 5030, spellHue, 0, EffectLayer.Waist );
				m.PlaySound( 0x202 );
				
				// Show heal amount on target (same as Heal spell)
				ShowHealAmount(m, toHeal, isConsecutiveCast);
				
				// Update last cast time for consecutive cast tracking
				UpdateLastCastTime();
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private GreaterHealSpell m_Owner;

			public InternalTarget( GreaterHealSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}