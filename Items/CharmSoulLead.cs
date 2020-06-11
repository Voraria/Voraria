using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public class CharmSoulLead : CharmSoulBase<CharmSoulIronBuff>
    {
        public override ItemTier Tier => ItemTier.IronLead;
        public override int Metal => ItemID.LeadBar;
    }
}
