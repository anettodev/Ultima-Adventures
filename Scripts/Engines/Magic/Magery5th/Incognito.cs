using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Targeting;

namespace Server.Spells.Fifth
{
	/// <summary>
	/// Incognito - 5th Circle Utility Spell
	/// Disguises the caster as another player or creature
	/// Changes: Name, Gender, Hue, Hair, Facial Hair
	/// Cannot be used with Polymorph, Disguise Kit, or Body Paint
	/// Duration based on Magery and Evaluate Intelligence skills
	/// </summary>
	public class IncognitoSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Incognito", "Kal In Ex",
				206,
				9002,
				Reagent.Bloodmoss,
				Reagent.Garlic,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		#region Constants

		// Body modification IDs
		/// <summary>Body paint BodyMod ID (female)</summary>
		private const int BODY_PAINT_FEMALE = 183;

		/// <summary>Body paint BodyMod ID (male)</summary>
		private const int BODY_PAINT_MALE = 184;

		// Effect constants - Transform (start)
		/// <summary>Transform particle effect ID</summary>
		private const int TRANSFORM_PARTICLE_EFFECT = 0x373A;

		/// <summary>Transform particle speed</summary>
		private const int TRANSFORM_PARTICLE_SPEED = 10;

		/// <summary>Transform particle duration</summary>
		private const int TRANSFORM_PARTICLE_DURATION = 15;

		/// <summary>Transform particle hue offset</summary>
		private const int TRANSFORM_PARTICLE_HUE = 5036;

		/// <summary>Transform sound effect</summary>
		private const int TRANSFORM_SOUND = 0x3BD;

		// Effect constants - Revert (end)
		/// <summary>Revert particle effect ID</summary>
		private const int REVERT_PARTICLE_EFFECT = 0x376A;

		/// <summary>Revert particle speed</summary>
		private const int REVERT_PARTICLE_SPEED = 9;

		/// <summary>Revert particle duration</summary>
		private const int REVERT_PARTICLE_DURATION = 32;

		/// <summary>Revert particle hue offset</summary>
		private const int REVERT_PARTICLE_HUE = 5008;

		/// <summary>Revert initial sound effect</summary>
		private const int REVERT_SOUND_INITIAL = 0x1EA;

		// Audio constants
		/// <summary>Female "oops" sound when disguise ends</summary>
		private const int FEMALE_OOPS_SOUND = 812;

		/// <summary>Male "oops" sound when disguise ends</summary>
		private const int MALE_OOPS_SOUND = 1086;

		// Buff constants
		/// <summary>Incognito buff localized message ID</summary>
		private const int BUFF_CLILOC_ID = 1075819;

		// Hardcoded strings
		/// <summary>Fake title shown while disguised</summary>
		private const string FAKE_TITLE = "[fake]";

		/// <summary>Spoken message when disguise ends</summary>
		private const string OOPS_MESSAGE = "*oops*";

		#endregion

		#region Appearance Data Storage

		/// <summary>
		/// Stores original appearance data for each disguised mobile
		/// Thread-safe storage for multiple simultaneous incognito effects
		/// </summary>
		private class AppearanceData
		{
			public bool OriginalGenderFemale { get; set; }
			public int OriginalHue { get; set; }
			public int OriginalHairHue { get; set; }
			public int OriginalFacialHairHue { get; set; }
		}

	private static Dictionary<Mobile, AppearanceData> m_AppearanceData = new Dictionary<Mobile, AppearanceData>();
	
	/// <summary>
	/// Backwards-compatible timer dictionary wrapper
	/// Provides Contains() method for legacy code (was Hashtable, now Dictionary)
	/// </summary>
	public class TimerDictionary : Dictionary<Mobile, Timer>
	{
		public bool Contains(Mobile key)
		{
			return ContainsKey(key);
		}
	}
	
	public static TimerDictionary m_Timers = new TimerDictionary();

		#endregion

