using Terraria;

namespace VoreMod
{
	public class DialogueHelpers
	{
		public static string GetNPCName(int type)
		{
			if (NPC.FindFirstNPC(type) >= 0)
				return Main.npc[NPC.FindFirstNPC(type)].GivenName;

			return "someone";
		}
	}
}
