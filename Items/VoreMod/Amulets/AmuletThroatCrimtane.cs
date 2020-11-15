using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items.VoreMod.Amulets
{
	public class AmuletThroatCrimtane : AmuletThroatBase
	{
		public override ItemTier Tier => ItemTier.DemoniteCrimtane;
		public override int Metal => ItemID.CrimtaneBar;
		public override int Capacity => 2;
		public override int EscapeLimit => 75;
	}
}
