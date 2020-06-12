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
using Terraria.GameContent.Events;

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
				if (npc.type == NPCID.PartyGirl)
				{
					if (npc.GetEntity().IsBeingDigested())
					{
						msg.Add("U-uhhh...I dunno if I like this party anymore...");
						msg.Add("You're...you're gonna let me out soon, right? Hello? " + Main.LocalPlayer.GetEntity() + "?");
						msg.Add("Mmf...w--well, I hope you enjoyed me, at least! Like a big, delicious cake...");
					}
					else
					{
						msg.Add("Ooo, I always love these sorts of parties! They're so comfy and warm...");
						msg.Add("Hey, can you send down some balloons and cupcakes? I'm kinda hungry too...");
						msg.Add("Mmm...might take a nap in here...so nice and cozy...");
					}
				}
				else
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
			}
			else if (Main.LocalPlayer.GetEntity().IsSwallowedBy(npc))
			{
				if (npc.type == NPCID.Stylist)
				{
					msg.Add("Mmmf...something about your scalp just speaks volumes to my taste buds...");
					msg.Add("This is MUCH better than just doing your hair with scissors and stuff...much more filling, too!");
					if (Main.LocalPlayer.GetEntity().IsBeingDigested())
					{
						msg.Add("Is...is this how all those spiders feel when they eat their prey? I'll have to try this a bit more myself...");
						msg.Add("Well, hey, you've got an easy cut. Just stick your head in those acids for a bit, your hair'll be nice and short!");
						msg.Add("Look, we'll cut a deal: you stay and digest, and I give you a discount on your next cut. Will that make you stop squirming?");
					}
					else
					{
						msg.Add("Now, be careful not to rub against the walls too much. You wouldn't want your hair to get ruined.");
						msg.Add("Alright, sit in there and marinate for now, I'll drink some water to rinse your color out in about 25 minutes...");
					}
				}
				else if (npc.type == NPCID.PartyGirl)
				{
					msg.Add("Ooo, you went down like a big, delicious cake! I SO wa1nna eat you again later...");
					msg.Add("You havin' fun at the party in my belly, " + Main.LocalPlayer.GetEntity() + "?");
					msg.Add("Oof, I feel so heavy now...I hope I can still have parties with you in there...");
					msg.Add("Hehee, look at how big you made me! Like a Pigronata full of candy!~");
					if (Main.LocalPlayer.GetEntity().IsBeingDigested())
					{
						msg.Add("Mmm...I really want cupcakes for dessert...not that they'd taste better than you, " + Main.LocalPlayer.GetEntity() + ".");
						msg.Add("Aww, trying to leave so soon? The party's not over yet, silly! You gotta clean up after any good rave...");
						msg.Add("Well, look at it this way: once you're belly fat, you'll always help me stay the life of the party! :D");
					}
					else
					{
						msg.Add("See? It's not so bad! Just relax and have a great time!");
						msg.Add("Aren't my belly's parties the best? I try to invite other people, but they don't seem too excited...");
						msg.Add("You know, you can sleep in there if you want...slumber parties are always great, especially when they're so tasty and filling!");
						msg.Add("I should totally eat some balloons and party favors...no party's complete without 'em, not even a belly party!");
					}
				}
				else if (npc.type == NPCID.Mechanic)
				{
					msg.Add("Mmm...");
					msg.Add("You havin' fun at the party in my belly, " + Main.LocalPlayer.GetEntity() + "?");
					msg.Add("Oof, I feel so heavy now...I hope I can still have parties with you in there...");
					msg.Add("Hehee, look at how big you made me! Like a Pigronata full of candy!~");
					if (Main.LocalPlayer.GetEntity().IsBeingDigested())
					{
						msg.Add("Mmm...I really want cupcakes for dessert...not that they'd taste better than you, " + Main.LocalPlayer.GetEntity() + ".");
						msg.Add("Aww, trying to leave so soon? The party's not over yet, silly! You gotta clean up after any good rave...");
						msg.Add("Well, look at it this way: once you're belly fat, you'll always help me stay the life of the party! :D");
					}
					else
					{
						msg.Add("See? It's not so bad! Just relax and have a great time!");
						msg.Add("Aren't my belly's parties the best? I try to invite other people, but they don't seem too excited...");
						msg.Add("You know, you can sleep in there if you want...slumber parties are always great, especially when they're so tasty and filling!");
						msg.Add("I should totally eat some balloons and party favors...no party's complete without 'em, not even a belly party!");
					}
				}
				else if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("THIEF"))
				{
					msg.Add("...I wonder if I could use this belly to pass as being pregnant and get some sweet deals...");
					if (Main.LocalPlayer.GetEntity().IsBeingDigested())
					{
						msg.Add("If anyone asks, I'm not stealing you. I'm just borrowing you until you digest.");
						msg.Add("You'd be surprised, but you're not the first person I've eaten...and you won't be the last.");
					}
					else
					{
						msg.Add("There you go...take a nap, relax in the best lootin' bag there is.");
					}
				}
				else if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
				{
					msg.Add("Mmmf...part of me wants a chaser, but part of me wants to keep your awesome flavor on my tongue...~");
					msg.Add("Still think I can't stomach you and a- " + $"[c/00FF00:*HIC!*]" + " -keg of beer, buster?");
					msg.Add("Will you quit your bellyachin' in there? You'll probably come right back to fondle the bod you're adding to, anyway.~");
					msg.Add("...huh? Let you out? I don't remember sayin' anything about lettin' you out...");
					msg.Add("Keep it up in there, it feels awesome...but quit screamin' or I'm drowning you in a keg's worth of beer in there.");
				}
				else
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
			}
			else
			{
				if (npc.GetEntity().HasSwallowedAny())
				{
					if (npc.type == NPCID.Stylist)
					{
						msg.Add("Oh, you want a haircut? Just sit in the chair, I'll do yours once I'm done with this one...");
						msg.Add("Does this belly make my hair look bad? Give me an honest answer, I won't judge.");
						msg.Add("Alright, just a minute, I'll be with you once my current client's done...what kinda cut do you want?");
					}
					else if (npc.type == NPCID.PartyGirl)
					{
						msg.Add("Oohhh, it's always so fun to throw a party in my belly! You wanna join in?");
						msg.Add("Hey, do you have some balloons for me to eat? I think this tum needs to be a little more festive...");
						msg.Add("Happy birthday to you, happy " + $"[c/00FF00:*BURP!*]" + "-day to you...");
					}
					else if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
					{
						msg.Add("Hey, can you get me a keg of wine? I need a chaser, just ate a big meal...");
						msg.Add("What? This nerd dared me to stomach them and a full bottle of everclear! That's free food and free money, I'm not gonna turn it down!");
						msg.Add("Haha, look at this gut...it's like I've got my own personal beer keg right here.~");
						msg.Add("God, my back hurts so much more with this chump hanging off me." + (npc.GetEntity().IsDigestingAny() ? "They're gonna make my assets even heavier, too..." : ""));
					}
					else
					{
						msg.Add("Man, I'm so full!");
						msg.Add("That was delicious.");
						msg.Add("I'm still hungry...");
					}
				}
				if (Main.LocalPlayer.HeldItem.modItem is TalismanBase)
				{
					if (npc.type == NPCID.Stylist)
					{
						msg.Add("Hey, your head looks a bit messy...howsabout a special kind of soak for that hair?~");
						msg.Add("Just a little off the top? Honey, stomach acid's gonna get you way more than that...");
						msg.Add("That hair of yours looks particularly tasty today...mind letting me have a taste?~");
						msg.Add("Alright, just stand here...what kinda cut can my belly and I do for you today?");
					}
					else if (npc.type == NPCID.Dryad)
					{
						msg.Add("Hmm...you know, it's been a long time since I've had a live meal...");
						msg.Add("Why is my stomach rumbling, you ask? I'm a tree nymph, we eat other creatures rather often.");
						msg.Add("You've been off so much purifying the world...how about cleansing my stomach for a change?");
					}
					else if (npc.type == NPCID.PartyGirl)
					{
						msg.Add("Ooo, that necklace looks cool...and you smell like a tasty cupcake with it on, too!");
						msg.Add("Heyyyy! There's a party in my belly, and you're invited! Wanna stop by?");
						msg.Add("You smell like a yummy cake...come on, lemme have a taste! Just one bite, I promise!");
					}
					else if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("THIEF"))
					{
						msg.Add("You know, you smell good enough to eat...how about you let me \"steal\" you away for a little bit?~");
						msg.Add("Hey, you wanna help me practice swallowin' stuff? A thief's gotta do it more than you think...");
						msg.Add("Fun fact: I've stolen a couple of really pricey things before, and now they're just belly fat. Shame, too, could've made 'em into more money instead of more me.");
						msg.Add("The feeling of having someone in my gut is more priceless than any relic, I swear...mind gettin' in for a bit?");
					}
					else if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
					{
						msg.Add("Hmhm...betcha a bottle of booze you can't last a day in my gut.~");
						msg.Add("Huh? Whaddaya mean I can't stomach you with a keg of booze? Is that a challenge!?");
						msg.Add("You know, I could probably eat you and then forget you went in there within the hour...crazy, huh?~");
						msg.Add("Whuh? You wanna add to THESE girls? Kid, I've already got back problems WITHOUT you hanging on my gut...but alright.~");
						msg.Add("A nap? Inside me? Alright, but fair warning, kid: once you go in there, you might not get back out...");
					}
					else
					{
						msg.Add("Oh my, is that a talisman? Looking for a place to nap, or do you want the full tour?");
						msg.Add("Is my little belly pet ready to go?");
						msg.Add("You know, you're looking especially delicious today.");
					}
				}
				if (Main.LocalPlayer.HeldItem.modItem is AmuletBase)
				{
					if (npc.type == NPCID.PartyGirl)
					{
						msg.Add("Ooo, you look hungry...m-maybe I can give you a cake or something?");
						msg.Add("Oh, hey there! Got any exciting plans, or just here to have some cupcakes?...o-or me...?");
						msg.Add("Wait, a party in YOUR belly?...that sounds awesome, lemme in!");
					}
					else
					{
						msg.Add("An amulet, huh? I see that hungry look in your eyes...");
						msg.Add("If it's my time to go, getting eaten by you is far from the worst way to do it.");
						msg.Add("I'm ready; please eat me!");
					}
				}
			}
			if (msg.elements.Count > 0) chat = msg.Get();
		}
	}
}
