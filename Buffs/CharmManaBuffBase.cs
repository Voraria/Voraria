using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
    public abstract class CharmManaBuffBase : CharmBuffBase
    {
        public override CharmEffect Effect => CharmEffect.Mana;

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Mana Charm");
            Description.SetDefault("Digesting prey regenerates mana");
        }
    }
}
