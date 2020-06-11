using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmLifeCopper : CharmLifeBase<CharmLifeCopperBuff>
    {
        public override ItemTier Tier => ItemTier.CopperTin;
        public override int Metal => ItemID.CopperBar;
    }
}
