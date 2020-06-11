using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public class TalismanThroat : TalismanBase
    {
        public override VoreType VoreType => VoreType.Oral;
        public override int Gem => ItemID.Amethyst;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows you to feed yourself to other creatures.");
            DisplayName.SetDefault("Throat Talisman");
        }

        public override void OnHit(VoreEntity player, VoreEntity target)
        {
            if (target.CanSwallow(player)) target.Swallow(player);
            else Main.NewText(target.GetName() + " cannot swallow you.", Colors.RarityYellow);
        }
    }
}