using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace VoreMod.Items.VoreMod.Amulets
{
	public abstract class AmuletBase : ModItem
	{
		public abstract ItemTier Tier { get; }
		public abstract VoreType VoreType { get; }
		public abstract int Gem { get; }
		public abstract int Metal { get; }
		public abstract int Capacity { get; }
		public abstract int EscapeLimit { get; }

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

			switch (Tier)
			{
				case ItemTier.CopperTin:
					item.rare = ItemRarityID.White;
					item.value = 1500;
					break;
				case ItemTier.IronLead:
					item.rare = ItemRarityID.White;
					item.value = 2250;
					break;
				case ItemTier.SilverTungsten:
					item.rare = ItemRarityID.White;
					item.value = 3750;
					break;
				case ItemTier.GoldPlatinum:
					item.rare = ItemRarityID.Blue;
					item.value = 6750;
					break;
				case ItemTier.DemoniteCrimtane:
					item.rare = ItemRarityID.Blue;
					item.value = 15750;
					break;
				case ItemTier.Hellstone:
					item.rare = ItemRarityID.Orange;
					item.value = 20750;
					break;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddTile(TileID.Anvils);
			recipe.AddIngredient(Metal, 5);
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
			if (!tooltips.Any(t => t.Name == "Capacity"))
			{
				tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Capacity", "" + Capacity + " prey capacity"));
			}
			if (!tooltips.Any(t => t.Name == "Escape Limit"))
			{
				tooltips.Insert(tooltips.FindIndex(t => t.Name == "ItemName") + 1, new TooltipLine(mod, "Escape Limit", "+" + EscapeLimit + "% escape limit"));
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
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

		public override bool CanUseItem(Player player)
		{
			SetDefaults();
			if (player.altFunctionUse == 2)
			{
				item.damage = 0;
				item.holdStyle = ItemHoldStyleID.HoldingUp;
				item.useStyle = ItemUseStyleID.EatingUsing;
			}
			return base.CanUseItem(player);
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				OnUse(player);
				SetDefaults();
				return true;
			}
			return base.UseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (player.altFunctionUse != 2) OnHit(player, target);
		}

		public override void OnHitPvp(Player player, Player target, int damage, bool crit)
		{
			if (player.altFunctionUse != 2) OnHit(player, target);
		}

		public abstract void OnUse(VoreEntity player);
		public abstract void OnHit(VoreEntity player, VoreEntity target);
	}
}