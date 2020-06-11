using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public class AmuletThroatTin : AmuletThroatBase
    {
        public override ItemTier Tier => ItemTier.CopperTin;
        public override int Metal => ItemID.TinBar;
        public override int Capacity => 1;
        public override int EscapeLimit => 24;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.TinBar, 5);
            recipe.AddIngredient(ItemID.Amethyst, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
