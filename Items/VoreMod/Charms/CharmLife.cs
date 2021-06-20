using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items.VoreMod.Charms
{
	public abstract class CharmLife : CharmBase
	{
		public override CharmEffect Effect => CharmEffect.Life;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Life Charm");
			Tooltip.SetDefault("Controls life regeneration from digestion and for nonfatal prey");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (!tooltips.Any(t => t.Name == "Life Charm Self-Heal"))
			{
				tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(Mod, "Life Charm Self-Heal", "+" + (int)Tier + " healing from digestion"));
			}
			if (!tooltips.Any(t => t.Name == "Life Charm Other Heal"))
			{
				tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(Mod, "Life Charm Other Heal", "+" + (int)Tier + " prey healing"));
			}
		}
	}
}
