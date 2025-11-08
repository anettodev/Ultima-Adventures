using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Third
{
	/// <summary>
	/// Teleport - 3rd Circle Movement Spell
	/// Instantly transports the caster to a targeted location
	/// </summary>
	public class TeleportSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Teleport", "Rel Por",
				215,
				9031,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Detection and Validation
		private const int CURSED_CREATURE_SCAN_RANGE = 10;
		private const int KARMA_THRESHOLD_FOR_CURSED_CHECK = -5000;

		// Effects Constants
		private const int PLAYER_EFFECT_ID = 0x3728;
		private const int PLAYER_EFFECT_SPEED = 10;
		private const int PLAYER_EFFECT_RENDER = 10;
		private const int PLAYER_FROM_DURATION = 2023;
		private const int PLAYER_TO_DURATION = 5023;
		private const int NPC_EFFECT_ID = 0x376A;
		private const int NPC_EFFECT_SPEED = 9;
		private const int NPC_EFFECT_RENDER = 32;
		private const int NPC_EFFECT_DURATION = 0x13AF;
		private const int SOUND_ID = 0x1FE;
		private const int DEFAULT_HUE = 0;

		// Target Constants
		private const int TARGET_RANGE_ML = 11;
		private const int TARGET_RANGE_LEGACY = 12;

		// Pet Teleport
		private const double PET_TELEPORT_CHANCE = 0.5; // 50%
		#endregion

		#region Validation Result
		/// <summary>
		/// Represents the result of teleport validation
		/// </summary>
		private enum ValidationResult
		{
			Success,
			CursedCreaturesNearby,
			Overloaded,
			InvalidMap,
			CannotTravelFrom,
			CannotTravelTo,
			LocationBlocked
		}
		#endregion

		public TeleportSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if (IsOverloaded())
			{
				Caster.SendMessage(Spell.MSG_COLOR_WARNING, Spell.SpellMessages.ERROR_TOO_HEAVY_TELEPORT);
				return false;
			}

			return SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom);
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target(IPoint3D targetPoint)
		{
			IPoint3D originalTarget = targetPoint;
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop(ref targetPoint);

			ValidationResult validation = ValidateTeleport(targetPoint, map);

			if (validation != ValidationResult.Success)
			{
				HandleValidationFailure(validation);
				FinishSequence();
				return;
			}

			if (CheckSequence())
			{
				Point3D destination = new Point3D(targetPoint);
				TryTeleportPets(targetPoint, map);
				SpellHelper.Turn(Caster, originalTarget);

				Point3D from = Caster.Location;
				Caster.Location = destination;
				Caster.ProcessDelta();

				PlayTeleportEffects(from, destination);
				TriggerFieldItems();
			}

			FinishSequence();
		}

		/// <summary>
		/// Validates all teleport conditions in optimal order for performance
		/// Fast checks are performed first, expensive checks last
		/// </summary>
		/// <param name="targetPoint">Destination point to validate</param>
		/// <param name="map">Map to validate against</param>
		/// <returns>ValidationResult indicating success or specific failure reason</returns>
		private ValidationResult ValidateTeleport(IPoint3D targetPoint, Map map)
		{
			// Optimization 3: Early map validation before expensive operations
			if (map == null)
			{
				return ValidationResult.InvalidMap;
			}

			// Fast checks first - no expensive operations
			if (HasNearbyCursedCreatures())
			{
				return ValidationResult.CursedCreaturesNearby;
			}

			if (IsOverloaded())
			{
				return ValidationResult.Overloaded;
			}

			// Expensive travel checks - performed after fast validations
			if (!SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom))
			{
				return ValidationResult.CannotTravelFrom;
			}

			if (!SpellHelper.CheckTravel(Caster, map, new Point3D(targetPoint), TravelCheckType.TeleportTo))
			{
				return ValidationResult.CannotTravelTo;
			}

			// Multi check - performed last as it's moderately expensive
			if (SpellHelper.CheckMulti(new Point3D(targetPoint), map))
			{
				return ValidationResult.LocationBlocked;
			}

			return ValidationResult.Success;
		}

		/// <summary>
		/// Handles validation failure by sending appropriate error messages and fizzling the spell
		/// </summary>
		/// <param name="result">The validation result that failed</param>
		private void HandleValidationFailure(ValidationResult result)
		{
			switch (result)
			{
				case ValidationResult.CursedCreaturesNearby:
					Caster.SendMessage(Spell.MSG_COLOR_WARNING, Spell.SpellMessages.ERROR_CURSED_PREVENTS_TELEPORT);
					DoFizzle();
					break;

				case ValidationResult.Overloaded:
					DoFizzle();
					Caster.SendMessage(Spell.MSG_COLOR_WARNING, Spell.SpellMessages.ERROR_TOO_HEAVY_TELEPORT);
					break;

				case ValidationResult.InvalidMap:
					Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.ERROR_LOCATION_BLOCKED);
					break;

				case ValidationResult.CannotTravelFrom:
				case ValidationResult.CannotTravelTo:
					// Error messages handled by SpellHelper.CheckTravel
					break;

				case ValidationResult.LocationBlocked:
					Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.ERROR_LOCATION_BLOCKED);
					break;
			}
		}

		/// <summary>
		/// Checks if the caster is overloaded and cannot teleport
		/// </summary>
		/// <returns>True if caster is overloaded</returns>
		private bool IsOverloaded()
		{
			return Server.Misc.WeightOverloading.IsOverloaded(Caster);
		}

		/// <summary>
		/// Checks for cursed creatures nearby that would prevent teleportation
		/// Optimization 1: Only scans if caster karma is above threshold (avoids expensive scan for evil players)
		/// </summary>
		/// <returns>True if cursed creatures are nearby and caster has high karma</returns>
		private bool HasNearbyCursedCreatures()
		{
			// Optimization 1: Early exit if karma is too low - no need to scan
			if (Caster.Karma <= KARMA_THRESHOLD_FOR_CURSED_CHECK)
			{
				return false;
			}

			foreach (Mobile mob in Caster.GetMobilesInRange(CURSED_CREATURE_SCAN_RANGE))
			{
				if (mob is BaseCursed)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Attempts to teleport pets if caster is a PlayerMobile
		/// </summary>
		/// <param name="targetPoint">Destination point</param>
		/// <param name="map">Destination map</param>
		private void TryTeleportPets(IPoint3D targetPoint, Map map)
		{
			if (Caster is PlayerMobile && Utility.RandomDouble() < PET_TELEPORT_CHANCE)
			{
				Point3D petDestination = new Point3D(targetPoint);
				BaseCreature.TeleportPets(Caster, petDestination, map, false);
			}
		}

		/// <summary>
		/// Plays teleport visual and sound effects
		/// </summary>
		/// <param name="from">Starting location</param>
		/// <param name="to">Destination location</param>
		private void PlayTeleportEffects(Point3D from, Point3D to)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);

			if (Caster.Player)
			{
				Effects.SendLocationParticles(EffectItem.Create(from, Caster.Map, EffectItem.DefaultDuration),
					PLAYER_EFFECT_ID, PLAYER_EFFECT_SPEED, PLAYER_EFFECT_RENDER, hue, 0, PLAYER_FROM_DURATION, 0);

				Effects.SendLocationParticles(EffectItem.Create(to, Caster.Map, EffectItem.DefaultDuration),
					PLAYER_EFFECT_ID, PLAYER_EFFECT_SPEED, PLAYER_EFFECT_RENDER, hue, 0, PLAYER_TO_DURATION, 0);
			}
			else
			{
				Caster.FixedParticles(NPC_EFFECT_ID, NPC_EFFECT_SPEED, NPC_EFFECT_RENDER, NPC_EFFECT_DURATION,
					hue, 0, EffectLayer.Waist);
			}

			Caster.PlaySound(SOUND_ID);
		}

		/// <summary>
		/// Triggers OnMoveOver for field items at the caster's new location
		/// </summary>
		private void TriggerFieldItems()
		{
			IPooledEnumerable items = Caster.GetItemsInRange(0);

			foreach (Item item in items)
			{
				if (item is Server.Spells.Sixth.ParalyzeFieldSpell.InternalItem ||
					item is Server.Spells.Fifth.PoisonFieldSpell.InternalItem ||
					item is Server.Spells.Fourth.FireFieldSpell.FireFieldItem)
				{
					item.OnMoveOver(Caster);
				}
			}

			items.Free();
		}

		public class InternalTarget : Target
		{
			private TeleportSpell m_Owner;

			public InternalTarget(TeleportSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, true, TargetFlags.None)
			{
				m_Owner = owner;
			}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is IPoint3D)
			{
				m_Owner.Target((IPoint3D)o);
			}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}