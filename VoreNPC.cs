using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VoreMod.Items;
using VoreMod.Items.VoreMod.Amulets;
using VoreMod.NPCs.VoreMod.TownNPCs;

namespace VoreMod
{
	public class VoreNPC : GlobalNPC
	{
		public VoreEntity entity;

		float cachedScale;

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
			if (target.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP") && target.GetEntity().CanSwallow(npc))
			{
				damage = 0;
				target.GetEntity().Swallow(npc);
				return;
			}
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

		public static void DrawLayer(NPC npc, SpriteBatch batch, Color drawColor, SpriteType type)
		{
			VoreEntity entity = npc.GetEntity();
			if (!entity.HasSprites(type)) return;
			foreach (VoreSprite sprite in entity.GetSprites(type))
			{
				float ratio = MathHelper.Clamp(entity.GetBellyRatio(), 0f, 1f);

				Texture2D texture = sprite.GetTexture();

				int animFrames = Main.npcFrameCount[npc.type];
				int animFrame = npc.frame.Y / (texture.Height / animFrames);
				Rectangle rect = sprite.GetRect(texture, ratio, animFrames, animFrame);

				Vector2 pos = npc.Center - Main.screenPosition;
				Vector2 offset = sprite.GetOffset(animFrame) * new Vector2(npc.direction, 1f);
				Vector2 origin = new Vector2(rect.Center.X - rect.Left, rect.Center.Y - rect.Top) - offset;

				Color color = sprite.GetColor().MultiplyRGB(drawColor);

				pos.X = npc.direction == -1 ? (int)Math.Floor(pos.X) : (int)Math.Ceiling(pos.X);
				pos.Y = (int)Math.Ceiling(pos.Y);

				batch.Draw(texture, pos, rect, color, npc.rotation, origin, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
		}

		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
		{
			base.PostDraw(npc, spriteBatch, drawColor);
			if (!npc.GetEntity().IsSwallowed() || npc.GetEntity().ShouldShowWhileSwallowed())
			{
				DrawLayer(npc, spriteBatch, drawColor, SpriteType.Belly);
			}
			npc.scale = cachedScale;
		}

		public override void GetChat(NPC npc, ref string chat)
		{
			if (Main.dedServ) return;
			WeightedRandom<string> msgs = new WeightedRandom<string>();

			VoreEntity entity = npc.GetEntity();
			VoreEntity player = Main.LocalPlayer.GetEntity();

			if (entity.IsSwallowedBy(player))
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerVored, npc), player, npc))
				{
					msgs.Add("Hey! " + player + "! Can you hear me out there?");
					msgs.Add("My body is yours to use as you see fit!");
					msgs.Add("I love it in here! It's so warm.");
					msgs.Add("Was I delicious?");
				}
				if (entity.IsBeingDigested())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerDigesting, npc), player, npc))
					{
						msgs.Add("Yes! Yes! Gurgle me to nothing!");
						msgs.Add("Consume me like the meat I am!");
						msgs.Add("Getting... sleepy...");
					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerNonFatal, npc), player, npc))
					{
						msgs.Add("Snug fit, but I'll make it work.");
						msgs.Add("It's so tight, and slimy...");
						msgs.Add("I can barely hear anything out there. Where are we going?");
					}
				}
			}
			else if (player.IsSwallowedBy(npc))
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.VoredPlayer, npc), npc, player))
				{
					msgs.Add("Oh god, you tasted so good going down!");
					msgs.Add("I love your flavor. So sweet!");
					msgs.Add("I'm so full!");
					msgs.Add("Thanks " + player + "! You were delicious!");
				}
				if (player.IsBeingDigested())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.DigestingPlayer, npc), npc, player))
					{
						msgs.Add("You're about to be nothing but fat on my gut and thighs.");
						msgs.Add("Good night, my little gut slave. It'll all be over soon...");
						msgs.Add("Would you settle down and stop squirming already?!");
					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.NonFatalPlayer, npc), npc, player))
					{
						msgs.Add("Are you getting comfortable in there?");
						msgs.Add("Just lay back and try to relax. Maybe take a nap?");
						msgs.Add("Hush now. You're safe.");
					}
				}
			}
			else if (entity.IsSwallowed())
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.OtherVored, npc), entity.GetPredator(), entity))
				{

				}
				if (entity.IsBeingDigested())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.OtherDigesting, npc), entity.GetPredator(), entity))
					{

					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.OtherNonFatal, npc), entity.GetPredator(), entity))
					{

					}
				}
			}
			else if (entity.HasSwallowedAny())
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.VoredOther, npc), npc, player))
				{
					msgs.Add("Man, I'm so full!");
					msgs.Add("That was delicious.");
					msgs.Add("I'm still hungry...");
				}
				if (entity.IsDigestingAny())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.DigestingOther, npc), npc, player))
					{

					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.NonFatalOther, npc), npc, player))
					{

					}
				}
			}
			else if (Main.LocalPlayer.HeldItem.modItem is TalismanBase)
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerTalisman, npc), npc, player))
				{
					msgs.Add("Oh my, is that a talisman? Looking for a place to nap, or do you want the full tour?");
					msgs.Add("Is my little belly pet ready to go?");
					msgs.Add("You know, you're looking especially delicious today.");
				}
			}
			else if (Main.LocalPlayer.HeldItem.modItem is AmuletBase)
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerAmulet, npc), player, npc))
				{
					msgs.Add("An amulet, huh? I see that hungry look in your eyes...");
					msgs.Add("If it's my time to go, getting eaten by you is far from the worst way to do it.");
					msgs.Add("I'm ready; please eat me!");
				}
			}
			if (msgs.elements.Count > 0) chat = msgs.Get();
		}
	}
}
