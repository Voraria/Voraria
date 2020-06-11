using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmHungerHellstone : CharmHungerBase<CharmHungerHellstoneBuff>
    {
        public override ItemTier Tier => ItemTier.Hellstone;
        public override int Metal => ItemID.HellstoneBar;
    }
}
