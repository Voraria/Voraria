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
		public static int storedPlayerSnacksArmsDealer;
		#endregion
		#region Clothier
		public static float storedStatsMultClothier;
		public static int storedPlayerSnacksClothier;
		#endregion
		#region Cyborg
		public static float storedStatsMultCyborg;
		public static int storedPlayerSnacksCyborg;
		#endregion
		#region Demolitionist
		public static float storedStatsMultDemolitionist;
		public static int storedPlayerSnacksDemolitionist;
		#endregion
		#region Dryad
		public static float storedStatsMultDryad;
		public static int storedPlayerSnacksDryad;
		#endregion
		#region Dye Trader
		public static float storedStatsMultDyeTrader;
		public static int storedPlayerSnacksDyeTrader;
		#endregion
		#region Golfer
		// public static float storedStatsMultGolfer;
		// public static int storedPlayerSnacksGolfer;
		#endregion
		#region Guide
		public static float storedStatsMultGuide;
		public static int storedPlayerSnacksGuide;
		#endregion
		#region Mechanic
		public static float storedStatsMultMechanic;
		public static int storedPlayerSnacksMechanic;
		#endregion
		#region Nurse
		public static float storedStatsMultNurse;
		public static int storedPlayerSnacksNurse;
		#endregion
		#region Painter
		public static float storedStatsMultPainter;
		public static int storedPlayerSnacksPainter;
		#endregion
		#region Party Girl
		public static float storedStatsMultPartyGirl;
		public static int storedPlayerSnacksPartyGirl;
		#endregion
		#region Pirate
		public static float storedStatsMultPirate;
		public static int storedPlayerSnacksPirate;
		#endregion
		#region Santa Claus
		public static float storedStatsMultSanta;
		public static int storedPlayerSnacksSanta;
		#endregion
		#region Steampunker
		public static float storedStatsMultSteampunker;
		public static int storedPlayerSnacksSteampunker;
		#endregion
		#region Stylist
		public static float storedStatsMultStylist; // best girl is here
		public static int storedPlayerSnacksStylist; // best girl deserves best food: players!
		#endregion
		#region Zoologist
		// public static float storedStatsMultZoologist;
		// public static int storedPlayerSnacksZoologist;
		#endregion
		#endregion
		#region Town NPCs: Calamity
		#region Bandit
		public static float storedStatsMultBandit;
		public static int storedPlayerSnacksBandit;
		#endregion
		#region Cirrus, the Drunk Princess
		public static float storedStatsMultCirrus;
		public static int storedPlayerSnacksCirrus;
		#endregion
		#endregion

		public override void Initialize()
		{
			storedStatsMultNurse = 1f;
			storedPlayerSnacksNurse = 0;
			storedStatsMultDryad = 1f;
			storedPlayerSnacksDryad = 0;
			storedStatsMultMechanic = 1f;
			storedPlayerSnacksMechanic = 0;
			storedStatsMultSteampunker = 1f;
			storedPlayerSnacksSteampunker = 0;
			storedStatsMultStylist = 1f;
			storedPlayerSnacksStylist = 0;
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"steampunkerMult", storedStatsMultSteampunker},
				{"steampunkerMeals", storedPlayerSnacksSteampunker},
				{"stylistMult", storedStatsMultStylist},
				{"stylistMeals", storedPlayerSnacksStylist},
			};
		}

		public override void Load(TagCompound tag)
		{
			storedStatsMultStylist = tag.GetFloat("stylistMult");
			storedPlayerSnacksStylist = tag.GetInt("stylistMeals");
		}
	}
}
