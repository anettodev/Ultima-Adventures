using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	/// <summary>
	/// Base class for adventurer NPCs that face different directions.
	/// Contains common logic for all adventurer types.
	/// </summary>
	public abstract class AdventurerBase : Citizens
	{
		/// <summary>
		/// Initializes a new instance of the AdventurerBase class with the specified direction.
		/// </summary>
		/// <param name="direction">The direction the adventurer should face</param>
		protected AdventurerBase(Direction direction) : base()
		{
			Direction = direction;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">The serialization reader</param>
		public AdventurerBase(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Removes weapons and armor from the adventurer after spawning.
		/// </summary>
		protected void RemoveAdventurerEquipment()
		{
			Item oneHanded = this.FindItemOnLayer(Layer.OneHanded);
			if (oneHanded != null)
			{
				oneHanded.Delete();
			}

			Item twoHanded = this.FindItemOnLayer(Layer.TwoHanded);
			if (twoHanded != null)
			{
				twoHanded.Delete();
			}

			Item helm = this.FindItemOnLayer(Layer.Helm);
			if (helm != null)
			{
				if (helm is BaseArmor)
				{
					helm.Delete();
				}
			}
		}

		/// <summary>
		/// Called after the adventurer spawns. Removes equipment and applies transformations.
		/// </summary>
		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			RemoveAdventurerEquipment();
			Server.Misc.MorphingTime.CheckNecromancer(this);
			Server.Items.EssenceBase.ColorCitizen(this);
		}

		/// <summary>
		/// Serializes the adventurer data
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the adventurer data
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	/// <summary>
	/// Adventurer NPC facing East direction
	/// </summary>
	public class AdventurerEast : AdventurerBase
	{
		[Constructable]
		public AdventurerEast() : base(Direction.East)
		{
		}

		public AdventurerEast(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Serializes the adventurer data
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the adventurer data
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	/// <summary>
	/// Adventurer NPC facing West direction
	/// </summary>
	public class AdventurerWest : AdventurerBase
	{
		[Constructable]
		public AdventurerWest() : base(Direction.West)
		{
		}

		public AdventurerWest(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Serializes the adventurer data
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the adventurer data
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	/// <summary>
	/// Adventurer NPC facing South direction
	/// </summary>
	public class AdventurerSouth : AdventurerBase
	{
		[Constructable]
		public AdventurerSouth() : base(Direction.South)
		{
		}

		public AdventurerSouth(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Serializes the adventurer data
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the adventurer data
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	/// <summary>
	/// Adventurer NPC facing North direction
	/// </summary>
	public class AdventurerNorth : AdventurerBase
	{
		[Constructable]
		public AdventurerNorth() : base(Direction.North)
		{
		}

		public AdventurerNorth(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Serializes the adventurer data
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the adventurer data
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}
