using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmManaTin : CharmManaBase<CharmManaCopperBuff>
    {
        public override ItemTier Tier => ItemTier.CopperTin;
        public override int Metal => ItemID.TinBar;
    }
}
