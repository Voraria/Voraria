using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items
{
    public abstract class CharmLifeBase<TBuff> : CharmBase<TBuff> where TBuff : CharmLifeBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Life;
        public override int Material => ItemID.LifeCrystal;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Charm");
            Tooltip.SetDefault("Controls life regeneration from digestion and for nonfatal prey");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (!tooltips.Any(t => t.Name == "Life Charm Self-Heal"))
            {
                tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Life Charm Self-Heal", "+" + (int)Tier + " healing from digestion"));
            }
            if (!tooltips.Any(t => t.Name == "Life Charm Other Heal"))
            {
                tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Life Charm Other Heal", "+" + (int)Tier + " prey healing"));
            }
        }
    }
}
