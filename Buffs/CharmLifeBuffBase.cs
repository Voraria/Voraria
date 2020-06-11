using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
    public abstract class CharmLifeBuffBase : CharmBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Life;

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Life Charm");
            Description.SetDefault("Digesting prey regenerates health");
        }
    }
}
