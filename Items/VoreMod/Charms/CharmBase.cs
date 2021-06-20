using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VoreMod.Buffs;

namespace VoreMod.Items.VoreMod.Charms
{
	public abstract class CharmBase : ModItem
	{
		public abstract CharmEffect Effect { get; }

		public override void SetDefaults()
		{
			Item.holdStyle = ItemHoldStyleID.None;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.UseSound = SoundID.Item28;
			Item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}