using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items
{
    public abstract class CharmHungerBase<TBuff> : CharmBase<TBuff> where TBuff : CharmHungerBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Hunger;
        public override int Material => ItemID.Daybloom;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hunger Charm");
            Tooltip.SetDefault("Controls digestion of your predators");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (!tooltips.Any(t => t.Name == "Hunger Charm"))
            {
                tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Hunger Charm", "+" + (int)Tier + " predator digestion damage"));
            }
        }
    }
}
