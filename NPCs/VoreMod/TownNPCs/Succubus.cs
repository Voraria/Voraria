
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using VoreMod.Items.VoreMod.Amulets;
using VoreMod.Items.VoreMod.Charms;
using VoreMod.Items.VoreMod.Talismans;
using Terraria.Audio;

namespace VoreMod.NPCs.VoreMod.TownNPCs
{
	[AutoloadHead]
	public class Succubus : ModNPC
	{
		const int SOURCE_ID = NPCID.Dryad;

		public override string Texture => nameof(VoreMod) + "/NPCs/" + nameof(VoreMod) + "/TownNPCs/" + nameof(Succubus);

		int helpIndex = 0;

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[SOURCE_ID];
			NPCID.Sets.ExtraFramesCount[npc.type] = NPCID.Sets.ExtraFramesCount[SOURCE_ID];
			NPCID.Sets.AttackFrameCount[npc.type] = NPCID.Sets.AttackFrameCount[SOURCE_ID];
			NPCID.Sets.DangerDetectRange[npc.type] = NPCID.Sets.DangerDetectRange[SOURCE_ID];
			NPCID.Sets.AttackType[npc.type] = NPCID.Sets.AttackType[SOURCE_ID];
			NPCID.Sets.AttackTime[npc.type] = 120;
			NPCID.Sets.AttackAverageChance[npc.type] = NPCID.Sets.AttackAverageChance[SOURCE_ID];
			NPCID.Sets.HatOffsetY[npc.type] = NPCID.Sets.HatOffsetY[SOURCE_ID];
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.lifeMax = 250;
			npc.damage = 10;
			npc.defense = 15;
			npc.knockBackResist = 0.5f;
			npc.HitSound = SoundID.NPCHit1;
			animationType = SOURCE_ID;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return true;
		}

		public override string TownNPCName()
		{
			WeightedRandom<string> names = new WeightedRandom<string>();
			names.Add("Lilith");
			names.Add("Jezebel");
			names.Add("Lucy");
			names.Add("Morrigan");
			names.Add("Jennifer");
			return names;
		}

		public override void FindFrame(int frameHeight)
		{

		}

		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add("I could really go for a meal right now... You offering?");
			chat.Add("Man, I'm starving...");
			chat.Add("In this world, it's eat or be eaten.");
			chat.Add("So you interested in vore, or what?");
			chat.Add("Got some charms I'm looking to sell. Might be useful to a morsel-- I mean, mortal-- like you.");
			return chat;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Help";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
			else
			{
				switch (helpIndex++)
				{
					case 0:
						Main.npcChatText = "If you're looking to fill your stomach, you'll want to buy a starter amulet, courtesy of yours truly, or craft a better one from gems and metal bars at an anvil. That'll allow you to swallow prey the way us succubi do. They'll struggle, though, and might be able to escape if you're not careful. You can spit them back up at any time. If you want your prey to actually go away and not just sit in your stomach forever, you'll need an acid charm to digest them.";
						break;
					case 1:
						Main.npcChatText = "If you're looking to get yourself eaten, the monsters will be happy to oblige. You can also force others to eat you using a talisman, which can be purchased from me or crafted at a workbench from gems and wood. If you want out, hold down the jump key to keep struggling and hopefully you'll break free before it's too late. Also, your friends aren't normally in the mood to digest you, but you'll be irresistable if you put on a hunger charm.";
						break;
					case 2:
						Main.npcChatText = "Charms are little trinkets made with metal bars and certain other ingredients at workbenches, and have various effects when equipped. For example, coat a life crystal in a thin layer of iron, and you've got yourself a life charm, which'll allow you to regenerate health when you digest prey! Just equip a charm in the appropriate equipment slot, and you're good to gurgle.";
						break;
					case 3:
						Main.npcChatText = "Some townsfolk can do weird things to the people they swallow. For example, the nurse heals you while you're in her belly, or the dryad restores your mana! This doesn't work if they're digesting you because of a hunger charm, though.";
						break;
					default:
						Main.npcChatText = "That's about all I have to say. Want me to start over?";
						helpIndex = 0;
						break;
				}
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot++].SetDefaults(ModContent.ItemType<TalismanThroat>());
			if (WorldGen.SavedOreTiers.Copper == TileID.Copper)
			{
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<AmuletThroatCopper>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmAcidCopper>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmHungerCopper>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmLifeCopper>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmManaCopper>());
			}
			else
			{
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<AmuletThroatTin>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmAcidTin>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmHungerTin>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmLifeTin>());
				shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CharmManaTin>());
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextBool(8))
				Item.NewItem(npc.getRect(), ItemID.DemonScythe);

			if (!npc.GetEntity().IsBeingDigested())
				SoundEngine.PlaySound(SoundID.NPCDeath1);

			VoreWorld.storedStatsMultSuccubus = 1f;
		}

		public override bool CanGoToStatue(bool toKingStatue)
		{
			return !toKingStatue;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackMagic(ref float auraLightMultiplier)
		{
			auraLightMultiplier = 1f;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ProjectileID.DemonScythe;
			attackDelay = 90;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 0f;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 40;
			itemHeight = 40;
		}
	}
}
