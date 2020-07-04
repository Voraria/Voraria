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

		public bool hasEatenSomeone;
		public bool storedStats;
		public int storedPredHealth;
		public int storedPredDamage;
		public int storedPredDefense;
		public float storedPredStatsMult;

		public override bool InstancePerEntity => true;

		public override void SpawnNPC(int npc, int tileX, int tileY)
		{
			hasEatenSomeone = false;
		}

		public override void SetDefaults(NPC npc)
		{

		}

		public override void ResetEffects(NPC npc)
		{
			base.ResetEffects(npc);
			npc.GetEntity().ResetTick();
		}

		public override bool PreAI(NPC npc)
		{
			if (npc.GetEntity().IsSwallowed())
				return false;

			foreach (VoreEntityPlayer player in npc.GetEntity().GetAllPrey(true).OfType<VoreEntityPlayer>())
			{
				player.SetPosition(player.swallowedLocation);
				//player.GetPlayer().dead = true;
			}

			return base.PreAI(npc);
		}

		public override void AI(NPC npc)
		{
			if (!storedStats)
			{
				storedPredHealth = npc.lifeMax;
				storedPredDamage = npc.damage;
				storedPredDefense = npc.defense;
				storedPredStatsMult = 1f;

				if (npc.type == NPCID.ArmsDealer)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultArmsDealer;
				}
				if (npc.type == NPCID.Clothier)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultClothier;
				}
				if (npc.type == NPCID.Cyborg)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultCyborg;
				}
				if (npc.type == NPCID.Demolitionist)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultDemolitionist;
				}
				if (npc.type == NPCID.Dryad)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultDryad;
				}
				if (npc.type == NPCID.DyeTrader)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultDyeTrader;
				}
				/*if (npc.type == NPCID.Golfer)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultGolfer;
				}*/
				if (npc.type == NPCID.Guide)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultGuide;
				}
				if (npc.type == NPCID.Mechanic)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultMechanic;
				}
				if (npc.type == NPCID.Nurse)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultNurse;
				}
				if (npc.type == NPCID.Painter)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultPainter;
				}
				if (npc.type == NPCID.PartyGirl)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultPartyGirl;
				}
				if (npc.type == NPCID.Pirate)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultPirate;
				}
				if (npc.type == NPCID.SantaClaus)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultSanta;
				}
				if (npc.type == NPCID.Steampunker)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultSteampunker;
				}
				if (npc.type == NPCID.Stylist)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultStylist;
				}
				/*if (npc.type == NPCID.Zoologist)
				{
					storedPredStatsMult = VoreWorld.storedStatsMultZoologist;
				}*/

				if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("THIEF"))
				{
					storedPredStatsMult = VoreWorld.storedStatsMultBandit;
				}
				if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
				{
					storedPredStatsMult = VoreWorld.storedStatsMultCirrus;
				}

				if (npc.type == ModContent.NPCType<Succubus>())
				{
					storedPredStatsMult = VoreWorld.storedStatsMultSuccubus;
				}

				npc.lifeMax = (int)((float)storedPredHealth * storedPredStatsMult);
				npc.defDamage = (int)((float)storedPredDamage * storedPredStatsMult);
				npc.defDefense = (int)((float)storedPredDefense * storedPredStatsMult);

				if (npc.type == NPCID.ArmsDealer)
				{
					VoreWorld.storedStatsMultArmsDealer = storedPredStatsMult;
				}
				if (npc.type == NPCID.Clothier)
				{
					VoreWorld.storedStatsMultClothier = storedPredStatsMult;
				}
				if (npc.type == NPCID.Cyborg)
				{
					VoreWorld.storedStatsMultCyborg = storedPredStatsMult;
				}
				if (npc.type == NPCID.Demolitionist)
				{
					VoreWorld.storedStatsMultDemolitionist = storedPredStatsMult;
				}
				if (npc.type == NPCID.Dryad)
				{
					VoreWorld.storedStatsMultDryad = storedPredStatsMult;
				}
				if (npc.type == NPCID.DyeTrader)
				{
					VoreWorld.storedStatsMultDyeTrader = storedPredStatsMult;
				}
				/*if (npc.type == NPCID.Golfer)
				{
					VoreWorld.storedStatsMultGolfer = storedPredStatsMult;
				}*/
				if (npc.type == NPCID.Guide)
				{
					VoreWorld.storedStatsMultGuide = storedPredStatsMult;
				}
				if (npc.type == NPCID.Mechanic)
				{
					VoreWorld.storedStatsMultMechanic = storedPredStatsMult;
				}
				if (npc.type == NPCID.Nurse)
				{
					VoreWorld.storedStatsMultNurse = storedPredStatsMult;
				}
				if (npc.type == NPCID.Painter)
				{
					VoreWorld.storedStatsMultPainter = storedPredStatsMult;
				}
				if (npc.type == NPCID.PartyGirl)
				{
					VoreWorld.storedStatsMultPartyGirl = storedPredStatsMult;
				}
				if (npc.type == NPCID.Pirate)
				{
					VoreWorld.storedStatsMultPirate = storedPredStatsMult;
				}
				if (npc.type == NPCID.SantaClaus)
				{
					VoreWorld.storedStatsMultSanta = storedPredStatsMult;
				}
				if (npc.type == NPCID.Steampunker)
				{
					VoreWorld.storedStatsMultSteampunker = storedPredStatsMult;
				}
				if (npc.type == NPCID.Stylist)
				{
					VoreWorld.storedStatsMultStylist = storedPredStatsMult;
				}
				/*if (npc.type == NPCID.Zoologist)
				{
					VoreWorld.storedStatsMultZoologist = storedPredStatsMult;
				}*/

				if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("THIEF"))
				{
					VoreWorld.storedStatsMultBandit = storedPredStatsMult;
				}
				if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
				{
					VoreWorld.storedStatsMultCirrus = storedPredStatsMult;
				}

				if (npc.type == ModContent.NPCType<Succubus>())
				{
					VoreWorld.storedStatsMultSuccubus = storedPredStatsMult;
				}
				npc.life = npc.lifeMax;
				npc.damage = npc.defDamage;
				npc.defense = npc.defDefense;
				storedStats = true;
			}
			else
			{
				npc.lifeMax = (int)((float)storedPredHealth * storedPredStatsMult);
				npc.defDamage = (int)((float)storedPredDamage * storedPredStatsMult);
				npc.defDefense = (int)((float)storedPredDefense * storedPredStatsMult);

				if (npc.type == NPCID.ArmsDealer)
				{
					VoreWorld.storedStatsMultArmsDealer = storedPredStatsMult;
				}
				if (npc.type == NPCID.Clothier)
				{
					VoreWorld.storedStatsMultClothier = storedPredStatsMult;
				}
				if (npc.type == NPCID.Cyborg)
				{
					VoreWorld.storedStatsMultCyborg = storedPredStatsMult;
				}
				if (npc.type == NPCID.Demolitionist)
				{
					VoreWorld.storedStatsMultDemolitionist = storedPredStatsMult;
				}
				if (npc.type == NPCID.Dryad)
				{
					VoreWorld.storedStatsMultDryad = storedPredStatsMult;
				}
				if (npc.type == NPCID.DyeTrader)
				{
					VoreWorld.storedStatsMultDyeTrader = storedPredStatsMult;
				}
				/*if (npc.type == NPCID.Golfer)
				{
					VoreWorld.storedStatsMultGolfer = storedPredStatsMult;
				}*/
				if (npc.type == NPCID.Guide)
				{
					VoreWorld.storedStatsMultGuide = storedPredStatsMult;
				}
				if (npc.type == NPCID.Mechanic)
				{
					VoreWorld.storedStatsMultMechanic = storedPredStatsMult;
				}
				if (npc.type == NPCID.Nurse)
				{
					VoreWorld.storedStatsMultNurse = storedPredStatsMult;
				}
				if (npc.type == NPCID.Painter)
				{
					VoreWorld.storedStatsMultPainter = storedPredStatsMult;
				}
				if (npc.type == NPCID.PartyGirl)
				{
					VoreWorld.storedStatsMultPartyGirl = storedPredStatsMult;
				}
				if (npc.type == NPCID.Pirate)
				{
					VoreWorld.storedStatsMultPirate = storedPredStatsMult;
				}
				if (npc.type == NPCID.SantaClaus)
				{
					VoreWorld.storedStatsMultSanta = storedPredStatsMult;
				}
				if (npc.type == NPCID.Steampunker)
				{
					VoreWorld.storedStatsMultSteampunker = storedPredStatsMult;
				}
				if (npc.type == NPCID.Stylist)
				{
					VoreWorld.storedStatsMultStylist = storedPredStatsMult;
				}
				/*if (npc.type == NPCID.Zoologist)
				{
					VoreWorld.storedStatsMultZoologist = storedPredStatsMult;
				}*/

				if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("THIEF"))
				{
					VoreWorld.storedStatsMultBandit = storedPredStatsMult;
				}
				if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
				{
					VoreWorld.storedStatsMultCirrus = storedPredStatsMult;
				}

				if (npc.type == ModContent.NPCType<Succubus>())
				{
					VoreWorld.storedStatsMultSuccubus = storedPredStatsMult;
				}
			}
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

		public override void TownNPCAttackStrength(NPC npc, ref int damage, ref float knockback)
		{
			float fullDamage = damage * storedPredStatsMult;
			damage = (int)fullDamage;
			float fullKB = knockback * storedPredStatsMult;
			knockback = (int)fullKB;
		}

		public override void NPCLoot(NPC npc)
		{
			if (npc.type == NPCID.ArmsDealer)
			{
				VoreWorld.storedStatsMultArmsDealer = 1f;
			}
			if (npc.type == NPCID.Clothier)
			{
				VoreWorld.storedStatsMultClothier = 1f;
			}
			if (npc.type == NPCID.Cyborg)
			{
				VoreWorld.storedStatsMultCyborg = 1f;
			}
			if (npc.type == NPCID.Demolitionist)
			{
				VoreWorld.storedStatsMultDemolitionist = 1f;
			}
			if (npc.type == NPCID.Dryad)
			{
				VoreWorld.storedStatsMultDryad = 1f;
			}
			if (npc.type == NPCID.DyeTrader)
			{
				VoreWorld.storedStatsMultDyeTrader = 1f;
			}
			/*if (npc.type == NPCID.Golfer)
			{
				VoreWorld.storedStatsMultGolfer = 1f;
			}*/
			if (npc.type == NPCID.Guide)
			{
				VoreWorld.storedStatsMultGuide = 1f;
			}
			if (npc.type == NPCID.Mechanic)
			{
				VoreWorld.storedStatsMultMechanic = 1f;
			}
			if (npc.type == NPCID.Nurse)
			{
				VoreWorld.storedStatsMultNurse = 1f;
			}
			if (npc.type == NPCID.Painter)
			{
				VoreWorld.storedStatsMultPainter = 1f;
			}
			if (npc.type == NPCID.PartyGirl)
			{
				VoreWorld.storedStatsMultPartyGirl = 1f;
			}
			if (npc.type == NPCID.Pirate)
			{
				VoreWorld.storedStatsMultPirate = 1f;
			}
			if (npc.type == NPCID.SantaClaus)
			{
				VoreWorld.storedStatsMultSanta = 1f;
			}
			if (npc.type == NPCID.Steampunker)
			{
				VoreWorld.storedStatsMultSteampunker = 1f;
			}
			if (npc.type == NPCID.Stylist)
			{
				VoreWorld.storedStatsMultStylist = 1f;
			}
			/*if (npc.type == NPCID.Zoologist)
			{
				VoreWorld.storedStatsMultZoologist = 1f;
			}*/

			if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("THIEF"))
			{
				VoreWorld.storedStatsMultBandit = 1f;
			}
			if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
			{
				VoreWorld.storedStatsMultCirrus = 1f;
			}
			npc.GetEntity().Death();
			base.NPCLoot(npc);
		}

		public override void HitEffect(NPC npc, int hitDirection, double damage)
		{
			if (npc.life <= 0 && !VoreConfig.Instance.TweakEnableGore) VoreMod.Instance.cleanVoreGore = true;
		}

		public override bool CheckActive(NPC npc)
		{
			if (npc.GetEntity().IsSwallowed())
				return false;

			if (hasEatenSomeone)
				return false;

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
			if (target.type == NPCID.Stylist && VoreWorld.storedStatsMultStylist >= 2.00f && target.GetEntity().CanSwallow(npc))
			{
				damage = 0;
				target.GetEntity().Swallow(npc);
				return;
			}
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
			if (entity.IsSwallowed() && !entity.ShouldShowWhileSwallowed()) return;
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
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerVored, npc, entity.GetDialogueTags()), player, npc))
				{
					msgs.Add("Hey! " + player + "! Can you hear me out there?");
					msgs.Add("My body is yours to use as you see fit!");
					msgs.Add("I love it in here! It's so warm.");
					msgs.Add("Was I delicious?");
				}
				if (entity.IsBeingDigested())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerDigesting, npc, entity.GetDialogueTags()), player, npc))
					{
						msgs.Add("Yes! Yes! Gurgle me to nothing!");
						msgs.Add("Consume me like the meat I am!");
						msgs.Add("Getting... sleepy...");
					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerNonFatal, npc, entity.GetDialogueTags()), player, npc))
					{
						msgs.Add("Snug fit, but I'll make it work.");
						msgs.Add("It's so tight, and slimy...");
						msgs.Add("I can barely hear anything out there. Where are we going?");
					}
				}
			}
			else if (player.IsSwallowedBy(npc))
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.VoredPlayer, npc, entity.GetDialogueTags()), npc, player))
				{
					msgs.Add("Oh god, you tasted so good going down!");
					msgs.Add("I love your flavor. So sweet!");
					msgs.Add("I'm so full!");
					msgs.Add("Thanks " + player + "! You were delicious!");
				}
				if (player.IsBeingDigested())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.DigestingPlayer, npc, entity.GetDialogueTags()), npc, player))
					{
						msgs.Add("You're about to be nothing but fat on my gut and thighs.");
						msgs.Add("Good night, my little gut slave. It'll all be over soon...");
						msgs.Add("Would you settle down and stop squirming already?!");
					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.NonFatalPlayer, npc, entity.GetDialogueTags()), npc, player))
					{
						msgs.Add("Are you getting comfortable in there?");
						msgs.Add("Just lay back and try to relax. Maybe take a nap?");
						msgs.Add("Hush now. You're safe.");
					}
				}
			}
			else if (entity.IsSwallowed())
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.OtherVored, npc, entity.GetDialogueTags()), entity.GetPredator(), entity))
				{

				}
				if (entity.IsBeingDigested())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.OtherDigesting, npc, entity.GetDialogueTags()), entity.GetPredator(), entity))
					{

					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.OtherNonFatal, npc, entity.GetDialogueTags()), entity.GetPredator(), entity))
					{

					}
				}
			}
			else if (entity.HasSwallowedAny())
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.VoredOther, npc, entity.GetDialogueTags()), npc, player))
				{
					msgs.Add("Man, I'm so full!");
					msgs.Add("That was delicious.");
					msgs.Add("I'm still hungry...");
				}
				if (entity.IsDigestingAny())
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.DigestingOther, npc, entity.GetDialogueTags()), npc, player))
					{

					}
				}
				else
				{
					if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.NonFatalOther, npc, entity.GetDialogueTags()), npc, player))
					{

					}
				}
			}
			else if (Main.LocalPlayer.HeldItem.modItem is TalismanBase)
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerTalisman, npc, entity.GetDialogueTags()), npc, player))
				{
					msgs.Add("Oh my, is that a talisman? Looking for a place to nap, or do you want the full tour?");
					msgs.Add("Is my little belly pet ready to go?");
					msgs.Add("You know, you're looking especially delicious today.");
				}
			}
			else if (Main.LocalPlayer.HeldItem.modItem is AmuletBase)
			{
				if (!msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.PlayerAmulet, npc, entity.GetDialogueTags()), player, npc))
				{
					msgs.Add("An amulet, huh? I see that hungry look in your eyes...");
					msgs.Add("If it's my time to go, getting eaten by you is far from the worst way to do it.");
					msgs.Add("I'm ready; please eat me!");
				}
			}
			if (msgs.elements.Count == 0)
			{
				if (msgs.TryAddAll(VoreMod.GetPluginDialogues(DialogueType.Chat, npc, entity.GetDialogueTags()), npc, player))
				{
					msgs.Add(chat);
				}
			}
			if (msgs.elements.Count > 0) chat = msgs.Get();
		}
	}
}