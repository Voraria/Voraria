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

namespace VoreMod
{
	public class VoreEntityPlayer : VoreEntity
	{
		Player player;
		StatePlayer playerBackup;

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
			PlayerDeathReason reason = PlayerDeathReason.ByCustomReason(string.Format(GetRandomDeathMessage(Main.npc[damager.GetID()].type), this, damager));
			if (damager is VoreEntityPlayer)
				GetPlayer().Hurt(reason, damage, damager.GetDirection(), true);
			if (damager is VoreEntityNPC)
				GetPlayer().Hurt(reason, damage, damager.GetDirection());
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

		public override Texture2D GetBellyTexture() => ModContent.GetTexture(nameof(VoreMod) + "/Belly2");

		public override Rectangle GetBellyRect()
        {
            Texture2D texture = GetBellyTexture();

            int frameCount = 15;
            int frameSize = texture.Height / frameCount;
            int frame = 0;

            float ratio = MathHelper.Clamp(GetBellyRatio() * 0.5f, 0f, 1f);
            frame = (int)(ratio * frameCount * 0.999f);

            return new Rectangle(0, frameSize * frame, texture.Width, frameSize);
        }

        public override Vector2 GetBellyOffset()
        {
            int frame = (player.bodyFrame.Y / 56);
            bool bounce = (frame >= 7 && frame <= 9) || (frame >= 14 && frame <= 16);
            Vector2 offset = new Vector2(-15.5f + player.direction * 13f, 14.5f + (bounce ? -2f : 0f));
            offset.Y += player.mount.PlayerOffset;
            return offset;
        }

        public override Color GetBellyColor() => new Color(255, 255, 255);

        public override int GetStruggleBonus(VoreEntity pred) => 100;

        public override int GetEscapeLimit(VoreEntity prey)
        {
            int limit = 0;
            limit += GetPlayer().inventory.Select(i => i.modItem).OfType<AmuletBase>().Max(i => i.EscapeLimit);
            return limit;
        }

        public override bool IsHostileTo(VoreEntity other)
        {
            if (other is VoreEntityNPC)
            {
                return !(other as VoreEntityNPC).GetNPC().friendly;
            }
            return true;
        }

        public override bool ShouldStruggle() => CanStruggle() && GetPlayer().controlJump;

        public override float GetRandomVoreChance(VoreEntity pred)
        {
            if (GetPlayer().HeldItem.modItem is TalismanBase) return 1f;
            return 1f / 3f;
        }

        public override int GetCapacity() => GetPlayer().inventory.Select(i => i.modItem).OfType<AmuletBase>().Max(i => i.Capacity);

        public override CharmEffects GetBaseCharms()
        {
            return new CharmEffects();
        }

		private string GetRandomDeathMessage(int predType)
		{
			WeightedRandom<string> msgs = new WeightedRandom<string>();
			msgs.Add("{0} was gurgled by {1}.");
			msgs.Add("{1} churned {0} into stomach soup.");
			msgs.Add("{0} is just padding on {1}'s body.");
			msgs.Add("{1} devoured {0} whole.");
			msgs.Add("{0} was digested by {1}.");
			msgs.Add("{1} found {0} delicious.");
			if (predType == NPCID.Stylist)
			{
				msgs.Add("{1} let {0} marinate for a while in her stomach acids.");
				msgs.Add("{0} got a free 'cut courtesy of {1}'s gut.");
				msgs.Add("{1} just couldn't resist {0}'s delectable hair.");
			}
			if (predType == NPCID.Mechanic)
			{
				msgs.Add("{1} swallowed up {0} like a big, filling spaghetti wire.");
				msgs.Add("{0} got {1}'s stomach too wired up.");
			}
			if (predType == NPCID.PartyGirl)
			{
				msgs.Add("{1} stuffed herself like a Pigronata with {0}.");
				msgs.Add("{0} spent too long at the party in {1}'s belly.");
				msgs.Add("{1} gobbled up {0} like a big birthday cake.");
			}
			return msgs.Get();
		}

        public override EntityTags GetTags()
        {
            return EntityTags.Player | (player.Male ? EntityTags.Male : EntityTags.Female);
        }
    }
}
