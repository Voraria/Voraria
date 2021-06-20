using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items.VoreMod.Charms
{
	public class CharmAcid : CharmBase
	{
		public override CharmEffect Effect => CharmEffect.Acid;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acid Charm");
			Tooltip.SetDefault("+10% digestion damage");
		}
	}
}
