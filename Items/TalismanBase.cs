using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace VoreMod.Items
{
    public abstract class TalismanBase : ModItem
    {
        public abstract VoreType VoreType { get; }
        public abstract int Gem { get; }

        public override void SetDefaults()
        {
            item.damage = 1;
            item.holdStyle = ItemHoldStyleID.Default;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.width = 40;
            item.height = 60;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 0;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noUseGraphic = true;

            item.value = 1000;
            item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 5);
            recipe.AddIngredient(Gem, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (tooltips.Count > 0)
            {
                for (int i = tooltips.Count - 1; i >= 0; i--)
                {
                    switch (tooltips[i].Name)
                    {
                        case "Damage":
                        case "CritChance":
                        case "Speed":
                        case "Knockback":
                            tooltips.RemoveAt(i);
                            break;
                    }
                }
            }
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            damage = 0;
            crit = false;
        }

        public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
        {
            damage = 0;
            crit = false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            OnHit(player, target);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            OnHit(player, target);
        }

        public abstract void OnHit(VoreEntity player, VoreEntity target);
    }
}