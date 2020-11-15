using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items.VoreMod.Amulets
{
	public class AmuletThroatHellstone : AmuletThroatBase
	{
		public override ItemTier Tier => ItemTier.Hellstone;
		public override int Metal => ItemID.HellstoneBar;
		public override int Capacity => 3;
		public override int EscapeLimit => 100;
	}
}
