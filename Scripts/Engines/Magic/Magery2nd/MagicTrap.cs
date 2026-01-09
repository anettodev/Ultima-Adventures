using System;
using Server; 
using Server.Targeting;
using Server.Network;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells.Second
{
	/// <summary>
	/// Magic Trap - 2nd Circle Utility Spell
	/// Creates magical traps on containers or ground runes
	/// </summary>
	public class MagicTrapSpell : MagerySpell
	{
		#region Constants
		// Trap Power Calculations
		private const int MAGERY_POWER_DIVISOR = 3;
		private const int CONTAINER_POWER_DIVISOR = 1;
		private const int GROUND_POWER_DIVISOR = 2;
		private const int MAX_TRAP_LEVEL = 60;
		private const int MIN_TRAP_LEVEL = 0;
		private const int MAX_TRAPS_IN_AREA = 2;
		private const int TRAP_CHECK_RANGE = 10;

		// Effect Constants
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_RENDER = 10;
		private const int EFFECT_HUE_OFFSET = 9502;
		private const int EFFECT_CENTER_ID = 0;
		private const int EFFECT_CENTER_DURATION = 5014;
		private const int SOUND_ID = 0x1EF;

		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Magic Trap", "In Jux",
				212,
				9001,
				Reagent.Garlic,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public MagicTrapSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public void Target(TrapableContainer container)
		{
			if (!Caster.CanSee(container))
			{
				Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (container.TrapType != TrapType.None && container.TrapType != TrapType.MagicTrap)
			{
				base.DoFizzle();
			}
			else if (CheckSequence())
			{
				SpellHelper.Turn(Caster, container);

				// Set trap properties
				container.TrapType = TrapType.MagicTrap;
				container.TrapPower = CalculateTrapPower(CONTAINER_POWER_DIVISOR);
				container.TrapLevel = CalculateTrapLevel();

				PlayContainerEffects(container);
			}

			FinishSequence();
		}

		public void MTarget(IPoint3D point)
		{
			Point3D loc = new Point3D(point.X, point.Y, point.Z);

			if (!Caster.CanSee(point))
			{
				Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (SpellHelper.CheckTown(point, Caster) && CheckSequence())
			{
				int nearbyTraps = CountNearbyTraps();

				if (nearbyTraps > MAX_TRAPS_IN_AREA)
				{
					Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.MAGIC_TRAP_TOO_MANY);
				}
				else if (!Caster.Region.AllowHarmful(Caster, Caster))
				{
					Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.MAGIC_TRAP_BAD_LOCATION);
					return;
				}
				else
				{
					SpellHelper.Turn(Caster, point);
					SpellHelper.GetSurfaceTop(ref point);

					NMSUtils.makeCriminalAction(Caster, true);

					// Create spell trap on ground
					int trapPower = CalculateTrapPower(GROUND_POWER_DIVISOR);
					SpellTrap trap = new SpellTrap(Caster, trapPower);
					trap.Map = Caster.Map;
					trap.Location = loc;

					PlayGroundEffects(loc, Caster.Map);
				}
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates trap power based on caster's Magery skill
		/// </summary>
		private int CalculateTrapPower(int divisor)
		{
			double power = (int)(Caster.Skills[SkillName.Magery].Value) / MAGERY_POWER_DIVISOR;
			return (int)Math.Floor((power * NMSUtils.getDamageEvalBenefit(Caster)) / divisor) + 1;
		}

		/// <summary>
		/// Calculates trap level based on caster's Magery skill
		/// </summary>
		private int CalculateTrapLevel()
		{
			int level = (int)(Caster.Skills[SkillName.Magery].Value / MAGERY_POWER_DIVISOR);

			if (level > MAX_TRAP_LEVEL)
				level = MAX_TRAP_LEVEL;
			if (level < MIN_TRAP_LEVEL)
				level = MIN_TRAP_LEVEL;

			return level;
		}

		/// <summary>
		/// Counts existing spell traps near caster
		/// </summary>
		private int CountNearbyTraps()
		{
			int trapCount = 0;

			foreach (Item item in Caster.GetItemsInRange(TRAP_CHECK_RANGE))
			{
				if (item is SpellTrap)
					trapCount++;
			}

			return trapCount;
		}

		/// <summary>
		/// Plays trap creation effects at container location
		/// </summary>
		private void PlayContainerEffects(TrapableContainer container)
		{
			Point3D loc = container.GetWorldLocation();
			Map map = container.Map;

			// Create effect at 4 cardinal directions
			PlayEffectAtOffset(loc, map, 1, 0);   // East
			PlayEffectAtOffset(loc, map, 0, -1);  // North
			PlayEffectAtOffset(loc, map, -1, 0);  // West
			PlayEffectAtOffset(loc, map, 0, 1);   // South

			// Center effect
			Effects.SendLocationParticles(
				EffectItem.Create(loc, map, EffectItem.DefaultDuration), 
				EFFECT_CENTER_ID, 0, 0, EFFECT_CENTER_DURATION);

			Effects.PlaySound(loc, map, SOUND_ID);
		}

		/// <summary>
		/// Plays effect at specified offset from location
		/// </summary>
		private void PlayEffectAtOffset(Point3D baseLoc, Map map, int xOffset, int yOffset)
		{
			Point3D effectLoc = new Point3D(baseLoc.X + xOffset, baseLoc.Y + yOffset, baseLoc.Z);
			Effects.SendLocationParticles(
				EffectItem.Create(effectLoc, map, EffectItem.DefaultDuration), 
				EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_HUE_OFFSET);
		}

		/// <summary>
		/// Plays trap creation effects at ground location
		/// </summary>
		private void PlayGroundEffects(Point3D loc, Map map)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			Effects.SendLocationParticles(
				EffectItem.Create(loc, map, EffectItem.DefaultDuration), 
				EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_HUE_OFFSET, 0);
			Effects.PlaySound(loc, map, SOUND_ID);
		}

		private class InternalTarget : Target
		{
			private MagicTrapSpell m_Owner;

			public InternalTarget(MagicTrapSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, true, TargetFlags.None)
			{
				m_Owner = owner;
			}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is TrapableContainer)
			{
				m_Owner.Target((TrapableContainer)o);
			}
			else if (o is IPoint3D)
			{
				m_Owner.MTarget((IPoint3D)o);
			}
			else
			{
				from.SendMessage(MSG_COLOR_ERROR, SpellMessages.MAGIC_TRAP_INVALID_TARGET);
			}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
