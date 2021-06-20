using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items.VoreMod.Charms
{
	public abstract class CharmMana : CharmBase
	{
		public override CharmEffect Effect => CharmEffect.Mana;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mana Charm");
			Tooltip.SetDefault("Controls mana regeneration from digestion and for nonfatal prey");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (!tooltips.Any(t => t.Name == "Mana Charm Self-Heal"))
			{
				tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(Mod, "Mana Charm Self-Heal", "+" + (int)Tier + " mana regen from digestion"));
			}
			if (!tooltips.Any(t => t.Name == "Mana Charm Other Heal"))
			{
				tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(Mod, "Mana Charm Other Heal", "+" + (int)Tier + " prey mana regen"));
			}
		}
	}
}
