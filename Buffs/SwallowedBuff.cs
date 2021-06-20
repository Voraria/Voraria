using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
	public class SwallowedBuff : ModBuff
	{
		public static int BuffType => ModContent.BuffType<SwallowedBuff>();

		public override void SetDefaults()
		{
			this.canBeCleared = false;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = false;
			DisplayName.SetDefault("Swallowed");
			Description.SetDefault("You have been swallowed!");
		}
	}
}
