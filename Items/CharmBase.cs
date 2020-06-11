using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items
{
    public abstract class CharmBase<TBuff> : ModItem where TBuff : CharmBuffBase
    {
        public abstract CharmEffect Effect { get; }
        public abstract ItemTier Tier { get; }
        public abstract int Material { get; }
        public abstract int Metal { get; }

        public override void SetDefaults()
        {
            item.holdStyle = ItemHoldStyleID.Default;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.UseSound = SoundID.Item28;
            item.autoReuse = false;
            item.buffType = ModContent.BuffType<TBuff>();
            item.buffTime = 60 * 60 * 60 * 24;

            switch (Tier)
            {
                case ItemTier.CopperTin:
                    item.rare = ItemRarityID.Gray;
                    item.value = 1500;
                    break;
                case ItemTier.IronLead:
                    item.rare = ItemRarityID.White;
                    item.value = 2250;
                    break;
                case ItemTier.SilverTungsten:
                    item.rare = ItemRarityID.Blue;
                    item.value = 3750;
                    break;
                case ItemTier.GoldPlatinum:
                    item.rare = ItemRarityID.Green;
                    item.value = 6750;
                    break;
                case ItemTier.DemoniteCrimtane:
                    item.rare = ItemRarityID.Orange;
                    item.value = 15750;
                    break;
                case ItemTier.Hellstone:
                    item.rare = ItemRarityID.LightRed;
                    item.value = 20750;
                    break;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddIngredient(Metal, 3);
            recipe.AddIngredient(Material, 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}