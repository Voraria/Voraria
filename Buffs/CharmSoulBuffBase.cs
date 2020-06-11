using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
    public abstract class CharmSoulBuffBase : CharmBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Soul;

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Soul Charm");
            Description.SetDefault("Digested prey may drop a soul");
        }
    }
}
