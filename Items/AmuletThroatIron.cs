using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public class AmuletThroatIron : AmuletThroatBase
    {
        public override ItemTier Tier => ItemTier.IronLead;
        public override int Metal => ItemID.IronBar;
        public override int Capacity => 2;
        public override int EscapeLimit => 40;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.Amethyst, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
