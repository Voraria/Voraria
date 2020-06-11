using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VoreMod.Items;
using VoreMod.NPCs;

namespace VoreMod
{
    public class VoreNPC : GlobalNPC
    {
        public VoreEntity entity;

        public override bool InstancePerEntity => true;

        public override bool PreAI(NPC npc)
        {
            if (npc.GetEntity().IsSwallowed()) return false;
            foreach (VoreEntityPlayer player in npc.GetEntity().GetAllPrey(true).OfType<VoreEntityPlayer>())
            {
                player.SetPosition(player.swallowedLocation);
                //player.GetPlayer().dead = true;
            }
            return base.PreAI(npc);
        }

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
            npc.GetEntity().ResetTick();
        }

        public override void PostAI(NPC npc)
        {
            foreach (VoreEntityPlayer player in npc.GetEntity().GetAllPrey(true).OfType<VoreEntityPlayer>())
            {
                player.SetPosition(npc.GetEntity().GetBellyLocation());
                //player.GetPlayer().dead = false;
            }
            npc.GetEntity().UpdateTick();
        }

        public override void NPCLoot(NPC npc)
        {
            npc.GetEntity().Death();
            base.NPCLoot(npc);
        }

        public override bool CheckActive(NPC npc)
        {
            if (npc.GetEntity().IsSwallowed()) return false;
            return base.CheckActive(npc);
        }

        public override bool CheckDead(NPC npc)
        {
            if (npc.GetEntity().IsSwallowed()) return false;
            return base.CheckDead(npc);
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (item.modItem is AmuletBase) return player.GetEntity().CanSwallow(npc);
            if (item.modItem is TalismanBase) return npc.GetEntity().CanSwallow(player);
            if (!player.GetEntity().CanDamage(npc)) return false;
            return base.CanBeHitByItem(npc, player, item);
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (!npc.GetEntity().CanBeDamaged()) return false;
            return base.CanBeHitByProjectile(npc, projectile);
        }

        public override bool? CanHitNPC(NPC npc, NPC target)
        {
            if (!npc.GetEntity().CanDamage(target)) return false;
            return base.CanHitNPC(npc, target);
        }

        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            if (!npc.GetEntity().CanDamage(target)) return false;
            return base.CanHitPlayer(npc, target, ref cooldownSlot);
        }

        public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (target.type == ModContent.NPCType<Succubus>() && target.GetEntity().CanSwallow(npc))
            {
                damage = 0;
                target.GetEntity().Swallow(npc);
                return;
            }
            if (npc.GetEntity().AttemptRandomVore(target))
            {
                damage = 0;
                return;
            }
            base.ModifyHitNPC(npc, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (npc.GetEntity().AttemptRandomVore(target))
            {
                damage = 0;
                return;
            }
            base.ModifyHitPlayer(npc, target, ref damage, ref crit);
        }

        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (npc.GetEntity().IsSwallowed()) return false;
            return base.DrawHealthBar(npc, hbPosition, ref scale, ref position);
        }

        float cachedScale;

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.GetEntity().IsSwallowed() && !npc.GetEntity().ShouldShowWhileSwallowed()) return false;
            cachedScale = npc.scale;
            npc.scale *= npc.GetEntity().GetScale();
            return base.PreDraw(npc, spriteBatch, drawColor);
        }

        public override void DrawBehind(NPC npc, int index)
        {
            if (npc.GetEntity().HasSwallowedAny())
            {
                Main.instance.DrawCacheNPCsOverPlayers.Add(index);
            }
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            base.PostDraw(npc, spriteBatch, drawColor);
            npc.scale = cachedScale;
            if (!npc.GetEntity().HasBelly()) return;
            Texture2D texture = npc.GetEntity().GetBellyTexture();
            Rectangle rect = npc.GetEntity().GetBellyRect();

            Vector2 pos = npc.Center - Main.screenPosition;
            Vector2 offset = npc.GetEntity().GetBellyOffset();
            Vector2 origin = new Vector2(rect.Center.X - rect.Left, rect.Center.Y - rect.Top) - offset;
            Color color = drawColor.MultiplyRGB(npc.GetEntity().GetBellyColor());

            spriteBatch.Draw(texture, pos, rect, color, npc.rotation, origin, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            if (Main.dedServ) return;
            WeightedRandom<string> msg = new WeightedRandom<string>();
            if (npc.GetEntity().IsSwallowedBy(Main.LocalPlayer))
            {
                msg.Add("Hey! " + Main.LocalPlayer.GetEntity() + "! Can you hear me out there?");
                msg.Add("My body is yours to use as you see fit!");
                msg.Add("I love it in here! It's so warm.");
                msg.Add("Was I delicious?");
                if (npc.GetEntity().IsBeingDigested())
                {
                    msg.Add("Yes! Yes! Gurgle me to nothing!");
                    msg.Add("Consume me like the meat I am!");
                    msg.Add("Getting... sleepy...");
                }
                else
                {
                    msg.Add("Snug fit, but I'll make it work.");
                    msg.Add("It's so tight, and slimy...");
                    msg.Add("I can barely hear anything out there. Where are we going?");
                }
            }
            else if (Main.LocalPlayer.GetEntity().IsSwallowedBy(npc))
            {
                msg.Add("Oh god, you tasted so good going down!");
                msg.Add("I love your flavor. So sweet!");
                msg.Add("I'm so full!");
                msg.Add("Thanks " + Main.LocalPlayer.GetEntity() + "! You were delicious!");
                if (Main.LocalPlayer.GetEntity().IsBeingDigested())
                {
                    msg.Add("You're about to be nothing but fat on my gut and thighs.");
                    msg.Add("Good night, my little gut slave. It'll all be over soon...");
                    msg.Add("Would you settle down and stop squirming already?!");
                }
                else
                {
                    msg.Add("Are you getting comfortable in there?");
                    msg.Add("Just lay back and try to relax. Maybe take a nap?");
                    msg.Add("Hush now. You're safe.");
                }
            }
            else
            {
                if (npc.GetEntity().HasSwallowedAny())
                {
                    msg.Add("Man, I'm so full!");
                    msg.Add("That was delicious.");
                    msg.Add("I'm still hungry...");
                }
                if (Main.LocalPlayer.HeldItem.modItem is TalismanBase)
                {
                    msg.Add("Oh my, is that a talisman? Looking for a place to nap, or do you want the full tour?");
                    msg.Add("Is my little belly pet ready to go?");
                    msg.Add("You know, you're looking especially delicious today.");
                }
                if (Main.LocalPlayer.HeldItem.modItem is AmuletBase)
                {
                    msg.Add("An amulet, huh? I see that hungry look in your eyes...");
                    msg.Add("If it's my time to go, getting eaten by you is far from the worst way to do it.");
                    msg.Add("I'm ready; please eat me!");
                }
            }
            if (msg.elements.Count > 0) chat = msg.Get();
        }
    }
}
