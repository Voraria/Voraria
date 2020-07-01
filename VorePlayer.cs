using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using VoreMod.Buffs;
using VoreMod.Items;
using VoreMod.Items.VoreMod.Amulets;
using VoreMod.Items.VoreMod.Charms;
using VoreMod.UI;

namespace VoreMod
{
    public class VorePlayer : ModPlayer
    {
        public VoreEntity entity;

        public bool lastHitFromPVP;

        public bool wasDigested;

        public static PlayerLayer BellyLayer = null;

        List<CharmSlot> charmSlots = new List<CharmSlot>();

        public static void DrawLayer(PlayerDrawInfo drawInfo, SpriteType type)
        {
            Player player = drawInfo.drawPlayer;
            VoreEntity entity = player.GetEntity();
            if (entity.IsSwallowed() && !entity.ShouldShowWhileSwallowed()) return;
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

        public override void Initialize()
        {
            foreach (CharmEffect value in Enum.GetValues(typeof(CharmEffect)))
            {
                if (value != CharmEffect.None && !charmSlots.Any(slot => slot.charm == value))
                {
                    charmSlots.Add(new CharmSlot(value));
                }
            }
        }

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            TagCompound charmSlotTag = new TagCompound();
            foreach (CharmSlot slot in charmSlots) charmSlotTag.Add(slot.charm.ToString(), slot.Item);
            tag.Add("charmSlots", charmSlotTag);
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            if (tag.ContainsKey("charmSlots"))
            {
                TagCompound charmSlotTag = tag.GetCompound("charmSlots");
                foreach (CharmSlot slot in charmSlots)
                {
                    if (charmSlotTag.ContainsKey(slot.charm.ToString())) slot.Item = charmSlotTag.Get<Item>(slot.charm.ToString());
                }
            }
        }

        public void DrawUI(SpriteBatch batch)
        {
            foreach (CharmSlot slot in charmSlots) slot.Draw(batch);
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
            layers.Insert(layers.IndexOf(PlayerLayer.Arms) + 1, BellyLayer);
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
            wasDigested = false;

            foreach (CharmSlot slot in charmSlots)
            {
                CharmBase charm = slot.Item.modItem as CharmBase;
                if (charm != null) player.GetEntity().ApplyCharm(charm.Effect, charm.Tier);
            }
        }

        public override void PreUpdateBuffs()
        {
            VoreEntity entity = player.GetEntity();
            if (entity.IsBeingDigested()) player.AddBuff(DigestingBuff.BuffType, 2);
            if (entity.IsSwallowed() && !entity.IsBeingDigested()) player.AddBuff(SwallowedBuff.BuffType, 2);

        }

