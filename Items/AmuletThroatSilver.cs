using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public class AmuletThroatSilver : AmuletThroatBase
    {
        public override ItemTier Tier => ItemTier.SilverTungsten;
        public override int Metal => ItemID.SilverBar;
        public override int Capacity => 3;
        public override int EscapeLimit => 60;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.SilverBar, 5);
            recipe.AddIngredient(ItemID.Amethyst, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
