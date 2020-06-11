using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public abstract class AmuletThroatBase : AmuletBase
    {
        public override VoreType VoreType => VoreType.Oral;
        public override int Gem => ItemID.Amethyst;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throat Amulet");
            Tooltip.SetDefault("Allows you to swallow other creatures\nUse <right> to regurgitate");
        }

        public override void OnUse(VoreEntity player)
        {
            if (player.CanRegurgitateAny())
            {
                player.RegurgitateLast();
            }
            else
            {
                Main.NewText("You have no prey to regurgitate.", Colors.RarityYellow);
            }
        }

        public override void OnHit(VoreEntity player, VoreEntity target)
        {
            if (player.CanSwallow(target))
            {
                player.Swallow(target);
            }
            else
            {
                Main.NewText("You cannot swallow any more prey.", Colors.RarityYellow);
            }
        }
    }
}
