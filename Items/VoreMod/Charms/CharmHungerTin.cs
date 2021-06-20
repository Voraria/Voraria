using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items.VoreMod.Charms
{
	public class CharmHungerTin : CharmHungerBase
	{
		public override ItemTier Tier => ItemTier.CopperTin;
		public override int Metal => ItemID.TinBar;
	}
}
