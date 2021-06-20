using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections.Generic;
using VoreMod.NPCs.VoreMod.TownNPCs;
using System;

namespace VoreMod
{
	public class VoreEntityNPC : VoreEntity
	{
		NPC npc;
		StateNPC npcBackup;

		public VoreEntityNPC(NPC npc)
		{
			this.npc = npc;
		}

		public NPC GetNPC() => npc;

		public override void OnSwallowedBy(VoreEntity pred)
		{
			if (IsChild())
			{
				if (!GetParent().IsSwallowedBy(pred)) pred.Swallow(GetParent());
			}
			if (IsParent())
			{
				foreach (VoreEntity child in GetChildren())
				{
					if (!child.IsSwallowedBy(pred)) pred.Swallow(child);
				}
			}
		}

		public override void OnRegurgitatedBy(VoreEntity pred)
		{
			if (IsChild())
			{
				if (GetParent().IsSwallowedBy(pred)) pred.Regurgitate(GetParent());
			}
			if (IsParent())
			{
				foreach (VoreEntity child in GetChildren())
				{
					if (child.IsSwallowedBy(pred)) pred.Regurgitate(child);
				}
			}
		}

		public override void OnDisposedBy(VoreEntity pred)
		{
			if (IsChild())
			{
				if (GetParent().IsSwallowedBy(pred)) pred.Dispose(GetParent());
			}
			if (IsParent())
			{
				foreach (VoreEntity child in GetChildren())
				{
					if (child.IsSwallowedBy(pred)) pred.Dispose(child);
				}
			}
		}

		public override bool IsChild() => GetNPC().realLife >= 0 && GetNPC().realLife != GetNPC().whoAmI;

