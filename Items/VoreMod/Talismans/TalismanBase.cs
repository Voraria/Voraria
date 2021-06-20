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
			Item.damage = 1;
			Item.holdStyle = ItemHoldStyleID.None;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.width = 40;
			Item.height = 60;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 0;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.noUseGraphic = true;

			Item.value = 1000;
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(RecipeGroupID.Wood, 5)
				.AddIngredient(Gem, 2)
				.AddTile(TileID.WorkBenches)
				.Register();
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