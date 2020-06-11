using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmManaCrimtane : CharmManaBase<CharmManaDemoniteBuff>
    {
        public override ItemTier Tier => ItemTier.DemoniteCrimtane;
        public override int Metal => ItemID.CrimtaneBar;
    }
}
