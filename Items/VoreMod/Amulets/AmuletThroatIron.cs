using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items.VoreMod.Amulets
{
	public class AmuletThroatIron : AmuletThroatBase
	{
		public override ItemTier Tier => ItemTier.IronLead;
		public override int Metal => ItemID.IronBar;
		public override int Capacity => 1;
		public override int EscapeLimit => 35;
	}
}
