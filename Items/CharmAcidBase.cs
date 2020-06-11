using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items
{
    public abstract class CharmAcidBase<TBuff> : CharmBase<TBuff> where TBuff : CharmAcidBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Acid;
        public override int Material => ItemID.Gel;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Charm");
            Tooltip.SetDefault("Controls digestion of prey");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (!tooltips.Any(t => t.Name == "Acid Charm"))
            {
                tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Acid Charm", "+" + (int)Tier + " digestion damage"));
            }
        }
    }
}
