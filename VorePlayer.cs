using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using VoreMod.Buffs;
using VoreMod.Items;
using VoreMod.Items.VoreMod.Charms;
using VoreMod.UI;

namespace VoreMod
{
	public class VorePlayer : ModPlayer
	{
		public VoreEntity entity;

		public bool lastHitFromPVP;

		public bool wasDigested;

		List<CharmSlot> charmSlots = new List<CharmSlot>();

		public class BellyLayer : PlayerDrawLayer
		{
			public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Torso);

			protected override void Draw(ref PlayerDrawSet drawInfo)
			{
				DrawLayer(ref drawInfo, SpriteType.Belly);
			}

			protected static void DrawLayer(ref PlayerDrawSet drawInfo, SpriteType type)
			{
				Player Player = drawInfo.drawPlayer;
				VoreEntity entity = Player.GetEntity();
				if (entity.IsSwallowed() && !entity.ShouldShowWhileSwallowed()) return;
				if (!entity.HasSprites(type)) return;
				foreach (VoreSprite sprite in entity.GetSprites(type))
				{
					float ratio = MathHelper.Clamp(entity.GetBellyRatio() / 2f, 0f, 1f);

					Texture2D texture = sprite.GetTexture();

					int animFrames = texture.Height / 56;
					int animFrame = Player.bodyFrame.Y / 56;
					Rectangle rect = sprite.GetRect(texture, ratio, animFrames, animFrame);

					Vector2 pos = Player.Center - Main.screenPosition;
					Vector2 offset = sprite.GetOffset(animFrame) * new Vector2(Player.direction, 1f);
					offset.Y += Player.mount.PlayerOffset;
					Vector2 origin = new Vector2(rect.Center.X - rect.Left, rect.Center.Y - rect.Top) - offset;

					pos.X = Player.direction == -1 ? (int)Math.Floor(pos.X) : (int)Math.Ceiling(pos.X);
					pos.Y = (int)Math.Ceiling(pos.Y);

					Color color = sprite.GetColor();
					switch (sprite.GetColorMode())
					{
						case ColorMode.Default:
							color = color.MultiplyRGB(Lighting.GetColor((int)((drawInfo.drawPlayer.position.X + Player.width / 2f) / 16f), (int)((drawInfo.drawPlayer.position.Y + Player.height / 2f) / 16f)));
							break;
						case ColorMode.Skin:
							color = color.MultiplyRGB(drawInfo.colorBodySkin);
							break;
						case ColorMode.Dye:
							color = color.MultiplyRGB(drawInfo.colorArmorBody);
							break;
					}

					DrawData data = new DrawData(texture, pos, rect, color, 0f, origin, 1f, Player.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
					drawInfo.DrawDataCache.Add(data);
				}
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

		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			VoreEntity ent = drawInfo.drawPlayer.GetEntity();
			if (ent.IsSwallowed() && !ent.ShouldShowWhileSwallowed())
			{
				drawInfo.drawPlayer.position = new Vector2(-100f, -100f);
			}
		}

		public override void ResetEffects()
		{
			base.ResetEffects();
			Player.GetEntity().ResetTick();
			wasDigested = false;

			foreach (CharmSlot slot in charmSlots)
			{
				CharmBase charm = slot.Item.ModItem as CharmBase;
				if (charm != null) Player.GetEntity().ApplyCharm(charm.Effect, charm.Tier);
			}
		}

		public override void PreUpdateBuffs()
		{
			VoreEntity entity = Player.GetEntity();
			if (entity.IsBeingDigested()) Player.AddBuff(DigestingBuff.BuffType, 2);
			if (entity.IsSwallowed() && !entity.IsBeingDigested()) Player.AddBuff(SwallowedBuff.BuffType, 2);

		}

		public override void PostUpdateBuffs()
		{
			if (Player.GetEntity().IsSwallowed()) Player.velocity = Vector2.Zero;
			Player.GetEntity().UpdateTick();
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
		Don't touch this method. If you do...
		...your life shall come to a swift and abrupt end.
		Makes sense, right?
		Good.
		Now, leave me to my work.
		 - the pixelated Sign Painter
		*/
		public void KillMe(PlayerDeathReason damageSource, double dmg, int hitDirection)
		{
			bool pvp = lastHitFromPVP;
			if (Player.dead)
			{
				return;
			}
			bool playSound = true;
			bool genGore = true;
			if (!PlayerLoader.PreKill(Player, dmg, hitDirection, pvp, ref playSound, ref genGore, ref damageSource))
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
				Player.pvpDeath = true;
			}
			if (Player.trapDebuffSource)
			{
				AchievementsHelper.HandleSpecialEvent(Player, 4);
			}
			Player.lastDeathPostion = Player.Center;
			Player.lastDeathTime = DateTime.Now;
			Player.showLastDeath = true;
			bool flag4;
			int coinsOwned = (int)Utils.CoinsCount(out flag4, Player.inventory);
			if (Main.myPlayer == Player.whoAmI)
			{
				Player.lostCoins = coinsOwned;
				Player.lostCoinString = Main.ValueToCoins(Player.lostCoins);
			}
			if (Main.myPlayer == Player.whoAmI)
			{
				Main.mapFullscreen = false;
			}
			if (Main.myPlayer == Player.whoAmI)
			{
				Player.trashItem.SetDefaults();
				if (Player.difficulty == 0)
				{
					for (int k = 0; k < 59; k++)
					{
						if (Player.inventory[k].stack > 0 && ((Player.inventory[k].type >= ItemID.LargeAmethyst && Player.inventory[k].type <= ItemID.LargeDiamond) || Player.inventory[k].type == ItemID.LargeAmber))
						{
							if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
							{
								int num5 = Item.NewItem((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height, Player.inventory[k].type);
								Main.item[num5].netDefaults(Player.inventory[k].netID);
								Main.item[num5].Prefix(Player.inventory[k].prefix);
								Main.item[num5].stack = Player.inventory[k].stack;
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
							Player.inventory[k].SetDefaults();
						}
					}
				}
				else if (Player.difficulty == 1)
				{
					if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
						Player.DropItems();
					else
						LoseDigestedItems();
				}
				else if (Player.difficulty == 2)
				{
					if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
						Player.DropItems();
					else
						LoseDigestedItems();
					Player.KillMeForGood();
				}
			}
			if (!wasDigested && playSound)
			{
				SoundEngine.PlaySound(SoundID.PlayerKilled, (int)Player.position.X, (int)Player.position.Y);
			}
			Player.headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			Player.bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			Player.legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			Player.headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			Player.bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			Player.legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			if (Player.stoned || !genGore)
			{
				Player.headPosition = Vector2.Zero;
				Player.bodyPosition = Vector2.Zero;
				Player.legPosition = Vector2.Zero;
			}
			if (!wasDigested && genGore)
			{
				for (int j = 0; j < 100; j++)
				{
					if (Player.stoned)
					{
						Dust.NewDust(Player.position, Player.width, Player.height, 1, 2 * hitDirection, -2f);
					}
					else if (Player.frostArmor)
					{
						int num4 = Dust.NewDust(Player.position, Player.width, Player.height, 135, 2 * hitDirection, -2f);
						Main.dust[num4].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
					}
					else if (Player.boneArmor)
					{
						int num3 = Dust.NewDust(Player.position, Player.width, Player.height, 26, 2 * hitDirection, -2f);
						Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
					}
					else
					{
						Dust.NewDust(Player.position, Player.width, Player.height, 5, 2 * hitDirection, -2f);
					}
				}
			}
			Player.mount.Dismount(Player);
			Player.dead = true;
			Player.respawnTimer = 600;
			bool flag3 = false;
			if (Main.netMode != NetmodeID.SinglePlayer && !pvp)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && (Main.npc[i].boss || (Main.npc[i].type >= NPCID.EaterofWorldsHead && Main.npc[i].type <= NPCID.EaterofWorldsTail)) && Math.Abs(Player.Center.X - Main.npc[i].Center.X) + Math.Abs(Player.Center.Y - Main.npc[i].Center.Y) < 4000f)
					{
						flag3 = true;
						break;
					}
				}
			}
			if (flag3)
			{
				Player.respawnTimer += 600;
			}
			if (Main.expertMode)
			{
				Player.respawnTimer = (int)((double)Player.respawnTimer * 1.5);
			}
			if (!wasDigested)
				PlayerLoader.Kill(Player, dmg, hitDirection, pvp, damageSource);
			else
			{
				Player.GetEntity().Death();
			}
			Player.immuneAlpha = 0;
			if (wasDigested)
				Player.immuneAlpha = 255;
			Player.palladiumRegen = false;
			Player.iceBarrier = false;
			Player.crystalLeaf = false;
			NetworkText deathText = damageSource.GetDeathText(Player.name);
			if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(deathText, new Color(225, 25, 25));
			}
			else if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText(deathText.ToString(), 225, 25, 25);
			}
			if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
			{
				NetMessage.SendPlayerDeath(Player.whoAmI, damageSource, (int)dmg, hitDirection, pvp);
			}
			if (Player.whoAmI == Main.myPlayer && Player.difficulty == 0)
			{
				if (!pvp)
				{
					if (!wasDigested || !VoreConfig.Instance.TweakItemsAreFood)
						Player.DropCoins();
					else
						LoseDigestedCoins();
				}
				else
				{
					Player.lostCoins = 0;
					Player.lostCoinString = Main.ValueToCoins(Player.lostCoins);
				}
			}
			if (!wasDigested)
				Player.DropTombstone(coinsOwned, deathText, hitDirection);

