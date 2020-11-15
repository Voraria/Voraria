using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items.VoreMod.Amulets
{
	public class AmuletThroatTin : AmuletThroatBase
	{
		public override ItemTier Tier => ItemTier.CopperTin;
		public override int Metal => ItemID.TinBar;
		public override int Capacity => 1;
		public override int EscapeLimit => 20;
	}
}
