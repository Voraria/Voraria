using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public class AmuletThroatTungsten : AmuletThroatBase
    {
        public override ItemTier Tier => ItemTier.SilverTungsten;
        public override int Metal => ItemID.TungstenBar;
        public override int Capacity => 3;
        public override int EscapeLimit => 72;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.TungstenBar, 5);
            recipe.AddIngredient(ItemID.Amethyst, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