			if (Player.whoAmI == Main.myPlayer)
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
			List<Item> startInventory = PlayerLoader.GetStartingItems(Player, DropItems_GetDefaults().Where(item => !item.IsAir), true);
			PlayerLoader.SetStartInventory(Player, startInventory);
			Main.mouseItem = new Item();
		}

		private IEnumerable<Item> DropItems_GetDefaults()
		{
			var inventory = new Item[Player.inventory.Length];

			for (int i = 0; i < inventory.Length; i++)
			{
				inventory[i] = new Item();
			}

			inventory[0].SetDefaults(3507);
			inventory[0].Prefix(-1);
			inventory[1].SetDefaults(3509);
			inventory[1].Prefix(-1);
			inventory[2].SetDefaults(3506);
			inventory[2].Prefix(-1);

			return inventory;
		}

		public int LoseDigestedCoins()
		{
			int num6 = 0;
			for (int i = 0; i < 59; i++)
			{
				if (Player.inventory[i].type >= 71 && Player.inventory[i].type <= 74)
				{
					int num5 = Item.NewItem((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height, Player.inventory[i].type);
					int num4 = Player.inventory[i].stack / 2;
					if (Main.expertMode)
					{
						num4 = (int)((double)Player.inventory[i].stack * 0.25);
					}
					num4 = Player.inventory[i].stack - num4;
					Player.inventory[i].stack -= num4;
					if (Player.inventory[i].type == 71)
					{
						num6 += num4;
					}
					if (Player.inventory[i].type == 72)
					{
						num6 += num4 * 100;
					}
					if (Player.inventory[i].type == 73)
					{
						num6 += num4 * 10000;
					}
					if (Player.inventory[i].type == 74)
					{
						num6 += num4 * 1000000;
					}
					if (Player.inventory[i].stack <= 0)
					{
						Player.inventory[i] = new Item();
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
						Main.mouseItem = Player.inventory[i].Clone();
					}
				}
			}
			Player.lostCoins = num6;
			Player.lostCoinString = Main.ValueToCoins(Player.lostCoins);
			return num6;
		}

		public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
		{
			if (!npc.GetEntity().CanDamage(Player.GetEntity())) return false;
			return base.CanBeHitByNPC(npc, ref cooldownSlot);
		}

		public override bool CanBeHitByProjectile(Projectile proj)
		{
			if (!Player.GetEntity().CanBeDamaged()) return false;
			return base.CanBeHitByProjectile(proj);
		}

		public override bool? CanHitNPC(Item item, NPC target)
		{
			if (!Player.GetEntity().CanDamage(target)) return false;
			if (item.ModItem is TalismanBase)
				return target.GetEntity().CanSwallow(Player);
			return base.CanHitNPC(item, target);
		}

		public override bool CanHitPvp(Item item, Player target)
		{
			if (!Player.GetEntity().CanDamage(target)) return false;
			if (item.ModItem is TalismanBase)
				return target.GetEntity().CanSwallow(Player);
			return base.CanHitPvp(item, target);
		}

		public override bool PreItemCheck()
		{
			if (Player.GetEntity().IsSwallowed())
				return false;
			return base.PreItemCheck();
		}

		public override void NaturalLifeRegen(ref float regen)
		{
			if (Player.GetEntity().IsSwallowed())
				regen = 0f;
		}
	}
}
