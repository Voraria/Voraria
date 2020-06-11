using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmHungerPlatinum : CharmHungerBase<CharmHungerGoldBuff>
    {
        public override ItemTier Tier => ItemTier.GoldPlatinum;
        public override int Metal => ItemID.PlatinumBar;
    }
}
