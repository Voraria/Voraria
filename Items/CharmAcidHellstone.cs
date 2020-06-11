using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmAcidHellstone : CharmAcidBase<CharmAcidHellstoneBuff>
    {
        public override ItemTier Tier => ItemTier.Hellstone;
        public override int Metal => ItemID.HellstoneBar;
    }
}
