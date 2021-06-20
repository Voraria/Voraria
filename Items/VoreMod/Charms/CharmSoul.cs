using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items.VoreMod.Charms
{
	public abstract class CharmSoul : CharmBase
	{
		public override CharmEffect Effect => CharmEffect.Soul;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Charm");
			Tooltip.SetDefault("Digesting certain enemies may grant soul essence");
		}
	}
}