		public override IEnumerable<VoreEntity> GetSiblings()
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i] != GetNPC() && Main.npc[i].realLife == GetNPC().realLife) yield return Main.npc[i];
			}
		}

		public override bool IsParent() => GetChildren().Any();

		public override VoreEntity GetParent() => IsChild() ? Main.npc[GetNPC().realLife] : null;

		public override IEnumerable<VoreEntity> GetChildren()
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i] == GetNPC()) continue;
				if (Main.npc[i].realLife == GetNPC().whoAmI) yield return Main.npc[i];
			}
		}

		public override bool IsValid() => npc != null && npc.active;

		public override int GetID() => npc.whoAmI;

		public override string GetName() => VoreConfig.Instance.DebugInfo ? "[" + GetNPC().whoAmI + "] {" + GetNPC().type + "} " + GetNPC().GivenOrTypeName : GetNPC().GivenOrTypeName;

		public override int GetLife() => GetNPC().life;

		public override void SetLife(int life)
		{
			GetNPC().life = life;
		}

		public override int GetLifeMax() => GetNPC().lifeMax;

		public override int GetMana() => 0;

		public override void SetMana(int mana) { }

		public override int GetManaMax() => 0;

		public override LegacySoundStyle GetHitSound() => GetNPC().HitSound;

		public override int GetDirection() => GetNPC().direction;

		public override Vector2 GetPosition() => GetNPC().Bottom;

		public override void SetPosition(Vector2 position)
		{
			GetNPC().Bottom = position;
		}

		public override float GetSizeFactor() => npc.width * npc.height / 1200f;

		public override void Damage(VoreEntity damager, int damage, float knockback)
		{
			if (damager is VoreEntityPlayer)
			{
				VoreEntityPlayer player = damager as VoreEntityPlayer;
				GetNPC().PlayerInteraction(player.GetPlayer().whoAmI);
			}
			GetNPC().StrikeNPC(damage, knockback, damager.GetDirection(), false, false);
		}

		public override void Knockback(Vector2 knockback)
		{
			GetNPC().velocity += knockback;
		}

		public override void Heal(int healing)
		{
			GetNPC().life += healing;
			GetNPC().HealEffect(healing, true);
		}

		public override void BackupState()
		{
			npcBackup = new StateNPC(GetNPC());
		}

		public override void RestoreState()
		{
			npcBackup.Restore(GetNPC());
		}

		public override void SetStateSwallowed()
		{
			StateNPC.SwallowedState.Restore(GetNPC());
		}

		public override List<VoreSprite> GetSprites(SpriteType type)
		{
			return VoreMod.GetPluginSprites(type, GetNPC());
		}

		public override int GetStruggleBonus(VoreEntity pred) => (int)System.Math.Round((npc.ModNPC is Succubus ? int.MaxValue : (Main.expertMode ? npc.defDamage / 3 : npc.defDamage)) * GetLifeRatio());

		public override int GetEscapeLimit(VoreEntity prey) => (int)System.Math.Round((Main.expertMode ? npc.defDefense * 3 : npc.defDefense * 10) * GetLifeRatio());

		public override bool IsHostileTo(VoreEntity other)
		{
			if (other is VoreEntityNPC)
			{
				return (other as VoreEntityNPC).GetNPC().friendly != GetNPC().friendly;
			}
			else if (other is VoreEntityPlayer)
			{
				return !GetNPC().friendly;
			}
			return true;
		}

		public override float GetRandomVoreChance(VoreEntity pred) => 0.75f;

		public override int GetCapacity() => GetNPC().ModNPC is Succubus ? int.MaxValue : 1;

		public override CharmEffects GetBaseCharms()
		{
			return new CharmEffects()
			{
				life = GetNPC().type == NPCID.Nurse || GetNPC().type == NPCID.WitchDoctor ? ItemTier.SilverTungsten : ItemTier.CopperTin,
				mana = GetNPC().type == NPCID.Dryad || GetNPC().type == NPCID.Wizard ? ItemTier.SilverTungsten : ItemTier.None,
				acid = GetNPC().type == ModContent.NPCType<Succubus>() ? ItemTier.Hellstone : ItemTier.CopperTin,
				soul = GetNPC().type == ModContent.NPCType<Succubus>() ? ItemTier.Hellstone : ItemTier.None,
				hunger = ItemTier.CopperTin
			};
		}

		public override bool ShouldShowPrey() => EntityTagLists.IsSlime(npc.type);

		public override float GetScale() => EntityTagLists.IsEye(npc.type) || EntityTagLists.IsSlime(npc.type) ? 1f + GetBellyRatio() * 25f / npc.width : 1f;

		public override void Dispose(VoreEntity prey)
		{
			GetNPC().GetGlobalNPC<VoreNPC>().hasEatenSomeone = true;
			GetNPC().GetGlobalNPC<VoreNPC>().storedPredStatsMult += Math.Max(0.0125f, ((float)prey.GetLifeMax() / (float)GetLifeMax()) / 15f);
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
			}
			npc.netUpdate = true;
			base.Dispose(prey);
		}

		public override EntityTags GetTags()
		{
			EntityTags tags = EntityTags.None;
			if (EntityTagLists.IsTownNPC(npc.type) || npc.townNPC) tags |= EntityTags.TownNPC;
			else tags |= EntityTags.Monster;
			if (EntityTagLists.IsMale(npc.type)) tags |= EntityTags.Male;
			if (EntityTagLists.IsFemale(npc.type)) tags |= EntityTags.Female;
			if (EntityTagLists.IsGenderless(npc.type)) tags |= EntityTags.Genderless;
			if (EntityTagLists.IsBats(npc.type)) tags |= EntityTags.Bat;
			if (EntityTagLists.IsHornet(npc.type)) tags |= EntityTags.Hornet;
			if (EntityTagLists.IsZombie(npc.type)) tags |= EntityTags.Zombie;
			if (EntityTagLists.IsSkeleton(npc.type)) tags |= EntityTags.Skeleton;
			if (EntityTagLists.IsUndead(npc.type)) tags |= EntityTags.Undead;
			if (EntityTagLists.IsWorm(npc.type)) tags |= EntityTags.Worm;
			if (EntityTagLists.IsBeast(npc.type)) tags |= EntityTags.Beast;
			if (EntityTagLists.IsReptile(npc.type)) tags |= EntityTags.Reptile;
			if (EntityTagLists.IsEye(npc.type)) tags |= EntityTags.Eye;
			if (EntityTagLists.IsSlime(npc.type)) tags |= EntityTags.Slime;
			if (EntityTagLists.IsConstruct(npc.type)) tags |= EntityTags.Construct;
			if (EntityTagLists.IsPlant(npc.type)) tags |= EntityTags.Plant;
			if (EntityTagLists.IsAmbient(npc.type)) tags |= EntityTags.Critter;
			if (EntityTagLists.IsFish(npc.type)) tags |= EntityTags.Fish;
			if (EntityTagLists.IsInsect(npc.type)) tags |= EntityTags.Insect;
			if (EntityTagLists.IsFlying(npc.type)) tags |= EntityTags.Flying;
			if (EntityTagLists.IsMimic(npc.type)) tags |= EntityTags.Mimic;
			if (EntityTagLists.IsProjectile(npc.type)) tags |= EntityTags.Projectile;
			if (EntityTagLists.IsBoss(npc.type) || npc.boss) tags |= EntityTags.Boss;
			return tags;
		}

		public override bool EligibleForVore()
		{
			if (EntityTagLists.IsProjectile(npc.type)) return false;
			switch (npc.type)
			{
				case NPCID.DungeonGuardian:
				case NPCID.CultistTablet:
				case NPCID.PirateShip:
				case NPCID.PirateShipCannon:
				case NPCID.ForceBubble:
				case NPCID.DD2EterniaCrystal:
				case NPCID.DD2LanePortal:
				case NPCID.TargetDummy:
					return false;
			}
			return true;
		}
	}
}
