using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
    public abstract class CharmAcidBuffBase : CharmBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Acid;

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Acid Charm");
            Description.SetDefault("Prey you eat are automatically digested");
        }
    }
}