		public IncognitoSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			// Store original appearance data before transformation
			AppearanceData data = new AppearanceData
			{
				OriginalGenderFemale = Caster.Female,
				OriginalHairHue = Caster.HairHue,
				OriginalFacialHairHue = Caster.FacialHairHue,
				OriginalHue = Caster.Hue
			};
			m_AppearanceData[Caster] = data;

			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_INCOGNITO_SELECT_TARGET);
			Caster.Target = new InternalTarget(this);
		}

		/// <summary>
		/// Validates target and applies incognito disguise
		/// </summary>
		/// <param name="m">Target mobile to mimic</param>
		public void Target(Mobile m)
		{
			if (!Caster.CanSee(m))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (!CanActivateIncognito())
			{
				// Error message already sent by validation method
			}
			else if (!Caster.CanBeginAction(typeof(PolymorphSpell)) || Caster.IsBodyMod)
			{
				DoFizzle();
			}
			else if (IsValidIncognitoTarget(m) && CheckBSequence(m))
			{
				if (Caster.BeginAction(typeof(IncognitoSpell)))
				{
					ApplyIncognitoDisguise(m);
				}
				else
				{
					Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_INCOGNITO_ALREADY_ACTIVE);
				}
			}
			else
			{
				DoFizzle();
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_INCOGNITO_INVALID_TARGET);
			}
			FinishSequence();
		}

		/// <summary>
		/// Validates if caster can activate incognito
		/// Checks: already active, body paint, existing disguise
		/// </summary>
		/// <returns>True if incognito can be activated</returns>
		private bool CanActivateIncognito()
		{
			if (!Caster.CanBeginAction(typeof(IncognitoSpell)))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_INCOGNITO_ALREADY_ACTIVE);
				return false;
			}

			if (IsWearingBodyPaint())
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_INCOGNITO_BODY_PAINT);
				return false;
			}

			if (DisguiseTimers.IsDisguised(Caster))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_INCOGNITO_ALREADY_DISGUISED);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if caster is wearing body paint (cannot use incognito)
		/// </summary>
		/// <returns>True if wearing body paint</returns>
		private bool IsWearingBodyPaint()
		{
			return Caster.BodyMod == BODY_PAINT_FEMALE || Caster.BodyMod == BODY_PAINT_MALE;
		}

		/// <summary>
		/// Validates if target is valid for incognito
		/// Must be PlayerMobile or BaseCreature with same body type as caster
		/// </summary>
		/// <param name="target">Target mobile</param>
		/// <returns>True if target is valid</returns>
		private bool IsValidIncognitoTarget(Mobile target)
		{
			return (target is PlayerMobile || target is BaseCreature) && 
			       (target.Body.Type == Caster.Body.Type);
		}

		/// <summary>
		/// Applies incognito disguise to caster
		/// Copies appearance from target and starts duration timer
		/// </summary>
		/// <param name="target">Target to mimic</param>
		private void ApplyIncognitoDisguise(Mobile target)
		{
			DisguiseTimers.StopTimer(Caster);
			PlayerMobile pm = Caster as PlayerMobile;

			if (pm != null && pm.Race != null)
			{
				// Copy target's appearance
				pm.HueMod = target.HueMod;
				pm.NameMod = target.Name;
				pm.Title = FAKE_TITLE;
				pm.SetHairMods(target.HairItemID, target.FacialHairItemID);
				pm.Female = target.Female;
				pm.HairHue = target.HairHue;
				pm.Hue = target.Hue;
				pm.FacialHairHue = target.FacialHairHue;
			}

			// Play transformation effects
			PlayTransformEffects();

			// Revalidate equipment appearance
			BaseArmor.ValidateMobile(Caster);
			BaseClothing.ValidateMobile(Caster);

			// Start duration timer
			StopTimer(Caster);
			TimeSpan length = SpellHelper.NMSGetDuration(Caster, Caster, false);
			Timer t = new InternalTimer(Caster, length);
			m_Timers[Caster] = t;
			t.Start();

			// Add buff icon
			BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.Incognito, BUFF_CLILOC_ID, length, Caster));
		}

		/// <summary>
		/// Plays visual and audio effects when transforming into disguise
		/// </summary>
		private void PlayTransformEffects()
		{
			Caster.FixedParticles(
				TRANSFORM_PARTICLE_EFFECT, 
				TRANSFORM_PARTICLE_SPEED, 
				TRANSFORM_PARTICLE_DURATION, 
				TRANSFORM_PARTICLE_HUE, 
				Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0), 
				0, 
				EffectLayer.Head
			);
			Caster.PlaySound(TRANSFORM_SOUND);
		}

		/// <summary>
		/// Validates if caster can cast incognito
		/// Checks: already active, body paint
		/// </summary>
		/// <param name="caster">Spell caster</param>
		/// <returns>True if cast can proceed</returns>
		public override bool CheckCast(Mobile caster)
		{
			if (!Caster.CanBeginAction(typeof(IncognitoSpell)))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_INCOGNITO_ALREADY_ACTIVE);
				return false;
			}

			if (IsWearingBodyPaint())
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_INCOGNITO_BODY_PAINT);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Stops incognito timer and removes buff
		/// Public static method for external systems to cancel incognito
		/// </summary>
		/// <param name="m">Mobile to remove incognito from</param>
		/// <returns>True if timer was found and stopped</returns>
		public static bool StopTimer(Mobile m)
		{
			Timer t;
			if (m_Timers.TryGetValue(m, out t))
			{
				if (t != null)
				{
					t.Stop();
				}
				m_Timers.Remove(m);
				BuffInfo.RemoveBuff(m, BuffIcon.Incognito);
				return true;
			}

			return false;
		}

		#region Timer and Target Classes

		/// <summary>
		/// Timer that restores original appearance when incognito expires
		/// </summary>
		private class InternalTimer : Timer
		{
			private Mobile m_Owner;

			public InternalTimer(Mobile owner, TimeSpan length) : base(length)
			{
				m_Owner = owner;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if (!m_Owner.CanBeginAction(typeof(IncognitoSpell)))
				{
					RestoreOriginalAppearance();
					PlayRevertEffects();
					m_Owner.EndAction(typeof(IncognitoSpell));
				}
			}

			/// <summary>
			/// Restores caster's original appearance from stored data
			/// </summary>
			private void RestoreOriginalAppearance()
			{
				AppearanceData data;
				if (m_AppearanceData.TryGetValue(m_Owner, out data))
				{
					if (m_Owner is PlayerMobile)
						((PlayerMobile)m_Owner).SetHairMods(-1, -1);

					m_Owner.BodyMod = 0;
					m_Owner.HueMod = -1;
					m_Owner.NameMod = null;
					m_Owner.Title = null;
					m_Owner.HairHue = data.OriginalHairHue;
					m_Owner.FacialHairHue = data.OriginalFacialHairHue;
					m_Owner.Female = data.OriginalGenderFemale;
					m_Owner.Hue = data.OriginalHue;

					m_AppearanceData.Remove(m_Owner);
				}

				BaseArmor.ValidateMobile(m_Owner);
				BaseClothing.ValidateMobile(m_Owner);
			}

			/// <summary>
			/// Plays visual and audio effects when disguise ends
			/// </summary>
			private void PlayRevertEffects()
			{
				m_Owner.PlaySound(REVERT_SOUND_INITIAL);
				m_Owner.FixedParticles(
					REVERT_PARTICLE_EFFECT, 
					REVERT_PARTICLE_SPEED, 
					REVERT_PARTICLE_DURATION, 
					REVERT_PARTICLE_HUE, 
					Server.Items.CharacterDatabase.GetMySpellHue(m_Owner, 0), 
					0, 
					EffectLayer.Waist
				);

				m_Owner.PlaySound(m_Owner.Female ? FEMALE_OOPS_SOUND : MALE_OOPS_SOUND);
				m_Owner.Say(OOPS_MESSAGE);
			}
		}

		private class InternalTarget : Target
		{
			private IncognitoSpell m_Owner;

			public InternalTarget(IncognitoSpell owner) : base(SpellConstants.GetSpellRange(), false, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Mobile)
				{
					m_Owner.Target((Mobile)o);
				}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}
