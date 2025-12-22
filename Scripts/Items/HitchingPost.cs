using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Custom
{
	/// <summary>
	/// Hitching Post - Item that allows temporary pet storage.
	/// Can store one pet per post, accessible only by the pet's owner.
	/// Pets are blessed and immobilized while hitched.
	/// </summary>
	public class HitchingPost : Item
	{
		#region Fields

		private Dictionary<Mobile, BaseCreature> m_StabledTable;
		private bool m_Solid;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the dictionary of stabled pets (owner -> pet mapping).
		/// </summary>
		public Dictionary<Mobile, BaseCreature> StabledTable
		{
			get { return m_StabledTable; }
		}

		/// <summary>
		/// Gets or sets whether the post is solid (locked down).
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public bool Solid
		{
			get { return m_Solid; }
			set { m_Solid = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HitchingPost class.
		/// </summary>
		[Constructable]
		public HitchingPost() : base()
		{
			ItemID = HitchingPostConstants.ITEM_ID;
			Name = HitchingPostStringConstants.ITEM_NAME;
			Hue = HitchingPostConstants.HUE;
			m_StabledTable = new Dictionary<Mobile, BaseCreature>();
			Solid = false;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public HitchingPost(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the HitchingPost.
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)HitchingPostConstants.SERIALIZATION_VERSION);

			writer.Write((bool)Solid);

			writer.Write((int)m_StabledTable.Count);

			foreach (KeyValuePair<Mobile, BaseCreature> kvp in m_StabledTable)
			{
				writer.Write(kvp.Key);
				writer.Write(kvp.Value);
			}
		}

		/// <summary>
		/// Deserializes the HitchingPost.
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 2:
				{
					Solid = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					m_StabledTable = new Dictionary<Mobile, BaseCreature>();

					int count = reader.ReadInt();

					for (int i = 0; i < count; ++i)
					{
						Mobile owner = reader.ReadMobile();
						BaseCreature pet = reader.ReadMobile() as BaseCreature;

						if (owner != null && pet != null)
						{
							m_StabledTable[owner] = pet;
						}
					}

					goto case 0;
				}
				case 0:
				{
					if (m_StabledTable == null)
					{
						m_StabledTable = new Dictionary<Mobile, BaseCreature>();
					}
					break;
				}
			}
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to release or hitch a pet.
		/// </summary>
		/// <param name="from">The player using the post</param>
		public override void OnDoubleClick(Mobile from)
		{
			if (HasStabledPet(from))
			{
				ReleasePet(from);
			}
			else if (IsPostBusy())
			{
				from.SendMessage(HitchingPostStringConstants.MSG_POST_BUSY);
			}
			else
			{
				from.Target = new StableTarget(this);
				from.SendMessage(HitchingPostStringConstants.MSG_WHAT_PET_HITCH);
			}
		}

		/// <summary>
		/// Removes an owner from the stabled table.
		/// </summary>
		/// <param name="from">The owner to remove</param>
		public void RemoveOwner(Mobile from)
		{
			if (m_StabledTable != null)
			{
				m_StabledTable.Remove(from);
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Checks if the post has a stabled pet for the given owner.
		/// </summary>
		/// <param name="owner">The owner to check</param>
		/// <returns>True if owner has a pet stabled, false otherwise</returns>
		private bool HasStabledPet(Mobile owner)
		{
			if (m_StabledTable == null || m_StabledTable.Count == 0)
				return false;

			BaseCreature pet;
			if (m_StabledTable.TryGetValue(owner, out pet))
			{
				return pet != null && pet.IsHitchStabled && pet.ControlMaster == null;
			}

			return false;
		}

		/// <summary>
		/// Checks if the post is busy (has any stabled pets).
		/// </summary>
		/// <returns>True if post is busy, false otherwise</returns>
		private bool IsPostBusy()
		{
			return m_StabledTable != null && m_StabledTable.Count > 0;
		}

		/// <summary>
		/// Releases a pet from the hitching post to its owner.
		/// </summary>
		/// <param name="from">The owner claiming the pet</param>
		private void ReleasePet(Mobile from)
		{
			BaseCreature pet;
			if (!m_StabledTable.TryGetValue(from, out pet) || pet == null)
			{
				from.SendMessage(HitchingPostStringConstants.MSG_POST_BUSY);
				return;
			}

			if (from.Followers + pet.ControlSlots <= from.FollowersMax)
			{
				pet.IsHitchStabled = false;
				pet.Blessed = false;
				pet.ControlMaster = from;
				pet.ControlOrder = OrderType.Follow;
				pet.CantWalk = false;
				m_StabledTable.Remove(from);
				from.SendMessage(HitchingPostStringConstants.MSG_PET_RELEASED);

				if (!Solid)
				{
					this.Movable = true;
				}
			}
			else
			{
				from.SendMessage(string.Format(HitchingPostStringConstants.MSG_PET_REMAINED_FORMAT, pet.Name));
			}
		}

		/// <summary>
		/// Hitches a pet to the post.
		/// </summary>
		/// <param name="from">The owner hitching the pet</param>
		/// <param name="pet">The pet being hitched</param>
		private void HitchPet(Mobile from, BaseCreature pet)
		{
			pet.IsHitchStabled = true;
			pet.Blessed = true;
			pet.ControlOrder = OrderType.Stay;
			pet.CantWalk = true;
			pet.ControlMaster = null;
			m_StabledTable[from] = pet;

			from.SendMessage(HitchingPostStringConstants.MSG_PET_STABLED_SUCCESS);
			UpdatePostMovability();
		}

		/// <summary>
		/// Updates the post's movability based on its locked state.
		/// </summary>
		private void UpdatePostMovability()
		{
			if (Movable && !IsLockedDown)
			{
				Movable = false;
				Solid = false;
			}
			else if (!Movable || IsLockedDown)
			{
				Solid = true;
			}
		}

		/// <summary>
		/// Checks if a creature type can be hitched.
		/// </summary>
		/// <param name="targeted">The object being targeted</param>
		/// <returns>Error message if cannot hitch, null if valid</returns>
		private string CanHitchCreatureType(object targeted)
		{
			if (targeted is GolemPorter)
				return HitchingPostStringConstants.MSG_CANT_HITCH_GOLEM;

			if (targeted is HenchmanArcher || targeted is HenchmanFighter || targeted is HenchmanWizard || targeted is Squire)
				return HitchingPostStringConstants.MSG_CANT_HITCH_HENCHMAN;

			if (targeted is BaseFamiliar)
				return HitchingPostStringConstants.MSG_CANT_HITCH_FAMILIAR;

			return null; // Valid type
		}

		/// <summary>
		/// Validates if a pet can be hitched to the post.
		/// </summary>
		/// <param name="from">The owner attempting to hitch</param>
		/// <param name="pet">The pet to validate</param>
		/// <param name="post">The hitching post</param>
		/// <returns>Error message if validation fails, null if valid</returns>
		private string ValidatePetForHitching(Mobile from, BaseCreature pet, HitchingPost post)
		{
			if (!pet.InRange(post, HitchingPostConstants.PET_RANGE_CHECK))
				return HitchingPostStringConstants.MSG_MUST_BE_NEAR;

			if (!pet.Controlled)
				return HitchingPostStringConstants.MSG_MUST_BE_CONTROLLING;

			if (pet.ControlMaster != from)
				return HitchingPostStringConstants.MSG_CAN_STABLE_ONLY_OWN;

			if (pet.IsDeadPet)
				return HitchingPostStringConstants.MSG_CREATURE_MUST_BE_ALIVE;

			if (pet.IsHitchStabled)
				return HitchingPostStringConstants.MSG_PET_ALREADY_STABLED;

			return null; // Valid
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target class for selecting a pet to hitch.
		/// </summary>
		private class StableTarget : Target
		{
			private HitchingPost m_Post;

			/// <summary>
			/// Initializes a new instance of the StableTarget class.
			/// </summary>
			public StableTarget(HitchingPost post) : base(HitchingPostConstants.TARGET_RANGE, false, TargetFlags.None)
			{
				m_Post = post;
			}

			/// <summary>
			/// Handles the target selection and hitching process.
			/// </summary>
			/// <param name="from">The player who selected the target</param>
			/// <param name="targeted">The object that was targeted</param>
			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Post == null || m_Post.Deleted)
					return;

				if (m_Post.IsPostBusy())
				{
					from.SendMessage(HitchingPostStringConstants.MSG_POST_BUSY);
					return;
				}

				string errorMessage = m_Post.CanHitchCreatureType(targeted);
				if (errorMessage != null)
				{
					from.SendMessage(errorMessage);
					return;
				}

				BaseCreature pet = targeted as BaseCreature;
				if (pet == null)
					return;

				errorMessage = m_Post.ValidatePetForHitching(from, pet, m_Post);
				if (errorMessage != null)
				{
					from.SendMessage(errorMessage);
					return;
				}

				m_Post.HitchPet(from, pet);
			}
		}

		#endregion
	}
}
