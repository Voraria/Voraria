using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmManaGold : CharmManaBase<CharmManaGoldBuff>
    {
        public override ItemTier Tier => ItemTier.GoldPlatinum;
        public override int Metal => ItemID.GoldBar;
    }
}
