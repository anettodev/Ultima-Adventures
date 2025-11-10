using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Fifth
{
	public class PoisonFieldSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Poison Field", "In Nox Grav",
				230,
				9052,
				false,
				Reagent.BlackPearl,
				Reagent.Nightshade,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		public PoisonFieldSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public void Target(IPoint3D p)
		{
			if (!Caster.CanSee(p))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
			{
				SpellHelper.Turn(Caster, p);
				SpellHelper.GetSurfaceTop(ref p);

				// Use centralized field orientation calculation
				bool eastToWest = FieldSpellHelper.GetFieldOrientation(Caster.Location, p);

				Effects.PlaySound(p, Caster.Map, 0x20B);

				int itemID = eastToWest ? 0x3915 : 0x3922;

				// Use NMS duration system for consistency with other field spells
				// This automatically sends the duration message to the caster
				TimeSpan duration = FieldSpellHelper.GetFieldDuration(Caster);

				// Create field items in a line (-2 to +2 range)
				for (int i = -2; i <= 2; ++i)
				{
					Point3D loc = new Point3D(eastToWest ? p.X + i : p.X, eastToWest ? p.Y : p.Y + i, p.Z);
					new InternalItem(itemID, loc, Caster, Caster.Map, duration, i);
				}
			}

			FinishSequence();
		}

		[DispellableField]
		public class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;
			private Mobile m_Caster;

			public override bool BlocksFit { get { return true; } }

			public InternalItem(int itemID, Point3D loc, Mobile caster, Map map, TimeSpan duration, int val) : base(itemID)
			{
				bool canFit = SpellHelper.AdjustField(ref loc, map, 12, false);

				Visible = false;
				Movable = false;
				Light = LightType.Circle300;
				Hue = 0;
				if (Server.Items.CharacterDatabase.GetMySpellHue(caster, 0) >= 0)
				{
					Hue = Server.Items.CharacterDatabase.GetMySpellHue(caster, 0) + 1;
				}

				MoveToWorld(loc, map);

				m_Caster = caster;
				m_End = DateTime.UtcNow + duration;

				m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
				m_Timer.Start();
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if (m_Timer != null)
					m_Timer.Stop();
			}

			public InternalItem(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);

				writer.Write((int)1); // version

				writer.Write(m_Caster);
				writer.WriteDeltaTime(m_End);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);

				int version = reader.ReadInt();

				switch (version)
				{
					case 1:
					{
						m_Caster = reader.ReadMobile();

						goto case 0;
					}
					case 0:
					{
						m_End = reader.ReadDeltaTime();

						m_Timer = new InternalTimer(this, TimeSpan.Zero, true, true);
						m_Timer.Start();

						break;
					}
				}
			}

		/// <summary>
		/// Applies poison to a mobile that enters or stands in the field
		/// Uses centralized poison calculation, friendly fire reduction, and Magic Resistance
		/// </summary>
		public void ApplyPoisonTo(Mobile m)
		{
			if (m_Caster == null)
				return;

		// Check Magic Resistance skill (trains the skill)
		// Resistance chance based on: (MagicResist / 2.5) - (Caster Magery / 5) [DOUBLED]
		double resistChance = (m.Skills[SkillName.MagicResist].Value / 2.5) - (m_Caster.Skills[SkillName.Magery].Value / 5.0);
			
		// Train Magic Resistance skill on poison attempts (5th circle: max 60.0)
		if (m.Skills[SkillName.MagicResist].Value < 60.0)
		{
			m.CheckSkill(SkillName.MagicResist, 0.0, m.Skills[SkillName.MagicResist].Cap);
		}

			// Calculate poison level using centralized helper
			Poison p = PoisonHelper.CalculatePoisonLevel(m_Caster);

			// Apply friendly fire reduction (lower poison level for allies)
			if (FieldSpellHelper.IsFriendlyTarget(m_Caster, m))
			{
				p = PoisonHelper.GetReducedPoisonLevel(p);
			}

			// Check if poison is resisted (reduces poison level)
			if (resistChance > Utility.RandomDouble() * 100.0)
			{
				// Resisted! Reduce poison level
				p = PoisonHelper.GetReducedPoisonLevel(p);
				m.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.RESIST_SPELL_EFFECTS);
			}

			if (m.ApplyPoison(m_Caster, p) == ApplyPoisonResult.Poisoned)
				if (SpellHelper.CanRevealCaster(m))
					m_Caster.RevealingAction();

			if (m is BaseCreature)
				((BaseCreature)m).OnHarmfulSpell(m_Caster);
		}

		public override bool OnMoveOver(Mobile m)
		{
			// Caster CAN be poisoned by their own field (dangerous magic!)
			if (Visible && m_Caster != null && SpellHelper.ValidIndirectTarget(m_Caster, m) && m_Caster.CanBeHarmful(m, false))
			{
				m_Caster.DoHarmful(m);

				ApplyPoisonTo(m);
				m.PlaySound(0x474);
			}

			return true;
		}

			private class InternalTimer : Timer
			{
				private InternalItem m_Item;
				private bool m_InLOS, m_CanFit;

				private static Queue m_Queue = new Queue();

				public InternalTimer(InternalItem item, TimeSpan delay, bool inLOS, bool canFit) : base(delay, TimeSpan.FromSeconds(1.5))
				{
					m_Item = item;
					m_InLOS = inLOS;
					m_CanFit = canFit;

					Priority = TimerPriority.FiftyMS;
				}

				protected override void OnTick()
				{
					if (m_Item.Deleted)
						return;

					if (!m_Item.Visible)
					{
						if (m_InLOS && m_CanFit)
							m_Item.Visible = true;
						else
							m_Item.Delete();

						if (!m_Item.Deleted)
						{
							m_Item.ProcessDelta();
							Effects.SendLocationParticles(EffectItem.Create(m_Item.Location, m_Item.Map, EffectItem.DefaultDuration), 0x376A, 9, 10, Server.Items.CharacterDatabase.GetMySpellHue(m_Item.m_Caster, 0), 0, 5040, 0);
						}
					}
					else if (DateTime.UtcNow > m_Item.m_End)
					{
						m_Item.Delete();
						Stop();
					}
					else
					{
						Map map = m_Item.Map;
						Mobile caster = m_Item.m_Caster;

						if (map != null && caster != null)
						{
							bool eastToWest = (m_Item.ItemID == 0x3915);
							IPooledEnumerable eable = map.GetMobilesInBounds(new Rectangle2D(m_Item.X - (eastToWest ? 0 : 1), m_Item.Y - (eastToWest ? 1 : 0), (eastToWest ? 1 : 2), (eastToWest ? 2 : 1)));

						foreach (Mobile m in eable)
						{
							// Caster CAN be poisoned by their own field (dangerous magic!)
							if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z && SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false))
								m_Queue.Enqueue(m);
						}

							eable.Free();

							while (m_Queue.Count > 0)
							{
								Mobile m = (Mobile)m_Queue.Dequeue();

								caster.DoHarmful(m);

								m_Item.ApplyPoisonTo(m);
								m.PlaySound(0x474);
							}
						}
					}
				}
			}
		}

		public class InternalTarget : Target
		{
			private PoisonFieldSpell m_Owner;

			public InternalTarget(PoisonFieldSpell owner) : base(SpellConstants.GetSpellRange(), true, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is IPoint3D)
					m_Owner.Target((IPoint3D)o);
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
