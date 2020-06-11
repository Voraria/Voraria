using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
    public abstract class CharmHungerBuffBase : CharmBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Hunger;

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Hunger Charm");
            Description.SetDefault("Predators always digest you");
        }
    }
}
