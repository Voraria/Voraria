using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items.VoreMod.Amulets
{
	public class AmuletThroatSilver : AmuletThroatBase
	{
		public override ItemTier Tier => ItemTier.SilverTungsten;
		public override int Metal => ItemID.SilverBar;
		public override int Capacity => 1;
		public override int EscapeLimit => 50;
	}
}
