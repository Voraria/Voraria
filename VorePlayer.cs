using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using VoreMod.Items;
using VoreMod.Items.VoreMod.Amulets;

namespace VoreMod
{
    public class VorePlayer : ModPlayer
    {
        public VoreEntity entity;

        public static PlayerLayer BellyLayer = null;

        public static void DrawLayer(PlayerDrawInfo drawInfo, SpriteType type)
        {
            Player player = drawInfo.drawPlayer;
            VoreEntity entity = player.GetEntity();
            if (!entity.HasSprites(type)) return;
            foreach (VoreSprite sprite in entity.GetSprites(type))
            {
                float ratio = MathHelper.Clamp(entity.GetBellyRatio() / 2f, 0f, 1f);

                Texture2D texture = sprite.GetTexture();

                int animFrames = texture.Height / 56;
                int animFrame = player.bodyFrame.Y / 56;
                Rectangle rect = sprite.GetRect(texture, ratio, animFrames, animFrame);

                Vector2 pos = player.Center - Main.screenPosition;
                Vector2 offset = sprite.GetOffset(animFrame) * new Vector2(player.direction, 1f);
                offset.Y += player.mount.PlayerOffset;
                Vector2 origin = new Vector2(rect.Center.X - rect.Left, rect.Center.Y - rect.Top) - offset;

                pos.X = player.direction == -1 ? (int)Math.Floor(pos.X) : (int)Math.Ceiling(pos.X);
                pos.Y = (int)Math.Ceiling(pos.Y);

                Color color = sprite.GetColor();
                switch (sprite.GetColorMode())
                {
                    case ColorMode.Default:
                        color = color.MultiplyRGB(Lighting.GetColor((int)((drawInfo.position.X + player.width / 2f) / 16f), (int)((drawInfo.position.Y + player.height / 2f) / 16f)));
                        break;
                    case ColorMode.Skin:
                        color = color.MultiplyRGB(drawInfo.bodyColor);
                        break;
                    case ColorMode.Dye:
                        color = color.MultiplyRGB(drawInfo.middleArmorColor);
                        break;
                }

                DrawData data = new DrawData(texture, pos, rect, color, 0f, origin, 1f, player.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (BellyLayer == null)
            {
                BellyLayer = new PlayerLayer(nameof(VoreMod), nameof(BellyLayer), PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
                {
                    DrawLayer(drawInfo, SpriteType.Belly);
                });
            }
            BellyLayer.visible = true;
            layers.Insert(layers.IndexOf(PlayerLayer.Arms), BellyLayer);
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            VoreEntity ent = drawInfo.drawPlayer.GetEntity();
            if (ent.IsSwallowed() && !ent.ShouldShowWhileSwallowed())
            {
                drawInfo.position = new Vector2(-100f, -100f);
            }
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            player.GetEntity().ResetTick();
        }

        public override void PostUpdateBuffs()
        {
            if (player.GetEntity().IsSwallowed()) player.velocity = Vector2.Zero;
            player.GetEntity().UpdateTick();
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (player.GetEntity().IsSwallowed())
                genGore = false;
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            player.GetEntity().Death();
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (!npc.GetEntity().CanDamage(player.GetEntity())) return false;
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (!player.GetEntity().CanBeDamaged()) return false;
            return base.CanBeHitByProjectile(proj);
        }

        public override bool? CanHitNPC(Item item, NPC target)
        {
            if (!player.GetEntity().CanDamage(target)) return false;
            if (item.modItem is AmuletBase) return player.GetEntity().CanSwallow(target);
            if (item.modItem is TalismanBase) return target.GetEntity().CanSwallow(player);
            return base.CanHitNPC(item, target);
        }

        public override bool CanHitPvp(Item item, Player target)
        {
            if (!player.GetEntity().CanDamage(target)) return false;
            if (item.modItem is AmuletBase) return player.GetEntity().CanSwallow(target);
            if (item.modItem is TalismanBase) return target.GetEntity().CanSwallow(player);
            return base.CanHitPvp(item, target);
        }

        public override bool PreItemCheck()
        {
            if (player.GetEntity().IsSwallowed()) return false;
            return base.PreItemCheck();
        }

        public override void NaturalLifeRegen(ref float regen)
        {
            if (player.GetEntity().IsSwallowed())
            {
                regen = 0f;
            }
        }
    }
}
