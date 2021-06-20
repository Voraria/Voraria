using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using VoreMod.NPCs.VoreMod.TownNPCs;

namespace VoreMod
{
	class VoreWorld : ModSystem
	{
		private const int saveVersion = 0;

		public static float[] storedStatsMultForTownNPCs;
		public static List<int> townNPCIDs = new List<int>()
		{
			NPCID.ArmsDealer,
			NPCID.BestiaryGirl,
			NPCID.Clothier,
			NPCID.Cyborg,
			NPCID.Demolitionist,
			NPCID.Dryad,
			NPCID.DyeTrader,
			NPCID.Golfer,
			NPCID.Guide,
			NPCID.Mechanic,
			NPCID.Nurse,
			NPCID.Painter,
			NPCID.PartyGirl,
			NPCID.Pirate,
			NPCID.SantaClaus,
			NPCID.Steampunker,
			NPCID.Stylist,
			NPCID.DD2Bartender,
			NPCID.TaxCollector,
			NPCID.Truffle,
			NPCID.WitchDoctor,
			NPCID.Wizard,
		};

		public override void OnWorldLoad()
		{
			storedStatsMultForTownNPCs = new float[NPCLoader.NPCCount];
			for (int i = 0; i < NPCLoader.NPCCount; i++)
			{
				if (townNPCIDs.Contains(i))
					storedStatsMultForTownNPCs[i] = 1f;
				else
					storedStatsMultForTownNPCs[i] = 0f;
			}
		}

		public override void OnWorldUnload()
		{
			storedStatsMultForTownNPCs = new float[NPCLoader.NPCCount];
			for (int i = 0; i < NPCLoader.NPCCount; i++)
			{
				if (townNPCIDs.Contains(i))
					storedStatsMultForTownNPCs[i] = 1f;
				else
					storedStatsMultForTownNPCs[i] = 0f;
			}
		}

		public override void PostUpdateEverything()
		{
			for (int i = 0; i < Main.gore.Length; i++)
			{
				Gore gore = Main.gore[i];
				if (gore.active)
				{
					if (VoreMod.Instance.cleanVoreGore && !VoreMod.Instance.activeGore.Contains(i))
					{
						gore.active = false;
					}
					VoreMod.Instance.activeGore.Add(i);
				}
				else
				{
					VoreMod.Instance.activeGore.Remove(i);
				}
			}
			VoreMod.Instance.cleanVoreGore = false;
		}

