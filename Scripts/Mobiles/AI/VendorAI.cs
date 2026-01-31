using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	/// <summary>
	/// AI behavior for vendor NPCs.
	/// Handles vendor-specific actions: wandering, customer interaction, combat, and speech keyword recognition.
	/// </summary>
	public class VendorAI : BaseAI
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of VendorAI for the specified creature.
		/// </summary>
		/// <param name="m">The creature this AI controls</param>
		public VendorAI(BaseCreature m) : base(m)
		{
		}
		
		#endregion
		
		#region Core AI Methods
		
		/// <summary>
		/// Handles wandering behavior for vendor NPCs.
		/// Checks for combatants, handles customer interactions, and manages focus mob.
		/// </summary>
		/// <returns>True if action was handled</returns>
		public override bool DoActionWander()
		{
			m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_I_AM_FINE);

			if (m_Mobile.Combatant != null)
			{
				if (m_Mobile.Combatant is PlayerMobile && m_Mobile.Combatant.Criminal)
				{
					m_Mobile.FocusMob = m_Mobile.Combatant;
					Action = ActionType.Combat;
				}
				else
				{
					if (m_Mobile.Debug)
						m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_ATTACKED_BY_FORMAT, m_Mobile.Combatant.Name);

					m_Mobile.Say(Utility.RandomList(VendorAIConstants.SPEECH_ATTACKED_1, VendorAIConstants.SPEECH_ATTACKED_2));

					Action = ActionType.Flee;
				}
			}
			else
			{
				if (m_Mobile.FocusMob != null)
				{
					if (m_Mobile.Debug)
						m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_TALKED_TO_FORMAT, m_Mobile.FocusMob.Name);

					Action = ActionType.Interact;
				}
				else
				{
					m_Mobile.Warmode = false;
					base.DoActionWander();
				}
			}

			return true;
		}

		/// <summary>
		/// Handles interaction behavior with customers.
		/// Validates customer, handles combat interruptions, and manages customer proximity.
		/// </summary>
		/// <returns>True if action was handled</returns>
		public override bool DoActionInteract()
		{
			Mobile customer = m_Mobile.FocusMob;

			if (m_Mobile.Combatant != null)
			{
				if (m_Mobile.Debug)
					m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_ATTACKED_BY_FORMAT, m_Mobile.Combatant.Name);

				m_Mobile.Say(Utility.RandomList(VendorAIConstants.SPEECH_ATTACKED_1, VendorAIConstants.SPEECH_ATTACKED_2));

				Action = ActionType.Flee;
				
				return true;
			}

			if (!IsValidCustomer(customer))
			{
				m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_CUSTOMER_DISAPPEARED);
				m_Mobile.FocusMob = null;
				Action = ActionType.Wander;
			}
			else
			{
				if (customer.InRange(m_Mobile, m_Mobile.RangeFight))
				{
					if (m_Mobile.Debug)
						m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_WITH_CUSTOMER_FORMAT, customer.Name);

					m_Mobile.Direction = m_Mobile.GetDirectionTo(customer);
				}
				else
				{
					if (m_Mobile.Debug)
						m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_CUSTOMER_GONE_FORMAT, customer.Name);

					m_Mobile.FocusMob = null;
					Action = ActionType.Wander;	
				}
			}

			return true;
		}

		/// <summary>
		/// Handles guard behavior for vendor NPCs.
		/// Sets focus mob to combatant and delegates to base guard behavior.
		/// </summary>
		/// <returns>True if action was handled</returns>
		public override bool DoActionGuard()
		{
			m_Mobile.FocusMob = m_Mobile.Combatant;
			return base.DoActionGuard();
		}

		/// <summary>
		/// Handles combat behavior for vendor NPCs.
		/// Validates combatant, manages movement, and checks health for flee conditions.
		/// </summary>
		/// <returns>True if action was handled</returns>
		public override bool DoActionCombat()
		{
			Mobile combatant = m_Mobile.Combatant;

			if (!IsValidCombatant(combatant))
			{
				m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_COMBATANT_GONE);
				Action = ActionType.Wander;
				return true;
			}

			if (WalkMobileRange(combatant, 1, true, m_Mobile.RangeFight, m_Mobile.RangeFight))
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo(combatant);
			}
			else
			{
				if (m_Mobile.GetDistanceToSqrt(combatant) > m_Mobile.RangePerception + 1)
				{
					if (m_Mobile.Debug)
						m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_CANNOT_FIND_FORMAT, combatant.Name);

					Action = ActionType.Wander;
					return true;
				}
				else
				{
					if (m_Mobile.Debug)
						m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_SHOULD_BE_CLOSER_FORMAT, combatant.Name);
				}
			}

			if (!m_Mobile.Controlled && !m_Mobile.Summoned)
			{
				double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;

				if (hitPercent < VendorAIConstants.LOW_HEALTH_THRESHOLD)
				{
					m_Mobile.DebugSay(VendorAIStringConstants.DEBUG_LOW_HEALTH);
					Action = ActionType.Flee;
				}
			}

			return true;
		}
		
		#endregion
		
		#region Speech Handling
		
		/// <summary>
		/// Determines if this AI should handle speech from the specified mobile.
		/// </summary>
		/// <param name="from">The mobile speaking</param>
		/// <returns>True if within speech range</returns>
		public override bool HandlesOnSpeech(Mobile from)
		{
			if (from.InRange(m_Mobile, VendorAIConstants.SPEECH_RANGE))
				return true;

			return base.HandlesOnSpeech(from);
		}

		/// <summary>
		/// Handles speech events for vendor interactions.
		/// Recognizes buy/sell keywords and triggers appropriate vendor actions.
		/// </summary>
		/// <param name="e">Speech event arguments</param>
		public override void OnSpeech(SpeechEventArgs e)
		{
			base.OnSpeech(e);
 
			Mobile from = e.Mobile;
			
			if (m_Mobile is BaseVendor && !e.Handled)
			{
				HandleVendorKeyword(e, from);
			}
		}
		
		#endregion
		
		#region Helper Methods
		
		/// <summary>
		/// Validates if a combatant is still valid (not null, not deleted, same map).
		/// </summary>
		/// <param name="combatant">The combatant to validate</param>
		/// <returns>True if combatant is valid</returns>
		private bool IsValidCombatant(Mobile combatant)
		{
			return combatant != null && !combatant.Deleted && combatant.Map == m_Mobile.Map;
		}

		/// <summary>
		/// Validates if a customer is still valid (not null, not deleted, same map).
		/// </summary>
		/// <param name="customer">The customer to validate</param>
		/// <returns>True if customer is valid</returns>
		private bool IsValidCustomer(Mobile customer)
		{
			return customer != null && !customer.Deleted && customer.Map == m_Mobile.Map;
		}

		/// <summary>
		/// Handles vendor-related speech keywords (buy/sell commands).
		/// </summary>
		/// <param name="e">Speech event arguments</param>
		/// <param name="from">The mobile speaking</param>
		private void HandleVendorKeyword(SpeechEventArgs e, Mobile from)
		{
			if (e.HasKeyword(VendorAIConstants.KEYWORD_VENDOR_SELL) || e.HasKeyword(VendorAIConstants.KEYWORD_SELL))
			{
				e.Handled = true;
				((BaseVendor)m_Mobile).VendorSell(from);
			}
			else if (e.HasKeyword(VendorAIConstants.KEYWORD_VENDOR_BUY) || e.HasKeyword(VendorAIConstants.KEYWORD_BUY))
			{
				e.Handled = true;
				((BaseVendor)m_Mobile).VendorBuy(from);
			}
			else
			{
				base.OnSpeech(e);
			}
		}
		
		#endregion
	}
}
