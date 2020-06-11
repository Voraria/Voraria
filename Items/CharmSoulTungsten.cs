using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmSoulTungsten : CharmSoulBase<CharmSoulSilverBuff>
    {
        public override ItemTier Tier => ItemTier.SilverTungsten;
        public override int Metal => ItemID.TungstenBar;
    }
}
