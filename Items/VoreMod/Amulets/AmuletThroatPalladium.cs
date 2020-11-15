using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items.VoreMod.Amulets
{
	public class AmuletThroatPalladium : AmuletThroatBase
	{
		public override ItemTier Tier => ItemTier.CobaltPalladium;
		public override int Metal => ItemID.PalladiumBar;
		public override int Capacity => 4;
		public override int EscapeLimit => 120;
	}
}
