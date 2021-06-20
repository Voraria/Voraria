using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using VoreMod.Items;
using VoreMod.Items.VoreMod.Amulets;

namespace VoreMod
{
	public class VoreEntityPlayer : VoreEntity
	{
		Player player;
		StatePlayer playerBackup;

		List<VoreSprite> cachedSprites = new List<VoreSprite>(new VoreSprite[] {
			new VoreSprite.Builder(new VoreSprite(nameof(VoreMod) + "/Common/Belly3", SpriteType.Belly)).Layout(SpriteLayout.SizeY).Frames(28).Offset(22f, 0f).FrameOffset(0f, -2f, 7, 9).FrameOffset(0f, -2f, 14, 16).ColorMode(ColorMode.Skin)
		});

		public VoreEntityPlayer(Player player)
		{
			this.player = player;
		}

		public Player GetPlayer() => player;

		public override bool IsValid() => player != null && player.active;

		public override int GetID() => player.whoAmI;

		public override string GetName() => GetPlayer().name;

		public override int GetLife() => GetPlayer().statLife;

		public override void SetLife(int life)
		{
			GetPlayer().statLife = life;
		}

		public override int GetLifeMax() => GetPlayer().statLifeMax2;

		public override int GetMana() => GetPlayer().statMana;

		public override void SetMana(int mana)
		{
			GetPlayer().statMana = mana;
		}

		public override int GetManaMax() => GetPlayer().statManaMax2;

		public override LegacySoundStyle GetHitSound() => new LegacySoundStyle(GetTags().HasAll(EntityTags.Female) ? SoundID.FemaleHit : SoundID.PlayerHit, 1);

		public override int GetDirection() => GetPlayer().direction;

		public override Vector2 GetPosition() => GetPlayer().Bottom;

		public override void SetPosition(Vector2 position)
		{
			GetPlayer().Bottom = position;
		}

		public override float GetSizeFactor() => player.width * player.height / 1200f;

		public override void Damage(VoreEntity damager, int damage, float knockback)
		{
			PlayerDeathReason reason = PlayerDeathReason.ByCustomReason(GetRandomDeathMessage(damager));
			if (damager is VoreEntityPlayer)
				GetPlayer().Hurt(reason, damage, damager.GetDirection(), true, true);
			if (damager is VoreEntityNPC)
				GetPlayer().Hurt(reason, damage, damager.GetDirection(), false, true);
			Knockback(new Vector2(knockback * damager.GetDirection(), -knockback));
		}

		public override void Knockback(Vector2 knockback)
		{
			GetPlayer().velocity += knockback;
		}

		public override void Heal(int healing)
		{
			GetPlayer().statLife += healing;
			GetPlayer().HealEffect(healing, true);
		}

		public override void BackupState()
		{
			playerBackup = new StatePlayer(GetPlayer());
		}

		public override void RestoreState()
		{
			playerBackup.Restore(GetPlayer());
		}

		public override void SetStateSwallowed()
		{
			StatePlayer.SwallowedState.Restore(GetPlayer());
		}

		public override List<VoreSprite> GetSprites(SpriteType type)
		{
			List<VoreSprite> list = new List<VoreSprite>();
			list.AddRange(cachedSprites);
			foreach (Item armor in GetPlayer().armor)
				list.SafeConcat(VoreMod.GetPluginSprites(type, armor));
			foreach (Item equip in GetPlayer().miscEquips)
				list.SafeConcat(VoreMod.GetPluginSprites(type, equip));
			return list;
		}

		public override int GetStruggleBonus(VoreEntity pred) => 100;

		public override int GetEscapeLimit(VoreEntity prey)
		{
			int limit = 0;
			limit += GetPlayer().inventory.Select(i => i.ModItem).OfType<AmuletBase>().MaxOrDefault(i => i.EscapeLimit);
			return limit;
		}

		public override bool IsHostileTo(VoreEntity other)
		{
			if (other is VoreEntityNPC)
			{
				return !(other as VoreEntityNPC).GetNPC().friendly && (other as VoreEntityNPC).GetNPC().GetGlobalNPC<VoreNPC>().storedPredStatsMult < 2.50f;
			}
			return true;
		}

		public override bool ShouldStruggle() => CanStruggle() && GetPlayer().controlJump;

		public override float GetRandomVoreChance(VoreEntity pred)
		{
			if (GetPlayer().HeldItem.ModItem is TalismanBase) return 1f;
			return 1f / 3f;
		}

		public override int GetCapacity() => GetPlayer().inventory.Select(i => i.ModItem).OfType<AmuletBase>().MaxOrDefault(i => i.Capacity);

		public override CharmEffects GetBaseCharms()
		{
			return new CharmEffects();
		}

		public override void OnDisposedBy(VoreEntity pred)
		{
			GetPlayer().GetModPlayer<VorePlayer>().wasDigested = true;
			GetPlayer().KillMe(PlayerDeathReason.ByCustomReason(GetRandomDeathMessage(pred)), 0, pred.GetDirection(), pred is VoreEntityPlayer);
		}

		private string GetRandomDeathMessage(VoreEntity pred)
		{
			WeightedRandom<string> msgs = new WeightedRandom<string>();
			if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.DigestedPlayer, pred, GetDialogueTags()), pred, this))
			{
				msgs.Add("{1} was gurgled by {0}.");
				msgs.Add("{0} churned {1} into stomach soup.");
				msgs.Add("{1} is just padding on {0}'s body.");
				msgs.Add("{0} devoured {1} whole.");
				msgs.Add("{1} was digested by {0}.");
				msgs.Add("{0} found {1} delicious.");
			}
			return string.Format(msgs.Get(), pred, this);
		}

		public override EntityTags GetTags()
		{
			return EntityTags.Player | (player.Male ? EntityTags.Male : EntityTags.Female);
		}
	}
}