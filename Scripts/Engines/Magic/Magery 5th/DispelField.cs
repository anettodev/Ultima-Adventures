using System;
using Server.Targeting;
using Server.Network;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using Server.Spells;

namespace Server.Spells.Fifth
{
	public class DispelFieldSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Dispel Field", "An Grav",
				206,
				9002,
				Reagent.BlackPearl,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh,
				Reagent.Garlic
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		#region Constants
		/// <summary>
		/// Range in tiles for dispelling fields
		/// </summary>
		private const int DISPEL_RANGE = 2;

		/// <summary>
		/// Range for dispelling linked moongate destinations (exact location only)
		/// </summary>
		private const int GATE_DESTINATION_RANGE = 2;

		// Visual and audio effect constants
		private const int DISPEL_EFFECT_ID = 0x376A;
		private const int DISPEL_EFFECT_SPEED = 9;
		private const int DISPEL_EFFECT_DURATION = 20;
		private const int DISPEL_EFFECT_HUE = 5042;
		private const int DISPEL_SOUND_ID = 0x201;

		// Caster reaction sounds (gender-specific)
		private const int SOUND_FEMALE_CONFUSED = 799;
		private const int SOUND_MALE_CONFUSED = 1071;
		private const int SOUND_FEMALE_OOPS = 812;
		private const int SOUND_MALE_OOPS = 1086;
		#endregion

		public DispelFieldSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		/// <summary>
		/// Dispels all dispellable fields in range of the given location
		/// Includes special handling for Moongate pairs
		/// </summary>
		/// <param name="location">Center point for dispel radius</param>
		/// <param name="map">Map to search for fields</param>
		private void DispelFieldsInRange(Point3D location, Map map)
		{
			List<Item> items = new List<Item>();

			// Collect all dispellable fields in range
			IPooledEnumerable eable = map.GetItemsInRange(location, DISPEL_RANGE);
			foreach (Item i in eable)
			{
				if (i.GetType().IsDefined(typeof(DispellableFieldAttribute), false))
					items.Add(i);
			}
			eable.Free();

			// Dispel each field with visual/audio feedback
			foreach (Item targ in items)
			{
				if (targ == null)
					continue;

				// Visual effect
				Effects.SendLocationParticles(
					EffectItem.Create(targ.Location, targ.Map, EffectItem.DefaultDuration), 
					DISPEL_EFFECT_ID, 
					DISPEL_EFFECT_SPEED, 
					DISPEL_EFFECT_DURATION, 
					Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0), 
					0, 
					DISPEL_EFFECT_HUE, 
					0
				);

				// Audio feedback
				Effects.PlaySound(targ.GetWorldLocation(), targ.Map, DISPEL_SOUND_ID);

				// Delete the field
				targ.Delete();

				// Special handling: dispel linked moongate destination
				if (targ is Moongate)
				{
					Moongate gate = (Moongate)targ;
					if (gate.Dispellable)
					{
						DestroyGateTargetFrom(gate);
					}
				}
			}
		}

		/// <summary>
		/// Targets a ground location to dispel fields in radius
		/// </summary>
		/// <param name="p">Target location</param>
		public void Target(IPoint3D p)
		{
			if (!Caster.CanSee(p))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (CheckSequence())
			{
				SpellHelper.Turn(Caster, p);
				SpellHelper.GetSurfaceTop(ref p);

				DispelFieldsInRange(new Point3D(p), Caster.Map);
			}
			FinishSequence();
		}

		/// <summary>
		/// Targets a specific item to dispel
		/// </summary>
		/// <param name="item">Item to attempt dispelling</param>
		public void TargetItem(Item item)
		{
			if (!Caster.CanSee(item))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (!item.GetType().IsDefined(typeof(DispellableFieldAttribute), false))
			{
				// Not a dispellable field
				Caster.PlaySound(Caster.Female ? SOUND_FEMALE_CONFUSED : SOUND_MALE_CONFUSED);
				Caster.Say("*huh?*");
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_DISPEL);
			}
			else if (item is Moongate && !((Moongate)item).Dispellable)
			{
				// Non-dispellable moongate (too chaotic)
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_MAGIC_TOO_CHAOTIC);
				Caster.PlaySound(Caster.Female ? SOUND_FEMALE_OOPS : SOUND_MALE_OOPS);
				Caster.Say("*oops*");
			}
			else if (CheckSequence())
			{
				SpellHelper.Turn(Caster, item);

				DispelFieldsInRange(item.GetWorldLocation(), item.Map);
			}

			FinishSequence();
		}

		/// <summary>
		/// When dispelling a moongate, also destroy its linked destination gate
		/// This prevents orphaned gates and maintains consistency
		/// </summary>
		/// <param name="gate">The source moongate being dispelled</param>
		private void DestroyGateTargetFrom(Moongate gate)
		{
			List<Item> gatesDest = new List<Item>();
			Point3D destination = gate.Target;
			
			// Find dispellable items at the gate's destination (exact location)
			IPooledEnumerable neable = gate.TargetMap.GetItemsInRange(destination, GATE_DESTINATION_RANGE);

			foreach (Item i in neable)
			{
				if (i.GetType().IsDefined(typeof(DispellableFieldAttribute), false))
					gatesDest.Add(i);
			}
			neable.Free();

			// Delete all linked destination gates
			foreach (Item destGate in gatesDest)
			{
				destGate.Delete();
			}
		}

		private class InternalTarget : Target
		{
			private DispelFieldSpell m_Owner;

			public InternalTarget(DispelFieldSpell owner) : base(SpellConstants.GetSpellRange(), true, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Item)
				{
					m_Owner.TargetItem((Item)o);
				}
				else if (o is IPoint3D)
				{
					IPoint3D p = o as IPoint3D;

					if (p != null)
						m_Owner.Target(p);
				}
				else
				{
					// Invalid target type
					m_Owner.Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_DISPEL);
				}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
