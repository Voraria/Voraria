using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
	public class DigestingBuff : ModBuff
	{
		public static int BuffType => ModContent.BuffType<DigestingBuff>();

		public override void SetDefaults()
		{
			this.canBeCleared = false;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = true;
			DisplayName.SetDefault("Digesting");
			Description.SetDefault("You are being digested!");
		}
	}
}
