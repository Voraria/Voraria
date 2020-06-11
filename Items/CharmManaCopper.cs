using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmManaCopper : CharmManaBase<CharmManaCopperBuff>
    {
        public override ItemTier Tier => ItemTier.CopperTin;
        public override int Metal => ItemID.CopperBar;
    }
}
