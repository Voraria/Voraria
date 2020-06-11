using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public class AmuletThroatHellstone : AmuletThroatBase
    {
        public override ItemTier Tier => ItemTier.Hellstone;
        public override int Metal => ItemID.HellstoneBar;
        public override int Capacity => 6;
        public override int EscapeLimit => 120;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.Amethyst, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