		public override void UpdateUI(GameTime gameTime)
		{
			VoreMod.Instance.lastTime = gameTime;
			if (VoreMod.Instance.voreUI != null)
				VoreMod.Instance.voreUI.UpdateUI(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			if (VoreMod.Instance.voreUI != null)
				VoreMod.Instance.voreUI.ApplyToInterfaceLayers(layers, VoreMod.Instance.lastTime);
		}

		public override void PostDrawInterface(SpriteBatch spriteBatch)
		{
			Main.LocalPlayer.GetModPlayer<VorePlayer>().DrawUI(spriteBatch);
		}

		public override TagCompound SaveWorldData()
		{
			return new TagCompound
			{
				{"armsDealerMult", storedStatsMultForTownNPCs[NPCID.ArmsDealer]},
				{"clothierMult", storedStatsMultForTownNPCs[NPCID.Clothier]},
				{"cyborgMult", storedStatsMultForTownNPCs[NPCID.Cyborg]},
				{"demolitionistMult", storedStatsMultForTownNPCs[NPCID.Demolitionist]},
				{"dryadMult", storedStatsMultForTownNPCs[NPCID.Dryad]},
				{"dyeTraderMult", storedStatsMultForTownNPCs[NPCID.DyeTrader]},
				{"golferMult", storedStatsMultForTownNPCs[NPCID.Golfer]},
				{"guideMult", storedStatsMultForTownNPCs[NPCID.Guide]},
				{"mechanicMult", storedStatsMultForTownNPCs[NPCID.Mechanic]},
				{"nurseMult", storedStatsMultForTownNPCs[NPCID.Nurse]},
				{"painterMult", storedStatsMultForTownNPCs[NPCID.Painter]},
				{"partyGirlMult", storedStatsMultForTownNPCs[NPCID.PartyGirl]},
				{"pirateMult", storedStatsMultForTownNPCs[NPCID.Pirate]},
				{"santaMult", storedStatsMultForTownNPCs[NPCID.SantaClaus]},
				{"steampunkerMult", storedStatsMultForTownNPCs[NPCID.Steampunker]},
				{"stylistMult", storedStatsMultForTownNPCs[NPCID.Stylist]},
				{"tavernkeepMult", storedStatsMultForTownNPCs[NPCID.DD2Bartender]},
				{"taxmanMult", storedStatsMultForTownNPCs[NPCID.TaxCollector]},
				{"truffleMult", storedStatsMultForTownNPCs[NPCID.Truffle]},
				{"witchDoctorMult", storedStatsMultForTownNPCs[NPCID.WitchDoctor]},
				{"wizardMult", storedStatsMultForTownNPCs[NPCID.Wizard]},
				{"zoologistMult", storedStatsMultForTownNPCs[NPCID.BestiaryGirl]},

				{"succubusMult", storedStatsMultForTownNPCs[ModContent.NPCType<Succubus>()]},
			};
		}

		public override void LoadWorldData(TagCompound tag)
		{
			storedStatsMultForTownNPCs[NPCID.ArmsDealer] = tag.GetFloat("armsDealerMult");
			storedStatsMultForTownNPCs[NPCID.Clothier] = tag.GetFloat("clothierMult");
			storedStatsMultForTownNPCs[NPCID.Cyborg] = tag.GetFloat("cyborgMult");
			storedStatsMultForTownNPCs[NPCID.Demolitionist] = tag.GetFloat("demolitionistMult");
			storedStatsMultForTownNPCs[NPCID.Dryad] = tag.GetFloat("dryadMult");
			storedStatsMultForTownNPCs[NPCID.DyeTrader] = tag.GetFloat("dyeTraderMult");
			storedStatsMultForTownNPCs[NPCID.Golfer] = tag.GetFloat("golferMult");
			storedStatsMultForTownNPCs[NPCID.Guide] = tag.GetFloat("guideMult");
			storedStatsMultForTownNPCs[NPCID.Mechanic] = tag.GetFloat("mechanicMult");
			storedStatsMultForTownNPCs[NPCID.Nurse] = tag.GetFloat("nurseMult");
			storedStatsMultForTownNPCs[NPCID.Painter] = tag.GetFloat("painterMult");
			storedStatsMultForTownNPCs[NPCID.PartyGirl] = tag.GetFloat("partyGirlMult");
			storedStatsMultForTownNPCs[NPCID.Pirate] = tag.GetFloat("pirateMult");
			storedStatsMultForTownNPCs[NPCID.SantaClaus] = tag.GetFloat("santaMult");
			storedStatsMultForTownNPCs[NPCID.Steampunker] = tag.GetFloat("steampunkerMult");
			storedStatsMultForTownNPCs[NPCID.Stylist] = tag.GetFloat("stylistMult");
			storedStatsMultForTownNPCs[NPCID.DD2Bartender] = tag.GetFloat("tavernkeepMult");
			storedStatsMultForTownNPCs[NPCID.TaxCollector] = tag.GetFloat("taxmanMult");
			storedStatsMultForTownNPCs[NPCID.Truffle] = tag.GetFloat("truffleMult");
			storedStatsMultForTownNPCs[NPCID.WitchDoctor] = tag.GetFloat("witchDoctorMult");
			storedStatsMultForTownNPCs[NPCID.Wizard] = tag.GetFloat("wizardMult");
			storedStatsMultForTownNPCs[NPCID.BestiaryGirl] = tag.GetFloat("zoologistMult");

			storedStatsMultForTownNPCs[ModContent.NPCType<Succubus>()] = tag.GetFloat("succubusMult");
		}
	}
}