        public override void PostUpdateBuffs()
        {
            if (player.GetEntity().IsSwallowed()) player.velocity = Vector2.Zero;
            player.GetEntity().UpdateTick();
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            lastHitFromPVP = pvp;

            playSound = !wasDigested;
            genGore = !wasDigested;

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            playSound = !wasDigested;
            genGore = !wasDigested;

            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        /*
		Hey, just a little word of warning for you:
		If you touch this method or any of the methods relating to it without consulting me...
		...your life shall come to a swift and abrupt end at the hands of the Corporation.
		Makes sense, yeah? Good, now leave me to my work.
		I have a lot of Stylist feeding to do.
		The best girl deserves the best meals.
		 - the debatably-predatory Sign Painter
		*/
        public void KillMe(PlayerDeathReason damageSource, double dmg, int hitDirection)
        {
            bool pvp = lastHitFromPVP;
            if (player.dead)
            {
                return;
            }
            bool playSound = true;
            bool genGore = true;
            if (!PlayerHooks.PreKill(player, dmg, hitDirection, pvp, ref playSound, ref genGore, ref damageSource))
            {
                return;
            }
            if (wasDigested)
            {
                playSound = false;
                genGore = false;
            }
            if (pvp)
            {
                player.pvpDeath = true;
            }
            if (player.trapDebuffSource)
            {
                AchievementsHelper.HandleSpecialEvent(player, 4);
            }
            player.lastDeathPostion = player.Center;
            player.lastDeathTime = DateTime.Now;
            player.showLastDeath = true;
            bool flag4;
            int coinsOwned = (int)Utils.CoinsCount(out flag4, player.inventory);
            if (Main.myPlayer == player.whoAmI)
            {
                player.lostCoins = coinsOwned;
                player.lostCoinString = Main.ValueToCoins(player.lostCoins);
            }
            if (Main.myPlayer == player.whoAmI)
            {
                Main.mapFullscreen = false;
            }
            if (Main.myPlayer == player.whoAmI)
            {
                player.trashItem.SetDefaults();
                if (player.difficulty == 0)
                {
                    for (int k = 0; k < 59; k++)
                    {
                        if (player.inventory[k].stack > 0 && ((player.inventory[k].type >= ItemID.LargeAmethyst && player.inventory[k].type <= ItemID.LargeDiamond) || player.inventory[k].type == ItemID.LargeAmber))
                        {
                            if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
                            {
                                int num5 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, player.inventory[k].type);
                                Main.item[num5].netDefaults(player.inventory[k].netID);
                                Main.item[num5].Prefix(player.inventory[k].prefix);
                                Main.item[num5].stack = player.inventory[k].stack;
                                Main.item[num5].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                                Main.item[num5].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                                Main.item[num5].noGrabDelay = 100;
                                Main.item[num5].favorited = false;
                                Main.item[num5].newAndShiny = false;
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num5);
                                }
                            }
                            player.inventory[k].SetDefaults();
                        }
                    }
                }
                else if (player.difficulty == 1)
                {
                    if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
                        player.DropItems();
                    else
                        LoseDigestedItems();
                }
                else if (player.difficulty == 2)
                {
                    if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
                        player.DropItems();
                    else
                        LoseDigestedItems();
                    player.KillMeForGood();
                }
            }
            if (!wasDigested && playSound)
            {
                Main.PlaySound(SoundID.PlayerKilled, (int)player.position.X, (int)player.position.Y);
            }
            player.headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
            player.bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
            player.legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
            player.headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
            player.bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
            player.legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
            if (player.stoned || !genGore)
            {
                player.headPosition = Vector2.Zero;
                player.bodyPosition = Vector2.Zero;
                player.legPosition = Vector2.Zero;
            }
            if (!wasDigested && genGore)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (player.stoned)
                    {
                        Dust.NewDust(player.position, player.width, player.height, 1, 2 * hitDirection, -2f);
                    }
                    else if (player.frostArmor)
                    {
                        int num4 = Dust.NewDust(player.position, player.width, player.height, 135, 2 * hitDirection, -2f);
                        Main.dust[num4].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                    }
                    else if (player.boneArmor)
                    {
                        int num3 = Dust.NewDust(player.position, player.width, player.height, 26, 2 * hitDirection, -2f);
                        Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                    }
                    else
                    {
                        Dust.NewDust(player.position, player.width, player.height, 5, 2 * hitDirection, -2f);
                    }
                }
            }
            player.mount.Dismount(player);
            player.dead = true;
            player.respawnTimer = 600;
            bool flag3 = false;
            if (Main.netMode != NetmodeID.SinglePlayer && !pvp)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && (Main.npc[i].boss || (Main.npc[i].type >= NPCID.EaterofWorldsHead && Main.npc[i].type <= NPCID.EaterofWorldsTail)) && Math.Abs(player.Center.X - Main.npc[i].Center.X) + Math.Abs(player.Center.Y - Main.npc[i].Center.Y) < 4000f)
                    {
                        flag3 = true;
                        break;
                    }
                }
            }
            if (flag3)
            {
                player.respawnTimer += 600;
            }
            if (Main.expertMode)
            {
                player.respawnTimer = (int)((double)player.respawnTimer * 1.5);
            }
            if (!wasDigested)
                PlayerHooks.Kill(player, dmg, hitDirection, pvp, damageSource);
            else
            {
                if (player.GetEntity().GetPredator() is VoreEntityNPC)
                {
                    NPC fatty = Main.npc[player.GetEntity().GetPredator().GetID()];
                    if (fatty.type == NPCID.Stylist)
                    {
                        VoreWorld.storedPlayerSnacksStylist++;
                    }
                }

                player.GetEntity().Death();
            }
            player.immuneAlpha = 0;
            if (wasDigested)
                player.immuneAlpha = 255;
            player.palladiumRegen = false;
            player.iceBarrier = false;
            player.crystalLeaf = false;
            NetworkText deathText = damageSource.GetDeathText(player.name);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.BroadcastChatMessage(deathText, new Color(225, 25, 25));
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(deathText.ToString(), 225, 25, 25);
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
            {
                NetMessage.SendPlayerDeath(player.whoAmI, damageSource, (int)dmg, hitDirection, pvp);
            }
            if (player.whoAmI == Main.myPlayer && player.difficulty == 0)
            {
                if (!pvp)
                {
                    if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
                        player.DropCoins();
                    else
                        LoseDigestedCoins();
                }
                else
                {
                    player.lostCoins = 0;
                    player.lostCoinString = Main.ValueToCoins(player.lostCoins);
                }
            }
            if (!wasDigested)
                player.DropTombstone(coinsOwned, deathText, hitDirection);
            if (player.whoAmI == Main.myPlayer)
            {
                try
                {
                    WorldGen.saveToonWhilePlaying();
                }
                catch
                {
                }
            }
            wasDigested = false;
        }

        public void LoseDigestedItems()
        {
            IList<Item> startInventory = PlayerHooks.SetupStartInventory(player, mediumcoreDeath: true);
            PlayerHooks.SetStartInventory(player, startInventory);
            Main.mouseItem = new Item();
        }

        public int LoseDigestedCoins()
        {
            int num6 = 0;
            for (int i = 0; i < 59; i++)
            {
                if (player.inventory[i].type >= 71 && player.inventory[i].type <= 74)
                {
                    int num5 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, player.inventory[i].type);
                    int num4 = player.inventory[i].stack / 2;
                    if (Main.expertMode)
                    {
                        num4 = (int)((double)player.inventory[i].stack * 0.25);
                    }
                    num4 = player.inventory[i].stack - num4;
                    player.inventory[i].stack -= num4;
                    if (player.inventory[i].type == 71)
                    {
                        num6 += num4;
                    }
                    if (player.inventory[i].type == 72)
                    {
                        num6 += num4 * 100;
                    }
                    if (player.inventory[i].type == 73)
                    {
                        num6 += num4 * 10000;
                    }
                    if (player.inventory[i].type == 74)
                    {
                        num6 += num4 * 1000000;
                    }
                    if (player.inventory[i].stack <= 0)
                    {
                        player.inventory[i] = new Item();
                    }
                    Main.item[num5].stack = num4;
                    Main.item[num5].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                    Main.item[num5].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                    Main.item[num5].noGrabDelay = 100;
                    Main.item[num5].TurnToAir();
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, num5);
                    }
                    if (i == 58)
                    {
                        Main.mouseItem = player.inventory[i].Clone();
                    }
                }
            }
            player.lostCoins = num6;
            player.lostCoinString = Main.ValueToCoins(player.lostCoins);
            return num6;
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
