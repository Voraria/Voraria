using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod.Items
{
    public abstract class CharmManaBase<TBuff> : CharmBase<TBuff> where TBuff : CharmManaBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Mana;
        public override int Material => ItemID.ManaCrystal;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Charm");
            Tooltip.SetDefault("Controls mana regeneration from digestion and for nonfatal prey");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (!tooltips.Any(t => t.Name == "Mana Charm Self-Heal"))
            {
                tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Mana Charm Self-Heal", "+" + (int)Tier + " mana regen from digestion"));
            }
            if (!tooltips.Any(t => t.Name == "Mana Charm Other Heal"))
            {
                tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Mana Charm Other Heal", "+" + (int)Tier + " prey mana regen"));
            }
        }
    }
}
