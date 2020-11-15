using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace VoreMod
{
	class VoreWorld : ModWorld
	{
		private const int saveVersion = 0;

		#region Town NPCs: Vanilla
		#region Arms Dealer
		public static float storedStatsMultArmsDealer;
		#endregion
		#region Clothier
		public static float storedStatsMultClothier;
		#endregion
		#region Cyborg
		public static float storedStatsMultCyborg;
		#endregion
		#region Demolitionist
		public static float storedStatsMultDemolitionist;
		#endregion
		#region Dryad
		public static float storedStatsMultDryad;
		#endregion
		#region Dye Trader
		public static float storedStatsMultDyeTrader;
		#endregion
		#region Golfer (unimplemented)
		public static float storedStatsMultGolfer;
		#endregion
		#region Guide
		public static float storedStatsMultGuide;
		#endregion
		#region Mechanic
		public static float storedStatsMultMechanic;
		#endregion
		#region Nurse
		public static float storedStatsMultNurse;
		#endregion
		#region Painter
		public static float storedStatsMultPainter;
		#endregion
		#region Party Girl
		public static float storedStatsMultPartyGirl;
		#endregion
		#region Pirate
		public static float storedStatsMultPirate;
		#endregion
		#region Santa Claus
		public static float storedStatsMultSanta;
		#endregion
		#region Steampunker
		public static float storedStatsMultSteampunker;
		#endregion
		#region Stylist
		public static float storedStatsMultStylist; // best girl is here
		#endregion
		#region Steampunker
		public static float storedStatsMultTavernkeep;
		#endregion
		#region Steampunker
		public static float storedStatsMultTaxDemon;
		#endregion
		#region Steampunker
		public static float storedStatsMultTruffle;
		#endregion
		#region Steampunker
		public static float storedStatsMultWitchDoctor;
		#endregion
		#region Steampunker
		public static float storedStatsMultWizard;
		#endregion
		#region Zoologist (unimplemented)
		public static float storedStatsMultZoologist;
		#endregion
		#endregion
		#region Town NPCs: Voraria
		#region Succubus
		public static float storedStatsMultSuccubus;
		#endregion
		#endregion
		#region Town NPCs: Calamity
		#region Bandit
		public static float storedStatsMultBandit;
		#endregion
		#region Cirrus, the Drunk Princess
		public static float storedStatsMultCirrus;
		#endregion
		#endregion

		public override void Initialize()
		{
			storedStatsMultArmsDealer = 1f;
			storedStatsMultClothier = 1f;
			storedStatsMultCyborg = 1f;
			storedStatsMultDemolitionist = 1f;
			storedStatsMultDryad = 1f;
			storedStatsMultDyeTrader = 1f;
			storedStatsMultGolfer = 1f;
			storedStatsMultGuide = 1f;
			storedStatsMultMechanic = 1f;
			storedStatsMultNurse = 1f;
			storedStatsMultPainter = 1f;
			storedStatsMultPartyGirl = 1f;
			storedStatsMultPirate = 1f;
			storedStatsMultSanta = 1f;
			storedStatsMultSteampunker = 1f;
			storedStatsMultStylist = 1f;
			storedStatsMultTavernkeep = 1f;
			storedStatsMultTaxDemon = 1f;
			storedStatsMultTruffle = 1f;
			storedStatsMultWitchDoctor = 1f;
			storedStatsMultWizard = 1f;
			storedStatsMultZoologist = 1f;

			storedStatsMultSuccubus = 1f;

			storedStatsMultBandit = 1f;
			storedStatsMultCirrus = 1f;
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"armsDealerMult", storedStatsMultArmsDealer},
				{"clothierMult", storedStatsMultClothier},
				{"cyborgMult", storedStatsMultCyborg},
				{"demolitionistMult", storedStatsMultDemolitionist},
				{"dryadMult", storedStatsMultDryad},
				{"dyeTraderMult", storedStatsMultDyeTrader},
				{"golferMult", storedStatsMultGolfer},
				{"guideMult", storedStatsMultGuide},
				{"mechanicMult", storedStatsMultMechanic},
				{"nurseMult", storedStatsMultNurse},
				{"painterMult", storedStatsMultPainter},
				{"partyGirlMult", storedStatsMultPartyGirl},
				{"pirateMult", storedStatsMultPirate},
				{"santaMult", storedStatsMultSanta},
				{"steampunkerMult", storedStatsMultSteampunker},
				{"stylistMult", storedStatsMultStylist},
				{"taxmanMult", storedStatsMultTaxDemon},
				{"tavernkeepMult", storedStatsMultTavernkeep},
				{"truffleMult", storedStatsMultTruffle},
				{"witchDoctorMult", storedStatsMultWitchDoctor},
				{"wizardMult", storedStatsMultWizard},
				{"zoologistMult", storedStatsMultZoologist},

				{"succubusMult", storedStatsMultSuccubus},

				{"banditMult", storedStatsMultBandit},
				{"drunkPrincessMult", storedStatsMultCirrus},
			};
		}

		public override void Load(TagCompound tag)
		{
			storedStatsMultArmsDealer = tag.GetFloat("armsDealerMult");
			storedStatsMultClothier = tag.GetFloat("clothierMult");
			storedStatsMultCyborg = tag.GetFloat("cyborgMult");
			storedStatsMultDemolitionist = tag.GetFloat("demolitionistMult");
			storedStatsMultDryad = tag.GetFloat("dryadMult");
			storedStatsMultDyeTrader = tag.GetFloat("dyeTraderMult");
			storedStatsMultGolfer = tag.GetFloat("golferMult");
			storedStatsMultGuide = tag.GetFloat("guideMult");
			storedStatsMultMechanic = tag.GetFloat("mechanicMult");
			storedStatsMultNurse = tag.GetFloat("nurseMult");
			storedStatsMultPainter = tag.GetFloat("painterMult");
			storedStatsMultPartyGirl = tag.GetFloat("partyGirlMult");
			storedStatsMultPirate = tag.GetFloat("pirateMult");
			storedStatsMultSanta = tag.GetFloat("santaMult");
			storedStatsMultSteampunker = tag.GetFloat("steampunkerMult");
			storedStatsMultStylist = tag.GetFloat("stylistMult");
			storedStatsMultTavernkeep = tag.GetFloat("tavernkeepMult");
			storedStatsMultTaxDemon = tag.GetFloat("taxmanMult");
			storedStatsMultTruffle = tag.GetFloat("truffleMult");
			storedStatsMultWitchDoctor = tag.GetFloat("witchDoctorMult");
			storedStatsMultWizard = tag.GetFloat("wizardMult");
			storedStatsMultZoologist = tag.GetFloat("zoologistMult");

			storedStatsMultSuccubus = tag.GetFloat("succubusMult");

			storedStatsMultBandit = tag.GetFloat("banditMult");
			storedStatsMultCirrus = tag.GetFloat("drunkPrincessMult");
		}
	}
}
